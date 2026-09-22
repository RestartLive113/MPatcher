using System;
using System.Collections.Generic;
using HarmonyLib;
using McnCraft;
using UnityEngine;

namespace MPatcherFork.CustomPatches
{
    internal static class LegacyHostMigration
    {
        internal static LegacyHostMigrationDriver Current;
        private static bool registered;
        internal static bool Enabled { get { return LegacyIndivFixBundle.Enabled; } }
        internal static void SetEnabled(bool value)
        { MPatchrMain.MPatchr._0024Ymloe9RVCTW7x1ASuQ3c68.legacyHostMigration = value;
            MPatchrMain.MPatchr._0024Ymloe9RVCTW7x1ASuQ3c68.UUiRNMwxRbfk_Fs4cDErRoM(); Log("SETTING enabled=" + value + " saved=true"); }
        internal static bool Active { get { return Current != null; } }
        internal static bool SuppressDisconnect { get { return Current != null && Current.ReplacingTransport; } }
        internal static void TryRegister()
        {
            if (registered) return;
            Harmony h = new Harmony("mpatcher.legacy-host-migration.v1");
            h.Patch(AccessTools.Method(typeof(Game), "Start"), null, new HarmonyMethod(typeof(LegacyHostMigration), "GameStarted"));
            h.Patch(AccessTools.Method(typeof(MachineController), "Update"), new HarmonyMethod(typeof(LegacyHostMigration), "MachineUpdate"));
            registered = true;
            LegacyHostMigrationProbe.TryRegister();
            Log("REGISTERED version=3 compatiblePeersRequired=true absentSeconds=120 failures=3 cooldown=600 nativeState=preserved retainedCloneReplay=migration+connection-loss voluntaryRetirement=original+previous+current");
        }
        private static void GameStarted(Game __instance)
        {
            if (!LegacyHostStability.IsLegacyHost()) return;
            if (__instance.GetComponent<LegacyHostMigrationDriver>() == null) __instance.gameObject.AddComponent<LegacyHostMigrationDriver>();
        }
        private static bool MachineUpdate(MachineController __instance)
        { return !LegacyMigrationFreeze.Holds(__instance); }
        internal static void Stop(string reason)
        { if (Current != null) Current.Exit(reason); }
        internal static void Log(string message)
        { LegacyTransientReconnect.Log("[HOST-MIGRATION] " + message); }
    }

    internal sealed class LegacyMigrationFreeze : IDisposable
    {
        private static float scale;
        private bool active;
        private static int count;
        private readonly List<OwnerBody> ownerBodies = new List<OwnerBody>();
        private readonly MachineController owner;
        private static readonly HashSet<MachineController> heldOwners = new HashSet<MachineController>();
        private sealed class OwnerBody
        { internal BodyController Body; internal Vector3 Position; internal Quaternion Rotation; internal float Health, Bonus; }
        internal static bool Active { get { return count > 0; } }
        internal static bool Holds(MachineController machine) { return heldOwners.Contains(machine); }
        internal LegacyMigrationFreeze()
        {
            Game game = UnityEngine.Object.FindObjectOfType<Game>();
            owner = game == null ? null : game.FICMBCLEFDL;
            if (game != null && game.FICMBCLEFDL != null)
            {
                List<BodyController> bodies; string source, reason;
                if (LegacyTransientReconnect.TryCollectMachineBodies(game.FICMBCLEFDL, out bodies, out source, out reason))
                    foreach (BodyController b in bodies) if (b != null && b.NFMPBACKJOJ != null)
                        ownerBodies.Add(new OwnerBody { Body = b, Position = b.NFMPBACKJOJ.position, Rotation = b.NFMPBACKJOJ.rotation,
                            Health = b.MEJNIODBGFI, Bonus = b.HPOMNJCEJIP });
            }
            // Complete fallible snapshot work before changing global time.
            int bodyCount = UnityEngine.Object.FindObjectsOfType(typeof(Rigidbody)).Length;
            if (count == 0) scale = Time.timeScale;
            Time.timeScale = 0; count++; active = true;
            if (owner != null) heldOwners.Add(owner);
            LegacyHostMigration.Log("WORLD_PAUSED bodies=" + bodyCount);
        }
        public void Dispose()
        {
            if (!active) return; active = false; count--;
            if (owner != null) heldOwners.Remove(owner);
            if (count == 0) Time.timeScale = scale;
            float position = 0, rotation = 0, health = 0; int missing = 0;
            foreach (OwnerBody b in ownerBodies)
            {
                if (b.Body == null || b.Body.NFMPBACKJOJ == null) { missing++; continue; }
                position = Mathf.Max(position, Vector3.Distance(b.Position, b.Body.NFMPBACKJOJ.position));
                Quaternion current = b.Body.NFMPBACKJOJ.rotation;
                if (!b.Rotation.Equals(current)) rotation = Mathf.Max(rotation, Quaternion.Angle(b.Rotation, current));
                health = Mathf.Max(health, Mathf.Abs(b.Health - b.Body.MEJNIODBGFI), Mathf.Abs(b.Bonus - b.Body.HPOMNJCEJIP));
            }
            LegacyHostMigration.Log("WORLD_RESUMED timeScale=" + scale + " ownerBodies=" + ownerBodies.Count + " missing=" + missing
                + " maxPositionDelta=" + position + " maxAngleDelta=" + rotation + " maxHealthDelta=" + health);
        }
    }

