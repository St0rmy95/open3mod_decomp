using System;
using Assimp;
using OpenTK;
using OpenTK.Graphics;
using OpenTK.Graphics.OpenGL;

namespace open3mod
{
	// Token: 0x02000019 RID: 25
	public static class OverlayBoundingBox
	{
		// Token: 0x060000E5 RID: 229 RVA: 0x000074AC File Offset: 0x000056AC
		public static void DrawBoundingBox(Node node, int meshIndex, Mesh mesh, CpuSkinningEvaluator skinner)
		{
			GL.Disable(EnableCap.Lighting);
			GL.Disable(EnableCap.Texture2D);
			GL.Enable(EnableCap.ColorMaterial);
			GL.Color4(new Color4(1f, 0f, 0f, 1f));
			Vector3 v = new Vector3(1E+10f, 1E+10f, 1E+10f);
			Vector3 vector = new Vector3(-1E+10f, -1E+10f, -1E+10f);
			uint num = 0U;
			while ((ulong)num < (ulong)((long)mesh.VertexCount))
			{
				Vector3 vector2;
				if (skinner != null && mesh.HasBones)
				{
					skinner.GetTransformedVertexPosition(node, meshIndex, num, out vector2);
				}
				else
				{
					vector2 = AssimpToOpenTk.FromVector(mesh.Vertices[(int)num]);
				}
				v.X = Math.Min(v.X, vector2.X);
				v.Y = Math.Min(v.Y, vector2.Y);
				v.Z = Math.Min(v.Z, vector2.Z);
				vector.X = Math.Max(vector.X, vector2.X);
				vector.Y = Math.Max(vector.Y, vector2.Y);
				vector.Z = Math.Max(vector.Z, vector2.Z);
				num += 1U;
			}
			GL.Begin(BeginMode.LineLoop);
			GL.Vertex3(v);
			GL.Vertex3(new Vector3(v.X, vector.Y, v.Z));
			GL.Vertex3(new Vector3(v.X, vector.Y, vector.Z));
			GL.Vertex3(new Vector3(v.X, v.Y, vector.Z));
			GL.End();
			GL.Begin(BeginMode.LineLoop);
			GL.Vertex3(new Vector3(vector.X, v.Y, v.Z));
			GL.Vertex3(new Vector3(vector.X, vector.Y, v.Z));
			GL.Vertex3(new Vector3(vector.X, vector.Y, vector.Z));
			GL.Vertex3(new Vector3(vector.X, v.Y, vector.Z));
			GL.End();
			GL.Begin(BeginMode.Lines);
			GL.Vertex3(v);
			GL.Vertex3(new Vector3(vector.X, v.Y, v.Z));
			GL.Vertex3(new Vector3(v.X, vector.Y, v.Z));
			GL.Vertex3(new Vector3(vector.X, vector.Y, v.Z));
			GL.Vertex3(new Vector3(v.X, vector.Y, vector.Z));
			GL.Vertex3(new Vector3(vector.X, vector.Y, vector.Z));
			GL.Vertex3(new Vector3(v.X, v.Y, vector.Z));
			GL.Vertex3(new Vector3(vector.X, v.Y, vector.Z));
			GL.End();
			GL.Disable(EnableCap.ColorMaterial);
		}
	}
}
