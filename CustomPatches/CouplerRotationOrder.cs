using System;
using System.Collections.Generic;

namespace MPatcherFork.CustomPatches
{
	internal static class CouplerRotationOrder
	{
		internal const int Default = 0;
		internal const int Count = 6;
		internal const string PropertyName = "couplerRotationOrder";
		private static readonly string[] Names = { "YXZ", "XYZ", "XZY", "YZX", "ZXY", "ZYX" };

		internal static bool IsValid(int order)
		{
			return order >= 0 && order < Count;
		}

		internal static string Name(int order)
		{
			return Names[IsValid(order) ? order : Default];
		}

		internal static int Axis(int order, int row)
		{
			if (row < 0 || row > 2)
				throw new ArgumentOutOfRangeException("row");
			return Name(order)[row] - 'X';
		}

		internal static int[] AvailableAxes(int order, int row)
		{
			if (row < 0 || row > 2)
				throw new ArgumentOutOfRangeException("row");
			List<int> available = new List<int>();
			for (int axis = 0; axis < 3; axis++)
			{
				bool used = false;
				for (int previous = 0; previous < row; previous++)
					used |= Axis(order, previous) == axis;
				if (!used)
					available.Add(axis);
			}
			return available.ToArray();
		}

		internal static int SelectAxis(int order, int row, int axis)
		{
			if (!IsValid(order) || row < 0 || row > 2 || axis < 0 || axis > 2)
				return IsValid(order) ? order : Default;
			char[] axes = Name(order).ToCharArray();
			int selected = Array.IndexOf(axes, (char)('X' + axis));
			if (selected < row)
				return order;
			char previous = axes[row];
			axes[row] = axes[selected];
			axes[selected] = previous;
			return Array.IndexOf(Names, new string(axes));
		}

		internal static int Read(BlockData block)
		{
			object stored;
			if (block == null || block.type != BlockData.AAHMDBHDCDK.Coupler || block.props == null
				|| !block.props.TryGetValue(PropertyName, out stored))
				return Default;
			int order = Array.IndexOf(Names, stored as string);
			return IsValid(order) ? order : Default;
		}

		internal static bool Set(BlockData block, int order)
		{
			if (block == null || block.type != BlockData.AAHMDBHDCDK.Coupler || !IsValid(order) || Read(block) == order)
				return false;
			if (order == Default)
			{
				if (block.props != null)
				{
					block.props.Remove(PropertyName);
					if (block.props.Count == 0)
						block.props = null;
				}
			}
			else
			{
				if (block.props == null)
					block.props = new Dictionary<string, object>();
				block.props[PropertyName] = Name(order);
			}
			return true;
		}
	}
}
