namespace MPatcherFork.CustomPatches
{
    internal static class LegacyCatalogueOrphanPolicy
    {
        // A shut-down peer with no request type cannot have a live catalogue job.
        // Do not cancel an active peer, queued request, publication or host row.
        internal static bool CanRelease(bool disconnected, bool inactive, int query,
            int registration, int update, int submission, int typeLength,
            int nameLength, int commentLength, int row)
        {
            return disconnected && inactive && query == 1 && registration == 0
                && update == 0 && submission == 0 && typeLength == 0
                && nameLength == 0 && commentLength == 0 && row == -1;
        }
    }
}
