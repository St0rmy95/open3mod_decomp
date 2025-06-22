namespace open3mod
{
	// Token: 0x0200000C RID: 12
	public partial class DonationDialog : global::System.Windows.Forms.Form
	{
		// Token: 0x06000087 RID: 135 RVA: 0x00004B2E File Offset: 0x00002D2E
		protected override void Dispose(bool disposing)
		{
			if (disposing && this.components != null)
			{
				this.components.Dispose();
			}
			base.Dispose(disposing);
		}

		// Token: 0x06000088 RID: 136 RVA: 0x00004B50 File Offset: 0x00002D50
		private void InitializeComponent()
		{
			this.button1 = new global::System.Windows.Forms.Button();
			this.button2 = new global::System.Windows.Forms.Button();
			this.button3 = new global::System.Windows.Forms.Button();
			this.label1 = new global::System.Windows.Forms.Label();
			this.labelCount = new global::System.Windows.Forms.Label();
			base.SuspendLayout();
			this.button1.FlatStyle = global::System.Windows.Forms.FlatStyle.Popup;
			this.button1.ForeColor = global::System.Drawing.SystemColors.Desktop;
			this.button1.Location = new global::System.Drawing.Point(450, 109);
			this.button1.Name = "button1";
			this.button1.Size = new global::System.Drawing.Size(118, 23);
			this.button1.TabIndex = 1;
			this.button1.Text = "Never show again :-(";
			this.button1.UseVisualStyleBackColor = true;
			this.button1.Click += new global::System.EventHandler(this.DontAskAgain);
			this.button2.Location = new global::System.Drawing.Point(15, 109);
			this.button2.Name = "button2";
			this.button2.Size = new global::System.Drawing.Size(116, 23);
			this.button2.TabIndex = 2;
			this.button2.Text = "Ask me later";
			this.button2.UseVisualStyleBackColor = true;
			this.button2.Click += new global::System.EventHandler(this.NotNowAskAgain);
			this.button3.Font = new global::System.Drawing.Font("Microsoft Sans Serif", 11.25f, global::System.Drawing.FontStyle.Bold, global::System.Drawing.GraphicsUnit.Point, 0);
			this.button3.ForeColor = global::System.Drawing.Color.FromArgb(0, 0, 64);
			this.button3.Location = new global::System.Drawing.Point(219, 102);
			this.button3.Name = "button3";
			this.button3.Size = new global::System.Drawing.Size(143, 34);
			this.button3.TabIndex = 0;
			this.button3.Text = "Donate";
			this.button3.UseVisualStyleBackColor = true;
			this.button3.Click += new global::System.EventHandler(this.Donate);
			this.label1.AutoSize = true;
			this.label1.Font = new global::System.Drawing.Font("Microsoft Sans Serif", 9.75f, global::System.Drawing.FontStyle.Regular, global::System.Drawing.GraphicsUnit.Point, 0);
			this.label1.Location = new global::System.Drawing.Point(12, 47);
			this.label1.Name = "label1";
			this.label1.Size = new global::System.Drawing.Size(556, 48);
			this.label1.TabIndex = 3;
			this.label1.Text = "Thanks for using open3mod! If open3mod has helped you so far, consider helping us! Please\r\ndonate as a \"thank you\", to fund further development, and to support Open Source software!\r\n\r\n";
			this.labelCount.AutoSize = true;
			this.labelCount.Font = new global::System.Drawing.Font("Microsoft Sans Serif", 9.75f, global::System.Drawing.FontStyle.Bold, global::System.Drawing.GraphicsUnit.Point, 0);
			this.labelCount.ForeColor = global::System.Drawing.SystemColors.HotTrack;
			this.labelCount.Location = new global::System.Drawing.Point(12, 22);
			this.labelCount.Name = "labelCount";
			this.labelCount.Size = new global::System.Drawing.Size(262, 16);
			this.labelCount.TabIndex = 8;
			this.labelCount.Text = "In total, you've opened N 3D models.";
			base.AutoScaleDimensions = new global::System.Drawing.SizeF(6f, 13f);
			base.AutoScaleMode = global::System.Windows.Forms.AutoScaleMode.Font;
			base.ClientSize = new global::System.Drawing.Size(584, 150);
			base.Controls.Add(this.labelCount);
			base.Controls.Add(this.label1);
			base.Controls.Add(this.button3);
			base.Controls.Add(this.button2);
			base.Controls.Add(this.button1);
			base.FormBorderStyle = global::System.Windows.Forms.FormBorderStyle.FixedDialog;
			base.Name = "DonationDialog";
			base.StartPosition = global::System.Windows.Forms.FormStartPosition.CenterScreen;
			this.Text = "Your friendly donation reminder";
			base.ResumeLayout(false);
			base.PerformLayout();
		}

		// Token: 0x04000041 RID: 65
		private global::System.ComponentModel.IContainer components;

		// Token: 0x04000042 RID: 66
		private global::System.Windows.Forms.Button button1;

		// Token: 0x04000043 RID: 67
		private global::System.Windows.Forms.Button button2;

		// Token: 0x04000044 RID: 68
		private global::System.Windows.Forms.Button button3;

		// Token: 0x04000045 RID: 69
		private global::System.Windows.Forms.Label label1;

		// Token: 0x04000046 RID: 70
		private global::System.Windows.Forms.Label labelCount;
	}
}
