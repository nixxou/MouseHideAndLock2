using CursorAutoHider;
using Gma.System.MouseKeyHook;
using System;
using System.Drawing;
using System.Windows.Forms;

namespace MouseHideAndLock2
{
	public class MouseManager
	{

		public static MouseManager Instance { get; } = new MouseManager();
		public IKeyboardMouseEvents g;

		public Timer m_timer;
		public Timer m_checkProcessTimer = new Timer();
		public Point? m_lastMousePosition;
		public Point? m_lastMousePosition2;
		public DateTime? m_lastTime;
		public volatile bool m_mouseClicked;

		public int m_distanceThreshold = 5;
		public int m_timeThresholdS = 2;
		public string m_watchedApplication = "";

		static Rectangle workingRectangle = Screen.PrimaryScreen.Bounds;

		public bool lockMouseOnScreen = false;
		public bool activeMouseHide = false;

		public bool shift = false;

		public MouseManager()
		{

		}

		public void Reset()
		{
			if (m_checkProcessTimer != null)
				m_checkProcessTimer.Stop();
			if (m_timer != null)
				m_timer.Stop();

			m_timer = null;
			m_checkProcessTimer = new Timer();
			m_lastMousePosition = null;
			m_lastMousePosition2 = null;

			m_lastTime = null;
			m_mouseClicked = false;

			m_distanceThreshold = 5;
			m_timeThresholdS = 2;
			m_watchedApplication = "";

			workingRectangle = Screen.PrimaryScreen.Bounds;

			lockMouseOnScreen = false;
			activeMouseHide = false;

			shift = false;
		}

		public void Enable()
		{
			g = Hook.GlobalEvents();
			g.MouseDownExt += GlobalHookMouseDownExt;
			g.MouseMoveExt += G_MouseMoveExt;
			g.KeyDown += G_KeyDown;
			g.KeyUp += G_KeyUp;
		}

		public void Disable()
		{
			g.MouseDownExt -= GlobalHookMouseDownExt;
			g.MouseMoveExt -= G_MouseMoveExt;
			g.KeyDown -= G_KeyDown;
			g.KeyUp -= G_KeyUp;
		}

		public void TimerStart()
		{
			activeMouseHide = true;
			m_checkProcessTimer.Interval = 1000;
			m_checkProcessTimer.Tick += CheckProcessTimer_Tick;
			m_checkProcessTimer.Start();
		}

		public void TimerStop()
		{
			activeMouseHide = false;
			if (m_checkProcessTimer != null)
				m_checkProcessTimer.Stop();
			if (m_timer != null)
				m_timer.Stop();
		}

		private void CheckProcessTimer_Tick(object sender, EventArgs e)
		{
			if (activeMouseHide)
			{
				if (m_timer == null)
				{
					m_lastTime = null;
					m_lastMousePosition = null;

					m_timer = new Timer();
					m_timer.Interval = 300;
					m_timer.Tick += Timer_Tick;
					m_timer.Start();
				}
			}
			else if (m_timer != null)
			{
				m_timer.Stop();
				m_timer = null;
				CursorsManager.Instance.RestoreCursors();

				m_lastTime = null;
				m_lastMousePosition = null;
			}
		}

		private void Timer_Tick(object sender, EventArgs e)
		{
			var mousePosition = System.Windows.Forms.Cursor.Position;
			var now = DateTime.Now;

			if (m_lastMousePosition.HasValue)
			{
				double xdiff = (mousePosition.X - m_lastMousePosition.Value.X);
				double ydiff = (mousePosition.Y - m_lastMousePosition.Value.Y);
				double distSqr = xdiff * xdiff + ydiff * ydiff;

				if ((now - m_lastTime.Value).TotalSeconds > m_timeThresholdS)
				{
					if (!m_mouseClicked && distSqr < m_distanceThreshold)
						CursorsManager.Instance.HideCursors();
					else
						CursorsManager.Instance.RestoreCursors();

					m_mouseClicked = false;
					m_lastTime = null;
					m_lastMousePosition = null;
				}
				else if (distSqr > m_distanceThreshold)
					CursorsManager.Instance.RestoreCursors();
			}
			else
			{
				m_lastTime = now;
				m_lastMousePosition = mousePosition;
			}
		}

