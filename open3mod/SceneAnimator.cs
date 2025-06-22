using System;
using System.Collections.Generic;
using Assimp;
using OpenTK;

namespace open3mod
{
	// Token: 0x0200004D RID: 77
	public class SceneAnimator
	{
		// Token: 0x060002BA RID: 698 RVA: 0x000167BC File Offset: 0x000149BC
		internal SceneAnimator(Scene scene)
		{
			this._scene = scene;
			this._raw = scene.Raw;
			this._nodeStateByName = new Dictionary<string, SceneAnimator.NodeState>();
			int num = 0;
			for (int i = 0; i < this._raw.MeshCount; i++)
			{
				int boneCount = this._raw.Meshes[i].BoneCount;
				if (boneCount > num)
				{
					num = boneCount;
				}
			}
			this._boneMatrices = new Matrix4[num];
			this.ActiveAnimation = -1;
		}

		// Token: 0x17000084 RID: 132
		// (get) Token: 0x060002BB RID: 699 RVA: 0x00016853 File Offset: 0x00014A53
		// (set) Token: 0x060002BC RID: 700 RVA: 0x0001685C File Offset: 0x00014A5C
		public int ActiveAnimation
		{
			get
			{
				return this._activeAnim;
			}
			set
			{
				if (value == this._activeAnim)
				{
					return;
				}
				this._activeAnim = value;
				this._tree = this.CreateNodeTree(this._raw.RootNode, null);
				if (this._activeAnim != -1)
				{
					this._evaluator = new AnimEvaluator(this._raw.Animations[this._activeAnim], this.TicksPerSecond);
				}
			}
		}

		// Token: 0x17000085 RID: 133
		// (get) Token: 0x060002BD RID: 701 RVA: 0x000168C2 File Offset: 0x00014AC2
		// (set) Token: 0x060002BE RID: 702 RVA: 0x000168CA File Offset: 0x00014ACA
		public double AnimationPlaybackSpeed
		{
			get
			{
				return this._animPlaybackSpeed;
			}
			set
			{
				this._animPlaybackSpeed = value;
			}
		}

		// Token: 0x17000086 RID: 134
		// (get) Token: 0x060002BF RID: 703 RVA: 0x000168D3 File Offset: 0x00014AD3
		// (set) Token: 0x060002C0 RID: 704 RVA: 0x000168DC File Offset: 0x00014ADC
		public double AnimationCursor
		{
			get
			{
				return this._animCursor;
			}
			set
			{
				this._animCursor = value;
				if (!this.Loop && this._animCursor > this.AnimationDuration)
				{
					this._animCursor = this.AnimationDuration;
					this._isInEndPosition = true;
				}
				else
				{
					this._isInEndPosition = false;
				}
				this.Recalculate();
			}
		}

		// Token: 0x17000087 RID: 135
		// (get) Token: 0x060002C1 RID: 705 RVA: 0x00016928 File Offset: 0x00014B28
		public double TicksPerSecond
		{
			get
			{
				if (this.ActiveAnimation == -1)
				{
					return 0.0;
				}
				Animation animation = this._raw.Animations[this.ActiveAnimation];
				if (animation.TicksPerSecond <= 1E-10)
				{
					return 25.0;
				}
				return animation.TicksPerSecond;
			}
		}

		// Token: 0x17000088 RID: 136
		// (get) Token: 0x060002C2 RID: 706 RVA: 0x00016980 File Offset: 0x00014B80
		public double AnimationDuration
		{
			get
			{
				if (this.ActiveAnimation == -1)
				{
					return 0.0;
				}
				Animation animation = this._raw.Animations[this.ActiveAnimation];
				return animation.DurationInTicks / this.TicksPerSecond;
			}
		}

		// Token: 0x17000089 RID: 137
		// (get) Token: 0x060002C3 RID: 707 RVA: 0x000169C4 File Offset: 0x00014BC4
		// (set) Token: 0x060002C4 RID: 708 RVA: 0x000169CC File Offset: 0x00014BCC
		public bool Loop
		{
			get
			{
				return this._loop;
			}
			set
			{
				if (this._loop == value)
				{
					return;
				}
				this._loop = value;
				if (!value)
				{
					return;
				}
				double num = this._animCursor;
				if (this.AnimationDuration > 1E-06)
				{
					num %= this.AnimationDuration;
				}
				this.AnimationCursor = num;
				this._isInEndPosition = false;
			}
		}

		// Token: 0x1700008A RID: 138
		// (get) Token: 0x060002C5 RID: 709 RVA: 0x00016A1D File Offset: 0x00014C1D
		public bool IsAnimationActive
		{
			get
			{
				return this.ActiveAnimation > -1;
			}
		}

		// Token: 0x1700008B RID: 139
		// (get) Token: 0x060002C6 RID: 710 RVA: 0x00016A28 File Offset: 0x00014C28
		public bool IsInEndPosition
		{
			get
			{
				return this._isInEndPosition;
			}
		}

		// Token: 0x060002C7 RID: 711 RVA: 0x00016A30 File Offset: 0x00014C30
		public void Update(double delta)
		{
			this.AnimationCursor += delta * this.AnimationPlaybackSpeed;
		}

		// Token: 0x060002C8 RID: 712 RVA: 0x00016A47 File Offset: 0x00014C47
		public void GetGlobalTransform(string name, out Matrix4 outTrafo)
		{
			outTrafo = this._nodeStateByName[name].GlobalTransform;
		}

