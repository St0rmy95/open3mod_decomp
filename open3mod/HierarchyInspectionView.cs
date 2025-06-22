using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Globalization;
using System.Linq;
using System.Windows.Forms;
using Assimp;

namespace open3mod
{
	// Token: 0x02000022 RID: 34
	public sealed class HierarchyInspectionView : UserControl
	{
		// Token: 0x06000117 RID: 279 RVA: 0x00009994 File Offset: 0x00007B94
		public HierarchyInspectionView(Scene scene, TabPage tabPageHierarchy)
		{
			this._filterByMesh = new Dictionary<Node, List<Mesh>>();
			this.Anchor = (AnchorStyles.Top | AnchorStyles.Left | AnchorStyles.Right);
			this.Dock = DockStyle.Fill;
			this.InitializeComponent();
			this._searchInfoText = this.textBoxFilter.Text;
			this._searchInfoColor = this.textBoxFilter.ForeColor;
			this._scene = scene;
			tabPageHierarchy.Controls.Add(this);
			this._hidden = new Dictionary<Node, TreeNode>();
			this._nodePurposes = new Dictionary<Node, NodePurpose>();
			this.labelHitCount.BackColor = HierarchyInspectionView.PositiveBackColor;
			this.nodeInfoPopup.Owner = this;
			this.meshInfoPopup.Owner = this;
			this.HidePopups();
			this.AddNodes();
			this.CountMeshes();
			this.UpdateStatistics();
		}

		// Token: 0x06000118 RID: 280 RVA: 0x00009A68 File Offset: 0x00007C68
		private void UpdateStatistics()
		{
			this.labelNodeStats.Text = string.Format("Showing {0} of {1} nodes ({2} meshes, {3} instances)", new object[]
			{
				this.CountVisible,
				this.CountNodes,
				this.CountVisibleMeshes,
				this.CountVisibleInstancedMeshes
			});
		}

		// Token: 0x1700004B RID: 75
		// (get) Token: 0x06000119 RID: 281 RVA: 0x00009ACA File Offset: 0x00007CCA
		public int CountVisible
		{
			get
			{
				return this._visibleNodes;
			}
		}

		// Token: 0x1700004C RID: 76
		// (get) Token: 0x0600011A RID: 282 RVA: 0x00009AD2 File Offset: 0x00007CD2
		public int CountNodes
		{
			get
			{
				return this._nodeCount;
			}
		}

		// Token: 0x1700004D RID: 77
		// (get) Token: 0x0600011B RID: 283 RVA: 0x00009ADA File Offset: 0x00007CDA
		public int CountVisibleMeshes
		{
			get
			{
				return this._visibleMeshes;
			}
		}

		// Token: 0x1700004E RID: 78
		// (get) Token: 0x0600011C RID: 284 RVA: 0x00009AE2 File Offset: 0x00007CE2
		public int CountVisibleInstancedMeshes
		{
			get
			{
				return this._visibleInstancedMeshes;
			}
		}

		// Token: 0x0600011D RID: 285 RVA: 0x00009AF4 File Offset: 0x00007CF4
		private void CountMeshes()
		{
			List<int> list = new List<int>(this._scene.Raw.MeshCount);
			for (int j = 0; j < this._scene.Raw.MeshCount; j++)
			{
				list.Add(0);
			}
			this.CountMeshes(this._scene.Raw.RootNode, list);
			this._visibleInstancedMeshes = (this._instancedMeshCountFullScene = list.Sum());
			this._visibleMeshes = (this._meshCountFullScene = list.Count((int i) => i != 0));
		}

		// Token: 0x0600011E RID: 286 RVA: 0x00009B98 File Offset: 0x00007D98
		private void CountMeshes(Node node, IList<int> counters)
		{
			if (node.Children != null)
			{
				foreach (Node node2 in node.Children)
				{
					this.CountMeshes(node2, counters);
				}
			}
			if (node.MeshIndices != null)
			{
				foreach (int num in node.MeshIndices)
				{
					int index;
					counters[index = num] = counters[index] + 1;
				}
			}
		}

		// Token: 0x0600011F RID: 287 RVA: 0x00009C4C File Offset: 0x00007E4C
		private void AddNodes()
		{
			this._tree.BeginUpdate();
			this.AddNodes(this._scene.Raw.RootNode, null, 0);
			this._visibleNodes = this._nodeCount;
			this._tree.EndUpdate();
		}

		// Token: 0x06000120 RID: 288 RVA: 0x00009C8C File Offset: 0x00007E8C
		private bool AddNodes(Node node, TreeNode uiNode, int level)
		{
			NodePurpose nodePurpose = NodePurpose.GenericMeshHolder;
			bool flag = false;
			if ((node.Name.StartsWith("<") && node.Name.EndsWith(">")) || level == 0)
			{
				nodePurpose = NodePurpose.ImporterGenerated;
			}
			else
			{
				for (int i = 0; i < this._scene.Raw.CameraCount; i++)
				{
					if (this._scene.Raw.Cameras[i].Name == node.Name)
					{
						nodePurpose = NodePurpose.Camera;
						break;
					}
				}
				if (nodePurpose == NodePurpose.GenericMeshHolder)
				{
					for (int j = 0; j < this._scene.Raw.LightCount; j++)
					{
						if (this._scene.Raw.Lights[j].Name == node.Name)
						{
							nodePurpose = NodePurpose.Light;
							break;
						}
					}
				}
				if (nodePurpose == NodePurpose.GenericMeshHolder)
				{
					flag = (node.MeshCount == 0);
				}
			}
			TreeNode treeNode = new TreeNode(node.Name)
			{
				Tag = node,
				ContextMenuStrip = this.contextMenuStripTreeNode
			};
			if (uiNode == null)
			{
				this._tree.Nodes.Add(treeNode);
				this.SetPivotNode(treeNode);
			}
			else
			{
				uiNode.Nodes.Add(treeNode);
			}
			this._nodeCount++;
			if (node.Children != null)
			{
				foreach (Node node2 in node.Children)
				{
					flag = (this.AddNodes(node2, treeNode, level + 1) && flag);
				}
			}
			if (node.MeshCount != 0)
			{
				foreach (int num in node.MeshIndices)
				{
					this.AddMeshNode(node, this._scene.Raw.Meshes[num], num, treeNode);
				}
			}
			if (flag)
			{
				nodePurpose = NodePurpose.Joint;
			}
			this._nodePurposes.Add(node, nodePurpose);
			int selectedImageIndex = (int)nodePurpose;
			if (nodePurpose == NodePurpose.Light || nodePurpose == NodePurpose.Camera)
			{
				selectedImageIndex = 1;
			}
			treeNode.ImageIndex = (treeNode.SelectedImageIndex = selectedImageIndex);
			if (level < 4)
			{
				treeNode.Expand();
			}
			return flag;
		}

