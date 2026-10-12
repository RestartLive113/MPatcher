using MPatcherFork.CustomPatches;
using System;
using System.Collections.Generic;

namespace MPatcherFork.ScaleRuntime
{
    // Group parents describe the assembled forest. An edge is traversable for
    // selection only when its child group is driven by a Coupler.
    internal static class CouplerBranchSelection
    {
        internal static bool[] Select(int[] parents, bool[] couplerLinks, int root)
        {
            ValidateFlags(parents, couplerLinks, "couplerLinks");
            ValidateGroup(parents, root, "root");
            ValidateForest(parents);
            List<int>[] children = Children(parents);
            bool[] selected = new bool[parents.Length];
            List<int> pending = new List<int>();
            pending.Add(root);
            selected[root] = true;
            for (int i = 0; i < pending.Count; i++)
            {
                List<int> next = children[pending[i]];
                if (next == null) continue;
                foreach (int child in next)
                {
                    if (!couplerLinks[child]) continue;
                    selected[child] = true;
                    pending.Add(child);
                }
            }
            return selected;
        }

        internal static bool HasUnselectedDescendant(int[] parents, bool[] selectedGroups,
            int group, bool[] groupsWithShapes)
        {
            ValidateFlags(parents, selectedGroups, "selectedGroups");
            ValidateFlags(parents, groupsWithShapes, "groupsWithShapes");
            ValidateGroup(parents, group, "group");
            ValidateForest(parents);
            List<int>[] children = Children(parents);
            List<int> pending = new List<int>();
            List<int> first = children[group];
            if (first != null) pending.AddRange(first);
            // Mechanism edges are deliberately included in protection: changing
            // an ancestor Coupler would still move shapes below that mechanism.
            for (int i = 0; i < pending.Count; i++)
            {
                int child = pending[i];
                if (!selectedGroups[child] && groupsWithShapes[child]) return true;
                List<int> next = children[child];
                if (next != null) pending.AddRange(next);
            }
            return false;
        }

        private static void ValidateFlags(int[] parents, bool[] flags, string parameter)
        {
            if (parents == null) throw new ArgumentNullException("parents");
            if (flags == null) throw new ArgumentNullException(parameter);
            if (parents.Length != flags.Length)
                throw new ArgumentException(ScaleLocalization.Source("Group arrays must have the same length.", "グループの配列の長さが一致している必要があります。"), parameter);
        }

        private static void ValidateGroup(int[] parents, int group, string parameter)
        {
            if (group < 0 || group >= parents.Length) throw new ArgumentOutOfRangeException(parameter);
        }

        private static void ValidateForest(int[] parents)
        {
            for (int i = 0; i < parents.Length; i++)
                if (parents[i] < -1 || parents[i] >= parents.Length)
                    throw new ArgumentException(ScaleLocalization.Source("Invalid parent group at index ", "親グループが不正な位置: ") + i + ".", "parents");
            byte[] states = new byte[parents.Length];
            int[] chain = new int[parents.Length];
            for (int start = 0; start < parents.Length; start++)
            {
                if (states[start] != 0) continue;
                int node = start;
                int count = 0;
                while (node >= 0 && states[node] == 0)
                {
                    states[node] = 1;
                    chain[count++] = node;
                    node = parents[node];
                }
                if (node >= 0 && states[node] == 1)
                    throw new ArgumentException(ScaleLocalization.Source("Cycle in assembled group parents at index ", "組み立てグループの親に循環があります。位置: ") + node + ".", "parents");
                for (int i = 0; i < count; i++) states[chain[i]] = 2;
            }
        }

        private static List<int>[] Children(int[] parents)
        {
            List<int>[] children = new List<int>[parents.Length];
            for (int child = 0; child < parents.Length; child++)
            {
                int parent = parents[child];
                if (parent < 0) continue;
                if (children[parent] == null) children[parent] = new List<int>();
                children[parent].Add(child);
            }
            return children;
        }
    }
}
