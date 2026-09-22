namespace MPatcherFork.CustomPatches
{
    internal static class LegacyCatalogueRecoveryPolicy
    {
        internal static bool CancelPhotonSearch(int from, int to, bool disconnected)
        { return disconnected && to == 4 && (from == 0 || from == 1 || from == 2 || from == 3 || from == 5); }
        internal static bool KeepBrowser(bool lobby, bool individual, bool disconnected, float joinTimer)
        {
            // A Legacy catalogue error never belongs to a Photon tab, even if
            // that tab is currently joining a Photon room. Individual preserves
            // its existing join/connected error flow.
            return lobby && (!individual || (disconnected && joinTimer <= 0f));
        }
    }
}
