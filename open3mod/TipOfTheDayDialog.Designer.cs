namespace open3mod
{
	// Token: 0x02000071 RID: 113
	public partial class TipOfTheDayDialog : global::System.Windows.Forms.Form
	{
		// Token: 0x060003B0 RID: 944 RVA: 0x0001E4A5 File Offset: 0x0001C6A5
		protected override void Dispose(bool disposing)
		{
			if (disposing && this.components != null)
			{
				this.components.Dispose();
			}
			base.Dispose(disposing);
		}

		// Token: 0x060003B1 RID: 945 RVA: 0x0001E4C4 File Offset: 0x0001C6C4
		private void InitializeComponent()
		{
			global::System.ComponentModel.ComponentResourceManager componentResourceManager = new global::System.ComponentModel.ComponentResourceManager(typeof(global::open3mod.TipOfTheDayDialog));
			this.button1 = new global::System.Windows.Forms.Button();
			this.button2 = new global::System.Windows.Forms.Button();
			this.buttonOk = new global::System.Windows.Forms.Button();
			this.checkBoxDoNotShowAgain = new global::System.Windows.Forms.CheckBox();
			this.panel1 = new global::System.Windows.Forms.Panel();
			this.labelTipText = new global::System.Windows.Forms.Label();
			this.pictureBoxTipPic = new global::System.Windows.Forms.PictureBox();
			this.panel1.SuspendLayout();
			((global::System.ComponentModel.ISupportInitialize)this.pictureBoxTipPic).BeginInit();
			base.SuspendLayout();
			this.button1.Location = new global::System.Drawing.Point(104, 223);
			this.button1.Name = "button1";
			this.button1.Size = new global::System.Drawing.Size(88, 23);
			this.button1.TabIndex = 0;
			this.button1.Text = "Next";
			this.button1.UseVisualStyleBackColor = true;
			this.button1.Click += new global::System.EventHandler(this.OnNext);
			this.button2.Location = new global::System.Drawing.Point(12, 223);
			this.button2.Name = "button2";
			this.button2.Size = new global::System.Drawing.Size(92, 23);
			this.button2.TabIndex = 1;
			this.button2.Text = "Previous";
			this.button2.UseVisualStyleBackColor = true;
			this.button2.Click += new global::System.EventHandler(this.OnPrevious);
			this.buttonOk.DialogResult = global::System.Windows.Forms.DialogResult.OK;
			this.buttonOk.Location = new global::System.Drawing.Point(430, 223);
			this.buttonOk.Name = "buttonOk";
			this.buttonOk.Size = new global::System.Drawing.Size(100, 23);
			this.buttonOk.TabIndex = 2;
			this.buttonOk.Text = "OK";
			this.buttonOk.UseVisualStyleBackColor = true;
			this.checkBoxDoNotShowAgain.AutoSize = true;
			this.checkBoxDoNotShowAgain.Checked = global::CoreSettings.CoreSettings.Default.ShowTipsOnStartup;
			this.checkBoxDoNotShowAgain.DataBindings.Add(new global::System.Windows.Forms.Binding("Checked", global::CoreSettings.CoreSettings.Default, "ShowTipsOnStartup", true, global::System.Windows.Forms.DataSourceUpdateMode.OnPropertyChanged));
			this.checkBoxDoNotShowAgain.Location = new global::System.Drawing.Point(321, 227);
			this.checkBoxDoNotShowAgain.Name = "checkBoxDoNotShowAgain";
			this.checkBoxDoNotShowAgain.Size = new global::System.Drawing.Size(103, 17);
			this.checkBoxDoNotShowAgain.TabIndex = 3;
			this.checkBoxDoNotShowAgain.Text = "Show on startup";
			this.checkBoxDoNotShowAgain.UseVisualStyleBackColor = true;
			this.checkBoxDoNotShowAgain.CheckedChanged += new global::System.EventHandler(this.OnChangeStartup);
			this.panel1.BackColor = global::System.Drawing.Color.White;
			this.panel1.Controls.Add(this.labelTipText);
			this.panel1.Controls.Add(this.pictureBoxTipPic);
			this.panel1.Location = new global::System.Drawing.Point(-1, 0);
			this.panel1.Name = "panel1";
			this.panel1.Size = new global::System.Drawing.Size(543, 207);
			this.panel1.TabIndex = 4;
			this.labelTipText.AutoSize = true;
			this.labelTipText.Font = new global::System.Drawing.Font("Microsoft Sans Serif", 9.25f, global::System.Drawing.FontStyle.Regular, global::System.Drawing.GraphicsUnit.Point, 0);
			this.labelTipText.Location = new global::System.Drawing.Point(229, 25);
			this.labelTipText.Name = "labelTipText";
			this.labelTipText.Size = new global::System.Drawing.Size(205, 96);
			this.labelTipText.TabIndex = 1;
			this.labelTipText.Text = "You can use the force to control \r\nalmost everything.\r\n\r\nJust make sure the force is strong\r\nin you. Do not attempt to count\r\nMidi-Chlorians.";
			this.pictureBoxTipPic.Image = (global::System.Drawing.Image)componentResourceManager.GetObject("pictureBoxTipPic.Image");
			this.pictureBoxTipPic.Location = new global::System.Drawing.Point(13, 12);
			this.pictureBoxTipPic.Name = "pictureBoxTipPic";
			this.pictureBoxTipPic.Size = new global::System.Drawing.Size(220, 179);
			this.pictureBoxTipPic.TabIndex = 0;
			this.pictureBoxTipPic.TabStop = false;
			base.AutoScaleDimensions = new global::System.Drawing.SizeF(6f, 13f);
			base.AutoScaleMode = global::System.Windows.Forms.AutoScaleMode.Font;
			base.ClientSize = new global::System.Drawing.Size(542, 258);
			base.Controls.Add(this.panel1);
			base.Controls.Add(this.checkBoxDoNotShowAgain);
			base.Controls.Add(this.buttonOk);
			base.Controls.Add(this.button2);
			base.Controls.Add(this.button1);
			base.FormBorderStyle = global::System.Windows.Forms.FormBorderStyle.FixedDialog;
			base.Name = "TipOfTheDayDialog";
			base.StartPosition = global::System.Windows.Forms.FormStartPosition.CenterScreen;
			this.Text = "Tip of the Day";
			base.FormClosing += new global::System.Windows.Forms.FormClosingEventHandler(this.OnClose);
			this.panel1.ResumeLayout(false);
			this.panel1.PerformLayout();
			((global::System.ComponentModel.ISupportInitialize)this.pictureBoxTipPic).EndInit();
			base.ResumeLayout(false);
			base.PerformLayout();
		}

		// Token: 0x040002ED RID: 749
		private global::System.ComponentModel.IContainer components;

		// Token: 0x040002EE RID: 750
		private global::System.Windows.Forms.Button button1;

		// Token: 0x040002EF RID: 751
		private global::System.Windows.Forms.Button button2;

		// Token: 0x040002F0 RID: 752
		private global::System.Windows.Forms.Button buttonOk;

		// Token: 0x040002F1 RID: 753
		private global::System.Windows.Forms.CheckBox checkBoxDoNotShowAgain;

		// Token: 0x040002F2 RID: 754
		private global::System.Windows.Forms.Panel panel1;

		// Token: 0x040002F3 RID: 755
		private global::System.Windows.Forms.PictureBox pictureBoxTipPic;

		// Token: 0x040002F4 RID: 756
		private global::System.Windows.Forms.Label labelTipText;
	}
}
