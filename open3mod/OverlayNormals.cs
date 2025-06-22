using System;
using Assimp;
using OpenTK;
using OpenTK.Graphics;
using OpenTK.Graphics.OpenGL;

namespace open3mod
{
	// Token: 0x0200003F RID: 63
	public static class OverlayNormals
	{
		// Token: 0x06000240 RID: 576 RVA: 0x000134E0 File Offset: 0x000116E0
		public static void DrawNormals(Node node, int meshIndex, Mesh mesh, CpuSkinningEvaluator skinner, float invGlobalScale, Matrix4 transform)
		{
			if (!mesh.HasNormals)
			{
				return;
			}
			Matrix4 mat = transform;
			mat.Invert();
			mat.Transpose();
			float scale = invGlobalScale * 0.05f;
			GL.Begin(BeginMode.Lines);
			GL.Disable(EnableCap.Lighting);
			GL.Disable(EnableCap.Texture2D);
			GL.Enable(EnableCap.ColorMaterial);
			GL.Color4(new Color4(0f, 1f, 0f, 1f));
			uint num = 0U;
			while ((ulong)num < (ulong)((long)mesh.VertexCount))
			{
				Vector3 vector;
				if (skinner != null && mesh.HasBones)
				{
					skinner.GetTransformedVertexPosition(node, meshIndex, num, out vector);
				}
				else
				{
					vector = AssimpToOpenTk.FromVector(mesh.Vertices[(int)num]);
				}
				vector = Vector4.Transform(new Vector4(vector, 1f), transform).Xyz;
				Vector3 vector2;
				if (skinner != null)
				{
					skinner.GetTransformedVertexNormal(node, meshIndex, num, out vector2);
				}
				else
				{
					vector2 = AssimpToOpenTk.FromVector(mesh.Normals[(int)num]);
				}
				vector2 = Vector4.Transform(new Vector4(vector2, 0f), mat).Xyz;
				vector2.Normalize();
				GL.Vertex3(vector);
				GL.Vertex3(vector + vector2 * scale);
				num += 1U;
			}
			GL.End();
			GL.Disable(EnableCap.ColorMaterial);
		}
	}
}
