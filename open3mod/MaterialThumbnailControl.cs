using System;
using System.Drawing;
using System.Windows.Forms;
using Assimp;

namespace open3mod
{
	// Token: 0x02000039 RID: 57
	public class MaterialThumbnailControl : ThumbnailControlBase<MaterialThumbnailControl>
	{
		// Token: 0x0600021C RID: 540 RVA: 0x00012580 File Offset: 0x00010780
		public MaterialThumbnailControl(MaterialInspectionView owner, Scene scene, Material material) : base(owner, MaterialThumbnailControl.GetBackgroundImage(), material.HasName ? material.Name : "Unnamed Material")
		{
			this._owner = owner;
			this._scene = scene;
			this._material = material;
			this._superSample = true;
			this.UpdatePreview();
			this.SetLoadingState();
		}

		// Token: 0x17000064 RID: 100
		// (get) Token: 0x0600021D RID: 541 RVA: 0x000125E1 File Offset: 0x000107E1
		public Material Material
		{
			get
			{
				return this._material;
			}
		}

		// Token: 0x17000065 RID: 101
		// (get) Token: 0x0600021E RID: 542 RVA: 0x000125E9 File Offset: 0x000107E9
		// (set) Token: 0x0600021F RID: 543 RVA: 0x000125F1 File Offset: 0x000107F1
		public bool SuperSample
		{
			get
			{
				return this._superSample;
			}
			set
			{
				if (this._superSample == value)
				{
					return;
				}
				this._superSample = value;
				this.UpdatePreview();
			}
		}

		// Token: 0x06000220 RID: 544 RVA: 0x000126F0 File Offset: 0x000108F0
		public void UpdatePreview()
		{
			lock (this._lock)
			{
				this._wantUpdate = true;
				if (this._renderer != null)
				{
					return;
				}
				this._renderer = new MaterialPreviewRenderer(this._owner.Window, this._scene, this._material, (uint)(this.pictureBox.Width * (this.SuperSample ? 2 : 1)), (uint)(this.pictureBox.Height * (this.SuperSample ? 2 : 1)));
				this._wantUpdate = false;
			}
			this._renderer.PreviewAvailable += delegate(MaterialPreviewRenderer me)
			{
				MaterialPreviewRenderer renderer = this._renderer;
				lock (this._lock)
				{
					this._renderer = null;
					if (this._wantUpdate)
					{
						this.UpdatePreview();
					}
				}
				base.BeginInvoke(new MethodInvoker(delegate()
				{
					Image previewImage = renderer.PreviewImage;
					if (previewImage != null)
					{
						this.pictureBox.Image = previewImage;
						this.pictureBox.SizeMode = PictureBoxSizeMode.Zoom;
						return;
					}
					this.pictureBox.Image = MaterialThumbnailControl.GetLoadErrorImage();
					this.pictureBox.SizeMode = PictureBoxSizeMode.CenterImage;
				}));
			};
		}

		// Token: 0x06000221 RID: 545 RVA: 0x000127A4 File Offset: 0x000109A4
		protected override ThumbnailControlBase<MaterialThumbnailControl>.State GetState()
		{
			return ThumbnailControlBase<MaterialThumbnailControl>.State.Good;
		}

		// Token: 0x06000222 RID: 546 RVA: 0x000127A7 File Offset: 0x000109A7
		private void SetLoadingState()
		{
			this.pictureBox.Image = MaterialThumbnailControl.GetLoadingImage();
			this.pictureBox.SizeMode = PictureBoxSizeMode.CenterImage;
		}

		// Token: 0x06000223 RID: 547 RVA: 0x000127C5 File Offset: 0x000109C5
		public static Image GetLoadErrorImage()
		{
			if (MaterialThumbnailControl._loadError != null)
			{
				return MaterialThumbnailControl._loadError;
			}
			MaterialThumbnailControl._loadError = ImageFromResource.Get("open3mod.Images.FailedToLoad.png");
			return MaterialThumbnailControl._loadError;
		}

		// Token: 0x06000224 RID: 548 RVA: 0x000127E8 File Offset: 0x000109E8
		private static Image GetBackgroundImage()
		{
			if (MaterialThumbnailControl._background != null)
			{
				return MaterialThumbnailControl._background;
			}
			MaterialThumbnailControl._background = ImageFromResource.Get("open3mod.Images.TextureTransparentBackground.png");
			return MaterialThumbnailControl._background;
		}

		// Token: 0x06000225 RID: 549 RVA: 0x0001280B File Offset: 0x00010A0B
		private static Image GetLoadingImage()
		{
			if (MaterialThumbnailControl._loadAnimImage != null)
			{
				return MaterialThumbnailControl._loadAnimImage;
			}
			MaterialThumbnailControl._loadAnimImage = ImageFromResource.Get("open3mod.Images.TextureLoading.gif");
			return MaterialThumbnailControl._loadAnimImage;
		}

		// Token: 0x040001B2 RID: 434
		private new readonly MaterialInspectionView _owner;

		// Token: 0x040001B3 RID: 435
		private readonly Scene _scene;

		// Token: 0x040001B4 RID: 436
		private readonly Material _material;

		// Token: 0x040001B5 RID: 437
		private readonly object _lock = new object();

		// Token: 0x040001B6 RID: 438
		private static Image _loadError;

		// Token: 0x040001B7 RID: 439
		private static Image _background;

		// Token: 0x040001B8 RID: 440
		private static Image _loadAnimImage;

		// Token: 0x040001B9 RID: 441
		private MaterialPreviewRenderer _renderer;

		// Token: 0x040001BA RID: 442
		private bool _wantUpdate;

		// Token: 0x040001BB RID: 443
		private bool _superSample;
	}
}
