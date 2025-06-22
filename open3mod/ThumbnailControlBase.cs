using System;
using System.ComponentModel;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Windows.Forms;

namespace open3mod
{
	// Token: 0x02000037 RID: 55
	public abstract class ThumbnailControlBase<TDeriving> : UserControl where TDeriving : ThumbnailControlBase<TDeriving>
	{
		// Token: 0x06000209 RID: 521 RVA: 0x00011CE8 File Offset: 0x0000FEE8
		protected ThumbnailControlBase(ThumbnailViewBase<TDeriving> owner, Image backgroundImage, string initialCaption)
		{
			this._owner = owner;
			this.InitializeComponent();
			this.labelOldTexture.Text = "";
			this.pictureBox.BackgroundImage = backgroundImage;
			this.texCaptionLabel.Text = initialCaption;
			foreach (object obj in base.Controls)
			{
				Control control = obj as Control;
				if (control != null)
				{
					control.Click += delegate(object sender, EventArgs args)
					{
						this.OnClick(new EventArgs());
					};
					control.MouseEnter += delegate(object sender, EventArgs args)
					{
						this.OnMouseEnter(new EventArgs());
					};
					control.MouseLeave += delegate(object sender, EventArgs args)
					{
						this.OnMouseLeave(new EventArgs());
					};
				}
			}
			base.MouseDown += delegate(object sender, MouseEventArgs args)
			{
				owner.SelectEntry((TDeriving)((object)this));
			};
			this.pictureBox.DoubleClick += delegate(object sender, EventArgs args)
			{
				TDeriving tderiving = (TDeriving)((object)this);
				tderiving.OnDoubleClick(args);
			};
			this.labelOldTexture.DoubleClick += delegate(object sender, EventArgs args)
			{
				TDeriving tderiving = (TDeriving)((object)this);
				tderiving.OnDoubleClick(args);
			};
			this.texCaptionLabel.DoubleClick += delegate(object sender, EventArgs args)
			{
				TDeriving tderiving = (TDeriving)((object)this);
				tderiving.OnDoubleClick(args);
			};
		}

		// Token: 0x0600020A RID: 522 RVA: 0x00011E74 File Offset: 0x00010074
		protected void OnContextMenuOpen(object sender, EventArgs e)
		{
			this._owner.SelectEntry((TDeriving)((object)this));
		}

		// Token: 0x0600020B RID: 523 RVA: 0x00011E88 File Offset: 0x00010088
		public void OnSetTooltips(ToolTip tips)
		{
			string caption = "Right-click for tools and options";
			tips.SetToolTip(this, caption);
			tips.SetToolTip(this.pictureBox, caption);
			tips.SetToolTip(this.labelOldTexture, caption);
			tips.SetToolTip(this.texCaptionLabel, caption);
		}

		// Token: 0x0600020C RID: 524
		protected abstract ThumbnailControlBase<TDeriving>.State GetState();

		// Token: 0x17000063 RID: 99
		// (get) Token: 0x0600020D RID: 525 RVA: 0x00011ECA File Offset: 0x000100CA
		// (set) Token: 0x0600020E RID: 526 RVA: 0x00011ED4 File Offset: 0x000100D4
		public bool IsSelected
		{
			get
			{
				return this._selected;
			}
			set
			{
				if (this._selected == value)
				{
					return;
				}
				this._selected = value;
				this.texCaptionLabel.ForeColor = (this._selected ? Color.White : Color.Black);
				this.labelOldTexture.ForeColor = (this._selected ? Color.White : Color.DarkGray);
				base.Invalidate();
			}
		}

