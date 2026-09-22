using System;
using System.IO;
using UnityEngine;

namespace MPatcherFork.CustomPatches
{
    // Explicit diagnostic: ordinary read-only public catalogue queries, no host/join.
    internal sealed class LegacyCatalogueRouteProbe : MonoBehaviour
    {
        private string trigger;
        private float ready, started = -1f, next;
        private bool done;
        private bool restartPending;
        private int failures;
        private Lobby browser;
        private int previousRegion;
        private System.Reflection.MethodInfo requestList, failedCallback, receivedCallback;
        internal static bool TryRegister(string path)
        {
            string scenario = Environment.GetEnvironmentVariable("MPATCHER_LEGACY_MASTER_PROBE");
            if (scenario != "route-same-socket" && scenario != "route-socket-check" && scenario != "route-restart-socket" && scenario != "route-browser-recovery") return false;
            GameObject root = new GameObject("MPatcher catalogue route probe");
            DontDestroyOnLoad(root);
            LegacyCatalogueRouteProbe probe = root.AddComponent<LegacyCatalogueRouteProbe>();
            probe.trigger = path; probe.ready = Time.realtimeSinceStartup + 3f;
            Log("ARMED case=" + scenario + " endpoint=173.230.144.203:23466 queries=read-only");
            return true;
        }
        private void Update()
        {
            if (done) return;
            try
            {
                if (started < 0f)
                {
                    if (Time.realtimeSinceStartup < ready || !File.Exists(trigger)) return;
                    if (Environment.GetEnvironmentVariable("MPATCHER_LEGACY_MASTER_PROBE") == "route-socket-check")
                    { CheckSockets(); Finish(); return; }
                    LegacyMasterQueryFix.TryRegister();
                    if (!LegacyMasterQueryFix.Applied) throw new InvalidOperationException("Query fix absent");
                    if (Environment.GetEnvironmentVariable("MPATCHER_LEGACY_MASTER_PROBE_FIX") == "1")
                    {
                        LegacySocketSelfTestFix.TryRegister();
                        if (!LegacySocketSelfTestFix.Applied) throw new InvalidOperationException("Socket self-test fix absent");
                    }
                    LegacyMasterNativeState.Initialize();
                    MasterServer.ipAddress = "173.230.144.203"; MasterServer.port = 23466;
                    if (Environment.GetEnvironmentVariable("MPATCHER_LEGACY_MASTER_PROBE") == "route-browser-recovery")
                    {
                        LegacyCatalogueRecovery.TryRegister();
                        previousRegion = JKGKJLLFMLE.IGOBPLOLHEP.photonRegion;
                        JKGKJLLFMLE.IGOBPLOLHEP.photonRegion = 4;
                        GameObject inactive = new GameObject("MPatcher inactive browser route probe"); inactive.SetActive(false);
                        browser = inactive.AddComponent<Lobby>();
                        System.Reflection.BindingFlags instance = System.Reflection.BindingFlags.Instance | System.Reflection.BindingFlags.NonPublic;
                        System.Reflection.BindingFlags statics = System.Reflection.BindingFlags.Static | System.Reflection.BindingFlags.NonPublic;
                        typeof(Connect).GetField("OCFLJMCKNHB", instance).SetValue(browser, 0f);
                        requestList = typeof(LegacyCatalogueRecovery).GetMethod("RequestList", statics);
                        receivedCallback = typeof(LegacyCatalogueRecovery).GetMethod("ReceivedPostfix", statics);
                        failedCallback = typeof(Connect).GetMethod("OnFailedToConnectToMasterServer", instance);
                        Log("BROWSER_ROUTE inactiveLobby=true productionRequestWrapper=true actualFailureCallback=true visualUiTest=false");
                    }
                    started = Time.realtimeSinceStartup;
                }
                float elapsed = Time.realtimeSinceStartup - started;
                if (elapsed >= (browser != null || Environment.GetEnvironmentVariable("MPATCHER_LEGACY_MASTER_PROBE") == "route-restart-socket" ? 115f : 65f)) { Finish(); return; }
                if (restartPending && LegacyMasterNativeState.Read().QueryPending == 0)
                {
                    restartPending = false;
                    Log("QUERY_PEER_RESTART " + LegacyCatalogueQueryPeer.Restart());
                    next = Time.realtimeSinceStartup + 0.25f;
                }
                if (Time.realtimeSinceStartup < next) return;
                next = Time.realtimeSinceStartup + 9f;
                if (browser != null) requestList.Invoke(null, new object[] { "McnC", browser });
                else MasterServer.RequestHostList("McnC");
                Log("QUERY t=" + elapsed.ToString("F3", System.Globalization.CultureInfo.InvariantCulture) + " " + LegacyMasterNativeState.Read());
            }
            catch (Exception error) { Log("ERROR type=" + error.GetType().Name + " message=" + error.Message); Finish(); }
        }
        private void OnMasterServerEvent(MasterServerEvent value)
        {
            Log("EVENT value=" + value + " hosts=" + MasterServer.PollHostList().Length);
            if (value == MasterServerEvent.HostListReceived) failures = 0;
            if (browser != null) receivedCallback.Invoke(null, new object[] { browser, value });
        }
        private static void CheckSockets()
        {
            foreach (bool udp in new bool[] { true, false })
                foreach (System.Net.IPAddress address in new System.Net.IPAddress[] { System.Net.IPAddress.Any, System.Net.IPAddress.Loopback })
                {
                    string phase = "create";
                    try
                    {
                        using (System.Net.Sockets.Socket socket = new System.Net.Sockets.Socket(System.Net.Sockets.AddressFamily.InterNetwork,
                            udp ? System.Net.Sockets.SocketType.Dgram : System.Net.Sockets.SocketType.Stream,
                            udp ? System.Net.Sockets.ProtocolType.Udp : System.Net.Sockets.ProtocolType.Tcp))
                        {
                            phase = "bind";
                            socket.Bind(new System.Net.IPEndPoint(address, 0));
                            Log("SOCKET_PASS protocol=" + (udp ? "UDP" : "TCP") + " local=" + socket.LocalEndPoint);
                            if (udp)
                            {
                                System.Net.IPEndPoint local = (System.Net.IPEndPoint)socket.LocalEndPoint;
                                System.Net.IPAddress self = local.Address.Equals(System.Net.IPAddress.Any) ? System.Net.IPAddress.Loopback : local.Address;
                                foreach (System.Net.IPAddress destination in new System.Net.IPAddress[] { System.Net.IPAddress.Loopback, self })
                                {
                                    try
                                    {
                                        int bytes = socket.SendTo(new byte[4], new System.Net.IPEndPoint(destination, local.Port));
                                        Log("SELF_SEND_PASS bound=" + address + " actual=" + local + " destination=" + destination + " bytes=" + bytes);
                                    }
                                    catch (System.Net.Sockets.SocketException error)
                                    { Log("SELF_SEND_FAIL bound=" + address + " actual=" + local + " destination=" + destination + " nativeError=" + error.NativeErrorCode + " socketError=" + error.SocketErrorCode); }
                                }
                            }
                        }
                    }
                    catch (System.Net.Sockets.SocketException error)
                    {
                        Log("SOCKET_FAIL protocol=" + (udp ? "UDP" : "TCP") + " local=" + address + " phase=" + phase
                            + " nativeError=" + error.NativeErrorCode + " socketError=" + error.SocketErrorCode + " message=" + error.Message);
                    }
                }
            bool ran = false;
            System.Threading.Thread thread = new System.Threading.Thread(delegate() { ran = true; });
            thread.IsBackground = true; thread.Start();
            bool joined = thread.Join(1000);
            Log("THREAD_CHECK ran=" + ran + " joined=" + joined);
        }
        private void OnFailedToConnectToMasterServer(NetworkConnectionError value)
        {
            Log("FAILED value=" + value);
            if (browser != null) failedCallback.Invoke(browser, new object[] { value });
            if (value == NetworkConnectionError.ConnectionFailed
                && Environment.GetEnvironmentVariable("MPATCHER_LEGACY_MASTER_PROBE") == "route-restart-socket" && ++failures >= 2)
            { restartPending = true; failures = 0; }
        }
        private void Finish()
        {
            done = true;
            if (browser != null) { DestroyImmediate(browser.gameObject); JKGKJLLFMLE.IGOBPLOLHEP.photonRegion = previousRegion; }
            Log("COMPLETE"); CrashDiagnostics.MarkCleanExit(); Application.Quit();
        }
        private static void Log(string value)
        { mK6lLU33ECSzxV4u22c7_0024ijC0MeyAkqA_PRIEl9WpAZK.YELeoCirSeVGf6u7nOIXkng("[LEGACY-ROUTE-PROBE] " + value); }
    }
}
