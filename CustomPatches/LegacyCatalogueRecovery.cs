using System;
using System.Collections.Generic;
using System.Reflection;
using System.Reflection.Emit;
using HarmonyLib;
using UnityEngine;

namespace MPatcherFork.CustomPatches
{
    internal static class LegacyCatalogueRecovery
    {
        private static Harmony harmony;
        private static FieldInfo joinTimer;
        private static FieldInfo refreshTimer;
        private static FieldInfo displayedHosts;
        private static MethodInfo cancelPhotonSearch;
        private static MethodInfo drawList;
        private static FieldInfo gameTypeField;
        private static Lobby cachedOwner;
        private static string requestedGameType, cachedGameType;
        private static HostData[] cachedHosts, replayHosts;
        private static float cachedAt;
        private static Lobby socketOwner;
        private static LegacyCatalogueSocketPolicy socketPolicy = new LegacyCatalogueSocketPolicy();
        internal static int RetainedFailures { get; private set; }
        internal static void TryRegister()
        {
            if (harmony != null) return;
            const string id = "mpatcher.legacy-catalogue-recovery.v1";
            Harmony candidate = new Harmony(id);
            try
            {
                joinTimer = AccessTools.Field(typeof(Connect), "OCFLJMCKNHB");
                refreshTimer = AccessTools.Field(typeof(Connect), "AOGHPEKPLKK");
                displayedHosts = AccessTools.Field(typeof(Lobby), "HCNMJGBGHJP");
                gameTypeField = AccessTools.Field(typeof(Connect), "JEJJNNDDLID");
                drawList = AccessTools.Method(typeof(Lobby), "CLDHJDELGEB", new Type[] { typeof(bool) });
                if (gameTypeField == null || drawList == null) throw new MissingMemberException("Lobby cached display targets");
                cancelPhotonSearch = AccessTools.Method(typeof(Connect), "JPDLDKABINE", new Type[] { typeof(bool) });
                if (cancelPhotonSearch == null) throw new MissingMethodException("Connect.JPDLDKABINE(bool)");
                if (joinTimer == null || joinTimer.FieldType != typeof(float)) throw new MissingFieldException("Connect.OCFLJMCKNHB");
                if (refreshTimer == null || refreshTimer.FieldType != typeof(float) || displayedHosts == null
                    || displayedHosts.FieldType != typeof(HostData[])) throw new MissingFieldException("Lobby catalogue state");
                MethodInfo target = AccessTools.Method(typeof(Connect), "OnFailedToConnectToMasterServer", new Type[] { typeof(NetworkConnectionError) });
                if (target == null) throw new MissingMethodException("Connect.OnFailedToConnectToMasterServer");
                candidate.Patch(target, new HarmonyMethod(AccessTools.Method(typeof(LegacyCatalogueRecovery), "FailedPrefix")));
                MethodInfo tab = AccessTools.Method(typeof(Lobby), "GMKBKFPBKPF", new Type[] { typeof(string), typeof(GameObject) });
                MethodInfo update = AccessTools.Method(typeof(Lobby), "Update", Type.EmptyTypes);
                MethodInfo received = AccessTools.Method(typeof(Connect), "OnMasterServerEvent", new Type[] { typeof(MasterServerEvent) });
                if (tab == null || update == null || received == null) throw new MissingMethodException("Lobby catalogue targets");
                candidate.Patch(tab, new HarmonyMethod(AccessTools.Method(typeof(LegacyCatalogueRecovery), "TabPrefix")),
                    new HarmonyMethod(AccessTools.Method(typeof(LegacyCatalogueRecovery), "TabPostfix")));
                candidate.Patch(update, null, null, new HarmonyMethod(AccessTools.Method(typeof(LegacyCatalogueRecovery), "UpdateTranspiler")));
                candidate.Patch(received, null, new HarmonyMethod(AccessTools.Method(typeof(LegacyCatalogueRecovery), "ReceivedPostfix")));
                candidate.Patch(drawList, null, null, new HarmonyMethod(AccessTools.Method(typeof(LegacyCatalogueRecovery), "DisplayTranspiler")));
                harmony = candidate;
                Log("REGISTERED version=7 scope=idle-Individual+ignore-Legacy-error-on-Photon polling=native-9s querySocketRecovery=two-failures-or-failed-tab-return cooldown=30s cachedRows=native-redraw orphanedQuery=inactive+empty-type cancelPhotonSearchOnReturn=true");
            }
            catch (Exception error) { candidate.UnpatchAll(id); Log("REGISTER_FAILED type=" + error.GetType().Name + " message=" + error.Message); }
        }

