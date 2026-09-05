using System.Collections.Generic;
using UnityEngine;

internal static class JQpZ6lP9aBgIeq1zc9f_eT6euqKTWcd_R1Xq7K0V0oLZ
{
	private static Vector3[] nbAkMoMGxAjWkaFw9as_0024bpY;

	private static List<Color> BA3FCYk9XmbiKjOP8P2PNxVKX8eRVklkk69NmWA3PdRg = new List<Color>();

	private static List<Vector3> zAHEJPagAbut1y8EZjS7w_Y = new List<Vector3>();

	private static List<Matrix4x4> list_0 = new List<Matrix4x4>();

	private static List<Color> K2u9QmoymniG_WAhy_DEo6tsLNh__Y7W8ja4pFSzNyxY = new List<Color>();

	private static List<tRJvS6DbDGSAxEUp64c3QC48aU7MDx_d9E7eZDgMLdNx> list_1 = new List<tRJvS6DbDGSAxEUp64c3QC48aU7MDx_d9E7eZDgMLdNx>();

	internal static Material v9cXvr_0024GnMZFPia_0024zufmsb0 = smethod_10(smethod_9(global::_003CModule_003E.smethod_29<string>(885299896u)));

	internal static void IkbypDCNwbjaXcD3TQc7JBk(Color color_0, Vector3 vector3_0, Vector3 vector3_1)
	{
		BA3FCYk9XmbiKjOP8P2PNxVKX8eRVklkk69NmWA3PdRg.Add(color_0);
		zAHEJPagAbut1y8EZjS7w_Y.Add(vector3_0);
		zAHEJPagAbut1y8EZjS7w_Y.Add(vector3_1);
	}

	internal static void d5XvqNLNuVqmANrqHnMnO9c(Matrix4x4 matrix4x4_0, Color color_0)
	{
		if (nbAkMoMGxAjWkaFw9as_0024bpY == null)
		{
			nbAkMoMGxAjWkaFw9as_0024bpY = new Vector3[8];
			nbAkMoMGxAjWkaFw9as_0024bpY[0] = new Vector3(-0.5f, 0.5f, 0.5f);
			nbAkMoMGxAjWkaFw9as_0024bpY[1] = new Vector3(0.5f, 0.5f, 0.5f);
			nbAkMoMGxAjWkaFw9as_0024bpY[2] = new Vector3(0.5f, 0.5f, -0.5f);
			nbAkMoMGxAjWkaFw9as_0024bpY[3] = new Vector3(-0.5f, 0.5f, -0.5f);
			nbAkMoMGxAjWkaFw9as_0024bpY[4] = new Vector3(-0.5f, -0.5f, 0.5f);
			nbAkMoMGxAjWkaFw9as_0024bpY[5] = new Vector3(0.5f, -0.5f, 0.5f);
			nbAkMoMGxAjWkaFw9as_0024bpY[6] = new Vector3(0.5f, -0.5f, -0.5f);
			nbAkMoMGxAjWkaFw9as_0024bpY[7] = new Vector3(-0.5f, -0.5f, -0.5f);
		}
		list_0.Add(matrix4x4_0);
		K2u9QmoymniG_WAhy_DEo6tsLNh__Y7W8ja4pFSzNyxY.Add(color_0);
	}

	internal static void d5XvqNLNuVqmANrqHnMnO9c(Vector3 vector3_0, Quaternion quaternion_0, Vector3 vector3_1, Color color_0)
	{
		d5XvqNLNuVqmANrqHnMnO9c(Matrix4x4.TRS(vector3_0, quaternion_0, vector3_1), color_0);
	}

	internal static void Bn_seeVFKvqZM_0024PkKfbal3s(Vector3 vector3_0, Vector3 vector3_1, Color color_0, float float_0, bool bool_0 = false)
	{
		list_1.Add(new tRJvS6DbDGSAxEUp64c3QC48aU7MDx_d9E7eZDgMLdNx(vector3_0, vector3_1, color_0, float_0, bool_0));
	}

	private static void u4Dn4xMKvgBxPukZVNnyrlQ()
	{
		for (int i = 0; i < list_0.Count; i++)
		{
			smethod_0(v9cXvr_0024GnMZFPia_0024zufmsb0, 0);
			smethod_1();
			smethod_2(list_0[i]);
			smethod_3(1);
			smethod_4(K2u9QmoymniG_WAhy_DEo6tsLNh__Y7W8ja4pFSzNyxY[i]);
			for (int j = 0; j < 4; j++)
			{
				smethod_5(nbAkMoMGxAjWkaFw9as_0024bpY[j]);
				int num = (j + 1) % 4;
				smethod_5(nbAkMoMGxAjWkaFw9as_0024bpY[num]);
				smethod_5(nbAkMoMGxAjWkaFw9as_0024bpY[j + 4]);
				smethod_5(nbAkMoMGxAjWkaFw9as_0024bpY[num + 4]);
				smethod_5(nbAkMoMGxAjWkaFw9as_0024bpY[j]);
				smethod_5(nbAkMoMGxAjWkaFw9as_0024bpY[j + 4]);
			}
			smethod_6();
			smethod_7();
		}
	}

