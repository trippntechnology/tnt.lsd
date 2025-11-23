using TNT.LSD.Components;
using TNT.ToolStripItemManager.Extension;

namespace LandscapeSprinklerDesigner.MenuEvents;

class CalculateAreaMenuEvent() : MenuEvent(Resource.menu_calculate_area, Resource.menu_calculate_area_tooltip, image: "LandscapeSprinklerDesigner.Images.calculate_area.png".ToImage())
{
    private const double ACRE_FEET = 43560.1742405;

    public override void OnApplicationIdle(TNTCAD cad)
    {
        Enabled = cad.AreaAvailable;
    }

    public override void OnMouseClicked(Form owner, TNTCAD cad)
    {
        var name = Path.GetFileNameWithoutExtension(cad.CurrentFileName);
        double area = cad.GetArea();
        double sqrFt = Math.Round(area, 2);
        double acre = Math.Round(area / 43560.1742405, 2);

        // Marshal clipboard operation to UI thread to ensure proper STA context
        owner?.Invoke((Action)(() =>
        {
            try
            {
                Clipboard.SetText($"{name}\t{DateTime.Now.ToShortDateString()}\t{acre}");
            }
            catch (System.Runtime.InteropServices.ExternalException ex)
            {
                System.Diagnostics.Debug.WriteLine($"Clipboard operation failed: {ex.Message}");
                // Continue to show the message even if clipboard copy fails
            }
        }));

        MessageBox.Show(string.Format("{0} square feet. {1} acres", sqrFt, acre), "Selected Area");
    }
}
