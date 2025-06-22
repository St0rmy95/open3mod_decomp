using System;
using System.ComponentModel;
using System.Drawing;
using System.Linq;
using System.Windows.Forms;
using Assimp;
using OpenTK;

namespace open3mod
{
	// Token: 0x02000072 RID: 114
	public class TrafoMatrixViewControl : UserControl
	{
		// Token: 0x060003B3 RID: 947 RVA: 0x0001EA24 File Offset: 0x0001CC24
		public TrafoMatrixViewControl()
		{
			this.InitializeComponent();
			this.comboBoxRotMode.SelectedIndex = ((Properties.CoreSettings.Default.DefaultRotationMode < this.comboBoxRotMode.Items.Count) ? Properties.CoreSettings.Default.DefaultRotationMode : 0);
			foreach (Control control in base.Controls.OfType<Control>())
			{
				control.BackColor = control.BackColor;
			}
		}

		// Token: 0x060003B4 RID: 948 RVA: 0x0001EABC File Offset: 0x0001CCBC
		public void SetMatrix(ref Matrix4x4 mat)
		{
			this.UpdateUi(mat, false);
			this._baseMatrix = mat;
		}

		// Token: 0x060003B5 RID: 949 RVA: 0x0001EAD7 File Offset: 0x0001CCD7
		public void SetAnimatedMatrix(ref Matrix4x4 mat)
		{
			this._isInDiffView = true;
			this.UpdateUi(mat, true);
		}

		// Token: 0x060003B6 RID: 950 RVA: 0x0001EAED File Offset: 0x0001CCED
		public void ResetAnimatedMatrix()
		{
			this._isInDiffView = false;
			this.UpdateUi(this._baseMatrix, false);
		}

		// Token: 0x060003B7 RID: 951 RVA: 0x0001EB03 File Offset: 0x0001CD03
		private void OnUpdateRotation(object sender, EventArgs e)
		{
			Properties.CoreSettings.Default.DefaultRotationMode = this.comboBoxRotMode.SelectedIndex;
			this.SetRotation(this._isInDiffView);
		}