	private static void lv_1pTkfEGhj9qGPI4tXVso()
	{
		smethod_3(7);
		foreach (tRJvS6DbDGSAxEUp64c3QC48aU7MDx_d9E7eZDgMLdNx item in list_1)
		{
			smethod_4(item.k9jTQ33irMfqZyYWrqgwpFA);
			if (item.bool_0)
			{
				Quaternion quaternion_ = Quaternion.FromToRotation(Vector3.up, Camera.main.transform.forward);
				H5hAiipRO8Ii4u_0024wHB8nsxWtutlMT_F22q5VOQTPXCdo.Zqq2Di1oxqgZnuhMZcyLlUQ97Ask5CP1U2nV51_AKad7(H5hAiipRO8Ii4u_0024wHB8nsxWtutlMT_F22q5VOQTPXCdo.oE0iISLu17q5U2guufpMSPD2VEAJN4_0024d9BQV5BVasnRT(item.PckPQeUtSskSo3_3JV8NibM + new Vector3(0f - item.dl82iJYW3R8ih87fjwFip6Q, 0f, 0f - item.dl82iJYW3R8ih87fjwFip6Q), item.PckPQeUtSskSo3_3JV8NibM, quaternion_));
				H5hAiipRO8Ii4u_0024wHB8nsxWtutlMT_F22q5VOQTPXCdo.Zqq2Di1oxqgZnuhMZcyLlUQ97Ask5CP1U2nV51_AKad7(H5hAiipRO8Ii4u_0024wHB8nsxWtutlMT_F22q5VOQTPXCdo.oE0iISLu17q5U2guufpMSPD2VEAJN4_0024d9BQV5BVasnRT(item.PckPQeUtSskSo3_3JV8NibM + new Vector3(0f - item.dl82iJYW3R8ih87fjwFip6Q, 0f, item.dl82iJYW3R8ih87fjwFip6Q), item.PckPQeUtSskSo3_3JV8NibM, quaternion_));
				H5hAiipRO8Ii4u_0024wHB8nsxWtutlMT_F22q5VOQTPXCdo.Zqq2Di1oxqgZnuhMZcyLlUQ97Ask5CP1U2nV51_AKad7(H5hAiipRO8Ii4u_0024wHB8nsxWtutlMT_F22q5VOQTPXCdo.oE0iISLu17q5U2guufpMSPD2VEAJN4_0024d9BQV5BVasnRT(item.PckPQeUtSskSo3_3JV8NibM + new Vector3(item.dl82iJYW3R8ih87fjwFip6Q, 0f, item.dl82iJYW3R8ih87fjwFip6Q), item.PckPQeUtSskSo3_3JV8NibM, quaternion_));
				H5hAiipRO8Ii4u_0024wHB8nsxWtutlMT_F22q5VOQTPXCdo.Zqq2Di1oxqgZnuhMZcyLlUQ97Ask5CP1U2nV51_AKad7(H5hAiipRO8Ii4u_0024wHB8nsxWtutlMT_F22q5VOQTPXCdo.oE0iISLu17q5U2guufpMSPD2VEAJN4_0024d9BQV5BVasnRT(item.PckPQeUtSskSo3_3JV8NibM + new Vector3(item.dl82iJYW3R8ih87fjwFip6Q, 0f, 0f - item.dl82iJYW3R8ih87fjwFip6Q), item.PckPQeUtSskSo3_3JV8NibM, quaternion_));
			}
			else
			{
				Quaternion quaternion_2 = Quaternion.FromToRotation(Vector3.up, item.vector3_0);
				H5hAiipRO8Ii4u_0024wHB8nsxWtutlMT_F22q5VOQTPXCdo.Zqq2Di1oxqgZnuhMZcyLlUQ97Ask5CP1U2nV51_AKad7(H5hAiipRO8Ii4u_0024wHB8nsxWtutlMT_F22q5VOQTPXCdo.oE0iISLu17q5U2guufpMSPD2VEAJN4_0024d9BQV5BVasnRT(item.PckPQeUtSskSo3_3JV8NibM + new Vector3(0f - item.dl82iJYW3R8ih87fjwFip6Q, 0f, 0f - item.dl82iJYW3R8ih87fjwFip6Q), item.PckPQeUtSskSo3_3JV8NibM, quaternion_2));
				H5hAiipRO8Ii4u_0024wHB8nsxWtutlMT_F22q5VOQTPXCdo.Zqq2Di1oxqgZnuhMZcyLlUQ97Ask5CP1U2nV51_AKad7(H5hAiipRO8Ii4u_0024wHB8nsxWtutlMT_F22q5VOQTPXCdo.oE0iISLu17q5U2guufpMSPD2VEAJN4_0024d9BQV5BVasnRT(item.PckPQeUtSskSo3_3JV8NibM + new Vector3(0f - item.dl82iJYW3R8ih87fjwFip6Q, 0f, item.dl82iJYW3R8ih87fjwFip6Q), item.PckPQeUtSskSo3_3JV8NibM, quaternion_2));
				H5hAiipRO8Ii4u_0024wHB8nsxWtutlMT_F22q5VOQTPXCdo.Zqq2Di1oxqgZnuhMZcyLlUQ97Ask5CP1U2nV51_AKad7(H5hAiipRO8Ii4u_0024wHB8nsxWtutlMT_F22q5VOQTPXCdo.oE0iISLu17q5U2guufpMSPD2VEAJN4_0024d9BQV5BVasnRT(item.PckPQeUtSskSo3_3JV8NibM + new Vector3(item.dl82iJYW3R8ih87fjwFip6Q, 0f, item.dl82iJYW3R8ih87fjwFip6Q), item.PckPQeUtSskSo3_3JV8NibM, quaternion_2));
				H5hAiipRO8Ii4u_0024wHB8nsxWtutlMT_F22q5VOQTPXCdo.Zqq2Di1oxqgZnuhMZcyLlUQ97Ask5CP1U2nV51_AKad7(H5hAiipRO8Ii4u_0024wHB8nsxWtutlMT_F22q5VOQTPXCdo.oE0iISLu17q5U2guufpMSPD2VEAJN4_0024d9BQV5BVasnRT(item.PckPQeUtSskSo3_3JV8NibM + new Vector3(item.dl82iJYW3R8ih87fjwFip6Q, 0f, 0f - item.dl82iJYW3R8ih87fjwFip6Q), item.PckPQeUtSskSo3_3JV8NibM, quaternion_2));
			}
		}
		GL.End();
	}

