using System;
using System.Collections.Generic;

namespace MPatcherFork.CustomPatches
{
    internal enum LegacyHostPortPhase { Idle, Opening, QueryDue, WaitingReply, RegisterDue, Ready, Exhausted, Cancelled, PlayersPresent }
    internal enum LegacyHostPortAction { None, OpenPort, Query, Register, Commit, Stop }
    internal enum LegacyHostPortEvidence { Unknown, Listed, Missing, BindFailed }

    // Finite port selection while admission is closed. The startup adapter must
    // reject connected players; live migration prepares them in its own barrier.
    internal sealed class LegacyHostPortPolicy
    {
        internal const double ReplyTimeout = 12, CandidateTimeout = 42, TotalTimeout = 180;
        internal LegacyHostPortPhase Phase { get; private set; }
        internal LegacyHostPortEvidence Evidence { get; private set; }
        internal int Port { get { return ports.Length == 0 ? 0 : ports[index]; } }
        internal int Attempt { get { return index + 1; } }
        internal int Count { get { return ports.Length; } }
        internal int PositiveReplies { get; private set; }
        internal string Reason { get; private set; }
        private int[] ports = new int[0];
        private int index, failures;
        private double started, opened, due, firstPositive;
        private bool openPending;
        internal bool Active { get { return Phase == LegacyHostPortPhase.Opening || Phase == LegacyHostPortPhase.QueryDue
            || Phase == LegacyHostPortPhase.WaitingReply || Phase == LegacyHostPortPhase.RegisterDue; } }

        internal static int[] Candidates(int preferred)
        {
            if (preferred < 1 || preferred > 65535) throw new ArgumentOutOfRangeException("preferred");
            List<int> result = new List<int>();
            foreach (int port in new int[] { preferred, 23466, 35017, 25551 })
                if (!result.Contains(port)) result.Add(port);
            return result.ToArray();
        }
        internal static bool MatchesListing(string guid, string nonce, string candidateGuid, string candidateComment)
        {
            // Unity reuses the process GUID across Disconnect/InitializeServer.
            // Require this attempt's published nonce, not an old row for that GUID.
            if (string.IsNullOrEmpty(guid) || guid == "0" || candidateGuid != guid
                || nonce == null || nonce.Length != 32 || candidateComment == null) return false;
            string prefix = "~MPA" + nonce;
            return candidateComment == prefix || candidateComment.StartsWith(prefix + "~", StringComparison.Ordinal);
        }
        internal void Begin(int preferred, double now)
        {
            BeginCandidates(Candidates(preferred), now);
        }
        internal void BeginCandidates(int[] candidates, double now)
        {
            if (candidates == null || candidates.Length == 0 || candidates.Length > 4) throw new ArgumentException("Invalid candidate count");
            List<int> checkedPorts = new List<int>();
            foreach (int p in candidates)
            { if (p < 1 || p > 65535 || checkedPorts.Contains(p)) throw new ArgumentException("Invalid candidate port"); checkedPorts.Add(p); }
            ports = checkedPorts.ToArray(); index = 0; started = now;
            Select(now);
        }
        private void Select(double now)
        {
            Phase = LegacyHostPortPhase.Opening; Evidence = LegacyHostPortEvidence.Unknown;
            PositiveReplies = failures = 0; firstPositive = -1; opened = now;
            due = now; openPending = true; Reason = "opening candidate";
        }
        internal void Opened(double now, bool success)
        {
            if (Phase != LegacyHostPortPhase.Opening || openPending) return;
            if (!success) { Evidence = LegacyHostPortEvidence.BindFailed; Advance(now, "bind failed"); return; }
            Phase = LegacyHostPortPhase.QueryDue; due = now + 3; opened = now;
            Reason = "registration sent; awaiting independent list";
        }
        internal void ListReceived(double now, bool found)
        {
            if (Phase != LegacyHostPortPhase.WaitingReply) return;
            Evidence = found ? LegacyHostPortEvidence.Listed : LegacyHostPortEvidence.Missing;
            if (!found) { Failed(now, "fresh list omitted current GUID"); return; }
            if (PositiveReplies++ == 0) firstPositive = now;
            failures = 0;
            // A single successful packet is insufficient (observed loss starts ~6s).
            if (PositiveReplies >= 3 && now - firstPositive >= 10 && now - opened >= 12)
            { Phase = LegacyHostPortPhase.Ready; Reason = "three spaced fresh listings"; return; }
            Phase = LegacyHostPortPhase.QueryDue; due = now + 5; Reason = "checking sustained listing";
        }
        private void Failed(double now, string reason)
        {
            PositiveReplies = 0; firstPositive = -1; failures++; Reason = reason;
            if (failures >= 2 && now - opened >= 20) Advance(now, reason);
            else { Phase = LegacyHostPortPhase.RegisterDue; due = now + 2; }
        }
        private void Advance(double now, string reason)
        {
            if (index + 1 >= ports.Length || now - started >= TotalTimeout)
            { Phase = LegacyHostPortPhase.Exhausted; Reason = reason; return; }
            index++; Select(now); Reason = reason + "; next candidate";
        }
        internal LegacyHostPortAction Tick(double now, bool playersPresent, bool facilitatorPending)
        {
            if ((Active || Phase == LegacyHostPortPhase.Ready) && playersPresent)
            { Phase = LegacyHostPortPhase.PlayersPresent; Reason = "players present; no teardown allowed"; return LegacyHostPortAction.Stop; }
            if (!Active && Phase != LegacyHostPortPhase.Ready) return LegacyHostPortAction.None;
            if (Phase == LegacyHostPortPhase.Ready)
            {
                if (!facilitatorPending) return LegacyHostPortAction.Commit;
                Evidence = LegacyHostPortEvidence.Unknown; Failed(now, "facilitator recovery pending");
            }
            if (now - started >= TotalTimeout)
            { Phase = LegacyHostPortPhase.Exhausted; Reason = "total time limit"; return LegacyHostPortAction.Stop; }
            if (now - opened >= CandidateTimeout) Advance(now, "candidate time limit");
            if (Phase == LegacyHostPortPhase.Exhausted) return LegacyHostPortAction.Stop;
            if (openPending) { openPending = false; return LegacyHostPortAction.OpenPort; }
            if (now < due) return LegacyHostPortAction.None;
            if (Phase == LegacyHostPortPhase.QueryDue)
            { Phase = LegacyHostPortPhase.WaitingReply; due = now + ReplyTimeout; return LegacyHostPortAction.Query; }
            if (Phase == LegacyHostPortPhase.RegisterDue)
            { Phase = LegacyHostPortPhase.QueryDue; due = now + 3; return LegacyHostPortAction.Register; }
            if (Phase == LegacyHostPortPhase.WaitingReply)
            { Evidence = LegacyHostPortEvidence.Unknown; Failed(now, "list timeout; visibility unknown"); }
            return Phase == LegacyHostPortPhase.Exhausted ? LegacyHostPortAction.Stop : LegacyHostPortAction.None;
        }
        internal void Cancel() { Phase = LegacyHostPortPhase.Cancelled; Reason = "cancelled"; }
    }
}
