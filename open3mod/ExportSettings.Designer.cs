using System;
using System.CodeDom.Compiler;
using System.Configuration;
using System.Diagnostics;
using System.Runtime.CompilerServices;

namespace open3mod
{
	// Token: 0x0200000E RID: 14
	[CompilerGenerated]
	[GeneratedCode("Microsoft.VisualStudio.Editors.SettingsDesigner.SettingsSingleFileGenerator", "11.0.0.0")]
	internal sealed partial class ExportSettings : ApplicationSettingsBase
	{
		// Token: 0x17000030 RID: 48
		// (get) Token: 0x0600008A RID: 138 RVA: 0x00004F78 File Offset: 0x00003178
		public static ExportSettings Default
		{
			get
			{
				return ExportSettings.defaultInstance;
			}
		}

		// Token: 0x17000031 RID: 49
		// (get) Token: 0x0600008B RID: 139 RVA: 0x00004F7F File Offset: 0x0000317F
		// (set) Token: 0x0600008C RID: 140 RVA: 0x00004F91 File Offset: 0x00003191
		[DebuggerNonUserCode]
		[UserScopedSetting]
		[DefaultSettingValue("textures")]
		public string CopyTexturesToFolder_Target
		{
			get
			{
				return (string)this["CopyTexturesToFolder_Target"];
			}
			set
			{
				this["CopyTexturesToFolder_Target"] = value;
			}
		}

		// Token: 0x17000032 RID: 50
		// (get) Token: 0x0600008D RID: 141 RVA: 0x00004F9F File Offset: 0x0000319F
		// (set) Token: 0x0600008E RID: 142 RVA: 0x00004FB1 File Offset: 0x000031B1
		[DefaultSettingValue("True")]
		[DebuggerNonUserCode]
		[UserScopedSetting]
		public bool UseRelativeTexturePaths
		{
			get
			{
				return (bool)this["UseRelativeTexturePaths"];
			}
			set
			{
				this["UseRelativeTexturePaths"] = value;
			}
		}

		// Token: 0x17000033 RID: 51
		// (get) Token: 0x0600008F RID: 143 RVA: 0x00004FC4 File Offset: 0x000031C4
		// (set) Token: 0x06000090 RID: 144 RVA: 0x00004FD6 File Offset: 0x000031D6
		[DebuggerNonUserCode]
		[DefaultSettingValue("True")]
		[UserScopedSetting]
		public bool IncludeSceneHierarchy
		{
			get
			{
				return (bool)this["IncludeSceneHierarchy"];
			}
			set
			{
				this["IncludeSceneHierarchy"] = value;
			}
		}

		// Token: 0x17000034 RID: 52
		// (get) Token: 0x06000091 RID: 145 RVA: 0x00004FE9 File Offset: 0x000031E9
		// (set) Token: 0x06000092 RID: 146 RVA: 0x00004FFB File Offset: 0x000031FB
		[DebuggerNonUserCode]
		[DefaultSettingValue("True")]
		[UserScopedSetting]
		public bool IncludeAnimations
		{
			get
			{
				return (bool)this["IncludeAnimations"];
			}
			set
			{
				this["IncludeAnimations"] = value;
			}
		}

		// Token: 0x17000035 RID: 53
		// (get) Token: 0x06000093 RID: 147 RVA: 0x0000500E File Offset: 0x0000320E
		// (set) Token: 0x06000094 RID: 148 RVA: 0x00005020 File Offset: 0x00003220
		[UserScopedSetting]
		[DefaultSettingValue("")]
		[DebuggerNonUserCode]
		public string OutputPath
		{
			get
			{
				return (string)this["OutputPath"];
			}
			set
			{
				this["OutputPath"] = value;
			}
		}

		// Token: 0x17000036 RID: 54
		// (get) Token: 0x06000095 RID: 149 RVA: 0x0000502E File Offset: 0x0000322E
		// (set) Token: 0x06000096 RID: 150 RVA: 0x00005040 File Offset: 0x00003240
		[UserScopedSetting]
		[DefaultSettingValue("False")]
		[DebuggerNonUserCode]
		public bool OpenExportedFileInViewer
		{
			get
			{
				return (bool)this["OpenExportedFileInViewer"];
			}
			set
			{
				this["OpenExportedFileInViewer"] = value;
			}
		}

		// Token: 0x17000037 RID: 55
		// (get) Token: 0x06000097 RID: 151 RVA: 0x00005053 File Offset: 0x00003253
		// (set) Token: 0x06000098 RID: 152 RVA: 0x00005065 File Offset: 0x00003265
		[UserScopedSetting]
		[DebuggerNonUserCode]
		[DefaultSettingValue("True")]
		public bool CopyTexturesToFolder
		{
			get
			{
				return (bool)this["CopyTexturesToFolder"];
			}
			set
			{
				this["CopyTexturesToFolder"] = value;
			}
		}

		// Token: 0x17000038 RID: 56
		// (get) Token: 0x06000099 RID: 153 RVA: 0x00005078 File Offset: 0x00003278
		// (set) Token: 0x0600009A RID: 154 RVA: 0x0000508A File Offset: 0x0000328A
		[DebuggerNonUserCode]
		[DefaultSettingValue("0")]
		[UserScopedSetting]
		public int ExportFormatIndex
		{
			get
			{
				return (int)this["ExportFormatIndex"];
			}
			set
			{
				this["ExportFormatIndex"] = value;
			}
		}

		// Token: 0x04000047 RID: 71
		private static ExportSettings defaultInstance = (ExportSettings)SettingsBase.Synchronized(new ExportSettings());
	}
}
