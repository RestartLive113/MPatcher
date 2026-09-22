using System;
using System.IO;
using System.Net;
using System.Net.Sockets;
using HarmonyLib;
using UnityEngine;

namespace MPatcherFork.CustomPatches
{
    // Explicit diagnostic process only. Drives the actual Configure Start handler;
    // fault injection is local to this process, never a firewall/route change.
    internal sealed class LegacyHostAutoPortProbe : MonoBehaviour
    {
        private string file, scenario;
        private float began, configured = -1, world = -1, next;
        private int phase, preferred = 35017;
        private UdpClient occupied;
        private Configure configure;
        internal static void TryRegister()
        {
            string path = Environment.GetEnvironmentVariable("MPATCHER_LEGACY_HOST_PORT_PROBE");
            if (string.IsNullOrEmpty(path)) return;
            if (!Path.IsPathRooted(path)) throw new ArgumentException("Absolute auto-port probe file required");
            GameObject root = new GameObject("MPatcher host auto-port probe"); DontDestroyOnLoad(root);
            LegacyHostAutoPortProbe probe = root.AddComponent<LegacyHostAutoPortProbe>(); probe.file = path;
            probe.scenario = Environment.GetEnvironmentVariable("MPATCHER_LEGACY_HOST_PORT_SCENARIO") ?? "healthy";
            if (probe.scenario != "healthy" && probe.scenario != "occupied" && probe.scenario != "cancel" && probe.scenario != "offline")
                throw new ArgumentException("Unknown host auto-port diagnostic scenario");
            probe.began = Time.realtimeSinceStartup;
            LegacyHostAutoPort.Log("PROBE_ARMED scenario=" + probe.scenario);
        }
        private void Update()
        {
            if (Time.realtimeSinceStartup < next) return; next = Time.realtimeSinceStartup + 0.25f;
            try
            {
                if (File.Exists(file) && File.ReadAllText(file).Trim() == "QUIT") { Finish("requested quit"); return; }
                if (Time.realtimeSinceStartup - began > 215) { Finish("timeout"); return; }
                if (phase == 0)
                {
                    if (Time.realtimeSinceStartup - began < 8 || !File.Exists(file) || SceneMan.JFAOKFIDAGK == null) return;
                    Patches patches = Harmony.GetPatchInfo(AccessTools.Method(typeof(Connect), "Awake"));
                    bool ready = false;
                    if (patches != null) foreach (Patch patch in patches.Postfixes)
                        if (patch.patch.DeclaringType == typeof(ED5WPQxYa_WFEJ8sZpYciyF71F7y6F6L9h2_xaanIs8prucXCT5rAhf6sMNx9KK4eg)) ready = true;
                    if (!ready) return;
                    MPatchrMain.MPatchr._0024Ymloe9RVCTW7x1ASuQ3c68.legacyHostAutoPort = true;
                    MPatchrMain.MPatchr._0024Ymloe9RVCTW7x1ASuQ3c68.hiddenRooms = true;
                    MPatchrMain.MPatchr._0024Ymloe9RVCTW7x1ASuQ3c68.roomCode = "AutoPortDiagnostic";
                    JKGKJLLFMLE.IGOBPLOLHEP.port = preferred;
                    JKGKJLLFMLE.IGOBPLOLHEP.isJoin = false;
                    JKGKJLLFMLE.IGOBPLOLHEP.serverName = "MPatcher AutoPort TEST";
                    AccessTools.Method(typeof(SceneMan), "CJLFFPJICPC", new Type[] { typeof(string), typeof(bool) })
                        .Invoke(SceneMan.JFAOKFIDAGK, new object[] { "Configure", false });
                    phase = 1;
                }
                else if (phase == 1)
                {
                    configure = FindObjectOfType<Configure>(); if (configure == null) return;
                    if (configured < 0) { configured = Time.realtimeSinceStartup; return; }
                    if (Time.realtimeSinceStartup - configured < 2) return;
                    if (scenario == "occupied")
                    {
                        occupied = new UdpClient(); occupied.Client.ExclusiveAddressUse = true;
                        occupied.Client.Bind(new IPEndPoint(IPAddress.Any, preferred));
                    }
                    if (scenario == "offline") { MasterServer.ipAddress = "127.0.0.1"; MasterServer.port = 36662; }
                    PressStart(); phase = 2;
                }
                else if (phase == 2)
                {
                    LegacyHostAutoPortDriver driver = LegacyHostAutoPort.Current;
                    string value = "scene=" + Application.loadedLevelName + " server=" + Network.isServer
                        + " port=" + Network.player.port + " guid=" + Network.player.guid + " peers=" + Network.connections.Length
                        + " phase=" + (driver == null ? "none" : driver.Policy.Phase.ToString());
                    File.WriteAllText(file + ".state", value);
                    if (driver != null && scenario == "cancel" && driver.Policy.Phase == LegacyHostPortPhase.WaitingReply)
                    {
                        driver.Cancel("probe cancel"); phase = 3;
                        configured = Time.realtimeSinceStartup; LegacyHostAutoPort.Log("PROBE_CANCEL state=" + Network.peerType);
                    }
                    else if (driver != null && driver.Policy.Phase == LegacyHostPortPhase.Exhausted) Finish("exhausted " + value);
                    else if (driver == null && Network.isServer && Application.loadedLevelName == "Host")
                    {
                        if (world < 0) world = Time.realtimeSinceStartup;
                        if (Time.realtimeSinceStartup - world > 12) Finish("world ready " + value);
                    }
                }
                else if (phase == 3 && Time.realtimeSinceStartup - configured > 2)
                { scenario = "healthy"; PressStart(); phase = 2; }
            }
            catch (Exception error) { LegacyHostAutoPort.Log("PROBE_FAILED " + error); Finish("exception"); }
        }
        private void PressStart()
        {
            AccessTools.Method(typeof(Configure), "BDKIMPEDKCJ", new Type[] { typeof(string), typeof(GameObject) })
                .Invoke(configure, new object[] { "Start", configure.GetBTN("Start") });
        }
        private void Finish(string result)
        {
            if (phase == 99) return; phase = 99;
            LegacyHostAutoPort.Log("PROBE_FINISH " + result);
            File.WriteAllText(file + ".result", result);
            if (occupied != null) { occupied.Close(); occupied = null; }
            CrashDiagnostics.MarkCleanExit(); Application.Quit();
        }
    }
}