		// Token: 0x060003B8 RID: 952 RVA: 0x0001EB28 File Offset: 0x0001CD28
		private void UpdateUi(Matrix4x4 mat, bool diffAgainstBaseMatrix)
		{
			this.checkBoxNonStandard.Checked = ((double)Math.Abs(mat.Determinant()) < 1E-05);
			Vector3D scale;
			Assimp.Quaternion quaternion;
			Vector3D trans;
			mat.Decompose(out scale, out quaternion, out trans);
			this.textBoxTransX.Text = trans.X.ToString("#0.000");
			this.textBoxTransY.Text = trans.Y.ToString("#0.000");
			this.textBoxTransZ.Text = trans.Z.ToString("#0.000");
			bool flag = (double)Math.Abs(scale.X - scale.Y) < 1E-05 && (double)Math.Abs(scale.X - scale.Z) < 1E-05;
			this.textBoxScaleX.Text = scale.X.ToString("#0.000");
			this.textBoxScaleY.Text = scale.Y.ToString("#0.000");
			this.textBoxScaleZ.Text = scale.Z.ToString("#0.000");
			this.textBoxScaleY.Visible = !flag;
			this.textBoxScaleZ.Visible = !flag;
			this.labelScalingX.Visible = !flag;
			this.labelScalingY.Visible = !flag;
			this.labelScalingZ.Visible = !flag;
			if (diffAgainstBaseMatrix)
			{
				if ((double)Math.Abs(scale.X - this._scale.X) > 9.9999997473787516E-06)
				{
					this.labelScalingX.ForeColor = this.ColorIsAnimated;
					this.textBoxScaleX.ForeColor = this.ColorIsAnimated;
				}
				if ((double)Math.Abs(scale.Y - this._scale.Y) > 9.9999997473787516E-06)
				{
					this.labelScalingY.ForeColor = this.ColorIsAnimated;
					this.textBoxScaleY.ForeColor = this.ColorIsAnimated;
				}
				if ((double)Math.Abs(scale.Z - this._scale.Z) > 9.9999997473787516E-06)
				{
					this.labelScalingZ.ForeColor = this.ColorIsAnimated;
					this.textBoxScaleZ.ForeColor = this.ColorIsAnimated;
				}
				if ((double)Math.Abs(trans.X - this._trans.X) > 9.9999997473787516E-06)
				{
					this.labelTranslationX.ForeColor = this.ColorIsAnimated;
					this.textBoxTransX.ForeColor = this.ColorIsAnimated;
				}
				if ((double)Math.Abs(trans.Y - this._trans.Y) > 9.9999997473787516E-06)
				{
					this.labelTranslationY.ForeColor = this.ColorIsAnimated;
					this.textBoxTransY.ForeColor = this.ColorIsAnimated;
				}
				if ((double)Math.Abs(trans.Z - this._trans.Z) > 9.9999997473787516E-06)
				{
					this.labelTranslationZ.ForeColor = this.ColorIsAnimated;
					this.textBoxTransZ.ForeColor = this.ColorIsAnimated;
				}
			}
			else
			{
				this.labelScalingX.ForeColor = this.ColorNotAnimated;
				this.textBoxScaleX.ForeColor = this.ColorNotAnimated;
				this.labelScalingY.ForeColor = this.ColorNotAnimated;
				this.textBoxScaleY.ForeColor = this.ColorNotAnimated;
				this.labelScalingZ.ForeColor = this.ColorNotAnimated;
				this.textBoxScaleZ.ForeColor = this.ColorNotAnimated;
				this.labelTranslationX.ForeColor = this.ColorNotAnimated;
				this.textBoxTransX.ForeColor = this.ColorNotAnimated;
				this.labelTranslationY.ForeColor = this.ColorNotAnimated;
				this.textBoxTransY.ForeColor = this.ColorNotAnimated;
				this.labelTranslationZ.ForeColor = this.ColorNotAnimated;
				this.textBoxTransZ.ForeColor = this.ColorNotAnimated;
				this._scale = scale;
				this._trans = trans;
				this._rot = quaternion;
			}
			this._rotCurrent = quaternion;
			this.SetRotation(diffAgainstBaseMatrix);
		}

		// Token: 0x170000A5 RID: 165
		// (get) Token: 0x060003B9 RID: 953 RVA: 0x0001EF3D File Offset: 0x0001D13D
		protected Color ColorNotAnimated
		{
			get
			{
				return Color.Black;
			}
		}

		// Token: 0x170000A6 RID: 166
		// (get) Token: 0x060003BA RID: 954 RVA: 0x0001EF44 File Offset: 0x0001D144
		protected Color ColorIsAnimated
		{
			get
			{
				return Color.Red;
			}
		}

		// Token: 0x060003BB RID: 955 RVA: 0x0001EF4C File Offset: 0x0001D14C
		private static void QuatToEulerXyz(ref Assimp.Quaternion q1, out Vector3 outVector)
		{
			double num = (double)(q1.W * q1.W);
			double num2 = (double)(q1.X * q1.X);
			double num3 = (double)(q1.Y * q1.Y);
			double num4 = (double)(q1.Z * q1.Z);
			double num5 = num2 + num3 + num4 + num;
			double num6 = (double)(q1.X * q1.Y + q1.Z * q1.W);
			if (num6 > 0.499 * num5)
			{
				outVector.Z = (float)(2.0 * Math.Atan2((double)q1.X, (double)q1.W));
				outVector.Y = 1.57079637f;
				outVector.X = 0f;
				return;
			}
			if (num6 < -0.499 * num5)
			{
				outVector.Z = (float)(-2.0 * Math.Atan2((double)q1.X, (double)q1.W));
				outVector.Y = -1.57079637f;
				outVector.X = 0f;
				return;
			}
			outVector.Z = (float)Math.Atan2((double)(2f * q1.Y * q1.W - 2f * q1.X * q1.Z), num2 - num3 - num4 + num);
			outVector.Y = (float)Math.Asin(2.0 * num6 / num5);
			outVector.X = (float)Math.Atan2((double)(2f * q1.X * q1.W - 2f * q1.Y * q1.Z), -num2 + num3 - num4 + num);
		}

