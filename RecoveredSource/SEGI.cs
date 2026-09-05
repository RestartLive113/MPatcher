using System;
using System.Runtime.InteropServices;
using UnityEngine;

[AddComponentMenu("Image Effects/Sonic Ether/SEGI")]
[RequireComponent(typeof(Camera))]
[ExecuteInEditMode]
public class SEGI : MonoBehaviour
{
	[Serializable]
	public enum VoxelResolution
	{
		low = 0x80,
		high = 0x100
	}

	private enum SBPAmo0wGlHXa4G23uvylMDoQx2xUFANeE9GQoOn4b_0024K
	{
		Voxelize,
		Bounce
	}

	[StructLayout(LayoutKind.Sequential, Size = 1)]
	private struct qK929_0024Id7SkRqjEjesCnAz0
	{
		public static int B6PpKyUGqLQ5wtn3ZYSUqMY = 0;

		public static int oyfhQsh2G93SjLRR4H1yV7I = 1;

		public static int GdkzUKeEbmK2QOpGiGcEbH4 = 2;

		public static int UyNCGtYyPlDyRu2bnc67Q4c = 3;

		public static int ojxdZvkss5oUP68uIvL3Od0 = 4;

		public static int I1TX9fwl20itYRYIa86_ughcPSUkktmKuTkesGpikW9G = 5;

		public static int EO2KTni9LQqLLv9FybVNc0I = 6;

		public static int OuZtpHSmXY8upCgmoSxBrHY = 7;

		public static int ULnipMq0mo6Al0y17bFGFOU = 8;

		public static int iW7x_0024CEd6Tc1H8QLwpx770k = 10;

		public static int Gf_0024AuukWAf6RonQAXybhMEZuhIG9EZetLdi1ZMKcVge9 = 11;
	}

	public struct SystemSupported
	{
		public bool hdrTextures;

		public bool rIntTextures;

		public bool dx11;

		public bool volumeTextures;

		public bool postShader;

		public bool sunDepthShader;

		public bool voxelizationShader;

		public bool tracingShader;

		public bool fullFunctionality
		{
			get
			{
				if (hdrTextures && rIntTextures && dx11 && volumeTextures && postShader && sunDepthShader && voxelizationShader)
				{
					return tracingShader;
				}
				return false;
			}
		}
	}

	public bool updateGI = true;

	public LayerMask giCullingMask = int.MaxValue;

	public float shadowSpaceSize = 50f;

	public Light sun;

	public Color skyColor;

	public float voxelSpaceSize = 25f;

	public bool useBilateralFiltering;

	[Range(0f, 2f)]
	public int innerOcclusionLayers = 1;

	[Range(0.01f, 1f)]
	public float temporalBlendWeight = 0.1f;

	public VoxelResolution voxelResolution = VoxelResolution.high;

	public bool visualizeSunDepthTexture;

	public bool visualizeGI;

	public bool visualizeVoxels;

	public bool halfResolution = true;

	public bool stochasticSampling = true;

	public bool infiniteBounces;

	public Transform followTransform;

	[Range(1f, 128f)]
	public int cones = 6;

	[Range(1f, 32f)]
	public int coneTraceSteps = 14;

	[Range(0.1f, 2f)]
	public float coneLength = 1f;

	[Range(0.5f, 6f)]
	public float coneWidth = 5.5f;

	[Range(0f, 4f)]
	public float occlusionStrength = 1f;

	[Range(0f, 4f)]
	public float nearOcclusionStrength = 0.5f;

	[Range(0.001f, 4f)]
	public float occlusionPower = 1.5f;

	[Range(0f, 4f)]
	public float coneTraceBias = 1f;

	[Range(0f, 4f)]
	public float nearLightGain = 1f;

	[Range(0f, 4f)]
	public float giGain = 1f;

	[Range(0f, 4f)]
	public float secondaryBounceGain = 1f;

	[Range(0f, 16f)]
	public float softSunlight;

	[Range(0f, 8f)]
	public float skyIntensity = 1f;

	public bool doReflections = true;

	[Range(12f, 128f)]
	public int reflectionSteps = 64;

	[Range(0.001f, 4f)]
	public float reflectionOcclusionPower = 1f;

	[Range(0f, 1f)]
	public float skyReflectionIntensity = 1f;

	public bool voxelAA;

	public bool gaussianMipFilter;

	[Range(0.1f, 4f)]
	public float farOcclusionStrength = 1f;

	[Range(0.1f, 4f)]
	public float farthestOcclusionStrength = 1f;

	[Range(3f, 16f)]
	public int secondaryCones = 6;

	[Range(0.1f, 4f)]
	public float secondaryOcclusionStrength = 1f;

	public bool sphericalSkylight;

	private object LTIQIOKgpROjbtrHNOCWGs8;

	private Material v9cXvr_0024GnMZFPia_0024zufmsb0;

	private Camera xU6_0024sWf25471NIrEawLbg5Y;

	private Transform transform_0;

	private Camera h9tJJ72fwhl0tAHi8xVmzwE;

	private GameObject OGDA00FhfxLR8vhfgOslQ2yLifyEA3gTPdwK4Nhf5WSO;

	private Texture2D[] JL1cXRJ7RTR3zZj248SYVx0;

	private int IE16nOiWFgVvy6eutK1geFvtCyR8KrVUlZ3P_0024onh_y7V = 256;

	private int zVHmE2bRLLFjwMIM3jC0U2s_0024kg1piRAhS2_00248LXBUcDkT;

	private Shader shader_0;

	private float DjBTgKiMJa1J56WSL4LIjyBPPRkJkDJeG2NLvCOtr_0024AV = 10f;

	private int JzFwzFRpbj_F0sJ_0024ctfffyk;

	private RenderTexture jDKfAEEsFhXvaiCI3vBP5oM;

	private RenderTexture gtohyIloJHa1Bs2GBGB_pKHnoo6ekUsuI7c05u5t0Hbw;

	private RenderTexture renderTexture_0;

	private RenderTexture BGMycV3Th9mx6IWJHaC3bdc;

	private RenderTexture[] renderTexture_1;

	private RenderTexture iixTCydx_0024YSeML5_0024LurQgiW6K_00247JkLXoMiyedGk_0024q_0024Ft;

	private RenderTexture rWBD3h5_0024TYIymf9wdYd7gFc;

	private RenderTexture _0024K_16I5_jZB6F8pQNPbtu4M;

	private RenderTexture renderTexture_2;

	private RenderTexture kzArpgN77EenM_0024rwG9aUiCt2KRZCwiATI0usnukmFiyK;

	private RenderTexture _8Cs00R352uKGFd6ZDQVDJOTtdXKvn1kbNr_0024s6_0024BdcmT;

	private bool UXGJ1As6BnxRfO3_NmX60CuIuLJiuMRdI_dRmWw9_0024XtP;

	private Shader shader_1;

	private Shader yagnK_Ylg_0024mphSgUp3cc1hVPTHg7eb0r_0024OyTD5m3IPch;

	private ComputeShader JtBuENn09KKbt4VusdO6fs4;

	private ComputeShader QKm_iaJDcMtaPfHUcx_kAsoHyrVV6_ulniDKzWGmmbMa;

	private ComputeShader P6ncUp7E7lcAN5iCoxdiu5AFMqp4Jid406_S_DCe3pLh;

	private const int int_0 = 6;

	private Camera O1O7t2ivipE0i32PfGcalvo;

	private GameObject N3m79WJbrajw_0024ojqHMGXreo;

	private GameObject BAmibN9na7H3ZTL_cbCgnw8;

	private GameObject cq2GOmBacgrU3GCDQxofHaU;

	private Vector3 jrSex6S8C07dIdTRGErwp8ito605hWWmPmMb4X3eai_9;

	private Vector3 Ya1pUtOuDK0XDhm7IYX_HvpbYOYW9_XMqOJBzl9X2Ol2;

	private Vector3 fvQXa_0024Rygoic_lY8_2bkg7Ijlursi0xZcqx32SpcpjbP;

	private Quaternion B3y1jaoOHb27vrqRtfshUXg = new Quaternion(0f, 0f, 0f, 1f);

	private Quaternion SUP__0024VYm7_0024LaHj_I7j6eZKM = new Quaternion(0f, 0.7f, 0f, 0.7f);

	private Quaternion t5WXs069egBfeiL0IkAlRN4 = new Quaternion(0.7f, 0f, 0f, 0.7f);

	private int MoMGhYR6ydOb81kD6COeafU;

	private SBPAmo0wGlHXa4G23uvylMDoQx2xUFANeE9GQoOn4b_0024K x_7qq7jXOw7w_00249RgTuwON2U;

	public SystemSupported systemSupported;

	private float voxelScaleFactor => (float)voxelResolution / 256f;

