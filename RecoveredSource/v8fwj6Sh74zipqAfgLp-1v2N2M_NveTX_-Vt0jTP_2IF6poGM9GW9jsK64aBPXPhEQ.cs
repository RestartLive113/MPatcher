using System;
using System.Collections;
using System.Collections.Generic;
using MPatchrMain;
using McnCraft;
using UnityEngine;

internal static class v8fwj6Sh74zipqAfgLp_00241v2N2M_NveTX__0024Vt0jTP_2IF6poGM9GW9jsK64aBPXPhEQ
{
	internal static bool RBtLBR0TJy8mcKdKSpTYNhZDhiIRgOUyUaJ85WWtWKVR = false;

	private static quoQt4rDIgY_dfcYMj4e8Gr8OG953LjeBzZL9TDoVf_0024j aCdVsgGLPyujYKCrUl3Dd6o;

	private static List<Transform> NxDvinX4pnS1zXGt2D0N4Bc = new List<Transform>();

	public static Transform LzXdqc9Uiv1Wlp_mqlLQlUc(this Transform transform_0, string string_0)
	{
		Queue<Transform> queue = new Queue<Transform>();
		queue.Enqueue(transform_0);
		Transform transform;
		while (true)
		{
			if (queue.Count > 0)
			{
				transform = queue.Dequeue();
				if (smethod_1(smethod_0((UnityEngine.Object)transform), string_0))
				{
					break;
				}
				IEnumerator enumerator = smethod_2(transform);
				try
				{
					while (smethod_4(enumerator))
					{
						Transform item = (Transform)smethod_3(enumerator);
						queue.Enqueue(item);
					}
				}
				finally
				{
					if (enumerator is IDisposable idisposable_)
					{
						smethod_5(idisposable_);
					}
				}
				continue;
			}
			return null;
		}
		return transform;
	}

	internal static void REBI1skbtl8UFDgYriSYqvg()
	{
		if (NxDvinX4pnS1zXGt2D0N4Bc.Count > 0 && smethod_6((UnityEngine.Object)NxDvinX4pnS1zXGt2D0N4Bc[0], (UnityEngine.Object)null))
		{
			NxDvinX4pnS1zXGt2D0N4Bc.Clear();
			aCdVsgGLPyujYKCrUl3Dd6o = null;
		}
		if (aCdVsgGLPyujYKCrUl3Dd6o == null)
		{
			return;
		}
		foreach (Transform key in aCdVsgGLPyujYKCrUl3Dd6o.wY6if0_6Qx7z2kZcmYsBWkk.Keys)
		{
			if (smethod_7((UnityEngine.Object)key.GetComponent<Rigidbody>(), (UnityEngine.Object)null))
			{
				smethod_8((UnityEngine.Object)key);
			}
		}
	}

	internal static void wB31kBoLrv4uk3XZ1aC0Eqo(Transform transform_0, Transform transform_1)
	{
		smethod_9(Arena.OEDCBNHNGMJ, bool_0: true, bool_1: false);
		z9_pDiVt_00240lkJ2DTHRVVfFk(transform_1, null);
		NxDvinX4pnS1zXGt2D0N4Bc.Reverse();
		List<Transform> list = new List<Transform>();
		Vector3 euler = Vector3.zero;
		for (int i = 0; i < NxDvinX4pnS1zXGt2D0N4Bc.Count; i++)
		{
			Transform transform_2 = NxDvinX4pnS1zXGt2D0N4Bc[i];
			Transform transform = smethod_10(transform_2.LzXdqc9Uiv1Wlp_mqlLQlUc(global::_003CModule_003E.smethod_29<string>(278194873u)));
			Transform transform2 = smethod_11(transform_2, global::_003CModule_003E.smethod_27<string>(2970538013u));
			if (i != 0)
			{
				transform2 = transform;
			}
			MeshRenderer component = transform2.GetComponent<MeshRenderer>();
			Vector3 zero = Vector3.zero;
			zero = ((!smethod_6((UnityEngine.Object)component, (UnityEngine.Object)null)) ? smethod_13((Renderer)component).center : smethod_12(transform2));
			if (i != NxDvinX4pnS1zXGt2D0N4Bc.Count - 1)
			{
				euler = (NxDvinX4pnS1zXGt2D0N4Bc[i].position - NxDvinX4pnS1zXGt2D0N4Bc[i + 1].position).normalized;
			}
			GameObject gameObject = new GameObject(global::_003CModule_003E.smethod_25<string>(2347716656u) + i);
			GameObject gameObject2 = GameObject.CreatePrimitive(PrimitiveType.Sphere);
			gameObject2.transform.SetParent(gameObject.transform);
			gameObject2.transform.localPosition = Vector3.zero;
			gameObject2.GetComponent<MeshRenderer>().enabled = false;
			gameObject.transform.position = zero;
			gameObject.transform.rotation = Quaternion.Euler(euler);
			if (list.Count > 0)
			{
				gameObject.transform.SetParent(list[list.Count - 1]);
			}
			list.Add(gameObject.transform);
			NxDvinX4pnS1zXGt2D0N4Bc[i].SetParent(gameObject.transform);
		}
		HingeJoint[] componentsInChildren = list[0].GetComponentsInChildren<HingeJoint>();
		for (int j = 0; j < componentsInChildren.Length; j++)
		{
			UnityEngine.Object.Destroy(componentsInChildren[j]);
		}
		SimpleIKSolver simpleIKSolver = list[0].gameObject.AddComponent<SimpleIKSolver>();
		List<SimpleIKSolver.JointEntity> list2 = new List<SimpleIKSolver.JointEntity>();
		foreach (Transform item in list)
		{
			list2.Add(new SimpleIKSolver.JointEntity(item));
		}
		simpleIKSolver.Target = transform_0;
		simpleIKSolver.JointEntities = list2.ToArray();
		aCdVsgGLPyujYKCrUl3Dd6o = new quoQt4rDIgY_dfcYMj4e8Gr8OG953LjeBzZL9TDoVf_0024j(NxDvinX4pnS1zXGt2D0N4Bc, list, transform_1);
	}

