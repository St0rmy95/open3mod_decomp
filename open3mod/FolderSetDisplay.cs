using System;
using System.ComponentModel;
using System.Drawing;
using System.Windows.Forms;

namespace open3mod
{
	// Token: 0x02000010 RID: 16
	public class FolderSetDisplay : UserControl
	{
		// Token: 0x0600009F RID: 159 RVA: 0x000051C4 File Offset: 0x000033C4
		public FolderSetDisplay()
		{
			this.InitializeComponent();
		}

		// Token: 0x14000001 RID: 1
		// (add) Token: 0x060000A0 RID: 160 RVA: 0x000051D4 File Offset: 0x000033D4
		// (remove) Token: 0x060000A1 RID: 161 RVA: 0x0000520C File Offset: 0x0000340C
		public event FolderSetDisplay.OnChangeHandler Change;

		// Token: 0x060000A2 RID: 162 RVA: 0x00005244 File Offset: 0x00003444
		public void OnChange()
		{
			FolderSetDisplay.OnChangeHandler change = this.Change;
			if (change != null)
			{
				change(this);
			}
		}

		// Token: 0x17000039 RID: 57
		// (get) Token: 0x060000A3 RID: 163 RVA: 0x00005264 File Offset: 0x00003464
		// (set) Token: 0x060000A4 RID: 164 RVA: 0x000052E0 File Offset: 0x000034E0
		public string[] Folders
		{
			get
			{
				string[] array = new string[this.listBoxFolders.Items.Count];
				int num = 0;
				foreach (object obj in this.listBoxFolders.Items)
				{
					array[num++] = (string)obj;
				}
				return array;
			}
			set
			{
				this.listBoxFolders.Items.Clear();
				for (int i = 0; i < value.Length; i++)
				{
					string item = value[i];
					this.listBoxFolders.Items.Add(item);
				}
				this.OnChange();
			}
		}

		// Token: 0x060000A5 RID: 165 RVA: 0x0000532C File Offset: 0x0000352C
		private void OnAddFolder(object sender, EventArgs e)
		{
			string text = this.textBoxFolder.Text.Trim();
			if (text.Length == 0)
			{
				return;
			}
			this.listBoxFolders.Items.Insert(0, text);
			this.listBoxFolders.SelectedItem = this.listBoxFolders.Items[0];
			this.OnChange();
		}

		// Token: 0x060000A6 RID: 166 RVA: 0x00005388 File Offset: 0x00003588
		private void OnRemoveFolder(object sender, EventArgs e)
		{
			if (this.listBoxFolders.SelectedItem == null)
			{
				return;
			}
			this.listBoxFolders.Items.Remove(this.listBoxFolders.SelectedItem);
			this.textBoxFolder.Text = "";
			this.OnChange();
		}

		// Token: 0x060000A7 RID: 167 RVA: 0x000053D4 File Offset: 0x000035D4
		private void OnSelectFolder(object sender, EventArgs e)
		{
			if (this.listBoxFolders.SelectedItem == null)
			{
				return;
			}
			this.textBoxFolder.Text = (string)this.listBoxFolders.SelectedItem;
		}

		// Token: 0x060000A8 RID: 168 RVA: 0x000053FF File Offset: 0x000035FF
		private void OnBrowse(object sender, EventArgs e)
		{
			if (this.folderBrowserDialog.ShowDialog() == DialogResult.OK)
			{
				this.textBoxFolder.Text = this.folderBrowserDialog.SelectedPath;
			}
		}

		// Token: 0x060000A9 RID: 169 RVA: 0x00005425 File Offset: 0x00003625
		protected override void Dispose(bool disposing)
		{
			if (disposing && this.components != null)
			{
				this.components.Dispose();
			}
			base.Dispose(disposing);
		}

		// Token: 0x060000AA RID: 170 RVA: 0x00005444 File Offset: 0x00003644
		private void InitializeComponent()
		{
			this.buttonRemoveFolder = new Button();
			this.buttonAddFolder = new Button();
			this.textBoxFolder = new TextBox();
			this.button2 = new Button();
			this.listBoxFolders = new ListBox();
			this.folderBrowserDialog = new FolderBrowserDialog();
			base.SuspendLayout();
			this.buttonRemoveFolder.Location = new Point(397, 42);
			this.buttonRemoveFolder.Name = "buttonRemoveFolder";
			this.buttonRemoveFolder.Size = new Size(89, 23);
			this.buttonRemoveFolder.TabIndex = 11;
			this.buttonRemoveFolder.Text = "Remove";
			this.buttonRemoveFolder.UseVisualStyleBackColor = true;
			this.buttonRemoveFolder.Click += this.OnRemoveFolder;
			this.buttonAddFolder.Location = new Point(397, 13);
			this.buttonAddFolder.Name = "buttonAddFolder";
			this.buttonAddFolder.Size = new Size(89, 23);
			this.buttonAddFolder.TabIndex = 10;
			this.buttonAddFolder.Text = "Add";
			this.buttonAddFolder.UseVisualStyleBackColor = true;
			this.buttonAddFolder.Click += this.OnAddFolder;
			this.textBoxFolder.Location = new Point(14, 13);
			this.textBoxFolder.Name = "textBoxFolder";
			this.textBoxFolder.Size = new Size(338, 20);
			this.textBoxFolder.TabIndex = 9;
			this.button2.Location = new Point(358, 12);
			this.button2.Name = "button2";
			this.button2.Size = new Size(33, 23);
			this.button2.TabIndex = 8;
			this.button2.Text = "...";
			this.button2.UseVisualStyleBackColor = true;
			this.button2.Click += this.OnBrowse;
			this.listBoxFolders.FormattingEnabled = true;
			this.listBoxFolders.Location = new Point(14, 42);
			this.listBoxFolders.Name = "listBoxFolders";
			this.listBoxFolders.Size = new Size(377, 95);
			this.listBoxFolders.TabIndex = 7;
			this.listBoxFolders.SelectedIndexChanged += this.OnSelectFolder;
			base.AutoScaleDimensions = new SizeF(6f, 13f);
			base.AutoScaleMode = AutoScaleMode.Font;
			base.Controls.Add(this.buttonRemoveFolder);
			base.Controls.Add(this.buttonAddFolder);
			base.Controls.Add(this.textBoxFolder);
			base.Controls.Add(this.button2);
			base.Controls.Add(this.listBoxFolders);
			base.Name = "FolderSetDisplay";
			base.Size = new Size(499, 152);
			base.ResumeLayout(false);
			base.PerformLayout();
		}

		// Token: 0x04000049 RID: 73
		private IContainer components;

		// Token: 0x0400004A RID: 74
		private Button buttonRemoveFolder;

		// Token: 0x0400004B RID: 75
		private Button buttonAddFolder;

		// Token: 0x0400004C RID: 76
		private TextBox textBoxFolder;

		// Token: 0x0400004D RID: 77
		private Button button2;

		// Token: 0x0400004E RID: 78
		private ListBox listBoxFolders;

		// Token: 0x0400004F RID: 79
		private FolderBrowserDialog folderBrowserDialog;

		// Token: 0x02000011 RID: 17
		// (Invoke) Token: 0x060000AC RID: 172
		public delegate void OnChangeHandler(object sender);
	}
}