	public float vramUsage
	{
		get
		{
			long num = 0L;
			if (smethod_0((UnityEngine.Object)jDKfAEEsFhXvaiCI3vBP5oM, (UnityEngine.Object)null))
			{
				num += smethod_1((Texture)jDKfAEEsFhXvaiCI3vBP5oM) * smethod_2((Texture)jDKfAEEsFhXvaiCI3vBP5oM) * 16;
			}
			if (smethod_0((UnityEngine.Object)gtohyIloJHa1Bs2GBGB_pKHnoo6ekUsuI7c05u5t0Hbw, (UnityEngine.Object)null))
			{
				num += smethod_1((Texture)gtohyIloJHa1Bs2GBGB_pKHnoo6ekUsuI7c05u5t0Hbw) * smethod_2((Texture)gtohyIloJHa1Bs2GBGB_pKHnoo6ekUsuI7c05u5t0Hbw) * 16 * 4;
			}
			if (smethod_0((UnityEngine.Object)renderTexture_0, (UnityEngine.Object)null))
			{
				num += smethod_1((Texture)renderTexture_0) * smethod_2((Texture)renderTexture_0) * 32;
			}
			if (smethod_0((UnityEngine.Object)BGMycV3Th9mx6IWJHaC3bdc, (UnityEngine.Object)null))
			{
				num += smethod_1((Texture)BGMycV3Th9mx6IWJHaC3bdc) * smethod_2((Texture)BGMycV3Th9mx6IWJHaC3bdc) * smethod_3(BGMycV3Th9mx6IWJHaC3bdc) * 32;
			}
			if (renderTexture_1 != null)
			{
				for (int i = 0; i < renderTexture_1.Length; i++)
				{
					if (smethod_0((UnityEngine.Object)renderTexture_1[i], (UnityEngine.Object)null))
					{
						num += smethod_1((Texture)renderTexture_1[i]) * smethod_2((Texture)renderTexture_1[i]) * smethod_3(renderTexture_1[i]) * 16 * 4;
					}
				}
			}
			if (smethod_0((UnityEngine.Object)iixTCydx_0024YSeML5_0024LurQgiW6K_00247JkLXoMiyedGk_0024q_0024Ft, (UnityEngine.Object)null))
			{
				num += smethod_1((Texture)iixTCydx_0024YSeML5_0024LurQgiW6K_00247JkLXoMiyedGk_0024q_0024Ft) * smethod_2((Texture)iixTCydx_0024YSeML5_0024LurQgiW6K_00247JkLXoMiyedGk_0024q_0024Ft) * smethod_3(iixTCydx_0024YSeML5_0024LurQgiW6K_00247JkLXoMiyedGk_0024q_0024Ft) * 16 * 4;
			}
			if (smethod_0((UnityEngine.Object)rWBD3h5_0024TYIymf9wdYd7gFc, (UnityEngine.Object)null))
			{
				num += smethod_1((Texture)rWBD3h5_0024TYIymf9wdYd7gFc) * smethod_2((Texture)rWBD3h5_0024TYIymf9wdYd7gFc) * smethod_3(rWBD3h5_0024TYIymf9wdYd7gFc) * 16 * 4;
			}
			if (smethod_0((UnityEngine.Object)kzArpgN77EenM_0024rwG9aUiCt2KRZCwiATI0usnukmFiyK, (UnityEngine.Object)null))
			{
				num += smethod_1((Texture)kzArpgN77EenM_0024rwG9aUiCt2KRZCwiATI0usnukmFiyK) * smethod_2((Texture)kzArpgN77EenM_0024rwG9aUiCt2KRZCwiATI0usnukmFiyK) * 8;
			}
			if (smethod_0((UnityEngine.Object)_8Cs00R352uKGFd6ZDQVDJOTtdXKvn1kbNr_0024s6_0024BdcmT, (UnityEngine.Object)null))
			{
				num += smethod_1((Texture)_8Cs00R352uKGFd6ZDQVDJOTtdXKvn1kbNr_0024s6_0024BdcmT) * smethod_2((Texture)_8Cs00R352uKGFd6ZDQVDJOTtdXKvn1kbNr_0024s6_0024BdcmT) * 8;
			}
			return (float)num / 8388608f;
		}
	}

	private int mipFilterKernel => gaussianMipFilter ? 1 : 0;

	private int dummyVoxelResolution => (int)voxelResolution * ((!voxelAA) ? 1 : 2);

	private int giRenderRes
	{
		get
		{
			if (!halfResolution)
			{
				return 1;
			}
			return 2;
		}
	}

	public void ApplyPreset(SEGIPreset preset)
	{
		voxelResolution = preset.voxelResolution;
		voxelAA = preset.voxelAA;
		innerOcclusionLayers = preset.innerOcclusionLayers;
		infiniteBounces = preset.infiniteBounces;
		temporalBlendWeight = preset.temporalBlendWeight;
		useBilateralFiltering = preset.useBilateralFiltering;
		halfResolution = preset.halfResolution;
		stochasticSampling = preset.stochasticSampling;
		doReflections = preset.doReflections;
		cones = preset.cones;
		coneTraceSteps = preset.coneTraceSteps;
		coneLength = preset.coneLength;
		coneWidth = preset.coneWidth;
		coneTraceBias = preset.coneTraceBias;
		occlusionStrength = preset.occlusionStrength;
		nearOcclusionStrength = preset.nearOcclusionStrength;
		occlusionPower = preset.occlusionPower;
		nearLightGain = preset.nearLightGain;
		giGain = preset.giGain;
		secondaryBounceGain = preset.secondaryBounceGain;
		reflectionSteps = preset.reflectionSteps;
		reflectionOcclusionPower = preset.reflectionOcclusionPower;
		skyReflectionIntensity = preset.skyReflectionIntensity;
		gaussianMipFilter = preset.gaussianMipFilter;
		farOcclusionStrength = preset.farOcclusionStrength;
		farthestOcclusionStrength = preset.farthestOcclusionStrength;
		secondaryCones = preset.secondaryCones;
		secondaryOcclusionStrength = preset.secondaryOcclusionStrength;
	}

	private void Start()
	{
		DNNHvTtfWvRvxiu9RxCdoHQ();
	}

	private void DNNHvTtfWvRvxiu9RxCdoHQ()
	{
		if (LTIQIOKgpROjbtrHNOCWGs8 == null)
		{
			QABDMcdabHvOCeFt2UXD4sg();
		}
	}

	private void GqyUSKTh1clCJUinryCu9Mo82P_4nR2THau6of8AsSNA()
	{
		renderTexture_1 = new RenderTexture[6];
		for (int i = 0; i < 6; i++)
		{
			if (smethod_4((UnityEngine.Object)renderTexture_1[i]))
			{
				smethod_5(renderTexture_1[i]);
				smethod_6(renderTexture_1[i]);
				smethod_7((UnityEngine.Object)renderTexture_1[i]);
			}
			int num = (int)voxelResolution / Mathf.RoundToInt(Mathf.Pow(2f, i));
			renderTexture_1[i] = smethod_8(num, num, 0, RenderTextureFormat.ARGBHalf, RenderTextureReadWrite.Linear);
			smethod_9(renderTexture_1[i], bool_0: true);
			smethod_10(renderTexture_1[i], num);
			smethod_11(renderTexture_1[i], bool_0: true);
			smethod_12((Texture)renderTexture_1[i], FilterMode.Bilinear);
			smethod_13(renderTexture_1[i], bool_0: false);
			smethod_14(renderTexture_1[i], bool_0: false);
			smethod_15(renderTexture_1[i]);
			smethod_16((UnityEngine.Object)renderTexture_1[i], HideFlags.HideAndDontSave);
		}
		if (smethod_4((UnityEngine.Object)rWBD3h5_0024TYIymf9wdYd7gFc))
		{
			smethod_5(rWBD3h5_0024TYIymf9wdYd7gFc);
			smethod_6(rWBD3h5_0024TYIymf9wdYd7gFc);
			smethod_7((UnityEngine.Object)rWBD3h5_0024TYIymf9wdYd7gFc);
		}
		rWBD3h5_0024TYIymf9wdYd7gFc = smethod_8((int)voxelResolution, (int)voxelResolution, 0, RenderTextureFormat.ARGBHalf, RenderTextureReadWrite.Linear);
		smethod_9(rWBD3h5_0024TYIymf9wdYd7gFc, bool_0: true);
		smethod_10(rWBD3h5_0024TYIymf9wdYd7gFc, (int)voxelResolution);
		smethod_11(rWBD3h5_0024TYIymf9wdYd7gFc, bool_0: true);
		smethod_12((Texture)rWBD3h5_0024TYIymf9wdYd7gFc, FilterMode.Bilinear);
		smethod_13(rWBD3h5_0024TYIymf9wdYd7gFc, bool_0: false);
		smethod_14(rWBD3h5_0024TYIymf9wdYd7gFc, bool_0: false);
		smethod_15(rWBD3h5_0024TYIymf9wdYd7gFc);
		smethod_16((UnityEngine.Object)rWBD3h5_0024TYIymf9wdYd7gFc, HideFlags.HideAndDontSave);
		if (smethod_4((UnityEngine.Object)iixTCydx_0024YSeML5_0024LurQgiW6K_00247JkLXoMiyedGk_0024q_0024Ft))
		{
			smethod_5(iixTCydx_0024YSeML5_0024LurQgiW6K_00247JkLXoMiyedGk_0024q_0024Ft);
			smethod_6(iixTCydx_0024YSeML5_0024LurQgiW6K_00247JkLXoMiyedGk_0024q_0024Ft);
			smethod_7((UnityEngine.Object)iixTCydx_0024YSeML5_0024LurQgiW6K_00247JkLXoMiyedGk_0024q_0024Ft);
		}
		iixTCydx_0024YSeML5_0024LurQgiW6K_00247JkLXoMiyedGk_0024q_0024Ft = smethod_8((int)voxelResolution, (int)voxelResolution, 0, RenderTextureFormat.ARGBHalf, RenderTextureReadWrite.Linear);
		smethod_9(iixTCydx_0024YSeML5_0024LurQgiW6K_00247JkLXoMiyedGk_0024q_0024Ft, bool_0: true);
		smethod_10(iixTCydx_0024YSeML5_0024LurQgiW6K_00247JkLXoMiyedGk_0024q_0024Ft, (int)voxelResolution);
		smethod_11(iixTCydx_0024YSeML5_0024LurQgiW6K_00247JkLXoMiyedGk_0024q_0024Ft, bool_0: true);
		smethod_12((Texture)iixTCydx_0024YSeML5_0024LurQgiW6K_00247JkLXoMiyedGk_0024q_0024Ft, FilterMode.Point);
		smethod_13(iixTCydx_0024YSeML5_0024LurQgiW6K_00247JkLXoMiyedGk_0024q_0024Ft, bool_0: false);
		smethod_14(iixTCydx_0024YSeML5_0024LurQgiW6K_00247JkLXoMiyedGk_0024q_0024Ft, bool_0: false);
		smethod_17(iixTCydx_0024YSeML5_0024LurQgiW6K_00247JkLXoMiyedGk_0024q_0024Ft, 1);
		smethod_15(iixTCydx_0024YSeML5_0024LurQgiW6K_00247JkLXoMiyedGk_0024q_0024Ft);
		smethod_16((UnityEngine.Object)iixTCydx_0024YSeML5_0024LurQgiW6K_00247JkLXoMiyedGk_0024q_0024Ft, HideFlags.HideAndDontSave);
		if (smethod_4((UnityEngine.Object)BGMycV3Th9mx6IWJHaC3bdc))
		{
			smethod_5(BGMycV3Th9mx6IWJHaC3bdc);
			smethod_6(BGMycV3Th9mx6IWJHaC3bdc);
			smethod_7((UnityEngine.Object)BGMycV3Th9mx6IWJHaC3bdc);
		}
		BGMycV3Th9mx6IWJHaC3bdc = smethod_8((int)voxelResolution, (int)voxelResolution, 0, RenderTextureFormat.RInt, RenderTextureReadWrite.Linear);
		smethod_9(BGMycV3Th9mx6IWJHaC3bdc, bool_0: true);
		smethod_10(BGMycV3Th9mx6IWJHaC3bdc, (int)voxelResolution);
		smethod_11(BGMycV3Th9mx6IWJHaC3bdc, bool_0: true);
		smethod_12((Texture)BGMycV3Th9mx6IWJHaC3bdc, FilterMode.Point);
		smethod_15(BGMycV3Th9mx6IWJHaC3bdc);
		smethod_16((UnityEngine.Object)BGMycV3Th9mx6IWJHaC3bdc, HideFlags.HideAndDontSave);
		X5ZL_0024eLYqBX9ZqQQnL1VHc5FURIsW8Aghqr10ZL_d0cy();
	}

