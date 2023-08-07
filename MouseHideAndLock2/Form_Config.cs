using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using Unbroken.LaunchBox.Plugins.Data;
using Unbroken.LaunchBox.Plugins;
using System.Reflection.Emit;

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

			Config.LoadListFromIniFile();

			dataGridView1.Rows.Clear();
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

		private void buttonSave_Click(object sender, EventArgs e)
		{
			Config.SaveListBoxContentsToIniFile(dataGridView1);
			Close();
		}
	}
}
