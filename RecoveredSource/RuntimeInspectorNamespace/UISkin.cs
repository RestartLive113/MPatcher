using UnityEngine;

namespace RuntimeInspectorNamespace;

[CreateAssetMenu(fileName = "UI Skin", menuName = "RuntimeInspector/UI Skin", order = 111)]
public class UISkin : ScriptableObject
{
	private int pv_zWcL3Z_0024cdK2wgj_OVSJY;

	[SerializeField]
	private Font font_0;

	[SerializeField]
	private int Quht5twPqqDxYXWYPv820GQ = 12;

	[SerializeField]
	private int ZXZ_00241iylFvWJ4Q4SGcz737o = 30;

	[SerializeField]
	private int int_0 = 12;

	[SerializeField]
	private float float_0 = 0.4f;

	[SerializeField]
	private Color ZT41isQqk7h5Q8lp1eMtJso = Color.grey;

	[SerializeField]
	private Color y2iCsisUyAyUrDTNOE1cRViuAZVO3M2EKdQFPDB8O_0024ud = Color.grey;

	[SerializeField]
	private Color RP7V4KJGCG5T1nzNedMY1Es = Color.black;

	[SerializeField]
	private Color RxEsUXR1Tq1wJdBIP3zXzuIwBwutsiBL4AumePPYN_zq = Color.black;

	[SerializeField]
	private Color Tldg_0024DrVRxWZolOeKY1vCj2Jp0MGzp5HJeUVjqqoSPy9 = Color.black;

	[SerializeField]
	private Color DMDqZyAqLrv_k3MRewLF_W2pLTfUHcq2RS8ZplzfTS_0024zOEcudIOE8FyWVd45x7CY7A = Color.white;

	[SerializeField]
	private Color color_0 = Color.red;

	[SerializeField]
	private Color ZFZuqc1jR64KLC2jhe6RFIVbkojsWMv1wKmQ3qoBGkLA = Color.black;

	[SerializeField]
	private Color qKhmGNgmeLQAbpgDevmsf2UB6AvB_0024R3WVLcHh8gkdyQ2 = Color.black;

	[SerializeField]
	private Color Sun4sokbU_0024ABkXUtABpMhAw3Tv4ap_afGSWbt6jgqC2X = Color.white;

	[SerializeField]
	private Color hHcsg7fc9LZ2_0024mksjlSka2aH8EOv04RLP7xOxj9HXfZf = Color.black;

	[SerializeField]
	private Color jbftbGrrCuUvUOduAZlg0sLwK2YUS2T3eZ6YLUWqdV08 = Color.white;

	[SerializeField]
	private Color color_1 = Color.black;

	[SerializeField]
	private Color color_2 = Color.blue;

	[SerializeField]
	private Color color_3 = Color.black;

	public int Version => pv_zWcL3Z_0024cdK2wgj_OVSJY;

	public Font Font
	{
		get
		{
			return font_0;
		}
		set
		{
			if (smethod_1((Object)font_0, (Object)value))
			{
				font_0 = value;
				pv_zWcL3Z_0024cdK2wgj_OVSJY++;
			}
		}
	}

	public int FontSize
	{
		get
		{
			return Quht5twPqqDxYXWYPv820GQ;
		}
		set
		{
			if (Quht5twPqqDxYXWYPv820GQ != value)
			{
				Quht5twPqqDxYXWYPv820GQ = value;
				pv_zWcL3Z_0024cdK2wgj_OVSJY++;
			}
		}
	}

	public int LineHeight
	{
		get
		{
			return ZXZ_00241iylFvWJ4Q4SGcz737o;
		}
		set
		{
			if (ZXZ_00241iylFvWJ4Q4SGcz737o != value)
			{
				ZXZ_00241iylFvWJ4Q4SGcz737o = value;
				pv_zWcL3Z_0024cdK2wgj_OVSJY++;
			}
		}
	}

