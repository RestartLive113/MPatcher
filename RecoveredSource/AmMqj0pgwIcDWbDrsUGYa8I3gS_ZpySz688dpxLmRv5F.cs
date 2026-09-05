using System;
using System.Collections.Generic;
using UnityEngine;

internal class AmMqj0pgwIcDWbDrsUGYa8I3gS_ZpySz688dpxLmRv5F : MonoBehaviour
{
	private Action<AmMqj0pgwIcDWbDrsUGYa8I3gS_ZpySz688dpxLmRv5F> sFAd3zIvl7HvFNXQC8XqeKU;

	private Action<AmMqj0pgwIcDWbDrsUGYa8I3gS_ZpySz688dpxLmRv5F> m_t_zrTdQKbKIXwXxX7i_vc;

	private Dictionary<string, object> n4Zu0mYSlHwlumVbpLN8LpQ = new Dictionary<string, object>();

	internal void Start()
	{
		if (sFAd3zIvl7HvFNXQC8XqeKU != null)
		{
			sFAd3zIvl7HvFNXQC8XqeKU(this);
		}
	}

	internal void Update()
	{
		if (m_t_zrTdQKbKIXwXxX7i_vc != null)
		{
			m_t_zrTdQKbKIXwXxX7i_vc(this);
		}
	}

	internal bool AqTKGFxfR1r6eAzrvm4_0024bck(string string_0)
	{
		return n4Zu0mYSlHwlumVbpLN8LpQ.ContainsKey(string_0);
	}

	internal void clf0br4v0HxmaJWIcm_0024_0024GUg<T>(string string_0, T gparam_0)
	{
		if (!n4Zu0mYSlHwlumVbpLN8LpQ.ContainsKey(string_0))
		{
			n4Zu0mYSlHwlumVbpLN8LpQ.Add(string_0, gparam_0);
		}
		else
		{
			n4Zu0mYSlHwlumVbpLN8LpQ[string_0] = gparam_0;
		}
	}

	internal T hpiqzm2jQTswCo32f7jvrQ4<T>(string string_0)
	{
		if (!n4Zu0mYSlHwlumVbpLN8LpQ.ContainsKey(string_0))
		{
			return default(T);
		}
		return (T)n4Zu0mYSlHwlumVbpLN8LpQ[string_0];
	}

	internal void method_0(Action<AmMqj0pgwIcDWbDrsUGYa8I3gS_ZpySz688dpxLmRv5F> action_0)
	{
		sFAd3zIvl7HvFNXQC8XqeKU = action_0;
	}

	internal void U_0024eIoX6e3N9Ag_us_EcGHBI(Action<AmMqj0pgwIcDWbDrsUGYa8I3gS_ZpySz688dpxLmRv5F> action_0)
	{
		m_t_zrTdQKbKIXwXxX7i_vc = action_0;
	}
}
