using System;
using System.Collections.Generic;
using LitJson;

namespace MPatchrMain;

public class mcpd
{
	public enum buildWireframeMode
	{
		OFF,
		SOLID,
		TRANSPARENT,
		SOLIDTRANSPARENT,
		QUADS
	}

	public int fver = globals.MCPDVer;

	public string nmapURL;

	public int nmapHash;

	public int smooth = 50;

	public int metal;

	public int xOffset;

	public int yOffset;

	public int vrScale = 3;

	public int waterEffectHideRange;

	public int recoilMult = 100;

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

	public bool noCouplerWeight;

	public bool noInvisWeight;

	public buildWireframeMode objPlan_wireframe;

	internal static bool vZW_B4ovxRyY1YRoksOUhxmYxixzJzkIDJXf3KdYD4bu(int int_0, int int_1, int int_2, Dictionary<string, object> dictionary_0)
	{
		int num = 0;
		while (true)
		{
			if (num < JKGKJLLFMLE.HHGILAIOCLG.blockData.Count)
			{
				BlockData blockData = JKGKJLLFMLE.HHGILAIOCLG.blockData[num];
				if (blockData.x == int_0 && blockData.y == int_1 && blockData.z == int_2)
				{
					break;
				}
				num++;
				continue;
			}
			return false;
		}
		JKGKJLLFMLE.HHGILAIOCLG.blockData[num].props = dictionary_0;
		return true;
	}

	internal static mcpd smethod_0(string string_0)
	{
		mcpd mcpd2 = JsonMapper.ToObject<mcpd>(string_0);
		try
		{
			while (mcpd2.fver < globals.MCPDVer)
			{
				if (mcpd2.fver != 1)
				{
					if (mcpd2.fver != 2)
					{
						continue;
					}
					mK6lLU33ECSzxV4u22c7_0024ijC0MeyAkqA_PRIEl9WpAZK.YELeoCirSeVGf6u7nOIXkng(global::_003CModule_003E.smethod_26<string>(700104883u));
					Dictionary<string, Dictionary<string, object>> dictionary = JsonMapper.ToObject<Dictionary<string, Dictionary<string, object>>>(smethod_3((object)smethod_2(smethod_1(string_0), global::_003CModule_003E.smethod_29<string>(2237579423u))));
					foreach (string key in dictionary.Keys)
					{
						string[] array = smethod_4(key, new char[1] { ',' });
						int int_ = int.Parse(array[0]);
						int int_2 = int.Parse(array[1]);
						int int_3 = int.Parse(array[2]);
						bool flag = vZW_B4ovxRyY1YRoksOUhxmYxixzJzkIDJXf3KdYD4bu(int_, int_2, int_3, dictionary[key]);
						mK6lLU33ECSzxV4u22c7_0024ijC0MeyAkqA_PRIEl9WpAZK.YELeoCirSeVGf6u7nOIXkng(global::_003CModule_003E.smethod_27<string>(438964940u) + key + global::_003CModule_003E.smethod_27<string>(3109622783u) + flag);
					}
					JKGKJLLFMLE.BOMAFGLNGMI();
					mcpd2.fver = 3;
					mcpd2.UUiRNMwxRbfk_Fs4cDErRoM();
				}
				else
				{
					mcpd2.fver = 2;
					mcpd2.UUiRNMwxRbfk_Fs4cDErRoM();
				}
			}
		}
		catch (Exception ex)
		{
			mK6lLU33ECSzxV4u22c7_0024ijC0MeyAkqA_PRIEl9WpAZK.YELeoCirSeVGf6u7nOIXkng(global::_003CModule_003E.smethod_28<string>(1500386224u) + ex.Message + global::_003CModule_003E.smethod_27<string>(2085947933u) + ex.StackTrace);
		}
		return mcpd2;
	}

	internal string z1yjsYepiQdxZ6m9Dpbg_Pk()
	{
		return smethod_3((object)this);
	}

	internal void UUiRNMwxRbfk_Fs4cDErRoM()
	{
		CeGPiEeLcGa_jAqF8L8J7S5Vi4pPh_0024X8_GlojvQ92gul.XCwrmU0I6plQX_waggFR3GE(z1yjsYepiQdxZ6m9Dpbg_Pk(), global::_003CModule_003E.smethod_26<string>(866759019u));
	}

	internal static JsonData smethod_1(string string_0)
	{
		return JsonMapper.ToObject(string_0);
	}

	internal static JsonData smethod_2(JsonData jsonData_0, string string_0)
	{
		return jsonData_0[string_0];
	}

	internal static string smethod_3(object object_0)
	{
		return JsonMapper.ToJson(object_0);
	}

	internal static string[] smethod_4(string string_0, char[] char_0)
	{
		return string_0.Split(char_0);
	}
}
