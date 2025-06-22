using System;
using System.ComponentModel;
using System.Drawing;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using open3mod.Properties;

namespace open3mod
{
	// Token: 0x0200002B RID: 43
	public partial class LogViewer : Form
	{
		// Token: 0x17000056 RID: 86
		// (get) Token: 0x06000169 RID: 361 RVA: 0x0000C2A3 File Offset: 0x0000A4A3
		public MainWindow MainWindow
		{
			get
			{
				return this._mainWindow;
			}
		}

		// Token: 0x0600016A RID: 362 RVA: 0x0000C2BC File Offset: 0x0000A4BC
		public LogViewer(MainWindow mainWindow)
		{
			this._mainWindow = mainWindow;
			this.InitializeComponent();
			this._mainWindow.TabChanged += delegate(Tab tab, bool add)
			{
				if (base.IsDisposed)
				{
					return;
				}
				this.PopulateList();
			};
			this.PopulateList();
		}

		// Token: 0x0600016B RID: 363 RVA: 0x0000C300 File Offset: 0x0000A500
		private void PopulateList()
		{
			this.comboBoxSource.Items.Clear();
			int num = -1;
			foreach (Tab tab in this.MainWindow.UiState.Tabs)
			{
				if (tab.File != null)
				{
					int num2 = this.comboBoxSource.Items.Add(tab.File);
					if (tab == this.MainWindow.UiState.ActiveTab)
					{
						num = num2;
					}
				}
			}
			if (num != -1)
			{
				this.comboBoxSource.SelectedItem = this.comboBoxSource.Items[num];
			}
			else
			{
				this.comboBoxSource.SelectedItem = ((this.comboBoxSource.Items.Count > 0) ? this.comboBoxSource.Items[0] : null);
			}
			this.FetchLogEntriesFromScene();
		}

		// Token: 0x0600016C RID: 364 RVA: 0x0000C41C File Offset: 0x0000A61C
		private void FetchLogEntriesFromScene()
		{
			string sceneName = (string)this.comboBoxSource.SelectedItem;
			Scene scene = (from tab in this.MainWindow.UiState.Tabs
			where tab.File == sceneName
			select tab.ActiveScene).FirstOrDefault<Scene>();
			if (scene == null)
			{
				this.richTextBox.Text = Resources.LogViewer_FetchLogEntriesFromScene_No_scene_loaded;
				return;
			}
			this._currentLogStore = scene.LogStore;
			this.BuildRtf();
		}

		// Token: 0x0600016D RID: 365 RVA: 0x0000C4B4 File Offset: 0x0000A6B4
		private void BuildRtf()
		{
			StringBuilder stringBuilder = new StringBuilder();
			stringBuilder.Append("{\\rtf1\\ansi\\ansicpg1252\\deff0\\deflang1033{\\fonttbl{\\f0\\fnil\\fcharset0 Consolas;}}{\\colortbl;\\red255\\green0\\blue0;\\red255\\green120\\blue0;\\red0\\green150\\blue0;\\red0\\green0\\blue180;\\red0\\green0\\blue0;}");
			foreach (LogStore.Entry entry in this._currentLogStore.Messages)
			{
				string text;
				switch (entry.Cat)
				{
				case LogStore.Category.Info:
					if (!this.checkBoxFilterInformation.Checked)
					{
						continue;
					}
					text = "\\pard \\cf3 \\b \\fs18 ";
					break;
				case LogStore.Category.Warn:
					if (!this.checkBoxFilterWarning.Checked)
					{
						continue;
					}
					text = "\\pard \\cf2 \\b \\fs18 ";
					break;
				case LogStore.Category.Error:
					if (!this.checkBoxFilterError.Checked)
					{
						continue;
					}
					text = "\\pard \\cf1 \\b \\fs18 ";
					break;
				case LogStore.Category.Debug:
					if (!this.checkBoxFilterVerbose.Checked)
					{
						continue;
					}
					text = "\\pard \\cf4 \\b \\fs18 ";
					break;
				case LogStore.Category.System:
					text = "\\pard \\cf5 \\b \\fs18 ";
					break;
				default:
					throw new ArgumentOutOfRangeException();
				}
				string[] array = new string[6];
				array[0] = text;
				array[1] = "job: ";
				string[] array2 = array;
				int num = 2;
				int threadId = entry.ThreadId;
				array2[num] = threadId.ToString(CultureInfo.InvariantCulture).PadLeft(5, ' ');
				array[3] = ",\t time: ";
				string[] array3 = array;
				int num2 = 4;
				long time = entry.Time;
				array3[num2] = time.ToString(CultureInfo.InvariantCulture).PadLeft(10, ' ');
				array[5] = ",\t";
				text = string.Concat(array);
				stringBuilder.Append(text);
				foreach (char c in entry.Message)
				{
					if (c != '\n' && c != '\r')
					{
						if (c == '\\' || c == '}' || c == '{')
						{
							stringBuilder.Append('\\');
						}
						stringBuilder.Append(c);
					}
				}
				stringBuilder.Append("\\par ");
			}
			stringBuilder.Append('}');
			string rtf = stringBuilder.ToString();
			this.richTextBox.Rtf = rtf;
		}

