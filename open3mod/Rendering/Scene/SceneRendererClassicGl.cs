using System;
using System.Collections.Generic;
using Assimp;
using OpenTK;
using OpenTK.Graphics;
using OpenTK.Graphics.OpenGL;

namespace open3mod
{
	// Token: 0x02000050 RID: 80
	public sealed class SceneRendererClassicGl : SceneRendererShared, ISceneRenderer, IDisposable
	{
		// Token: 0x060002D6 RID: 726 RVA: 0x00017190 File Offset: 0x00015390
		internal SceneRendererClassicGl(Scene owner, Vector3 initposeMin, Vector3 initposeMax) : base(owner, initposeMin, initposeMax)
		{
		}

		// Token: 0x060002D7 RID: 727 RVA: 0x0001719B File Offset: 0x0001539B
		public void Dispose()
		{
			if (this._displayList != 0)
			{
				GL.DeleteLists(this._displayList, 1);
				this._displayList = 0;
			}
			if (this._displayListAlpha != 0)
			{
				GL.DeleteLists(this._displayListAlpha, 1);
				this._displayListAlpha = 0;
			}
			GC.SuppressFinalize(this);
		}

		// Token: 0x060002D8 RID: 728 RVA: 0x000171D9 File Offset: 0x000153D9
		public void Update(double delta)
		{
			this.Skinner.Update();
		}

		// Token: 0x060002D9 RID: 729 RVA: 0x000171F8 File Offset: 0x000153F8
		public void Render(ICameraController cam, Dictionary<Node, List<Mesh>> visibleMeshesByNode, bool visibleSetChanged, bool texturesChanged, RenderFlags flags, Renderer renderer)
		{
			GL.Disable(EnableCap.Texture2D);
			GL.Hint(HintTarget.PerspectiveCorrectionHint, HintMode.Nicest);
			GL.Enable(EnableCap.DepthTest);
			GL.ShadeModel(ShadingModel.Smooth);
			GL.LightModel(LightModelParameter.LightModelAmbient, new float[]
			{
				0.3f,
				0.3f,
				0.3f,
				1f
			});
			GL.Enable(EnableCap.Lighting);
			GL.Enable(EnableCap.Light0);
			if (flags.HasFlag(RenderFlags.Wireframe))
			{
				GL.PolygonMode(MaterialFace.FrontAndBack, PolygonMode.Line);
			}
			float num = this.InitposeMax.X - this.InitposeMin.X;
			num = Math.Max(this.InitposeMax.Y - this.InitposeMin.Y, num);
			num = Math.Max(this.InitposeMax.Z - this.InitposeMin.Z, num);
			float num2 = 2f / num;
			if (cam != null)
			{
				cam.SetPivot(this.Owner.Pivot * num2);
			}
			Matrix4 matrix = (cam == null) ? Matrix4.LookAt(0f, 10f, 5f, 0f, 0f, 0f, 0f, 1f, 0f) : cam.GetView();
			GL.MatrixMode(MatrixMode.Modelview);
			GL.LoadMatrix(ref matrix);
			Vector3 dir = new Vector3(1f, 1f, 0f);
			Matrix4 lightRotation = renderer.LightRotation;
			Vector3.TransformNormal(ref dir, ref lightRotation, out dir);
			LightName light = LightName.Light0;
			LightParameter pname = LightParameter.Position;
			float[] array = new float[4];
			array[0] = dir.X;
			array[1] = dir.Y;
			array[2] = dir.Z;
			GL.Light(light, pname, array);
			Vector3 vec = new Vector3(1f, 1f, 1f);
			vec *= (0.25f + 1.5f * (float)Properties.GraphicsSettings.Default.OutputBrightness / 100f) * 1.5f;
			GL.Light(LightName.Light0, LightParameter.Diffuse, new float[]
			{
				vec.X,
				vec.Y,
				vec.Z,
				1f
			});
			GL.Light(LightName.Light0, LightParameter.Specular, new float[]
			{
				vec.X,
				vec.Y,
				vec.Z,
				1f
			});
			if (flags.HasFlag(RenderFlags.Shaded))
			{
				OverlayLightSource.DrawLightSource(dir);
			}
			GL.Scale(num2, num2, num2);
			if (texturesChanged)
			{
				base.UploadTextures();
			}
			GL.PushMatrix();
			bool isAnimationActive = this.Owner.SceneAnimator.IsAnimationActive;
			if (this._displayList == 0 || visibleSetChanged || texturesChanged || flags != this._lastFlags || isAnimationActive)
			{
				this._lastFlags = flags;
				if (!isAnimationActive)
				{
					if (this._displayList == 0)
					{
						this._displayList = GL.GenLists(1);
					}
					GL.NewList(this._displayList, ListMode.Compile);
				}
				bool flag = this.RecursiveRender(this.Owner.Raw.RootNode, visibleMeshesByNode, flags, isAnimationActive);
				if (flags.HasFlag(RenderFlags.ShowSkeleton))
				{
					this.RecursiveRenderNoScale(this.Owner.Raw.RootNode, visibleMeshesByNode, flags, 1f / num2, isAnimationActive);
				}
				if (flags.HasFlag(RenderFlags.ShowNormals))
				{
					this.RecursiveRenderNormals(this.Owner.Raw.RootNode, visibleMeshesByNode, flags, 1f / num2, isAnimationActive, Matrix4.Identity);
				}
				if (!isAnimationActive)
				{
					GL.EndList();
				}
				if (flag)
				{
					if (!isAnimationActive)
					{
						if (this._displayListAlpha == 0)
						{
							this._displayListAlpha = GL.GenLists(1);
						}
						GL.NewList(this._displayListAlpha, ListMode.Compile);
					}
					this.RecursiveRenderWithAlpha(this.Owner.Raw.RootNode, visibleMeshesByNode, flags, isAnimationActive);
					if (!isAnimationActive)
					{
						GL.EndList();
					}
				}
				else if (this._displayListAlpha != 0)
				{
					GL.DeleteLists(this._displayListAlpha, 1);
					this._displayListAlpha = 0;
				}
			}
			if (!isAnimationActive)
			{
				GL.CallList(this._displayList);
				if (this._displayListAlpha != 0)
				{
					GL.CallList(this._displayListAlpha);
				}
			}
			GL.PopMatrix();
			GL.PolygonMode(MaterialFace.FrontAndBack, PolygonMode.Fill);
			GL.Disable(EnableCap.DepthTest);
			GL.Disable(EnableCap.Texture2D);
			GL.Disable(EnableCap.Lighting);
		}

