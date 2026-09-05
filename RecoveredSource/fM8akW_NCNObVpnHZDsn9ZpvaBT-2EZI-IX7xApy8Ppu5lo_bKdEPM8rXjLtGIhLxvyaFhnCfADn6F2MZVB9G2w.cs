using HarmonyLib;
using MPatchrMain;
using McnCraft;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

[HarmonyPatch(typeof(Meeting))]
[HarmonyPatch("GMKBKFPBKPF")]
internal static class fM8akW_NCNObVpnHZDsn9ZpvaBT_00242EZI_0024IX7xApy8Ppu5lo_bKdEPM8rXjLtGIhLxvyaFhnCfADn6F2MZVB9G2w
{
	internal static bool Lpp9T_00243HIEfu54IStegVPWU(MachineController machineController)
	{
		if (JKGKJLLFMLE.EGFHGHKLNAO == JKGKJLLFMLE.LENPCAMMAEP.Meeting && !(smethod_0().name != global::_003CModule_003E.smethod_28<string>(695999064u)))
		{
			if (machineController.KBLANAFAJFP.Count != 0)
			{
				machineController.ActivateChunk(BHCKMFDEBBH: false);
				if (!Game.IALNHPEKDON.Contains(machineController.LCKDHPKIPEI))
				{
					Game.IALNHPEKDON.Add(machineController.LCKDHPKIPEI);
				}
				for (int num = Arena.CKDFHAMIBDA.childCount - 1; num >= 0; num--)
				{
					PrimitiveController component = Arena.CKDFHAMIBDA.GetChild(num).GetComponent<PrimitiveController>();
					if (component.LCKDHPKIPEI == machineController.LCKDHPKIPEI)
					{
						component.IsValid(BHCKMFDEBBH: false);
					}
				}
				for (int num2 = Arena.DBFGEMIGLAF.childCount - 1; num2 >= 0; num2--)
				{
					PrimNodeController component2 = Arena.DBFGEMIGLAF.GetChild(num2).GetComponent<PrimNodeController>();
					if (component2.LCKDHPKIPEI == machineController.LCKDHPKIPEI)
					{
						component2.IsValid(BHCKMFDEBBH: false);
					}
				}
				int num3 = -1;
				for (int i = 0; i < Arena.PBBCHKBJAEA.Count; i++)
				{
					if (Arena.PBBCHKBJAEA[i] == machineController)
					{
						num3 = i;
						break;
					}
				}
				if (num3 == -1)
				{
					return true;
				}
				GameObject gameObject = GameObject.Find(global::_003CModule_003E.smethod_26<string>(3103631283u)).smethod_0(global::_003CModule_003E.smethod_28<string>(1844434841u) + num3);
				if (gameObject == null)
				{
					return true;
				}
				gameObject.smethod_0(global::_003CModule_003E.smethod_29<string>(1055586274u)).GetComponent<Text>().color = Color.red;
				ld6AXUgS2ayU6_0024pzUEyMQ2o_IqDuQEME8gZoDobzlZrQ(machineController, num3);
				return true;
			}
			return false;
		}
		return true;
	}

	internal static bool ld6AXUgS2ayU6_0024pzUEyMQ2o_IqDuQEME8gZoDobzlZrQ(MachineController machineController, int index = -1)
	{
		if (JKGKJLLFMLE.EGFHGHKLNAO == JKGKJLLFMLE.LENPCAMMAEP.Meeting && !(smethod_0().name != global::_003CModule_003E.smethod_27<string>(3421699567u)))
		{
			if (machineController.KBLANAFAJFP.Count == 0)
			{
				return false;
			}
			Game.BILILIOGGKI.Add(machineController.gameObject);
			machineController.ADCGGAHFEMK = true;
			machineController.ActivateCollider(BHCKMFDEBBH: false);
			if (index == -1)
			{
				for (int i = 0; i < Arena.PBBCHKBJAEA.Count; i++)
				{
					if (Arena.PBBCHKBJAEA[i] == machineController)
					{
						index = i;
						break;
					}
				}
			}
			if (index == -1)
			{
				return true;
			}
			GameObject gameObject = GameObject.Find(global::_003CModule_003E.smethod_27<string>(862174272u)).smethod_0(global::_003CModule_003E.smethod_25<string>(2906604134u) + index);
			if (gameObject == null)
			{
				return true;
			}
			gameObject.GetComponent<Toggle>().isOn = true;
			return true;
		}
		return true;
	}

	[HarmonyPostfix]
	internal static void FeUAVwFbW6wGJJdNimZY9yI(string DPGKEOAGONA, GameObject NGLBLAGMBLN)
	{
		if (JKGKJLLFMLE.EGFHGHKLNAO != JKGKJLLFMLE.LENPCAMMAEP.Meeting || smethod_0().name != global::_003CModule_003E.smethod_27<string>(3421699567u) || !MPatchr._0024Ymloe9RVCTW7x1ASuQ3c68.persistantBlock || !(DPGKEOAGONA == global::_003CModule_003E.smethod_28<string>(2785782370u)))
		{
			return;
		}
		bool isOn = NGLBLAGMBLN.GetComponent<Toggle>().isOn;
		int widgetID = SceneMan.GetWidgetID(NGLBLAGMBLN);
		int aKAFEPJIFKC = Arena.PBBCHKBJAEA[widgetID].AKAFEPJIFKC;
		bool value = HOCGCCAIPFF.AFLJECMLJDL(SystemData.EHLMFKOOHLI.Modifier);
		if (isOn)
		{
			if (!MPatchr._0024Ymloe9RVCTW7x1ASuQ3c68.blockedPlayers.ContainsKey(aKAFEPJIFKC.ToString()))
			{
				MPatchr._0024Ymloe9RVCTW7x1ASuQ3c68.blockedPlayers.Add(aKAFEPJIFKC.ToString(), value);
			}
		}
		else if (MPatchr._0024Ymloe9RVCTW7x1ASuQ3c68.blockedPlayers.ContainsKey(aKAFEPJIFKC.ToString()))
		{
			MPatchr._0024Ymloe9RVCTW7x1ASuQ3c68.blockedPlayers.Remove(aKAFEPJIFKC.ToString());
		}
		MPatchr._0024Ymloe9RVCTW7x1ASuQ3c68.UUiRNMwxRbfk_Fs4cDErRoM();
	}

	internal static Scene smethod_0()
	{
		return SceneManager.GetActiveScene();
	}
}
