using System;
using System.Drawing;
using System.Drawing.Imaging;
using System.Windows.Forms;
using OpenTK;
using OpenTK.Graphics.OpenGL;

namespace open3mod
{
	// Token: 0x02000044 RID: 68
	public class Renderer : IDisposable
	{
		// Token: 0x14000005 RID: 5
		// (add) Token: 0x06000251 RID: 593 RVA: 0x00013AC0 File Offset: 0x00011CC0
		// (remove) Token: 0x06000252 RID: 594 RVA: 0x00013AF8 File Offset: 0x00011CF8
		public event Renderer.GlExtraDrawJobDelegate GlExtraDrawJob;

		// Token: 0x06000253 RID: 595 RVA: 0x00013B30 File Offset: 0x00011D30
		private void OnGlExtraDrawJob()
		{
			Renderer.GlExtraDrawJobDelegate glExtraDrawJob = this.GlExtraDrawJob;
			if (glExtraDrawJob != null)
			{
				glExtraDrawJob(this);
				this.GlExtraDrawJob = null;
			}
		}

		// Token: 0x17000068 RID: 104
		// (get) Token: 0x06000254 RID: 596 RVA: 0x00013B55 File Offset: 0x00011D55
		protected Color HudColor
		{
			get
			{
				return Color.FromArgb(100, 80, 80, 80);
			}
		}

		// Token: 0x17000069 RID: 105
		// (get) Token: 0x06000255 RID: 597 RVA: 0x00013B64 File Offset: 0x00011D64
		protected Color BorderColor
		{
			get
			{
				return Color.DimGray;
			}
		}

		// Token: 0x1700006A RID: 106
		// (get) Token: 0x06000256 RID: 598 RVA: 0x00013B6B File Offset: 0x00011D6B
		protected Color ActiveBorderColor
		{
			get
			{
				return Color.GreenYellow;
			}
		}

		// Token: 0x1700006B RID: 107
		// (get) Token: 0x06000257 RID: 599 RVA: 0x00013B72 File Offset: 0x00011D72
		protected Color BackgroundColor
		{
			get
			{
				return Color.FromArgb(255, 165, 166, 165);
			}
		}

		// Token: 0x1700006C RID: 108
		// (get) Token: 0x06000258 RID: 600 RVA: 0x00013B8D File Offset: 0x00011D8D
		protected Color ActiveViewColor
		{
			get
			{
				return Color.FromArgb(255, 175, 175, 175);
			}
		}

		// Token: 0x1700006D RID: 109
		// (get) Token: 0x06000259 RID: 601 RVA: 0x00013BA8 File Offset: 0x00011DA8
		protected float HudHoverTime
		{
			get
			{
				return 0.2f;
			}
		}

		// Token: 0x1700006E RID: 110
		// (get) Token: 0x0600025A RID: 602 RVA: 0x00013BAF File Offset: 0x00011DAF
		public GLControl GlControl
		{
			get
			{
				return this._window.GlControl;
			}
		}

		// Token: 0x1700006F RID: 111
		// (get) Token: 0x0600025B RID: 603 RVA: 0x00013BBC File Offset: 0x00011DBC
		public MainWindow Window
		{
			get
			{
				return this._window;
			}
		}

		// Token: 0x17000070 RID: 112
		// (get) Token: 0x0600025C RID: 604 RVA: 0x00013BC4 File Offset: 0x00011DC4
		public TextOverlay TextOverlay
		{
			get
			{
				return this._textOverlay;
			}
		}

		// Token: 0x17000071 RID: 113
		// (get) Token: 0x0600025D RID: 605 RVA: 0x00013BCC File Offset: 0x00011DCC
		public Size RenderResolution
		{
			get
			{
				return this.GlControl.ClientSize;
			}
		}

		// Token: 0x17000072 RID: 114
		// (get) Token: 0x0600025E RID: 606 RVA: 0x00013BD9 File Offset: 0x00011DD9
		public Matrix4 LightRotation
		{
			get
			{
				return this._lightRotation;
			}
		}

