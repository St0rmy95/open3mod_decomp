using System;
using System.ComponentModel;
using System.Drawing;
using System.Windows.Forms;
using Assimp;

namespace open3mod
{
	// Token: 0x02000003 RID: 3
	public sealed class AnimationInspectionView : UserControl
	{
		// Token: 0x06000012 RID: 18 RVA: 0x00002990 File Offset: 0x00000B90
		public AnimationInspectionView(Scene scene, TabPage tabPageAnimations)
		{
			this._scene = scene;
			this.Anchor = (AnchorStyles.Top | AnchorStyles.Left | AnchorStyles.Right);
			this.Dock = DockStyle.Fill;
			this.InitializeComponent();
			tabPageAnimations.Controls.Add(this);
			this.listBoxAnimations.Items.Add("None (Bind Pose)");
			if (scene.Raw.Animations != null)
			{
				foreach (Animation animation in scene.Raw.Animations)
				{
					double num = animation.DurationInTicks;
					if (animation.TicksPerSecond > 1E-10)
					{
						num /= animation.TicksPerSecond;
					}
					else
					{
						num /= 25.0;
					}
					this.listBoxAnimations.Items.Add(string.Format("{0} ({1}s)", animation.Name, num.ToString("0.000")));
				}
			}
			this.listBoxAnimations.SelectedIndex = 0;
			this.checkBoxLoop.Checked = this._scene.SceneAnimator.Loop;
			this._imagePlay = ImageFromResource.Get("open3mod.Images.PlayAnim.png");
			this._imageStop = ImageFromResource.Get("open3mod.Images.StopAnim.png");
			this.buttonPlay.Image = this._imagePlay;
			this._scene.SceneAnimator.AnimationPlaybackSpeed = 0.0;
			this.labelSpeedValue.Text = "1.0x";
			this.timeSlideControl.Rewind += delegate(object o, TimeSlideControl.RewindDelegateArgs args)
			{
				if (this._scene.SceneAnimator.ActiveAnimation >= 0)
				{
					this._scene.SceneAnimator.AnimationCursor = args.NewPosition;
				}
			};
		}

		// Token: 0x17000007 RID: 7
		// (get) Token: 0x06000013 RID: 19 RVA: 0x00002B40 File Offset: 0x00000D40
		public bool Empty
		{
			get
			{
				return this._scene.Raw.AnimationCount == 0;
			}
		}

		// Token: 0x17000008 RID: 8
		// (get) Token: 0x06000014 RID: 20 RVA: 0x00002B55 File Offset: 0x00000D55
		// (set) Token: 0x06000015 RID: 21 RVA: 0x00002B60 File Offset: 0x00000D60
		public bool Playing
		{
			get
			{
				return this._playing;
			}
			set
			{
				if (value == this._playing)
				{
					return;
				}
				this._playing = value;
				if (value)
				{
					this.StartPlayingTimer();
					this._scene.SceneAnimator.AnimationPlaybackSpeed = ((this._scene.SceneAnimator.ActiveAnimation >= 0) ? this.AnimPlaybackSpeed : 0.0);
					return;
				}
				this.StopPlayingTimer();
				this._scene.SceneAnimator.AnimationPlaybackSpeed = 0.0;
			}
		}

		// Token: 0x17000009 RID: 9
		// (get) Token: 0x06000016 RID: 22 RVA: 0x00002BDB File Offset: 0x00000DDB
		// (set) Token: 0x06000017 RID: 23 RVA: 0x00002C0C File Offset: 0x00000E0C
		public double AnimPlaybackSpeed
		{
			get
			{
				return this._animPlaybackSpeed;
			}
			private set
			{
				this._animPlaybackSpeed = value;
				if (Math.Abs(this._animPlaybackSpeed - 1.0) < 1E-07)
				{
					this._animPlaybackSpeed = 1.0;
				}
				if (this._playing)
				{
					this._scene.SceneAnimator.AnimationPlaybackSpeed = this.AnimPlaybackSpeed;
				}
				base.BeginInvoke(new MethodInvoker(delegate()
				{
					this.labelSpeedValue.Text = this._animPlaybackSpeed.ToString("0.00") + "x";
				}));
			}
		}