		// Token: 0x06000121 RID: 289 RVA: 0x00009ED0 File Offset: 0x000080D0
		private void AddMeshNode(Node owner, Mesh mesh, int id, TreeNode uiNode)
		{
			string text = "Mesh " + ((!string.IsNullOrEmpty(mesh.Name)) ? ("\"" + mesh.Name + "\"") : id.ToString(CultureInfo.InvariantCulture));
			TreeNode node = new TreeNode(text)
			{
				Tag = new KeyValuePair<Node, Mesh>(owner, mesh),
				ImageIndex = 3,
				SelectedImageIndex = 3,
				ContextMenuStrip = this.contextMenuStripMesh
			};
			uiNode.Nodes.Add(node);
		}

		// Token: 0x06000122 RID: 290 RVA: 0x00009F64 File Offset: 0x00008164
		private void UpdateFilters(TreeNode hoverNode = null)
		{
			TreeNode treeNode = hoverNode ?? this._tree.SelectedNode;
			object obj = (treeNode == null) ? this._scene.Raw.RootNode : treeNode.Tag;
			this._filterByMesh.Clear();
			bool skeletonVisibleOverride = false;
			if (obj == this._scene.Raw.RootNode && !this.HasPermanentlyHiddenNodes)
			{
				this._scene.SetVisibleNodes(null);
				this._visibleNodes = this._nodeCount;
				this._visibleMeshes = this._meshCountFullScene;
				this._visibleInstancedMeshes = this._instancedMeshCountFullScene;
				this.HidePopups();
				return;
			}
			Node node = obj as Node;
			if (node != null && !this.IsNodePermanentlyHidden(node))
			{
				List<int> list = new List<int>(this._scene.Raw.MeshCount);
				for (int j = 0; j < this._scene.Raw.MeshCount; j++)
				{
					list.Add(0);
				}
				this.AddNodeToSet(this._filterByMesh, node);
				this.CountMeshes(node, list);
				this._visibleInstancedMeshes = list.Sum();
				this._visibleMeshes = list.Count((int i) => i != 0);
				if (treeNode != null && this.GetNodePurpose(node) == NodePurpose.Joint)
				{
					skeletonVisibleOverride = true;
				}
				if (treeNode != null)
				{
					this.PopulateNodeInfoPopup(treeNode);
					if (HierarchyInspectionView._nodeDiag != null)
					{
						this.SetNodeDetailDialogInfo(node);
					}
				}
				else
				{
					this.HidePopups();
				}
			}
			else if (obj is KeyValuePair<Node, Mesh>)
			{
				KeyValuePair<Node, Mesh> keyValuePair = (KeyValuePair<Node, Mesh>)obj;
				List<Mesh> value = new List<Mesh>
				{
					keyValuePair.Value
				};
				this._filterByMesh.Add(keyValuePair.Key, value);
				this._visibleMeshes = 1;
				this._visibleInstancedMeshes = 1;
				this.PopulateMeshInfoPopup(treeNode);
				if (HierarchyInspectionView._meshDiag != null && treeNode != null)
				{
					this.SetMeshDetailDialogInfo(keyValuePair.Value, treeNode.Text);
				}
			}
			else
			{
				this.HidePopups();
			}
			this._scene.SetVisibleNodes(this._filterByMesh);
			this._visibleNodes = this._filterByMesh.Count;
			this.UpdateStatistics();
			this._scene.SetSkeletonVisibleOverride(skeletonVisibleOverride);
		}

		// Token: 0x06000123 RID: 291 RVA: 0x0000A180 File Offset: 0x00008380
		private void HidePopups()
		{
			this.nodeInfoPopup.Visible = false;
			this.meshInfoPopup.Visible = false;
		}

		// Token: 0x06000124 RID: 292 RVA: 0x0000A19C File Offset: 0x0000839C
		private void PopulateMeshInfoPopup(TreeNode node)
		{
			bool flag = this.nodeInfoPopup.Visible || this.meshInfoPopup.Visible;
			if (this.nodeInfoPopup.Visible)
			{
				this.meshInfoPopup.Location = this.nodeInfoPopup.Location;
			}
			if (this._tree.Width - node.Bounds.Right < 80)
			{
				this.meshInfoPopup.Visible = false;
				this.nodeInfoPopup.Visible = false;
				return;
			}
			this.meshInfoPopup.Visible = true;
			this.nodeInfoPopup.Visible = false;
			if (flag)
			{
				this.AnimatePopup(node.Bounds.Top);
			}
			else
			{
				Point location = this.meshInfoPopup.Location;
				location.Y = node.Bounds.Top;
				this.meshInfoPopup.Location = location;
			}
			this.meshInfoPopup.Populate(((KeyValuePair<Node, Mesh>)node.Tag).Value);
		}