		// Token: 0x0600025F RID: 607 RVA: 0x00013BE1 File Offset: 0x00011DE1
		internal Renderer(MainWindow window)
		{
			this._window = window;
			this._textOverlay = new TextOverlay(this);
		}

		// Token: 0x06000260 RID: 608 RVA: 0x00013C1A File Offset: 0x00011E1A
		public void Update(double delta)
		{
			if (this._hoverFadeInTime > 0f)
			{
				this._hoverFadeInTime -= (float)delta;
				this._hudDirty = true;
			}
		}

		// Token: 0x06000261 RID: 609 RVA: 0x00013C40 File Offset: 0x00011E40
		public void Draw(Tab activeTab)
		{
			GL.DepthMask(true);
			GL.ClearColor(this.BackgroundColor);
			GL.Clear(ClearBufferMask.DepthBufferBit | ClearBufferMask.ColorBufferBit);
			Tab activeTab2 = this.Window.UiState.ActiveTab;
			if (activeTab2.ActiveScene != null)
			{
				Tab.ViewIndex viewIndex = Tab.ViewIndex.Index0;
				foreach (Viewport viewport in activeTab2.ActiveViews)
				{
					if (viewport == null || activeTab2.ActiveViewIndex == viewIndex)
					{
						viewIndex++;
					}
					else
					{
						Vector4 bounds = viewport.Bounds;
						ICameraController view = viewport.ActiveCameraControllerForView();
						this.DrawViewport(view, activeTab, (double)bounds.X, (double)bounds.Y, (double)bounds.Z, (double)bounds.W, activeTab2.ActiveViewIndex == viewIndex);
						viewIndex++;
					}
				}
				Viewport viewport2 = activeTab2.ActiveViews[(int)activeTab2.ActiveViewIndex];
				Vector4 bounds2 = viewport2.Bounds;
				this.DrawViewport(viewport2.ActiveCameraControllerForView(), activeTab, (double)bounds2.X, (double)bounds2.Y, (double)bounds2.Z, (double)bounds2.W, true);
				if (activeTab2.ActiveViewMode != Tab.ViewMode.Single)
				{
					this.SetFullViewport();
				}
				if (this.Window.UiState.ShowFps)
				{
					this.DrawFps();
				}
				if (!this._window.IsDraggingViewportSeparator)
				{
					if (!this._hudHidden)
					{
						this.DrawHud();
					}
				}
				else
				{
					this._textOverlay.WantRedrawNextFrame = true;
					this._hudHidden = true;
				}
			}
			else
			{
				this.SetFullViewport();
				if (activeTab.State == Tab.TabState.Failed)
				{
					this.DrawFailureSplash(activeTab.ErrorMessage);
				}
				else if (activeTab.State == Tab.TabState.Loading)
				{
					this.DrawLoadingSplash();
				}
				else
				{
					this.DrawNoSceneSplash();
				}
			}
			this._textOverlay.Draw();
			if (activeTab2.ActiveScene != null && activeTab2.ActiveViewMode != Tab.ViewMode.Single)
			{
				Tab.ViewIndex viewIndex2 = Tab.ViewIndex.Index0;
				foreach (Viewport viewport3 in activeTab2.ActiveViews)
				{
					if (viewport3 == null || activeTab2.ActiveViewIndex == viewIndex2)
					{
						viewIndex2++;
					}
					else
					{
						Vector4 bounds3 = viewport3.Bounds;
						this.DrawViewportPost((double)bounds3.X, (double)bounds3.Y, (double)bounds3.Z, (double)bounds3.W, false);
						viewIndex2++;
					}
				}
				Viewport viewport4 = activeTab2.ActiveViews[(int)activeTab2.ActiveViewIndex];
				Vector4 bounds4 = viewport4.Bounds;
				this.DrawViewportPost((double)bounds4.X, (double)bounds4.Y, (double)bounds4.Z, (double)bounds4.W, true);
			}
			this.OnGlExtraDrawJob();
		}

