using MPatcherFork.CustomPatches;
using System;
using System.Collections.Generic;

namespace MPatcherFork.ScaleRuntime
{
    // Logical editor topology only. Native pose construction remains separate.
    internal sealed class ConnectedShapeHierarchy
    {
        internal enum NodeKind { RigidPart, Coupler, Mechanism, PairChannel }

        internal sealed class Result
        {
            internal bool Success;
            internal int Root = -1;
            internal int IncomingCoupler = -1;
            internal bool[] SelectedNodes;
            internal string Reason;
        }

        private readonly NodeKind[] kinds;
        private readonly int[][] adjacent;
        private readonly int[] distance, parent;
        private readonly bool[] ambiguousPath;
        private readonly int core;

        internal ConnectedShapeHierarchy(NodeKind[] kinds, int[][] neighbors, int coreNode)
        {
            if (kinds == null || neighbors == null || kinds.Length != neighbors.Length)
                throw new ArgumentException(ScaleLocalization.Source("Logical topology nodes and adjacency must have equal lengths.", "接続構造のノード配列と隣接配列の長さが一致している必要があります。"));
            if (coreNode < -1 || coreNode >= kinds.Length)
                throw new ArgumentOutOfRangeException("coreNode");
            this.kinds = (NodeKind[])kinds.Clone();
            core = coreNode;
            HashSet<int>[] sets = new HashSet<int>[kinds.Length];
            for (int i = 0; i < sets.Length; i++) sets[i] = new HashSet<int>();
            for (int i = 0; i < neighbors.Length; i++)
                if (neighbors[i] != null)
                    foreach (int other in neighbors[i])
                    {
                        if (other < 0 || other >= kinds.Length) throw new ArgumentOutOfRangeException("neighbors");
                        sets[i].Add(other);
                        sets[other].Add(i);
                    }
            adjacent = new int[kinds.Length][];
            for (int i = 0; i < adjacent.Length; i++)
            {
                adjacent[i] = new int[sets[i].Count];
                sets[i].CopyTo(adjacent[i]);
            }
            distance = new int[kinds.Length];
            parent = new int[kinds.Length];
            ambiguousPath = new bool[kinds.Length];
            for (int i = 0; i < kinds.Length; i++) distance[i] = parent[i] = -1;
            if (core >= 0) BuildParents();
        }

        internal Result Resolve(int seed) { return Select(seed); }

        // A caller may use this only after resolving an exact native Coupler
        // driver whose native subtree contains no Box/Capsule. This is a set of
        // forward geometry candidates, not a replacement pose/owner hierarchy.
        // The caller keeps each shape's native frame and freezes logical-only
        // Coupler carriers; it must not select an entire native owner group.
        internal Result SelectForwardGeometryCandidate(int seed)
        {
            if (seed < 0 || seed >= kinds.Length) return Failure(ScaleLocalization.Source("The selected Coupler is missing from the graph.", "選択した Coupler がグラフにありません。"));
            if (kinds[seed] != NodeKind.Coupler) return Failure(ScaleLocalization.Source("This geometry selection requires a selected Coupler.", "この形状の選択には Coupler の選択が必要です。"));
            if (core < 0) return Failure(ScaleLocalization.Source("No Core was found in the graph.", "グラフ内に Core がありません。"));
            if (distance[seed] < 0) return Failure(ScaleLocalization.Source("The selected Coupler is not connected to the Core.", "選択した Coupler は Core に接続されていません。"));
            bool[] selected = new bool[kinds.Length];
            int[] queue = new int[kinds.Length];
            int head = 0, tail = 0;
            selected[seed] = true;
            queue[tail++] = seed;
            while (head < tail)
            {
                int node = queue[head++];
                foreach (int child in adjacent[node])
                {
                    if (distance[child] != distance[node] + 1 || kinds[child] == NodeKind.Mechanism
                        || selected[child]) continue;
                    selected[child] = true;
                    queue[tail++] = child;
                }
            }
            return new Result { Success = true, Root = seed, IncomingCoupler = seed,
                SelectedNodes = selected, Reason = string.Empty };
        }

