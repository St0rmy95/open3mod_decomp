using System;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Drawing.Imaging;
using System.Runtime.InteropServices;
using Assimp;
using CoreSettings;
using OpenTK.Graphics.OpenGL;

namespace open3mod
{
	// Token: 0x0200005F RID: 95
	public sealed class Texture : IDisposable
	{
		// Token: 0x06000330 RID: 816 RVA: 0x0001BA40 File Offset: 0x00019C40
		public Texture(string file, string baseDir, Texture.CompletionCallback callback)
		{
			Texture <>4__this = this;
			this._file = file;
			this._baseDir = baseDir;
			this._callback = delegate(string s, Image image, string actualLocation, TextureLoader.LoadResult status)
			{
				callback(<>4__this);
			};
			if (CoreSettings.Default.LoadTextures)
			{
				this.LoadAsync();
				return;
			}
			this.SetImage(null, TextureLoader.LoadResult.FileNotFound);
		}

		// Token: 0x06000331 RID: 817 RVA: 0x0001BAD0 File Offset: 0x00019CD0
		public Texture(EmbeddedTexture dataSource, string refName, Texture.CompletionCallback callback)
		{
			Texture <>4__this = this;
			this._file = refName;
			this._dataSource = dataSource;
			this._callback = delegate(string s, Image image, string actualLocation, TextureLoader.LoadResult status)
			{
				callback(<>4__this);
			};
			if (CoreSettings.Default.LoadTextures)
			{
				this.LoadAsync();
				return;
			}
			this.SetImage(null, TextureLoader.LoadResult.FileNotFound);
		}

		// Token: 0x17000097 RID: 151
		// (get) Token: 0x06000332 RID: 818 RVA: 0x0001BB44 File Offset: 0x00019D44
		public Image Image
		{
			get
			{
				return this._image;
			}
		}

		// Token: 0x17000098 RID: 152
		// (get) Token: 0x06000333 RID: 819 RVA: 0x0001BB4C File Offset: 0x00019D4C
		public string ActualLocation
		{
			get
			{
				return this._actualLocation;
			}
		}

		// Token: 0x17000099 RID: 153
		// (get) Token: 0x06000334 RID: 820 RVA: 0x0001BB54 File Offset: 0x00019D54
		public Texture.AlphaState HasAlpha
		{
			get
			{
				if (this._alphaState == Texture.AlphaState.NotKnownYet)
				{
					this.TryDetectAlpha();
				}
				return this._alphaState;
			}
		}

		// Token: 0x1700009A RID: 154
		// (get) Token: 0x06000335 RID: 821 RVA: 0x0001BB6E File Offset: 0x00019D6E
		// (set) Token: 0x06000336 RID: 822 RVA: 0x0001BB78 File Offset: 0x00019D78
		public Texture.TextureState State
		{
			get
			{
				return this._state;
			}
			private set
			{
				this._state = value;
			}
		}

		// Token: 0x1700009B RID: 155
		// (get) Token: 0x06000337 RID: 823 RVA: 0x0001BB83 File Offset: 0x00019D83
		public string FileName
		{
			get
			{
				return this._file;
			}
		}

		// Token: 0x1700009C RID: 156
		// (get) Token: 0x06000338 RID: 824 RVA: 0x0001BB8B File Offset: 0x00019D8B
		// (set) Token: 0x06000339 RID: 825 RVA: 0x0001BB95 File Offset: 0x00019D95
		public bool ReconfigureUploadedTextureRequested
		{
			get
			{
				return this._reconfigure;
			}
			set
			{
				this._reconfigure = value;
			}
		}

		// Token: 0x0600033A RID: 826 RVA: 0x0001BBA0 File Offset: 0x00019DA0
		public void Upload()
		{
			if (this._gl != 0)
			{
				GL.DeleteTexture(this._gl);
				this._gl = 0;
			}
			lock (this._lock)
			{
				Bitmap bitmap = null;
				bool flag = false;
				try
				{
					if (this._image is Bitmap)
					{
						bitmap = (Bitmap)this._image;
					}
					else
					{
						bitmap = new Bitmap(this._image);
						flag = true;
					}
					GL.GetError();
					if (GraphicsSettings.Default.TexQualityBias > 0)
					{
						Bitmap bitmap2 = Texture.ApplyResolutionBias(bitmap, GraphicsSettings.Default.TexQualityBias);
						if (flag)
						{
							bitmap.Dispose();
						}
						bitmap = bitmap2;
						flag = true;
					}
					BitmapData bitmapData = bitmap.LockBits(new Rectangle(0, 0, bitmap.Width, bitmap.Height), ImageLockMode.ReadOnly, System.Drawing.Imaging.PixelFormat.Format32bppArgb);
					if (this._alphaState == Texture.AlphaState.NotKnownYet)
					{
						this._alphaState = (Texture.LookForAlphaBits(bitmapData) ? Texture.AlphaState.HasAlpha : Texture.AlphaState.Opaque);
					}
					int num;
					GL.GenTextures(1, out num);
					GL.ActiveTexture(TextureUnit.Texture0);
					GL.BindTexture(TextureTarget.Texture2D, num);
					this.ConfigureFilters();
					GL.TexImage2D(TextureTarget.Texture2D, 0, PixelInternalFormat.Four, bitmap.Width, bitmap.Height, 0, OpenTK.Graphics.OpenGL.PixelFormat.Bgra, PixelType.UnsignedByte, bitmapData.Scan0);
					bitmap.UnlockBits(bitmapData);
					if (GL.GetError() == ErrorCode.NoError)
					{
						this._gl = num;
						this.State = Texture.TextureState.GlTextureCreated;
					}
				}
				finally
				{
					if (flag)
					{
						bitmap.Dispose();
					}
				}
			}
		}

