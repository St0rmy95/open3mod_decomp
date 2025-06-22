using System;
using Assimp;

namespace open3mod
{
	// Token: 0x02000016 RID: 22
	public sealed class MaterialMapperModernGl : MaterialMapper
	{
		// Token: 0x060000D3 RID: 211 RVA: 0x00005FF2 File Offset: 0x000041F2
		internal MaterialMapperModernGl(Scene scene) : base(scene)
		{
		}

		// Token: 0x060000D4 RID: 212 RVA: 0x00005FFB File Offset: 0x000041FB
		public override void Dispose()
		{
			GC.SuppressFinalize(this);
		}

		// Token: 0x060000D5 RID: 213 RVA: 0x00006003 File Offset: 0x00004203
		public override void ApplyMaterial(Mesh mesh, Material mat, bool textured, bool shaded)
		{
		}

		// Token: 0x060000D6 RID: 214 RVA: 0x00006005 File Offset: 0x00004205
		public override void ApplyGhostMaterial(Mesh mesh, Material material, bool shaded)
		{
		}
	}
}
