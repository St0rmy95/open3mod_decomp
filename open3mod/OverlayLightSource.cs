using System;
using OpenTK;
using OpenTK.Graphics;
using OpenTK.Graphics.OpenGL;

namespace open3mod
{
	// Token: 0x0200003E RID: 62
	public static class OverlayLightSource
	{
		// Token: 0x0600023F RID: 575 RVA: 0x00013434 File Offset: 0x00011634
		public static void DrawLightSource(Vector3 dir)
		{
			GL.Disable(EnableCap.Lighting);
			GL.Disable(EnableCap.Texture2D);
			GL.Enable(EnableCap.ColorMaterial);
			GL.Disable(EnableCap.CullFace);
			GL.ColorMaterial(MaterialFace.FrontAndBack, ColorMaterialParameter.AmbientAndDiffuse);
			GL.Begin(BeginMode.Lines);
			GL.Color4(new Color4(1f, 0f, 0f, 1f));
			GL.Vertex3(dir * 2f);
			GL.Color4(new Color4(1f, 0f, 0f, 1f));
			GL.Vertex3(dir * 2.1f);
			GL.End();
		}
	}
}
