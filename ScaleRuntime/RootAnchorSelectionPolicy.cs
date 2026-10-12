using MPatcherFork.CustomPatches;
using System;
using System.Collections.Generic;

namespace MPatcherFork.ScaleRuntime
{
    // Coreless mounting-end proof. The native contact snapshot supplies all
    // edges and real direct contacts; there is no axis, size, color shortcut or
    // guessed first controller. This chooses a private probe anchor only.
    internal static class RootAnchorSelectionPolicy
    {
        internal struct Contact
        {
            internal int Node, ControllerId;
            internal Contact(int node, int controllerId) { Node = node; ControllerId = controllerId; }
        }

        internal sealed class Result
        {
            internal bool Success;
            internal int AnchorNode = -1, AnchorControllerId = -1;
            internal bool[] SelectedNodes, ExcludedAnchorNodes;
            internal string Reason;
        }

        internal static Result Resolve(ConnectedShapeHierarchy.NodeKind[] kinds, int[][] adjacent,
            bool[] hasBoxCap, int seedNode, Contact[] realContacts)
        {
            int count = kinds == null ? 0 : kinds.Length;
            if (kinds == null || adjacent == null || hasBoxCap == null || realContacts == null
                || adjacent.Length != count || hasBoxCap.Length != count)
                return Failure(count, ScaleLocalization.Source("The anchor block graph is unavailable.", "基準ブロックのグラフを利用できません。"));
            if (seedNode < 0 || seedNode >= count
                || kinds[seedNode] != ConnectedShapeHierarchy.NodeKind.Coupler)
                return Failure(count, ScaleLocalization.Source("Select the outermost Single Coupler that holds the shape.", "形状を支える枝の根元の Single Coupler を選択してください。"));

            HashSet<int>[] neighbors = new HashSet<int>[count];
            for (int i = 0; i < count; i++) neighbors[i] = new HashSet<int>();
            for (int i = 0; i < count; i++)
                if (adjacent[i] != null)
                    foreach (int other in adjacent[i])
                    {
                        if (other < 0 || other >= count || other == i)
                            return Failure(count, ScaleLocalization.Source("Invalid connection in the anchor graph.", "基準のグラフに不正な接続があります。"));
                        neighbors[i].Add(other); neighbors[other].Add(i);
                    }

            int[] component = new int[count];
            for (int i = 0; i < count; i++) component[i] = -1;
            component[seedNode] = -2;
            List<List<int>> components = new List<List<int>>();
            List<bool> geometry = new List<bool>();
            foreach (int first in neighbors[seedNode])
            {
                if (component[first] >= 0) continue;
                int id = components.Count;
                List<int> queue = new List<int>(); queue.Add(first); component[first] = id;
                bool hasGeometry = false;
                for (int i = 0; i < queue.Count; i++)
                {
                    int node = queue[i]; hasGeometry |= hasBoxCap[node];
                    foreach (int other in neighbors[node])
                        if (other != seedNode && component[other] < 0)
                        { component[other] = id; queue.Add(other); }
                }
                components.Add(queue); geometry.Add(hasGeometry);
            }
            int empty = -1, emptyCount = 0, geometryCount = 0;
            for (int i = 0; i < components.Count; i++)
                if (geometry[i]) geometryCount++;
                else { empty = i; emptyCount++; }
            if (emptyCount != 1 || geometryCount == 0)
                return Failure(count, ScaleLocalization.Source("Could not identify a unique anchor without a Core. Select the outermost Coupler before the shape.", "Core がないため基準を一意に特定できません。形状の手前にある枝の根元の Coupler を選択してください。"));

            Dictionary<int, int> anchors = new Dictionary<int, int>();
            foreach (Contact contact in realContacts)
            {
                if (contact.Node < 0 || contact.Node >= count || contact.ControllerId < 0
                    || !neighbors[seedNode].Contains(contact.Node))
                    return Failure(count, ScaleLocalization.Source("The native graph does not confirm the anchor block contact.", "標準のグラフで基準ブロックの接触を確認できません。"));
                if (component[contact.Node] != empty
                    || kinds[contact.Node] != ConnectedShapeHierarchy.NodeKind.RigidPart) continue;
                int old;
                if (anchors.TryGetValue(contact.ControllerId, out old) && old != contact.Node)
                    return Failure(count, ScaleLocalization.Source("The anchor block has inconsistent ownership.", "基準ブロックの所属が矛盾しています。"));
                anchors[contact.ControllerId] = contact.Node;
            }
            if (anchors.Count != 1)
                return Failure(count, ScaleLocalization.Source("A unique real anchor block is required before the selected Coupler.", "選択した Coupler の手前に、実際の基準ブロックが 1 つ必要です。"));
            int anchorId = -1, anchorNode = -1;
            foreach (KeyValuePair<int, int> entry in anchors)
            { anchorId = entry.Key; anchorNode = entry.Value; } // Count is exactly one: no ordering choice.
            bool[] selected = new bool[count], excluded = new bool[count];
            selected[seedNode] = true;
            for (int i = 0; i < components.Count; i++)
                foreach (int node in components[i])
                    if (i == empty) excluded[node] = true; else selected[node] = true;
            return new Result { Success = true, AnchorNode = anchorNode, AnchorControllerId = anchorId,
                SelectedNodes = selected, ExcludedAnchorNodes = excluded, Reason = string.Empty };
        }

        private static Result Failure(int count, string reason)
        {
            return new Result { SelectedNodes = new bool[count], ExcludedAnchorNodes = new bool[count], Reason = reason };
        }
    }
}
