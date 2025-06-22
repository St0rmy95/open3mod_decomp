using System;
using System.Linq;
using CoreSettings;
using OpenTK;

namespace open3mod
{
	// Token: 0x02000058 RID: 88
	public sealed class Tab
	{
		// Token: 0x1700008E RID: 142
		// (get) Token: 0x06000304 RID: 772 RVA: 0x0001AC4C File Offset: 0x00018E4C
		// (set) Token: 0x06000305 RID: 773 RVA: 0x0001AC54 File Offset: 0x00018E54
		public Tab.TabState State { get; private set; }

		// Token: 0x1700008F RID: 143
		// (get) Token: 0x06000306 RID: 774 RVA: 0x0001AC5D File Offset: 0x00018E5D
		// (set) Token: 0x06000307 RID: 775 RVA: 0x0001AC73 File Offset: 0x00018E73
		public Viewport[] ActiveViews
		{
			get
			{
				if (this._dirtySplit)
				{
					this.ValidateViewportBounds();
				}
				return this._activeViews;
			}
			set
			{
				this._activeViews = value;
			}
		}

		// Token: 0x17000090 RID: 144
		// (get) Token: 0x06000308 RID: 776 RVA: 0x0001AC7C File Offset: 0x00018E7C
		// (set) Token: 0x06000309 RID: 777 RVA: 0x0001AC84 File Offset: 0x00018E84
		public Tab.ViewMode ActiveViewMode
		{
			get
			{
				return this._activeViewMode;
			}
			set
			{
				this._activeViewMode = value;
				CoreSettings.Default.DefaultViewMode = (int)value;
				switch (this._activeViewMode)
				{
				case Tab.ViewMode.Single:
				{
					Viewport[] array = new Viewport[4];
					array[0] = new Viewport(new Vector4(0f, 0f, 1f, 1f), CameraMode.Orbit);
					this.ActiveViews = array;
					break;
				}
				case Tab.ViewMode.Two:
				{
					Viewport[] array2 = new Viewport[4];
					array2[0] = new Viewport(new Vector4(0f, 0f, 1f, 0.5f), CameraMode.Orbit);
					array2[2] = new Viewport(new Vector4(0f, 0.5f, 1f, 1f), CameraMode.X);
					this.ActiveViews = array2;
					break;
				}
				case Tab.ViewMode.Four:
					this.ActiveViews = new Viewport[]
					{
						new Viewport(new Vector4(0f, 0f, 0.5f, 0.5f), CameraMode.Orbit),
						new Viewport(new Vector4(0.5f, 0f, 1f, 0.5f), CameraMode.Z),
						new Viewport(new Vector4(0f, 0.5f, 0.5f, 1f), CameraMode.X),
						new Viewport(new Vector4(0.5f, 0.5f, 1f, 1f), CameraMode.Y)
					};
					break;
				default:
					throw new ArgumentOutOfRangeException();
				}
				if (this.ActiveViews[(int)this.ActiveViewIndex] == null)
				{
					this.ActiveViewIndex = Tab.ViewIndex.Index0;
				}
			}
		}

		// Token: 0x17000091 RID: 145
		// (get) Token: 0x0600030A RID: 778 RVA: 0x0001AE01 File Offset: 0x00019001
		public ICameraController ActiveCameraController
		{
			get
			{
				return this.ActiveCameraControllerForView(this.ActiveViewIndex);
			}
		}

		// Token: 0x17000092 RID: 146
		// (get) Token: 0x0600030B RID: 779 RVA: 0x0001AE0F File Offset: 0x0001900F
		// (set) Token: 0x0600030C RID: 780 RVA: 0x0001AE17 File Offset: 0x00019017
		public Scene ActiveScene
		{
			get
			{
				return this._activeScene;
			}
			set
			{
				if (this._activeScene != null)
				{
					this._activeScene.Dispose();
				}
				this._activeScene = value;
				if (this._activeScene == null)
				{
					this.State = Tab.TabState.Empty;
					return;
				}
				this.State = Tab.TabState.Rendering;
			}
		}

		// Token: 0x17000093 RID: 147
		// (get) Token: 0x0600030D RID: 781 RVA: 0x0001AE4A File Offset: 0x0001904A
		// (set) Token: 0x0600030E RID: 782 RVA: 0x0001AE52 File Offset: 0x00019052
		public string File { get; private set; }

