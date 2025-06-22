using System;
using System.Collections.Generic;
using System.Linq;
using Assimp;

namespace open3mod
{
	// Token: 0x0200006B RID: 107
	public sealed class TextureSet : IDisposable
	{
		// Token: 0x0600036B RID: 875 RVA: 0x0001CC14 File Offset: 0x0001AE14
		public TextureSet(string baseDir)
		{
			this._baseDir = baseDir;
			this._dict = new Dictionary<string, Texture>();
			this._replacements = new Dictionary<string, KeyValuePair<string, string>>();
			this._loaded = new List<string>();
			this._textureCallbacks = new List<TextureSet.TextureCallback>();
			this._replaceCallbacks = new List<TextureSet.TextureCallback>();
		}

		// Token: 0x0600036C RID: 876 RVA: 0x0001CC65 File Offset: 0x0001AE65
		public void AddReplaceCallback(TextureSet.TextureCallback callback)
		{
			this._replaceCallbacks.Add(callback);
		}

		// Token: 0x0600036D RID: 877 RVA: 0x0001CC74 File Offset: 0x0001AE74
		public void AddCallback(TextureSet.TextureCallback callback)
		{
			lock (this._loaded)
			{
				if (!this.InvokeCallbacks(callback))
				{
					this._textureCallbacks.Add(callback);
				}
			}
		}

		// Token: 0x0600036E RID: 878 RVA: 0x0001CCC0 File Offset: 0x0001AEC0
		private bool InvokeCallbacks(TextureSet.TextureCallback callback)
		{
			foreach (string text in this._loaded)
			{
				if (!callback(text, this._dict[text]))
				{
					return true;
				}
			}
			foreach (KeyValuePair<string, KeyValuePair<string, string>> keyValuePair in this._replacements)
			{
				if (this._loaded.Contains(keyValuePair.Value.Value) && !callback(keyValuePair.Value.Key, this.Get(keyValuePair.Value.Value)))
				{
					return true;
				}
			}
			return false;
		}

		// Token: 0x0600036F RID: 879 RVA: 0x0001CF60 File Offset: 0x0001B160
		public void Add(string path, EmbeddedTexture embeddedDataSource = null)
		{
			if (this._dict.ContainsKey(path))
			{
				return;
			}
			Texture.CompletionCallback callback = delegate(Texture self)
			{
				lock (this._loaded)
				{
					if (!this._disposed)
					{
						this._loaded.Add(path);
						foreach (KeyValuePair<string, KeyValuePair<string, string>> keyValuePair in this._replacements)
						{
							if (keyValuePair.Value.Value == path)
							{
								int i = 0;
								int num = this._textureCallbacks.Count;
								while (i < num)
								{
									TextureSet.TextureCallback textureCallback = this._textureCallbacks[i];
									if (!textureCallback(keyValuePair.Value.Key, self))
									{
										this._textureCallbacks.RemoveAt(i);
										num--;
									}
									else
									{
										i++;
									}
								}
							}
						}
						int j = 0;
						int num2 = this._textureCallbacks.Count;
						while (j < num2)
						{
							TextureSet.TextureCallback textureCallback2 = this._textureCallbacks[j];
							if (!textureCallback2(path, self))
							{
								this._textureCallbacks.RemoveAt(j);
								num2--;
							}
							else
							{
								j++;
							}
						}
					}
				}
			};
			this._dict.Add(path, (embeddedDataSource == null) ? new Texture(path, this._baseDir, callback) : new Texture(embeddedDataSource, path, callback));
		}

		// Token: 0x06000370 RID: 880 RVA: 0x0001CFD7 File Offset: 0x0001B1D7
		public bool Exists(string path)
		{
			return this._dict.ContainsKey(path);
		}

		// Token: 0x06000371 RID: 881 RVA: 0x0001CFE5 File Offset: 0x0001B1E5
		public string[] GetTextureIds()
		{
			return this._dict.Keys.ToArray<string>();
		}

		// Token: 0x06000372 RID: 882 RVA: 0x0001CFF7 File Offset: 0x0001B1F7
		public Texture Get(string path)
		{
			return this._dict[path];
		}

		// Token: 0x06000373 RID: 883 RVA: 0x0001D008 File Offset: 0x0001B208
		public Texture GetOriginalOrReplacement(string path)
		{
			if (this._replacements.ContainsKey(path))
			{
				return this.GetOriginalOrReplacement(this._replacements[path].Key);
			}
			return this._dict[path];
		}

		// Token: 0x06000374 RID: 884 RVA: 0x0001D04C File Offset: 0x0001B24C
		public void Delete(string path)
		{
			Texture texture = this.Get(path);
			texture.Dispose();
			this._dict.Remove(path);
		}

		// Token: 0x06000375 RID: 885 RVA: 0x0001D074 File Offset: 0x0001B274
		public string Replace(string path, string newPath)
		{
			string text;
			if (this.Exists(newPath))
			{
				text = newPath + '_' + Guid.NewGuid();
			}
			else
			{
				text = newPath;
				this.Add(newPath, null);
			}
			Texture originalOrReplacement = this.GetOriginalOrReplacement(path);
			this._replacements[path] = new KeyValuePair<string, string>(text, newPath);
			if (this._replaceCallbacks.Count > 0)
			{
				foreach (TextureSet.TextureCallback textureCallback in this._replaceCallbacks)
				{
					textureCallback(path, originalOrReplacement);
				}
			}
			return text;
		}

		// Token: 0x06000376 RID: 886 RVA: 0x0001D120 File Offset: 0x0001B320
		public void Dispose()
		{
			if (this._disposed)
			{
				return;
			}
			this._disposed = true;
			foreach (KeyValuePair<string, Texture> keyValuePair in this._dict)
			{
				keyValuePair.Value.Dispose();
			}
			lock (this._loaded)
			{
				this._dict.Clear();
				this._loaded.Clear();
			}
		}

		// Token: 0x06000377 RID: 887 RVA: 0x0001D3C8 File Offset: 0x0001B5C8
		public IEnumerable<Texture> GetLoadedTexturesCollectionThreadsafe()
		{
			lock (this._loaded)
			{
				foreach (string v in this._loaded)
				{
					yield return this._dict[v];
				}
			}
			yield break;
		}

		// Token: 0x040002CA RID: 714
		private readonly string _baseDir;

		// Token: 0x040002CB RID: 715
		private readonly Dictionary<string, Texture> _dict;

		// Token: 0x040002CC RID: 716
		private readonly List<string> _loaded;

		// Token: 0x040002CD RID: 717
		private readonly List<TextureSet.TextureCallback> _textureCallbacks;

		// Token: 0x040002CE RID: 718
		private readonly Dictionary<string, KeyValuePair<string, string>> _replacements;

		// Token: 0x040002CF RID: 719
		private bool _disposed;

		// Token: 0x040002D0 RID: 720
		private readonly List<TextureSet.TextureCallback> _replaceCallbacks;

		// Token: 0x0200006C RID: 108
		// (Invoke) Token: 0x06000379 RID: 889
		public delegate bool TextureCallback(string name, Texture tex);
	}
}
