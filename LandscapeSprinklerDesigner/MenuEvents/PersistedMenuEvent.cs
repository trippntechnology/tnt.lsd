using System.Drawing;
using TNT.Utilities;

namespace LandscapeSprinklerDesigner.MenuEvents
{
	abstract class PersistedMenuEvent : MenuEvent
	{
		public PersistedMenuEvent(Image image = null) : base(image)
		{
		}

		virtual public void RestoreState(ApplicationRegistry applicationRegistry) { }
		virtual public void SaveState(ApplicationRegistry applicationRegistry) { }
	}
}