		// Token: 0x0600020F RID: 527 RVA: 0x00011F38 File Offset: 0x00010138
		protected override void OnPaint(PaintEventArgs e)
		{
			base.OnPaint(e);
			ThumbnailControlBase<TDeriving>.State state = this.GetState();
			if (this._selected || this._mouseOverFadeTimer > 0 || this._mouseOverCounter > 0 || state != ThumbnailControlBase<TDeriving>.State.Good)
			{
				this.CreateGraphicsPathForSelection();
				Color color = ThumbnailControlBase<TDeriving>.SelectionColor;
				if (this._selected)
				{
					color = ThumbnailControlBase<TDeriving>.SelectionColor;
				}
				else if (state == ThumbnailControlBase<TDeriving>.State.Pending)
				{
					color = ThumbnailControlBase<TDeriving>.LoadingColor;
				}
				else if (state == ThumbnailControlBase<TDeriving>.State.Failed)
				{
					color = ThumbnailControlBase<TDeriving>.FailureColor;
				}
				if (!this._selected)
				{
					float num = 0.5f * ((this._mouseOverCounter > 0) ? 1f : ((float)this._mouseOverFadeTimer / 500f));
					if (num < 0f)
					{
						num = 0f;
					}
					if (state == ThumbnailControlBase<TDeriving>.State.Pending)
					{
						num += 0.4f;
					}
					else if (state == ThumbnailControlBase<TDeriving>.State.Failed)
					{
						num += 0.6f;
					}
					if (num > 1f)
					{
						num = 1f;
					}
					color = Color.FromArgb((int)((byte)(num * 255f)), color);
				}
				e.Graphics.FillPath(new SolidBrush(color), ThumbnailControlBase<TDeriving>._selectPath);
			}
		}

		// Token: 0x06000210 RID: 528 RVA: 0x0001202C File Offset: 0x0001022C
		private void CreateGraphicsPathForSelection()
		{
			if (ThumbnailControlBase<TDeriving>._selectPath != null)
			{
				return;
			}
			int width = base.Size.Width;
			int height = base.Size.Height;
			ThumbnailControlBase<TDeriving>._selectPath = RoundedRectangle.Create(1, 1, width - 2, height - 2, 7);
		}

		// Token: 0x06000211 RID: 529 RVA: 0x00012072 File Offset: 0x00010272
		protected override void OnMouseEnter(EventArgs e)
		{
			base.OnMouseEnter(e);
			this._mouseOverFadeTimer = 500;
			this._mouseOverCounter = 1;
			base.Invalidate();
		}

		// Token: 0x06000212 RID: 530 RVA: 0x000120EC File Offset: 0x000102EC
		protected override void OnMouseLeave(EventArgs e)
		{
			if (base.ClientRectangle.Contains(base.PointToClient(Control.MousePosition)))
			{
				return;
			}
			base.OnMouseLeave(e);
			this._mouseOverCounter = 0;
			this._mouseOverFadeTimer = 500;
			Timer t = new Timer
			{
				Interval = 30
			};
			t.Tick += delegate(object sender, EventArgs args)
			{
				this._mouseOverFadeTimer -= t.Interval;
				if (this._mouseOverFadeTimer < 0)
				{
					t.Stop();
				}
				this.Invalidate();
			};
			t.Start();
		}

		// Token: 0x06000213 RID: 531 RVA: 0x00012172 File Offset: 0x00010372
		protected override void Dispose(bool disposing)
		{
			if (disposing && this.components != null)
			{
				this.components.Dispose();
			}
			base.Dispose(disposing);
		}