	private void X5ZL_0024eLYqBX9ZqQQnL1VHc5FURIsW8Aghqr10ZL_d0cy()
	{
		if (smethod_4((UnityEngine.Object)kzArpgN77EenM_0024rwG9aUiCt2KRZCwiATI0usnukmFiyK))
		{
			smethod_5(kzArpgN77EenM_0024rwG9aUiCt2KRZCwiATI0usnukmFiyK);
			smethod_6(kzArpgN77EenM_0024rwG9aUiCt2KRZCwiATI0usnukmFiyK);
			smethod_7((UnityEngine.Object)kzArpgN77EenM_0024rwG9aUiCt2KRZCwiATI0usnukmFiyK);
		}
		kzArpgN77EenM_0024rwG9aUiCt2KRZCwiATI0usnukmFiyK = smethod_18(dummyVoxelResolution, dummyVoxelResolution, 0, RenderTextureFormat.R8);
		smethod_15(kzArpgN77EenM_0024rwG9aUiCt2KRZCwiATI0usnukmFiyK);
		smethod_16((UnityEngine.Object)kzArpgN77EenM_0024rwG9aUiCt2KRZCwiATI0usnukmFiyK, HideFlags.HideAndDontSave);
		if (smethod_4((UnityEngine.Object)_8Cs00R352uKGFd6ZDQVDJOTtdXKvn1kbNr_0024s6_0024BdcmT))
		{
			smethod_5(_8Cs00R352uKGFd6ZDQVDJOTtdXKvn1kbNr_0024s6_0024BdcmT);
			smethod_6(_8Cs00R352uKGFd6ZDQVDJOTtdXKvn1kbNr_0024s6_0024BdcmT);
			smethod_7((UnityEngine.Object)_8Cs00R352uKGFd6ZDQVDJOTtdXKvn1kbNr_0024s6_0024BdcmT);
		}
		_8Cs00R352uKGFd6ZDQVDJOTtdXKvn1kbNr_0024s6_0024BdcmT = smethod_18((int)voxelResolution, (int)voxelResolution, 0, RenderTextureFormat.R8);
		smethod_15(_8Cs00R352uKGFd6ZDQVDJOTtdXKvn1kbNr_0024s6_0024BdcmT);
		smethod_16((UnityEngine.Object)_8Cs00R352uKGFd6ZDQVDJOTtdXKvn1kbNr_0024s6_0024BdcmT, HideFlags.HideAndDontSave);
	}

	private void QABDMcdabHvOCeFt2UXD4sg()
	{
		shader_0 = r0FjGfwwmg4ieGHTHNmU4cI.cqWoMNveroNrLO3XzL3B_0024XA<Shader>(global::_003CModule_003E.smethod_25<string>(517420521u));
		JtBuENn09KKbt4VusdO6fs4 = r0FjGfwwmg4ieGHTHNmU4cI.cqWoMNveroNrLO3XzL3B_0024XA(global::_003CModule_003E.smethod_27<string>(573616453u)) as ComputeShader;
		QKm_iaJDcMtaPfHUcx_kAsoHyrVV6_ulniDKzWGmmbMa = r0FjGfwwmg4ieGHTHNmU4cI.cqWoMNveroNrLO3XzL3B_0024XA(global::_003CModule_003E.smethod_28<string>(2092053270u)) as ComputeShader;
		P6ncUp7E7lcAN5iCoxdiu5AFMqp4Jid406_S_DCe3pLh = r0FjGfwwmg4ieGHTHNmU4cI.cqWoMNveroNrLO3XzL3B_0024XA(global::_003CModule_003E.smethod_26<string>(479246379u)) as ComputeShader;
		shader_1 = r0FjGfwwmg4ieGHTHNmU4cI.cqWoMNveroNrLO3XzL3B_0024XA<Shader>(global::_003CModule_003E.smethod_26<string>(639630098u));
		yagnK_Ylg_0024mphSgUp3cc1hVPTHg7eb0r_0024OyTD5m3IPch = r0FjGfwwmg4ieGHTHNmU4cI.cqWoMNveroNrLO3XzL3B_0024XA<Shader>(global::_003CModule_003E.smethod_29<string>(3703338224u));
		v9cXvr_0024GnMZFPia_0024zufmsb0 = smethod_19(r0FjGfwwmg4ieGHTHNmU4cI.cqWoMNveroNrLO3XzL3B_0024XA<Shader>(global::_003CModule_003E.smethod_27<string>(3030537544u)));
		smethod_16((UnityEngine.Object)v9cXvr_0024GnMZFPia_0024zufmsb0, HideFlags.HideAndDontSave);
		xU6_0024sWf25471NIrEawLbg5Y = GetComponent<Camera>();
		Camera camera_ = xU6_0024sWf25471NIrEawLbg5Y;
		smethod_21(camera_, smethod_20(camera_) | DepthTextureMode.Depth);
		Camera camera_2 = xU6_0024sWf25471NIrEawLbg5Y;
		smethod_21(camera_2, smethod_20(camera_2) | DepthTextureMode.MotionVectors);
		GameObject gameObject = smethod_22(global::_003CModule_003E.smethod_26<string>(4132678799u));
		if (!smethod_4((UnityEngine.Object)gameObject))
		{
			OGDA00FhfxLR8vhfgOslQ2yLifyEA3gTPdwK4Nhf5WSO = smethod_23(global::_003CModule_003E.smethod_25<string>(2676810590u));
			h9tJJ72fwhl0tAHi8xVmzwE = OGDA00FhfxLR8vhfgOslQ2yLifyEA3gTPdwK4Nhf5WSO.AddComponent<Camera>();
			smethod_16((UnityEngine.Object)OGDA00FhfxLR8vhfgOslQ2yLifyEA3gTPdwK4Nhf5WSO, HideFlags.HideAndDontSave);
			smethod_24((Behaviour)h9tJJ72fwhl0tAHi8xVmzwE, bool_0: false);
			smethod_26(h9tJJ72fwhl0tAHi8xVmzwE, smethod_25(xU6_0024sWf25471NIrEawLbg5Y) - 1f);
			smethod_27(h9tJJ72fwhl0tAHi8xVmzwE, bool_0: true);
			smethod_28(h9tJJ72fwhl0tAHi8xVmzwE, shadowSpaceSize);
			smethod_29(h9tJJ72fwhl0tAHi8xVmzwE, CameraClearFlags.Color);
			h9tJJ72fwhl0tAHi8xVmzwE.backgroundColor = new Color(0f, 0f, 0f, 1f);
			h9tJJ72fwhl0tAHi8xVmzwE.farClipPlane = shadowSpaceSize * 2f * DjBTgKiMJa1J56WSL4LIjyBPPRkJkDJeG2NLvCOtr_0024AV;
			h9tJJ72fwhl0tAHi8xVmzwE.cullingMask = giCullingMask;
			h9tJJ72fwhl0tAHi8xVmzwE.useOcclusionCulling = false;
			transform_0 = OGDA00FhfxLR8vhfgOslQ2yLifyEA3gTPdwK4Nhf5WSO.transform;
		}
		else
		{
			OGDA00FhfxLR8vhfgOslQ2yLifyEA3gTPdwK4Nhf5WSO = gameObject;
			h9tJJ72fwhl0tAHi8xVmzwE = gameObject.GetComponent<Camera>();
			transform_0 = OGDA00FhfxLR8vhfgOslQ2yLifyEA3gTPdwK4Nhf5WSO.transform;
		}
		GameObject gameObject2 = GameObject.Find(global::_003CModule_003E.smethod_26<string>(4293062518u));
		if ((bool)gameObject2)
		{
			UnityEngine.Object.DestroyImmediate(gameObject2);
		}
		N3m79WJbrajw_0024ojqHMGXreo = new GameObject(global::_003CModule_003E.smethod_28<string>(2274173072u));
		N3m79WJbrajw_0024ojqHMGXreo.hideFlags = HideFlags.HideAndDontSave;
		O1O7t2ivipE0i32PfGcalvo = N3m79WJbrajw_0024ojqHMGXreo.AddComponent<Camera>();
		O1O7t2ivipE0i32PfGcalvo.enabled = false;
		O1O7t2ivipE0i32PfGcalvo.orthographic = true;
		O1O7t2ivipE0i32PfGcalvo.orthographicSize = voxelSpaceSize * 0.5f;
		O1O7t2ivipE0i32PfGcalvo.nearClipPlane = 0f;
		O1O7t2ivipE0i32PfGcalvo.farClipPlane = voxelSpaceSize;
		O1O7t2ivipE0i32PfGcalvo.depth = -2f;
		O1O7t2ivipE0i32PfGcalvo.renderingPath = RenderingPath.Forward;
		O1O7t2ivipE0i32PfGcalvo.clearFlags = CameraClearFlags.Color;
		O1O7t2ivipE0i32PfGcalvo.backgroundColor = Color.black;
		O1O7t2ivipE0i32PfGcalvo.useOcclusionCulling = false;
		GameObject gameObject3 = GameObject.Find(global::_003CModule_003E.smethod_25<string>(1670994955u));
		if ((bool)gameObject3)
		{
			UnityEngine.Object.DestroyImmediate(gameObject3);
		}
		BAmibN9na7H3ZTL_cbCgnw8 = new GameObject(global::_003CModule_003E.smethod_27<string>(2830438733u));
		BAmibN9na7H3ZTL_cbCgnw8.hideFlags = HideFlags.HideAndDontSave;
		GameObject gameObject4 = GameObject.Find(global::_003CModule_003E.smethod_27<string>(513109667u));
		if ((bool)gameObject4)
		{
			UnityEngine.Object.DestroyImmediate(gameObject4);
		}
		cq2GOmBacgrU3GCDQxofHaU = new GameObject(global::_003CModule_003E.smethod_29<string>(2112929982u));
		cq2GOmBacgrU3GCDQxofHaU.hideFlags = HideFlags.HideAndDontSave;
		JL1cXRJ7RTR3zZj248SYVx0 = null;
		JL1cXRJ7RTR3zZj248SYVx0 = new Texture2D[64];
		for (int i = 0; i < 64; i++)
		{
			string text = global::_003CModule_003E.smethod_28<string>(2167961222u) + i;
			Texture2D texture2D = r0FjGfwwmg4ieGHTHNmU4cI.cqWoMNveroNrLO3XzL3B_0024XA<Texture2D>(text);
			if (texture2D == null)
			{
				Debug.LogWarning(global::_003CModule_003E.smethod_27<string>(420048317u) + text + global::_003CModule_003E.smethod_29<string>(631096466u));
			}
			JL1cXRJ7RTR3zZj248SYVx0[i] = texture2D;
		}
		if ((bool)jDKfAEEsFhXvaiCI3vBP5oM)
		{
			jDKfAEEsFhXvaiCI3vBP5oM.DiscardContents();
			jDKfAEEsFhXvaiCI3vBP5oM.Release();
			UnityEngine.Object.DestroyImmediate(jDKfAEEsFhXvaiCI3vBP5oM);
		}
		jDKfAEEsFhXvaiCI3vBP5oM = new RenderTexture(IE16nOiWFgVvy6eutK1geFvtCyR8KrVUlZ3P_0024onh_y7V, IE16nOiWFgVvy6eutK1geFvtCyR8KrVUlZ3P_0024onh_y7V, 16, RenderTextureFormat.RHalf, RenderTextureReadWrite.Linear);
		jDKfAEEsFhXvaiCI3vBP5oM.wrapMode = TextureWrapMode.Clamp;
		jDKfAEEsFhXvaiCI3vBP5oM.filterMode = FilterMode.Point;
		jDKfAEEsFhXvaiCI3vBP5oM.Create();
		jDKfAEEsFhXvaiCI3vBP5oM.hideFlags = HideFlags.HideAndDontSave;
		GqyUSKTh1clCJUinryCu9Mo82P_4nR2THau6of8AsSNA();
		LTIQIOKgpROjbtrHNOCWGs8 = new object();
	}

