using System.Runtime.CompilerServices;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

namespace RuntimeInspectorNamespace;

public class BoundSlider : MonoBehaviour
{
	public delegate void OnValueChangedDelegate(BoundSlider source, float value);

	[SerializeField]
	private Slider jZIFr50VTsXC_0024VRvW02e2U4;

	[SerializeField]
	private Image Zy0022dIvFBPdseqEyD_0024McwX2bB5H5YzOhnUdb473flV;

	[SerializeField]
	private Image cgD0MiEZ_0024MDRoQ43zXgytAw;

	private bool iti1TRPLTiaG1_00241CZ_0024idGjY;

	private int tez8QKQVeFGVS4AMHMsbzyw;

	private UISkin E58c_5PzPLk6LleLXcBTp_0024M;

	public OnValueChangedDelegate OnValueChanged;

	public Slider BackingField => jZIFr50VTsXC_0024VRvW02e2U4;

	public bool IsFocused => iti1TRPLTiaG1_00241CZ_0024idGjY;

	public float Value
	{
		get
		{
			return smethod_0(jZIFr50VTsXC_0024VRvW02e2U4);
		}
		set
		{
			smethod_1(jZIFr50VTsXC_0024VRvW02e2U4, value);
		}
	}

	public UISkin Skin
	{
		get
		{
			return E58c_5PzPLk6LleLXcBTp_0024M;
		}
		set
		{
			if (smethod_2((Object)E58c_5PzPLk6LleLXcBTp_0024M, (Object)value) || tez8QKQVeFGVS4AMHMsbzyw != E58c_5PzPLk6LleLXcBTp_0024M.Version)
			{
				E58c_5PzPLk6LleLXcBTp_0024M = value;
				tez8QKQVeFGVS4AMHMsbzyw = E58c_5PzPLk6LleLXcBTp_0024M.Version;
				smethod_3((Graphic)Zy0022dIvFBPdseqEyD_0024McwX2bB5H5YzOhnUdb473flV, E58c_5PzPLk6LleLXcBTp_0024M.SliderBackgroundColor);
				smethod_3((Graphic)cgD0MiEZ_0024MDRoQ43zXgytAw, E58c_5PzPLk6LleLXcBTp_0024M.SliderThumbColor);
			}
		}
	}

	private void LfaiZbNwRELPQoddbWEUzjg()
	{
		PointerEventListener pointerEventListener = smethod_4((Component)jZIFr50VTsXC_0024VRvW02e2U4).AddComponent<PointerEventListener>();
		pointerEventListener.PointerDown += delegate
		{
			iti1TRPLTiaG1_00241CZ_0024idGjY = true;
		};
		pointerEventListener.PointerDown -= delegate
		{
			iti1TRPLTiaG1_00241CZ_0024idGjY = false;
		};
		smethod_5(jZIFr50VTsXC_0024VRvW02e2U4).AddListener(method_0);
	}

	public void SetRange(float min, float max)
	{
		if (min > max)
		{
			float num = min;
			min = max;
			max = num;
		}
		smethod_6(jZIFr50VTsXC_0024VRvW02e2U4, min);
		smethod_7(jZIFr50VTsXC_0024VRvW02e2U4, max);
	}

	private void method_0(float float_0)
	{
		if (iti1TRPLTiaG1_00241CZ_0024idGjY && OnValueChanged != null)
		{
			OnValueChanged(this, float_0);
		}
	}

	[CompilerGenerated]
	private void KrcQTfzecQWN8HUtkGym72A(PointerEventData pointerEventData_0)
	{
		iti1TRPLTiaG1_00241CZ_0024idGjY = false;
	}

	internal static float smethod_0(Slider slider_0)
	{
		return slider_0.value;
	}

	internal static void smethod_1(Slider slider_0, float float_0)
	{
		slider_0.value = float_0;
	}

	internal static bool smethod_2(Object object_0, Object object_1)
	{
		return object_0 != object_1;
	}

	internal static void smethod_3(Graphic graphic_0, Color color_0)
	{
		graphic_0.color = color_0;
	}

	internal static GameObject smethod_4(Component component_0)
	{
		return component_0.gameObject;
	}

	internal static Slider.SliderEvent smethod_5(Slider slider_0)
	{
		return slider_0.onValueChanged;
	}

	internal static void smethod_6(Slider slider_0, float float_0)
	{
		slider_0.minValue = float_0;
	}

	internal static void smethod_7(Slider slider_0, float float_0)
	{
		slider_0.maxValue = float_0;
	}
}
