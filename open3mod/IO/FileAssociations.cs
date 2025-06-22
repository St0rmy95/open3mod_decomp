using System;
using System.Runtime.InteropServices;
using System.Windows.Forms;
using Microsoft.Win32;

namespace open3mod
{
	// Token: 0x0200000F RID: 15
	public static class FileAssociations
	{
		// Token: 0x0600009D RID: 157 RVA: 0x000050BC File Offset: 0x000032BC
		public static bool SetAssociations(string[] extensionList)
		{
			foreach (string str in extensionList)
			{
				using (RegistryKey registryKey = Registry.CurrentUser.CreateSubKey("Software\\Classes\\" + str))
				{
					if (registryKey == null)
					{
						return false;
					}
					registryKey.SetValue("", "OPEN3MOD_CLASS");
				}
			}
			using (Registry.CurrentUser.CreateSubKey("Software\\Classes\\OPEN3MOD_CLASS"))
			{
				using (RegistryKey registryKey3 = Registry.CurrentUser.CreateSubKey("Software\\Classes\\OPEN3MOD_CLASS\\shell\\open\\command"))
				{
					if (registryKey3 == null)
					{
						return false;
					}
					registryKey3.SetValue("", Application.ExecutablePath + " \"%1\"");
				}
			}
			FileAssociations.SHChangeNotify(134217728U, 0U, IntPtr.Zero, IntPtr.Zero);
			return true;
		}

		// Token: 0x0600009E RID: 158
		[DllImport("shell32.dll", CharSet = CharSet.Auto, SetLastError = true)]
		private static extern void SHChangeNotify(uint wEventId, uint uFlags, IntPtr dwItem1, IntPtr dwItem2);
	}
}
