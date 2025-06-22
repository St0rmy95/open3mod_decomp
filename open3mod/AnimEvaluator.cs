using System;
using System.Linq;
using Assimp;
using OpenTK;

namespace open3mod
{
	// Token: 0x02000004 RID: 4
	public class AnimEvaluator
	{
		// Token: 0x06000025 RID: 37 RVA: 0x000039A8 File Offset: 0x00001BA8
		public AnimEvaluator(Animation animation, double ticksPerSecond)
		{
			this._animation = animation;
			this._ticksPerSecond = ticksPerSecond;
			this._lastPositions = new AnimEvaluator.T3[this._animation.NodeAnimationChannelCount];
			this._currentTransforms = new Matrix4[this._animation.NodeAnimationChannelCount];
		}

		// Token: 0x1700000A RID: 10
		// (get) Token: 0x06000026 RID: 38 RVA: 0x000039F5 File Offset: 0x00001BF5
		public Animation Animation
		{
			get
			{
				return this._animation;
			}
		}

		// Token: 0x1700000B RID: 11
		// (get) Token: 0x06000027 RID: 39 RVA: 0x000039FD File Offset: 0x00001BFD
		public Matrix4[] CurrentTransforms
		{
			get
			{
				return this._currentTransforms;
			}
		}

		// Token: 0x06000028 RID: 40 RVA: 0x00003A08 File Offset: 0x00001C08
		public void Evaluate(double pTime, bool isInEndPosition)
		{
			pTime *= this._ticksPerSecond;
			double num = 0.0;
			if (this._animation.DurationInTicks > 0.0)
			{
				num = pTime % this._animation.DurationInTicks;
			}
			for (int i = 0; i < this._animation.NodeAnimationChannelCount; i++)
			{
				NodeAnimationChannel nodeAnimationChannel = this._animation.NodeAnimationChannels[i];
				Vector3D vector3D = new Vector3D(0f, 0f, 0f);
				Assimp.Quaternion quaternion = new Assimp.Quaternion(1f, 0f, 0f, 0f);
				Vector3D vector3D2 = new Vector3D(1f, 1f, 1f);
				if (isInEndPosition)
				{
					if (nodeAnimationChannel.PositionKeyCount > 0)
					{
						vector3D = nodeAnimationChannel.PositionKeys.Last<VectorKey>().Value;
						this._lastPositions[i].Item1 = nodeAnimationChannel.PositionKeyCount - 1;
					}
					if (nodeAnimationChannel.RotationKeyCount > 0)
					{
						quaternion = nodeAnimationChannel.RotationKeys.Last<QuaternionKey>().Value;
						this._lastPositions[i].Item2 = nodeAnimationChannel.RotationKeyCount - 1;
					}
					if (nodeAnimationChannel.ScalingKeyCount > 0)
					{
						vector3D2 = nodeAnimationChannel.ScalingKeys.Last<VectorKey>().Value;
						this._lastPositions[i].Item3 = nodeAnimationChannel.ScalingKeyCount - 1;
					}
					AnimEvaluator.BuildTransform(ref quaternion, ref vector3D2, ref vector3D, out this._currentTransforms[i]);
				}
				else
				{
					if (nodeAnimationChannel.PositionKeyCount > 0)
					{
						int num2 = (num >= this._lastTime) ? this._lastPositions[i].Item1 : 0;
						while (num2 < nodeAnimationChannel.PositionKeyCount - 1 && num >= nodeAnimationChannel.PositionKeys[num2 + 1].Time)
						{
							num2++;
						}
						int index = (num2 + 1) % nodeAnimationChannel.PositionKeyCount;
						VectorKey vectorKey = nodeAnimationChannel.PositionKeys[num2];
						VectorKey vectorKey2 = nodeAnimationChannel.PositionKeys[index];
						double num3 = vectorKey2.Time - vectorKey.Time;
						if (num3 < 0.0)
						{
							num3 += this._animation.DurationInTicks;
						}
						if (num3 > 0.0)
						{
							float scale = (float)((num - vectorKey.Time) / num3);
							vector3D = vectorKey.Value + (vectorKey2.Value - vectorKey.Value) * scale;
						}
						else
						{
							vector3D = vectorKey.Value;
						}
						this._lastPositions[i].Item1 = num2;
					}
					if (nodeAnimationChannel.RotationKeyCount > 0)
					{
						int num4 = (num >= this._lastTime) ? this._lastPositions[i].Item2 : 0;
						while (num4 < nodeAnimationChannel.RotationKeyCount - 1 && num >= nodeAnimationChannel.RotationKeys[num4 + 1].Time)
						{
							num4++;
						}
						int index2 = (num4 + 1) % nodeAnimationChannel.RotationKeyCount;
						QuaternionKey quaternionKey = nodeAnimationChannel.RotationKeys[num4];
						QuaternionKey quaternionKey2 = nodeAnimationChannel.RotationKeys[index2];
						double num5 = quaternionKey2.Time - quaternionKey.Time;
						if (num5 < 0.0)
						{
							num5 += this._animation.DurationInTicks;
						}
						if (num5 > 0.0)
						{
							float factor = (float)((num - quaternionKey.Time) / num5);
							quaternion = Assimp.Quaternion.Slerp(quaternionKey.Value, quaternionKey2.Value, factor);
						}
						else
						{
							quaternion = quaternionKey.Value;
						}
						this._lastPositions[i].Item2 = num4;
					}
					if (nodeAnimationChannel.ScalingKeyCount > 0)
					{
						int num6 = (num >= this._lastTime) ? this._lastPositions[i].Item3 : 0;
						while (num6 < nodeAnimationChannel.ScalingKeyCount - 1 && num >= nodeAnimationChannel.ScalingKeys[num6 + 1].Time)
						{
							num6++;
						}
						vector3D2 = nodeAnimationChannel.ScalingKeys[num6].Value;
						this._lastPositions[i].Item3 = num6;
					}
					AnimEvaluator.BuildTransform(ref quaternion, ref vector3D2, ref vector3D, out this._currentTransforms[i]);
				}
			}
			this._lastTime = num;
		}

