namespace DVoip;

internal class VoipBuffer
{
	private class Fragment
	{
		public float[] data;

		public int index = -1;
	}

	public readonly int fragmentSize;

	public readonly int fragmentCount;

	private readonly Fragment[] data;

	private readonly float[] empty;

	public VoipBuffer(int fragmentSize, int fragmentCount)
	{
		this.fragmentSize = fragmentSize;
		this.fragmentCount = fragmentCount;
		data = new Fragment[fragmentCount];
		empty = new float[fragmentSize];
		for (int i = 0; i < fragmentCount; i++)
		{
			data[i] = new Fragment();
		}
	}

	public void Write(int position, float[] fragment)
	{
		int num = position % fragmentCount;
		data[num].index = position;
		data[num].data = fragment;
	}

	public float[] Read(int position)
	{
		int num = position % fragmentCount;
		if (data[num].index == position)
		{
			return data[num].data;
		}
		return empty;
	}
}
