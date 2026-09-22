using System;
using System.Collections.Generic;
using System.Reflection;
using System.Reflection.Emit;
using HarmonyLib;
using MPatchrMain;
using UnityEngine;

namespace MPatcherFork.CustomPatches
{
    internal static class LegacyHostAutoPort
    {
        private const string PatchId = "mpatcher.legacy-host-auto-port.v1";
        private static Harmony harmony;
        internal static LegacyHostAutoPortDriver Current;
        internal static bool Enabled { get { return LegacyIndivFixBundle.Enabled; } }
        internal static void TryRegister()
        {
            if (harmony != null) return;
            Harmony candidate = new Harmony(PatchId);
            try
            {
                candidate.Patch(AccessTools.Method(typeof(Configure), "EOHFEIHAMHD", Type.EmptyTypes),
                    null, null, new HarmonyMethod(typeof(LegacyHostAutoPort), "CaptureInitialize"));
                candidate.Patch(AccessTools.Method(typeof(Configure), "BDKIMPEDKCJ", new Type[] { typeof(string), typeof(GameObject) }),
                    new HarmonyMethod(typeof(LegacyHostAutoPort), "ButtonPrefix"));
                candidate.Patch(AccessTools.Method(typeof(Connect), "OnFailedToConnectToMasterServer", new Type[] { typeof(NetworkConnectionError) }),
                    new HarmonyMethod(typeof(LegacyHostAutoPort), "MasterFailurePrefix"));
                harmony = candidate;
                LegacyHostAutoPortProbe.TryRegister();
                Log("REGISTERED version=1 default=true stage=before-world candidates=preferred,23466,35017,25551 bound=180s admission=closed liveMigration=false");
            }
            catch (Exception error) { candidate.UnpatchAll(PatchId); Log("REGISTER_FAILED " + error); }
        }
        internal static void SetEnabled(bool value)
        {
            MPatchr._0024Ymloe9RVCTW7x1ASuQ3c68.legacyHostAutoPort = value;
            MPatchr._0024Ymloe9RVCTW7x1ASuQ3c68.UUiRNMwxRbfk_Fs4cDErRoM();
            Log("SETTING enabled=" + value);
        }
        private static IEnumerable<CodeInstruction> CaptureInitialize(IEnumerable<CodeInstruction> input)
        {
            List<CodeInstruction> result = new List<CodeInstruction>(); int count = 0;
            MethodInfo original = AccessTools.Method(typeof(Network), "InitializeServer", new Type[] { typeof(int), typeof(int), typeof(bool) });
            foreach (CodeInstruction instruction in input)
            {
                CodeInstruction copy = new CodeInstruction(instruction);
                if (copy.opcode == OpCodes.Call && Equals(copy.operand, original))
                { copy.operand = AccessTools.Method(typeof(LegacyHostAutoPort), "InitializeOrCapture"); count++; }
                result.Add(copy);
            }
            if (count != 1) throw new InvalidOperationException("Expected one Configure InitializeServer call, found " + count);
            return result;
        }
        private static NetworkConnectionError InitializeOrCapture(int capacity, int port, bool nat)
        {
            if (Current == null || !Current.Capturing) return Network.InitializeServer(capacity, port, nat);
            Current.Capacity = capacity; Current.PreferredPort = port; Current.UseNat = nat;
            // The native method ignores the result. Opening is deferred until its
            // complete metadata and settings have been captured, without loading a world.
            return NetworkConnectionError.NoError;
        }
        internal static bool CaptureRegistration(string type, string name, string comment)
        {
            if (Current == null || !Current.Capturing) return false;
            Current.Capture(type, name, comment); return true;
        }
        private static bool ButtonPrefix(Configure __instance, string __0)
        {
            if (Current != null && Current.Owner == __instance)
            {
                if (__0 == "Exit") Current.Cancel("native exit");
                else return false;
            }
            if (__0 != "Start" || !Enabled) return true;
            if (!LegacyHostStability.Registered || !LegacyMasterLifecycleFix.Applied || !LegacyMasterQueryFix.Applied)
            { Log("BYPASS reason=required-native-lifecycle-fix-unavailable"); return true; }
            // Never take ownership of a transport created elsewhere.
            if (Network.peerType != NetworkPeerType.Disconnected || Network.connections.Length != 0)
            { Log("START_REJECTED reason=existing-transport"); return false; }
            try
            {
                LegacyHostPortPolicy.Candidates(JKGKJLLFMLE.IGOBPLOLHEP.port);
                Current = __instance.gameObject.AddComponent<LegacyHostAutoPortDriver>();
                Current.Begin(__instance);
                HNJDDKJLHMM.FHLGOMHPDLN = HNJDDKJLHMM.HKGAACMIPIH.Legacy;
                // NKDGNMMOMBB fills BOTH Legacy text and Photon containers, but
                // EOHFEIHAMHD clears the containers only for Photon. Native Start
                // normally leaves Configure immediately; cancellation now allows a
                // second Start on that same instance, so rebuild these scratch fields.
                ((System.Collections.IDictionary)AccessTools.Field(typeof(Configure), "HKEIAHNEHKN").GetValue(__instance)).Clear();
                ((System.Collections.IList)AccessTools.Field(typeof(Configure), "ANFKKKLFJAB").GetValue(__instance)).Clear();
                Current.Capturing = true;
                AccessTools.Method(typeof(Configure), "EOHFEIHAMHD", Type.EmptyTypes).Invoke(__instance, null);
                Current.Capturing = false;
                if (!Current.HasMetadata) Current.Cancel("native start rejected before registration");
                else Current.StartSelection();
            }
            catch (Exception error)
            {
                Log("START_FAILED " + error);
                if (Current != null) Current.Cancel("start exception");
            }
            return false;
        }
        private static bool MasterFailurePrefix(Connect __instance)
        {
            // Native Connect otherwise returns to Menu during a port cooldown.
            return Current == null || Current.Owner != __instance;
        }
        internal static void EnterWorld(Configure owner)
        {
            string scene = HNJDDKJLHMM.OLHPKFAKNOG ? "Host" : JKGKJLLFMLE.AMHCEFLLMIE();
            AccessTools.Method(typeof(SceneMan), "CJLFFPJICPC", new Type[] { typeof(string), typeof(bool) })
                .Invoke(owner, new object[] { scene, false });
        }
        internal static void Log(string value)
        { try { mK6lLU33ECSzxV4u22c7_0024ijC0MeyAkqA_PRIEl9WpAZK.YELeoCirSeVGf6u7nOIXkng("[LEGACY-AUTO-PORT] " + value); } catch { } }
    }

