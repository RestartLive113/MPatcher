using System;
using System.Collections.Generic;
using System.Reflection;
using System.Runtime.Serialization;
using System.Text;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.Events;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

namespace RuntimeInspectorNamespace;

public static class RuntimeInspectorUtils
{
	private static readonly Dictionary<Type, MemberInfo[]> aooc_luBWAdup_0024JkhE9j6Xg = new Dictionary<Type, MemberInfo[]>(89) { 
	{
		smethod_22(typeof(object).TypeHandle),
		null
	} };

	private static readonly Dictionary<Type, ExposedMethod[]> U_0024i77w2SjHrlVAZMa7fq_9S7zWI42zm6gzEVav1PP7Fr = new Dictionary<Type, ExposedMethod[]>(89);

	private static readonly HashSet<Type> q55ibINekdAf7pQ2ghWWdMQViA2HvHqB5qRG0Hqa2tlJ = new HashSet<Type>
	{
		smethod_22(typeof(string).TypeHandle),
		smethod_22(typeof(Vector4).TypeHandle),
		smethod_22(typeof(Vector3).TypeHandle),
		smethod_22(typeof(Vector2).TypeHandle),
		smethod_22(typeof(Rect).TypeHandle),
		smethod_22(typeof(Quaternion).TypeHandle),
		smethod_22(typeof(Color).TypeHandle),
		smethod_22(typeof(Color32).TypeHandle),
		smethod_22(typeof(LayerMask).TypeHandle),
		smethod_22(typeof(Bounds).TypeHandle),
		smethod_22(typeof(Matrix4x4).TypeHandle),
		smethod_22(typeof(AnimationCurve).TypeHandle),
		smethod_22(typeof(Gradient).TypeHandle),
		smethod_22(typeof(RectOffset).TypeHandle),
		smethod_22(typeof(GUIStyle).TypeHandle),
		smethod_22(typeof(bool[]).TypeHandle),
		smethod_22(typeof(byte[]).TypeHandle),
		smethod_22(typeof(sbyte[]).TypeHandle),
		smethod_22(typeof(char[]).TypeHandle),
		smethod_22(typeof(decimal[]).TypeHandle),
		smethod_22(typeof(double[]).TypeHandle),
		smethod_22(typeof(float[]).TypeHandle),
		smethod_22(typeof(int[]).TypeHandle),
		smethod_22(typeof(uint[]).TypeHandle),
		smethod_22(typeof(long[]).TypeHandle),
		smethod_22(typeof(ulong[]).TypeHandle),
		smethod_22(typeof(short[]).TypeHandle),
		smethod_22(typeof(ushort[]).TypeHandle),
		smethod_22(typeof(string[]).TypeHandle),
		smethod_22(typeof(Vector4[]).TypeHandle),
		smethod_22(typeof(Vector3[]).TypeHandle),
		smethod_22(typeof(Vector2[]).TypeHandle),
		smethod_22(typeof(Rect[]).TypeHandle),
		smethod_22(typeof(Quaternion[]).TypeHandle),
		smethod_22(typeof(Color[]).TypeHandle),
		smethod_22(typeof(Color32[]).TypeHandle),
		smethod_22(typeof(LayerMask[]).TypeHandle),
		smethod_22(typeof(Bounds[]).TypeHandle),
		smethod_22(typeof(Matrix4x4[]).TypeHandle),
		smethod_22(typeof(AnimationCurve[]).TypeHandle),
		smethod_22(typeof(Gradient[]).TypeHandle),
		smethod_22(typeof(RectOffset[]).TypeHandle),
		smethod_22(typeof(GUIStyle[]).TypeHandle),
		smethod_22(typeof(List<bool>).TypeHandle),
		smethod_22(typeof(List<byte>).TypeHandle),
		smethod_22(typeof(List<sbyte>).TypeHandle),
		smethod_22(typeof(List<char>).TypeHandle),
		smethod_22(typeof(List<decimal>).TypeHandle),
		smethod_22(typeof(List<double>).TypeHandle),
		smethod_22(typeof(List<float>).TypeHandle),
		smethod_22(typeof(List<int>).TypeHandle),
		smethod_22(typeof(List<uint>).TypeHandle),
		smethod_22(typeof(List<long>).TypeHandle),
		smethod_22(typeof(List<ulong>).TypeHandle),
		smethod_22(typeof(List<short>).TypeHandle),
		smethod_22(typeof(List<ushort>).TypeHandle),
		smethod_22(typeof(List<string>).TypeHandle),
		smethod_22(typeof(List<Vector4>).TypeHandle),
		smethod_22(typeof(List<Vector3>).TypeHandle),
		smethod_22(typeof(List<Vector2>).TypeHandle),
		smethod_22(typeof(List<Rect>).TypeHandle),
		smethod_22(typeof(List<Quaternion>).TypeHandle),
		smethod_22(typeof(List<Color>).TypeHandle),
		smethod_22(typeof(List<Color32>).TypeHandle),
		smethod_22(typeof(List<LayerMask>).TypeHandle),
		smethod_22(typeof(List<Bounds>).TypeHandle),
		smethod_22(typeof(List<Matrix4x4>).TypeHandle),
		smethod_22(typeof(List<AnimationCurve>).TypeHandle),
		smethod_22(typeof(List<Gradient>).TypeHandle),
		smethod_22(typeof(List<RectOffset>).TypeHandle),
		smethod_22(typeof(List<GUIStyle>).TypeHandle)
	};

	private static readonly List<MemberInfo> qbJexGsj7IMsLd14wXCmPV1hnbugCPQHwcGdmP4HLxFY = new List<MemberInfo>(32);

	private static readonly List<Type> jELua6WP0pmSe4bZCK8oewu_SnSWCO2UJ_Mtp7BlhUC4 = new List<Type>(8);

