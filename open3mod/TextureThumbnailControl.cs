using System;
using System.Diagnostics;
using System.Drawing;
using System.Drawing.Imaging;
using System.IO;
using System.Linq;
using System.Windows.Forms;

namespace open3mod
{
	// Token: 0x0200006D RID: 109
	public sealed class TextureThumbnailControl : ThumbnailControlBase<TextureThumbnailControl>
	{
		// Token: 0x0600037C RID: 892 RVA: 0x0001D3E8 File Offset: 0x0001B5E8
		public TextureThumbnailControl(TextureInspectionView owner, Scene scene, string filePath) : base(owner, TextureThumbnailControl.GetBackgroundImage(), Path.GetFileName(filePath))
		{
			this._owner = owner;
			this._scene = scene;
			this._filePath = filePath;
			this.SetLoadingState();
			this.ContextMenuStrip = new ContextMenuStrip();
			ToolStripMenuItem toolStripMenuItem = new ToolStripMenuItem("Zoom", null, new EventHandler(this.OnContextMenuZoom));
			this.ContextMenuStrip.Items.Add(toolStripMenuItem);
			toolStripMenuItem.CheckOnClick = true;
			toolStripMenuItem.Checked = false;
			toolStripMenuItem = new ToolStripMenuItem("Show Transparency", null, new EventHandler(this.OnContextMenuToggleAlpha));
			this.ContextMenuStrip.Items.Add(toolStripMenuItem);
			toolStripMenuItem.CheckOnClick = true;
			toolStripMenuItem.Checked = true;
			this.ContextMenuStrip.Items.Add(new ToolStripSeparator());
			toolStripMenuItem = new ToolStripMenuItem("Mirror along X (U) axis", null, new EventHandler(this.OnContextMenuMirrorX));
			this.ContextMenuStrip.Items.Add(toolStripMenuItem);
			toolStripMenuItem.CheckOnClick = true;
			toolStripMenuItem.Checked = false;
			toolStripMenuItem.Enabled = true;
			toolStripMenuItem = new ToolStripMenuItem("Mirror along Y (V) axis", null, new EventHandler(this.OnContextMenuMirrorY));
			this.ContextMenuStrip.Items.Add(toolStripMenuItem);
			toolStripMenuItem.CheckOnClick = true;
			toolStripMenuItem.Checked = false;
			toolStripMenuItem.Enabled = true;
			this.ContextMenuStrip.Items.Add(new ToolStripSeparator());
			toolStripMenuItem = new ToolStripMenuItem("Details", null, new EventHandler(this.OnContextMenuDetails));
			this.ContextMenuStrip.Items.Add(toolStripMenuItem);
			toolStripMenuItem.Enabled = false;
			toolStripMenuItem = new ToolStripMenuItem("Export", null, new EventHandler(this.OnContextMenuExport));
			this.ContextMenuStrip.Items.Add(toolStripMenuItem);
			toolStripMenuItem.Enabled = false;
			this.ContextMenuStrip.Opened += base.OnContextMenuOpen;
			base.DoubleClick += this.OnContextMenuDetails;
		}

		// Token: 0x0600037D RID: 893 RVA: 0x0001D5D2 File Offset: 0x0001B7D2
		private void OnContextMenuZoom(object sender, EventArgs eventArgs)
		{
			this.SetZoom();
		}

		// Token: 0x0600037E RID: 894 RVA: 0x0001D5DA File Offset: 0x0001B7DA
		private void OnContextMenuDetails(object sender, EventArgs eventArgs)
		{
			if (this.GetState() == ThumbnailControlBase<TextureThumbnailControl>.State.Good)
			{
				this._owner.ShowDetails(this);
			}
		}

		// Token: 0x0600037F RID: 895 RVA: 0x0001D5F1 File Offset: 0x0001B7F1
		private void OnContextMenuMirrorX(object sender, EventArgs eventArgs)
		{
			if (this.GetState() != ThumbnailControlBase<TextureThumbnailControl>.State.Good)
			{
				return;
			}
			this._texture.Image.RotateFlip(RotateFlipType.RotateNoneFlipX);
			this._texture.ReleaseUpload();
			this._texture.Upload();
		}