		// Token: 0x0600016E RID: 366 RVA: 0x0000C6B8 File Offset: 0x0000A8B8
		private void OnClearAll(object sender, EventArgs e)
		{
			this._currentLogStore.Drop();
			this.richTextBox.Text = Resources.LogViewer_OnClearAll_Nothing_to_display;
		}

		// Token: 0x0600016F RID: 367 RVA: 0x0000C6D8 File Offset: 0x0000A8D8
		private void OnSave(object sender, EventArgs e)
		{
			if (this.saveFileDialog.ShowDialog() != DialogResult.OK)
			{
				return;
			}
			using (StreamWriter streamWriter = new StreamWriter(this.saveFileDialog.OpenFile()))
			{
				foreach (LogStore.Entry entry in this._currentLogStore.Messages)
				{
					streamWriter.Write(this.LogEntryToPlainText(entry) + "\r\n");
				}
			}
		}

		// Token: 0x06000170 RID: 368 RVA: 0x0000C778 File Offset: 0x0000A978
		private string LogEntryToPlainText(LogStore.Entry entry)
		{
			string text;
			switch (entry.Cat)
			{
			case LogStore.Category.Info:
				text = "Info:   ";
				break;
			case LogStore.Category.Warn:
				text = "Warn:   ";
				break;
			case LogStore.Category.Error:
				text = "Error:  ";
				break;
			case LogStore.Category.Debug:
				text = "Debug:  ";
				break;
			case LogStore.Category.System:
				text = "System: ";
				break;
			default:
				throw new ArgumentOutOfRangeException();
			}
			return string.Concat(new string[]
			{
				entry.ThreadId.ToString(CultureInfo.InvariantCulture).PadLeft(4),
				"|",
				entry.Time.ToString(CultureInfo.InvariantCulture).PadLeft(10, '0'),
				"   ",
				text,
				entry.Message
			});
		}

		// Token: 0x06000171 RID: 369 RVA: 0x0000C839 File Offset: 0x0000AA39
		private void OnFilterChange(object sender, EventArgs e)
		{
			this.BuildRtf();
		}

		// Token: 0x06000172 RID: 370 RVA: 0x0000C841 File Offset: 0x0000AA41
		private void filterToolStripMenuItem_Click(object sender, EventArgs e)
		{
		}

		// Token: 0x06000173 RID: 371 RVA: 0x0000C843 File Offset: 0x0000AA43
		private void label1_Click(object sender, EventArgs e)
		{
		}

		// Token: 0x06000174 RID: 372 RVA: 0x0000C845 File Offset: 0x0000AA45
		private void ChangeLogSource(object sender, EventArgs e)
		{
			this.FetchLogEntriesFromScene();
		}

		// Token: 0x0400010D RID: 269
		private const string RtfHeader = "{\\rtf1\\ansi\\ansicpg1252\\deff0\\deflang1033{\\fonttbl{\\f0\\fnil\\fcharset0 Consolas;}}{\\colortbl;\\red255\\green0\\blue0;\\red255\\green120\\blue0;\\red0\\green150\\blue0;\\red0\\green0\\blue180;\\red0\\green0\\blue0;}";

		// Token: 0x0400010E RID: 270
		private readonly MainWindow _mainWindow;

		// Token: 0x0400010F RID: 271
		private LogStore _currentLogStore;
	}
}
