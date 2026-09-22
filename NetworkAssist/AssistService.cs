using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.IO.Pipes;
using System.Runtime.InteropServices;
using System.Security.AccessControl;
using System.Security.Cryptography;
using System.Security.Principal;
using System.ServiceProcess;
using System.Text;
using System.Threading;
using Microsoft.Win32;
using Microsoft.Win32.SafeHandles;

namespace MPatcher.NetworkAssist
{
    internal sealed class AssistService : ServiceBase
    {
        private readonly object gate = new object();
        private readonly AssistLeases leases = new AssistLeases();
        private readonly ManualResetEvent stopped = new ManualResetEvent(false);
        private Thread listener;
        private Timer timer;
        private Process child;
        private IntPtr job;
        private string status = "OFF";
        private DateTime retryAt;
        private string lastLogged;
        private NamedPipeServerStream activePipe;
        private static readonly object logGate = new object();
        private static string Root { get { return AppDomain.CurrentDomain.BaseDirectory; } }
        private static string LogPath { get { return Path.Combine(Root, "network-assist.log"); } }

        private static int Main(string[] args)
        {
            if (args.Length == 1 && args[0] == "--verify")
            {
                try { VerifyPayload(); return 0; } catch { return 1; }
            }
            if (args.Length != 0) return 2;
            ServiceBase.Run(new AssistService());
            return 0;
        }
        internal AssistService() { ServiceName = AssistContract.ServiceName; CanStop = true; CanShutdown = true; AutoLog = false; }
        protected override void OnStart(string[] args)
        {
            VerifyPayload();
            Log("SERVICE_START version=1 idle=true profile=legacy-services-only");
            listener = new Thread(Listen); listener.IsBackground = true; listener.Start();
            timer = new Timer(Tick, null, 1000, 1000);
        }
        protected override void OnShutdown() { OnStop(); }
        protected override void OnStop()
        {
            stopped.Set();
            if (timer != null) timer.Dispose();
            lock (gate) { leases.Clear(); StopChild("service-stop"); }
            try { if (activePipe != null) activePipe.Dispose(); } catch { }
            if (listener != null) listener.Join(3000);
            Log("SERVICE_STOP");
        }
        private void Listen()
        {
            while (!stopped.WaitOne(0))
            {
                try
                {
                    PipeSecurity security = new PipeSecurity();
                    security.SetAccessRuleProtection(true, false);
                    security.AddAccessRule(new PipeAccessRule(new SecurityIdentifier(WellKnownSidType.NetworkSid, null), PipeAccessRights.FullControl, AccessControlType.Deny));
                    security.AddAccessRule(new PipeAccessRule(new SecurityIdentifier(WellKnownSidType.LocalSystemSid, null), PipeAccessRights.FullControl, AccessControlType.Allow));
                    security.AddAccessRule(new PipeAccessRule(new SecurityIdentifier(WellKnownSidType.BuiltinAdministratorsSid, null), PipeAccessRights.FullControl, AccessControlType.Allow));
                    security.AddAccessRule(new PipeAccessRule(new SecurityIdentifier(WellKnownSidType.AuthenticatedUserSid, null), PipeAccessRights.ReadWrite, AccessControlType.Allow));
                    using (NamedPipeServerStream pipe = new NamedPipeServerStream(AssistContract.PipeName, PipeDirection.InOut, 1,
                        PipeTransmissionMode.Byte, PipeOptions.Asynchronous, 256, 256, security))
                    {
                        activePipe = pipe;
                        IAsyncResult accept = pipe.BeginWaitForConnection(null, null);
                        int signaled = WaitHandle.WaitAny(new WaitHandle[] { stopped, accept.AsyncWaitHandle });
                        if (signaled == 0) break;
                        pipe.EndWaitForConnection(accept); accept.AsyncWaitHandle.Close();
                        // Bound even malformed/local clients so one request cannot occupy the broker.
                        using (Timer timeout = new Timer(delegate { try { pipe.Dispose(); } catch { } }, null, 2500, Timeout.Infinite))
                        {
                            uint pid;
                            if (!GetNamedPipeClientProcessId(pipe.SafePipeHandle, out pid)) continue;
                            int command = pipe.ReadByte();
                            string result;
                            lock (gate) { result = Command((int)pid, command); }
                            byte[] answer = Encoding.ASCII.GetBytes(result + "\n");
                            pipe.Write(answer, 0, answer.Length); pipe.Flush();
                        }
                    }
                }
                catch (Exception error) { if (!stopped.WaitOne(0)) { Log("PIPE_ERROR " + error.GetType().Name); stopped.WaitOne(250); } }
                finally { activePipe = null; }
            }
        }
        private string Command(int pid, int command)
        {
            if (command != '1' && command != '0' && command != '?') return "REJECTED_COMMAND";
            long generation;
            if (!Authorized(pid, out generation)) return "REJECTED_PROCESS";
            if (command == '1') leases.Refresh(pid, generation, DateTime.UtcNow);
            if (command == '0') leases.Remove(pid);
            Reconcile();
            return status + " leases=" + leases.Count;
        }
        private static bool Authorized(int pid, out long generation)
        {
            generation = 0;
            try
            {
                using (Process process = Process.GetProcessById(pid))
                {
                    if (process.HasExited || process.SessionId == 0) return false;
                    string path = Path.GetFullPath(process.MainModule.FileName);
                    if (!string.Equals(Path.GetFileName(path), "McnCraft.exe", StringComparison.OrdinalIgnoreCase)) return false;
                    using (RegistryKey machine = RegistryKey.OpenBaseKey(RegistryHive.LocalMachine, RegistryView.Registry64))
                    using (RegistryKey registrations = machine.OpenSubKey(AssistContract.RegistryPath + @"\Games"))
                    {
                        bool found = false;
                        if (registrations != null) foreach (string name in registrations.GetValueNames())
                            if (string.Equals(registrations.GetValue(name) as string, path, StringComparison.OrdinalIgnoreCase)) { found = true; break; }
                        if (!found) return false;
                    }
                    // The protocol never accepts a PID, path, command line, DLL or profile from the caller.
                    if (!string.Equals(Hash(path), AssistContract.GameHash, StringComparison.OrdinalIgnoreCase)) return false;
                    generation = process.StartTime.ToUniversalTime().Ticks;
                    return true;
                }
            }
            catch { return false; }
        }
        private static bool Alive(int pid, long generation)
        {
            try { using (Process p = Process.GetProcessById(pid)) return !p.HasExited && p.StartTime.ToUniversalTime().Ticks == generation; }
            catch { return false; }
        }
        private void Tick(object unused)
        {
            try { lock (gate) { if (!stopped.WaitOne(0)) Reconcile(); } }
            catch (Exception error) { lock (gate) { StopChild("tick-error"); status = "ERROR"; retryAt = DateTime.UtcNow.AddSeconds(30); } Log("TICK_ERROR " + error); }
        }
        private void Reconcile()
        {
            leases.Prune(DateTime.UtcNow, Alive);
            if (leases.Count == 0) { StopChild("no-enabled-games"); SetStatus("OFF"); return; }
            if (child != null && child.HasExited)
            {
                int exit = child.ExitCode; StopChild("exit=" + exit); retryAt = DateTime.UtcNow.AddSeconds(30); SetStatus("ERROR_EXIT_" + exit); return;
            }
            if (OtherFilterRunning()) { StopChild("external-zapret"); SetStatus("EXTERNAL_ZAPRET"); return; }
            if (child != null) { SetStatus("RUNNING"); return; }
            if (DateTime.UtcNow < retryAt) return;
            try
            {
                VerifyPayload();
                job = CreateJobObject(IntPtr.Zero, null);
                if (job == IntPtr.Zero) throw new System.ComponentModel.Win32Exception();
                JobInformation limits = new JobInformation(); limits.BasicLimitInformation.LimitFlags = 0x2000;
                if (!SetInformationJobObject(job, 9, ref limits, (uint)Marshal.SizeOf(typeof(JobInformation)))) throw new System.ComponentModel.Win32Exception();
                ProcessStartInfo info = new ProcessStartInfo(Path.Combine(Root, "winws.exe"), AssistContract.Arguments);
                info.WorkingDirectory = Root; info.UseShellExecute = false; info.CreateNoWindow = true;
                info.RedirectStandardOutput = true; info.RedirectStandardError = true;
                child = new Process(); child.StartInfo = info;
                child.OutputDataReceived += delegate(object sender, DataReceivedEventArgs e) { if (!string.IsNullOrEmpty(e.Data)) Log("WINWS " + e.Data); };
                child.ErrorDataReceived += delegate(object sender, DataReceivedEventArgs e) { if (!string.IsNullOrEmpty(e.Data)) Log("WINWS_ERR " + e.Data); };
                child.Start();
                if (!AssignProcessToJobObject(job, child.Handle)) throw new System.ComponentModel.Win32Exception();
                child.BeginOutputReadLine(); child.BeginErrorReadLine();
                Log("CHILD_START pid=" + child.Id + " leases=" + leases.Count + " " + AssistContract.Arguments);
                SetStatus("STARTING");
            }
            catch (Exception error) { StopChild("start-failed"); retryAt = DateTime.UtcNow.AddSeconds(30); SetStatus("ERROR_START"); Log("START_ERROR " + error); }
        }
        private bool OtherFilterRunning()
        {
            foreach (string name in new string[] { "winws", "winws2" })
            foreach (Process p in Process.GetProcessesByName(name))
                using (p) { if (child == null || p.Id != child.Id) return true; }
            return false;
        }
        private void StopChild(string reason)
        {
            if (child != null)
            {
                try { if (!child.HasExited) { child.Kill(); child.WaitForExit(3000); } } catch (Exception e) { Log("CHILD_STOP_ERROR " + e.GetType().Name); }
                child.Dispose(); child = null; Log("CHILD_STOP " + reason);
            }
            if (job != IntPtr.Zero) { CloseHandle(job); job = IntPtr.Zero; }
        }
        private void SetStatus(string value) { status = value; if (lastLogged != value) { Log("STATE " + value + " leases=" + leases.Count); lastLogged = value; } }
        internal static string Hash(string file)
        {
            using (SHA256 sha = SHA256.Create()) using (FileStream stream = File.OpenRead(file))
                return BitConverter.ToString(sha.ComputeHash(stream)).Replace("-", "");
        }
        private static void VerifyPayload()
        {
            foreach (string entry in AssistPayload.Files)
            {
                string[] parts = entry.Split('|');
                string file = Path.Combine(Root, parts[0]);
                if ((File.GetAttributes(file) & FileAttributes.ReparsePoint) != 0 || Hash(file) != parts[1])
                    throw new InvalidDataException("Network helper integrity: " + parts[0]);
            }
        }
        private static void Log(string message)
        {
            try { lock (logGate) { if (File.Exists(LogPath) && new FileInfo(LogPath).Length > 2 * 1024 * 1024) { File.Delete(LogPath + ".previous"); File.Move(LogPath, LogPath + ".previous"); } File.AppendAllText(LogPath, DateTime.UtcNow.ToString("o") + " " + message.Replace("\r", " ").Replace("\n", " ") + Environment.NewLine); } } catch { }
        }
        [StructLayout(LayoutKind.Sequential)] private struct BasicJobInformation { public long PerProcessUserTimeLimit, PerJobUserTimeLimit; public uint LimitFlags; public UIntPtr MinimumWorkingSetSize, MaximumWorkingSetSize; public uint ActiveProcessLimit; public UIntPtr Affinity; public uint PriorityClass, SchedulingClass; }
        [StructLayout(LayoutKind.Sequential)] private struct IoCounters { public ulong ReadOperationCount, WriteOperationCount, OtherOperationCount, ReadTransferCount, WriteTransferCount, OtherTransferCount; }
        [StructLayout(LayoutKind.Sequential)] private struct JobInformation { public BasicJobInformation BasicLimitInformation; public IoCounters IoInfo; public UIntPtr ProcessMemoryLimit, JobMemoryLimit, PeakProcessMemoryUsed, PeakJobMemoryUsed; }
        [DllImport("kernel32.dll", SetLastError = true)] private static extern bool GetNamedPipeClientProcessId(SafePipeHandle pipe, out uint pid);
        [DllImport("kernel32.dll", SetLastError = true)] private static extern IntPtr CreateJobObject(IntPtr unused, string name);
        [DllImport("kernel32.dll", SetLastError = true)] private static extern bool SetInformationJobObject(IntPtr job, int type, ref JobInformation info, uint length);
        [DllImport("kernel32.dll", SetLastError = true)] private static extern bool AssignProcessToJobObject(IntPtr job, IntPtr process);
        [DllImport("kernel32.dll")] private static extern bool CloseHandle(IntPtr handle);
    }
}
