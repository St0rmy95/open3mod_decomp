using System;
using System.ComponentModel;
using System.Drawing;
using System.Windows.Forms;

namespace open3mod
{
	// Token: 0x0200006E RID: 110
	public class TimeSlideControl : UserControl
	{
		// Token: 0x06000392 RID: 914 RVA: 0x0001DE8C File Offset: 0x0001C08C
		public TimeSlideControl()
		{
			this.InitializeComponent();
			this._font = new Font(FontFamily.GenericMonospace, 9f);
			this._redPen = new Pen(new SolidBrush(Color.Red), 1f);
			this._lightGray = new SolidBrush(Color.LightGray);
			this._dimGrayPen = new Pen(new SolidBrush(Color.DimGray), 1f);
			this._blackPen = new Pen(new SolidBrush(Color.Black), 1f);
		}

		// Token: 0x170000A0 RID: 160
		// (get) Token: 0x06000393 RID: 915 RVA: 0x0001DF18 File Offset: 0x0001C118
		// (set) Token: 0x06000394 RID: 916 RVA: 0x0001DF20 File Offset: 0x0001C120
		public double RangeMin
		{
			get
			{
				return this._rangeMin;
			}
			set
			{
				this._rangeMin = value;
				if (this._pos < this._rangeMin)
				{
					this._pos = this._rangeMin;
				}
				base.Invalidate();
			}
		}

		// Token: 0x170000A1 RID: 161
		// (get) Token: 0x06000395 RID: 917 RVA: 0x0001DF49 File Offset: 0x0001C149
		// (set) Token: 0x06000396 RID: 918 RVA: 0x0001DF51 File Offset: 0x0001C151
		public double RangeMax
		{
			get
			{
				return this._rangeMax;
			}
			set
			{
				this._rangeMax = value;
				if (this._pos > this._rangeMax)
				{
					this._pos = this._rangeMax;
				}
				base.Invalidate();
			}
		}

		// Token: 0x170000A2 RID: 162
		// (get) Token: 0x06000397 RID: 919 RVA: 0x0001DF7A File Offset: 0x0001C17A
		// (set) Token: 0x06000398 RID: 920 RVA: 0x0001DF84 File Offset: 0x0001C184
		public double Position
		{
			get
			{
				return this._pos;
			}
			set
			{
				this._pos = value;
				if (this._pos > this.RangeMax)
				{
					this._pos = this.RangeMax;
				}
				if (this._pos < this.RangeMin)
				{
					this._pos = this.RangeMin;
				}
				base.Invalidate();
			}
		}

		// Token: 0x170000A3 RID: 163
		// (get) Token: 0x06000399 RID: 921 RVA: 0x0001DFD2 File Offset: 0x0001C1D2
		public double Range
		{
			get
			{
				return this._rangeMax - this._rangeMin;
			}
		}

		// Token: 0x170000A4 RID: 164
		// (get) Token: 0x0600039A RID: 922 RVA: 0x0001DFE4 File Offset: 0x0001C1E4
		public double RelativePosition
		{
			get
			{
				double range = this.Range;
				if (range < 1E-07)
				{
					return 0.0;
				}
				return (this._pos - this._rangeMin) / range;
			}
		}

		// Token: 0x14000006 RID: 6
		// (add) Token: 0x0600039B RID: 923 RVA: 0x0001E020 File Offset: 0x0001C220
		// (remove) Token: 0x0600039C RID: 924 RVA: 0x0001E058 File Offset: 0x0001C258
		public event TimeSlideControl.RewindDelegate Rewind;

		// Token: 0x0600039D RID: 925 RVA: 0x0001E090 File Offset: 0x0001C290
		public virtual void OnRewind(TimeSlideControl.RewindDelegateArgs args)
		{
			TimeSlideControl.RewindDelegate rewind = this.Rewind;
			if (rewind != null)
			{
				rewind(this, args);
			}
		}

		// Token: 0x0600039E RID: 926 RVA: 0x0001E0AF File Offset: 0x0001C2AF
		protected override void OnEnabledChanged(EventArgs e)
		{
			base.OnEnabledChanged(e);
			base.Invalidate();
		}

		// Token: 0x0600039F RID: 927 RVA: 0x0001E0C0 File Offset: 0x0001C2C0
		protected override void OnMouseClick(MouseEventArgs e)
		{
			base.OnMouseClick(e);
			Rectangle clientRectangle = base.ClientRectangle;
			double num = (double)(e.X - clientRectangle.Left) * this.Range / (double)clientRectangle.Width + this._rangeMin;
			this.OnRewind(new TimeSlideControl.RewindDelegateArgs
			{
				OldPosition = this._pos,
				NewPosition = num
			});
			this.Position = num;
		}