		// Token: 0x06000018 RID: 24 RVA: 0x00002C80 File Offset: 0x00000E80
		private void StopPlayingTimer()
		{
			if (this._timer != null)
			{
				this._timer.Stop();
				this._timer = null;
			}
		}

		// Token: 0x06000019 RID: 25 RVA: 0x00002CE4 File Offset: 0x00000EE4
		private void StartPlayingTimer()
		{
			if (!this.Playing)
			{
				return;
			}
			this._timer = new Timer
			{
				Interval = 30
			};
			this._timer.Tick += delegate(object o, EventArgs args)
			{
				double num = this._scene.SceneAnimator.AnimationCursor;
				if (!this._scene.SceneAnimator.IsInEndPosition)
				{
					num %= this._duration;
				}
				this.timeSlideControl.Position = num;
			};
			this._timer.Start();
		}

		// Token: 0x0600001A RID: 26 RVA: 0x00002D34 File Offset: 0x00000F34
		private void OnChangeSelectedAnimation(object sender, EventArgs e)
		{
			this._scene.SceneAnimator.ActiveAnimation = this.listBoxAnimations.SelectedIndex - 1;
			if (this._scene.SceneAnimator.ActiveAnimation >= 0)
			{
				Animation animation = this._scene.Raw.Animations[this._scene.SceneAnimator.ActiveAnimation];
				foreach (object obj in this.panelAnimTools.Controls)
				{
					if ((obj != this.buttonSlower || this._speedAdjust != -8) && (obj != this.buttonFaster || this._speedAdjust != 8))
					{
						((Control)obj).Enabled = true;
					}
				}
				this._duration = this._scene.SceneAnimator.AnimationDuration;
				this.timeSlideControl.RangeMin = 0.0;
				this.timeSlideControl.RangeMax = this._duration;
				this.timeSlideControl.Position = 0.0;
				this._scene.SceneAnimator.AnimationCursor = 0.0;
				this.StartPlayingTimer();
				return;
			}
			foreach (object obj2 in this.panelAnimTools.Controls)
			{
				((Control)obj2).Enabled = false;
			}
			this.StopPlayingTimer();
		}

		// Token: 0x0600001B RID: 27 RVA: 0x00002EDC File Offset: 0x000010DC
		private void OnPlay(object sender, EventArgs e)
		{
			this.Playing = !this.Playing;
			this.buttonPlay.Image = (this.Playing ? this._imageStop : this._imagePlay);
		}

		// Token: 0x0600001C RID: 28 RVA: 0x00002F10 File Offset: 0x00001110
		private void OnSlower(object sender, EventArgs e)
		{
			if (--this._speedAdjust == -8)
			{
				this.buttonSlower.Enabled = false;
			}
			this.buttonFaster.Enabled = true;
			this.AnimPlaybackSpeed *= 0.6666;
		}

		// Token: 0x0600001D RID: 29 RVA: 0x00002F60 File Offset: 0x00001160
		private void OnFaster(object sender, EventArgs e)
		{
			if (++this._speedAdjust == 8)
			{
				this.buttonFaster.Enabled = false;
			}
			this.buttonSlower.Enabled = true;
			this.AnimPlaybackSpeed /= 0.6666;
		}

		// Token: 0x0600001E RID: 30 RVA: 0x00002FAF File Offset: 0x000011AF
		private void OnChangeLooping(object sender, EventArgs e)
		{
			this._scene.SceneAnimator.Loop = this.checkBoxLoop.Checked;
		}

