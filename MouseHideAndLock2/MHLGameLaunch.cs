using CursorAutoHider;
using System.Windows.Forms;
using Unbroken.LaunchBox.Plugins;
using Unbroken.LaunchBox.Plugins.Data;

namespace MouseHideAndLock2
{

	public class MHLGameLaunch : IGameLaunchingPlugin
	{
		public static bool isActiveLock = false;
		public void OnAfterGameLaunched(IGame game, IAdditionalApplication app, IEmulator emulator)
		{


		}


		public void OnBeforeGameLaunching(IGame game, IAdditionalApplication app, IEmulator emulator)
		{
			if (!isActiveLock)
			{
				isActiveLock = true;
				MouseManager.Instance.Reset();

				if (!Config.emulatorListInitialized) Config.LoadListFromIniFile();
				string plateform = game.Platform;
				string emul = emulator.Title;
				string key = plateform + " : " + emul;

				bool ShowOnMove = true;
				bool LockCursor = false;
				bool HideCursor = false;

				if (Config.emulatorsWithHideCursor.ContainsKey(key)) HideCursor = Config.emulatorsWithHideCursor[key];
				if (Config.emulatorsWithLockCursor.ContainsKey(key)) LockCursor = Config.emulatorsWithLockCursor[key];
				if (Config.emulatorsWithShowCursorOnMove.ContainsKey(key)) ShowOnMove = Config.emulatorsWithShowCursorOnMove[key];


				if (HideCursor)
				{
					CursorsManager.Instance.HideCursors();
					if (ShowOnMove)
					{
						MouseManager.Instance.TimerStart();
					}
				}
				if (LockCursor)
				{
					Screen s = Screen.FromPoint(Cursor.Position);
					if (s.Primary == false)
					{
						Cursor.Position = MouseManager.GoBack(Cursor.Position);
					}
					MouseManager.Instance.lockMouseOnScreen = true;
				}
			}

		}

		public void OnGameExited()
		{
			if (isActiveLock)
			{
				MouseManager.Instance.lockMouseOnScreen = false;
				MouseManager.Instance.TimerStop();
				CursorsManager.Instance.RestoreCursors();

				isActiveLock = false;

			}

		}
	}
}
