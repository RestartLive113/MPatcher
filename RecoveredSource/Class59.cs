using UnityEngine;
using UnityEngine.UI;

internal static class Class59
{
	private static Control0 jTTqmajOihzy_0024L_0024rB2iRstvrrnCEO7Gg_j2QoQRXq6pM;

	private static Control0 H8mBIT9DGK0WcBl3QTl9ZhUJJXdlOd0EyrsI_L3gkLOZ;

	private static SliderController sliderController_0;

	private static SliderController di2o7CcdsprFSPDgre_0024qhTo;

	private static void gA0WHcgiS3LwdeWIGLNAiuW9m_0024kkVJp1L5vciJewX1rr(BlockData aimingAtBlock, BlockController aimingAtBlockObj, GameObject panelSetup)
	{
		if (!smethod_0((Object)jTTqmajOihzy_0024L_0024rB2iRstvrrnCEO7Gg_j2QoQRXq6pM, (Object)null) || aimingAtBlock.type != BlockData.AAHMDBHDCDK.Lamp)
		{
			return;
		}
		jTTqmajOihzy_0024L_0024rB2iRstvrrnCEO7Gg_j2QoQRXq6pM = Rw1gRBZINYqqUycQDAVzVUspDHxB9kBz2FYjLkqn6c5t8_00242gRBS9DLOoFVjsA69CGA.uEsWMK_pFkCY_0024M5zt8zLsQk(global::_003CModule_003E.smethod_27<string>(2605806447u), new Vector2(-355f, -200f), global::_003CModule_003E.smethod_26<string>(1528776098u), panelSetup.transform);
		jTTqmajOihzy_0024L_0024rB2iRstvrrnCEO7Gg_j2QoQRXq6pM.DmPZGWxJ26_0024f_0024QOvQiqpmW8(new Vector2(50f, 0f));
		H8mBIT9DGK0WcBl3QTl9ZhUJJXdlOd0EyrsI_L3gkLOZ = Rw1gRBZINYqqUycQDAVzVUspDHxB9kBz2FYjLkqn6c5t8_00242gRBS9DLOoFVjsA69CGA.uEsWMK_pFkCY_0024M5zt8zLsQk(global::_003CModule_003E.smethod_28<string>(1936383984u), new Vector2(-355f, -150f), global::_003CModule_003E.smethod_29<string>(3885818505u), panelSetup.transform);
		H8mBIT9DGK0WcBl3QTl9ZhUJJXdlOd0EyrsI_L3gkLOZ.DmPZGWxJ26_0024f_0024QOvQiqpmW8(new Vector2(50f, 0f));
		sliderController_0 = Rw1gRBZINYqqUycQDAVzVUspDHxB9kBz2FYjLkqn6c5t8_00242gRBS9DLOoFVjsA69CGA.tjX92nnz9Ioc_0024izMOOLq4fI(global::_003CModule_003E.smethod_27<string>(2196404141u), global::_003CModule_003E.smethod_27<string>(3787820860u), new Vector2(-355f, -250f), 100, 500, oIuuMMCo9UmUIWmYFQEqxXAOJy8LYqspmW2FKyHMiXKdcT1cBP8p9QxEsmrfao0MIw.AIE60Kky2bU2sqqKbuxgR7LcNb5l6bc5BCXD6nzVkX_0024t(aimingAtBlockObj, global::_003CModule_003E.smethod_29<string>(1964714070u), 100), delegate(float slider)
		{
			BlockController component = aIaZtYI7wNQDacAAHL34p0OkQ2hMebOyXaBe3QcGmX_OSQeoNa1agDdJf9YfxZnGGw.PE5sYlzltGGVQg4HL2k4HC8IHLb_pbX477PyFTi4cBWYR4CijFhQ7jSAn1NoFkOd_0024A.GetComponent<BlockController>();
			if ((int)slider > 100)
			{
				oIuuMMCo9UmUIWmYFQEqxXAOJy8LYqspmW2FKyHMiXKdcT1cBP8p9QxEsmrfao0MIw.ioLu5ucx0cm9MvyfVTOkUsE(component, global::_003CModule_003E.smethod_29<string>(1964714070u), (int)slider);
			}
			else
			{
				oIuuMMCo9UmUIWmYFQEqxXAOJy8LYqspmW2FKyHMiXKdcT1cBP8p9QxEsmrfao0MIw.h42VsxFMP4yzP0oyRVsymO0(component, global::_003CModule_003E.smethod_26<string>(2315924474u));
			}
		}, panelSetup.transform);
		di2o7CcdsprFSPDgre_0024qhTo = Rw1gRBZINYqqUycQDAVzVUspDHxB9kBz2FYjLkqn6c5t8_00242gRBS9DLOoFVjsA69CGA.tjX92nnz9Ioc_0024izMOOLq4fI(global::_003CModule_003E.smethod_27<string>(2242934816u), global::_003CModule_003E.smethod_28<string>(1131848617u), new Vector2(-355f, -300f), 50, 150, oIuuMMCo9UmUIWmYFQEqxXAOJy8LYqspmW2FKyHMiXKdcT1cBP8p9QxEsmrfao0MIw.AIE60Kky2bU2sqqKbuxgR7LcNb5l6bc5BCXD6nzVkX_0024t(aimingAtBlockObj, global::_003CModule_003E.smethod_29<string>(2711182178u), 50), delegate(float slider)
		{
			BlockController component = aIaZtYI7wNQDacAAHL34p0OkQ2hMebOyXaBe3QcGmX_OSQeoNa1agDdJf9YfxZnGGw.PE5sYlzltGGVQg4HL2k4HC8IHLb_pbX477PyFTi4cBWYR4CijFhQ7jSAn1NoFkOd_0024A.GetComponent<BlockController>();
			if ((int)slider <= 50)
			{
				oIuuMMCo9UmUIWmYFQEqxXAOJy8LYqspmW2FKyHMiXKdcT1cBP8p9QxEsmrfao0MIw.h42VsxFMP4yzP0oyRVsymO0(component, global::_003CModule_003E.smethod_26<string>(3961425198u));
			}
			else
			{
				oIuuMMCo9UmUIWmYFQEqxXAOJy8LYqspmW2FKyHMiXKdcT1cBP8p9QxEsmrfao0MIw.ioLu5ucx0cm9MvyfVTOkUsE(component, global::_003CModule_003E.smethod_26<string>(3961425198u), (int)slider);
			}
		}, panelSetup.transform);
	}

