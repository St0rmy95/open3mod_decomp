using System;
using System.Collections.Specialized;
using System.ComponentModel;
using System.Diagnostics;
using System.DirectoryServices;
using System.Drawing;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Threading;
using System.Windows.Forms;
using Assimp;
using CoreSettings;
using OpenTK;

namespace open3mod
{
	// Token: 0x0200002C RID: 44
	public partial class MainWindow : Form
	{
		// Token: 0x14000002 RID: 2
		// (add) Token: 0x06000179 RID: 377 RVA: 0x0000CED8 File Offset: 0x0000B0D8
		// (remove) Token: 0x0600017A RID: 378 RVA: 0x0000CF10 File Offset: 0x0000B110
		public event MainWindow.TabAddRemoveHandler TabChanged;

		// Token: 0x14000003 RID: 3
		// (add) Token: 0x0600017B RID: 379 RVA: 0x0000CF48 File Offset: 0x0000B148
		// (remove) Token: 0x0600017C RID: 380 RVA: 0x0000CF80 File Offset: 0x0000B180
		public event MainWindow.TabSelectionChangeHandler SelectedTabChanged;

		// Token: 0x17000057 RID: 87
		// (get) Token: 0x0600017D RID: 381 RVA: 0x0000CFB5 File Offset: 0x0000B1B5
		public GLControl GlControl
		{
			get
			{
				return this.glControl1;
			}
		}

		// Token: 0x17000058 RID: 88
		// (get) Token: 0x0600017E RID: 382 RVA: 0x0000CFBD File Offset: 0x0000B1BD
		public UiState UiState
		{
			get
			{
				return this._ui;
			}
		}

		// Token: 0x17000059 RID: 89
		// (get) Token: 0x0600017F RID: 383 RVA: 0x0000CFC5 File Offset: 0x0000B1C5
		public FpsTracker Fps
		{
			get
			{
				return this._fps;
			}
		}

		// Token: 0x1700005A RID: 90
		// (get) Token: 0x06000180 RID: 384 RVA: 0x0000CFCD File Offset: 0x0000B1CD
		public Renderer Renderer
		{
			get
			{
				return this._renderer;
			}
		}

		// Token: 0x06000181 RID: 385 RVA: 0x0000D000 File Offset: 0x0000B200
		public MainWindow()
		{
			this._delegateSelectTab = new MainWindow.DelegateSelectTab(this.SelectTab);
			this._delegatePopulateInspector = new MainWindow.DelegatePopulateInspector(this.PopulateInspector);
			this.InitializeComponent();
			this._captionStub = this.Text;
			this.AddEmptyTab();
			this._ui = new UiState(new Tab(this._emptyTab, null));
			this._fps = new FpsTracker();
			this.framerateToolStripMenuItem.Checked = (this.toolStripButtonShowFPS.Checked = this._ui.ShowFps);
			this.lightingToolStripMenuItem.Checked = (this.toolStripButtonShowShaded.Checked = this._ui.RenderLit);
			this.texturedToolStripMenuItem.Checked = (this.toolStripButtonShowTextures.Checked = this._ui.RenderTextured);
			this.wireframeToolStripMenuItem.Checked = (this.toolStripButtonWireframe.Checked = this._ui.RenderWireframe);
			this.showNormalVectorsToolStripMenuItem.Checked = (this.toolStripButtonShowNormals.Checked = this._ui.ShowNormals);
			this.showBoundingBoxesToolStripMenuItem.Checked = (this.toolStripButtonShowBB.Checked = this._ui.ShowBBs);
			this.showAnimationSkeletonToolStripMenuItem.Checked = (this.toolStripButtonShowSkeleton.Checked = this._ui.ShowSkeleton);
			this.glControl1.MouseWheel += this.OnMouseMove;
			base.KeyPreview = true;
			this.InitRecentList();
			this.tabControl1.SelectedIndexChanged += delegate(object o, EventArgs e)
			{
				if (this.SelectedTabChanged != null)
				{
					this.SelectedTabChanged(this.UiState.TabForId(this.tabControl1.SelectedTab));
				}
			};
			this._initialized = true;
		}

		// Token: 0x1700005B RID: 91
		// (get) Token: 0x06000182 RID: 386 RVA: 0x0000D1D3 File Offset: 0x0000B3D3
		// (set) Token: 0x06000183 RID: 387 RVA: 0x0000D1DB File Offset: 0x0000B3DB
		public sealed override string Text
		{
			get
			{
				return base.Text;
			}
			set
			{
				base.Text = value;
			}
		}

		// Token: 0x06000184 RID: 388 RVA: 0x0000D1E4 File Offset: 0x0000B3E4
		private void AddEmptyTab()
		{
			this.tabControl1.TabPages.Add("empty");
			this._emptyTab = this.tabControl1.TabPages[this.tabControl1.TabPages.Count - 1];
			this.PopulateUITab(this._emptyTab);
			this.ActivateUiTab(this._emptyTab);
			if (this._ui != null)
			{
				this._ui.AddTab(new Tab(this._emptyTab, null));
			}
		}

		// Token: 0x06000185 RID: 389 RVA: 0x0000D268 File Offset: 0x0000B468
		protected override void OnCreateControl()
		{
			base.OnCreateControl();
			MainWindow.DelayExecution(TimeSpan.FromSeconds(2.0), new Action(MainWindow.MaybeShowTipOfTheDay));
			MainWindow.DelayExecution(TimeSpan.FromSeconds(20.0), new Action(MainWindow.MaybeShowDonationDialog));
		}

		// Token: 0x06000186 RID: 390 RVA: 0x0000D2BC File Offset: 0x0000B4BC
		private static void MaybeShowTipOfTheDay()
		{
			if (CoreSettings.Default.ShowTipsOnStartup)
			{
				TipOfTheDayDialog tipOfTheDayDialog = new TipOfTheDayDialog();
				tipOfTheDayDialog.ShowDialog();
			}
		}

		// Token: 0x06000187 RID: 391 RVA: 0x0000D31C File Offset: 0x0000B51C
		public static void DelayExecution(TimeSpan delay, Action action)
		{
			System.Threading.Timer timer = null;
			SynchronizationContext context = SynchronizationContext.Current;
			timer = new System.Threading.Timer(delegate(object ignore)
			{
				timer.Dispose();
				context.Post(delegate(object ignore2)
				{
					action();
				}, null);
			}, null, delay, TimeSpan.FromMilliseconds(-1.0));
		}

