using System;
using System.Collections.Generic;
using System.Globalization;
using UnityEngine;

namespace MPatcherFork.CustomPatches
{
	internal static class CouplerRotationProfiles
	{
		internal const string PropertyName = "couplerRotationProfiles";

		internal struct Rotation
		{
			internal readonly int Order;
			internal readonly int X;
			internal readonly int Y;
			internal readonly int Z;
			internal readonly int MilliX;
			internal readonly int MilliY;
			internal readonly int MilliZ;

			internal Rotation(int order, int angleX, int angleY, int angleZ)
			{
				Order = order;
				X = angleX;
				Y = angleY;
				Z = angleZ;
				MilliX = angleX * SetupPrecisionData.Scale;
				MilliY = angleY * SetupPrecisionData.Scale;
				MilliZ = angleZ * SetupPrecisionData.Scale;
			}

			internal Rotation(int order, Vector3 angles)
			{
				Order = order;
				MilliX = SetupPrecisionData.Quantize(angles.x);
				MilliY = SetupPrecisionData.Quantize(angles.y);
				MilliZ = SetupPrecisionData.Quantize(angles.z);
				X = MilliX / SetupPrecisionData.Scale; Y = MilliY / SetupPrecisionData.Scale; Z = MilliZ / SetupPrecisionData.Scale;
			}

			internal bool Same(Rotation other)
			{
				return Order == other.Order && MilliX == other.MilliX && MilliY == other.MilliY && MilliZ == other.MilliZ;
			}

			internal Vector3 Angles
			{
				get { return new Vector3(MilliX / (float)SetupPrecisionData.Scale, MilliY / (float)SetupPrecisionData.Scale, MilliZ / (float)SetupPrecisionData.Scale); }
			}

			internal bool Valid
			{
				get { return CouplerRotationOrder.IsValid(Order) && Math.Abs((long)MilliX) <= 180 * (long)SetupPrecisionData.Scale
					&& Math.Abs((long)MilliY) <= 180 * (long)SetupPrecisionData.Scale && Math.Abs((long)MilliZ) <= 180 * (long)SetupPrecisionData.Scale; }
			}

			internal bool Vanilla
			{
				get { return Valid && Order == CouplerRotationOrder.Default && (MilliX != 0 ? 1 : 0) + (MilliY != 0 ? 1 : 0) + (MilliZ != 0 ? 1 : 0) <= 1; }
			}
		}

		private struct Archive
		{
			internal bool Vanilla;
			internal Rotation Saved;
		}

		internal static bool HasFreeValues(BlockData block)
		{
			return IsCoupler(block) && (HasArchive(block) || !Current(block).Vanilla);
		}

		internal static bool IsVanilla(BlockData block)
		{
			Archive archive;
			return TryRead(block, out archive) && archive.Vanilla;
		}

		internal static bool HasArchive(BlockData block)
		{
			return IsCoupler(block) && block.props != null && block.props.ContainsKey(PropertyName);
		}

		internal static Rotation Current(BlockData block)
		{
			return new Rotation(CouplerRotationOrder.Read(block), SetupPrecisionData.Angles(block));
		}

		internal static Rotation InitialVanilla(BlockData block)
		{
			Rotation current = IsCoupler(block) ? Current(block) : default(Rotation);
			return current.Vanilla ? current : default(Rotation);
		}

		internal static bool RememberFreeEdit(BlockData block, Rotation vanilla)
		{
			if (!IsCoupler(block) || HasArchive(block) || !Current(block).Valid || !vanilla.Vanilla)
				return false;
			SaveArchive(block, new Archive { Vanilla = false, Saved = vanilla });
			CouplerRotation.Log("PROFILE_CAPTURED block=" + Position(block) + " vanilla=" + FormatAngles(vanilla) + " free=" + FormatAngles(Current(block)));
			return true;
		}

		internal static bool CanSwitch(BlockData block, bool vanilla)
		{
			if (!IsCoupler(block) || !Current(block).Valid)
				return false;
			Archive archive;
			if (!TryRead(block, out archive))
				return !HasArchive(block) && HasFreeValues(block) && vanilla;
			return archive.Vanilla != vanilla && (!archive.Vanilla || Current(block).Vanilla);
		}

