using System;
using System.ComponentModel;
using System.Drawing;
using System.Windows.Forms;

namespace open3mod
{
	// Token: 0x02000025 RID: 37
	public class InspectionView : UserControl
	{
		// Token: 0x17000050 RID: 80
		// (get) Token: 0x06000151 RID: 337 RVA: 0x0000BAF9 File Offset: 0x00009CF9
		// (set) Token: 0x06000152 RID: 338 RVA: 0x0000BB01 File Offset: 0x00009D01
		public Scene Scene { get; private set; }

		// Token: 0x17000051 RID: 81
		// (get) Token: 0x06000153 RID: 339 RVA: 0x0000BB0A File Offset: 0x00009D0A
		// (set) Token: 0x06000154 RID: 340 RVA: 0x0000BB12 File Offset: 0x00009D12
		public HierarchyInspectionView Hierarchy { get; private set; }

		// Token: 0x17000052 RID: 82
		// (get) Token: 0x06000155 RID: 341 RVA: 0x0000BB1B File Offset: 0x00009D1B
		// (set) Token: 0x06000156 RID: 342 RVA: 0x0000BB23 File Offset: 0x00009D23
		public TextureInspectionView Textures { get; private set; }

		// Token: 0x17000053 RID: 83
		// (get) Token: 0x06000157 RID: 343 RVA: 0x0000BB2C File Offset: 0x00009D2C
		// (set) Token: 0x06000158 RID: 344 RVA: 0x0000BB34 File Offset: 0x00009D34
		public MaterialInspectionView Materials { get; private set; }

		// Token: 0x17000054 RID: 84
		// (get) Token: 0x06000159 RID: 345 RVA: 0x0000BB3D File Offset: 0x00009D3D
		// (set) Token: 0x0600015A RID: 346 RVA: 0x0000BB45 File Offset: 0x00009D45
		public AnimationInspectionView Animations { get; private set; }

		// Token: 0x0600015B RID: 347 RVA: 0x0000BB4E File Offset: 0x00009D4E
		public InspectionView()
		{
			this.InitializeComponent();
			base.Enabled = false;
		}

		// Token: 0x0600015C RID: 348 RVA: 0x0000BB63 File Offset: 0x00009D63
		public void OpenMaterialsTabAndScrollTo(MaterialThumbnailControl thumb)
		{
			this.tabPageMaterials.ScrollControlIntoView(thumb);
			this.tabControlInfoViewPicker.SelectedTab = this.tabPageMaterials;
			this.tabPageMaterials.Focus();
		}

		// Token: 0x0600015D RID: 349 RVA: 0x0000BB90 File Offset: 0x00009D90
		public void SetSceneSource(Scene scene)
		{
			if (this.Scene == scene)
			{
				return;
			}
			this.Clear();
			this.Scene = scene;
			if (scene == null)
			{
				base.Enabled = false;
				return;
			}
			base.Enabled = true;
			this.Hierarchy = new HierarchyInspectionView(this.Scene, this.tabPageTree);
			this.Textures = new TextureInspectionView(this.Scene, this.textureFlowPanel);
			if (this.Textures.Empty)
			{
				this.tabControlInfoViewPicker.TabPages.Remove(this.tabPageTextures);
			}
			this.Animations = new AnimationInspectionView(this.Scene, this.tabPageAnimations);
			if (this.Animations.Empty)
			{
				this.tabControlInfoViewPicker.TabPages.Remove(this.tabPageAnimations);
			}
			this.Materials = new MaterialInspectionView(this.Scene, base.ParentForm as MainWindow, this.materialFlowPanel);
		}

		// Token: 0x0600015E RID: 350 RVA: 0x0000BC73 File Offset: 0x00009E73
		private void Clear()
		{
		}

		// Token: 0x0600015F RID: 351 RVA: 0x0000BC75 File Offset: 0x00009E75
		private void tabPageMaterials_MouseEnter(object sender, EventArgs e)
		{
			this.tabPageMaterials.Focus();
		}

		// Token: 0x06000160 RID: 352 RVA: 0x0000BC83 File Offset: 0x00009E83
		private void tabPageTextures_MouseEnter(object sender, EventArgs e)
		{
			this.tabPageTextures.Focus();
		}

		// Token: 0x06000161 RID: 353 RVA: 0x0000BC91 File Offset: 0x00009E91
		protected override void Dispose(bool disposing)
		{
			if (disposing && this.components != null)
			{
				this.components.Dispose();
			}
			base.Dispose(disposing);
		}

