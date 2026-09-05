using System;
using MPatchrMain;
using UnityEngine;
using VRGIN.Controls.Speech;
using VRGIN.Core;
using VRGIN.Visuals;

internal class v8fwj6Sh74zipqAfgLp_00241v03QrY57rxEDKsDLlfUsTxb : IVRManagerContext
{
	private DefaultMaterialPalette zcHpYIs3qsMMWGaglDMYGio;

	private VRSettings NnlrQ9kKtff9pRSXCIXCu9c;

	public string GuiLayer => global::_003CModule_003E.smethod_26<string>(1993068346u);

	public string UILayer => global::_003CModule_003E.smethod_29<string>(3362765468u);

	public int UILayerMask => LayerMask.GetMask(UILayer);

	public int IgnoreMask => 0;

	public Color PrimaryColor => Color.cyan;

	public IMaterialPalette Materials => zcHpYIs3qsMMWGaglDMYGio;

	public VRSettings Settings => NnlrQ9kKtff9pRSXCIXCu9c;

	public string InvisibleLayer => global::_003CModule_003E.smethod_27<string>(1495363439u);

	public bool SimulateCursor => true;

	public bool GUIAlternativeSortingMode => true;

	public Type VoiceCommandType => smethod_4(typeof(VoiceCommand).TypeHandle);

	public float GuiNearClipPlane => -100000f;

	public float GuiFarClipPlane => 100000f;

	public float NearClipPlane => 0.01f;

	public float UnitToMeter => 1f;

	public bool EnforceDefaultGUIMaterials => false;

	public bool ConfineMouse => MPatchr._0024Ymloe9RVCTW7x1ASuQ3c68.vr_lockMouse;

	public GUIType PreferredGUI => GUIType.uGUI;

	public v8fwj6Sh74zipqAfgLp_00241v03QrY57rxEDKsDLlfUsTxb()
	{
		zcHpYIs3qsMMWGaglDMYGio = smethod_0();
		NnlrQ9kKtff9pRSXCIXCu9c = smethod_1();
		smethod_2(NnlrQ9kKtff9pRSXCIXCu9c, MPatchr._0024Ymloe9RVCTW7x1ASuQ3c68.vr_curvedScreen ? GUIMonitor.CurvinessState.Curved : GUIMonitor.CurvinessState.Flat);
		smethod_3(NnlrQ9kKtff9pRSXCIXCu9c, 3f);
	}

	internal static DefaultMaterialPalette smethod_0()
	{
		return new DefaultMaterialPalette();
	}

	internal static VRSettings smethod_1()
	{
		return new VRSettings();
	}

	internal static void smethod_2(VRSettings vrsettings_0, GUIMonitor.CurvinessState curvinessState_0)
	{
		vrsettings_0.Projection = curvinessState_0;
	}

	internal static void smethod_3(VRSettings vrsettings_0, float float_0)
	{
		vrsettings_0.IPDScale = float_0;
	}

	internal static Type smethod_4(RuntimeTypeHandle runtimeTypeHandle_0)
	{
		return Type.GetTypeFromHandle(runtimeTypeHandle_0);
	}
}