		internal static bool Switch(BlockData block, bool vanilla)
		{
			if (!CanSwitch(block, vanilla))
				return false;
			Archive archive;
			TryRead(block, out archive);
			Rotation previous = Current(block);
			Rotation next = archive.Saved;
			Dictionary<string, object> properties = CopyProperties(block);
			properties[PropertyName] = Encode(new Archive { Vanilla = vanilla, Saved = previous });
			if (next.Order == CouplerRotationOrder.Default)
				properties.Remove(CouplerRotationOrder.PropertyName);
			else
				properties[CouplerRotationOrder.PropertyName] = CouplerRotationOrder.Name(next.Order);
			block.props = properties;
			SetupPrecisionData.SetAngles(block, next.Angles);
			CouplerRotation.Log("PROFILE_CHANGED block=" + Position(block) + " active=" + (vanilla ? "Vanilla" : "Free")
				+ " order=" + CouplerRotationOrder.Name(next.Order) + " xyz=" + FormatAngles(next)
				+ " savedOrder=" + CouplerRotationOrder.Name(previous.Order) + " saved=" + FormatAngles(previous));
			return true;
		}

		internal static bool TryGetProfile(BlockData block, bool vanilla, out Rotation rotation)
		{
			rotation = default(Rotation);
			if (!IsCoupler(block))
				return false;
			Rotation current = Current(block);
			if (!current.Valid)
				return false;
			if (HasArchive(block))
			{
				Archive archive;
				if (!TryRead(block, out archive) || (archive.Vanilla && !current.Vanilla))
					return false;
				rotation = archive.Vanilla == vanilla ? current : archive.Saved;
			}
			else
				rotation = vanilla ? InitialVanilla(block) : current;
			return rotation.Valid && (!vanilla || rotation.Vanilla);
		}

		internal static bool CopySettings(BlockData destination, BlockData source, bool vanilla)
		{
			Rotation selected;
			Rotation retained;
			if (!TryGetProfile(source, vanilla, out selected) || !TryGetProfile(destination, !vanilla, out retained))
				return false;
			bool keepFree = !vanilla || HasFreeValues(destination);
			Dictionary<string, object> properties = CopyProperties(destination);
			if (keepFree)
				properties[PropertyName] = Encode(new Archive { Vanilla = vanilla, Saved = retained });
			else
				properties.Remove(PropertyName);
			if (selected.Order == CouplerRotationOrder.Default)
				properties.Remove(CouplerRotationOrder.PropertyName);
			else
				properties[CouplerRotationOrder.PropertyName] = CouplerRotationOrder.Name(selected.Order);
			destination.props = properties.Count == 0 ? null : properties;
			SetupPrecisionData.SetAngles(destination, selected.Angles);
			return true;
		}

		internal static bool Copy(BlockData destination, BlockData source)
		{
			if (!IsCoupler(destination) || !IsCoupler(source) || Matches(destination, source))
				return false;
			Dictionary<string, object> properties = CopyProperties(destination);
			if (HasArchive(source))
				properties[PropertyName] = source.props[PropertyName];
			else
				properties.Remove(PropertyName);
			destination.props = properties.Count == 0 ? null : properties;
			return true;
		}

		internal static bool Matches(BlockData first, BlockData second)
		{
			bool firstHasArchive = HasArchive(first);
			bool secondHasArchive = HasArchive(second);
			return firstHasArchive == secondHasArchive && (!firstHasArchive || Equals(first.props[PropertyName], second.props[PropertyName]));
		}

		internal static bool CopyToBuild(BlockData source, IList<BlockData> preview, IList<int> indices, IList<BlockData> build)
		{
			if (!IsCoupler(source) || preview == null || indices == null || build == null)
				return false;
			int previewIndex = preview.IndexOf(source);
			if (previewIndex < 0 || previewIndex >= indices.Count || indices[previewIndex] < 0 || indices[previewIndex] >= build.Count)
				return false;
			BlockData target = build[indices[previewIndex]];
			if (!IsCoupler(target) || ReferenceEquals(target, source))
				return false;
			bool changed = CouplerRotationOrder.Set(target, CouplerRotationOrder.Read(source));
			return Copy(target, source) || changed;
		}

