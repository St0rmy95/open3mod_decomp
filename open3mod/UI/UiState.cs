using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;

namespace open3mod
{
	// Token: 0x02000074 RID: 116
	public sealed class UiState : IDisposable
	{
		// Token: 0x170000A7 RID: 167
		// (get) Token: 0x060003BF RID: 959 RVA: 0x000201DF File Offset: 0x0001E3DF
		// (set) Token: 0x060003C0 RID: 960 RVA: 0x000201EB File Offset: 0x0001E3EB
		public bool RenderWireframe
		{
			get
			{
				return Properties.CoreSettings.Default.RenderWireframe;
			}
			set
			{
				Properties.CoreSettings.Default.RenderWireframe = value;
			}
		}

		// Token: 0x170000A8 RID: 168
		// (get) Token: 0x060003C1 RID: 961 RVA: 0x000201F8 File Offset: 0x0001E3F8
		// (set) Token: 0x060003C2 RID: 962 RVA: 0x00020204 File Offset: 0x0001E404
		public bool RenderTextured
		{
			get
			{
				return Properties.CoreSettings.Default.RenderTextured;
			}
			set
			{
				Properties.CoreSettings.Default.RenderTextured = value;
			}
		}

		// Token: 0x170000A9 RID: 169
		// (get) Token: 0x060003C3 RID: 963 RVA: 0x00020211 File Offset: 0x0001E411
		// (set) Token: 0x060003C4 RID: 964 RVA: 0x0002021D File Offset: 0x0001E41D
		public bool RenderLit
		{
			get
			{
				return Properties.CoreSettings.Default.RenderLit;
			}
			set
			{
				Properties.CoreSettings.Default.RenderLit = value;
			}
		}

		// Token: 0x170000AA RID: 170
		// (get) Token: 0x060003C5 RID: 965 RVA: 0x0002022A File Offset: 0x0001E42A
		// (set) Token: 0x060003C6 RID: 966 RVA: 0x00020236 File Offset: 0x0001E436
		public bool ShowFps
		{
			get
			{
				return Properties.CoreSettings.Default.ShowFps;
			}
			set
			{
				Properties.CoreSettings.Default.ShowFps = value;
			}
		}

		// Token: 0x170000AB RID: 171
		// (get) Token: 0x060003C7 RID: 967 RVA: 0x00020243 File Offset: 0x0001E443
		// (set) Token: 0x060003C8 RID: 968 RVA: 0x0002024F File Offset: 0x0001E44F
		public bool ShowBBs
		{
			get
			{
				return Properties.CoreSettings.Default.ShowBBs;
			}
			set
			{
				Properties.CoreSettings.Default.ShowBBs = value;
			}
		}

		// Token: 0x170000AC RID: 172
		// (get) Token: 0x060003C9 RID: 969 RVA: 0x0002025C File Offset: 0x0001E45C
		// (set) Token: 0x060003CA RID: 970 RVA: 0x00020268 File Offset: 0x0001E468
		public bool ShowNormals
		{
			get
			{
				return Properties.CoreSettings.Default.ShowNormals;
			}
			set
			{
				Properties.CoreSettings.Default.ShowNormals = value;
			}
		}

		// Token: 0x170000AD RID: 173
		// (get) Token: 0x060003CB RID: 971 RVA: 0x00020275 File Offset: 0x0001E475
		// (set) Token: 0x060003CC RID: 972 RVA: 0x00020281 File Offset: 0x0001E481
		public bool ShowSkeleton
		{
			get
			{
				return Properties.CoreSettings.Default.ShowSkeleton;
			}
			set
			{
				Properties.CoreSettings.Default.ShowSkeleton = value;
			}
		}

		// Token: 0x060003CD RID: 973 RVA: 0x00020290 File Offset: 0x0001E490
		public UiState(Tab defaultTab)
		{
			this.DefaultFont10 = new Font("Segoe UI", 8f);
			this.DefaultFont12 = new Font("Segoe UI", 12f);
			this.DefaultFont16 = new Font("Segoe UI", 18f);
			this.Tabs = new List<Tab>
			{
				defaultTab
			};
			this.ActiveTab = defaultTab;
		}

		// Token: 0x170000AE RID: 174
		// (get) Token: 0x060003CE RID: 974 RVA: 0x000202FD File Offset: 0x0001E4FD
		// (set) Token: 0x060003CF RID: 975 RVA: 0x00020305 File Offset: 0x0001E505
		public Tab ActiveTab { get; private set; }

		// Token: 0x060003D0 RID: 976 RVA: 0x0002031C File Offset: 0x0001E51C
		public IEnumerable<Tab> TabsWithActiveScenes()
		{
			return from tab in this.Tabs
			where tab.ActiveScene != null
			select tab;
		}

		// Token: 0x060003D1 RID: 977 RVA: 0x0002034E File Offset: 0x0001E54E
		public IEnumerable<Scene> ActiveScenes()
		{
			return from tab in this.TabsWithActiveScenes()
			select tab.ActiveScene;
		}

		// Token: 0x060003D2 RID: 978 RVA: 0x00020390 File Offset: 0x0001E590
		public Tab TabForId(object id)
		{
			return this.Tabs.FirstOrDefault((Tab ts) => ts.Id == id);
		}

		// Token: 0x060003D3 RID: 979 RVA: 0x000203DC File Offset: 0x0001E5DC
		public void SelectTab(object id)
		{
			using (IEnumerator<Tab> enumerator = (from ts in this.Tabs
			where ts.Id == id
			select ts).GetEnumerator())
			{
				if (enumerator.MoveNext())
				{
					Tab activeTab = enumerator.Current;
					this.ActiveTab = activeTab;
				}
			}
		}

		// Token: 0x060003D4 RID: 980 RVA: 0x0002044C File Offset: 0x0001E64C
		public void SelectTab(Tab tab)
		{
			this.SelectTab(tab.Id);
		}

		// Token: 0x060003D5 RID: 981 RVA: 0x0002045C File Offset: 0x0001E65C
		public void RemoveTab(object id)
		{
			foreach (Tab tab in this.Tabs)
			{
				if (tab.Id == id)
				{
					this.Tabs.Remove(tab);
					tab.Dispose();
					break;
				}
			}
		}

		// Token: 0x060003D6 RID: 982 RVA: 0x000204C8 File Offset: 0x0001E6C8
		public void RemoveTab(Tab tab)
		{
			this.RemoveTab(tab.Id);
		}

		// Token: 0x060003D7 RID: 983 RVA: 0x000204D6 File Offset: 0x0001E6D6
		public void AddTab(Tab tab)
		{
			this.Tabs.Add(tab);
		}

		// Token: 0x060003D8 RID: 984 RVA: 0x000204E4 File Offset: 0x0001E6E4
		public void Dispose()
		{
			foreach (Tab tab in this.Tabs)
			{
				tab.Dispose();
			}
		}

		// Token: 0x0400031A RID: 794
		public readonly Font DefaultFont12;

		// Token: 0x0400031B RID: 795
		public readonly Font DefaultFont16;

		// Token: 0x0400031C RID: 796
		public readonly Font DefaultFont10;

		// Token: 0x0400031D RID: 797
		public readonly List<Tab> Tabs;
	}
}
