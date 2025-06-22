using System;
using OpenTK;

namespace open3mod
{
	// Token: 0x0200001F RID: 31
	public class FpsCameraController : ICameraController
	{
		// Token: 0x06000106 RID: 262 RVA: 0x0000959F File Offset: 0x0000779F
		public FpsCameraController()
		{
			this._view = Matrix4.Identity;
			this._translation = FpsCameraController.StartPosition;
			this.UpdateViewMatrix();
		}

		// Token: 0x06000107 RID: 263 RVA: 0x000095D1 File Offset: 0x000077D1
		public void SetPivot(Vector3 pivot)
		{
		}

		// Token: 0x06000108 RID: 264 RVA: 0x000095D3 File Offset: 0x000077D3
		public Matrix4 GetView()
		{
			if (this._dirty)
			{
				this.UpdateViewMatrix();
			}
			return this._view;
		}

		// Token: 0x06000109 RID: 265 RVA: 0x000095E9 File Offset: 0x000077E9
		public void Pan(float x, float y)
		{
		}

		// Token: 0x0600010A RID: 266 RVA: 0x000095EC File Offset: 0x000077EC
		public void MovementKey(float x, float y, float z)
		{
			Vector3 vector = new Vector3(x, y, z) * 1f;
			Matrix4 orientation = this.GetOrientation();
			this._translation += vector.X * orientation.Row0.Xyz + vector.Y * orientation.Row1.Xyz + vector.Z * orientation.Row2.Xyz;
			this._dirty = true;
		}

		// Token: 0x0600010B RID: 267 RVA: 0x0000967D File Offset: 0x0000787D
		public CameraMode GetCameraMode()
		{
			return CameraMode.Fps;
		}

		// Token: 0x0600010C RID: 268 RVA: 0x00009680 File Offset: 0x00007880
		public void MouseMove(int x, int y)
		{
			if (y != 0)
			{
				this._pitchAngle += (float)((double)((float)(-(float)y) * 0.5f) * 3.1415926535897931 / 180.0);
			}
			if (x != 0)
			{
				this._yawAngle += (float)((double)((float)(-(float)x) * 0.5f) * 3.1415926535897931 / 180.0);
			}
			this._dirty = true;
			this._updateOrientation = true;
		}

		// Token: 0x0600010D RID: 269 RVA: 0x000096FC File Offset: 0x000078FC
		public void Scroll(float z)
		{
			Matrix4 orientation = this.GetOrientation();
			this._translation -= orientation.Row2.Xyz * z * 0.002f;
			this._dirty = true;
		}

		// Token: 0x0600010E RID: 270 RVA: 0x00009744 File Offset: 0x00007944
		private void UpdateViewMatrix()
		{
			this._view = this.GetOrientation();
			this._view *= Matrix4.CreateFromAxisAngle(this._view.Row0.Xyz, this._pitchAngle);
			this._view.Transpose();
			this._view = Matrix4.CreateTranslation(-this._translation) * this._view;
			this._dirty = false;
		}

		// Token: 0x0600010F RID: 271 RVA: 0x000097BC File Offset: 0x000079BC
		private Matrix4 GetOrientation()
		{
			if (this._updateOrientation)
			{
				this._updateOrientation = false;
				this._orientation = Matrix4.CreateFromAxisAngle(Vector3.UnitY, this._yawAngle);
				this._orientation *= Matrix4.CreateFromAxisAngle(this._orientation.Row0.Xyz, this._pitchAngle);
			}
			return this._orientation;
		}

		// Token: 0x06000110 RID: 272 RVA: 0x00009820 File Offset: 0x00007A20
		public void LeapInput(float x, float y, float z, float pitch, float roll, float yaw)
		{
			this.Scroll(-z);
			this._pitchAngle += pitch * 0.05f;
			this._yawAngle += -yaw * 0.05f;
			this._updateOrientation = true;
			this.UpdateViewMatrix();
		}

		// Token: 0x04000095 RID: 149
		private const float MovementBaseSpeed = 1f;

		// Token: 0x04000096 RID: 150
		private const float BaseZoomSpeed = 0.002f;

		// Token: 0x04000097 RID: 151
		private const float RotationSpeed = 0.5f;

		// Token: 0x04000098 RID: 152
		private Matrix4 _view;

		// Token: 0x04000099 RID: 153
		private Matrix4 _orientation;

		// Token: 0x0400009A RID: 154
		private Vector3 _translation;

		// Token: 0x0400009B RID: 155
		private static readonly Vector3 StartPosition = new Vector3(0f, 1f, 2.5f);

		// Token: 0x0400009C RID: 156
		private bool _dirty = true;

		// Token: 0x0400009D RID: 157
		private bool _updateOrientation = true;

		// Token: 0x0400009E RID: 158
		private float _pitchAngle;

		// Token: 0x0400009F RID: 159
		private float _rollAngle;

		// Token: 0x040000A0 RID: 160
		private float _yawAngle;
	}
}
