using UnityEngine;
using VRGIN.Core;

internal class Nz27rvAh_TUCPsctJNzLiCjjTfZt7XFBnnDiA7F_0024B8yr6ll5sodkRaq_0024s3PhpIOSQA : GameInterpreter
{
	public virtual bool _0024BGuNnITZW8qtcjPWKOeQD8(Canvas canvas)
	{
		return method_0(canvas);
	}

	public virtual bool I_0024ukdwUW_0024RErv7BWp86UKZs(Camera camera)
	{
		return smethod_1(smethod_0((Object)camera), global::_003CModule_003E.smethod_25<string>(2284215744u));
	}

	public virtual bool MK5aAP_Jd2UbBRsfO_PlPAg(Collider collider)
	{
		return false;
	}

	public virtual bool O24x_0024GCpbkH92RwsOgSJPvc(MonoBehaviour effect)
	{
		return true;
	}

	public virtual bool jQMB9s7MpLqAxwdtNfFFSZNbQH_00244zlHBzRdmpLTrfdAB(Camera blueprint)
	{
		return method_1(blueprint);
	}

	bool method_0(Canvas canvas_0)
	{
		return base.IsIgnoredCanvas(canvas_0);
	}

	internal static string smethod_0(Object object_0)
	{
		return object_0.name;
	}

	internal static bool smethod_1(string string_0, string string_1)
	{
		return string_0.Equals(string_1);
	}

	bool method_1(Camera camera_0)
	{
		return base.IsIrrelevantCamera(camera_0);
	}
}