		// Token: 0x17000094 RID: 148
		// (get) Token: 0x0600030F RID: 783 RVA: 0x0001AE5B File Offset: 0x0001905B
		public string ErrorMessage
		{
			get
			{
				return this._errorMessage;
			}
		}

		// Token: 0x06000310 RID: 784 RVA: 0x0001AE64 File Offset: 0x00019064
		public Tab(object id, string fileBeingLoaded)
		{
			int defaultViewMode = CoreSettings.Default.DefaultViewMode;
			if (defaultViewMode <= 2 && defaultViewMode >= 0)
			{
				this.ActiveViewMode = (Tab.ViewMode)defaultViewMode;
			}
			else
			{
				this.ActiveViewMode = Tab.ViewMode.Four;
				CoreSettings.Default.DefaultViewMode = 2;
			}
			this.State = ((fileBeingLoaded == null) ? Tab.TabState.Empty : Tab.TabState.Loading);
			this.File = fileBeingLoaded;
			this.Id = id;
		}

		// Token: 0x06000311 RID: 785 RVA: 0x0001AEF0 File Offset: 0x000190F0
		public ICameraController ActiveCameraControllerForView(Tab.ViewIndex targetView)
		{
			if (this.ActiveViews[(int)targetView] != null)
			{
				return this.ActiveViews[(int)targetView].ActiveCameraControllerForView();
			}
			return null;
		}

		// Token: 0x06000312 RID: 786 RVA: 0x0001AF0B File Offset: 0x0001910B
		public void Dispose()
		{
			if (this.ActiveScene != null)
			{
				this.ActiveScene.Dispose();
				this.ActiveScene = null;
			}
			GC.SuppressFinalize(this);
		}

		// Token: 0x06000313 RID: 787 RVA: 0x0001AF2D File Offset: 0x0001912D
		public void SetFailed(string message)
		{
			this.State = Tab.TabState.Failed;
			this._activeScene = null;
			this._errorMessage = message;
		}

		// Token: 0x06000314 RID: 788 RVA: 0x0001AF44 File Offset: 0x00019144
		public void ChangeActiveCameraMode(CameraMode cameraMode)
		{
			this.ChangeCameraModeForView(this.ActiveViewIndex, cameraMode);
		}

		// Token: 0x06000315 RID: 789 RVA: 0x0001AF54 File Offset: 0x00019154
		public void ChangeCameraModeForView(Tab.ViewIndex viewIndex, CameraMode cameraMode)
		{
			Viewport viewport = this.ActiveViews[(int)viewIndex];
			viewport.ChangeCameraModeForView(cameraMode);
		}

		// Token: 0x06000316 RID: 790 RVA: 0x0001AF74 File Offset: 0x00019174
		public void ResetActiveCameraController()
		{
			Viewport viewport = this.ActiveViews[(int)this.ActiveViewIndex];
			viewport.ResetCameraController();
		}

		// Token: 0x06000317 RID: 791 RVA: 0x0001AF98 File Offset: 0x00019198
		public Tab.ViewIndex GetViewportIndexHit(float x, float y)
		{
			Tab.ViewIndex viewIndex = Tab.ViewIndex.Index0;
			foreach (Viewport viewport in this.ActiveViews)
			{
				if (viewport == null)
				{
					viewIndex++;
				}
				else
				{
					Vector4 bounds = viewport.Bounds;
					if (x >= bounds.X && x <= bounds.Z && y >= bounds.Y && y <= bounds.W)
					{
						break;
					}
					viewIndex++;
				}
			}
			return viewIndex;
		}

		// Token: 0x06000318 RID: 792 RVA: 0x0001B004 File Offset: 0x00019204
		public Tab.ViewSeparator GetViewportSeparatorHit(float x, float y)
		{
			if (this._activeViewMode == Tab.ViewMode.Single)
			{
				return Tab.ViewSeparator._Max;
			}
			Viewport viewport = this.ActiveViews[0];
			if (Math.Abs(x - viewport.Bounds.Z) < 0.01f && this._activeViewMode != Tab.ViewMode.Two)
			{
				if (Math.Abs(y - viewport.Bounds.W) < 0.01f)
				{
					return Tab.ViewSeparator.Both;
				}
				return Tab.ViewSeparator.Vertical;
			}
			else
			{
				if (Math.Abs(y - viewport.Bounds.W) < 0.01f)
				{
					return Tab.ViewSeparator.Horizontal;
				}
				return Tab.ViewSeparator._Max;
			}
		}