		// Token: 0x06000262 RID: 610 RVA: 0x00013EA8 File Offset: 0x000120A8
		private void DrawShadowedString(Graphics graphics, string s, Font font, RectangleF rect, Color main, Color shadow, StringFormat format)
		{
			using (SolidBrush solidBrush = new SolidBrush(main))
			{
				using (SolidBrush solidBrush2 = new SolidBrush(shadow))
				{
					for (int i = -1; i <= 1; i += 2)
					{
						for (int j = -1; j <= 1; j += 2)
						{
							RectangleF layoutRectangle = new RectangleF(rect.Left + (float)i, rect.Top + (float)j, rect.Width, rect.Height);
							graphics.DrawString(s, font, solidBrush2, layoutRectangle, format);
						}
					}
					graphics.DrawString(s, font, solidBrush, rect, format);
				}
			}
		}

		// Token: 0x06000263 RID: 611 RVA: 0x00013F54 File Offset: 0x00012154
		private void DrawHud()
		{
			Tab activeTab = this.Window.UiState.ActiveTab;
			if (activeTab.ActiveViews[(int)this._hoverViewIndex] == null)
			{
				return;
			}
			float x = this._hoverViewport.X;
			float y = this._hoverViewport.Y;
			float z = this._hoverViewport.Z;
			float w = this._hoverViewport.W;
			if (!this._hudDirty)
			{
				this._hudDirty = (x != this._lastActiveVp[0] || y != this._lastActiveVp[1] || z != this._lastActiveVp[2] || w != this._lastActiveVp[3]);
			}
			CameraMode cameraMode = activeTab.ActiveCameraControllerForView(this._hoverViewIndex).GetCameraMode();
			if (cameraMode != this._lastHoverViewCameraMode)
			{
				this._hudDirty = true;
			}
			bool flag = this._hoverViewIndex == activeTab.ActiveViewIndex;
			if (flag != this._hoverViewWasActive)
			{
				this._hudDirty = true;
			}
			this._hoverViewWasActive = flag;
			this._lastHoverViewCameraMode = cameraMode;
			this._lastActiveVp[0] = x;
			this._lastActiveVp[1] = y;
			this._lastActiveVp[2] = z;
			this._lastActiveVp[3] = w;
			if (!this._textOverlay.WantRedraw)
			{
				if (this._hudDirty)
				{
					this._textOverlay.WantRedrawNextFrame = true;
				}
				return;
			}
			this._hudDirty = false;
			this.LoadHudImages();
			Graphics drawableGraphicsContext = this._textOverlay.GetDrawableGraphicsContext();
			if (drawableGraphicsContext == null)
			{
				return;
			}
			int num = 3 + (int)((double)z * (double)this.RenderResolution.Width);
			int num2 = (int)((double)(1f - w) * (double)this.RenderResolution.Height);
			if (activeTab.ActiveViewMode != Tab.ViewMode.Single)
			{
				num -= 3;
				num2 += 3;
			}
			int width = this._hudImages[0, 0].Width;
			int height = this._hudImages[0, 0].Height;
			int num3 = width * this._hudImages.GetLength(0) + 4 * (this._hudImages.GetLength(0) - 1);
			if ((float)num3 > (z - x) * (float)this.RenderResolution.Width || 27f > (w - y) * (float)this.RenderResolution.Height)
			{
				return;
			}
			num -= num3;
			this._hoverRegion = new Rectangle(num, num2, num3 - 2, 27);
			if (this._hoverFadeInTime > 0f)
			{
				ColorMatrix colorMatrix = new ColorMatrix();
				ImageAttributes imageAttributes = new ImageAttributes();
				colorMatrix.Matrix33 = 1f - this._hoverFadeInTime / this.HudHoverTime;
				imageAttributes.SetColorMatrix(colorMatrix);
				drawableGraphicsContext.DrawImage(this._hudBar, this._hoverRegion, 0, 0, this._hudBar.Width, this._hudBar.Height, GraphicsUnit.Pixel, imageAttributes);
			}
			else
			{
				drawableGraphicsContext.DrawImage(this._hudBar, this._hoverRegion);
			}
			if (this._hoverViewIndex == activeTab.ActiveViewIndex)
			{
				StringFormat format = new StringFormat
				{
					LineAlignment = StringAlignment.Near,
					Alignment = StringAlignment.Near
				};
				RectangleF rect = new RectangleF(x * (float)this.RenderResolution.Width + 10f, (1f - w) * (float)this.RenderResolution.Height + 10f, (z - x) * (float)this.RenderResolution.Width, (w - y) * (float)this.RenderResolution.Height);
				this.DrawShadowedString(drawableGraphicsContext, "Press [R] to reset the view", this.Window.UiState.DefaultFont10, rect, Color.Black, Color.FromArgb(50, Color.White), format);
			}
			num += this._hudImages.GetLength(0) / 2;
			for (int i = 0; i < this._hudImages.GetLength(0); i++)
			{
				int num4 = num;
				int num5 = num2 + 4;
				int num6 = (int)((double)width * 2.0 / 3.0);
				int num7 = (int)((double)height * 2.0 / 3.0);
				if (this._processHudClick && this._mouseClickPos.X > num4 && this._mouseClickPos.X <= num4 + num6 && this._mouseClickPos.Y > num5 && this._mouseClickPos.Y <= num5 + num7)
				{
					this._processHudClick = false;
					activeTab.ChangeCameraModeForView(this._hoverViewIndex, (CameraMode)i);
				}
				int num8 = 0;
				bool flag2 = this._mousePos.X > num4 && this._mousePos.X <= num4 + num6 && this._mousePos.Y > num5 && this._mousePos.Y <= num5 + num7;
				if (activeTab.ActiveCameraControllerForView(this._hoverViewIndex).GetCameraMode() == (CameraMode)i)
				{
					num8 = 2;
				}
				else if (flag2)
				{
					num8 = 1;
				}
				if (flag2)
				{
					StringFormat format2 = new StringFormat
					{
						LineAlignment = StringAlignment.Near,
						Alignment = StringAlignment.Far
					};
					RectangleF rect2 = new RectangleF(x * (float)this.RenderResolution.Width, (1f - w) * (float)this.RenderResolution.Height + 35f, (z - x) * (float)this.RenderResolution.Width - 10f, (w - y) * (float)this.RenderResolution.Height - 2f);
					this.DrawShadowedString(drawableGraphicsContext, Renderer.DescTable[i], this.Window.UiState.DefaultFont10, rect2, Color.Black, Color.FromArgb(50, Color.White), format2);
				}
				Image image = this._hudImages[i, num8];
				drawableGraphicsContext.DrawImage(image, new Rectangle(num4, num5, num6, num7), 0, 0, image.Width, image.Height, GraphicsUnit.Pixel, null);
				num += width;
			}
		}

