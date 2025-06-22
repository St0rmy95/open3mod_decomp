using System;

namespace open3mod
{
	// Token: 0x02000026 RID: 38
	[Flags]
	public enum RenderFlags
	{
		// Token: 0x040000FB RID: 251
		Wireframe = 1,
		// Token: 0x040000FC RID: 252
		Shaded = 2,
		// Token: 0x040000FD RID: 253
		ShowBoundingBoxes = 4,
		// Token: 0x040000FE RID: 254
		ShowNormals = 8,
		// Token: 0x040000FF RID: 255
		ShowSkeleton = 16,
		// Token: 0x04000100 RID: 256
		Textured = 32,
		// Token: 0x04000101 RID: 257
		ShowGhosts = 64
	}
}
