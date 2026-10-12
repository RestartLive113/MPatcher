using MPatcherFork.CustomPatches;
namespace MPatcherFork.ScaleRuntime
{
    // Presentation only. The caller supplies an already-resolved exact native
    // Coupler root for the empty/shared geometry route. Parent edges include
    // mechanisms: their shapes still inherit an ancestor's transform.
    internal static class SelectedShapePreviewPolicy
    {
        internal static bool CanCompose(bool emptyShared, int root, int[] parents, int[] shapeGroups)
        {
            if (!emptyShared || parents == null || shapeGroups == null || shapeGroups.Length == 0
                || root < 0 || root >= parents.Length) return false;
            byte[] states = new byte[parents.Length];
            bool[] inheritsRoot = new bool[parents.Length];
            int[] chain = new int[parents.Length];
            // Even when no shape inherits this root, its own parent chain must
            // be valid before a local transform can be applied to the view.
            if (!ResolveChain(root, root, parents, states, inheritsRoot, chain)) return false;
            foreach (int group in shapeGroups)
            {
                if (!ResolveChain(group, root, parents, states, inheritsRoot, chain)
                    || inheritsRoot[group]) return false;
            }
            return true;
        }

        private static bool ResolveChain(int start, int root, int[] parents, byte[] states,
            bool[] inheritsRoot, int[] chain)
        {
            if (start < 0 || start >= parents.Length) return false;
            int count = 0, node = start;
            while (node >= 0)
            {
                if (node >= parents.Length || parents[node] < -1 || parents[node] >= parents.Length)
                    return false;
                if (states[node] == 2) break;
                if (states[node] == 1) return false;
                states[node] = 1;
                chain[count++] = node;
                node = parents[node];
            }
            bool inherited = node >= 0 && inheritsRoot[node];
            for (int i = count - 1; i >= 0; i--)
            {
                node = chain[i];
                inherited |= node == root;
                inheritsRoot[node] = inherited;
                states[node] = 2;
            }
            return true;
        }
    }
}