	private void n2eBB2V6H1_0024MpK7jLzOfL4Y()
	{
		systemSupported.hdrTextures = smethod_30(RenderTextureFormat.ARGBHalf);
		systemSupported.rIntTextures = smethod_30(RenderTextureFormat.RInt);
		systemSupported.dx11 = smethod_31() >= 50 && smethod_32();
		systemSupported.volumeTextures = smethod_33();
		systemSupported.postShader = smethod_35(smethod_34(v9cXvr_0024GnMZFPia_0024zufmsb0));
		systemSupported.sunDepthShader = smethod_35(shader_0);
		systemSupported.voxelizationShader = smethod_35(shader_1);
		systemSupported.tracingShader = smethod_35(yagnK_Ylg_0024mphSgUp3cc1hVPTHg7eb0r_0024OyTD5m3IPch);
		if (!systemSupported.fullFunctionality)
		{
			smethod_36((object)global::_003CModule_003E.smethod_29<string>(1377564574u));
			smethod_37((Behaviour)this, bool_0: false);
		}
	}

	private void axhHAdDmmE72quoFXNwO65oYLiqfE5Xb8ExHKkmEBcVm()
	{
		Color color = smethod_38();
		Gizmos.color = new Color(1f, 0.25f, 0f, 0.5f);
		Gizmos.DrawCube(jrSex6S8C07dIdTRGErwp8ito605hWWmPmMb4X3eai_9, new Vector3(voxelSpaceSize, voxelSpaceSize, voxelSpaceSize));
		Gizmos.color = new Color(1f, 0f, 0f, 0.1f);
		Gizmos.color = color;
	}

	private void A_0024WaBDu05Gbdes15XfaAKYE(ref RenderTexture renderTexture_3)
	{
		smethod_5(renderTexture_3);
		smethod_7((UnityEngine.Object)renderTexture_3);
	}

	private void fKnnJm0rqvY2GMp4B2RPgIo()
	{
		A_0024WaBDu05Gbdes15XfaAKYE(ref jDKfAEEsFhXvaiCI3vBP5oM);
		A_0024WaBDu05Gbdes15XfaAKYE(ref gtohyIloJHa1Bs2GBGB_pKHnoo6ekUsuI7c05u5t0Hbw);
		A_0024WaBDu05Gbdes15XfaAKYE(ref renderTexture_0);
		A_0024WaBDu05Gbdes15XfaAKYE(ref BGMycV3Th9mx6IWJHaC3bdc);
		for (int i = 0; i < renderTexture_1.Length; i++)
		{
			A_0024WaBDu05Gbdes15XfaAKYE(ref renderTexture_1[i]);
		}
		A_0024WaBDu05Gbdes15XfaAKYE(ref iixTCydx_0024YSeML5_0024LurQgiW6K_00247JkLXoMiyedGk_0024q_0024Ft);
		A_0024WaBDu05Gbdes15XfaAKYE(ref rWBD3h5_0024TYIymf9wdYd7gFc);
		A_0024WaBDu05Gbdes15XfaAKYE(ref kzArpgN77EenM_0024rwG9aUiCt2KRZCwiATI0usnukmFiyK);
		A_0024WaBDu05Gbdes15XfaAKYE(ref _8Cs00R352uKGFd6ZDQVDJOTtdXKvn1kbNr_0024s6_0024BdcmT);
	}

	private void LHKu6HOf37EKE_95Cwdglj8()
	{
		smethod_7((UnityEngine.Object)v9cXvr_0024GnMZFPia_0024zufmsb0);
		smethod_7((UnityEngine.Object)N3m79WJbrajw_0024ojqHMGXreo);
		smethod_7((UnityEngine.Object)BAmibN9na7H3ZTL_cbCgnw8);
		smethod_7((UnityEngine.Object)cq2GOmBacgrU3GCDQxofHaU);
		smethod_7((UnityEngine.Object)OGDA00FhfxLR8vhfgOslQ2yLifyEA3gTPdwK4Nhf5WSO);
		LTIQIOKgpROjbtrHNOCWGs8 = null;
		fKnnJm0rqvY2GMp4B2RPgIo();
	}

	private void OnEnable()
	{
		DNNHvTtfWvRvxiu9RxCdoHQ();
		itEZVRFZOmLFyMZy4zZc9D_Rd97cXyXEzSsK_pSfeEzL();
		n2eBB2V6H1_0024MpK7jLzOfL4Y();
	}

	private void OnDisable()
	{
		LHKu6HOf37EKE_95Cwdglj8();
	}

	private void itEZVRFZOmLFyMZy4zZc9D_Rd97cXyXEzSsK_pSfeEzL()
	{
		if (smethod_4((UnityEngine.Object)gtohyIloJHa1Bs2GBGB_pKHnoo6ekUsuI7c05u5t0Hbw))
		{
			smethod_5(gtohyIloJHa1Bs2GBGB_pKHnoo6ekUsuI7c05u5t0Hbw);
			smethod_6(gtohyIloJHa1Bs2GBGB_pKHnoo6ekUsuI7c05u5t0Hbw);
			smethod_7((UnityEngine.Object)gtohyIloJHa1Bs2GBGB_pKHnoo6ekUsuI7c05u5t0Hbw);
		}
		int int_ = ((smethod_39(xU6_0024sWf25471NIrEawLbg5Y) == 0) ? 2 : smethod_39(xU6_0024sWf25471NIrEawLbg5Y));
		int int_2 = ((smethod_40(xU6_0024sWf25471NIrEawLbg5Y) == 0) ? 2 : smethod_40(xU6_0024sWf25471NIrEawLbg5Y));
		gtohyIloJHa1Bs2GBGB_pKHnoo6ekUsuI7c05u5t0Hbw = smethod_18(int_, int_2, 0, RenderTextureFormat.ARGBHalf);
		smethod_41((Texture)gtohyIloJHa1Bs2GBGB_pKHnoo6ekUsuI7c05u5t0Hbw, TextureWrapMode.Clamp);
		smethod_12((Texture)gtohyIloJHa1Bs2GBGB_pKHnoo6ekUsuI7c05u5t0Hbw, FilterMode.Bilinear);
		smethod_14(gtohyIloJHa1Bs2GBGB_pKHnoo6ekUsuI7c05u5t0Hbw, bool_0: true);
		smethod_13(gtohyIloJHa1Bs2GBGB_pKHnoo6ekUsuI7c05u5t0Hbw, bool_0: false);
		smethod_15(gtohyIloJHa1Bs2GBGB_pKHnoo6ekUsuI7c05u5t0Hbw);
		smethod_16((UnityEngine.Object)gtohyIloJHa1Bs2GBGB_pKHnoo6ekUsuI7c05u5t0Hbw, HideFlags.HideAndDontSave);
		if (smethod_4((UnityEngine.Object)renderTexture_0))
		{
			smethod_5(renderTexture_0);
			smethod_6(renderTexture_0);
			smethod_7((UnityEngine.Object)renderTexture_0);
		}
		renderTexture_0 = smethod_8(int_, int_2, 0, RenderTextureFormat.RFloat, RenderTextureReadWrite.Linear);
		smethod_41((Texture)renderTexture_0, TextureWrapMode.Clamp);
		smethod_12((Texture)renderTexture_0, FilterMode.Bilinear);
		smethod_15(renderTexture_0);
		smethod_16((UnityEngine.Object)renderTexture_0, HideFlags.HideAndDontSave);
	}