    internal sealed class LegacyHostAutoPortDriver : MonoBehaviour
    {
        internal Configure Owner;
        internal bool Capturing, HasMetadata, UseNat;
        internal int Capacity, PreferredPort;
        internal readonly LegacyHostPortPolicy Policy = new LegacyHostPortPolicy();
        private string gameType, gameName, comment, ownedGuid, attemptNonce, oldPassword;
        private string masterAddress, facilitatorAddress;
        private int masterPort, facilitatorPort, pendingPort;
        private bool running, owned, committed, cleaned, oldServer, oldDedicated, oldRole, oldMasterDedicated;
        private HNJDDKJLHMM.HKGAACMIPIH oldMode;
        private string oldName;
        private float nextOpen;
        private LegacyHostAutoPortUi ui;
        private string lastState;
        internal void Begin(Configure owner)
        {
            Owner = owner; oldPassword = Network.incomingPassword;
            oldServer = HNJDDKJLHMM.IOOILBCOFMF; oldDedicated = HNJDDKJLHMM.OLHPKFAKNOG;
            oldRole = HNJDDKJLHMM.NIKEKIIPJFI; oldMode = HNJDDKJLHMM.FHLGOMHPDLN;
            oldName = HNJDDKJLHMM.JCOLMIBIGOP; oldMasterDedicated = MasterServer.dedicatedServer;
            masterAddress = MasterServer.ipAddress; masterPort = MasterServer.port;
            facilitatorAddress = Network.natFacilitatorIP; facilitatorPort = Network.natFacilitatorPort;
            ui = LegacyHostAutoPortUi.Create(owner, this);
        }
        internal void Capture(string type, string name, string data)
        { gameType = type; gameName = name; comment = data; HasMetadata = !string.IsNullOrEmpty(type) && !string.IsNullOrEmpty(name); }
        internal void StartSelection()
        {
            Policy.Begin(PreferredPort, Time.realtimeSinceStartup); running = true;
            LegacyHostAutoPort.Log("BEGIN preferred=" + PreferredPort + " candidates=" + Policy.Count + " capacity=" + Capacity
                + " type=" + gameType + " nat=" + UseNat + " worldLoaded=false");
        }
        private bool SameEndpoint()
        { return MasterServer.ipAddress == masterAddress && MasterServer.port == masterPort
            && Network.natFacilitatorIP == facilitatorAddress && Network.natFacilitatorPort == facilitatorPort; }
        private bool OwnPeer()
        { return owned && Network.isServer && Network.player.guid == ownedGuid; }
        private void Update()
        {
            if (!running) return;
            try
            {
                if (Owner == null || !SameEndpoint()) { Cancel("owner or service endpoint changed"); return; }
                // Check peers before every action, including teardown, timeout and manual start.
                if (Network.connections.Length > 0)
                { LegacyHostAutoPort.Log("SAFETY_STOP peers-present migration=false"); if (OwnPeer()) Commit(false); else Cancel("foreign players"); return; }
                double now = Time.realtimeSinceStartup;
                if (pendingPort != 0)
                {
                    if (now < nextOpen) return;
                    OpenCandidate(pendingPort); pendingPort = 0;
                }
                // A bind result or an asynchronous list callback can exhaust the
                // policy before Tick; terminal policies intentionally emit no action.
                if (Policy.Phase == LegacyHostPortPhase.Exhausted) { Fail(); return; }
                IntPtr manager;
                bool facilitatorPending = LegacyHostFacilitatorReconnectFix.TryGetPending(out manager);
                LegacyHostPortAction action = Policy.Tick(now, false, facilitatorPending);
                if (action == LegacyHostPortAction.OpenPort)
                {
                    if (!ReleasePeer()) { Cancel("transport ownership changed"); return; }
                    pendingPort = Policy.Port; nextOpen = Time.realtimeSinceStartup + 1f;
                }
                else if (action == LegacyHostPortAction.Query)
                {
                    if (!OwnPeer()) { Cancel("candidate transport lost"); return; }
                    MasterServer.ClearHostList(); MasterServer.RequestHostList(gameType);
                    LegacyHostAutoPort.Log("QUERY port=" + Policy.Port + " guid=" + ownedGuid);
                }
                else if (action == LegacyHostPortAction.Register) Register();
                else if (action == LegacyHostPortAction.Commit) Commit(true);
                else if (action == LegacyHostPortAction.Stop) Fail();
                Report();
                if (ui != null) ui.Refresh(this);
            }
            catch (Exception error) { LegacyHostAutoPort.Log("FAILED " + error); Cancel("adapter exception"); }
        }
        private void OpenCandidate(int port)
        {
            if (Network.peerType != NetworkPeerType.Disconnected || Network.connections.Length != 0)
                throw new InvalidOperationException("Candidate open requires a disconnected peer");
            // Password closes even the synchronous InitializeServer/maxConnections window.
            Network.incomingPassword = Guid.NewGuid().ToString("N");
            LegacyHostFacilitatorReconnectFix.ClearPending();
            NetworkConnectionError result = Network.InitializeServer(Capacity, port, UseNat);
            owned = result == NetworkConnectionError.NoError && Network.isServer;
            ownedGuid = owned ? Network.player.guid : null;
            attemptNonce = Guid.NewGuid().ToString("N");
            if (owned) Network.maxConnections = 0;
            LegacyHostAutoPort.Log("OPEN port=" + port + " result=" + result + " server=" + owned
                + " guid=" + ownedGuid + " connections=" + Network.connections.Length + " admission=closed");
            Policy.Opened(Time.realtimeSinceStartup, owned);
            if (owned) Register();
        }
        private void Register()
        {
            if (!OwnPeer()) throw new InvalidOperationException("Registration requires owned peer");
            MasterServer.RegisterHost(gameType, gameName, "~MPA" + attemptNonce + comment);
            LegacyHostAutoPort.Log("REGISTER port=" + Policy.Port + " guid=" + ownedGuid + " attemptNonce=" + attemptNonce);
        }
        private void OnMasterServerEvent(MasterServerEvent value)
        {
            if (!running || !OwnPeer()) return;
            if (value == MasterServerEvent.HostListReceived && Policy.Phase == LegacyHostPortPhase.WaitingReply)
            {
                HostData[] list = MasterServer.PollHostList(); bool found = false;
                foreach (HostData host in list)
                    if (host != null && LegacyHostPortPolicy.MatchesListing(ownedGuid, attemptNonce, host.guid, host.comment)) { found = true; break; }
                Policy.ListReceived(Time.realtimeSinceStartup, found);
                LegacyHostAutoPort.Log("LIST port=" + Policy.Port + " guid=" + ownedGuid + " found=" + found + " entries=" + list.Length);
            }
            else LegacyHostAutoPort.Log("MASTER_EVENT value=" + value + " port=" + Policy.Port);
        }
        private void OnFailedToConnectToMasterServer(NetworkConnectionError error)
        { if (running) LegacyHostAutoPort.Log("MASTER_FAILURE error=" + error + " visibility=unknown waitForListDeadline=true"); }
        internal bool CanStartUnchecked { get { return running && OwnPeer() && pendingPort == 0; } }
        internal void Commit(bool verified)
        {
            if (!CanStartUnchecked || Owner == null) return;
            int actualPort = Network.player.port;
            // Native scene transition stops incoming messages until the world is ready.
            // Publication monitor takes over this same GUID; it never restarts the peer.
            Network.incomingPassword = oldPassword;
            Network.maxConnections = Capacity;
            LegacyHostStability.RegisterOriginalAndCapture(gameType, gameName, comment);
            LegacyHostAutoPort.Log("COMMIT port=" + actualPort + " guid=" + ownedGuid + " listed=" + verified
                + " externalJoin=unverified reinitialized=false capacity=" + Capacity);
            // This is a session choice, not an overwrite of the user's preferred port.
            LegacyHostAutoPort.EnterWorld(Owner);
            committed = true; running = owned = false;
            if (ui != null) ui.Close();
            LegacyHostAutoPort.Current = null;
            Destroy(this);
        }
        private bool ReleasePeer()
        {
            if (Network.connections.Length != 0) return false;
            if (owned)
            {
                if (!OwnPeer() && Network.peerType != NetworkPeerType.Disconnected) return false;
                MasterServer.UnregisterHost();
                if (OwnPeer()) Network.Disconnect();
                owned = false; ownedGuid = null;
            }
            return Network.peerType == NetworkPeerType.Disconnected;
        }
        private void Fail()
        {
            running = false; pendingPort = 0;
            ReleasePeer(); Restore(); Report();
            if (ui != null) ui.Refresh(this);
            LegacyHostAutoPort.Log("EXHAUSTED reason=" + Policy.Reason + " next=retry-or-cancel worldLoaded=false");
        }
        internal void Cancel(string reason)
        {
            if (committed || cleaned) return;
            running = false; pendingPort = 0; Policy.Cancel(); ReleasePeer(); Restore();
            if (ui != null) ui.Close();
            if (LegacyHostAutoPort.Current == this) LegacyHostAutoPort.Current = null;
            LegacyHostAutoPort.Log("CANCEL reason=" + reason + " connections=" + Network.connections.Length);
            cleaned = true;
            Destroy(this);
        }
        private void Restore()
        {
            // A foreign transport is never mutated during cleanup.
            if (Network.peerType != NetworkPeerType.Disconnected) return;
            Network.incomingPassword = oldPassword; MasterServer.dedicatedServer = oldMasterDedicated;
            HNJDDKJLHMM.IOOILBCOFMF = oldServer; HNJDDKJLHMM.OLHPKFAKNOG = oldDedicated;
            HNJDDKJLHMM.NIKEKIIPJFI = oldRole; HNJDDKJLHMM.FHLGOMHPDLN = oldMode; HNJDDKJLHMM.JCOLMIBIGOP = oldName;
            LegacyHostFacilitatorReconnectFix.ClearPending();
        }
        private void Report()
        {
            string state = "phase=" + Policy.Phase + " port=" + Policy.Port + " attempt=" + Policy.Attempt
                + "/" + Policy.Count + " evidence=" + Policy.Evidence + " positives=" + Policy.PositiveReplies + " reason=" + Policy.Reason;
            if (state == lastState) return; lastState = state; LegacyHostAutoPort.Log("STATE " + state);
        }
        private void OnApplicationQuit() { if (!committed) Cancel("application quit"); }
        private void OnDestroy()
        {
            if (!committed && !cleaned) { running = false; ReleasePeer(); Restore(); if (ui != null) ui.Close(); cleaned = true; }
            if (LegacyHostAutoPort.Current == this) LegacyHostAutoPort.Current = null;
        }
    }
}
