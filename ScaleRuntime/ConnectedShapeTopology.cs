using MPatcherFork.CustomPatches;
using System;
using System.Collections.Generic;
using System.Reflection;
using HarmonyLib;

namespace MPatcherFork.ScaleRuntime
{
    // Selection topology is built from editor contacts, independently of native
    // Core physics groups. No controller, selection flag, cell or BlockData is written.
    internal sealed class ConnectedShapeTopology
    {
        private const long BoundaryMask = 543277952L;
        private static readonly FieldInfo GraphRoot = AccessTools.Field(typeof(HDBLLPODNLN), "GEGCCNJLMEE");
        private delegate BlockController NeighborLookup(HDBLLPODNLN graph, BlockData data, int x, int y, int z);
        private delegate BlockController AuxiliaryLookup(HDBLLPODNLN graph, BlockController block);
        private static readonly NeighborLookup Neighbor = NativeNeighbor();
        private static readonly AuxiliaryLookup Auxiliary = NativeAuxiliary();
        private static readonly int[,] Directions = new int[,] {
            { 1, 0, 0 }, { -1, 0, 0 }, { 0, 1, 0 }, { 0, -1, 0 }, { 0, 0, 1 }, { 0, 0, -1 }
        };

        internal readonly ConnectedShapeHierarchy.NodeKind[] Kinds;
        internal readonly int[][] Adjacent;
        internal readonly int CoreNode;
        internal readonly Dictionary<BlockController, int> BlockNode;
        internal readonly List<BlockController>[] Members;
        internal readonly ConnectedShapeHierarchy Hierarchy;
        internal readonly string Summary;
        private readonly Dictionary<BlockController, List<BlockController>> physicalContacts;

        private ConnectedShapeTopology(List<ConnectedShapeHierarchy.NodeKind> kinds,
            List<HashSet<int>> neighbors, List<List<BlockController>> members,
            Dictionary<BlockController, int> blockNode, int coreNode, int contacts, int channels, int aliases,
            Dictionary<BlockController, List<BlockController>> physicalContacts)
        {
            Kinds = kinds.ToArray();
            Adjacent = new int[neighbors.Count][];
            for (int i = 0; i < neighbors.Count; i++)
            {
                int[] row = new int[neighbors[i].Count];
                neighbors[i].CopyTo(row);
                Array.Sort(row);
                Adjacent[i] = row;
            }
            Members = members.ToArray();
            BlockNode = blockNode;
            CoreNode = coreNode;
            this.physicalContacts = physicalContacts;
            Hierarchy = new ConnectedShapeHierarchy(Kinds, Adjacent, CoreNode);
            int parts = 0, couplers = 0, mechanisms = 0;
            foreach (ConnectedShapeHierarchy.NodeKind kind in Kinds)
            {
                if (kind == ConnectedShapeHierarchy.NodeKind.RigidPart) parts++;
                else if (kind == ConnectedShapeHierarchy.NodeKind.Coupler) couplers++;
                else if (kind == ConnectedShapeHierarchy.NodeKind.Mechanism) mechanisms++;
            }
            Summary = "blocks=" + blockNode.Count + " nodes=" + Kinds.Length + " rigidParts=" + parts
                + " couplers=" + couplers + " mechanisms=" + mechanisms + " pairedChannels=" + channels
                + " physicalContacts=" + contacts + " auxiliaryAliases=" + aliases
                + " coreNode=" + CoreNode + " coreReachableNodes=" + Reachable(Adjacent, CoreNode)
                + " contacts=native-CheckLinkG gidFilter=native remoteLinks=paired-only-RGB";
        }

        internal int NodeOf(BlockController controller)
        {
            int node;
            return controller != null && BlockNode.TryGetValue(controller, out node) ? node : -1;
        }

        internal BlockController FirstController(int node)
        { return node >= 0 && node < Members.Length && Members[node].Count != 0 ? Members[node][0] : null; }

        internal List<BlockController> ContactsOf(BlockController block)
        {
            List<BlockController> contacts;
            return block != null && physicalContacts.TryGetValue(block, out contacts)
                ? new List<BlockController>(contacts) : new List<BlockController>();
        }

