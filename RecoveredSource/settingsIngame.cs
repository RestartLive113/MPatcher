using System.Collections;
using System.Collections.Generic;
using System.IO;
using LitJson;
using MPatchrMain;

public class settingsIngame
{
	public enum translationMode
	{
		OFF,
		OTR,
		en,
		ja,
		ru,
		fr,
		ko
	}

	public enum translationEngines
	{
		microsoft,
		deepl
	}

	public enum updateChannels
	{
		stable,
		beta,
		alpha
	}

	public bool showUpdateNotif = true;

	public bool hardKick;

	public bool vrARG;

	public bool indivFix;

	public bool discordRPC;

	public bool extraCommands;

	public bool stampNormalMap;

	public bool regionSelect;

	public bool teambuild;

	public bool hiddenRooms;

	public string roomCode = "";

	public bool QOL;

	public bool depthFix;

	public bool audioVariations;

	public bool scopeBodyHide;

	public bool hostViewHealth;

	public bool constructPlus;

	public bool graphicsPlus;

	public bool freeBoxRot;

	public bool freeCouplerRot;

	public bool setupPrecision = true;

	public int setupPrecisionEditorMode;

	public bool audioCutoffFix;

	public bool OBJPlan;

	public bool resizableWindow;

	public bool serverBusyMitigation = true;

	public bool compression;

	public bool workshopplus;

	public bool battlecost;

	public bool moreStageboxMats;

	public bool transparentAxis;

	public bool machineSwitching;

	public bool customTrackerAim;

	public bool waterEffectRange;

	public bool moreStampSize;

	public bool defaultCollisionsOff;

	public bool persistantBlock;

	public bool moreCannonRecoil;

	public bool hostScript;

	public bool mcnBugfix;

	public bool animPreview;

	public bool weightChange;

	public bool tracing;

	public int smoothUI = -1;

	public bool smallTags;

	public bool movieFile;

	public bool stickyNotes = true;

	public bool moreViewDistance;

	public string objPlan_name;

	public int objPlan_xoff;

	public int objPlan_yoff;

	public int objPlan_zoff;

	public int objPlan_xrot;

	public int objPlan_yrot;

	public int objPlan_zrot;

	public int objPlan_scale = 100;

	public int objPlan_opacity = 50;

	public int objPlan_color;

	public bool objplan_xMirrored;

	public bool objplan_yMirrored;

	public bool objplan_zMirrored;

	public mcpd.buildWireframeMode objPlan_wireframe;

	public translationMode translation;

	public translationEngines translationEngine;

	public updateChannels updateChannel;

	public string hostScripts = "";

	public int vr_mode = -1;

	public int vr_headset = -1;

	public bool vr_curvedScreen;

	public bool vr_camOffset = true;

	public bool vr_lockMouse = true;

	public bool vr_gameRendDist = true;

	public int resizable_w;

	public int resizable_h;

	public bool resizable_full;

	public Dictionary<string, bool> blockedPlayers = new Dictionary<string, bool>();

	public bool vcSupported => smethod_1(global::_003CModule_003E.smethod_26<string>(1685259480u));

	public bool vrSupported
	{
		get
		{
			if (smethod_1(global::_003CModule_003E.smethod_28<string>(2380384922u)) && smethod_1(global::_003CModule_003E.smethod_27<string>(1857896900u)))
			{
				return smethod_1(global::_003CModule_003E.smethod_26<string>(2047690452u));
			}
			return false;
		}
	}

	public bool discordSupported => smethod_1(global::_003CModule_003E.smethod_26<string>(4013958614u));

	public bool tracingSupported => smethod_1(global::_003CModule_003E.smethod_26<string>(1524875761u));

	internal void UUiRNMwxRbfk_Fs4cDErRoM()
	{
		smethod_4(smethod_2(global::_003CModule_003E.smethod_27<string>(4198575846u), global::_003CModule_003E.smethod_27<string>(3812354335u)), smethod_3((object)this));
	}

	internal void N4UcFQkZXBLT3Ewo5_rO7w4(translationMode translationMode_0)
	{
		translation = translationMode_0;
		UUiRNMwxRbfk_Fs4cDErRoM();
	}