	internal static void d0feay_b5lVTNLXp6cDGkn0(BlockData aimingAtBlock, BlockController aimingAtBlockObj, GameObject panelSetup)
	{
		gA0WHcgiS3LwdeWIGLNAiuW9m_0024kkVJp1L5vciJewX1rr(aimingAtBlock, aimingAtBlockObj, panelSetup);
		if (aimingAtBlock.type == BlockData.AAHMDBHDCDK.Lamp)
		{
			smethod_3(smethod_2((Component)jTTqmajOihzy_0024L_0024rB2iRstvrrnCEO7Gg_j2QoQRXq6pM), bool_0: true);
			smethod_3(smethod_2((Component)H8mBIT9DGK0WcBl3QTl9ZhUJJXdlOd0EyrsI_L3gkLOZ), bool_0: true);
			smethod_3(smethod_2((Component)smethod_5(smethod_4((Component)sliderController_0))), bool_0: true);
			smethod_3(smethod_2((Component)smethod_5(smethod_4((Component)di2o7CcdsprFSPDgre_0024qhTo))), bool_0: true);
			H8mBIT9DGK0WcBl3QTl9ZhUJJXdlOd0EyrsI_L3gkLOZ.hLxnG9Hq33zU_YUsu_00240_zak = oIuuMMCo9UmUIWmYFQEqxXAOJy8LYqspmW2FKyHMiXKdcT1cBP8p9QxEsmrfao0MIw.AIE60Kky2bU2sqqKbuxgR7LcNb5l6bc5BCXD6nzVkX_0024t(aimingAtBlockObj, global::_003CModule_003E.smethod_29<string>(2570525489u), defaultVal: false);
			jTTqmajOihzy_0024L_0024rB2iRstvrrnCEO7Gg_j2QoQRXq6pM.hLxnG9Hq33zU_YUsu_00240_zak = oIuuMMCo9UmUIWmYFQEqxXAOJy8LYqspmW2FKyHMiXKdcT1cBP8p9QxEsmrfao0MIw.AIE60Kky2bU2sqqKbuxgR7LcNb5l6bc5BCXD6nzVkX_0024t(aimingAtBlockObj, global::_003CModule_003E.smethod_27<string>(2624215815u), defaultVal: false);
			smethod_6(H5hAiipRO8Ii4u_0024wHB8nsxWtutlMT_F22q5VOQTPXCdo.H1Pr8mmPslEwoIU22reQcXE(sliderController_0), (float)oIuuMMCo9UmUIWmYFQEqxXAOJy8LYqspmW2FKyHMiXKdcT1cBP8p9QxEsmrfao0MIw.AIE60Kky2bU2sqqKbuxgR7LcNb5l6bc5BCXD6nzVkX_0024t(aimingAtBlockObj, global::_003CModule_003E.smethod_27<string>(553516235u), 100));
			smethod_6(H5hAiipRO8Ii4u_0024wHB8nsxWtutlMT_F22q5VOQTPXCdo.H1Pr8mmPslEwoIU22reQcXE(di2o7CcdsprFSPDgre_0024qhTo), (float)oIuuMMCo9UmUIWmYFQEqxXAOJy8LYqspmW2FKyHMiXKdcT1cBP8p9QxEsmrfao0MIw.AIE60Kky2bU2sqqKbuxgR7LcNb5l6bc5BCXD6nzVkX_0024t(aimingAtBlockObj, global::_003CModule_003E.smethod_26<string>(3961425198u), 50));
			jTTqmajOihzy_0024L_0024rB2iRstvrrnCEO7Gg_j2QoQRXq6pM.t2iJT_tBPyB6QRMBLAdXYUs(delegate(bool toggled)
			{
				BlockController component = aIaZtYI7wNQDacAAHL34p0OkQ2hMebOyXaBe3QcGmX_OSQeoNa1agDdJf9YfxZnGGw.PE5sYlzltGGVQg4HL2k4HC8IHLb_pbX477PyFTi4cBWYR4CijFhQ7jSAn1NoFkOd_0024A.GetComponent<BlockController>();
				if (!toggled)
				{
					oIuuMMCo9UmUIWmYFQEqxXAOJy8LYqspmW2FKyHMiXKdcT1cBP8p9QxEsmrfao0MIw.h42VsxFMP4yzP0oyRVsymO0(component, global::_003CModule_003E.smethod_28<string>(2026999264u));
				}
				else
				{
					oIuuMMCo9UmUIWmYFQEqxXAOJy8LYqspmW2FKyHMiXKdcT1cBP8p9QxEsmrfao0MIw.ioLu5ucx0cm9MvyfVTOkUsE(component, global::_003CModule_003E.smethod_28<string>(2026999264u), true);
				}
			});
			H8mBIT9DGK0WcBl3QTl9ZhUJJXdlOd0EyrsI_L3gkLOZ.t2iJT_tBPyB6QRMBLAdXYUs(delegate(bool toggled)
			{
				BlockController component = aIaZtYI7wNQDacAAHL34p0OkQ2hMebOyXaBe3QcGmX_OSQeoNa1agDdJf9YfxZnGGw.PE5sYlzltGGVQg4HL2k4HC8IHLb_pbX477PyFTi4cBWYR4CijFhQ7jSAn1NoFkOd_0024A.GetComponent<BlockController>();
				if (!toggled)
				{
					oIuuMMCo9UmUIWmYFQEqxXAOJy8LYqspmW2FKyHMiXKdcT1cBP8p9QxEsmrfao0MIw.h42VsxFMP4yzP0oyRVsymO0(component, global::_003CModule_003E.smethod_29<string>(2570525489u));
				}
				else
				{
					oIuuMMCo9UmUIWmYFQEqxXAOJy8LYqspmW2FKyHMiXKdcT1cBP8p9QxEsmrfao0MIw.ioLu5ucx0cm9MvyfVTOkUsE(component, global::_003CModule_003E.smethod_26<string>(2797075631u), true);
				}
			});
		}
		else if (smethod_1((Object)jTTqmajOihzy_0024L_0024rB2iRstvrrnCEO7Gg_j2QoQRXq6pM, (Object)null))
		{
			smethod_3(smethod_2((Component)jTTqmajOihzy_0024L_0024rB2iRstvrrnCEO7Gg_j2QoQRXq6pM), bool_0: false);
			smethod_3(smethod_2((Component)H8mBIT9DGK0WcBl3QTl9ZhUJJXdlOd0EyrsI_L3gkLOZ), bool_0: false);
			smethod_3(smethod_2((Component)smethod_5(smethod_4((Component)sliderController_0))), bool_0: false);
			smethod_3(smethod_2((Component)smethod_5(smethod_4((Component)di2o7CcdsprFSPDgre_0024qhTo))), bool_0: false);
		}
	}

	internal static bool smethod_0(Object object_0, Object object_1)
	{
		return object_0 == object_1;
	}

	internal static bool smethod_1(Object object_0, Object object_1)
	{
		return object_0 != object_1;
	}

	internal static GameObject smethod_2(Component component_0)
	{
		return component_0.gameObject;
	}

	internal static void smethod_3(GameObject gameObject_0, bool bool_0)
	{
		gameObject_0.SetActive(bool_0);
	}

	internal static Transform smethod_4(Component component_0)
	{
		return component_0.transform;
	}

	internal static Transform smethod_5(Transform transform_0)
	{
		return transform_0.parent;
	}

	internal static void smethod_6(Slider slider_0, float float_0)
	{
		slider_0.value = float_0;
	}
}
