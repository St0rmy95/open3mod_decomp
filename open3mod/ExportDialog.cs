using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Globalization;
using System.IO;
using System.Threading;
using System.Windows.Forms;
using Assimp;

namespace open3mod
{
	// Token: 0x0200001D RID: 29
	public partial class ExportDialog : Form
	{
		// Token: 0x17000046 RID: 70
		// (get) Token: 0x060000EE RID: 238 RVA: 0x00007C78 File Offset: 0x00005E78
		public string SelectedFormatId
		{
			get
			{
				return this._formats[this.comboBoxExportFormats.SelectedIndex].FormatId;
			}
		}

		// Token: 0x17000047 RID: 71
		// (get) Token: 0x060000EF RID: 239 RVA: 0x00007C91 File Offset: 0x00005E91
		public ExportFormatDescription SelectedFormat
		{
			get
			{
				return this._formats[this.comboBoxExportFormats.SelectedIndex];
			}
		}

		// Token: 0x060000F0 RID: 240 RVA: 0x00007CDC File Offset: 0x00005EDC
		public ExportDialog(MainWindow main)
		{
			this._main = main;
			this.InitializeComponent();
			using (AssimpContext assimpContext = new AssimpContext())
			{
				this._formats = assimpContext.GetSupportedExportFormats();
				foreach (ExportFormatDescription exportFormatDescription in this._formats)
				{
					this.comboBoxExportFormats.Items.Add(exportFormatDescription.Description + "  (" + exportFormatDescription.FileExtension + ")");
				}
				this.comboBoxExportFormats.SelectedIndex = ExportSettings.Default.ExportFormatIndex;
				this.comboBoxExportFormats.SelectedIndexChanged += delegate(object s, EventArgs e)
				{
					ExportSettings.Default.ExportFormatIndex = this.comboBoxExportFormats.SelectedIndex;
					this.UpdateFileName(true);
				};
			}
			this.textBoxFileName.KeyPress += delegate(object s, KeyPressEventArgs e)
			{
				this._changedText = true;
			};
			this._main.SelectedTabChanged += delegate(Tab tab)
			{
				this.UpdateFileName(false);
				this.UpdateCaption();
			};
			this.UpdateFileName(false);
			this.UpdateCaption();
		}

		// Token: 0x060000F1 RID: 241 RVA: 0x00007DF8 File Offset: 0x00005FF8
		private void UpdateCaption()
		{
			string text = "Export ";
			Scene activeScene = this._main.UiState.ActiveTab.ActiveScene;
			if (activeScene != null)
			{
				text += Path.GetFileName(activeScene.File);
				this.buttonExportRun.Enabled = true;
			}
			else
			{
				this.buttonExportRun.Enabled = false;
				text += "<no scene currently selected>";
			}
			this.Text = text;
		}

		// Token: 0x060000F2 RID: 242 RVA: 0x00007E64 File Offset: 0x00006064
		private void UpdateFileName(bool extensionOnly = false)
		{
			string path = this.textBoxFileName.Text;
			if (!extensionOnly && !this._changedText)
			{
				Scene activeScene = this._main.UiState.ActiveTab.ActiveScene;
				if (activeScene != null)
				{
					path = Path.GetFileName(activeScene.File);
				}
			}
			this.textBoxFileName.Text = Path.ChangeExtension(path, this.SelectedFormat.FileExtension);
		}

		// Token: 0x060000F3 RID: 243 RVA: 0x00007EC9 File Offset: 0x000060C9
		private void buttonSelectFolder_Click(object sender, EventArgs e)
		{
			this.folderBrowserDialog.ShowDialog(this);
			this.textBoxPath.Text = this.folderBrowserDialog.SelectedPath;
		}

		// Token: 0x060000F4 RID: 244 RVA: 0x00007EF0 File Offset: 0x000060F0
		private void buttonExport(object sender, EventArgs e)
		{
			Scene activeScene = this._main.UiState.ActiveTab.ActiveScene;
			if (activeScene == null)
			{
				MessageBox.Show("No exportable scene selected");
				return;
			}
			this.DoExport(activeScene, this.SelectedFormatId);
		}

