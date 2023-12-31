﻿using System.Drawing;
using Unbroken.LaunchBox.Plugins;



namespace MouseHideAndLock2
{
	public class MHLConfig : ISystemMenuItemPlugin
	{

		public string Caption
		{
			get
			{
				return "MouseHideAndLock2 Configuration";
			}
		}

		public System.Drawing.Image IconImage
		{
			get
			{
				return SystemIcons.Exclamation.ToBitmap();
			}
		}

		public bool ShowInLaunchBox
		{
			get
			{
				return true;
			}
		}


		public bool ShowInBigBox
		{
			get
			{
				return false;
			}
		}


		public bool AllowInBigBoxWhenLocked
		{
			get
			{
				return false;
			}
		}

		public void OnSelected()
		{
			var x = new Form_Config();
			x.ShowDialog();

		}
	}
}
