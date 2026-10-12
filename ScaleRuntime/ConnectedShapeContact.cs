using MPatcherFork.CustomPatches;
using System;
using UnityEngine;

namespace MPatcherFork.ScaleRuntime
{
    // Convex-hull distance for editor meshes. Box/Capsule template meshes are convex;
    // the broad phase uses their world AABBs, then GJK decides actual contact.
    internal static class ConnectedShapeContact
    {
        internal sealed class Hull
        {
            internal Vector3[] Points;
            internal Bounds Bounds;
        }

        internal static Hull Create(Mesh mesh, Vector3 position, Quaternion rotation, Vector3 scale)
        {
            if (mesh == null) return null;
            Vector3[] source;
            try { source = mesh.vertices; }
            catch (Exception) { return null; }
            if (source == null || source.Length == 0) return null;
            Matrix4x4 transform = Matrix4x4.TRS(position, rotation, scale);
            Vector3[] points = new Vector3[source.Length];
            for (int i = 0; i < source.Length; i++) points[i] = transform.MultiplyPoint3x4(source[i]);
            Bounds bounds = new Bounds(points[0], Vector3.zero);
            for (int i = 1; i < points.Length; i++) bounds.Encapsulate(points[i]);
            return new Hull { Points = points, Bounds = bounds };
        }

        internal static bool Touches(Hull a, Hull b, float tolerance)
        {
            if (a == null || b == null || !BoundsNear(a.Bounds, b.Bounds, tolerance)) return false;
            Vector3[] simplex = new Vector3[4];
            Vector3 direction = b.Bounds.center - a.Bounds.center;
            if (direction.sqrMagnitude < 0.00000001f) direction = Vector3.right;
            simplex[0] = Support(a, b, direction);
            int count = 1;
            Vector3 closest = simplex[0];
            float toleranceSquared = tolerance * tolerance;
            for (int iteration = 0; iteration < 48; iteration++)
            {
                if (closest.sqrMagnitude <= toleranceSquared) return true;
                direction = -closest;
                Vector3 next = Support(a, b, direction);
                float advance = Vector3.Dot(next - closest, direction);
                if (advance <= 0.0000001f * Mathf.Max(1f, direction.magnitude)) return false;
                for (int i = 0; i < count; i++)
                    if ((next - simplex[i]).sqrMagnitude <= 0.0000000001f) return false;
                simplex[count++] = next;
                if (Reduce(simplex, ref count, out closest)) return true;
            }
            return closest.sqrMagnitude <= toleranceSquared;
        }

        internal static bool BoundsNear(Bounds a, Bounds b, float tolerance)
        {
            return a.min.x <= b.max.x + tolerance && b.min.x <= a.max.x + tolerance
                && a.min.y <= b.max.y + tolerance && b.min.y <= a.max.y + tolerance
                && a.min.z <= b.max.z + tolerance && b.min.z <= a.max.z + tolerance;
        }

        private static Vector3 Support(Hull a, Hull b, Vector3 direction)
        {
            Vector3 bestA = a.Points[0];
            Vector3 bestB = b.Points[0];
            float scoreA = Vector3.Dot(bestA, direction);
            float scoreB = Vector3.Dot(bestB, direction);
            for (int i = 1; i < a.Points.Length; i++)
            {
                float score = Vector3.Dot(a.Points[i], direction);
                if (score > scoreA) { scoreA = score; bestA = a.Points[i]; }
            }
            for (int i = 1; i < b.Points.Length; i++)
            {
                float score = Vector3.Dot(b.Points[i], direction);
                if (score < scoreB) { scoreB = score; bestB = b.Points[i]; }
            }
            return bestA - bestB;
        }

        private static bool Reduce(Vector3[] simplex, ref int count, out Vector3 closest)
        {
            if (count == 2)
            {
                Vector3 a = simplex[0], b = simplex[1];
                float lengthSquared = (b - a).sqrMagnitude;
                float t = lengthSquared <= 0.0000000001f ? 0f : Mathf.Clamp01(-Vector3.Dot(a, b - a) / lengthSquared);
                closest = a + (b - a) * t;
                if (t <= 0f) { simplex[0] = a; count = 1; }
                else if (t >= 1f) { simplex[0] = b; count = 1; }
                return false;
            }
            if (count == 3)
            {
                float x, y, z;
                closest = TriangleClosest(simplex[0], simplex[1], simplex[2], out x, out y, out z);
                SelectTriangle(simplex, ref count, simplex[0], simplex[1], simplex[2], x, y, z);
                return false;
            }
            Vector3 p0 = simplex[0], p1 = simplex[1], p2 = simplex[2], p3 = simplex[3];
            if (OriginInsideTetrahedron(p0, p1, p2, p3)) { closest = Vector3.zero; return true; }
            Vector3[] points = { p0, p1, p2, p3 };
            int[,] faces = { { 0, 1, 2 }, { 0, 1, 3 }, { 0, 2, 3 }, { 1, 2, 3 } };
            float best = float.PositiveInfinity;
            int face = 0;
            float bestX = 0f, bestY = 0f, bestZ = 0f;
            closest = Vector3.zero;
            for (int i = 0; i < 4; i++)
            {
                float x, y, z;
                Vector3 candidate = TriangleClosest(points[faces[i, 0]], points[faces[i, 1]], points[faces[i, 2]], out x, out y, out z);
                if (candidate.sqrMagnitude >= best) continue;
                best = candidate.sqrMagnitude;
                closest = candidate;
                face = i;
                bestX = x; bestY = y; bestZ = z;
            }
            SelectTriangle(simplex, ref count, points[faces[face, 0]], points[faces[face, 1]], points[faces[face, 2]], bestX, bestY, bestZ);
            return false;
        }