		// Token: 0x060000F5 RID: 245 RVA: 0x00008248 File Offset: 0x00006448
		private void DoExport(Scene scene, string id)
		{
			bool overwriteWithoutConfirmation = this.checkBoxNoOverwriteConfirm.Checked;
			string text = this.textBoxPath.Text.Trim();
			text = ((text.Length > 0) ? text : scene.BaseDir);
			string path = this.textBoxFileName.Text.Trim();
			string fullPath = Path.Combine(text, path);
			if (!overwriteWithoutConfirmation && Path.GetFullPath(fullPath) == Path.GetFullPath(scene.File) && MessageBox.Show("This will overwrite the current scene's source file. Continue?", "Warning", MessageBoxButtons.YesNo, MessageBoxIcon.Exclamation) == DialogResult.No)
			{
				this.PushLog("Canceled");
				return;
			}
			bool copyTextures = this.checkBoxCopyTexturesToSubfolder.Checked;
			bool @checked = this.checkBoxUseRelativeTexturePaths.Checked;
			bool checked2 = this.checkBoxIncludeAnimations.Checked;
			bool includeSceneHierarchy = this.checkBoxIncludeSceneHierarchy.Checked;
			Dictionary<string, string> textureCopyJobs = new Dictionary<string, string>();
			string text2 = this.textBoxCopyTexturesToFolder.Text;
			this.PushLog("*** Export: " + scene.File);
			if (copyTextures)
			{
				try
				{
					Directory.CreateDirectory(Path.Combine(text, text2));
				}
				catch (Exception)
				{
					this.PushLog("Failed to create texture destination directory " + Path.Combine(text, text2));
					return;
				}
			}
			this.progressBarExport.Style = ProgressBarStyle.Marquee;
			this.progressBarExport.MarqueeAnimationSpeed = 5;
			Scene sourceScene = new Scene();
			sourceScene.Textures = scene.Raw.Textures;
			sourceScene.SceneFlags = scene.Raw.SceneFlags;
			sourceScene.RootNode = scene.Raw.RootNode;
			sourceScene.Meshes = scene.Raw.Meshes;
			sourceScene.Lights = scene.Raw.Lights;
			sourceScene.Cameras = scene.Raw.Cameras;
			if (checked2)
			{
				sourceScene.Animations = scene.Raw.Animations;
			}
			HashSet<string> hashSet = new HashSet<string>();
			Dictionary<string, string> dictionary = new Dictionary<string, string>();
			this.PushLog("Locating all textures");
			foreach (string text3 in scene.TextureSet.GetTextureIds())
			{
				string text4 = text3;
				if (text4.StartsWith("*"))
				{
					this.PushLog("Ignoring embedded texture: " + text4);
				}
				else
				{
					string text5;
					try
					{
						TextureLoader.ObtainStream(text3, scene.BaseDir, out text5).Close();
					}
					catch (IOException)
					{
						this.PushLog("Failed to locate texture " + text3);
						goto IL_370;
					}
					if (copyTextures)
					{
						int num = 1;
						do
						{
							text4 = Path.Combine(text, Path.Combine(text2, Path.GetFileNameWithoutExtension(text5) + ((num == 1) ? "" : (num.ToString(CultureInfo.InvariantCulture) + "_")) + Path.GetExtension(text5)));
							num++;
						}
						while (hashSet.Contains(text4));
						hashSet.Add(text4);
					}
					if (@checked)
					{
						dictionary[text3] = ExportDialog.GetRelativePath(text + "\\", text4);
					}
					else
					{
						dictionary[text3] = text4;
					}
					textureCopyJobs[text5] = text4;
					this.PushLog("Texture " + text3 + " maps to " + dictionary[text3]);
				}
				IL_370:;
			}
			foreach (Material mat in scene.Raw.Materials)
			{
				sourceScene.Materials.Add(ExportDialog.CloneMaterial(mat, dictionary));
			}
			Thread thread = new Thread(delegate()
			{
				using (AssimpContext assimpContext = new AssimpContext())
				{
					this.PushLog("Exporting using Assimp to " + fullPath + ", using format id: " + id);
					bool result = assimpContext.ExportFile(sourceScene, fullPath, id, includeSceneHierarchy ? PostProcessSteps.None : PostProcessSteps.PreTransformVertices);
					this._main.BeginInvoke(new MethodInvoker(delegate()
					{
						this.progressBarExport.Style = ProgressBarStyle.Continuous;
						this.progressBarExport.MarqueeAnimationSpeed = 0;
						if (!result)
						{
							this.PushLog("Export failure");
							MessageBox.Show("Failed to export to " + fullPath, "Export error", MessageBoxButtons.OK, MessageBoxIcon.Hand);
							return;
						}
						if (copyTextures)
						{
							this.PushLog("Copying textures");
							foreach (KeyValuePair<string, string> keyValuePair in textureCopyJobs)
							{
								this.PushLog(" ... " + keyValuePair.Key + " -> " + keyValuePair.Value);
								try
								{
									File.Copy(keyValuePair.Key, keyValuePair.Value, false);
								}
								catch (IOException)
								{
									if (!File.Exists(keyValuePair.Value))
									{
										throw;
									}
									if (!overwriteWithoutConfirmation && MessageBox.Show("Texture " + keyValuePair.Value + " already exists. Overwrite?", "Overwrite Warning", MessageBoxButtons.YesNo, MessageBoxIcon.Exclamation) == DialogResult.No)
									{
										this.PushLog("Exists already, skipping");
									}
									else
									{
										this.PushLog("Exists already, overwriting");
										File.Copy(keyValuePair.Key, keyValuePair.Value, true);
									}
								}
								catch (Exception ex)
								{
									this.PushLog(ex.Message);
								}
							}
						}
						if (this.checkBoxOpenExportedFile.Checked)
						{
							this._main.AddTab(fullPath, true, false);
						}
						this.PushLog("Export completed");
					}));
				}
			});
			thread.Start();
		}