	private static readonly List<string> TdJnO4nrPEGG_0024LCYZkF5AEjT6yk5saY7_yjra359tWA2 = new List<string>(32);

	private static readonly List<ExposedMethod> UGs3nYR2vKhY_00247ZTdYVq3MPJ4u73bGLUuF2iKEx4U7ir = new List<ExposedMethod>(4);

	private static readonly List<ExposedExtensionMethodHolder> R1HU4XPSIR6_lCGz295QH1LKVP1H3XgUQ0jjZf1IbQrG = new List<ExposedExtensionMethodHolder>();

	public static readonly HashSet<Transform> IgnoredTransformsInHierarchy = new HashSet<Transform>();

	private static Canvas canvas_0 = null;

	private static Canvas VxEvsruDch93h63E6cpngU_0024KllMSo4gw6qAPDb9dy__1 = null;

	private static Tooltip bRbFFF6LCbJmP_M7lokItRE;

	private static readonly Stack<DraggedReferenceItem> v0OmvUgTJ62IXL6ud0Mc3eJ8pSfItbZjx4UUO6_0024OYNam = new Stack<DraggedReferenceItem>();

	internal static readonly StringBuilder OrPIIOQlqp5UvEq_w10vC58 = smethod_90(200);

	public static Type ExposedExtensionMethodsHolder
	{
		set
		{
			HekA4PNA_RGk4YrxgpooZx5NDmDT4pSS7hF2_0024UIJXZd_0024(value);
		}
	}

	public static bool IsNull(this object obj)
	{
		if (obj is UnityEngine.Object)
		{
			if (obj == null)
			{
				return true;
			}
			return smethod_0(obj, (object)null);
		}
		return obj == null;
	}

	public static string ToTitleCase(this string str)
	{
		if (str != null && smethod_1(str) != 0)
		{
			byte b = 1;
			int i = 0;
			if (smethod_1(str) > 1 && smethod_2(str, 1) == '_')
			{
				i = 2;
			}
			smethod_3(OrPIIOQlqp5UvEq_w10vC58, 0);
			for (; i < smethod_1(str); i++)
			{
				char c = smethod_2(str, i);
				if (char.IsUpper(c))
				{
					if ((b < 2 || (smethod_1(str) > i + 1 && char.IsLower(smethod_2(str, i + 1)))) && smethod_4(OrPIIOQlqp5UvEq_w10vC58) > 0)
					{
						smethod_5(OrPIIOQlqp5UvEq_w10vC58, ' ');
					}
					smethod_5(OrPIIOQlqp5UvEq_w10vC58, c);
					b = 3;
					continue;
				}
				if (c == '_')
				{
					b = 1;
					continue;
				}
				if (char.IsNumber(c))
				{
					if (b != 2 && smethod_4(OrPIIOQlqp5UvEq_w10vC58) > 0)
					{
						smethod_5(OrPIIOQlqp5UvEq_w10vC58, ' ');
					}
					smethod_5(OrPIIOQlqp5UvEq_w10vC58, c);
					b = 2;
					continue;
				}
				if (b == 1 || b == 2)
				{
					if (smethod_4(OrPIIOQlqp5UvEq_w10vC58) > 0)
					{
						smethod_5(OrPIIOQlqp5UvEq_w10vC58, ' ');
					}
					smethod_5(OrPIIOQlqp5UvEq_w10vC58, char.ToUpper(c));
				}
				else
				{
					smethod_5(OrPIIOQlqp5UvEq_w10vC58, c);
				}
				b = 0;
			}
			if (smethod_4(OrPIIOQlqp5UvEq_w10vC58) != 0)
			{
				return smethod_6((object)OrPIIOQlqp5UvEq_w10vC58);
			}
			return str;
		}
		return string.Empty;
	}

	public static string GetName(this UnityEngine.Object obj)
	{
		if (!smethod_7(obj))
		{
			return global::_003CModule_003E.smethod_29<string>(499090203u);
		}
		return smethod_8(obj);
	}

	public static string GetNameWithType(this object obj, Type defaultType = null)
	{
		if (obj.IsNull())
		{
			if (defaultType == null)
			{
				return global::_003CModule_003E.smethod_28<string>(3305839467u);
			}
			return smethod_10(global::_003CModule_003E.smethod_25<string>(1216526446u), smethod_9((MemberInfo)defaultType), global::_003CModule_003E.smethod_27<string>(1583484277u));
		}
		if (obj is UnityEngine.Object)
		{
			return smethod_12(smethod_8((UnityEngine.Object)obj), global::_003CModule_003E.smethod_29<string>(358433514u), smethod_9((MemberInfo)smethod_11(obj)), global::_003CModule_003E.smethod_29<string>(2132683108u));
		}
		return smethod_9((MemberInfo)smethod_11(obj));
	}

	public static Texture GetTexture(this UnityEngine.Object obj)
	{
		if (smethod_7(obj))
		{
			if (obj is Texture)
			{
				return (Texture)obj;
			}
			if (obj is Sprite)
			{
				return smethod_13((Sprite)obj);
			}
		}
		return null;
	}

	public static Color Tint(this Color color, float tintAmount)
	{
		if (color.r + color.g + color.b > 1.5f)
		{
			color.r -= tintAmount;
			color.g -= tintAmount;
			color.b -= tintAmount;
		}
		else
		{
			color.r += tintAmount;
			color.g += tintAmount;
			color.b += tintAmount;
		}
		return color;
	}

