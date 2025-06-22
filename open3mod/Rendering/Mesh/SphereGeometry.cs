using System;
using OpenTK;
using OpenTK.Graphics.OpenGL;

namespace open3mod
{
	// Token: 0x02000056 RID: 86
	public static class SphereGeometry
	{
		// Token: 0x06000301 RID: 769 RVA: 0x0001A9FC File Offset: 0x00018BFC
		public static void Draw(SphereGeometry.Vertex[] sphereVertices, ushort[] sphereElements)
		{
			GL.Begin(BeginMode.Triangles);
			foreach (ushort num in sphereElements)
			{
				SphereGeometry.Vertex vertex = sphereVertices[(int)num];
				GL.TexCoord2(vertex.TexCoord);
				GL.Normal3(vertex.Normal);
				GL.Vertex3(vertex.Position);
			}
			GL.End();
		}

		// Token: 0x06000302 RID: 770 RVA: 0x0001AA5C File Offset: 0x00018C5C
		public static SphereGeometry.Vertex[] CalculateVertices(float radius, float height, byte segments, byte rings)
		{
			SphereGeometry.Vertex[] array = new SphereGeometry.Vertex[(int)(segments * rings)];
			int num = 0;
			for (double num2 = 0.0; num2 < (double)rings; num2 += 1.0)
			{
				double num3 = num2 / (double)(rings - 1) * 3.1415926535897931;
				for (double num4 = 0.0; num4 < (double)segments; num4 += 1.0)
				{
					double num5 = num4 / (double)(segments - 1) * 2.0 * 3.1415926535897931;
					Vector3 vector = new Vector3
					{
						X = (float)((double)radius * Math.Sin(num3) * Math.Cos(num5)),
						Y = (float)((double)height * Math.Cos(num3)),
						Z = (float)((double)radius * Math.Sin(num3) * Math.Sin(num5))
					};
					Vector3 normal = Vector3.Normalize(vector);
					Vector2 texCoord = new Vector2
					{
						X = (float)(num4 / (double)(segments - 1)),
						Y = (float)(num2 / (double)(rings - 1))
					};
					array[num] = new SphereGeometry.Vertex
					{
						Position = vector,
						Normal = normal,
						TexCoord = texCoord
					};
					num++;
				}
			}
			return array;
		}

		// Token: 0x06000303 RID: 771 RVA: 0x0001ABA4 File Offset: 0x00018DA4
		public static ushort[] CalculateElements(byte segments, byte rings)
		{
			int num = (int)(segments * rings);
			ushort[] array = new ushort[num * 6];
			ushort num2 = 0;
			for (byte b = 0; b < rings - 1; b += 1)
			{
				for (byte b2 = 0; b2 < segments - 1; b2 += 1)
				{
					ushort[] array2 = array;
					ushort num3 = num2;
					num2 = num3++;
					array2[(int)num3] = (ushort)(b * segments + b2);
					ushort[] array3 = array;
					ushort num4 = num2;
					num2 = num4++;
					array3[(int)num4] = (ushort)((b + 1) * segments + b2);
					ushort[] array4 = array;
					ushort num5 = num2;
					num2 = num5++;
					array4[(int)num5] = (ushort)((b + 1) * segments + b2 + 1);
					ushort[] array5 = array;
					ushort num6 = num2;
					num2 = num6++;
					array5[(int)num6] = (ushort)((b + 1) * segments + b2 + 1);
					ushort[] array6 = array;
					ushort num7 = num2;
					num2 = num7++;
					array6[(int)num7] = (ushort)(b * segments + b2 + 1);
					ushort[] array7 = array;
					ushort num8 = num2;
					num2 = num8++;
					array7[(int)num8] = (ushort)(b * segments + b2);
				}
			}
			return array;
		}

		// Token: 0x02000057 RID: 87
		public struct Vertex
		{
			// Token: 0x0400027B RID: 635
			public Vector2 TexCoord;

			// Token: 0x0400027C RID: 636
			public Vector3 Normal;

			// Token: 0x0400027D RID: 637
			public Vector3 Position;
		}
	}
}