		// Token: 0x06000125 RID: 293 RVA: 0x0000A2A0 File Offset: 0x000084A0
		private void PopulateNodeInfoPopup(TreeNode node)
		{
			bool flag = this.nodeInfoPopup.Visible || this.meshInfoPopup.Visible;
			if (this._tree.Width - node.Bounds.Right < 80)
			{
				this.meshInfoPopup.Visible = false;
				this.nodeInfoPopup.Visible = false;
				return;
			}
			if (this.meshInfoPopup.Visible)
			{
				this.nodeInfoPopup.Location = this.meshInfoPopup.Location;
			}
			this.meshInfoPopup.Visible = false;
			this.nodeInfoPopup.Visible = true;
			if (flag)
			{
				this.AnimatePopup(node.Bounds.Top);
			}
			else
			{
				Point location = this.nodeInfoPopup.Location;
				location.Y = node.Bounds.Top;
				this.nodeInfoPopup.Location = location;
			}
			this.nodeInfoPopup.Populate(this._scene.Raw, (Node)node.Tag, this.GetNodePurpose((Node)node.Tag));
		}

		// Token: 0x06000126 RID: 294 RVA: 0x0000A43C File Offset: 0x0000863C
		private void AnimatePopup(int targetLocY)
		{
			Control control = this.nodeInfoPopup.Visible ? this.nodeInfoPopup : this.meshInfoPopup;
			this._targetLocY = targetLocY;
			this._oldLocY = control.Location.Y;
			this._popupAnimFramesRemaining = 5;
			if (this._popupAnimTimer == null)
			{
				this._popupAnimTimer = new Timer
				{
					Interval = 30
				};
				this._popupAnimTimer.Tick += delegate(object sender, EventArgs args)
				{
					Control control2 = this.nodeInfoPopup.Visible ? this.nodeInfoPopup : this.meshInfoPopup;
					this._popupAnimFramesRemaining--;
					Point location = control2.Location;
					location.Y = this._targetLocY - (int)((double)(this._targetLocY - this._oldLocY) * ((double)this._popupAnimFramesRemaining / 5.0));
					control2.Location = location;
					if (this._popupAnimFramesRemaining == 0)
					{
						this._popupAnimTimer.Stop();
					}
				};
			}
			this._popupAnimTimer.Start();
		}

		// Token: 0x06000127 RID: 295 RVA: 0x0000A4D8 File Offset: 0x000086D8
		private void SetMeshDetailDialogInfo(Mesh mesh, string text)
		{
			if (HierarchyInspectionView._meshDiag == null)
			{
				HierarchyInspectionView._meshDiag = new MeshDetailsDialog();
				HierarchyInspectionView._meshDiag.FormClosed += delegate(object o, FormClosedEventArgs args)
				{
					HierarchyInspectionView._meshDiag = null;
				};
				HierarchyInspectionView._meshDiag.Show();
			}
			else
			{
				HierarchyInspectionView._meshDiag.BringToFront();
			}
			HierarchyInspectionView._meshDiag.SetMesh(base.FindForm() as MainWindow, mesh, text);
		}

		// Token: 0x06000128 RID: 296 RVA: 0x0000A554 File Offset: 0x00008754
		private void SetNodeDetailDialogInfo(Node node)
		{
			if (HierarchyInspectionView._nodeDiag == null)
			{
				HierarchyInspectionView._nodeDiag = new NodeItemsDialog();
				HierarchyInspectionView._nodeDiag.FormClosed += delegate(object o, FormClosedEventArgs args)
				{
					HierarchyInspectionView._nodeDiag = null;
				};
				HierarchyInspectionView._nodeDiag.Show();
			}
			else
			{
				HierarchyInspectionView._nodeDiag.BringToFront();
			}
			HierarchyInspectionView._nodeDiag.SetNode(base.FindForm() as MainWindow, this._scene, node);
		}

		// Token: 0x06000129 RID: 297 RVA: 0x0000A5CB File Offset: 0x000087CB
		private NodePurpose GetNodePurpose(Node node)
		{
			return this._nodePurposes[node];
		}

		// Token: 0x0600012A RID: 298 RVA: 0x0000A5DC File Offset: 0x000087DC
		private void UpdateTextSearch()
		{
			if (this._searchText != "")
			{
				List<TreeNode> list = new List<TreeNode>();
				this.UpdateHighlighting(this._tree.Nodes[0], list);
				if (list.Count > 0)
				{
					list[0].EnsureVisible();
					this._hitNodes = list;
					this._hitNodeCursor = 0;
				}
				else
				{
					this._hitNodes = null;
					this._hitNodeCursor = -1;
				}
				this.labelHitCount.Text = string.Format("{0} hit{1}", list.Count.ToString(CultureInfo.InvariantCulture), (list.Count == 1) ? "" : "s");
				return;
			}
			this.ResetHighlighting(this._tree.Nodes[0]);
			this.labelHitCount.Text = "";
			this._hitNodes = null;
			this._hitNodeCursor = -1;
		}

		// Token: 0x0600012B RID: 299 RVA: 0x0000A6C4 File Offset: 0x000088C4
		private void ResetHighlighting(TreeNode n)
		{
			n.BackColor = HierarchyInspectionView.DefaultBackColor;
			for (int i = 0; i < n.Nodes.Count; i++)
			{
				this.ResetHighlighting(n.Nodes[i]);
			}
		}