	public static void ShowTooltip(string tooltip, PointerEventData pointer, UISkin skin = null, Canvas referenceCanvas = null)
	{
		bool flag = WDWuJhdkDWe9z_0024Asi1GtjeXCVjYXjj5lpDdr7K0yKpRU(referenceCanvas);
		if (smethod_7((UnityEngine.Object)bRbFFF6LCbJmP_M7lokItRE))
		{
			smethod_16(smethod_15((Component)bRbFFF6LCbJmP_M7lokItRE), bool_0: true);
		}
		else
		{
			bRbFFF6LCbJmP_M7lokItRE = UnityEngine.Object.Instantiate(awf1opR73mv9LSqQ84LlTsI.cqWoMNveroNrLO3XzL3B_0024XA<Tooltip>(global::_003CModule_003E.smethod_28<string>(1105249894u)), smethod_14((Component)canvas_0), worldPositionStays: false);
			flag = true;
		}
		if (flag)
		{
			bRbFFF6LCbJmP_M7lokItRE.Initialize(canvas_0);
		}
		if (smethod_7((UnityEngine.Object)skin))
		{
			bRbFFF6LCbJmP_M7lokItRE.Skin = skin;
		}
		bRbFFF6LCbJmP_M7lokItRE.SetContent(tooltip, pointer);
	}

	public static void HideTooltip()
	{
		if (smethod_7((UnityEngine.Object)bRbFFF6LCbJmP_M7lokItRE) && smethod_17(smethod_15((Component)bRbFFF6LCbJmP_M7lokItRE)))
		{
			smethod_16(smethod_15((Component)bRbFFF6LCbJmP_M7lokItRE), bool_0: false);
		}
	}

	public static DraggedReferenceItem CreateDraggedReferenceItem(UnityEngine.Object reference, PointerEventData draggingPointer, UISkin skin = null, Canvas referenceCanvas = null)
	{
		bool flag = WDWuJhdkDWe9z_0024Asi1GtjeXCVjYXjj5lpDdr7K0yKpRU(referenceCanvas);
		DraggedReferenceItem draggedReferenceItem;
		if (v0OmvUgTJ62IXL6ud0Mc3eJ8pSfItbZjx4UUO6_0024OYNam.Count > 0)
		{
			draggedReferenceItem = v0OmvUgTJ62IXL6ud0Mc3eJ8pSfItbZjx4UUO6_0024OYNam.Pop();
			smethod_16(smethod_15((Component)draggedReferenceItem), bool_0: true);
		}
		else
		{
			draggedReferenceItem = UnityEngine.Object.Instantiate(awf1opR73mv9LSqQ84LlTsI.cqWoMNveroNrLO3XzL3B_0024XA<DraggedReferenceItem>(global::_003CModule_003E.smethod_29<string>(3484962635u)), smethod_14((Component)canvas_0), worldPositionStays: false);
			flag = true;
		}
		if (flag)
		{
			draggedReferenceItem.Initialize(canvas_0);
		}
		if (smethod_7((UnityEngine.Object)skin))
		{
			draggedReferenceItem.Skin = skin;
		}
		draggedReferenceItem.SetContent(reference, draggingPointer);
		smethod_18(draggingPointer, bool_0: true);
		smethod_19(draggingPointer, bool_0: false);
		return draggedReferenceItem;
	}

	public static void PoolDraggedReferenceItem(DraggedReferenceItem item)
	{
		smethod_16(smethod_15((Component)item), bool_0: false);
		v0OmvUgTJ62IXL6ud0Mc3eJ8pSfItbZjx4UUO6_0024OYNam.Push(item);
	}

	public static UnityEngine.Object GetAssignableObjectFromDraggedReferenceItem(PointerEventData draggingPointer, Type assignableType)
	{
		if (!smethod_7((UnityEngine.Object)smethod_20(draggingPointer)))
		{
			return null;
		}
		DraggedReferenceItem component = smethod_20(draggingPointer).GetComponent<DraggedReferenceItem>();
		if (smethod_7((UnityEngine.Object)component) && smethod_7(component.Reference))
		{
			if (smethod_21(assignableType, smethod_11((object)component.Reference)))
			{
				return component.Reference;
			}
			if (!smethod_21(smethod_22(typeof(Component).TypeHandle), assignableType))
			{
				if (smethod_21(smethod_22(typeof(GameObject).TypeHandle), assignableType) && component.Reference is Component)
				{
					return smethod_15((Component)component.Reference);
				}
			}
			else
			{
				UnityEngine.Object obj = null;
				if (!(component.Reference is Component))
				{
					if (component.Reference is GameObject)
					{
						obj = smethod_24((GameObject)component.Reference, assignableType);
					}
				}
				else
				{
					obj = smethod_23((Component)component.Reference, assignableType);
				}
				if (smethod_7(obj))
				{
					return obj;
				}
			}
		}
		return null;
	}

