using System;
using System.Collections.Generic;
using System.Windows.Forms;
using Assimp;

namespace open3mod
{
	// Token: 0x02000032 RID: 50
	public class MaterialInspectionView : ThumbnailViewBase<MaterialThumbnailControl>
	{
		// Token: 0x060001EF RID: 495 RVA: 0x0001147C File Offset: 0x0000F67C
		public MaterialInspectionView(Scene scene, MainWindow window, FlowLayoutPanel flow) : base(flow)
		{
			this._scene = scene;
			this._window = window;
			foreach (Material material in scene.Raw.Materials)
			{
				HashSet<string> hashSet = new HashSet<string>();
				TextureSlot[] allMaterialTextures = material.GetAllMaterialTextures();
				foreach (TextureSlot textureSlot in allMaterialTextures)
				{
					hashSet.Add(textureSlot.FilePath);
				}
				this.AddMaterialEntry(material, hashSet);
			}
		}

		// Token: 0x1700005F RID: 95
		// (get) Token: 0x060001F0 RID: 496 RVA: 0x0001152C File Offset: 0x0000F72C
		public Scene Scene
		{
			get
			{
				return this._scene;
			}
		}

		// Token: 0x17000060 RID: 96
		// (get) Token: 0x060001F1 RID: 497 RVA: 0x00011534 File Offset: 0x0000F734
		public MainWindow Window
		{
			get
			{
				return this._window;
			}
		}

		// Token: 0x060001F2 RID: 498 RVA: 0x00011594 File Offset: 0x0000F794
		private void AddMaterialEntry(Material material, HashSet<string> dependencies)
		{
			MaterialThumbnailControl entry = base.AddEntry(new MaterialThumbnailControl(this, this.Scene, material));
			if (dependencies.Count == 0)
			{
				return;
			}
			TextureSet.TextureCallback callback = delegate(string name, Texture tex)
			{
				if (this.Flow.IsDisposed)
				{
					return false;
				}
				if (dependencies.Contains(name))
				{
					entry.UpdatePreview();
					dependencies.Add(tex.FileName);
				}
				return true;
			};
			this.Scene.TextureSet.AddCallback(callback);
			this.Scene.TextureSet.AddReplaceCallback(callback);
		}

		// Token: 0x060001F3 RID: 499 RVA: 0x0001160C File Offset: 0x0000F80C
		public void SelectEntry(Material thumb)
		{
			foreach (MaterialThumbnailControl materialThumbnailControl in this.Entries)
			{
				if (materialThumbnailControl.Material == thumb)
				{
					base.SelectEntry(materialThumbnailControl);
					break;
				}
			}
		}

		// Token: 0x060001F4 RID: 500 RVA: 0x0001166C File Offset: 0x0000F86C
		public MaterialThumbnailControl GetMaterialControl(Material mat)
		{
			foreach (MaterialThumbnailControl materialThumbnailControl in this.Entries)
			{
				if (materialThumbnailControl.Material == mat)
				{
					return materialThumbnailControl;
				}
			}
			return null;
		}

		// Token: 0x0400018C RID: 396
		private readonly Scene _scene;

		// Token: 0x0400018D RID: 397
		private readonly MainWindow _window;

		// Token: 0x02000033 RID: 51
		// (Invoke) Token: 0x060001F6 RID: 502
		private delegate void SetLabelTextDelegate(string name, Texture tex);
	}
}