		// Token: 0x06000188 RID: 392 RVA: 0x0000D370 File Offset: 0x0000B570
		private static void MaybeShowDonationDialog()
		{
			if (CoreSettings.Default.DonationUseCountDown == 0)
			{
				CoreSettings.Default.DonationUseCountDown = 10;
			}
			if (CoreSettings.Default.DonationUseCountDown != -1 && --CoreSettings.Default.DonationUseCountDown == 0)
			{
				CoreSettings.Default.DonationUseCountDown = 10;
				DonationDialog donationDialog = new DonationDialog();
				donationDialog.ShowDialog();
			}
		}

		// Token: 0x06000189 RID: 393 RVA: 0x0000D3D4 File Offset: 0x0000B5D4
		private void PopulateUITab(TabPage ui)
		{
			TabUiSkeleton tabUiSkeleton = new TabUiSkeleton();
			tabUiSkeleton.Size = ui.ClientSize;
			tabUiSkeleton.AutoSize = false;
			tabUiSkeleton.Dock = DockStyle.Fill;
			ui.Controls.Add(tabUiSkeleton);
		}

		// Token: 0x0600018A RID: 394 RVA: 0x0000D410 File Offset: 0x0000B610
		private void ActivateUiTab(TabPage ui)
		{
			((TabUiSkeleton)ui.Controls[0]).InjectGlControl(this.glControl1);
			if (this._renderer != null)
			{
				this._renderer.TextOverlay.Clear();
			}
			if (this.UiState != null)
			{
				Tab tab = this.UiState.TabForId(ui);
				if (tab != null)
				{
					if (!string.IsNullOrEmpty(tab.File))
					{
						this.Text = this._captionStub + "  [" + tab.File + "]";
						return;
					}
					this.Text = this._captionStub;
				}
			}
		}

		// Token: 0x0600018B RID: 395 RVA: 0x0000D4C8 File Offset: 0x0000B6C8
		public void AddTab(string file, bool async = true, bool setActive = true)
		{
			this.AddRecentItem(file);
			for (int i = 0; i < this.tabControl1.TabPages.Count; i++)
			{
				Tab tab = this.UiState.TabForId(this.tabControl1.TabPages[i]);
				if (tab.File == file)
				{
					if (setActive)
					{
						this.SelectTab(this.tabControl1.TabPages[i]);
					}
					return;
				}
			}
			string key = this.GenerateTabKey();
			this.tabControl1.TabPages.Add(key, this.GenerateTabCaption(file) + " (loading)");
			TabPage tabPage = this.tabControl1.TabPages[key];
			tabPage.ToolTipText = file;
			this.tabControl1.ShowToolTips = true;
			this.PopulateUITab(tabPage);
			Tab t = new Tab(tabPage, file);
			this.UiState.AddTab(t);
			if (this.TabChanged != null)
			{
				this.TabChanged(t, true);
			}
			if (async)
			{
				Thread thread = new Thread(delegate()
				{
					this.OpenFile(t, setActive);
				});
				thread.Start();
			}
			else
			{
				this.OpenFile(t, setActive);
			}
			if (this._emptyTab != null)
			{
				this.CloseTab(this._emptyTab);
			}
		}

		// Token: 0x0600018C RID: 396 RVA: 0x0000D65C File Offset: 0x0000B85C
		private void InitRecentList()
		{
			this.recentToolStripMenuItem.DropDownItems.Clear();
			StringCollection stringCollection = CoreSettings.Default.RecentFiles;
			if (stringCollection == null)
			{
				stringCollection = (CoreSettings.Default.RecentFiles = new StringCollection());
				CoreSettings.Default.Save();
			}
			foreach (string path2 in stringCollection)
			{
				ToolStripItem toolStripItem = this.recentToolStripMenuItem.DropDownItems.Add(Path.GetFileName(path2));
				string path = path2;
				toolStripItem.Click += delegate(object sender, EventArgs args)
				{
					this.AddTab(path, true, true);
				};
			}
		}

		// Token: 0x0600018D RID: 397 RVA: 0x0000D748 File Offset: 0x0000B948
		private void AddRecentItem(string file)
		{
			StringCollection recentFiles = CoreSettings.Default.RecentFiles;
			bool flag = false;
			int num = 0;
			foreach (string text in recentFiles)
			{
				if (text == file)
				{
					this.recentToolStripMenuItem.DropDownItems.RemoveAt(num);
					recentFiles.Remove(text);
					flag = true;
					break;
				}
				num++;
			}
			if (!flag && recentFiles.Count == 12)
			{
				recentFiles.RemoveAt(recentFiles.Count - 1);
			}
			recentFiles.Insert(0, file);
			CoreSettings.Default.Save();
			this.recentToolStripMenuItem.DropDownItems.Insert(0, new ToolStripMenuItem(Path.GetFileName(file), null, delegate(object sender, EventArgs args)
			{
				this.AddTab(file, true, true);
			}));
		}

		// Token: 0x0600018E RID: 398 RVA: 0x0000D850 File Offset: 0x0000BA50
		private string GenerateTabCaption(string file)
		{
			string fileName = Path.GetFileName(file);
			for (int i = 0; i < this.tabControl1.TabPages.Count; i++)
			{
				if (fileName == this.tabControl1.TabPages[i].Text)
				{
					string text = null;
					int num = 2;
					while (text == null)
					{
						text = string.Concat(new object[]
						{
							fileName,
							" (",
							num,
							")"
						});
						for (int j = 0; j < this.tabControl1.TabPages.Count; j++)
						{
							if (text == this.tabControl1.TabPages[j].Text)
							{
								text = null;
								break;
							}
						}
						num++;
					}
					return text;
				}
			}
			return fileName;
		}

		// Token: 0x0600018F RID: 399 RVA: 0x0000D92C File Offset: 0x0000BB2C
		private void CloseTab(TabPage tab)
		{
			if (this.tabControl1.TabCount == 1)
			{
				if (CoreSettings.Default.ExitOnTabClosing)
				{
					Application.Exit();
					return;
				}
				this.AddEmptyTab();
			}
			if (tab == this.tabControl1.SelectedTab)
			{
				for (int i = 0; i < this.tabControl1.TabCount; i++)
				{
					if (this.tabControl1.TabPages[i] != tab)
					{
						this.SelectTab(this.tabControl1.TabPages[i]);
						break;
					}
				}
			}
			this.UiState.RemoveTab(tab);
			this.tabControl1.TabPages.Remove(tab);
			if (this.TabChanged != null)
			{
				this.TabChanged((Tab)tab.Tag, false);
			}
			if (this._emptyTab == tab)
			{
				this._emptyTab = null;
			}
		}

