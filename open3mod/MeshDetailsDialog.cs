using System;
using System.ComponentModel;
using System.Drawing;
using System.Windows.Forms;
using Assimp;

namespace open3mod
{
	// Token: 0x02000017 RID: 23
	public partial class MeshDetailsDialog : Form
	{
		// Token: 0x060000D7 RID: 215 RVA: 0x00006007 File Offset: 0x00004207
		public MeshDetailsDialog()
		{
			this.InitializeComponent();
		}

		// Token: 0x060000D8 RID: 216 RVA: 0x00006018 File Offset: 0x00004218
		public void SetMesh(MainWindow host, Mesh mesh, string meshName)
		{
			this._mesh = mesh;
			this._host = host;
			this.labelVertexCount.Text = mesh.VertexCount + " Vertices";
			this.labelFaceCount.Text = mesh.FaceCount + " Faces";
			this.Text = meshName + " - Details";
			this.checkedListBoxPerFace.CheckOnClick = false;
			this.checkedListBoxPerFace.SetItemCheckState(0, mesh.PrimitiveType.HasFlag(PrimitiveType.Triangle) ? CheckState.Checked : CheckState.Unchecked);
			this.checkedListBoxPerFace.SetItemCheckState(1, mesh.PrimitiveType.HasFlag(PrimitiveType.Line) ? CheckState.Checked : CheckState.Unchecked);
			this.checkedListBoxPerFace.SetItemCheckState(2, mesh.PrimitiveType.HasFlag(PrimitiveType.Point) ? CheckState.Checked : CheckState.Unchecked);
			this.checkedListBoxPerVertex.CheckOnClick = false;
			this.checkedListBoxPerVertex.SetItemCheckState(0, CheckState.Checked);
			this.checkedListBoxPerVertex.SetItemCheckState(1, mesh.HasNormals ? CheckState.Checked : CheckState.Unchecked);
			this.checkedListBoxPerVertex.SetItemCheckState(2, mesh.HasTangentBasis ? CheckState.Checked : CheckState.Unchecked);
			for (int i = 0; i < 4; i++)
			{
				this.checkedListBoxPerVertex.SetItemCheckState(3 + i, mesh.HasTextureCoords(i) ? CheckState.Checked : CheckState.Unchecked);
			}
			for (int j = 0; j < 4; j++)
			{
				this.checkedListBoxPerVertex.SetItemCheckState(7 + j, mesh.HasVertexColors(j) ? CheckState.Checked : CheckState.Unchecked);
			}
			this.checkedListBoxPerVertex.SetItemCheckState(11, mesh.HasBones ? CheckState.Checked : CheckState.Unchecked);
		}

		// Token: 0x060000D9 RID: 217 RVA: 0x000061B8 File Offset: 0x000043B8
		private void OnJumpToMaterial(object sender, LinkLabelLinkClickedEventArgs e)
		{
			this.linkLabel1.LinkVisited = false;
			foreach (Tab tab in this._host.UiState.TabsWithActiveScenes())
			{
				Scene activeScene = tab.ActiveScene;
				for (int i = 0; i < activeScene.Raw.MeshCount; i++)
				{
					Mesh mesh = activeScene.Raw.Meshes[i];
					if (mesh == this._mesh)
					{
						Material material = activeScene.Raw.Materials[mesh.MaterialIndex];
						TabUiSkeleton tabUiSkeleton = this._host.UiForTab(tab);
						InspectionView inspector = tabUiSkeleton.GetInspector();
						inspector.Materials.SelectEntry(material);
						MaterialThumbnailControl materialControl = inspector.Materials.GetMaterialControl(material);
						inspector.OpenMaterialsTabAndScrollTo(materialControl);
					}
				}
			}
		}

		// Token: 0x04000056 RID: 86
		private Mesh _mesh;

		// Token: 0x04000057 RID: 87
		private MainWindow _host;
	}
}