        internal Result Select(int seed)
        {
            if (seed < 0 || seed >= kinds.Length) return Failure(ScaleLocalization.Source("The selected node is missing from the graph.", "選択したノードがグラフにありません。"));
            if (core < 0) return Failure(ScaleLocalization.Source("No Core was found in the graph.", "グラフ内に Core がありません。"));
            if (distance[seed] < 0) return Failure(ScaleLocalization.Source("The selected part is not connected to the Core.", "選択した部品は Core に接続されていません。"));
            if (kinds[seed] == NodeKind.Mechanism || kinds[seed] == NodeKind.PairChannel)
                return Failure(ScaleLocalization.Source("Select a Coupler or a Box/Capsule shape.", "Coupler または Box/Capsule の形状を選択してください。"));
            if (parent[seed] == -2) return Failure(ScaleLocalization.Source("The selected part has multiple parents equally distant from the Core.", "選択した部品に Core から等距離の親が複数あります。"));
            int root = seed;
            int incoming = kinds[seed] == NodeKind.Coupler ? seed : -1;
            if (kinds[seed] == NodeKind.RigidPart)
            {
                int previous = parent[seed];
                while (previous >= 0 && kinds[previous] == NodeKind.PairChannel)
                {
                    if (parent[previous] == -2) return Failure(ScaleLocalization.Source("The selected shape has an ambiguous incoming connection.", "選択した形状への接続を一意に特定できません。"));
                    previous = parent[previous];
                }
                if (previous >= 0 && kinds[previous] == NodeKind.Coupler) root = incoming = previous;
            }
            if (parent[root] == -2) return Failure(ScaleLocalization.Source("The incoming Coupler has multiple parents equally distant from the Core.", "接続元の Coupler に Core から等距離の親が複数あります。"));
            bool[] selected = new bool[kinds.Length];
            int[] queue = new int[kinds.Length];
            int head = 0, tail = 0;
            queue[tail++] = root;
            selected[root] = true;
            while (head < tail)
            {
                int node = queue[head++];
                foreach (int child in adjacent[node])
                {
                    if (distance[child] != distance[node] + 1 || kinds[child] == NodeKind.Mechanism) continue;
                    if (selected[child]) continue;
                    selected[child] = true;
                    queue[tail++] = child;
                }
            }
            // Equal predecessors and lateral contacts can form harmless loops
            // wholly inside this branch. They become ambiguous only when a
            // selected descendant is also attached to an unselected sibling.
            for (int i = 0; i < tail; i++)
            {
                int node = queue[i];
                foreach (int other in adjacent[node])
                {
                    if (other == node) return Failure(ScaleLocalization.Source("The selected part contains a connection to itself.", "選択した部品が自分自身に接続されています。"));
                    if (selected[other]) continue;
                    if (distance[other] == distance[node]
                        || (node != root && distance[other] == distance[node] - 1))
                        return Failure(ScaleLocalization.Source("The selected branch is also attached to an unselected neighboring branch.", "選択した枝は、選択していない隣の枝にも接続されています。"));
                }
            }
            return new Result { Success = true, Root = root, IncomingCoupler = incoming,
                SelectedNodes = selected, Reason = string.Empty };
        }

        private void BuildParents()
        {
            int[] queue = new int[kinds.Length];
            int head = 0, tail = 0;
            distance[core] = 0;
            queue[tail++] = core;
            while (head < tail)
            {
                int node = queue[head++];
                foreach (int other in adjacent[node])
                {
                    if (distance[other] < 0)
                    {
                        distance[other] = distance[node] + 1;
                        parent[other] = node;
                        queue[tail++] = other;
                    }
                    else if (distance[other] == distance[node] + 1 && parent[other] != node)
                        parent[other] = -2; // Never choose the first equally near parent.
                }
            }
            // Every predecessor is earlier in this BFS order. Cycles in unrelated
            // components/branches do not invalidate a separate selected subtree.
            for (int i = 0; i < tail; i++)
            {
                int node = queue[i];
                bool bad = parent[node] == -2;
                foreach (int other in adjacent[node])
                {
                    if (distance[other] == distance[node]) bad = true;
                    else if (distance[other] == distance[node] - 1 && ambiguousPath[other]) bad = true;
                }
                ambiguousPath[node] = bad;
            }
        }

        private Result Failure(string reason)
        {
            return new Result { SelectedNodes = new bool[kinds.Length], Reason = reason };
        }
    }
}
