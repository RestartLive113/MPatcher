using System;
using System.Reflection;
[assembly: AssemblyVersion("1.0.0.0")]
[assembly: AssemblyFileVersion("0.3.9.12")]
[assembly: AssemblyInformationalVersion("0.3.9.12")]
[assembly: MPatcherFork.ScaleRuntime.ScaleRuntimeApiVersionAttribute(1)]
namespace MPatcherFork.ScaleRuntime
{
    [AttributeUsage(AttributeTargets.Assembly, AllowMultiple = false)]
    internal sealed class ScaleRuntimeApiVersionAttribute : Attribute
    {
        internal ScaleRuntimeApiVersionAttribute(int version) { Version = version; }
        internal int Version { get; private set; }
    }
}