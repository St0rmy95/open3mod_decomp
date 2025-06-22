using System;
using System.Collections.Generic;
using System.IO;
using System.Reflection;
using OpenTK.Graphics.OpenGL;

namespace open3mod
{
	// Token: 0x02000053 RID: 83
	public sealed class Shader : IDisposable
	{
		// Token: 0x060002FA RID: 762 RVA: 0x0001A698 File Offset: 0x00018898
		public static Shader FromResource(string vertexShaderResName, string fragmentShaderResName, string defines)
		{
			Assembly executingAssembly = Assembly.GetExecutingAssembly();
			string text;
			string text2;
			lock (Shader._textCache)
			{
				if (!Shader._textCache.TryGetValue(vertexShaderResName, out text))
				{
					Stream manifestResourceStream = executingAssembly.GetManifestResourceStream(vertexShaderResName);
					if (manifestResourceStream == null)
					{
						throw new Exception("failed to locate resource containing the vertex stage source code: " + vertexShaderResName);
					}
					using (StreamReader streamReader = new StreamReader(manifestResourceStream))
					{
						text = streamReader.ReadToEnd();
						Shader._textCache.Add(vertexShaderResName, text);
					}
				}
				if (!Shader._textCache.TryGetValue(fragmentShaderResName, out text2))
				{
					Stream manifestResourceStream2 = executingAssembly.GetManifestResourceStream(fragmentShaderResName);
					if (manifestResourceStream2 == null)
					{
						throw new Exception("failed to locate resource containing the fragment stage source code " + fragmentShaderResName);
					}
					using (StreamReader streamReader2 = new StreamReader(manifestResourceStream2))
					{
						text2 = streamReader2.ReadToEnd();
						Shader._textCache.Add(fragmentShaderResName, text2);
					}
				}
			}
			return new Shader(text, text2, defines);
		}

		// Token: 0x060002FB RID: 763 RVA: 0x0001A7A0 File Offset: 0x000189A0
		public Shader(string vertexShader, string fragmentShader, string defines)
		{
			this._program = GL.CreateProgram();
			this._vs = GL.CreateShader(ShaderType.VertexShader);
			GL.ShaderSource(this._vs, string.Format("{0}\n{1}", defines, vertexShader));
			GL.CompileShader(this._vs);
			int num;
			GL.GetShader(this._vs, ShaderParameter.CompileStatus, out num);
			if (num == 0)
			{
				this.Dispose();
				throw new Exception("failed to compile vertex shader");
			}
			this._fs = GL.CreateShader(ShaderType.FragmentShader);
			GL.ShaderSource(this._fs, string.Format("{0}\n{1}", defines, fragmentShader));
			GL.CompileShader(this._fs);
			GL.GetShader(this._fs, ShaderParameter.CompileStatus, out num);
			if (num == 0)
			{
				this.Dispose();
				throw new Exception("failed to compile fragment shader");
			}
			GL.AttachShader(this.Program, this._vs);
			GL.AttachShader(this.Program, this._fs);
			GL.LinkProgram(this.Program);
			GL.GetProgram(this.Program, ProgramParameter.LinkStatus, out num);
			if (num == 0)
			{
				this.Dispose();
				throw new Exception("failed to link shader program");
			}
		}

		// Token: 0x1700008D RID: 141
		// (get) Token: 0x060002FC RID: 764 RVA: 0x0001A8C1 File Offset: 0x00018AC1
		public int Program
		{
			get
			{
				return this._program;
			}
		}

		// Token: 0x060002FD RID: 765 RVA: 0x0001A8CC File Offset: 0x00018ACC
		public void Dispose()
		{
			if (this._disposed)
			{
				return;
			}
			this._disposed = true;
			if (this._program != 0)
			{
				GL.DeleteProgram(this._program);
			}
			if (this._vs != 0)
			{
				GL.DeleteShader(this._vs);
			}
			if (this._fs != 0)
			{
				GL.DeleteShader(this._fs);
			}
			GC.SuppressFinalize(this);
		}

		// Token: 0x04000270 RID: 624
		private readonly int _program;

		// Token: 0x04000271 RID: 625
		private readonly int _vs;

		// Token: 0x04000272 RID: 626
		private readonly int _fs;

		// Token: 0x04000273 RID: 627
		private bool _disposed;

		// Token: 0x04000274 RID: 628
		private static readonly Dictionary<string, string> _textCache = new Dictionary<string, string>();
	}
}