		// Token: 0x060003BC RID: 956 RVA: 0x0001F0E4 File Offset: 0x0001D2E4
		private void SetRotation(bool diffAgainstBaseMatrix)
		{
			switch (this.comboBoxRotMode.SelectedIndex)
			{
			case 0:
			case 1:
			{
				this.labelRotationW.Visible = false;
				this.textBoxRotW.Visible = false;
				Vector3 vec;
				TrafoMatrixViewControl.QuatToEulerXyz(ref this._rotCurrent, out vec);
				if (this.comboBoxRotMode.SelectedIndex == 0)
				{
					vec *= 57.2957764f;
				}
				this.textBoxRotX.Text = vec.X.ToString("#0.000");
				this.textBoxRotY.Text = vec.Y.ToString("#0.000");
				this.textBoxRotZ.Text = vec.Z.ToString("#0.000");
				if (diffAgainstBaseMatrix)
				{
					Vector3 vec2;
					TrafoMatrixViewControl.QuatToEulerXyz(ref this._rot, out vec2);
					if (this.comboBoxRotMode.SelectedIndex == 0)
					{
						vec2 *= 57.2957764f;
					}
					if ((double)Math.Abs(vec.X - vec2.X) > 9.9999997473787516E-06)
					{
						this.labelRotationX.ForeColor = this.ColorIsAnimated;
						this.textBoxRotX.ForeColor = this.ColorIsAnimated;
					}
					if ((double)Math.Abs(vec.Y - vec2.Y) > 9.9999997473787516E-06)
					{
						this.labelRotationY.ForeColor = this.ColorIsAnimated;
						this.textBoxRotY.ForeColor = this.ColorIsAnimated;
					}
					if ((double)Math.Abs(vec.Z - vec2.Z) > 9.9999997473787516E-06)
					{
						this.labelRotationZ.ForeColor = this.ColorIsAnimated;
						this.textBoxRotZ.ForeColor = this.ColorIsAnimated;
					}
				}
				break;
			}
			case 2:
				this.labelRotationW.Visible = true;
				this.textBoxRotW.Visible = true;
				this.textBoxRotX.Text = this._rotCurrent.X.ToString("#0.000");
				this.textBoxRotY.Text = this._rotCurrent.Y.ToString("#0.000");
				this.textBoxRotZ.Text = this._rotCurrent.Z.ToString("#0.000");
				this.textBoxRotW.Text = this._rotCurrent.W.ToString("#0.000");
				if (diffAgainstBaseMatrix)
				{
					if ((double)Math.Abs(this._rotCurrent.X - this._rot.X) > 9.9999997473787516E-06)
					{
						this.labelRotationX.ForeColor = this.ColorIsAnimated;
						this.textBoxRotX.ForeColor = this.ColorIsAnimated;
					}
					if ((double)Math.Abs(this._rotCurrent.Y - this._rot.Y) > 9.9999997473787516E-06)
					{
						this.labelRotationY.ForeColor = this.ColorIsAnimated;
						this.textBoxRotY.ForeColor = this.ColorIsAnimated;
					}
					if ((double)Math.Abs(this._rotCurrent.Z - this._rot.Z) > 9.9999997473787516E-06)
					{
						this.labelRotationZ.ForeColor = this.ColorIsAnimated;
						this.textBoxRotZ.ForeColor = this.ColorIsAnimated;
					}
					if ((double)Math.Abs(this._rotCurrent.W - this._rot.W) > 9.9999997473787516E-06)
					{
						this.labelRotationW.ForeColor = this.ColorIsAnimated;
						this.textBoxRotW.ForeColor = this.ColorIsAnimated;
					}
				}
				break;
			}
			if (!diffAgainstBaseMatrix)
			{
				this.textBoxRotX.ForeColor = this.ColorNotAnimated;
				this.textBoxRotY.ForeColor = this.ColorNotAnimated;
				this.textBoxRotZ.ForeColor = this.ColorNotAnimated;
				this.textBoxRotW.ForeColor = this.ColorNotAnimated;
				this.labelRotationX.ForeColor = this.ColorNotAnimated;
				this.labelRotationY.ForeColor = this.ColorNotAnimated;
				this.labelRotationZ.ForeColor = this.ColorNotAnimated;
				this.labelRotationW.ForeColor = this.ColorNotAnimated;
			}
		}

