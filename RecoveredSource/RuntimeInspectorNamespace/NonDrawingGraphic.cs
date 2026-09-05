using UnityEngine.UI;

namespace RuntimeInspectorNamespace;

public class NonDrawingGraphic : Graphic
{
	public override void SetMaterialDirty()
	{
	}

	public override void SetVerticesDirty()
	{
	}

	protected override void OnPopulateMesh(VertexHelper vh)
	{
		smethod_0(vh);
	}

	internal static void smethod_0(VertexHelper vertexHelper_0)
	{
		vertexHelper_0.Clear();
	}
}
