using System;
using System.CodeDom.Compiler;
using System.ComponentModel;
using System.Diagnostics;
using System.Globalization;
using System.Resources;
using System.Runtime.CompilerServices;

namespace open3mod.Properties
{
	// Token: 0x02000076 RID: 118
	[GeneratedCode("System.Resources.Tools.StronglyTypedResourceBuilder", "4.0.0.0")]
	[DebuggerNonUserCode]
	[CompilerGenerated]
	internal class Resources
	{
		// Token: 0x060003E4 RID: 996 RVA: 0x000206E1 File Offset: 0x0001E8E1
		internal Resources()
		{
		}

		// Token: 0x170000B3 RID: 179
		// (get) Token: 0x060003E5 RID: 997 RVA: 0x000206EC File Offset: 0x0001E8EC
		[EditorBrowsable(EditorBrowsableState.Advanced)]
		internal static ResourceManager ResourceManager
		{
			get
			{
				if (object.ReferenceEquals(Resources.resourceMan, null))
				{
					ResourceManager resourceManager = new ResourceManager("open3mod.Properties.Resources", typeof(Resources).Assembly);
					Resources.resourceMan = resourceManager;
				}
				return Resources.resourceMan;
			}
		}

		// Token: 0x170000B4 RID: 180
		// (get) Token: 0x060003E6 RID: 998 RVA: 0x0002072B File Offset: 0x0001E92B
		// (set) Token: 0x060003E7 RID: 999 RVA: 0x00020732 File Offset: 0x0001E932
		[EditorBrowsable(EditorBrowsableState.Advanced)]
		internal static CultureInfo Culture
		{
			get
			{
				return Resources.resourceCulture;
			}
			set
			{
				Resources.resourceCulture = value;
			}
		}

		// Token: 0x170000B5 RID: 181
		// (get) Token: 0x060003E8 RID: 1000 RVA: 0x0002073A File Offset: 0x0001E93A
		internal static string LogViewer_FetchLogEntriesFromScene_No_scene_loaded
		{
			get
			{
				return Resources.ResourceManager.GetString("LogViewer_FetchLogEntriesFromScene_No_scene_loaded", Resources.resourceCulture);
			}
		}

		// Token: 0x170000B6 RID: 182
		// (get) Token: 0x060003E9 RID: 1001 RVA: 0x00020750 File Offset: 0x0001E950
		internal static string LogViewer_OnClearAll_Nothing_to_display
		{
			get
			{
				return Resources.ResourceManager.GetString("LogViewer_OnClearAll_Nothing_to_display", Resources.resourceCulture);
			}
		}

		// Token: 0x04000324 RID: 804
		private static ResourceManager resourceMan;

		// Token: 0x04000325 RID: 805
		private static CultureInfo resourceCulture;
	}
}