		// Token: 0x060003BD RID: 957 RVA: 0x0001F4F4 File Offset: 0x0001D6F4
		protected override void Dispose(bool disposing)
		{
			if (disposing && this.components != null)
			{
				this.components.Dispose();
			}
			base.Dispose(disposing);
		}

		// Token: 0x060003BE RID: 958 RVA: 0x0001F514 File Offset: 0x0001D714
		private void InitializeComponent()
		{
			this.label1 = new Label();
			this.textBoxTransX = new TextBox();
			this.labelTranslationX = new Label();
			this.labelTranslationY = new Label();
			this.textBoxTransY = new TextBox();
			this.labelTranslationZ = new Label();
			this.textBoxTransZ = new TextBox();
			this.label5 = new Label();
			this.labelScalingZ = new Label();
			this.textBoxScaleZ = new TextBox();
			this.labelScalingY = new Label();
			this.textBoxScaleY = new TextBox();
			this.labelScalingX = new Label();
			this.textBoxScaleX = new TextBox();
			this.labelRotationZ = new Label();
			this.textBoxRotZ = new TextBox();
			this.labelRotationY = new Label();
			this.textBoxRotY = new TextBox();
			this.labelRotationX = new Label();
			this.textBoxRotX = new TextBox();
			this.label12 = new Label();
			this.comboBoxRotMode = new ComboBox();
			this.textBoxRotW = new TextBox();
			this.labelRotationW = new Label();
			this.checkBoxNonStandard = new CheckBox();
			base.SuspendLayout();
			this.label1.AutoSize = true;
			this.label1.Font = new Font("Microsoft Sans Serif", 8.25f, FontStyle.Bold, GraphicsUnit.Point, 0);
			this.label1.Location = new Point(4, 5);
			this.label1.Name = "label1";
			this.label1.Size = new Size(74, 13);
			this.label1.TabIndex = 0;
			this.label1.Text = "Translation:";
			this.textBoxTransX.Location = new Point(93, 3);
			this.textBoxTransX.Name = "textBoxTransX";
			this.textBoxTransX.ReadOnly = true;
			this.textBoxTransX.Size = new Size(64, 20);
			this.textBoxTransX.TabIndex = 1;
			this.labelTranslationX.AutoSize = true;
			this.labelTranslationX.Location = new Point(77, 6);
			this.labelTranslationX.Name = "labelTranslationX";
			this.labelTranslationX.Size = new Size(14, 13);
			this.labelTranslationX.TabIndex = 2;
			this.labelTranslationX.Text = "X";
			this.labelTranslationY.AutoSize = true;
			this.labelTranslationY.Location = new Point(171, 6);
			this.labelTranslationY.Name = "labelTranslationY";
			this.labelTranslationY.Size = new Size(14, 13);
			this.labelTranslationY.TabIndex = 4;
			this.labelTranslationY.Text = "Y";
			this.textBoxTransY.Location = new Point(187, 3);
			this.textBoxTransY.Name = "textBoxTransY";
			this.textBoxTransY.ReadOnly = true;
			this.textBoxTransY.Size = new Size(64, 20);
			this.textBoxTransY.TabIndex = 3;
			this.labelTranslationZ.AutoSize = true;
			this.labelTranslationZ.Location = new Point(264, 6);
			this.labelTranslationZ.Name = "labelTranslationZ";
			this.labelTranslationZ.Size = new Size(14, 13);
			this.labelTranslationZ.TabIndex = 6;
			this.labelTranslationZ.Text = "Z";
			this.textBoxTransZ.Location = new Point(280, 3);
			this.textBoxTransZ.Name = "textBoxTransZ";
			this.textBoxTransZ.ReadOnly = true;
			this.textBoxTransZ.Size = new Size(64, 20);
			this.textBoxTransZ.TabIndex = 5;
			this.label5.AutoSize = true;
			this.label5.Font = new Font("Microsoft Sans Serif", 8.25f, FontStyle.Bold, GraphicsUnit.Point, 0);
			this.label5.Location = new Point(25, 32);
			this.label5.Name = "label5";
			this.label5.Size = new Size(53, 13);
			this.label5.TabIndex = 7;
			this.label5.Text = "Scaling:";
			this.labelScalingZ.AutoSize = true;
			this.labelScalingZ.Location = new Point(264, 32);
			this.labelScalingZ.Name = "labelScalingZ";
			this.labelScalingZ.Size = new Size(14, 13);
			this.labelScalingZ.TabIndex = 13;
			this.labelScalingZ.Text = "Z";
			this.textBoxScaleZ.Location = new Point(280, 29);
			this.textBoxScaleZ.Name = "textBoxScaleZ";
			this.textBoxScaleZ.ReadOnly = true;
			this.textBoxScaleZ.Size = new Size(64, 20);
			this.textBoxScaleZ.TabIndex = 12;
			this.labelScalingY.AutoSize = true;
			this.labelScalingY.Location = new Point(171, 32);
			this.labelScalingY.Name = "labelScalingY";
			this.labelScalingY.Size = new Size(14, 13);
			this.labelScalingY.TabIndex = 11;
			this.labelScalingY.Text = "Y";
			this.textBoxScaleY.Location = new Point(187, 29);
			this.textBoxScaleY.Name = "textBoxScaleY";
			this.textBoxScaleY.ReadOnly = true;
			this.textBoxScaleY.Size = new Size(64, 20);
			this.textBoxScaleY.TabIndex = 10;
			this.labelScalingX.AutoSize = true;
			this.labelScalingX.Location = new Point(77, 32);
			this.labelScalingX.Name = "labelScalingX";
			this.labelScalingX.Size = new Size(14, 13);
			this.labelScalingX.TabIndex = 9;
			this.labelScalingX.Text = "X";
			this.textBoxScaleX.Location = new Point(93, 29);
			this.textBoxScaleX.Name = "textBoxScaleX";
			this.textBoxScaleX.ReadOnly = true;
			this.textBoxScaleX.Size = new Size(64, 20);
			this.textBoxScaleX.TabIndex = 8;
			this.labelRotationZ.AutoSize = true;
			this.labelRotationZ.Location = new Point(264, 93);
			this.labelRotationZ.Name = "labelRotationZ";
			this.labelRotationZ.Size = new Size(14, 13);
			this.labelRotationZ.TabIndex = 20;
			this.labelRotationZ.Text = "Z";
			this.textBoxRotZ.Location = new Point(280, 90);
			this.textBoxRotZ.Name = "textBoxRotZ";
			this.textBoxRotZ.ReadOnly = true;
			this.textBoxRotZ.Size = new Size(64, 20);
			this.textBoxRotZ.TabIndex = 19;
			this.labelRotationY.AutoSize = true;
			this.labelRotationY.Location = new Point(171, 93);
			this.labelRotationY.Name = "labelRotationY";
			this.labelRotationY.Size = new Size(14, 13);
			this.labelRotationY.TabIndex = 18;
			this.labelRotationY.Text = "Y";
			this.textBoxRotY.Location = new Point(187, 90);
			this.textBoxRotY.Name = "textBoxRotY";
			this.textBoxRotY.ReadOnly = true;
			this.textBoxRotY.Size = new Size(64, 20);
			this.textBoxRotY.TabIndex = 17;
			this.labelRotationX.AutoSize = true;
			this.labelRotationX.Location = new Point(77, 93);
			this.labelRotationX.Name = "labelRotationX";
			this.labelRotationX.Size = new Size(14, 13);
			this.labelRotationX.TabIndex = 16;
			this.labelRotationX.Text = "X";
			this.textBoxRotX.Location = new Point(93, 90);
			this.textBoxRotX.Name = "textBoxRotX";
			this.textBoxRotX.ReadOnly = true;
			this.textBoxRotX.Size = new Size(64, 20);
			this.textBoxRotX.TabIndex = 15;
			this.label12.AutoSize = true;
			this.label12.Font = new Font("Microsoft Sans Serif", 8.25f, FontStyle.Bold, GraphicsUnit.Point, 0);
			this.label12.Location = new Point(19, 93);
			this.label12.Name = "label12";
			this.label12.Size = new Size(59, 13);
			this.label12.TabIndex = 14;
			this.label12.Text = "Rotation:";
			this.comboBoxRotMode.DropDownStyle = ComboBoxStyle.DropDownList;
			this.comboBoxRotMode.FormattingEnabled = true;
			this.comboBoxRotMode.Items.AddRange(new object[]
			{
				"Euler XYZ (degrees)",
				"Euler XYZ (radians)",
				"Quaternion"
			});
			this.comboBoxRotMode.Location = new Point(93, 63);
			this.comboBoxRotMode.Name = "comboBoxRotMode";
			this.comboBoxRotMode.Size = new Size(158, 21);
			this.comboBoxRotMode.TabIndex = 21;
			this.comboBoxRotMode.SelectedIndexChanged += this.OnUpdateRotation;
			this.textBoxRotW.Location = new Point(93, 116);
			this.textBoxRotW.Name = "textBoxRotW";
			this.textBoxRotW.ReadOnly = true;
			this.textBoxRotW.Size = new Size(64, 20);
			this.textBoxRotW.TabIndex = 22;
			this.labelRotationW.AutoSize = true;
			this.labelRotationW.Location = new Point(73, 123);
			this.labelRotationW.Name = "labelRotationW";
			this.labelRotationW.Size = new Size(18, 13);
			this.labelRotationW.TabIndex = 23;
			this.labelRotationW.Text = "W";
			this.checkBoxNonStandard.AutoCheck = false;
			this.checkBoxNonStandard.AutoSize = true;
			this.checkBoxNonStandard.FlatStyle = FlatStyle.Popup;
			this.checkBoxNonStandard.Location = new Point(22, 152);
			this.checkBoxNonStandard.Name = "checkBoxNonStandard";
			this.checkBoxNonStandard.Size = new Size(306, 30);
			this.checkBoxNonStandard.TabIndex = 24;
			this.checkBoxNonStandard.Text = "Transformation does not define an invertible affine operation\r\nconsisting of scaling, rotation and translation.\r\n";
			this.checkBoxNonStandard.UseVisualStyleBackColor = true;
			base.AutoScaleDimensions = new SizeF(6f, 13f);
			base.AutoScaleMode = AutoScaleMode.Font;
			base.Controls.Add(this.checkBoxNonStandard);
			base.Controls.Add(this.labelRotationW);
			base.Controls.Add(this.textBoxRotW);
			base.Controls.Add(this.comboBoxRotMode);
			base.Controls.Add(this.labelRotationZ);
			base.Controls.Add(this.textBoxRotZ);
			base.Controls.Add(this.labelRotationY);
			base.Controls.Add(this.textBoxRotY);
			base.Controls.Add(this.labelRotationX);
			base.Controls.Add(this.textBoxRotX);
			base.Controls.Add(this.label12);
			base.Controls.Add(this.labelScalingZ);
			base.Controls.Add(this.textBoxScaleZ);
			base.Controls.Add(this.labelScalingY);
			base.Controls.Add(this.textBoxScaleY);
			base.Controls.Add(this.labelScalingX);
			base.Controls.Add(this.textBoxScaleX);
			base.Controls.Add(this.label5);
			base.Controls.Add(this.labelTranslationZ);
			base.Controls.Add(this.textBoxTransZ);
			base.Controls.Add(this.labelTranslationY);
			base.Controls.Add(this.textBoxTransY);
			base.Controls.Add(this.labelTranslationX);
			base.Controls.Add(this.textBoxTransX);
			base.Controls.Add(this.label1);
			base.Name = "TrafoMatrixViewControl";
			base.Size = new Size(347, 188);
			base.ResumeLayout(false);
			base.PerformLayout();
		}