		// Token: 0x0600033B RID: 827 RVA: 0x0001BD28 File Offset: 0x00019F28
		public void BindGlTexture()
		{
			GL.BindTexture(TextureTarget.Texture2D, this._gl);
			if (this.ReconfigureUploadedTextureRequested)
			{
				this.ReconfigureUploadedTexture();
			}
		}

		// Token: 0x0600033C RID: 828 RVA: 0x0001BD48 File Offset: 0x00019F48
		private void ConfigureFilters()
		{
			GraphicsSettings @default = GraphicsSettings.Default;
			bool useMips = @default.UseMips;
			switch (@default.TextureFilter)
			{
			case 0:
				GL.TexParameter(TextureTarget.Texture2D, TextureParameterName.TextureMinFilter, useMips ? 9984 : 9728);
				GL.TexParameter(TextureTarget.Texture2D, TextureParameterName.TextureMagFilter, 9728);
				break;
			case 1:
			case 2:
			case 3:
				GL.TexParameter(TextureTarget.Texture2D, TextureParameterName.TextureMinFilter, useMips ? 9987 : 9729);
				GL.TexParameter(TextureTarget.Texture2D, TextureParameterName.TextureMagFilter, 9729);
				break;
			}
			if (@default.TextureFilter >= 2)
			{
				float num;
				GL.GetFloat((GetPName)34047, out num);
				GL.TexParameter(TextureTarget.Texture2D, (TextureParameterName)34046, (@default.TextureFilter >= 3) ? num : (num * 0.5f));
			}
			else
			{
				GL.TexParameter(TextureTarget.Texture2D, (TextureParameterName)34046, 0f);
			}
			GL.TexParameter(TextureTarget.Texture2D, TextureParameterName.GenerateMipmap, useMips ? 1 : 0);
			if (!useMips)
			{
				return;
			}
			if (this.State == Texture.TextureState.GlTextureCreated)
			{
				GL.GenerateMipmap(GenerateMipmapTarget.Texture2D);
			}
		}

		// Token: 0x0600033D RID: 829 RVA: 0x0001BE64 File Offset: 0x0001A064
		public void ReleaseUpload()
		{
			lock (this._lock)
			{
				this.State = Texture.TextureState.WinFormsImageCreated;
			}
		}

		// Token: 0x0600033E RID: 830 RVA: 0x0001BEA0 File Offset: 0x0001A0A0
		public void ReconfigureUploadedTexture()
		{
			this.ReconfigureUploadedTextureRequested = false;
			int num;
			GL.GetInteger(GetPName.TextureBinding2D, out num);
			GL.BindTexture(TextureTarget.Texture2D, this._gl);
			this.ConfigureFilters();
			if (num != 0)
			{
				GL.BindTexture(TextureTarget.Texture2D, num);
			}
		}

		// Token: 0x0600033F RID: 831 RVA: 0x0001BEE4 File Offset: 0x0001A0E4
		private static Bitmap ApplyResolutionBias(Bitmap textureBitmap, int bias)
		{
			int width = textureBitmap.Width >> bias;
			int height = textureBitmap.Height >> bias;
			Bitmap bitmap = new Bitmap(width, height);
			using (Graphics graphics = Graphics.FromImage(bitmap))
			{
				graphics.InterpolationMode = InterpolationMode.NearestNeighbor;
				graphics.DrawImage(textureBitmap, 0, 0, width, height);
			}
			return bitmap;
		}