	internal static Transform He4K_0M3uErbrHIgPi2YaTlUCAb5SpeeT0Kmx_VD20fR(Vector3 vector3_0)
	{
		float num = -1f;
		Transform transform = null;
		Transform[] componentsInChildren = MPatchr.A_yjdMZQtUOdoTXKT3B_Is62e0jep9fvy4aqoNcLHKgj().GetComponentsInChildren<Transform>();
		foreach (Transform transform2 in componentsInChildren)
		{
			if (smethod_14(smethod_0((UnityEngine.Object)transform2), global::_003CModule_003E.smethod_25<string>(4144833717u)))
			{
				float num2 = Vector3.Distance(vector3_0, smethod_12(transform2));
				if (smethod_6((UnityEngine.Object)transform, (UnityEngine.Object)null) || num2 < num)
				{
					num = Vector3.Distance(vector3_0, smethod_12(transform2));
					transform = transform2;
				}
			}
		}
		return transform;
	}

	private static void z9_pDiVt_00240lkJ2DTHRVVfFk(Transform transform_0, Transform transform_1, Transform transform_2 = null)
	{
		if (smethod_6((UnityEngine.Object)transform_2, (UnityEngine.Object)null))
		{
			transform_2 = smethod_15(MPatchr.A_yjdMZQtUOdoTXKT3B_Is62e0jep9fvy4aqoNcLHKgj());
		}
		if (smethod_6((UnityEngine.Object)transform_0, (UnityEngine.Object)transform_2) || smethod_7((UnityEngine.Object)transform_0.GetComponent<MachineSerializer>(), (UnityEngine.Object)null) || smethod_6((UnityEngine.Object)transform_0.GetComponent<BodyController>(), (UnityEngine.Object)null) || smethod_7((UnityEngine.Object)transform_0.GetComponent<MachineController>(), (UnityEngine.Object)null))
		{
			return;
		}
		if (smethod_16((UnityEngine.Object)smethod_11(transform_0, global::_003CModule_003E.smethod_25<string>(113633563u))))
		{
			NxDvinX4pnS1zXGt2D0N4Bc.Add(transform_0);
		}
		JointController[] componentsInChildren = transform_0.GetComponentsInChildren<JointController>();
		foreach (JointController jointController in componentsInChildren)
		{
			Transform transform = jointController.NDFBMKMPJHA;
			while (!smethod_14(smethod_0((UnityEngine.Object)transform), global::_003CModule_003E.smethod_25<string>(814366737u)) && smethod_7((UnityEngine.Object)transform, (UnityEngine.Object)transform_2))
			{
				transform = smethod_10(transform);
			}
			z9_pDiVt_00240lkJ2DTHRVVfFk(transform, transform_0, transform_2);
			smethod_17((Behaviour)jointController, bool_0: false);
			smethod_8((UnityEngine.Object)jointController.ALGHGLCBLDJ);
		}
	}

	internal static string smethod_0(UnityEngine.Object object_0)
	{
		return object_0.name;
	}

	internal static bool smethod_1(string string_0, string string_1)
	{
		return string_0 == string_1;
	}

	internal static IEnumerator smethod_2(Transform transform_0)
	{
		return transform_0.GetEnumerator();
	}

	internal static object smethod_3(IEnumerator ienumerator_0)
	{
		return ienumerator_0.Current;
	}

	internal static bool smethod_4(IEnumerator ienumerator_0)
	{
		return ienumerator_0.MoveNext();
	}

	internal static void smethod_5(IDisposable idisposable_0)
	{
		idisposable_0.Dispose();
	}

	internal static bool smethod_6(UnityEngine.Object object_0, UnityEngine.Object object_1)
	{
		return object_0 == object_1;
	}

	internal static bool smethod_7(UnityEngine.Object object_0, UnityEngine.Object object_1)
	{
		return object_0 != object_1;
	}

	internal static void smethod_8(UnityEngine.Object object_0)
	{
		UnityEngine.Object.Destroy(object_0);
	}

	internal static void smethod_9(Arena arena_0, bool bool_0, bool bool_1)
	{
		arena_0.LockSelf(bool_0, bool_1);
	}

	internal static Transform smethod_10(Transform transform_0)
	{
		return transform_0.parent;
	}

	internal static Transform smethod_11(Transform transform_0, string string_0)
	{
		return transform_0.Find(string_0);
	}

	internal static Vector3 smethod_12(Transform transform_0)
	{
		return transform_0.position;
	}

	internal static Bounds smethod_13(Renderer renderer_0)
	{
		return renderer_0.bounds;
	}

	internal static bool smethod_14(string string_0, string string_1)
	{
		return string_0.Contains(string_1);
	}

	internal static Transform smethod_15(GameObject gameObject_0)
	{
		return gameObject_0.transform;
	}

	internal static bool smethod_16(UnityEngine.Object object_0)
	{
		return object_0;
	}

	internal static void smethod_17(Behaviour behaviour_0, bool bool_0)
	{
		behaviour_0.enabled = bool_0;
	}
}
