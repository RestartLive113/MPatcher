using System;
using System.IO;
using UnityEngine;

namespace MPatcherFork.CustomPatches
{
    // Explicitly armed diagnostic process only. Optional private test registration; no player join.
    internal sealed class LegacyServiceProbe : MonoBehaviour
    {
        private string trigger;
        private float started = -1f;
        private float next;
        private bool finished;
        private float readyAt;
        private string gameType = "McnC";
        private bool host;
        internal static bool TryRegisterBaseline()
        {
            if (Environment.GetEnvironmentVariable("MPATCHER_LEGACY_SERVICE_PROBE_BASELINE") != "1"
                || string.IsNullOrEmpty(Environment.GetEnvironmentVariable("MPATCHER_LEGACY_SERVICE_PROBE"))) return false;
            DateTime deadline = DateTime.UtcNow.AddSeconds(60);
            while (SceneMan.JFAOKFIDAGK == null)
            {
                if (DateTime.UtcNow > deadline) throw new TimeoutException("Diagnostic scene initialization timed out");
                System.Threading.Thread.Sleep(10);
            }
            TryRegister();
            Log("BASELINE native Unity service APIs; regular MPatcher bootstrap skipped");
            return true;
        }
        internal static void TryRegister()
        {
            string path = Environment.GetEnvironmentVariable("MPATCHER_LEGACY_SERVICE_PROBE");
            if (string.IsNullOrEmpty(path)) return;
            if (!Path.IsPathRooted(path)) throw new ArgumentException("Probe trigger must be absolute");
            if (LegacyCatalogueTabsProbe.TryRegister(path)) return;
            if (LegacyCatalogueRouteProbe.TryRegister(path)) return;
            if (LegacyMasterQueryProbe.TryRegister(path)) return;
            if (LegacyMasterLifecycleProbe.TryRegister(path)) return;
            GameObject obj = new GameObject("MPatcher service diagnostic");
            DontDestroyOnLoad(obj);
            LegacyServiceProbe probe = obj.AddComponent<LegacyServiceProbe>();
            probe.trigger = path;
            probe.readyAt = Time.realtimeSinceStartup + 15f;
            probe.host = Environment.GetEnvironmentVariable("MPATCHER_LEGACY_SERVICE_PROBE_HOST") == "1";
            Log("ARMED trigger=" + path + " scope=" + (probe.host ? "private-host-and-catalogue" : "catalogue-query-only"));
        }
        private void Update()
        {
            if (finished) return;
            if (started < 0)
            {
                if (trigger == null || !File.Exists(trigger) || Time.realtimeSinceStartup < readyAt) return;
                started = Time.realtimeSinceStartup;
                MasterServer.ipAddress = "173.230.144.203";
                MasterServer.port = 23466;
                Network.natFacilitatorIP = "173.230.144.203";
                Network.natFacilitatorPort = 50005;
                Log("BEGIN local=" + Network.player.ipAddress + " master=" + MasterServer.ipAddress);
                if (host)
                {
                    gameType = "MCTRDiag" + System.Diagnostics.Process.GetCurrentProcess().Id;
                    NetworkConnectionError result = Network.InitializeServer(2, 25551, true);
                    Log("HOST_START result=" + result + " gameType=" + gameType);
                    if (result == NetworkConnectionError.NoError)
                        MasterServer.RegisterHost(gameType, "MPatcher isolated service probe", "diagnostic; no playable room");
                }
            }
            if (Time.realtimeSinceStartup - started > 25f)
            {
                finished = true;
                Log("COMPLETE hosts=" + MasterServer.PollHostList().Length);
                if (host) { MasterServer.UnregisterHost(); Network.Disconnect(); }
                CrashDiagnostics.MarkCleanExit();
                Application.Quit();
                return;
            }
            if (Time.realtimeSinceStartup < next) return;
            next = Time.realtimeSinceStartup + 6f;
            MasterServer.RequestHostList(gameType);
            Log("QUERY elapsed=" + (Time.realtimeSinceStartup - started) + " master=" + MasterServer.ipAddress + ":" + MasterServer.port
                + " facilitator=" + Network.natFacilitatorIP + ":" + Network.natFacilitatorPort + " local=" + Network.player.ipAddress);
        }
        private void OnMasterServerEvent(MasterServerEvent value)
        {
            Log("EVENT value=" + value + " hosts=" + MasterServer.PollHostList().Length);
        }
        private void OnFailedToConnectToMasterServer(NetworkConnectionError value)
        {
            Log("FAILED value=" + value);
        }
        private static void Log(string value)
        {
            mK6lLU33ECSzxV4u22c7_0024ijC0MeyAkqA_PRIEl9WpAZK.YELeoCirSeVGf6u7nOIXkng("[LEGACY-SERVICE-PROBE] " + value);
        }
    }
}