		// Token: 0x0600012C RID: 300 RVA: 0x0000A704 File Offset: 0x00008904
		private void UpdateHighlighting(TreeNode n, List<TreeNode> searchHitNodes = null)
		{
			if (n.Tag != null)
			{
				Node node = n.Tag as Node;
				if (node != null)
				{
					if (node.Name.ToLower().Contains(this._searchText))
					{
						n.BackColor = HierarchyInspectionView.PositiveBackColor;
						if (searchHitNodes != null)
						{
							searchHitNodes.Add(n);
						}
					}
					else
					{
						n.BackColor = HierarchyInspectionView.DefaultBackColor;
					}
				}
				else
				{
					KeyValuePair<Node, Mesh> keyValuePair = (KeyValuePair<Node, Mesh>)n.Tag;
					if (keyValuePair.Value.Name.Length > 0 && keyValuePair.Value.Name.ToLower().Contains(this._searchText))
					{
						n.BackColor = HierarchyInspectionView.PositiveBackColor;
						if (searchHitNodes != null)
						{
							searchHitNodes.Add(n);
						}
					}
					else
					{
						n.BackColor = HierarchyInspectionView.DefaultBackColor;
					}
				}
			}
			for (int i = 0; i < n.Nodes.Count; i++)
			{
				this.UpdateHighlighting(n.Nodes[i], searchHitNodes);
			}
		}

		// Token: 0x0600012D RID: 301 RVA: 0x0000A7F0 File Offset: 0x000089F0
		private void AddNodeToSet(Dictionary<Node, List<Mesh>> filter, Node itemAsNode)
		{
			if (!this.IsNodePermanentlyHidden(itemAsNode))
			{
				filter.Add(itemAsNode, null);
			}
			if (itemAsNode.Children == null)
			{
				return;
			}
			foreach (Node itemAsNode2 in itemAsNode.Children)
			{
				this.AddNodeToSet(filter, itemAsNode2);
			}
		}

		// Token: 0x0600012E RID: 302 RVA: 0x0000A858 File Offset: 0x00008A58
		private void OnMouseLeave(object sender, EventArgs e)
		{
			this.UpdateFilters(null);
		}

		// Token: 0x0600012F RID: 303 RVA: 0x0000A861 File Offset: 0x00008A61
		private void OnMouseEnter(object sender, EventArgs e)
		{
		}

		// Token: 0x06000130 RID: 304 RVA: 0x0000A863 File Offset: 0x00008A63
		private void OnNodeHover(object sender, TreeNodeMouseHoverEventArgs e)
		{
			this.UpdateFilters(e.Node);
		}

		// Token: 0x06000131 RID: 305 RVA: 0x0000A871 File Offset: 0x00008A71
		private void AfterSelect(object sender, TreeViewEventArgs e)
		{
		}

		// Token: 0x06000132 RID: 306 RVA: 0x0000A874 File Offset: 0x00008A74
		private void OnChangeFilterText(object sender, EventArgs e)
		{
			string text = this._isInSearchMode ? this.textBoxFilter.Text.ToLower().Trim() : "";
			if (text != this._searchText)
			{
				this._searchText = text;
				this._searchLocked = false;
				this.UpdateTextSearch();
			}
		}

		// Token: 0x06000133 RID: 307 RVA: 0x0000A8C8 File Offset: 0x00008AC8
		private void OnClickSearchBox(object sender, EventArgs e)
		{
			if (this._isInSearchMode)
			{
				return;
			}
			this._isInSearchMode = true;
			this.textBoxFilter.ForeColor = Color.Black;
			this.textBoxFilter.Text = "";
		}

		// Token: 0x06000134 RID: 308 RVA: 0x0000A8FA File Offset: 0x00008AFA
		private void OnStopFocusingOnSearch(object sender, EventArgs e)
		{
			if (this._searchLocked)
			{
				return;
			}
			this._isInSearchMode = false;
			this.textBoxFilter.Text = this._searchInfoText;
			this.textBoxFilter.ForeColor = this._searchInfoColor;
		}

		// Token: 0x06000135 RID: 309 RVA: 0x0000A92E File Offset: 0x00008B2E
		private void OnKeyDown(object sender, KeyEventArgs e)
		{
			if (e.KeyCode == Keys.Return)
			{
				e.SuppressKeyPress = false;
			}
		}

		// Token: 0x06000136 RID: 310 RVA: 0x0000A944 File Offset: 0x00008B44
		private void OnKeyPress(object sender, KeyPressEventArgs e)
		{
			if (e.KeyChar == '\r')
			{
				e.Handled = true;
				if (sender == this.textBoxFilter && this._hitNodes != null)
				{
					this._searchLocked = true;
				}
				if (this._searchLocked)
				{
					this._hitNodes[(this._hitNodeCursor > 0) ? (this._hitNodeCursor - 1) : (this._hitNodes.Count - 1)].BackColor = HierarchyInspectionView.PositiveBackColor;
					this._tree.SelectedNode = this._hitNodes[this._hitNodeCursor];
					this._tree.SelectedNode.EnsureVisible();
					this._tree.SelectedNode.BackColor = HierarchyInspectionView.SearchIterateBackColor;
					this.UpdateFilters(null);
					this._hitNodeCursor = (this._hitNodeCursor + 1) % this._hitNodes.Count;
				}
			}
		}

		// Token: 0x06000137 RID: 311 RVA: 0x0000AA1F File Offset: 0x00008C1F
		private void AfterNodeDoubleClick(object sender, TreeNodeMouseClickEventArgs e)
		{
			this.ShowDetailsForTreeNode(e.Node);
		}

		// Token: 0x06000138 RID: 312 RVA: 0x0000AA30 File Offset: 0x00008C30
		private void ShowDetailsForTreeNode(TreeNode node)
		{
			if (node.Tag is KeyValuePair<Node, Mesh>)
			{
				Mesh value = ((KeyValuePair<Node, Mesh>)node.Tag).Value;
				this.SetMeshDetailDialogInfo(value, node.Text);
				return;
			}
			if (node.Tag is Node)
			{
				this.SetNodeDetailDialogInfo((Node)node.Tag);
			}
		}

