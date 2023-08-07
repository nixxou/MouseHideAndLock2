using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Windows.Forms;
using Unbroken.LaunchBox.Plugins;
using Unbroken.LaunchBox.Plugins.Data;

namespace MouseHideAndLock2
{
	public partial class Form_Config : Form
	{
		public Form_Config()
		{
			InitializeComponent();
		}

		private void Form_Config_Load(object sender, EventArgs e)
		{
			dataGridView1.Rows.Clear();
			dataGridView1.Columns.Cast<DataGridViewColumn>().ToList().ForEach(f => f.SortMode = DataGridViewColumnSortMode.NotSortable);
			dataGridView1.RowHeadersVisible = false;
			dataGridView1.AllowUserToAddRows = false;
			dataGridView1.AllowDrop = false;
			dataGridView1.AllowUserToDeleteRows = false;
			dataGridView1.AllowUserToResizeRows = false;

			dataGridView1.CellContentClick += DataGridView1_CellContentClick;

			Config.LoadListFromIniFile();




			{
				var ligne = new DataGridViewRow();
				var gridEmulatorName = new DataGridViewTextBoxCell();
				var gridHideCursor = new DataGridViewButtonCell();
				var gridShowCursorOnMove = new DataGridViewButtonCell();
				var gridLockCursor = new DataGridViewButtonCell();
				gridEmulatorName.Value = "";
				gridHideCursor.Value = "Check-All";
				gridShowCursorOnMove.Value = "Check-All";
				gridLockCursor.Value = "Check-All";
				ligne.Cells.Add(gridEmulatorName);
				ligne.Cells.Add(gridHideCursor);
				ligne.Cells.Add(gridShowCursorOnMove);
				ligne.Cells.Add(gridLockCursor);
				dataGridView1.Rows.Add(ligne);

			}
			//var listeEmulateurs = PluginHelper.DataManager.GetAllEmulators().Select(emu => emu.Title).ToArray();
			var listePlateform = PluginHelper.DataManager.GetAllPlatforms().Select(plat => plat.Name).ToArray();


			List<(IEmulator, IEmulatorPlatform)> emulators = new List<(IEmulator, IEmulatorPlatform)>();

			var listeCle = new List<string>();
			foreach (var emulator in PluginHelper.DataManager.GetAllEmulators())
			{
				foreach (var emulatorPlatform in emulator.GetAllEmulatorPlatforms())
				{
					if (listePlateform.Contains(emulatorPlatform.Platform))
					{
						string cle = emulatorPlatform.Platform + " : " + emulator.Title;
						listeCle.Add(cle);
					}
				}
			}
			listeCle.Sort();


			foreach (var cle in listeCle)
			{
				var ligne = new DataGridViewRow();
				var gridEmulatorName = new DataGridViewTextBoxCell();
				var gridHideCursor = new DataGridViewCheckBoxCell();
				var gridShowCursorOnMove = new DataGridViewCheckBoxCell();
				var gridLockCursor = new DataGridViewCheckBoxCell();

				gridEmulatorName.Value = cle;

				gridHideCursor.Value = false;
				if (Config.emulatorsWithHideCursor.ContainsKey(cle))
				{
					gridHideCursor.Value = Config.emulatorsWithHideCursor[cle];
				}

				gridShowCursorOnMove.Value = false;
				if (Config.emulatorsWithShowCursorOnMove.ContainsKey(cle))
				{
					gridShowCursorOnMove.Value = Config.emulatorsWithShowCursorOnMove[cle];
				}

				gridLockCursor.Value = false;
				if (Config.emulatorsWithLockCursor.ContainsKey(cle))
				{
					gridLockCursor.Value = Config.emulatorsWithLockCursor[cle];
				}

				ligne.Cells.Add(gridEmulatorName);
				ligne.Cells.Add(gridHideCursor);
				ligne.Cells.Add(gridShowCursorOnMove);
				ligne.Cells.Add(gridLockCursor);

				dataGridView1.Rows.Add(ligne);
			}

		}

		private void DataGridView1_CellContentClick(object sender, DataGridViewCellEventArgs e)
		{
			if (e.RowIndex == 0)
			{
				if (e.ColumnIndex >= 1 && e.ColumnIndex <= 3)
				{
					bool newValue = false;
					var baseButton = dataGridView1.Rows[0].Cells[e.ColumnIndex];
					if (baseButton.Value.ToString() == "Check-All")
					{
						newValue = true;
						baseButton.Value = "Uncheck-All";
					}
					else
					{
						newValue = false;
						baseButton.Value = "Check-All";
					}

					foreach (DataGridViewRow row in dataGridView1.Rows)
					{
						if (row.Cells[0].Value != null && row.Cells[0].Value.ToString() != "")
						{
							row.Cells[e.ColumnIndex].Value = newValue;
						}
					}
				}
			}

		}

		private void buttonSave_Click(object sender, EventArgs e)
		{
			Config.SaveListBoxContentsToIniFile(dataGridView1);
			Close();
		}
	}
}
