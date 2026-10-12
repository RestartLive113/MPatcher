using MPatcherFork.CustomPatches;
using System;
using System.Collections.Generic;
using System.Reflection;
using HarmonyLib;
using UnityEngine;

namespace MPatcherFork.ScaleRuntime
{
    // Unlike native All/Mask refresh, this never rebuilds blocks or changes colliders.
    internal sealed class ShapeScaleVisibility
    {
        private static readonly FieldInfo Graph = AccessTools.Field(typeof(Build), "EPGELCMKKOC");
        private sealed class State
        {
            internal Renderer Renderer;
            internal BlockController Owner;
            internal bool Enabled;
        }
        private readonly Build build;
        private readonly ConnectedShapeScaleV2 scaler;
        private readonly List<State> states = new List<State>();
        private readonly List<BlockController> blocks = new List<BlockController>();
        private readonly HashSet<BlockController> graphOwners = new HashSet<BlockController>();
        private readonly HashSet<BlockController> replacementOwners = new HashSet<BlockController>();
        private readonly Dictionary<BlockController, List<Renderer>> carrierSurfaces = new Dictionary<BlockController, List<Renderer>>();
        private HDBLLPODNLN graph;
        private int revision = -1;

        internal ShapeScaleVisibility(Build owner, ConnectedShapeScaleV2 control)
        { build = owner; scaler = control; }

        internal static bool DisplayType(BlockData.AAHMDBHDCDK type)
        {
            return type == BlockData.AAHMDBHDCDK.BoxGen || type == BlockData.AAHMDBHDCDK.CapGen
                || type == BlockData.AAHMDBHDCDK.Coupler;
        }

        internal static bool ShouldHide(BlockData.AAHMDBHDCDK type, bool selected,
            bool replacementVisible, bool generatedShape)
        {
            // A projected replacement supersedes native geometry as well as its
            // carrier. Otherwise the old unprojected mesh remains at another point.
            return !DisplayType(type) || (selected && replacementVisible);
        }

        internal static BlockController RendererOwner(Transform transform, HashSet<BlockController> graphControllers)
        {
            // Unity 5 GetComponentInParent skips inactive GameObjects. Build culls
            // distant carriers, so ownership must be resolved without that filter.
            while (transform != null)
            {
                BlockController owner = transform.GetComponent<BlockController>();
                // Sculpt's editor prefab has a disabled Chassis controller which
                // is not a machine block; keep walking to its real BoxGen owner.
                if (owner != null && graphControllers.Contains(owner)) return owner;
                transform = transform.parent;
            }
            return null;
        }

        internal BlockController PickOwner(Transform transform)
        { return RendererOwner(transform, graphOwners); }

        internal bool TryCarrierSurface(BlockController owner, Ray ray, out float distance)
        {
            distance = float.PositiveInfinity;
            List<Renderer> surfaces;
            return owner != null && carrierSurfaces.TryGetValue(owner, out surfaces)
                && ShapeScalePicking.TryClosestSurface(surfaces, ray, false, out distance);
        }

        internal void Refresh()
        {
            HDBLLPODNLN current = Graph.GetValue(build) as HDBLLPODNLN;
            if (current == null || current.GEGOAHPBGKB == null) { Restore(); return; }
            bool changed = graph != current || blocks.Count != current.GEGOAHPBGKB.Count
                || revision != scaler.SelectionRevision;
            if (!changed)
                for (int i = 0; i < blocks.Count; i++)
                    if (blocks[i] != current.GEGOAHPBGKB[i]) { changed = true; break; }
            if (changed)
            {
                Restore();
                graph = current;
                revision = scaler.SelectionRevision;
                foreach (BlockController block in current.GEGOAHPBGKB)
                    if (block != null) graphOwners.Add(block);
                foreach (BlockController block in current.GEGOAHPBGKB)
                {
                    blocks.Add(block);
                    if (block == null) continue;
                    foreach (Renderer renderer in block.GetComponentsInChildren<Renderer>(true))
                        if (RendererOwner(renderer.transform, graphOwners) == block)
                        {
                            states.Add(new State { Renderer = renderer, Owner = block, Enabled = renderer.enabled });
                            if (!ConnectedShapeScaleV2.IsGeneratedShapeRenderer(block, renderer))
                            {
                                List<Renderer> surfaces;
                                if (!carrierSurfaces.TryGetValue(block, out surfaces))
                                { surfaces = new List<Renderer>(); carrierSurfaces.Add(block, surfaces); }
                                surfaces.Add(renderer);
                            }
                        }
                }
                Log("VISIBILITY_CAPTURE blocks=" + blocks.Count + " renderers=" + states.Count
                    + " selected=" + scaler.HasSelection + " policy=Box-Capsule-Coupler selected-carriers=hidden ownership=graph-controller-active-or-inactive");
            }
            scaler.CollectVisibleReplacementOwners(replacementOwners);
            foreach (State state in states)
            {
                if (state.Renderer == null || state.Owner == null || state.Owner.JNKEKNOAPHO == null) continue;
                bool hide = ShouldHide(state.Owner.JNKEKNOAPHO.type, scaler.IsSelectedCarrier(state.Owner),
                    replacementOwners.Contains(state.Owner),
                    ConnectedShapeScaleV2.IsGeneratedShapeRenderer(state.Owner, state.Renderer));
                // A native mask's initially hidden renderer stays hidden throughout Scale.
                state.Renderer.enabled = state.Enabled && !hide;
            }
        }

        internal void Restore()
        {
            foreach (State state in states)
                if (state.Renderer != null) state.Renderer.enabled = state.Enabled;
            if (states.Count > 0) Log("VISIBILITY_RESTORE renderers=" + states.Count);
            states.Clear(); blocks.Clear(); graphOwners.Clear(); replacementOwners.Clear(); carrierSurfaces.Clear(); graph = null; revision = -1;
        }

        private static void Log(string message)
        {
            try { ScaleRuntimeHost.Log("[SCULPT-SCALE-UI] " + message); }
            catch (Exception) { }
        }
    }
}