	public static void CopyValuesFrom(this Canvas canvas, Canvas referenceCanvas)
	{
		if (!smethod_7((UnityEngine.Object)canvas) || !smethod_7((UnityEngine.Object)referenceCanvas))
		{
			return;
		}
		smethod_26(canvas, smethod_25(referenceCanvas));
		smethod_28(canvas, smethod_27(referenceCanvas));
		smethod_30(canvas, smethod_29(referenceCanvas));
		smethod_32(canvas, smethod_31(referenceCanvas));
		switch (smethod_27(referenceCanvas))
		{
		case RenderMode.WorldSpace:
		{
			smethod_34(canvas, smethod_33(referenceCanvas));
			RectTransform rectTransform = (RectTransform)smethod_14((Component)referenceCanvas);
			Vector3 position2;
			if (!(smethod_37(rectTransform) == new Vector2(0.5f, 0.5f)))
			{
				Rect rect = rectTransform.rect;
				Vector3 position = new Vector3((0.5f - rectTransform.pivot.x) * rect.width, (0.5f - rectTransform.pivot.y) * rect.height, 0f);
				position2 = rectTransform.TransformPoint(position);
			}
			else
			{
				position2 = rectTransform.position;
			}
			canvas.transform.SetPositionAndRotation(position2, rectTransform.rotation);
			canvas.transform.localScale = rectTransform.localScale;
			break;
		}
		case RenderMode.ScreenSpaceCamera:
			smethod_34(canvas, smethod_33(referenceCanvas));
			smethod_36(canvas, smethod_35(referenceCanvas) * 0.75f);
			break;
		}
		CanvasScaler component = canvas.GetComponent<CanvasScaler>();
		CanvasScaler component2 = referenceCanvas.GetComponent<CanvasScaler>();
		if (!component || !component2)
		{
			return;
		}
		component.referencePixelsPerUnit = component2.referencePixelsPerUnit;
		if (referenceCanvas.renderMode == RenderMode.WorldSpace)
		{
			component.dynamicPixelsPerUnit = component2.dynamicPixelsPerUnit;
			return;
		}
		component.uiScaleMode = component2.uiScaleMode;
		switch (component2.uiScaleMode)
		{
		case CanvasScaler.ScaleMode.ConstantPixelSize:
			component.scaleFactor = component2.scaleFactor;
			break;
		case CanvasScaler.ScaleMode.ScaleWithScreenSize:
			component.referenceResolution = component2.referenceResolution;
			component.screenMatchMode = component2.screenMatchMode;
			component.matchWidthOrHeight = component2.matchWidthOrHeight;
			break;
		case CanvasScaler.ScaleMode.ConstantPhysicalSize:
			component.physicalUnit = component2.physicalUnit;
			component.fallbackScreenDPI = component2.fallbackScreenDPI;
			component.defaultSpriteDPI = component2.defaultSpriteDPI;
			break;
		}
	}

	private static bool WDWuJhdkDWe9z_0024Asi1GtjeXCVjYXjj5lpDdr7K0yKpRU(Canvas canvas_1)
	{
		bool flag = !smethod_7((UnityEngine.Object)canvas_0);
		if (!smethod_7((UnityEngine.Object)canvas_0))
		{
			canvas_0 = smethod_38(global::_003CModule_003E.smethod_25<string>(1438978269u)).AddComponent<Canvas>();
			smethod_15((Component)canvas_0).AddComponent<CanvasScaler>();
			if (!smethod_7((UnityEngine.Object)canvas_1))
			{
				smethod_28(canvas_0, RenderMode.ScreenSpaceOverlay);
				smethod_32(canvas_0, 987654);
			}
			smethod_39((UnityAction<Scene, LoadSceneMode>)delegate
			{
				if (smethod_7((UnityEngine.Object)canvas_0))
				{
					Transform transform_ = smethod_14((Component)canvas_0);
					for (int num = smethod_43(transform_) - 1; num >= 0; num--)
					{
						smethod_45((UnityEngine.Object)smethod_15((Component)smethod_44(transform_, num)));
					}
				}
			});
			smethod_40((UnityAction<Scene, LoadSceneMode>)delegate
			{
				if (smethod_7((UnityEngine.Object)canvas_0))
				{
					Transform transform_ = smethod_14((Component)canvas_0);
					for (int num = smethod_43(transform_) - 1; num >= 0; num--)
					{
						smethod_45((UnityEngine.Object)smethod_15((Component)smethod_44(transform_, num)));
					}
				}
			});
			smethod_41((UnityEngine.Object)smethod_15((Component)canvas_0));
			IgnoredTransformsInHierarchy.Add(smethod_14((Component)canvas_0));
		}
		if (smethod_7((UnityEngine.Object)canvas_1) && smethod_42((UnityEngine.Object)canvas_1, (UnityEngine.Object)VxEvsruDch93h63E6cpngU_0024KllMSo4gw6qAPDb9dy__1))
		{
			VxEvsruDch93h63E6cpngU_0024KllMSo4gw6qAPDb9dy__1 = canvas_1;
			canvas_0.CopyValuesFrom(canvas_1);
			smethod_32(canvas_0, Mathf.Max(987654, smethod_31(canvas_1) + 100));
			flag = true;
		}
		if (flag)
		{
			smethod_16(smethod_15((Component)canvas_0), bool_0: false);
			smethod_16(smethod_15((Component)canvas_0), bool_0: true);
		}
		return flag;
	}

	private static void Pmq0aRxp9fUcuOl3_0024LDARC0(Scene scene_0, LoadSceneMode loadSceneMode_0)
	{
		if (smethod_7((UnityEngine.Object)canvas_0))
		{
			Transform transform_ = smethod_14((Component)canvas_0);
			for (int num = smethod_43(transform_) - 1; num >= 0; num--)
			{
				smethod_45((UnityEngine.Object)smethod_15((Component)smethod_44(transform_, num)));
			}
		}
	}

	public static bool IsPointerValid(this PointerEventData eventData)
	{
		int num = smethod_46() - 1;
		while (true)
		{
			if (num >= 0)
			{
				if (smethod_47(num).fingerId == eventData.pointerId)
				{
					break;
				}
				num--;
				continue;
			}
			return Input.GetMouseButton((int)eventData.button);
		}
		return true;
	}

