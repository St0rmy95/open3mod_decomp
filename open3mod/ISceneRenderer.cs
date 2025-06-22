using System;
using System.Collections.Generic;
using Assimp;

namespace open3mod
{
	// Token: 0x02000027 RID: 39
	public interface ISceneRenderer : IDisposable
	{
		// Token: 0x06000163 RID: 355
		void Update(double delta);

		// Token: 0x06000164 RID: 356
		void Render(ICameraController cam, Dictionary<Node, List<Mesh>> visibleMeshesByNode, bool visibleSetChanged, bool texturesChanged, RenderFlags flags, Renderer renderer);
	}
}