		// Token: 0x060000F6 RID: 246 RVA: 0x000086D8 File Offset: 0x000068D8
		private void PushLog(string message)
		{
			this._main.BeginInvoke(new MethodInvoker(delegate()
			{
				TextBox textBox = this.textBoxExportLog;
				textBox.Text = textBox.Text + message + Environment.NewLine;
				this.textBoxExportLog.SelectionStart = this.textBoxExportLog.TextLength;
				this.textBoxExportLog.ScrollToCaret();
			}));
		}

		// Token: 0x060000F7 RID: 247 RVA: 0x00008714 File Offset: 0x00006914
		private static Material CloneMaterial(Material mat, Dictionary<string, string> textureMapping)
		{
			Material material = new Material();
			foreach (MaterialProperty materialProperty in mat.GetAllProperties())
			{
				MaterialProperty materialProperty2 = materialProperty;
				if (materialProperty.PropertyType == PropertyType.String && textureMapping.ContainsKey(materialProperty.GetStringValue()))
				{
					materialProperty2 = new MaterialProperty
					{
						PropertyType = PropertyType.String,
						Name = materialProperty.Name
					};
					materialProperty2.TextureIndex = materialProperty.TextureIndex;
					materialProperty2.TextureType = materialProperty.TextureType;
					materialProperty2.SetStringValue(textureMapping[materialProperty.GetStringValue()]);
				}
				material.AddProperty(materialProperty2);
			}
			return material;
		}

		// Token: 0x060000F8 RID: 248 RVA: 0x000087B0 File Offset: 0x000069B0
		public static string GetRelativePath(string fromPath, string toPath)
		{
			Uri uri = new Uri(Path.GetFullPath(fromPath), UriKind.Absolute);
			Uri uri2 = new Uri(Path.GetFullPath(toPath), UriKind.Absolute);
			return uri.MakeRelativeUri(uri2).ToString();
		}

		// Token: 0x0400007B RID: 123
		private readonly MainWindow _main;

		// Token: 0x0400007C RID: 124
		private readonly ExportFormatDescription[] _formats;

		// Token: 0x0400007D RID: 125
		private bool _changedText;
	}
}