	private void wGAFrQtsVJoRWGuJ9T2JYnbu7w3PYj8szYfcrVtXPzhW()
	{
		if (smethod_4((UnityEngine.Object)jDKfAEEsFhXvaiCI3vBP5oM))
		{
			smethod_5(jDKfAEEsFhXvaiCI3vBP5oM);
			smethod_6(jDKfAEEsFhXvaiCI3vBP5oM);
			smethod_7((UnityEngine.Object)jDKfAEEsFhXvaiCI3vBP5oM);
		}
		jDKfAEEsFhXvaiCI3vBP5oM = smethod_8(IE16nOiWFgVvy6eutK1geFvtCyR8KrVUlZ3P_0024onh_y7V, IE16nOiWFgVvy6eutK1geFvtCyR8KrVUlZ3P_0024onh_y7V, 16, RenderTextureFormat.RHalf, RenderTextureReadWrite.Linear);
		smethod_41((Texture)jDKfAEEsFhXvaiCI3vBP5oM, TextureWrapMode.Clamp);
		smethod_12((Texture)jDKfAEEsFhXvaiCI3vBP5oM, FilterMode.Point);
		smethod_15(jDKfAEEsFhXvaiCI3vBP5oM);
		smethod_16((UnityEngine.Object)jDKfAEEsFhXvaiCI3vBP5oM, HideFlags.HideAndDontSave);
	}

	private void Update()
	{
		if (!UXGJ1As6BnxRfO3_NmX60CuIuLJiuMRdI_dRmWw9_0024XtP)
		{
			if (gtohyIloJHa1Bs2GBGB_pKHnoo6ekUsuI7c05u5t0Hbw == null)
			{
				itEZVRFZOmLFyMZy4zZc9D_Rd97cXyXEzSsK_pSfeEzL();
			}
			if (gtohyIloJHa1Bs2GBGB_pKHnoo6ekUsuI7c05u5t0Hbw.width != xU6_0024sWf25471NIrEawLbg5Y.pixelWidth || gtohyIloJHa1Bs2GBGB_pKHnoo6ekUsuI7c05u5t0Hbw.height != xU6_0024sWf25471NIrEawLbg5Y.pixelHeight)
			{
				itEZVRFZOmLFyMZy4zZc9D_Rd97cXyXEzSsK_pSfeEzL();
			}
			if (IE16nOiWFgVvy6eutK1geFvtCyR8KrVUlZ3P_0024onh_y7V != zVHmE2bRLLFjwMIM3jC0U2s_0024kg1piRAhS2_00248LXBUcDkT)
			{
				wGAFrQtsVJoRWGuJ9T2JYnbu7w3PYj8szYfcrVtXPzhW();
			}
			zVHmE2bRLLFjwMIM3jC0U2s_0024kg1piRAhS2_00248LXBUcDkT = IE16nOiWFgVvy6eutK1geFvtCyR8KrVUlZ3P_0024onh_y7V;
			if (renderTexture_1[0].width != (int)voxelResolution)
			{
				GqyUSKTh1clCJUinryCu9Mo82P_4nR2THau6of8AsSNA();
			}
			if (kzArpgN77EenM_0024rwG9aUiCt2KRZCwiATI0usnukmFiyK.width != dummyVoxelResolution)
			{
				X5ZL_0024eLYqBX9ZqQQnL1VHc5FURIsW8Aghqr10ZL_d0cy();
			}
		}
	}

	private Matrix4x4 xqe6xxhwbDnct0x65upSW9o_y5XYD0NR6rQkNDXMf9XV(Matrix4x4 matrix4x4_0)
	{
		if (smethod_42())
		{
			matrix4x4_0[2, 0] = 0f - matrix4x4_0[2, 0];
			matrix4x4_0[2, 1] = 0f - matrix4x4_0[2, 1];
			matrix4x4_0[2, 2] = 0f - matrix4x4_0[2, 2];
			matrix4x4_0[2, 3] = 0f - matrix4x4_0[2, 3];
		}
		return matrix4x4_0;
	}

