using System;
using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using System.Runtime.CompilerServices;
using UnityEngine;
using UnityEngine.EventSystems;

namespace RuntimeInspectorNamespace;

public class DraggedReferenceSourceUI : MonoBehaviour, IPointerDownHandler, IEventSystemHandler, IPointerUpHandler, IBeginDragHandler
{
	[CompilerGenerated]
	private sealed class zltjuKcX691uISKavwlXretqXaFCI1qvAub0PBYd5LOgdCL32hjgKYh5I4HWBqeI2D3tX3iFFihdhcpZAsXx2rF6dNRb5N_86I5ODMxLROzMZZDno_p8lO4xtUO1g6Qkqg : IEnumerator<object>, IDisposable, IEnumerator
	{
		private int SjlBM8inVA_YE4YVlr_0024gluY;

		private object yT7HpVIzmqW54W307WgJtr4;

		public DraggedReferenceSourceUI SKCFxHGAEbVQbKCDB_0024Jj8p4;

		public PointerEventData pJ3lXGILhJ73vefu8VrFRWc;

		private float BBVsDz0o_M5_0024WT4cxopcFM7lR_0024VBo9x7C2D34COi5bhO;

		object IEnumerator<object>.Current
		{
			[DebuggerHidden]
			get
			{
				return yT7HpVIzmqW54W307WgJtr4;
			}
		}

		object IEnumerator.Current
		{
			[DebuggerHidden]
			get
			{
				return yT7HpVIzmqW54W307WgJtr4;
			}
		}

		[DebuggerHidden]
		public zltjuKcX691uISKavwlXretqXaFCI1qvAub0PBYd5LOgdCL32hjgKYh5I4HWBqeI2D3tX3iFFihdhcpZAsXx2rF6dNRb5N_86I5ODMxLROzMZZDno_p8lO4xtUO1g6Qkqg(int int_0)
		{
			SjlBM8inVA_YE4YVlr_0024gluY = int_0;
		}

		[DebuggerHidden]
		void IDisposable.Dispose()
		{
			SjlBM8inVA_YE4YVlr_0024gluY = -2;
		}

		private bool MoveNext()
		{
			int num = SjlBM8inVA_YE4YVlr_0024gluY;
			DraggedReferenceSourceUI draggedReferenceSourceUI = SKCFxHGAEbVQbKCDB_0024Jj8p4;
			switch (num)
			{
			default:
				return false;
			case 1:
				SjlBM8inVA_YE4YVlr_0024gluY = -1;
				if (smethod_3(draggedReferenceSourceUI.NfNLpq6TEVx7X05RbDEMYkc) && (smethod_4(pJ3lXGILhJ73vefu8VrFRWc) - smethod_5(pJ3lXGILhJ73vefu8VrFRWc)).sqrMagnitude < BBVsDz0o_M5_0024WT4cxopcFM7lR_0024VBo9x7C2D34COi5bhO * BBVsDz0o_M5_0024WT4cxopcFM7lR_0024VBo9x7C2D34COi5bhO)
				{
					RuntimeInspectorUtils.CreateDraggedReferenceItem(draggedReferenceSourceUI.NfNLpq6TEVx7X05RbDEMYkc, pJ3lXGILhJ73vefu8VrFRWc, draggedReferenceSourceUI.Tk87AWktKBPxlXEouSMMrptEbcDu7sFaDRPwE_Y0IKcg, draggedReferenceSourceUI.GetComponentInParent<Canvas>());
				}
				return false;
			case 0:
				SjlBM8inVA_YE4YVlr_0024gluY = -1;
				BBVsDz0o_M5_0024WT4cxopcFM7lR_0024VBo9x7C2D34COi5bhO = smethod_1(smethod_0());
				yT7HpVIzmqW54W307WgJtr4 = smethod_2(draggedReferenceSourceUI.float_0);
				SjlBM8inVA_YE4YVlr_0024gluY = 1;
				return true;
			}
		}

		bool IEnumerator.MoveNext()
		{
			//ILSpy generated this explicit interface implementation from .override directive in MoveNext
			return this.MoveNext();
		}

		[DebuggerHidden]
		void IEnumerator.Reset()
		{
			throw smethod_6();
		}

		internal static EventSystem smethod_0()
		{
			return EventSystem.current;
		}

		internal static int smethod_1(EventSystem eventSystem_0)
		{
			return eventSystem_0.pixelDragThreshold;
		}

		internal static WaitForSecondsRealtime smethod_2(float float_0)
		{
			return new WaitForSecondsRealtime(float_0);
		}

		internal static bool smethod_3(UnityEngine.Object object_0)
		{
			return object_0;
		}

		internal static Vector2 smethod_4(PointerEventData pointerEventData_0)
		{
			return pointerEventData_0.position;
		}

		internal static Vector2 smethod_5(PointerEventData pointerEventData_0)
		{
			return pointerEventData_0.pressPosition;
		}

		internal static NotSupportedException smethod_6()
		{
			return new NotSupportedException();
		}
	}

	[SerializeField]
	private UnityEngine.Object NfNLpq6TEVx7X05RbDEMYkc;

	[SerializeField]
	private UISkin Tk87AWktKBPxlXEouSMMrptEbcDu7sFaDRPwE_Y0IKcg;