		// Token: 0x0600001F RID: 31 RVA: 0x00002FCC File Offset: 0x000011CC
		private void OnGoTo(object sender, KeyEventArgs e)
		{
			this.labelGotoError.Text = "";
			if (e.KeyCode != Keys.Return)
			{
				return;
			}
			string text = this.textBoxGoto.Text;
			double num;
			try
			{
				num = double.Parse(text);
				if (num < 0.0 || num > this._duration)
				{
					throw new FormatException();
				}
			}
			catch (FormatException)
			{
				this.labelGotoError.Text = "Not a valid time";
				return;
			}
			this._scene.SceneAnimator.AnimationCursor = num;
		}

		// Token: 0x06000020 RID: 32 RVA: 0x00003058 File Offset: 0x00001258
		protected override void Dispose(bool disposing)
		{
			if (disposing && this.components != null)
			{
				this.components.Dispose();
			}
			base.Dispose(disposing);
		}

		// Token: 0x06000021 RID: 33 RVA: 0x00003078 File Offset: 0x00001278
		private void InitializeComponent()
		{
			this.textBoxGoto = new TextBox();
			this.labelGoto = new Label();
			this.buttonFaster = new Button();
			this.buttonSlower = new Button();
			this.checkBoxLoop = new CheckBox();
			this.buttonPlay = new Button();
			this.label3 = new Label();
			this.listBoxAnimations = new ListBox();
			this.panelAnimTools = new Panel();
			this.labelSpeed = new Label();
			this.panel1 = new Panel();
			this.labelSpeedValue = new Label();
			this.labelGotoError = new Label();
			this.timeSlideControl = new TimeSlideControl();
			this.panelAnimTools.SuspendLayout();
			this.panel1.SuspendLayout();
			base.SuspendLayout();
			this.textBoxGoto.Location = new Point(41, 193);
			this.textBoxGoto.Name = "textBoxGoto";
			this.textBoxGoto.Size = new Size(78, 20);
			this.textBoxGoto.TabIndex = 16;
			this.textBoxGoto.KeyDown += this.OnGoTo;
			this.labelGoto.AutoSize = true;
			this.labelGoto.Location = new Point(2, 196);
			this.labelGoto.Name = "labelGoto";
			this.labelGoto.Size = new Size(33, 13);
			this.labelGoto.TabIndex = 15;
			this.labelGoto.Text = "Go to";
			this.buttonFaster.FlatAppearance.BorderSize = 2;
			this.buttonFaster.FlatAppearance.MouseDownBackColor = Color.Gray;
			this.buttonFaster.FlatAppearance.MouseOverBackColor = Color.WhiteSmoke;
			this.buttonFaster.FlatStyle = FlatStyle.Flat;
			this.buttonFaster.Location = new Point(163, 18);
			this.buttonFaster.Name = "buttonFaster";
			this.buttonFaster.Size = new Size(60, 40);
			this.buttonFaster.TabIndex = 14;
			this.buttonFaster.Text = "Faster";
			this.buttonFaster.UseVisualStyleBackColor = true;
			this.buttonFaster.Click += this.OnFaster;
			this.buttonSlower.FlatAppearance.BorderSize = 2;
			this.buttonSlower.FlatAppearance.MouseDownBackColor = Color.Gray;
			this.buttonSlower.FlatAppearance.MouseOverBackColor = Color.WhiteSmoke;
			this.buttonSlower.FlatStyle = FlatStyle.Flat;
			this.buttonSlower.Location = new Point(16, 18);
			this.buttonSlower.Name = "buttonSlower";
			this.buttonSlower.Size = new Size(60, 40);
			this.buttonSlower.TabIndex = 13;
			this.buttonSlower.Text = "Slower";
			this.buttonSlower.UseVisualStyleBackColor = true;
			this.buttonSlower.Click += this.OnSlower;
			this.checkBoxLoop.AutoSize = true;
			this.checkBoxLoop.Location = new Point(6, 1);
			this.checkBoxLoop.Name = "checkBoxLoop";
			this.checkBoxLoop.Size = new Size(50, 17);
			this.checkBoxLoop.TabIndex = 12;
			this.checkBoxLoop.Text = "Loop";
			this.checkBoxLoop.UseVisualStyleBackColor = true;
			this.checkBoxLoop.CheckedChanged += this.OnChangeLooping;
			this.buttonPlay.FlatAppearance.BorderSize = 2;
			this.buttonPlay.FlatAppearance.MouseDownBackColor = Color.Gray;
			this.buttonPlay.FlatAppearance.MouseOverBackColor = Color.WhiteSmoke;
			this.buttonPlay.FlatStyle = FlatStyle.Flat;
			this.buttonPlay.Location = new Point(82, 3);
			this.buttonPlay.MaximumSize = new Size(100, 100);
			this.buttonPlay.Name = "buttonPlay";
			this.buttonPlay.Size = new Size(75, 68);
			this.buttonPlay.TabIndex = 11;
			this.buttonPlay.UseVisualStyleBackColor = true;
			this.buttonPlay.Click += this.OnPlay;
			this.label3.AutoSize = true;
			this.label3.Location = new Point(9, 7);
			this.label3.Name = "label3";
			this.label3.Size = new Size(107, 13);
			this.label3.TabIndex = 10;
			this.label3.Text = "Available Animations:";
			this.listBoxAnimations.Anchor = (AnchorStyles.Top | AnchorStyles.Left | AnchorStyles.Right);
			this.listBoxAnimations.FormattingEnabled = true;
			this.listBoxAnimations.Location = new Point(3, 23);
			this.listBoxAnimations.Name = "listBoxAnimations";
			this.listBoxAnimations.ScrollAlwaysVisible = true;
			this.listBoxAnimations.Size = new Size(335, 186);
			this.listBoxAnimations.TabIndex = 9;
			this.listBoxAnimations.SelectedIndexChanged += this.OnChangeSelectedAnimation;
			this.panelAnimTools.Anchor = (AnchorStyles.Top | AnchorStyles.Bottom | AnchorStyles.Left | AnchorStyles.Right);
			this.panelAnimTools.Controls.Add(this.labelGotoError);
			this.panelAnimTools.Controls.Add(this.labelSpeedValue);
			this.panelAnimTools.Controls.Add(this.panel1);
			this.panelAnimTools.Controls.Add(this.labelSpeed);
			this.panelAnimTools.Controls.Add(this.textBoxGoto);
			this.panelAnimTools.Controls.Add(this.timeSlideControl);
			this.panelAnimTools.Controls.Add(this.checkBoxLoop);
			this.panelAnimTools.Controls.Add(this.labelGoto);
			this.panelAnimTools.Location = new Point(6, 215);
			this.panelAnimTools.Name = "panelAnimTools";
			this.panelAnimTools.Size = new Size(332, 458);
			this.panelAnimTools.TabIndex = 18;
			this.labelSpeed.Anchor = (AnchorStyles.Top | AnchorStyles.Right);
			this.labelSpeed.AutoSize = true;
			this.labelSpeed.Location = new Point(251, 2);
			this.labelSpeed.Name = "labelSpeed";
			this.labelSpeed.Size = new Size(41, 13);
			this.labelSpeed.TabIndex = 18;
			this.labelSpeed.Text = "Speed:";
			this.panel1.Anchor = AnchorStyles.Top;
			this.panel1.Controls.Add(this.buttonFaster);
			this.panel1.Controls.Add(this.buttonSlower);
			this.panel1.Controls.Add(this.buttonPlay);
			this.panel1.Location = new Point(41, 30);
			this.panel1.Name = "panel1";
			this.panel1.Size = new Size(242, 77);
			this.panel1.TabIndex = 19;
			this.labelSpeedValue.Anchor = (AnchorStyles.Top | AnchorStyles.Right);
			this.labelSpeedValue.AutoSize = true;
			this.labelSpeedValue.Location = new Point(291, 2);
			this.labelSpeedValue.Name = "labelSpeedValue";
			this.labelSpeedValue.Size = new Size(0, 13);
			this.labelSpeedValue.TabIndex = 20;
			this.labelGotoError.AutoSize = true;
			this.labelGotoError.Location = new Point(125, 196);
			this.labelGotoError.Name = "labelGotoError";
			this.labelGotoError.Size = new Size(0, 13);
			this.labelGotoError.TabIndex = 21;
			this.timeSlideControl.Anchor = (AnchorStyles.Top | AnchorStyles.Left | AnchorStyles.Right);
			this.timeSlideControl.BorderStyle = BorderStyle.FixedSingle;
			this.timeSlideControl.Location = new Point(0, 113);
			this.timeSlideControl.Name = "timeSlideControl";
			this.timeSlideControl.Position = 0.0;
			this.timeSlideControl.RangeMax = 0.0;
			this.timeSlideControl.RangeMin = 0.0;
			this.timeSlideControl.Size = new Size(332, 67);
			this.timeSlideControl.TabIndex = 17;
			base.AutoScaleDimensions = new SizeF(6f, 13f);
			base.AutoScaleMode = AutoScaleMode.Font;
			base.Controls.Add(this.panelAnimTools);
			base.Controls.Add(this.label3);
			base.Controls.Add(this.listBoxAnimations);
			base.Name = "AnimationInspectionView";
			base.Size = new Size(341, 676);
			this.panelAnimTools.ResumeLayout(false);
			this.panelAnimTools.PerformLayout();
			this.panel1.ResumeLayout(false);
			base.ResumeLayout(false);
			base.PerformLayout();
		}

