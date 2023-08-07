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
