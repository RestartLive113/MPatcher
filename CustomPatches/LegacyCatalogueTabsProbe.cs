using System;
using System.IO;
using System.Reflection;
using HarmonyLib;
using UnityEngine;
using UnityEngine.UI;

namespace MPatcherFork.CustomPatches
{
    // Explicit owned diagnostic process. Loads the real Lobby, operates its real
    // Toggles and observes its normal Update/callback/row path. Never joins a room.
    internal sealed class LegacyCatalogueTabsProbe : MonoBehaviour
    {
        private string trigger;
        private float ready, began = -1f, switched, nextLog;
        private float auWait = 3f;
        private bool returnReplyLogged;
        private bool waitForFailure;
        private int phase, savedRegion, replies, beforeReturnReplies;
        private bool done, control;
        private WWW stageDownload;
        private float stageStarted, nextStageLog;
        private bool stageFinished;
        private Lobby lobby;
        private FieldInfo timer, coroutine, marker;
        internal static bool TryRegister(string path)
        {
            if (Environment.GetEnvironmentVariable("MPATCHER_LEGACY_MASTER_PROBE") != "catalogue-tabs-ui") return false;
            GameObject root = new GameObject("MPatcher actual catalogue tabs probe");
            DontDestroyOnLoad(root);
            LegacyCatalogueTabsProbe probe = root.AddComponent<LegacyCatalogueTabsProbe>();
            probe.trigger = path; probe.ready = Time.realtimeSinceStartup + 3f;
            probe.control = Environment.GetEnvironmentVariable("MPATCHER_LEGACY_CATALOGUE_TAB_CANCEL_DISABLE") == "1";
            probe.waitForFailure = Environment.GetEnvironmentVariable("MPATCHER_LEGACY_CATALOGUE_WAIT_FAILURE") == "1";
            int waitSeconds;
            if (int.TryParse(Environment.GetEnvironmentVariable("MPATCHER_LEGACY_CATALOGUE_AU_WAIT"), out waitSeconds))
                probe.auWait = Mathf.Clamp(waitSeconds, 3, 40);
            Log("ARMED actualLobby=true actualToggles=true catalogueOnly=true control=" + probe.control
                + " auWait=" + probe.auWait + " waitForFailure=" + probe.waitForFailure);
            return true;
        }
        private void Update()
        {
            if (done) return;
            try { Tick(); }
            catch (Exception error) { Log("ERROR " + error); Finish(); }
        }
        private void Tick()
        {
            if (began < 0f)
            {
                if (Time.realtimeSinceStartup < ready || !File.Exists(trigger)) return;
                SceneMan scene = SceneMan.JFAOKFIDAGK;
                if (scene == null) return;
                // The settings coroutine enables IndivFix after bootstrap. Opening
                // Lobby earlier exercises Unity's abandoned default endpoint.
                Patches awakePatches = Harmony.GetPatchInfo(AccessTools.Method(typeof(Connect), "Awake"));
                bool indivReady = false;
                if (awakePatches != null)
                    foreach (Patch patch in awakePatches.Postfixes)
                        if (patch.patch.DeclaringType == typeof(ED5WPQxYa_WFEJ8sZpYciyF71F7y6F6L9h2_xaanIs8prucXCT5rAhf6sMNx9KK4eg)) indivReady = true;
                if (!indivReady) return;
                savedRegion = JKGKJLLFMLE.IGOBPLOLHEP.photonRegion;
                JKGKJLLFMLE.IGOBPLOLHEP.photonRegion = 4;
                Connect.GMNEILNHKGN = -1; // Same action as Menu's Play case.
                typeof(SceneMan).GetMethod("CJLFFPJICPC", BindingFlags.Instance | BindingFlags.NonPublic)
                    .Invoke(scene, new object[] { "Lobby", false });
                timer = typeof(Connect).GetField("OCFLJMCKNHB", BindingFlags.Instance | BindingFlags.NonPublic);
                coroutine = typeof(Connect).GetField("LFAMCJIHJIM", BindingFlags.Instance | BindingFlags.NonPublic);
                marker = typeof(Connect).GetField("CEFHHPLGBFO", BindingFlags.Instance | BindingFlags.NonPublic);
                began = Time.realtimeSinceStartup;
                string stageUrl = Environment.GetEnvironmentVariable("MPATCHER_LEGACY_STAGE_PROBE_URL");
                if (!string.IsNullOrEmpty(stageUrl))
                {
                    stageDownload = new WWW(stageUrl); stageStarted = began;
                    Log("STAGE_PROBE_BEGIN nativeWWW=true cacheWrite=false url=" + stageUrl);
                }
                Log("OPEN_LOBBY indivFixReady=true");
                return;
            }
            float now = Time.realtimeSinceStartup;
            TickStage(now);
            if (now - began > 85f) { Log("TIMEOUT phase=" + phase); Finish(); return; }
            if (lobby == null) lobby = UnityEngine.Object.FindObjectOfType<Lobby>();
            if (lobby == null) return;
            GameObject group = GameObject.Find("GRP_Server");
            if (group == null || group.transform.FindChild("PNL_Server11") == null) return;
            int rows = 0;
            for (int i = 0; i < 12; i++) if (group.transform.FindChild("PNL_Server" + i).gameObject.activeSelf) rows++;
            if (now >= nextLog)
            {
                nextLog = now + 2f;
                Log("STATE phase=" + phase + " region=" + JKGKJLLFMLE.IGOBPLOLHEP.photonRegion
                    + " joining=" + timer.GetValue(lobby) + " replies=" + replies + " visibleRows=" + rows
                    + " coroutineHandle=" + (coroutine.GetValue(lobby) != null) + " photonJoinMarker=" + marker.GetValue(lobby));
            }
            if (phase == 0 && replies > 0 && rows > 0 && (!waitForFailure || LegacyCatalogueRecovery.RetainedFailures > 0))
            {
                SelectTab("Au", 5);
                switched = now; phase = 1;
                Log("AU_SELECTED timer=" + timer.GetValue(lobby));
            }
            else if (phase == 1 && now - switched >= auWait)
            {
                beforeReturnReplies = replies;
                SelectTab("Ind", 4);
                switched = now; phase = 2;
                int immediateRows = 0;
                for (int i = 0; i < 12; i++) if (group.transform.FindChild("PNL_Server" + i).gameObject.activeSelf) immediateRows++;
                Log("INDIVIDUAL_SELECTED timer=" + timer.GetValue(lobby) + " immediateRows=" + immediateRows);
            }
            else if (phase == 2)
            {
                if (!returnReplyLogged && replies > beforeReturnReplies && rows > 0)
                {
                    returnReplyLogged = true;
                    Log("RETURN_LIST_VISIBLE elapsed=" + (now - switched) + " activeRows=" + rows);
                }
                if (now - switched < (control ? 10f : 15f) || (stageDownload != null && !stageFinished)) return;
                if (control)
                {
                    Log("CONTROL_RESULT timer=" + timer.GetValue(lobby) + " newReplies=" + (replies - beforeReturnReplies) + " visibleRows=" + rows);
                    Finish();
                }
                else if (replies > beforeReturnReplies && rows > 0 && (float)timer.GetValue(lobby) <= 0f
                    && (int)marker.GetValue(lobby) == -1)
                {
                    Log("PASS actualLobby=true nativeUpdate=true actualTabHandler=true freshReplies="
                        + (replies - beforeReturnReplies) + " visibleRows=" + rows + " photonJoinMarker=-1");
                    Finish();
                }
            }
        }
        private void OnMasterServerEvent(MasterServerEvent value)
        { if (value == MasterServerEvent.HostListReceived) replies++; }
        private void TickStage(float now)
        {
            if (stageDownload == null || stageFinished) return;
            if (now >= nextStageLog)
            {
                nextStageLog = now + 10f;
                Log("STAGE_PROBE_PROGRESS elapsed=" + (now - stageStarted) + " progress=" + stageDownload.progress + " done=" + stageDownload.isDone);
            }
            if (stageDownload.isDone)
            {
                stageFinished = true;
                Log("STAGE_PROBE_HTTP elapsed=" + (now - stageStarted) + " error=" + stageDownload.error + " bytes=" + stageDownload.bytes.Length);
                if (string.IsNullOrEmpty(stageDownload.error))
                {
                    byte[] raw = (byte[])typeof(Game).Assembly.GetType("IPFOLBIPILG", true)
                        .GetMethod("LJPINOEINPE", BindingFlags.Public | BindingFlags.Static)
                        .Invoke(null, new object[] { stageDownload.bytes });
                    ConstructData data = JKGKJLLFMLE.DLDFNBIEDOI(raw);
                    Log("STAGE_PROBE_DECODE decodedBytes=" + raw.Length + " valid=" + (data != null)
                        + " primitives=" + (data == null ? -1 : data.primData.Count));
                }
            }
            else if (now - stageStarted >= 65f)
            { stageFinished = true; Log("STAGE_PROBE_TIMEOUT elapsed=" + (now - stageStarted)); }
        }
        private void SelectTab(string name, int region)
        {
            GameObject toggle = lobby.GetTGL(name);
            toggle.GetComponent<Toggle>().isOn = true;
            // SceneMan dispatches its widget action separately from Toggle.isOn.
            // Invoke the actual patched handler with the real scene widget.
            if (JKGKJLLFMLE.IGOBPLOLHEP.photonRegion != region)
                typeof(Lobby).GetMethod("GMKBKFPBKPF", BindingFlags.Instance | BindingFlags.NonPublic)
                    .Invoke(lobby, new object[] { name, toggle });
            if (JKGKJLLFMLE.IGOBPLOLHEP.photonRegion != region)
                throw new InvalidOperationException("Actual tab handler did not select " + name);
        }
        private void Finish()
        {
            done = true;
            if (stageDownload != null) { stageDownload.Dispose(); stageDownload = null; }
            if (began >= 0f) JKGKJLLFMLE.IGOBPLOLHEP.photonRegion = savedRegion;
            Log("COMPLETE"); CrashDiagnostics.MarkCleanExit(); Application.Quit();
        }
        private static void Log(string value)
        { mK6lLU33ECSzxV4u22c7_0024ijC0MeyAkqA_PRIEl9WpAZK.YELeoCirSeVGf6u7nOIXkng("[LEGACY-TABS-PROBE] " + value); }
    }
}
