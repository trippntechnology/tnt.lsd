using System.Drawing;
using System.Windows.Forms;
using TNT.LSD.Components;
using TNT.Plugin.Manager;

namespace TNT.LSD.Background;

public class ImportBackgroundPlugin : BackgroundPlugin
{
  public override string Text => "Import";

  public override string ToolTipText => "Import landscape image";

  public override Image Image => base.GetImage("TNT.LSD.Background.Images.import_background_image.png");

  public override void Execute(IWin32Window owner, ToolStripItem sender, IApplicationData content)
  {
    if (content is ApplicationData appData && appData.TNTCAD != null)
    {
      using (BackgroundImporter bi = new BackgroundImporter())
      {
        if (bi.ShowDialog(owner, appData.TNTCAD.State) == DialogResult.OK)
        {
          var showBackgroundPlugin = new ShowBackgroundPlugin();
          var plugin = _Manager.GetPlugins(p => p.Text == showBackgroundPlugin.Text);
          (plugin.FirstOrDefault() as ShowBackgroundPlugin)?.SetChecked(owner, content);

          var button = new ToolStripButton
          {
            CheckOnClick = true,
            Checked = true
          };
          (new ShowBackgroundPlugin()).Execute(owner, button, content);
        }
      }
    }
  }

  public override MenuStrip GetMenuStrip()
  {
    var menuStrip = base.GetMenuStrip();
    var landscapeMenu = menuStrip.Items.FindItem("Landscape");
    ToolStripItem item = (ToolStripMenuItem)CreateToolStripItem<ToolStripMenuItem>();
    landscapeMenu.DropDownItems.Add(item);
    return menuStrip;
  }

  public override ToolStrip GetToolStrip()
  {
    ToolStrip toolStrip = new ToolStrip();

    ToolStripButton toolStripButton = (ToolStripButton)CreateToolStripItem<ToolStripButton>();
    toolStripButton.DisplayStyle = ToolStripItemDisplayStyle.Image;
    toolStrip.Items.Add(toolStripButton);

    return toolStrip;
  }
}
