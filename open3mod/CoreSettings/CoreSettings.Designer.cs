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
		// Token: 0x1700000D RID: 13
		// (get) Token: 0x06000032 RID: 50 RVA: 0x000042B8 File Offset: 0x000024B8
		public static CoreSettings Default
		{
			get
			{
				return CoreSettings.defaultInstance;
			}
		}

		// Token: 0x1700000F RID: 15
		// (get) Token: 0x06000035 RID: 53 RVA: 0x000042DF File Offset: 0x000024DF
		// (set) Token: 0x06000036 RID: 54 RVA: 0x000042F1 File Offset: 0x000024F1
		[DebuggerNonUserCode]
		[DefaultSettingValue("0, 0")]
		[UserScopedSetting]
		public Point Location
		{
			get
			{
				return (Point)this["Location"];
			}
			set
			{
				this["Location"] = value;
			}
		}

		// Token: 0x17000010 RID: 16
		// (get) Token: 0x06000037 RID: 55 RVA: 0x00004304 File Offset: 0x00002504
		// (set) Token: 0x06000038 RID: 56 RVA: 0x00004316 File Offset: 0x00002516
		[UserScopedSetting]
		[DefaultSettingValue("0, 0")]
		[DebuggerNonUserCode]
		public Size Size
		{
			get
			{
				return (Size)this["Size"];
			}
			set
			{
				this["Size"] = value;
			}
		}

		// Token: 0x17000011 RID: 17
		// (get) Token: 0x06000039 RID: 57 RVA: 0x00004329 File Offset: 0x00002529
		// (set) Token: 0x0600003A RID: 58 RVA: 0x0000433B File Offset: 0x0000253B
		[DefaultSettingValue("False")]
		[UserScopedSetting]
		[DebuggerNonUserCode]
		public bool Maximized
		{
			get
			{
				return (bool)this["Maximized"];
			}
			set
			{
				this["Maximized"] = value;
			}
		}

		// Token: 0x17000012 RID: 18
		// (get) Token: 0x0600003B RID: 59 RVA: 0x0000434E File Offset: 0x0000254E
		// (set) Token: 0x0600003C RID: 60 RVA: 0x00004360 File Offset: 0x00002560
		[DefaultSettingValue("False")]
		[DebuggerNonUserCode]
		[UserScopedSetting]
		public bool RenderWireframe
		{
			get
			{
				return (bool)this["RenderWireframe"];
			}
			set
			{
				this["RenderWireframe"] = value;
			}
		}

		// Token: 0x17000013 RID: 19
		// (get) Token: 0x0600003D RID: 61 RVA: 0x00004373 File Offset: 0x00002573
		// (set) Token: 0x0600003E RID: 62 RVA: 0x00004385 File Offset: 0x00002585
		[DebuggerNonUserCode]
		[UserScopedSetting]
		[DefaultSettingValue("True")]
		public bool RenderTextured
		{
			get
			{
				return (bool)this["RenderTextured"];
			}
			set
			{
				this["RenderTextured"] = value;
			}
		}

		// Token: 0x17000014 RID: 20
		// (get) Token: 0x0600003F RID: 63 RVA: 0x00004398 File Offset: 0x00002598
		// (set) Token: 0x06000040 RID: 64 RVA: 0x000043AA File Offset: 0x000025AA
		[UserScopedSetting]
		[DebuggerNonUserCode]
		[DefaultSettingValue("True")]
		public bool RenderLit
		{
			get
			{
				return (bool)this["RenderLit"];
			}
			set
			{
				this["RenderLit"] = value;
			}
		}

		// Token: 0x17000015 RID: 21
		// (get) Token: 0x06000041 RID: 65 RVA: 0x000043BD File Offset: 0x000025BD
		// (set) Token: 0x06000042 RID: 66 RVA: 0x000043CF File Offset: 0x000025CF
		[DebuggerNonUserCode]
		[UserScopedSetting]
		[DefaultSettingValue("False")]
		public bool ShowFps
		{
			get
			{
				return (bool)this["ShowFps"];
			}
			set
			{
				this["ShowFps"] = value;
			}
		}

		// Token: 0x17000016 RID: 22
		// (get) Token: 0x06000043 RID: 67 RVA: 0x000043E2 File Offset: 0x000025E2
		// (set) Token: 0x06000044 RID: 68 RVA: 0x000043F4 File Offset: 0x000025F4
		[DefaultSettingValue("False")]
		[UserScopedSetting]
		[DebuggerNonUserCode]
		public bool ShowBBs
		{
			get
			{
				return (bool)this["ShowBBs"];
			}
			set
			{
				this["ShowBBs"] = value;
			}
		}

		// Token: 0x17000017 RID: 23
		// (get) Token: 0x06000045 RID: 69 RVA: 0x00004407 File Offset: 0x00002607
		// (set) Token: 0x06000046 RID: 70 RVA: 0x00004419 File Offset: 0x00002619
		[UserScopedSetting]
		[DebuggerNonUserCode]
		[DefaultSettingValue("False")]
		public bool ShowNormals
		{
			get
			{
				return (bool)this["ShowNormals"];
			}
			set
			{
				this["ShowNormals"] = value;
			}
		}

		// Token: 0x17000018 RID: 24
		// (get) Token: 0x06000047 RID: 71 RVA: 0x0000442C File Offset: 0x0000262C
		// (set) Token: 0x06000048 RID: 72 RVA: 0x0000443E File Offset: 0x0000263E
		[UserScopedSetting]
		[DefaultSettingValue("False")]
		[DebuggerNonUserCode]
		public bool ShowSkeleton
		{
			get
			{
				return (bool)this["ShowSkeleton"];
			}
			set
			{
				this["ShowSkeleton"] = value;
			}
		}

		// Token: 0x17000019 RID: 25
		// (get) Token: 0x06000049 RID: 73 RVA: 0x00004451 File Offset: 0x00002651
		// (set) Token: 0x0600004A RID: 74 RVA: 0x00004463 File Offset: 0x00002663
		[UserScopedSetting]
		[DebuggerNonUserCode]
		[DefaultSettingValue("0")]
		public int DefaultRotationMode
		{
			get
			{
				return (int)this["DefaultRotationMode"];
			}
			set
			{
				this["DefaultRotationMode"] = value;
			}
		}

		// Token: 0x1700001A RID: 26
		// (get) Token: 0x0600004B RID: 75 RVA: 0x00004476 File Offset: 0x00002676
		// (set) Token: 0x0600004C RID: 76 RVA: 0x00004488 File Offset: 0x00002688
		[DefaultSettingValue("True")]
		[DebuggerNonUserCode]
		[UserScopedSetting]
		public bool NodeInfoShowAnimatedTrafo
		{
			get
			{
				return (bool)this["NodeInfoShowAnimatedTrafo"];
			}
			set
			{
				this["NodeInfoShowAnimatedTrafo"] = value;
			}
		}

		// Token: 0x1700001B RID: 27
		// (get) Token: 0x0600004D RID: 77 RVA: 0x0000449B File Offset: 0x0000269B
		// (set) Token: 0x0600004E RID: 78 RVA: 0x000044AD File Offset: 0x000026AD
		[DefaultSettingValue("False")]
		[UserScopedSetting]
		[DebuggerNonUserCode]
		public bool ShowGlobalTrafo
		{
			get
			{
				return (bool)this["ShowGlobalTrafo"];
			}
			set
			{
				this["ShowGlobalTrafo"] = value;
			}
		}

		// Token: 0x1700001C RID: 28
		// (get) Token: 0x0600004F RID: 79 RVA: 0x000044C0 File Offset: 0x000026C0
		// (set) Token: 0x06000050 RID: 80 RVA: 0x000044D2 File Offset: 0x000026D2
		[DefaultSettingValue("2")]
		[UserScopedSetting]
		[DebuggerNonUserCode]
		public int DefaultViewMode
		{
			get
			{
				return (int)this["DefaultViewMode"];
			}
			set
			{
				this["DefaultViewMode"] = value;
			}
		}

		// Token: 0x1700001D RID: 29
		// (get) Token: 0x06000051 RID: 81 RVA: 0x000044E5 File Offset: 0x000026E5
		// (set) Token: 0x06000052 RID: 82 RVA: 0x000044F7 File Offset: 0x000026F7
		[UserScopedSetting]
		[DefaultSettingValue("273")]
		[DebuggerNonUserCode]
		public int InspectorSplitterPos
		{
			get
			{
				return (int)this["InspectorSplitterPos"];
			}
			set
			{
				this["InspectorSplitterPos"] = value;
			}
		}

		// Token: 0x1700001E RID: 30
		// (get) Token: 0x06000053 RID: 83 RVA: 0x0000450A File Offset: 0x0000270A
		// (set) Token: 0x06000054 RID: 84 RVA: 0x0000451C File Offset: 0x0000271C
		[DefaultSettingValue("True")]
		[UserScopedSetting]
		[DebuggerNonUserCode]
		public bool ShowTipsOnStartup
		{
			get
			{
				return (bool)this["ShowTipsOnStartup"];
			}
			set
			{
				this["ShowTipsOnStartup"] = value;
			}
		}

		// Token: 0x1700001F RID: 31
		// (get) Token: 0x06000055 RID: 85 RVA: 0x0000452F File Offset: 0x0000272F
		// (set) Token: 0x06000056 RID: 86 RVA: 0x00004541 File Offset: 0x00002741
		[UserScopedSetting]
		[DefaultSettingValue("0")]
		[DebuggerNonUserCode]
		public int NextTip
		{
			get
			{
				return (int)this["NextTip"];
			}
			set
			{
				this["NextTip"] = value;
			}
		}

		// Token: 0x17000020 RID: 32
		// (get) Token: 0x06000057 RID: 87 RVA: 0x00004554 File Offset: 0x00002754
		// (set) Token: 0x06000058 RID: 88 RVA: 0x00004566 File Offset: 0x00002766
		[UserScopedSetting]
		[DebuggerNonUserCode]
		[DefaultSettingValue("0")]
		public int DonationUseCountDown
		{
			get
			{
				return (int)this["DonationUseCountDown"];
			}
			set
			{
				this["DonationUseCountDown"] = value;
			}
		}

		// Token: 0x17000021 RID: 33
		// (get) Token: 0x06000059 RID: 89 RVA: 0x00004579 File Offset: 0x00002779
		// (set) Token: 0x0600005A RID: 90 RVA: 0x0000458B File Offset: 0x0000278B
		[DebuggerNonUserCode]
		[DefaultSettingValue("True")]
		[UserScopedSetting]
		public bool ImportGenNormals
		{
			get
			{
				return (bool)this["ImportGenNormals"];
			}
			set
			{
				this["ImportGenNormals"] = value;
			}
		}

		// Token: 0x17000022 RID: 34
		// (get) Token: 0x0600005B RID: 91 RVA: 0x0000459E File Offset: 0x0000279E
		// (set) Token: 0x0600005C RID: 92 RVA: 0x000045B0 File Offset: 0x000027B0
		[DefaultSettingValue("True")]
		[DebuggerNonUserCode]
		[UserScopedSetting]
		public bool ImportGenTangents
		{
			get
			{
				return (bool)this["ImportGenTangents"];
			}
			set
			{
				this["ImportGenTangents"] = value;
			}
		}

		// Token: 0x17000023 RID: 35
		// (get) Token: 0x0600005D RID: 93 RVA: 0x000045C3 File Offset: 0x000027C3
		// (set) Token: 0x0600005E RID: 94 RVA: 0x000045D5 File Offset: 0x000027D5
		[UserScopedSetting]
		[DefaultSettingValue("True")]
		[DebuggerNonUserCode]
		public bool ImportOptimize
		{
			get
			{
				return (bool)this["ImportOptimize"];
			}
			set
			{
				this["ImportOptimize"] = value;
			}
		}

		// Token: 0x17000024 RID: 36
		// (get) Token: 0x0600005F RID: 95 RVA: 0x000045E8 File Offset: 0x000027E8
		// (set) Token: 0x06000060 RID: 96 RVA: 0x000045FA File Offset: 0x000027FA
		[DefaultSettingValue("False")]
		[UserScopedSetting]
		[DebuggerNonUserCode]
		public bool ImportSortByPType
		{
			get
			{
				return (bool)this["ImportSortByPType"];
			}
			set
			{
				this["ImportSortByPType"] = value;
			}
		}

		// Token: 0x17000025 RID: 37
		// (get) Token: 0x06000061 RID: 97 RVA: 0x0000460D File Offset: 0x0000280D
		// (set) Token: 0x06000062 RID: 98 RVA: 0x0000461F File Offset: 0x0000281F
		[DefaultSettingValue("True")]
		[DebuggerNonUserCode]
		[UserScopedSetting]
		public bool ImportRemoveDegenerates
		{
			get
			{
				return (bool)this["ImportRemoveDegenerates"];
			}
			set
			{
				this["ImportRemoveDegenerates"] = value;
			}
		}

		// Token: 0x17000026 RID: 38
		// (get) Token: 0x06000063 RID: 99 RVA: 0x00004632 File Offset: 0x00002832
		// (set) Token: 0x06000064 RID: 100 RVA: 0x00004644 File Offset: 0x00002844
		[DefaultSettingValue("True")]
		[UserScopedSetting]
		[DebuggerNonUserCode]
		public bool ImportFixInfacing
		{
			get
			{
				return (bool)this["ImportFixInfacing"];
			}
			set
			{
				this["ImportFixInfacing"] = value;
			}
		}

		// Token: 0x17000027 RID: 39
		// (get) Token: 0x06000065 RID: 101 RVA: 0x00004657 File Offset: 0x00002857
		// (set) Token: 0x06000066 RID: 102 RVA: 0x00004669 File Offset: 0x00002869
		[UserScopedSetting]
		[DefaultSettingValue("True")]
		[DebuggerNonUserCode]
		public bool ImportMergeDuplicates
		{
			get
			{
				return (bool)this["ImportMergeDuplicates"];
			}
			set
			{
				this["ImportMergeDuplicates"] = value;
			}
		}

		// Token: 0x17000028 RID: 40
		// (get) Token: 0x06000067 RID: 103 RVA: 0x0000467C File Offset: 0x0000287C
		// (set) Token: 0x06000068 RID: 104 RVA: 0x0000468E File Offset: 0x0000288E
		[DefaultSettingValue("True")]
		[UserScopedSetting]
		[DebuggerNonUserCode]
		public bool LoadTextures
		{
			get
			{
				return (bool)this["LoadTextures"];
			}
			set
			{
				this["LoadTextures"] = value;
			}
		}

		// Token: 0x1700002A RID: 42
		// (get) Token: 0x0600006B RID: 107 RVA: 0x000046C1 File Offset: 0x000028C1
		// (set) Token: 0x0600006C RID: 108 RVA: 0x000046D3 File Offset: 0x000028D3
		[DefaultSettingValue("-1")]
		[UserScopedSetting]
		[DebuggerNonUserCode]
		public int InspectorRecordedWidth
		{
			get
			{
				return (int)this["InspectorRecordedWidth"];
			}
			set
			{
				this["InspectorRecordedWidth"] = value;
			}
		}

		// Token: 0x1700002C RID: 44
		// (get) Token: 0x0600006F RID: 111 RVA: 0x0000470B File Offset: 0x0000290B
		// (set) Token: 0x06000070 RID: 112 RVA: 0x0000471D File Offset: 0x0000291D
		[DefaultSettingValue("30")]
		[UserScopedSetting]
		[DebuggerNonUserCode]
		public int Leap_SmoothingWindowSize
		{
			get
			{
				return (int)this["Leap_SmoothingWindowSize"];
			}
			set
			{
				this["Leap_SmoothingWindowSize"] = value;
			}
		}

		// Token: 0x1700002D RID: 45
		// (get) Token: 0x06000071 RID: 113 RVA: 0x00004730 File Offset: 0x00002930
		// (set) Token: 0x06000072 RID: 114 RVA: 0x00004742 File Offset: 0x00002942
		[DebuggerNonUserCode]
		[DefaultSettingValue("True")]
		[UserScopedSetting]
		public bool Leap_TranslationSmoothing
		{
			get
			{
				return (bool)this["Leap_TranslationSmoothing"];
			}
			set
			{
				this["Leap_TranslationSmoothing"] = value;
			}
		}

		// Token: 0x1700002E RID: 46
		// (get) Token: 0x06000073 RID: 115 RVA: 0x00004755 File Offset: 0x00002955
		// (set) Token: 0x06000074 RID: 116 RVA: 0x00004767 File Offset: 0x00002967
		[DefaultSettingValue("True")]
		[DebuggerNonUserCode]
		[UserScopedSetting]
		public bool Leap_RotationSmoothing
		{
			get
			{
				return (bool)this["Leap_RotationSmoothing"];
			}
			set
			{
				this["Leap_RotationSmoothing"] = value;
			}
		}

		// Token: 0x1700002F RID: 47
		// (get) Token: 0x06000075 RID: 117 RVA: 0x0000477A File Offset: 0x0000297A
		// (set) Token: 0x06000076 RID: 118 RVA: 0x0000478C File Offset: 0x0000298C
		[DebuggerNonUserCode]
		[DefaultSettingValue("0")]
		[UserScopedSetting]
		public int CountFilesOpened
		{
			get
			{
				return (int)this["CountFilesOpened"];
			}
			set
			{
				this["CountFilesOpened"] = value;
			}
		}

		// Token: 0x04000037 RID: 55
		private static CoreSettings defaultInstance = (CoreSettings)SettingsBase.Synchronized(new CoreSettings());
	}
}
