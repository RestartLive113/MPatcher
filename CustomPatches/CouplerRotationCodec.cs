using System;
using System.Collections.Generic;
using UnityEngine;

namespace MPatcherFork.CustomPatches
{
	internal static class CouplerRotationCodec
	{
		private const int ExtendedMarker = 0x3C000000;
		private const int AngleVersion = 1 << 27;
		private const int OrderedAngleVersion = 2 << 27;
		private const int FooterWords = 6;

		internal static bool IsExtended(uint header)
		{
			return (header & ExtendedMarker) == ExtendedMarker;
		}

		internal static Vector3 Write(List<int> words, Vector3 rotationVector, Vector3 position)
		{
			int order;
			return WriteRecord(words, rotationVector, position, out order);
		}

		internal static Vector3 WriteRecord(List<int> words, Vector3 rotationVector, Vector3 position, out int order)
		{
			Quaternion rotation = CouplerRotationMath.FromRotationVector(rotationVector);
			Vector3 angles = IntegerRepresentation(rotation, out order);
			int angleX = (int)Math.Round(angles.x);
			int angleY = (int)Math.Round(angles.y);
			int angleZ = (int)Math.Round(angles.z);
			int version = order == CouplerRotationOrder.Default ? AngleVersion : OrderedAngleVersion;
			int packedAngles = version | (angleX + 180) | ((angleY + 180) << 9) | ((angleZ + 180) << 18);
			words.Add(0x40000000 | ExtendedMarker | (order << 16) | ((int)Math.Round(position.x * 100f) & 0xFFFF));
			words.Add(((int)Math.Round(position.y * 100f) & 0xFFFF) | (((int)Math.Round(position.z * 100f) & 0xFFFF) << 16));
			words.Add(packedAngles);
			return new Vector3(angleX, angleY, angleZ);
		}

		internal static bool Read(uint header, int[] words, ref int cursor, out Vector3 angles, out Vector3 position)
		{
			int order;
			bool valid = ReadRecord(header, words, ref cursor, out angles, out position, out order);
			if (valid && order != CouplerRotationOrder.Default)
				angles = CouplerRotationMath.ToBoxEuler(CouplerRotationMath.FromEuler(angles, order));
			return valid;
		}

		internal static bool ReadRecord(uint header, int[] words, ref int cursor, out Vector3 angles, out Vector3 position, out int order)
		{
			angles = Vector3.zero;
			position = Vector3.zero;
			order = CouplerRotationOrder.Default;
			if (!IsExtended(header))
				return false;
			int end = words == null ? 0 : Math.Max(0, words.Length - FooterWords);
			if (cursor < 0 || cursor >= end - 1)
			{
				cursor = end;
				return false;
			}
			int offsetWord = words[cursor++];
			int angleWord = words[cursor++];
			position = new Vector3((short)(header & 0xFFFF), (short)(offsetWord & 0xFFFF), (short)(offsetWord >> 16)) * 0.01f;
			int angleX = angleWord & 0x1FF;
			int angleY = (angleWord >> 9) & 0x1FF;
			int angleZ = (angleWord >> 18) & 0x1FF;
			int version = angleWord & unchecked((int)0xF8000000);
			int encodedOrder = (int)((header >> 16) & 7);
			if ((version != AngleVersion && version != OrderedAngleVersion)
				|| !CouplerRotationOrder.IsValid(encodedOrder)
				|| (version == AngleVersion && encodedOrder != CouplerRotationOrder.Default)
				|| (header & 0x03F80000) != 0 || angleX > 360 || angleY > 360 || angleZ > 360)
				return false;
			order = encodedOrder;
			angles = new Vector3(angleX - 180, angleY - 180, angleZ - 180);
			return true;
		}

		private static Vector3 IntegerRepresentation(Quaternion rotation, out int order)
		{
			Vector3 bestAngles = Vector3.zero;
			double bestError = double.MaxValue;
			order = CouplerRotationOrder.Default;
			for (int candidate = 0; candidate < CouplerRotationOrder.Count; candidate++)
			{
				Vector3 angles = CouplerRotationMath.ToEuler(rotation, candidate);
				angles = new Vector3((float)Math.Round(angles.x), (float)Math.Round(angles.y), (float)Math.Round(angles.z));
				double error = CouplerRotationMath.RotationErrorSquared(rotation, CouplerRotationMath.FromEuler(angles, candidate));
				if (error < bestError)
				{
					bestAngles = angles;
					bestError = error;
					order = candidate;
				}
				if (bestError < 1e-12)
					break;
			}
			return bestAngles;
		}
	}
}