		public void GlobalHookMouseDownExt(object sender, MouseEventExtArgs e)
		{
			//m_logControl.Items.Add("Mouse clicked: " + e.Clicked);
			m_mouseClicked |= e.Clicked;
		}

		private void G_MouseMoveExt(object sender, MouseEventExtArgs e)
		{
			if (lockMouseOnScreen)
			{
				Point ModdedCursorPos = Cursor.Position;
				if (e.Y >= -40 && e.Y < 0) ModdedCursorPos.Y = 0;
				if (e.X >= -40 && e.X < 0) ModdedCursorPos.X = 0;
				if (e.X >= workingRectangle.Width && e.X <= workingRectangle.Width + 40) ModdedCursorPos.X = workingRectangle.Width - 2;
				if (e.Y >= workingRectangle.Height && e.Y <= workingRectangle.Height + 40) ModdedCursorPos.Y = workingRectangle.Height - 2;

				Screen s = Screen.FromPoint(ModdedCursorPos);
				if (s.Primary)
				{
					//  this.Text = e.X + ":" + e.Y + ":" + e.Location;
					if (e.Y < 0 && !shift && !Control.IsKeyLocked(Keys.Scroll))
					{
						Cursor.Position = new Point(e.X, 0);
						e.Handled = true;
					}

					if (e.X < 0 && !shift && !Control.IsKeyLocked(Keys.Scroll))
					{
						Cursor.Position = new Point(0, e.Y);
						e.Handled = true;
					}

					if (e.X >= workingRectangle.Width && !shift && !Control.IsKeyLocked(Keys.Scroll))
					{
						Cursor.Position = new Point(workingRectangle.Width - 5, e.Y);
						e.Handled = true;
					}

					if (e.Y >= workingRectangle.Height && !shift && !Control.IsKeyLocked(Keys.Scroll))
					{
						Cursor.Position = new Point(e.X, workingRectangle.Height - 5);
						e.Handled = true;
					}
				}
				else
				{
					if(m_lastMousePosition2 != null)
					{
						Point LastPos = (Point)m_lastMousePosition2;
						Screen lastscreen = Screen.FromPoint(LastPos);
						if (lastscreen.Primary)
						{
							if (e.Y < 0 && !shift && !Control.IsKeyLocked(Keys.Scroll))
							{
								Cursor.Position = new Point(e.X, 0);
								e.Handled = true;
							}

							if (e.X < 0 && !shift && !Control.IsKeyLocked(Keys.Scroll))
							{
								Cursor.Position = new Point(0, e.Y);
								e.Handled = true;
							}

							if (e.X >= workingRectangle.Width && !shift && !Control.IsKeyLocked(Keys.Scroll))
							{
								Cursor.Position = new Point(workingRectangle.Width - 5, e.Y);
								e.Handled = true;
							}

							if (e.Y >= workingRectangle.Height && !shift && !Control.IsKeyLocked(Keys.Scroll))
							{
								Cursor.Position = new Point(e.X, workingRectangle.Height - 5);
								e.Handled = true;
							}

						}

					}
				}
				m_lastMousePosition2 = System.Windows.Forms.Cursor.Position;
			}


		}

		private void G_KeyDown(object sender, KeyEventArgs e)
		{
			if (e.KeyCode == Keys.LShiftKey || e.KeyCode == Keys.RShiftKey)
			{
				shift = true;
			}
		}

		private void G_KeyUp(object sender, KeyEventArgs e)
		{

			if (e.KeyCode == Keys.LShiftKey || e.KeyCode == Keys.RShiftKey)
			{
				shift = false;
			}
		}


		public static Point GoBack(Point cursor)
		{
			if (cursor.X <= 3) cursor.X = 5;
			if (cursor.Y <= 3) cursor.Y = 5;
			if (cursor.X >= workingRectangle.Width - 3) cursor.X = workingRectangle.Width - 5;
			if (cursor.Y >= workingRectangle.Height - 3) cursor.Y = workingRectangle.Height - 5;


			return cursor;
		}


	}
}