        internal static ConnectedShapeTopology Build(HDBLLPODNLN graph, BlockController core,
            bool allowMissingCore = false)
        {
            if (graph == null || graph.GEGOAHPBGKB == null) throw new ArgumentNullException("graph");
            if (GraphRoot == null) throw new MissingMemberException(ScaleLocalization.Source("Native editor Core graph root", "標準エディターの Core グラフの根"));
            BlockController nativeCore = GraphRoot.GetValue(graph) as BlockController;
            if (core == null) core = nativeCore;
            if (core == null && !allowMissingCore)
                throw new InvalidOperationException(ScaleLocalization.Source("The editor topology has no valid Core.", "エディターの接続構造に有効な Core がありません。"));
            if (core != null && (core != nativeCore || core.JNKEKNOAPHO == null
                || core.JNKEKNOAPHO.type != BlockData.AAHMDBHDCDK.Core))
                throw new InvalidOperationException(ScaleLocalization.Source("The editor topology has no valid Core.", "エディターの接続構造に有効な Core がありません。"));
            List<BlockController> blocks = new List<BlockController>(graph.GEGOAHPBGKB);
            Dictionary<BlockController, int> index = new Dictionary<BlockController, int>();
            for (int i = 0; i < blocks.Count; i++)
            {
                if (blocks[i] == null || blocks[i].JNKEKNOAPHO == null)
                    throw new InvalidOperationException(ScaleLocalization.Source("The editor graph contains a block without data.", "エディターのグラフにデータのないブロックがあります。"));
                if (index.ContainsKey(blocks[i])) throw new InvalidOperationException(ScaleLocalization.Source("A block appears more than once in the editor graph.", "エディターのグラフ内に同じブロックが重複しています。"));
                index.Add(blocks[i], i);
            }
            if (core != null && !index.ContainsKey(core)) throw new InvalidOperationException(ScaleLocalization.Source("The Core is missing from the editor blocks.", "エディターのブロック内に Core がありません。"));
            Dictionary<BlockController, List<BlockController>> realContacts
                = new Dictionary<BlockController, List<BlockController>>();
            foreach (BlockController block in blocks) realContacts.Add(block, new List<BlockController>());
            int[] parent = new int[blocks.Count];
            byte[] rank = new byte[blocks.Count];
            bool[] boundary = new bool[blocks.Count], auxiliary = new bool[blocks.Count];
            int[] alias = new int[blocks.Count];
            for (int i = 0; i < blocks.Count; i++)
            {
                parent[i] = i;
                BlockData data = blocks[i].JNKEKNOAPHO;
                boundary[i] = data.CheckMask(BoundaryMask);
                auxiliary[i] = data.type == BlockData.AAHMDBHDCDK.Chassis && (data.press >> 6) != 0;
                alias[i] = -1;
                if (auxiliary[i])
                {
                    BlockController owner = Auxiliary(graph, blocks[i]);
                    int ownerIndex;
                    if (owner != null && index.TryGetValue(owner, out ownerIndex)) alias[i] = ownerIndex;
                }
            }
            List<int> edgeFirst = new List<int>(), edgeSecond = new List<int>();
            for (int i = 0; i < blocks.Count; i++)
            {
                if (auxiliary[i]) continue;
                BlockController first = blocks[i];
                for (int direction = 0; direction < 6; direction++)
                {
                    BlockController second = Neighbor(graph, first.JNKEKNOAPHO,
                        Directions[direction, 0], Directions[direction, 1], Directions[direction, 2]);
                    int j;
                    if (second == null || !index.TryGetValue(second, out j) || j <= i || auxiliary[j]) continue;
                    // Keep the game's group/merge/joint exceptions as well as
                    // its exact faces. Ignoring gid joins separate colored bodies
                    // and changes Coupler parentage even when group counts match.
                    if (!first.CheckLinkG(second) || !second.CheckLinkG(first)) continue;
                    edgeFirst.Add(i); edgeSecond.Add(j);
                    realContacts[first].Add(second); realContacts[second].Add(first);
                    if (!boundary[i] && !boundary[j]) Union(parent, rank, i, j);
                }
            }
            List<ConnectedShapeHierarchy.NodeKind> kinds = new List<ConnectedShapeHierarchy.NodeKind>();
            List<HashSet<int>> neighbors = new List<HashSet<int>>();
            List<List<BlockController>> members = new List<List<BlockController>>();
            Dictionary<int, int> rigidNodes = new Dictionary<int, int>();
            Dictionary<BlockController, int> blockNode = new Dictionary<BlockController, int>();
            for (int i = 0; i < blocks.Count; i++)
            {
                if (auxiliary[i] && alias[i] >= 0) continue;
                int node;
                if (boundary[i]) node = AddNode(kinds, neighbors, members,
                    blocks[i].JNKEKNOAPHO.type == BlockData.AAHMDBHDCDK.Coupler
                        ? ConnectedShapeHierarchy.NodeKind.Coupler : ConnectedShapeHierarchy.NodeKind.Mechanism);
                else
                {
                    int root = Find(parent, i);
                    if (!rigidNodes.TryGetValue(root, out node))
                    {
                        node = AddNode(kinds, neighbors, members, ConnectedShapeHierarchy.NodeKind.RigidPart);
                        rigidNodes.Add(root, node);
                    }
                }
                blockNode.Add(blocks[i], node);
                members[node].Add(blocks[i]);
            }
            int aliases = 0;
            for (int i = 0; i < blocks.Count; i++)
            {
                if (!auxiliary[i] || alias[i] < 0) continue;
                int node;
                if (!blockNode.TryGetValue(blocks[alias[i]], out node))
                    throw new InvalidOperationException(ScaleLocalization.Source("An auxiliary block has no physical owner.", "補助ブロックの物理的な所有者がありません。"));
                blockNode.Add(blocks[i], node); members[node].Add(blocks[i]); aliases++;
            }
            for (int i = 0; i < edgeFirst.Count; i++)
                Connect(neighbors, blockNode[blocks[edgeFirst[i]]], blockNode[blocks[edgeSecond[i]]]);
            Dictionary<int, int> channels = new Dictionary<int, int>();
            foreach (BlockController block in blocks)
            {
                BlockData data = block.JNKEKNOAPHO;
                if (data.type != BlockData.AAHMDBHDCDK.Coupler || data.actionParam == null
                    || data.actionParam.Length <= 7 || data.actionParam[7] != 0) continue;
                int hub;
                if (!channels.TryGetValue(data.rgbI, out hub))
                {
                    hub = AddNode(kinds, neighbors, members, ConnectedShapeHierarchy.NodeKind.PairChannel);
                    channels.Add(data.rgbI, hub);
                }
                Connect(neighbors, blockNode[block], hub);
            }
            return new ConnectedShapeTopology(kinds, neighbors, members, blockNode,
                core == null ? -1 : blockNode[core], edgeFirst.Count, channels.Count, aliases, realContacts);
        }

