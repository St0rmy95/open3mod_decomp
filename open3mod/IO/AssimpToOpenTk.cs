using System;
using Assimp;
using OpenTK;
using OpenTK.Graphics;

namespace open3mod
{
	// Token: 0x02000006 RID: 6
	public static class AssimpToOpenTk
	{
		// Token: 0x0600002A RID: 42 RVA: 0x00003F3F File Offset: 0x0000213F
		public static Matrix4 FromMatrix(Matrix4x4 mat)
		{
			return AssimpToOpenTk.FromMatrix(ref mat);
		}

		// Token: 0x0600002B RID: 43 RVA: 0x00003F48 File Offset: 0x00002148
		public static Matrix4 FromMatrix(ref Matrix4x4 mat)
		{
			return new Matrix4
			{
				M11 = mat.A1,
				M12 = mat.A2,
				M13 = mat.A3,
				M14 = mat.A4,
				M21 = mat.B1,
				M22 = mat.B2,
				M23 = mat.B3,
				M24 = mat.B4,
				M31 = mat.C1,
				M32 = mat.C2,
				M33 = mat.C3,
				M34 = mat.C4,
				M41 = mat.D1,
				M42 = mat.D2,
				M43 = mat.D3,
				M44 = mat.D4
			};
		}

		// Token: 0x0600002C RID: 44 RVA: 0x00004030 File Offset: 0x00002230
		public static Vector3 FromVector(Vector3D vec)
		{
			Vector3 result;
			result.X = vec.X;
			result.Y = vec.Y;
			result.Z = vec.Z;
			return result;
		}

		// Token: 0x0600002D RID: 45 RVA: 0x00004068 File Offset: 0x00002268
		public static Color4 FromColor(Color4D color)
		{
			Color4 result;
			result.R = color.R;
			result.G = color.G;
			result.B = color.B;
			result.A = color.A;
			return result;
		}
	}
}
