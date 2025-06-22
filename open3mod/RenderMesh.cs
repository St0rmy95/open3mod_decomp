using System;
using System.Collections.Generic;
using System.Linq;
using Assimp;
using OpenTK;
using OpenTK.Graphics.OpenGL;

namespace open3mod
{
	// Token: 0x02000046 RID: 70
	public class RenderMesh
	{
		// Token: 0x0600027A RID: 634 RVA: 0x00014DFC File Offset: 0x00012FFC
		public RenderMesh(Mesh mesh)
		{
			this._mesh = mesh;
			this.Upload(out this._vbo);
		}

		// Token: 0x0600027B RID: 635 RVA: 0x00014E18 File Offset: 0x00013018
		public void Render(RenderFlags flags)
		{
			GL.PushClientAttrib(ClientAttribMask.ClientVertexArrayBit);
			if (flags.HasFlag(RenderFlags.Shaded) && this._vbo.NormalBufferId != 0)
			{
				GL.BindBuffer(BufferTarget.ArrayBuffer, this._vbo.NormalBufferId);
				GL.NormalPointer(NormalPointerType.Float, Vector3.SizeInBytes, IntPtr.Zero);
				GL.EnableClientState(ArrayCap.NormalArray);
			}
			if (this._vbo.ColorBufferId != 0)
			{
				GL.BindBuffer(BufferTarget.ArrayBuffer, this._vbo.ColorBufferId);
				GL.ColorPointer(4, ColorPointerType.UnsignedByte, 4, IntPtr.Zero);
				GL.EnableClientState(ArrayCap.ColorArray);
			}
			if (flags.HasFlag(RenderFlags.Textured) && this._vbo.TexCoordBufferId != 0)
			{
				GL.BindBuffer(BufferTarget.ArrayBuffer, this._vbo.TexCoordBufferId);
				GL.TexCoordPointer(2, TexCoordPointerType.Float, 8, IntPtr.Zero);
				GL.EnableClientState(ArrayCap.TextureCoordArray);
			}
			GL.BindBuffer(BufferTarget.ArrayBuffer, this._vbo.VertexBufferId);
			GL.VertexPointer(3, VertexPointerType.Float, Vector3.SizeInBytes, IntPtr.Zero);
			GL.EnableClientState(ArrayCap.VertexArray);
			GL.BindBuffer(BufferTarget.ElementArrayBuffer, this._vbo.ElementBufferID);
			GL.DrawElements(BeginMode.Triangles, this._vbo.NumElements, DrawElementsType.UnsignedInt, IntPtr.Zero);
			GL.PopClientAttrib();
		}

		// Token: 0x0600027C RID: 636 RVA: 0x00014F78 File Offset: 0x00013178
		private void Upload(out RenderMesh.Vbo vboToFill)
		{
			vboToFill = default(RenderMesh.Vbo);
			this.UploadVertices(out vboToFill.VertexBufferId);
			if (this._mesh.HasNormals)
			{
				this.UploadNormals(out vboToFill.NormalBufferId);
			}
			if (this._mesh.HasVertexColors(0))
			{
				this.UploadColors(out vboToFill.ColorBufferId);
			}
			if (this._mesh.HasTextureCoords(0))
			{
				this.UploadTextureCoords(out vboToFill.TexCoordBufferId);
			}
			if (this._mesh.HasTangentBasis)
			{
				this.UploadTangentsAndBitangents(out vboToFill.TangentBufferId, out vboToFill.BitangentBufferId);
			}
			vboToFill.NumElements = this.UploadPrimitives(out vboToFill.ElementBufferID);
		}

		// Token: 0x0600027D RID: 637 RVA: 0x00015018 File Offset: 0x00013218
		private void GenAndFillBuffer(out int outGlBufferId, List<Vector3D> dataBuffer)
		{
			GL.GenBuffers(1, out outGlBufferId);
			GL.BindBuffer(BufferTarget.ArrayBuffer, outGlBufferId);
			int num = dataBuffer.Count * 12;
			float[] array = new float[num];
			int num2 = 0;
			foreach (Vector3D vector3D in dataBuffer)
			{
				array[num2++] = vector3D.X;
				array[num2++] = vector3D.Y;
				array[num2++] = vector3D.Z;
			}
			GL.BufferData<float>(BufferTarget.ArrayBuffer, (IntPtr)num, array, BufferUsageHint.StaticDraw);
			this.VerifyBufferSize(num);
			GL.BindBuffer(BufferTarget.ArrayBuffer, 0);
		}

		// Token: 0x0600027E RID: 638 RVA: 0x000150D8 File Offset: 0x000132D8
		private void VerifyBufferSize(int byteCount)
		{
			int num;
			GL.GetBufferParameter(BufferTarget.ArrayBuffer, BufferParameterName.BufferSize, out num);
			if (byteCount != num)
			{
				throw new Exception("Vertex data array not uploaded correctly - buffer size does not match upload size");
			}
		}

