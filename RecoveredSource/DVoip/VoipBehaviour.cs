using UnityEngine;

namespace DVoip;

internal class VoipBehaviour : MonoBehaviour
{
	private int bps;

	private float bpst;

	public int chunkCount { get; protected set; }

	public int bytes { get; protected set; }

	public float bytesPerSecond { get; protected set; }

	protected void UpdateStats()
	{
		bpst += smethod_0();
		if (bpst > 0.2f)
		{
			bpst %= 0.2f;
			bytesPerSecond = Mathf.Lerp(bytesPerSecond, bytes - bps, 0.5f);
			bps = bytes;
		}
	}

	internal static float smethod_0()
	{
		return Time.unscaledDeltaTime;
	}
}