		// Token: 0x06000162 RID: 354 RVA: 0x0000BCB0 File Offset: 0x00009EB0
		private void InitializeComponent()
		{
			this.tabControlInfoViewPicker = new TabControl();
			this.tabPageTree = new TabPage();
			this.tabPageTextures = new TabPage();
			this.textureFlowPanel = new FlowLayoutPanel();
			this.tabPageMaterials = new TabPage();
			this.materialFlowPanel = new FlowLayoutPanel();
			this.tabPageAnimations = new TabPage();
			this.openFileDialog1 = new OpenFileDialog();
			this.tabControlInfoViewPicker.SuspendLayout();
			this.tabPageTextures.SuspendLayout();
			this.tabPageMaterials.SuspendLayout();
			base.SuspendLayout();
			this.tabControlInfoViewPicker.Controls.Add(this.tabPageTree);
			this.tabControlInfoViewPicker.Controls.Add(this.tabPageTextures);
			this.tabControlInfoViewPicker.Controls.Add(this.tabPageMaterials);
			this.tabControlInfoViewPicker.Controls.Add(this.tabPageAnimations);
			this.tabControlInfoViewPicker.Dock = DockStyle.Fill;
			this.tabControlInfoViewPicker.HotTrack = true;
			this.tabControlInfoViewPicker.Location = new Point(0, 0);
			this.tabControlInfoViewPicker.Margin = new Padding(0);
			this.tabControlInfoViewPicker.Multiline = true;
			this.tabControlInfoViewPicker.Name = "tabControlInfoViewPicker";
			this.tabControlInfoViewPicker.SelectedIndex = 0;
			this.tabControlInfoViewPicker.Size = new Size(357, 694);
			this.tabControlInfoViewPicker.TabIndex = 2;
			this.tabPageTree.BackColor = Color.Silver;
			this.tabPageTree.Location = new Point(4, 22);
			this.tabPageTree.Name = "tabPageTree";
			this.tabPageTree.Padding = new Padding(3);
			this.tabPageTree.Size = new Size(349, 668);
			this.tabPageTree.TabIndex = 0;
			this.tabPageTree.Text = "Tree";
			this.tabPageTextures.AutoScroll = true;
			this.tabPageTextures.Controls.Add(this.textureFlowPanel);
			this.tabPageTextures.Location = new Point(4, 22);
			this.tabPageTextures.Name = "tabPageTextures";
			this.tabPageTextures.Padding = new Padding(3);
			this.tabPageTextures.Size = new Size(349, 668);
			this.tabPageTextures.TabIndex = 1;
			this.tabPageTextures.Text = "Textures";
			this.tabPageTextures.UseVisualStyleBackColor = true;
			this.tabPageTextures.MouseEnter += this.tabPageTextures_MouseEnter;
			this.textureFlowPanel.AllowDrop = true;
			this.textureFlowPanel.AutoScroll = true;
			this.textureFlowPanel.AutoSize = true;
			this.textureFlowPanel.AutoSizeMode = AutoSizeMode.GrowAndShrink;
			this.textureFlowPanel.BackColor = Color.Transparent;
			this.textureFlowPanel.Dock = DockStyle.Top;
			this.textureFlowPanel.Location = new Point(3, 3);
			this.textureFlowPanel.Name = "textureFlowPanel";
			this.textureFlowPanel.Size = new Size(343, 0);
			this.textureFlowPanel.TabIndex = 0;
			this.textureFlowPanel.TabStop = true;
			this.tabPageMaterials.AutoScroll = true;
			this.tabPageMaterials.Controls.Add(this.materialFlowPanel);
			this.tabPageMaterials.Location = new Point(4, 22);
			this.tabPageMaterials.Name = "tabPageMaterials";
			this.tabPageMaterials.Padding = new Padding(3);
			this.tabPageMaterials.Size = new Size(349, 668);
			this.tabPageMaterials.TabIndex = 3;
			this.tabPageMaterials.Text = "Materials";
			this.tabPageMaterials.UseVisualStyleBackColor = true;
			this.tabPageMaterials.MouseEnter += this.tabPageMaterials_MouseEnter;
			this.materialFlowPanel.AutoScroll = true;
			this.materialFlowPanel.AutoSize = true;
			this.materialFlowPanel.AutoSizeMode = AutoSizeMode.GrowAndShrink;
			this.materialFlowPanel.BackColor = Color.Transparent;
			this.materialFlowPanel.Dock = DockStyle.Top;
			this.materialFlowPanel.Location = new Point(3, 3);
			this.materialFlowPanel.Name = "materialFlowPanel";
			this.materialFlowPanel.Size = new Size(343, 0);
			this.materialFlowPanel.TabIndex = 1;
			this.tabPageAnimations.Location = new Point(4, 22);
			this.tabPageAnimations.Name = "tabPageAnimations";
			this.tabPageAnimations.Padding = new Padding(3);
			this.tabPageAnimations.Size = new Size(349, 668);
			this.tabPageAnimations.TabIndex = 2;
			this.tabPageAnimations.Text = "Animations";
			this.tabPageAnimations.UseVisualStyleBackColor = true;
			this.openFileDialog1.FileName = "openFileDialog1";
			base.AutoScaleDimensions = new SizeF(6f, 13f);
			base.AutoScaleMode = AutoScaleMode.Font;
			base.Controls.Add(this.tabControlInfoViewPicker);
			base.Margin = new Padding(0);
			base.Name = "InspectionView";
			base.Size = new Size(357, 694);
			this.tabControlInfoViewPicker.ResumeLayout(false);
			this.tabPageTextures.ResumeLayout(false);
			this.tabPageTextures.PerformLayout();
			this.tabPageMaterials.ResumeLayout(false);
			this.tabPageMaterials.PerformLayout();
			base.ResumeLayout(false);
		}

		// Token: 0x040000EC RID: 236
		private IContainer components;

		// Token: 0x040000ED RID: 237
		private TabControl tabControlInfoViewPicker;

		// Token: 0x040000EE RID: 238
		private TabPage tabPageTextures;

		// Token: 0x040000EF RID: 239
		private TabPage tabPageAnimations;

		// Token: 0x040000F0 RID: 240
		private TabPage tabPageMaterials;

		// Token: 0x040000F1 RID: 241
		private FlowLayoutPanel textureFlowPanel;

		// Token: 0x040000F2 RID: 242
		private OpenFileDialog openFileDialog1;

		// Token: 0x040000F3 RID: 243
		private FlowLayoutPanel materialFlowPanel;

		// Token: 0x040000F4 RID: 244
		private TabPage tabPageTree;
	}
}