		// Token: 0x06000139 RID: 313 RVA: 0x0000AA8C File Offset: 0x00008C8C
		private TreeNode GetTreeNodeForContextMenuEvent(object sender)
		{
			ContextMenuStrip contextMenuStrip = (sender as ContextMenuStrip) ?? ((ContextMenuStrip)((ToolStripMenuItem)sender).Owner);
			TreeView treeView = (TreeView)contextMenuStrip.SourceControl;
			return this._tree.GetNodeAt(treeView.PointToClient(contextMenuStrip.Location));
		}

		// Token: 0x0600013A RID: 314 RVA: 0x0000AADC File Offset: 0x00008CDC
		private void OnContextMenuShowDetails(object sender, EventArgs e)
		{
			TreeNode treeNodeForContextMenuEvent = this.GetTreeNodeForContextMenuEvent(sender);
			if (treeNodeForContextMenuEvent == null)
			{
				return;
			}
			this.ShowDetailsForTreeNode(treeNodeForContextMenuEvent);
		}

		// Token: 0x0600013B RID: 315 RVA: 0x0000AAFC File Offset: 0x00008CFC
		private void OnContextMenuHideNode(object sender, EventArgs e)
		{
			TreeNode treeNodeForContextMenuEvent = this.GetTreeNodeForContextMenuEvent(sender);
			if (treeNodeForContextMenuEvent == null)
			{
				return;
			}
			if (this.IsNodePermanentlyHidden((Node)treeNodeForContextMenuEvent.Tag))
			{
				this.UnhideSubhierarchy(treeNodeForContextMenuEvent);
				return;
			}
			this.HideSubhierarchyPermanently(treeNodeForContextMenuEvent);
		}

		// Token: 0x0600013C RID: 316 RVA: 0x0000AB38 File Offset: 0x00008D38
		private void OnContextMenuPivotNode(object sender, EventArgs e)
		{
			TreeNode treeNodeForContextMenuEvent = this.GetTreeNodeForContextMenuEvent(sender);
			if (treeNodeForContextMenuEvent == null)
			{
				return;
			}
			this.SetPivotNode(treeNodeForContextMenuEvent);
		}

		// Token: 0x0600013D RID: 317 RVA: 0x0000AB58 File Offset: 0x00008D58
		private void SetPivotNode(TreeNode node)
		{
			if (node == this._pivotNode)
			{
				return;
			}
			if (this._pivotNode != null)
			{
				this._pivotNode.Text = this._pivotNode.Text.Substring(0, this._pivotNode.Text.Length - " (pivot)".Length);
				this._pivotNode.ForeColor = Color.Black;
				this._pivotNode.NodeFont = node.TreeView.Font;
			}
			this._pivotNode = node;
			node.Text += " (pivot)";
			node.ForeColor = Color.DarkSlateGray;
			node.NodeFont = new Font(node.TreeView.Font, FontStyle.Italic);
			Node node2 = (Node)node.Tag;
			this._scene.SetPivot(node2, true);
		}

		// Token: 0x1700004F RID: 79
		// (get) Token: 0x0600013E RID: 318 RVA: 0x0000AC2C File Offset: 0x00008E2C
		private bool HasPermanentlyHiddenNodes
		{
			get
			{
				return this._hidden.Count != 0;
			}
		}

		// Token: 0x0600013F RID: 319 RVA: 0x0000AC3F File Offset: 0x00008E3F
		private bool IsNodePermanentlyHidden(Node node)
		{
			return this._hidden.ContainsKey(node);
		}

		// Token: 0x06000140 RID: 320 RVA: 0x0000AC50 File Offset: 0x00008E50
		private void HideSubhierarchyPermanently(TreeNode root)
		{
			Node key = (Node)root.Tag;
			this._hidden.Add(key, root);
			root.ImageIndex = (root.SelectedImageIndex = 4);
			root.Collapse();
			this.UpdateFilters(null);
			this.UpdateHiddenNodesInfoPanel();
		}

		// Token: 0x06000141 RID: 321 RVA: 0x0000AC9C File Offset: 0x00008E9C
		private void UpdateHiddenNodesInfoPanel()
		{
			if (this._hidden.Count == 0)
			{
				this.panelHiddenInfo.Visible = false;
				return;
			}
			this.panelHiddenInfo.Visible = true;
			this.labelHiddenCount.Text = this._hidden.Count.ToString(CultureInfo.InvariantCulture) + ((this._hidden.Count > 1) ? " items are permanently hidden" : " item is permanently hidden");
		}

		// Token: 0x06000142 RID: 322 RVA: 0x0000AD14 File Offset: 0x00008F14
		private void UnhideSubhierarchy(TreeNode root)
		{
			Node node = (Node)root.Tag;
			root.ImageIndex = (root.SelectedImageIndex = (int)this.GetNodePurpose(node));
			this._hidden.Remove(node);
			this.UpdateFilters(null);
			this.UpdateHiddenNodesInfoPanel();
		}

		// Token: 0x06000143 RID: 323 RVA: 0x0000AD60 File Offset: 0x00008F60
		private void OnMouseClick(object sender, MouseEventArgs e)
		{
			if (e.Button != MouseButtons.Right)
			{
				if (e.Button == MouseButtons.Left)
				{
					int num = (int)DateTime.Now.Subtract(this._lastMouseDown).TotalMilliseconds;
					this._preventExpand = (num < SystemInformation.DoubleClickTime);
					this._lastMouseDown = DateTime.Now;
				}
				return;
			}
			TreeNode nodeAt = this._tree.GetNodeAt(this._tree.PointToClient(e.Location));
			TreeNode selectedNode = this._tree.SelectedNode;
			if (nodeAt == null || nodeAt == selectedNode)
			{
				return;
			}
			this._tree.SelectedNode = nodeAt;
			this.UpdateFilters(null);
		}

		// Token: 0x06000144 RID: 324 RVA: 0x0000AE03 File Offset: 0x00009003
		private void BeforeExpand(object sender, TreeViewCancelEventArgs e)
		{
			e.Cancel = this._preventExpand;
			this._preventExpand = false;
		}