		// Token: 0x06000214 RID: 532 RVA: 0x00012194 File Offset: 0x00010394
		private void InitializeComponent()
		{
			this.components = new Container();
			this.pictureBox = new PictureBox();
			this.texCaptionLabel = new Label();
			this.labelOldTexture = new Label();
			this.contextMenuStrip1 = new ContextMenuStrip(this.components);
			this.replaceToolStripMenuItem = new ToolStripMenuItem();
			this.replaceToolStripMenuItem1 = new ToolStripMenuItem();
			this.exportToolStripMenuItem = new ToolStripMenuItem();
			((ISupportInitialize)this.pictureBox).BeginInit();
			this.contextMenuStrip1.SuspendLayout();
			base.SuspendLayout();
			this.pictureBox.BorderStyle = BorderStyle.FixedSingle;
			this.pictureBox.Location = new Point(9, 8);
			this.pictureBox.Name = "pictureBox";
			this.pictureBox.Size = new Size(210, 210);
			this.pictureBox.SizeMode = PictureBoxSizeMode.CenterImage;
			this.pictureBox.TabIndex = 0;
			this.pictureBox.TabStop = false;
			this.texCaptionLabel.AutoSize = true;
			this.texCaptionLabel.BackColor = Color.Transparent;
			this.texCaptionLabel.Location = new Point(7, 223);
			this.texCaptionLabel.Name = "texCaptionLabel";
			this.texCaptionLabel.Size = new Size(99, 13);
			this.texCaptionLabel.TabIndex = 1;
			this.texCaptionLabel.Text = "ATextureName.png";
			this.labelOldTexture.AutoSize = true;
			this.labelOldTexture.BackColor = Color.Transparent;
			this.labelOldTexture.Font = new Font("Microsoft Sans Serif", 6.25f, FontStyle.Regular, GraphicsUnit.Point, 0);
			this.labelOldTexture.ForeColor = SystemColors.ButtonShadow;
			this.labelOldTexture.Location = new Point(9, 230);
			this.labelOldTexture.Name = "labelOldTexture";
			this.labelOldTexture.Size = new Size(29, 12);
			this.labelOldTexture.TabIndex = 2;
			this.labelOldTexture.Text = "label1";
			this.contextMenuStrip1.Items.AddRange(new ToolStripItem[]
			{
				this.replaceToolStripMenuItem,
				this.replaceToolStripMenuItem1,
				this.exportToolStripMenuItem
			});
			this.contextMenuStrip1.Name = "contextMenuStrip1";
			this.contextMenuStrip1.Size = new Size(116, 70);
			this.replaceToolStripMenuItem.Enabled = false;
			this.replaceToolStripMenuItem.Name = "replaceToolStripMenuItem";
			this.replaceToolStripMenuItem.Size = new Size(115, 22);
			this.replaceToolStripMenuItem.Text = "Open";
			this.replaceToolStripMenuItem.Click += this.OnContextMenuOpen;
			this.replaceToolStripMenuItem1.Name = "replaceToolStripMenuItem1";
			this.replaceToolStripMenuItem1.Size = new Size(115, 22);
			this.replaceToolStripMenuItem1.Text = "Replace";
			this.exportToolStripMenuItem.Enabled = false;
			this.exportToolStripMenuItem.Name = "exportToolStripMenuItem";
			this.exportToolStripMenuItem.Size = new Size(115, 22);
			this.exportToolStripMenuItem.Text = "Export";
			this.AllowDrop = true;
			base.AutoScaleDimensions = new SizeF(6f, 13f);
			base.AutoScaleMode = AutoScaleMode.Font;
			base.Controls.Add(this.labelOldTexture);
			base.Controls.Add(this.texCaptionLabel);
			base.Controls.Add(this.pictureBox);
			this.DoubleBuffered = true;
			base.Name = "ThumbnailControlBase";
			base.Size = new Size(228, 245);
			((ISupportInitialize)this.pictureBox).EndInit();
			this.contextMenuStrip1.ResumeLayout(false);
			base.ResumeLayout(false);
			base.PerformLayout();
		}

		// Token: 0x0400019D RID: 413
		private const int FadeTime = 500;

		// Token: 0x0400019E RID: 414
		protected readonly ThumbnailViewBase<TDeriving> _owner;

		// Token: 0x0400019F RID: 415
		private bool _selected;

		// Token: 0x040001A0 RID: 416
		private static readonly Color SelectionColor = Color.CornflowerBlue;

		// Token: 0x040001A1 RID: 417
		private static readonly Color LoadingColor = Color.Chartreuse;

		// Token: 0x040001A2 RID: 418
		private static readonly Color FailureColor = Color.Red;

		// Token: 0x040001A3 RID: 419
		private static GraphicsPath _selectPath;

		// Token: 0x040001A4 RID: 420
		private int _mouseOverCounter;

		// Token: 0x040001A5 RID: 421
		private int _mouseOverFadeTimer;

		// Token: 0x040001A6 RID: 422
		private IContainer components;

		// Token: 0x040001A7 RID: 423
		protected PictureBox pictureBox;

		// Token: 0x040001A8 RID: 424
		protected Label texCaptionLabel;

		// Token: 0x040001A9 RID: 425
		protected Label labelOldTexture;

		// Token: 0x040001AA RID: 426
		protected ContextMenuStrip contextMenuStrip1;

		// Token: 0x040001AB RID: 427
		protected ToolStripMenuItem replaceToolStripMenuItem;

		// Token: 0x040001AC RID: 428
		protected ToolStripMenuItem replaceToolStripMenuItem1;

		// Token: 0x040001AD RID: 429
		protected ToolStripMenuItem exportToolStripMenuItem;

		// Token: 0x02000038 RID: 56
		protected enum State
		{
			// Token: 0x040001AF RID: 431
			Failed,
			// Token: 0x040001B0 RID: 432
			Pending,
			// Token: 0x040001B1 RID: 433
			Good
		}
	}
}
