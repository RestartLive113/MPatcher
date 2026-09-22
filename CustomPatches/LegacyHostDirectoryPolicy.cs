namespace MPatcherFork.CustomPatches
{
	internal enum LegacyHostDirectoryAction
	{
		None,
		RequestList,
		Register
	}

	internal enum LegacyHostDirectoryPhase
	{
		Idle,
		AwaitingHost,
		CheckDue,
		WaitingList,
		RetryDue,
		WaitingRegistration,
		Stopped
	}

	internal enum LegacyHostDirectoryEvidence
	{
		Unknown,
		RegistrationAcknowledged,
		ListedInReply,
		MissingFromReply
	}

	// Pure scheduling policy. Unity calls and room metadata stay in the adapter.
	internal sealed class LegacyHostDirectoryPolicy
	{
		internal const double ReplyTimeoutSeconds = 12d;
		internal const double CheckIntervalSeconds = 15d;
		internal const double HostStartTimeoutSeconds = 120d;
		private static readonly double[] RetryDelaysSeconds = { 2d, 5d, 10d, 30d };
		// A repeat RegisterHost can leave the ACK silent while directory queries
		// still succeed. Every repeat must therefore be followed by a list probe.
		private bool registrationVerificationPending;

		internal LegacyHostDirectoryPhase Phase { get; private set; }
		internal LegacyHostDirectoryEvidence Evidence { get; private set; }
		internal string Reason { get; private set; }
		internal double DueAt { get; private set; }
		internal double LastSeenAt { get; private set; }
		internal int Failures { get; private set; }
		internal int RegistrationAttempts { get; private set; }
		internal bool Active { get; private set; }
		internal bool Tracking
		{
			get { return Phase != LegacyHostDirectoryPhase.Idle && Phase != LegacyHostDirectoryPhase.Stopped; }
		}

		internal LegacyHostDirectoryPolicy()
		{
			Phase = LegacyHostDirectoryPhase.Idle;
			Evidence = LegacyHostDirectoryEvidence.Unknown;
			Reason = "waiting for original host registration";
			LastSeenAt = -1d;
		}

		internal void Capture(double now)
		{
			Phase = LegacyHostDirectoryPhase.AwaitingHost;
			Evidence = LegacyHostDirectoryEvidence.Unknown;
			Reason = "original registration captured; waiting for live host identity";
			DueAt = now + HostStartTimeoutSeconds;
			LastSeenAt = -1d;
			Failures = 0;
			RegistrationAttempts = 0;
			Active = false;
			registrationVerificationPending = false;
		}

		internal LegacyHostDirectoryAction Tick(double now, bool sameLiveHost)
		{
			if (!Tracking) return LegacyHostDirectoryAction.None;
			if (Phase == LegacyHostDirectoryPhase.AwaitingHost)
			{
				if (sameLiveHost)
				{
					Active = true;
					Phase = LegacyHostDirectoryPhase.CheckDue;
					DueAt = now + 5d;
					Reason = "host running; initial catalogue check scheduled";
				}
				else if (now >= DueAt)
				{
					Stop("host did not become live within capture window");
				}
				return LegacyHostDirectoryAction.None;
			}
			if (!sameLiveHost)
			{
				Stop("captured host stopped or changed");
				return LegacyHostDirectoryAction.None;
			}
			if (now < DueAt) return LegacyHostDirectoryAction.None;
			if (Phase == LegacyHostDirectoryPhase.WaitingRegistration)
			{
				Phase = LegacyHostDirectoryPhase.WaitingList;
				DueAt = now + ReplyTimeoutSeconds;
				Reason = "registration acknowledgement absent; checking catalogue independently";
				return LegacyHostDirectoryAction.RequestList;
			}
			if (Phase == LegacyHostDirectoryPhase.WaitingList)
			{
				registrationVerificationPending = false;
				Failure(now, "catalogue reply timeout; visibility unknown", false);
				return LegacyHostDirectoryAction.None;
			}
			if (Phase == LegacyHostDirectoryPhase.CheckDue)
			{
				Phase = LegacyHostDirectoryPhase.WaitingList;
				DueAt = now + ReplyTimeoutSeconds;
				Reason = "catalogue query sent; waiting for reply";
				return LegacyHostDirectoryAction.RequestList;
			}
			if (Phase == LegacyHostDirectoryPhase.RetryDue)
			{
				Phase = LegacyHostDirectoryPhase.WaitingRegistration;
				DueAt = now + ReplyTimeoutSeconds;
				RegistrationAttempts++;
				registrationVerificationPending = true;
				Reason = "repeat registration sent; waiting for acknowledgement";
				return LegacyHostDirectoryAction.Register;
			}
			return LegacyHostDirectoryAction.None;
		}

		internal void RegistrationAcknowledged(double now)
		{
			if (Phase != LegacyHostDirectoryPhase.AwaitingHost && Phase != LegacyHostDirectoryPhase.WaitingRegistration)
				return;
			Evidence = LegacyHostDirectoryEvidence.RegistrationAcknowledged;
			Reason = "registration acknowledged; list presence still unverified";
			if (!Active) return;
			Phase = LegacyHostDirectoryPhase.CheckDue;
			DueAt = now + 2d;
		}

		internal void ListReceived(double now, bool ownGuidFound)
		{
			if (Phase != LegacyHostDirectoryPhase.WaitingList) return;
			registrationVerificationPending = false;
			if (ownGuidFound)
			{
				Evidence = LegacyHostDirectoryEvidence.ListedInReply;
				LastSeenAt = now;
				Failures = 0;
				Phase = LegacyHostDirectoryPhase.CheckDue;
				DueAt = now + CheckIntervalSeconds;
				Reason = "current GUID found in catalogue reply";
				return;
			}
			Failure(now, "current GUID missing from catalogue reply", false);
			Evidence = LegacyHostDirectoryEvidence.MissingFromReply;
		}

		internal void IdentityChanged(double now)
		{
			if (!Active || !Tracking) return;
			registrationVerificationPending = false;
			Evidence = LegacyHostDirectoryEvidence.Unknown;
			Failures = 0;
			Phase = LegacyHostDirectoryPhase.RetryDue;
			DueAt = now;
			Reason = "live host GUID changed; publish current identity";
		}

		internal void PublicationChanged(double now)
		{
			if (!Active || !Tracking) return;
			// An outage already has an in-flight registration or a bounded retry.
			// Adapter flapping must not bypass that backoff or postpone its deadline.
			if (Phase == LegacyHostDirectoryPhase.RetryDue || Phase == LegacyHostDirectoryPhase.WaitingRegistration
				|| registrationVerificationPending) return;
			Evidence = LegacyHostDirectoryEvidence.Unknown;
			Phase = LegacyHostDirectoryPhase.RetryDue;
			DueAt = now + 2d;
			Reason = "live host endpoint refresh; publish current addresses";
		}

		internal void Failure(double now, string reason, bool permanent)
		{
			if (!Tracking) return;
			if (permanent)
			{
				Stop(reason);
				return;
			}
			Evidence = LegacyHostDirectoryEvidence.Unknown;
			// Native failures are not tagged as register-vs-query. Keep the bounded
			// verification alive; only its fresh result or timeout decides recovery.
			if (registrationVerificationPending)
			{
				Reason = "master operation failed; independent catalogue verification still pending";
				return;
			}
			Reason = reason;
			if (!Active || Phase == LegacyHostDirectoryPhase.RetryDue) return;
			double delay = RetryDelaysSeconds[System.Math.Min(Failures, RetryDelaysSeconds.Length - 1)];
			Failures = System.Math.Min(Failures + 1, 1000000);
			Phase = LegacyHostDirectoryPhase.RetryDue;
			DueAt = now + delay;
		}

		internal void Stop(string reason)
		{
			Phase = LegacyHostDirectoryPhase.Stopped;
			Evidence = LegacyHostDirectoryEvidence.Unknown;
			Reason = reason;
			Active = false;
			registrationVerificationPending = false;
			DueAt = 0d;
		}
	}
}