	public static MemberInfo[] GetAllVariables(this Type type)
	{
		if (aooc_luBWAdup_0024JkhE9j6Xg.TryGetValue(type, out var value))
		{
			return value;
		}
		qbJexGsj7IMsLd14wXCmPV1hnbugCPQHwcGdmP4HLxFY.Clear();
		jELua6WP0pmSe4bZCK8oewu_SnSWCO2UJ_Mtp7BlhUC4.Clear();
		Type type2 = type;
		while (type2 != smethod_22(typeof(object).TypeHandle))
		{
			if (!aooc_luBWAdup_0024JkhE9j6Xg.TryGetValue(type2, out value))
			{
				jELua6WP0pmSe4bZCK8oewu_SnSWCO2UJ_Mtp7BlhUC4.Add(type2);
				type2 = smethod_48(type2);
				continue;
			}
			if (value != null)
			{
				qbJexGsj7IMsLd14wXCmPV1hnbugCPQHwcGdmP4HLxFY.AddRange(value);
			}
			break;
		}
		for (int num = jELua6WP0pmSe4bZCK8oewu_SnSWCO2UJ_Mtp7BlhUC4.Count - 1; num >= 0; num--)
		{
			type2 = jELua6WP0pmSe4bZCK8oewu_SnSWCO2UJ_Mtp7BlhUC4[num];
			TdJnO4nrPEGG_0024LCYZkF5AEjT6yk5saY7_yjra359tWA2.Clear();
			PropertyInfo[] array = smethod_49(type2, BindingFlags.DeclaredOnly | BindingFlags.Instance | BindingFlags.Public | BindingFlags.NonPublic);
			foreach (PropertyInfo propertyInfo in array)
			{
				MethodInfo methodInfo = smethod_50(propertyInfo, bool_0: true);
				if (methodInfo != null && smethod_51(propertyInfo, bool_0: true) != null && smethod_52(propertyInfo).Length == 0 && smethod_53(propertyInfo).bbRMCk3btOlDId4wv6VKUvg() && !propertyInfo.HasAttribute<ObsoleteAttribute>() && !propertyInfo.HasAttribute<NonSerializedAttribute>() && !propertyInfo.HasAttribute<HideInInspector>() && smethod_55((MemberInfo)smethod_54(methodInfo)) == smethod_55((MemberInfo)methodInfo))
				{
					TdJnO4nrPEGG_0024LCYZkF5AEjT6yk5saY7_yjra359tWA2.Add(smethod_9((MemberInfo)propertyInfo));
					qbJexGsj7IMsLd14wXCmPV1hnbugCPQHwcGdmP4HLxFY.Add(propertyInfo);
				}
			}
			FieldInfo[] array2 = smethod_56(type2, BindingFlags.DeclaredOnly | BindingFlags.Instance | BindingFlags.Public | BindingFlags.NonPublic);
			foreach (FieldInfo fieldInfo in array2)
			{
				if (smethod_57(fieldInfo) || smethod_58(fieldInfo) || !smethod_59(fieldInfo).bbRMCk3btOlDId4wv6VKUvg() || fieldInfo.HasAttribute<ObsoleteAttribute>() || fieldInfo.HasAttribute<NonSerializedAttribute>() || fieldInfo.HasAttribute<HideInInspector>())
				{
					continue;
				}
				string string_ = smethod_9((MemberInfo)fieldInfo);
				if (smethod_60(string_, global::_003CModule_003E.smethod_26<string>(560344062u)))
				{
					continue;
				}
				int num2 = 0;
				if (smethod_1(string_) > 1)
				{
					if (smethod_1(string_) > 2 && smethod_2(string_, 1) == '_')
					{
						num2 = 2;
					}
					else if (smethod_2(string_, 0) == '_')
					{
						num2 = 1;
					}
				}
				bool flag = false;
				for (int num3 = TdJnO4nrPEGG_0024LCYZkF5AEjT6yk5saY7_yjra359tWA2.Count - 1; num3 >= 0; num3--)
				{
					string string_2 = TdJnO4nrPEGG_0024LCYZkF5AEjT6yk5saY7_yjra359tWA2[num3];
					if (smethod_1(string_) - num2 == smethod_1(string_2))
					{
						int num4 = smethod_2(string_, num2);
						int num5 = smethod_2(string_2, 0);
						if (num4 == num5 || num4 + 32 == num5 || num4 - 32 == num5)
						{
							int k;
							for (k = 1; k < smethod_1(string_2) && smethod_2(string_, num2 + k) == smethod_2(string_2, k); k++)
							{
							}
							if (k == smethod_1(string_2))
							{
								flag = true;
								break;
							}
						}
					}
				}
				if (!flag)
				{
					qbJexGsj7IMsLd14wXCmPV1hnbugCPQHwcGdmP4HLxFY.Add(fieldInfo);
				}
			}
			value = ((qbJexGsj7IMsLd14wXCmPV1hnbugCPQHwcGdmP4HLxFY.Count > 0) ? qbJexGsj7IMsLd14wXCmPV1hnbugCPQHwcGdmP4HLxFY.ToArray() : null);
			aooc_luBWAdup_0024JkhE9j6Xg[type2] = value;
		}
		return value;
	}

	public static ExposedMethod[] GetExposedMethods(this Type type)
	{
		if (!U_0024i77w2SjHrlVAZMa7fq_9S7zWI42zm6gzEVav1PP7Fr.TryGetValue(type, out var value))
		{
			MethodInfo[] array = smethod_61(type, BindingFlags.Instance | BindingFlags.Static | BindingFlags.Public | BindingFlags.NonPublic);
			UGs3nYR2vKhY_00247ZTdYVq3MPJ4u73bGLUuF2iKEx4U7ir.Clear();
			for (int i = 0; i < array.Length; i++)
			{
				if (array[i].HasAttribute<RuntimeInspectorButtonAttribute>() && smethod_62((MethodBase)array[i]).Length == 0)
				{
					RuntimeInspectorButtonAttribute attribute = array[i].GetAttribute<RuntimeInspectorButtonAttribute>();
					if (!attribute.IsInitializer || smethod_21(type, smethod_63(array[i])))
					{
						UGs3nYR2vKhY_00247ZTdYVq3MPJ4u73bGLUuF2iKEx4U7ir.Add(new ExposedMethod(array[i], attribute, bool_0: false));
					}
				}
			}
			for (int j = 0; j < R1HU4XPSIR6_lCGz295QH1LKVP1H3XgUQ0jjZf1IbQrG.Count; j++)
			{
				ExposedExtensionMethodHolder exposedExtensionMethodHolder = R1HU4XPSIR6_lCGz295QH1LKVP1H3XgUQ0jjZf1IbQrG[j];
				if (smethod_21(exposedExtensionMethodHolder.extendedType, type))
				{
					UGs3nYR2vKhY_00247ZTdYVq3MPJ4u73bGLUuF2iKEx4U7ir.Add(new ExposedMethod(exposedExtensionMethodHolder.method, exposedExtensionMethodHolder.properties, bool_0: true));
				}
			}
			value = ((UGs3nYR2vKhY_00247ZTdYVq3MPJ4u73bGLUuF2iKEx4U7ir.Count <= 0) ? null : UGs3nYR2vKhY_00247ZTdYVq3MPJ4u73bGLUuF2iKEx4U7ir.ToArray());
			U_0024i77w2SjHrlVAZMa7fq_9S7zWI42zm6gzEVav1PP7Fr[type] = value;
		}
		return value;
	}