	private void OnPreRender()
	{
		DNNHvTtfWvRvxiu9RxCdoHQ();
		if (UXGJ1As6BnxRfO3_NmX60CuIuLJiuMRdI_dRmWw9_0024XtP || !updateGI)
		{
			return;
		}
		RenderTexture active = RenderTexture.active;
		Shader.SetGlobalInt("SEGIVoxelAA", voxelAA ? 1 : 0);
		if (x_7qq7jXOw7w_00249RgTuwON2U == SBPAmo0wGlHXa4G23uvylMDoQx2xUFANeE9GQoOn4b_0024K.Voxelize)
		{
			_0024K_16I5_jZB6F8pQNPbtu4M = ((MoMGhYR6ydOb81kD6COeafU == 0) ? renderTexture_1[0] : rWBD3h5_0024TYIymf9wdYd7gFc);
			renderTexture_2 = ((MoMGhYR6ydOb81kD6COeafU == 0) ? rWBD3h5_0024TYIymf9wdYd7gFc : renderTexture_1[0]);
			float num = voxelSpaceSize / 8f;
			Vector3 vector = ((!followTransform) ? (base.transform.position + base.transform.forward * voxelSpaceSize / 4f) : followTransform.position);
			jrSex6S8C07dIdTRGErwp8ito605hWWmPmMb4X3eai_9 = new Vector3(Mathf.Round(vector.x / num) * num, Mathf.Round(vector.y / num) * num, Mathf.Round(vector.z / num) * num);
			fvQXa_0024Rygoic_lY8_2bkg7Ijlursi0xZcqx32SpcpjbP = jrSex6S8C07dIdTRGErwp8ito605hWWmPmMb4X3eai_9 - Ya1pUtOuDK0XDhm7IYX_HvpbYOYW9_XMqOJBzl9X2Ol2;
			Shader.SetGlobalVector("SEGIVoxelSpaceOriginDelta", fvQXa_0024Rygoic_lY8_2bkg7Ijlursi0xZcqx32SpcpjbP / voxelSpaceSize);
			Ya1pUtOuDK0XDhm7IYX_HvpbYOYW9_XMqOJBzl9X2Ol2 = jrSex6S8C07dIdTRGErwp8ito605hWWmPmMb4X3eai_9;
			O1O7t2ivipE0i32PfGcalvo.enabled = false;
			O1O7t2ivipE0i32PfGcalvo.orthographic = true;
			O1O7t2ivipE0i32PfGcalvo.orthographicSize = voxelSpaceSize * 0.5f;
			O1O7t2ivipE0i32PfGcalvo.nearClipPlane = 0f;
			O1O7t2ivipE0i32PfGcalvo.farClipPlane = voxelSpaceSize;
			O1O7t2ivipE0i32PfGcalvo.depth = -2f;
			O1O7t2ivipE0i32PfGcalvo.renderingPath = RenderingPath.Forward;
			O1O7t2ivipE0i32PfGcalvo.clearFlags = CameraClearFlags.Color;
			O1O7t2ivipE0i32PfGcalvo.backgroundColor = Color.black;
			O1O7t2ivipE0i32PfGcalvo.cullingMask = giCullingMask;
			N3m79WJbrajw_0024ojqHMGXreo.transform.position = jrSex6S8C07dIdTRGErwp8ito605hWWmPmMb4X3eai_9 - Vector3.forward * voxelSpaceSize * 0.5f;
			N3m79WJbrajw_0024ojqHMGXreo.transform.rotation = B3y1jaoOHb27vrqRtfshUXg;
			BAmibN9na7H3ZTL_cbCgnw8.transform.position = jrSex6S8C07dIdTRGErwp8ito605hWWmPmMb4X3eai_9 + Vector3.left * voxelSpaceSize * 0.5f;
			BAmibN9na7H3ZTL_cbCgnw8.transform.rotation = SUP__0024VYm7_0024LaHj_I7j6eZKM;
			cq2GOmBacgrU3GCDQxofHaU.transform.position = jrSex6S8C07dIdTRGErwp8ito605hWWmPmMb4X3eai_9 + Vector3.up * voxelSpaceSize * 0.5f;
			cq2GOmBacgrU3GCDQxofHaU.transform.rotation = t5WXs069egBfeiL0IkAlRN4;
			Shader.SetGlobalMatrix("WorldToCamera", xU6_0024sWf25471NIrEawLbg5Y.worldToCameraMatrix);
			Shader.SetGlobalMatrix("SEGIVoxelViewFront", xqe6xxhwbDnct0x65upSW9o_y5XYD0NR6rQkNDXMf9XV(O1O7t2ivipE0i32PfGcalvo.transform.worldToLocalMatrix));
			Shader.SetGlobalMatrix("SEGIVoxelViewLeft", xqe6xxhwbDnct0x65upSW9o_y5XYD0NR6rQkNDXMf9XV(BAmibN9na7H3ZTL_cbCgnw8.transform.worldToLocalMatrix));
			Shader.SetGlobalMatrix("SEGIVoxelViewTop", xqe6xxhwbDnct0x65upSW9o_y5XYD0NR6rQkNDXMf9XV(cq2GOmBacgrU3GCDQxofHaU.transform.worldToLocalMatrix));
			Shader.SetGlobalMatrix("SEGIWorldToVoxel", O1O7t2ivipE0i32PfGcalvo.worldToCameraMatrix);
			Shader.SetGlobalMatrix("SEGIVoxelProjection", O1O7t2ivipE0i32PfGcalvo.projectionMatrix);
			Shader.SetGlobalMatrix("SEGIVoxelProjectionInverse", O1O7t2ivipE0i32PfGcalvo.projectionMatrix.inverse);
			Shader.SetGlobalInt("SEGIVoxelResolution", (int)voxelResolution);
			Matrix4x4 value = h9tJJ72fwhl0tAHi8xVmzwE.projectionMatrix * h9tJJ72fwhl0tAHi8xVmzwE.worldToCameraMatrix * O1O7t2ivipE0i32PfGcalvo.cameraToWorldMatrix;
			Shader.SetGlobalMatrix("SEGIVoxelToGIProjection", value);
			Shader.SetGlobalVector("SEGISunlightVector", sun ? Vector3.Normalize(sun.transform.forward) : Vector3.up);
			Shader.SetGlobalColor("GISunColor", (sun == null) ? Color.black : new Color(Mathf.Pow(sun.color.r, 2.2f), Mathf.Pow(sun.color.g, 2.2f), Mathf.Pow(sun.color.b, 2.2f), Mathf.Pow(sun.intensity, 2.2f)));
			Shader.SetGlobalColor("SEGISkyColor", new Color(Mathf.Pow(skyColor.r * skyIntensity * 0.5f, 2.2f), Mathf.Pow(skyColor.g * skyIntensity * 0.5f, 2.2f), Mathf.Pow(skyColor.b * skyIntensity * 0.5f, 2.2f), Mathf.Pow(skyColor.a, 2.2f)));
			Shader.SetGlobalFloat("GIGain", giGain);
			Shader.SetGlobalFloat("SEGISecondaryBounceGain", infiniteBounces ? secondaryBounceGain : 0f);
			Shader.SetGlobalFloat("SEGISoftSunlight", softSunlight);
			Shader.SetGlobalInt("SEGISphericalSkylight", sphericalSkylight ? 1 : 0);
			Shader.SetGlobalInt("SEGIInnerOcclusionLayers", innerOcclusionLayers);
			if (sun != null)
			{
				h9tJJ72fwhl0tAHi8xVmzwE.cullingMask = giCullingMask;
				Vector3 position = jrSex6S8C07dIdTRGErwp8ito605hWWmPmMb4X3eai_9 + Vector3.Normalize(-sun.transform.forward) * shadowSpaceSize * 0.5f * DjBTgKiMJa1J56WSL4LIjyBPPRkJkDJeG2NLvCOtr_0024AV;
				transform_0.position = position;
				transform_0.LookAt(jrSex6S8C07dIdTRGErwp8ito605hWWmPmMb4X3eai_9, Vector3.up);
				h9tJJ72fwhl0tAHi8xVmzwE.renderingPath = RenderingPath.Forward;
				h9tJJ72fwhl0tAHi8xVmzwE.depthTextureMode |= DepthTextureMode.None;
				h9tJJ72fwhl0tAHi8xVmzwE.orthographicSize = shadowSpaceSize;
				h9tJJ72fwhl0tAHi8xVmzwE.farClipPlane = shadowSpaceSize * 2f * DjBTgKiMJa1J56WSL4LIjyBPPRkJkDJeG2NLvCOtr_0024AV;
				Graphics.SetRenderTarget(jDKfAEEsFhXvaiCI3vBP5oM);
				h9tJJ72fwhl0tAHi8xVmzwE.SetTargetBuffers(jDKfAEEsFhXvaiCI3vBP5oM.colorBuffer, jDKfAEEsFhXvaiCI3vBP5oM.depthBuffer);
				h9tJJ72fwhl0tAHi8xVmzwE.RenderWithShader(shader_0, "");
				Shader.SetGlobalTexture("SEGISunDepth", jDKfAEEsFhXvaiCI3vBP5oM);
			}
			JtBuENn09KKbt4VusdO6fs4.SetTexture(0, "RG0", BGMycV3Th9mx6IWJHaC3bdc);
			JtBuENn09KKbt4VusdO6fs4.SetInt("Res", (int)voxelResolution);
			JtBuENn09KKbt4VusdO6fs4.Dispatch(0, (int)voxelResolution / 16, (int)voxelResolution / 16, 1);
			Graphics.SetRandomWriteTarget(1, BGMycV3Th9mx6IWJHaC3bdc);
			O1O7t2ivipE0i32PfGcalvo.targetTexture = kzArpgN77EenM_0024rwG9aUiCt2KRZCwiATI0usnukmFiyK;
			O1O7t2ivipE0i32PfGcalvo.RenderWithShader(shader_1, "");
			Graphics.ClearRandomWriteTargets();
			QKm_iaJDcMtaPfHUcx_kAsoHyrVV6_ulniDKzWGmmbMa.SetTexture(0, "Result", _0024K_16I5_jZB6F8pQNPbtu4M);
			QKm_iaJDcMtaPfHUcx_kAsoHyrVV6_ulniDKzWGmmbMa.SetTexture(0, "PrevResult", renderTexture_2);
			QKm_iaJDcMtaPfHUcx_kAsoHyrVV6_ulniDKzWGmmbMa.SetTexture(0, "RG0", BGMycV3Th9mx6IWJHaC3bdc);
			QKm_iaJDcMtaPfHUcx_kAsoHyrVV6_ulniDKzWGmmbMa.SetInt("VoxelAA", voxelAA ? 1 : 0);
			QKm_iaJDcMtaPfHUcx_kAsoHyrVV6_ulniDKzWGmmbMa.SetInt("Resolution", (int)voxelResolution);
			QKm_iaJDcMtaPfHUcx_kAsoHyrVV6_ulniDKzWGmmbMa.SetVector("VoxelOriginDelta", fvQXa_0024Rygoic_lY8_2bkg7Ijlursi0xZcqx32SpcpjbP / voxelSpaceSize * (float)voxelResolution);
			QKm_iaJDcMtaPfHUcx_kAsoHyrVV6_ulniDKzWGmmbMa.Dispatch(0, (int)voxelResolution / 16, (int)voxelResolution / 16, 1);
			Shader.SetGlobalTexture("SEGIVolumeLevel0", _0024K_16I5_jZB6F8pQNPbtu4M);
			for (int i = 0; i < 5; i++)
			{
				RenderTexture texture = renderTexture_1[i];
				if (i == 0)
				{
					texture = _0024K_16I5_jZB6F8pQNPbtu4M;
				}
				int num2 = (int)voxelResolution / Mathf.RoundToInt(Mathf.Pow(2f, (float)i + 1f));
				P6ncUp7E7lcAN5iCoxdiu5AFMqp4Jid406_S_DCe3pLh.SetInt("destinationRes", num2);
				P6ncUp7E7lcAN5iCoxdiu5AFMqp4Jid406_S_DCe3pLh.SetTexture(mipFilterKernel, "Source", texture);
				P6ncUp7E7lcAN5iCoxdiu5AFMqp4Jid406_S_DCe3pLh.SetTexture(mipFilterKernel, "Destination", renderTexture_1[i + 1]);
				P6ncUp7E7lcAN5iCoxdiu5AFMqp4Jid406_S_DCe3pLh.Dispatch(mipFilterKernel, num2 / 8, num2 / 8, 1);
				Shader.SetGlobalTexture("SEGIVolumeLevel" + (i + 1), renderTexture_1[i + 1]);
			}
			MoMGhYR6ydOb81kD6COeafU++;
			MoMGhYR6ydOb81kD6COeafU %= 2;
			if (infiniteBounces)
			{
				x_7qq7jXOw7w_00249RgTuwON2U = SBPAmo0wGlHXa4G23uvylMDoQx2xUFANeE9GQoOn4b_0024K.Bounce;
			}
		}
		else if (x_7qq7jXOw7w_00249RgTuwON2U == SBPAmo0wGlHXa4G23uvylMDoQx2xUFANeE9GQoOn4b_0024K.Bounce)
		{
			JtBuENn09KKbt4VusdO6fs4.SetTexture(0, "RG0", BGMycV3Th9mx6IWJHaC3bdc);
			JtBuENn09KKbt4VusdO6fs4.Dispatch(0, (int)voxelResolution / 16, (int)voxelResolution / 16, 1);
			Shader.SetGlobalInt("SEGISecondaryCones", secondaryCones);
			Shader.SetGlobalFloat("SEGISecondaryOcclusionStrength", secondaryOcclusionStrength);
			Graphics.SetRandomWriteTarget(1, BGMycV3Th9mx6IWJHaC3bdc);
			O1O7t2ivipE0i32PfGcalvo.targetTexture = _8Cs00R352uKGFd6ZDQVDJOTtdXKvn1kbNr_0024s6_0024BdcmT;
			O1O7t2ivipE0i32PfGcalvo.RenderWithShader(yagnK_Ylg_0024mphSgUp3cc1hVPTHg7eb0r_0024OyTD5m3IPch, "");
			Graphics.ClearRandomWriteTargets();
			QKm_iaJDcMtaPfHUcx_kAsoHyrVV6_ulniDKzWGmmbMa.SetTexture(1, "Result", iixTCydx_0024YSeML5_0024LurQgiW6K_00247JkLXoMiyedGk_0024q_0024Ft);
			QKm_iaJDcMtaPfHUcx_kAsoHyrVV6_ulniDKzWGmmbMa.SetTexture(1, "RG0", BGMycV3Th9mx6IWJHaC3bdc);
			QKm_iaJDcMtaPfHUcx_kAsoHyrVV6_ulniDKzWGmmbMa.SetInt("Resolution", (int)voxelResolution);
			QKm_iaJDcMtaPfHUcx_kAsoHyrVV6_ulniDKzWGmmbMa.Dispatch(1, (int)voxelResolution / 16, (int)voxelResolution / 16, 1);
			Shader.SetGlobalTexture("SEGIVolumeTexture1", iixTCydx_0024YSeML5_0024LurQgiW6K_00247JkLXoMiyedGk_0024q_0024Ft);
			x_7qq7jXOw7w_00249RgTuwON2U = SBPAmo0wGlHXa4G23uvylMDoQx2xUFANeE9GQoOn4b_0024K.Voxelize;
		}
		RenderTexture.active = active;
	}

