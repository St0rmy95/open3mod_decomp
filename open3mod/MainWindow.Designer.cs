namespace open3mod
{
	// Token: 0x0200002C RID: 44
	public partial class MainWindow : global::System.Windows.Forms.Form
	{
		// Token: 0x060001C1 RID: 449 RVA: 0x0000E897 File Offset: 0x0000CA97
		protected override void Dispose(bool disposing)
		{
			this._ui.Dispose();
			this._renderer.Dispose();
			if (disposing && this.components != null)
			{
				this.components.Dispose();
			}
			base.Dispose(disposing);
		}

		// Token: 0x060001C2 RID: 450 RVA: 0x0000E8CC File Offset: 0x0000CACC
		private void InitializeComponent()
		{
			this.components = new global::System.ComponentModel.Container();
			global::System.ComponentModel.ComponentResourceManager componentResourceManager = new global::System.ComponentModel.ComponentResourceManager(typeof(global::open3mod.MainWindow));
			this.menuStrip1 = new global::System.Windows.Forms.MenuStrip();
			this.fileToolStripMenuItem = new global::System.Windows.Forms.ToolStripMenuItem();
			this.openToolStripMenuItem = new global::System.Windows.Forms.ToolStripMenuItem();
			this.closeAllToolStripMenuItem = new global::System.Windows.Forms.ToolStripMenuItem();
			this.toolStripSeparator2 = new global::System.Windows.Forms.ToolStripSeparator();
			this.recentToolStripMenuItem = new global::System.Windows.Forms.ToolStripMenuItem();
			this.toolStripMenuItem4 = new global::System.Windows.Forms.ToolStripMenuItem();
			this.wusonToolStripMenuItem = new global::System.Windows.Forms.ToolStripMenuItem();
			this.jeepToolStripMenuItem = new global::System.Windows.Forms.ToolStripMenuItem();
			this.duckToolStripMenuItem = new global::System.Windows.Forms.ToolStripMenuItem();
			this.wustonAnimatedToolStripMenuItem = new global::System.Windows.Forms.ToolStripMenuItem();
			this.lostEmpireToolStripMenuItem = new global::System.Windows.Forms.ToolStripMenuItem();
			this.toolStripSeparator3 = new global::System.Windows.Forms.ToolStripSeparator();
			this.quitToolStripMenuItem = new global::System.Windows.Forms.ToolStripMenuItem();
			this.toolStripMenuItemView = new global::System.Windows.Forms.ToolStripMenuItem();
			this.fullViewToolStripMenuItem = new global::System.Windows.Forms.ToolStripMenuItem();
			this.twoViewsToolStripMenuItem = new global::System.Windows.Forms.ToolStripMenuItem();
			this.fourViewsToolStripMenuItem = new global::System.Windows.Forms.ToolStripMenuItem();
			this.toolStripSeparator6 = new global::System.Windows.Forms.ToolStripSeparator();
			this.wireframeToolStripMenuItem = new global::System.Windows.Forms.ToolStripMenuItem();
			this.texturedToolStripMenuItem = new global::System.Windows.Forms.ToolStripMenuItem();
			this.lightingToolStripMenuItem = new global::System.Windows.Forms.ToolStripMenuItem();
			this.framerateToolStripMenuItem = new global::System.Windows.Forms.ToolStripMenuItem();
			this.toolStripSeparator7 = new global::System.Windows.Forms.ToolStripSeparator();
			this.showBoundingBoxesToolStripMenuItem = new global::System.Windows.Forms.ToolStripMenuItem();
			this.showNormalVectorsToolStripMenuItem = new global::System.Windows.Forms.ToolStripMenuItem();
			this.showAnimationSkeletonToolStripMenuItem = new global::System.Windows.Forms.ToolStripMenuItem();
			this.toolsToolStripMenuItem = new global::System.Windows.Forms.ToolStripMenuItem();
			this.toolStripMenuItem3 = new global::System.Windows.Forms.ToolStripMenuItem();
			this.toolStripSeparator11 = new global::System.Windows.Forms.ToolStripSeparator();
			this.logViewerToolStripMenuItem = new global::System.Windows.Forms.ToolStripMenuItem();
			this.exportToolStripMenuItem = new global::System.Windows.Forms.ToolStripMenuItem();
			this.exportAllToolStripMenuItem = new global::System.Windows.Forms.ToolStripMenuItem();
			this.toolStripSeparator8 = new global::System.Windows.Forms.ToolStripSeparator();
			this.optionsToolStripMenuItem = new global::System.Windows.Forms.ToolStripMenuItem();
			this.toolStripMenuItem1 = new global::System.Windows.Forms.ToolStripMenuItem();
			this.toolStripMenuItem2 = new global::System.Windows.Forms.ToolStripMenuItem();
			this.toolStripSeparator10 = new global::System.Windows.Forms.ToolStripSeparator();
			this.aboutToolStripMenuItem = new global::System.Windows.Forms.ToolStripMenuItem();
			this.toolStripSelectRenderer = new global::System.Windows.Forms.ToolStrip();
			this.openToolStripButton = new global::System.Windows.Forms.ToolStripButton();
			this.toolStripSeparator4 = new global::System.Windows.Forms.ToolStripSeparator();
			this.toolStripButtonFullView = new global::System.Windows.Forms.ToolStripButton();
			this.toolStripButtonTwoViews = new global::System.Windows.Forms.ToolStripButton();
			this.toolStripButtonFourViews = new global::System.Windows.Forms.ToolStripButton();
			this.toolStripSeparator = new global::System.Windows.Forms.ToolStripSeparator();
			this.toolStripButtonWireframe = new global::System.Windows.Forms.ToolStripButton();
			this.toolStripButtonShowTextures = new global::System.Windows.Forms.ToolStripButton();
			this.toolStripButtonShowShaded = new global::System.Windows.Forms.ToolStripButton();
			this.toolStripSeparator1 = new global::System.Windows.Forms.ToolStripSeparator();
			this.toolStripButtonShowBB = new global::System.Windows.Forms.ToolStripButton();
			this.toolStripButtonShowNormals = new global::System.Windows.Forms.ToolStripButton();
			this.toolStripButtonShowSkeleton = new global::System.Windows.Forms.ToolStripButton();
			this.toolStripSeparator5 = new global::System.Windows.Forms.ToolStripSeparator();
			this.toolStripButtonShowFPS = new global::System.Windows.Forms.ToolStripButton();
			this.toolStripSeparator9 = new global::System.Windows.Forms.ToolStripSeparator();
			this.toolStripButtonShowSettings = new global::System.Windows.Forms.ToolStripButton();
			this.tabControl1 = new global::System.Windows.Forms.TabControl();
			this.tabContextMenuStrip = new global::System.Windows.Forms.ContextMenuStrip(this.components);
			this.closeToolStripMenuItem = new global::System.Windows.Forms.ToolStripMenuItem();
			this.closeAllButThisToolStripMenuItem = new global::System.Windows.Forms.ToolStripMenuItem();
			this.buttonTabClose = new global::System.Windows.Forms.Button();
			this.openFileDialog = new global::System.Windows.Forms.OpenFileDialog();
			this.toolStripStatistics = new global::System.Windows.Forms.ToolStripStatusLabel();
			this.statusStrip = new global::System.Windows.Forms.StatusStrip();
			this.toolStripStatusLabel1 = new global::System.Windows.Forms.ToolStripStatusLabel();
			this.directorySearcher1 = new global::System.DirectoryServices.DirectorySearcher();
			this.colorDialog1 = new global::System.Windows.Forms.ColorDialog();
			this.linkLabelDonate = new global::System.Windows.Forms.LinkLabel();
			this.label1 = new global::System.Windows.Forms.Label();
			this.linkLabelWebsite = new global::System.Windows.Forms.LinkLabel();
			this.trackBarBrightness = new global::System.Windows.Forms.TrackBar();
			this.glControl1 = new global::open3mod.RenderControl();
			this.menuStrip1.SuspendLayout();
			this.toolStripSelectRenderer.SuspendLayout();
			this.tabContextMenuStrip.SuspendLayout();
			this.statusStrip.SuspendLayout();
			((global::System.ComponentModel.ISupportInitialize)this.trackBarBrightness).BeginInit();
			base.SuspendLayout();
			this.menuStrip1.GripStyle = global::System.Windows.Forms.ToolStripGripStyle.Visible;
			this.menuStrip1.Items.AddRange(new global::System.Windows.Forms.ToolStripItem[]
			{
				this.fileToolStripMenuItem,
				this.toolStripMenuItemView,
				this.toolsToolStripMenuItem,
				this.toolStripMenuItem1
			});
			this.menuStrip1.Location = new global::System.Drawing.Point(0, 0);
			this.menuStrip1.Name = "menuStrip1";
			this.menuStrip1.Size = new global::System.Drawing.Size(1051, 24);
			this.menuStrip1.TabIndex = 0;
			this.menuStrip1.Text = "menuStrip1";
			this.fileToolStripMenuItem.DropDownItems.AddRange(new global::System.Windows.Forms.ToolStripItem[]
			{
				this.openToolStripMenuItem,
				this.closeAllToolStripMenuItem,
				this.toolStripSeparator2,
				this.recentToolStripMenuItem,
				this.toolStripMenuItem4,
				this.toolStripSeparator3,
				this.quitToolStripMenuItem
			});
			this.fileToolStripMenuItem.Name = "fileToolStripMenuItem";
			this.fileToolStripMenuItem.Size = new global::System.Drawing.Size(37, 20);
			this.fileToolStripMenuItem.Text = "File";
			this.openToolStripMenuItem.Name = "openToolStripMenuItem";
			this.openToolStripMenuItem.Size = new global::System.Drawing.Size(151, 22);
			this.openToolStripMenuItem.Text = "Open";
			this.openToolStripMenuItem.Click += new global::System.EventHandler(this.OnFileMenuOpen);
			this.closeAllToolStripMenuItem.Name = "closeAllToolStripMenuItem";
			this.closeAllToolStripMenuItem.Size = new global::System.Drawing.Size(151, 22);
			this.closeAllToolStripMenuItem.Text = "Close all";
			this.closeAllToolStripMenuItem.Click += new global::System.EventHandler(this.OnFileMenuCloseAll);
			this.toolStripSeparator2.Name = "toolStripSeparator2";
			this.toolStripSeparator2.Size = new global::System.Drawing.Size(148, 6);
			this.recentToolStripMenuItem.Name = "recentToolStripMenuItem";
			this.recentToolStripMenuItem.Size = new global::System.Drawing.Size(151, 22);
			this.recentToolStripMenuItem.Text = "Recent";
			this.recentToolStripMenuItem.Click += new global::System.EventHandler(this.OnFileMenuRecent);
			this.toolStripMenuItem4.DropDownItems.AddRange(new global::System.Windows.Forms.ToolStripItem[]
			{
				this.wusonToolStripMenuItem,
				this.jeepToolStripMenuItem,
				this.duckToolStripMenuItem,
				this.wustonAnimatedToolStripMenuItem,
				this.lostEmpireToolStripMenuItem
			});
			this.toolStripMenuItem4.Name = "toolStripMenuItem4";
			this.toolStripMenuItem4.Size = new global::System.Drawing.Size(151, 22);
			this.toolStripMenuItem4.Text = "Sample scenes";
			this.wusonToolStripMenuItem.Name = "wusonToolStripMenuItem";
			this.wusonToolStripMenuItem.Size = new global::System.Drawing.Size(174, 22);
			this.wusonToolStripMenuItem.Text = "Spider";
			this.wusonToolStripMenuItem.Click += new global::System.EventHandler(this.wusonToolStripMenuItem_Click);
			this.jeepToolStripMenuItem.Name = "jeepToolStripMenuItem";
			this.jeepToolStripMenuItem.Size = new global::System.Drawing.Size(174, 22);
			this.jeepToolStripMenuItem.Text = "Jeep";
			this.jeepToolStripMenuItem.Click += new global::System.EventHandler(this.jeepToolStripMenuItem_Click);
			this.duckToolStripMenuItem.Name = "duckToolStripMenuItem";
			this.duckToolStripMenuItem.Size = new global::System.Drawing.Size(174, 22);
			this.duckToolStripMenuItem.Text = "Duck";
			this.duckToolStripMenuItem.Click += new global::System.EventHandler(this.duckToolStripMenuItem_Click);
			this.wustonAnimatedToolStripMenuItem.Name = "wustonAnimatedToolStripMenuItem";
			this.wustonAnimatedToolStripMenuItem.Size = new global::System.Drawing.Size(174, 22);
			this.wustonAnimatedToolStripMenuItem.Text = "Wuson (Animated)";
			this.wustonAnimatedToolStripMenuItem.Click += new global::System.EventHandler(this.wustonAnimatedToolStripMenuItem_Click);
			this.lostEmpireToolStripMenuItem.Name = "lostEmpireToolStripMenuItem";
			this.lostEmpireToolStripMenuItem.Size = new global::System.Drawing.Size(174, 22);
			this.lostEmpireToolStripMenuItem.Text = "Lost Empire";
			this.lostEmpireToolStripMenuItem.Click += new global::System.EventHandler(this.lostEmpireToolStripMenuItem_Click);
			this.toolStripSeparator3.Name = "toolStripSeparator3";
			this.toolStripSeparator3.Size = new global::System.Drawing.Size(148, 6);
			this.quitToolStripMenuItem.Name = "quitToolStripMenuItem";
			this.quitToolStripMenuItem.Size = new global::System.Drawing.Size(151, 22);
			this.quitToolStripMenuItem.Text = "Quit";
			this.quitToolStripMenuItem.Click += new global::System.EventHandler(this.OnFileMenuQuit);
			this.toolStripMenuItemView.DropDownItems.AddRange(new global::System.Windows.Forms.ToolStripItem[]
			{
				this.fullViewToolStripMenuItem,
				this.twoViewsToolStripMenuItem,
				this.fourViewsToolStripMenuItem,
				this.toolStripSeparator6,
				this.wireframeToolStripMenuItem,
				this.texturedToolStripMenuItem,
				this.lightingToolStripMenuItem,
				this.framerateToolStripMenuItem,
				this.toolStripSeparator7,
				this.showBoundingBoxesToolStripMenuItem,
				this.showNormalVectorsToolStripMenuItem,
				this.showAnimationSkeletonToolStripMenuItem
			});
			this.toolStripMenuItemView.Name = "toolStripMenuItemView";
			this.toolStripMenuItemView.Size = new global::System.Drawing.Size(44, 20);
			this.toolStripMenuItemView.Text = "View";
			this.fullViewToolStripMenuItem.CheckOnClick = true;
			this.fullViewToolStripMenuItem.Name = "fullViewToolStripMenuItem";
			this.fullViewToolStripMenuItem.Size = new global::System.Drawing.Size(178, 22);
			this.fullViewToolStripMenuItem.Text = "Full 3D View";
			this.fullViewToolStripMenuItem.Click += new global::System.EventHandler(this.ToggleFullView);
			this.twoViewsToolStripMenuItem.CheckOnClick = true;
			this.twoViewsToolStripMenuItem.Name = "twoViewsToolStripMenuItem";
			this.twoViewsToolStripMenuItem.Size = new global::System.Drawing.Size(178, 22);
			this.twoViewsToolStripMenuItem.Text = "Two 3D Views";
			this.twoViewsToolStripMenuItem.Click += new global::System.EventHandler(this.ToggleTwoViews);
			this.fourViewsToolStripMenuItem.CheckOnClick = true;
			this.fourViewsToolStripMenuItem.Name = "fourViewsToolStripMenuItem";
			this.fourViewsToolStripMenuItem.Size = new global::System.Drawing.Size(178, 22);
			this.fourViewsToolStripMenuItem.Text = "Four 3D Views";
			this.fourViewsToolStripMenuItem.Click += new global::System.EventHandler(this.ToggleFourViews);
			this.toolStripSeparator6.Name = "toolStripSeparator6";
			this.toolStripSeparator6.Size = new global::System.Drawing.Size(175, 6);
			this.wireframeToolStripMenuItem.CheckOnClick = true;
			this.wireframeToolStripMenuItem.Name = "wireframeToolStripMenuItem";
			this.wireframeToolStripMenuItem.Size = new global::System.Drawing.Size(178, 22);
			this.wireframeToolStripMenuItem.Text = "Wireframe";
			this.wireframeToolStripMenuItem.Click += new global::System.EventHandler(this.ToggleWireframe);
			this.texturedToolStripMenuItem.CheckOnClick = true;
			this.texturedToolStripMenuItem.Name = "texturedToolStripMenuItem";
			this.texturedToolStripMenuItem.Size = new global::System.Drawing.Size(178, 22);
			this.texturedToolStripMenuItem.Text = "Textures";
			this.texturedToolStripMenuItem.Click += new global::System.EventHandler(this.ToggleTextures);
			this.lightingToolStripMenuItem.CheckOnClick = true;
			this.lightingToolStripMenuItem.Name = "lightingToolStripMenuItem";
			this.lightingToolStripMenuItem.Size = new global::System.Drawing.Size(178, 22);
			this.lightingToolStripMenuItem.Text = "Lighting";
			this.lightingToolStripMenuItem.Click += new global::System.EventHandler(this.ToggleShading);
			this.framerateToolStripMenuItem.CheckOnClick = true;
			this.framerateToolStripMenuItem.Name = "framerateToolStripMenuItem";
			this.framerateToolStripMenuItem.Size = new global::System.Drawing.Size(178, 22);
			this.framerateToolStripMenuItem.Text = "Framerate";
			this.framerateToolStripMenuItem.Click += new global::System.EventHandler(this.ToggleFps);
			this.toolStripSeparator7.Name = "toolStripSeparator7";
			this.toolStripSeparator7.Size = new global::System.Drawing.Size(175, 6);
			this.showBoundingBoxesToolStripMenuItem.Name = "showBoundingBoxesToolStripMenuItem";
			this.showBoundingBoxesToolStripMenuItem.Size = new global::System.Drawing.Size(178, 22);
			this.showBoundingBoxesToolStripMenuItem.Text = "Bounding Boxes";
			this.showBoundingBoxesToolStripMenuItem.Click += new global::System.EventHandler(this.ToggleShowBb);
			this.showNormalVectorsToolStripMenuItem.Name = "showNormalVectorsToolStripMenuItem";
			this.showNormalVectorsToolStripMenuItem.Size = new global::System.Drawing.Size(178, 22);
			this.showNormalVectorsToolStripMenuItem.Text = "Normal Vectors";
			this.showNormalVectorsToolStripMenuItem.Click += new global::System.EventHandler(this.ToggleShowNormals);
			this.showAnimationSkeletonToolStripMenuItem.Name = "showAnimationSkeletonToolStripMenuItem";
			this.showAnimationSkeletonToolStripMenuItem.Size = new global::System.Drawing.Size(178, 22);
			this.showAnimationSkeletonToolStripMenuItem.Text = "Animation Skeleton";
			this.showAnimationSkeletonToolStripMenuItem.Click += new global::System.EventHandler(this.ToggleShowSkeleton);
			this.toolsToolStripMenuItem.DropDownItems.AddRange(new global::System.Windows.Forms.ToolStripItem[]
			{
				this.toolStripMenuItem3,
				this.toolStripSeparator11,
				this.logViewerToolStripMenuItem,
				this.exportToolStripMenuItem,
				this.exportAllToolStripMenuItem,
				this.toolStripSeparator8,
				this.optionsToolStripMenuItem
			});
			this.toolsToolStripMenuItem.Name = "toolsToolStripMenuItem";
			this.toolsToolStripMenuItem.Size = new global::System.Drawing.Size(48, 20);
			this.toolsToolStripMenuItem.Text = "Tools";
			this.toolsToolStripMenuItem.Click += new global::System.EventHandler(this.ToolsToolStripMenuItemClick);
			this.toolStripMenuItem3.Name = "toolStripMenuItem3";
			this.toolStripMenuItem3.Size = new global::System.Drawing.Size(176, 22);
			this.toolStripMenuItem3.Text = "Set file associations";
			this.toolStripMenuItem3.Click += new global::System.EventHandler(this.OnSetFileAssociations);
			this.toolStripSeparator11.Name = "toolStripSeparator11";
			this.toolStripSeparator11.Size = new global::System.Drawing.Size(173, 6);
			this.logViewerToolStripMenuItem.Name = "logViewerToolStripMenuItem";
			this.logViewerToolStripMenuItem.Size = new global::System.Drawing.Size(176, 22);
			this.logViewerToolStripMenuItem.Text = "Log Viewer";
			this.logViewerToolStripMenuItem.Click += new global::System.EventHandler(this.OnShowLogViewer);
			this.exportToolStripMenuItem.Name = "exportToolStripMenuItem";
			this.exportToolStripMenuItem.Size = new global::System.Drawing.Size(176, 22);
			this.exportToolStripMenuItem.Text = "Export";
			this.exportToolStripMenuItem.Click += new global::System.EventHandler(this.OnExport);
			this.exportAllToolStripMenuItem.Enabled = false;
			this.exportAllToolStripMenuItem.Name = "exportAllToolStripMenuItem";
			this.exportAllToolStripMenuItem.Size = new global::System.Drawing.Size(176, 22);
			this.exportAllToolStripMenuItem.Text = "Export all";
			this.toolStripSeparator8.Name = "toolStripSeparator8";
			this.toolStripSeparator8.Size = new global::System.Drawing.Size(173, 6);
			this.optionsToolStripMenuItem.Name = "optionsToolStripMenuItem";
			this.optionsToolStripMenuItem.Size = new global::System.Drawing.Size(176, 22);
			this.optionsToolStripMenuItem.Text = "Options";
			this.optionsToolStripMenuItem.Click += new global::System.EventHandler(this.OnShowSettings);
			this.toolStripMenuItem1.DropDownItems.AddRange(new global::System.Windows.Forms.ToolStripItem[]
			{
				this.toolStripMenuItem2,
				this.toolStripSeparator10,
				this.aboutToolStripMenuItem
			});
			this.toolStripMenuItem1.Name = "toolStripMenuItem1";
			this.toolStripMenuItem1.Size = new global::System.Drawing.Size(24, 20);
			this.toolStripMenuItem1.Text = "?";
			this.toolStripMenuItem2.Name = "toolStripMenuItem2";
			this.toolStripMenuItem2.Size = new global::System.Drawing.Size(148, 22);
			this.toolStripMenuItem2.Text = "Tip of the Day";
			this.toolStripMenuItem2.Click += new global::System.EventHandler(this.OnTipOfTheDay);
			this.toolStripSeparator10.Name = "toolStripSeparator10";
			this.toolStripSeparator10.Size = new global::System.Drawing.Size(145, 6);
			this.aboutToolStripMenuItem.Name = "aboutToolStripMenuItem";
			this.aboutToolStripMenuItem.Size = new global::System.Drawing.Size(148, 22);
			this.aboutToolStripMenuItem.Text = "About";
			this.aboutToolStripMenuItem.Click += new global::System.EventHandler(this.AboutToolStripMenuItemClick);
			this.toolStripSelectRenderer.Items.AddRange(new global::System.Windows.Forms.ToolStripItem[]
			{
				this.openToolStripButton,
				this.toolStripSeparator4,
				this.toolStripButtonFullView,
				this.toolStripButtonTwoViews,
				this.toolStripButtonFourViews,
				this.toolStripSeparator,
				this.toolStripButtonWireframe,
				this.toolStripButtonShowTextures,
				this.toolStripButtonShowShaded,
				this.toolStripSeparator1,
				this.toolStripButtonShowBB,
				this.toolStripButtonShowNormals,
				this.toolStripButtonShowSkeleton,
				this.toolStripSeparator5,
				this.toolStripButtonShowFPS,
				this.toolStripSeparator9,
				this.toolStripButtonShowSettings
			});
			this.toolStripSelectRenderer.Location = new global::System.Drawing.Point(0, 24);
			this.toolStripSelectRenderer.Name = "toolStripSelectRenderer";
			this.toolStripSelectRenderer.Size = new global::System.Drawing.Size(1051, 25);
			this.toolStripSelectRenderer.TabIndex = 2;
			this.toolStripSelectRenderer.Text = "toolStrip1";
			this.openToolStripButton.DisplayStyle = global::System.Windows.Forms.ToolStripItemDisplayStyle.Image;
			this.openToolStripButton.Image = (global::System.Drawing.Image)componentResourceManager.GetObject("openToolStripButton.Image");
			this.openToolStripButton.ImageTransparentColor = global::System.Drawing.Color.Magenta;
			this.openToolStripButton.Name = "openToolStripButton";
			this.openToolStripButton.Size = new global::System.Drawing.Size(23, 22);
			this.openToolStripButton.Text = "&Open";
			this.openToolStripButton.ToolTipText = "Open a 3D file";
			this.openToolStripButton.Click += new global::System.EventHandler(this.OnFileMenuOpen);
			this.toolStripSeparator4.Name = "toolStripSeparator4";
			this.toolStripSeparator4.Size = new global::System.Drawing.Size(6, 25);
			this.toolStripButtonFullView.DisplayStyle = global::System.Windows.Forms.ToolStripItemDisplayStyle.Image;
			this.toolStripButtonFullView.Image = (global::System.Drawing.Image)componentResourceManager.GetObject("toolStripButtonFullView.Image");
			this.toolStripButtonFullView.ImageTransparentColor = global::System.Drawing.Color.Magenta;
			this.toolStripButtonFullView.Name = "toolStripButtonFullView";
			this.toolStripButtonFullView.Size = new global::System.Drawing.Size(23, 22);
			this.toolStripButtonFullView.Text = "Full View";
			this.toolStripButtonFullView.ToolTipText = "Show full-size 3D view";
			this.toolStripButtonFullView.Click += new global::System.EventHandler(this.ToggleFullView);
			this.toolStripButtonTwoViews.DisplayStyle = global::System.Windows.Forms.ToolStripItemDisplayStyle.Image;
			this.toolStripButtonTwoViews.Image = (global::System.Drawing.Image)componentResourceManager.GetObject("toolStripButtonTwoViews.Image");
			this.toolStripButtonTwoViews.ImageTransparentColor = global::System.Drawing.Color.Magenta;
			this.toolStripButtonTwoViews.Name = "toolStripButtonTwoViews";
			this.toolStripButtonTwoViews.Size = new global::System.Drawing.Size(23, 22);
			this.toolStripButtonTwoViews.Text = "Two Views";
			this.toolStripButtonTwoViews.ToolTipText = "Split into two 3D views stacked on top of each other";
			this.toolStripButtonTwoViews.Click += new global::System.EventHandler(this.ToggleTwoViews);
			this.toolStripButtonFourViews.DisplayStyle = global::System.Windows.Forms.ToolStripItemDisplayStyle.Image;
			this.toolStripButtonFourViews.Image = (global::System.Drawing.Image)componentResourceManager.GetObject("toolStripButtonFourViews.Image");
			this.toolStripButtonFourViews.ImageTransparentColor = global::System.Drawing.Color.Magenta;
			this.toolStripButtonFourViews.Name = "toolStripButtonFourViews";
			this.toolStripButtonFourViews.Size = new global::System.Drawing.Size(23, 22);
			this.toolStripButtonFourViews.Text = "Four Views";
			this.toolStripButtonFourViews.ToolTipText = "Split into four 3D views";
			this.toolStripButtonFourViews.Click += new global::System.EventHandler(this.ToggleFourViews);
			this.toolStripSeparator.Name = "toolStripSeparator";
			this.toolStripSeparator.Size = new global::System.Drawing.Size(6, 25);
			this.toolStripButtonWireframe.CheckOnClick = true;
			this.toolStripButtonWireframe.DisplayStyle = global::System.Windows.Forms.ToolStripItemDisplayStyle.Image;
			this.toolStripButtonWireframe.Image = (global::System.Drawing.Image)componentResourceManager.GetObject("toolStripButtonWireframe.Image");
			this.toolStripButtonWireframe.ImageTransparentColor = global::System.Drawing.Color.Magenta;
			this.toolStripButtonWireframe.Name = "toolStripButtonWireframe";
			this.toolStripButtonWireframe.Size = new global::System.Drawing.Size(23, 22);
			this.toolStripButtonWireframe.Text = "Enable Wireframe Mode";
			this.toolStripButtonWireframe.ToolTipText = "Enable wireframe mode";
			this.toolStripButtonWireframe.Click += new global::System.EventHandler(this.ToggleWireframe);
			this.toolStripButtonShowTextures.CheckOnClick = true;
			this.toolStripButtonShowTextures.DisplayStyle = global::System.Windows.Forms.ToolStripItemDisplayStyle.Image;
			this.toolStripButtonShowTextures.Image = (global::System.Drawing.Image)componentResourceManager.GetObject("toolStripButtonShowTextures.Image");
			this.toolStripButtonShowTextures.ImageTransparentColor = global::System.Drawing.Color.Magenta;
			this.toolStripButtonShowTextures.Name = "toolStripButtonShowTextures";
			this.toolStripButtonShowTextures.Size = new global::System.Drawing.Size(23, 22);
			this.toolStripButtonShowTextures.Text = "Enable Textures";
			this.toolStripButtonShowTextures.ToolTipText = "Enable textures in 3D view";
			this.toolStripButtonShowTextures.Click += new global::System.EventHandler(this.ToggleTextures);
			this.toolStripButtonShowShaded.CheckOnClick = true;
			this.toolStripButtonShowShaded.DisplayStyle = global::System.Windows.Forms.ToolStripItemDisplayStyle.Image;
			this.toolStripButtonShowShaded.Image = (global::System.Drawing.Image)componentResourceManager.GetObject("toolStripButtonShowShaded.Image");
			this.toolStripButtonShowShaded.ImageTransparentColor = global::System.Drawing.Color.Magenta;
			this.toolStripButtonShowShaded.Name = "toolStripButtonShowShaded";
			this.toolStripButtonShowShaded.Size = new global::System.Drawing.Size(23, 22);
			this.toolStripButtonShowShaded.Text = "Enable Shading";
			this.toolStripButtonShowShaded.ToolTipText = "Enable shading (lighting) in 3D view";
			this.toolStripButtonShowShaded.Click += new global::System.EventHandler(this.ToggleShading);
			this.toolStripSeparator1.Name = "toolStripSeparator1";
			this.toolStripSeparator1.Size = new global::System.Drawing.Size(6, 25);
			this.toolStripButtonShowBB.CheckOnClick = true;
			this.toolStripButtonShowBB.DisplayStyle = global::System.Windows.Forms.ToolStripItemDisplayStyle.Image;
			this.toolStripButtonShowBB.Image = (global::System.Drawing.Image)componentResourceManager.GetObject("toolStripButtonShowBB.Image");
			this.toolStripButtonShowBB.ImageTransparentColor = global::System.Drawing.Color.Magenta;
			this.toolStripButtonShowBB.Name = "toolStripButtonShowBB";
			this.toolStripButtonShowBB.Size = new global::System.Drawing.Size(23, 22);
			this.toolStripButtonShowBB.Text = "Show Bounding Boxes";
			this.toolStripButtonShowBB.ToolTipText = "Show axis-aligned bounding boxes for nodes";
			this.toolStripButtonShowBB.Click += new global::System.EventHandler(this.ToggleShowBb);
			this.toolStripButtonShowNormals.CheckOnClick = true;
			this.toolStripButtonShowNormals.DisplayStyle = global::System.Windows.Forms.ToolStripItemDisplayStyle.Image;
			this.toolStripButtonShowNormals.Image = (global::System.Drawing.Image)componentResourceManager.GetObject("toolStripButtonShowNormals.Image");
			this.toolStripButtonShowNormals.ImageTransparentColor = global::System.Drawing.Color.Magenta;
			this.toolStripButtonShowNormals.Name = "toolStripButtonShowNormals";
			this.toolStripButtonShowNormals.Size = new global::System.Drawing.Size(23, 22);
			this.toolStripButtonShowNormals.Text = "Show Normals";
			this.toolStripButtonShowNormals.ToolTipText = "Show geometric normal vectors";
			this.toolStripButtonShowNormals.Click += new global::System.EventHandler(this.ToggleShowNormals);
			this.toolStripButtonShowSkeleton.CheckOnClick = true;
			this.toolStripButtonShowSkeleton.DisplayStyle = global::System.Windows.Forms.ToolStripItemDisplayStyle.Image;
			this.toolStripButtonShowSkeleton.Image = (global::System.Drawing.Image)componentResourceManager.GetObject("toolStripButtonShowSkeleton.Image");
			this.toolStripButtonShowSkeleton.ImageTransparentColor = global::System.Drawing.Color.Magenta;
			this.toolStripButtonShowSkeleton.Name = "toolStripButtonShowSkeleton";
			this.toolStripButtonShowSkeleton.Size = new global::System.Drawing.Size(23, 22);
			this.toolStripButtonShowSkeleton.Text = "Show Skeleton";
			this.toolStripButtonShowSkeleton.ToolTipText = "Show skeleton joints in 3D view";
			this.toolStripButtonShowSkeleton.Click += new global::System.EventHandler(this.ToggleShowSkeleton);
			this.toolStripSeparator5.Name = "toolStripSeparator5";
			this.toolStripSeparator5.Size = new global::System.Drawing.Size(6, 25);
			this.toolStripButtonShowFPS.CheckOnClick = true;
			this.toolStripButtonShowFPS.DisplayStyle = global::System.Windows.Forms.ToolStripItemDisplayStyle.Image;
			this.toolStripButtonShowFPS.Image = (global::System.Drawing.Image)componentResourceManager.GetObject("toolStripButtonShowFPS.Image");
			this.toolStripButtonShowFPS.ImageTransparentColor = global::System.Drawing.Color.Magenta;
			this.toolStripButtonShowFPS.Name = "toolStripButtonShowFPS";
			this.toolStripButtonShowFPS.Size = new global::System.Drawing.Size(23, 22);
			this.toolStripButtonShowFPS.Text = "Show Frames per Second (FPS)";
			this.toolStripButtonShowFPS.ToolTipText = "Show frames per second (FPS)";
			this.toolStripButtonShowFPS.Click += new global::System.EventHandler(this.ToggleFps);
			this.toolStripSeparator9.Name = "toolStripSeparator9";
			this.toolStripSeparator9.Size = new global::System.Drawing.Size(6, 25);
			this.toolStripButtonShowSettings.DisplayStyle = global::System.Windows.Forms.ToolStripItemDisplayStyle.Image;
			this.toolStripButtonShowSettings.Image = (global::System.Drawing.Image)componentResourceManager.GetObject("toolStripButtonShowSettings.Image");
			this.toolStripButtonShowSettings.ImageTransparentColor = global::System.Drawing.Color.Magenta;
			this.toolStripButtonShowSettings.Name = "toolStripButtonShowSettings";
			this.toolStripButtonShowSettings.Size = new global::System.Drawing.Size(23, 22);
			this.toolStripButtonShowSettings.Text = "Settings";
			this.toolStripButtonShowSettings.ToolTipText = "Open settings dialog";
			this.toolStripButtonShowSettings.Click += new global::System.EventHandler(this.OnShowSettings);
			this.tabControl1.Dock = global::System.Windows.Forms.DockStyle.Fill;
			this.tabControl1.Location = new global::System.Drawing.Point(0, 49);
			this.tabControl1.Name = "tabControl1";
			this.tabControl1.SelectedIndex = 0;
			this.tabControl1.Size = new global::System.Drawing.Size(1051, 663);
			this.tabControl1.TabIndex = 3;
			this.tabControl1.Selected += new global::System.Windows.Forms.TabControlEventHandler(this.OnTabSelected);
			this.tabControl1.MouseUp += new global::System.Windows.Forms.MouseEventHandler(this.OnShowTabContextMenu);
			this.tabContextMenuStrip.Items.AddRange(new global::System.Windows.Forms.ToolStripItem[]
			{
				this.closeToolStripMenuItem,
				this.closeAllButThisToolStripMenuItem
			});
			this.tabContextMenuStrip.Name = "tabContextMenuStrip";
			this.tabContextMenuStrip.Size = new global::System.Drawing.Size(162, 48);
			this.closeToolStripMenuItem.Name = "closeToolStripMenuItem";
			this.closeToolStripMenuItem.Size = new global::System.Drawing.Size(161, 22);
			this.closeToolStripMenuItem.Text = "Close";
			this.closeToolStripMenuItem.Click += new global::System.EventHandler(this.OnCloseTabFromContextMenu);
			this.closeAllButThisToolStripMenuItem.Name = "closeAllButThisToolStripMenuItem";
			this.closeAllButThisToolStripMenuItem.Size = new global::System.Drawing.Size(161, 22);
			this.closeAllButThisToolStripMenuItem.Text = "Close all but this";
			this.closeAllButThisToolStripMenuItem.Click += new global::System.EventHandler(this.OnCloseAllTabsButThisFromContextMenu);
			this.buttonTabClose.Anchor = (global::System.Windows.Forms.AnchorStyles.Top | global::System.Windows.Forms.AnchorStyles.Right);
			this.buttonTabClose.FlatAppearance.MouseOverBackColor = global::System.Drawing.Color.LightGray;
			this.buttonTabClose.FlatStyle = global::System.Windows.Forms.FlatStyle.Flat;
			this.buttonTabClose.Font = new global::System.Drawing.Font("Microsoft Sans Serif", 8.25f, global::System.Drawing.FontStyle.Bold, global::System.Drawing.GraphicsUnit.Point, 0);
			this.buttonTabClose.Location = new global::System.Drawing.Point(1011, 35);
			this.buttonTabClose.Name = "buttonTabClose";
			this.buttonTabClose.Size = new global::System.Drawing.Size(28, 24);
			this.buttonTabClose.TabIndex = 4;
			this.buttonTabClose.Text = "X";
			this.buttonTabClose.UseVisualStyleBackColor = true;
			this.buttonTabClose.Click += new global::System.EventHandler(this.OnCloseTab);
			this.openFileDialog.Multiselect = true;
			this.toolStripStatistics.BackColor = global::System.Drawing.SystemColors.Control;
			this.toolStripStatistics.Name = "toolStripStatistics";
			this.toolStripStatistics.Size = new global::System.Drawing.Size(0, 17);
			this.statusStrip.Items.AddRange(new global::System.Windows.Forms.ToolStripItem[]
			{
				this.toolStripStatistics,
				this.toolStripStatusLabel1
			});
			this.statusStrip.Location = new global::System.Drawing.Point(0, 712);
			this.statusStrip.Name = "statusStrip";
			this.statusStrip.Size = new global::System.Drawing.Size(1051, 22);
			this.statusStrip.TabIndex = 1;
			this.statusStrip.Text = "statusStrip1";
			this.toolStripStatusLabel1.BackColor = global::System.Drawing.Color.Transparent;
			this.toolStripStatusLabel1.Name = "toolStripStatusLabel1";
			this.toolStripStatusLabel1.Size = new global::System.Drawing.Size(300, 17);
			this.toolStripStatusLabel1.Text = "| Keep right mouse button pressed to move light source";
			this.directorySearcher1.ClientTimeout = global::System.TimeSpan.Parse("-00:00:01");
			this.directorySearcher1.ServerPageTimeLimit = global::System.TimeSpan.Parse("-00:00:01");
			this.directorySearcher1.ServerTimeLimit = global::System.TimeSpan.Parse("-00:00:01");
			this.linkLabelDonate.Anchor = (global::System.Windows.Forms.AnchorStyles.Top | global::System.Windows.Forms.AnchorStyles.Right);
			this.linkLabelDonate.AutoSize = true;
			this.linkLabelDonate.Location = new global::System.Drawing.Point(847, 11);
			this.linkLabelDonate.Name = "linkLabelDonate";
			this.linkLabelDonate.Size = new global::System.Drawing.Size(42, 13);
			this.linkLabelDonate.TabIndex = 5;
			this.linkLabelDonate.TabStop = true;
			this.linkLabelDonate.Text = "Donate";
			this.linkLabelDonate.LinkClicked += new global::System.Windows.Forms.LinkLabelLinkClickedEventHandler(this.OnDonate);
			this.label1.Anchor = (global::System.Windows.Forms.AnchorStyles.Bottom | global::System.Windows.Forms.AnchorStyles.Right);
			this.label1.BackColor = global::System.Drawing.Color.Transparent;
			this.label1.FlatStyle = global::System.Windows.Forms.FlatStyle.Popup;
			this.label1.Location = new global::System.Drawing.Point(806, 713);
			this.label1.Name = "label1";
			this.label1.Size = new global::System.Drawing.Size(61, 21);
			this.label1.TabIndex = 7;
			this.label1.Text = "Brightness";
			this.label1.TextAlign = global::System.Drawing.ContentAlignment.MiddleCenter;
			this.linkLabelWebsite.Anchor = (global::System.Windows.Forms.AnchorStyles.Top | global::System.Windows.Forms.AnchorStyles.Right);
			this.linkLabelWebsite.AutoSize = true;
			this.linkLabelWebsite.Location = new global::System.Drawing.Point(895, 11);
			this.linkLabelWebsite.Name = "linkLabelWebsite";
			this.linkLabelWebsite.Size = new global::System.Drawing.Size(144, 13);
			this.linkLabelWebsite.TabIndex = 8;
			this.linkLabelWebsite.TabStop = true;
			this.linkLabelWebsite.Text = "Website - Check for Updates";
			this.linkLabelWebsite.LinkClicked += new global::System.Windows.Forms.LinkLabelLinkClickedEventHandler(this.linkLabelWebsite_LinkClicked);
			this.trackBarBrightness.Anchor = (global::System.Windows.Forms.AnchorStyles.Bottom | global::System.Windows.Forms.AnchorStyles.Right);
			this.trackBarBrightness.BackColor = global::System.Drawing.SystemColors.ActiveBorder;
			this.trackBarBrightness.DataBindings.Add(new global::System.Windows.Forms.Binding("Value", global::open3mod.GraphicsSettings.Default, "OutputBrightness", true, global::System.Windows.Forms.DataSourceUpdateMode.OnPropertyChanged));
			this.trackBarBrightness.Location = new global::System.Drawing.Point(863, 713);
			this.trackBarBrightness.Margin = new global::System.Windows.Forms.Padding(0);
			this.trackBarBrightness.Maximum = 100;
			this.trackBarBrightness.Name = "trackBarBrightness";
			this.trackBarBrightness.Size = new global::System.Drawing.Size(188, 45);
			this.trackBarBrightness.TabIndex = 6;
			this.trackBarBrightness.Value = global::open3mod.GraphicsSettings.Default.OutputBrightness;
			this.glControl1.AllowDrop = true;
			this.glControl1.Anchor = (global::System.Windows.Forms.AnchorStyles.Top | global::System.Windows.Forms.AnchorStyles.Bottom | global::System.Windows.Forms.AnchorStyles.Left | global::System.Windows.Forms.AnchorStyles.Right);
			this.glControl1.BackColor = global::System.Drawing.Color.Black;
			this.glControl1.Location = new global::System.Drawing.Point(177, 70);
			this.glControl1.Name = "glControl1";
			this.glControl1.Size = new global::System.Drawing.Size(742, 626);
			this.glControl1.TabIndex = 0;
			this.glControl1.VSync = true;
			this.glControl1.Load += new global::System.EventHandler(this.OnGlLoad);
			this.glControl1.DragDrop += new global::System.Windows.Forms.DragEventHandler(this.OnDrag);
			this.glControl1.DragEnter += new global::System.Windows.Forms.DragEventHandler(this.OnDragEnter);
			this.glControl1.Paint += new global::System.Windows.Forms.PaintEventHandler(this.GlPaint);
			this.glControl1.KeyDown += new global::System.Windows.Forms.KeyEventHandler(this.OnKeyDown);
			this.glControl1.KeyUp += new global::System.Windows.Forms.KeyEventHandler(this.OnKeyUp);
			this.glControl1.MouseDown += new global::System.Windows.Forms.MouseEventHandler(this.OnMouseDown);
			this.glControl1.MouseEnter += new global::System.EventHandler(this.OnMouseEnter);
			this.glControl1.MouseLeave += new global::System.EventHandler(this.OnMouseLeave);
			this.glControl1.MouseMove += new global::System.Windows.Forms.MouseEventHandler(this.OnMouseMove);
			this.glControl1.MouseUp += new global::System.Windows.Forms.MouseEventHandler(this.OnMouseUp);
			this.glControl1.PreviewKeyDown += new global::System.Windows.Forms.PreviewKeyDownEventHandler(this.OnPreviewKeyDown);
			this.glControl1.Resize += new global::System.EventHandler(this.OnGlResize);
			this.AllowDrop = true;
			base.AutoScaleDimensions = new global::System.Drawing.SizeF(6f, 13f);
			base.AutoScaleMode = global::System.Windows.Forms.AutoScaleMode.Font;
			this.BackColor = global::System.Drawing.SystemColors.Window;
			base.ClientSize = new global::System.Drawing.Size(1051, 734);
			base.Controls.Add(this.linkLabelWebsite);
			base.Controls.Add(this.label1);
			base.Controls.Add(this.trackBarBrightness);
			base.Controls.Add(this.linkLabelDonate);
			base.Controls.Add(this.buttonTabClose);
			base.Controls.Add(this.glControl1);
			base.Controls.Add(this.tabControl1);
			base.Controls.Add(this.toolStripSelectRenderer);
			base.Controls.Add(this.statusStrip);
			base.Controls.Add(this.menuStrip1);
			base.Icon = (global::System.Drawing.Icon)componentResourceManager.GetObject("$this.Icon");
			base.KeyPreview = true;
			base.MainMenuStrip = this.menuStrip1;
			this.MinimumSize = new global::System.Drawing.Size(800, 600);
			base.Name = "MainWindow";
			base.StartPosition = global::System.Windows.Forms.FormStartPosition.CenterScreen;
			this.Text = "Open 3D Model Viewer";
			base.FormClosing += new global::System.Windows.Forms.FormClosingEventHandler(this.OnClosing);
			base.FormClosed += new global::System.Windows.Forms.FormClosedEventHandler(this.OnCloseForm);
			base.Load += new global::System.EventHandler(this.OnLoad);
			this.menuStrip1.ResumeLayout(false);
			this.menuStrip1.PerformLayout();
			this.toolStripSelectRenderer.ResumeLayout(false);
			this.toolStripSelectRenderer.PerformLayout();
			this.tabContextMenuStrip.ResumeLayout(false);
			this.statusStrip.ResumeLayout(false);
			this.statusStrip.PerformLayout();
			((global::System.ComponentModel.ISupportInitialize)this.trackBarBrightness).EndInit();
			base.ResumeLayout(false);
			base.PerformLayout();
		}

		// Token: 0x04000123 RID: 291
		private readonly global::open3mod.UiState _ui;

		// Token: 0x04000124 RID: 292
		private global::open3mod.Renderer _renderer;

		// Token: 0x04000132 RID: 306
		private global::System.ComponentModel.IContainer components;

		// Token: 0x04000133 RID: 307
		private global::System.Windows.Forms.MenuStrip menuStrip1;

		// Token: 0x04000134 RID: 308
		private global::System.Windows.Forms.ToolStripMenuItem fileToolStripMenuItem;

		// Token: 0x04000135 RID: 309
		private global::System.Windows.Forms.ToolStripMenuItem toolsToolStripMenuItem;

		// Token: 0x04000136 RID: 310
		private global::System.Windows.Forms.ToolStripMenuItem toolStripMenuItem1;

		// Token: 0x04000137 RID: 311
		private global::System.Windows.Forms.ToolStrip toolStripSelectRenderer;

		// Token: 0x04000138 RID: 312
		private global::System.Windows.Forms.ToolStripMenuItem aboutToolStripMenuItem;

		// Token: 0x04000139 RID: 313
		private global::System.Windows.Forms.ToolStripButton openToolStripButton;

		// Token: 0x0400013A RID: 314
		private global::System.Windows.Forms.ToolStripSeparator toolStripSeparator;

		// Token: 0x0400013B RID: 315
		private global::System.Windows.Forms.ToolStripSeparator toolStripSeparator1;

		// Token: 0x0400013C RID: 316
		private global::System.Windows.Forms.ToolStripMenuItem openToolStripMenuItem;

		// Token: 0x0400013D RID: 317
		private global::System.Windows.Forms.ToolStripMenuItem closeAllToolStripMenuItem;

		// Token: 0x0400013E RID: 318
		private global::System.Windows.Forms.ToolStripSeparator toolStripSeparator2;

		// Token: 0x0400013F RID: 319
		private global::System.Windows.Forms.ToolStripMenuItem recentToolStripMenuItem;

		// Token: 0x04000140 RID: 320
		private global::System.Windows.Forms.ToolStripSeparator toolStripSeparator3;

		// Token: 0x04000141 RID: 321
		private global::System.Windows.Forms.ToolStripMenuItem quitToolStripMenuItem;

		// Token: 0x04000142 RID: 322
		private global::System.Windows.Forms.ToolStripMenuItem toolStripMenuItemView;

		// Token: 0x04000143 RID: 323
		private global::System.Windows.Forms.ToolStripMenuItem fullViewToolStripMenuItem;

		// Token: 0x04000144 RID: 324
		private global::System.Windows.Forms.ToolStripButton toolStripButtonWireframe;

		// Token: 0x04000145 RID: 325
		private global::System.Windows.Forms.ToolStripButton toolStripButtonShowTextures;

		// Token: 0x04000146 RID: 326
		private global::System.Windows.Forms.ToolStripButton toolStripButtonShowShaded;

		// Token: 0x04000147 RID: 327
		private global::System.Windows.Forms.ToolStripButton toolStripButtonShowFPS;

		// Token: 0x04000148 RID: 328
		private global::System.Windows.Forms.ToolStripSeparator toolStripSeparator4;

		// Token: 0x04000149 RID: 329
		private global::System.Windows.Forms.ToolStripButton toolStripButtonFullView;

		// Token: 0x0400014A RID: 330
		private global::System.Windows.Forms.ToolStripButton toolStripButtonTwoViews;

		// Token: 0x0400014B RID: 331
		private global::System.Windows.Forms.ToolStripButton toolStripButtonFourViews;

		// Token: 0x0400014C RID: 332
		private global::System.Windows.Forms.ToolStripMenuItem logViewerToolStripMenuItem;

		// Token: 0x0400014D RID: 333
		private global::open3mod.RenderControl glControl1;

		// Token: 0x0400014E RID: 334
		private global::System.Windows.Forms.TabControl tabControl1;

		// Token: 0x0400014F RID: 335
		private global::System.Windows.Forms.ContextMenuStrip tabContextMenuStrip;

		// Token: 0x04000150 RID: 336
		private global::System.Windows.Forms.ToolStripMenuItem closeToolStripMenuItem;

		// Token: 0x04000151 RID: 337
		private global::System.Windows.Forms.ToolStripMenuItem closeAllButThisToolStripMenuItem;

		// Token: 0x04000152 RID: 338
		private global::System.Windows.Forms.Button buttonTabClose;

		// Token: 0x04000153 RID: 339
		private global::System.Windows.Forms.OpenFileDialog openFileDialog;

		// Token: 0x04000154 RID: 340
		private global::System.Windows.Forms.ToolStripButton toolStripButtonShowBB;

		// Token: 0x04000155 RID: 341
		private global::System.Windows.Forms.ToolStripButton toolStripButtonShowNormals;

		// Token: 0x04000156 RID: 342
		private global::System.Windows.Forms.ToolStripButton toolStripButtonShowSkeleton;

		// Token: 0x04000157 RID: 343
		private global::System.Windows.Forms.ToolStripSeparator toolStripSeparator5;

		// Token: 0x04000158 RID: 344
		private global::System.Windows.Forms.ToolStripButton toolStripButtonShowSettings;

		// Token: 0x04000159 RID: 345
		private global::System.Windows.Forms.ToolStripMenuItem exportToolStripMenuItem;

		// Token: 0x0400015A RID: 346
		private global::System.Windows.Forms.ToolStripMenuItem exportAllToolStripMenuItem;

		// Token: 0x0400015B RID: 347
		private global::System.Windows.Forms.ToolStripStatusLabel toolStripStatistics;

		// Token: 0x0400015C RID: 348
		private global::System.Windows.Forms.StatusStrip statusStrip;

		// Token: 0x0400015D RID: 349
		private global::System.Windows.Forms.ToolStripMenuItem twoViewsToolStripMenuItem;

		// Token: 0x0400015E RID: 350
		private global::System.Windows.Forms.ToolStripMenuItem fourViewsToolStripMenuItem;

		// Token: 0x0400015F RID: 351
		private global::System.Windows.Forms.ToolStripSeparator toolStripSeparator6;

		// Token: 0x04000160 RID: 352
		private global::System.Windows.Forms.ToolStripMenuItem wireframeToolStripMenuItem;

		// Token: 0x04000161 RID: 353
		private global::System.Windows.Forms.ToolStripMenuItem texturedToolStripMenuItem;

		// Token: 0x04000162 RID: 354
		private global::System.Windows.Forms.ToolStripMenuItem lightingToolStripMenuItem;

		// Token: 0x04000163 RID: 355
		private global::System.Windows.Forms.ToolStripMenuItem framerateToolStripMenuItem;

		// Token: 0x04000164 RID: 356
		private global::System.Windows.Forms.ToolStripSeparator toolStripSeparator7;

		// Token: 0x04000165 RID: 357
		private global::System.Windows.Forms.ToolStripMenuItem showBoundingBoxesToolStripMenuItem;

		// Token: 0x04000166 RID: 358
		private global::System.Windows.Forms.ToolStripMenuItem showNormalVectorsToolStripMenuItem;

		// Token: 0x04000167 RID: 359
		private global::System.Windows.Forms.ToolStripMenuItem showAnimationSkeletonToolStripMenuItem;

		// Token: 0x04000168 RID: 360
		private global::System.Windows.Forms.ToolStripSeparator toolStripSeparator8;

		// Token: 0x04000169 RID: 361
		private global::System.Windows.Forms.ToolStripMenuItem optionsToolStripMenuItem;

		// Token: 0x0400016A RID: 362
		private global::System.DirectoryServices.DirectorySearcher directorySearcher1;

		// Token: 0x0400016B RID: 363
		private global::System.Windows.Forms.ColorDialog colorDialog1;

		// Token: 0x0400016C RID: 364
		private global::System.Windows.Forms.ToolStripSeparator toolStripSeparator9;

		// Token: 0x0400016D RID: 365
		private global::System.Windows.Forms.ToolStripMenuItem toolStripMenuItem2;

		// Token: 0x0400016E RID: 366
		private global::System.Windows.Forms.ToolStripSeparator toolStripSeparator10;

		// Token: 0x0400016F RID: 367
		private global::System.Windows.Forms.LinkLabel linkLabelDonate;

		// Token: 0x04000170 RID: 368
		private global::System.Windows.Forms.ToolStripMenuItem toolStripMenuItem3;

		// Token: 0x04000171 RID: 369
		private global::System.Windows.Forms.ToolStripSeparator toolStripSeparator11;

		// Token: 0x04000172 RID: 370
		private global::System.Windows.Forms.TrackBar trackBarBrightness;

		// Token: 0x04000173 RID: 371
		private global::System.Windows.Forms.Label label1;

		// Token: 0x04000174 RID: 372
		private global::System.Windows.Forms.LinkLabel linkLabelWebsite;

		// Token: 0x04000175 RID: 373
		private global::System.Windows.Forms.ToolStripStatusLabel toolStripStatusLabel1;

		// Token: 0x04000176 RID: 374
		private global::System.Windows.Forms.ToolStripMenuItem toolStripMenuItem4;

		// Token: 0x04000177 RID: 375
		private global::System.Windows.Forms.ToolStripMenuItem wusonToolStripMenuItem;

		// Token: 0x04000178 RID: 376
		private global::System.Windows.Forms.ToolStripMenuItem wustonAnimatedToolStripMenuItem;

		// Token: 0x04000179 RID: 377
		private global::System.Windows.Forms.ToolStripMenuItem lostEmpireToolStripMenuItem;

		// Token: 0x0400017A RID: 378
		private global::System.Windows.Forms.ToolStripMenuItem jeepToolStripMenuItem;

		// Token: 0x0400017B RID: 379
		private global::System.Windows.Forms.ToolStripMenuItem duckToolStripMenuItem;
	}
}
