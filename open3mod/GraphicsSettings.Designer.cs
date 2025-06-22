using System;
using System.CodeDom.Compiler;
using System.Configuration;
using System.Diagnostics;
using System.Runtime.CompilerServices;

namespace open3mod
{
	// Token: 0x02000012 RID: 18
	[CompilerGenerated]
	[GeneratedCode("Microsoft.VisualStudio.Editors.SettingsDesigner.SettingsSingleFileGenerator", "11.0.0.0")]
	internal sealed partial class GraphicsSettings : ApplicationSettingsBase
	{
		// Token: 0x1700003A RID: 58
		// (get) Token: 0x060000AF RID: 175 RVA: 0x00005759 File Offset: 0x00003959
		public static GraphicsSettings Default
		{
			get
			{
				return GraphicsSettings.defaultInstance;
			}
		}

		// Token: 0x1700003B RID: 59
		// (get) Token: 0x060000B0 RID: 176 RVA: 0x00005760 File Offset: 0x00003960
		// (set) Token: 0x060000B1 RID: 177 RVA: 0x00005772 File Offset: 0x00003972
		[DebuggerNonUserCode]
		[DefaultSettingValue("0")]
		[UserScopedSetting]
		public int TexQualityBias
		{
			get
			{
				return (int)this["TexQualityBias"];
			}
			set
			{
				this["TexQualityBias"] = value;
			}
		}

		// Token: 0x1700003C RID: 60
		// (get) Token: 0x060000B2 RID: 178 RVA: 0x00005785 File Offset: 0x00003985
		// (set) Token: 0x060000B3 RID: 179 RVA: 0x00005797 File Offset: 0x00003997
		[DefaultSettingValue("3")]
		[DebuggerNonUserCode]
		[UserScopedSetting]
		public int TextureFilter
		{
			get
			{
				return (int)this["TextureFilter"];
			}
			set
			{
				this["TextureFilter"] = value;
			}
		}

		// Token: 0x1700003D RID: 61
		// (get) Token: 0x060000B4 RID: 180 RVA: 0x000057AA File Offset: 0x000039AA
		// (set) Token: 0x060000B5 RID: 181 RVA: 0x000057BC File Offset: 0x000039BC
		[DefaultSettingValue("True")]
		[DebuggerNonUserCode]
		[UserScopedSetting]
		public bool UseMips
		{
			get
			{
				return (bool)this["UseMips"];
			}
			set
			{
				this["UseMips"] = value;
			}
		}

		// Token: 0x1700003E RID: 62
		// (get) Token: 0x060000B6 RID: 182 RVA: 0x000057CF File Offset: 0x000039CF
		// (set) Token: 0x060000B7 RID: 183 RVA: 0x000057E1 File Offset: 0x000039E1
		[DebuggerNonUserCode]
		[DefaultSettingValue("2")]
		[UserScopedSetting]
		public int MultiSampling
		{
			get
			{
				return (int)this["MultiSampling"];
			}
			set
			{
				this["MultiSampling"] = value;
			}
		}

		// Token: 0x1700003F RID: 63
		// (get) Token: 0x060000B8 RID: 184 RVA: 0x000057F4 File Offset: 0x000039F4
		// (set) Token: 0x060000B9 RID: 185 RVA: 0x00005806 File Offset: 0x00003A06
		[UserScopedSetting]
		[DefaultSettingValue("0")]
		[DebuggerNonUserCode]
		public int RenderingBackend
		{
			get
			{
				return (int)this["RenderingBackend"];
			}
			set
			{
				this["RenderingBackend"] = value;
			}
		}

		// Token: 0x17000040 RID: 64
		// (get) Token: 0x060000BA RID: 186 RVA: 0x00005819 File Offset: 0x00003A19
		// (set) Token: 0x060000BB RID: 187 RVA: 0x0000582B File Offset: 0x00003A2B
		[UserScopedSetting]
		[DefaultSettingValue("1")]
		[DebuggerNonUserCode]
		public int LightingQuality
		{
			get
			{
				return (int)this["LightingQuality"];
			}
			set
			{
				this["LightingQuality"] = value;
			}
		}

		// Token: 0x17000041 RID: 65
		// (get) Token: 0x060000BC RID: 188 RVA: 0x0000583E File Offset: 0x00003A3E
		// (set) Token: 0x060000BD RID: 189 RVA: 0x00005850 File Offset: 0x00003A50
		[DefaultSettingValue("False")]
		[UserScopedSetting]
		[DebuggerNonUserCode]
		public bool BackFaceCulling
		{
			get
			{
				return (bool)this["BackFaceCulling"];
			}
			set
			{
				this["BackFaceCulling"] = value;
			}
		}

		// Token: 0x17000042 RID: 66
		// (get) Token: 0x060000BE RID: 190 RVA: 0x00005863 File Offset: 0x00003A63
		// (set) Token: 0x060000BF RID: 191 RVA: 0x00005875 File Offset: 0x00003A75
		[UserScopedSetting]
		[DefaultSettingValue("50")]
		[DebuggerNonUserCode]
		public int OutputBrightness
		{
			get
			{
				return (int)this["OutputBrightness"];
			}
			set
			{
				this["OutputBrightness"] = value;
			}
		}

		// Token: 0x04000050 RID: 80
		private static GraphicsSettings defaultInstance = (GraphicsSettings)SettingsBase.Synchronized(new GraphicsSettings());
	}
}
