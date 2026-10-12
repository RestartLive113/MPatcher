using System;
using System.Collections.Generic;

namespace MPatcherFork.ScaleRuntime
{
    internal enum ShapeSelectionOperation { Replace, Add, Remove }

    // Explicit membership only: removed children never regrow from selected roots.
    internal static class ManualShapeSelection
    {
        internal static int[] Apply(int[] current, int[] candidate, ShapeSelectionOperation operation)
        {
            if (current == null || candidate == null) throw new ArgumentNullException("selection");
            if (operation != ShapeSelectionOperation.Replace && operation != ShapeSelectionOperation.Add
                && operation != ShapeSelectionOperation.Remove) throw new ArgumentOutOfRangeException("operation");
            HashSet<int> selected = new HashSet<int>();
            foreach (int id in current)
            {
                if (id < 0) throw new ArgumentOutOfRangeException("current");
                if (operation != ShapeSelectionOperation.Replace) selected.Add(id);
            }
            foreach (int id in candidate)
            {
                if (id < 0) throw new ArgumentOutOfRangeException("candidate");
                if (operation == ShapeSelectionOperation.Remove) selected.Remove(id);
                else selected.Add(id);
            }
            List<int> ordered = new List<int>(selected);
            ordered.Sort();
            return ordered.ToArray();
        }
    }
}