		// Token: 0x06000380 RID: 896 RVA: 0x0001D624 File Offset: 0x0001B824
		private void OnContextMenuMirrorY(object sender, EventArgs eventArgs)
		{
			if (this.GetState() != ThumbnailControlBase<TextureThumbnailControl>.State.Good)
			{
				return;
			}
			this._texture.Image.RotateFlip(RotateFlipType.Rotate180FlipX);
			this._texture.ReleaseUpload();
			this._texture.Upload();
		}

		// Token: 0x06000381 RID: 897 RVA: 0x0001D664 File Offset: 0x0001B864
		private void OnContextMenuExport(object sender, EventArgs eventArgs)
		{
			Image image = this._texture.Image;
			TextureExporter textureExporter = new TextureExporter(this._texture);
			SaveFileDialog saveFileDialog = new SaveFileDialog();
			saveFileDialog.Title = "Specify file to export " + Path.GetFileName(this._texture.FileName) + " to";
			if (this._texture.FileName.Length > 0 && this._texture.FileName[0] == '*')
			{
				saveFileDialog.FileName = "EmbeddedTexture_" + this._texture.FileName.Substring(1) + ".png";
			}
			else
			{
				saveFileDialog.FileName = Path.GetFileName(this._texture.FileName);
			}
			string text = string.Join(";", (from s in textureExporter.GetExtensionList()
			select "*." + s).ToArray<string>());
			saveFileDialog.Filter = string.Concat(new string[]
			{
				"Image Files (",
				text,
				")|",
				text,
				"|All files (*.*)|*.*"
			});
			if (saveFileDialog.ShowDialog(base.FindForm()) == DialogResult.OK && !textureExporter.Export(saveFileDialog.FileName))
			{
				MessageBox.Show(base.FindForm(), "Failed to export to " + saveFileDialog.FileName);
			}
		}

		// Token: 0x06000382 RID: 898 RVA: 0x0001D7BE File Offset: 0x0001B9BE
		private void OnContextMenuToggleAlpha(object sender, EventArgs eventArgs)
		{
			this.SetPictureBoxImage();
		}

		// Token: 0x1700009E RID: 158
		// (get) Token: 0x06000383 RID: 899 RVA: 0x0001D7C6 File Offset: 0x0001B9C6
		public string FilePath
		{
			get
			{
				return this._filePath;
			}
		}

		// Token: 0x1700009F RID: 159
		// (get) Token: 0x06000384 RID: 900 RVA: 0x0001D7CE File Offset: 0x0001B9CE
		public Texture Texture
		{
			get
			{
				return this._texture;
			}
		}

		// Token: 0x06000385 RID: 901 RVA: 0x0001D8F0 File Offset: 0x0001BAF0
		public void SetTexture(Texture texture)
		{
			this._texture = texture;
			Image image = texture.Image ?? TextureThumbnailControl.GetLoadErrorImage();
			base.BeginInvoke(new MethodInvoker(delegate()
			{
				this._imageWithAlpha = image;
				if (this.GetState() == ThumbnailControlBase<TextureThumbnailControl>.State.Good)
				{
					this.SetPictureBoxImage();
					this.SetZoom();
					for (int i = 2; i < this.ContextMenuStrip.Items.Count; i++)
					{
						this.ContextMenuStrip.Items[i].Enabled = true;
					}
				}
				else
				{
					this.pictureBox.Image = this._imageWithAlpha;
					this.pictureBox.SizeMode = PictureBoxSizeMode.CenterImage;
					foreach (object obj in this.ContextMenuStrip.Items)
					{
						((ToolStripItem)obj).Enabled = false;
					}
				}
				this.Invalidate();
			}));
		}