		// Token: 0x06000190 RID: 400 RVA: 0x0000D9FC File Offset: 0x0000BBFC
		public void SelectTab(TabPage tab)
		{
			this.tabControl1.SelectedTab = tab;
			Tab tab2 = this.UiState.TabForId(tab);
			if (tab2.ActiveScene != null)
			{
				this.toolStripStatistics.Text = tab2.ActiveScene.StatsString;
			}
			else
			{
				this.toolStripStatistics.Text = "";
			}
			this.UiState.SelectTab(tab);
			Tab.ViewMode activeViewMode = this._ui.ActiveTab.ActiveViewMode;
			this.fullViewToolStripMenuItem.CheckState = (this.toolStripButtonFullView.CheckState = ((activeViewMode == Tab.ViewMode.Single) ? CheckState.Checked : CheckState.Unchecked));
			this.twoViewsToolStripMenuItem.CheckState = (this.toolStripButtonTwoViews.CheckState = ((activeViewMode == Tab.ViewMode.Two) ? CheckState.Checked : CheckState.Unchecked));
			this.fourViewsToolStripMenuItem.CheckState = (this.toolStripButtonFourViews.CheckState = ((activeViewMode == Tab.ViewMode.Four) ? CheckState.Checked : CheckState.Unchecked));
			this.ActivateUiTab(tab);
		}

		// Token: 0x06000191 RID: 401 RVA: 0x0000DADC File Offset: 0x0000BCDC
		private string GenerateTabKey()
		{
			int num = ++MainWindow._tabCounter;
			return num.ToString(CultureInfo.InvariantCulture);
		}

		// Token: 0x06000192 RID: 402 RVA: 0x0000DB8C File Offset: 0x0000BD8C
		private void OpenFile(Tab tab, bool setActive)
		{
			try
			{
				tab.ActiveScene = new Scene(tab.File);
				CoreSettings.Default.CountFilesOpened++;
			}
			catch (Exception ex)
			{
				tab.SetFailed(ex.Message);
			}
			MethodInvoker methodInvoker = delegate()
			{
				TabPage tabPage = (TabPage)tab.Id;
				if (!tabPage.Text.EndsWith(" (loading)"))
				{
					return;
				}
				tabPage.Text = tabPage.Text.Substring(0, tabPage.Text.Length - " (loading)".Length);
				if (tab.State == Tab.TabState.Failed)
				{
					tabPage.Text += " (failed)";
				}
			};
			if (!this._initialized)
			{
				if (setActive)
				{
					this.SelectTab((TabPage)tab.Id);
				}
				this.PopulateInspector(tab);
				methodInvoker();
				return;
			}
			if (setActive)
			{
				base.BeginInvoke(this._delegateSelectTab, new object[]
				{
					tab.Id
				});
			}
			base.BeginInvoke(this._delegatePopulateInspector, new object[]
			{
				tab
			});
			base.BeginInvoke(methodInvoker);
		}

		// Token: 0x06000193 RID: 403 RVA: 0x0000DC8C File Offset: 0x0000BE8C
		private void PopulateInspector(Tab tab)
		{
			TabUiSkeleton tabUiSkeleton = this.UiForTab(tab);
			InspectionView inspector = tabUiSkeleton.GetInspector();
			inspector.SetSceneSource(tab.ActiveScene);
		}

		// Token: 0x06000194 RID: 404 RVA: 0x0000DCB4 File Offset: 0x0000BEB4
		public TabUiSkeleton UiForTab(Tab tab)
		{
			return (TabUiSkeleton)((TabPage)tab.Id).Controls[0];
		}

		// Token: 0x06000195 RID: 405 RVA: 0x0000DCD1 File Offset: 0x0000BED1
		private void ToolsToolStripMenuItemClick(object sender, EventArgs e)
		{
		}

		// Token: 0x06000196 RID: 406 RVA: 0x0000DCD4 File Offset: 0x0000BED4
		private void AboutToolStripMenuItemClick(object sender, EventArgs e)
		{
			About about = new About();
			about.ShowDialog();
		}

		// Token: 0x06000197 RID: 407 RVA: 0x0000DCEE File Offset: 0x0000BEEE
		private void OnGlLoad(object sender, EventArgs e)
		{
			if (this._renderer != null)
			{
				this._renderer.Dispose();
			}
			this._renderer = new Renderer(this);
			Application.Idle += this.ApplicationIdle;
		}

		// Token: 0x06000198 RID: 408 RVA: 0x0000DD20 File Offset: 0x0000BF20
		private void OnGlResize(object sender, EventArgs e)
		{
			if (this._renderer == null)
			{
				return;
			}
			this._renderer.Resize();
		}

		// Token: 0x06000199 RID: 409 RVA: 0x0000DD36 File Offset: 0x0000BF36
		private void GlPaint(object sender, PaintEventArgs e)
		{
			if (this._renderer == null)
			{
				return;
			}
			this.FrameRender();
		}

		// Token: 0x0600019A RID: 410 RVA: 0x0000DD47 File Offset: 0x0000BF47
		private void ApplicationIdle(object sender, EventArgs e)
		{
			if (base.IsDisposed)
			{
				return;
			}
			while (this.glControl1.IsIdle)
			{
				this.FrameUpdate();
				this.FrameRender();
			}
		}

		// Token: 0x0600019B RID: 411 RVA: 0x0000DD6C File Offset: 0x0000BF6C
		private void FrameUpdate()
		{
			this._fps.Update();
			double lastFrameDelta = this._fps.LastFrameDelta;
			this._renderer.Update(lastFrameDelta);
			foreach (Tab tab in this.UiState.Tabs)
			{
				if (tab.ActiveScene != null)
				{
					tab.ActiveScene.Update(lastFrameDelta, tab != this.UiState.ActiveTab);
				}
			}
			this.ProcessKeys();
		}

		// Token: 0x0600019C RID: 412 RVA: 0x0000DE0C File Offset: 0x0000C00C
		private void FrameRender()
		{
			this._renderer.Draw(this._ui.ActiveTab);
			this.glControl1.SwapBuffers();
		}

		// Token: 0x0600019D RID: 413 RVA: 0x0000DE30 File Offset: 0x0000C030
		private void ToggleFps(object sender, EventArgs e)
		{
			this._ui.ShowFps = !this._ui.ShowFps;
			this.framerateToolStripMenuItem.Checked = (this.toolStripButtonShowFPS.Checked = this._ui.ShowFps);
		}

