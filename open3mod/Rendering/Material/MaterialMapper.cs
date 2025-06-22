using System;
using Assimp;

namespace open3mod
{
	// Token: 0x02000014 RID: 20
	public abstract class MaterialMapper : IDisposable
	{
		// Token: 0x060000C6 RID: 198 RVA: 0x000059CF File Offset: 0x00003BCF
		protected MaterialMapper(Scene scene)
		{
			this._scene = scene;
		}

		// Token: 0x060000C7 RID: 199
		public abstract void Dispose();

		// Token: 0x060000C8 RID: 200 RVA: 0x000059E0 File Offset: 0x00003BE0
		public bool IsAlphaMaterial(Material material)
		{
			if (material.HasOpacity && MaterialMapper.IsTransparent(material.Opacity))
			{
				return true;
			}
			if (material.HasColorDiffuse && MaterialMapper.IsTransparent(material.ColorDiffuse.A))
			{
				return true;
			}
			if (material.HasColorSpecular && MaterialMapper.IsTransparent(material.ColorSpecular.A))
			{
				return true;
			}
			if (material.HasColorAmbient && MaterialMapper.IsTransparent(material.ColorAmbient.A))
			{
				return true;
			}
			if (material.HasColorEmissive && MaterialMapper.IsTransparent(material.ColorEmissive.A))
			{
				return true;
			}
			if (material.GetMaterialTextureCount(TextureType.Diffuse) > 0)
			{
				TextureSlot textureSlot;
				material.GetMaterialTexture(TextureType.Diffuse, 0, out textureSlot);
				Texture originalOrReplacement = this._scene.TextureSet.GetOriginalOrReplacement(textureSlot.FilePath);
				if (originalOrReplacement.HasAlpha == Texture.AlphaState.HasAlpha)
				{
					return true;
				}
			}
			return false;
		}

		// Token: 0x060000C9 RID: 201 RVA: 0x00005AAD File Offset: 0x00003CAD
		private static bool IsTransparent(float f)
		{
			return f < 1f && f > 1E-05f;
		}

		// Token: 0x060000CA RID: 202
		public abstract void ApplyMaterial(Mesh mesh, Material mat, bool textured, bool shaded);

		// Token: 0x060000CB RID: 203
		public abstract void ApplyGhostMaterial(Mesh mesh, Material material, bool shaded);

		// Token: 0x060000CC RID: 204 RVA: 0x00005AC4 File Offset: 0x00003CC4
		public bool UploadTextures(Material material)
		{
			bool result = false;
			if (material.GetMaterialTextureCount(TextureType.Diffuse) > 0)
			{
				TextureSlot textureSlot;
				material.GetMaterialTexture(TextureType.Diffuse, 0, out textureSlot);
				Texture originalOrReplacement = this._scene.TextureSet.GetOriginalOrReplacement(textureSlot.FilePath);
				if (originalOrReplacement.State == Texture.TextureState.WinFormsImageCreated)
				{
					originalOrReplacement.Upload();
					result = true;
				}
				else if (originalOrReplacement.ReconfigureUploadedTextureRequested)
				{
					originalOrReplacement.ReconfigureUploadedTexture();
				}
			}
			return result;
		}

		// Token: 0x04000054 RID: 84
		public const float AlphaSuppressionThreshold = 1E-05f;

		// Token: 0x04000055 RID: 85
		protected readonly Scene _scene;
	}
}
