using MPatcherFork.CustomPatches;
using System;
using System.Collections.Generic;
using UnityEngine;

namespace MPatcherFork.ScaleRuntime
{
    // Selection reads rendered triangles only. Hidden editor colliders remain
    // untouched and cannot become a visible surface by an AABB fallback.
    internal static class ShapeScalePicking
    {
        private const float Epsilon = 0.000001f;

        internal static bool VisiblePolicy(bool enabled, bool activeInHierarchy)
        { return enabled && activeInHierarchy; }

        internal static bool CanPickRenderer(Renderer renderer)
        {
            return renderer != null && VisiblePolicy(renderer.enabled, renderer.gameObject.activeInHierarchy);
        }

        internal static bool TryClosestSurface(IEnumerable<Renderer> renderers, Ray ray,
            bool cullBackfaces, out float distance)
        {
            distance = float.PositiveInfinity;
            if (renderers == null) return false;
            foreach (Renderer renderer in renderers)
            {
                float hit;
                if (TrySurface(renderer, ray, cullBackfaces, out hit) && hit < distance) distance = hit;
            }
            return !float.IsInfinity(distance);
        }

        internal static bool TrySurface(Renderer renderer, Ray ray, bool cullBackfaces, out float distance)
        {
            distance = float.PositiveInfinity;
            if (!CanPickRenderer(renderer)) return false;
            MeshFilter filter = renderer.GetComponent<MeshFilter>();
            if (filter == null || filter.sharedMesh == null) return false;
            float coarse;
            if (!renderer.bounds.IntersectRay(ray, out coarse)) return false;
            try
            {
                Mesh mesh = filter.sharedMesh;
                Vector3[] vertices = mesh.vertices;
                int[] triangles = mesh.triangles;
                Matrix4x4 inverse = filter.transform.worldToLocalMatrix;
                Matrix4x4 forward = filter.transform.localToWorldMatrix;
                Vector3 origin = inverse.MultiplyPoint3x4(ray.origin);
                Vector3 direction = inverse.MultiplyVector(ray.direction);
                // Mirrored transforms reverse the displayed winding. Keep the
                // ray parameter unnormalised locally so its distance stays world t.
                float orientation = Vector3.Dot(forward.MultiplyVector(Vector3.right),
                    Vector3.Cross(forward.MultiplyVector(Vector3.up), forward.MultiplyVector(Vector3.forward)));
                for (int i = 0; i + 2 < triangles.Length; i += 3)
                {
                    int first = triangles[i], second = triangles[i + 1], third = triangles[i + 2];
                    if (first < 0 || second < 0 || third < 0
                        || first >= vertices.Length || second >= vertices.Length || third >= vertices.Length) continue;
                    float hit;
                    if (TriangleHit(origin, direction, vertices[first], vertices[second], vertices[third],
                        cullBackfaces, orientation, out hit) && hit < distance) distance = hit;
                }
            }
            catch (Exception)
            {
                // Native/imported unreadable meshes do not authorize a cube or
                // bounds hit: another visible supported surface may be selected.
                distance = float.PositiveInfinity;
                return false;
            }
            return !float.IsInfinity(distance);
        }

        internal static bool TriangleHit(Vector3 origin, Vector3 direction, Vector3 first,
            Vector3 second, Vector3 third, bool cullBackfaces, float orientation, out float distance)
        {
            distance = float.PositiveInfinity;
            Vector3 edge1 = second - first, edge2 = third - first;
            Vector3 cross = Vector3.Cross(direction, edge2);
            float determinant = Vector3.Dot(edge1, cross);
            if (float.IsNaN(determinant) || float.IsInfinity(determinant) || Math.Abs(determinant) < Epsilon
                || float.IsNaN(orientation) || float.IsInfinity(orientation) || orientation == 0f
                || cullBackfaces && determinant * (orientation < 0f ? -1f : 1f) <= Epsilon) return false;
            float reciprocal = 1f / determinant;
            Vector3 offset = origin - first;
            float u = Vector3.Dot(offset, cross) * reciprocal;
            if (u < 0f || u > 1f) return false;
            Vector3 q = Vector3.Cross(offset, edge1);
            float v = Vector3.Dot(direction, q) * reciprocal;
            if (v < 0f || u + v > 1f) return false;
            float hit = Vector3.Dot(edge2, q) * reciprocal;
            if (float.IsNaN(hit) || float.IsInfinity(hit) || hit < 0f) return false;
            distance = hit;
            return true;
        }
    }
}
