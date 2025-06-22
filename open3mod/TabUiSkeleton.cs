using System;
using System.ComponentModel;
using System.Drawing;
using System.Windows.Forms;
using CoreSettings;
using OpenTK;

namespace open3mod
{
	// Token: 0x0200005D RID: 93
	public class TabUiSkeleton : UserControl
	{
		// Token: 0x0600031D RID: 797 RVA: 0x0001B23C File Offset: 0x0001943C
		public TabUiSkeleton()
		{
			this.InitializeComponent();
			this.splitContainer.SplitterDistance = this.splitContainer.Width - 440;
		}

		// Token: 0x0600031E RID: 798 RVA: 0x0001B266 File Offset: 0x00019466
		public SplitContainer GetSplitter()
		{
			return this.splitContainer;
		}

		// Token: 0x0600031F RID: 799 RVA: 0x0001B26E File Offset: 0x0001946E
		public InspectionView GetInspector()
		{
			return this.inspectionView1;
		}

		// Token: 0x06000320 RID: 800 RVA: 0x0001B278 File Offset: 0x00019478
		public void InjectGlControl(GLControl gl)
		{
			Control control = this.splitContainer.Controls[0];
			control.Controls.Add(gl);
			gl.Left = control.Left;
			gl.Top = control.Top;
			gl.Width = control.Width;
			gl.Height = control.Height;
		}

		// Token: 0x06000321 RID: 801 RVA: 0x0001B2D3 File Offset: 0x000194D3
		private void OnLoad(object sender, EventArgs e)
		{
		}

		// Token: 0x06000322 RID: 802 RVA: 0x0001B2D5 File Offset: 0x000194D5
		private void OnSplitterMove(object sender, SplitterEventArgs e)
		{
		}

		// Token: 0x06000323 RID: 803 RVA: 0x0001B2D7 File Offset: 0x000194D7
		protected override void Dispose(bool disposing)
		{
			if (disposing && this.components != null)
			{
				this.components.Dispose();
			}
			base.Dispose(disposing);
		}

		// Token: 0x06000324 RID: 804 RVA: 0x0001B2F8 File Offset: 0x000194F8
		private void InitializeComponent()
		{
			this.splitContainer = new SplitContainer();
			this.inspectionView1 = new InspectionView();
			this.splitContainer.Panel2.SuspendLayout();
			this.splitContainer.SuspendLayout();
			base.SuspendLayout();
			this.splitContainer.Anchor = (AnchorStyles.Top | AnchorStyles.Bottom | AnchorStyles.Left | AnchorStyles.Right);
			this.splitContainer.DataBindings.Add(new Binding("SplitterDistance", CoreSettings.Default, "InspectorSplitterPos", true, DataSourceUpdateMode.OnPropertyChanged));
			this.splitContainer.FixedPanel = FixedPanel.Panel2;
			this.splitContainer.Location = new Point(0, 0);
			this.splitContainer.Name = "splitContainer";
			this.splitContainer.Panel1MinSize = 10;
			this.splitContainer.Panel2.Controls.Add(this.inspectionView1);
			this.splitContainer.Panel2MinSize = 10;
			this.splitContainer.SplitterDistance = 20;
			this.splitContainer.Size = new Size(971, 650);
			this.splitContainer.SplitterWidth = 3;
			this.splitContainer.TabIndex = 0;
			this.splitContainer.SplitterMoved += this.OnSplitterMove;
			this.inspectionView1.Dock = DockStyle.Fill;
			this.inspectionView1.Enabled = false;
			this.inspectionView1.Location = new Point(0, 0);
			this.inspectionView1.Margin = new Padding(0);
			this.inspectionView1.Name = "inspectionView1";
			this.inspectionView1.Size = new Size(710, 650);
			this.inspectionView1.TabIndex = 0;
			base.AutoScaleDimensions = new SizeF(6f, 13f);
			base.AutoScaleMode = AutoScaleMode.Font;
			base.Controls.Add(this.splitContainer);
			base.Margin = new Padding(0);
			this.MinimumSize = new Size(400, 0);
			base.Name = "TabUiSkeleton";
			base.Size = new Size(971, 650);
			this.splitContainer.Panel2.ResumeLayout(false);
			this.splitContainer.ResumeLayout(false);
			base.ResumeLayout(false);
		}

		// Token: 0x0400029F RID: 671
		private IContainer components;

		// Token: 0x040002A0 RID: 672
		private SplitContainer splitContainer;

		// Token: 0x040002A1 RID: 673
		private InspectionView inspectionView1;
	}
}