		// Token: 0x0600019E RID: 414 RVA: 0x0000DE7C File Offset: 0x0000C07C
		private void ToggleShading(object sender, EventArgs e)
		{
			this._ui.RenderLit = !this._ui.RenderLit;
			this.lightingToolStripMenuItem.Checked = (this.toolStripButtonShowShaded.Checked = this._ui.RenderLit);
		}

		// Token: 0x0600019F RID: 415 RVA: 0x0000DEC8 File Offset: 0x0000C0C8
		private void ToggleTextures(object sender, EventArgs e)
		{
			this._ui.RenderTextured = !this._ui.RenderTextured;
			this.texturedToolStripMenuItem.Checked = (this.toolStripButtonShowTextures.Checked = this._ui.RenderTextured);
		}

		// Token: 0x060001A0 RID: 416 RVA: 0x0000DF14 File Offset: 0x0000C114
		private void ToggleWireframe(object sender, EventArgs e)
		{
			this._ui.RenderWireframe = !this._ui.RenderWireframe;
			this.wireframeToolStripMenuItem.Checked = (this.toolStripButtonWireframe.Checked = this._ui.RenderWireframe);
		}

		// Token: 0x060001A1 RID: 417 RVA: 0x0000DF60 File Offset: 0x0000C160
		private void ToggleShowBb(object sender, EventArgs e)
		{
			this._ui.ShowBBs = !this._ui.ShowBBs;
			this.showBoundingBoxesToolStripMenuItem.Checked = (this.toolStripButtonShowBB.Checked = this._ui.ShowBBs);
		}

		// Token: 0x060001A2 RID: 418 RVA: 0x0000DFAC File Offset: 0x0000C1AC
		private void ToggleShowNormals(object sender, EventArgs e)
		{
			this._ui.ShowNormals = !this._ui.ShowNormals;
			this.showNormalVectorsToolStripMenuItem.Checked = (this.toolStripButtonShowNormals.Checked = this._ui.ShowNormals);
		}

		// Token: 0x060001A3 RID: 419 RVA: 0x0000DFF8 File Offset: 0x0000C1F8
		private void ToggleShowSkeleton(object sender, EventArgs e)
		{
			this._ui.ShowSkeleton = !this._ui.ShowSkeleton;
			this.showAnimationSkeletonToolStripMenuItem.Checked = (this.toolStripButtonShowSkeleton.Checked = this._ui.ShowSkeleton);
		}

		// Token: 0x060001A4 RID: 420 RVA: 0x0000E044 File Offset: 0x0000C244
		private void ToggleFullView(object sender, EventArgs e)
		{
			if (this.UiState.ActiveTab.ActiveViewMode == Tab.ViewMode.Single)
			{
				return;
			}
			this.UiState.ActiveTab.ActiveViewMode = Tab.ViewMode.Single;
			this.toolStripButtonFullView.CheckState = CheckState.Checked;
			this.toolStripButtonTwoViews.CheckState = CheckState.Unchecked;
			this.toolStripButtonFourViews.CheckState = CheckState.Unchecked;
			this.fullViewToolStripMenuItem.CheckState = CheckState.Checked;
			this.twoViewsToolStripMenuItem.CheckState = CheckState.Unchecked;
			this.fourViewsToolStripMenuItem.CheckState = CheckState.Unchecked;
		}

		// Token: 0x060001A5 RID: 421 RVA: 0x0000E0C0 File Offset: 0x0000C2C0
		private void ToggleTwoViews(object sender, EventArgs e)
		{
			if (this.UiState.ActiveTab.ActiveViewMode == Tab.ViewMode.Two)
			{
				return;
			}
			this.UiState.ActiveTab.ActiveViewMode = Tab.ViewMode.Two;
			this.toolStripButtonFullView.CheckState = CheckState.Unchecked;
			this.toolStripButtonTwoViews.CheckState = CheckState.Checked;
			this.toolStripButtonFourViews.CheckState = CheckState.Unchecked;
			this.fullViewToolStripMenuItem.CheckState = CheckState.Unchecked;
			this.twoViewsToolStripMenuItem.CheckState = CheckState.Checked;
			this.fourViewsToolStripMenuItem.CheckState = CheckState.Unchecked;
		}

		// Token: 0x060001A6 RID: 422 RVA: 0x0000E13C File Offset: 0x0000C33C
		private void ToggleFourViews(object sender, EventArgs e)
		{
			if (this.UiState.ActiveTab.ActiveViewMode == Tab.ViewMode.Four)
			{
				return;
			}
			this.UiState.ActiveTab.ActiveViewMode = Tab.ViewMode.Four;
			this.toolStripButtonFullView.CheckState = CheckState.Unchecked;
			this.toolStripButtonTwoViews.CheckState = CheckState.Unchecked;
			this.toolStripButtonFourViews.CheckState = CheckState.Checked;
			this.fullViewToolStripMenuItem.CheckState = CheckState.Unchecked;
			this.twoViewsToolStripMenuItem.CheckState = CheckState.Unchecked;
			this.fourViewsToolStripMenuItem.CheckState = CheckState.Checked;
		}

		// Token: 0x060001A7 RID: 423 RVA: 0x0000E1B8 File Offset: 0x0000C3B8
		private void OnTabSelected(object sender, TabControlEventArgs e)
		{
			TabPage selectedTab = this.tabControl1.SelectedTab;
			this.SelectTab(selectedTab);
		}

		// Token: 0x060001A8 RID: 424 RVA: 0x0000E1D8 File Offset: 0x0000C3D8
		private void OnShowTabContextMenu(object sender, MouseEventArgs e)
		{
			if (e.Button == MouseButtons.Right)
			{
				for (int i = 0; i < this.tabControl1.TabCount; i++)
				{
					if (this.tabControl1.GetTabRect(i).Contains(e.Location))
					{
						this._tabContextMenuOwner = this.tabControl1.TabPages[i];
						this.tabContextMenuStrip.Show(this.tabControl1, e.Location);
					}
				}
			}
		}

		// Token: 0x060001A9 RID: 425 RVA: 0x0000E252 File Offset: 0x0000C452
		private void OnCloseTabFromContextMenu(object sender, EventArgs e)
		{
			this.CloseTab(this._tabContextMenuOwner);
		}