	private static bool bbRMCk3btOlDId4wv6VKUvg(this Type type_0)
	{
		if (!smethod_64(type_0) && !q55ibINekdAf7pQ2ghWWdMQViA2HvHqB5qRG0Hqa2tlJ.Contains(type_0) && !smethod_65(type_0))
		{
			if (smethod_21(smethod_22(typeof(UnityEngine.Object).TypeHandle), type_0))
			{
				return true;
			}
			if (smethod_66(type_0))
			{
				if (smethod_67(type_0) == 1)
				{
					return smethod_68(type_0).bbRMCk3btOlDId4wv6VKUvg();
				}
				return false;
			}
			if (!smethod_69(type_0))
			{
				if (smethod_72((MemberInfo)type_0, smethod_22(typeof(SerializableAttribute).TypeHandle), bool_0: false))
				{
					return true;
				}
				return false;
			}
			if (smethod_70(type_0) == smethod_22(typeof(List<>).TypeHandle))
			{
				return smethod_71(type_0)[0].bbRMCk3btOlDId4wv6VKUvg();
			}
			return false;
		}
		return true;
	}

	public static bool HasAttribute<T>(this MemberInfo variable) where T : Attribute
	{
		return smethod_72(variable, smethod_22(typeof(T).TypeHandle), bool_0: true);
	}

	public static T GetAttribute<T>(this MemberInfo variable) where T : Attribute
	{
		return (T)smethod_73(variable, smethod_22(typeof(T).TypeHandle), bool_0: true);
	}

	public static T[] GetAttributes<T>(this MemberInfo variable) where T : Attribute
	{
		return (T[])smethod_74(variable, smethod_22(typeof(T).TypeHandle), bool_0: true);
	}

	public static object Instantiate(this Type type)
	{
		try
		{
			if (smethod_21(smethod_22(typeof(ScriptableObject).TypeHandle), type))
			{
				return smethod_75(type);
			}
			if (smethod_76(type, BindingFlags.Instance | BindingFlags.Public | BindingFlags.NonPublic, (Binder)null, Type.EmptyTypes, (ParameterModifier[])null) == null)
			{
				return smethod_78(type);
			}
			return smethod_77(type, bool_0: true);
		}
		catch
		{
			return null;
		}
	}

	public static Type GetType(string typeName)
	{
		try
		{
			Type type = smethod_79(typeName);
			if (type != null)
			{
				return type;
			}
			if (smethod_60(typeName, global::_003CModule_003E.smethod_26<string>(2366228505u)))
			{
				Assembly assembly = smethod_82(smethod_81(typeName, 0, smethod_80(typeName, '.')));
				if (assembly == null)
				{
					return null;
				}
				type = smethod_83(assembly, typeName);
				if (type != null)
				{
					return type;
				}
			}
			else
			{
				type = smethod_83(smethod_82(global::_003CModule_003E.smethod_28<string>(1378429597u)), smethod_84(global::_003CModule_003E.smethod_29<string>(1894554393u), typeName));
				if (type != null)
				{
					return type;
				}
			}
			Assembly[] array = smethod_86(smethod_85());
			for (int i = 0; i < array.Length; i++)
			{
				Type[] array2 = smethod_87(array[i]);
				foreach (Type type2 in array2)
				{
					if (smethod_88(smethod_9((MemberInfo)type2), typeName))
					{
						return type2;
					}
				}
			}
		}
		catch
		{
		}
		return null;
	}

	private static void HekA4PNA_RGk4YrxgpooZx5NDmDT4pSS7hF2_0024UIJXZd_0024(Type type_0)
	{
		R1HU4XPSIR6_lCGz295QH1LKVP1H3XgUQ0jjZf1IbQrG.Clear();
		U_0024i77w2SjHrlVAZMa7fq_9S7zWI42zm6gzEVav1PP7Fr.Clear();
		MethodInfo[] array = smethod_61(type_0, BindingFlags.Static | BindingFlags.Public | BindingFlags.NonPublic);
		for (int i = 0; i < array.Length; i++)
		{
			if (!array[i].HasAttribute<RuntimeInspectorButtonAttribute>())
			{
				continue;
			}
			ParameterInfo[] array2 = smethod_62((MethodBase)array[i]);
			if (array2.Length == 1)
			{
				RuntimeInspectorButtonAttribute attribute = array[i].GetAttribute<RuntimeInspectorButtonAttribute>();
				Type type_1 = smethod_89(array2[0]);
				if (!attribute.IsInitializer || smethod_21(type_1, smethod_63(array[i])))
				{
					R1HU4XPSIR6_lCGz295QH1LKVP1H3XgUQ0jjZf1IbQrG.Add(new ExposedExtensionMethodHolder(type_1, array[i], attribute));
				}
			}
		}
	}

	internal static bool smethod_0(object object_0, object object_1)
	{
		return object_0.Equals(object_1);
	}

