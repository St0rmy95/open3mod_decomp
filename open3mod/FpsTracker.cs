using System;
using System.Diagnostics;
using System.Threading;

namespace open3mod
{
	// Token: 0x02000020 RID: 32
	public class FpsTracker
	{
		// Token: 0x17000048 RID: 72
		// (get) Token: 0x06000112 RID: 274 RVA: 0x00009888 File Offset: 0x00007A88
		public double LastFrameDelta
		{
			get
			{
				return this._lastFrameDelta;
			}
		}

		// Token: 0x17000049 RID: 73
		// (get) Token: 0x06000113 RID: 275 RVA: 0x00009890 File Offset: 0x00007A90
		public int FrameCnt
		{
			get
			{
				return this._frameCnt;
			}
		}

		// Token: 0x1700004A RID: 74
		// (get) Token: 0x06000114 RID: 276 RVA: 0x00009898 File Offset: 0x00007A98
		public double LastFps
		{
			get
			{
				return this._lastFps;
			}
		}

		// Token: 0x06000115 RID: 277 RVA: 0x000098A0 File Offset: 0x00007AA0
		public void Update()
		{
			this._frameCnt = this.FrameCnt + 1;
			if (this._sw == null)
			{
				this._sw = new Stopwatch();
				this._lastFrameDelta = 0.0;
			}
			else
			{
				this._lastFrameDelta = this._sw.Elapsed.TotalMilliseconds / 1000.0;
				this._sw.Reset();
			}
			this._sw.Start();
			if (this._lastFrameDelta < 1E-08)
			{
				this._lastFps = 0.0;
			}
			else
			{
				this._lastFps = 1.0 / this._lastFrameDelta;
			}
			if (this._lastFps > 100.0)
			{
				Thread.Sleep(1 + (int)(10.0 - this._lastFrameDelta * 1000.0));
			}
		}

		// Token: 0x040000A1 RID: 161
		public const int FRAMERATE_LIMIT = 100;

		// Token: 0x040000A2 RID: 162
		private int _frameCnt = 1;

		// Token: 0x040000A3 RID: 163
		private Stopwatch _sw;

		// Token: 0x040000A4 RID: 164
		private double _lastFrameDelta;

		// Token: 0x040000A5 RID: 165
		private double _lastFps;
	}
}