		// Token: 0x060002DA RID: 730 RVA: 0x00017668 File Offset: 0x00015868
		private bool RecursiveRender(Node node, Dictionary<Node, List<Mesh>> visibleMeshesByNode, RenderFlags flags, bool animated)
		{
			bool flag = false;
			Matrix4 matrix;
			if (animated)
			{
				this.Owner.SceneAnimator.GetLocalTransform(node, out matrix);
			}
			else
			{
				matrix = AssimpToOpenTk.FromMatrix(node.Transform);
			}
			matrix.Transpose();
			GL.PushMatrix();
			GL.MultMatrix(ref matrix);
			if (node.HasMeshes)
			{
				flag = base.DrawOpaqueMeshes(node, visibleMeshesByNode, flags, animated);
			}
			for (int i = 0; i < node.ChildCount; i++)
			{
				flag = (this.RecursiveRender(node.Children[i], visibleMeshesByNode, flags, animated) || flag);
			}
			GL.PopMatrix();
			return flag;
		}

		// Token: 0x060002DB RID: 731 RVA: 0x000176F8 File Offset: 0x000158F8
		private void RecursiveRenderWithAlpha(Node node, Dictionary<Node, List<Mesh>> visibleNodes, RenderFlags flags, bool animated)
		{
			Matrix4 matrix;
			if (animated)
			{
				this.Owner.SceneAnimator.GetLocalTransform(node, out matrix);
			}
			else
			{
				matrix = AssimpToOpenTk.FromMatrix(node.Transform);
			}
			matrix.Transpose();
			GL.PushMatrix();
			GL.MultMatrix(ref matrix);
			if (node.HasMeshes)
			{
				base.DrawAlphaMeshes(node, visibleNodes, flags, animated);
			}
			for (int i = 0; i < node.ChildCount; i++)
			{
				this.RecursiveRenderWithAlpha(node.Children[i], visibleNodes, flags, animated);
			}
			GL.PopMatrix();
		}

