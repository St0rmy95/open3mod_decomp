using System;
using System.Collections.Generic;
using Assimp;
using OpenTK;
using OpenTK.Graphics.OpenGL;

namespace open3mod
{
	// Token: 0x02000051 RID: 81
	public class SceneRendererModernGl : SceneRendererShared, ISceneRenderer, IDisposable
	{
		// Token: 0x060002DF RID: 735 RVA: 0x00017C34 File Offset: 0x00015E34
		internal SceneRendererModernGl(Scene owner, Vector3 initposeMin, Vector3 initposeMax) : base(owner, initposeMin, initposeMax)
		{
			this._meshes = new RenderMesh[owner.Raw.MeshCount];
		}

		// Token: 0x060002E0 RID: 736 RVA: 0x00017C55 File Offset: 0x00015E55
		public void Update(double delta)
		{
			this.Skinner.Update();
		}

		// Token: 0x060002E1 RID: 737 RVA: 0x00017C64 File Offset: 0x00015E64
		public void Render(ICameraController cam, Dictionary<Node, List<Mesh>> visibleMeshesByNode, bool visibleSetChanged, bool texturesChanged, RenderFlags flags, Renderer renderer)
		{
			GL.Disable(EnableCap.Texture2D);
			GL.Hint(HintTarget.PerspectiveCorrectionHint, HintMode.Nicest);
			GL.Enable(EnableCap.DepthTest);
			GL.FrontFace(FrontFaceDirection.Ccw);
			if (flags.HasFlag(RenderFlags.Wireframe))
			{
				GL.PolygonMode(MaterialFace.FrontAndBack, PolygonMode.Line);
			}
			if (texturesChanged)
			{
				base.UploadTextures();
			}
			GL.MatrixMode(MatrixMode.Modelview);
			if (cam != null)
			{
				cam.GetView();
			}
			else
			{
				Matrix4.LookAt(0f, 10f, 5f, 0f, 0f, 0f, 0f, 1f, 0f);
			}
			float num = this.InitposeMax.X - this.InitposeMin.X;
			num = Math.Max(this.InitposeMax.Y - this.InitposeMin.Y, num);
			num = Math.Max(this.InitposeMax.Z - this.InitposeMin.Z, num);
			num = 2f / num;
			Matrix4 left = Matrix4.Scale(num);
			left *= Matrix4.CreateTranslation(-(this.InitposeMin + this.InitposeMax) * 0.5f);
			bool isAnimationActive = this.Owner.SceneAnimator.IsAnimationActive;
			bool flag = this.RecursiveRender(this.Owner.Raw.RootNode, visibleMeshesByNode, flags, isAnimationActive, ref left);
			if (!flags.HasFlag(RenderFlags.ShowSkeleton))
			{
				flags.HasFlag(RenderFlags.ShowNormals);
			}
			if (flag)
			{
				this.RecursiveRenderWithAlpha(this.Owner.Raw.RootNode, visibleMeshesByNode, flags, isAnimationActive);
			}
			GL.PolygonMode(MaterialFace.FrontAndBack, PolygonMode.Fill);
			GL.Disable(EnableCap.DepthTest);
			GL.Disable(EnableCap.Texture2D);
			GL.Disable(EnableCap.Lighting);
		}

		// Token: 0x060002E2 RID: 738 RVA: 0x00017E44 File Offset: 0x00016044
		private bool RecursiveRender(Node node, Dictionary<Node, List<Mesh>> visibleMeshesByNode, RenderFlags flags, bool animated, ref Matrix4 world)
		{
			bool flag = false;
			Matrix4 right;
			if (animated)
			{
				this.Owner.SceneAnimator.GetLocalTransform(node, out right);
			}
			else
			{
				right = AssimpToOpenTk.FromMatrix(node.Transform);
			}
			right.Transpose();
			Matrix4 matrix = world * right;
			if (node.HasMeshes)
			{
				flag = base.DrawOpaqueMeshes(node, visibleMeshesByNode, flags, animated);
			}
			for (int i = 0; i < node.ChildCount; i++)
			{
				flag = (this.RecursiveRender(node.Children[i], visibleMeshesByNode, flags, animated, ref matrix) || flag);
			}
			return flag;
		}

		// Token: 0x060002E3 RID: 739 RVA: 0x00017ED4 File Offset: 0x000160D4
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
			if (node.HasMeshes)
			{
				base.DrawAlphaMeshes(node, visibleNodes, flags, animated);
			}
			for (int i = 0; i < node.ChildCount; i++)
			{
				this.RecursiveRenderWithAlpha(node.Children[i], visibleNodes, flags, animated);
			}
		}

		// Token: 0x060002E4 RID: 740 RVA: 0x00017F47 File Offset: 0x00016147
		protected override bool DrawMesh(Node node, bool animated, bool showGhost, int index, Mesh mesh, RenderFlags flags)
		{
			if (this._meshes[index] == null)
			{
				this._meshes[index] = new RenderMesh(mesh);
			}
			this._meshes[index].Render(flags);
			return true;
		}

		// Token: 0x060002E5 RID: 741 RVA: 0x00017F75 File Offset: 0x00016175
		public void Dispose()
		{
			GC.SuppressFinalize(this);
		}

		// Token: 0x0400023B RID: 571
		private RenderMesh[] _meshes;
	}
}
