using System;
using System.Collections.Generic;
using System.Reflection;
using System.Runtime.CompilerServices;
using HarmonyLib;

namespace MPatcherFork.CustomPatches
{
    // The native assembly solver owns Coupler placement semantics. The editor
    // visual probe omits only the physics gid filter, synchronously and for its
    // exact controller set. Every normal physics/editor call remains native.
    internal static class ConnectedShapePoseScope
    {
        private const string PatchId = "local.moddev.machinecraft.connected-shape-pose-scope.v1";
        private static bool registered;
        [ThreadStatic] private static Membership current;

        private sealed class ReferenceComparer : IEqualityComparer<object>
        {
            public new bool Equals(object first, object second) { return ReferenceEquals(first, second); }
            public int GetHashCode(object value) { return RuntimeHelpers.GetHashCode(value); }
        }

        private sealed class Membership : IDisposable
        {
            internal readonly HashSet<object> Controllers;
            private readonly Membership previous;
            private bool disposed;

            internal Membership(IEnumerable<object> controllers)
            {
                Controllers = new HashSet<object>(new ReferenceComparer());
                foreach (object controller in controllers)
                    if (!ReferenceEquals(controller, null)) Controllers.Add(controller);
                previous = current;
                current = this;
            }

            public void Dispose()
            {
                if (disposed) return;
                if (!ReferenceEquals(current, this))
                    throw new InvalidOperationException("Visual pose scopes must close in reverse order.");
                current = previous;
                disposed = true;
            }
        }

        internal static void TryRegister()
        {
            if (registered) return;
            Harmony patcher = new Harmony(PatchId);
            try
            {
                MethodInfo link = AccessTools.Method(typeof(BlockController), "CheckLinkG",
                    new Type[] { typeof(BlockController) });
                MethodInfo prefix = AccessTools.Method(typeof(ConnectedShapePoseScope), "CheckLinkPrefix");
                if (link == null || prefix == null) throw new MissingMethodException("Native visual pose CheckLinkG");
                patcher.Patch(link, new HarmonyMethod(prefix), null, null);
                registered = true;
                Log("REGISTERED target=BlockController.CheckLinkG scope=synchronous-thread-and-controller-set outside=native");
            }
            catch (Exception error)
            {
                Log("REGISTER_FAILED " + error);
                try { patcher.UnpatchAll(PatchId); }
                catch (Exception rollback) { Log("ROLLBACK_FAILED " + rollback); }
            }
        }

        internal static IDisposable Enter(IEnumerable<BlockController> controllers)
        {
            if (!registered) throw new InvalidOperationException("Visual link calculation is not registered.");
            if (current != null) throw new InvalidOperationException("Nested visual pose calculations are not allowed.");
            List<object> objects = new List<object>();
            foreach (BlockController controller in controllers) objects.Add(controller);
            return EnterObjects(objects);
        }

        // Pure membership entry allows an exact compiled-scope regression test
        // without creating Unity objects or installing a live Harmony patch.
        internal static IDisposable EnterObjects(IEnumerable<object> controllers)
        {
            if (controllers == null) throw new ArgumentNullException("controllers");
            return new Membership(controllers);
        }

        internal static bool ShouldRedirect(object first, object second)
        {
            return current != null && current.Controllers.Contains(first)
                && current.Controllers.Contains(second);
        }

        internal static bool Active { get { return current != null; } }

        private static bool CheckLinkPrefix(BlockController __instance, BlockController __0, ref bool __result)
        {
            if (!ShouldRedirect(__instance, __0)) return true;
            __result = __instance.CheckLink(__0);
            return false;
        }

        private static void Log(string message)
        {
            try { global::mK6lLU33ECSzxV4u22c7_0024ijC0MeyAkqA_PRIEl9WpAZK.YELeoCirSeVGf6u7nOIXkng("[CONNECTED-SHAPE-POSE] " + message); }
            catch (Exception) { }
        }
    }
}