		// Token: 0x0400000F RID: 15
		private const int TimerInterval = 30;

		// Token: 0x04000010 RID: 16
		private const double PlaybackSpeedAdjustFactor = 0.6666;

		// Token: 0x04000011 RID: 17
		private const int MaxSpeedAdjustLevels = 8;

		// Token: 0x04000012 RID: 18
		private readonly Scene _scene;

		// Token: 0x04000013 RID: 19
		private Timer _timer;

		// Token: 0x04000014 RID: 20
		private double _duration;

		// Token: 0x04000015 RID: 21
		private bool _playing;

		// Token: 0x04000016 RID: 22
		private double _animPlaybackSpeed = 1.0;

		// Token: 0x04000017 RID: 23
		private int _speedAdjust;

		// Token: 0x04000018 RID: 24
		private readonly Image _imagePlay;

		// Token: 0x04000019 RID: 25
		private readonly Image _imageStop;

		// Token: 0x0400001A RID: 26
		private IContainer components;

		// Token: 0x0400001B RID: 27
		private TextBox textBoxGoto;

		// Token: 0x0400001C RID: 28
		private Label labelGoto;

		// Token: 0x0400001D RID: 29
		private Button buttonFaster;

		// Token: 0x0400001E RID: 30
		private Button buttonSlower;

		// Token: 0x0400001F RID: 31
		private CheckBox checkBoxLoop;

		// Token: 0x04000020 RID: 32
		private Button buttonPlay;

		// Token: 0x04000021 RID: 33
		private Label label3;

		// Token: 0x04000022 RID: 34
		private ListBox listBoxAnimations;

		// Token: 0x04000023 RID: 35
		private TimeSlideControl timeSlideControl;

		// Token: 0x04000024 RID: 36
		private Panel panelAnimTools;

		// Token: 0x04000025 RID: 37
		private Panel panel1;

		// Token: 0x04000026 RID: 38
		private Label labelSpeed;

		// Token: 0x04000027 RID: 39
		private Label labelSpeedValue;

		// Token: 0x04000028 RID: 40
		private Label labelGotoError;
	}
}
