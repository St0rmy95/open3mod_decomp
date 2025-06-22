using System;
using System.ComponentModel;
using System.Drawing;
using System.Windows.Forms;
using Assimp;

namespace open3mod
{
	// Token: 0x0200003B RID: 59
	public class NodeInfoPopup : UserControl
	{
		// Token: 0x0600022C RID: 556 RVA: 0x00012B1D File Offset: 0x00010D1D
		public NodeInfoPopup()
		{
			this.InitializeComponent();
		}

		// Token: 0x17000067 RID: 103
		// (set) Token: 0x0600022D RID: 557 RVA: 0x00012B2B File Offset: 0x00010D2B
		public HierarchyInspectionView Owner
		{
			set
			{
				this._owner = value;
			}
		}

		// Token: 0x0600022E RID: 558 RVA: 0x00012B34 File Offset: 0x00010D34
		public void Populate(Scene scene, Node node, NodePurpose purpose)
		{
			switch (purpose)
			{
			case NodePurpose.ImporterGenerated:
				this.labelCaption.Text = "Root";
				break;
			case NodePurpose.GenericMeshHolder:
				this.labelCaption.Text = "Node";
				break;
			case NodePurpose.Joint:
				this.labelCaption.Text = "Joint";
				break;
			case NodePurpose.Camera:
				this.labelCaption.Text = "Camera";
				break;
			case NodePurpose.Light:
				this.labelCaption.Text = "Light";
				break;
			}
			int num = 0;
			this.CountChildren(node, ref num);
			bool flag = false;
			int num2 = 0;
			while (num2 < scene.AnimationCount && !flag)
			{
				Animation animation = scene.Animations[num2];
				for (int i = 0; i < animation.NodeAnimationChannelCount; i++)
				{
					if (animation.NodeAnimationChannels[i].NodeName == node.Name)
					{
						flag = true;
						break;
					}
				}
				num2++;
			}
			this.labelInfo.Text = string.Format("{0} Children\n{1}", num, flag ? "Animated" : "Not animated");
		}

		// Token: 0x0600022F RID: 559 RVA: 0x00012C4C File Offset: 0x00010E4C
		private void CountChildren(Node node, ref int children)
		{
			children += node.ChildCount;
			for (int i = 0; i < node.ChildCount; i++)
			{
				this.CountChildren(node.Children[i], ref children);
			}
		}

		// Token: 0x06000230 RID: 560 RVA: 0x00012C88 File Offset: 0x00010E88
		protected override void Dispose(bool disposing)
		{
			if (disposing && this.components != null)
			{
				this.components.Dispose();
			}
			base.Dispose(disposing);
		}

		// Token: 0x06000231 RID: 561 RVA: 0x00012CA8 File Offset: 0x00010EA8
		private void InitializeComponent()
		{
			this.panel3 = new Panel();
			this.labelCaption = new Label();
			this.labelInfo = new Label();
			this.panel3.SuspendLayout();
			base.SuspendLayout();
			this.panel3.Anchor = (AnchorStyles.Top | AnchorStyles.Bottom | AnchorStyles.Left | AnchorStyles.Right);
			this.panel3.BackColor = Color.White;
			this.panel3.BorderStyle = BorderStyle.FixedSingle;
			this.panel3.Controls.Add(this.labelCaption);
			this.panel3.Controls.Add(this.labelInfo);
			this.panel3.Location = new Point(0, 3);
			this.panel3.Name = "panel3";
			this.panel3.Size = new Size(88, 84);
			this.panel3.TabIndex = 12;
			this.labelCaption.AutoSize = true;
			this.labelCaption.Font = new Font("Microsoft Sans Serif", 8.25f, FontStyle.Bold, GraphicsUnit.Point, 0);
			this.labelCaption.Location = new Point(8, 12);
			this.labelCaption.Name = "labelCaption";
			this.labelCaption.Size = new Size(37, 13);
			this.labelCaption.TabIndex = 12;
			this.labelCaption.Text = "Node";
			this.labelInfo.AutoSize = true;
			this.labelInfo.Location = new Point(8, 36);
			this.labelInfo.Name = "labelInfo";
			this.labelInfo.Size = new Size(70, 26);
			this.labelInfo.TabIndex = 0;
			this.labelInfo.Text = "32 Children \r\nNot animated\r\n";
			base.AutoScaleDimensions = new SizeF(6f, 13f);
			base.AutoScaleMode = AutoScaleMode.Font;
			base.Controls.Add(this.panel3);
			base.Name = "NodeInfoPopup";
			base.Size = new Size(88, 90);
			this.panel3.ResumeLayout(false);
			this.panel3.PerformLayout();
			base.ResumeLayout(false);
		}

		// Token: 0x040001C2 RID: 450
		private HierarchyInspectionView _owner;

		// Token: 0x040001C3 RID: 451
		private IContainer components;

		// Token: 0x040001C4 RID: 452
		private Panel panel3;

		// Token: 0x040001C5 RID: 453
		private Label labelInfo;

		// Token: 0x040001C6 RID: 454
		private Label labelCaption;
	}
}
