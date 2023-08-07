using Microsoft.SqlServer.Server;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using Unbroken.LaunchBox.Plugins.Data;
using Unbroken.LaunchBox.Plugins;
using CursorAutoHider;
using System.Windows.Forms;
using System.Drawing;
using System.Timers;

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

		private void Timer_Tick(object sender, EventArgs e)
		{
			/*
			var mousePosition = System.Windows.Forms.Cursor.Position;
			var now = DateTime.Now;
			var m_lastMousePosition = MouseManager.Instance.m_lastMousePosition;
			var m_lastTime = MouseManager.Instance.m_lastTime;
			var m_timeThresholdS = MouseManager.Instance.m_timeThresholdS;

			if (m_lastMousePosition.HasValue)
			{
				double xdiff = (mousePosition.X - m_lastMousePosition.Value.X);
				double ydiff = (mousePosition.Y - m_lastMousePosition.Value.Y);
				double distSqr = xdiff * xdiff + ydiff * ydiff;

				if ((now - m_lastTime.Value).TotalSeconds > m_timeThresholdS)
				{
					if (!m_mouseClicked && distSqr < m_distanceThreshold)
						HideMouse();
					else
						ShowMouse();

					m_mouseClicked = false;
					m_lastTime = null;
					m_lastMousePosition = null;
				}
				else if (distSqr > m_distanceThreshold)
					ShowMouse();
			}
			else
			{
				m_lastTime = now;
				m_lastMousePosition = mousePosition;
			}
			*/
		}





	}
}
