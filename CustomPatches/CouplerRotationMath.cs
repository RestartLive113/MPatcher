using System;
using UnityEngine;

namespace MPatcherFork.CustomPatches
{
	internal static class CouplerRotationMath
	{
		private const double DegreesToRadians = Math.PI / 180.0;
		private const double RadiansToDegrees = 180.0 / Math.PI;

		internal static bool IsMixed(Vector3 rotation)
		{
			return (Math.Abs(rotation.x) > 0.00001f ? 1 : 0) + (Math.Abs(rotation.y) > 0.00001f ? 1 : 0)
				+ (Math.Abs(rotation.z) > 0.00001f ? 1 : 0) > 1;
		}

		internal static Quaternion FromBoxEuler(Vector3 angles)
		{
			double halfX = -angles.x * DegreesToRadians * 0.5;
			double halfY = -angles.y * DegreesToRadians * 0.5;
			double halfZ = -angles.z * DegreesToRadians * 0.5;
			double sineX = Math.Sin(halfX);
			double sineY = Math.Sin(halfY);
			double sineZ = Math.Sin(halfZ);
			double cosineX = Math.Cos(halfX);
			double cosineY = Math.Cos(halfY);
			double cosineZ = Math.Cos(halfZ);
			return new Quaternion(
				(float)(cosineY * sineX * cosineZ + sineY * cosineX * sineZ),
				(float)(sineY * cosineX * cosineZ - cosineY * sineX * sineZ),
				(float)(cosineY * cosineX * sineZ - sineY * sineX * cosineZ),
				(float)(cosineY * cosineX * cosineZ + sineY * sineX * sineZ));
		}

		internal static Quaternion FromEuler(Vector3 angles, int order)
		{
			if (order == CouplerRotationOrder.Default || !CouplerRotationOrder.IsValid(order))
				return FromBoxEuler(angles);
			Quaternion rotation = Quaternion.identity;
			for (int row = 0; row < 3; row++)
			{
				int axis = CouplerRotationOrder.Axis(order, row);
				double halfAngle = -angles[axis] * DegreesToRadians * 0.5;
				float sine = (float)Math.Sin(halfAngle);
				Quaternion step = new Quaternion(axis == 0 ? sine : 0f, axis == 1 ? sine : 0f,
					axis == 2 ? sine : 0f, (float)Math.Cos(halfAngle));
				rotation = rotation * step;
			}
			return rotation;
		}

		internal static Vector3 ToEuler(Quaternion rotation, int order)
		{
			if (order == CouplerRotationOrder.Default || !CouplerRotationOrder.IsValid(order))
				return ToBoxEuler(rotation);
			int virtualX = CouplerRotationOrder.Axis(order, 1);
			int virtualY = CouplerRotationOrder.Axis(order, 0);
			int virtualZ = CouplerRotationOrder.Axis(order, 2);
			int inversions = (virtualX > virtualY ? 1 : 0) + (virtualX > virtualZ ? 1 : 0) + (virtualY > virtualZ ? 1 : 0);
			float orientationSign = (inversions & 1) == 0 ? 1f : -1f;
			Quaternion permuted = new Quaternion(rotation[virtualX] * orientationSign, rotation[virtualY] * orientationSign,
				rotation[virtualZ] * orientationSign, rotation.w);
			Vector3 angles = ToBoxEuler(permuted) * orientationSign;
			Vector3 result = Vector3.zero;
			result[virtualX] = angles.x;
			result[virtualY] = angles.y;
			result[virtualZ] = angles.z;
			return result;
		}

		internal static double RotationErrorSquared(Quaternion rotation, Quaternion other)
		{
			double direct = 0.0;
			double negated = 0.0;
			for (int component = 0; component < 4; component++)
			{
				double difference = (double)rotation[component] - other[component];
				double sum = (double)rotation[component] + other[component];
				direct += difference * difference;
				negated += sum * sum;
			}
			return Math.Min(direct, negated);
		}

		internal static Vector3 ToRotationVector(Quaternion rotation)
		{
			double magnitude = Math.Sqrt((double)rotation.x * rotation.x + (double)rotation.y * rotation.y
				+ (double)rotation.z * rotation.z);
			if (magnitude < 1e-12)
				return Vector3.zero;
			double sign = rotation.w < 0f ? -1.0 : 1.0;
			double scale = -sign * 2.0 * Math.Atan2(magnitude, Math.Abs(rotation.w)) * RadiansToDegrees / magnitude;
			return new Vector3((float)(rotation.x * scale), (float)(rotation.y * scale), (float)(rotation.z * scale));
		}

		internal static Quaternion FromRotationVector(Vector3 rotation)
		{
			double magnitude = Math.Sqrt((double)rotation.x * rotation.x + (double)rotation.y * rotation.y
				+ (double)rotation.z * rotation.z);
			if (magnitude < 1e-12)
				return Quaternion.identity;
			double halfAngle = -magnitude * DegreesToRadians * 0.5;
			double scale = Math.Sin(halfAngle) / magnitude;
			return new Quaternion((float)(rotation.x * scale), (float)(rotation.y * scale),
				(float)(rotation.z * scale), (float)Math.Cos(halfAngle));
		}

		internal static Vector3 ToBoxEuler(Quaternion rotation)
		{
			double componentX = rotation.x;
			double componentY = rotation.y;
			double componentZ = rotation.z;
			double componentW = rotation.w;
			double norm = componentX * componentX + componentY * componentY + componentZ * componentZ + componentW * componentW;
			if (norm < 1e-12)
				return Vector3.zero;
			double sineX = Math.Max(-1.0, Math.Min(1.0, 2.0 * (componentY * componentZ - componentX * componentW) / norm));
			double angleX = Math.Asin(sineX);
			double angleY;
			double angleZ;
			if (1.0 - sineX * sineX > 1e-10)
			{
				angleY = -Math.Atan2(2.0 * (componentX * componentZ + componentY * componentW),
					norm - 2.0 * (componentX * componentX + componentY * componentY));
				angleZ = -Math.Atan2(2.0 * (componentX * componentY + componentZ * componentW),
					norm - 2.0 * (componentX * componentX + componentZ * componentZ));
			}
			else
			{
				angleY = Math.Atan2(2.0 * (componentX * componentZ - componentY * componentW),
					norm - 2.0 * (componentY * componentY + componentZ * componentZ));
				angleZ = 0.0;
			}
			return new Vector3((float)(angleX * RadiansToDegrees), (float)(angleY * RadiansToDegrees),
				(float)(angleZ * RadiansToDegrees));
		}
	}
}
