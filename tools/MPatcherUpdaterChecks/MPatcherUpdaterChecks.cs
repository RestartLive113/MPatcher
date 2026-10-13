using System;
using System.Collections;
using MPatcherFork.CustomPatches;

// Pure checks for the release-manifest trust and version contract.
internal static class MPatcherUpdaterChecks
{
	private const string Hash = "0123456789abcdef0123456789abcdef0123456789abcdef0123456789abcdef";
	private static int failures;

	private static string Manifest(string version, string installerUrl, string hash, long length)
	{
		return "Format=1\nVersion=" + version + "\nInstallerUrl=" + installerUrl
			+ "\nInstallerSha256=" + hash + "\nInstallerLength=" + length + "\n";
	}

	private static void ExpectValid(string name, string text, string source, Action<MPatcherUpdateManifest> inspect)
	{
		MPatcherUpdateManifest parsed;
		string error;
		if (!MPatcherUpdateManifest.TryParse(text, new Uri(source), out parsed, out error))
		{
			Fail(name + ": " + error);
			return;
		}
		if (inspect != null)
			inspect(parsed);
		Pass(name);
	}

	private static void ExpectInvalid(string name, string text, string source)
	{
		MPatcherUpdateManifest parsed;
		string error;
		if (MPatcherUpdateManifest.TryParse(text, new Uri(source), out parsed, out error))
			Fail(name + ": unexpectedly accepted");
		else
			Pass(name);
	}

	private static void Expect(bool condition, string message)
	{
		if (!condition)
			Fail(message);
	}

	private static void Pass(string name) { Console.WriteLine("PASS " + name); }
	private static void Fail(string message) { failures++; Console.Error.WriteLine("FAIL " + message); }
	private static void CheckDownloadDeadlines()
	{
		foreach (int duration in new[] { 45, 180 })
		{
			var clock = new MPatcherDownloadDeadline(0f);
			for (int second = 1; second <= duration; second++)
			{
				clock.Observe(second, second * 1000L);
				Expect(clock.TimeoutReason(second) == null, "continuous " + duration + "s download was aborted at " + second);
			}
			Pass("continuous " + duration + "s transfer exceeds old 30s deadline");
		}
		var firstByte = new MPatcherDownloadDeadline(100f);
		firstByte.Observe(159.999f, 0L);
		Expect(firstByte.TimeoutReason(159.999f) == null, "first-byte deadline expired early");
		firstByte.Observe(160f, 0L);
		Expect(firstByte.TimeoutReason(160f) == "first-byte", "first-byte stall was not bounded at 60s");
		Pass("bounded first-byte wait");
		var stalled = new MPatcherDownloadDeadline(0f);
		stalled.Observe(5f, 100L);
		stalled.Observe(64.999f, 100L);
		Expect(stalled.TimeoutReason(64.999f) == null, "stall deadline expired early");
		stalled.Observe(65f, 100L);
		Expect(stalled.TimeoutReason(65f) == "stall", "unchanged bytes were not bounded at 60s");
		stalled.Observe(66f, 101L);
		Expect(stalled.TimeoutReason(66f) == null, "new bytes did not reset idle deadline");
		Pass("stalled bytes and genuine progress reset");
		var total = new MPatcherDownloadDeadline(0f);
		for (int second = 1; second < 600; second++) total.Observe(second, second);
		Expect(total.TimeoutReason(599f) == null, "total deadline expired early");
		total.Observe(600f, 600L);
		Expect(total.TimeoutReason(600f) == "total", "total download lifetime is unbounded");
		Pass("600s total limit despite continuous traffic");
		var retry = new MPatcherDownloadDeadline(200f);
		retry.Observe(201f, 0L);
		Expect(retry.TimeoutReason(201f) == null && retry.ReceivedBytes == 0L, "retry retained prior deadline or bytes");
		Pass("new request resets deadline and byte state");
		var rollback = new MPatcherDownloadDeadline(100f);
		rollback.Observe(110f, 100L);
		rollback.Observe(109f, 200L);
		Expect(rollback.TimeoutReason(109f) == "clock-rollback", "clock rollback silently extended deadline");
		rollback.Observe(111f, 300L);
		Expect(rollback.TimeoutReason(111f) == "clock-rollback", "clock rollback failure was not retained");
		Pass("clock rollback fails closed");
		long estimate = 0L;
		foreach (float progress in new[] { 0f, 0.001f, 0.2f, 0.199999f, 0.4f, 0.399999f, 1f })
		{
			long next = MPatcherDownloadDeadline.EstimateProgress(progress, 25000000L, estimate);
			Expect(next >= estimate && next <= 25000000L, "estimated progress regressed or exceeded manifest length");
			estimate = next;
		}
		Expect(estimate == 25000000L, "complete estimate differs from declared size");
		Expect(MPatcherDownloadDeadline.EstimateProgress(float.NaN, 1000L, 500L) == 500L
			&& MPatcherDownloadDeadline.EstimateProgress(float.PositiveInfinity, 1000L, 500L) == 500L
			&& MPatcherDownloadDeadline.EstimateProgress(-1f, 1000L, 500L) == 500L
			&& MPatcherDownloadDeadline.EstimateProgress(2f, 1000L, 500L) == 1000L,
			"nonfinite/out-of-range progress invalidated a bounded estimate");
		Pass("finite bounded monotonic progress estimates");
		var progressing = new MPatcherDownloadDeadline(0f);
		for (int second = 1; second <= 180; second++)
		{
			progressing.Observe(second, MPatcherDownloadDeadline.EstimateProgress(second / 200f,
				25000000L, progressing.ReceivedBytes));
			Expect(progressing.TimeoutReason(second) == null, "estimated active transfer timed out at " + second);
		}
		Pass("estimated progress keeps slow transfers active beyond first-byte deadline");
	}
	private sealed class DownloadFixture : IEnumerator, IDisposable
	{
		internal int DisposeCount;
		internal Action OnMove;
		internal bool Next = true;
		public object Current { get { return null; } }
		public void Reset() { throw new NotSupportedException(); }
		public bool MoveNext() { if (OnMove != null) OnMove(); return Next; }
		public void Dispose() { DisposeCount++; }
	}
	private static void CheckDownloadScope()
	{
		foreach (string mode in new[] { "dispose-at-yield", "complete", "exception" })
		{
			var original = new DownloadFixture();
			var scope = new MPatcherDownloadScope(original);
			int cleaned = 0;
			original.OnMove = delegate {
				Expect(ReferenceEquals(MPatcherDownloadScope.CurrentScope, scope), "transfer was not associated with its executing scope");
				scope.Cleanup = delegate { cleaned++; };
				if (mode == "exception") throw new InvalidOperationException("fixture error");
			};
			original.Next = mode != "complete";
			try { scope.MoveNext(); }
			catch (InvalidOperationException) { Expect(mode == "exception", "unexpected scope exception"); }
			scope.Dispose(); scope.Dispose();
			Expect(cleaned == 1 && original.DisposeCount == 1 && MPatcherDownloadScope.CurrentScope == null,
				mode + " did not release exactly once and restore the thread scope");
			Expect(!scope.MoveNext(), "disposed transfer scope resumed");
			Pass("transfer cleanup " + mode);
		}
	}

