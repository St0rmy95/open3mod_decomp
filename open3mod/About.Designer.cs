namespace open3mod
{
	// Token: 0x02000002 RID: 2
	internal partial class About : global::System.Windows.Forms.Form
	{
		// Token: 0x06000010 RID: 16 RVA: 0x00002214 File Offset: 0x00000414
		protected override void Dispose(bool disposing)
		{
			if (disposing && this.components != null)
			{
				this.components.Dispose();
			}
			base.Dispose(disposing);
		}

		// Token: 0x06000011 RID: 17 RVA: 0x00002234 File Offset: 0x00000434
		private void InitializeComponent()
		{
			global::System.ComponentModel.ComponentResourceManager componentResourceManager = new global::System.ComponentModel.ComponentResourceManager(typeof(global::open3mod.About));
			this.label1 = new global::System.Windows.Forms.Label();
			this.label3 = new global::System.Windows.Forms.Label();
			this.label4 = new global::System.Windows.Forms.Label();
			this.linkLabel1 = new global::System.Windows.Forms.LinkLabel();
			this.linkLabel2 = new global::System.Windows.Forms.LinkLabel();
			this.button1 = new global::System.Windows.Forms.Button();
			this.helpProvider1 = new global::System.Windows.Forms.HelpProvider();
			this.richTextBox1 = new global::System.Windows.Forms.RichTextBox();
			this.label2 = new global::System.Windows.Forms.Label();
			this.linkLabel3 = new global::System.Windows.Forms.LinkLabel();
			this.pageSetupDialog1 = new global::System.Windows.Forms.PageSetupDialog();
			this.process1 = new global::System.Diagnostics.Process();
			this.label5 = new global::System.Windows.Forms.Label();
			base.SuspendLayout();
			this.label1.AutoSize = true;
			this.label1.Font = new global::System.Drawing.Font("Microsoft Sans Serif", 10.25f, global::System.Drawing.FontStyle.Regular, global::System.Drawing.GraphicsUnit.Point, 0);
			this.label1.Location = new global::System.Drawing.Point(25, 20);
			this.label1.Name = "label1";
			this.label1.Size = new global::System.Drawing.Size(153, 17);
			this.label1.TabIndex = 0;
			this.label1.Text = "Open 3D Model Viewer";
			this.label3.AutoSize = true;
			this.label3.Font = new global::System.Drawing.Font("Microsoft Sans Serif", 8.25f, global::System.Drawing.FontStyle.Regular, global::System.Drawing.GraphicsUnit.Point, 0);
			this.label3.Location = new global::System.Drawing.Point(12, 102);
			this.label3.Name = "label3";
			this.label3.Size = new global::System.Drawing.Size(7, 13);
			this.label3.TabIndex = 3;
			this.label3.Text = "\r\n";
			this.label4.AutoSize = true;
			this.label4.Font = new global::System.Drawing.Font("Microsoft Sans Serif", 8.25f, global::System.Drawing.FontStyle.Bold, global::System.Drawing.GraphicsUnit.Point, 0);
			this.label4.Location = new global::System.Drawing.Point(25, 37);
			this.label4.Name = "label4";
			this.label4.Size = new global::System.Drawing.Size(71, 13);
			this.label4.TabIndex = 4;
			this.label4.Text = "Version 1.1";
			this.linkLabel1.AutoSize = true;
			this.linkLabel1.Location = new global::System.Drawing.Point(543, 23);
			this.linkLabel1.Name = "linkLabel1";
			this.linkLabel1.Size = new global::System.Drawing.Size(46, 13);
			this.linkLabel1.TabIndex = 5;
			this.linkLabel1.TabStop = true;
			this.linkLabel1.Text = "Website";
			this.linkLabel1.LinkClicked += new global::System.Windows.Forms.LinkLabelLinkClickedEventHandler(this.linkLabel1_LinkClicked);
			this.linkLabel2.AutoSize = true;
			this.linkLabel2.Location = new global::System.Drawing.Point(499, 23);
			this.linkLabel2.Name = "linkLabel2";
			this.linkLabel2.Size = new global::System.Drawing.Size(38, 13);
			this.linkLabel2.TabIndex = 6;
			this.linkLabel2.TabStop = true;
			this.linkLabel2.Text = "Github";
			this.linkLabel2.LinkClicked += new global::System.Windows.Forms.LinkLabelLinkClickedEventHandler(this.linkLabel2_LinkClicked);
			this.button1.DialogResult = global::System.Windows.Forms.DialogResult.OK;
			this.button1.Location = new global::System.Drawing.Point(504, 451);
			this.button1.Name = "button1";
			this.button1.Size = new global::System.Drawing.Size(94, 23);
			this.button1.TabIndex = 0;
			this.button1.Text = "OK";
			this.button1.UseVisualStyleBackColor = true;
			this.richTextBox1.Font = new global::System.Drawing.Font("Courier New", 8.25f, global::System.Drawing.FontStyle.Regular, global::System.Drawing.GraphicsUnit.Point, 0);
			this.richTextBox1.Location = new global::System.Drawing.Point(21, 145);
			this.richTextBox1.Name = "richTextBox1";
			this.richTextBox1.Size = new global::System.Drawing.Size(577, 298);
			this.richTextBox1.TabIndex = 9;
			this.richTextBox1.Text = componentResourceManager.GetString("richTextBox1.Text");
			this.label2.AutoSize = true;
			this.label2.Location = new global::System.Drawing.Point(25, 102);
			this.label2.Name = "label2";
			this.label2.Size = new global::System.Drawing.Size(577, 26);
			this.label2.TabIndex = 10;
			this.label2.Text = componentResourceManager.GetString("label2.Text");
			this.linkLabel3.AutoSize = true;
			this.linkLabel3.Location = new global::System.Drawing.Point(25, 78);
			this.linkLabel3.Name = "linkLabel3";
			this.linkLabel3.Size = new global::System.Drawing.Size(316, 13);
			this.linkLabel3.TabIndex = 11;
			this.linkLabel3.TabStop = true;
			this.linkLabel3.Text = "Please consider donating money to help fund further development";
			this.linkLabel3.LinkClicked += new global::System.Windows.Forms.LinkLabelLinkClickedEventHandler(this.linkLabel3_LinkClicked);
			this.process1.StartInfo.Domain = "";
			this.process1.StartInfo.LoadUserProfile = false;
			this.process1.StartInfo.Password = null;
			this.process1.StartInfo.StandardErrorEncoding = null;
			this.process1.StartInfo.StandardOutputEncoding = null;
			this.process1.StartInfo.UserName = "";
			this.process1.SynchronizingObject = this;
			this.label5.AutoSize = true;
			this.label5.Location = new global::System.Drawing.Point(50, 185);
			this.label5.Name = "label5";
			this.label5.Size = new global::System.Drawing.Size(0, 13);
			this.label5.TabIndex = 12;
			base.AutoScaleDimensions = new global::System.Drawing.SizeF(6f, 13f);
			base.AutoScaleMode = global::System.Windows.Forms.AutoScaleMode.Font;
			base.ClientSize = new global::System.Drawing.Size(610, 486);
			base.Controls.Add(this.label5);
			base.Controls.Add(this.linkLabel3);
			base.Controls.Add(this.label2);
			base.Controls.Add(this.richTextBox1);
			base.Controls.Add(this.button1);
			base.Controls.Add(this.linkLabel2);
			base.Controls.Add(this.linkLabel1);
			base.Controls.Add(this.label4);
			base.Controls.Add(this.label3);
			base.Controls.Add(this.label1);
			base.FormBorderStyle = global::System.Windows.Forms.FormBorderStyle.FixedDialog;
			base.MaximizeBox = false;
			base.MinimizeBox = false;
			base.Name = "About";
			base.Padding = new global::System.Windows.Forms.Padding(9);
			base.ShowIcon = false;
			base.ShowInTaskbar = false;
			base.StartPosition = global::System.Windows.Forms.FormStartPosition.CenterParent;
			this.Text = "About open3mod";
			base.Load += new global::System.EventHandler(this.About_Load);
			base.ResumeLayout(false);
			base.PerformLayout();
		}

		// Token: 0x04000001 RID: 1
		private global::System.ComponentModel.IContainer components;

		// Token: 0x04000002 RID: 2
		private global::System.Windows.Forms.Label label1;

		// Token: 0x04000003 RID: 3
		private global::System.Windows.Forms.Label label3;

		// Token: 0x04000004 RID: 4
		private global::System.Windows.Forms.Label label4;

		// Token: 0x04000005 RID: 5
		private global::System.Windows.Forms.LinkLabel linkLabel1;

		// Token: 0x04000006 RID: 6
		private global::System.Windows.Forms.LinkLabel linkLabel2;

		// Token: 0x04000007 RID: 7
		private global::System.Windows.Forms.Button button1;

		// Token: 0x04000008 RID: 8
		private global::System.Windows.Forms.HelpProvider helpProvider1;

		// Token: 0x04000009 RID: 9
		private global::System.Windows.Forms.RichTextBox richTextBox1;

		// Token: 0x0400000A RID: 10
		private global::System.Windows.Forms.Label label2;

		// Token: 0x0400000B RID: 11
		private global::System.Windows.Forms.LinkLabel linkLabel3;

		// Token: 0x0400000C RID: 12
		private global::System.Windows.Forms.PageSetupDialog pageSetupDialog1;

		// Token: 0x0400000D RID: 13
		private global::System.Diagnostics.Process process1;

		// Token: 0x0400000E RID: 14
		private global::System.Windows.Forms.Label label5;
	}
}
