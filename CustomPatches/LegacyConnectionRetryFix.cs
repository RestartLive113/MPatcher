using System;
using System.Reflection;
using UnityEngine;

namespace MPatcherFork.CustomPatches
{
	// A single click may hit a transiently silent Legacy NAT facilitator. Keep
	// the stock ten-second attempt and retry the same GUID up to three times.
	// Direct HostData connections and non-transient admission errors are untouched.
	internal static class LegacyConnectionRetryFix
	{
		private const int MaximumPhysicalAttempts = 3;
		private const float AttemptTimeoutSeconds = 10f;
		private const float RetryDelaySeconds = 0.75f;
		private static FieldInfo timerField;
		private static Lobby lobby;
		private static string guid;
		private static int physicalAttempt;
		private static bool active;
		private static bool retryPending;
		private static float retryAt;
		private static bool enabled;

		internal static void Initialize(FieldInfo connectionTimerField)
		{
			timerField = connectionTimerField;
			enabled = Environment.GetEnvironmentVariable("MPATCHER_LEGACY_AUTO_RETRY_DISABLE") != "1";
			Log(enabled
				? "REGISTERED version=1 scope=Legacy-GUID-only physicalAttempts=3 perAttemptTimeout=10 retryDelay=0.75 directHostData=unchanged admissionErrors=unchanged"
				: "DISABLED_BY_PROCESS_ENVIRONMENT behavior=stock-single-attempt");
		}

		internal static void Begin(string targetGuid, bool useNat)
		{
			CancelState();
			if (!enabled || !useNat || string.IsNullOrEmpty(targetGuid)) return;
			guid = targetGuid;
			physicalAttempt = 1;
			active = true;
			Log("ATTEMPT_BEGIN physical=1/3 cause=user-click guid=" + Clean(guid));
		}

		internal static void Attach(Lobby instance)
		{
			if (active && instance != null) lobby = instance;
		}

		internal static void InitialResult(NetworkConnectionError result)
		{
			if (!active || result == NetworkConnectionError.NoError) return;
			if (result == NetworkConnectionError.ConnectionFailed && physicalAttempt < MaximumPhysicalAttempts)
			{
				Schedule("immediate-ConnectionFailed", false);
				return;
			}
			Log("GIVE_UP physical=" + physicalAttempt + "/3 result=" + result);
			CancelState();
		}

		// Return false to suppress Connect.OnFailedToConnect's sole timer reset
		// while a retry remains scheduled.
		internal static bool HandleFailure(NetworkConnectionError error)
		{
			if (!active) return true;
			if (error == NetworkConnectionError.ConnectionFailed && physicalAttempt < MaximumPhysicalAttempts)
			{
				Schedule("native-ConnectionFailed", false);
				return false;
			}
			Log("GIVE_UP physical=" + physicalAttempt + "/3 result=" + error);
			CancelState();
			return true;
		}

		internal static void Update(Lobby instance)
		{
			if (!active || !retryPending || Time.realtimeSinceStartup < retryAt) return;
			if (instance != null) lobby = instance;
			retryPending = false;
			physicalAttempt++;
			SetTimer(AttemptTimeoutSeconds);
			NetworkConnectionError result;
			try { result = Network.Connect(guid); }
			catch (Exception error)
			{
				Log("ATTEMPT_THROW physical=" + physicalAttempt + "/3 type=" + error.GetType().Name + " message=" + Clean(error.Message));
				SetTimer(0f);
				CancelState();
				return;
			}
			Log("ATTEMPT_BEGIN physical=" + physicalAttempt + "/3 cause=automatic guid=" + Clean(guid) + " result=" + result);
			if (result == NetworkConnectionError.NoError) return;
			if (result == NetworkConnectionError.ConnectionFailed && physicalAttempt < MaximumPhysicalAttempts)
			{
				Schedule("immediate-ConnectionFailed", false);
				return;
			}
			Log("GIVE_UP physical=" + physicalAttempt + "/3 result=" + result);
			SetTimer(0f);
			CancelState();
		}

		// BFMGHLNCKJK is also the user's cancel action. A positive timer means
		// the call did not come from Lobby.Update's timeout branch and is allowed.
		internal static bool BeforeReset(Lobby instance)
		{
			if (!active) return true;
			if (instance != null) lobby = instance;
			float timer = ReadTimer();
			if (timer > 0f)
			{
				Log("CANCELLED_BY_USER physical=" + physicalAttempt + "/3");
				CancelState();
				return true;
			}
			if (retryPending)
			{
				SetTimer(Mathf.Max(0.05f, retryAt - Time.realtimeSinceStartup));
				return false;
			}
			if (physicalAttempt >= MaximumPhysicalAttempts)
			{
				Log("GIVE_UP physical=3/3 result=ui-timeout");
				CancelState();
				return true;
			}
			Schedule("ui-timeout", true);
			return false;
		}

		internal static void Connected()
		{
			if (active) Log("SUCCEEDED physical=" + physicalAttempt + "/3");
			CancelState();
		}

		private static void Schedule(string cause, bool disconnectAttempt)
		{
			if (disconnectAttempt)
			{
				try { Network.Disconnect(); }
				catch (Exception error) { Log("DISCONNECT_BEFORE_RETRY_FAILED type=" + error.GetType().Name); }
			}
			retryPending = true;
			retryAt = Time.realtimeSinceStartup + RetryDelaySeconds;
			SetTimer(RetryDelaySeconds);
			Log("RETRY_SCHEDULED completedPhysical=" + physicalAttempt + "/3 next=" + (physicalAttempt + 1) + "/3 cause=" + cause + " delay=0.75");
		}

		private static float ReadTimer()
		{
			try { return lobby == null || timerField == null ? 0f : (float)timerField.GetValue(lobby); }
			catch { return 0f; }
		}

		private static void SetTimer(float value)
		{
			try { if (lobby != null && timerField != null) timerField.SetValue(lobby, value); }
			catch (Exception error) { Log("TIMER_WRITE_FAILED type=" + error.GetType().Name); }
		}

		private static void CancelState()
		{
			lobby = null;
			guid = null;
			physicalAttempt = 0;
			active = false;
			retryPending = false;
			retryAt = 0f;
		}

		private static string Clean(string value) { return (value ?? "").Replace("\r", " ").Replace("\n", " "); }
		private static void Log(string message)
		{
			try { mK6lLU33ECSzxV4u22c7_0024ijC0MeyAkqA_PRIEl9WpAZK.YELeoCirSeVGf6u7nOIXkng("[LEGACY-RETRY] " + message); }
			catch { }
		}
	}
}
