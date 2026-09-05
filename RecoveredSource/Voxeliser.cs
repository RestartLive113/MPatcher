using System;
using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using System.Runtime.CompilerServices;
using UnityEngine;

public class Voxeliser
{
	[CompilerGenerated]
	private sealed class qsz9KOVhmnHmcM0GhbshsNlKza4_0024wWxnLZqO8jsyoBaT : IEnumerator<object>, IDisposable, IEnumerator
	{
		private int SjlBM8inVA_YE4YVlr_0024gluY;

		private object yT7HpVIzmqW54W307WgJtr4;

		public Transform rlWBFtnihrsMM80pDK50nLg;

		public Voxeliser SKCFxHGAEbVQbKCDB_0024Jj8p4;

		public Action action_0;

		object IEnumerator<object>.Current
		{
			[DebuggerHidden]
			get
			{
				return yT7HpVIzmqW54W307WgJtr4;
			}
		}

		object IEnumerator.Current
		{
			[DebuggerHidden]
			get
			{
				return yT7HpVIzmqW54W307WgJtr4;
			}
		}

		[DebuggerHidden]
		public qsz9KOVhmnHmcM0GhbshsNlKza4_0024wWxnLZqO8jsyoBaT(int int_0)
		{
			SjlBM8inVA_YE4YVlr_0024gluY = int_0;
		}

		[DebuggerHidden]
		void IDisposable.Dispose()
		{
			SjlBM8inVA_YE4YVlr_0024gluY = -2;
		}

		private bool MoveNext()
		{
			int num = SjlBM8inVA_YE4YVlr_0024gluY;
			Voxeliser voxeliser = SKCFxHGAEbVQbKCDB_0024Jj8p4;
			switch (num)
			{
			case 0:
			{
				SjlBM8inVA_YE4YVlr_0024gluY = -1;
				MeshFilter[] componentsInChildren = rlWBFtnihrsMM80pDK50nLg.GetComponentsInChildren<MeshFilter>();
				List<TriAABBOverlap.BoundHierarchy> list = new List<TriAABBOverlap.BoundHierarchy>();
				MeshFilter[] array = componentsInChildren;
				foreach (MeshFilter meshFilter in array)
				{
					Mesh mesh_ = smethod_0(meshFilter);
					Vector3[] array2 = smethod_1(mesh_);
					int[] array3 = smethod_2(mesh_);
					List<TriAABBOverlap.BoundHierarchy> list2 = new List<TriAABBOverlap.BoundHierarchy>();
					for (int j = 0; j < array3.Length; j += 3)
					{
						Vector3 vector3_ = array2[array3[j]];
						Vector3 vector3_2 = array2[array3[j + 1]];
						Vector3 vector3_3 = array2[array3[j + 2]];
						vector3_ = smethod_4(smethod_3((Component)meshFilter), vector3_);
						vector3_2 = smethod_4(smethod_3((Component)meshFilter), vector3_2);
						vector3_3 = smethod_4(smethod_3((Component)meshFilter), vector3_3);
						Vector3 lhs = vector3_2 - vector3_3;
						Vector3 rhs = vector3_3 - vector3_;
						Vector3 normalized = Vector3.Cross(lhs, rhs).normalized;
						Bounds bound = new Bounds(vector3_, Vector3.zero);
						bound.Encapsulate(vector3_2);
						bound.Encapsulate(vector3_3);
						TriAABBOverlap.Triangle triList = new TriAABBOverlap.Triangle
						{
							vertA = vector3_,
							vertB = vector3_2,
							vertC = vector3_3,
							normal = normalized,
							bound = bound
						};
						list2.Add(new TriAABBOverlap.BoundHierarchy
						{
							bound = bound,
							subBounds = null,
							triList = triList
						});
					}
					list.Add(new TriAABBOverlap.BoundHierarchy
					{
						bound = meshFilter.GetComponent<Renderer>().bounds,
						subBounds = list2.ToArray()
					});
				}
				TriAABBOverlap.BoundHierarchy boundHierarchy_ = new TriAABBOverlap.BoundHierarchy
				{
					bound = voxeliser.gKzt8Vjk0YQR7LXcfZMOun0,
					subBounds = list.ToArray()
				};
				voxeliser.iZ_q6FP8m_jn1pG6g5eUmElrS2ibWUag7trKCyaSzQ7h();
				yT7HpVIzmqW54W307WgJtr4 = voxeliser.e6V2u2bfqePHfXQ17qXhvZBV_WYIcqOEZa_0024iyFKS_Y1v(boundHierarchy_, action_0);
				SjlBM8inVA_YE4YVlr_0024gluY = 1;
				return true;
			}
			case 1:
				SjlBM8inVA_YE4YVlr_0024gluY = -1;
				return false;
			default:
				return false;
			}
		}

