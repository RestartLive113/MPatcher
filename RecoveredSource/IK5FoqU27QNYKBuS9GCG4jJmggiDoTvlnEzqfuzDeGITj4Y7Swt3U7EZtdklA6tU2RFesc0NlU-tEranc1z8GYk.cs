using MPatchrMain;
using McnCraft;
using UnityEngine;

internal class IK5FoqU27QNYKBuS9GCG4jJmggiDoTvlnEzqfuzDeGITj4Y7Swt3U7EZtdklA6tU2RFesc0NlU_0024tEranc1z8GYk : MonoBehaviour
{
	private MachineController wRW7iu9_0024Mt8no56mTB9zDBE;

	private StampController Gg3uz74A2VuqhrCBW63dCis;

	private PlateController WcreQ_0024_w_0024YzbiNuU8E7VX2o;

	private Material bI2brPAlXsrzT5DHrzCi4uw;

	private bool JfvfNa5n1GWtOzYRmeMuREk;

	private MeshRenderer Icb2ZYI6rl1wr0jZHuKcd_gcNeAYdm_0024HMuKLbEPGufTQ;

	private MeshRenderer meshRenderer_0;

	private Vector2 G8xcnDkhgVRTHLkNszGjPx8 = Vector2.zero;

	private Class56 class56_0;

	private Class56 t8eJ2m_0024SkfTQDTtrqZgFOyE
	{
		get
		{
			if (smethod_0((Object)wRW7iu9_0024Mt8no56mTB9zDBE, (Object)null))
			{
				if (!smethod_1((Object)WcreQ_0024_w_0024YzbiNuU8E7VX2o, (Object)null))
				{
					wRW7iu9_0024Mt8no56mTB9zDBE = C3ICgSiiIP_0024leAFADbuBdgpjYcSPPqNFo_0024XLNKthe48m(smethod_2((Component)this));
				}
				else
				{
					wRW7iu9_0024Mt8no56mTB9zDBE = WcreQ_0024_w_0024YzbiNuU8E7VX2o.FPEBEEJGFPI;
				}
			}
			if (smethod_0((Object)class56_0, (Object)null))
			{
				class56_0 = wRW7iu9_0024Mt8no56mTB9zDBE.GetComponent<Class56>();
			}
			return class56_0;
		}
	}

	private MachineController C3ICgSiiIP_0024leAFADbuBdgpjYcSPPqNFo_0024XLNKthe48m(Transform start)
	{
		while (smethod_1((Object)smethod_3(start), (Object)null) && smethod_0((Object)start.GetComponent<MachineController>(), (Object)null) && smethod_0((Object)start.GetComponent<BodyController>(), (Object)null))
		{
			start = smethod_3(start);
		}
		if (smethod_1((Object)start.GetComponent<MachineController>(), (Object)null))
		{
			return start.GetComponent<MachineController>();
		}
		if (smethod_1((Object)start.GetComponent<BodyController>(), (Object)null))
		{
			return start.GetComponent<BodyController>().CIPOPAGDJDE;
		}
		return null;
	}

	internal void Qc0PQF_0024d1GAkt_QHyg2FdlQ(StampController sc, PlateController pc = null, MeshRenderer originalMeshRenderer = null)
	{
		Gg3uz74A2VuqhrCBW63dCis = sc;
		if (smethod_1((Object)Gg3uz74A2VuqhrCBW63dCis, (Object)null))
		{
			WcreQ_0024_w_0024YzbiNuU8E7VX2o = Gg3uz74A2VuqhrCBW63dCis.HDKLPEHKJNA.GetComponent<PlateController>();
		}
		else
		{
			WcreQ_0024_w_0024YzbiNuU8E7VX2o = pc;
		}
		bI2brPAlXsrzT5DHrzCi4uw = smethod_4((Renderer)GetComponent<MeshRenderer>());
		Icb2ZYI6rl1wr0jZHuKcd_gcNeAYdm_0024HMuKLbEPGufTQ = originalMeshRenderer;
	}