	[SerializeField]
	private float float_0 = 0.4f;

	private IEnumerator XnTU_j42lE4Lef1dJMU8iRO9gY6doHIDXWo0JMBU_0024LGx;

	public UnityEngine.Object Reference
	{
		get
		{
			return NfNLpq6TEVx7X05RbDEMYkc;
		}
		set
		{
			NfNLpq6TEVx7X05RbDEMYkc = value;
		}
	}

	public void OnPointerDown(PointerEventData eventData)
	{
		if (XnTU_j42lE4Lef1dJMU8iRO9gY6doHIDXWo0JMBU_0024LGx == null && smethod_0(NfNLpq6TEVx7X05RbDEMYkc))
		{
			XnTU_j42lE4Lef1dJMU8iRO9gY6doHIDXWo0JMBU_0024LGx = Bv8vJC8lVqXjYjfl5aR6P0iW2ZJAZ_VAz5W8e9TK1Y0K(eventData);
			smethod_1((MonoBehaviour)this, XnTU_j42lE4Lef1dJMU8iRO9gY6doHIDXWo0JMBU_0024LGx);
		}
	}

	public void OnPointerUp(PointerEventData eventData)
	{
		if (XnTU_j42lE4Lef1dJMU8iRO9gY6doHIDXWo0JMBU_0024LGx != null)
		{
			smethod_2((MonoBehaviour)this, XnTU_j42lE4Lef1dJMU8iRO9gY6doHIDXWo0JMBU_0024LGx);
			XnTU_j42lE4Lef1dJMU8iRO9gY6doHIDXWo0JMBU_0024LGx = null;
		}
	}

	public void OnBeginDrag(PointerEventData eventData)
	{
		if (XnTU_j42lE4Lef1dJMU8iRO9gY6doHIDXWo0JMBU_0024LGx != null)
		{
			smethod_2((MonoBehaviour)this, XnTU_j42lE4Lef1dJMU8iRO9gY6doHIDXWo0JMBU_0024LGx);
			XnTU_j42lE4Lef1dJMU8iRO9gY6doHIDXWo0JMBU_0024LGx = null;
		}
	}

	private IEnumerator Bv8vJC8lVqXjYjfl5aR6P0iW2ZJAZ_VAz5W8e9TK1Y0K(PointerEventData pointerEventData_0)
	{
		float num = zltjuKcX691uISKavwlXretqXaFCI1qvAub0PBYd5LOgdCL32hjgKYh5I4HWBqeI2D3tX3iFFihdhcpZAsXx2rF6dNRb5N_86I5ODMxLROzMZZDno_p8lO4xtUO1g6Qkqg.smethod_1(zltjuKcX691uISKavwlXretqXaFCI1qvAub0PBYd5LOgdCL32hjgKYh5I4HWBqeI2D3tX3iFFihdhcpZAsXx2rF6dNRb5N_86I5ODMxLROzMZZDno_p8lO4xtUO1g6Qkqg.smethod_0());
		yield return zltjuKcX691uISKavwlXretqXaFCI1qvAub0PBYd5LOgdCL32hjgKYh5I4HWBqeI2D3tX3iFFihdhcpZAsXx2rF6dNRb5N_86I5ODMxLROzMZZDno_p8lO4xtUO1g6Qkqg.smethod_2(float_0);
		if (zltjuKcX691uISKavwlXretqXaFCI1qvAub0PBYd5LOgdCL32hjgKYh5I4HWBqeI2D3tX3iFFihdhcpZAsXx2rF6dNRb5N_86I5ODMxLROzMZZDno_p8lO4xtUO1g6Qkqg.smethod_3(NfNLpq6TEVx7X05RbDEMYkc) && (zltjuKcX691uISKavwlXretqXaFCI1qvAub0PBYd5LOgdCL32hjgKYh5I4HWBqeI2D3tX3iFFihdhcpZAsXx2rF6dNRb5N_86I5ODMxLROzMZZDno_p8lO4xtUO1g6Qkqg.smethod_4(pointerEventData_0) - zltjuKcX691uISKavwlXretqXaFCI1qvAub0PBYd5LOgdCL32hjgKYh5I4HWBqeI2D3tX3iFFihdhcpZAsXx2rF6dNRb5N_86I5ODMxLROzMZZDno_p8lO4xtUO1g6Qkqg.smethod_5(pointerEventData_0)).sqrMagnitude < num * num)
		{
			RuntimeInspectorUtils.CreateDraggedReferenceItem(NfNLpq6TEVx7X05RbDEMYkc, pointerEventData_0, Tk87AWktKBPxlXEouSMMrptEbcDu7sFaDRPwE_Y0IKcg, GetComponentInParent<Canvas>());
		}
	}

	internal static bool smethod_0(UnityEngine.Object object_0)
	{
		return object_0;
	}

	internal static Coroutine smethod_1(MonoBehaviour monoBehaviour_0, IEnumerator ienumerator_0)
	{
		return monoBehaviour_0.StartCoroutine(ienumerator_0);
	}

	internal static void smethod_2(MonoBehaviour monoBehaviour_0, IEnumerator ienumerator_0)
	{
		monoBehaviour_0.StopCoroutine(ienumerator_0);
	}
}