		// Token: 0x06000145 RID: 325 RVA: 0x0000AE18 File Offset: 0x00009018
		private void UnhideAllNodes(object sender, LinkLabelLinkClickedEventArgs e)
		{
			this.linkLabel1.LinkVisited = false;
			foreach (KeyValuePair<Node, TreeNode> keyValuePair in this._hidden)
			{
				keyValuePair.Value.ImageIndex = (keyValuePair.Value.SelectedImageIndex = (int)this.GetNodePurpose(keyValuePair.Key));
			}
			this._hidden.Clear();
			this.UpdateFilters(null);
			this.UpdateHiddenNodesInfoPanel();
		}

		// Token: 0x06000146 RID: 326 RVA: 0x0000AEB0 File Offset: 0x000090B0
		private void OpOpenNodeContextMenu(object sender, CancelEventArgs e)
		{
			ContextMenuStrip contextMenuStrip = (ContextMenuStrip)sender;
			TreeNode treeNodeForContextMenuEvent = this.GetTreeNodeForContextMenuEvent(sender);
			if (treeNodeForContextMenuEvent == null)
			{
				return;
			}
			Node node = treeNodeForContextMenuEvent.Tag as Node;
			contextMenuStrip.Items[1].Enabled = (this.GetNodePurpose(node) != NodePurpose.Joint);
			contextMenuStrip.Items[1].Text = (this.IsNodePermanentlyHidden(node) ? "Unhide" : "Hide");
		}

		// Token: 0x06000147 RID: 327 RVA: 0x0000AF20 File Offset: 0x00009120
		protected override void Dispose(bool disposing)
		{
			if (disposing && this.components != null)
			{
				this.components.Dispose();
			}
			base.Dispose(disposing);
		}