    internal sealed class LegacyHostMigrationDriver : MonoBehaviour
    {
        private readonly LegacyHostMigrationPolicy outage = new LegacyHostMigrationPolicy();
        private readonly LegacyHostPortPolicy ports = new LegacyHostPortPolicy();
        private LegacyMigrationBarrier ack, ready;
        private LegacyMigrationFreeze freeze;
        private string epoch, session, gameType, gameName, password, nonce;
        private int oldPort, capacity;
        private bool nat, switched, verified;
        private float deadline, nextCheck;
        private int phase; // 0 idle, 1 preparing, 2 selecting, 3 reconnect barrier
        private readonly HashSet<string> participantGuids = new HashSet<string>();
        internal bool ReplacingTransport { get { return switched && phase == 2; } }
        internal bool Selecting { get { return phase == 2; } }
        internal string State { get { return "phase=" + phase + " port=" + Network.player.port + " peers=" + Network.connections.Length
            + " ack=" + (ack == null ? "none" : ack.Arrived + "/" + ack.Count) + " ready=" + (ready == null ? "none" : ready.Arrived + "/" + ready.Count); } }

        internal void ObserveCatalogue(bool listed, int failures)
        {
            float now = Time.realtimeSinceStartup;
            if (phase != 0 || !LegacyHostMigration.Enabled) return;
            if (listed || failures > 0) outage.Observe(now, listed);
            if (outage.Due(now, failures) && now >= nextCheck)
            { nextCheck = now + 30f; Begin("persistent-catalogue-outage"); }
        }
        internal bool Begin(string reason)
        {
            if (phase != 0 || !LegacyHostMigration.Enabled || !LegacyHostStability.IsLegacyHost()
                || LegacyHostAutoPort.Current != null) return false;
            LegacyHostStabilityDriver publication = LegacyHostStability.Driver;
            string[] tokens = LegacyTransientReconnect.MigrationParticipants();
            if (publication == null || tokens == null)
            { LegacyHostMigration.Log("DEFER reason=incompatible-or-unregistered-peer transportUnchanged=true"); return false; }
            try
            {
                LegacyHostMigrationNative.CheckProfile();
                // Validate native storage without replacing the transport.
                using (LegacyHostMigrationNative check = new LegacyHostMigrationNative()) { }
                nat = LegacyHostMigrationNative.UseNat();
                gameType = publication.MigrationGameType; gameName = publication.MigrationGameName;
                session = publication.MigrationSession;
                if (string.IsNullOrEmpty(gameType) || !LegacyHostRoutePolicy.ValidSession(session)) return false;
                oldPort = Network.player.port; capacity = Network.maxConnections; password = Network.incomingPassword;
                epoch = Guid.NewGuid().ToString("N"); ack = new LegacyMigrationBarrier(epoch, tokens); ready = new LegacyMigrationBarrier(epoch, tokens);
                participantGuids.Clear(); foreach (NetworkPlayer p in Network.connections) participantGuids.Add(p.guid);
                phase = 1; switched = verified = false; LegacyHostMigration.Current = this;
                outage.Attempted(Time.realtimeSinceStartup);
                Network.maxConnections = 0; freeze = new LegacyMigrationFreeze();
                deadline = Time.realtimeSinceStartup + 12f;
                LegacyTransientReconnect.MigrationSend("MPatcherMigrationPrepareV1", epoch, session);
                LegacyHostMigration.Log("PREPARE reason=" + reason + " epoch=" + epoch + " session=" + session + " oldPort=" + oldPort + " participants=" + tokens.Length);
                return true;
            }
            catch (Exception error) { LegacyHostMigration.Log("PREPARE_FAILED " + error); Exit("prepare-failed"); return false; }
        }
        internal void Acknowledge(string incomingEpoch, string token, bool authenticated, bool restored)
        {
            LegacyMigrationBarrier barrier = restored ? ready : ack;
            if (barrier == null || restored && phase != 3 || !restored && phase != 1) return;
            if (barrier.Accept(incomingEpoch, token, authenticated))
                LegacyHostMigration.Log((restored ? "READY " : "ACK ") + State);
        }
        private void Update()
        {
            if (phase == 0) return;
            float now = Time.realtimeSinceStartup;
            try
            {
                if (!LegacyTransientReconnect.IsLegacy()) { Exit("scene-exit"); return; }
                if (phase == 1)
                {
                    if (!LegacyHostMigration.Enabled) { Exit("disabled-before-switch"); return; }
                    if (now >= deadline) { Exit("prepare-timeout-transport-kept"); return; }
                    if (!ack.Complete) return;
                    string[] actual = LegacyTransientReconnect.MigrationParticipants();
                    if (actual == null || actual.Length != ack.Count) { Exit("participants-changed"); return; }
                    foreach (string token in actual) if (!ack.Contains(token)) { Exit("participants-changed"); return; }
                    // ACK proves each current owner has paused and armed intentional disconnect recovery.
                    LegacyTransientReconnect.RetainForMigration();
                    switched = true; phase = 2;
                    ports.BeginCandidates(LegacyHostMigrationPolicy.Alternatives(oldPort), now);
                    LegacyHostMigration.Log("SWITCH_BEGIN " + State);
                }
                if (phase == 2)
                {
                    if (ports.Phase == LegacyHostPortPhase.Exhausted) { Rollback(); return; }
                    IntPtr unused;
                    LegacyHostPortAction action = ports.Tick(now, false, LegacyHostFacilitatorReconnectFix.TryGetPending(out unused));
                    if (action == LegacyHostPortAction.OpenPort) Open(ports.Port, true);
                    else if (action == LegacyHostPortAction.Query)
                    { MasterServer.ClearHostList(); MasterServer.RequestHostList(gameType); LegacyHostMigration.Log("QUERY port=" + ports.Port); }
                    else if (action == LegacyHostPortAction.Register) Publish();
                    else if (action == LegacyHostPortAction.Commit) Admit(true);
                    else if (action == LegacyHostPortAction.Stop) Rollback();
                }
                else if (phase == 3 && (ready.Complete || now >= deadline))
                { LegacyHostMigration.Log("BARRIER_FINISHED allReturned=" + ready.Complete + " " + State); Exit(ready.Complete ? "complete" : "some-peers-still-recovering"); }
            }
            catch (Exception error)
            {
                LegacyHostMigration.Log("FAILED " + error);
                if (switched && phase == 2) { try { Rollback(); } catch (Exception rollback) { LegacyHostMigration.Log("ROLLBACK_FAILED " + rollback); Exit("rollback-failed"); } }
                else Exit("adapter-failed");
            }
        }
        private void Open(int port, bool candidate)
        {
            Network.incomingPassword = Guid.NewGuid().ToString("N");
            LegacyHostFacilitatorReconnectFix.ClearPending();
            int buffered;
            NetworkConnectionError result;
            using (LegacyHostMigrationNative saved = new LegacyHostMigrationNative())
            { buffered = saved.BufferedCount; result = Network.InitializeServer(capacity, port, nat); }
            if (Network.isServer) Network.maxConnections = 0;
            nonce = Guid.NewGuid().ToString("N");
            bool success = result == NetworkConnectionError.NoError && Network.isServer;
            LegacyHostMigration.Log("OPEN port=" + port + " result=" + result + " bufferedRpcs=" + buffered
                + " views=" + UnityEngine.Object.FindObjectsOfType(typeof(NetworkView)).Length + " world=kept session=" + session);
            if (candidate) ports.Opened(Time.realtimeSinceStartup, success);
            else if (!success) throw new InvalidOperationException("Original port unavailable: " + result);
            if (success) Publish();
        }
        private void Publish()
        {
            LegacyHostStabilityDriver publication = LegacyHostStability.Driver;
            if (!Network.isServer || publication == null || publication.MigrationSession != session) throw new InvalidOperationException("Migration publication lost");
            MasterServer.RegisterHost(gameType, gameName, "~MPA" + nonce + publication.PublishedComment());
        }
        private void Rollback()
        { LegacyHostMigration.Log("ROLLBACK oldPort=" + oldPort + " reason=" + ports.Reason); Open(oldPort, false); Admit(false); }
        private void Admit(bool listed)
        {
            verified = listed; Network.incomingPassword = password; Network.maxConnections = capacity;
            phase = 3; deadline = Time.realtimeSinceStartup + 75f;
            LegacyHostStability.Driver.MigrationRebound();
            LegacyHostMigration.Log("ADMIT verified=" + listed + " oldPort=" + oldPort + " newPort=" + Network.player.port + " session=" + session + " " + State);
        }
        private void OnMasterServerEvent(MasterServerEvent value)
        {
            if (phase != 2 || value != MasterServerEvent.HostListReceived || ports.Phase != LegacyHostPortPhase.WaitingReply) return;
            bool found = false;
            HostData[] entries = MasterServer.PollHostList();
            foreach (HostData host in entries)
            {
                if (host != null && host.guid == Network.player.guid)
                    LegacyHostMigration.Log("LIST_OWN_ROW advertisedPort=" + host.port
                        + " sessionMatch=" + (LegacyHostRoutePolicy.Session(host.comment) == session)
                        + " nonceMatch=" + LegacyHostPortPolicy.MatchesListing(Network.player.guid, nonce, host.guid, host.comment));
                // Master may advertise a NAT-mapped port, not our local bind port.
                if (host != null && LegacyHostRoutePolicy.Session(host.comment) == session
                    && LegacyHostPortPolicy.MatchesListing(Network.player.guid, nonce, host.guid, host.comment)) { found = true; break; }
            }
            int queriedPort = ports.Port;
            ports.ListReceived(Time.realtimeSinceStartup, found);
            LegacyHostMigration.Log("LIST found=" + found + " port=" + queriedPort + " entries=" + entries.Length + " positives=" + ports.PositiveReplies);
        }
        internal void Exit(string reason)
        {
            if (phase == 0) return;
            try
            {
                if (Network.isServer)
                {
                    Network.incomingPassword = password; Network.maxConnections = capacity;
                    LegacyTransientReconnect.MigrationSend("MPatcherMigrationReleaseV1", epoch, session);
                }
            }
            catch (Exception error) { LegacyHostMigration.Log("RELEASE_SEND_FAILED " + error); }
            finally
            {
                if (freeze != null) { freeze.Dispose(); freeze = null; }
                outage.Finished(verified); phase = 0;
                if (LegacyHostMigration.Current == this) LegacyHostMigration.Current = null;
                LegacyHostMigration.Log("END reason=" + reason + " verified=" + verified + " world=kept");
            }
        }
        private void OnDestroy() { Exit("game-destroyed"); }
        private void OnApplicationQuit() { Exit("application-quit"); }
        internal bool AdmitParticipant(NetworkPlayer player)
        {
            if (phase != 3 || participantGuids.Contains(player.guid)) return true;
            Network.CloseConnection(player, true);
            LegacyHostMigration.Log("ADMISSION_DEFERRED reason=returning-peers-first"); return false;
        }
    }
}
