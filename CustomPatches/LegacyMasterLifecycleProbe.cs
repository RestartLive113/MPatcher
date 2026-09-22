using System;
using System.Globalization;
using System.IO;
using UnityEngine;

namespace MPatcherFork.CustomPatches
{
    // Explicit process-only diagnostic mode. Loopback by default; never joins public rooms.
    internal sealed class LegacyMasterLifecycleProbe : MonoBehaviour
    {
        private string trigger, scenario, gameType;
        private float readyAt, started = -1f, nextState;
        private int stage, gamePort, masterPort;
        private bool done;
        private string lastState;

        internal static bool TryRegister(string triggerPath)
        {
            string scenario = Environment.GetEnvironmentVariable("MPATCHER_LEGACY_MASTER_PROBE");
            if (string.IsNullOrEmpty(scenario)) return false;
            if (scenario != "clean" && scenario != "cancel-pending" && scenario != "transport-restart"
                && scenario != "master-stub" && scenario != "client" && scenario != "cancel-query"
                && scenario != "cancel-only" && scenario != "live-peer")
                throw new ArgumentException("Unknown master lifecycle scenario");
            GameObject obj = new GameObject("MPatcher isolated master lifecycle probe");
            DontDestroyOnLoad(obj);
            LegacyMasterLifecycleProbe probe = obj.AddComponent<LegacyMasterLifecycleProbe>();
            probe.scenario = scenario;
            probe.trigger = triggerPath;
            probe.gamePort = ReadPort("MPATCHER_LEGACY_MASTER_GAME_PORT", 35551);
            probe.masterPort = ReadPort("MPATCHER_LEGACY_MASTER_PORT", 36661);
            probe.readyAt = Time.realtimeSinceStartup + 3f;
            probe.gameType = "MCTRDiag" + System.Diagnostics.Process.GetCurrentProcess().Id;
            Log("ARMED version=1 case=" + scenario + " endpoint=loopback gamePort=" + probe.gamePort
                + " masterPort=" + probe.masterPort + " baseline="
                + (Environment.GetEnvironmentVariable("MPATCHER_LEGACY_SERVICE_PROBE_BASELINE") == "1")
                + " lifecycleFixRequested=" + (Environment.GetEnvironmentVariable("MPATCHER_LEGACY_MASTER_PROBE_FIX") == "1"));
            return true;
        }

        private static int ReadPort(string name, int fallback)
        {
            string value = Environment.GetEnvironmentVariable(name);
            if (string.IsNullOrEmpty(value)) return fallback;
            int port;
            if (!int.TryParse(value, out port) || port < 1024 || port > 65535)
                throw new ArgumentException("Invalid diagnostic port: " + name);
            return port;
        }

        private void Update()
        {
            if (done) return;
            try { Tick(); }
            catch (Exception error) { Log("FAILED type=" + error.GetType().Name + " message=" + error.Message); Finish(); }
        }