		// Token: 0x040002F5 RID: 757
		private const string Format = "#0.000";

		// Token: 0x040002F6 RID: 758
		private Vector3D _scale;

		// Token: 0x040002F7 RID: 759
		private Assimp.Quaternion _rot;

		// Token: 0x040002F8 RID: 760
		private Vector3D _trans;

		// Token: 0x040002F9 RID: 761
		private Matrix4x4 _baseMatrix;

		// Token: 0x040002FA RID: 762
		private Assimp.Quaternion _rotCurrent;

		// Token: 0x040002FB RID: 763
		private bool _isInDiffView;

		// Token: 0x040002FC RID: 764
		private IContainer components;

		// Token: 0x040002FD RID: 765
		private Label label1;

		// Token: 0x040002FE RID: 766
		private TextBox textBoxTransX;

		// Token: 0x040002FF RID: 767
		private Label labelTranslationX;

		// Token: 0x04000300 RID: 768
		private Label labelTranslationY;

		// Token: 0x04000301 RID: 769
		private TextBox textBoxTransY;

		// Token: 0x04000302 RID: 770
		private Label labelTranslationZ;

		// Token: 0x04000303 RID: 771
		private TextBox textBoxTransZ;

		// Token: 0x04000304 RID: 772
		private Label label5;

