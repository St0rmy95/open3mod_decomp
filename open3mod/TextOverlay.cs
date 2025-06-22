using System;
using System.Drawing;
using System.Drawing.Imaging;
using System.Drawing.Text;
using OpenTK.Graphics.OpenGL;

namespace open3mod
{
	// Token: 0x0200005E RID: 94
	public class TextOverlay : IDisposable
	{
		// Token: 0x17000095 RID: 149
		// (get) Token: 0x06000325 RID: 805 RVA: 0x0001B527 File Offset: 0x00019727
		public bool WantRedraw
		{
			get
			{
				return this._tempContext != null;
			}
		}

		// Token: 0x17000096 RID: 150
		// (get) Token: 0x06000326 RID: 806 RVA: 0x0001B535 File Offset: 0x00019735
		// (set) Token: 0x06000327 RID: 807 RVA: 0x0001B53D File Offset: 0x0001973D
		public bool WantRedrawNextFrame { get; set; }

		// Token: 0x06000328 RID: 808 RVA: 0x0001B548 File Offset: 0x00019748
		public TextOverlay(Renderer renderer)
		{
			this._renderer = renderer;
			Size renderResolution = renderer.RenderResolution;
			this._textBmp = new Bitmap(renderResolution.Width, renderResolution.Height);
			this._textTexture = GL.GenTexture();
			GL.BindTexture(TextureTarget.Texture2D, this._textTexture);
			GL.TexParameter(TextureTarget.Texture2D, TextureParameterName.TextureMagFilter, 9729);
			GL.TexParameter(TextureTarget.Texture2D, TextureParameterName.TextureMinFilter, 9729);
			GL.TexImage2D(TextureTarget.Texture2D, 0, PixelInternalFormat.Rgba, this._textBmp.Width, this._textBmp.Height, 0, OpenTK.Graphics.OpenGL.PixelFormat.Bgra, PixelType.UnsignedByte, IntPtr.Zero);
		}

		// Token: 0x06000329 RID: 809 RVA: 0x0001B5FC File Offset: 0x000197FC
		public void Resize()
		{
			Size renderResolution = this._renderer.RenderResolution;
			if (renderResolution.Width == 0 || renderResolution.Height == 0)
			{
				return;
			}
			this._textBmp.Dispose();
			this._textBmp = new Bitmap(renderResolution.Width, renderResolution.Height);
			GL.BindTexture(TextureTarget.Texture2D, this._textTexture);
			GL.TexImage2D(TextureTarget.Texture2D, 0, PixelInternalFormat.Rgba, this._textBmp.Width, this._textBmp.Height, 0, OpenTK.Graphics.OpenGL.PixelFormat.Bgra, PixelType.UnsignedByte, IntPtr.Zero);
			this.GetDrawableGraphicsContext();
		}

		// Token: 0x0600032A RID: 810 RVA: 0x0001B69C File Offset: 0x0001989C
		public Graphics GetDrawableGraphicsContext()
		{
			if (this._disposed)
			{
				return null;
			}
			if (this._tempContext == null)
			{
				try
				{
					this._tempContext = Graphics.FromImage(this._textBmp);
				}
				catch (Exception)
				{
					this._tempContext = null;
					return null;
				}
				this._tempContext.Clear(Color.Transparent);
				this._tempContext.TextRenderingHint = TextRenderingHint.AntiAliasGridFit;
			}
			return this._tempContext;
		}

		// Token: 0x0600032B RID: 811 RVA: 0x0001B710 File Offset: 0x00019910
		public void Clear()
		{
			if (this._tempContext == null)
			{
				this._tempContext = Graphics.FromImage(this._textBmp);
			}
			this._tempContext.Clear(Color.Transparent);
		}

		// Token: 0x0600032C RID: 812 RVA: 0x0001B73B File Offset: 0x0001993B
		public void Dispose()
		{
			this.Dispose(true);
			GC.SuppressFinalize(this);
		}