	[ImageEffectOpaque]
	private void OnRenderImage(RenderTexture source, RenderTexture destination)
	{
		if (UXGJ1As6BnxRfO3_NmX60CuIuLJiuMRdI_dRmWw9_0024XtP)
		{
			Graphics.Blit(source, destination);
			return;
		}
		Shader.SetGlobalFloat("SEGIVoxelScaleFactor", voxelScaleFactor);
		v9cXvr_0024GnMZFPia_0024zufmsb0.SetMatrix("CameraToWorld", xU6_0024sWf25471NIrEawLbg5Y.cameraToWorldMatrix);
		v9cXvr_0024GnMZFPia_0024zufmsb0.SetMatrix("WorldToCamera", xU6_0024sWf25471NIrEawLbg5Y.worldToCameraMatrix);
		v9cXvr_0024GnMZFPia_0024zufmsb0.SetMatrix("ProjectionMatrixInverse", xU6_0024sWf25471NIrEawLbg5Y.projectionMatrix.inverse);
		v9cXvr_0024GnMZFPia_0024zufmsb0.SetMatrix("ProjectionMatrix", xU6_0024sWf25471NIrEawLbg5Y.projectionMatrix);
		v9cXvr_0024GnMZFPia_0024zufmsb0.SetInt("FrameSwitch", JzFwzFRpbj_F0sJ_0024ctfffyk);
		Shader.SetGlobalInt("SEGIFrameSwitch", JzFwzFRpbj_F0sJ_0024ctfffyk);
		v9cXvr_0024GnMZFPia_0024zufmsb0.SetVector("CameraPosition", base.transform.position);
		v9cXvr_0024GnMZFPia_0024zufmsb0.SetFloat("DeltaTime", Time.deltaTime);
		v9cXvr_0024GnMZFPia_0024zufmsb0.SetInt("StochasticSampling", stochasticSampling ? 1 : 0);
		v9cXvr_0024GnMZFPia_0024zufmsb0.SetInt("TraceDirections", cones);
		v9cXvr_0024GnMZFPia_0024zufmsb0.SetInt("TraceSteps", coneTraceSteps);
		v9cXvr_0024GnMZFPia_0024zufmsb0.SetFloat("TraceLength", coneLength);
		v9cXvr_0024GnMZFPia_0024zufmsb0.SetFloat("ConeSize", coneWidth);
		v9cXvr_0024GnMZFPia_0024zufmsb0.SetFloat("OcclusionStrength", occlusionStrength);
		v9cXvr_0024GnMZFPia_0024zufmsb0.SetFloat("OcclusionPower", occlusionPower);
		v9cXvr_0024GnMZFPia_0024zufmsb0.SetFloat("ConeTraceBias", coneTraceBias);
		v9cXvr_0024GnMZFPia_0024zufmsb0.SetFloat("GIGain", giGain);
		v9cXvr_0024GnMZFPia_0024zufmsb0.SetFloat("NearLightGain", nearLightGain);
		v9cXvr_0024GnMZFPia_0024zufmsb0.SetFloat("NearOcclusionStrength", nearOcclusionStrength);
		v9cXvr_0024GnMZFPia_0024zufmsb0.SetInt("DoReflections", doReflections ? 1 : 0);
		v9cXvr_0024GnMZFPia_0024zufmsb0.SetInt("HalfResolution", halfResolution ? 1 : 0);
		v9cXvr_0024GnMZFPia_0024zufmsb0.SetInt("ReflectionSteps", reflectionSteps);
		v9cXvr_0024GnMZFPia_0024zufmsb0.SetFloat("ReflectionOcclusionPower", reflectionOcclusionPower);
		v9cXvr_0024GnMZFPia_0024zufmsb0.SetFloat("SkyReflectionIntensity", skyReflectionIntensity);
		v9cXvr_0024GnMZFPia_0024zufmsb0.SetFloat("FarOcclusionStrength", farOcclusionStrength);
		v9cXvr_0024GnMZFPia_0024zufmsb0.SetFloat("FarthestOcclusionStrength", farthestOcclusionStrength);
		v9cXvr_0024GnMZFPia_0024zufmsb0.SetTexture("NoiseTexture", JL1cXRJ7RTR3zZj248SYVx0[JzFwzFRpbj_F0sJ_0024ctfffyk % 64]);
		v9cXvr_0024GnMZFPia_0024zufmsb0.SetFloat("BlendWeight", temporalBlendWeight);
		if (visualizeVoxels)
		{
			Graphics.Blit(source, destination, v9cXvr_0024GnMZFPia_0024zufmsb0, qK929_0024Id7SkRqjEjesCnAz0.iW7x_0024CEd6Tc1H8QLwpx770k);
			return;
		}
		RenderTexture temporary = RenderTexture.GetTemporary(source.width / giRenderRes, source.height / giRenderRes, 0, RenderTextureFormat.ARGBHalf);
		RenderTexture temporary2 = RenderTexture.GetTemporary(source.width / giRenderRes, source.height / giRenderRes, 0, RenderTextureFormat.ARGBHalf);
		RenderTexture renderTexture = null;
		if (doReflections)
		{
			renderTexture = RenderTexture.GetTemporary(source.width, source.height, 0, RenderTextureFormat.ARGBHalf);
		}
		RenderTexture temporary3 = RenderTexture.GetTemporary(source.width / giRenderRes, source.height / giRenderRes, 0, RenderTextureFormat.RFloat, RenderTextureReadWrite.Linear);
		temporary3.filterMode = FilterMode.Point;
		RenderTexture temporary4 = RenderTexture.GetTemporary(source.width / giRenderRes, source.height / giRenderRes, 0, RenderTextureFormat.ARGBHalf, RenderTextureReadWrite.Linear);
		temporary4.filterMode = FilterMode.Point;
		Graphics.Blit(source, temporary3, v9cXvr_0024GnMZFPia_0024zufmsb0, qK929_0024Id7SkRqjEjesCnAz0.I1TX9fwl20itYRYIa86_ughcPSUkktmKuTkesGpikW9G);
		v9cXvr_0024GnMZFPia_0024zufmsb0.SetTexture("CurrentDepth", temporary3);
		Graphics.Blit(source, temporary4, v9cXvr_0024GnMZFPia_0024zufmsb0, qK929_0024Id7SkRqjEjesCnAz0.EO2KTni9LQqLLv9FybVNc0I);
		v9cXvr_0024GnMZFPia_0024zufmsb0.SetTexture("CurrentNormal", temporary4);
		v9cXvr_0024GnMZFPia_0024zufmsb0.SetTexture("PreviousGITexture", gtohyIloJHa1Bs2GBGB_pKHnoo6ekUsuI7c05u5t0Hbw);
		Shader.SetGlobalTexture("PreviousGITexture", gtohyIloJHa1Bs2GBGB_pKHnoo6ekUsuI7c05u5t0Hbw);
		v9cXvr_0024GnMZFPia_0024zufmsb0.SetTexture("PreviousDepth", renderTexture_0);
		Graphics.Blit(source, temporary2, v9cXvr_0024GnMZFPia_0024zufmsb0, qK929_0024Id7SkRqjEjesCnAz0.B6PpKyUGqLQ5wtn3ZYSUqMY);
		if (doReflections)
		{
			Graphics.Blit(source, renderTexture, v9cXvr_0024GnMZFPia_0024zufmsb0, qK929_0024Id7SkRqjEjesCnAz0.ojxdZvkss5oUP68uIvL3Od0);
			v9cXvr_0024GnMZFPia_0024zufmsb0.SetTexture("Reflections", renderTexture);
		}
		if (useBilateralFiltering)
		{
			v9cXvr_0024GnMZFPia_0024zufmsb0.SetVector("Kernel", new Vector2(0f, 1f));
			Graphics.Blit(temporary2, temporary, v9cXvr_0024GnMZFPia_0024zufmsb0, qK929_0024Id7SkRqjEjesCnAz0.oyfhQsh2G93SjLRR4H1yV7I);
			v9cXvr_0024GnMZFPia_0024zufmsb0.SetVector("Kernel", new Vector2(1f, 0f));
			Graphics.Blit(temporary, temporary2, v9cXvr_0024GnMZFPia_0024zufmsb0, qK929_0024Id7SkRqjEjesCnAz0.oyfhQsh2G93SjLRR4H1yV7I);
			v9cXvr_0024GnMZFPia_0024zufmsb0.SetVector("Kernel", new Vector2(0f, 1f));
			Graphics.Blit(temporary2, temporary, v9cXvr_0024GnMZFPia_0024zufmsb0, qK929_0024Id7SkRqjEjesCnAz0.oyfhQsh2G93SjLRR4H1yV7I);
			v9cXvr_0024GnMZFPia_0024zufmsb0.SetVector("Kernel", new Vector2(1f, 0f));
			Graphics.Blit(temporary, temporary2, v9cXvr_0024GnMZFPia_0024zufmsb0, qK929_0024Id7SkRqjEjesCnAz0.oyfhQsh2G93SjLRR4H1yV7I);
		}
		if (giRenderRes == 2)
		{
			RenderTexture.ReleaseTemporary(temporary);
			RenderTexture temporary5 = RenderTexture.GetTemporary(source.width, source.height, 0, RenderTextureFormat.ARGBHalf);
			RenderTexture temporary6 = RenderTexture.GetTemporary(source.width, source.height, 0, RenderTextureFormat.ARGBHalf);
			temporary2.filterMode = FilterMode.Point;
			Graphics.Blit(temporary2, temporary6);
			RenderTexture.ReleaseTemporary(temporary2);
			temporary6.filterMode = FilterMode.Point;
			temporary5.filterMode = FilterMode.Point;
			v9cXvr_0024GnMZFPia_0024zufmsb0.SetVector("Kernel", new Vector2(1f, 0f));
			Graphics.Blit(temporary6, temporary5, v9cXvr_0024GnMZFPia_0024zufmsb0, qK929_0024Id7SkRqjEjesCnAz0.Gf_0024AuukWAf6RonQAXybhMEZuhIG9EZetLdi1ZMKcVge9);
			v9cXvr_0024GnMZFPia_0024zufmsb0.SetVector("Kernel", new Vector2(0f, 1f));
			if (temporalBlendWeight < 1f)
			{
				Graphics.Blit(temporary5, temporary6);
				Graphics.Blit(temporary6, temporary5, v9cXvr_0024GnMZFPia_0024zufmsb0, qK929_0024Id7SkRqjEjesCnAz0.UyNCGtYyPlDyRu2bnc67Q4c);
				Graphics.Blit(temporary5, gtohyIloJHa1Bs2GBGB_pKHnoo6ekUsuI7c05u5t0Hbw);
				Graphics.Blit(source, renderTexture_0, v9cXvr_0024GnMZFPia_0024zufmsb0, qK929_0024Id7SkRqjEjesCnAz0.I1TX9fwl20itYRYIa86_ughcPSUkktmKuTkesGpikW9G);
			}
			v9cXvr_0024GnMZFPia_0024zufmsb0.SetTexture("GITexture", temporary5);
			Graphics.Blit(source, destination, v9cXvr_0024GnMZFPia_0024zufmsb0, visualizeGI ? qK929_0024Id7SkRqjEjesCnAz0.OuZtpHSmXY8upCgmoSxBrHY : qK929_0024Id7SkRqjEjesCnAz0.GdkzUKeEbmK2QOpGiGcEbH4);
			RenderTexture.ReleaseTemporary(temporary5);
			RenderTexture.ReleaseTemporary(temporary6);
		}
		else
		{
			if (temporalBlendWeight < 1f)
			{
				Graphics.Blit(temporary2, temporary, v9cXvr_0024GnMZFPia_0024zufmsb0, qK929_0024Id7SkRqjEjesCnAz0.UyNCGtYyPlDyRu2bnc67Q4c);
				Graphics.Blit(temporary, gtohyIloJHa1Bs2GBGB_pKHnoo6ekUsuI7c05u5t0Hbw);
				Graphics.Blit(source, renderTexture_0, v9cXvr_0024GnMZFPia_0024zufmsb0, qK929_0024Id7SkRqjEjesCnAz0.I1TX9fwl20itYRYIa86_ughcPSUkktmKuTkesGpikW9G);
			}
			v9cXvr_0024GnMZFPia_0024zufmsb0.SetTexture("GITexture", (temporalBlendWeight < 1f) ? temporary : temporary2);
			Graphics.Blit(source, destination, v9cXvr_0024GnMZFPia_0024zufmsb0, visualizeGI ? qK929_0024Id7SkRqjEjesCnAz0.OuZtpHSmXY8upCgmoSxBrHY : qK929_0024Id7SkRqjEjesCnAz0.GdkzUKeEbmK2QOpGiGcEbH4);
			RenderTexture.ReleaseTemporary(temporary);
			RenderTexture.ReleaseTemporary(temporary2);
		}
		RenderTexture.ReleaseTemporary(temporary3);
		RenderTexture.ReleaseTemporary(temporary4);
		if (visualizeSunDepthTexture)
		{
			Graphics.Blit(jDKfAEEsFhXvaiCI3vBP5oM, destination);
		}
		if (doReflections)
		{
			RenderTexture.ReleaseTemporary(renderTexture);
		}
		v9cXvr_0024GnMZFPia_0024zufmsb0.SetMatrix("ProjectionPrev", xU6_0024sWf25471NIrEawLbg5Y.projectionMatrix);
		v9cXvr_0024GnMZFPia_0024zufmsb0.SetMatrix("ProjectionPrevInverse", xU6_0024sWf25471NIrEawLbg5Y.projectionMatrix.inverse);
		v9cXvr_0024GnMZFPia_0024zufmsb0.SetMatrix("WorldToCameraPrev", xU6_0024sWf25471NIrEawLbg5Y.worldToCameraMatrix);
		v9cXvr_0024GnMZFPia_0024zufmsb0.SetMatrix("CameraToWorldPrev", xU6_0024sWf25471NIrEawLbg5Y.cameraToWorldMatrix);
		v9cXvr_0024GnMZFPia_0024zufmsb0.SetVector("CameraPositionPrev", base.transform.position);
		JzFwzFRpbj_F0sJ_0024ctfffyk = (JzFwzFRpbj_F0sJ_0024ctfffyk + 1) % 64;
	}