        private void Tick()
        {
            if (started < 0)
            {
                if (Time.realtimeSinceStartup < readyAt || !File.Exists(trigger)) return;
                started = Time.realtimeSinceStartup;
                if (Environment.GetEnvironmentVariable("MPATCHER_LEGACY_MASTER_PROBE_FIX") == "1")
                {
                    LegacyMasterLifecycleFix.TryRegister();
                    if (!LegacyMasterLifecycleFix.Applied) throw new InvalidOperationException("Requested lifecycle fix was not applied");
                }
                if (scenario == "client")
                {
                    Log("CLIENT_BEGIN result=" + Network.Connect("127.0.0.1", gamePort));
                    return;
                }
                MasterServer.ipAddress = "127.0.0.1";
                MasterServer.port = masterPort;
                NetworkConnectionError result = Network.InitializeServer(4, scenario == "master-stub" ? masterPort : gamePort, false);
                Log("HOST_BEGIN result=" + result + " peer=" + Network.peerType + " guid=" + Network.player.guid);
                if (result != NetworkConnectionError.NoError) throw new InvalidOperationException("Probe could not start transport");
                if (scenario == "master-stub") return;
                LegacyMasterNativeState.Initialize();
                if (scenario == "live-peer") { stage = 10; return; }
                RunRegistration();
                if (stage == 1) return;
            }
            float elapsed = Time.realtimeSinceStartup - started;
            if (scenario == "master-stub" || scenario == "client")
            {
                if (elapsed > 60f) Finish();
                return;
            }
            if (stage == 10)
            {
                if (Network.connections.Length > 0)
                {
                    Log("LIVE_PEER_READY guid=" + Network.player.guid);
                    RunRegistration();
                }
                else if (elapsed > 20f) throw new TimeoutException("Live peer did not arrive");
                return;
            }
            if (stage == 1 && elapsed > 0.5f)
            {
                NetworkConnectionError result = Network.InitializeServer(4, gamePort, false);
                Log("HOST_RESTART result=" + result);
                if (result != NetworkConnectionError.NoError) throw new InvalidOperationException("Restart rejected");
                State("after-transport-restart");
                RegisterSecond();
                stage = 2;
            }
            if (elapsed >= nextState)
            {
                nextState = elapsed + 0.1f;
                State("tick");
            }
            if (elapsed > (scenario == "live-peer" ? 30f : 8f)) Finish();
        }

        private void RunRegistration()
        {
                State("before-first-register");
                MasterServer.RegisterHost(gameType, "First", "first");
                State("after-first-register");
                if (scenario != "clean")
                {
                    if (scenario == "cancel-query")
                    {
                        MasterServer.RequestHostList(gameType);
                        State("before-unregister-with-query");
                    }
                    MasterServer.UnregisterHost();
                    State("after-unregister");
                    if (scenario == "transport-restart")
                    {
                        Network.Disconnect();
                        State("after-disconnect");
                        stage = 1;
                        return;
                    }
                    if (scenario != "cancel-only") RegisterSecond();
                }
                stage = 2;
        }

        private void RegisterSecond()
        {
            MasterServer.RegisterHost(gameType, "Second registration", "second registration");
            State("after-second-register");
            MasterServer.RequestHostList(gameType);
            State("after-list-query");
        }

        private void State(string where)
        {
            LegacyMasterLifecycleFix.ReportChanges();
            string state = LegacyMasterNativeState.Read().ToString();
            if (where == "tick" && state == lastState) return;
            lastState = state;
            Log("STATE point=" + where + " t=" + (Time.realtimeSinceStartup-started).ToString("F3", CultureInfo.InvariantCulture)
                + " peer=" + Network.peerType + " connections=" + Network.connections.Length + " " + state);
        }

        private void Finish()
        {
            if (done) return;
            done = true;
            Log("COMPLETE case=" + scenario + " peer=" + Network.peerType + " connections=" + Network.connections.Length);
            if (scenario != "client" && scenario != "master-stub") MasterServer.UnregisterHost();
            Network.Disconnect();
            CrashDiagnostics.MarkCleanExit();
            Application.Quit();
        }

        private void OnMasterServerEvent(MasterServerEvent value) { Log("EVENT value=" + value); if (started >= 0 && !done) State("master-event"); }
        private void OnFailedToConnectToMasterServer(NetworkConnectionError value) { Log("MASTER_FAILED value=" + value); if (started >= 0 && !done) State("master-failure"); }
        private void OnPlayerConnected(NetworkPlayer player) { Log("PLAYER_CONNECTED guid=" + player.guid + " connections=" + Network.connections.Length); }
        private void OnPlayerDisconnected(NetworkPlayer player) { Log("PLAYER_DISCONNECTED guid=" + player.guid + " connections=" + Network.connections.Length); }
        private void OnConnectedToServer() { Log("CLIENT_CONNECTED peer=" + Network.peerType); }
        private void OnDisconnectedFromServer(NetworkDisconnection reason) { Log("DISCONNECTED reason=" + reason); }
        private static void Log(string value) { mK6lLU33ECSzxV4u22c7_0024ijC0MeyAkqA_PRIEl9WpAZK.YELeoCirSeVGf6u7nOIXkng("[LEGACY-MASTER-PROBE] " + value); }
    }
}