	public void Update()
	{
		if (smethod_0((Object)meshRenderer_0, (Object)null))
		{
			meshRenderer_0 = GetComponent<MeshRenderer>();
		}
		if (smethod_1((Object)meshRenderer_0, (Object)null) && smethod_1((Object)Icb2ZYI6rl1wr0jZHuKcd_gcNeAYdm_0024HMuKLbEPGufTQ, (Object)null))
		{
			smethod_6((Renderer)meshRenderer_0, smethod_5((Renderer)Icb2ZYI6rl1wr0jZHuKcd_gcNeAYdm_0024HMuKLbEPGufTQ));
		}
		if (!JfvfNa5n1GWtOzYRmeMuREk && smethod_1((Object)t8eJ2m_0024SkfTQDTtrqZgFOyE, (Object)null) && smethod_1((Object)t8eJ2m_0024SkfTQDTtrqZgFOyE.texture2D_0, (Object)null))
		{
			JfvfNa5n1GWtOzYRmeMuREk = true;
			smethod_7(bI2brPAlXsrzT5DHrzCi4uw, global::_003CModule_003E.smethod_29<string>(1716911193u), (Texture)H5hAiipRO8Ii4u_0024wHB8nsxWtutlMT_F22q5VOQTPXCdo.b6Ztrosp6JyR7j55isJA2gcLLe_NYUiYgy8g5Yxncty2(t8eJ2m_0024SkfTQDTtrqZgFOyE.texture2D_0));
			smethod_8(bI2brPAlXsrzT5DHrzCi4uw, global::_003CModule_003E.smethod_25<string>(2279731259u), t8eJ2m_0024SkfTQDTtrqZgFOyE.float_0);
			smethod_8(bI2brPAlXsrzT5DHrzCi4uw, global::_003CModule_003E.smethod_25<string>(3681197607u), t8eJ2m_0024SkfTQDTtrqZgFOyE.Z17lHAwIZq_0024CHjlLc4rbKik);
		}
		if (smethod_1((Object)t8eJ2m_0024SkfTQDTtrqZgFOyE, (Object)null)
			&& MPatchr.JrTT7_0024xMFXTIRLPMXXUOnw4 != null)
		{
			bI2brPAlXsrzT5DHrzCi4uw.SetTextureOffset(global::_003CModule_003E.smethod_29<string>(134950976u), new Vector2(t8eJ2m_0024SkfTQDTtrqZgFOyE.a1LjQ2IIOmFaJUM544IaryU, t8eJ2m_0024SkfTQDTtrqZgFOyE.RCHkKphZpq9eNrQBt_H6wwU));
			t8eJ2m_0024SkfTQDTtrqZgFOyE.a1LjQ2IIOmFaJUM544IaryU = (float)MPatchr.JrTT7_0024xMFXTIRLPMXXUOnw4.xOffset / 100f;
			t8eJ2m_0024SkfTQDTtrqZgFOyE.RCHkKphZpq9eNrQBt_H6wwU = (float)MPatchr.JrTT7_0024xMFXTIRLPMXXUOnw4.yOffset / 100f;
		}
		if (!(WcreQ_0024_w_0024YzbiNuU8E7VX2o != null))
		{
			if (!(Icb2ZYI6rl1wr0jZHuKcd_gcNeAYdm_0024HMuKLbEPGufTQ != null))
			{
				return;
			}
			MaterialPropertyBlock materialPropertyBlock = new MaterialPropertyBlock();
			Icb2ZYI6rl1wr0jZHuKcd_gcNeAYdm_0024HMuKLbEPGufTQ.GetPropertyBlock(materialPropertyBlock);
			bI2brPAlXsrzT5DHrzCi4uw.SetFloat(global::_003CModule_003E.smethod_29<string>(2892571256u), materialPropertyBlock.GetVector(global::_003CModule_003E.smethod_26<string>(258247180u)).x);
			if (JfvfNa5n1GWtOzYRmeMuREk)
			{
				Vector4 vector = materialPropertyBlock.GetVector(global::_003CModule_003E.smethod_26<string>(2586946314u));
				float num = 1f / vector.z;
				float num2 = 1f / vector.w;
				if (num != G8xcnDkhgVRTHLkNszGjPx8.x || num2 != G8xcnDkhgVRTHLkNszGjPx8.y)
				{
					int width = t8eJ2m_0024SkfTQDTtrqZgFOyE.texture2D_0.width;
					int height = t8eJ2m_0024SkfTQDTtrqZgFOyE.texture2D_0.height;
					G8xcnDkhgVRTHLkNszGjPx8 = new Vector2(num, num2);
					int num3 = width / (int)num;
					int num4 = height / (int)num;
					Vector2 vector2 = Icb2ZYI6rl1wr0jZHuKcd_gcNeAYdm_0024HMuKLbEPGufTQ.GetComponent<MeshFilter>().mesh.uv[0];
					int int_ = (int)Mathf.Repeat(vector2.x * (float)width, width - num3 + 1);
					int int_2 = (int)Mathf.Repeat(vector2.y * (float)height, height - num4 + 1);
					Texture2D texture2D = new Texture2D(width, height);
					texture2D.SetPixels32(t8eJ2m_0024SkfTQDTtrqZgFOyE.texture2D_0.GetPixels32());
					texture2D.Apply();
					H5hAiipRO8Ii4u_0024wHB8nsxWtutlMT_F22q5VOQTPXCdo.hdq70e8KhoWbflpNQ4EBjiVtEp907uqHxTAunMXjw4mv(texture2D);
					bI2brPAlXsrzT5DHrzCi4uw.SetTexture(global::_003CModule_003E.smethod_27<string>(1877658948u), H5hAiipRO8Ii4u_0024wHB8nsxWtutlMT_F22q5VOQTPXCdo.b6Ztrosp6JyR7j55isJA2gcLLe_NYUiYgy8g5Yxncty2(H5hAiipRO8Ii4u_0024wHB8nsxWtutlMT_F22q5VOQTPXCdo._d_0024sJ5sV8a2fDg6YsFOl6BY(texture2D, int_, int_2, num3, num4)));
				}
			}
		}
		else if (WcreQ_0024_w_0024YzbiNuU8E7VX2o.JIMCKEPNANP != 999f)
		{
			if (JfvfNa5n1GWtOzYRmeMuREk)
			{
				MaterialPropertyBlock materialPropertyBlock2 = new MaterialPropertyBlock();
				Icb2ZYI6rl1wr0jZHuKcd_gcNeAYdm_0024HMuKLbEPGufTQ.GetPropertyBlock(materialPropertyBlock2);
				Vector4 vector3 = materialPropertyBlock2.GetVector(global::_003CModule_003E.smethod_27<string>(2177656603u));
				float num5 = 1f / vector3.z;
				float num6 = 1f / vector3.w;
				if (num5 != G8xcnDkhgVRTHLkNszGjPx8.x || num6 != G8xcnDkhgVRTHLkNszGjPx8.y)
				{
					int width2 = t8eJ2m_0024SkfTQDTtrqZgFOyE.texture2D_0.width;
					int height2 = t8eJ2m_0024SkfTQDTtrqZgFOyE.texture2D_0.height;
					G8xcnDkhgVRTHLkNszGjPx8 = new Vector2(num5, num6);
					int num7 = width2 / (int)num5;
					int num8 = height2 / (int)num5;
					Vector2 vector4 = new Vector2(vector3.x, vector3.y);
					int int_3 = (int)Mathf.Repeat(vector4.x * (float)width2, width2 - num7 + 1);
					int int_4 = (int)Mathf.Repeat(vector4.y * (float)height2, height2 - num8 + 1);
					Texture2D texture2D2 = new Texture2D(width2, height2);
					texture2D2.SetPixels32(t8eJ2m_0024SkfTQDTtrqZgFOyE.texture2D_0.GetPixels32());
					texture2D2.Apply();
					H5hAiipRO8Ii4u_0024wHB8nsxWtutlMT_F22q5VOQTPXCdo.hdq70e8KhoWbflpNQ4EBjiVtEp907uqHxTAunMXjw4mv(texture2D2);
					bI2brPAlXsrzT5DHrzCi4uw.SetTexture(global::_003CModule_003E.smethod_29<string>(1716911193u), H5hAiipRO8Ii4u_0024wHB8nsxWtutlMT_F22q5VOQTPXCdo.b6Ztrosp6JyR7j55isJA2gcLLe_NYUiYgy8g5Yxncty2(H5hAiipRO8Ii4u_0024wHB8nsxWtutlMT_F22q5VOQTPXCdo._d_0024sJ5sV8a2fDg6YsFOl6BY(texture2D2, int_3, int_4, num7, num8)));
				}
				Vector4 vector5 = Class17.iEGZnC5Qz9aAS5QbiOmfb_0024sNSSdkec6eJqnSdaVbVgmO<Vector4>(global::_003CModule_003E.smethod_26<string>(1262213028u), WcreQ_0024_w_0024YzbiNuU8E7VX2o);
				bI2brPAlXsrzT5DHrzCi4uw.SetTextureScale(global::_003CModule_003E.smethod_25<string>(3589834831u), new Vector2(vector5.x, 0f - vector5.y));
				bI2brPAlXsrzT5DHrzCi4uw.SetTextureOffset(global::_003CModule_003E.smethod_26<string>(1524317328u), new Vector2(vector5.z * num5, (0f - vector5.w) * num6));
			}
		}
		else
		{
			Vector4 vector6 = Class17.iEGZnC5Qz9aAS5QbiOmfb_0024sNSSdkec6eJqnSdaVbVgmO<Vector4>(global::_003CModule_003E.smethod_26<string>(662341686u), WcreQ_0024_w_0024YzbiNuU8E7VX2o);
			bI2brPAlXsrzT5DHrzCi4uw.SetTextureScale(global::_003CModule_003E.smethod_27<string>(3387454153u), new Vector2(vector6.z, 0f - vector6.w));
			bI2brPAlXsrzT5DHrzCi4uw.SetTextureOffset(global::_003CModule_003E.smethod_25<string>(3589834831u), new Vector2(vector6.x, vector6.y));
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

	internal static Transform smethod_2(Component component_0)
	{
		return component_0.transform;
	}

	internal static Transform smethod_3(Transform transform_0)
	{
		return transform_0.parent;
	}

	internal static Material smethod_4(Renderer renderer_0)
	{
		return renderer_0.material;
	}

	internal static bool smethod_5(Renderer renderer_0)
	{
		return renderer_0.enabled;
	}

	internal static void smethod_6(Renderer renderer_0, bool bool_0)
	{
		renderer_0.enabled = bool_0;
	}

	internal static void smethod_7(Material material_0, string string_0, Texture texture_0)
	{
		material_0.SetTexture(string_0, texture_0);
	}

	internal static void smethod_8(Material material_0, string string_0, float float_0)
	{
		material_0.SetFloat(string_0, float_0);
	}
}
