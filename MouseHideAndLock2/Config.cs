using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Windows.Forms;
using Unbroken.LaunchBox.Plugins;
using Unbroken.LaunchBox.Plugins.Data;

namespace MouseHideAndLock2
{
	public class Config
	{
		//public static Dictionary<string, bool> emulatorsWithMouseHideActivated = new Dictionary<string, bool>();

		public static Dictionary<string, bool> emulatorsWithHideCursor = new Dictionary<string, bool>();
		public static Dictionary<string, bool> emulatorsWithShowCursorOnMove = new Dictionary<string, bool>();
		public static Dictionary<string, bool> emulatorsWithLockCursor = new Dictionary<string, bool>();

		public static bool emulatorListInitialized = false;
		private static string _pluginPath = "";

		public static string GetPluginPath()
		{
			if (_pluginPath != "") return _pluginPath;
			string assemblyPath = Assembly.GetEntryAssembly().Location;
			string assemblyDirectory = Path.GetDirectoryName(assemblyPath);

			string launchBoxRootPath = Path.GetFullPath(Path.Combine(assemblyDirectory, @".."));
			string relativePluginPath = @"Plugins\MouseHideAndLock2";
			_pluginPath = Path.Combine(launchBoxRootPath, relativePluginPath);
			return _pluginPath;
		}


		public static string GetConfigFile()
		{
			string assemblyPath = Assembly.GetEntryAssembly().Location;
			string assemblyFileName = Path.GetFileName(assemblyPath);
			string assemblyDirectory = Path.GetDirectoryName(assemblyPath);

			string launchBoxRootPath = Path.GetFullPath(Path.Combine(assemblyDirectory, @".."));
			string relativePluginPath = @"Plugins\MouseHideAndLock2";

			string iniFilePath = Path.Combine(launchBoxRootPath, relativePluginPath, "config_mhl.ini");
			return iniFilePath;
		}





		public static void LoadListFromIniFile()
		{
			emulatorsWithHideCursor.Clear();
			emulatorsWithLockCursor.Clear();
			emulatorsWithShowCursorOnMove.Clear();
			IniFile ini = new IniFile(GetConfigFile());
			var listePlateform = PluginHelper.DataManager.GetAllPlatforms().Select(plat => plat.Name).ToArray();
			List<(IEmulator, IEmulatorPlatform)> emulators = new List<(IEmulator, IEmulatorPlatform)>();

			foreach (var emulator in PluginHelper.DataManager.GetAllEmulators())
			{
				foreach (var emulatorPlatform in emulator.GetAllEmulatorPlatforms())
				{
					if (listePlateform.Contains(emulatorPlatform.Platform))
					{
						string key = emulatorPlatform.Platform + " : " + emulator.Title;
						if (key == "Nintendo Wii U : Cemu")
						{

						}
						bool val = bool.Parse(ini.Read("HideCursor", key, "False"));
						emulatorsWithHideCursor[key] = val;

						val = bool.Parse(ini.Read("LockCursor", key, "False"));
						emulatorsWithLockCursor[key] = val;

						val = bool.Parse(ini.Read("ShowCursorOnMove", key, "False"));
						emulatorsWithShowCursorOnMove[key] = val;

					}
				}
			}
			emulatorListInitialized = true;
		}

		public static void SaveListBoxContentsToIniFile(DataGridView dataGridView1)
		{

			Dictionary<string, bool> listeHideCursor = new Dictionary<string, bool>();
			Dictionary<string, bool> listeShowCursorOnMove = new Dictionary<string, bool>();
			Dictionary<string, bool> listeLockCursor = new Dictionary<string, bool>();
			IniFile ini = new IniFile(GetConfigFile());

			foreach (DataGridViewRow row in dataGridView1.Rows)
			{
				if (row.Cells[0].Value == null || String.IsNullOrEmpty(row.Cells[0].Value.ToString())) continue;
				string emulatorName = "";
				bool hideCursor = false;
				bool showCursorOnMove = false;
				bool lockCursor = false;

				emulatorName = row.Cells[0].Value.ToString();
				if (emulatorName != "")
				{
					hideCursor = (bool)row.Cells[1].Value;
					showCursorOnMove = (bool)row.Cells[2].Value;
					lockCursor = (bool)row.Cells[3].Value;
					listeHideCursor.Add(emulatorName, hideCursor);
					listeShowCursorOnMove.Add(emulatorName, showCursorOnMove);
					listeLockCursor.Add(emulatorName, lockCursor);


					ini.Write("HideCursor", emulatorName, hideCursor.ToString());
					ini.Write("LockCursor", emulatorName, showCursorOnMove.ToString());
					ini.Write("ShowCursorOnMove", emulatorName, lockCursor.ToString());


				}
			}
			Config.emulatorsWithHideCursor = listeHideCursor;
			Config.emulatorsWithShowCursorOnMove = listeShowCursorOnMove;
			Config.emulatorsWithLockCursor = listeLockCursor;

		}
	}
}
