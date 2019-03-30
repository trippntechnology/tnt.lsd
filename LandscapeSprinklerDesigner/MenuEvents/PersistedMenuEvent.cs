using LandscapeSprinklerDesigner.Properties;
using Microsoft.Win32;
using System.Drawing;
using TNT.Utilities;

namespace LandscapeSprinklerDesigner.MenuEvents
{
	abstract class PersistedMenuEvent : MenuEvent
	{
		//private ApplicationRegistry AppRegistry = new ApplicationRegistry(Registry.CurrentUser, Resources.Company, Resources.Application);

		public PersistedMenuEvent(Image image = null) : base(image)
		{
			//RestoreState(AppRegistry);
		}

		~PersistedMenuEvent()
		{
			//SaveState(AppRegistry);
		}

		virtual public void RestoreState(ApplicationRegistry applicationRegistry) { }
		virtual public void SaveState(ApplicationRegistry applicationRegistry) { }
	}
}
