using System;
using OpenTK;
using OpenTK.Graphics;

namespace open3mod
{
	// Token: 0x02000043 RID: 67
	internal class RenderControl : GLControl
	{
		// Token: 0x0600024E RID: 590 RVA: 0x00013A1F File Offset: 0x00011C1F
		public RenderControl() : base(new GraphicsMode(new ColorFormat(32), 24, 8, RenderControl.GetSampleCount(GraphicsSettings.Default.MultiSampling)))
		{
		}

		// Token: 0x0600024F RID: 591 RVA: 0x00013A48 File Offset: 0x00011C48
		private static int GetSampleCount(int multiSampling)
		{
			switch (multiSampling)
			{
			case 0:
				return 0;
			case 1:
				return 2;
			case 2:
				return 4;
			case 3:
				return RenderControl.MaximumSampleCount();
			default:
				return 0;
			}
		}

		// Token: 0x06000250 RID: 592 RVA: 0x00013A7C File Offset: 0x00011C7C
		private static int MaximumSampleCount()
		{
			int num = 0;
			int num2 = 0;
			do
			{
				GraphicsMode graphicsMode = new GraphicsMode(32, 0, 0, num2);
				if (graphicsMode.Samples == num2 && graphicsMode.Samples > num)
				{
					num = graphicsMode.Samples;
				}
				num2 += 2;
			}
			while (num2 <= 32);
			return num;
		}
	}
}