		// Token: 0x060003A0 RID: 928 RVA: 0x0001E130 File Offset: 0x0001C330
		protected override void OnMouseMove(MouseEventArgs e)
		{
			base.OnMouseMove(e);
			Rectangle clientRectangle = base.ClientRectangle;
			this._mouseRelativePos = (double)(e.X - clientRectangle.Left) / (double)clientRectangle.Width;
			base.Invalidate();
		}

		// Token: 0x060003A1 RID: 929 RVA: 0x0001E16F File Offset: 0x0001C36F
		protected override void OnMouseEnter(EventArgs e)
		{
			base.OnMouseEnter(e);
			this._mouseEntered = true;
			base.Invalidate();
		}

		// Token: 0x060003A2 RID: 930 RVA: 0x0001E185 File Offset: 0x0001C385
		protected override void OnMouseLeave(EventArgs e)
		{
			base.OnMouseLeave(e);
			this._mouseEntered = false;
			base.Invalidate();
		}

		// Token: 0x060003A3 RID: 931 RVA: 0x0001E19C File Offset: 0x0001C39C
		protected override void OnPaint(PaintEventArgs e)
		{
			base.OnPaint(e);
			Graphics graphics = e.Graphics;
			Rectangle clientRectangle = base.ClientRectangle;
			graphics.FillRectangle(this._lightGray, clientRectangle);
			if (!base.Enabled)
			{
				return;
			}
			double relativePosition = this.RelativePosition;
			int num = clientRectangle.Left + (int)((double)clientRectangle.Width * relativePosition);
			graphics.DrawLine(this._redPen, num, 15, num, clientRectangle.Bottom);
			double num2 = (double)clientRectangle.Width / this.Range;
			double d = Math.Log10(this.Range);
			int num3 = (int)Math.Floor(d);
			float num4 = (float)Math.Pow(10.0, (double)num3);
			for (float num5 = 0f; num5 < (float)this.Range; num5 += num4)
			{
				int num6 = (int)((double)num5 * num2);
				graphics.DrawLine(this._dimGrayPen, num6, 55, num6, clientRectangle.Bottom);
			}
			if (this._mouseEntered)
			{
				graphics.DrawString((this._mouseRelativePos * this.Range).ToString("0.000") + "s", this._font, this._blackPen.Brush, 5f, 1f);
				num = clientRectangle.Left + (int)((double)clientRectangle.Width * this._mouseRelativePos);
				graphics.DrawLine(this._blackPen, num, 40, num, clientRectangle.Bottom);
			}
			graphics.DrawString(this.Position.ToString("0.000") + "s", this._font, this._redPen.Brush, (float)(clientRectangle.Width - 70), 1f);
		}

		// Token: 0x060003A4 RID: 932 RVA: 0x0001E342 File Offset: 0x0001C542
		protected override void Dispose(bool disposing)
		{
			if (disposing && this.components != null)
			{
				this.components.Dispose();
			}
			base.Dispose(disposing);
		}

		// Token: 0x060003A5 RID: 933 RVA: 0x0001E364 File Offset: 0x0001C564
		private void InitializeComponent()
		{
			base.SuspendLayout();
			base.AutoScaleDimensions = new SizeF(6f, 13f);
			base.AutoScaleMode = AutoScaleMode.Font;
			base.BorderStyle = BorderStyle.FixedSingle;
			this.DoubleBuffered = true;
			base.Name = "TimeSlideControl";
			base.Size = new Size(360, 61);
			base.ResumeLayout(false);
		}

		// Token: 0x040002DD RID: 733
		private double _rangeMin;

		// Token: 0x040002DE RID: 734
		private double _rangeMax;

		// Token: 0x040002DF RID: 735
		private double _pos;

		// Token: 0x040002E0 RID: 736
		private double _mouseRelativePos;

		// Token: 0x040002E1 RID: 737
		private bool _mouseEntered;

		// Token: 0x040002E2 RID: 738
		private readonly Font _font;

		// Token: 0x040002E3 RID: 739
		private readonly Pen _redPen;

		// Token: 0x040002E4 RID: 740
		private readonly SolidBrush _lightGray;

		// Token: 0x040002E5 RID: 741
		private readonly Pen _dimGrayPen;

		// Token: 0x040002E6 RID: 742
		private readonly Pen _blackPen;

		// Token: 0x040002E8 RID: 744
		private IContainer components;

		// Token: 0x0200006F RID: 111
		// (Invoke) Token: 0x060003A7 RID: 935
		public delegate void RewindDelegate(object sender, TimeSlideControl.RewindDelegateArgs args);

		// Token: 0x02000070 RID: 112
		public struct RewindDelegateArgs
		{
			// Token: 0x040002E9 RID: 745
			public double OldPosition;

			// Token: 0x040002EA RID: 746
			public double NewPosition;
		}
	}
}