		// Token: 0x04000305 RID: 773
		private Label labelScalingZ;

		// Token: 0x04000306 RID: 774
		private TextBox textBoxScaleZ;

		// Token: 0x04000307 RID: 775
		private Label labelScalingY;

		// Token: 0x04000308 RID: 776
		private TextBox textBoxScaleY;

		// Token: 0x04000309 RID: 777
		private Label labelScalingX;

		// Token: 0x0400030A RID: 778
		private TextBox textBoxScaleX;

		// Token: 0x0400030B RID: 779
		private Label labelRotationZ;

		// Token: 0x0400030C RID: 780
		private TextBox textBoxRotZ;

		// Token: 0x0400030D RID: 781
		private Label labelRotationY;

		// Token: 0x0400030E RID: 782
		private TextBox textBoxRotY;

		// Token: 0x0400030F RID: 783
		private Label labelRotationX;

		// Token: 0x04000310 RID: 784
		private TextBox textBoxRotX;

		// Token: 0x04000311 RID: 785
		private Label label12;

		// Token: 0x04000312 RID: 786
		private ComboBox comboBoxRotMode;

		// Token: 0x04000313 RID: 787
		private TextBox textBoxRotW;

		// Token: 0x04000314 RID: 788
		private Label labelRotationW;

		// Token: 0x04000315 RID: 789
		private CheckBox checkBoxNonStandard;

		// Token: 0x02000073 RID: 115
		private enum RotationMode
		{
			// Token: 0x04000317 RID: 791
			EulerXyzDegrees,
			// Token: 0x04000318 RID: 792
			EulerXyzRadians,
			// Token: 0x04000319 RID: 793
			Quaternion
		}
	}
}
