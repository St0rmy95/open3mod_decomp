using System;

namespace open3mod
{
	// Token: 0x02000077 RID: 119
	public class TextureExporter
	{
		// Token: 0x060003EA RID: 1002 RVA: 0x00020766 File Offset: 0x0001E966
		public TextureExporter(Texture texture)
		{
			this._texture = texture;
		}

		// Token: 0x060003EB RID: 1003 RVA: 0x00020778 File Offset: 0x0001E978
		public string[] GetExtensionList()
		{
			return new string[]
			{
				"bmp",
				"emf",
				"exif",
				"gif",
				"ico",
				"jpeg",
				"png",
				"tiff",
				"wmf"
			};
		}

		// Token: 0x060003EC RID: 1004 RVA: 0x000207D8 File Offset: 0x0001E9D8
		public bool Export(string path)
		{
			try
			{
				this._texture.Image.Save(path);
			}
			catch (Exception)
			{
				return false;
			}
			return true;
		}

		// Token: 0x04000326 RID: 806
		private readonly Texture _texture;
	}
}
