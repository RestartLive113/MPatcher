using UnityEngine;
using VRGIN.Visuals;

internal class Class16 : IMaterialPalette
{
	private Material CjHjuARRQ9LQ9zYw0tF5wqA;

	private Material fGJQ2YhDvfs3y5YD_2nuSZtLpKG9io4VK5wYDpqjJb3K;

	private Material material_0;

	private Material F0si_0024E_PnhyRpRb8ozg3CqI;

	public Material Sprite
	{
		get
		{
			if (smethod_0((Object)F0si_0024E_PnhyRpRb8ozg3CqI, (Object)null))
			{
				F0si_0024E_PnhyRpRb8ozg3CqI = smethod_2(smethod_1(global::_003CModule_003E.smethod_27<string>(2700558647u)));
			}
			return F0si_0024E_PnhyRpRb8ozg3CqI;
		}
	}

	public Material Unlit
	{
		get
		{
			if (smethod_0((Object)CjHjuARRQ9LQ9zYw0tF5wqA, (Object)null))
			{
				CjHjuARRQ9LQ9zYw0tF5wqA = smethod_2(smethod_1(global::_003CModule_003E.smethod_29<string>(4108007439u)));
			}
			return CjHjuARRQ9LQ9zYw0tF5wqA;
		}
	}

	public Material UnlitTransparent
	{
		get
		{
			if (smethod_0((Object)fGJQ2YhDvfs3y5YD_2nuSZtLpKG9io4VK5wYDpqjJb3K, (Object)null))
			{
				fGJQ2YhDvfs3y5YD_2nuSZtLpKG9io4VK5wYDpqjJb3K = smethod_2(smethod_1(global::_003CModule_003E.smethod_29<string>(1446633048u)));
			}
			return fGJQ2YhDvfs3y5YD_2nuSZtLpKG9io4VK5wYDpqjJb3K;
		}
	}

	public Material UnlitTransparentCombined
	{
		get
		{
			if (smethod_0((Object)material_0, (Object)null))
			{
				material_0 = smethod_2(smethod_1(global::_003CModule_003E.smethod_28<string>(726895790u)));
			}
			return material_0;
		}
	}

	public Shader StandardShader => smethod_1(global::_003CModule_003E.smethod_26<string>(4255022278u));

	internal static bool smethod_0(Object object_0, Object object_1)
	{
		return object_0 == object_1;
	}

	internal static Shader smethod_1(string string_0)
	{
		return Shader.Find(string_0);
	}

	internal static Material smethod_2(Shader shader_0)
	{
		return new Material(shader_0);
	}
}
