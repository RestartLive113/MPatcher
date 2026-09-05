using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;

internal static class TOZmG6HRM9kr_0024vysloxzF6vqAPZhI51GXJz9SNSgTege
{
	public static IEnumerable<Type> p3nbSpNsjKdbH6O8i2Fww0ahj07ygzoRqiSdbQqKuD59(this Assembly assembly_0)
	{
		try
		{
			return smethod_0(assembly_0);
		}
		catch (ReflectionTypeLoadException reflectionTypeLoadException_)
		{
			return from type_0 in smethod_1(reflectionTypeLoadException_)
				where type_0 != null
				select type_0;
		}
	}

	internal static Type[] smethod_0(Assembly assembly_0)
	{
		return assembly_0.GetTypes();
	}

	internal static Type[] smethod_1(ReflectionTypeLoadException reflectionTypeLoadException_0)
	{
		return reflectionTypeLoadException_0.Types;
	}
}
