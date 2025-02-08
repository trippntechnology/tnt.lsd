using TNT.LSD.Components;

namespace PaletteDesigner.Events;

class AddImage : NodeEvents
{
  public OpenFileDialog OpenFileDialog { get { return (ExternalObject as Tuple<object, object>).Item2 as OpenFileDialog; } }

  public override string Text => "Add Image";

  public override string ToolTipText => "Add image to node";

  public override void OnMouseClick(object sender, EventArgs e)
  {
    base.OnMouseClick(sender, e);
    using (OpenFileDialog ofd = new OpenFileDialog())
    {
      if (OpenFileDialog.ShowDialog() == DialogResult.OK)
      {
        PalletNodeTreeView.AddImage(OpenFileDialog.FileName);
      }
    }
  }
}
