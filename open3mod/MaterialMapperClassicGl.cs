using System;
using Assimp;
using OpenTK.Graphics;
using OpenTK.Graphics.OpenGL;

namespace open3mod
{
	// Token: 0x02000015 RID: 21
	public sealed class MaterialMapperClassicGl : MaterialMapper
	{
		// Token: 0x060000CD RID: 205 RVA: 0x00005B22 File Offset: 0x00003D22
		internal MaterialMapperClassicGl(Scene scene) : base(scene)
		{
		}

		// Token: 0x060000CE RID: 206 RVA: 0x00005B2B File Offset: 0x00003D2B
		public override void Dispose()
		{
		}

		// Token: 0x060000CF RID: 207 RVA: 0x00005B2D File Offset: 0x00003D2D
		public override void ApplyMaterial(Mesh mesh, Material mat, bool textured, bool shaded)
		{
			this.ApplyFixedFunctionMaterial(mesh, mat, textured, shaded);
		}

		// Token: 0x060000D0 RID: 208 RVA: 0x00005B3A File Offset: 0x00003D3A
		public override void ApplyGhostMaterial(Mesh mesh, Material material, bool shaded)
		{
			this.ApplyFixedFunctionGhostMaterial(mesh, material, shaded);
		}

		// Token: 0x060000D1 RID: 209 RVA: 0x00005B48 File Offset: 0x00003D48
		private void ApplyFixedFunctionMaterial(Mesh mesh, Material mat, bool textured, bool shaded)
		{
			shaded = (shaded && (mesh == null || mesh.HasNormals));
			if (shaded)
			{
				GL.Enable(EnableCap.Lighting);
			}
			else
			{
				GL.Disable(EnableCap.Lighting);
			}
			bool flag = mesh != null && mesh.HasVertexColors(0);
			if (flag)
			{
				GL.Enable(EnableCap.ColorMaterial);
				GL.ColorMaterial(MaterialFace.FrontAndBack, ColorMaterialParameter.AmbientAndDiffuse);
			}
			else
			{
				GL.Disable(EnableCap.ColorMaterial);
			}
			bool flag2 = false;
			bool flag3 = false;
			if (textured && mat.GetMaterialTextureCount(TextureType.Diffuse) > 0)
			{
				flag3 = true;
				TextureSlot textureSlot;
				mat.GetMaterialTexture(TextureType.Diffuse, 0, out textureSlot);
				Texture originalOrReplacement = this._scene.TextureSet.GetOriginalOrReplacement(textureSlot.FilePath);
				flag2 = (flag2 || originalOrReplacement.HasAlpha == Texture.AlphaState.HasAlpha);
				if (originalOrReplacement.State == Texture.TextureState.GlTextureCreated)
				{
					GL.ActiveTexture(TextureUnit.Texture0);
					originalOrReplacement.BindGlTexture();
					GL.Enable(EnableCap.Texture2D);
				}
				else
				{
					GL.Disable(EnableCap.Texture2D);
				}
			}
			else
			{
				GL.Disable(EnableCap.Texture2D);
			}
			GL.Enable(EnableCap.Normalize);
			float num = 1f;
			if (mat.HasOpacity)
			{
				num = mat.Opacity;
				if (num < 1E-05f)
				{
					num = 1f;
				}
			}
			Color4 @params = new Color4(0.8f, 0.8f, 0.8f, 1f);
			if (mat.HasColorDiffuse)
			{
				@params = AssimpToOpenTk.FromColor(mat.ColorDiffuse);
				if (@params.A < 1E-05f)
				{
					@params.A = 1f;
				}
			}
			@params.A *= num;
			flag2 = (flag2 || @params.A < 1f);
			if (shaded)
			{
				if (flag3 && @params.R < 0.001f && @params.G < 0.001f && @params.B < 0.001f)
				{
					GL.Material(MaterialFace.FrontAndBack, MaterialParameter.Diffuse, Color4.White);
				}
				else
				{
					GL.Material(MaterialFace.FrontAndBack, MaterialParameter.Diffuse, @params);
				}
				@params = new Color4(0f, 0f, 0f, 1f);
				if (mat.HasColorSpecular)
				{
					@params = AssimpToOpenTk.FromColor(mat.ColorSpecular);
				}
				GL.Material(MaterialFace.FrontAndBack, MaterialParameter.Specular, @params);
				@params = new Color4(0.2f, 0.2f, 0.2f, 1f);
				if (mat.HasColorAmbient)
				{
					@params = AssimpToOpenTk.FromColor(mat.ColorAmbient);
				}
				GL.Material(MaterialFace.FrontAndBack, MaterialParameter.Ambient, @params);
				@params = new Color4(0f, 0f, 0f, 1f);
				if (mat.HasColorEmissive)
				{
					@params = AssimpToOpenTk.FromColor(mat.ColorEmissive);
				}
				GL.Material(MaterialFace.FrontAndBack, MaterialParameter.Emission, @params);
				float num2 = 1f;
				float num3 = 1f;
				if (mat.HasShininess)
				{
					num2 = mat.Shininess;
				}
				if (mat.HasShininessStrength)
				{
					num3 = mat.ShininessStrength;
				}
				float num4 = num2 * num3;
				if (num4 >= 128f)
				{
					num4 = 128f;
				}
				GL.Material(MaterialFace.FrontAndBack, MaterialParameter.Shininess, num4);
			}
			else if (!flag)
			{
				GL.Color3(@params.R, @params.G, @params.B);
			}
			if (flag2)
			{
				GL.Enable(EnableCap.Blend);
				GL.BlendFunc(BlendingFactorSrc.SrcAlpha, BlendingFactorDest.OneMinusSrcAlpha);
				GL.DepthMask(false);
				return;
			}
			GL.Disable(EnableCap.Blend);
			GL.DepthMask(true);
		}

		// Token: 0x060000D2 RID: 210 RVA: 0x00005EA8 File Offset: 0x000040A8
		private void ApplyFixedFunctionGhostMaterial(Mesh mesh, Material mat, bool shaded)
		{
			GL.Enable(EnableCap.Blend);
			GL.BlendFunc(BlendingFactorSrc.SrcAlpha, BlendingFactorDest.OneMinusSrcAlpha);
			GL.DepthMask(false);
			Color4 @params = new Color4(0.6f, 0.6f, 0.9f, 0.15f);
			shaded = (shaded && (mesh == null || mesh.HasNormals));
			if (shaded)
			{
				GL.Enable(EnableCap.Lighting);
				GL.Material(MaterialFace.FrontAndBack, MaterialParameter.Diffuse, @params);
				@params = new Color4(1f, 1f, 1f, 0.4f);
				GL.Material(MaterialFace.FrontAndBack, MaterialParameter.Specular, @params);
				@params = new Color4(0.2f, 0.2f, 0.2f, 0.1f);
				GL.Material(MaterialFace.FrontAndBack, MaterialParameter.Ambient, @params);
				@params = new Color4(0f, 0f, 0f, 0f);
				GL.Material(MaterialFace.FrontAndBack, MaterialParameter.Emission, @params);
				GL.Material(MaterialFace.FrontAndBack, MaterialParameter.Shininess, 16f);
			}
			else
			{
				GL.Disable(EnableCap.Lighting);
				GL.Color3(@params.R, @params.G, @params.B);
			}
			GL.Disable(EnableCap.ColorMaterial);
			GL.Disable(EnableCap.Texture2D);
		}
	}
}
