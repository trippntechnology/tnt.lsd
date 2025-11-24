using TNT.LSD.Components;
using TNT.LSD.Objects;

namespace LandscapeSprinklerDesigner.MenuEvents;

abstract class RotateMenuEvent(string text, string? toolTipText, Image? image) : MenuEvent(text, toolTipText, image: image)
{
    public abstract int Angle { get; }

    public override void OnApplicationIdle(TNTCAD cad)
    {
        var paletteParts = cad.SelectedObjects.OfType<PalettePart>().ToList();
        Enabled = cad.SelectedObjects.Count != 0 && cad.SelectedObjects.Count == paletteParts.Count;
    }

    public override void OnMouseClicked(Form owner, TNTCAD cad, LayoutSettingsForm layoutSettings)
    {
        cad.SelectedObjects.OfType<PalettePart>().ToList().ForEach(palettePart => palettePart.RotationAngle += Angle);
        cad.ShowPropertyChanges();
    }
}
