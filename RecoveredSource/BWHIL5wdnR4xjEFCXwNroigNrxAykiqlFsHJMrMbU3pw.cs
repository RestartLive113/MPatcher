using UnityEngine;

internal static class BWHIL5wdnR4xjEFCXwNroigNrxAykiqlFsHJMrMbU3pw
{
	internal static Sprite PLX3X99qjMiqi8ErUqFTqg0(this Texture2D texture2D_0, float float_0 = 100f, uint uint_0 = 1u, SpriteMeshType spriteMeshType_0 = SpriteMeshType.Tight, Vector4 vector4_0 = default(Vector4))
	{
		return Sprite.Create(texture2D_0, new Rect(0f, 0f, smethod_0((Texture)texture2D_0), smethod_1((Texture)texture2D_0)), Vector2.zero, float_0, uint_0, spriteMeshType_0, vector4_0);
	}

	internal static Texture2D oPoOeyx7nfZcVVhd8CSfgNA(byte[] byte_0)
	{
		Texture2D texture2D = smethod_2(1, 1);
		smethod_3((Texture)texture2D, FilterMode.Point);
		smethod_4(texture2D, byte_0);
		return texture2D;
	}

	internal static int smethod_0(Texture texture_0)
	{
		return texture_0.width;
	}

	internal static int smethod_1(Texture texture_0)
	{
		return texture_0.height;
	}

	internal static Texture2D smethod_2(int int_0, int int_1)
	{
		return new Texture2D(int_0, int_1);
	}

	internal static void smethod_3(Texture texture_0, FilterMode filterMode_0)
	{
		texture_0.filterMode = filterMode_0;
	}

	internal static bool smethod_4(Texture2D texture2D_0, byte[] byte_0)
	{
		return texture2D_0.LoadImage(byte_0);
	}
}
