using System;
using System.Collections.Generic;
using Assimp;
using OpenTK;

namespace open3mod
{
	// Token: 0x0200004F RID: 79
	public abstract class SceneRendererShared
	{
		// Token: 0x060002D1 RID: 721 RVA: 0x00016CE0 File Offset: 0x00014EE0
		protected SceneRendererShared(Scene owner, Vector3 initposeMin, Vector3 initposeMax)
		{
			this.Owner = owner;
			this.InitposeMin = initposeMin;
			this.InitposeMax = initposeMax;
			this.Skinner = new CpuSkinningEvaluator(owner);
			this.IsAlphaMaterial = new bool[owner.Raw.MaterialCount];
			for (int i = 0; i < this.IsAlphaMaterial.Length; i++)
			{
				this.IsAlphaMaterial[i] = this.Owner.MaterialMapper.IsAlphaMaterial(owner.Raw.Materials[i]);
			}
		}

		// Token: 0x060002D2 RID: 722 RVA: 0x00016D68 File Offset: 0x00014F68
		protected void UploadTextures()
		{
			if (this.Owner.Raw.Materials == null)
			{
				return;
			}
			int num = 0;
			foreach (Material material in this.Owner.Raw.Materials)
			{
				if (this.Owner.MaterialMapper.UploadTextures(material))
				{
					this.IsAlphaMaterial[num] = this.Owner.MaterialMapper.IsAlphaMaterial(material);
				}
				num++;
			}
		}

		// Token: 0x060002D3 RID: 723 RVA: 0x00016E04 File Offset: 0x00015004
		protected bool DrawOpaqueMeshes(Node node, Dictionary<Node, List<Mesh>> visibleMeshesByNode, RenderFlags flags, bool animated)
		{
			bool result = false;
			if (visibleMeshesByNode == null)
			{
				using (List<int>.Enumerator enumerator = node.MeshIndices.GetEnumerator())
				{
					while (enumerator.MoveNext())
					{
						int num = enumerator.Current;
						Mesh mesh = this.Owner.Raw.Meshes[num];
						if (this.IsAlphaMaterial[mesh.MaterialIndex])
						{
							result = true;
						}
						else
						{
							bool flag = this.DrawMesh(node, animated, false, num, mesh, flags);
							if (flags.HasFlag(RenderFlags.ShowBoundingBoxes))
							{
								OverlayBoundingBox.DrawBoundingBox(node, num, mesh, flag ? this.Skinner : null);
							}
						}
					}
					return result;
				}
			}
			List<Mesh> list;
			if (visibleMeshesByNode.TryGetValue(node, out list))
			{
				using (List<int>.Enumerator enumerator2 = node.MeshIndices.GetEnumerator())
				{
					while (enumerator2.MoveNext())
					{
						int num2 = enumerator2.Current;
						Mesh mesh2 = this.Owner.Raw.Meshes[num2];
						if (this.IsAlphaMaterial[mesh2.MaterialIndex] || (list != null && !list.Contains(mesh2)))
						{
							result = true;
						}
						else
						{
							bool flag2 = this.DrawMesh(node, animated, false, num2, mesh2, flags);
							if (flags.HasFlag(RenderFlags.ShowBoundingBoxes))
							{
								OverlayBoundingBox.DrawBoundingBox(node, num2, mesh2, flag2 ? this.Skinner : null);
							}
						}
					}
					return result;
				}
			}
			result = true;
			return result;
		}

		// Token: 0x060002D4 RID: 724 RVA: 0x00016F88 File Offset: 0x00015188
		protected void DrawAlphaMeshes(Node node, Dictionary<Node, List<Mesh>> visibleNodes, RenderFlags flags, bool animated)
		{
			if (visibleNodes == null)
			{
				using (List<int>.Enumerator enumerator = node.MeshIndices.GetEnumerator())
				{
					while (enumerator.MoveNext())
					{
						int index = enumerator.Current;
						Mesh mesh = this.Owner.Raw.Meshes[index];
						if (this.IsAlphaMaterial[mesh.MaterialIndex])
						{
							this.DrawMesh(node, animated, false, index, mesh, flags);
						}
					}
					return;
				}
			}
			List<Mesh> list;
			if (visibleNodes.TryGetValue(node, out list))
			{
				if (list == null)
				{
					using (List<int>.Enumerator enumerator2 = node.MeshIndices.GetEnumerator())
					{
						while (enumerator2.MoveNext())
						{
							int index2 = enumerator2.Current;
							Mesh mesh2 = this.Owner.Raw.Meshes[index2];
							if (this.IsAlphaMaterial[mesh2.MaterialIndex])
							{
								this.DrawMesh(node, animated, false, index2, mesh2, flags);
							}
						}
						return;
					}
				}
				using (List<int>.Enumerator enumerator3 = node.MeshIndices.GetEnumerator())
				{
					while (enumerator3.MoveNext())
					{
						int index3 = enumerator3.Current;
						Mesh mesh3 = this.Owner.Raw.Meshes[index3];
						if (!list.Contains(mesh3))
						{
							this.DrawMesh(node, animated, true, index3, mesh3, flags);
						}
						else if (this.IsAlphaMaterial[mesh3.MaterialIndex])
						{
							this.DrawMesh(node, animated, false, index3, mesh3, flags);
						}
					}
					return;
				}
			}
			foreach (int index4 in node.MeshIndices)
			{
				Mesh mesh4 = this.Owner.Raw.Meshes[index4];
				this.DrawMesh(node, animated, true, index4, mesh4, flags);
			}
		}

		// Token: 0x060002D5 RID: 725
		protected abstract bool DrawMesh(Node node, bool animated, bool showGhost, int index, Mesh mesh, RenderFlags flags);

		// Token: 0x04000233 RID: 563
		protected readonly Scene Owner;

		// Token: 0x04000234 RID: 564
		protected readonly Vector3 InitposeMin;

		// Token: 0x04000235 RID: 565
		protected readonly Vector3 InitposeMax;

		// Token: 0x04000236 RID: 566
		protected readonly CpuSkinningEvaluator Skinner;

		// Token: 0x04000237 RID: 567
		protected readonly bool[] IsAlphaMaterial;
	}
}