		// Token: 0x06000386 RID: 902 RVA: 0x0001D93C File Offset: 0x0001BB3C
		private void SetPictureBoxImage()
		{
			if (this.GetState() != ThumbnailControlBase<TextureThumbnailControl>.State.Good)
			{
				return;
			}
			ToolStripMenuItem toolStripMenuItem = (ToolStripMenuItem)this.ContextMenuStrip.Items[0];
			if (!Image.IsAlphaPixelFormat(this._imageWithAlpha.PixelFormat))
			{
				toolStripMenuItem.Enabled = false;
				toolStripMenuItem.Checked = false;
				this.pictureBox.Image = this._imageWithAlpha;
				return;
			}
			if (toolStripMenuItem.Checked)
			{
				this.pictureBox.Image = this._imageWithAlpha;
				return;
			}
			if (this._imageWithoutAlpha == null)
			{
				Bitmap bitmap = new Bitmap(this._imageWithAlpha.Width, this._imageWithAlpha.Height, PixelFormat.Format24bppRgb);
				using (Graphics graphics = Graphics.FromImage(bitmap))
				{
					graphics.Clear(Color.White);
					graphics.DrawImage(this._imageWithAlpha, new Rectangle(0, 0, bitmap.Width, bitmap.Height));
				}
				this._imageWithoutAlpha = bitmap;
			}
			this.pictureBox.Image = this._imageWithoutAlpha;
		}

		// Token: 0x06000387 RID: 903 RVA: 0x0001DA44 File Offset: 0x0001BC44
		private void SetZoom()
		{
			if (this.GetState() != ThumbnailControlBase<TextureThumbnailControl>.State.Good)
			{
				return;
			}
			ToolStripMenuItem toolStripMenuItem = (ToolStripMenuItem)this.ContextMenuStrip.Items[1];
			if (this._imageWithAlpha.Width >= this.pictureBox.Width && this._imageWithAlpha.Height >= this.pictureBox.Height)
			{
				toolStripMenuItem.Enabled = false;
				toolStripMenuItem.Checked = false;
				this.pictureBox.SizeMode = PictureBoxSizeMode.Zoom;
				return;
			}
			if (this._imageWithAlpha.Width < this.pictureBox.Width && this._imageWithAlpha.Height < this.pictureBox.Height && !toolStripMenuItem.Checked)
			{
				this.pictureBox.SizeMode = PictureBoxSizeMode.CenterImage;
				return;
			}
			this.pictureBox.SizeMode = PictureBoxSizeMode.Zoom;
		}

		// Token: 0x06000388 RID: 904 RVA: 0x0001DC58 File Offset: 0x0001BE58
		public void ChangeTextureSource(string newFile)
		{
			if (this.Texture.FileName == newFile)
			{
				return;
			}
			this._newFileId = this._owner.Scene.TextureSet.Replace(this.Texture.FileName, newFile);
			base.BeginInvoke(new MethodInvoker(delegate()
			{
				this.SetLoadingState();
				this._owner.Scene.TextureSet.AddCallback(delegate(string id, Texture tex)
				{
					if (id == this._newFileId)
					{
						string old = this.texCaptionLabel.Text;
						this.BeginInvoke(new MethodInvoker(delegate()
						{
							this.texCaptionLabel.Text = Path.GetFileName(newFile);
							if (!this._replaced)
							{
								this.labelOldTexture.Text = "was " + old;
								this.texCaptionLabel.Top -= 4;
								this._replaced = true;
							}
						}));
						this.SetTexture(tex);
						return false;
					}
					return true;
				});
			}));
			base.Invalidate();
		}

		// Token: 0x06000389 RID: 905 RVA: 0x0001DCD7 File Offset: 0x0001BED7
		protected override ThumbnailControlBase<TextureThumbnailControl>.State GetState()
		{
			if (this.Texture == null)
			{
				return ThumbnailControlBase<TextureThumbnailControl>.State.Pending;
			}
			if (this.Texture.State != Texture.TextureState.LoadingFailed)
			{
				return ThumbnailControlBase<TextureThumbnailControl>.State.Good;
			}
			return ThumbnailControlBase<TextureThumbnailControl>.State.Failed;
		}