		// Token: 0x06000264 RID: 612 RVA: 0x00014520 File Offset: 0x00012720
		public void OnMouseMove(MouseEventArgs mouseEventArgs, Vector4 viewport, Tab.ViewIndex viewIndex)
		{
			this._mousePos = mouseEventArgs.Location;
			if (this._mousePos.X > this._hoverRegion.Left && this._mousePos.X <= this._hoverRegion.Right && this._mousePos.Y > this._hoverRegion.Top && this._mousePos.Y <= this._hoverRegion.Bottom)
			{
				this._hudDirty = true;
			}
			if (viewport == this._hoverViewport)
			{
				return;
			}
			this._hudDirty = true;
			this._hoverViewport = viewport;
			this._hoverViewIndex = viewIndex;
			this._hoverFadeInTime = this.HudHoverTime;
			this._hudHidden = false;
		}

		// Token: 0x06000265 RID: 613 RVA: 0x000145D8 File Offset: 0x000127D8
		public void OnMouseClick(MouseEventArgs mouseEventArgs, Vector4 viewport, Tab.ViewIndex viewIndex)
		{
			if (this._mousePos.X > this._hoverRegion.Left && this._mousePos.X <= this._hoverRegion.Right && this._mousePos.Y > this._hoverRegion.Top && this._mousePos.Y <= this._hoverRegion.Bottom)
			{
				this._mouseClickPos = mouseEventArgs.Location;
				this._processHudClick = true;
				this._hudDirty = true;
			}
		}

