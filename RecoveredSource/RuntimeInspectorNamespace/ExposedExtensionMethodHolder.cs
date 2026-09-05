using System;
using System.Reflection;

namespace RuntimeInspectorNamespace;

public struct ExposedExtensionMethodHolder
{
	public readonly Type extendedType;

	public readonly MethodInfo method;

	public readonly RuntimeInspectorButtonAttribute properties;

	public ExposedExtensionMethodHolder(Type type_0, MethodInfo methodInfo_0, RuntimeInspectorButtonAttribute runtimeInspectorButtonAttribute_0)
	{
		extendedType = type_0;
		method = methodInfo_0;
		properties = runtimeInspectorButtonAttribute_0;
	}
}
