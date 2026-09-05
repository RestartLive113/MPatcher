using System;
using System.Collections.Generic;
using UnityEngine;

internal static class x2KzkW9JuGQXmzuCdgytKZhtDl_Q4F_kgfJY01G7qH2bwnaHFpw66hwDQa0zL5zVOw
{
	private static Dictionary<string, Mesh> dictionary_0 = new Dictionary<string, Mesh>();

	internal static GameObject o1Rvqe6BzXX706LinpRkiNE(int int_0 = 10, float float_0 = 0f, float float_1 = 1f, float float_2 = 1f, float float_3 = 0f, bool bool_0 = true, bool bool_1 = false, bool bool_2 = false)
	{
		GameObject gameObject = smethod_0(global::_003CModule_003E.smethod_28<string>(135228744u));
		if (float_3 > 0f && float_3 < 180f)
		{
			float_0 = 0f;
			float_1 = float_2 * Mathf.Tan(float_3 * ((float)Math.PI / 180f) / 2f);
		}
		string text = smethod_1((UnityEngine.Object)gameObject) + int_0 + global::_003CModule_003E.smethod_26<string>(864111896u) + float_0 + global::_003CModule_003E.smethod_28<string>(4065956436u) + float_1 + global::_003CModule_003E.smethod_27<string>(536797717u) + float_2 + global::_003CModule_003E.smethod_26<string>(1184879334u) + float_2 + (bool_0 ? global::_003CModule_003E.smethod_28<string>(2138546566u) : "") + (bool_1 ? global::_003CModule_003E.smethod_28<string>(1956426764u) : "");
		Mesh mesh = null;
		if (dictionary_0.ContainsKey(text))
		{
			mesh = dictionary_0[text];
		}
		if (mesh == null)
		{
			mesh = new Mesh();
			mesh.name = text;
			int num = (bool_0 ? 1 : 0) + (bool_1 ? 1 : 0);
			int num2 = ((bool_0 && bool_1) ? (2 * int_0) : 0);
			Vector3[] array = new Vector3[2 * num * int_0];
			Vector3[] array2 = new Vector3[2 * num * int_0];
			Vector2[] array3 = new Vector2[2 * num * int_0];
			float f = Mathf.Atan((float_1 - float_0) / float_2);
			float num3 = Mathf.Sin(f);
			float num4 = Mathf.Cos(f);
			for (int i = 0; i < int_0; i++)
			{
				float f2 = (float)Math.PI * 2f * (float)i / (float)int_0;
				float num5 = Mathf.Sin(f2);
				float num6 = Mathf.Cos(f2);
				float f3 = (float)Math.PI * 2f * ((float)i + 0.5f) / (float)int_0;
				float num7 = Mathf.Sin(f3);
				float num8 = Mathf.Cos(f3);
				array[i] = new Vector3(float_0 * num6, float_0 * num5, 0f);
				array[i + int_0] = new Vector3(float_1 * num6, float_1 * num5, float_2);
				if (float_0 == 0f)
				{
					array2[i] = new Vector3(num8 * num4, num7 * num4, 0f - num3);
				}
				else
				{
					array2[i] = new Vector3(num6 * num4, num5 * num4, 0f - num3);
				}
				if (float_1 != 0f)
				{
					array2[i + int_0] = new Vector3(num6 * num4, num5 * num4, 0f - num3);
				}
				else
				{
					array2[i + int_0] = new Vector3(num8 * num4, num7 * num4, 0f - num3);
				}
				array3[i] = new Vector2(1f * (float)i / (float)int_0, 1f);
				array3[i + int_0] = new Vector2(1f * (float)i / (float)int_0, 0f);
				if (bool_0 && bool_1)
				{
					array[i + 2 * int_0] = array[i];
					array[i + 3 * int_0] = array[i + int_0];
					array3[i + 2 * int_0] = array3[i];
					array3[i + 3 * int_0] = array3[i + int_0];
				}
				if (bool_1)
				{
					array2[i + num2] = -array2[i];
					array2[i + int_0 + num2] = -array2[i + int_0];
				}
			}
			mesh.vertices = array;
			mesh.normals = array2;
			mesh.uv = array3;
			int num9 = 0;
			int[] array4;
			if (float_0 == 0f)
			{
				array4 = new int[int_0 * 3 * num];
				if (bool_0)
				{
					for (int i = 0; i < int_0; i++)
					{
						array4[num9++] = i + int_0;
						array4[num9++] = i;
						if (i != int_0 - 1)
						{
							array4[num9++] = i + 1 + int_0;
						}
						else
						{
							array4[num9++] = int_0;
						}
					}
				}
				if (bool_1)
				{
					for (int i = num2; i < int_0 + num2; i++)
					{
						array4[num9++] = i;
						array4[num9++] = i + int_0;
						if (i == int_0 - 1 + num2)
						{
							array4[num9++] = int_0 + num2;
						}
						else
						{
							array4[num9++] = i + 1 + int_0;
						}
					}
				}
			}
			else if (float_1 == 0f)
			{
				array4 = new int[int_0 * 3 * num];
				if (bool_0)
				{
					for (int i = 0; i < int_0; i++)
					{
						array4[num9++] = i;
						if (i == int_0 - 1)
						{
							array4[num9++] = 0;
						}
						else
						{
							array4[num9++] = i + 1;
						}
						array4[num9++] = i + int_0;
					}
				}
				if (bool_1)
				{
					for (int i = num2; i < int_0 + num2; i++)
					{
						if (i == int_0 - 1 + num2)
						{
							array4[num9++] = num2;
						}
						else
						{
							array4[num9++] = i + 1;
						}
						array4[num9++] = i;
						array4[num9++] = i + int_0;
					}
				}
			}
			else
			{
				array4 = new int[int_0 * 6 * num];
				if (bool_0)
				{
					for (int i = 0; i < int_0; i++)
					{
						int num10 = i + 1;
						if (num10 == int_0)
						{
							num10 = 0;
						}
						array4[num9++] = i;
						array4[num9++] = num10;
						array4[num9++] = i + int_0;
						array4[num9++] = num10 + int_0;
						array4[num9++] = i + int_0;
						array4[num9++] = num10;
					}
				}
				if (bool_1)
				{
					for (int i = num2; i < int_0 + num2; i++)
					{
						int num11 = i + 1;
						if (num11 == int_0 + num2)
						{
							num11 = num2;
						}
						array4[num9++] = num11;
						array4[num9++] = i;
						array4[num9++] = i + int_0;
						array4[num9++] = i + int_0;
						array4[num9++] = num11 + int_0;
						array4[num9++] = num11;
					}
				}
			}
			mesh.triangles = array4;
			dictionary_0.Add(text, mesh);
		}
		MeshFilter meshFilter = gameObject.AddComponent<MeshFilter>();
		meshFilter.mesh = mesh;
		gameObject.AddComponent<MeshRenderer>();
		if (bool_2)
		{
			gameObject.AddComponent<MeshCollider>().sharedMesh = meshFilter.sharedMesh;
		}
		return gameObject;
	}

	internal static GameObject smethod_0(string string_0)
	{
		return new GameObject(string_0);
	}

	internal static string smethod_1(UnityEngine.Object object_0)
	{
		return object_0.name;
	}
}
