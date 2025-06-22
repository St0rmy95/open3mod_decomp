using System;
using System.ComponentModel;
using System.Diagnostics;
using System.Drawing;
using System.Windows.Forms;

namespace open3mod
{
	// Token: 0x0200000C RID: 12
	public partial class DonationDialog : Form
	{
		// Token: 0x06000083 RID: 131 RVA: 0x00004AC9 File Offset: 0x00002CC9
		public DonationDialog()
		{
			this.InitializeComponent();
			this.labelCount.Text = "In total, you have opened " + Properties.CoreSettings.Default.CountFilesOpened + " 3D models";
		}

		// Token: 0x06000084 RID: 132 RVA: 0x00004B00 File Offset: 0x00002D00
		private void NotNowAskAgain(object sender, EventArgs e)
		{
			base.Close();
		}

		// Token: 0x06000085 RID: 133 RVA: 0x00004B08 File Offset: 0x00002D08
		private void DontAskAgain(object sender, EventArgs e)
		{
			Properties.CoreSettings.Default.DonationUseCountDown = -1;
			base.Close();
		}

		// Token: 0x06000086 RID: 134 RVA: 0x00004B1B File Offset: 0x00002D1B
		private void Donate(object sender, EventArgs e)
		{
			Process.Start("http://www.open3mod.com/donate.htm?ref=inapp");
			base.Close();
		}
	}
}
