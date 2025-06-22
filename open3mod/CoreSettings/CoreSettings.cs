using System;
using System.CodeDom.Compiler;
using System.Collections.Specialized;
using System.Configuration;
using System.Diagnostics;
using System.Drawing;
using System.Runtime.CompilerServices;

namespace CoreSettings
{
	// Token: 0x02000009 RID: 9
	[GeneratedCode("Microsoft.VisualStudio.Editors.SettingsDesigner.SettingsSingleFileGenerator", "11.0.0.0")]
	[CompilerGenerated]
	internal sealed partial class CoreSettings : ApplicationSettingsBase
	{
		// Token: 0x1700000E RID: 14
		// (get) Token: 0x06000033 RID: 51 RVA: 0x000042BF File Offset: 0x000024BF
		// (set) Token: 0x06000034 RID: 52 RVA: 0x000042D1 File Offset: 0x000024D1
		[DebuggerNonUserCode]
		[UserScopedSetting]
		public StringCollection RecentFiles
		{
			get
			{
				return (StringCollection)this["RecentFiles"];
			}
			set
			{
				this["RecentFiles"] = value;
			}
		}

		// Token: 0x17000029 RID: 41
		// (get) Token: 0x06000069 RID: 105 RVA: 0x000046A1 File Offset: 0x000028A1
		// (set) Token: 0x0600006A RID: 106 RVA: 0x000046B3 File Offset: 0x000028B3
		[DebuggerNonUserCode]
		[UserScopedSetting]
		public StringCollection AdditionalTextureFolders
		{
			get
			{
				return (StringCollection)this["AdditionalTextureFolders"];
			}
			set
			{
				this["AdditionalTextureFolders"] = value;
			}
		}

		// Token: 0x1700002B RID: 43
		// (get) Token: 0x0600006D RID: 109 RVA: 0x000046E6 File Offset: 0x000028E6
		// (set) Token: 0x0600006E RID: 110 RVA: 0x000046F8 File Offset: 0x000028F8
		[UserScopedSetting]
		[DebuggerNonUserCode]
		public bool ExitOnTabClosing
		{
			get
			{
				return (bool)this["ExitOnTabClosing"];
			}
			set
			{
				this["ExitOnTabClosing"] = value;
			}
		}
	}
}
