using System;
using System.Drawing;
using System.Drawing.Imaging;
using System.IO;
using DevIL;
using DevIL.Unmanaged;

namespace open3mod
{
	// Token: 0x0200001A RID: 26
	public class TextureLoader
	{
		// Token: 0x060000E6 RID: 230 RVA: 0x000077E9 File Offset: 0x000059E9
		protected TextureLoader()
		{
			this._result = TextureLoader.LoadResult.UnknownFileFormat;
		}

		// Token: 0x060000E7 RID: 231 RVA: 0x000077F8 File Offset: 0x000059F8
		public TextureLoader(string name, string basedir)
		{
			try
			{
				using (Stream stream = TextureLoader.ObtainStream(name, basedir, out this._actualLocation))
				{
					this.SetFromStream(stream);
				}
			}
			catch (Exception ex)
			{
				this._result = TextureLoader.LoadResult.FileNotFound;
			}
		}

		// Token: 0x060000E8 RID: 232 RVA: 0x00007854 File Offset: 0x00005A54
		protected void SetFromStream(Stream stream)
		{
			try
			{
				using (System.Drawing.Image image = System.Drawing.Image.FromStream(stream))
				{
					this._image = new Bitmap(image.Width, image.Height, PixelFormat.Format32bppArgb);
					using (Graphics graphics = Graphics.FromImage(this._image))
					{
						graphics.DrawImage(image, 0, 0, image.Width, image.Height);
					}
					this._result = TextureLoader.LoadResult.Good;
				}
			}
			catch (Exception)
			{
				using (ImageImporter imageImporter = new ImageImporter())
				{
					try
					{
						using (DevIL.Image image2 = imageImporter.LoadImageFromStream(stream))
						{
							image2.Bind();
							ImageInfo imageInfo = IL.GetImageInfo();
							Bitmap bitmap = new Bitmap(imageInfo.Width, imageInfo.Height, PixelFormat.Format32bppArgb);
							Rectangle rect = new Rectangle(0, 0, imageInfo.Width, imageInfo.Height);
							BitmapData bitmapData = bitmap.LockBits(rect, ImageLockMode.WriteOnly, PixelFormat.Format32bppArgb);
							IL.CopyPixels(0, 0, 0, imageInfo.Width, imageInfo.Height, 1, DataFormat.BGRA, DataType.UnsignedByte, bitmapData.Scan0);
							bitmap.UnlockBits(bitmapData);
							this._image = bitmap;
							this._image.RotateFlip(RotateFlipType.RotateNoneFlipY);
							this._result = TextureLoader.LoadResult.Good;
						}
					}
					catch (Exception)
					{
						this._result = TextureLoader.LoadResult.UnknownFileFormat;
					}
				}
			}
		}

		// Token: 0x060000E9 RID: 233 RVA: 0x000079DC File Offset: 0x00005BDC
		public static Stream ObtainStream(string name, string basedir, out string actualLocation)
		{
			Stream stream = null;
			string text = null;
			try
			{
				text = Path.Combine(basedir, name);
				stream = new FileStream(text, FileMode.Open, FileAccess.Read);
			}
			catch (IOException)
			{
				string fileName = Path.GetFileName(name);
				if (fileName == null)
				{
					throw;
				}
				try
				{
					text = Path.Combine(basedir, fileName);
					stream = new FileStream(text, FileMode.Open, FileAccess.Read);
				}
				catch (IOException)
				{
					try
					{
						text = name;
						stream = new FileStream(name, FileMode.Open, FileAccess.Read);
					}
					catch (IOException)
					{
						foreach (string path in Properties.CoreSettings.Default.AdditionalTextureFolders)
						{
							try
							{
								text = Path.Combine(path, fileName);
								stream = new FileStream(text, FileMode.Open, FileAccess.Read);
								break;
							}
							catch (IOException)
							{
							}
						}
						if (stream == null)
						{
							throw new IOException();
						}
					}
				}
			}
			actualLocation = text;
			return stream;
		}

		// Token: 0x17000043 RID: 67
		// (get) Token: 0x060000EA RID: 234 RVA: 0x00007AD8 File Offset: 0x00005CD8
		public System.Drawing.Image Image
		{
			get
			{
				return this._image;
			}
		}

		// Token: 0x17000044 RID: 68
		// (get) Token: 0x060000EB RID: 235 RVA: 0x00007AE0 File Offset: 0x00005CE0
		public TextureLoader.LoadResult Result
		{
			get
			{
				return this._result;
			}
		}

		// Token: 0x17000045 RID: 69
		// (get) Token: 0x060000EC RID: 236 RVA: 0x00007AE8 File Offset: 0x00005CE8
		public string ActualLocation
		{
			get
			{
				return this._actualLocation;
			}
		}

		// Token: 0x04000074 RID: 116
		protected TextureLoader.LoadResult _result;

		// Token: 0x04000075 RID: 117
		protected System.Drawing.Image _image;

		// Token: 0x04000076 RID: 118
		protected string _actualLocation;

		// Token: 0x0200001B RID: 27
		public enum LoadResult
		{
			// Token: 0x04000078 RID: 120
			Good,
			// Token: 0x04000079 RID: 121
			FileNotFound,
			// Token: 0x0400007A RID: 122
			UnknownFileFormat
		}
	}
}