		// Token: 0x0600027F RID: 639 RVA: 0x00015138 File Offset: 0x00013338
		private int UploadPrimitives(out int elementBufferId)
		{
			GL.GenBuffers(1, out elementBufferId);
			GL.BindBuffer(BufferTarget.ElementArrayBuffer, elementBufferId);
			List<Face> faces = this._mesh.Faces;
			int num = 0;
			bool flag = false;
			foreach (Face face2 in faces)
			{
				if (face2.IndexCount == 3)
				{
					num++;
					if (face2.Indices.Any((int idx) => idx > 65535))
					{
						flag = true;
					}
				}
			}
			int num2 = num * 3;
			int num3;
			if (flag)
			{
				uint[] array = new uint[num2];
				num3 = num2 * 4;
				int num4 = 0;
				foreach (int num5 in (from face in faces
				where face.IndexCount == 3
				select face).SelectMany((Face face) => face.Indices))
				{
					array[num4++] = (uint)num5;
				}
				GL.BufferData<uint>(BufferTarget.ElementArrayBuffer, (IntPtr)num3, array, BufferUsageHint.StaticDraw);
			}
			else
			{
				ushort[] array2 = new ushort[num2];
				num3 = num2 * 2;
				int num6 = 0;
				foreach (int num7 in (from face in faces
				where face.IndexCount == 3
				select face).SelectMany((Face face) => face.Indices))
				{
					array2[num6++] = (ushort)num7;
				}
				GL.BufferData<ushort>(BufferTarget.ElementArrayBuffer, (IntPtr)num3, array2, BufferUsageHint.StaticDraw);
			}
			int num8;
			GL.GetBufferParameter(BufferTarget.ElementArrayBuffer, BufferParameterName.BufferSize, out num8);
			if (num3 != num8)
			{
				throw new Exception("Index data array not uploaded correctly - buffer size does not match upload size");
			}
			GL.BindBuffer(BufferTarget.ElementArrayBuffer, 0);
			return num * 3;
		}

		// Token: 0x06000280 RID: 640 RVA: 0x0001537C File Offset: 0x0001357C
		private void UploadTextureCoords(out int texCoordBufferId)
		{
			GL.GenBuffers(1, out texCoordBufferId);
			GL.BindBuffer(BufferTarget.ArrayBuffer, texCoordBufferId);
			List<Vector3D> list = this._mesh.TextureCoordinateChannels[0];
			int num = list.Count * 2;
			float[] array = new float[num];
			int num2 = 0;
			foreach (Vector3D vector3D in list)
			{
				array[num2++] = vector3D.X;
				array[num2++] = vector3D.Y;
			}
			int num3 = num * 4;
			GL.BufferData<float>(BufferTarget.ArrayBuffer, (IntPtr)num3, array, BufferUsageHint.StaticDraw);
			this.VerifyBufferSize(num3);
			GL.BindBuffer(BufferTarget.ArrayBuffer, 0);
		}

		// Token: 0x06000281 RID: 641 RVA: 0x00015444 File Offset: 0x00013644
		private void UploadVertices(out int normalBufferId)
		{
			this.GenAndFillBuffer(out normalBufferId, this._mesh.Normals);
		}

		// Token: 0x06000282 RID: 642 RVA: 0x00015458 File Offset: 0x00013658
		private void UploadNormals(out int normalBufferId)
		{
			this.GenAndFillBuffer(out normalBufferId, this._mesh.Normals);
		}

		// Token: 0x06000283 RID: 643 RVA: 0x0001546C File Offset: 0x0001366C
		private void UploadTangentsAndBitangents(out int tangentBufferId, out int bitangentBufferId)
		{
			List<Vector3D> tangents = this._mesh.Tangents;
			this.GenAndFillBuffer(out tangentBufferId, tangents);
			List<Vector3D> biTangents = this._mesh.BiTangents;
			this.GenAndFillBuffer(out bitangentBufferId, biTangents);
		}

		// Token: 0x06000284 RID: 644 RVA: 0x000154A4 File Offset: 0x000136A4
		private void UploadColors(out int colorBufferId)
		{
			GL.GenBuffers(1, out colorBufferId);
			GL.BindBuffer(BufferTarget.ArrayBuffer, colorBufferId);
			List<Color4D> list = this._mesh.VertexColorChannels[0];
			int num = list.Count * 4;
			byte[] array = new byte[num];
			int num2 = 0;
			foreach (Color4D color4D in list)
			{
				array[num2++] = (byte)(color4D.R * 255f);
				array[num2++] = (byte)(color4D.G * 255f);
				array[num2++] = (byte)(color4D.B * 255f);
				array[num2++] = (byte)(color4D.A * 255f);
			}
			GL.BufferData<byte>(BufferTarget.ArrayBuffer, (IntPtr)num, array, BufferUsageHint.StaticDraw);
			int num3;
			GL.GetBufferParameter(BufferTarget.ArrayBuffer, BufferParameterName.BufferSize, out num3);
			if (num != num3)
			{
				throw new Exception("Vertex array not uploaded correctly");
			}
			GL.BindBuffer(BufferTarget.ArrayBuffer, 0);
		}

		// Token: 0x040001F0 RID: 496
		private readonly Mesh _mesh;

		// Token: 0x040001F1 RID: 497
		private readonly RenderMesh.Vbo _vbo;

		// Token: 0x02000047 RID: 71
		private struct Vbo
		{
			// Token: 0x040001F7 RID: 503
			public int VertexBufferId;

			// Token: 0x040001F8 RID: 504
			public int ColorBufferId;

			// Token: 0x040001F9 RID: 505
			public int TexCoordBufferId;

			// Token: 0x040001FA RID: 506
			public int NormalBufferId;

			// Token: 0x040001FB RID: 507
			public int TangentBufferId;

			// Token: 0x040001FC RID: 508
			public int ElementBufferID;

			// Token: 0x040001FD RID: 509
			public int NumElements;

			// Token: 0x040001FE RID: 510
			public int BitangentBufferId;
		}
	}
}
