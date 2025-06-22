using System;
using System.Collections.Generic;
using System.Drawing;
using System.Threading;
using Assimp;

namespace open3mod
{
	// Token: 0x02000066 RID: 102
	public static class TextureQueue
	{
		// Token: 0x0600035B RID: 859 RVA: 0x0001C984 File Offset: 0x0001AB84
		public static void Enqueue(string file, string baseDir, TextureQueue.CompletionCallback callback)
		{
			if (TextureQueue._thread == null)
			{
				TextureQueue.StartThread();
			}
			lock (TextureQueue.Queue)
			{
				TextureQueue.Queue.Enqueue(new TextureQueue.TextureFromFileTask(file, baseDir, callback));
				Monitor.Pulse(TextureQueue.Queue);
			}
		}

		// Token: 0x0600035C RID: 860 RVA: 0x0001C9E0 File Offset: 0x0001ABE0
		public static void Enqueue(EmbeddedTexture dataSource, string refName, TextureQueue.CompletionCallback callback)
		{
			if (TextureQueue._thread == null)
			{
				TextureQueue.StartThread();
			}
			lock (TextureQueue.Queue)
			{
				TextureQueue.Queue.Enqueue(new TextureQueue.TextureFromMemoryTask(dataSource, refName, callback));
				Monitor.Pulse(TextureQueue.Queue);
			}
		}

		// Token: 0x0600035D RID: 861 RVA: 0x0001CA3C File Offset: 0x0001AC3C
		public static void Terminate()
		{
			if (TextureQueue._thread == null || !TextureQueue._thread.IsAlive)
			{
				return;
			}
			lock (TextureQueue.Queue)
			{
				while (TextureQueue.Queue.Count != 0)
				{
					Monitor.Wait(TextureQueue.Queue);
				}
			}
			TextureQueue._thread.Interrupt();
		}

		// Token: 0x0600035E RID: 862 RVA: 0x0001CAA8 File Offset: 0x0001ACA8
		private static void StartThread()
		{
			TextureQueue._thread = new Thread(new ThreadStart(TextureQueue.ThreadProc));
			TextureQueue._thread.Start();
		}

		// Token: 0x0600035F RID: 863 RVA: 0x0001CACC File Offset: 0x0001ACCC
		private static void ThreadProc()
		{
			try
			{
				for (;;)
				{
					lock (TextureQueue.Queue)
					{
						while (TextureQueue.Queue.Count > 0)
						{
							TextureQueue.Task task = TextureQueue.Queue.Dequeue();
							task.Load();
						}
						Monitor.Wait(TextureQueue.Queue);
					}
				}
			}
			catch (ThreadInterruptedException)
			{
			}
			catch (InvalidOperationException)
			{
			}
		}

		// Token: 0x040002C3 RID: 707
		private static Thread _thread;

		// Token: 0x040002C4 RID: 708
		private static readonly Queue<TextureQueue.Task> Queue = new Queue<TextureQueue.Task>();

		// Token: 0x02000067 RID: 103
		// (Invoke) Token: 0x06000362 RID: 866
		public delegate void CompletionCallback(string file, Image image, string actualLocation, TextureLoader.LoadResult result);

		// Token: 0x02000068 RID: 104
		private abstract class Task
		{
			// Token: 0x06000365 RID: 869
			public abstract void Load();

			// Token: 0x06000366 RID: 870 RVA: 0x0001CB54 File Offset: 0x0001AD54
			protected Task(TextureQueue.CompletionCallback callback)
			{
				this.Callback = callback;
			}

			// Token: 0x040002C5 RID: 709
			protected readonly TextureQueue.CompletionCallback Callback;
		}

		// Token: 0x02000069 RID: 105
		private class TextureFromFileTask : TextureQueue.Task
		{
			// Token: 0x06000367 RID: 871 RVA: 0x0001CB63 File Offset: 0x0001AD63
			public TextureFromFileTask(string file, string baseDir, TextureQueue.CompletionCallback callback) : base(callback)
			{
				this._file = file;
				this._baseDir = baseDir;
			}

			// Token: 0x06000368 RID: 872 RVA: 0x0001CB7C File Offset: 0x0001AD7C
			public override void Load()
			{
				TextureLoader textureLoader = new TextureLoader(this._file, this._baseDir);
				this.Callback(this._file, textureLoader.Image, textureLoader.ActualLocation, textureLoader.Result);
			}

			// Token: 0x040002C6 RID: 710
			private readonly string _file;

			// Token: 0x040002C7 RID: 711
			private readonly string _baseDir;
		}

		// Token: 0x0200006A RID: 106
		private class TextureFromMemoryTask : TextureQueue.Task
		{
			// Token: 0x06000369 RID: 873 RVA: 0x0001CBBE File Offset: 0x0001ADBE
			public TextureFromMemoryTask(EmbeddedTexture dataSource, string refName, TextureQueue.CompletionCallback callback) : base(callback)
			{
				this._dataSource = dataSource;
				this._refName = refName;
			}

			// Token: 0x0600036A RID: 874 RVA: 0x0001CBD8 File Offset: 0x0001ADD8
			public override void Load()
			{
				EmbeddedTextureLoader embeddedTextureLoader = new EmbeddedTextureLoader(this._dataSource);
				this.Callback(this._refName, embeddedTextureLoader.Image, "", embeddedTextureLoader.Result);
			}

			// Token: 0x040002C8 RID: 712
			private readonly EmbeddedTexture _dataSource;

			// Token: 0x040002C9 RID: 713
			private readonly string _refName;
		}
	}
}