		// Token: 0x0600032D RID: 813 RVA: 0x0001B74C File Offset: 0x0001994C
		protected virtual void Dispose(bool disposing)
		{
			if (this._disposed)
			{
				return;
			}
			this._disposed = true;
			if (disposing)
			{
				if (this._textBmp != null)
				{
					this._textBmp.Dispose();
					this._textBmp = null;
				}
				if (this._tempContext != null)
				{
					this._tempContext.Dispose();
					this._tempContext = null;
				}
			}
			if (this._textTexture > 0)
			{
				GL.DeleteTexture(this._textTexture);
				this._textTexture = 0;
			}
		}

		// Token: 0x0600032E RID: 814 RVA: 0x0001B7BC File Offset: 0x000199BC
		public void Draw()
		{
			if (this._tempContext != null)
			{
				this.Commit();
				this._tempContext.Dispose();
				this._tempContext = null;
			}
			GL.MatrixMode(MatrixMode.Modelview);
			GL.PushMatrix();
			GL.LoadIdentity();
			GL.MatrixMode(MatrixMode.Projection);
			GL.PushMatrix();
			GL.LoadIdentity();
			Size renderResolution = this._renderer.RenderResolution;
			GL.Ortho(0.0, (double)renderResolution.Width, (double)renderResolution.Height, 0.0, -1.0, 1.0);
			GL.Enable(EnableCap.Texture2D);
			GL.Enable(EnableCap.Blend);
			GL.BlendFunc(BlendingFactorSrc.SrcAlpha, BlendingFactorDest.OneMinusSrcAlpha);
			GL.BindTexture(TextureTarget.Texture2D, this._textTexture);
			GL.Color3(1f, 1f, 1f);
			GL.Begin(BeginMode.Quads);
			GL.TexCoord2(0f, 0f);
			GL.Vertex2(0f, 0f);
			GL.TexCoord2(1f, 0f);
			GL.Vertex2((float)renderResolution.Width - 0f, 0f);
			GL.TexCoord2(1f, 1f);
			GL.Vertex2((float)renderResolution.Width - 0f, (float)renderResolution.Height - 0f);
			GL.TexCoord2(0f, 1f);
			GL.Vertex2(0f, (float)renderResolution.Height - 0f);
			GL.End();
			GL.Disable(EnableCap.Blend);
			GL.Disable(EnableCap.Texture2D);
			GL.PopMatrix();
			GL.MatrixMode(MatrixMode.Modelview);
			GL.PopMatrix();
			if (this.WantRedrawNextFrame)
			{
				this.GetDrawableGraphicsContext();
				this.WantRedrawNextFrame = false;
			}
		}

		// Token: 0x0600032F RID: 815 RVA: 0x0001B984 File Offset: 0x00019B84
		private void Commit()
		{
			try
			{
				BitmapData bitmapData = this._textBmp.LockBits(new Rectangle(0, 0, this._textBmp.Width, this._textBmp.Height), ImageLockMode.ReadOnly, System.Drawing.Imaging.PixelFormat.Format32bppArgb);
				GL.BindTexture(TextureTarget.Texture2D, this._textTexture);
				GL.TexSubImage2D(TextureTarget.Texture2D, 0, 0, 0, this._textBmp.Width, this._textBmp.Height, OpenTK.Graphics.OpenGL.PixelFormat.Bgra, PixelType.UnsignedByte, bitmapData.Scan0);
				this._textBmp.UnlockBits(bitmapData);
			}
			catch (ArgumentException)
			{
			}
		}

		// Token: 0x040002A2 RID: 674
		private readonly Renderer _renderer;

		// Token: 0x040002A3 RID: 675
		private Bitmap _textBmp;

		// Token: 0x040002A4 RID: 676
		private int _textTexture;

		// Token: 0x040002A5 RID: 677
		private Graphics _tempContext;

		// Token: 0x040002A6 RID: 678
		private bool _disposed;
	}
}
