using System;
using System.Diagnostics;
using Assimp;

namespace open3mod
{
	// Token: 0x02000013 RID: 19
	public class LogPipe : IDisposable
	{
		// Token: 0x060000C2 RID: 194 RVA: 0x000058A6 File Offset: 0x00003AA6
		public LogPipe(LogStore logStore)
		{
			this._logStore = logStore;
			this._stream = new LogStream(new LoggingCallback(this.LogStreamCallback));
			this._stream.Attach();
		}

		// Token: 0x060000C3 RID: 195 RVA: 0x000058D7 File Offset: 0x00003AD7
		public LogStream GetStream()
		{
			return this._stream;
		}

		// Token: 0x060000C4 RID: 196 RVA: 0x000058DF File Offset: 0x00003ADF
		public void Dispose()
		{
			this._stream.Detach();
			this._stream.Dispose();
			GC.SuppressFinalize(this);
		}

		// Token: 0x060000C5 RID: 197 RVA: 0x00005900 File Offset: 0x00003B00
		private void LogStreamCallback(string msg, string userdata)
		{
			if (this._timer == null)
			{
				this._timer = new Stopwatch();
				this._timer.Start();
			}
			long elapsedMilliseconds = this._timer.ElapsedMilliseconds;
			int num = msg.IndexOf(':');
			if (num == -1)
			{
				return;
			}
			LogStore.Category cat;
			if (msg.StartsWith("Error, "))
			{
				cat = LogStore.Category.Error;
			}
			else if (msg.StartsWith("Debug, "))
			{
				cat = LogStore.Category.Debug;
			}
			else if (msg.StartsWith("Warn, "))
			{
				cat = LogStore.Category.Warn;
			}
			else
			{
				if (!msg.StartsWith("Info, "))
				{
					return;
				}
				cat = LogStore.Category.Info;
			}
			int num2 = msg.IndexOf('T');
			if (num2 == -1 || num2 >= num)
			{
				return;
			}
			int tid = 0;
			int.TryParse(msg.Substring(num2 + 1, num - num2 - 1), out tid);
			this._logStore.Add(cat, msg.Substring(num + 1), elapsedMilliseconds, tid);
		}

		// Token: 0x04000051 RID: 81
		private readonly LogStore _logStore;

		// Token: 0x04000052 RID: 82
		private Stopwatch _timer;

		// Token: 0x04000053 RID: 83
		private LogStream _stream;
	}
}
