using TNT.LSD.Components;
using TNT.Utilities;

namespace LandscapeSprinklerDesigner.MenuEvents;

abstract class PersistedMenuEvent(string text, string? toolTipText = null, bool checkOnClick = false, Image? image = null) : MenuEvent(text, toolTipText, checkOnClick, image)
{
    virtual public void RestoreState(ApplicationRegistry applicationRegistry, TNTCAD cad) { }
    virtual public void SaveState(ApplicationRegistry applicationRegistry) { }
}
