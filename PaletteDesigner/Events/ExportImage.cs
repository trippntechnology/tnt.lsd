namespace PaletteDesigner.Events

{
  class ExportImage : NodeEvents
  {
    public override string Text => "Export Image";

    public override string ToolTipText => "Export image associated with node";

    public override void OnMouseClick(object sender, EventArgs e)
    {
      base.OnMouseClick(sender, e);
      using (var sfd = new SaveFileDialog())
      {
        if (sfd.ShowDialog() == DialogResult.OK)
        {
          PalletNodeTreeView.ExportImage(sfd.FileName);
        }
      }
    }
  }
}