		// Token: 0x060001AA RID: 426 RVA: 0x0000E260 File Offset: 0x0000C460
		private void OnCloseAllTabsButThisFromContextMenu(object sender, EventArgs e)
		{
			while (this.tabControl1.TabCount > 1)
			{
				for (int i = 0; i < this.tabControl1.TabCount; i++)
				{
					if (this._tabContextMenuOwner != this.tabControl1.TabPages[i])
					{
						this.CloseTab(this.tabControl1.TabPages[i]);
					}
				}
			}
		}

		// Token: 0x060001AB RID: 427 RVA: 0x0000E2C3 File Offset: 0x0000C4C3
		private void OnCloseTab(object sender, EventArgs e)
		{
			if (this.tabControl1.SelectedTab != null)
			{
				this.CloseTab(this.tabControl1.SelectedTab);
			}
		}

		// Token: 0x060001AC RID: 428 RVA: 0x0000E2E4 File Offset: 0x0000C4E4
		private void OnFileMenuOpen(object sender, EventArgs e)
		{
			if (this.openFileDialog.ShowDialog() == DialogResult.OK)
			{
				string[] fileNames = this.openFileDialog.FileNames;
				bool setActive = true;
				foreach (string file in fileNames)
				{
					this.AddTab(file, true, setActive);
					setActive = false;
				}
			}
		}

		// Token: 0x060001AD RID: 429 RVA: 0x0000E334 File Offset: 0x0000C534
		private void OnFileMenuCloseAll(object sender, EventArgs e)
		{
			while (this.tabControl1.TabPages.Count > 1)
			{
				this.CloseTab(this.tabControl1.TabPages[0]);
			}
			this.CloseTab(this.tabControl1.TabPages[0]);
		}

		// Token: 0x060001AE RID: 430 RVA: 0x0000E384 File Offset: 0x0000C584
		private void OnFileMenuRecent(object sender, EventArgs e)
		{
		}

		// Token: 0x060001AF RID: 431 RVA: 0x0000E386 File Offset: 0x0000C586
		private void OnFileMenuQuit(object sender, EventArgs e)
		{
			base.Close();
		}

		// Token: 0x060001B0 RID: 432 RVA: 0x0000E38E File Offset: 0x0000C58E
		private void OnCloseForm(object sender, FormClosedEventArgs e)
		{
			this.UiState.Dispose();
			this._renderer.Dispose();
		}

		// Token: 0x060001B1 RID: 433 RVA: 0x0000E3A8 File Offset: 0x0000C5A8
		private void OnShowSettings(object sender, EventArgs e)
		{
			if (this._settings == null || this._settings.IsDisposed)
			{
				this._settings = new SettingsDialog
				{
					Main = this
				};
			}
			if (!this._settings.Visible)
			{
				this._settings.Show();
			}
		}

		// Token: 0x060001B2 RID: 434 RVA: 0x0000E3F6 File Offset: 0x0000C5F6
		public void CloseSettingsDialog()
		{
			this._settings.Close();
			this._settings = null;
		}

		// Token: 0x060001B3 RID: 435 RVA: 0x0000E40C File Offset: 0x0000C60C
		private void OnExport(object sender, EventArgs e)
		{
			ExportDialog exportDialog = new ExportDialog(this);
			exportDialog.Show(this);
		}

		// Token: 0x060001B4 RID: 436 RVA: 0x0000E440 File Offset: 0x0000C640
		private void OnDrag(object sender, DragEventArgs e)
		{
			try
			{
				Array array = (Array)e.Data.GetData(DataFormats.FileDrop);
				if (array != null && array.GetLength(0) > 0)
				{
					int i = 0;
					int length = array.GetLength(0);
					while (i < length)
					{
						string text = array.GetValue(i).ToString();
						try
						{
							FileAttributes attributes = File.GetAttributes(text);
							if (attributes.HasFlag(FileAttributes.Directory))
							{
								string[] supportedImportFormats;
								using (AssimpContext assimpContext = new AssimpContext())
								{
									supportedImportFormats = assimpContext.GetSupportedImportFormats();
								}
								string[] files = Directory.GetFiles(text);
								string[] array2 = files;
								for (int j = 0; j < array2.Length; j++)
								{
									string text2 = array2[j];
									string extension = Path.GetExtension(text2);
									if (extension != null)
									{
										string lowerExt = extension.ToLower();
										if (supportedImportFormats.Any((string format) => lowerExt == format))
										{
											this.AddTab(text2, true, true);
										}
									}
								}
								goto IL_FB;
							}
						}
						catch (Exception)
						{
						}
						goto IL_F2;
						IL_FB:
						i++;
						continue;
						IL_F2:
						this.AddTab(text, true, true);
						goto IL_FB;
					}
					base.Activate();
				}
			}
			catch (Exception ex)
			{
				Trace.WriteLine("Error in DragDrop function: " + ex.Message);
			}
		}

		// Token: 0x060001B5 RID: 437 RVA: 0x0000E5C4 File Offset: 0x0000C7C4
		private void OnDragEnter(object sender, DragEventArgs e)
		{
			e.Effect = (e.Data.GetDataPresent(DataFormats.FileDrop) ? DragDropEffects.Copy : DragDropEffects.None);
		}

		// Token: 0x060001B6 RID: 438 RVA: 0x0000E5E4 File Offset: 0x0000C7E4
		private void OnLoad(object sender, EventArgs e)
		{
			if (CoreSettings.Default.Maximized)
			{
				base.WindowState = FormWindowState.Maximized;
				base.Location = CoreSettings.Default.Location;
				base.Size = CoreSettings.Default.Size;
				return;
			}
			Size size = CoreSettings.Default.Size;
			if (size.Width != 0)
			{
				base.Location = CoreSettings.Default.Location;
				base.Size = size;
			}
		}

		// Token: 0x060001B7 RID: 439 RVA: 0x0000E650 File Offset: 0x0000C850
		private void OnClosing(object sender, FormClosingEventArgs e)
		{
			if (base.WindowState == FormWindowState.Maximized)
			{
				CoreSettings.Default.Location = base.RestoreBounds.Location;
				CoreSettings.Default.Size = base.RestoreBounds.Size;
				CoreSettings.Default.Maximized = true;
			}
			else
			{
				CoreSettings.Default.Location = base.Location;
				CoreSettings.Default.Size = base.Size;
				CoreSettings.Default.Maximized = false;
			}
			CoreSettings.Default.Save();
		}

		// Token: 0x060001B8 RID: 440 RVA: 0x0000E6D8 File Offset: 0x0000C8D8
		private void OnTipOfTheDay(object sender, EventArgs e)
		{
			TipOfTheDayDialog tipOfTheDayDialog = new TipOfTheDayDialog();
			tipOfTheDayDialog.ShowDialog();
		}

