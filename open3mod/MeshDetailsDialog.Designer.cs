namespace open3mod
{
	// Token: 0x02000017 RID: 23
	public partial class MeshDetailsDialog : global::System.Windows.Forms.Form
	{
		// Token: 0x060000DA RID: 218 RVA: 0x000062B0 File Offset: 0x000044B0
		protected override void Dispose(bool disposing)
		{
			if (disposing && this.components != null)
			{
				this.components.Dispose();
			}
			base.Dispose(disposing);
		}

		// Token: 0x060000DB RID: 219 RVA: 0x000062D0 File Offset: 0x000044D0
		private void InitializeComponent()
		{
			this.checkedListBoxPerVertex = new global::System.Windows.Forms.CheckedListBox();
			this.label1 = new global::System.Windows.Forms.Label();
			this.labelVertexCount = new global::System.Windows.Forms.Label();
			this.labelFaceCount = new global::System.Windows.Forms.Label();
			this.label4 = new global::System.Windows.Forms.Label();
			this.checkedListBoxPerFace = new global::System.Windows.Forms.CheckedListBox();
			this.linkLabel1 = new global::System.Windows.Forms.LinkLabel();
			base.SuspendLayout();
			this.checkedListBoxPerVertex.Enabled = false;
			this.checkedListBoxPerVertex.FormattingEnabled = true;
			this.checkedListBoxPerVertex.Items.AddRange(new object[]
			{
				"Xyz Positions",
				"Normals",
				"Tangent Space Basis",
				"Texture Coordinates #1",
				"Texture Coordinates #2",
				"Texture Coordinates #3",
				"Texture Coordinates #4",
				"Vertex Colors #1",
				"Vertex Colors #2",
				"Vertex Colors #3",
				"Vertex Colors #4",
				"Bone Weights"
			});
			this.checkedListBoxPerVertex.Location = new global::System.Drawing.Point(15, 57);
			this.checkedListBoxPerVertex.Name = "checkedListBoxPerVertex";
			this.checkedListBoxPerVertex.RightToLeft = global::System.Windows.Forms.RightToLeft.No;
			this.checkedListBoxPerVertex.SelectionMode = global::System.Windows.Forms.SelectionMode.None;
			this.checkedListBoxPerVertex.Size = new global::System.Drawing.Size(157, 184);
			this.checkedListBoxPerVertex.TabIndex = 5;
			this.label1.AutoSize = true;
			this.label1.Location = new global::System.Drawing.Point(12, 41);
			this.label1.Name = "label1";
			this.label1.Size = new global::System.Drawing.Size(82, 13);
			this.label1.TabIndex = 6;
			this.label1.Text = "Per-vertex data:";
			this.labelVertexCount.AutoSize = true;
			this.labelVertexCount.Font = new global::System.Drawing.Font("Microsoft Sans Serif", 8.25f, global::System.Drawing.FontStyle.Bold, global::System.Drawing.GraphicsUnit.Point, 0);
			this.labelVertexCount.Location = new global::System.Drawing.Point(12, 13);
			this.labelVertexCount.Name = "labelVertexCount";
			this.labelVertexCount.Size = new global::System.Drawing.Size(121, 13);
			this.labelVertexCount.TabIndex = 7;
			this.labelVertexCount.Text = "<aNumber> Vertices";
			this.labelFaceCount.AutoSize = true;
			this.labelFaceCount.Font = new global::System.Drawing.Font("Microsoft Sans Serif", 8.25f, global::System.Drawing.FontStyle.Bold, global::System.Drawing.GraphicsUnit.Point, 0);
			this.labelFaceCount.Location = new global::System.Drawing.Point(12, 265);
			this.labelFaceCount.Name = "labelFaceCount";
			this.labelFaceCount.Size = new global::System.Drawing.Size(109, 13);
			this.labelFaceCount.TabIndex = 8;
			this.labelFaceCount.Text = "<aNumber> Faces";
			this.label4.AutoSize = true;
			this.label4.Location = new global::System.Drawing.Point(261, 41);
			this.label4.Name = "label4";
			this.label4.Size = new global::System.Drawing.Size(0, 13);
			this.label4.TabIndex = 9;
			this.checkedListBoxPerFace.Enabled = false;
			this.checkedListBoxPerFace.FormattingEnabled = true;
			this.checkedListBoxPerFace.Items.AddRange(new object[]
			{
				"Triangles",
				"Lines",
				"Points"
			});
			this.checkedListBoxPerFace.Location = new global::System.Drawing.Point(15, 291);
			this.checkedListBoxPerFace.Name = "checkedListBoxPerFace";
			this.checkedListBoxPerFace.Size = new global::System.Drawing.Size(160, 49);
			this.checkedListBoxPerFace.TabIndex = 10;
			this.linkLabel1.AutoSize = true;
			this.linkLabel1.Location = new global::System.Drawing.Point(92, 355);
			this.linkLabel1.Name = "linkLabel1";
			this.linkLabel1.Size = new global::System.Drawing.Size(83, 13);
			this.linkLabel1.TabIndex = 11;
			this.linkLabel1.TabStop = true;
			this.linkLabel1.Text = "Jump to material";
			this.linkLabel1.LinkClicked += new global::System.Windows.Forms.LinkLabelLinkClickedEventHandler(this.OnJumpToMaterial);
			base.AutoScaleDimensions = new global::System.Drawing.SizeF(6f, 13f);
			base.AutoScaleMode = global::System.Windows.Forms.AutoScaleMode.Font;
			base.ClientSize = new global::System.Drawing.Size(187, 387);
			base.Controls.Add(this.linkLabel1);
			base.Controls.Add(this.checkedListBoxPerFace);
			base.Controls.Add(this.label4);
			base.Controls.Add(this.labelFaceCount);
			base.Controls.Add(this.labelVertexCount);
			base.Controls.Add(this.label1);
			base.Controls.Add(this.checkedListBoxPerVertex);
			base.FormBorderStyle = global::System.Windows.Forms.FormBorderStyle.FixedToolWindow;
			base.Name = "MeshDetailsDialog";
			base.StartPosition = global::System.Windows.Forms.FormStartPosition.CenterScreen;
			this.Text = "<meshName> Details";
			base.TopMost = true;
			base.ResumeLayout(false);
			base.PerformLayout();
		}

		// Token: 0x04000058 RID: 88
		private global::System.ComponentModel.IContainer components;

		// Token: 0x04000059 RID: 89
		private global::System.Windows.Forms.CheckedListBox checkedListBoxPerVertex;

		// Token: 0x0400005A RID: 90
		private global::System.Windows.Forms.Label label1;

		// Token: 0x0400005B RID: 91
		private global::System.Windows.Forms.Label labelVertexCount;

		// Token: 0x0400005C RID: 92
		private global::System.Windows.Forms.Label labelFaceCount;

		// Token: 0x0400005D RID: 93
		private global::System.Windows.Forms.Label label4;

		// Token: 0x0400005E RID: 94
		private global::System.Windows.Forms.CheckedListBox checkedListBoxPerFace;

		// Token: 0x0400005F RID: 95
		private global::System.Windows.Forms.LinkLabel linkLabel1;
	}
}