        private static bool FailedPrefix(Connect __instance, NetworkConnectionError __0)
        {
            try
            {
                if (!LegacyCatalogueRecoveryPolicy.KeepBrowser(__instance is Lobby,
                    JKGKJLLFMLE.IGOBPLOLHEP.photonRegion == 4,
                    Network.peerType == NetworkPeerType.Disconnected, (float)joinTimer.GetValue(__instance))) return true;
                RetainedFailures++;
                if (__instance == socketOwner && __0 == NetworkConnectionError.ConnectionFailed)
                    socketPolicy.Failed(JKGKJLLFMLE.IGOBPLOLHEP.photonRegion == 4
                        && Network.peerType == NetworkPeerType.Disconnected && (float)joinTimer.GetValue(__instance) <= 0f);
                Log("BROWSER_RETAINED error=" + __0 + " region=" + JKGKJLLFMLE.IGOBPLOLHEP.photonRegion
                    + " cause=" + (JKGKJLLFMLE.IGOBPLOLHEP.photonRegion == 4 ? "idle-catalogue-retry" : "unrelated-Legacy-callback-on-Photon")
                    + " nativeQueryFix=" + LegacyMasterQueryFix.Applied);
                return false;
            }
            catch (Exception error) { Log("GUARD_FAILED type=" + error.GetType().Name); return true; }
        }
        private static void TabPrefix(out int __state) { __state = JKGKJLLFMLE.IGOBPLOLHEP.photonRegion; }
        private static void TabPostfix(Lobby __instance, int __state)
        {
            try
            {
                int region = JKGKJLLFMLE.IGOBPLOLHEP.photonRegion;
                if (__state == region) return;
                if (Environment.GetEnvironmentVariable("MPATCHER_LEGACY_CATALOGUE_TAB_CANCEL_DISABLE") != "1"
                    && LegacyCatalogueRecoveryPolicy.CancelPhotonSearch(__state, region, Network.peerType == NetworkPeerType.Disconnected))
                {
                    float previousTimer = (float)joinTimer.GetValue(__instance);
                    // Native cancellation stops the exact saved coroutine, disconnects
                    // only Photon and clears its late OnConnectedToMaster join marker.
                    // It does not clear the shared timer; leaving this browser does.
                    cancelPhotonSearch.Invoke(__instance, new object[] { false });
                    joinTimer.SetValue(__instance, 0f);
                    refreshTimer.SetValue(__instance, 1f);
                    if (__instance == socketOwner
                        && Environment.GetEnvironmentVariable("MPATCHER_LEGACY_CATALOGUE_REOPEN_DISABLE") != "1")
                    {
                        // Defer cleanup to the next ordinary query, outside the
                        // tab callback. Keep the native pending-operation guard
                        // and existing cooldown; never reset the game transport.
                        socketPolicy.Reopened();
                        Log("QUERY_SESSION_RECONSIDERED cause=tab-return requiresPriorFailure=true cooldown=30s pendingGuard=unchanged");
                    }
                    ReplayList(__instance);
                    Log("PHOTON_SEARCH_CANCELLED from=" + __state + " to=" + region
                        + " previousTimer=" + previousTimer + " joining=0 refresh=1 gamePeer=unchanged");
                }
                HostData[] hosts = (HostData[])displayedHosts.GetValue(__instance);
                Log("TAB_SWITCH from=" + __state + " to=" + region + " refresh=" + refreshTimer.GetValue(__instance)
                    + " joining=" + joinTimer.GetValue(__instance) + " retainedRows=" + (hosts == null ? 0 : hosts.Length)
                    + " nativeRows=" + MasterServer.PollHostList().Length + " peer=" + Network.peerType);
            }
            catch (Exception error) { Log("TAB_LOG_FAILED type=" + error.GetType().Name); }
        }
        private static IEnumerable<CodeInstruction> UpdateTranspiler(IEnumerable<CodeInstruction> instructions)
        {
            List<CodeInstruction> code = new List<CodeInstruction>(instructions);
            MethodInfo target = AccessTools.Method(typeof(MasterServer), "RequestHostList", new Type[] { typeof(string) });
            int count = 0;
            for (int i = 0; i < code.Count; i++)
            {
                CodeInstruction item = code[i];
                if (item.opcode == OpCodes.Call && Equals(item.operand, target))
                {
                    CodeInstruction owner = new CodeInstruction(OpCodes.Ldarg_0);
                    owner.labels.AddRange(item.labels); item.labels.Clear();
                    owner.blocks.AddRange(item.blocks); item.blocks.Clear();
                    code.Insert(i++, owner);
                    item.operand = AccessTools.Method(typeof(LegacyCatalogueRecovery), "RequestList"); count++;
                }
            }
            if (count != 1) throw new InvalidOperationException("Expected one native Lobby catalogue request: " + count);
            return code;
        }
        private static void RequestList(string gameType, Lobby owner)
        {
            if (owner != socketOwner) { socketOwner = owner; socketPolicy = new LegacyCatalogueSocketPolicy(); }
            requestedGameType = gameType;
            try
            {
                float now = Time.realtimeSinceStartup;
                if (owner != null && JKGKJLLFMLE.IGOBPLOLHEP.photonRegion == 4
                    && (float)joinTimer.GetValue(owner) <= 0f)
                {
                    string orphan = LegacyCatalogueQueryPeer.ReleaseOrphanedQuery();
                    if (orphan != null) Log("ORPHANED_QUERY_RELEASED " + orphan);
                }
                if (owner != null && socketPolicy.ShouldRestart(JKGKJLLFMLE.IGOBPLOLHEP.photonRegion == 4,
                    Network.peerType == NetworkPeerType.Disconnected, (float)joinTimer.GetValue(owner), now))
                {
                    try
                    {
                        // Pending native work can refuse cleanup. Do not consume
                        // the recovery request or its 30s success cooldown then.
                        string result = LegacyCatalogueQueryPeer.Restart();
                        socketPolicy.Attempted(now);
                        Log("QUERY_SOCKET_RESTART " + result);
                    }
                    catch
                    {
                        socketPolicy.Deferred(now);
                        throw;
                    }
                }
            }
            catch (Exception error) { Log("QUERY_SOCKET_RESTART_REFUSED type=" + error.GetType().Name + " message=" + error.Message); }
            Log("QUERY_BEGIN region=" + JKGKJLLFMLE.IGOBPLOLHEP.photonRegion + " gameType=" + gameType
                + " endpoint=" + MasterServer.ipAddress + ":" + MasterServer.port + " peer=" + Network.peerType);
            MasterServer.RequestHostList(gameType);
        }
        private static void ReceivedPostfix(Connect __instance, MasterServerEvent __0)
        {
            if (__0 != MasterServerEvent.HostListReceived || !(__instance is Lobby)) return;
            if (__instance == socketOwner) socketPolicy.Received();
            try
            {
                HostData[] hosts = (HostData[])displayedHosts.GetValue(__instance);
                if (__instance == socketOwner && JKGKJLLFMLE.IGOBPLOLHEP.photonRegion == 4
                    && (float)joinTimer.GetValue(__instance) <= 0f
                    && requestedGameType == (string)gameTypeField.GetValue(__instance))
                {
                    cachedOwner = (Lobby)__instance; cachedGameType = requestedGameType;
                    cachedHosts = hosts; cachedAt = Time.realtimeSinceStartup;
                }
                Log("LIST_RECEIVED region=" + JKGKJLLFMLE.IGOBPLOLHEP.photonRegion
                    + " nativeRows=" + MasterServer.PollHostList().Length + " retainedRows=" + (hosts == null ? 0 : hosts.Length)
                    + " visibleRows=" + VisibleRows());
            }
            catch (Exception error) { Log("LIST_LOG_FAILED type=" + error.GetType().Name); }
        }
        // The native tab handler hides rows but retains the hover/join array.
        // Redraw the last received list using the native formatter/filter/button
        // path. A scoped Poll replacement avoids modifying the network cache.
        private static void ReplayList(Lobby owner)
        {
            if (owner != cachedOwner || cachedHosts == null
                || cachedGameType != (string)gameTypeField.GetValue(owner)) return;
            try
            {
                replayHosts = cachedHosts;
                drawList.Invoke(owner, new object[] { true });
                Log("CACHED_LIST_SHOWN rows=" + cachedHosts.Length + " visibleRows=" + VisibleRows()
                    + " age=" + (Time.realtimeSinceStartup - cachedAt) + " fresh=false");
            }
            finally { replayHosts = null; }
        }
        private static HostData[] PollForDisplay()
        { return replayHosts ?? MasterServer.PollHostList(); }
        private static IEnumerable<CodeInstruction> DisplayTranspiler(IEnumerable<CodeInstruction> instructions)
        {
            MethodInfo poll = AccessTools.Method(typeof(MasterServer), "PollHostList", Type.EmptyTypes);
            int count = 0;
            foreach (CodeInstruction item in instructions)
            {
                if (item.opcode == OpCodes.Call && Equals(item.operand, poll))
                { item.operand = AccessTools.Method(typeof(LegacyCatalogueRecovery), "PollForDisplay"); count++; }
                yield return item;
            }
            if (count != 1) throw new InvalidOperationException("Expected one native display poll: " + count);
        }
        private static int VisibleRows()
        {
            GameObject group = GameObject.Find("GRP_Server");
            if (group == null) return -1;
            int count = 0;
            for (int i = 0; i < 12; i++)
            {
                Transform row = group.transform.FindChild("PNL_Server" + i);
                if (row != null && row.gameObject.activeInHierarchy) count++;
            }
            return count;
        }
        private static void Log(string value)
        {
            try { mK6lLU33ECSzxV4u22c7_0024ijC0MeyAkqA_PRIEl9WpAZK.YELeoCirSeVGf6u7nOIXkng("[LEGACY-CATALOGUE] " + value); }
            catch { }
        }
    }
}
