namespace MPatcherFork.CustomPatches
{
    // A browser return accelerates a known failure, never a healthy connection.
    // Both paths retain the same role guards and 30s limit.
    internal sealed class LegacyCatalogueSocketPolicy
    {
        private int failures;
        private float nextRestart;
        internal void Failed(bool eligible)
        {
            if (eligible && failures < 2) failures++;
        }
        internal void Received() { failures = 0; }
        internal void Reopened() { if (failures > 0) failures = 2; }
        internal bool ShouldRestart(bool individual, bool disconnected, float joinTimer, float now)
        { return individual && disconnected && joinTimer <= 0f && failures >= 2 && now >= nextRestart; }
        internal void Attempted(float now) { failures = 0; nextRestart = now + 30f; }
        // A refused native cleanup did not create a new session. Preserve the
        // recovery request and retry no sooner than the next normal poll.
        internal void Deferred(float now) { nextRestart = now + 9f; }
    }
}