	internal void KcD_0024nCK3Ay8M_lhV1rrgPkap2XWotLEXldWxUskU_0024hzx(translationEngines translationEngines_0)
	{
		translationEngine = translationEngines_0;
		UUiRNMwxRbfk_Fs4cDErRoM();
	}

	internal void WDBYFkMnkgdM_QKMopSHgAXXc82_0024PeBYKVLLXPK_I9I5(updateChannels updateChannels_0)
	{
		updateChannel = updateChannels_0;
		UUiRNMwxRbfk_Fs4cDErRoM();
	}

	internal void jNJFoLQ_wY8hPL4TF_0024pIwMo(int int_0)
	{
		smoothUI = int_0;
		UUiRNMwxRbfk_Fs4cDErRoM();
	}

	internal static settingsIngame smethod_0()
	{
		if (smethod_1(smethod_2(global::_003CModule_003E.smethod_27<string>(4198575846u), global::_003CModule_003E.smethod_27<string>(3812354335u))))
		{
			settingsIngame settingsIngame2 = JsonMapper.ToObject<settingsIngame>(smethod_5(smethod_2(global::_003CModule_003E.smethod_29<string>(3518270735u), global::_003CModule_003E.smethod_29<string>(2631145938u))));
			if (smethod_1(global::_003CModule_003E.smethod_28<string>(483130743u)))
			{
				JsonData jsonData = smethod_6(smethod_5(global::_003CModule_003E.smethod_29<string>(716239655u)));
				if (JsonDataContainsKey(jsonData, global::_003CModule_003E.smethod_27<string>(3333071474u)))
				{
					settingsIngame2.hardKick = smethod_8(smethod_7(jsonData, global::_003CModule_003E.smethod_27<string>(3333071474u)));
				}
				if (JsonDataContainsKey(jsonData, global::_003CModule_003E.smethod_27<string>(243299386u)))
				{
					settingsIngame2.vrARG = smethod_8(smethod_7(jsonData, global::_003CModule_003E.smethod_27<string>(243299386u)));
				}
				if (JsonDataContainsKey(jsonData, global::_003CModule_003E.smethod_28<string>(2031149060u)))
				{
					settingsIngame2.indivFix = smethod_8(smethod_7(jsonData, global::_003CModule_003E.smethod_26<string>(1701841346u)));
				}
				if (JsonDataContainsKey(jsonData, global::_003CModule_003E.smethod_27<string>(2220937616u)))
				{
					settingsIngame2.discordRPC = smethod_8(smethod_7(jsonData, global::_003CModule_003E.smethod_29<string>(1927862493u)));
				}
				smethod_9(global::_003CModule_003E.smethod_26<string>(4149260665u));
			}
			return settingsIngame2;
		}
		return new settingsIngame();
	}

	public static bool JsonDataContainsKey(JsonData data, string key)
	{
		bool result = false;
		if (data != null)
		{
			if (smethod_10(data))
			{
				if (data != null)
				{
					if (smethod_11((IDictionary)data, (object)key))
					{
						result = true;
					}
					return result;
				}
				return result;
			}
			return result;
		}
		return result;
	}

	internal static bool smethod_1(string string_0)
	{
		return File.Exists(string_0);
	}

	internal static string smethod_2(string string_0, string string_1)
	{
		return Path.Combine(string_0, string_1);
	}

	internal static string smethod_3(object object_0)
	{
		return JsonMapper.ToJson(object_0);
	}

	internal static void smethod_4(string string_0, string string_1)
	{
		File.WriteAllText(string_0, string_1);
	}

	internal static string smethod_5(string string_0)
	{
		return File.ReadAllText(string_0);
	}

	internal static JsonData smethod_6(string string_0)
	{
		return JsonMapper.ToObject(string_0);
	}

	internal static JsonData smethod_7(JsonData jsonData_0, string string_0)
	{
		return jsonData_0[string_0];
	}

	internal static bool smethod_8(JsonData jsonData_0)
	{
		return (bool)jsonData_0;
	}

	internal static void smethod_9(string string_0)
	{
		File.Delete(string_0);
	}

	internal static bool smethod_10(JsonData jsonData_0)
	{
		return jsonData_0.IsObject;
	}

	internal static bool smethod_11(IDictionary idictionary_0, object object_0)
	{
		return idictionary_0.Contains(object_0);
	}
}
