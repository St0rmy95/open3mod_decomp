using System;
using System.Drawing;
using System.Drawing.Imaging;
using System.IO;
using System.Runtime.InteropServices;
using Assimp;

namespace open3mod
{
	// Token: 0x0200001C RID: 28
	public class EmbeddedTextureLoader : TextureLoader
	{
		// Token: 0x060000ED RID: 237 RVA: 0x00007AF0 File Offset: 0x00005CF0
		public EmbeddedTextureLoader(EmbeddedTexture tex)
		{
			if (tex.IsCompressed)
			{
				if (!tex.HasCompressedData)
				{
					return;
				}
				base.SetFromStream(new MemoryStream(tex.CompressedData));
				return;
			}
			else
			{
				if (!tex.HasNonCompressedData || tex.Width < 1 || tex.Height < 1)
				{
					return;
				}
				Texel[] nonCompressedData = tex.NonCompressedData;
				Bitmap bitmap = new Bitmap(tex.Width, tex.Height, PixelFormat.Format32bppArgb);
				Rectangle rect = new Rectangle(0, 0, tex.Width, tex.Height);
				BitmapData bitmapData;
				try
				{
					bitmapData = bitmap.LockBits(rect, ImageLockMode.WriteOnly, bitmap.PixelFormat);
				}
				catch
				{
					return;
				}
				IntPtr scan = bitmapData.Scan0;
				int num = bitmapData.Stride * bitmap.Height;
				byte[] array = new byte[num];
				int num2 = bitmap.Width * 4;
				int num3 = bitmapData.Stride - num2;
				int num4 = 0;
				foreach (Texel texel in nonCompressedData)
				{
					array[num4++] = texel.B;
					array[num4++] = texel.G;
					array[num4++] = texel.R;
					array[num4++] = texel.A;
					if (num4 % num2 == 0)
					{
						num4 += num3;
					}
				}
				Marshal.Copy(array, 0, scan, num);
				bitmap.UnlockBits(bitmapData);
				this._image = bitmap;
				this._result = TextureLoader.LoadResult.Good;
				return;
			}
		}
	}
}
