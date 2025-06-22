using System;
using Assimp;

namespace open3mod
{
	// Token: 0x02000007 RID: 7
	public class BoneByVertexMap
	{
		// Token: 0x1700000C RID: 12
		// (get) Token: 0x0600002E RID: 46 RVA: 0x000040AE File Offset: 0x000022AE
		public BoneByVertexMap.IndexWeightTuple[] BonesByVertex
		{
			get
			{
				return this._bonesByVertex;
			}
		}

		// Token: 0x0600002F RID: 47 RVA: 0x000040B6 File Offset: 0x000022B6
		public void GetOffsetAndCountForVertex(uint vertex, out uint offset, out uint count)
		{
			offset = this._offsets[(int)((UIntPtr)vertex)];
			count = this._countBones[(int)((UIntPtr)vertex)];
		}

		// Token: 0x06000030 RID: 48 RVA: 0x000040D0 File Offset: 0x000022D0
		internal BoneByVertexMap(Mesh mesh)
		{
			this._mesh = mesh;
			this._offsets = new uint[mesh.VertexCount];
			this._countBones = new uint[mesh.VertexCount];
			if (this._mesh.BoneCount == 0)
			{
				this._bonesByVertex = new BoneByVertexMap.IndexWeightTuple[0];
				return;
			}
			int num = 0;
			for (int i = 0; i < mesh.BoneCount; i++)
			{
				Bone bone = mesh.Bones[i];
				num += bone.VertexWeightCount;
				for (int j = 0; j < bone.VertexWeightCount; j++)
				{
					VertexWeight vertexWeight = bone.VertexWeights[j];
					this._countBones[vertexWeight.VertexID] += 1U;
				}
			}
			this._bonesByVertex = new BoneByVertexMap.IndexWeightTuple[num];
			uint num2 = 0U;
			for (int k = 0; k < this._mesh.VertexCount; k++)
			{
				this._offsets[k] = num2;
				num2 += this._countBones[k];
			}
			for (int l = 0; l < mesh.BoneCount; l++)
			{
				Bone bone2 = mesh.Bones[l];
				num += bone2.VertexWeightCount;
				for (int m = 0; m < bone2.VertexWeightCount; m++)
				{
					VertexWeight vertexWeight2 = bone2.VertexWeights[m];
					this.BonesByVertex[(int)((UIntPtr)(this._offsets[vertexWeight2.VertexID]++))] = new BoneByVertexMap.IndexWeightTuple(l, vertexWeight2.Weight);
				}
			}
			for (int n = 0; n < this._mesh.VertexCount; n++)
			{
				this._offsets[n] -= this._countBones[n];
			}
		}

		// Token: 0x04000031 RID: 49
		private readonly Mesh _mesh;

		// Token: 0x04000032 RID: 50
		private readonly uint[] _countBones;

		// Token: 0x04000033 RID: 51
		private readonly BoneByVertexMap.IndexWeightTuple[] _bonesByVertex;

		// Token: 0x04000034 RID: 52
		private readonly uint[] _offsets;

		// Token: 0x02000008 RID: 8
		public struct IndexWeightTuple
		{
			// Token: 0x06000031 RID: 49 RVA: 0x000042A8 File Offset: 0x000024A8
			public IndexWeightTuple(int index, float weight)
			{
				this.Item1 = index;
				this.Item2 = weight;
			}

			// Token: 0x04000035 RID: 53
			public int Item1;

			// Token: 0x04000036 RID: 54
			public float Item2;
		}
	}
}