	public int IndentAmount
	{
		get
		{
			return int_0;
		}
		set
		{
			if (int_0 != value)
			{
				int_0 = value;
				pv_zWcL3Z_0024cdK2wgj_OVSJY++;
			}
		}
	}

	public float LabelWidthPercentage
	{
		get
		{
			return float_0;
		}
		set
		{
			if (float_0 != value)
			{
				float_0 = value;
				pv_zWcL3Z_0024cdK2wgj_OVSJY++;
			}
		}
	}

	public Color WindowColor
	{
		get
		{
			return ZT41isQqk7h5Q8lp1eMtJso;
		}
		set
		{
			if (ZT41isQqk7h5Q8lp1eMtJso != value)
			{
				ZT41isQqk7h5Q8lp1eMtJso = value;
				pv_zWcL3Z_0024cdK2wgj_OVSJY++;
			}
		}
	}

	public Color BackgroundColor
	{
		get
		{
			return y2iCsisUyAyUrDTNOE1cRViuAZVO3M2EKdQFPDB8O_0024ud;
		}
		set
		{
			if (y2iCsisUyAyUrDTNOE1cRViuAZVO3M2EKdQFPDB8O_0024ud != value)
			{
				y2iCsisUyAyUrDTNOE1cRViuAZVO3M2EKdQFPDB8O_0024ud = value;
				pv_zWcL3Z_0024cdK2wgj_OVSJY++;
			}
		}
	}

	public Color TextColor
	{
		get
		{
			return RP7V4KJGCG5T1nzNedMY1Es;
		}
		set
		{
			if (RP7V4KJGCG5T1nzNedMY1Es != value)
			{
				RP7V4KJGCG5T1nzNedMY1Es = value;
				pv_zWcL3Z_0024cdK2wgj_OVSJY++;
			}
		}
	}

	public Color ScrollbarColor
	{
		get
		{
			return RxEsUXR1Tq1wJdBIP3zXzuIwBwutsiBL4AumePPYN_zq;
		}
		set
		{
			if (RxEsUXR1Tq1wJdBIP3zXzuIwBwutsiBL4AumePPYN_zq != value)
			{
				RxEsUXR1Tq1wJdBIP3zXzuIwBwutsiBL4AumePPYN_zq = value;
				pv_zWcL3Z_0024cdK2wgj_OVSJY++;
			}
		}
	}

	public Color ExpandArrowColor
	{
		get
		{
			return Tldg_0024DrVRxWZolOeKY1vCj2Jp0MGzp5HJeUVjqqoSPy9;
		}
		set
		{
			if (Tldg_0024DrVRxWZolOeKY1vCj2Jp0MGzp5HJeUVjqqoSPy9 != value)
			{
				Tldg_0024DrVRxWZolOeKY1vCj2Jp0MGzp5HJeUVjqqoSPy9 = value;
				pv_zWcL3Z_0024cdK2wgj_OVSJY++;
			}
		}
	}

	public Color InputFieldNormalBackgroundColor
	{
		get
		{
			return DMDqZyAqLrv_k3MRewLF_W2pLTfUHcq2RS8ZplzfTS_0024zOEcudIOE8FyWVd45x7CY7A;
		}
		set
		{
			if (DMDqZyAqLrv_k3MRewLF_W2pLTfUHcq2RS8ZplzfTS_0024zOEcudIOE8FyWVd45x7CY7A != value)
			{
				DMDqZyAqLrv_k3MRewLF_W2pLTfUHcq2RS8ZplzfTS_0024zOEcudIOE8FyWVd45x7CY7A = value;
				pv_zWcL3Z_0024cdK2wgj_OVSJY++;
			}
		}
	}

	public Color InputFieldInvalidBackgroundColor
	{
		get
		{
			return color_0;
		}
		set
		{
			if (color_0 != value)
			{
				color_0 = value;
				pv_zWcL3Z_0024cdK2wgj_OVSJY++;
			}
		}
	}