		// Token: 0x06000148 RID: 328 RVA: 0x0000AF40 File Offset: 0x00009140
		private void InitializeComponent()
		{
			this.components = new Container();
			ComponentResourceManager componentResourceManager = new ComponentResourceManager(typeof(HierarchyInspectionView));
			this.linkLabel1 = new LinkLabel();
			this.textBoxFilter = new TextBox();
			this._tree = new TreeView();
			this.imageListIconsHierarchy = new ImageList(this.components);
			this.labelNodeStats = new Label();
			this.labelHiddenCount = new Label();
			this.panelHiddenInfo = new Panel();
			this.panel1 = new Panel();
			this.labelHitCount = new Label();
			this.contextMenuStripTreeNode = new ContextMenuStrip(this.components);
			this.detailsToolStripMenuItem = new ToolStripMenuItem();
			this.hideToolStripMenuItem = new ToolStripMenuItem();
			this.contextMenuStripMesh = new ContextMenuStrip(this.components);
			this.detailsToolStripMenuItem1 = new ToolStripMenuItem();
			this.toolTip1 = new ToolTip(this.components);
			this.meshInfoPopup = new MeshInfoPopup();
			this.nodeInfoPopup = new NodeInfoPopup();
			this.centerPivotToolStripMenuItem = new ToolStripMenuItem();
			this.panelHiddenInfo.SuspendLayout();
			this.panel1.SuspendLayout();
			this.contextMenuStripTreeNode.SuspendLayout();
			this.contextMenuStripMesh.SuspendLayout();
			base.SuspendLayout();
			this.linkLabel1.Anchor = (AnchorStyles.Top | AnchorStyles.Right);
			this.linkLabel1.AutoSize = true;
			this.linkLabel1.Location = new Point(245, 3);
			this.linkLabel1.Name = "linkLabel1";
			this.linkLabel1.Size = new Size(54, 13);
			this.linkLabel1.TabIndex = 1;
			this.linkLabel1.TabStop = true;
			this.linkLabel1.Text = "Unhide all";
			this.linkLabel1.LinkClicked += this.UnhideAllNodes;
			this.textBoxFilter.Anchor = (AnchorStyles.Top | AnchorStyles.Bottom | AnchorStyles.Left | AnchorStyles.Right);
			this.textBoxFilter.ForeColor = SystemColors.ButtonShadow;
			this.textBoxFilter.Location = new Point(12, 7);
			this.textBoxFilter.Name = "textBoxFilter";
			this.textBoxFilter.Size = new Size(287, 20);
			this.textBoxFilter.TabIndex = 7;
			this.textBoxFilter.Text = "Type to search";
			this.toolTip1.SetToolTip(this.textBoxFilter, "Enter search text here. Press Enter to lock search and to cycle through results.");
			this.textBoxFilter.Click += this.OnClickSearchBox;
			this.textBoxFilter.TextChanged += this.OnChangeFilterText;
			this.textBoxFilter.KeyDown += this.OnKeyDown;
			this.textBoxFilter.KeyPress += this.OnKeyPress;
			this.textBoxFilter.Leave += this.OnStopFocusingOnSearch;
			this._tree.Anchor = (AnchorStyles.Top | AnchorStyles.Bottom | AnchorStyles.Left | AnchorStyles.Right);
			this._tree.BackColor = Color.White;
			this._tree.FullRowSelect = true;
			this._tree.HotTracking = true;
			this._tree.ImageIndex = 0;
			this._tree.ImageList = this.imageListIconsHierarchy;
			this._tree.Location = new Point(0, 33);
			this._tree.Name = "_tree";
			this._tree.SelectedImageIndex = 0;
			this._tree.ShowNodeToolTips = true;
			this._tree.Size = new Size(311, 573);
			this._tree.TabIndex = 5;
			this._tree.BeforeCollapse += this.BeforeExpand;
			this._tree.BeforeExpand += this.BeforeExpand;
			this._tree.NodeMouseHover += this.OnNodeHover;
			this._tree.AfterSelect += this.AfterSelect;
			this._tree.NodeMouseDoubleClick += this.AfterNodeDoubleClick;
			this._tree.KeyDown += this.OnKeyDown;
			this._tree.KeyPress += this.OnKeyPress;
			this._tree.MouseDown += this.OnMouseClick;
			this._tree.MouseEnter += this.OnMouseEnter;
			this._tree.MouseLeave += this.OnMouseLeave;
			this.imageListIconsHierarchy.ImageStream = (ImageListStreamer)componentResourceManager.GetObject("imageListIconsHierarchy.ImageStream");
			this.imageListIconsHierarchy.TransparentColor = Color.Transparent;
			this.imageListIconsHierarchy.Images.SetKeyName(0, "root.png");
			this.imageListIconsHierarchy.Images.SetKeyName(1, "normal.png");
			this.imageListIconsHierarchy.Images.SetKeyName(2, "joints.png");
			this.imageListIconsHierarchy.Images.SetKeyName(3, "mesh.png");
			this.imageListIconsHierarchy.Images.SetKeyName(4, "HierarchyIconHidden.png");
			this.labelNodeStats.AutoSize = true;
			this.labelNodeStats.Location = new Point(9, 5);
			this.labelNodeStats.Name = "labelNodeStats";
			this.labelNodeStats.Size = new Size(118, 13);
			this.labelNodeStats.TabIndex = 0;
			this.labelNodeStats.Text = "Showing m of n nodes. ";
			this.labelHiddenCount.AutoSize = true;
			this.labelHiddenCount.Location = new Point(10, 3);
			this.labelHiddenCount.Name = "labelHiddenCount";
			this.labelHiddenCount.Size = new Size(158, 13);
			this.labelHiddenCount.TabIndex = 0;
			this.labelHiddenCount.Text = "p nodes are permanently hidden";
			this.panelHiddenInfo.BackColor = Color.LemonChiffon;
			this.panelHiddenInfo.BorderStyle = BorderStyle.FixedSingle;
			this.panelHiddenInfo.Controls.Add(this.linkLabel1);
			this.panelHiddenInfo.Controls.Add(this.labelHiddenCount);
			this.panelHiddenInfo.Dock = DockStyle.Bottom;
			this.panelHiddenInfo.Location = new Point(0, 612);
			this.panelHiddenInfo.Margin = new Padding(3, 3, 3, 6);
			this.panelHiddenInfo.Name = "panelHiddenInfo";
			this.panelHiddenInfo.Size = new Size(311, 21);
			this.panelHiddenInfo.TabIndex = 9;
			this.panelHiddenInfo.Visible = false;
			this.panel1.BackColor = Color.Cornsilk;
			this.panel1.Controls.Add(this.labelNodeStats);
			this.panel1.Dock = DockStyle.Bottom;
			this.panel1.Location = new Point(0, 633);
			this.panel1.Name = "panel1";
			this.panel1.Size = new Size(311, 21);
			this.panel1.TabIndex = 8;
			this.labelHitCount.Anchor = (AnchorStyles.Top | AnchorStyles.Right);
			this.labelHitCount.AutoSize = true;
			this.labelHitCount.BackColor = Color.GreenYellow;
			this.labelHitCount.Location = new Point(247, 10);
			this.labelHitCount.Name = "labelHitCount";
			this.labelHitCount.Size = new Size(0, 13);
			this.labelHitCount.TabIndex = 10;
			this.contextMenuStripTreeNode.Items.AddRange(new ToolStripItem[]
			{
				this.detailsToolStripMenuItem,
				this.hideToolStripMenuItem,
				this.centerPivotToolStripMenuItem
			});
			this.contextMenuStripTreeNode.Name = "contextMenuStripTreeNode";
			this.contextMenuStripTreeNode.Size = new Size(153, 92);
			this.contextMenuStripTreeNode.Opening += this.OpOpenNodeContextMenu;
			this.detailsToolStripMenuItem.Name = "detailsToolStripMenuItem";
			this.detailsToolStripMenuItem.Size = new Size(152, 22);
			this.detailsToolStripMenuItem.Text = "Details";
			this.detailsToolStripMenuItem.Click += this.OnContextMenuShowDetails;
			this.hideToolStripMenuItem.Name = "hideToolStripMenuItem";
			this.hideToolStripMenuItem.Size = new Size(152, 22);
			this.hideToolStripMenuItem.Text = "Hide";
			this.hideToolStripMenuItem.Click += this.OnContextMenuHideNode;
			this.contextMenuStripMesh.Items.AddRange(new ToolStripItem[]
			{
				this.detailsToolStripMenuItem1
			});
			this.contextMenuStripMesh.Name = "contextMenuStripMesh";
			this.contextMenuStripMesh.Size = new Size(110, 26);
			this.contextMenuStripMesh.Click += this.OnContextMenuShowDetails;
			this.detailsToolStripMenuItem1.Name = "detailsToolStripMenuItem1";
			this.detailsToolStripMenuItem1.Size = new Size(109, 22);
			this.detailsToolStripMenuItem1.Text = "Details";
			this.detailsToolStripMenuItem1.Click += this.OnContextMenuShowDetails;
			this.meshInfoPopup.Anchor = AnchorStyles.Right;
			this.meshInfoPopup.Location = new Point(223, 335);
			this.meshInfoPopup.Name = "meshInfoPopup";
			this.meshInfoPopup.Size = new Size(88, 90);
			this.meshInfoPopup.TabIndex = 12;
			this.nodeInfoPopup.Anchor = AnchorStyles.Right;
			this.nodeInfoPopup.Location = new Point(223, 43);
			this.nodeInfoPopup.Name = "nodeInfoPopup";
			this.nodeInfoPopup.Size = new Size(88, 90);
			this.nodeInfoPopup.TabIndex = 11;
			this.centerPivotToolStripMenuItem.Name = "centerPivotToolStripMenuItem";
			this.centerPivotToolStripMenuItem.Size = new Size(152, 22);
			this.centerPivotToolStripMenuItem.Text = "Center/Pivot";
			this.centerPivotToolStripMenuItem.Click += this.OnContextMenuPivotNode;
			base.AutoScaleDimensions = new SizeF(6f, 13f);
			base.AutoScaleMode = AutoScaleMode.Font;
			this.BackColor = Color.CornflowerBlue;
			base.Controls.Add(this.meshInfoPopup);
			base.Controls.Add(this.nodeInfoPopup);
			base.Controls.Add(this.labelHitCount);
			base.Controls.Add(this.textBoxFilter);
			base.Controls.Add(this._tree);
			base.Controls.Add(this.panelHiddenInfo);
			base.Controls.Add(this.panel1);
			base.Margin = new Padding(0);
			base.Name = "HierarchyInspectionView";
			base.Size = new Size(311, 654);
			this.panelHiddenInfo.ResumeLayout(false);
			this.panelHiddenInfo.PerformLayout();
			this.panel1.ResumeLayout(false);
			this.panel1.PerformLayout();
			this.contextMenuStripTreeNode.ResumeLayout(false);
			this.contextMenuStripMesh.ResumeLayout(false);
			base.ResumeLayout(false);
			base.PerformLayout();
		}

