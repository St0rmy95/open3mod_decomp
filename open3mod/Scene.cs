using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using Assimp;
using Assimp.Configs;
using CoreSettings;
using OpenTK;

namespace open3mod
{
	// Token: 0x0200004C RID: 76
	public sealed class Scene : IDisposable
	{
		// Token: 0x17000073 RID: 115
		// (get) Token: 0x06000295 RID: 661 RVA: 0x00015BE3 File Offset: 0x00013DE3
		public string File
		{
			get
			{
				return this._file;
			}
		}

		// Token: 0x17000074 RID: 116
		// (get) Token: 0x06000296 RID: 662 RVA: 0x00015BEB File Offset: 0x00013DEB
		public string BaseDir
		{
			get
			{
				return this._baseDir;
			}
		}

		// Token: 0x17000075 RID: 117
		// (get) Token: 0x06000297 RID: 663 RVA: 0x00015BF3 File Offset: 0x00013DF3
		public Scene Raw
		{
			get
			{
				return this._raw;
			}
		}

		// Token: 0x17000076 RID: 118
		// (get) Token: 0x06000298 RID: 664 RVA: 0x00015BFB File Offset: 0x00013DFB
		public Vector3 SceneCenter
		{
			get
			{
				return this._sceneCenter;
			}
		}

		// Token: 0x17000077 RID: 119
		// (get) Token: 0x06000299 RID: 665 RVA: 0x00015C03 File Offset: 0x00013E03
		public LogStore LogStore
		{
			get
			{
				return this._logStore;
			}
		}

		// Token: 0x17000078 RID: 120
		// (get) Token: 0x0600029A RID: 666 RVA: 0x00015C0B File Offset: 0x00013E0B
		public MaterialMapper MaterialMapper
		{
			get
			{
				return this._mapper;
			}
		}

		// Token: 0x17000079 RID: 121
		// (get) Token: 0x0600029B RID: 667 RVA: 0x00015C13 File Offset: 0x00013E13
		public SceneAnimator SceneAnimator
		{
			get
			{
				return this._animator;
			}
		}

		// Token: 0x1700007A RID: 122
		// (get) Token: 0x0600029C RID: 668 RVA: 0x00015C1B File Offset: 0x00013E1B
		public Dictionary<Node, List<Mesh>> VisibleMeshesByNode
		{
			get
			{
				return this._meshesToShow;
			}
		}

		// Token: 0x1700007B RID: 123
		// (get) Token: 0x0600029D RID: 669 RVA: 0x00015C23 File Offset: 0x00013E23
		public TextureSet TextureSet
		{
			get
			{
				return this._textureSet;
			}
		}

		// Token: 0x1700007C RID: 124
		// (get) Token: 0x0600029E RID: 670 RVA: 0x00015C2B File Offset: 0x00013E2B
		public bool IsIncompleteScene
		{
			get
			{
				return this._incomplete;
			}
		}

		// Token: 0x1700007D RID: 125
		// (get) Token: 0x0600029F RID: 671 RVA: 0x00015C33 File Offset: 0x00013E33
		public int TotalVertexCount
		{
			get
			{
				return this._totalVertexCount;
			}
		}

		// Token: 0x1700007E RID: 126
		// (get) Token: 0x060002A0 RID: 672 RVA: 0x00015C3B File Offset: 0x00013E3B
		public int TotalTriangleCount
		{
			get
			{
				return this._totalTriangleCount;
			}
		}

		// Token: 0x1700007F RID: 127
		// (get) Token: 0x060002A1 RID: 673 RVA: 0x00015C43 File Offset: 0x00013E43
		public int TotalLineCount
		{
			get
			{
				return this._totalLineCount;
			}
		}

		// Token: 0x17000080 RID: 128
		// (get) Token: 0x060002A2 RID: 674 RVA: 0x00015C4B File Offset: 0x00013E4B
		public int TotalPointCount
		{
			get
			{
				return this._totalPointCount;
			}
		}

