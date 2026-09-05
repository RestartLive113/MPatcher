using System.Reflection;

namespace RuntimeInspectorNamespace;

public struct ExposedMethod
{
	private readonly MethodInfo ATEIRsIoE_ag2_00244sGq9jMqg;

	private readonly RuntimeInspectorButtonAttribute t1cmx2tRyEQcfJmGex9G7gE;

	private readonly bool Pm31Iyl3uopm879gc1J105p7V_0024LQej_7QtRitpQT_9fR;

	public string Label => t1cmx2tRyEQcfJmGex9G7gE.Label;

	public bool IsInitializer => t1cmx2tRyEQcfJmGex9G7gE.IsInitializer;

	public bool VisibleWhenInitialized => (t1cmx2tRyEQcfJmGex9G7gE.Visibility & ButtonVisibility.InitializedObjects) == ButtonVisibility.InitializedObjects;

	public bool VisibleWhenUninitialized => (t1cmx2tRyEQcfJmGex9G7gE.Visibility & ButtonVisibility.UninitializedObjects) == ButtonVisibility.UninitializedObjects;

	public ExposedMethod(MethodInfo methodInfo_0, RuntimeInspectorButtonAttribute runtimeInspectorButtonAttribute_0, bool bool_0)
	{
		ATEIRsIoE_ag2_00244sGq9jMqg = methodInfo_0;
		t1cmx2tRyEQcfJmGex9G7gE = runtimeInspectorButtonAttribute_0;
		Pm31Iyl3uopm879gc1J105p7V_0024LQej_7QtRitpQT_9fR = bool_0;
	}

	public void Call(object source)
	{
		if (Pm31Iyl3uopm879gc1J105p7V_0024LQej_7QtRitpQT_9fR)
		{
			smethod_0((MethodBase)ATEIRsIoE_ag2_00244sGq9jMqg, (object)null, new object[1] { source });
		}
		else if (!smethod_1((MethodBase)ATEIRsIoE_ag2_00244sGq9jMqg))
		{
			if (source != null)
			{
				smethod_0((MethodBase)ATEIRsIoE_ag2_00244sGq9jMqg, source, (object[])null);
			}
		}
		else
		{
			smethod_0((MethodBase)ATEIRsIoE_ag2_00244sGq9jMqg, (object)null, (object[])null);
		}
	}

	public object CallAndReturnValue(object source)
	{
		if (Pm31Iyl3uopm879gc1J105p7V_0024LQej_7QtRitpQT_9fR)
		{
			return smethod_0((MethodBase)ATEIRsIoE_ag2_00244sGq9jMqg, (object)null, new object[1] { source });
		}
		if (!smethod_1((MethodBase)ATEIRsIoE_ag2_00244sGq9jMqg))
		{
			if (source != null)
			{
				return smethod_0((MethodBase)ATEIRsIoE_ag2_00244sGq9jMqg, source, (object[])null);
			}
			return null;
		}
		return smethod_0((MethodBase)ATEIRsIoE_ag2_00244sGq9jMqg, (object)null, (object[])null);
	}

	internal static object smethod_0(MethodBase methodBase_0, object object_0, object[] object_1)
	{
		return methodBase_0.Invoke(object_0, object_1);
	}

	internal static bool smethod_1(MethodBase methodBase_0)
	{
		return methodBase_0.IsStatic;
	}
}
