using System;
using System.Collections.Generic;

namespace open3mod
{
	// Token: 0x02000028 RID: 40
	public class LogStore
	{
		// Token: 0x17000055 RID: 85
		// (get) Token: 0x06000165 RID: 357 RVA: 0x0000C235 File Offset: 0x0000A435
		public List<LogStore.Entry> Messages
		{
			get
			{
				return this._messages;
			}
		}

		// Token: 0x06000166 RID: 358 RVA: 0x0000C23D File Offset: 0x0000A43D
		public LogStore(int capacity = 200)
		{
			this._messages = new List<LogStore.Entry>(capacity);
		}

		// Token: 0x06000167 RID: 359 RVA: 0x0000C254 File Offset: 0x0000A454
		public void Add(LogStore.Category cat, string message, long time, int tid)
		{
			this._messages.Add(new LogStore.Entry
			{
				Cat = cat,
				Message = message,
				Time = time,
				ThreadId = tid
			});
		}

		// Token: 0x06000168 RID: 360 RVA: 0x0000C296 File Offset: 0x0000A496
		public void Drop()
		{
			this._messages.Clear();
		}

		// Token: 0x04000102 RID: 258
		private readonly List<LogStore.Entry> _messages;

		// Token: 0x02000029 RID: 41
		public enum Category
		{
			// Token: 0x04000104 RID: 260
			Info,
			// Token: 0x04000105 RID: 261
			Warn,
			// Token: 0x04000106 RID: 262
			Error,
			// Token: 0x04000107 RID: 263
			Debug,
			// Token: 0x04000108 RID: 264
			System
		}

		// Token: 0x0200002A RID: 42
		public struct Entry
		{
			// Token: 0x04000109 RID: 265
			public int ThreadId;

			// Token: 0x0400010A RID: 266
			public LogStore.Category Cat;

			// Token: 0x0400010B RID: 267
			public string Message;

			// Token: 0x0400010C RID: 268
			public long Time;
		}
	}
}