		// Token: 0x06000266 RID: 614 RVA: 0x00014660 File Offset: 0x00012860
		private void LoadHudImages()
		{
			if (this._hudImages != null)
			{
				return;
			}
			this._hudImages = new Image[Renderer.PrefixTable.Length, 3];
			for (int i = 0; i < this._hudImages.GetLength(0); i++)
			{
				for (int j = 0; j < this._hudImages.GetLength(1); j++)
				{
					this._hudImages[i, j] = ImageFromResource.Get(Renderer.PrefixTable[i] + Renderer.PostFixTable[j] + ".png");
				}
			}
			this._hudBar = ImageFromResource.Get("open3mod.Images.HUDBar.png");
		}

		// Token: 0x06000267 RID: 615 RVA: 0x000146F1 File Offset: 0x000128F1
		public void Dispose()
		{
			this.Dispose(true);
			GC.SuppressFinalize(this);
		}

		// Token: 0x06000268 RID: 616 RVA: 0x00014700 File Offset: 0x00012900
		protected virtual void Dispose(bool disposing)
		{
			this._textOverlay.Dispose();
		}

		// Token: 0x06000269 RID: 617 RVA: 0x0001470D File Offset: 0x0001290D
		public void Resize()
		{
			this._textOverlay.Resize();
		}

		// Token: 0x0600026A RID: 618 RVA: 0x0001471C File Offset: 0x0001291C
		private void DrawViewport(ICameraController view, Tab activeTab, double xs, double ys, double xe, double ye, bool active = false)
		{
			double num = (double)this.RenderResolution.Width;
			double num2 = (double)this.RenderResolution.Height;
			int num3 = (int)((xe - xs) * num);
			int num4 = (int)((ye - ys) * num2);
			GL.Viewport((int)(xs * num), (int)(ys * num2), (int)((xe - xs) * num), (int)((ye - ys) * num2));
			this.DrawViewportColorsPre(active);
			float aspect = (float)num3 / (float)num4;
			Matrix4 matrix = Matrix4.CreatePerspectiveFieldOfView(0.7853982f, aspect, 0.001f, 100f);
			GL.MatrixMode(MatrixMode.Projection);
			GL.LoadMatrix(ref matrix);
			if (activeTab.ActiveScene != null)
			{
				this.DrawScene(activeTab.ActiveScene, view);
			}
		}

		// Token: 0x0600026B RID: 619 RVA: 0x000147C8 File Offset: 0x000129C8
		private void DrawViewportPost(double xs, double ys, double xe, double ye, bool active = false)
		{
			double num = (double)this.RenderResolution.Width;
			double num2 = (double)this.RenderResolution.Height;
			int width = (int)((xe - xs) * num);
			int height = (int)((ye - ys) * num2);
			GL.Viewport((int)(xs * num), (int)(ys * num2), (int)((xe - xs) * num), (int)((ye - ys) * num2));
			this.DrawViewportColorsPost(active, width, height);
		}

		// Token: 0x0600026C RID: 620 RVA: 0x0001482C File Offset: 0x00012A2C
		private void SetFullViewport()
		{
			GL.Viewport(0, 0, this.RenderResolution.Width, this.RenderResolution.Height);
		}

		// Token: 0x0600026D RID: 621 RVA: 0x0001485C File Offset: 0x00012A5C
		private void DrawViewportColorsPre(bool active)
		{
			if (!active)
			{
				return;
			}
			GL.MatrixMode(MatrixMode.Modelview);
			GL.LoadIdentity();
			GL.MatrixMode(MatrixMode.Projection);
			GL.LoadIdentity();
			GL.Color4(this.ActiveViewColor);
			GL.Rect(-1, -1, 1, 1);
		}

