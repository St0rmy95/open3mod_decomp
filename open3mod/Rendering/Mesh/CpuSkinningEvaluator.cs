using System;
using Assimp;
using OpenTK;

namespace open3mod
{
	// Token: 0x0200000A RID: 10
	public class CpuSkinningEvaluator
	{
		// Token: 0x06000079 RID: 121 RVA: 0x000047C0 File Offset: 0x000029C0
		public CpuSkinningEvaluator(Scene owner)
		{
			this._owner = owner;
			this._cache = new CpuSkinningEvaluator.CachedMeshData[owner.Raw.MeshCount];
			for (int i = 0; i < this._cache.Length; i++)
			{
				if (owner.Raw.Meshes[i].HasBones)
				{
					this._cache[i] = new CpuSkinningEvaluator.CachedMeshData(owner, owner.Raw.Meshes[i]);
				}
			}
		}

		// Token: 0x0600007A RID: 122 RVA: 0x0000483C File Offset: 0x00002A3C
		public void Update()
		{
			foreach (CpuSkinningEvaluator.CachedMeshData cachedMeshData in this._cache)
			{
				if (cachedMeshData != null)
				{
					cachedMeshData.Update();
				}
			}
		}

		// Token: 0x0600007B RID: 123 RVA: 0x0000486B File Offset: 0x00002A6B
		public void GetTransformedVertexPosition(Node node, int meshIndex, uint vertexIndex, out Vector3 pos)
		{
			this._cache[meshIndex].GetTransformedVertexPosition(node, vertexIndex, out pos);
		}

		// Token: 0x0600007C RID: 124 RVA: 0x0000487E File Offset: 0x00002A7E
		public void GetTransformedVertexNormal(Node node, int meshIndex, uint vertexIndex, out Vector3 nor)
		{
			this._cache[meshIndex].GetTransformedVertexNormal(node, vertexIndex, out nor);
		}

		// Token: 0x04000038 RID: 56
		private readonly Scene _owner;

		// Token: 0x04000039 RID: 57
		private readonly CpuSkinningEvaluator.CachedMeshData[] _cache;

		// Token: 0x0200000B RID: 11
		private sealed class CachedMeshData
		{
			// Token: 0x0600007D RID: 125 RVA: 0x00004894 File Offset: 0x00002A94
			public CachedMeshData(Scene scene, Mesh source)
			{
				this._scene = scene;
				this._source = source;
				this._cachedPositions = new Vector3[source.VertexCount];
				this._cachedNormals = new Vector3[source.VertexCount];
				this._boneMap = new BoneByVertexMap(source);
			}

			// Token: 0x0600007E RID: 126 RVA: 0x000048EA File Offset: 0x00002AEA
			public void Update()
			{
				this._dirty = true;
			}

			// Token: 0x0600007F RID: 127 RVA: 0x000048F3 File Offset: 0x00002AF3
			public void GetTransformedVertexPosition(Node node, uint vertexIndex, out Vector3 pos)
			{
				if (node != this._lastNode)
				{
					this._lastNode = node;
					this._dirty = true;
				}
				if (this._dirty)
				{
					this.Cache();
				}
				pos = this._cachedPositions[(int)((UIntPtr)vertexIndex)];
			}

			// Token: 0x06000080 RID: 128 RVA: 0x00004932 File Offset: 0x00002B32
			public void GetTransformedVertexNormal(Node node, uint vertexIndex, out Vector3 nor)
			{
				if (node != this._lastNode)
				{
					this._lastNode = node;
					this._dirty = true;
				}
				if (this._dirty)
				{
					this.Cache();
				}
				nor = this._cachedNormals[(int)((UIntPtr)vertexIndex)];
			}

			// Token: 0x06000081 RID: 129 RVA: 0x00004974 File Offset: 0x00002B74
			private void Cache()
			{
				Matrix4[] boneMatricesForMesh = this._scene.SceneAnimator.GetBoneMatricesForMesh(this._lastNode, this._source);
				for (int i = 0; i < this._cachedPositions.Length; i++)
				{
					Vector3 vector = AssimpToOpenTk.FromVector(this._source.Vertices[i]);
					this.EvaluateBoneInfluences(ref vector, (uint)i, boneMatricesForMesh, out this._cachedPositions[i], false);
				}
				for (int j = 0; j < this._cachedNormals.Length; j++)
				{
					Vector3 vector2 = AssimpToOpenTk.FromVector(this._source.Normals[j]);
					this.EvaluateBoneInfluences(ref vector2, (uint)j, boneMatricesForMesh, out this._cachedNormals[j], true);
				}
				this._dirty = false;
			}

			// Token: 0x06000082 RID: 130 RVA: 0x00004A28 File Offset: 0x00002C28
			private void EvaluateBoneInfluences(ref Vector3 pos, uint vertexIndex, Matrix4[] boneMatrices, out Vector3 transformedPosOut, bool isDirectionVector = false)
			{
				uint num;
				uint num2;
				this._boneMap.GetOffsetAndCountForVertex(vertexIndex, out num, out num2);
				Vector3 vector = Vector3.Zero;
				BoneByVertexMap.IndexWeightTuple[] bonesByVertex = this._boneMap.BonesByVertex;
				int num3 = 0;
				while ((long)num3 < (long)((ulong)num2))
				{
					BoneByVertexMap.IndexWeightTuple indexWeightTuple = bonesByVertex[(int)((UIntPtr)num)];
					Vector3 vec;
					if (isDirectionVector)
					{
						Vector3.TransformNormal(ref pos, ref boneMatrices[indexWeightTuple.Item1], out vec);
					}
					else
					{
						Vector3.Transform(ref pos, ref boneMatrices[indexWeightTuple.Item1], out vec);
					}
					vector += vec * indexWeightTuple.Item2;
					num3++;
					num += 1U;
				}
				transformedPosOut = vector;
			}

			// Token: 0x0400003A RID: 58
			private readonly Scene _scene;

			// Token: 0x0400003B RID: 59
			private readonly Mesh _source;

			// Token: 0x0400003C RID: 60
			private readonly Vector3[] _cachedPositions;

			// Token: 0x0400003D RID: 61
			private readonly Vector3[] _cachedNormals;

			// Token: 0x0400003E RID: 62
			private bool _dirty = true;

			// Token: 0x0400003F RID: 63
			private readonly BoneByVertexMap _boneMap;

			// Token: 0x04000040 RID: 64
			private Node _lastNode;
		}
	}
}
