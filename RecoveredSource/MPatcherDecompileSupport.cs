using System;
using System.Reflection.Emit;

// ILSpy rendered the generic IL sizeof opcode as a dependency on
// System.Runtime.CompilerServices.Unsafe.  Emit the same opcode at runtime so
// the recovered assembly has no new external dependency.
internal static class MPatcherDecompileSupport
{
    internal static int SizeOf<T>()
    {
        return SizeCache<T>.Value;
    }

    private static class SizeCache<T>
    {
        internal static readonly int Value = Create();

        private static int Create()
        {
            DynamicMethod method = new DynamicMethod(
                "MPatcher_SizeOf_" + typeof(T).FullName,
                typeof(int),
                Type.EmptyTypes,
                typeof(MPatcherDecompileSupport).Module,
                true);
            ILGenerator il = method.GetILGenerator();
            il.Emit(OpCodes.Sizeof, typeof(T));
            il.Emit(OpCodes.Ret);
            return ((Func<int>)method.CreateDelegate(typeof(Func<int>)))();
        }
    }
}