		bool IEnumerator.MoveNext()
		{
			//ILSpy generated this explicit interface implementation from .override directive in MoveNext
			return this.MoveNext();
		}

		[DebuggerHidden]
		void IEnumerator.Reset()
		{
			throw smethod_5();
		}

		internal static Mesh smethod_0(MeshFilter meshFilter_0)
		{
			return meshFilter_0.sharedMesh;
		}

		internal static Vector3[] smethod_1(Mesh mesh_0)
		{
			return mesh_0.vertices;
		}

		internal static int[] smethod_2(Mesh mesh_0)
		{
			return mesh_0.triangles;
		}

		internal static Transform smethod_3(Component component_0)
		{
			return component_0.transform;
		}

		internal static Vector3 smethod_4(Transform transform_0, Vector3 vector3_0)
		{
			return transform_0.TransformPoint(vector3_0);
		}

		internal static NotSupportedException smethod_5()
		{
			return new NotSupportedException();
		}
	}

	private bool[][][] cR262sfQ0CPkDN3ZF9M1d1I;

	private int int_0 = 8;

	private int aiEozkPdOczihXXpba4pNrM = 8;

	private int int_1 = 8;

	private Bounds gKzt8Vjk0YQR7LXcfZMOun0;

	public bool[][][] VoxelMap => cR262sfQ0CPkDN3ZF9M1d1I;

	public Voxeliser(Bounds bounds_0, int int_2, int int_3, int int_4)
	{
		gKzt8Vjk0YQR7LXcfZMOun0 = bounds_0;
		int_0 = int_2;
		aiEozkPdOczihXXpba4pNrM = int_3;
		int_1 = int_4;
	}

	public IEnumerator Voxelize(Transform root, Action onDone)
	{
		MeshFilter[] componentsInChildren = root.GetComponentsInChildren<MeshFilter>();
		List<TriAABBOverlap.BoundHierarchy> list = new List<TriAABBOverlap.BoundHierarchy>();
		MeshFilter[] array = componentsInChildren;
		foreach (MeshFilter meshFilter in array)
		{
			Mesh mesh_ = qsz9KOVhmnHmcM0GhbshsNlKza4_0024wWxnLZqO8jsyoBaT.smethod_0(meshFilter);
			Vector3[] array2 = qsz9KOVhmnHmcM0GhbshsNlKza4_0024wWxnLZqO8jsyoBaT.smethod_1(mesh_);
			int[] array3 = qsz9KOVhmnHmcM0GhbshsNlKza4_0024wWxnLZqO8jsyoBaT.smethod_2(mesh_);
			List<TriAABBOverlap.BoundHierarchy> list2 = new List<TriAABBOverlap.BoundHierarchy>();
			for (int j = 0; j < array3.Length; j += 3)
			{
				Vector3 vector3_ = array2[array3[j]];
				Vector3 vector3_2 = array2[array3[j + 1]];
				Vector3 vector3_3 = array2[array3[j + 2]];
				vector3_ = qsz9KOVhmnHmcM0GhbshsNlKza4_0024wWxnLZqO8jsyoBaT.smethod_4(qsz9KOVhmnHmcM0GhbshsNlKza4_0024wWxnLZqO8jsyoBaT.smethod_3((Component)meshFilter), vector3_);
				vector3_2 = qsz9KOVhmnHmcM0GhbshsNlKza4_0024wWxnLZqO8jsyoBaT.smethod_4(qsz9KOVhmnHmcM0GhbshsNlKza4_0024wWxnLZqO8jsyoBaT.smethod_3((Component)meshFilter), vector3_2);
				vector3_3 = qsz9KOVhmnHmcM0GhbshsNlKza4_0024wWxnLZqO8jsyoBaT.smethod_4(qsz9KOVhmnHmcM0GhbshsNlKza4_0024wWxnLZqO8jsyoBaT.smethod_3((Component)meshFilter), vector3_3);
				Vector3 lhs = vector3_2 - vector3_3;
				Vector3 rhs = vector3_3 - vector3_;
				Vector3 normalized = Vector3.Cross(lhs, rhs).normalized;
				Bounds bound = new Bounds(vector3_, Vector3.zero);
				bound.Encapsulate(vector3_2);
				bound.Encapsulate(vector3_3);
				TriAABBOverlap.Triangle triList = new TriAABBOverlap.Triangle
				{
					vertA = vector3_,
					vertB = vector3_2,
					vertC = vector3_3,
					normal = normalized,
					bound = bound
				};
				list2.Add(new TriAABBOverlap.BoundHierarchy
				{
					bound = bound,
					subBounds = null,
					triList = triList
				});
			}
			list.Add(new TriAABBOverlap.BoundHierarchy
			{
				bound = meshFilter.GetComponent<Renderer>().bounds,
				subBounds = list2.ToArray()
			});
		}
		TriAABBOverlap.BoundHierarchy boundHierarchy_ = new TriAABBOverlap.BoundHierarchy
		{
			bound = gKzt8Vjk0YQR7LXcfZMOun0,
			subBounds = list.ToArray()
		};
		iZ_q6FP8m_jn1pG6g5eUmElrS2ibWUag7trKCyaSzQ7h();
		yield return e6V2u2bfqePHfXQ17qXhvZBV_WYIcqOEZa_0024iyFKS_Y1v(boundHierarchy_, onDone);
	}