		// Token: 0x06000340 RID: 832 RVA: 0x0001BF48 File Offset: 0x0001A148
		private void TryDetectAlpha()
		{
			if (this.State != Texture.TextureState.WinFormsImageCreated && this.State != Texture.TextureState.GlTextureCreated)
			{
				return;
			}
			lock (this._lock)
			{
				try
				{
					Bitmap bitmap;
					if (this._image is Bitmap)
					{
						bitmap = (Bitmap)this._image;
					}
					else
					{
						Image image = this._image;
						bitmap = (this._image = new Bitmap(this._image));
						image.Dispose();
					}
					BitmapData bitmapData = bitmap.LockBits(new Rectangle(0, 0, bitmap.Width, bitmap.Height), ImageLockMode.ReadOnly, System.Drawing.Imaging.PixelFormat.Format32bppArgb);
					this._alphaState = (Texture.LookForAlphaBits(bitmapData) ? Texture.AlphaState.HasAlpha : Texture.AlphaState.Opaque);
					bitmap.UnlockBits(bitmapData);
				}
				catch
				{
				}
			}
		}

		// Token: 0x06000341 RID: 833 RVA: 0x0001C018 File Offset: 0x0001A218
		private static bool LookForAlphaBits(BitmapData textureData)
		{
			int num = textureData.Stride * textureData.Height;
			byte[] array = new byte[num];
			int num2 = textureData.Width * 4;
			int num3 = textureData.Stride - num2;
			Marshal.Copy(textureData.Scan0, array, 0, num);
			int num4 = 3;
			int i = 0;
			while (i < textureData.Height)
			{
				int j = 0;
				while (j < textureData.Width)
				{
					if (array[num4] < 255)
					{
						return true;
					}
					j++;
					num4 += 4;
				}
				i++;
				num4 += num3;
			}
			return false;
		}

		// Token: 0x06000342 RID: 834 RVA: 0x0001C0D8 File Offset: 0x0001A2D8
		private void LoadAsync()
		{
			TextureQueue.CompletionCallback callback = delegate(string file, Image image, string actualLocation, TextureLoader.LoadResult result)
			{
				this.SetImage(image, result);
				this._actualLocation = actualLocation;
				if (this._callback != null)
				{
					this._callback(this._file, this._image, actualLocation, result);
				}
			};
			lock (this._lock)
			{
				this.State = Texture.TextureState.LoadingPending;
				if (this._dataSource != null)
				{
					TextureQueue.Enqueue(this._dataSource, this._file, callback);
				}
				else
				{
					TextureQueue.Enqueue(this._file, this._baseDir, callback);
				}
			}
		}

		// Token: 0x06000343 RID: 835 RVA: 0x0001C150 File Offset: 0x0001A350
		private void SetImage(Image image, TextureLoader.LoadResult result)
		{
			lock (this._lock)
			{
				this._image = image;
				this.State = ((result != TextureLoader.LoadResult.Good) ? Texture.TextureState.LoadingFailed : Texture.TextureState.WinFormsImageCreated);
				if (this._image != null)
				{
					this.TryDetectAlpha();
				}
			}
		}

		// Token: 0x06000344 RID: 836 RVA: 0x0001C1A8 File Offset: 0x0001A3A8
		public void Dispose()
		{
			if (this._gl != 0)
			{
				GL.DeleteTexture(this._gl);
				this._gl = 0;
			}
			if (this._image != null)
			{
				this._image.Dispose();
				this._image = null;
			}
			GC.SuppressFinalize(this);
		}

		// Token: 0x040002A8 RID: 680
		private readonly string _file;

		// Token: 0x040002A9 RID: 681
		private readonly TextureQueue.CompletionCallback _callback;

		// Token: 0x040002AA RID: 682
		private Image _image;

		// Token: 0x040002AB RID: 683
		private int _gl;

		// Token: 0x040002AC RID: 684
		private string _actualLocation;

		// Token: 0x040002AD RID: 685
		private readonly object _lock = new object();

		// Token: 0x040002AE RID: 686
		private readonly string _baseDir;

		// Token: 0x040002AF RID: 687
		private readonly EmbeddedTexture _dataSource;

		// Token: 0x040002B0 RID: 688
		private volatile Texture.AlphaState _alphaState;

		// Token: 0x040002B1 RID: 689
		private volatile Texture.TextureState _state;

		// Token: 0x040002B2 RID: 690
		private volatile bool _reconfigure;

		// Token: 0x02000060 RID: 96
		public enum TextureState
		{
			// Token: 0x040002B4 RID: 692
			LoadingPending,
			// Token: 0x040002B5 RID: 693
			LoadingFailed,
			// Token: 0x040002B6 RID: 694
			WinFormsImageCreated,
			// Token: 0x040002B7 RID: 695
			GlTextureCreated
		}

		// Token: 0x02000061 RID: 97
		public enum AlphaState
		{
			// Token: 0x040002B9 RID: 697
			NotKnownYet,
			// Token: 0x040002BA RID: 698
			Opaque,
			// Token: 0x040002BB RID: 699
			HasAlpha
		}

		// Token: 0x02000062 RID: 98
		// (Invoke) Token: 0x06000347 RID: 839
		public delegate void CompletionCallback(Texture self);
	}
}
