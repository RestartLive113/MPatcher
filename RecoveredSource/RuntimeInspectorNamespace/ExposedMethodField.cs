using System;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

namespace RuntimeInspectorNamespace;

public class ExposedMethodField : InspectorField
{
	[SerializeField]
	private Button brbcwEH2QkF43M1XFTPSylk;

	protected ExposedMethod boundMethod;

	public override bool SupportsType(Type type)
	{
		return type == smethod_16(typeof(ExposedMethod).TypeHandle);
	}

	public override void Initialize()
	{
		base.Initialize();
		smethod_18((UnityEvent)smethod_17(brbcwEH2QkF43M1XFTPSylk), (UnityAction)InvokeMethod);
	}

	protected override void OnSkinChanged()
	{
		base.OnSkinChanged();
		brbcwEH2QkF43M1XFTPSylk.SetSkinButton(base.Skin);
	}

	protected override void OnDepthChanged()
	{
		((RectTransform)smethod_19((Component)brbcwEH2QkF43M1XFTPSylk)).sizeDelta = new Vector2(-base.Skin.IndentAmount * base.Depth, 0f);
	}

	public void SetBoundMethod(ExposedMethod boundMethod)
	{
		this.boundMethod = boundMethod;
		base.NameRaw = boundMethod.Label;
	}

	public void InvokeMethod()
	{
		Refresh();
		if (!boundMethod.IsInitializer)
		{
			boundMethod.Call(base.Value);
		}
		else
		{
			base.Value = boundMethod.CallAndReturnValue(base.Value);
		}
	}

	internal static Type smethod_16(RuntimeTypeHandle runtimeTypeHandle_0)
	{
		return Type.GetTypeFromHandle(runtimeTypeHandle_0);
	}

	internal static Button.ButtonClickedEvent smethod_17(Button button_0)
	{
		return button_0.onClick;
	}

	internal static void smethod_18(UnityEvent unityEvent_0, UnityAction unityAction_0)
	{
		unityEvent_0.AddListener(unityAction_0);
	}

	internal static Transform smethod_19(Component component_0)
	{
		return component_0.transform;
	}
}