		// Token: 0x06000029 RID: 41 RVA: 0x00003E38 File Offset: 0x00002038
		private static void BuildTransform(ref Assimp.Quaternion presentRotation, ref Vector3D presentScaling, ref Vector3D presentPosition, out Matrix4 outMatrix)
		{
			Matrix4x4 matrix4x = new Matrix4x4(presentRotation.GetMatrix());
			matrix4x.A1 *= presentScaling.X;
			matrix4x.B1 *= presentScaling.X;
			matrix4x.C1 *= presentScaling.X;
			matrix4x.A2 *= presentScaling.Y;
			matrix4x.B2 *= presentScaling.Y;
			matrix4x.C2 *= presentScaling.Y;
			matrix4x.A3 *= presentScaling.Z;
			matrix4x.B3 *= presentScaling.Z;
			matrix4x.C3 *= presentScaling.Z;
			matrix4x.A4 = presentPosition.X;
			matrix4x.B4 = presentPosition.Y;
			matrix4x.C4 = presentPosition.Z;
			outMatrix = AssimpToOpenTk.FromMatrix(ref matrix4x);
		}

		// Token: 0x04000029 RID: 41
		private readonly Animation _animation;

		// Token: 0x0400002A RID: 42
		private readonly double _ticksPerSecond;

		// Token: 0x0400002B RID: 43
		private readonly Matrix4[] _currentTransforms;

		// Token: 0x0400002C RID: 44
		private readonly AnimEvaluator.T3[] _lastPositions;

		// Token: 0x0400002D RID: 45
		private double _lastTime;

		// Token: 0x02000005 RID: 5
		private struct T3
		{
			// Token: 0x0400002E RID: 46
			public int Item1;

			// Token: 0x0400002F RID: 47
			public int Item2;

			// Token: 0x04000030 RID: 48
			public int Item3;
		}
	}
}
