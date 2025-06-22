using System;
using System.ComponentModel;
using System.Drawing;
using System.Windows.Forms;
using CoreSettings;

namespace open3mod
{
	// Token: 0x02000071 RID: 113
	public partial class TipOfTheDayDialog : Form
	{
		// Token: 0x060003AA RID: 938 RVA: 0x0001E3C5 File Offset: 0x0001C5C5
		public TipOfTheDayDialog()
		{
			this.InitializeComponent();
			this.SetTip(CoreSettings.Default.NextTip);
		}

		// Token: 0x060003AB RID: 939 RVA: 0x0001E3E4 File Offset: 0x0001C5E4
		private void SetTip(int nextTip)
		{
			while (nextTip < 0)
			{
				nextTip += TipOfTheDayDialog._tips.Length;
			}
			this._cursor = nextTip % TipOfTheDayDialog._tips.Length;
			this.pictureBoxTipPic.Image = ImageFromResource.Get("open3mod.Images.TipOfTheDay.Tip" + this._cursor + ".png");
			this.labelTipText.Text = TipOfTheDayDialog._tips[this._cursor];
		}

		// Token: 0x060003AC RID: 940 RVA: 0x0001E452 File Offset: 0x0001C652
		private void OnPrevious(object sender, EventArgs e)
		{
			this.SetTip(this._cursor - 1);
		}

		// Token: 0x060003AD RID: 941 RVA: 0x0001E462 File Offset: 0x0001C662
		private void OnNext(object sender, EventArgs e)
		{
			this.SetTip(this._cursor + 1);
		}

		// Token: 0x060003AE RID: 942 RVA: 0x0001E472 File Offset: 0x0001C672
		private void OnClose(object sender, FormClosingEventArgs e)
		{
			CoreSettings.Default.NextTip = (this._cursor + 1) % TipOfTheDayDialog._tips.Length;
		}

		// Token: 0x060003AF RID: 943 RVA: 0x0001E48E File Offset: 0x0001C68E
		private void OnChangeStartup(object sender, EventArgs e)
		{
			CoreSettings.Default.ShowTipsOnStartup = this.checkBoxDoNotShowAgain.Checked;
		}

		// Token: 0x040002EB RID: 747
		private static string[] _tips = new string[]
		{
			"You can lock on a search by pressing ENTER.\r\n\r\nPressing ENTER again cycles through the search \r\nresults. The current selection is highlighted\r\nin yellow then.",
			"You can permanently hide parts of a scene by\r\nright-clicking on it in the Scene Browser \r\nand selecting 'Hide'.\r\n",
			"Double-click on a texture to see it in full size. \r\nIf the texture viewer is already open, hovering \r\nover a mini texture shows a quick preview of it.\r\n",
			"In the toolbar you can highlight the joints in \r\na scene. This is extremely useful when viewing \r\nrigged models. When animations are played, \r\nthe visualization reflects the skeletal \r\nmovements.\r\n",
			"When hovering over a joint in the Scene Browser,\r\njoint highlighting is automatically turned on and \r\nthe selected joint highlighted.\r\n",
			"Use Bullseye mode to lock the current 3D view.\r\nWhen you hover with your mouse over parts of the\r\n3D scene, the Scene Browser automatically \r\nhighlights them in the scene hierarchy.\r\n"
		};

		// Token: 0x040002EC RID: 748
		private int _cursor;
	}
}