	private IEnumerator e6V2u2bfqePHfXQ17qXhvZBV_WYIcqOEZa_0024iyFKS_Y1v(TriAABBOverlap.BoundHierarchy boundHierarchy_0, Action action_0)
	{
		Vector3 vector = new Vector3(gKzt8Vjk0YQR7LXcfZMOun0.size.x / (float)int_0, gKzt8Vjk0YQR7LXcfZMOun0.size.y / (float)aiEozkPdOczihXXpba4pNrM, gKzt8Vjk0YQR7LXcfZMOun0.size.z / (float)int_1);
		Vector3 vector2 = gKzt8Vjk0YQR7LXcfZMOun0.min + vector / 2f;
		TriAABBOverlap.BoundHierarchy[] subBounds = boundHierarchy_0.subBounds;
		Bounds bounds = new Bounds(Vector3.zero, vector);
		Vector3 zero = Vector3.zero;
		int num = 50;
		int num2 = 0;
		_ = (float)(int_0 * aiEozkPdOczihXXpba4pNrM * int_1);
		for (int i = 0; i < int_0; i++)
		{
			for (int j = 0; j < aiEozkPdOczihXXpba4pNrM; j++)
			{
				for (int k = 0; k < int_1; k++)
				{
					num2++;
					JQpZ6lP9aBgIeq1zc9f_eT6euqKTWcd_R1Xq7K0V0oLZ.d5XvqNLNuVqmANrqHnMnO9c(new Vector3(i, j, k) + vector2, Quaternion.identity, Vector3.one, Color.red);
					JQpZ6lP9aBgIeq1zc9f_eT6euqKTWcd_R1Xq7K0V0oLZ.d5XvqNLNuVqmANrqHnMnO9c(gKzt8Vjk0YQR7LXcfZMOun0.center, Quaternion.identity, gKzt8Vjk0YQR7LXcfZMOun0.size, Color.cyan);
					if (num == 1 || num2 % num == 0)
					{
						if (num > 1 && 1f / Time.deltaTime < 30f)
						{
							num--;
						}
						else if (1f / Time.deltaTime > 40f && num < 100)
						{
							num++;
						}
						yield return null;
					}
					bool flag = false;
					for (int l = 0; l < subBounds.Length; l++)
					{
						zero.x = (float)i * vector.x + vector2.x;
						zero.y = (float)j * vector.y + vector2.y;
						zero.z = (float)k * vector.z + vector2.z;
						bounds.center = zero;
						if (bounds.Intersects(subBounds[l].bound))
						{
							TriAABBOverlap.BoundHierarchy[] subBounds2 = subBounds[l].subBounds;
							for (int m = 0; m < subBounds2.Length; m++)
							{
								TriAABBOverlap.Triangle triList = subBounds2[m].triList;
								if (TriAABBOverlap.Check(bounds, triList))
								{
									cR262sfQ0CPkDN3ZF9M1d1I[i][j][k] = true;
									flag = true;
									break;
								}
							}
							if (flag)
							{
								break;
							}
						}
						if (flag)
						{
							break;
						}
					}
				}
			}
		}
		action_0();
	}

	private void iZ_q6FP8m_jn1pG6g5eUmElrS2ibWUag7trKCyaSzQ7h()
	{
		cR262sfQ0CPkDN3ZF9M1d1I = new bool[int_0][][];
		for (int i = 0; i < int_0; i++)
		{
			cR262sfQ0CPkDN3ZF9M1d1I[i] = new bool[aiEozkPdOczihXXpba4pNrM][];
			for (int j = 0; j < aiEozkPdOczihXXpba4pNrM; j++)
			{
				cR262sfQ0CPkDN3ZF9M1d1I[i][j] = new bool[int_1];
			}
		}
	}
}
