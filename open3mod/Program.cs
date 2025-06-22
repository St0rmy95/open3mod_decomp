using System;
using System.IO;
using System.Windows.Forms;

namespace open3mod
{
	// Token: 0x02000042 RID: 66
	internal static class Program
	{
		// Token: 0x0600024D RID: 589 RVA: 0x000139D0 File Offset: 0x00011BD0
		[STAThread]
		private static void Main(string[] args)
		{
			MainWindow mainWindow = null;
			RunOnceGuard.Guard("open3mod_global_app", delegate
			{
				Application.EnableVisualStyles();
				Application.SetCompatibleTextRenderingDefault(false);
				mainWindow = new MainWindow();
				if (args.Length > 0)
				{
					mainWindow.AddTab(args[0], true, true);
				}
				Application.Run(mainWindow);
				mainWindow = null;
				TextureQueue.Terminate();
			}, delegate(string absPath)
			{
				if (mainWindow != null)
				{
					mainWindow.BeginInvoke(new MethodInvoker(delegate()
					{
						mainWindow.Activate();
						mainWindow.AddTab(absPath, true, true);
					}));
				}
			}, delegate
			{
				if (args.Length == 0)
				{
					return null;
				}
				return Path.GetFullPath(args[0]);
			});
		}
	}
}
