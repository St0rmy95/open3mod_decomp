using System;
using Assimp;
using OpenTK;

namespace open3mod
{
	// Token: 0x0200003C RID: 60
	public static class OpenTkToAssimp
	{
		// Token: 0x06000232 RID: 562 RVA: 0x00012EC0 File Offset: 0x000110C0
		public static void FromMatrix(ref Matrix4 mConv, out Matrix4x4 m)
		{
			m.A1 = mConv.M11;
			m.A2 = mConv.M12;
			m.A3 = mConv.M13;
			m.A4 = mConv.M14;
			m.B1 = mConv.M21;
			m.B2 = mConv.M22;
			m.B3 = mConv.M23;
			m.B4 = mConv.M24;
			m.C1 = mConv.M31;
			m.C2 = mConv.M32;
			m.C3 = mConv.M33;
			m.C4 = mConv.M34;
			m.D1 = mConv.M41;
			m.D2 = mConv.M42;
			m.D3 = mConv.M43;
			m.D4 = mConv.M44;
		}

		// Token: 0x06000233 RID: 563 RVA: 0x00012F90 File Offset: 0x00011190
		public static Matrix4x4 FromMatrix(Matrix4 mConv)
		{
			Matrix4x4 result;
			OpenTkToAssimp.FromMatrix(ref mConv, out result);
			return result;
		}
	}
}
