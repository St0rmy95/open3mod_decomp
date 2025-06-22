using System;
using System.ComponentModel;
using System.Drawing;
using System.IO;
using System.Windows.Forms;

namespace open3mod
{
	// Token: 0x02000063 RID: 99
	public partial class TextureDetailsDialog : Form
	{
		// Token: 0x0600034A RID: 842 RVA: 0x0001C1E4 File Offset: 0x0001A3E4
		public TextureDetailsDialog()
		{
			this.InitializeComponent();
		}

		// Token: 0x0600034B RID: 843 RVA: 0x0001C1F2 File Offset: 0x0001A3F2
		public TextureThumbnailControl GetTexture()
		{
			return this._tex;
		}

		// Token: 0x0600034C RID: 844 RVA: 0x0001C1FC File Offset: 0x0001A3FC
		public void SetTexture(TextureThumbnailControl tex)
		{
			this._tex = tex;
			Image image = tex.Texture.Image;
			this.Text = Path.GetFileName(tex.FilePath) + " - Details";
			this.pictureBox1.Image = image;
			this.labelInfo.Text = string.Format("Size: {0} x {1} px", image.Width, image.Height);
			this.checkBoxHasAlpha.Checked = (tex.Texture.HasAlpha == Texture.AlphaState.HasAlpha);
			this.pictureBox1.SizeMode = PictureBoxSizeMode.Zoom;
		}

		// Token: 0x040002BC RID: 700
		private TextureThumbnailControl _tex;
	}
}
