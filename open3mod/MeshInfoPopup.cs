using System;
using System.ComponentModel;
using System.Drawing;
using System.Windows.Forms;
using Assimp;

namespace open3mod
{
	// Token: 0x0200003A RID: 58
	public class MeshInfoPopup : UserControl
	{
		// Token: 0x06000227 RID: 551 RVA: 0x0001282E File Offset: 0x00010A2E
		public MeshInfoPopup()
		{
			this.InitializeComponent();
		}

		// Token: 0x17000066 RID: 102
		// (set) Token: 0x06000228 RID: 552 RVA: 0x0001283C File Offset: 0x00010A3C
		public HierarchyInspectionView Owner
		{
			set
			{
				this._owner = value;
			}
		}

		// Token: 0x06000229 RID: 553 RVA: 0x00012845 File Offset: 0x00010A45
		public void Populate(Mesh mesh)
		{
			this.labelInfo.Text = string.Format("{0} Vertices\n{1} Faces\n", mesh.VertexCount, mesh.FaceCount);
		}

		// Token: 0x0600022A RID: 554 RVA: 0x00012872 File Offset: 0x00010A72
		protected override void Dispose(bool disposing)
		{
			if (disposing && this.components != null)
			{
				this.components.Dispose();
			}
			base.Dispose(disposing);
		}

		// Token: 0x0600022B RID: 555 RVA: 0x00012894 File Offset: 0x00010A94
		private void InitializeComponent()
		{
			this.panel3 = new Panel();
			this.labelCaption = new Label();
			this.labelInfo = new Label();
			this.label1 = new Label();
			this.panel3.SuspendLayout();
			base.SuspendLayout();
			this.panel3.Anchor = (AnchorStyles.Top | AnchorStyles.Bottom | AnchorStyles.Left | AnchorStyles.Right);
			this.panel3.BackColor = Color.White;
			this.panel3.BorderStyle = BorderStyle.FixedSingle;
			this.panel3.Controls.Add(this.label1);
			this.panel3.Controls.Add(this.labelCaption);
			this.panel3.Controls.Add(this.labelInfo);
			this.panel3.Location = new Point(0, 3);
			this.panel3.Name = "panel3";
			this.panel3.Size = new Size(88, 84);
			this.panel3.TabIndex = 13;
			this.labelCaption.AutoSize = true;
			this.labelCaption.Font = new Font("Microsoft Sans Serif", 8.25f, FontStyle.Bold, GraphicsUnit.Point, 0);
			this.labelCaption.Location = new Point(8, 12);
			this.labelCaption.Name = "labelCaption";
			this.labelCaption.Size = new Size(37, 13);
			this.labelCaption.TabIndex = 12;
			this.labelCaption.Text = "Mesh";
			this.labelInfo.AutoSize = true;
			this.labelInfo.Location = new Point(8, 39);
			this.labelInfo.Name = "labelInfo";
			this.labelInfo.Size = new Size(66, 26);
			this.labelInfo.TabIndex = 0;
			this.labelInfo.Text = "134 Vertices\r\n197 Faces";
			this.label1.AutoSize = true;
			this.label1.Location = new Point(10, 65);
			this.label1.Name = "label1";
			this.label1.Size = new Size(0, 13);
			this.label1.TabIndex = 13;
			base.AutoScaleDimensions = new SizeF(6f, 13f);
			base.AutoScaleMode = AutoScaleMode.Font;
			base.Controls.Add(this.panel3);
			base.Name = "MeshInfoPopup";
			base.Size = new Size(88, 90);
			this.panel3.ResumeLayout(false);
			this.panel3.PerformLayout();
			base.ResumeLayout(false);
		}

		// Token: 0x040001BC RID: 444
		private HierarchyInspectionView _owner;

		// Token: 0x040001BD RID: 445
		private IContainer components;

		// Token: 0x040001BE RID: 446
		private Panel panel3;

		// Token: 0x040001BF RID: 447
		private Label labelInfo;

		// Token: 0x040001C0 RID: 448
		private Label labelCaption;

		// Token: 0x040001C1 RID: 449
		private Label label1;
	}
}