		// Token: 0x0600026E RID: 622 RVA: 0x00014894 File Offset: 0x00012A94
		private void DrawViewportColorsPost(bool active, int width, int height)
		{
			GL.Hint(HintTarget.LineSmoothHint, HintMode.Nicest);
			double num = 1.0 / (double)width;
			double num2 = 1.0 / (double)height;
			GL.MatrixMode(MatrixMode.Modelview);
			GL.LoadIdentity();
			GL.MatrixMode(MatrixMode.Projection);
			GL.LoadIdentity();
			int num3 = active ? 4 : 3;
			GL.LineWidth((float)num3);
			GL.Color4(active ? this.ActiveBorderColor : this.BorderColor);
			double num4 = (double)num3 * 0.5 * num;
			double num5 = (double)num3 * 0.5 * num2;
			GL.Begin(BeginMode.LineStrip);
			GL.Vertex2(-1.0 + num4, -1.0 + num5);
			GL.Vertex2(1.0 - num4, -1.0 + num5);
			GL.Vertex2(1.0 - num4, 1.0 - num5);
			GL.Vertex2(-1.0 + num4, 1.0 - num5);
			GL.Vertex2(-1.0 + num4, -1.0 + num5);
			GL.End();
			GL.LineWidth(1f);
			GL.MatrixMode(MatrixMode.Modelview);
		}

		// Token: 0x0600026F RID: 623 RVA: 0x000149D7 File Offset: 0x00012BD7
		private void DrawScene(Scene scene, ICameraController view)
		{
			scene.Render(this.Window.UiState, view, this);
		}

		// Token: 0x06000270 RID: 624 RVA: 0x000149EC File Offset: 0x00012BEC
		private void DrawNoSceneSplash()
		{
			Graphics drawableGraphicsContext = this._textOverlay.GetDrawableGraphicsContext();
			if (drawableGraphicsContext == null)
			{
				return;
			}
			StringFormat stringFormat = new StringFormat();
			stringFormat.LineAlignment = StringAlignment.Center;
			stringFormat.Alignment = StringAlignment.Center;
			drawableGraphicsContext.DrawString("Drag file here", this.Window.UiState.DefaultFont16, new SolidBrush(Color.Black), new RectangleF(0f, 0f, (float)this.GlControl.Width, (float)this.GlControl.Height), stringFormat);
		}

		// Token: 0x06000271 RID: 625 RVA: 0x00014A6C File Offset: 0x00012C6C
		private void DrawLoadingSplash()
		{
			Graphics drawableGraphicsContext = this._textOverlay.GetDrawableGraphicsContext();
			if (drawableGraphicsContext == null)
			{
				return;
			}
			StringFormat format = new StringFormat
			{
				LineAlignment = StringAlignment.Center,
				Alignment = StringAlignment.Center
			};
			drawableGraphicsContext.DrawString("Loading ...", this.Window.UiState.DefaultFont16, new SolidBrush(Color.Black), new RectangleF(0f, 0f, (float)this.GlControl.Width, (float)this.GlControl.Height), format);
		}

		// Token: 0x06000272 RID: 626 RVA: 0x00014AEC File Offset: 0x00012CEC
		private void DrawFailureSplash(string message)
		{
			Graphics drawableGraphicsContext = this._textOverlay.GetDrawableGraphicsContext();
			if (drawableGraphicsContext == null)
			{
				return;
			}
			StringFormat format = new StringFormat
			{
				LineAlignment = StringAlignment.Center,
				Alignment = StringAlignment.Center
			};
			Image loadErrorImage = TextureThumbnailControl.GetLoadErrorImage();
			drawableGraphicsContext.DrawImage(loadErrorImage, this.GlControl.Width / 2 - loadErrorImage.Width / 2, this.GlControl.Height / 2 - loadErrorImage.Height - 30, loadErrorImage.Width, loadErrorImage.Height);
			drawableGraphicsContext.DrawString("Sorry, this scene failed to load.", this.Window.UiState.DefaultFont16, new SolidBrush(Color.Red), new RectangleF(0f, 0f, (float)this.GlControl.Width, (float)this.GlControl.Height), format);
			drawableGraphicsContext.DrawString("What the importer said went wrong: " + message, this.Window.UiState.DefaultFont12, new SolidBrush(Color.Black), new RectangleF(0f, 100f, (float)this.GlControl.Width, (float)this.GlControl.Height), format);
		}

