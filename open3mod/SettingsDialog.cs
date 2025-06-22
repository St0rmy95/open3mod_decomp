using System;
using System.Collections.Specialized;
using System.ComponentModel;
using System.Diagnostics;
using System.Drawing;
using System.Linq;
using System.Windows.Forms;
using CoreSettings;

namespace open3mod
{
	// Token: 0x02000052 RID: 82
	public partial class SettingsDialog : Form
	{
		// Token: 0x060002E6 RID: 742 RVA: 0x00017FE0 File Offset: 0x000161E0
		public SettingsDialog()
		{
			this.InitializeComponent();
			this._gSettings = GraphicsSettings.Default;
			this.InitTexResolution();
			this.InitTexFilter();
			this.InitMultiSampling();
			this.InitLightingQuality();
			this.InitRenderingBackend();
			if (CoreSettings.Default.AdditionalTextureFolders != null)
			{
				this.folderSetDisplaySearchPaths.Folders = CoreSettings.Default.AdditionalTextureFolders.Cast<string>().ToArray<string>();
			}
			this.folderSetDisplaySearchPaths.Change += delegate(object sender)
			{
				if (CoreSettings.Default.AdditionalTextureFolders == null)
				{
					CoreSettings.Default.AdditionalTextureFolders = new StringCollection();
				}
				StringCollection additionalTextureFolders = CoreSettings.Default.AdditionalTextureFolders;
				additionalTextureFolders.Clear();
				foreach (string value in this.folderSetDisplaySearchPaths.Folders)
				{
					additionalTextureFolders.Add(value);
				}
			};
		}

		// Token: 0x1700008C RID: 140
		// (set) Token: 0x060002E7 RID: 743 RVA: 0x0001806B File Offset: 0x0001626B
		public MainWindow Main
		{
			set
			{
				this._main = value;
			}
		}

		// Token: 0x060002E8 RID: 744 RVA: 0x00018074 File Offset: 0x00016274
		private void OnOk(object sender, EventArgs e)
		{
			this._gSettings.Save();
			if (this._main == null)
			{
				base.Close();
				return;
			}
			this._main.CloseSettingsDialog();
		}

		// Token: 0x060002E9 RID: 745 RVA: 0x0001809C File Offset: 0x0001629C
		private void InitTexResolution()
		{
			int texQualityBias = this._gSettings.TexQualityBias;
			if (texQualityBias == 0)
			{
				this.comboBoxTexResolution.SelectedIndex = 0;
				return;
			}
			if (texQualityBias == 1)
			{
				this.comboBoxTexResolution.SelectedIndex = 1;
				return;
			}
			if (texQualityBias > 1)
			{
				this.comboBoxTexResolution.SelectedIndex = 2;
			}
		}

		// Token: 0x060002EA RID: 746 RVA: 0x000180E8 File Offset: 0x000162E8
		private void OnChangeTextureResolution(object sender, EventArgs e)
		{
			switch (this.comboBoxTexResolution.SelectedIndex)
			{
			case 0:
				this._gSettings.TexQualityBias = 0;
				break;
			case 1:
				this._gSettings.TexQualityBias = 1;
				break;
			case 2:
				this._gSettings.TexQualityBias = 3;
				break;
			}
			if (this._main == null)
			{
				return;
			}
			foreach (Scene scene in this._main.UiState.ActiveScenes())
			{
				scene.RequestReuploadTextures();
			}
		}

		// Token: 0x060002EB RID: 747 RVA: 0x00018190 File Offset: 0x00016390
		private void InitTexFilter()
		{
			this.comboBoxSetTextureFilter.SelectedIndex = this._gSettings.TextureFilter;
		}

		// Token: 0x060002EC RID: 748 RVA: 0x000181A8 File Offset: 0x000163A8
		private void OnChangeTextureFilter(object sender, EventArgs e)
		{
			this._gSettings.TextureFilter = this.comboBoxSetTextureFilter.SelectedIndex;
			if (this._main == null)
			{
				return;
			}
			foreach (Scene scene in this._main.UiState.ActiveScenes())
			{
				scene.RequestReconfigureTextures();
			}
		}

		// Token: 0x060002ED RID: 749 RVA: 0x00018220 File Offset: 0x00016420
		private void OnChangeMipSettings(object sender, EventArgs e)
		{
			foreach (Scene scene in this._main.UiState.ActiveScenes())
			{
				scene.RequestReconfigureTextures();
			}
		}

		// Token: 0x060002EE RID: 750 RVA: 0x00018278 File Offset: 0x00016478
		private void InitMultiSampling()
		{
			this.comboBoxSetMultiSampling.SelectedIndex = this._gSettings.MultiSampling;
		}

		// Token: 0x060002EF RID: 751 RVA: 0x00018290 File Offset: 0x00016490
		private void OnChangeMultiSamplingMode(object sender, EventArgs e)
		{
			if (this._gSettings.MultiSampling != this.comboBoxSetMultiSampling.SelectedIndex)
			{
				this._gSettings.MultiSampling = this.comboBoxSetMultiSampling.SelectedIndex;
				this.labelPleaseRestart.Visible = true;
			}
		}

		// Token: 0x060002F0 RID: 752 RVA: 0x000182CC File Offset: 0x000164CC
		private void InitLightingQuality()
		{
			this.comboBoxSetLightingMode.SelectedIndex = this._gSettings.LightingQuality;
		}

		// Token: 0x060002F1 RID: 753 RVA: 0x000182E4 File Offset: 0x000164E4
		private void InitRenderingBackend()
		{
			this.comboBoxSetBackend.SelectedIndex = this._gSettings.RenderingBackend;
		}

		// Token: 0x060002F2 RID: 754 RVA: 0x000182FC File Offset: 0x000164FC
		private void OnChangeRenderingBackend(object sender, EventArgs e)
		{
			this._gSettings.RenderingBackend = this.comboBoxSetBackend.SelectedIndex;
			if (this._main == null)
			{
				return;
			}
			foreach (Scene scene in this._main.UiState.ActiveScenes())
			{
				scene.RecreateRenderingBackend();
			}
		}

		// Token: 0x060002F3 RID: 755 RVA: 0x00018374 File Offset: 0x00016574
		private void checkBoxBFCulling_CheckedChanged(object sender, EventArgs e)
		{
			foreach (Scene scene in this._main.UiState.ActiveScenes())
			{
				scene.RequestRenderRefresh();
			}
		}

		// Token: 0x060002F4 RID: 756 RVA: 0x000183CC File Offset: 0x000165CC
		private void checkBoxGenerateTangentSpace_CheckedChanged(object sender, EventArgs e)
		{
		}

		// Token: 0x060002F5 RID: 757 RVA: 0x000183CE File Offset: 0x000165CE
		private void checkBoxComputeNormals_CheckedChanged(object sender, EventArgs e)
		{
		}

		// Token: 0x060002F6 RID: 758 RVA: 0x000183D0 File Offset: 0x000165D0
		private void OnLMWebsite(object sender, LinkLabelLinkClickedEventArgs e)
		{
			Process.Start("http://www.leapmotion.com");
		}

		// Token: 0x0400023C RID: 572
		private GraphicsSettings _gSettings;

		// Token: 0x0400023D RID: 573
		private MainWindow _main;
	}
}
