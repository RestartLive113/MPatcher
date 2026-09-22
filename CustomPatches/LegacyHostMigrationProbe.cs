using System;
using System.Collections.Generic;
using System.IO;
using System.Reflection;
using System.Reflection.Emit;
using System.Text;
using HarmonyLib;
using McnCraft;
using UnityEngine;
using UnityEngine.UI;

namespace MPatcherFork.CustomPatches
{
    // Only explicitly launched isolated diagnostic processes. Exercises native
    // Configure/Start and Lobby metadata/Join; loopback is not an external test.
    internal sealed class LegacyHostMigrationProbe : MonoBehaviour
    {
        private string file, role;
        private float began, next, sceneReady = -1;
        private int phase;
        private static bool hover;
        private static Vector3 mouse;
        private static bool damage;
        private string roomFile;
        private bool roomWritten;
        private const string Room = "MPatcher Migration TEST";
        internal static void TryRegister()
        {
            string file = Environment.GetEnvironmentVariable("MPATCHER_HOST_MIGRATION_PROBE");
            if (string.IsNullOrEmpty(file)) return;
            string role = Environment.GetEnvironmentVariable("MPATCHER_HOST_MIGRATION_ROLE");
            if (!Path.IsPathRooted(file) || role != "host" && role != "client") throw new ArgumentException("Invalid migration probe environment");
            GameObject root = new GameObject("MPatcher isolated migration probe"); DontDestroyOnLoad(root);
            LegacyHostMigrationProbe probe = root.AddComponent<LegacyHostMigrationProbe>(); probe.file = file; probe.role = role; probe.began = Time.realtimeSinceStartup;
            probe.roomFile = Environment.GetEnvironmentVariable("MPATCHER_HOST_MIGRATION_BOOTSTRAP_ROOM");
            if (probe.roomFile != null && !Path.IsPathRooted(probe.roomFile)) throw new ArgumentException("Absolute fixture room path required");
            Harmony diagnostic = new Harmony("mpatcher.migration.explicit-probe");
            diagnostic.Patch(AccessTools.Method(typeof(Lobby), "Update"), null, null,
                new HarmonyMethod(typeof(LegacyHostMigrationProbe), "MouseTranspiler"));
            diagnostic.Patch(AccessTools.Constructor(typeof(LegacyMigrationFreeze)), new HarmonyMethod(typeof(LegacyHostMigrationProbe), "DamageBeforePause"));
            Log("ARMED role=" + role + " file=" + file);
        }
        private static IEnumerable<CodeInstruction> MouseTranspiler(IEnumerable<CodeInstruction> instructions)
        {
            MethodInfo original = AccessTools.PropertyGetter(typeof(Input), "mousePosition");
            foreach (CodeInstruction instruction in instructions)
            {
                CodeInstruction copy = new CodeInstruction(instruction);
                if (copy.opcode == OpCodes.Call && Equals(copy.operand, original)) copy.operand = AccessTools.Method(typeof(LegacyHostMigrationProbe), "Mouse");
                yield return copy;
            }
        }
        private static Vector3 Mouse() { return hover ? mouse : Input.mousePosition; }
        private void Update()
        {
            float now = Time.realtimeSinceStartup;
            if (now < next) return; next = now + 0.5f;
            try
            {
                if (now - began > 900) { Finish("timeout"); return; }
                string command = File.Exists(file) ? File.ReadAllText(file).Trim() : "";
                if (command == "QUIT") { Finish("quit"); return; }
                if (command == "EXIT" && role == "client" && phase == 2)
                {
                    Game leaving = FindObjectOfType<Game>();
                    if (leaving == null) throw new InvalidOperationException("EXIT requires a game");
                    File.WriteAllText(file, "RUN"); phase = 3; roomFile = null;
                    Log("NATIVE_EXIT"); leaving.Exit(); return;
                }
                if (phase == 3 && command == "JOIN")
                {
                    if (FindObjectOfType<Game>() != null || Network.peerType != NetworkPeerType.Disconnected) return;
                    File.WriteAllText(file, "RUN");
                    AccessTools.Method(typeof(SceneMan), "CJLFFPJICPC", new Type[] { typeof(string), typeof(bool) })
                        .Invoke(SceneMan.JFAOKFIDAGK, new object[] { "Lobby", false });
                    phase = 1; sceneReady = -1; Log("SAME_PROCESS_REJOIN catalogue=true");
                }
                if (phase == 0)
                {
                    if (now - began < 8 || command != "START" || SceneMan.JFAOKFIDAGK == null) return;
                    Patches patches = Harmony.GetPatchInfo(AccessTools.Method(typeof(Connect), "Awake")); bool ready = false;
                    if (patches != null) foreach (Patch patch in patches.Postfixes)
                        if (patch.patch.DeclaringType == typeof(ED5WPQxYa_WFEJ8sZpYciyF71F7y6F6L9h2_xaanIs8prucXCT5rAhf6sMNx9KK4eg)) ready = true;
                    if (!ready) return;
                    MPatchrMain.MPatchr._0024Ymloe9RVCTW7x1ASuQ3c68.legacyHostMigration = true;
                    if (roomFile != null) MPatchrMain.MPatchr._0024Ymloe9RVCTW7x1ASuQ3c68.legacyHostAutoPort = false;
                    // Use the real toggle: it also wakes the service lease worker.
                    LegacyZapret.SetEnabled(role == "host");
                    MPatchrMain.MPatchr._0024Ymloe9RVCTW7x1ASuQ3c68.hiddenRooms = true;
                    MPatchrMain.MPatchr._0024Ymloe9RVCTW7x1ASuQ3c68.roomCode = "MigrationDiagnostic";
                    JKGKJLLFMLE.IGOBPLOLHEP.photonRegion = 4;
                    JKGKJLLFMLE.IGOBPLOLHEP.userName = role == "host" ? "MigrationHost" : "MigrationClient";
                    if (role == "host")
                    {
                        JKGKJLLFMLE.IGOBPLOLHEP.port = 35017; JKGKJLLFMLE.IGOBPLOLHEP.isJoin = true;
                        JKGKJLLFMLE.IGOBPLOLHEP.serverName = Room;
                    }
                    Connect.GMNEILNHKGN = -1;
                    AccessTools.Method(typeof(SceneMan), "CJLFFPJICPC", new Type[] { typeof(string), typeof(bool) })
                        .Invoke(SceneMan.JFAOKFIDAGK, new object[] { role == "host" ? "Configure" : "Lobby", false });
                    phase = 1;
                }
                else if (phase == 1)
                {
                    if (role == "host")
                    {
                        Configure configure = FindObjectOfType<Configure>(); if (configure == null) return;
                        if (sceneReady < 0) { sceneReady = now; return; } if (now - sceneReady < 3) return;
                        AccessTools.Method(typeof(Configure), "BDKIMPEDKCJ").Invoke(configure, new object[] { "Start", configure.GetBTN("Start") });
                        phase = 2;
                    }
                    else TryJoin();
                }
                Game game = FindObjectOfType<Game>();
                if (game != null && phase >= 2)
                {
                    if (role == "host" && roomFile != null && !roomWritten && Network.isServer && LegacyHostStability.Driver != null)
                    {
                        LegacyHostStabilityDriver publication = LegacyHostStability.Driver;
                        string[] fields = { Network.player.guid, Network.player.port.ToString(), Network.maxConnections.ToString(),
                            publication.MigrationGameType, publication.MigrationGameName, publication.PublishedComment() };
                        for (int i = 0; i < fields.Length; i++) fields[i] = Convert.ToBase64String(Encoding.UTF8.GetBytes(fields[i]));
                        File.WriteAllLines(roomFile, fields); roomWritten = true;
                        Log("BOOTSTRAP_ROOM_WRITTEN initialPort=" + Network.player.port + " catalogueNotAssumed=true");
                    }
                    if (command == "ARM_DAMAGE") { damage = true; File.WriteAllText(file, "RUN"); Log("DAMAGE_ARMED"); }
                    if (command == "CHANGE_MACHINE" && role == "client")
                    {
                        File.WriteAllText(file, "RUN");
                        Meeting meeting = game as Meeting;
                        if (meeting == null) throw new InvalidOperationException("Machine change requires Meeting");
                        const string selected = "NewMachine_3";
                        // The real picker and patched File action used by the button.
                        global::dyl7NQFWvb8SnwY4dXogp_aCQhx2Y7dLGUBgzCus25T9Wpo6h01g9Y342KDBl8ctV_NzWUOypgYMsbS0RAESeaU.Class50.WCKsvBPB6cSYds0fexVu_00247Y = "SelectMachine";
                        meeting.ValidatePNL("File", true);
                        string picker = global::_003CModule_003E.smethod_25<string>(4020490498u);
                        AccessTools.Method(typeof(SceneMan), picker, new Type[] { typeof(string), typeof(string), typeof(string) })
                            .Invoke(meeting, new object[] { JKGKJLLFMLE.CFGKIAPCDLB, JKGKJLLFMLE.IGOBPLOLHEP.machineName, null });
                        Text row = null;
                        foreach (Text label in FindObjectsOfType<Text>())
                            if (label.text == selected && label.color != Color.yellow && label.transform.parent != null)
                            { if (row != null) throw new InvalidOperationException("Ambiguous machine picker row"); row = label; }
                        if (row == null) throw new InvalidOperationException("Fixture machine picker row missing");
                        Log("MACHINE_CHANGE nativePicker=true selected=" + selected);
                        AccessTools.Method(typeof(Meeting), "BDKIMPEDKCJ", new Type[] { typeof(string), typeof(GameObject) })
                            .Invoke(meeting, new object[] { "File", row.transform.parent.gameObject });
                    }
                    LegacyHostMigrationDriver driver = game.GetComponent<LegacyHostMigrationDriver>();
                    if (command == "MIGRATE" && driver != null)
                    { File.WriteAllText(file, "RUN"); Log("TRIGGER accepted=" + driver.Begin("explicit-isolated-probe")); }
                    MachineController[] machines = FindObjectsOfType<MachineController>();
                    if (command == "INVENTORY")
                    {
                        StringBuilder inventory = new StringBuilder();
                        foreach (MachineController m in machines)
                        {
                            NetworkView view = m.GetComponent<NetworkView>();
                            inventory.Append("machine instance=").Append(m.GetInstanceID()).Append(" name=").Append(m.name)
                                .Append(" view=").Append(view == null ? "none" : view.viewID.ToString())
                                .Append(" bodies=").Append(m.ILBAAENKMBL == null ? -1 : m.ILBAAENKMBL.Count)
                                .Append(" children=").Append(m.GetComponentsInChildren<Transform>(true).Length).Append('\n');
                        }
                        foreach (NetworkView view in FindObjectsOfType<NetworkView>())
                            inventory.Append("view instance=").Append(view.GetInstanceID()).Append(" name=").Append(view.name)
                                .Append(" id=").Append(view.viewID).Append('\n');
                        if (Network.isServer) inventory.Append(LegacyHostMigrationNative.InspectBufferedRpc());
                        File.WriteAllText(file + ".inventory", inventory.ToString());
                        File.WriteAllText(file, "RUN"); Log("INVENTORY_WRITTEN");
                    }
                    int bodies = 0; foreach (MachineController m in machines) if (m.ILBAAENKMBL != null) bodies += m.ILBAAENKMBL.Count;
                    File.WriteAllText(file + ".state", "scene=" + Application.loadedLevelName + " peer=" + Network.peerType + " port=" + Network.player.port
                        + " views=" + FindObjectsOfType<NetworkView>().Length + " machines=" + machines.Length + " bodies=" + bodies
                        + " scale=" + Time.timeScale + " migration=" + (driver == null ? "client" : driver.State));
                }
            }
            catch (Exception error) { Log("FAILED " + error); File.WriteAllText(file + ".error", error.ToString()); Finish("exception"); }
        }
        private static void DamageBeforePause()
        {
            if (!damage) return; damage = false;
            Game game = FindObjectOfType<Game>();
            if (game == null || game.FICMBCLEFDL == null) throw new InvalidOperationException("Damage fixture has no owner");
            foreach (BodyController b in game.FICMBCLEFDL.ILBAAENKMBL)
            {
                if (b.MEJNIODBGFI <= 1 || b.MEJNIODBGFI > 1000000) continue;
                float before = b.MEJNIODBGFI; b.MEJNIODBGFI *= 0.75f;
                Log("DAMAGE_APPLIED body=" + b.BBLGKLFBJGE + " before=" + before + " after=" + b.MEJNIODBGFI); return;
            }
            throw new InvalidOperationException("Damage fixture has no live body");
        }
        private void TryJoin()
        {
            Lobby lobby = FindObjectOfType<Lobby>(); if (lobby == null) return;
            var code = global::v1JBKckAa1RFmn2CeELS4d1FhLzhlYwRV2bd7TgD_0024MEJnWym5unAzsCQpkwgvPK2FbBLfBqBfJdE_8ZO15q40ZU.LC0iMCkMK03PiX6mz5DQcnM;
            if ((UnityEngine.Object)code == null) return;
            code.pZEKY5TzLd4S3z2lXESoRnw = "MigrationDiagnostic";
            HostData[] hosts = (HostData[])AccessTools.Field(typeof(Lobby), "HCNMJGBGHJP").GetValue(lobby);
            if (roomFile != null)
            {
                if (!File.Exists(roomFile)) return;
                string[] fields = File.ReadAllLines(roomFile); if (fields.Length != 6) return;
                for (int i = 0; i < fields.Length; i++) fields[i] = Encoding.UTF8.GetString(Convert.FromBase64String(fields[i]));
                hosts = new HostData[] { new HostData { guid = fields[0], port = int.Parse(fields[1]), playerLimit = int.Parse(fields[2]),
                    gameType = fields[3], gameName = fields[4], comment = fields[5], connectedPlayers = 1 } };
                AccessTools.Field(typeof(Lobby), "HCNMJGBGHJP").SetValue(lobby, hosts);
                AccessTools.Field(typeof(Lobby), "EHFOPKFIBPA").SetValue(lobby, -1);
                Log("BOOTSTRAP_DIRECT_ROW realHostMetadata=true catalogueRow=synthetic initialPort=" + hosts[0].port);
            }
            if (hosts == null) return;
            for (int i = 0; i < hosts.Length && i < 12; i++)
            {
                HostData host = hosts[i]; if (host == null || !host.gameName.Contains("MPatcher Mig") || host.playerLimit <= host.connectedPlayers) continue;
                Transform row = GameObject.Find("GRP_Server").transform.FindChild("PNL_Server" + i);
                if (roomFile != null) row.gameObject.SetActive(true);
                Button button = row.GetComponentInChildren<Button>(); if (button == null) continue;
                if (roomFile != null)
                {
                    button.interactable = true;
                    button.GetComponentInChildren<Text>().text = host.gameName;
                }
                // Native Update parses all room settings from this real row.
                MethodInfo position = AccessTools.Method(typeof(SceneMan), "CECFBFEGJOB");
                Vector3 min = (Vector3)position.Invoke(lobby, new object[] { lobby.GetGRP("min") });
                Vector3 max = (Vector3)position.Invoke(lobby, new object[] { lobby.GetGRP("max") });
                float height = (max.y - min.y) / 12f;
                mouse = new Vector3((min.x + max.x) / 2f, min.y + (11 - i + 0.4f) * height, 0);
                hover = true;
                try { AccessTools.Method(typeof(Lobby), "Update").Invoke(lobby, null); }
                finally { hover = false; }
                if ((int)AccessTools.Field(typeof(Lobby), "EHFOPKFIBPA").GetValue(lobby) != i) throw new InvalidOperationException("Diagnostic row not selected");
                // Same PC: bypass NAT traversal, retain real session metadata.
                host.useNat = false; host.ip = new string[] { Environment.GetEnvironmentVariable("MPATCHER_HOST_MIGRATION_ADDRESS") ?? "127.0.0.1" };
                AccessTools.Method(typeof(Lobby), "BDKIMPEDKCJ").Invoke(lobby, new object[] { "Server", button.gameObject });
                // Unity can still report Disconnected on the frame Connect returns NoError.
                if ((float)AccessTools.Field(typeof(Lobby), "OCFLJMCKNHB").GetValue(lobby) <= 0)
                    throw new InvalidOperationException("Native Lobby did not arm its connection wait");
                phase = 2; Log("JOIN nativeLobby=true diagnosticAddress=" + host.ip[0] + " port=" + host.port); return;
            }
        }
        private void Finish(string reason)
        {
            if (phase == 99) return; phase = 99;
            File.WriteAllText(file + ".result", reason); Log("FINISH " + reason);
            CrashDiagnostics.MarkCleanExit(); Application.Quit();
        }
        private static void Log(string value) { LegacyHostMigration.Log("PROBE " + value); }
    }
}
