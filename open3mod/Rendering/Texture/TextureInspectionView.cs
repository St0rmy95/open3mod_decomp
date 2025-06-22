using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Windows.Forms;
using Assimp;

namespace open3mod
{
	// Token: 0x02000064 RID: 100
	public class TextureInspectionView : ThumbnailViewBase<TextureThumbnailControl>
	{
		// Token: 0x0600034F RID: 847 RVA: 0x0001C5F0 File Offset: 0x0001A7F0
		public TextureInspectionView(Scene scene, FlowLayoutPanel flow) : base(flow)
		{
			this._scene = scene;
			HashSet<string> hashSet = new HashSet<string>();
			foreach (Material material in scene.Raw.Materials)
			{
				TextureSlot[] allMaterialTextures = material.GetAllMaterialTextures();
				foreach (TextureSlot textureSlot in allMaterialTextures)
				{
					if (!hashSet.Contains(textureSlot.FilePath))
					{
						hashSet.Add(textureSlot.FilePath);
						this.AddTextureEntry(textureSlot.FilePath);
					}
				}
			}
			int countdown = hashSet.Count;
			this.Scene.TextureSet.AddCallback(delegate(string name, Texture tex)
			{
				if (this.Flow.IsDisposed)
				{
					return false;
				}
				if (this.Flow.IsHandleCreated)
				{
					this.Flow.BeginInvoke(new TextureInspectionView.SetLabelTextDelegate(this.SetTextureToLoadedStatus), new object[]
					{
						name,
						tex
					});
				}
				else
				{
					this.SetTextureToLoadedStatus(name, tex);
				}
				return --countdown > 0;
			});
			flow.AllowDrop = true;
			flow.DragDrop += delegate(object sender, DragEventArgs args)
			{
				try
				{
					Array array2 = (Array)args.Data.GetData(DataFormats.FileDrop, false);
					if (array2 != null)
					{
						string text = array2.GetValue(0).ToString();
						if (!Directory.Exists(text))
						{
							this.MatchWithFolder(text);
						}
					}
				}
				catch (Exception ex)
				{
					Trace.WriteLine("Error in DragDrop function: " + ex.Message);
				}
			};
		}

		// Token: 0x1700009D RID: 157
		// (get) Token: 0x06000350 RID: 848 RVA: 0x0001C70C File Offset: 0x0001A90C
		public Scene Scene
		{
			get
			{
				return this._scene;
			}
		}

		// Token: 0x06000351 RID: 849 RVA: 0x0001C78C File Offset: 0x0001A98C
		private void AddTextureEntry(string filePath)
		{
			TextureThumbnailControl t = new TextureThumbnailControl(this, this.Scene, filePath);
			base.AddEntry(t);
			TextureThumbnailControl old = null;
			t.MouseEnter += delegate(object sender, EventArgs args)
			{
				if (this._details != null)
				{
					old = this._details.GetTexture();
					this._details.SetTexture(t);
				}
			};
			t.MouseLeave += delegate(object sender, EventArgs args)
			{
				if (this._details != null && old != null)
				{
					this._details.SetTexture(old);
					old = null;
				}
			};
		}

		// Token: 0x06000352 RID: 850 RVA: 0x0001C818 File Offset: 0x0001AA18
		private void SetTextureToLoadedStatus(string name, Texture tex)
		{
			TextureThumbnailControl textureThumbnailControl = this.Entries.Find((TextureThumbnailControl con) => con.FilePath == name);
			if (textureThumbnailControl == null)
			{
				return;
			}
			textureThumbnailControl.SetTexture(tex);
		}

		// Token: 0x06000353 RID: 851 RVA: 0x0001C860 File Offset: 0x0001AA60
		public void ShowDetails(TextureThumbnailControl textureThumbnailControl)
		{
			if (this._details == null)
			{
				this._details = new TextureDetailsDialog();
				this._details.Closed += delegate(object sender, EventArgs args)
				{
					this._details = null;
				};
			}
			this._details.SetTexture(textureThumbnailControl);
			this._details.Show();
		}

		// Token: 0x06000354 RID: 852 RVA: 0x0001C8B8 File Offset: 0x0001AAB8
		public void MatchWithFolder(string s)
		{
			Dictionary<string, TextureThumbnailControl> dictionary = new Dictionary<string, TextureThumbnailControl>();
			foreach (TextureThumbnailControl textureThumbnailControl in this.Entries)
			{
				dictionary[(Path.GetFileName(textureThumbnailControl.FilePath) ?? "").ToLower()] = textureThumbnailControl;
			}
			foreach (string text in Directory.GetFiles(s))
			{
				string key = (Path.GetFileName(text) ?? "").ToLower();
				if (dictionary.ContainsKey(key))
				{
					TextureThumbnailControl textureThumbnailControl2 = dictionary[key];
					if (textureThumbnailControl2.CanChangeTextureSource())
					{
						textureThumbnailControl2.ChangeTextureSource(text);
					}
				}
			}
		}

		// Token: 0x040002C1 RID: 705
		private readonly Scene _scene;

		// Token: 0x040002C2 RID: 706
		private TextureDetailsDialog _details;

		// Token: 0x02000065 RID: 101
		// (Invoke) Token: 0x06000358 RID: 856
		private delegate void SetLabelTextDelegate(string name, Texture tex);
	}
}
