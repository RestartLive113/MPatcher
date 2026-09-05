using UnityEngine;

internal class c2wojWPyeJ90X8UxbVw8Ie7t2iSXT4W6AM5UctjLMkqq : MonoBehaviour
{
	public bool u5ER09FBgDoEuNjNt6mdw_k;

	private Material Yvnz5xIFwZxjG_0024GP8b9cMUs;

	private Vector3[] nbAkMoMGxAjWkaFw9as_0024bpY;

	public Color k9jTQ33irMfqZyYWrqgwpFA = Color.black;

	public void Start()
	{
		Yvnz5xIFwZxjG_0024GP8b9cMUs = smethod_1(smethod_0(global::_003CModule_003E.smethod_26<string>(3189187736u)));
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

	public void OnRenderObject()
	{
		if (u5ER09FBgDoEuNjNt6mdw_k)
		{
			Vector3 pos = smethod_3(smethod_2((Component)this));
			Quaternion q = smethod_4(smethod_2((Component)this));
			Vector3 s = smethod_5(smethod_2((Component)this));
			Matrix4x4 matrix4x4_ = Matrix4x4.TRS(pos, q, s);
			smethod_6(Yvnz5xIFwZxjG_0024GP8b9cMUs, 0);
			smethod_7();
			smethod_8(matrix4x4_);
			smethod_9(1);
			smethod_10(k9jTQ33irMfqZyYWrqgwpFA);
			for (int i = 0; i < 4; i++)
			{
				smethod_11(nbAkMoMGxAjWkaFw9as_0024bpY[i]);
				int num = (i + 1) % 4;
				smethod_11(nbAkMoMGxAjWkaFw9as_0024bpY[num]);
				smethod_11(nbAkMoMGxAjWkaFw9as_0024bpY[i + 4]);
				smethod_11(nbAkMoMGxAjWkaFw9as_0024bpY[num + 4]);
				smethod_11(nbAkMoMGxAjWkaFw9as_0024bpY[i]);
				smethod_11(nbAkMoMGxAjWkaFw9as_0024bpY[i + 4]);
			}
			smethod_12();
			smethod_13();
		}
	}

	internal static Shader smethod_0(string string_0)
	{
		return Shader.Find(string_0);
	}

	internal static Material smethod_1(Shader shader_0)
	{
		return new Material(shader_0);
	}

	internal static Transform smethod_2(Component component_0)
	{
		return component_0.transform;
	}

	internal static Vector3 smethod_3(Transform transform_0)
	{
		return transform_0.position;
	}

	internal static Quaternion smethod_4(Transform transform_0)
	{
		return transform_0.localRotation;
	}

	internal static Vector3 smethod_5(Transform transform_0)
	{
		return transform_0.localScale;
	}

	internal static bool smethod_6(Material material_0, int int_0)
	{
		return material_0.SetPass(int_0);
	}

	internal static void smethod_7()
	{
		GL.PushMatrix();
	}

	internal static void smethod_8(Matrix4x4 matrix4x4_0)
	{
		GL.MultMatrix(matrix4x4_0);
	}

	internal static void smethod_9(int int_0)
	{
		GL.Begin(int_0);
	}

	internal static void smethod_10(Color color_0)
	{
		GL.Color(color_0);
	}

	internal static void smethod_11(Vector3 vector3_0)
	{
		GL.Vertex(vector3_0);
	}

	internal static void smethod_12()
	{
		GL.End();
	}

	internal static void smethod_13()
	{
		GL.PopMatrix();
	}
}
