using System;
using System.ComponentModel;
using System.Drawing;
using System.Globalization;
using System.Windows.Forms;
using Assimp;
using OpenTK;

namespace open3mod
{
	// Token: 0x02000018 RID: 24
	public partial class NodeItemsDialog : Form
	{
		// Token: 0x060000DC RID: 220 RVA: 0x000067D0 File Offset: 0x000049D0
		public NodeItemsDialog()
		{
			this.InitializeComponent();
			this._timer = new Timer
			{
				Interval = 100
			};
			this._timer.Tick += this.TimerOnTick;
			if (this.checkBoxShowAnimated.Checked)
			{
				this._timer.Start();
			}
			this.UpdateCollapseState(true);
		}

		// Token: 0x060000DD RID: 221 RVA: 0x00006834 File Offset: 0x00004A34
		public void SetNode(MainWindow mainWindow, Scene scene, Node node)
		{
			this._node = node;
			this._scene = scene;
			Matrix4x4 transform = this._node.Transform;
			this.trafoMatrixViewControlLocal.SetMatrix(ref transform);
			Matrix4x4 b = Matrix4x4.Identity;
			for (Node node2 = node; node2 != null; node2 = node2.Parent)
			{
				Matrix4x4 transform2 = node2.Transform;
				transform2.Transpose();
				b = transform2 * b;
			}
			b.Transpose();
			this.trafoMatrixViewControlGlobal.SetMatrix(ref b);
			this.Text = node.Name + " - Details";
			this.labelMeshesDirect.Text = node.MeshCount.ToString(CultureInfo.InvariantCulture);
			this.labelChildrenDirect.Text = node.ChildCount.ToString(CultureInfo.InvariantCulture);
			int num = 0;
			int num2 = 0;
			this.CountMeshAndChildrenTotal(node, ref num, ref num2);
			this.labelMeshesTotal.Text = node.MeshCount.ToString(CultureInfo.InvariantCulture);
			this.labelChildrenTotal.Text = node.ChildCount.ToString(CultureInfo.InvariantCulture);
		}

		// Token: 0x060000DE RID: 222 RVA: 0x0000694C File Offset: 0x00004B4C
		private void CountMeshAndChildrenTotal(Node node, ref int meshTotal, ref int childTotal)
		{
			meshTotal += node.MeshCount;
			childTotal += node.ChildCount;
			for (int i = 0; i < node.ChildCount; i++)
			{
				this.CountMeshAndChildrenTotal(node.Children[i], ref meshTotal, ref childTotal);
			}
		}

		// Token: 0x060000DF RID: 223 RVA: 0x00006994 File Offset: 0x00004B94
		private void OnChangeAnimationState(object sender, EventArgs e)
		{
			if (this.checkBoxShowAnimated.Checked)
			{
				this._timer.Start();
				return;
			}
			this._timer.Stop();
			this.trafoMatrixViewControlGlobal.ResetAnimatedMatrix();
			this.trafoMatrixViewControlLocal.ResetAnimatedMatrix();
		}

		// Token: 0x060000E0 RID: 224 RVA: 0x000069D0 File Offset: 0x00004BD0
		private void TimerOnTick(object sender, EventArgs eventArgs)
		{
			if (this._scene == null)
			{
				return;
			}
			SceneAnimator sceneAnimator = this._scene.SceneAnimator;
			if (sceneAnimator.ActiveAnimation == -1)
			{
				this.trafoMatrixViewControlGlobal.ResetAnimatedMatrix();
				this.trafoMatrixViewControlLocal.ResetAnimatedMatrix();
				return;
			}
			Matrix4 matrix;
			sceneAnimator.GetGlobalTransform(this._node, out matrix);
			Matrix4x4 matrix4x;
			OpenTkToAssimp.FromMatrix(ref matrix, out matrix4x);
			this.trafoMatrixViewControlGlobal.SetAnimatedMatrix(ref matrix4x);
			sceneAnimator.GetLocalTransform(this._node, out matrix);
			OpenTkToAssimp.FromMatrix(ref matrix, out matrix4x);
			this.trafoMatrixViewControlLocal.SetAnimatedMatrix(ref matrix4x);
		}

		// Token: 0x060000E1 RID: 225 RVA: 0x00006A5A File Offset: 0x00004C5A
		private void OnToggleShowGlobalTrafo(object sender, EventArgs e)
		{
			this.UpdateCollapseState(false);
		}

		// Token: 0x060000E2 RID: 226 RVA: 0x00006A64 File Offset: 0x00004C64
		private void UpdateCollapseState(bool initial = false)
		{
			if (this.checkBoxShowGlobalTransformation.Checked)
			{
				this.checkBoxShowGlobalTransformation.Text = "Show Global Transformation ...";
				base.Height -= this.trafoMatrixViewControlGlobal.Height + 34;
				this.trafoMatrixViewControlGlobal.Visible = false;
				return;
			}
			if (!initial)
			{
				this.checkBoxShowGlobalTransformation.Text = "Hide Global Transformation ...";
				base.Height += this.trafoMatrixViewControlGlobal.Height + 34;
			}
			this.trafoMatrixViewControlGlobal.Visible = true;
		}

		// Token: 0x04000060 RID: 96
		private const int TimerUpdateInterval = 100;

		// Token: 0x04000061 RID: 97
		private Node _node;

		// Token: 0x04000062 RID: 98
		private readonly Timer _timer;

		// Token: 0x04000063 RID: 99
		private Scene _scene;
	}
}
