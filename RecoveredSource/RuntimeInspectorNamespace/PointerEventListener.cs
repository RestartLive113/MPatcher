using System;
using System.Runtime.CompilerServices;
using System.Threading;
using UnityEngine;
using UnityEngine.EventSystems;

namespace RuntimeInspectorNamespace;

public class PointerEventListener : MonoBehaviour, IPointerDownHandler, IEventSystemHandler, IPointerUpHandler, IPointerClickHandler
{
	public delegate void PointerEvent(PointerEventData eventData);

	[CompilerGenerated]
	private PointerEvent K3D0Ph_edPHwy39bngS0ius;

	[CompilerGenerated]
	private PointerEvent zDK9iiF6kr1_0024H3XdZqf0cy4;

	[CompilerGenerated]
	private PointerEvent q6yYLQgc7S5_BNTzmcPTu_w;

	public event PointerEvent PointerDown
	{
		[CompilerGenerated]
		add
		{
			PointerEvent pointerEvent = K3D0Ph_edPHwy39bngS0ius;
			PointerEvent pointerEvent2;
			do
			{
				pointerEvent2 = pointerEvent;
				PointerEvent value2 = (PointerEvent)smethod_0((Delegate)pointerEvent2, (Delegate)value);
				pointerEvent = Interlocked.CompareExchange(ref K3D0Ph_edPHwy39bngS0ius, value2, pointerEvent2);
			}
			while ((object)pointerEvent != pointerEvent2);
		}
		[CompilerGenerated]
		remove
		{
			PointerEvent pointerEvent = K3D0Ph_edPHwy39bngS0ius;
			PointerEvent pointerEvent2;
			do
			{
				pointerEvent2 = pointerEvent;
				PointerEvent value2 = (PointerEvent)smethod_1((Delegate)pointerEvent2, (Delegate)value);
				pointerEvent = Interlocked.CompareExchange(ref K3D0Ph_edPHwy39bngS0ius, value2, pointerEvent2);
			}
			while ((object)pointerEvent != pointerEvent2);
		}
	}

	public event PointerEvent PointerUp
	{
		[CompilerGenerated]
		add
		{
			PointerEvent pointerEvent = zDK9iiF6kr1_0024H3XdZqf0cy4;
			PointerEvent pointerEvent2;
			do
			{
				pointerEvent2 = pointerEvent;
				PointerEvent value2 = (PointerEvent)smethod_0((Delegate)pointerEvent2, (Delegate)value);
				pointerEvent = Interlocked.CompareExchange(ref zDK9iiF6kr1_0024H3XdZqf0cy4, value2, pointerEvent2);
			}
			while ((object)pointerEvent != pointerEvent2);
		}
		[CompilerGenerated]
		remove
		{
			PointerEvent pointerEvent = zDK9iiF6kr1_0024H3XdZqf0cy4;
			PointerEvent pointerEvent2;
			do
			{
				pointerEvent2 = pointerEvent;
				PointerEvent value2 = (PointerEvent)smethod_1((Delegate)pointerEvent2, (Delegate)value);
				pointerEvent = Interlocked.CompareExchange(ref zDK9iiF6kr1_0024H3XdZqf0cy4, value2, pointerEvent2);
			}
			while ((object)pointerEvent != pointerEvent2);
		}
	}

	public event PointerEvent PointerClick
	{
		[CompilerGenerated]
		add
		{
			PointerEvent pointerEvent = q6yYLQgc7S5_BNTzmcPTu_w;
			PointerEvent pointerEvent2;
			do
			{
				pointerEvent2 = pointerEvent;
				PointerEvent value2 = (PointerEvent)smethod_0((Delegate)pointerEvent2, (Delegate)value);
				pointerEvent = Interlocked.CompareExchange(ref q6yYLQgc7S5_BNTzmcPTu_w, value2, pointerEvent2);
			}
			while ((object)pointerEvent != pointerEvent2);
		}
		[CompilerGenerated]
		remove
		{
			PointerEvent pointerEvent = q6yYLQgc7S5_BNTzmcPTu_w;
			PointerEvent pointerEvent2;
			do
			{
				pointerEvent2 = pointerEvent;
				PointerEvent value2 = (PointerEvent)smethod_1((Delegate)pointerEvent2, (Delegate)value);
				pointerEvent = Interlocked.CompareExchange(ref q6yYLQgc7S5_BNTzmcPTu_w, value2, pointerEvent2);
			}
			while ((object)pointerEvent != pointerEvent2);
		}
	}

	void IPointerDownHandler.OnPointerDown(PointerEventData eventData)
	{
		if (K3D0Ph_edPHwy39bngS0ius != null)
		{
			K3D0Ph_edPHwy39bngS0ius(eventData);
		}
	}

	void IPointerUpHandler.OnPointerUp(PointerEventData eventData)
	{
		if (zDK9iiF6kr1_0024H3XdZqf0cy4 != null)
		{
			zDK9iiF6kr1_0024H3XdZqf0cy4(eventData);
		}
	}

	void IPointerClickHandler.OnPointerClick(PointerEventData eventData)
	{
		if (q6yYLQgc7S5_BNTzmcPTu_w != null)
		{
			q6yYLQgc7S5_BNTzmcPTu_w(eventData);
		}
	}

	internal static Delegate smethod_0(Delegate delegate_0, Delegate delegate_1)
	{
		return Delegate.Combine(delegate_0, delegate_1);
	}

	internal static Delegate smethod_1(Delegate delegate_0, Delegate delegate_1)
	{
		return Delegate.Remove(delegate_0, delegate_1);
	}
}
