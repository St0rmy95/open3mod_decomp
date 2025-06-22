using System;
using Assimp;
using OpenTK;
using OpenTK.Graphics;
using OpenTK.Graphics.OpenGL;

namespace open3mod
{
	// Token: 0x02000040 RID: 64
	public static class OverlaySkeleton
	{
		// Token: 0x06000241 RID: 577 RVA: 0x00013620 File Offset: 0x00011820
		public static void DrawSkeletonBone(Node node, float invGlobalScale, bool highlight)
		{
			Vector3 vector = new Vector3(node.Transform.A4, node.Transform.B4, node.Transform.C4);
			if (vector.LengthSquared <= 1E-06f)
			{
				return;
			}
			GL.Disable(EnableCap.Lighting);
			GL.Disable(EnableCap.Texture2D);
			GL.Enable(EnableCap.ColorMaterial);
			GL.Disable(EnableCap.DepthTest);
			GL.Color4(highlight ? new Color4(0f, 1f, 0.5f, 1f) : new Color4(0f, 0.5f, 1f, 1f));
			Vector3 vec = new Vector3(1f, 0f, 0f);
			Vector3 vector2 = vector;
			vector2.Normalize();
			Vector3 vec2;
			Vector3.Cross(ref vector2, ref vec, out vec2);
			Vector3.Cross(ref vec2, ref vector2, out vec);
			vec2 *= invGlobalScale;
			vec *= invGlobalScale;
			GL.Begin(BeginMode.LineLoop);
			GL.Vertex3(-0.03f * vec2 + -0.03f * vec);
			GL.Vertex3(-0.03f * vec2 + 0.03f * vec);
			GL.Vertex3(0.03f * vec2 + 0.03f * vec);
			GL.Vertex3(0.03f * vec2 + -0.03f * vec);
			GL.End();
			GL.Begin(BeginMode.Lines);
			GL.Vertex3(-0.03f * vec2 + -0.03f * vec);
			GL.Vertex3(vector);
			GL.Vertex3(-0.03f * vec2 + 0.03f * vec);
			GL.Vertex3(vector);
			GL.Vertex3(0.03f * vec2 + 0.03f * vec);
			GL.Vertex3(vector);
			GL.Vertex3(0.03f * vec2 + -0.03f * vec);
			GL.Vertex3(vector);
			GL.Color4(highlight ? new Color4(1f, 0f, 0f, 1f) : new Color4(1f, 1f, 0f, 1f));
			GL.Vertex3(Vector3.Zero);
			GL.Vertex3(vector);
			GL.End();
			GL.Disable(EnableCap.ColorMaterial);
			GL.Enable(EnableCap.DepthTest);
		}
	}
}