		// Token: 0x17000081 RID: 129
		// (get) Token: 0x060002A3 RID: 675 RVA: 0x00015C54 File Offset: 0x00013E54
		public string StatsString
		{
			get
			{
				string text = " Raw Loading Time: " + this.LoadingTime + " ms - ";
				object obj = text;
				text = string.Concat(new object[]
				{
					obj,
					this.TotalVertexCount,
					" Vertices, ",
					this.TotalTriangleCount,
					" Triangles"
				});
				if (this.TotalLineCount > 0)
				{
					object obj2 = text;
					text = string.Concat(new object[]
					{
						obj2,
						", ",
						this.TotalLineCount,
						" Lines"
					});
				}
				if (this.TotalPointCount > 0)
				{
					object obj3 = text;
					text = string.Concat(new object[]
					{
						obj3,
						", ",
						this.TotalLineCount,
						" Points"
					});
				}
				return text;
			}
		}

		// Token: 0x17000082 RID: 130
		// (get) Token: 0x060002A4 RID: 676 RVA: 0x00015D41 File Offset: 0x00013F41
		public long LoadingTime
		{
			get
			{
				return this._loadingTime;
			}
		}

		// Token: 0x17000083 RID: 131
		// (get) Token: 0x060002A5 RID: 677 RVA: 0x00015D49 File Offset: 0x00013F49
		public Vector3 Pivot
		{
			get
			{
				return this._pivot;
			}
		}

		// Token: 0x060002A6 RID: 678 RVA: 0x00015D54 File Offset: 0x00013F54
		public Scene(string file)
		{
			this._file = file;
			this._baseDir = Path.GetDirectoryName(file);
			this._logStore = new LogStore(200);
			Stopwatch stopwatch = new Stopwatch();
			stopwatch.Start();
			try
			{
				using (AssimpContext assimpContext = new AssimpContext())
				{
					LogStream.IsVerboseLoggingEnabled = true;
					using (new LogPipe(this._logStore))
					{
						assimpContext.SetConfig(new NormalSmoothingAngleConfig(66f));
						PostProcessSteps postProcessStepsFlags = Scene.GetPostProcessStepsFlags();
						this._raw = assimpContext.ImportFile(file, postProcessStepsFlags);
						if (this._raw == null)
						{
							this.Dispose();
							throw new Exception("failed to read file: " + file);
						}
						this._incomplete = this._raw.SceneFlags.HasFlag(SceneFlags.Incomplete);
					}
				}
			}
			catch (AssimpException ex)
			{
				this.Dispose();
				throw new Exception(string.Concat(new string[]
				{
					"failed to read file: ",
					file,
					" (",
					ex.Message,
					")"
				}));
			}
			stopwatch.Stop();
			this._loadingTime = stopwatch.ElapsedMilliseconds;
			this._animator = new SceneAnimator(this);
			this._textureSet = new TextureSet(this.BaseDir);
			this.LoadTextures();
			this.ComputeBoundingBox(out this._sceneMin, out this._sceneMax, out this._sceneCenter, null, false);
			this._pivot = this._sceneCenter;
			this.CountVertsAndFaces(out this._totalVertexCount, out this._totalTriangleCount, out this._totalLineCount, out this._totalPointCount);
			this.CreateRenderingBackend();
		}

		// Token: 0x060002A7 RID: 679 RVA: 0x00015F30 File Offset: 0x00014130
		private static PostProcessSteps GetPostProcessStepsFlags()
		{
			PostProcessSteps postProcessSteps = PostProcessPreset.TargetRealTimeMaximumQuality;
			CoreSettings @default = CoreSettings.Default;
			if (@default.ImportGenNormals)
			{
				postProcessSteps |= PostProcessSteps.GenerateSmoothNormals;
			}
			else
			{
				postProcessSteps &= ~PostProcessSteps.GenerateSmoothNormals;
			}
			if (@default.ImportGenTangents)
			{
				postProcessSteps |= PostProcessSteps.CalculateTangentSpace;
			}
			else
			{
				postProcessSteps &= ~PostProcessSteps.CalculateTangentSpace;
			}
			if (@default.ImportOptimize)
			{
				postProcessSteps |= PostProcessSteps.ImproveCacheLocality;
			}
			else
			{
				postProcessSteps &= ~PostProcessSteps.ImproveCacheLocality;
			}
			if (@default.ImportSortByPType)
			{
				postProcessSteps |= PostProcessSteps.SortByPrimitiveType;
			}
			else
			{
				postProcessSteps &= ~PostProcessSteps.SortByPrimitiveType;
			}
			if (@default.ImportRemoveDegenerates)
			{
				postProcessSteps |= (PostProcessSteps.FindDegenerates | PostProcessSteps.FindInvalidData);
			}
			else
			{
				postProcessSteps &= ~(PostProcessSteps.FindDegenerates | PostProcessSteps.FindInvalidData);
			}
			if (@default.ImportFixInfacing)
			{
				postProcessSteps |= PostProcessSteps.FixInFacingNormals;
			}
			else
			{
				postProcessSteps &= ~PostProcessSteps.FixInFacingNormals;
			}
			if (@default.ImportMergeDuplicates)
			{
				postProcessSteps |= PostProcessSteps.JoinIdenticalVertices;
			}
			else
			{
				postProcessSteps &= ~PostProcessSteps.JoinIdenticalVertices;
			}
			return postProcessSteps;
		}