		// Token: 0x0600038A RID: 906 RVA: 0x0001DCF4 File Offset: 0x0001BEF4
		private void SetLoadingState()
		{
			this._texture = null;
			this.pictureBox.Image = TextureThumbnailControl.GetLoadingImage();
			this.pictureBox.SizeMode = PictureBoxSizeMode.CenterImage;
		}

		// Token: 0x0600038B RID: 907 RVA: 0x0001DD19 File Offset: 0x0001BF19
		public bool CanChangeTextureSource()
		{
			return this.Texture != null;
		}

		// Token: 0x0600038C RID: 908 RVA: 0x0001DD44 File Offset: 0x0001BF44
		protected override void OnDragDrop(DragEventArgs e)
		{
			try
			{
				Array array = (Array)e.Data.GetData(DataFormats.FileDrop, false);
				if (array != null)
				{
					string s = array.GetValue(0).ToString();
					if (Directory.Exists(s))
					{
						this._owner.MatchWithFolder(s);
					}
					else
					{
						base.BeginInvoke(new MethodInvoker(delegate()
						{
							this.ChangeTextureSource(s);
						}), new object[]
						{
							this
						});
					}
				}
			}
			catch (Exception ex)
			{
				Trace.WriteLine("Error in DragDrop function: " + ex.Message);
			}
		}

		// Token: 0x0600038D RID: 909 RVA: 0x0001DDF4 File Offset: 0x0001BFF4
		protected override void OnDragEnter(DragEventArgs e)
		{
			if (!this.CanChangeTextureSource())
			{
				e.Effect = DragDropEffects.None;
				return;
			}
			e.Effect = (e.Data.GetDataPresent(DataFormats.FileDrop) ? DragDropEffects.Copy : DragDropEffects.None);
		}

		// Token: 0x0600038E RID: 910 RVA: 0x0001DE22 File Offset: 0x0001C022
		public static Image GetLoadErrorImage()
		{
			if (TextureThumbnailControl._loadError != null)
			{
				return TextureThumbnailControl._loadError;
			}
			TextureThumbnailControl._loadError = ImageFromResource.Get("open3mod.Images.FailedToLoad.png");
			return TextureThumbnailControl._loadError;
		}

		// Token: 0x0600038F RID: 911 RVA: 0x0001DE45 File Offset: 0x0001C045
		private static Image GetBackgroundImage()
		{
			if (TextureThumbnailControl._background != null)
			{
				return TextureThumbnailControl._background;
			}
			TextureThumbnailControl._background = ImageFromResource.Get("open3mod.Images.TextureTransparentBackground.png");
			return TextureThumbnailControl._background;
		}

		// Token: 0x06000390 RID: 912 RVA: 0x0001DE68 File Offset: 0x0001C068
		private static Image GetLoadingImage()
		{
			if (TextureThumbnailControl._loadAnimImage != null)
			{
				return TextureThumbnailControl._loadAnimImage;
			}
			TextureThumbnailControl._loadAnimImage = ImageFromResource.Get("open3mod.Images.TextureLoading.gif");
			return TextureThumbnailControl._loadAnimImage;
		}

		// Token: 0x040002D1 RID: 721
		private new readonly TextureInspectionView _owner;

		// Token: 0x040002D2 RID: 722
		private readonly Scene _scene;

		// Token: 0x040002D3 RID: 723
		private readonly string _filePath;

		// Token: 0x040002D4 RID: 724
		private Texture _texture;

		// Token: 0x040002D5 RID: 725
		private bool _replaced;

		// Token: 0x040002D6 RID: 726
		private string _newFileId;

		// Token: 0x040002D7 RID: 727
		private static Image _loadError;

		// Token: 0x040002D8 RID: 728
		private static Image _background;

		// Token: 0x040002D9 RID: 729
		private static Image _loadAnimImage;

		// Token: 0x040002DA RID: 730
		private Image _imageWithAlpha;

		// Token: 0x040002DB RID: 731
		private Image _imageWithoutAlpha;
	}
}
