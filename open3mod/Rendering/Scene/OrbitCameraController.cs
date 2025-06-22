using System;
using OpenTK;

namespace open3mod
{
	// Token: 0x0200003D RID: 61
	public class OrbitCameraController : ICameraController
	{
		// Token: 0x06000234 RID: 564 RVA: 0x00012FA8 File Offset: 0x000111A8
		public OrbitCameraController(CameraMode camMode)
		{
			this._view = Matrix4.CreateFromAxisAngle(new Vector3(0f, 1f, 0f), 0.9f);
			this._viewWithOffset = Matrix4.Identity;
			this._cameraDistance = 3f;
			this._right = Vector3.UnitX;
			this._up = Vector3.UnitY;
			this._front = Vector3.UnitZ;
			this.SetOrbitOrConstrainedMode(camMode, true);
		}

		// Token: 0x06000235 RID: 565 RVA: 0x00013030 File Offset: 0x00011230
		public void SetPivot(Vector3 pivot)
		{
			this._pivot = pivot;
			this._dirty = true;
		}

		// Token: 0x06000236 RID: 566 RVA: 0x00013040 File Offset: 0x00011240
		public Matrix4 GetView()
		{
			if (this._dirty)
			{
				this.UpdateViewMatrix();
			}
			return this._viewWithOffset;
		}

		// Token: 0x06000237 RID: 567 RVA: 0x00013058 File Offset: 0x00011258
		public void MouseMove(int x, int y)
		{
			if (x == 0 && y == 0)
			{
				return;
			}
			if (x != 0)
			{
				this._view *= Matrix4.CreateFromAxisAngle(this._up, (float)((double)((float)x * 0.5f) * 3.1415926535897931 / 180.0));
			}
			if (y != 0)
			{
				this._view *= Matrix4.CreateFromAxisAngle(this._right, (float)((double)((float)y * 0.5f) * 3.1415926535897931 / 180.0));
			}
			this._dirty = true;
			this.SetOrbitOrConstrainedMode(CameraMode.Orbit, false);
		}

		// Token: 0x06000238 RID: 568 RVA: 0x000130F5 File Offset: 0x000112F5
		public void Scroll(float z)
		{
			this._cameraDistance *= (float)Math.Pow(1.0010499954223633, (double)(-(double)z));
			this._cameraDistance = Math.Max(this._cameraDistance, 0.1f);
			this._dirty = true;
		}

		// Token: 0x06000239 RID: 569 RVA: 0x00013133 File Offset: 0x00011333
		public void Pan(float x, float y)
		{
			this._panVector.X = this._panVector.X + x * 0.004f;
			this._panVector.Y = this._panVector.Y + -y * 0.004f;
			this._dirty = true;
		}

		// Token: 0x0600023A RID: 570 RVA: 0x0001316F File Offset: 0x0001136F
		public void MovementKey(float x, float y, float z)
		{
		}

		// Token: 0x0600023B RID: 571 RVA: 0x00013171 File Offset: 0x00011371
		public CameraMode GetCameraMode()
		{
			return this._mode;
		}

		// Token: 0x0600023C RID: 572 RVA: 0x0001317C File Offset: 0x0001137C
		private void UpdateViewMatrix()
		{
			Matrix4 matrix = this._view * Matrix4.CreateFromAxisAngle(this._right, this._pitchAngle) * Matrix4.CreateFromAxisAngle(this._front, this._rollAngle);
			this._viewWithOffset = Matrix4.LookAt(matrix.Column2.Xyz * this._cameraDistance + this._pivot, this._pivot, matrix.Column1.Xyz);
			this._viewWithOffset *= Matrix4.CreateTranslation(this._panVector);
			this._dirty = false;
		}

		// Token: 0x0600023D RID: 573 RVA: 0x00013224 File Offset: 0x00011424
		public void SetOrbitOrConstrainedMode(CameraMode cameraMode, bool init = false)
		{
			if (this._mode == cameraMode && !init)
			{
				return;
			}
			this._mode = cameraMode;
			switch (this._mode)
			{
			case CameraMode.X:
				this._view = new Matrix4(0f, 0f, 1f, 0f, 0f, 1f, 0f, 0f, -1f, 0f, 0f, 0f, 0f, 0f, 0f, 1f);
				break;
			case CameraMode.Y:
				this._view = new Matrix4(0f, 1f, 0f, 0f, 0f, 0f, 1f, 0f, 1f, 0f, 0f, 0f, 0f, 0f, 0f, 1f);
				break;
			case CameraMode.Z:
				this._view = new Matrix4(1f, 0f, 0f, 0f, 0f, 1f, 0f, 0f, 0f, 0f, 1f, 0f, 0f, 0f, 0f, 1f);
				break;
			}
			if (this._mode != CameraMode.Orbit)
			{
				this._pitchAngle = 0f;
				this._rollAngle = 0f;
			}
			this._panVector = Vector3.Zero;
			this._dirty = true;
		}

		// Token: 0x0600023E RID: 574 RVA: 0x000133B0 File Offset: 0x000115B0
		public void LeapInput(float x, float y, float z, float pitch, float roll, float yaw)
		{
			this._pitchAngle = pitch * 3f;
			this._rollAngle = roll * 1f;
			Matrix4 right = Matrix4.CreateFromAxisAngle(this._up, (float)((double)x * 0.125 * 3.1415926535897931 / 180.0));
			this._view *= right;
			this.Scroll(z * 3f);
			this._dirty = true;
			this.SetOrbitOrConstrainedMode(CameraMode.Orbit, false);
		}

		// Token: 0x040001C7 RID: 455
		private const float ZoomSpeed = 1.00105f;

		// Token: 0x040001C8 RID: 456
		private const float MinimumCameraDistance = 0.1f;

		// Token: 0x040001C9 RID: 457
		private const float RotationSpeed = 0.5f;

		// Token: 0x040001CA RID: 458
		private const float PanSpeed = 0.004f;

		// Token: 0x040001CB RID: 459
		private const float InitialCameraDistance = 3f;

		// Token: 0x040001CC RID: 460
		private Matrix4 _view;

		// Token: 0x040001CD RID: 461
		private Matrix4 _viewWithOffset;

		// Token: 0x040001CE RID: 462
		private float _cameraDistance;

		// Token: 0x040001CF RID: 463
		private float _pitchAngle = 0.8f;

		// Token: 0x040001D0 RID: 464
		private float _rollAngle;

		// Token: 0x040001D1 RID: 465
		private readonly Vector3 _right;

		// Token: 0x040001D2 RID: 466
		private readonly Vector3 _up;

		// Token: 0x040001D3 RID: 467
		private readonly Vector3 _front;

		// Token: 0x040001D4 RID: 468
		private CameraMode _mode;

		// Token: 0x040001D5 RID: 469
		private Vector3 _panVector;

		// Token: 0x040001D6 RID: 470
		private bool _dirty = true;

		// Token: 0x040001D7 RID: 471
		private Vector3 _pivot;
	}
}
