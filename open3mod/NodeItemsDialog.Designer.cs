namespace open3mod
{
	// Token: 0x02000018 RID: 24
	public partial class NodeItemsDialog : global::System.Windows.Forms.Form
	{
		// Token: 0x060000E3 RID: 227 RVA: 0x00006AF0 File Offset: 0x00004CF0
		protected override void Dispose(bool disposing)
		{
			if (disposing && this.components != null)
			{
				this.components.Dispose();
			}
			base.Dispose(disposing);
		}

		// Token: 0x060000E4 RID: 228 RVA: 0x00006B10 File Offset: 0x00004D10
		private void InitializeComponent()
		{
			this.groupBox1 = new global::System.Windows.Forms.GroupBox();
			this.groupBox2 = new global::System.Windows.Forms.GroupBox();
			this.label1 = new global::System.Windows.Forms.Label();
			this.label2 = new global::System.Windows.Forms.Label();
			this.label3 = new global::System.Windows.Forms.Label();
			this.label4 = new global::System.Windows.Forms.Label();
			this.groupBox3 = new global::System.Windows.Forms.GroupBox();
			this.labelMeshesTotal = new global::System.Windows.Forms.Label();
			this.labelMeshesDirect = new global::System.Windows.Forms.Label();
			this.labelChildrenTotal = new global::System.Windows.Forms.Label();
			this.labelChildrenDirect = new global::System.Windows.Forms.Label();
			this.checkBoxShowGlobalTransformation = new global::System.Windows.Forms.CheckBox();
			this.checkBoxShowAnimated = new global::System.Windows.Forms.CheckBox();
			this.trafoMatrixViewControlGlobal = new global::open3mod.TrafoMatrixViewControl();
			this.trafoMatrixViewControlLocal = new global::open3mod.TrafoMatrixViewControl();
			this.groupBox1.SuspendLayout();
			this.groupBox2.SuspendLayout();
			this.groupBox3.SuspendLayout();
			base.SuspendLayout();
			this.groupBox1.Controls.Add(this.trafoMatrixViewControlLocal);
			this.groupBox1.Location = new global::System.Drawing.Point(12, 137);
			this.groupBox1.Name = "groupBox1";
			this.groupBox1.Size = new global::System.Drawing.Size(368, 210);
			this.groupBox1.TabIndex = 2;
			this.groupBox1.TabStop = false;
			this.groupBox1.Text = "Local Transformation";
			this.groupBox2.Controls.Add(this.trafoMatrixViewControlGlobal);
			this.groupBox2.Location = new global::System.Drawing.Point(12, 392);
			this.groupBox2.Name = "groupBox2";
			this.groupBox2.Size = new global::System.Drawing.Size(368, 210);
			this.groupBox2.TabIndex = 3;
			this.groupBox2.TabStop = false;
			this.groupBox2.Text = "Global Transformation";
			this.label1.AutoSize = true;
			this.label1.Location = new global::System.Drawing.Point(16, 28);
			this.label1.Name = "label1";
			this.label1.Size = new global::System.Drawing.Size(83, 13);
			this.label1.TabIndex = 4;
			this.label1.Text = "Children (direct):";
			this.label2.AutoSize = true;
			this.label2.Location = new global::System.Drawing.Point(16, 54);
			this.label2.Name = "label2";
			this.label2.Size = new global::System.Drawing.Size(77, 13);
			this.label2.TabIndex = 5;
			this.label2.Text = "Children (total):";
			this.label3.AutoSize = true;
			this.label3.Location = new global::System.Drawing.Point(190, 28);
			this.label3.Name = "label3";
			this.label3.Size = new global::System.Drawing.Size(88, 13);
			this.label3.TabIndex = 6;
			this.label3.Text = "Meshes (owned):";
			this.label4.AutoSize = true;
			this.label4.Location = new global::System.Drawing.Point(190, 54);
			this.label4.Name = "label4";
			this.label4.Size = new global::System.Drawing.Size(76, 13);
			this.label4.TabIndex = 7;
			this.label4.Text = "Meshes (total):";
			this.groupBox3.Controls.Add(this.labelMeshesTotal);
			this.groupBox3.Controls.Add(this.labelMeshesDirect);
			this.groupBox3.Controls.Add(this.labelChildrenTotal);
			this.groupBox3.Controls.Add(this.labelChildrenDirect);
			this.groupBox3.Controls.Add(this.label1);
			this.groupBox3.Controls.Add(this.label4);
			this.groupBox3.Controls.Add(this.label2);
			this.groupBox3.Controls.Add(this.label3);
			this.groupBox3.Location = new global::System.Drawing.Point(12, 12);
			this.groupBox3.Name = "groupBox3";
			this.groupBox3.Size = new global::System.Drawing.Size(368, 82);
			this.groupBox3.TabIndex = 8;
			this.groupBox3.TabStop = false;
			this.groupBox3.Text = "Statistics";
			this.labelMeshesTotal.AutoSize = true;
			this.labelMeshesTotal.Font = new global::System.Drawing.Font("Microsoft Sans Serif", 8.25f, global::System.Drawing.FontStyle.Bold, global::System.Drawing.GraphicsUnit.Point, 0);
			this.labelMeshesTotal.Location = new global::System.Drawing.Point(278, 54);
			this.labelMeshesTotal.Name = "labelMeshesTotal";
			this.labelMeshesTotal.Size = new global::System.Drawing.Size(52, 13);
			this.labelMeshesTotal.TabIndex = 11;
			this.labelMeshesTotal.Text = "<numC>";
			this.labelMeshesDirect.AutoSize = true;
			this.labelMeshesDirect.Font = new global::System.Drawing.Font("Microsoft Sans Serif", 8.25f, global::System.Drawing.FontStyle.Bold, global::System.Drawing.GraphicsUnit.Point, 0);
			this.labelMeshesDirect.Location = new global::System.Drawing.Point(278, 28);
			this.labelMeshesDirect.Name = "labelMeshesDirect";
			this.labelMeshesDirect.Size = new global::System.Drawing.Size(52, 13);
			this.labelMeshesDirect.TabIndex = 10;
			this.labelMeshesDirect.Text = "<numC>";
			this.labelChildrenTotal.AutoSize = true;
			this.labelChildrenTotal.Font = new global::System.Drawing.Font("Microsoft Sans Serif", 8.25f, global::System.Drawing.FontStyle.Bold, global::System.Drawing.GraphicsUnit.Point, 0);
			this.labelChildrenTotal.Location = new global::System.Drawing.Point(105, 54);
			this.labelChildrenTotal.Name = "labelChildrenTotal";
			this.labelChildrenTotal.Size = new global::System.Drawing.Size(52, 13);
			this.labelChildrenTotal.TabIndex = 9;
			this.labelChildrenTotal.Text = "<numC>";
			this.labelChildrenDirect.AutoSize = true;
			this.labelChildrenDirect.Font = new global::System.Drawing.Font("Microsoft Sans Serif", 8.25f, global::System.Drawing.FontStyle.Bold, global::System.Drawing.GraphicsUnit.Point, 0);
			this.labelChildrenDirect.Location = new global::System.Drawing.Point(105, 28);
			this.labelChildrenDirect.Name = "labelChildrenDirect";
			this.labelChildrenDirect.Size = new global::System.Drawing.Size(52, 13);
			this.labelChildrenDirect.TabIndex = 8;
			this.labelChildrenDirect.Text = "<numC>";
			this.checkBoxShowGlobalTransformation.Appearance = global::System.Windows.Forms.Appearance.Button;
			this.checkBoxShowGlobalTransformation.AutoSize = true;
			this.checkBoxShowGlobalTransformation.Checked = global::open3mod.Properties.CoreSettings.Default.ShowGlobalTrafo;
			this.checkBoxShowGlobalTransformation.DataBindings.Add(new global::System.Windows.Forms.Binding("Checked", global::open3mod.Properties.CoreSettings.Default, "ShowGlobalTrafo", true, global::System.Windows.Forms.DataSourceUpdateMode.OnPropertyChanged));
			this.checkBoxShowGlobalTransformation.FlatStyle = global::System.Windows.Forms.FlatStyle.Flat;
			this.checkBoxShowGlobalTransformation.Location = new global::System.Drawing.Point(218, 361);
			this.checkBoxShowGlobalTransformation.Name = "checkBoxShowGlobalTransformation";
			this.checkBoxShowGlobalTransformation.Size = new global::System.Drawing.Size(162, 23);
			this.checkBoxShowGlobalTransformation.TabIndex = 9;
			this.checkBoxShowGlobalTransformation.Text = "Show Global Transformation ...";
			this.checkBoxShowGlobalTransformation.UseVisualStyleBackColor = true;
			this.checkBoxShowGlobalTransformation.CheckedChanged += new global::System.EventHandler(this.OnToggleShowGlobalTrafo);
			this.checkBoxShowAnimated.AutoSize = true;
			this.checkBoxShowAnimated.Checked = global::open3mod.Properties.CoreSettings.Default.NodeInfoShowAnimatedTrafo;
			this.checkBoxShowAnimated.CheckState = global::System.Windows.Forms.CheckState.Checked;
			this.checkBoxShowAnimated.DataBindings.Add(new global::System.Windows.Forms.Binding("Checked", global::open3mod.Properties.CoreSettings.Default, "NodeInfoShowAnimatedTrafo", true, global::System.Windows.Forms.DataSourceUpdateMode.OnPropertyChanged));
			this.checkBoxShowAnimated.Location = new global::System.Drawing.Point(247, 114);
			this.checkBoxShowAnimated.Name = "checkBoxShowAnimated";
			this.checkBoxShowAnimated.Size = new global::System.Drawing.Size(133, 17);
			this.checkBoxShowAnimated.TabIndex = 0;
			this.checkBoxShowAnimated.Text = "Show animated values";
			this.checkBoxShowAnimated.UseVisualStyleBackColor = true;
			this.checkBoxShowAnimated.CheckedChanged += new global::System.EventHandler(this.OnChangeAnimationState);
			this.trafoMatrixViewControlGlobal.Location = new global::System.Drawing.Point(6, 20);
			this.trafoMatrixViewControlGlobal.Name = "trafoMatrixViewControlGlobal";
			this.trafoMatrixViewControlGlobal.Size = new global::System.Drawing.Size(352, 188);
			this.trafoMatrixViewControlGlobal.TabIndex = 1;
			this.trafoMatrixViewControlLocal.Location = new global::System.Drawing.Point(6, 20);
			this.trafoMatrixViewControlLocal.Name = "trafoMatrixViewControlLocal";
			this.trafoMatrixViewControlLocal.Size = new global::System.Drawing.Size(352, 183);
			this.trafoMatrixViewControlLocal.TabIndex = 1;
			base.AutoScaleDimensions = new global::System.Drawing.SizeF(6f, 13f);
			base.AutoScaleMode = global::System.Windows.Forms.AutoScaleMode.Font;
			base.ClientSize = new global::System.Drawing.Size(392, 615);
			base.Controls.Add(this.checkBoxShowGlobalTransformation);
			base.Controls.Add(this.groupBox3);
			base.Controls.Add(this.groupBox2);
			base.Controls.Add(this.groupBox1);
			base.Controls.Add(this.checkBoxShowAnimated);
			base.FormBorderStyle = global::System.Windows.Forms.FormBorderStyle.FixedToolWindow;
			base.Name = "NodeItemsDialog";
			base.StartPosition = global::System.Windows.Forms.FormStartPosition.CenterScreen;
			this.Text = "<NodeName> Details";
			base.TopMost = true;
			this.groupBox1.ResumeLayout(false);
			this.groupBox2.ResumeLayout(false);
			this.groupBox3.ResumeLayout(false);
			this.groupBox3.PerformLayout();
			base.ResumeLayout(false);
			base.PerformLayout();
		}

		// Token: 0x04000064 RID: 100
		private global::System.ComponentModel.IContainer components;

		// Token: 0x04000065 RID: 101
		private global::System.Windows.Forms.CheckBox checkBoxShowAnimated;

		// Token: 0x04000066 RID: 102
		private global::open3mod.TrafoMatrixViewControl trafoMatrixViewControlLocal;

		// Token: 0x04000067 RID: 103
		private global::System.Windows.Forms.GroupBox groupBox1;

		// Token: 0x04000068 RID: 104
		private global::System.Windows.Forms.GroupBox groupBox2;

		// Token: 0x04000069 RID: 105
		private global::open3mod.TrafoMatrixViewControl trafoMatrixViewControlGlobal;

		// Token: 0x0400006A RID: 106
		private global::System.Windows.Forms.Label label1;

		// Token: 0x0400006B RID: 107
		private global::System.Windows.Forms.Label label2;

		// Token: 0x0400006C RID: 108
		private global::System.Windows.Forms.Label label3;

		// Token: 0x0400006D RID: 109
		private global::System.Windows.Forms.Label label4;

		// Token: 0x0400006E RID: 110
		private global::System.Windows.Forms.GroupBox groupBox3;

		// Token: 0x0400006F RID: 111
		private global::System.Windows.Forms.Label labelChildrenDirect;

		// Token: 0x04000070 RID: 112
		private global::System.Windows.Forms.Label labelChildrenTotal;

		// Token: 0x04000071 RID: 113
		private global::System.Windows.Forms.Label labelMeshesTotal;

		// Token: 0x04000072 RID: 114
		private global::System.Windows.Forms.Label labelMeshesDirect;

		// Token: 0x04000073 RID: 115
		private global::System.Windows.Forms.CheckBox checkBoxShowGlobalTransformation;
	}
}