		// Token: 0x06000319 RID: 793 RVA: 0x0001B07F File Offset: 0x0001927F
		public void SetViewportSplitH(float f)
		{
			if (this.ActiveViewMode != Tab.ViewMode.Four)
			{
				return;
			}
			if (f < 0.1f)
			{
				f = 0.1f;
			}
			else if (f > 0.9f)
			{
				f = 0.9f;
			}
			this._horizontalSplitPos = f;
			this._dirtySplit = true;
		}

		// Token: 0x0600031A RID: 794 RVA: 0x0001B0BC File Offset: 0x000192BC
		public void SetViewportSplitV(float f)
		{
			if (this.ActiveViewMode != Tab.ViewMode.Two && this.ActiveViewMode != Tab.ViewMode.Four)
			{
				return;
			}
			if (f < 0.1f)
			{
				f = 0.1f;
			}
			else if (f > 0.9f)
			{
				f = 0.9f;
			}
			this._verticalSplitPos = f;
			this._dirtySplit = true;
		}

		// Token: 0x0600031B RID: 795 RVA: 0x0001B114 File Offset: 0x00019314
		private void ValidateViewportBounds()
		{
			foreach (Viewport viewport2 in from viewport in this._activeViews
			where viewport != null
			select viewport)
			{
				Vector4 bounds = viewport2.Bounds;
				if (Math.Abs(bounds.Y - this._verticalSplitPos) > 0f && bounds.Y >= 0.099999f)
				{
					bounds.Y = this._verticalSplitPos;
				}
				else if (bounds.W <= 0.900001f)
				{
					bounds.W = this._verticalSplitPos;
				}
				if (Math.Abs(bounds.X - this._horizontalSplitPos) > 0f && bounds.X >= 0.099999f)
				{
					bounds.X = this._horizontalSplitPos;
				}
				else if (bounds.Z <= 0.900001f)
				{
					bounds.Z = this._horizontalSplitPos;
				}
				viewport2.Bounds = bounds;
			}
			this._dirtySplit = false;
		}

		// Token: 0x0400027E RID: 638
		private const float MinimumViewportSplit = 0.1f;

		// Token: 0x0400027F RID: 639
		public Tab.ViewIndex ActiveViewIndex;

		// Token: 0x04000280 RID: 640
		private Viewport[] _activeViews = new Viewport[4];

		// Token: 0x04000281 RID: 641
		public readonly object Id;

		// Token: 0x04000282 RID: 642
		private Scene _activeScene;

		// Token: 0x04000283 RID: 643
		private string _errorMessage;

		// Token: 0x04000284 RID: 644
		private Tab.ViewMode _activeViewMode = Tab.ViewMode.Four;

		// Token: 0x04000285 RID: 645
		private float _verticalSplitPos = 0.5f;

		// Token: 0x04000286 RID: 646
		private float _horizontalSplitPos = 0.5f;

		// Token: 0x04000287 RID: 647
		private bool _dirtySplit = true;

		// Token: 0x02000059 RID: 89
		public enum TabState
		{
			// Token: 0x0400028C RID: 652
			Empty,
			// Token: 0x0400028D RID: 653
			Loading,
			// Token: 0x0400028E RID: 654
			Rendering,
			// Token: 0x0400028F RID: 655
			Failed
		}

		// Token: 0x0200005A RID: 90
		public enum ViewIndex
		{
			// Token: 0x04000291 RID: 657
			Index0,
			// Token: 0x04000292 RID: 658
			Index1,
			// Token: 0x04000293 RID: 659
			Index2,
			// Token: 0x04000294 RID: 660
			Index3,
			// Token: 0x04000295 RID: 661
			_Max
		}

		// Token: 0x0200005B RID: 91
		public enum ViewSeparator
		{
			// Token: 0x04000297 RID: 663
			Horizontal,
			// Token: 0x04000298 RID: 664
			Vertical,
			// Token: 0x04000299 RID: 665
			_Max,
			// Token: 0x0400029A RID: 666
			Both
		}

		// Token: 0x0200005C RID: 92
		public enum ViewMode
		{
			// Token: 0x0400029C RID: 668
			Single,
			// Token: 0x0400029D RID: 669
			Two,
			// Token: 0x0400029E RID: 670
			Four
		}
	}
}
