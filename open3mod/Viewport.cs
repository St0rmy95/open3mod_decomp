using System;
using OpenTK;

namespace open3mod
{
	// Token: 0x02000075 RID: 117
	public class Viewport
	{
		// Token: 0x060003DB RID: 987 RVA: 0x00020538 File Offset: 0x0001E738
		public Viewport(Vector4 bounds, CameraMode camMode)
		{
			this._bounds = bounds;
			this._camMode = camMode;
		}

		// Token: 0x170000AF RID: 175
		// (get) Token: 0x060003DC RID: 988 RVA: 0x0002055A File Offset: 0x0001E75A
		// (set) Token: 0x060003DD RID: 989 RVA: 0x00020562 File Offset: 0x0001E762
		public Vector4 Bounds
		{
			get
			{
				return this._bounds;
			}
			set
			{
				this._bounds = value;
			}
		}

		// Token: 0x170000B0 RID: 176
		// (get) Token: 0x060003DE RID: 990 RVA: 0x0002056B File Offset: 0x0001E76B
		public CameraMode CameraMode
		{
			get
			{
				return this._camMode;
			}
		}

		// Token: 0x170000B1 RID: 177
		// (get) Token: 0x060003DF RID: 991 RVA: 0x00020573 File Offset: 0x0001E773
		public double RelativeWidth
		{
			get
			{
				return (double)(this._bounds.Z - this._bounds.X);
			}
		}

		// Token: 0x170000B2 RID: 178
		// (get) Token: 0x060003E0 RID: 992 RVA: 0x0002058D File Offset: 0x0001E78D
		public double RelativeHeight
		{
			get
			{
				return (double)(this._bounds.W - this._bounds.Y);
			}
		}

		// Token: 0x060003E1 RID: 993 RVA: 0x000205A8 File Offset: 0x0001E7A8
		public ICameraController ActiveCameraControllerForView()
		{
			CameraMode camMode = this._camMode;
			if (this._cameraImpls[(int)camMode] == null)
			{
				switch (camMode)
				{
				case CameraMode.X:
				case CameraMode.Y:
				case CameraMode.Z:
				case CameraMode.Orbit:
				{
					OrbitCameraController orbitCameraController = new OrbitCameraController(camMode);
					this._cameraImpls[0] = orbitCameraController;
					this._cameraImpls[1] = orbitCameraController;
					this._cameraImpls[2] = orbitCameraController;
					this._cameraImpls[3] = orbitCameraController;
					break;
				}
				case CameraMode.Fps:
					this._cameraImpls[(int)camMode] = new FpsCameraController();
					break;
				case CameraMode.Pick:
					this._cameraImpls[(int)camMode] = new PickingCameraController();
					break;
				}
			}
			return this._cameraImpls[(int)camMode];
		}

		// Token: 0x060003E2 RID: 994 RVA: 0x00020639 File Offset: 0x0001E839
		public void ResetCameraController()
		{
			this._cameraImpls[(int)this._camMode] = null;
			this.ActiveCameraControllerForView();
		}

		// Token: 0x060003E3 RID: 995 RVA: 0x00020650 File Offset: 0x0001E850
		public void ChangeCameraModeForView(CameraMode cameraMode)
		{
			if (this._camMode == cameraMode)
			{
				return;
			}
			ICameraController cameraController = this._cameraImpls[(int)this._camMode];
			if (cameraMode == CameraMode.Pick)
			{
				if (this._cameraImpls[(int)cameraMode] == null)
				{
					this._cameraImpls[(int)cameraMode] = new PickingCameraController();
				}
				PickingCameraController pickingCameraController = (PickingCameraController)this._cameraImpls[(int)cameraMode];
				pickingCameraController.SetView(cameraController.GetView());
			}
			this._camMode = cameraMode;
			if (cameraMode == CameraMode.Z || cameraMode == CameraMode.Y || cameraMode == CameraMode.X || cameraMode == CameraMode.Orbit)
			{
				if (this._cameraImpls[3] == null)
				{
					return;
				}
				OrbitCameraController orbitCameraController = this._cameraImpls[3] as OrbitCameraController;
				orbitCameraController.SetOrbitOrConstrainedMode(cameraMode, false);
			}
		}

		// Token: 0x04000321 RID: 801
		private Vector4 _bounds;

		// Token: 0x04000322 RID: 802
		private CameraMode _camMode;

		// Token: 0x04000323 RID: 803
		private readonly ICameraController[] _cameraImpls = new ICameraController[6];
	}
}