		// Token: 0x060001B9 RID: 441 RVA: 0x0000E6F4 File Offset: 0x0000C8F4
		private void OnDonate(object sender, LinkLabelLinkClickedEventArgs e)
		{
			this.linkLabelDonate.LinkVisited = false;
			DonationDialog donationDialog = new DonationDialog();
			donationDialog.ShowDialog();
		}

		// Token: 0x060001BA RID: 442 RVA: 0x0000E728 File Offset: 0x0000C928
		private void OnSetFileAssociations(object sender, EventArgs e)
		{
			using (AssimpContext assimpContext = new AssimpContext())
			{
				string[] supportedImportFormats = assimpContext.GetSupportedImportFormats();
				string[] value = (from s in supportedImportFormats
				where s != ".xml"
				select s).ToArray<string>();
				string str = string.Join(", ", value);
				if (DialogResult.OK == MessageBox.Show(this, "The following file extensions will be associated with open3mod: " + str, "Set file associations", MessageBoxButtons.OKCancel))
				{
					if (!FileAssociations.SetAssociations(supportedImportFormats))
					{
						MessageBox.Show(this, "Failed to set file extensions", "Error", MessageBoxButtons.OK, MessageBoxIcon.Hand);
					}
					else
					{
						MessageBox.Show(this, "File extensions have been successfully associated", "open3mod", MessageBoxButtons.OK, MessageBoxIcon.Asterisk);
					}
				}
			}
		}

		// Token: 0x060001BB RID: 443 RVA: 0x0000E7E0 File Offset: 0x0000C9E0
		private void linkLabelWebsite_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
		{
			Process.Start("http://www.open3mod.com/");
		}

		// Token: 0x060001BC RID: 444 RVA: 0x0000E7ED File Offset: 0x0000C9ED
		private void wusonToolStripMenuItem_Click(object sender, EventArgs e)
		{
			this.AddTab(File.Exists("../../../testdata/scenes/spider.obj") ? "../../../testdata/scenes/spider.obj" : "testscenes/spider/spider.obj", true, true);
		}

		// Token: 0x060001BD RID: 445 RVA: 0x0000E80F File Offset: 0x0000CA0F
		private void jeepToolStripMenuItem_Click(object sender, EventArgs e)
		{
			this.AddTab(File.Exists("../../../testdata/redist/jeep/jeep1.ms3d") ? "../../../testdata/redist/jeep/jeep1.ms3d" : "testscenes/jeep/jeep1.ms3d", true, true);
		}

		// Token: 0x060001BE RID: 446 RVA: 0x0000E831 File Offset: 0x0000CA31
		private void duckToolStripMenuItem_Click(object sender, EventArgs e)
		{
			this.AddTab(File.Exists("../../../testdata/redist/duck/duck.dae") ? "../../../testdata/redist/duck/duck.dae" : "testscenes/duck/duck.dae", true, true);
		}

		// Token: 0x060001BF RID: 447 RVA: 0x0000E853 File Offset: 0x0000CA53
		private void wustonAnimatedToolStripMenuItem_Click(object sender, EventArgs e)
		{
			this.AddTab(File.Exists("../../../testdata/scenes/Testwuson.X") ? "../../../testdata/scenes/Testwuson.X" : "testscenes/wuson/Testwuson.X", true, true);
		}

		// Token: 0x060001C0 RID: 448 RVA: 0x0000E875 File Offset: 0x0000CA75
		private void lostEmpireToolStripMenuItem_Click(object sender, EventArgs e)
		{
			this.AddTab(File.Exists("../../../testdata/redist/lost-empire/lost_empire.obj") ? "../../../testdata/redist/lost-empire/lost_empire.obj" : "testscenes/lost-empire/lost_empire.obj", true, true);
		}

		// Token: 0x060001C3 RID: 451 RVA: 0x00010BC0 File Offset: 0x0000EDC0
		private void ProcessKeys()
		{
			ICameraController activeCameraController = this.UiState.ActiveTab.ActiveCameraController;
			if (activeCameraController == null)
			{
				return;
			}
			float num = (float)this._fps.LastFrameDelta;
			float num2 = 0f;
			float num3 = 0f;
			float num4 = 0f;
			bool flag = false;
			if (this._forwardPressed)
			{
				flag = true;
				num4 -= num;
			}
			if (this._backPressed)
			{
				flag = true;
				num4 += num;
			}
			if (this._rightPressed)
			{
				flag = true;
				num2 += num;
			}
			if (this._leftPressed)
			{
				flag = true;
				num2 -= num;
			}
			if (this._upPressed)
			{
				flag = true;
				num3 += num;
			}
			if (this._downPressed)
			{
				flag = true;
				num3 -= num;
			}
			if (!flag)
			{
				return;
			}
			activeCameraController.MovementKey(num2, num3, num4);
		}

		// Token: 0x060001C4 RID: 452 RVA: 0x00010C72 File Offset: 0x0000EE72
		private void UpdateActiveViewIfNeeded(MouseEventArgs e)
		{
			this._ui.ActiveTab.ActiveViewIndex = this.MousePosToViewportIndex(e.X, e.Y);
		}

		// Token: 0x060001C5 RID: 453 RVA: 0x00010C98 File Offset: 0x0000EE98
		private Tab.ViewIndex MousePosToViewportIndex(int x, int y)
		{
			float x2 = (float)x / (float)this.glControl1.ClientSize.Width;
			float y2 = 1f - (float)y / (float)this.glControl1.ClientSize.Height;
			return this._ui.ActiveTab.GetViewportIndexHit(x2, y2);
		}

		// Token: 0x060001C6 RID: 454 RVA: 0x00010CED File Offset: 0x0000EEED
		private void SetViewportSplitH(float f)
		{
			this._ui.ActiveTab.SetViewportSplitH(f);
		}

		// Token: 0x060001C7 RID: 455 RVA: 0x00010D00 File Offset: 0x0000EF00
		private void SetViewportSplitV(float f)
		{
			this._ui.ActiveTab.SetViewportSplitV(f);
		}

		// Token: 0x060001C8 RID: 456 RVA: 0x00010D14 File Offset: 0x0000EF14
		private Tab.ViewSeparator MousePosToViewportSeparator(int x, int y)
		{
			float x2 = (float)x / (float)this.glControl1.ClientSize.Width;
			float y2 = 1f - (float)y / (float)this.glControl1.ClientSize.Height;
			return this._ui.ActiveTab.GetViewportSeparatorHit(x2, y2);
		}