		// Token: 0x060002C9 RID: 713 RVA: 0x00016A60 File Offset: 0x00014C60
		public void GetGlobalTransform(Node node, out Matrix4 outTrafo)
		{
			this.GetGlobalTransform(node.Name, out outTrafo);
		}

		// Token: 0x060002CA RID: 714 RVA: 0x00016A6F File Offset: 0x00014C6F
		public void GetLocalTransform(string name, out Matrix4 outTrafo)
		{
			outTrafo = this._nodeStateByName[name].LocalTransform;
		}

		// Token: 0x060002CB RID: 715 RVA: 0x00016A88 File Offset: 0x00014C88
		public void GetLocalTransform(Node node, out Matrix4 outTrafo)
		{
			this.GetLocalTransform(node.Name, out outTrafo);
		}

		// Token: 0x060002CC RID: 716 RVA: 0x00016A98 File Offset: 0x00014C98
		public Matrix4[] GetBoneMatricesForMesh(Node node, Mesh mesh)
		{
			Matrix4 left;
			this.GetGlobalTransform(node, out left);
			left.Invert();
			for (int i = 0; i < mesh.BoneCount; i++)
			{
				Bone bone = mesh.Bones[i];
				Matrix4 right;
				this.GetGlobalTransform(bone.Name, out right);
				this._boneMatrices[i] = left * right * AssimpToOpenTk.FromMatrix(bone.OffsetMatrix);
				this._boneMatrices[i].Transpose();
			}
			return this._boneMatrices;
		}

		// Token: 0x060002CD RID: 717 RVA: 0x00016B20 File Offset: 0x00014D20
		private void Recalculate()
		{
			if (this.IsAnimationActive)
			{
				this._evaluator.Evaluate(this._animCursor, this._isInEndPosition);
				this.CalculateTransforms(this._tree, this._evaluator.CurrentTransforms);
			}
		}

		// Token: 0x060002CE RID: 718 RVA: 0x00016B58 File Offset: 0x00014D58
		private void CalculateTransforms(SceneAnimator.NodeState node, Matrix4[] perChannelLocalTransformation)
		{
			if (node.ChannelIndex != -1)
			{
				node.LocalTransform = perChannelLocalTransformation[node.ChannelIndex];
			}
			node.GlobalTransform = ((node.Parent != null) ? (node.Parent.GlobalTransform * node.LocalTransform) : node.LocalTransform);
			foreach (SceneAnimator.NodeState node2 in node.Children)
			{
				this.CalculateTransforms(node2, perChannelLocalTransformation);
			}
		}

		// Token: 0x060002CF RID: 719 RVA: 0x00016BD4 File Offset: 0x00014DD4
		private SceneAnimator.NodeState CreateNodeTree(Node rootNode, SceneAnimator.NodeState parent)
		{
			SceneAnimator.NodeState nodeState = new SceneAnimator.NodeState
			{
				LocalTransform = AssimpToOpenTk.FromMatrix(rootNode.Transform)
			};
			nodeState.Parent = parent;
			nodeState.GlobalTransform = ((parent != null) ? (parent.GlobalTransform * nodeState.LocalTransform) : nodeState.LocalTransform);
			this._nodeStateByName[rootNode.Name] = nodeState;
			nodeState.ChannelIndex = -1;
			if (this.ActiveAnimation != -1)
			{
				List<NodeAnimationChannel> nodeAnimationChannels = this._raw.Animations[this.ActiveAnimation].NodeAnimationChannels;
				for (int i = 0; i < nodeAnimationChannels.Count; i++)
				{
					if (!(nodeAnimationChannels[i].NodeName != rootNode.Name))
					{
						nodeState.ChannelIndex = i;
						break;
					}
				}
			}
			nodeState.Children = new SceneAnimator.NodeState[rootNode.ChildCount];
			for (int j = 0; j < rootNode.ChildCount; j++)
			{
				nodeState.Children[j] = this.CreateNodeTree(rootNode.Children[j], nodeState);
			}
			return nodeState;
		}

		// Token: 0x04000222 RID: 546
		public const double DefaultTicksPerSecond = 25.0;

		// Token: 0x04000223 RID: 547
		private readonly Scene _scene;

		// Token: 0x04000224 RID: 548
		private readonly Scene _raw;

		// Token: 0x04000225 RID: 549
		private int _activeAnim = -2;

		// Token: 0x04000226 RID: 550
		private double _animPlaybackSpeed = 1.0;

		// Token: 0x04000227 RID: 551
		private double _animCursor;

		// Token: 0x04000228 RID: 552
		private bool _loop = true;

		// Token: 0x04000229 RID: 553
		private AnimEvaluator _evaluator;

		// Token: 0x0400022A RID: 554
		private readonly Dictionary<string, SceneAnimator.NodeState> _nodeStateByName;

		// Token: 0x0400022B RID: 555
		private SceneAnimator.NodeState _tree;

		// Token: 0x0400022C RID: 556
		private readonly Matrix4[] _boneMatrices;

		// Token: 0x0400022D RID: 557
		private bool _isInEndPosition;

		// Token: 0x0200004E RID: 78
		private sealed class NodeState
		{
			// Token: 0x0400022E RID: 558
			public Matrix4 LocalTransform;

			// Token: 0x0400022F RID: 559
			public Matrix4 GlobalTransform;

			// Token: 0x04000230 RID: 560
			public int ChannelIndex;

			// Token: 0x04000231 RID: 561
			public SceneAnimator.NodeState Parent;

			// Token: 0x04000232 RID: 562
			public SceneAnimator.NodeState[] Children;
		}
	}
}