		// Token: 0x040000AC RID: 172
		private const int AutoExpandLevels = 4;

		// Token: 0x040000AD RID: 173
		private readonly Scene _scene;

		// Token: 0x040000AE RID: 174
		private int _nodeCount;

		// Token: 0x040000AF RID: 175
		private readonly Dictionary<Node, List<Mesh>> _filterByMesh;

		// Token: 0x040000B0 RID: 176
		private new static Color DefaultBackColor = Color.White;

		// Token: 0x040000B1 RID: 177
		private static Color PositiveBackColor = Color.GreenYellow;

		// Token: 0x040000B2 RID: 178
		private static Color SearchIterateBackColor = Color.Gold;

		// Token: 0x040000B3 RID: 179
		private static Color NegativeBackColor = Color.OrangeRed;

		// Token: 0x040000B4 RID: 180
		private int _visibleNodes;

		// Token: 0x040000B5 RID: 181
		private int _visibleMeshes;

		// Token: 0x040000B6 RID: 182
		private int _visibleInstancedMeshes;

		// Token: 0x040000B7 RID: 183
		private int _meshCountFullScene;

		// Token: 0x040000B8 RID: 184
		private int _instancedMeshCountFullScene;

		// Token: 0x040000B9 RID: 185
		private string _searchText = "";

		// Token: 0x040000BA RID: 186
		private readonly string _searchInfoText;

		// Token: 0x040000BB RID: 187
		private readonly Color _searchInfoColor;

		// Token: 0x040000BC RID: 188
		private bool _isInSearchMode;

		// Token: 0x040000BD RID: 189
		private bool _searchLocked;

		// Token: 0x040000BE RID: 190
		private int _hitNodeCursor;

		// Token: 0x040000BF RID: 191
		private List<TreeNode> _hitNodes;

		// Token: 0x040000C0 RID: 192
		private readonly Dictionary<Node, TreeNode> _hidden;

		// Token: 0x040000C1 RID: 193
		private readonly Dictionary<Node, NodePurpose> _nodePurposes;

		// Token: 0x040000C2 RID: 194
		private static MeshDetailsDialog _meshDiag;

		// Token: 0x040000C3 RID: 195
		private static NodeItemsDialog _nodeDiag;

		// Token: 0x040000C4 RID: 196
		private int _targetLocY;

		// Token: 0x040000C5 RID: 197
		private int _popupAnimFramesRemaining;

		// Token: 0x040000C6 RID: 198
		private Timer _popupAnimTimer;

		// Token: 0x040000C7 RID: 199
		private int _oldLocY;

		// Token: 0x040000C8 RID: 200
		private TreeNode _pivotNode;

		// Token: 0x040000C9 RID: 201
		private bool _preventExpand;

		// Token: 0x040000CA RID: 202
		private DateTime _lastMouseDown = DateTime.Now;

		// Token: 0x040000CB RID: 203
		private IContainer components;

		// Token: 0x040000CC RID: 204
		private LinkLabel linkLabel1;

		// Token: 0x040000CD RID: 205
		private TextBox textBoxFilter;

		// Token: 0x040000CE RID: 206
		private TreeView _tree;

		// Token: 0x040000CF RID: 207
		private Label labelNodeStats;

		// Token: 0x040000D0 RID: 208
		private Label labelHiddenCount;

		// Token: 0x040000D1 RID: 209
		private Panel panelHiddenInfo;

		// Token: 0x040000D2 RID: 210
		private Panel panel1;

		// Token: 0x040000D3 RID: 211
		private Label labelHitCount;

		// Token: 0x040000D4 RID: 212
		private ImageList imageListIconsHierarchy;

		// Token: 0x040000D5 RID: 213
		private NodeInfoPopup nodeInfoPopup;

		// Token: 0x040000D6 RID: 214
		private MeshInfoPopup meshInfoPopup;

		// Token: 0x040000D7 RID: 215
		private ContextMenuStrip contextMenuStripTreeNode;

		// Token: 0x040000D8 RID: 216
		private ToolStripMenuItem detailsToolStripMenuItem;

		// Token: 0x040000D9 RID: 217
		private ToolStripMenuItem hideToolStripMenuItem;

		// Token: 0x040000DA RID: 218
		private ContextMenuStrip contextMenuStripMesh;

		// Token: 0x040000DB RID: 219
		private ToolStripMenuItem detailsToolStripMenuItem1;

		// Token: 0x040000DC RID: 220
		private ToolTip toolTip1;

		// Token: 0x040000DD RID: 221
		private ToolStripMenuItem centerPivotToolStripMenuItem;
	}
}