		internal static void RotateSaved(BlockData block, Vector3 axis)
		{
			Archive archive;
			if (!TryRead(block, out archive))
				return;
			Quaternion basis = CouplerRotationMath.FromRotationVector(axis * 90f);
			Vector3 angles;
			if (archive.Vanilla)
			{
				Quaternion inverse = new Quaternion(-basis.x, -basis.y, -basis.z, basis.w);
				Quaternion rotation = CouplerRotationMath.FromEuler(archive.Saved.Angles, archive.Saved.Order);
				angles = CouplerRotationMath.ToEuler(basis * rotation * inverse, archive.Saved.Order);
			}
			else
				angles = basis * archive.Saved.Angles;
			archive.Saved = new Rotation(archive.Saved.Order, angles);
			SaveArchive(block, archive);
			CouplerRotation.Log("PROFILE_ROTATED block=" + Position(block) + " saved=" + FormatAngles(archive.Saved));
		}

		internal static void MirrorSaved(BlockData block)
		{
			Archive archive;
			if (!TryRead(block, out archive))
				return;
			Vector3 angles = archive.Saved.Angles;
			archive.Saved = new Rotation(archive.Saved.Order, new Vector3(angles.x, -angles.y, -angles.z));
			SaveArchive(block, archive);
			CouplerRotation.Log("PROFILE_MIRRORED block=" + Position(block) + " saved=" + FormatAngles(archive.Saved));
		}

		private static bool TryRead(BlockData block, out Archive archive)
		{
			archive = default(Archive);
			if (!HasArchive(block))
				return false;
			string stored = block.props[PropertyName] as string;
			if (stored == null || stored.Length > 64)
				return false;
			string[] fields = stored.Split('|');
			if (fields.Length != 6 || (fields[0] != "1" && fields[0] != "2" && fields[0] != "3") || (fields[1] != "F" && fields[1] != "V"))
				return false;
			int order = -1;
			for (int candidate = 0; candidate < CouplerRotationOrder.Count; candidate++)
				if (fields[2] == CouplerRotationOrder.Name(candidate))
					order = candidate;
			int angleX;
			int angleY;
			int angleZ;
			if (!int.TryParse(fields[3], NumberStyles.AllowLeadingSign, CultureInfo.InvariantCulture, out angleX)
				|| !int.TryParse(fields[4], NumberStyles.AllowLeadingSign, CultureInfo.InvariantCulture, out angleY)
				|| !int.TryParse(fields[5], NumberStyles.AllowLeadingSign, CultureInfo.InvariantCulture, out angleZ))
				return false;
			int storedScale = fields[0] == "1" ? 1 : fields[0] == "2" ? 100 : SetupPrecisionData.Scale;
			int limit = 180 * storedScale;
			if (Math.Abs((long)angleX) > limit || Math.Abs((long)angleY) > limit || Math.Abs((long)angleZ) > limit) return false;
			Rotation saved = storedScale == 1 ? new Rotation(order, angleX, angleY, angleZ)
				: new Rotation(order, new Vector3(angleX / (float)storedScale, angleY / (float)storedScale, angleZ / (float)storedScale));
			bool vanilla = fields[1] == "V";
			if (!saved.Valid || (!vanilla && !saved.Vanilla))
				return false;
			archive = new Archive { Vanilla = vanilla, Saved = saved };
			return true;
		}

		private static void SaveArchive(BlockData block, Archive archive)
		{
			Dictionary<string, object> properties = CopyProperties(block);
			properties[PropertyName] = Encode(archive);
			block.props = properties;
		}

		private static Dictionary<string, object> CopyProperties(BlockData block)
		{
			return block.props == null ? new Dictionary<string, object>() : new Dictionary<string, object>(block.props);
		}

		private static string Encode(Archive archive)
		{
			return "3|" + (archive.Vanilla ? "V" : "F") + "|" + CouplerRotationOrder.Name(archive.Saved.Order)
				+ "|" + archive.Saved.MilliX.ToString(CultureInfo.InvariantCulture)
				+ "|" + archive.Saved.MilliY.ToString(CultureInfo.InvariantCulture)
				+ "|" + archive.Saved.MilliZ.ToString(CultureInfo.InvariantCulture);
		}

		private static bool IsCoupler(BlockData block)
		{
			return block != null && block.type == BlockData.AAHMDBHDCDK.Coupler && block.actionParam != null && block.actionParam.Length >= 6;
		}

		private static bool InRange(int angle)
		{
			return angle >= -180 && angle <= 180;
		}

		private static string Position(BlockData block)
		{
			return block.x + "," + block.y + "," + block.z;
		}

		private static string FormatAngles(Rotation rotation)
		{
			return SetupPrecisionData.Format(rotation.Angles.x) + "," + SetupPrecisionData.Format(rotation.Angles.y) + "," + SetupPrecisionData.Format(rotation.Angles.z);
		}
	}
}
