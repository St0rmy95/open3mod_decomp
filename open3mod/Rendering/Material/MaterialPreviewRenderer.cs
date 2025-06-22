using System;
using System.Drawing;
using System.Drawing.Imaging;
using Assimp;
using OpenTK;
using OpenTK.Graphics.OpenGL;

namespace open3mod
{
	// Token: 0x02000034 RID: 52
	public class MaterialPreviewRenderer
	{
		// Token: 0x14000004 RID: 4
		// (add) Token: 0x060001F9 RID: 505 RVA: 0x000116C8 File Offset: 0x0000F8C8
		// (remove) Token: 0x060001FA RID: 506 RVA: 0x00011700 File Offset: 0x0000F900
		public event MaterialPreviewRenderer.OnPreviewAvailableDelegate PreviewAvailable;

		// Token: 0x060001FB RID: 507 RVA: 0x00011750 File Offset: 0x0000F950
		public MaterialPreviewRenderer(MainWindow window, Scene scene, Material material, uint width, uint height)
		{
			this._scene = scene;
			this._material = material;
			this._width = width;
			this._height = height;
			this._state = MaterialPreviewRenderer.CompletionState.Pending;
			window.Renderer.GlExtraDrawJob += delegate(object sender)
			{
				this._state = ((!this.RenderPreview()) ? MaterialPreviewRenderer.CompletionState.Failed : MaterialPreviewRenderer.CompletionState.Done);
				this.OnPreviewAvailable();
			};
		}

		// Token: 0x17000061 RID: 97
		// (get) Token: 0x060001FC RID: 508 RVA: 0x000117A6 File Offset: 0x0000F9A6
		public Image PreviewImage
		{
			get
			{
				return this._previewImage;
			}
		}

		// Token: 0x17000062 RID: 98
		// (get) Token: 0x060001FD RID: 509 RVA: 0x000117AE File Offset: 0x0000F9AE
		public MaterialPreviewRenderer.CompletionState State
		{
			get
			{
				return this._state;
			}
		}

		// Token: 0x060001FE RID: 510 RVA: 0x000117B8 File Offset: 0x0000F9B8
		private bool RenderPreview()
		{
			int width = (int)MaterialPreviewRenderer.RoundToNextPowerOfTwo(this._width);
			int height = (int)MaterialPreviewRenderer.RoundToNextPowerOfTwo(this._height);
			int num = -1;
			int num2 = -1;
			int num3 = -1;
			GL.GetError();
			GL.GenTextures(1, out num2);
			GL.BindTexture(TextureTarget.Texture2D, num2);
			GL.TexParameter(TextureTarget.Texture2D, TextureParameterName.TextureMinFilter, 9984);
			GL.TexParameter(TextureTarget.Texture2D, TextureParameterName.TextureMagFilter, 9728);
			GL.TexParameter(TextureTarget.Texture2D, TextureParameterName.TextureWrapS, 10496);
			GL.TexParameter(TextureTarget.Texture2D, TextureParameterName.TextureWrapT, 10496);
			GL.TexImage2D(TextureTarget.Texture2D, 0, PixelInternalFormat.Rgba8, width, height, 0, OpenTK.Graphics.OpenGL.PixelFormat.Rgba, PixelType.UnsignedByte, IntPtr.Zero);
			ErrorCode error = GL.GetError();
			if (error != ErrorCode.NoError)
			{
				MaterialPreviewRenderer.EnsureTemporaryResourcesReleased(num2, num3, num);
				return false;
			}
			GL.BindTexture(TextureTarget.Texture2D, 0);
			GL.Ext.GenRenderbuffers(1, out num3);
			GL.Ext.BindRenderbuffer(RenderbufferTarget.Renderbuffer, num3);
			GL.Ext.RenderbufferStorage(RenderbufferTarget.Renderbuffer, RenderbufferStorage.DepthComponent32, width, height);
			error = GL.GetError();
			if (error != ErrorCode.NoError)
			{
				MaterialPreviewRenderer.EnsureTemporaryResourcesReleased(num2, num3, num);
				return false;
			}
			GL.Ext.GenFramebuffers(1, out num);
			GL.Ext.BindFramebuffer(FramebufferTarget.Framebuffer, num);
			GL.Ext.FramebufferTexture2D(FramebufferTarget.Framebuffer, FramebufferAttachment.ColorAttachment0, TextureTarget.Texture2D, num2, 0);
			GL.Ext.FramebufferRenderbuffer(FramebufferTarget.Framebuffer, FramebufferAttachment.DepthAttachment, RenderbufferTarget.Renderbuffer, num3);
			FramebufferErrorCode framebufferErrorCode = GL.Ext.CheckFramebufferStatus(FramebufferTarget.Framebuffer);
			if (framebufferErrorCode != FramebufferErrorCode.FramebufferComplete)
			{
				MaterialPreviewRenderer.EnsureTemporaryResourcesReleased(num2, num3, num);
				return false;
			}
			GL.DrawBuffer(DrawBufferMode.ColorAttachment0);
			GL.PushAttrib(AttribMask.ViewportBit);
			GL.Viewport(0, 0, (int)this._width, (int)this._height);
			this.Draw();
			this.CopyToImage();
			GL.PopAttrib();
			GL.Ext.BindFramebuffer(FramebufferTarget.Framebuffer, 0);
			GL.DrawBuffer(DrawBufferMode.Back);
			MaterialPreviewRenderer.EnsureTemporaryResourcesReleased(num2, num3, num);
			return true;
		}

