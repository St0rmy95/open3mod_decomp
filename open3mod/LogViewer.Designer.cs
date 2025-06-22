namespace open3mod
{
	// Token: 0x0200002B RID: 43
	public partial class LogViewer : global::System.Windows.Forms.Form
	{
		// Token: 0x06000175 RID: 373 RVA: 0x0000C84D File Offset: 0x0000AA4D
		protected override void Dispose(bool disposing)
		{
			if (disposing && this.components != null)
			{
				this.components.Dispose();
			}
			base.Dispose(disposing);
		}

		// Token: 0x06000176 RID: 374 RVA: 0x0000C86C File Offset: 0x0000AA6C
		private void InitializeComponent()
		{
			global::System.ComponentModel.ComponentResourceManager componentResourceManager = new global::System.ComponentModel.ComponentResourceManager(typeof(global::open3mod.LogViewer));
			this.richTextBox = new global::System.Windows.Forms.RichTextBox();
			this.saveFileDialog = new global::System.Windows.Forms.SaveFileDialog();
			this.menuStrip1 = new global::System.Windows.Forms.MenuStrip();
			this.filterToolStripMenuItem = new global::System.Windows.Forms.ToolStripMenuItem();
			this.checkBoxFilterError = new global::System.Windows.Forms.ToolStripMenuItem();
			this.checkBoxFilterWarning = new global::System.Windows.Forms.ToolStripMenuItem();
			this.checkBoxFilterInformation = new global::System.Windows.Forms.ToolStripMenuItem();
			this.checkBoxFilterVerbose = new global::System.Windows.Forms.ToolStripMenuItem();
			this.toolsToolStripMenuItem = new global::System.Windows.Forms.ToolStripMenuItem();
			this.clearToolStripMenuItem = new global::System.Windows.Forms.ToolStripMenuItem();
			this.saveToFileToolStripMenuItem = new global::System.Windows.Forms.ToolStripMenuItem();
			this.comboBoxSource = new global::System.Windows.Forms.ComboBox();
			this.label1 = new global::System.Windows.Forms.Label();
			this.menuStrip1.SuspendLayout();
			base.SuspendLayout();
			this.richTextBox.Anchor = (global::System.Windows.Forms.AnchorStyles.Top | global::System.Windows.Forms.AnchorStyles.Bottom | global::System.Windows.Forms.AnchorStyles.Left | global::System.Windows.Forms.AnchorStyles.Right);
			this.richTextBox.BackColor = global::System.Drawing.Color.White;
			this.richTextBox.DetectUrls = false;
			this.richTextBox.Font = new global::System.Drawing.Font("Consolas", 8.25f, global::System.Drawing.FontStyle.Regular, global::System.Drawing.GraphicsUnit.Point, 0);
			this.richTextBox.Location = new global::System.Drawing.Point(0, 27);
			this.richTextBox.Name = "richTextBox";
			this.richTextBox.ReadOnly = true;
			this.richTextBox.ScrollBars = global::System.Windows.Forms.RichTextBoxScrollBars.ForcedVertical;
			this.richTextBox.Size = new global::System.Drawing.Size(794, 426);
			this.richTextBox.TabIndex = 0;
			this.richTextBox.Text = "";
			this.saveFileDialog.DefaultExt = "txt";
			this.menuStrip1.Items.AddRange(new global::System.Windows.Forms.ToolStripItem[]
			{
				this.filterToolStripMenuItem,
				this.toolsToolStripMenuItem
			});
			this.menuStrip1.Location = new global::System.Drawing.Point(0, 0);
			this.menuStrip1.Name = "menuStrip1";
			this.menuStrip1.Size = new global::System.Drawing.Size(793, 24);
			this.menuStrip1.TabIndex = 5;
			this.menuStrip1.Text = "menuStrip1";
			this.filterToolStripMenuItem.DropDownItems.AddRange(new global::System.Windows.Forms.ToolStripItem[]
			{
				this.checkBoxFilterError,
				this.checkBoxFilterWarning,
				this.checkBoxFilterInformation,
				this.checkBoxFilterVerbose
			});
			this.filterToolStripMenuItem.Name = "filterToolStripMenuItem";
			this.filterToolStripMenuItem.Size = new global::System.Drawing.Size(45, 20);
			this.filterToolStripMenuItem.Text = "Filter";
			this.filterToolStripMenuItem.Click += new global::System.EventHandler(this.filterToolStripMenuItem_Click);
			this.checkBoxFilterError.Checked = true;
			this.checkBoxFilterError.CheckState = global::System.Windows.Forms.CheckState.Checked;
			this.checkBoxFilterError.Name = "checkBoxFilterError";
			this.checkBoxFilterError.Size = new global::System.Drawing.Size(162, 22);
			this.checkBoxFilterError.Text = "Errors";
			this.checkBoxFilterWarning.Checked = true;
			this.checkBoxFilterWarning.CheckState = global::System.Windows.Forms.CheckState.Checked;
			this.checkBoxFilterWarning.Name = "checkBoxFilterWarning";
			this.checkBoxFilterWarning.Size = new global::System.Drawing.Size(162, 22);
			this.checkBoxFilterWarning.Text = "Warnings";
			this.checkBoxFilterInformation.Checked = true;
			this.checkBoxFilterInformation.CheckState = global::System.Windows.Forms.CheckState.Checked;
			this.checkBoxFilterInformation.Name = "checkBoxFilterInformation";
			this.checkBoxFilterInformation.Size = new global::System.Drawing.Size(162, 22);
			this.checkBoxFilterInformation.Text = "Information";
			this.checkBoxFilterVerbose.Checked = true;
			this.checkBoxFilterVerbose.CheckState = global::System.Windows.Forms.CheckState.Checked;
			this.checkBoxFilterVerbose.Name = "checkBoxFilterVerbose";
			this.checkBoxFilterVerbose.Size = new global::System.Drawing.Size(162, 22);
			this.checkBoxFilterVerbose.Text = "Verbose (Debug)";
			this.toolsToolStripMenuItem.DropDownItems.AddRange(new global::System.Windows.Forms.ToolStripItem[]
			{
				this.clearToolStripMenuItem,
				this.saveToFileToolStripMenuItem
			});
			this.toolsToolStripMenuItem.Name = "toolsToolStripMenuItem";
			this.toolsToolStripMenuItem.Size = new global::System.Drawing.Size(48, 20);
			this.toolsToolStripMenuItem.Text = "Tools";
			this.clearToolStripMenuItem.Name = "clearToolStripMenuItem";
			this.clearToolStripMenuItem.Size = new global::System.Drawing.Size(133, 22);
			this.clearToolStripMenuItem.Text = "Clear";
			this.clearToolStripMenuItem.Click += new global::System.EventHandler(this.OnClearAll);
			this.saveToFileToolStripMenuItem.Name = "saveToFileToolStripMenuItem";
			this.saveToFileToolStripMenuItem.Size = new global::System.Drawing.Size(133, 22);
			this.saveToFileToolStripMenuItem.Text = "Save to File";
			this.saveToFileToolStripMenuItem.Click += new global::System.EventHandler(this.OnSave);
			this.comboBoxSource.DropDownStyle = global::System.Windows.Forms.ComboBoxStyle.DropDownList;
			this.comboBoxSource.FormattingEnabled = true;
			this.comboBoxSource.Location = new global::System.Drawing.Point(241, 3);
			this.comboBoxSource.Name = "comboBoxSource";
			this.comboBoxSource.Size = new global::System.Drawing.Size(327, 21);
			this.comboBoxSource.TabIndex = 6;
			this.comboBoxSource.SelectedIndexChanged += new global::System.EventHandler(this.ChangeLogSource);
			this.label1.AutoSize = true;
			this.label1.Location = new global::System.Drawing.Point(159, 6);
			this.label1.Name = "label1";
			this.label1.Size = new global::System.Drawing.Size(66, 13);
			this.label1.TabIndex = 7;
			this.label1.Text = "Show log for";
			base.AutoScaleDimensions = new global::System.Drawing.SizeF(6f, 13f);
			base.AutoScaleMode = global::System.Windows.Forms.AutoScaleMode.Font;
			base.ClientSize = new global::System.Drawing.Size(793, 453);
			base.Controls.Add(this.label1);
			base.Controls.Add(this.comboBoxSource);
			base.Controls.Add(this.richTextBox);
			base.Controls.Add(this.menuStrip1);
			base.Icon = (global::System.Drawing.Icon)componentResourceManager.GetObject("$this.Icon");
			base.MainMenuStrip = this.menuStrip1;
			base.Name = "LogViewer";
			this.Text = "Log File Viewer";
			this.menuStrip1.ResumeLayout(false);
			this.menuStrip1.PerformLayout();
			base.ResumeLayout(false);
			base.PerformLayout();
		}

		// Token: 0x04000110 RID: 272
		private global::System.ComponentModel.IContainer components;

		// Token: 0x04000111 RID: 273
		private global::System.Windows.Forms.RichTextBox richTextBox;

		// Token: 0x04000112 RID: 274
		private global::System.Windows.Forms.SaveFileDialog saveFileDialog;

		// Token: 0x04000113 RID: 275
		private global::System.Windows.Forms.MenuStrip menuStrip1;

		// Token: 0x04000114 RID: 276
		private global::System.Windows.Forms.ToolStripMenuItem filterToolStripMenuItem;

		// Token: 0x04000115 RID: 277
		private global::System.Windows.Forms.ToolStripMenuItem checkBoxFilterError;

		// Token: 0x04000116 RID: 278
		private global::System.Windows.Forms.ToolStripMenuItem checkBoxFilterWarning;

		// Token: 0x04000117 RID: 279
		private global::System.Windows.Forms.ToolStripMenuItem checkBoxFilterInformation;

		// Token: 0x04000118 RID: 280
		private global::System.Windows.Forms.ToolStripMenuItem checkBoxFilterVerbose;

		// Token: 0x04000119 RID: 281
		private global::System.Windows.Forms.ToolStripMenuItem toolsToolStripMenuItem;

		// Token: 0x0400011A RID: 282
		private global::System.Windows.Forms.ToolStripMenuItem clearToolStripMenuItem;

		// Token: 0x0400011B RID: 283
		private global::System.Windows.Forms.ToolStripMenuItem saveToFileToolStripMenuItem;

		// Token: 0x0400011C RID: 284
		private global::System.Windows.Forms.ComboBox comboBoxSource;

		// Token: 0x0400011D RID: 285
		private global::System.Windows.Forms.Label label1;
	}
}