		// Token: 0x060001C9 RID: 457 RVA: 0x00010D74 File Offset: 0x0000EF74
		private void OnShowLogViewer(object sender, EventArgs e)
		{
			if (this._logViewer == null)
			{
				this._logViewer = new LogViewer(this);
				this._logViewer.Closed += delegate(object o, EventArgs args)
				{
					this._logViewer = null;
				};
				this._logViewer.Show();
			}
		}

		// Token: 0x060001CA RID: 458 RVA: 0x00010DC0 File Offset: 0x0000EFC0
		private void OnMouseDown(object sender, MouseEventArgs e)
		{
			this.UpdateActiveViewIfNeeded(e);
			this._previousMousePosX = e.X;
			this._previousMousePosY = e.Y;
			if (e.Button == MouseButtons.Middle)
			{
				this._mouseWheelDown = true;
				return;
			}
			if (e.Button == MouseButtons.Right)
			{
				this._mouseRightDown = true;
				return;
			}
			if (e.Button != MouseButtons.Left)
			{
				return;
			}
			this._mouseDown = true;
			Tab.ViewSeparator viewSeparator = this.MousePosToViewportSeparator(e.X, e.Y);
			if (viewSeparator != Tab.ViewSeparator._Max)
			{
				this._dragSeparator = viewSeparator;
				this.SetViewportDragCursor(viewSeparator);
			}
			Tab.ViewIndex viewIndex = this.MousePosToViewportIndex(e.X, e.Y);
			if (viewIndex == Tab.ViewIndex._Max)
			{
				return;
			}
			if (viewSeparator == Tab.ViewSeparator._Max)
			{
				Viewport viewport = this.UiState.ActiveTab.ActiveViews[(int)viewIndex];
				this._renderer.OnMouseClick(e, viewport.Bounds, viewIndex);
			}
		}

		// Token: 0x060001CB RID: 459 RVA: 0x00010E94 File Offset: 0x0000F094
		private void OnMouseUp(object sender, MouseEventArgs e)
		{
			if (e.Button == MouseButtons.Left)
			{
				this._mouseDown = false;
			}
			if (e.Button == MouseButtons.Middle)
			{
				this._mouseWheelDown = false;
			}
			if (e.Button == MouseButtons.Right)
			{
				this._mouseRightDown = false;
			}
			if (!this.IsDraggingViewportSeparator)
			{
				return;
			}
			this._dragSeparator = Tab.ViewSeparator._Max;
			this.Cursor = Cursors.Default;
		}

		// Token: 0x1700005C RID: 92
		// (get) Token: 0x060001CC RID: 460 RVA: 0x00010EF8 File Offset: 0x0000F0F8
		public bool IsDraggingViewportSeparator
		{
			get
			{
				return this._dragSeparator != Tab.ViewSeparator._Max;
			}
		}

		// Token: 0x060001CD RID: 461 RVA: 0x00010F08 File Offset: 0x0000F108
		private void OnMouseMove(object sender, MouseEventArgs e)
		{
			if (this._mouseWheelDown)
			{
				if (this.UiState.ActiveTab.ActiveCameraController != null)
				{
					this.UiState.ActiveTab.ActiveCameraController.Pan((float)(e.X - this._previousMousePosX), (float)(e.Y - this._previousMousePosY));
				}
				this._previousMousePosX = e.X;
				this._previousMousePosY = e.Y;
				return;
			}
			Tab.ViewSeparator viewSeparator = (this._dragSeparator != Tab.ViewSeparator._Max) ? this._dragSeparator : this.MousePosToViewportSeparator(e.X, e.Y);
			if (viewSeparator != Tab.ViewSeparator._Max)
			{
				this.SetViewportDragCursor(viewSeparator);
				if (this.IsDraggingViewportSeparator)
				{
					if (viewSeparator == Tab.ViewSeparator.Horizontal)
					{
						this.SetViewportSplitV(1f - (float)e.Y / (float)this.glControl1.ClientSize.Height);
						return;
					}
					if (viewSeparator == Tab.ViewSeparator.Vertical)
					{
						this.SetViewportSplitH((float)e.X / (float)this.glControl1.ClientSize.Width);
						return;
					}
					if (viewSeparator == Tab.ViewSeparator.Both)
					{
						this.SetViewportSplitV(1f - (float)e.Y / (float)this.glControl1.ClientSize.Height);
						this.SetViewportSplitH((float)e.X / (float)this.glControl1.ClientSize.Width);
					}
				}
				return;
			}
			this.Cursor = Cursors.Default;
			if (e.Delta != 0 && this.UiState.ActiveTab.ActiveCameraController != null)
			{
				this.UiState.ActiveTab.ActiveCameraController.Scroll((float)e.Delta);
			}
			Tab.ViewIndex viewIndex = this.MousePosToViewportIndex(e.X, e.Y);
			if (viewIndex == Tab.ViewIndex._Max)
			{
				return;
			}
			Viewport viewport = this.UiState.ActiveTab.ActiveViews[(int)viewIndex];
			this._renderer.OnMouseMove(e, viewport.Bounds, viewIndex);
			if (!this._mouseDown && !this._mouseRightDown)
			{
				return;
			}
			if (this.UiState.ActiveTab.ActiveCameraController != null)
			{
				int num = e.X - this._previousMousePosX;
				int num2 = e.Y - this._previousMousePosY;
				if (this._mouseRightDown)
				{
					Matrix4 matrix = (this.UiState.ActiveTab.ActiveCameraController == null) ? Matrix4.Identity : this.UiState.ActiveTab.ActiveCameraController.GetView();
					this.Renderer.HandleLightRotationOnMouseMove(num, num2, ref matrix);
				}
				else
				{
					this.UiState.ActiveTab.ActiveCameraController.MouseMove(num, num2);
				}
			}
			this._previousMousePosX = e.X;
			this._previousMousePosY = e.Y;
		}

		// Token: 0x060001CE RID: 462 RVA: 0x00011198 File Offset: 0x0000F398
		private void SetViewportDragCursor(Tab.ViewSeparator sep)
		{
			switch (sep)
			{
			case Tab.ViewSeparator.Horizontal:
				this.Cursor = Cursors.HSplit;
				return;
			case Tab.ViewSeparator.Vertical:
				this.Cursor = Cursors.VSplit;
				return;
			case Tab.ViewSeparator.Both:
				this.Cursor = Cursors.SizeAll;
				return;
			}
			throw new ArgumentOutOfRangeException();
		}

