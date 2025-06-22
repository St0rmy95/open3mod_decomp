using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.IO.Pipes;
using System.Linq;
using System.Runtime.InteropServices;
using System.Threading;

namespace open3mod
{
	// Token: 0x0200004A RID: 74
	public static class RunOnceGuard
	{
		// Token: 0x06000291 RID: 657
		[DllImport("user32.dll")]
		[return: MarshalAs(UnmanagedType.Bool)]
		private static extern bool SetForegroundWindow(IntPtr hWnd);

		// Token: 0x06000292 RID: 658 RVA: 0x000157CC File Offset: 0x000139CC
		public static void Guard(string mutexName, Action actionPrimary, Action<string> actionPrimaryReceiveMessage, Func<object> actionNotifyPrimary)
		{
			string pipeName = mutexName + "_pipe";
			bool flag;
			using (new Mutex(true, mutexName, ref flag))
			{
				if (flag)
				{
					using (new RunOnceGuard.ServerRunner(pipeName, actionPrimaryReceiveMessage))
					{
						actionPrimary();
						goto IL_121;
					}
				}
				using (Process current = Process.GetCurrentProcess())
				{
					using (IEnumerator<Process> enumerator = (from process in Process.GetProcessesByName(current.ProcessName)
					where process.Id != current.Id
					select process).GetEnumerator())
					{
						if (enumerator.MoveNext())
						{
							Process process2 = enumerator.Current;
							RunOnceGuard.SetForegroundWindow(process2.MainWindowHandle);
						}
					}
				}
				object obj = actionNotifyPrimary();
				if (obj != null)
				{
					Console.WriteLine("Communicating with primary application instance");
					using (NamedPipeClientStream namedPipeClientStream = new NamedPipeClientStream(".", pipeName, PipeDirection.Out, PipeOptions.None))
					{
						namedPipeClientStream.Connect();
						using (StreamWriter streamWriter = new StreamWriter(namedPipeClientStream))
						{
							streamWriter.Write(obj);
						}
					}
				}
				IL_121:;
			}
		}

		// Token: 0x0200004B RID: 75
		private class ServerRunner : IDisposable
		{
			// Token: 0x06000293 RID: 659 RVA: 0x00015B30 File Offset: 0x00013D30
			public ServerRunner(string pipeName, Action<string> actionPrimaryReceiveMessage)
			{
				RunOnceGuard.ServerRunner <>4__this = this;
				for (uint num = 0U; num < 2U; num += 1U)
				{
					this._threads[(int)((UIntPtr)num)] = new Thread(delegate()
					{
						while (!<>4__this._shutdown)
						{
							try
							{
								using (NamedPipeServerStream server = new NamedPipeServerStream(pipeName, PipeDirection.In, 2, PipeTransmissionMode.Byte, PipeOptions.Asynchronous))
								{
									AutoResetEvent connectEvent = new AutoResetEvent(false);
									server.BeginWaitForConnection(delegate(IAsyncResult ar)
									{
										if (<>4__this._shutdown)
										{
											return;
										}
										server.EndWaitForConnection(ar);
										using (StreamReader streamReader = new StreamReader(server))
										{
											string obj = streamReader.ReadLine();
											if (!<>4__this._shutdown)
											{
												actionPrimaryReceiveMessage(obj);
											}
										}
										connectEvent.Set();
									}, null);
									connectEvent.WaitOne();
								}
							}
							catch (IOException ex)
							{
								Console.WriteLine("Ignoring IOException in NamedPipe Server: " + ex.ToString());
							}
							catch (ThreadInterruptedException)
							{
								break;
							}
						}
					});
					this._threads[(int)((UIntPtr)num)].Start();
				}
			}

			// Token: 0x06000294 RID: 660 RVA: 0x00015BA8 File Offset: 0x00013DA8
			public void Dispose()
			{
				this._shutdown = true;
				foreach (Thread thread in this._threads)
				{
					thread.Interrupt();
					thread.Join();
				}
			}

			// Token: 0x04000206 RID: 518
			private const int ServerCount = 2;

			// Token: 0x04000207 RID: 519
			private readonly Thread[] _threads = new Thread[2];

			// Token: 0x04000208 RID: 520
			private volatile bool _shutdown;
		}
	}
}