		// Token: 0x060002A8 RID: 680 RVA: 0x00015FEC File Offset: 0x000141EC
		public void RecreateRenderingBackend()
		{
			if (this._renderer != null)
			{
				this._renderer.Dispose();
				this._renderer = null;
			}
			if (this._mapper != null)
			{
				this._mapper.Dispose();
				this._mapper = null;
			}
			this.CreateRenderingBackend();
		}

		// Token: 0x060002A9 RID: 681 RVA: 0x00016028 File Offset: 0x00014228
		private void CreateRenderingBackend()
		{
			if (GraphicsSettings.Default.RenderingBackend == 0)
			{
				this._mapper = new MaterialMapperClassicGl(this);
				this._renderer = new SceneRendererClassicGl(this, this._sceneMin, this._sceneMax);
				return;
			}
			this._mapper = new MaterialMapperModernGl(this);
			this._renderer = new SceneRendererModernGl(this, this._sceneMin, this._sceneMax);
		}

		// Token: 0x060002AA RID: 682 RVA: 0x0001608A File Offset: 0x0001428A
		public void SetVisibleNodes(Dictionary<Node, List<Mesh>> meshFilter)
		{
			this._meshesToShow = meshFilter;
			this._nodesToShowChanged = true;
		}

		// Token: 0x060002AB RID: 683 RVA: 0x0001609C File Offset: 0x0001429C
		public void Update(double delta, bool silent = false)
		{
			if (silent)
			{
				this._accumulatedTimeDelta += delta;
				return;
			}
			this._animator.Update(delta + this._accumulatedTimeDelta);
			this._accumulatedTimeDelta = 0.0;
			this._renderer.Update(delta);
		}

		// Token: 0x060002AC RID: 684 RVA: 0x000160EC File Offset: 0x000142EC
		public void Render(UiState state, ICameraController cam, Renderer target)
		{
			RenderFlags renderFlags = (RenderFlags)0;
			if (state.ShowNormals)
			{
				renderFlags |= RenderFlags.ShowNormals;
			}
			if (state.ShowBBs)
			{
				renderFlags |= RenderFlags.ShowBoundingBoxes;
			}
			if (state.ShowSkeleton || this._overrideSkeleton)
			{
				renderFlags |= RenderFlags.ShowSkeleton;
			}
			if (state.RenderLit)
			{
				renderFlags |= RenderFlags.Shaded;
			}
			if (state.RenderTextured)
			{
				renderFlags |= RenderFlags.Textured;
			}
			if (state.RenderWireframe)
			{
				renderFlags |= RenderFlags.Wireframe;
			}
			renderFlags |= RenderFlags.ShowGhosts;
			this._wantSetTexturesChanged = false;
			this._renderer.Render(cam, this._meshesToShow, this._nodesToShowChanged, this._texturesChanged, renderFlags, target);
			lock (this._texChangeLock)
			{
				if (!this._wantSetTexturesChanged)
				{
					this._texturesChanged = false;
				}
			}
			this._nodesToShowChanged = false;
		}

		// Token: 0x060002AD RID: 685 RVA: 0x000161CC File Offset: 0x000143CC
		private void LoadTextures()
		{
			List<Material> materials = this._raw.Materials;
			foreach (Material material in materials)
			{
				TextureSlot[] allMaterialTextures = material.GetAllMaterialTextures();
				foreach (TextureSlot textureSlot in allMaterialTextures)
				{
					string filePath = textureSlot.FilePath;
					EmbeddedTexture embeddedDataSource = null;
					uint num;
					if (filePath.StartsWith("*") && this.Raw.HasTextures && uint.TryParse(filePath.Substring(1), out num) && (ulong)num < (ulong)((long)this.Raw.TextureCount))
					{
						embeddedDataSource = this.Raw.Textures[(int)num];
					}
					this.TextureSet.Add(textureSlot.FilePath, embeddedDataSource);
				}
			}
			this.TextureSet.AddCallback(delegate(string name, Texture tex)
			{
				this.SetTexturesChangedFlag();
				return true;
			});
		}