	internal static void DTd4L68WBgbp_0024ZCuAPqkj8o()
	{
		smethod_8();
		smethod_0(v9cXvr_0024GnMZFPia_0024zufmsb0, 0);
		rhTLXahOJUgFzRwIhIE4rMI();
		lv_1pTkfEGhj9qGPI4tXVso();
		u4Dn4xMKvgBxPukZVNnyrlQ();
	}

	private static void rhTLXahOJUgFzRwIhIE4rMI()
	{
		int count = BA3FCYk9XmbiKjOP8P2PNxVKX8eRVklkk69NmWA3PdRg.Count;
		if (count > 0)
		{
			smethod_0(v9cXvr_0024GnMZFPia_0024zufmsb0, 0);
			smethod_3(1);
			for (int i = 0; i < count; i++)
			{
				smethod_4(BA3FCYk9XmbiKjOP8P2PNxVKX8eRVklkk69NmWA3PdRg[i]);
				smethod_5(zAHEJPagAbut1y8EZjS7w_Y[i * 2]);
				smethod_5(zAHEJPagAbut1y8EZjS7w_Y[i * 2 + 1]);
			}
			smethod_6();
		}
	}

	internal static void _0024ZnRHoWKKWEx4J1_0024EW6RsGs()
	{
		BA3FCYk9XmbiKjOP8P2PNxVKX8eRVklkk69NmWA3PdRg.Clear();
		zAHEJPagAbut1y8EZjS7w_Y.Clear();
		list_1.Clear();
		list_0.Clear();
	}

	internal static bool smethod_0(Material material_0, int int_0)
	{
		return material_0.SetPass(int_0);
	}

	internal static void smethod_1()
	{
		GL.PushMatrix();
	}

	internal static void smethod_2(Matrix4x4 matrix4x4_0)
	{
		GL.MultMatrix(matrix4x4_0);
	}

	internal static void smethod_3(int int_0)
	{
		GL.Begin(int_0);
	}

	internal static void smethod_4(Color color_0)
	{
		GL.Color(color_0);
	}

	internal static void smethod_5(Vector3 vector3_0)
	{
		GL.Vertex(vector3_0);
	}

	internal static void smethod_6()
	{
		GL.End();
	}

	internal static void smethod_7()
	{
		GL.PopMatrix();
	}

	internal static bool smethod_8()
	{
		return Arena.CheckMainCam();
	}

	internal static Shader smethod_9(string string_0)
	{
		return Shader.Find(string_0);
	}

	internal static Material smethod_10(Shader shader_0)
	{
		return new Material(shader_0);
	}
}