        private static void SelectTriangle(Vector3[] simplex, ref int count, Vector3 a, Vector3 b, Vector3 c,
            float x, float y, float z)
        {
            count = 0;
            if (x > 0.000001f) simplex[count++] = a;
            if (y > 0.000001f) simplex[count++] = b;
            if (z > 0.000001f) simplex[count++] = c;
            if (count == 0) { simplex[0] = a; count = 1; }
        }

        private static bool OriginInsideTetrahedron(Vector3 a, Vector3 b, Vector3 c, Vector3 d)
        {
            Vector3 ab = b - a, ac = c - a, ad = d - a, ao = -a;
            float determinant = Vector3.Dot(ab, Vector3.Cross(ac, ad));
            if (Mathf.Abs(determinant) <= 0.0000000001f) return false;
            float bWeight = Vector3.Dot(ao, Vector3.Cross(ac, ad)) / determinant;
            float cWeight = Vector3.Dot(ab, Vector3.Cross(ao, ad)) / determinant;
            float dWeight = Vector3.Dot(ab, Vector3.Cross(ac, ao)) / determinant;
            float aWeight = 1f - bWeight - cWeight - dWeight;
            return aWeight >= -0.000001f && bWeight >= -0.000001f
                && cWeight >= -0.000001f && dWeight >= -0.000001f;
        }

        private static Vector3 TriangleClosest(Vector3 a, Vector3 b, Vector3 c,
            out float x, out float y, out float z)
        {
            Vector3 ab = b - a, ac = c - a;
            if (Vector3.Cross(ab, ac).sqrMagnitude <= 0.0000000001f)
            {
                Vector3 best = a;
                float bestDistance = a.sqrMagnitude;
                x = 1f; y = z = 0f;
                ChooseSegment(a, b, 0, ref best, ref bestDistance, ref x, ref y, ref z);
                ChooseSegment(a, c, 1, ref best, ref bestDistance, ref x, ref y, ref z);
                ChooseSegment(b, c, 2, ref best, ref bestDistance, ref x, ref y, ref z);
                return best;
            }
            Vector3 ap = -a;
            float d1 = Vector3.Dot(ab, ap), d2 = Vector3.Dot(ac, ap);
            if (d1 <= 0f && d2 <= 0f) { x = 1f; y = z = 0f; return a; }
            Vector3 bp = -b;
            float d3 = Vector3.Dot(ab, bp), d4 = Vector3.Dot(ac, bp);
            if (d3 >= 0f && d4 <= d3) { y = 1f; x = z = 0f; return b; }
            float vc = d1 * d4 - d3 * d2;
            if (vc <= 0f && d1 >= 0f && d3 <= 0f)
            {
                y = d1 / (d1 - d3); x = 1f - y; z = 0f;
                return a + ab * y;
            }
            Vector3 cp = -c;
            float d5 = Vector3.Dot(ab, cp), d6 = Vector3.Dot(ac, cp);
            if (d6 >= 0f && d5 <= d6) { z = 1f; x = y = 0f; return c; }
            float vb = d5 * d2 - d1 * d6;
            if (vb <= 0f && d2 >= 0f && d6 <= 0f)
            {
                z = d2 / (d2 - d6); x = 1f - z; y = 0f;
                return a + ac * z;
            }
            float va = d3 * d6 - d5 * d4;
            if (va <= 0f && d4 - d3 >= 0f && d5 - d6 >= 0f)
            {
                z = (d4 - d3) / ((d4 - d3) + (d5 - d6));
                y = 1f - z; x = 0f;
                return b + (c - b) * z;
            }
            float denominator = 1f / (va + vb + vc);
            y = vb * denominator; z = vc * denominator; x = 1f - y - z;
            return a + ab * y + ac * z;
        }

        private static void ChooseSegment(Vector3 a, Vector3 b, int edge, ref Vector3 best,
            ref float bestDistance, ref float x, ref float y, ref float z)
        {
            float lengthSquared = (b - a).sqrMagnitude;
            float t = lengthSquared <= 0.0000000001f ? 0f : Mathf.Clamp01(-Vector3.Dot(a, b - a) / lengthSquared);
            Vector3 point = a + (b - a) * t;
            if (point.sqrMagnitude >= bestDistance) return;
            best = point;
            bestDistance = point.sqrMagnitude;
            if (edge == 0) { x = 1f - t; y = t; z = 0f; }
            else if (edge == 1) { x = 1f - t; y = 0f; z = t; }
            else { x = 0f; y = 1f - t; z = t; }
        }
    }
}