		// Token: 0x060002AE RID: 686 RVA: 0x000162E0 File Offset: 0x000144E0
		private void SetTexturesChangedFlag()
		{
			lock (this._texChangeLock)
			{
				this._wantSetTexturesChanged = true;
				this._texturesChanged = true;
			}
		}

		// Token: 0x060002AF RID: 687 RVA: 0x00016328 File Offset: 0x00014528
		public void RequestReuploadTextures()
		{
			this.SetTexturesChangedFlag();
			foreach (Texture texture in this.TextureSet.GetLoadedTexturesCollectionThreadsafe())
			{
				if (texture.State == Texture.TextureState.GlTextureCreated)
				{
					texture.ReleaseUpload();
				}
			}
		}

		// Token: 0x060002B0 RID: 688 RVA: 0x00016388 File Offset: 0x00014588
		public void RequestReconfigureTextures()
		{
			this.SetTexturesChangedFlag();
			foreach (Texture texture in this.TextureSet.GetLoadedTexturesCollectionThreadsafe())
			{
				if (texture.State == Texture.TextureState.GlTextureCreated)
				{
					texture.ReconfigureUploadedTextureRequested = true;
				}
			}
		}

		// Token: 0x060002B1 RID: 689 RVA: 0x000163EC File Offset: 0x000145EC
		public void RequestRenderRefresh()
		{
			this._nodesToShowChanged = true;
		}

		// Token: 0x060002B2 RID: 690 RVA: 0x000163F8 File Offset: 0x000145F8
		private void CountVertsAndFaces(out int totalVertexCount, out int totalTriangleCount, out int totalLineCount, out int totalPointCount)
		{
			totalVertexCount = 0;
			totalTriangleCount = 0;
			totalLineCount = 0;
			totalPointCount = 0;
			for (int i = 0; i < this._raw.MeshCount; i++)
			{
				Mesh mesh = this._raw.Meshes[i];
				totalVertexCount += mesh.VertexCount;
				for (int j = 0; j < mesh.FaceCount; j++)
				{
					Face face = mesh.Faces[j];
					switch (face.IndexCount)
					{
					case 1:
						totalPointCount++;
						break;
					case 2:
						totalLineCount++;
						break;
					case 3:
						totalTriangleCount++;
						break;
					}
				}
			}
		}

		// Token: 0x060002B3 RID: 691 RVA: 0x0001649C File Offset: 0x0001469C
		private void ComputeBoundingBox(out Vector3 sceneMin, out Vector3 sceneMax, out Vector3 sceneCenter, Node node = null, bool omitRootNodeTrafo = false)
		{
			sceneMin = new Vector3(1E+10f, 1E+10f, 1E+10f);
			sceneMax = new Vector3(-1E+10f, -1E+10f, -1E+10f);
			Matrix4 matrix = omitRootNodeTrafo ? Matrix4.Identity : AssimpToOpenTk.FromMatrix((node ?? this._raw.RootNode).Transform);
			matrix.Transpose();
			this.ComputeBoundingBox(node ?? this._raw.RootNode, ref sceneMin, ref sceneMax, ref matrix);
			sceneCenter = (sceneMin + sceneMax) / 2f;
		}

		// Token: 0x060002B4 RID: 692 RVA: 0x0001655C File Offset: 0x0001475C
		private void ComputeBoundingBox(Node node, ref Vector3 min, ref Vector3 max, ref Matrix4 trafo)
		{
			if (node.HasMeshes)
			{
				foreach (Mesh mesh in from index in node.MeshIndices
				select this._raw.Meshes[index])
				{
					for (int i = 0; i < mesh.VertexCount; i++)
					{
						Vector3 vector = AssimpToOpenTk.FromVector(mesh.Vertices[i]);
						Vector3.Transform(ref vector, ref trafo, out vector);
						min.X = Math.Min(min.X, vector.X);
						min.Y = Math.Min(min.Y, vector.Y);
						min.Z = Math.Min(min.Z, vector.Z);
						max.X = Math.Max(max.X, vector.X);
						max.Y = Math.Max(max.Y, vector.Y);
						max.Z = Math.Max(max.Z, vector.Z);
					}
				}
			}
			for (int j = 0; j < node.ChildCount; j++)
			{
				Matrix4 matrix = trafo;
				Matrix4 matrix2 = AssimpToOpenTk.FromMatrix(node.Children[j].Transform);
				matrix2.Transpose();
				Matrix4.Mult(ref matrix2, ref matrix, out matrix);
				this.ComputeBoundingBox(node.Children[j], ref min, ref max, ref matrix);
			}
		}

