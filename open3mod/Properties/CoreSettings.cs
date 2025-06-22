using System.Configuration;
using System.Diagnostics;
using System.Collections.Specialized;

namespace open3mod.Properties
{


	// This class allows you to handle specific events on the settings class:
	//  The SettingChanging event is raised before a setting's value is changed.
	//  The PropertyChanged event is raised after a setting's value is changed.
	//  The SettingsLoaded event is raised after the setting values are loaded.
	//  The SettingsSaving event is raised before the setting values are saved.
	internal sealed partial class CoreSettings
	{

		public CoreSettings()
		{
			// // To add event handlers for saving and changing settings, uncomment the lines below:
			//
			// this.SettingChanging += this.SettingChangingEventHandler;
			//
			// this.SettingsSaving += this.SettingsSavingEventHandler;
			//
		}

		private void SettingChangingEventHandler(object sender, System.Configuration.SettingChangingEventArgs e)
		{
			// Add code to handle the SettingChangingEvent event here.
		}

		private void SettingsSavingEventHandler(object sender, System.ComponentModel.CancelEventArgs e)
		{
			// Add code to handle the SettingsSaving event here.
		}

		// Token: 0x1700000E RID: 14
		// (get) Token: 0x06000033 RID: 51 RVA: 0x000042BF File Offset: 0x000024BF
		// (set) Token: 0x06000034 RID: 52 RVA: 0x000042D1 File Offset: 0x000024D1
		[DebuggerNonUserCode]
		[UserScopedSetting]
		public StringCollection RecentFiles
		{
			get
			{
				if(this["RecentFiles"] != null)
					return (StringCollection)this["RecentFiles"];
				else
					return new StringCollection();
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
				if (this["AdditionalTextureFolders"] != null)
					return (StringCollection)this["AdditionalTextureFolders"];
				else
					return new StringCollection();
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
				if(this["ExitOnTabClosing"] != null)
					return (bool)this["ExitOnTabClosing"];
				else
					return false;
			}
			set
			{
				this["ExitOnTabClosing"] = value;
			}
		}
	}
}
