namespace open3mod
{
	// Token: 0x02000063 RID: 99
	public partial class TextureDetailsDialog : global::System.Windows.Forms.Form
	{
		// Token: 0x0600034D RID: 845 RVA: 0x0001C293 File Offset: 0x0001A493
		protected override void Dispose(bool disposing)
		{
			if (disposing && this.components != null)
			{
				this.components.Dispose();
			}
			base.Dispose(disposing);
		}

		// Token: 0x0600034E RID: 846 RVA: 0x0001C2B4 File Offset: 0x0001A4B4
		private void InitializeComponent()
		{
			this.pictureBox1 = new global::System.Windows.Forms.PictureBox();
			this.labelInfo = new global::System.Windows.Forms.Label();
			this.checkBoxHasAlpha = new global::System.Windows.Forms.CheckBox();
			((global::System.ComponentModel.ISupportInitialize)this.pictureBox1).BeginInit();
			base.SuspendLayout();
			this.pictureBox1.Anchor = (global::System.Windows.Forms.AnchorStyles.Top | global::System.Windows.Forms.AnchorStyles.Bottom | global::System.Windows.Forms.AnchorStyles.Left | global::System.Windows.Forms.AnchorStyles.Right);
			this.pictureBox1.BorderStyle = global::System.Windows.Forms.BorderStyle.FixedSingle;
			this.pictureBox1.Location = new global::System.Drawing.Point(12, 41);
			this.pictureBox1.Name = "pictureBox1";
			this.pictureBox1.Size = new global::System.Drawing.Size(665, 577);
			this.pictureBox1.TabIndex = 0;
			this.pictureBox1.TabStop = false;
			this.labelInfo.AutoSize = true;
			this.labelInfo.Location = new global::System.Drawing.Point(12, 17);
			this.labelInfo.Name = "labelInfo";
			this.labelInfo.Size = new global::System.Drawing.Size(101, 13);
			this.labelInfo.TabIndex = 3;
			this.labelInfo.Text = "Size: <W> x <H> px";
			this.checkBoxHasAlpha.AutoCheck = false;
			this.checkBoxHasAlpha.AutoSize = true;
			this.checkBoxHasAlpha.FlatStyle = global::System.Windows.Forms.FlatStyle.Flat;
			this.checkBoxHasAlpha.Location = new global::System.Drawing.Point(137, 15);
			this.checkBoxHasAlpha.Name = "checkBoxHasAlpha";
			this.checkBoxHasAlpha.Size = new global::System.Drawing.Size(114, 17);
			this.checkBoxHasAlpha.TabIndex = 4;
			this.checkBoxHasAlpha.Text = "Has Alpha Channel";
			this.checkBoxHasAlpha.UseVisualStyleBackColor = true;
			base.AutoScaleDimensions = new global::System.Drawing.SizeF(6f, 13f);
			base.AutoScaleMode = global::System.Windows.Forms.AutoScaleMode.Font;
			base.ClientSize = new global::System.Drawing.Size(689, 630);
			base.Controls.Add(this.checkBoxHasAlpha);
			base.Controls.Add(this.labelInfo);
			base.Controls.Add(this.pictureBox1);
			base.FormBorderStyle = global::System.Windows.Forms.FormBorderStyle.SizableToolWindow;
			base.Name = "TextureDetailsDialog";
			base.StartPosition = global::System.Windows.Forms.FormStartPosition.CenterScreen;
			this.Text = "<TexName> - Details";
			base.TopMost = true;
			((global::System.ComponentModel.ISupportInitialize)this.pictureBox1).EndInit();
			base.ResumeLayout(false);
			base.PerformLayout();
		}

		// Token: 0x040002BD RID: 701
		private global::System.ComponentModel.IContainer components;

		// Token: 0x040002BE RID: 702
		private global::System.Windows.Forms.PictureBox pictureBox1;

		// Token: 0x040002BF RID: 703
		private global::System.Windows.Forms.Label labelInfo;

		// Token: 0x040002C0 RID: 704
		private global::System.Windows.Forms.CheckBox checkBoxHasAlpha;
	}
}