	private static int Main()
	{
		CheckDownloadDeadlines();
		CheckDownloadScope();
		const string source = "https://github.com/RestartLive113/MPatcher/releases/latest/download/MPatcherUpdate.ini";
		string production = Manifest("0.3.7", "https://github.com/RestartLive113/MPatcher/releases/download/v0.3.7/MPatcherPackage.exe", Hash, 1234567);
		ExpectValid("production manifest", production, source, delegate(MPatcherUpdateManifest parsed)
		{
			Expect(parsed.VersionText == "0.3.7", "version text changed");
			Expect(parsed.InstallerSha256 == Hash.ToUpperInvariant(), "hash was not normalized");
			Expect(parsed.InstallerLength == 1234567, "length changed");
			Expect(parsed.IsNewerThan("0.3.6"), "newer version was not detected");
			Expect(!parsed.IsNewerThan("0.3.7"), "equal version was treated as newer");
			Expect(!parsed.IsNewerThan("0.3.8"), "older version was treated as newer");
		});

		ExpectInvalid("bad hash", production.Replace(Hash, "xyz"), source);
		ExpectInvalid("duplicate key", production + "Version=0.3.8\n", source);
		ExpectInvalid("wrong repository", production.Replace("RestartLive113/MPatcher", "Other/MPatcher"), source);
		ExpectInvalid("remote HTTP installer", production.Replace("https://github.com", "http://github.com"), source);
		ExpectInvalid("version tag mismatch", production.Replace("/v0.3.7/", "/v0.3.8/"), source);
		ExpectInvalid("nested release path", production.Replace("/v0.3.7/MPatcherPackage.exe", "/v0.3.7/nested/MPatcherPackage.exe"), source);
		ExpectInvalid("installer query", production.Replace("MPatcherPackage.exe", "MPatcherPackage.exe?alternate=1"), source);
		ExpectInvalid("oversized installer", Manifest("0.3.7", "https://github.com/RestartLive113/MPatcher/releases/download/v0.3.7/MPatcherPackage.exe", Hash, MPatcherUpdateManifest.MaximumInstallerLength + 1), source);
		ExpectInvalid("wrong manifest source", production, "https://example.com/MPatcherUpdate.ini");

		const string alphaSource = "https://github.com/RestartLive113/MPatcher/releases/download/alpha/MPatcherUpdate.ini";
		string alpha = Manifest("0.3.9.2", "https://github.com/RestartLive113/MPatcher/releases/download/alpha/MPatcherPackage.exe", Hash, 7654321);
		ExpectValid("alpha rolling manifest", alpha, alphaSource, delegate(MPatcherUpdateManifest parsed)
		{
			Expect(parsed.Channel == MPatcherReleaseChannel.Alpha, "alpha channel was not inferred");
			Expect(parsed.IsNewerThan("0.3.9.1"), "alpha revision was not treated as newer");
			Expect(parsed.ShouldInstall("0.3.10", MPatcherReleaseChannel.Stable), "stable to alpha channel switch was not offered");
		});
		ExpectInvalid("alpha versioned tag", alpha.Replace("/alpha/MPatcherPackage.exe", "/v0.3.9.2/MPatcherPackage.exe"), alphaSource);
		ExpectInvalid("stable rolling alpha tag", production.Replace("/v0.3.7/", "/alpha/"), source);
		ExpectInvalid("alpha source query", alpha, alphaSource + "?cache=1");
		MPatcherUpdateManifest channelMismatch;
		string channelError;
		Expect(!MPatcherUpdateManifest.TryParse(alpha, new Uri(alphaSource), MPatcherReleaseChannel.Stable,
			out channelMismatch, out channelError), "alpha source accepted as stable channel");

		string loopback = Manifest("0.3.7", "http://127.0.0.1:18777/MPatcherPackage.exe", Hash, 42);
		ExpectValid("loopback test manifest", loopback, "http://127.0.0.1:18777/MPatcherUpdate.ini", null);
		ExpectInvalid("loopback manifest with remote installer", production, "http://127.0.0.1:18777/MPatcherUpdate.ini");

		if (failures != 0)
		{
			Console.Error.WriteLine("Updater checks failed: " + failures);
			return 1;
		}
		Console.WriteLine("All updater checks passed.");
		return 0;
	}
}
