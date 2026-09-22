using System;
using System.Globalization;
using System.IO;
using UnityEngine;

namespace MPatcherFork.CustomPatches
{
    // Explicit loopback-only fault reproduction. No public catalogue or player room.
    internal sealed class LegacyMasterQueryProbe : MonoBehaviour
    {
        private string scenario, trigger, signal, type, lastState;
        private float readyAt, start = -1f, nextQuery, nextState, transition;
        private int phase, port;
        private bool done, queried, signalled;

        internal static bool TryRegister(string triggerPath)
        {
            string value = Environment.GetEnvironmentVariable("MPATCHER_LEGACY_MASTER_PROBE");
            if (value != "query-recovery" && value != "query-flap-stub" && value != "query-live-host" && value != "browser-guard" && value != "query-orphan-teardown") return false;
            string signalPath = Environment.GetEnvironmentVariable("MPATCHER_LEGACY_QUERY_SIGNAL");
            if (string.IsNullOrEmpty(signalPath) || !Path.IsPathRooted(signalPath)) throw new ArgumentException("Absolute query signal required");
            int port;
            if (!int.TryParse(Environment.GetEnvironmentVariable("MPATCHER_LEGACY_MASTER_PORT"), out port)
                || port < 1024 || port > 65535) throw new ArgumentException("Query probe port required");
            GameObject root = new GameObject("MPatcher isolated query probe");
            DontDestroyOnLoad(root);
            LegacyMasterQueryProbe probe = root.AddComponent<LegacyMasterQueryProbe>();
            probe.scenario = value; probe.trigger = triggerPath; probe.signal = signalPath; probe.port = port;
            probe.type = "MCTRQuery" + System.Diagnostics.Process.GetCurrentProcess().Id;
            probe.readyAt = Time.realtimeSinceStartup + 3f;
            Log("ARMED case=" + value + " endpoint=loopback:" + port + " fixRequested=" + (Environment.GetEnvironmentVariable("MPATCHER_LEGACY_MASTER_PROBE_FIX") == "1"));
            return true;
        }

