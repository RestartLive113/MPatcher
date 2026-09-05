using System.Runtime.CompilerServices;
using UnityEngine;

namespace RuntimeInspectorNamespace;

[RequireComponent(typeof(RectTransform))]
public class RecycledListItem : MonoBehaviour
{
	[CompilerGenerated]
	private object object_0;

	[CompilerGenerated]
	private int DRUbUSAs0SFdKzjeygjMf6WHORGGiTAYX05cPjbla2vX;

	private IListViewAdapter WyZGbTAsrnDliiXt0TZpvkQ;

	public object Tag
	{
		[CompilerGenerated]
		get
		{
			return object_0;
		}
		[CompilerGenerated]
		set
		{
			object_0 = value;
		}
	}

	public int Position
	{
		[CompilerGenerated]
		get
		{
			return DRUbUSAs0SFdKzjeygjMf6WHORGGiTAYX05cPjbla2vX;
		}
		[CompilerGenerated]
		set
		{
			DRUbUSAs0SFdKzjeygjMf6WHORGGiTAYX05cPjbla2vX = value;
		}
	}

	internal void Vfgh0wMhcRKegI8WSbN1g_w(IListViewAdapter ilistViewAdapter_0)
	{
		WyZGbTAsrnDliiXt0TZpvkQ = ilistViewAdapter_0;
	}

	public void OnClick()
	{
		WyZGbTAsrnDliiXt0TZpvkQ.OnItemClicked(this);
	}
}