	internal static int smethod_1(string string_0)
	{
		return string_0.Length;
	}

	internal static char smethod_2(string string_0, int int_0)
	{
		return string_0[int_0];
	}

	internal static void smethod_3(StringBuilder stringBuilder_0, int int_0)
	{
		stringBuilder_0.Length = int_0;
	}

	internal static int smethod_4(StringBuilder stringBuilder_0)
	{
		return stringBuilder_0.Length;
	}

	internal static StringBuilder smethod_5(StringBuilder stringBuilder_0, char char_0)
	{
		return stringBuilder_0.Append(char_0);
	}

	internal static string smethod_6(object object_0)
	{
		return object_0.ToString();
	}

	internal static bool smethod_7(UnityEngine.Object object_0)
	{
		return object_0;
	}

	internal static string smethod_8(UnityEngine.Object object_0)
	{
		return object_0.name;
	}

	internal static string smethod_9(MemberInfo memberInfo_0)
	{
		return memberInfo_0.Name;
	}

	internal static string smethod_10(string string_0, string string_1, string string_2)
	{
		return string_0 + string_1 + string_2;
	}

	internal static Type smethod_11(object object_0)
	{
		return object_0.GetType();
	}

	internal static string smethod_12(string string_0, string string_1, string string_2, string string_3)
	{
		return string_0 + string_1 + string_2 + string_3;
	}

	internal static Texture2D smethod_13(Sprite sprite_0)
	{
		return sprite_0.texture;
	}

	internal static Transform smethod_14(Component component_0)
	{
		return component_0.transform;
	}

	internal static GameObject smethod_15(Component component_0)
	{
		return component_0.gameObject;
	}

	internal static void smethod_16(GameObject gameObject_0, bool bool_0)
	{
		gameObject_0.SetActive(bool_0);
	}

	internal static bool smethod_17(GameObject gameObject_0)
	{
		return gameObject_0.activeSelf;
	}

	internal static void smethod_18(PointerEventData pointerEventData_0, bool bool_0)
	{
		pointerEventData_0.dragging = bool_0;
	}

	internal static void smethod_19(PointerEventData pointerEventData_0, bool bool_0)
	{
		pointerEventData_0.eligibleForClick = bool_0;
	}

	internal static GameObject smethod_20(PointerEventData pointerEventData_0)
	{
		return pointerEventData_0.pointerDrag;
	}

	internal static bool smethod_21(Type type_0, Type type_1)
	{
		return type_0.IsAssignableFrom(type_1);
	}

	internal static Type smethod_22(RuntimeTypeHandle runtimeTypeHandle_0)
	{
		return Type.GetTypeFromHandle(runtimeTypeHandle_0);
	}

	internal static Component smethod_23(Component component_0, Type type_0)
	{
		return component_0.GetComponent(type_0);
	}

	internal static Component smethod_24(GameObject gameObject_0, Type type_0)
	{
		return gameObject_0.GetComponent(type_0);
	}

	internal static bool smethod_25(Canvas canvas_1)
	{
		return canvas_1.pixelPerfect;
	}

	internal static void smethod_26(Canvas canvas_1, bool bool_0)
	{
		canvas_1.pixelPerfect = bool_0;
	}

	internal static RenderMode smethod_27(Canvas canvas_1)
	{
		return canvas_1.renderMode;
	}

	internal static void smethod_28(Canvas canvas_1, RenderMode renderMode_0)
	{
		canvas_1.renderMode = renderMode_0;
	}

	internal static int smethod_29(Canvas canvas_1)
	{
		return canvas_1.sortingLayerID;
	}

	internal static void smethod_30(Canvas canvas_1, int int_0)
	{
		canvas_1.sortingLayerID = int_0;
	}

	internal static int smethod_31(Canvas canvas_1)
	{
		return canvas_1.sortingOrder;
	}

	internal static void smethod_32(Canvas canvas_1, int int_0)
	{
		canvas_1.sortingOrder = int_0;
	}

	internal static Camera smethod_33(Canvas canvas_1)
	{
		return canvas_1.worldCamera;
	}

	internal static void smethod_34(Canvas canvas_1, Camera camera_0)
	{
		canvas_1.worldCamera = camera_0;
	}

	internal static float smethod_35(Canvas canvas_1)
	{
		return canvas_1.planeDistance;
	}

	internal static void smethod_36(Canvas canvas_1, float float_0)
	{
		canvas_1.planeDistance = float_0;
	}

	internal static Vector2 smethod_37(RectTransform rectTransform_0)
	{
		return rectTransform_0.pivot;
	}

	internal static GameObject smethod_38(string string_0)
	{
		return new GameObject(string_0);
	}

	internal static void smethod_39(UnityAction<Scene, LoadSceneMode> unityAction_0)
	{
		SceneManager.sceneLoaded -= unityAction_0;
	}

	internal static void smethod_40(UnityAction<Scene, LoadSceneMode> unityAction_0)
	{
		SceneManager.sceneLoaded += unityAction_0;
	}

	internal static void smethod_41(UnityEngine.Object object_0)
	{
		UnityEngine.Object.DontDestroyOnLoad(object_0);
	}

	internal static bool smethod_42(UnityEngine.Object object_0, UnityEngine.Object object_1)
	{
		return object_0 != object_1;
	}

	internal static int smethod_43(Transform transform_0)
	{
		return transform_0.childCount;
	}

	internal static Transform smethod_44(Transform transform_0, int int_0)
	{
		return transform_0.GetChild(int_0);
	}

	internal static void smethod_45(UnityEngine.Object object_0)
	{
		UnityEngine.Object.Destroy(object_0);
	}

	internal static int smethod_46()
	{
		return Input.touchCount;
	}

	internal static Touch smethod_47(int int_0)
	{
		return Input.GetTouch(int_0);
	}

