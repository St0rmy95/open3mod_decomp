using System;
using System.Collections.Generic;
using System.Drawing;
using System.IO;
using System.Reflection;

namespace open3mod
{
	// Token: 0x02000024 RID: 36
	public static class ImageFromResource
	{
		// Token: 0x0600014F RID: 335 RVA: 0x0000BA98 File Offset: 0x00009C98
		public static Image Get(string resPath)
		{
			Image image;
			if (ImageFromResource.Cache.TryGetValue(resPath, out image))
			{
				return image;
			}
			Assembly executingAssembly = Assembly.GetExecutingAssembly();
			Stream manifestResourceStream = executingAssembly.GetManifestResourceStream(resPath);
			ImageFromResource.StreamRefs.Add(manifestResourceStream);
			image = Image.FromStream(manifestResourceStream);
			ImageFromResource.Cache[resPath] = image;
			return image;
		}

		// Token: 0x040000EA RID: 234
		private static readonly List<Stream> StreamRefs = new List<Stream>();

		// Token: 0x040000EB RID: 235
		private static readonly Dictionary<string, Image> Cache = new Dictionary<string, Image>();
	}
}
