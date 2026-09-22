using System;
using System.Diagnostics;
using System.IO;
using System.Reflection;
using System.Runtime.InteropServices;
using System.Security.AccessControl;
using System.Security.Cryptography;
using System.Security.Principal;
using System.ServiceProcess;
using System.Text;
using Microsoft.Win32;
using MPatcher.NetworkAssist;

namespace MachineCraftMPatcherInstaller
{
    internal static class NetworkAssistSetup
    {
        private static string BaseRoot { get { return Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.ProgramFiles), "MPatcher", "LegacyZapret"); } }
        private static RegistryKey RegistryRoot(bool write)
        {
            using (RegistryKey machine = RegistryKey.OpenBaseKey(RegistryHive.LocalMachine, RegistryView.Registry64))
                return write ? machine.CreateSubKey(AssistContract.RegistryPath) : machine.OpenSubKey(AssistContract.RegistryPath);
        }
        private static void RequireAdmin()
        {
            if (!Environment.Is64BitOperatingSystem) throw new PlatformNotSupportedException("Zapret package requires Windows x64.");
            if (!new WindowsPrincipal(WindowsIdentity.GetCurrent()).IsInRole(WindowsBuiltInRole.Administrator))
                throw new InvalidOperationException("Run this network TEST installer as administrator.");
        }
        internal static void Install(string gameRoot)
        {
            RequireAdmin();
            string game = Path.GetFullPath(Path.Combine(gameRoot, "McnCraft.exe"));
            if (Hash(game) != AssistContract.GameHash) throw new InvalidDataException("Unsupported McnCraft.exe for the network helper.");
            string target = Path.Combine(BaseRoot, AssistPackage.Id);
            string binary = Path.Combine(target, "MPatcherNetworkAssist.exe");
            string previous = null;
            bool createdService = false;
            bool registrationExisted = false;
            string registration = HashText(game.ToUpperInvariant());
            using (RegistryKey config = RegistryRoot(false))
            {
                if (config != null)
                {
                    previous = config.GetValue("PackageRoot") as string;
                    using (RegistryKey games = config.OpenSubKey("Games")) registrationExisted = games != null && games.GetValue(registration) != null;
                }
            }
            bool existed = Exists();
            if (existed)
            {
                if (string.IsNullOrEmpty(previous)) throw new InvalidOperationException("Existing network service has no MPatcher ownership record.");
                ValidateOwnedPath(previous);
                AssertServiceBinary(previous);
                if (!string.Equals(previous, target, StringComparison.OrdinalIgnoreCase))
                    foreach (Process p in Process.GetProcessesByName("McnCraft")) using (p)
                        if (!p.HasExited) throw new InvalidOperationException("Close other MachineCraft windows before updating the network helper.");
            }
            string productRoot = Path.GetDirectoryName(BaseRoot);
            Directory.CreateDirectory(productRoot);
            if ((File.GetAttributes(productRoot) & FileAttributes.ReparsePoint) != 0) throw new IOException("Invalid MPatcher directory.");
            ProtectDirectory(productRoot);
            EnsureProtectedDirectory(BaseRoot);
            EnsureProtectedDirectory(target);
            // Embedded, pinned payload only; never execute from a game or temporary writable directory.
            foreach (string entry in AssistPackage.Files)
            {
                string[] pair = entry.Split('|');
                string destination = Path.Combine(target, pair[0]);
                if (File.Exists(destination))
                {
                    if ((File.GetAttributes(destination) & FileAttributes.ReparsePoint) != 0 || Hash(destination) != pair[1])
                        throw new InvalidDataException("Changed helper payload: " + pair[0]);
                    FileSecurity permissions = new FileSecurity();
                    permissions.SetOwner(new SecurityIdentifier(WellKnownSidType.BuiltinAdministratorsSid, null));
                    permissions.SetAccessRuleProtection(false, false);
                    File.SetAccessControl(destination, permissions);
                    continue;
                }
                string stage = destination + ".new";
                if (File.Exists(stage)) throw new IOException("Incomplete previous helper staging: " + stage);
                try
                {
                    using (Stream source = Assembly.GetExecutingAssembly().GetManifestResourceStream("MachineCraftMPatcherInstaller.NetworkAssist." + pair[0]))
                    using (FileStream output = new FileStream(stage, FileMode.CreateNew, FileAccess.Write, FileShare.None))
                    {
                        if (source == null) throw new InvalidDataException("Missing embedded network payload.");
                        source.CopyTo(output);
                    }
                    if (Hash(stage) != pair[1]) throw new InvalidDataException("Network payload hash mismatch.");
                    File.Move(stage, destination);
                }
                finally { if (File.Exists(stage)) File.Delete(stage); }
            }
            File.WriteAllLines(Path.Combine(target, "owned-files.txt"), AssistPackage.Files);
            try
            {
                if (!existed) { ConfigureService(binary, true); createdService = true; }
                else if (!string.Equals(previous, target, StringComparison.OrdinalIgnoreCase)) { Stop(); ConfigureService(binary, false); }
                using (RegistryKey config = RegistryRoot(true))
                {
                    config.SetValue("PackageRoot", target);
                    using (RegistryKey games = config.CreateSubKey("Games")) games.SetValue(registration, game);
                }
                using (ServiceController service = new ServiceController(AssistContract.ServiceName))
                {
                    if (service.Status != ServiceControllerStatus.Running) { service.Start(); service.WaitForStatus(ServiceControllerStatus.Running, TimeSpan.FromSeconds(15)); }
                }
            }
            catch
            {
                if (createdService) { try { Stop(); DeleteServiceEntry(); } catch { } }
                else if (previous != null && previous != target) { try { Stop(); ConfigureService(Path.Combine(previous, "MPatcherNetworkAssist.exe"), false); using (ServiceController s = new ServiceController(AssistContract.ServiceName)) s.Start(); } catch { } }
                using (RegistryKey config = RegistryRoot(true))
                {
                    if (previous != null) config.SetValue("PackageRoot", previous); else config.DeleteValue("PackageRoot", false);
                    if (!registrationExisted) using (RegistryKey games = config.CreateSubKey("Games")) games.DeleteValue(registration, false);
                }
                throw;
            }
        }
        internal static void Uninstall(string gameRoot)
        {
            RequireAdmin();
            string game = Path.GetFullPath(Path.Combine(gameRoot, "McnCraft.exe"));
            using (RegistryKey config = RegistryRoot(false)) { if (config == null) return; }
            string package;
            using (RegistryKey config = RegistryRoot(true))
            {
                package = config.GetValue("PackageRoot") as string;
                using (RegistryKey games = config.CreateSubKey("Games"))
                {
                    string id = HashText(game.ToUpperInvariant());
                    games.DeleteValue(id, false);
                    if (games.ValueCount != 0) return;
                }
            }
            if (package != null) { ValidateOwnedPath(package); if (Exists()) { AssertServiceBinary(package); Stop(); DeleteServiceEntry(); } }
            // Only this product's protected directory. Never delete a shared WinDivert driver service.
            if (Directory.Exists(BaseRoot))
            {
                ValidateOwnedPath(BaseRoot);
                foreach (string directory in Directory.GetDirectories(BaseRoot))
                {
                    ValidateOwnedPath(directory);
                    string owned = Path.Combine(directory, "owned-files.txt");
                    if (!File.Exists(owned) || (File.GetAttributes(owned) & FileAttributes.ReparsePoint) != 0) continue;
                    foreach (string entry in File.ReadAllLines(owned))
                    {
                        string[] pair = entry.Split('|');
                        if (pair.Length != 2 || Path.GetFileName(pair[0]) != pair[0] || pair[0].IndexOf(':') >= 0) continue;
                        string file = Path.Combine(directory, pair[0]);
                        if (File.Exists(file) && (File.GetAttributes(file) & FileAttributes.ReparsePoint) == 0 && Hash(file) == pair[1]) File.Delete(file);
                    }
                    File.Delete(owned);
                    // Retain logs and modified/unrecognized files for diagnosis.
                    if (Directory.GetFileSystemEntries(directory).Length == 0) Directory.Delete(directory, false);
                }
                if (Directory.GetFileSystemEntries(BaseRoot).Length == 0) Directory.Delete(BaseRoot, false);
            }
            using (RegistryKey machine = RegistryKey.OpenBaseKey(RegistryHive.LocalMachine, RegistryView.Registry64)) machine.DeleteSubKeyTree(AssistContract.RegistryPath, false);
        }
        private static void ValidateOwnedPath(string path)
        {
            string full = Path.GetFullPath(path).TrimEnd(Path.DirectorySeparatorChar);
            if (full != BaseRoot && !full.StartsWith(BaseRoot + Path.DirectorySeparatorChar, StringComparison.OrdinalIgnoreCase)) throw new IOException("Network helper path outside protected root.");
            for (DirectoryInfo d = new DirectoryInfo(full); d != null; d = d.Parent)
                if (d.Exists && (d.Attributes & FileAttributes.ReparsePoint) != 0) throw new IOException("Network helper path contains a reparse point.");
        }
        private static void EnsureProtectedDirectory(string path)
        {
            ValidateOwnedPath(path);
            Directory.CreateDirectory(path);
            ProtectDirectory(path);
        }
        private static void ProtectDirectory(string path)
        {
            DirectorySecurity acl = new DirectorySecurity(); acl.SetAccessRuleProtection(true, false);
            acl.SetOwner(new SecurityIdentifier(WellKnownSidType.BuiltinAdministratorsSid, null));
            InheritanceFlags inherit = InheritanceFlags.ContainerInherit | InheritanceFlags.ObjectInherit;
            foreach (WellKnownSidType sid in new WellKnownSidType[] { WellKnownSidType.LocalSystemSid, WellKnownSidType.BuiltinAdministratorsSid })
                acl.AddAccessRule(new FileSystemAccessRule(new SecurityIdentifier(sid, null), FileSystemRights.FullControl, inherit, PropagationFlags.None, AccessControlType.Allow));
            acl.AddAccessRule(new FileSystemAccessRule(new SecurityIdentifier(WellKnownSidType.BuiltinUsersSid, null), FileSystemRights.ReadAndExecute, inherit, PropagationFlags.None, AccessControlType.Allow));
            Directory.SetAccessControl(path, acl);
        }
        private static void AssertServiceBinary(string root)
        {
            using (RegistryKey machine = RegistryKey.OpenBaseKey(RegistryHive.LocalMachine, RegistryView.Registry64))
            using (RegistryKey key = machine.OpenSubKey(@"SYSTEM\CurrentControlSet\Services\" + AssistContract.ServiceName))
                if (key == null || !string.Equals(key.GetValue("ImagePath") as string, "\"" + Path.Combine(root, "MPatcherNetworkAssist.exe") + "\"", StringComparison.OrdinalIgnoreCase))
                    throw new InvalidOperationException("Network service ownership mismatch.");
        }
        private static bool Exists()
        {
            foreach (ServiceController s in ServiceController.GetServices()) using (s) if (s.ServiceName == AssistContract.ServiceName) return true;
            return false;
        }
        private static void Stop()
        {
            using (ServiceController s = new ServiceController(AssistContract.ServiceName))
                if (s.Status != ServiceControllerStatus.Stopped) { s.Stop(); s.WaitForStatus(ServiceControllerStatus.Stopped, TimeSpan.FromSeconds(15)); }
        }
        private static void ConfigureService(string binary, bool create)
        {
            IntPtr scm = OpenSCManager(null, null, 3); if (scm == IntPtr.Zero) throw new System.ComponentModel.Win32Exception();
            IntPtr service = IntPtr.Zero;
            try
            {
                if (create) service = CreateService(scm, AssistContract.ServiceName, "MPatcher Legacy network helper", 0xF01FF, 0x10, 2, 1, "\"" + binary + "\"", null, IntPtr.Zero, null, "LocalSystem", null);
                else
                {
                    service = OpenService(scm, AssistContract.ServiceName, 0xF01FF);
                    if (service != IntPtr.Zero && !ChangeServiceConfig(service, 0xFFFFFFFF, 2, 0xFFFFFFFF, "\"" + binary + "\"", null, IntPtr.Zero, null, null, null, null)) throw new System.ComponentModel.Win32Exception();
                }
                if (service == IntPtr.Zero) throw new System.ComponentModel.Win32Exception();
            }
            finally { if (service != IntPtr.Zero) CloseServiceHandle(service); CloseServiceHandle(scm); }
        }
        private static void DeleteServiceEntry()
        {
            IntPtr scm = OpenSCManager(null, null, 1), service = IntPtr.Zero;
            try { service = OpenService(scm, AssistContract.ServiceName, 0x10000); if (service == IntPtr.Zero || !DeleteService(service)) throw new System.ComponentModel.Win32Exception(); }
            finally { if (service != IntPtr.Zero) CloseServiceHandle(service); if (scm != IntPtr.Zero) CloseServiceHandle(scm); }
        }
        private static string Hash(string path) { using (SHA256 sha = SHA256.Create()) using (FileStream file = File.OpenRead(path)) return BitConverter.ToString(sha.ComputeHash(file)).Replace("-", ""); }
        private static string HashText(string value) { using (SHA256 sha = SHA256.Create()) return BitConverter.ToString(sha.ComputeHash(Encoding.UTF8.GetBytes(value))).Replace("-", ""); }
        [DllImport("advapi32.dll", CharSet = CharSet.Unicode, SetLastError = true)] private static extern IntPtr OpenSCManager(string machine, string database, uint access);
        [DllImport("advapi32.dll", CharSet = CharSet.Unicode, SetLastError = true)] private static extern IntPtr OpenService(IntPtr manager, string name, uint access);
        [DllImport("advapi32.dll", CharSet = CharSet.Unicode, SetLastError = true)] private static extern IntPtr CreateService(IntPtr manager, string name, string display, uint access, uint type, uint start, uint error, string binary, string group, IntPtr tag, string dependencies, string user, string password);
        [DllImport("advapi32.dll", CharSet = CharSet.Unicode, SetLastError = true)] private static extern bool ChangeServiceConfig(IntPtr service, uint type, uint start, uint error, string binary, string group, IntPtr tag, string dependencies, string user, string password, string display);
        [DllImport("advapi32.dll", SetLastError = true)] private static extern bool DeleteService(IntPtr service);
        [DllImport("advapi32.dll")] private static extern bool CloseServiceHandle(IntPtr handle);
    }
}