	public Color InputFieldTextColor
	{
		get
		{
			return ZFZuqc1jR64KLC2jhe6RFIVbkojsWMv1wKmQ3qoBGkLA;
		}
		set
		{
			if (ZFZuqc1jR64KLC2jhe6RFIVbkojsWMv1wKmQ3qoBGkLA != value)
			{
				ZFZuqc1jR64KLC2jhe6RFIVbkojsWMv1wKmQ3qoBGkLA = value;
				pv_zWcL3Z_0024cdK2wgj_OVSJY++;
			}
		}
	}

	public Color ToggleCheckmarkColor
	{
		get
		{
			return qKhmGNgmeLQAbpgDevmsf2UB6AvB_0024R3WVLcHh8gkdyQ2;
		}
		set
		{
			if (qKhmGNgmeLQAbpgDevmsf2UB6AvB_0024R3WVLcHh8gkdyQ2 != value)
			{
				qKhmGNgmeLQAbpgDevmsf2UB6AvB_0024R3WVLcHh8gkdyQ2 = value;
				pv_zWcL3Z_0024cdK2wgj_OVSJY++;
			}
		}
	}

	public Color SliderBackgroundColor
	{
		get
		{
			return Sun4sokbU_0024ABkXUtABpMhAw3Tv4ap_afGSWbt6jgqC2X;
		}
		set
		{
			if (Sun4sokbU_0024ABkXUtABpMhAw3Tv4ap_afGSWbt6jgqC2X != value)
			{
				Sun4sokbU_0024ABkXUtABpMhAw3Tv4ap_afGSWbt6jgqC2X = value;
				pv_zWcL3Z_0024cdK2wgj_OVSJY++;
			}
		}
	}

	public Color SliderThumbColor
	{
		get
		{
			return hHcsg7fc9LZ2_0024mksjlSka2aH8EOv04RLP7xOxj9HXfZf;
		}
		set
		{
			if (hHcsg7fc9LZ2_0024mksjlSka2aH8EOv04RLP7xOxj9HXfZf != value)
			{
				hHcsg7fc9LZ2_0024mksjlSka2aH8EOv04RLP7xOxj9HXfZf = value;
				pv_zWcL3Z_0024cdK2wgj_OVSJY++;
			}
		}
	}

	public Color ButtonBackgroundColor
	{
		get
		{
			return jbftbGrrCuUvUOduAZlg0sLwK2YUS2T3eZ6YLUWqdV08;
		}
		set
		{
			if (jbftbGrrCuUvUOduAZlg0sLwK2YUS2T3eZ6YLUWqdV08 != value)
			{
				jbftbGrrCuUvUOduAZlg0sLwK2YUS2T3eZ6YLUWqdV08 = value;
				pv_zWcL3Z_0024cdK2wgj_OVSJY++;
			}
		}
	}

	public Color ButtonTextColor
	{
		get
		{
			return color_1;
		}
		set
		{
			if (color_1 != value)
			{
				color_1 = value;
				pv_zWcL3Z_0024cdK2wgj_OVSJY++;
			}
		}
	}

	public Color SelectedItemBackgroundColor
	{
		get
		{
			return color_2;
		}
		set
		{
			if (color_2 != value)
			{
				color_2 = value;
				pv_zWcL3Z_0024cdK2wgj_OVSJY++;
			}
		}
	}

	public Color SelectedItemTextColor
	{
		get
		{
			return color_3;
		}
		set
		{
			if (color_3 != value)
			{
				color_3 = value;
				pv_zWcL3Z_0024cdK2wgj_OVSJY++;
			}
		}
	}

	[ContextMenu("Refresh UI")]
	private void p1XNbHgYp38KQ4IaexpNUfw()
	{
		pv_zWcL3Z_0024cdK2wgj_OVSJY = smethod_0(int.MinValue, int.MaxValue);
	}

	internal static int smethod_0(int int_1, int int_2)
	{
		return Random.Range(int_1, int_2);
	}

	internal static bool smethod_1(Object object_0, Object object_1)
	{
		return object_0 != object_1;
	}
}
