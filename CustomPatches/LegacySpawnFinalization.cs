using System;
using System.Collections;

namespace MPatcherFork.CustomPatches
{
	internal sealed class LegacySpawnFinalization
	{
		internal bool Completed;
		internal bool Succeeded;
		internal Exception Error;
		internal void Fail(Exception error) { Error = error; Completed = true; Succeeded = false; }

		internal IEnumerator Run(Func<bool> exists, Func<bool> ready, Action warp,
			Func<float> clock, float timeout)
		{
			float deadline = clock() + timeout;
			try
			{
				// Native Sled/Joint Start methods run on the following frame.
				yield return null;
				while (!Completed)
				{
					try
					{
						if (!exists()) Fail(new InvalidOperationException("Replacement destroyed before finalization"));
						else if (ready()) { warp(); Succeeded = true; Completed = true; }
						else if (clock() >= deadline) Fail(new TimeoutException("Replacement Warp dependencies not ready"));
					}
					catch (Exception error) { Fail(error); }
					if (!Completed) yield return null;
				}
			}
			finally
			{
				if (!Completed) Fail(new OperationCanceledException("Replacement finalization stopped"));
			}
		}
	}
}