		// Token: 0x06000273 RID: 627 RVA: 0x00014C08 File Offset: 0x00012E08
		private void DrawFps()
		{
			this._accTime += this.Window.Fps.LastFrameDelta;
			if (this._accTime < 0.3333 && !this._textOverlay.WantRedraw)
			{
				if (this._accTime >= 0.3333)
				{
					this._textOverlay.WantRedrawNextFrame = true;
				}
				return;
			}
			if (this._accTime >= 0.3333)
			{
				this._displayFps = this.Window.Fps.LastFps;
				this._accTime = 0.0;
			}
			Graphics drawableGraphicsContext = this._textOverlay.GetDrawableGraphicsContext();
			if (drawableGraphicsContext == null)
			{
				return;
			}
			drawableGraphicsContext.DrawString("FPS: " + this._displayFps.ToString("0.0"), this.Window.UiState.DefaultFont12, new SolidBrush(Color.Red), 5f, 5f);
		}

		// Token: 0x06000274 RID: 628 RVA: 0x00014CF8 File Offset: 0x00012EF8
		public void HandleLightRotationOnMouseMove(int mouseDeltaX, int mouseDeltaY, ref Matrix4 view)
		{
			this._lightRotation *= Matrix4.CreateFromAxisAngle(view.Column1.Xyz, (float)mouseDeltaX * 0.005f);
			this._lightRotation *= Matrix4.CreateFromAxisAngle(view.Column0.Xyz, (float)mouseDeltaY * 0.005f);
		}

		// Token: 0x040001D9 RID: 473
		private readonly MainWindow _window;

		// Token: 0x040001DA RID: 474
		private readonly TextOverlay _textOverlay;

		// Token: 0x040001DB RID: 475
		private Image[,] _hudImages;

		// Token: 0x040001DC RID: 476
		private bool _hudDirty = true;

		// Token: 0x040001DD RID: 477
		private double _accTime;

		// Token: 0x040001DE RID: 478
		private readonly float[] _lastActiveVp = new float[4];

		// Token: 0x040001DF RID: 479
		private Point _mousePos;

		// Token: 0x040001E0 RID: 480
		private Rectangle _hoverRegion;

		// Token: 0x040001E1 RID: 481
		private double _displayFps;

		// Token: 0x040001E2 RID: 482
		private Vector4 _hoverViewport;

		// Token: 0x040001E3 RID: 483
		private Point _mouseClickPos;

		// Token: 0x040001E4 RID: 484
		private bool _processHudClick;

		// Token: 0x040001E5 RID: 485
		private Tab.ViewIndex _hoverViewIndex;

		// Token: 0x040001E6 RID: 486
		private float _hoverFadeInTime;

		// Token: 0x040001E7 RID: 487
		private Matrix4 _lightRotation = Matrix4.Identity;

		// Token: 0x040001E9 RID: 489
		private bool _hoverViewWasActive;

		// Token: 0x040001EA RID: 490
		private static readonly string[] DescTable = new string[]
		{
			"Lock on X axis",
			"Lock on Y axis",
			"Lock on Z axis",
			"Orbit view",
			"First-person view - use WASD or arrows to move"
		};

		// Token: 0x040001EB RID: 491
		private static readonly string[] PrefixTable = new string[]
		{
			"open3mod.Images.HUD_X",
			"open3mod.Images.HUD_Y",
			"open3mod.Images.HUD_Z",
			"open3mod.Images.HUD_Orbit",
			"open3mod.Images.HUD_FPS"
		};

		// Token: 0x040001EC RID: 492
		private static readonly string[] PostFixTable = new string[]
		{
			"_Normal",
			"_Hover",
			"_Selected"
		};

		// Token: 0x040001ED RID: 493
		private bool _hudHidden;

		// Token: 0x040001EE RID: 494
		private CameraMode _lastHoverViewCameraMode;

		// Token: 0x040001EF RID: 495
		private Image _hudBar;

		// Token: 0x02000045 RID: 69
		// (Invoke) Token: 0x06000277 RID: 631
		public delegate void GlExtraDrawJobDelegate(object sender);
	}
}
