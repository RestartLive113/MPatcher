using System;
using System.Globalization;
using System.Reflection;
using UnityEngine;

namespace MPatcherFork.CustomPatches
{
    internal static class MagnificationSledVisualUnits
    {
        private static readonly FieldInfo Hook = typeof(SledController).GetField("POMMKHACNGA", BindingFlags.Instance | BindingFlags.NonPublic);
        internal static void Apply(SledController sled, float scale)
        {
            if (!MagnificationScalePolicy.SameTime || MagnificationScalePolicy.Bypass(scale) || Hook == null) return;
            GameObject hook = Hook.GetValue(sled) as GameObject;
            if (hook == null || sled.OFIGLDAHIHG == null) return;
            // Native Start parents a world-sized visual without resetting its
            // local scale. The physical Roller already inherits the correct scale.
            if (hook.transform.parent != sled.transform || hook.GetComponentsInChildren<Collider>(true).Length != 0
                || hook.GetComponentsInChildren<Rigidbody>(true).Length != 0)
            {
                MagnificationDownscale.Log("SLED_VISUAL_SCALE_SKIPPED reason=unexpected-hook-prefab"); return;
            }
            Vector3 before = hook.transform.lossyScale;
            hook.transform.localScale = sled.OFIGLDAHIHG.transform.localScale;
            Rigidbody roller = MagnificationMassBoundary.SledBody(sled);
            SphereCollider sphere = roller == null ? null : roller.GetComponent<SphereCollider>();
            MagnificationDownscale.Log("SLED_VISUAL_SCALED scale=" + scale.ToString("R", CultureInfo.InvariantCulture)
                + " hookWorldBefore=" + before.ToString("R") + " hookWorldAfter=" + hook.transform.lossyScale.ToString("R")
                + " hookColliders=0 rollerGeometryWritten=false rollerRadiusWorld="
                + (sphere == null ? "missing" : sphere.bounds.extents.x.ToString("R", CultureInfo.InvariantCulture)));
        }
    }
}