	internal static bool smethod_0(UnityEngine.Object object_0, UnityEngine.Object object_1)
	{
		return object_0 != object_1;
	}

	internal static int smethod_1(Texture texture_0)
	{
		return texture_0.width;
	}

	internal static int smethod_2(Texture texture_0)
	{
		return texture_0.height;
	}

	internal static int smethod_3(RenderTexture renderTexture_3)
	{
		return renderTexture_3.volumeDepth;
	}

	internal static bool smethod_4(UnityEngine.Object object_0)
	{
		return object_0;
	}

	internal static void smethod_5(RenderTexture renderTexture_3)
	{
		renderTexture_3.DiscardContents();
	}

	internal static void smethod_6(RenderTexture renderTexture_3)
	{
		renderTexture_3.Release();
	}

	internal static void smethod_7(UnityEngine.Object object_0)
	{
		UnityEngine.Object.DestroyImmediate(object_0);
	}

	internal static RenderTexture smethod_8(int int_1, int int_2, int int_3, RenderTextureFormat renderTextureFormat_0, RenderTextureReadWrite renderTextureReadWrite_0)
	{
		return new RenderTexture(int_1, int_2, int_3, renderTextureFormat_0, renderTextureReadWrite_0);
	}

	internal static void smethod_9(RenderTexture renderTexture_3, bool bool_0)
	{
		renderTexture_3.isVolume = bool_0;
	}

	internal static void smethod_10(RenderTexture renderTexture_3, int int_1)
	{
		renderTexture_3.volumeDepth = int_1;
	}

	internal static void smethod_11(RenderTexture renderTexture_3, bool bool_0)
	{
		renderTexture_3.enableRandomWrite = bool_0;
	}

	internal static void smethod_12(Texture texture_0, FilterMode filterMode_0)
	{
		texture_0.filterMode = filterMode_0;
	}

	internal static void smethod_13(RenderTexture renderTexture_3, bool bool_0)
	{
		renderTexture_3.autoGenerateMips = bool_0;
	}

	internal static void smethod_14(RenderTexture renderTexture_3, bool bool_0)
	{
		renderTexture_3.useMipMap = bool_0;
	}

	internal static bool smethod_15(RenderTexture renderTexture_3)
	{
		return renderTexture_3.Create();
	}

	internal static void smethod_16(UnityEngine.Object object_0, HideFlags hideFlags_0)
	{
		object_0.hideFlags = hideFlags_0;
	}

	internal static void smethod_17(RenderTexture renderTexture_3, int int_1)
	{
		renderTexture_3.antiAliasing = int_1;
	}

	internal static RenderTexture smethod_18(int int_1, int int_2, int int_3, RenderTextureFormat renderTextureFormat_0)
	{
		return new RenderTexture(int_1, int_2, int_3, renderTextureFormat_0);
	}

	internal static Material smethod_19(Shader shader_2)
	{
		return new Material(shader_2);
	}

	internal static DepthTextureMode smethod_20(Camera camera_0)
	{
		return camera_0.depthTextureMode;
	}

	internal static void smethod_21(Camera camera_0, DepthTextureMode depthTextureMode_0)
	{
		camera_0.depthTextureMode = depthTextureMode_0;
	}

	internal static GameObject smethod_22(string string_0)
	{
		return GameObject.Find(string_0);
	}

	internal static GameObject smethod_23(string string_0)
	{
		return new GameObject(string_0);
	}

	internal static void smethod_24(Behaviour behaviour_0, bool bool_0)
	{
		behaviour_0.enabled = bool_0;
	}

	internal static float smethod_25(Camera camera_0)
	{
		return camera_0.depth;
	}

	internal static void smethod_26(Camera camera_0, float float_0)
	{
		camera_0.depth = float_0;
	}

	internal static void smethod_27(Camera camera_0, bool bool_0)
	{
		camera_0.orthographic = bool_0;
	}

	internal static void smethod_28(Camera camera_0, float float_0)
	{
		camera_0.orthographicSize = float_0;
	}

	internal static void smethod_29(Camera camera_0, CameraClearFlags cameraClearFlags_0)
	{
		camera_0.clearFlags = cameraClearFlags_0;
	}

	internal static bool smethod_30(RenderTextureFormat renderTextureFormat_0)
	{
		return SystemInfo.SupportsRenderTextureFormat(renderTextureFormat_0);
	}

	internal static int smethod_31()
	{
		return SystemInfo.graphicsShaderLevel;
	}

	internal static bool smethod_32()
	{
		return SystemInfo.supportsComputeShaders;
	}

	internal static bool smethod_33()
	{
		return SystemInfo.supports3DTextures;
	}

	internal static Shader smethod_34(Material material_0)
	{
		return material_0.shader;
	}

	internal static bool smethod_35(Shader shader_2)
	{
		return shader_2.isSupported;
	}

	internal static void smethod_36(object object_0)
	{
		Debug.LogWarning(object_0);
	}

	internal static void smethod_37(Behaviour behaviour_0, bool bool_0)
	{
		behaviour_0.enabled = bool_0;
	}

	internal static Color smethod_38()
	{
		return Gizmos.color;
	}

	internal static int smethod_39(Camera camera_0)
	{
		return camera_0.pixelWidth;
	}

	internal static int smethod_40(Camera camera_0)
	{
		return camera_0.pixelHeight;
	}

	internal static void smethod_41(Texture texture_0, TextureWrapMode textureWrapMode_0)
	{
		texture_0.wrapMode = textureWrapMode_0;
	}

	internal static bool smethod_42()
	{
		return SystemInfo.usesReversedZBuffer;
	}
}
