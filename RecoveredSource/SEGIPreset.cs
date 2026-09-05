using UnityEngine;

public class SEGIPreset : ScriptableObject
{
	public SEGI.VoxelResolution voxelResolution = SEGI.VoxelResolution.high;

	public bool voxelAA;

	[Range(0f, 2f)]
	public int innerOcclusionLayers = 1;

	public bool infiniteBounces = true;

	[Range(0.01f, 1f)]
	public float temporalBlendWeight = 0.15f;

	public bool useBilateralFiltering = true;

	public bool halfResolution = true;

	public bool stochasticSampling = true;

	public bool doReflections = true;

	[Range(1f, 128f)]
	public int cones = 13;

	[Range(1f, 32f)]
	public int coneTraceSteps = 8;

	[Range(0.1f, 2f)]
	public float coneLength = 1f;

	[Range(0.5f, 6f)]
	public float coneWidth = 6f;

	[Range(0f, 4f)]
	public float coneTraceBias = 0.63f;

	[Range(0f, 4f)]
	public float occlusionStrength = 1f;

	[Range(0f, 4f)]
	public float nearOcclusionStrength;

	[Range(0.001f, 4f)]
	public float occlusionPower = 1f;

	[Range(0f, 4f)]
	public float nearLightGain = 1f;

	[Range(0f, 4f)]
	public float giGain = 1f;

	[Range(0f, 2f)]
	public float secondaryBounceGain = 1f;

	[Range(12f, 128f)]
	public int reflectionSteps = 64;

	[Range(0.001f, 4f)]
	public float reflectionOcclusionPower = 1f;

	[Range(0f, 1f)]
	public float skyReflectionIntensity = 1f;

	public bool gaussianMipFilter;

	[Range(0.1f, 4f)]
	public float farOcclusionStrength = 1f;

	[Range(0.1f, 4f)]
	public float farthestOcclusionStrength = 1f;

	[Range(3f, 16f)]
	public int secondaryCones = 6;

	[Range(0.1f, 4f)]
	public float secondaryOcclusionStrength = 1f;
}
