using System;
using System.Collections.Generic;
using UnityEngine;

namespace MPatcherFork.CustomPatches
{
    internal static partial class LegacyTransientReconnect
    {
        internal static LegacyTransientReconnectController MigrationController { get { return controller; } }
        private static readonly Dictionary<string, string> migrationCapabilities = new Dictionary<string, string>();
        internal static void ClearMigrationCapabilities() { migrationCapabilities.Clear(); }
        internal static bool MigrationAuthenticated(string token, NetworkPlayer sender)
        {
            ResumeRecord r;
            return ValidToken(token) && records.TryGetValue(token, out r) && r.Active && !r.Voluntary && r.Player == sender;
        }
        internal static void MigrationCapability(string token, NetworkPlayer sender)
        { if (Network.isServer && MigrationAuthenticated(token, sender)) migrationCapabilities[token] = PlayerGuid(sender); }
        internal static string[] MigrationParticipants()
        {
            if (controller == null || !controller.KeepLiveHostScene || !Network.isServer) return null;
            List<string> result = new List<string>();
            foreach (NetworkPlayer peer in Network.connections)
            {
                ResumeRecord match = null;
                foreach (ResumeRecord r in records.Values) if (r.Active && !r.Retained && !r.Voluntary && r.Player == peer) { match = r; break; }
                string guid;
                if (match == null || !migrationCapabilities.TryGetValue(match.Token, out guid) || guid != PlayerGuid(peer)) return null;
                result.Add(match.Token);
            }
            return result.ToArray();
        }
        internal static void RetainForMigration()
        {
            NetworkPlayer[] peers = Network.connections;
            List<ResumeRecord> changed = new List<ResumeRecord>();
            try
            {
                foreach (NetworkPlayer peer in peers)
                {
                    foreach (ResumeRecord r in records.Values) if (r.Active && !r.Retained && r.Player == peer) { changed.Add(r); break; }
                    if (!TryRetainPlayer(peer, controller)) throw new InvalidOperationException("Migration retention failed for " + PlayerLabel(peer));
                }
            }
            catch
            {
                // No socket has changed yet; undo a partial freeze transaction.
                foreach (ResumeRecord r in changed) { ReleaseFrozen(r, "migration-prepare-rollback"); r.Active = true; r.Retained = false; r.RebindInProgress = false; }
                throw;
            }
            // Existing disconnected ghosts must survive the candidate cycle too.
            foreach (ResumeRecord r in records.Values) if (r.Retained) r.RetainUntil = Time.realtimeSinceStartup + 300f;
        }
        internal static void MigrationSend(string rpc, string epoch, string session)
        {
            if (controller == null || controller.SessionView == null) throw new InvalidOperationException("Migration control view absent");
            controller.SessionView.RPC(rpc, RPCMode.Others, epoch, session);
        }
    }

    internal sealed partial class LegacyTransientReconnectController
    {
        private float nextMigrationCapability;
        private string migrationEpoch;
        private float migrationExpires;
        private LegacyMigrationFreeze migrationFreeze;
        private bool migrationReadySent;
        internal bool MigrationArmed { get { return migrationEpoch != null && Time.realtimeSinceStartup < migrationExpires; } }

        private void MigrationTick(float now)
        {
            if (migrationEpoch != null && (voluntary || now >= migrationExpires)) EndClientMigration("expired-or-exit");
            if (!clientRole || voluntary || !Network.isClient || !registered || sessionView == null) return;
            if (LegacyReconnectTarget.MigrationAdvertised && now >= nextMigrationCapability)
            {
                nextMigrationCapability = now + 10f;
                sessionView.RPC("MPatcherMigrationCapabilityV1", RPCMode.Server, token);
            }
            if (MigrationArmed && !recovering && !migrationReadySent && ClientClaimsOwned())
            {
                migrationReadySent = true;
                sessionView.RPC("MPatcherMigrationReadyV1", RPCMode.Server, migrationEpoch, token);
                LegacyHostMigration.Log("CLIENT_READY epoch=" + migrationEpoch);
            }
        }
        [RPC] private void MPatcherMigrationCapabilityV1(string incomingToken, NetworkMessageInfo info)
        { LegacyTransientReconnect.MigrationCapability(incomingToken, info.sender); }
        [RPC] private void MPatcherMigrationPrepareV1(string epoch, string session, NetworkMessageInfo info)
        {
            if (!IsServerMessage(info) || !CanResumeClient || !registered || recovering || crashResumePending
                || !LegacyHostRoutePolicy.ValidSession(epoch) || session != LegacyReconnectTarget.MigrationSession
                || !LegacyReconnectTarget.MigrationAdvertised) return;
            if (migrationEpoch != null && migrationEpoch != epoch) return;
            migrationEpoch = epoch; migrationExpires = Time.realtimeSinceStartup + 290f;
            migrationReadySent = true; // only mark ready AFTER actual reconnect.
            if (migrationFreeze == null)
            {
                migrationFreeze = new LegacyMigrationFreeze();
                CaptureMigrationMachines();
            }
            sessionView.RPC("MPatcherMigrationAckV1", RPCMode.Server, epoch, token);
            LegacyHostMigration.Log("CLIENT_PREPARED epoch=" + epoch + " session=" + session + " frozen=true");
        }
        [RPC] private void MPatcherMigrationAckV1(string epoch, string incomingToken, NetworkMessageInfo info)
        {
            if (LegacyHostMigration.Current != null) LegacyHostMigration.Current.Acknowledge(epoch, incomingToken,
                Network.isServer && LegacyTransientReconnect.MigrationAuthenticated(incomingToken, info.sender), false);
        }
        [RPC] private void MPatcherMigrationReadyV1(string epoch, string incomingToken, NetworkMessageInfo info)
        {
            bool authenticated = Network.isServer && LegacyTransientReconnect.MigrationAuthenticated(incomingToken, info.sender);
            if (LegacyHostMigration.Current != null) LegacyHostMigration.Current.Acknowledge(epoch, incomingToken,
                authenticated, true);
            else if (authenticated && LegacyHostRoutePolicy.ValidSession(epoch) && LegacyHostStability.Driver != null)
                sessionView.RPC("MPatcherMigrationReleaseV1", info.sender, epoch, LegacyHostStability.Driver.MigrationSession);
        }
        [RPC] private void MPatcherMigrationReleaseV1(string epoch, string session, NetworkMessageInfo info)
        {
            if (IsServerMessage(info) && epoch == migrationEpoch && session == LegacyReconnectTarget.MigrationSession)
                EndClientMigration("server-release");
        }
        internal void EndClientMigration(string reason)
        {
            ReleaseMigrationMachines(reason);
            if (migrationFreeze != null) { migrationFreeze.Dispose(); migrationFreeze = null; }
            if (migrationEpoch != null) LegacyHostMigration.Log("CLIENT_RELEASE reason=" + reason + " epoch=" + migrationEpoch);
            migrationEpoch = null;
        }
        private void MigrationRecoveryStarted()
        {
            if (!MigrationArmed) return;
            migrationMachineReplayActive = true;
            migrationReadySent = false;
            nextRetry = Time.realtimeSinceStartup + 2f;
        }
    }
}