		// Token: 0x060002DC RID: 732 RVA: 0x0001777C File Offset: 0x0001597C
		protected override bool DrawMesh(Node node, bool animated, bool showGhost, int index, Mesh mesh, RenderFlags flags)
		{
			if (showGhost)
			{
				this.Owner.MaterialMapper.ApplyGhostMaterial(mesh, this.Owner.Raw.Materials[mesh.MaterialIndex], flags.HasFlag(RenderFlags.Shaded));
			}
			else
			{
				this.Owner.MaterialMapper.ApplyMaterial(mesh, this.Owner.Raw.Materials[mesh.MaterialIndex], flags.HasFlag(RenderFlags.Textured), flags.HasFlag(RenderFlags.Shaded));
			}
			if (Properties.GraphicsSettings.Default.BackFaceCulling)
			{
				GL.FrontFace(FrontFaceDirection.Ccw);
				GL.CullFace(CullFaceMode.Back);
				GL.Enable(EnableCap.CullFace);
			}
			else
			{
				GL.Disable(EnableCap.CullFace);
			}
			bool flag = mesh.HasVertexColors(0);
			bool flag2 = mesh.HasTextureCoords(0);
			bool flag3 = mesh.HasBones && animated;
			foreach (Face face in mesh.Faces)
			{
				BeginMode mode;
				switch (face.IndexCount)
				{
				case 1:
					mode = BeginMode.Points;
					break;
				case 2:
					mode = BeginMode.Lines;
					break;
				case 3:
					mode = BeginMode.Triangles;
					break;
				default:
					mode = BeginMode.Polygon;
					break;
				}
				GL.Begin(mode);
				for (int i = 0; i < face.IndexCount; i++)
				{
					int num = face.Indices[i];
					if (flag)
					{
						Color4 color = AssimpToOpenTk.FromColor(mesh.VertexColorChannels[0][num]);
						GL.Color4(color);
					}
					if (mesh.HasNormals)
					{
						Vector3 normal;
						if (flag3)
						{
							this.Skinner.GetTransformedVertexNormal(node, index, (uint)num, out normal);
						}
						else
						{
							normal = AssimpToOpenTk.FromVector(mesh.Normals[num]);
						}
						GL.Normal3(normal);
					}
					if (flag2)
					{
						Vector3 vector = AssimpToOpenTk.FromVector(mesh.TextureCoordinateChannels[0][num]);
						GL.TexCoord2(vector.X, 1f - vector.Y);
					}
					Vector3 v;
					if (flag3)
					{
						this.Skinner.GetTransformedVertexPosition(node, index, (uint)num, out v);
					}
					else
					{
						v = AssimpToOpenTk.FromVector(mesh.Vertices[num]);
					}
					GL.Vertex3(v);
				}
				GL.End();
			}
			GL.Disable(EnableCap.CullFace);
			return flag3;
		}

		// Token: 0x060002DD RID: 733 RVA: 0x00017A00 File Offset: 0x00015C00
		private void RecursiveRenderNoScale(Node node, Dictionary<Node, List<Mesh>> visibleMeshesByNode, RenderFlags flags, float invGlobalScale, bool animated)
		{
			Matrix4 matrix;
			Matrix4x4 matrix4x;
			if (animated)
			{
				this.Owner.SceneAnimator.GetLocalTransform(node, out matrix);
				OpenTkToAssimp.FromMatrix(ref matrix, out matrix4x);
			}
			else
			{
				matrix4x = node.Transform;
			}
			Vector3D vector3D;
			Assimp.Quaternion quaternion;
			Vector3D translation;
			matrix4x.Decompose(out vector3D, out quaternion, out translation);
			quaternion.Normalize();
			matrix4x = new Matrix4x4(quaternion.GetMatrix()) * Matrix4x4.FromTranslation(translation);
			matrix = AssimpToOpenTk.FromMatrix(ref matrix4x);
			matrix.Transpose();
			if (flags.HasFlag(RenderFlags.ShowSkeleton))
			{
				bool highlight = false;
				List<Mesh> list;
				if (visibleMeshesByNode != null && visibleMeshesByNode.TryGetValue(node, out list) && list == null && (node.Parent == null || !visibleMeshesByNode.TryGetValue(node.Parent, out list) || list != null))
				{
					highlight = true;
				}
				OverlaySkeleton.DrawSkeletonBone(node, invGlobalScale, highlight);
			}
			GL.PushMatrix();
			GL.MultMatrix(ref matrix);
			for (int i = 0; i < node.ChildCount; i++)
			{
				this.RecursiveRenderNoScale(node.Children[i], visibleMeshesByNode, flags, invGlobalScale, animated);
			}
			GL.PopMatrix();
		}

		// Token: 0x060002DE RID: 734 RVA: 0x00017B04 File Offset: 0x00015D04
		private void RecursiveRenderNormals(Node node, Dictionary<Node, List<Mesh>> visibleMeshesByNode, RenderFlags flags, float invGlobalScale, bool animated, Matrix4 transform)
		{
			Matrix4 left;
			if (animated)
			{
				this.Owner.SceneAnimator.GetLocalTransform(node, out left);
			}
			else
			{
				Matrix4x4 transform2 = node.Transform;
				left = AssimpToOpenTk.FromMatrix(ref transform2);
			}
			left.Transpose();
			transform = left * transform;
			if (flags.HasFlag(RenderFlags.ShowNormals))
			{
				List<Mesh> list = null;
				if (node.HasMeshes && (visibleMeshesByNode == null || visibleMeshesByNode.TryGetValue(node, out list)))
				{
					foreach (int num in node.MeshIndices)
					{
						Mesh mesh = this.Owner.Raw.Meshes[num];
						if (list == null || list.Contains(mesh))
						{
							OverlayNormals.DrawNormals(node, num, mesh, (mesh.HasBones && animated) ? this.Skinner : null, invGlobalScale, transform);
						}
					}
				}
			}
			for (int i = 0; i < node.ChildCount; i++)
			{
				this.RecursiveRenderNormals(node.Children[i], visibleMeshesByNode, flags, invGlobalScale, animated, transform);
			}
		}

		// Token: 0x04000238 RID: 568
		private int _displayList;

		// Token: 0x04000239 RID: 569
		private int _displayListAlpha;

		// Token: 0x0400023A RID: 570
		private RenderFlags _lastFlags;
	}
}
