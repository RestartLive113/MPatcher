using MPatcherFork.CustomPatches;
using System;
using System.Collections.Generic;

namespace MPatcherFork.ScaleRuntime
{
    // IDs are stable editor-controller identities supplied by the caller. The
    // native visual solver already chose each group's driver and parent. Mere
    // contacts between neighboring carriers do not change this ownership.
    internal static class ConnectedShapePoseHierarchy
    {
        internal sealed class Result
        {
            internal bool Success;
            internal int[] RootGroups;
            internal bool[] SelectedGroups;
            internal int DisplayParent = -1;
            internal string Route;
            internal string Reason;
        }

        internal static Result Resolve(int[] parents, bool[] couplerLinks, int[] driverIds,
            int[] pairBaseIds, bool[] detachedGroups, int seedId, int seedGroup, bool seedIsCoupler)
        {
            if (parents == null || couplerLinks == null || driverIds == null
                || pairBaseIds == null || detachedGroups == null)
                throw new ArgumentNullException(ScaleLocalization.Source("Native visual hierarchy arrays must be present.", "標準の表示階層の配列が必要です。"));
            int count = parents.Length;
            if (couplerLinks.Length != count || driverIds.Length != count
                || pairBaseIds.Length != count || detachedGroups.Length != count)
                throw new ArgumentException(ScaleLocalization.Source("Native visual hierarchy arrays must have equal lengths.", "標準の表示階層の配列の長さが一致している必要があります。"));
            if (seedId < 0 || seedGroup < 0 || seedGroup >= count)
                return Failure(count, ScaleLocalization.Source("The selected block is missing from the visual assembly.", "選択したブロックが表示用の組み立てにありません。"));
            if (detachedGroups[seedGroup])
                return Failure(count, ScaleLocalization.Source("The selected block has no calculated visual pose.", "選択したブロックの表示姿勢が未計算です。"));

            List<int> roots = new List<int>();
            for (int i = 0; i < count; i++)
                if (driverIds[i] == seedId) roots.Add(i);
            if (roots.Count != 0)
            {
                if (roots.Count != 1 || !couplerLinks[roots[0]] || !seedIsCoupler)
                    return Failure(count, ScaleLocalization.Source("The visual assembly cannot identify a unique Coupler for the selected part.", "表示用の組み立てで、選択した部品の Coupler を一意に特定できません。"));
                return Select(parents, couplerLinks, detachedGroups, roots,
                    parents[roots[0]], "exact-driver", false);
            }

            if (seedIsCoupler)
            {
                // A paired base may own several remote branches. Include only
                // its exact identity and direct child groups, never a color or
                // position match and never its parent body's geometry.
                for (int i = 0; i < count; i++)
                    if (couplerLinks[i] && pairBaseIds[i] == seedId && parents[i] == seedGroup)
                        roots.Add(i);
                if (roots.Count != 0)
                    return Select(parents, couplerLinks, detachedGroups, roots,
                        seedGroup, "exact-paired-base", false);
                return Failure(count, ScaleLocalization.Source("No visual branch belonging to the selected Coupler was found.", "選択した Coupler に属する表示用の枝がありません。"));
            }

            roots.Add(seedGroup);
            if (couplerLinks[seedGroup])
                return Select(parents, couplerLinks, detachedGroups, roots,
                    parents[seedGroup], "incoming-driver", false);
            // A plain Box/Capsule mesh with no incoming Coupler is still editable.
            // Selecting its own rigid group must not collect unrelated branches
            // attached to a Core or mechanism body.
            return Select(parents, couplerLinks, detachedGroups, roots,
                parents[seedGroup], "local-shape-group", true);
        }

        private static Result Select(int[] parents, bool[] couplerLinks, bool[] detachedGroups,
            List<int> roots, int displayParent, string route, bool localOnly)
        {
            bool[] selected = new bool[parents.Length];
            try
            {
                // Validate/build the first subtree once, then extend a paired
                // base's union in O(groups + links), even for many remote peers.
                bool[] subtree = CouplerBranchSelection.Select(parents, couplerLinks, roots[0]);
                if (localOnly) selected[roots[0]] = true;
                else
                {
                    selected = subtree;
                    if (roots.Count > 1)
                    {
                        List<int>[] children = new List<int>[parents.Length];
                        for (int i = 0; i < parents.Length; i++)
                            if (parents[i] >= 0)
                            {
                                if (children[parents[i]] == null) children[parents[i]] = new List<int>();
                                children[parents[i]].Add(i);
                            }
                        List<int> pending = new List<int>();
                        for (int i = 1; i < roots.Count; i++)
                            if (!selected[roots[i]]) { selected[roots[i]] = true; pending.Add(roots[i]); }
                        for (int i = 0; i < pending.Count; i++)
                        {
                            List<int> next = children[pending[i]];
                            if (next == null) continue;
                            foreach (int child in next)
                                if (couplerLinks[child] && !selected[child])
                                { selected[child] = true; pending.Add(child); }
                        }
                    }
                }
            }
            catch (ArgumentException error)
            {
                return Failure(parents.Length, ScaleLocalization.Source("Invalid visual hierarchy: ", "表示用の階層が不正です: ") + error.Message);
            }
            for (int i = 0; i < selected.Length; i++)
                if (selected[i] && detachedGroups[i])
                    return Failure(parents.Length, ScaleLocalization.Source("The selected branch contains a part without a visual pose.", "選択した枝に表示姿勢が未計算の部品があります。"));
            return new Result { Success = true, RootGroups = roots.ToArray(), SelectedGroups = selected,
                DisplayParent = displayParent, Route = route, Reason = string.Empty };
        }

        private static Result Failure(int count, string reason)
        {
            return new Result { RootGroups = new int[0], SelectedGroups = new bool[count],
                Route = "unresolved", Reason = reason };
        }
    }
}
