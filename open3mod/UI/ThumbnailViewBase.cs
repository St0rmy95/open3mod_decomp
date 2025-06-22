using System;
using System.Collections.Generic;
using System.Windows.Forms;

namespace open3mod
{
	// Token: 0x02000031 RID: 49
	public abstract class ThumbnailViewBase<TThumbnailType> where TThumbnailType : ThumbnailControlBase<TThumbnailType>
	{
		// Token: 0x060001E8 RID: 488 RVA: 0x00011347 File Offset: 0x0000F547
		protected ThumbnailViewBase(FlowLayoutPanel flow)
		{
			this.Flow = flow;
			this.Flow.AutoScroll = false;
			this.Entries = new List<TThumbnailType>();
			this._toolTip = new ToolTip();
		}

		// Token: 0x1700005D RID: 93
		// (get) Token: 0x060001E9 RID: 489 RVA: 0x00011378 File Offset: 0x0000F578
		public bool Empty
		{
			get
			{
				return this.Entries.Count == 0;
			}
		}

		// Token: 0x1700005E RID: 94
		// (get) Token: 0x060001EA RID: 490 RVA: 0x00011388 File Offset: 0x0000F588
		public TThumbnailType SelectedEntry
		{
			get
			{
				return this._selectedEntry;
			}
		}

		// Token: 0x060001EB RID: 491 RVA: 0x00011390 File Offset: 0x0000F590
		public void SelectEntry(TThumbnailType thumb)
		{
			if (thumb == this.SelectedEntry)
			{
				return;
			}
			thumb.IsSelected = true;
			if (this._selectedEntry != null)
			{
				this._selectedEntry.IsSelected = false;
			}
			this._selectedEntry = thumb;
		}

		// Token: 0x060001EC RID: 492 RVA: 0x000113E5 File Offset: 0x0000F5E5
		public void EnsureVisible(TThumbnailType thumb)
		{
			this.Flow.ScrollControlIntoView(thumb);
		}

		// Token: 0x060001ED RID: 493 RVA: 0x00011420 File Offset: 0x0000F620
		protected TThumbnailType AddEntry(TThumbnailType control)
		{
			control.Click += delegate(object sender, EventArgs args)
			{
				TThumbnailType tthumbnailType = sender as TThumbnailType;
				if (tthumbnailType != null)
				{
					this.SelectEntry(tthumbnailType);
				}
			};
			this.Entries.Add(control);
			this.Flow.Controls.Add(control);
			control.OnSetTooltips(this._toolTip);
			return control;
		}

		// Token: 0x04000188 RID: 392
		protected readonly FlowLayoutPanel Flow;

		// Token: 0x04000189 RID: 393
		protected readonly List<TThumbnailType> Entries;

		// Token: 0x0400018A RID: 394
		private TThumbnailType _selectedEntry;

		// Token: 0x0400018B RID: 395
		private ToolTip _toolTip;
	}
}