		// Token: 0x060002B5 RID: 693 RVA: 0x000166F4 File Offset: 0x000148F4
		public void Dispose()
		{
			if (this._textureSet != null)
			{
				this._textureSet.Dispose();
			}
			if (this._renderer != null)
			{
				this._renderer.Dispose();
			}
			if (this._mapper != null)
			{
				this._mapper.Dispose();
			}
			GC.SuppressFinalize(this);
		}

		// Token: 0x060002B6 RID: 694 RVA: 0x00016740 File Offset: 0x00014940
		public void SetSkeletonVisibleOverride(bool overrideSkeleton)
		{
			this._overrideSkeleton = overrideSkeleton;
		}

		// Token: 0x060002B7 RID: 695 RVA: 0x0001674C File Offset: 0x0001494C
		public void SetPivot(Node node, bool realCenter = true)
		{
			if (node == null)
			{
				this._pivot = (realCenter ? this._sceneCenter : Vector3.Zero);
				return;
			}
			Vector3 zero = Vector3.Zero;
			if (realCenter)
			{
				Vector3 vector;
				Vector3 vector2;
				this.ComputeBoundingBox(out vector, out vector2, out zero, node, true);
			}
			do
			{
				Matrix4 matrix = AssimpToOpenTk.FromMatrix(node.Transform);
				matrix.Transpose();
				Vector3.Transform(ref zero, ref matrix, out zero);
			}
			while ((node = node.Parent) != null);
			this._pivot = zero;
		}

		// Token: 0x04000209 RID: 521
		private readonly string _file;

		// Token: 0x0400020A RID: 522
		private readonly string _baseDir;

		// Token: 0x0400020B RID: 523
		private readonly Scene _raw;

		// Token: 0x0400020C RID: 524
		private readonly Vector3 _sceneCenter;

		// Token: 0x0400020D RID: 525
		private readonly Vector3 _sceneMin;

		// Token: 0x0400020E RID: 526
		private readonly Vector3 _sceneMax;

		// Token: 0x0400020F RID: 527
		private readonly LogStore _logStore;

		// Token: 0x04000210 RID: 528
		private readonly TextureSet _textureSet;

		// Token: 0x04000211 RID: 529
		private MaterialMapper _mapper;

		// Token: 0x04000212 RID: 530
		private ISceneRenderer _renderer;

		// Token: 0x04000213 RID: 531
		private volatile bool _texturesChanged;

		// Token: 0x04000214 RID: 532
		private volatile bool _wantSetTexturesChanged;

		// Token: 0x04000215 RID: 533
		private readonly object _texChangeLock = new object();

		// Token: 0x04000216 RID: 534
		private readonly SceneAnimator _animator;

		// Token: 0x04000217 RID: 535
		private double _accumulatedTimeDelta;

		// Token: 0x04000218 RID: 536
		private bool _nodesToShowChanged = true;

		// Token: 0x04000219 RID: 537
		private Dictionary<Node, List<Mesh>> _meshesToShow;

		// Token: 0x0400021A RID: 538
		private bool _overrideSkeleton;

		// Token: 0x0400021B RID: 539
		private readonly bool _incomplete;

		// Token: 0x0400021C RID: 540
		private readonly int _totalVertexCount;

		// Token: 0x0400021D RID: 541
		private readonly int _totalTriangleCount;

		// Token: 0x0400021E RID: 542
		private readonly int _totalLineCount;

		// Token: 0x0400021F RID: 543
		private readonly int _totalPointCount;

		// Token: 0x04000220 RID: 544
		private readonly long _loadingTime;

		// Token: 0x04000221 RID: 545
		private Vector3 _pivot;
	}
}