        private void Update()
        {
            if (done) return;
            try { Tick(); }
            catch (Exception error) { Log("FAILED type=" + error.GetType().Name + " message=" + error.Message); Finish(); }
        }
        private void Tick()
        {
            if (start < 0)
            {
                if (Time.realtimeSinceStartup < readyAt || !File.Exists(trigger)) return;
                start = Time.realtimeSinceStartup;
                if (scenario == "query-flap-stub")
                {
                    StartStub();
                    return;
                }
                if (scenario == "browser-guard") { BrowserGuard(); Finish(); return; }
                if (scenario == "query-orphan-teardown") { OrphanTeardown(); Finish(); return; }
                if (scenario == "query-live-host")
                {
                    int gamePort = int.Parse(Environment.GetEnvironmentVariable("MPATCHER_LEGACY_MASTER_GAME_PORT"));
                    if (gamePort < 1024 || gamePort > 65535 || gamePort == port) throw new ArgumentException("Separate game port required");
                    NetworkConnectionError result = Network.InitializeServer(4, gamePort, false);
                    if (result != NetworkConnectionError.NoError) throw new InvalidOperationException("Live host bind failed: " + result);
                    Log("LIVE_HOST_STARTED port=" + gamePort + " guid=" + Network.player.guid);
                }
                MasterServer.ipAddress = "127.0.0.1";
                MasterServer.port = port;
                if (Environment.GetEnvironmentVariable("MPATCHER_LEGACY_MASTER_PROBE_FIX") == "1")
                {
                    LegacyMasterQueryFix.TryRegister();
                    if (!LegacyMasterQueryFix.Applied) throw new InvalidOperationException("Requested query fix was not applied");
                }
                LegacyMasterNativeState.Initialize();
                LegacyMasterStateSnapshot snapshot = LegacyMasterNativeState.Read();
                Log("NATIVE queryPeer=0x" + snapshot.QueryPeer.ToInt32().ToString("X8")
                    + " initializeRva=0x" + snapshot.QueryInitializeRva.ToString("X")
                    + " activeRva=0x" + snapshot.QueryActiveRva.ToString("X"));
                State("before-first-query");
            }
            float elapsed = Time.realtimeSinceStartup - start;
            if (scenario == "query-live-host" && Network.connections.Length == 0)
            {
                if (queried || elapsed > 25f) throw new InvalidOperationException("Live peer absent during query test");
                return;
            }
            if (scenario == "query-flap-stub")
            {
                if (phase == 0 && File.Exists(signal)) { phase = 1; transition = elapsed + 3f; Log("STUB_CLIENT_READY"); }
                if (phase == 1 && elapsed >= transition)
                {
                    Network.Disconnect(); phase = 2; transition = elapsed + 12f;
                    Log("STUB_DOWN duration=12 t=" + Number(elapsed));
                }
                if (phase == 2 && elapsed >= transition) { StartStub(); phase = 3; Log("STUB_RESTARTED t=" + Number(elapsed)); }
                if (elapsed > 60f) Finish();
                return;
            }
            if (elapsed >= nextState)
            {
                nextState = elapsed + 0.1f;
                State("tick");
                if (queried && !signalled && LegacyMasterNativeState.Read().QueryPending == 0)
                {
                    File.WriteAllText(signal, "Initial real RakNet handshake completed; directory response not implemented");
                    signalled = true;
                    Log("INITIAL_HANDSHAKE_COMPLETED t=" + Number(elapsed));
                }
            }
            if (elapsed >= nextQuery)
            {
                nextQuery = elapsed + 3f;
                Log("QUERY_CALL t=" + Number(elapsed));
                MasterServer.RequestHostList(type);
                queried = true;
                State("after-query");
            }
            if (elapsed > 45f) Finish();
        }
        private void StartStub()
        {
            NetworkConnectionError result = Network.InitializeServer(4, port, false);
            Log("STUB_START result=" + result + " peer=" + Network.peerType);
            if (result != NetworkConnectionError.NoError) throw new InvalidOperationException("Stub failed to bind");
        }
        private void OrphanTeardown()
        {
            LegacyMasterQueryFix.TryRegister(); LegacySocketSelfTestFix.TryRegister();
            LegacyMasterNativeState.Initialize();
            MasterServer.ipAddress = "127.0.0.1"; MasterServer.port = port;
            int gamePort = int.Parse(Environment.GetEnvironmentVariable("MPATCHER_LEGACY_MASTER_GAME_PORT"));
            if (Network.InitializeServer(1, gamePort, false) != NetworkConnectionError.NoError)
                throw new InvalidOperationException("Loopback fixture bind failed");
            try
            {
                MasterServer.RequestHostList(type);
                State("pending-before-teardown");
                if (LegacyMasterNativeState.Read().QueryPending != 1) throw new InvalidOperationException("No pending query to cancel");
                if (LegacyCatalogueQueryPeer.ReleaseOrphanedQuery() != null) throw new InvalidOperationException("Touched running game server");
                Network.Disconnect();
                State("after-native-teardown");
                string released = LegacyCatalogueQueryPeer.ReleaseOrphanedQuery();
                if (released == null || LegacyMasterNativeState.Read().QueryPending != 0)
                    throw new InvalidOperationException("Native orphan was not reproduced/released");
                Log("ORPHAN_RELEASED " + released);
                MasterServer.RequestHostList(type);
                LegacyMasterStateSnapshot after = LegacyMasterNativeState.Read();
                if (after.QueryPending != 1 || after.TypeLength != type.Length
                    || System.Runtime.InteropServices.Marshal.ReadByte(after.QueryPeer, 4) != 0)
                    throw new InvalidOperationException("Native request failed to restart after release");
                State("new-query-after-repair");
                Log("ORPHAN_TEARDOWN_PASS nativeDisconnect=true nativeRequestRestarted=true activeGameGuard=true metadataInjected=false");
            }
            finally { Network.Disconnect(); }
        }
        private void State(string label)
        {
            string state = LegacyMasterNativeState.Read().ToString();
            if (label == "tick" && state == lastState) return;
            lastState = state;
            Log("STATE point=" + label + " t=" + Number(Time.realtimeSinceStartup - start) + " peer=" + Network.peerType + " connections=" + Network.connections.Length + " " + state);
        }
        private void OnMasterServerEvent(MasterServerEvent value)
        {
            Log("EVENT value=" + value);
            if (!done && start >= 0 && (scenario == "query-recovery" || scenario == "query-live-host")) State("event");
        }
        private void OnFailedToConnectToMasterServer(NetworkConnectionError value)
        {
            Log("MASTER_FAILED value=" + value + " t=" + Number(Time.realtimeSinceStartup - start));
            if (!done && start >= 0 && (scenario == "query-recovery" || scenario == "query-live-host")) State("failure");
        }
        private void Finish()
        {
            if (done) return;
            done = true;
            Log("COMPLETE case=" + scenario + " peer=" + Network.peerType + " connections=" + Network.connections.Length);
            if (scenario == "query-flap-stub" || scenario == "query-live-host") Network.Disconnect();
            CrashDiagnostics.MarkCleanExit(); Application.Quit();
        }
        private static void BrowserGuard()
        {
            LegacyCatalogueRecovery.TryRegister();
            GameObject root = new GameObject("MPatcher inactive browser callback probe");
            root.SetActive(false);
            int region = JKGKJLLFMLE.IGOBPLOLHEP.photonRegion;
            try
            {
                Lobby lobby = root.AddComponent<Lobby>();
                JKGKJLLFMLE.IGOBPLOLHEP.photonRegion = 4;
                System.Reflection.BindingFlags flags = System.Reflection.BindingFlags.Instance | System.Reflection.BindingFlags.NonPublic;
                typeof(Connect).GetField("OCFLJMCKNHB", flags).SetValue(lobby, 0f);
                int count = LegacyCatalogueRecovery.RetainedFailures;
                typeof(Connect).GetMethod("OnFailedToConnectToMasterServer", flags).Invoke(lobby, new object[] { NetworkConnectionError.ConnectionFailed });
                if (LegacyCatalogueRecovery.RetainedFailures != count + 1) throw new InvalidOperationException("Actual callback was not intercepted");
                JKGKJLLFMLE.IGOBPLOLHEP.photonRegion = 5;
                typeof(Connect).GetField("OCFLJMCKNHB", flags).SetValue(lobby, 10f);
                typeof(Connect).GetMethod("OnFailedToConnectToMasterServer", flags).Invoke(lobby, new object[] { NetworkConnectionError.ConnectionFailed });
                if (LegacyCatalogueRecovery.RetainedFailures != count + 2) throw new InvalidOperationException("Late Legacy failure reached Photon join");
                if ((float)typeof(Connect).GetField("OCFLJMCKNHB", flags).GetValue(lobby) != 10f) throw new InvalidOperationException("Photon join timer changed");
                Log("PHOTON_LATE_MASTER_FAILURE_PASS joinTimer=10 preserved=true actualHarmonyCallback=true");
                Log("BROWSER_GUARD_PASS actualHarmonyCallback=true inactiveLobby=true visualUiTest=false");
            }
            finally { JKGKJLLFMLE.IGOBPLOLHEP.photonRegion = region; DestroyImmediate(root); }
        }
        private void OnPlayerConnected(NetworkPlayer value) { Log("PLAYER_CONNECTED guid=" + value.guid); }
        private void OnPlayerDisconnected(NetworkPlayer value) { Log("PLAYER_DISCONNECTED guid=" + value.guid); }
        private static string Number(float value) { return value.ToString("F3", CultureInfo.InvariantCulture); }
        private static void Log(string value) { mK6lLU33ECSzxV4u22c7_0024ijC0MeyAkqA_PRIEl9WpAZK.YELeoCirSeVGf6u7nOIXkng("[LEGACY-QUERY-PROBE] " + value); }
    }
}
