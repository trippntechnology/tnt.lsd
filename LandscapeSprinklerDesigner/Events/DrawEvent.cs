using TNT.LSD.Components;
using TNT.LSD.Components.DrawingModes;
using TNT.ToolStripItemManager;

namespace LandscapeSprinklerDesigner.Events;

abstract class DrawEvent : ToolStripItemGroup
{
  public abstract DrawingMode DrawingMode { get; }

  public DrawEvent(Image image = null)
    : base(image)
  {
  }

  private Tuple<TNTCAD, PropertyForm, PalletTreeForm> _Tuple => ExternalObject as Tuple<TNTCAD, PropertyForm, PalletTreeForm>;

  protected TNTCAD Cad => _Tuple.Item1;
  protected PropertyForm PropertyForm => _Tuple.Item2;
  protected PalletTreeForm PalletTreeForm => _Tuple.Item3;

  public override void CheckedChanged(object sender, EventArgs e)
  {
    var toolStripItem = sender as ToolStripItem;
    if (!toolStripItem.GetChecked()) return;

    this.Cad.SetDrawingMode(this.DrawingMode);
    this.PropertyForm.SelectedObject = this.DrawingMode.DefaultObject;
    this.PalletTreeForm.UnselectAll();
    base.CheckedChanged(sender, e);
  }
}