		// Token: 0x060001FF RID: 511 RVA: 0x00011984 File Offset: 0x0000FB84
		private void CopyToImage()
		{
			Bitmap bitmap = new Bitmap((int)this._width, (int)this._height);
			BitmapData bitmapData = bitmap.LockBits(new Rectangle(0, 0, (int)this._width, (int)this._height), ImageLockMode.WriteOnly, System.Drawing.Imaging.PixelFormat.Format32bppArgb);
			GL.ReadPixels(0, 0, (int)this._width, (int)this._height, OpenTK.Graphics.OpenGL.PixelFormat.Bgra, PixelType.UnsignedByte, bitmapData.Scan0);
			bitmap.UnlockBits(bitmapData);
			this._previewImage = bitmap;
		}

		// Token: 0x06000200 RID: 512 RVA: 0x000119F4 File Offset: 0x0000FBF4
		private static void EnsureTemporaryResourcesReleased(int colorTexture, int depthRenderbuffer, int fboHandle)
		{
			try
			{
				if (colorTexture != -1)
				{
					GL.DeleteTexture(colorTexture);
				}
				if (depthRenderbuffer != -1)
				{
					GL.Ext.DeleteRenderbuffers(1, ref depthRenderbuffer);
				}
				if (fboHandle != -1)
				{
					GL.DeleteFramebuffers(1, ref fboHandle);
				}
			}
			catch (Exception)
			{
			}
		}

		// Token: 0x06000201 RID: 513 RVA: 0x00011A68 File Offset: 0x0000FC68
		private void Draw()
		{
			GL.ClearColor(Color.Transparent);
			GL.Clear(ClearBufferMask.DepthBufferBit | ClearBufferMask.ColorBufferBit);
			if (MaterialPreviewRenderer._sphereVertices == null)
			{
				MaterialPreviewRenderer._sphereVertices = SphereGeometry.CalculateVertices(0.9f, 0.9f, 50, 50);
			}
			if (MaterialPreviewRenderer._sphereElements == null)
			{
				MaterialPreviewRenderer._sphereElements = SphereGeometry.CalculateElements(50, 50);
			}
			GL.Color4(Color.White);
			GL.Disable(EnableCap.Blend);
			this._scene.MaterialMapper.ApplyMaterial(null, this._material, true, true);
			GL.DepthMask(true);
			GL.MatrixMode(MatrixMode.Modelview);
			Matrix4 matrix = Matrix4.LookAt(0f, 0f, -2.5f, 0f, 0f, 0f, 0f, 1f, 0f);
			GL.LoadMatrix(ref matrix);
			Matrix4 matrix2 = Matrix4.CreatePerspectiveFieldOfView(0.7853982f, 1f, 0.01f, 100f);
			GL.MatrixMode(MatrixMode.Projection);
			GL.LoadMatrix(ref matrix2);
			GL.Enable(EnableCap.DepthTest);
			GL.ShadeModel(ShadingModel.Smooth);
			GL.Enable(EnableCap.Light0);
			GL.Light(LightName.Light0, LightParameter.Position, new float[]
			{
				0.5f,
				-0.6f,
				-0.8f
			});
			GL.Light(LightName.Light0, LightParameter.Diffuse, new float[]
			{
				1f,
				1f,
				1f,
				1f
			});
			GL.Light(LightName.Light0, LightParameter.Specular, new float[]
			{
				1f,
				1f,
				1f,
				1f
			});
			SphereGeometry.Draw(MaterialPreviewRenderer._sphereVertices, MaterialPreviewRenderer._sphereElements);
		}

		// Token: 0x06000202 RID: 514 RVA: 0x00011BEC File Offset: 0x0000FDEC
		private static uint RoundToNextPowerOfTwo(uint s)
		{
			return (uint)Math.Pow(2.0, Math.Ceiling(Math.Log(s, 2.0)));
		}

		// Token: 0x06000203 RID: 515 RVA: 0x00011C14 File Offset: 0x0000FE14
		private void OnPreviewAvailable()
		{
			MaterialPreviewRenderer.OnPreviewAvailableDelegate previewAvailable = this.PreviewAvailable;
			if (previewAvailable != null)
			{
				previewAvailable(this);
			}
		}

		// Token: 0x0400018E RID: 398
		private const float SphereRadius = 0.9f;

		// Token: 0x0400018F RID: 399
		private const int SphereSegments = 50;

		// Token: 0x04000190 RID: 400
		private readonly Scene _scene;

		// Token: 0x04000191 RID: 401
		private readonly Material _material;

		// Token: 0x04000192 RID: 402
		private readonly uint _width;

		// Token: 0x04000193 RID: 403
		private readonly uint _height;

		// Token: 0x04000195 RID: 405
		private MaterialPreviewRenderer.CompletionState _state;

		// Token: 0x04000196 RID: 406
		private Image _previewImage;

		// Token: 0x04000197 RID: 407
		private static SphereGeometry.Vertex[] _sphereVertices;

		// Token: 0x04000198 RID: 408
		private static ushort[] _sphereElements;

		// Token: 0x02000035 RID: 53
		public enum CompletionState
		{
			// Token: 0x0400019A RID: 410
			Pending,
			// Token: 0x0400019B RID: 411
			Failed,
			// Token: 0x0400019C RID: 412
			Done
		}

		// Token: 0x02000036 RID: 54
		// (Invoke) Token: 0x06000206 RID: 518
		public delegate void OnPreviewAvailableDelegate(MaterialPreviewRenderer me);
	}
}