	internal static Type smethod_48(Type type_0)
	{
		return type_0.BaseType;
	}

	internal static PropertyInfo[] smethod_49(Type type_0, BindingFlags bindingFlags_0)
	{
		return type_0.GetProperties(bindingFlags_0);
	}

	internal static MethodInfo smethod_50(PropertyInfo propertyInfo_0, bool bool_0)
	{
		return propertyInfo_0.GetGetMethod(bool_0);
	}

	internal static MethodInfo smethod_51(PropertyInfo propertyInfo_0, bool bool_0)
	{
		return propertyInfo_0.GetSetMethod(bool_0);
	}

	internal static ParameterInfo[] smethod_52(PropertyInfo propertyInfo_0)
	{
		return propertyInfo_0.GetIndexParameters();
	}

	internal static Type smethod_53(PropertyInfo propertyInfo_0)
	{
		return propertyInfo_0.PropertyType;
	}

	internal static MethodInfo smethod_54(MethodInfo methodInfo_0)
	{
		return methodInfo_0.GetBaseDefinition();
	}

	internal static Type smethod_55(MemberInfo memberInfo_0)
	{
		return memberInfo_0.DeclaringType;
	}

	internal static FieldInfo[] smethod_56(Type type_0, BindingFlags bindingFlags_0)
	{
		return type_0.GetFields(bindingFlags_0);
	}

	internal static bool smethod_57(FieldInfo fieldInfo_0)
	{
		return fieldInfo_0.IsLiteral;
	}

	internal static bool smethod_58(FieldInfo fieldInfo_0)
	{
		return fieldInfo_0.IsInitOnly;
	}

	internal static Type smethod_59(FieldInfo fieldInfo_0)
	{
		return fieldInfo_0.FieldType;
	}

	internal static bool smethod_60(string string_0, string string_1)
	{
		return string_0.Contains(string_1);
	}

	internal static MethodInfo[] smethod_61(Type type_0, BindingFlags bindingFlags_0)
	{
		return type_0.GetMethods(bindingFlags_0);
	}

	internal static ParameterInfo[] smethod_62(MethodBase methodBase_0)
	{
		return methodBase_0.GetParameters();
	}

	internal static Type smethod_63(MethodInfo methodInfo_0)
	{
		return methodInfo_0.ReturnType;
	}

	internal static bool smethod_64(Type type_0)
	{
		return type_0.IsPrimitive;
	}

	internal static bool smethod_65(Type type_0)
	{
		return type_0.IsEnum;
	}

	internal static bool smethod_66(Type type_0)
	{
		return type_0.IsArray;
	}

	internal static int smethod_67(Type type_0)
	{
		return type_0.GetArrayRank();
	}

	internal static Type smethod_68(Type type_0)
	{
		return type_0.GetElementType();
	}

	internal static bool smethod_69(Type type_0)
	{
		return type_0.IsGenericType;
	}

	internal static Type smethod_70(Type type_0)
	{
		return type_0.GetGenericTypeDefinition();
	}

	internal static Type[] smethod_71(Type type_0)
	{
		return type_0.GetGenericArguments();
	}

	internal static bool smethod_72(MemberInfo memberInfo_0, Type type_0, bool bool_0)
	{
		return Attribute.IsDefined(memberInfo_0, type_0, bool_0);
	}

	internal static Attribute smethod_73(MemberInfo memberInfo_0, Type type_0, bool bool_0)
	{
		return Attribute.GetCustomAttribute(memberInfo_0, type_0, bool_0);
	}

	internal static Attribute[] smethod_74(MemberInfo memberInfo_0, Type type_0, bool bool_0)
	{
		return Attribute.GetCustomAttributes(memberInfo_0, type_0, bool_0);
	}

	internal static ScriptableObject smethod_75(Type type_0)
	{
		return ScriptableObject.CreateInstance(type_0);
	}

	internal static ConstructorInfo smethod_76(Type type_0, BindingFlags bindingFlags_0, Binder binder_0, Type[] type_1, ParameterModifier[] parameterModifier_0)
	{
		return type_0.GetConstructor(bindingFlags_0, binder_0, type_1, parameterModifier_0);
	}

	internal static object smethod_77(Type type_0, bool bool_0)
	{
		return Activator.CreateInstance(type_0, bool_0);
	}

	internal static object smethod_78(Type type_0)
	{
		return FormatterServices.GetUninitializedObject(type_0);
	}

	internal static Type smethod_79(string string_0)
	{
		return Type.GetType(string_0);
	}

	internal static int smethod_80(string string_0, char char_0)
	{
		return string_0.IndexOf(char_0);
	}

	internal static string smethod_81(string string_0, int int_0, int int_1)
	{
		return string_0.Substring(int_0, int_1);
	}

	internal static Assembly smethod_82(string string_0)
	{
		return Assembly.Load(string_0);
	}

	internal static Type smethod_83(Assembly assembly_0, string string_0)
	{
		return assembly_0.GetType(string_0);
	}

	internal static string smethod_84(string string_0, string string_1)
	{
		return string_0 + string_1;
	}

	internal static AppDomain smethod_85()
	{
		return AppDomain.CurrentDomain;
	}

	internal static Assembly[] smethod_86(AppDomain appDomain_0)
	{
		return appDomain_0.GetAssemblies();
	}

	internal static Type[] smethod_87(Assembly assembly_0)
	{
		return assembly_0.GetTypes();
	}

	internal static bool smethod_88(string string_0, string string_1)
	{
		return string_0 == string_1;
	}

	internal static Type smethod_89(ParameterInfo parameterInfo_0)
	{
		return parameterInfo_0.ParameterType;
	}

	internal static StringBuilder smethod_90(int int_0)
	{
		return new StringBuilder(int_0);
	}
}