		// Token: 0x060001CF RID: 463 RVA: 0x000111E8 File Offset: 0x0000F3E8
		private void OnMouseLeave(object sender, EventArgs e)
		{
			if (this._mouseDown)
			{
				base.Capture = true;
			}
			this.Cursor = Cursors.Default;
		}

		// Token: 0x060001D0 RID: 464 RVA: 0x00011204 File Offset: 0x0000F404
		private void OnMouseEnter(object sender, EventArgs e)
		{
			base.Capture = false;
		}

		// Token: 0x060001D1 RID: 465 RVA: 0x0001120D File Offset: 0x0000F40D
		protected override bool IsInputKey(Keys keyData)
		{
			return true;
		}

		// Token: 0x060001D2 RID: 466 RVA: 0x00011210 File Offset: 0x0000F410
		private void OnPreviewKeyDown(object sender, PreviewKeyDownEventArgs e)
		{
			e.IsInputKey = true;
		}

		// Token: 0x060001D3 RID: 467 RVA: 0x0001121C File Offset: 0x0000F41C
		private void OnKeyDown(object sender, KeyEventArgs e)
		{
			Keys keyData = e.KeyData;
			if (keyData <= Keys.A)
			{
				switch (keyData)
				{
				case Keys.Prior:
					this._upPressed = true;
					return;
				case Keys.Next:
					this._downPressed = true;
					return;
				case Keys.End:
				case Keys.Home:
					return;
				case Keys.Left:
					break;
				case Keys.Up:
					goto IL_57;
				case Keys.Right:
					goto IL_6F;
				case Keys.Down:
					goto IL_67;
				default:
					if (keyData != Keys.A)
					{
						return;
					}
					break;
				}
				this._leftPressed = true;
				return;
			}
			if (keyData == Keys.D)
			{
				goto IL_6F;
			}
			switch (keyData)
			{
			case Keys.R:
				this.UiState.ActiveTab.ResetActiveCameraController();
				return;
			case Keys.S:
				goto IL_67;
			default:
				if (keyData != Keys.W)
				{
					return;
				}
				break;
			}
			IL_57:
			this._forwardPressed = true;
			return;
			IL_67:
			this._backPressed = true;
			return;
			IL_6F:
			this._rightPressed = true;
		}

		// Token: 0x060001D4 RID: 468 RVA: 0x000112C0 File Offset: 0x0000F4C0
		private void OnKeyUp(object sender, KeyEventArgs e)
		{
			Keys keyData = e.KeyData;
			if (keyData <= Keys.A)
			{
				switch (keyData)
				{
				case Keys.Prior:
					this._upPressed = false;
					return;
				case Keys.Next:
					this._downPressed = false;
					return;
				case Keys.End:
				case Keys.Home:
					return;
				case Keys.Left:
					break;
				case Keys.Up:
					goto IL_4B;
				case Keys.Right:
					goto IL_63;
				case Keys.Down:
					goto IL_5B;
				default:
					if (keyData != Keys.A)
					{
						return;
					}
					break;
				}
				this._leftPressed = false;
				return;
			}
			if (keyData == Keys.D)
			{
				goto IL_63;
			}
			if (keyData == Keys.S)
			{
				goto IL_5B;
			}
			if (keyData != Keys.W)
			{
				return;
			}
			IL_4B:
			this._forwardPressed = false;
			return;
			IL_5B:
			this._backPressed = false;
			return;
			IL_63:
			this._rightPressed = false;
		}

		// Token: 0x0400011F RID: 287
		public const int MaxRecentItems = 12;

		// Token: 0x04000120 RID: 288
		private const int DonationCounterStart = 10;

		// Token: 0x04000121 RID: 289
		private const string LoadingTitlePostfix = " (loading)";

		// Token: 0x04000122 RID: 290
		private const string FailedTitlePostfix = " (failed)";

		// Token: 0x04000125 RID: 293
		private readonly FpsTracker _fps;

		// Token: 0x04000126 RID: 294
		private LogViewer _logViewer;

		// Token: 0x04000127 RID: 295
		private readonly MainWindow.DelegateSelectTab _delegateSelectTab;

		// Token: 0x04000128 RID: 296
		private readonly MainWindow.DelegatePopulateInspector _delegatePopulateInspector;

		// Token: 0x04000129 RID: 297
		private readonly bool _initialized;

		// Token: 0x0400012C RID: 300
		private static int _tabCounter;

		// Token: 0x0400012D RID: 301
		private TabPage _tabContextMenuOwner;

		// Token: 0x0400012E RID: 302
		private TabPage _emptyTab;

		// Token: 0x0400012F RID: 303
		private SettingsDialog _settings;

		// Token: 0x04000130 RID: 304
		private Tab.ViewSeparator _dragSeparator = Tab.ViewSeparator._Max;

		// Token: 0x04000131 RID: 305
		private string _captionStub;

		// Token: 0x0400017C RID: 380
		private bool _mouseWheelDown;

		// Token: 0x0400017D RID: 381
		private bool _forwardPressed;

		// Token: 0x0400017E RID: 382
		private bool _leftPressed;

		// Token: 0x0400017F RID: 383
		private bool _rightPressed;

		// Token: 0x04000180 RID: 384
		private bool _backPressed;

		// Token: 0x04000181 RID: 385
		private bool _upPressed;

		// Token: 0x04000182 RID: 386
		private bool _downPressed;

		// Token: 0x04000183 RID: 387
		private int _previousMousePosX = -1;

		// Token: 0x04000184 RID: 388
		private int _previousMousePosY = -1;

		// Token: 0x04000185 RID: 389
		private bool _mouseDown;

		// Token: 0x04000186 RID: 390
		private bool _mouseRightDown;

		// Token: 0x0200002D RID: 45
		// (Invoke) Token: 0x060001D9 RID: 473
		private delegate void DelegateSelectTab(TabPage tab);

		// Token: 0x0200002E RID: 46
		// (Invoke) Token: 0x060001DD RID: 477
		private delegate void DelegatePopulateInspector(Tab tab);

		// Token: 0x0200002F RID: 47
		// (Invoke) Token: 0x060001E1 RID: 481
		public delegate void TabAddRemoveHandler(Tab tab, bool add);

		// Token: 0x02000030 RID: 48
		// (Invoke) Token: 0x060001E5 RID: 485
		public delegate void TabSelectionChangeHandler(Tab tab);
	}
}
