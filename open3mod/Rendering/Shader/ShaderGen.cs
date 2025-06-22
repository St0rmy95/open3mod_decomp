using System;

namespace open3mod
{
	// Token: 0x02000054 RID: 84
	public class ShaderGen
	{
		// Token: 0x060002FF RID: 767 RVA: 0x0001A934 File Offset: 0x00018B34
		public Shader Generate(ShaderGen.GenFlags flags)
		{
			string text = "";
			if (flags.HasFlag(ShaderGen.GenFlags.ColorMap))
			{
				text += "#define HAS_COLOR_MAP\n";
			}
			if (flags.HasFlag(ShaderGen.GenFlags.VertexColor))
			{
				text += "#define HAS_VERTEX_COLOR\n";
			}
			if (flags.HasFlag(ShaderGen.GenFlags.PhongSpecularShading))
			{
				text += "#define HAS_PHONG_SPECULAR_SHADING\n";
			}
			if (flags.HasFlag(ShaderGen.GenFlags.Skinning))
			{
				text += "#define HAS_SKINNING\n";
			}
			if (flags.HasFlag(ShaderGen.GenFlags.Lighting))
			{
				text += "#define HAS_LIGHTING\n";
			}
			return new Shader("open3mod.Shader.UberVertexShader.glsl", "open3mod.Shader.UberFragmentShader.glsl", text);
		}

		// Token: 0x02000055 RID: 85
		[Flags]
		public enum GenFlags
		{
			// Token: 0x04000276 RID: 630
			ColorMap = 1,
			// Token: 0x04000277 RID: 631
			VertexColor = 2,
			// Token: 0x04000278 RID: 632
			PhongSpecularShading = 4,
			// Token: 0x04000279 RID: 633
			Skinning = 8,
			// Token: 0x0400027A RID: 634
			Lighting = 16
		}
	}
}
