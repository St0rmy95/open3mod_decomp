using System;
using OpenTK;

namespace open3mod
{
	// Token: 0x0200001E RID: 30
	public interface ICameraController
	{
		// Token: 0x060000FE RID: 254
		void SetPivot(Vector3 pivot);

		// Token: 0x060000FF RID: 255
		Matrix4 GetView();

		// Token: 0x06000100 RID: 256
		void MouseMove(int x, int y);

		// Token: 0x06000101 RID: 257
		void Scroll(float z);

		// Token: 0x06000102 RID: 258
		void Pan(float x, float y);

		// Token: 0x06000103 RID: 259
		void MovementKey(float x, float y, float z);

		// Token: 0x06000104 RID: 260
		CameraMode GetCameraMode();

		// Token: 0x06000105 RID: 261
		void LeapInput(float x, float y, float z, float pitch, float roll, float yaw);
	}
}
