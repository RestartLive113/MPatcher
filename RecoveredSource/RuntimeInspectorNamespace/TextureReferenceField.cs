using System;
using UnityEngine;
using UnityEngine.UI;

namespace RuntimeInspectorNamespace;

public class TextureReferenceField : ObjectReferenceField
{
	[SerializeField]
	private RawImage ZeMqqNVDnjjjn5eAvSS8lwSbRd5Bw2HwMshUyuhJ8myK;

	protected override float HeightMultiplier => 2f;

	public override bool SupportsType(Type type)
	{
		if (!smethod_31(smethod_30(typeof(Texture).TypeHandle), type))
		{
			return smethod_31(smethod_30(typeof(Sprite).TypeHandle), type);
		}
		return true;
	}

	protected override void OnReferenceChanged(UnityEngine.Object reference)
	{
		base.OnReferenceChanged(reference);
		smethod_34(smethod_32((Component)referenceNameText), !smethod_33(reference));
		Texture texture = reference.GetTexture();
		smethod_36((Behaviour)ZeMqqNVDnjjjn5eAvSS8lwSbRd5Bw2HwMshUyuhJ8myK, smethod_35((UnityEngine.Object)texture, (UnityEngine.Object)null));
		smethod_37(ZeMqqNVDnjjjn5eAvSS8lwSbRd5Bw2HwMshUyuhJ8myK, texture);
	}

	internal static Type smethod_30(RuntimeTypeHandle runtimeTypeHandle_0)
	{
		return Type.GetTypeFromHandle(runtimeTypeHandle_0);
	}

	internal static bool smethod_31(Type type_0, Type type_1)
	{
		return type_0.IsAssignableFrom(type_1);
	}

	internal static GameObject smethod_32(Component component_0)
	{
		return component_0.gameObject;
	}

	internal static bool smethod_33(UnityEngine.Object object_0)
	{
		return object_0;
	}

	internal static void smethod_34(GameObject gameObject_0, bool bool_0)
	{
		gameObject_0.SetActive(bool_0);
	}

	internal static bool smethod_35(UnityEngine.Object object_0, UnityEngine.Object object_1)
	{
		return object_0 != object_1;
	}

	internal static void smethod_36(Behaviour behaviour_0, bool bool_0)
	{
		behaviour_0.enabled = bool_0;
	}

	internal static void smethod_37(RawImage rawImage_0, Texture texture_0)
	{
		rawImage_0.texture = texture_0;
	}
}
