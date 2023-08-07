//using Gma.System.MouseKeyHook;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Unbroken.LaunchBox.Plugins;

namespace MouseHideAndLock2
{
	internal class MHLSystemEvents : ISystemEventsPlugin
	{
		public void OnEventRaised(string eventType)
		{
			if (eventType == "LaunchBoxStartupCompleted")
			{
				MouseManager.Instance.Enable();
			}
			if (eventType == "LaunchBoxShutdownBeginning")
			{
				MouseManager.Instance.Disable();
			}


		}
	}
}
