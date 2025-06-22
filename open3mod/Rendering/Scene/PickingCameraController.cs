using System;
using OpenTK;

namespace open3mod
{
	// Token: 0x02000041 RID: 65
	public class PickingCameraController : ICameraController
	{
		// Token: 0x06000242 RID: 578 RVA: 0x000138A0 File Offset: 0x00011AA0
		public PickingCameraController(Matrix4 view)
		{
			this._view = view;
		}

		// Token: 0x06000243 RID: 579 RVA: 0x000138AF File Offset: 0x00011AAF
		public PickingCameraController()
		{
		}

		// Token: 0x06000244 RID: 580 RVA: 0x000138B7 File Offset: 0x00011AB7
		public void SetPivot(Vector3 pivot)
		{
		}

		// Token: 0x06000245 RID: 581 RVA: 0x000138B9 File Offset: 0x00011AB9
		public void SetView(Matrix4 view)
		{
			this._view = view;
		}

		// Token: 0x06000246 RID: 582 RVA: 0x000138C2 File Offset: 0x00011AC2
		public Matrix4 GetView()
		{
			return this._view;
		}

		// Token: 0x06000247 RID: 583 RVA: 0x000138CA File Offset: 0x00011ACA
		public void Pan(float x, float y)
		{
		}

		// Token: 0x06000248 RID: 584 RVA: 0x000138CC File Offset: 0x00011ACC
		public void MovementKey(float x, float y, float z)
		{
		}

		// Token: 0x06000249 RID: 585 RVA: 0x000138CE File Offset: 0x00011ACE
		public CameraMode GetCameraMode()
		{
			return CameraMode.Pick;
		}

		// Token: 0x0600024A RID: 586 RVA: 0x000138D1 File Offset: 0x00011AD1
		public void MouseMove(int x, int y)
		{
		}

		// Token: 0x0600024B RID: 587 RVA: 0x000138D3 File Offset: 0x00011AD3
		public void Scroll(float z)
		{
		}

		// Token: 0x0600024C RID: 588 RVA: 0x000138D5 File Offset: 0x00011AD5
		public void LeapInput(float x, float y, float z, float pitch, float roll, float yaw)
		{
		}

		// Token: 0x040001D8 RID: 472
		private Matrix4 _view;
	}
}