        private static NeighborLookup NativeNeighbor()
        {
            MethodInfo method = AccessTools.Method(typeof(HDBLLPODNLN), "CMGKIOHPPOI",
                new Type[] { typeof(BlockData), typeof(int), typeof(int), typeof(int) });
            if (method == null) throw new MissingMethodException(ScaleLocalization.Source("Native editor cell neighbor lookup", "標準エディターの隣接セル検索"));
            return (NeighborLookup)Delegate.CreateDelegate(typeof(NeighborLookup), method);
        }

        private static AuxiliaryLookup NativeAuxiliary()
        {
            MethodInfo method = AccessTools.Method(typeof(HDBLLPODNLN), "ODENHACMMKO", new Type[] { typeof(BlockController) });
            if (method == null) throw new MissingMethodException(ScaleLocalization.Source("Native pressed-block owner lookup", "標準のクリックしたブロックの所有者検索"));
            return (AuxiliaryLookup)Delegate.CreateDelegate(typeof(AuxiliaryLookup), method);
        }

        private static int AddNode(List<ConnectedShapeHierarchy.NodeKind> kinds,
            List<HashSet<int>> neighbors, List<List<BlockController>> members, ConnectedShapeHierarchy.NodeKind kind)
        {
            int node = kinds.Count;
            kinds.Add(kind); neighbors.Add(new HashSet<int>()); members.Add(new List<BlockController>());
            return node;
        }

        private static void Connect(List<HashSet<int>> neighbors, int first, int second)
        {
            if (first == second) return;
            neighbors[first].Add(second); neighbors[second].Add(first);
        }

        private static int Find(int[] parent, int node)
        {
            int root = node;
            while (parent[root] != root) root = parent[root];
            while (node != root) { int next = parent[node]; parent[node] = root; node = next; }
            return root;
        }

        private static void Union(int[] parent, byte[] rank, int first, int second)
        {
            first = Find(parent, first); second = Find(parent, second);
            if (first == second) return;
            if (rank[first] < rank[second]) parent[first] = second;
            else { parent[second] = first; if (rank[first] == rank[second]) rank[first]++; }
        }

        private static int Reachable(int[][] adjacent, int root)
        {
            if (root < 0 || root >= adjacent.Length) return 0;
            bool[] seen = new bool[adjacent.Length];
            List<int> pending = new List<int>(); pending.Add(root); seen[root] = true;
            for (int i = 0; i < pending.Count; i++)
                foreach (int child in adjacent[pending[i]])
                    if (!seen[child]) { seen[child] = true; pending.Add(child); }
            return pending.Count;
        }
    }
}
