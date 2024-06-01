using Microsoft.Win32;
using System.Drawing;
using System.Windows.Forms;
using TNT.LSD.Components;
using TNT.LSD.Objects;
using TNT.Plugin.Manager;
using TNT.Utilities;

namespace TNT.LSD.Colorizer;

public class Plugin : TNT.Plugin.Manager.Plugin
{
  protected ApplicationRegistry m_ApplicationRegistry = new ApplicationRegistry(Registry.CurrentUser, "Tripp'n Technology", "ValveColorizerPlugin");

  public override string MenuStripName => "MenuStrip1";

  public override string ToolStripName => "ToolStrip3";

  public override string Text => "Apply Color";

  public override string ToolTipText => "Apply color to each valve in the design";

  public override Image Image => base.GetImage("TNT.LSD.Colorizer.Images.paintbrush.png");

  public override bool LicenseRequired => true;

  public override void Execute(System.Windows.Forms.IWin32Window owner, ToolStripItem sender, IApplicationData content)
  {
    ApplicationData appData = content as ApplicationData;

    List<int> rgbValues = m_ApplicationRegistry.ReadList<int>("Colors");
    List<Valve> valves = (from v in appData.TNTCAD.State.ObjectLayers.Last() where v is Valve select v as Valve).ToList();
    var pipes = (from o in appData.TNTCAD.State.ObjectLayers.Last() where o is Pipe select o as Pipe).ToList();

    int colorIndex = 0;

    if (rgbValues.Count == 0)
    {
      // Initialize with defaults
      foreach (var color in Constants.DEFAULT_COLORS)
      {
        rgbValues.Add(color.ToArgb());
      }
    }

    foreach (var pipe in pipes)
    {
      pipe.PipeColor = Color.Black;
    }

    foreach (Valve v in valves)
    {
      if (colorIndex >= rgbValues.Count)
      {
        colorIndex = 0;
      }

      v.Color = Color.FromArgb(rgbValues[colorIndex]);
      colorIndex++;
    }
  }

  public override System.Windows.Forms.MenuStrip GetMenuStrip()
  {
    MenuStrip menuStrip = new MenuStrip();
    ToolStripMenuItem menu = new ToolStripMenuItem("&Tools");

    // Causes the Menu item in this menu strip to match the merging menu strip
    menu.MergeAction = MergeAction.MatchOnly;

    menu.DropDownItems.Add(new ToolStripSeparator());

    menu.DropDownItems.Add((ToolStripMenuItem)CreateToolStripItem<ToolStripMenuItem>());

    menuStrip.Items.Add(menu);

    return menuStrip;
  }

  public override System.Windows.Forms.ToolStrip GetToolStrip()
  {
    ToolStrip toolStrip = new ToolStrip();

    ToolStripSplitButton toolStripSplitButton = (ToolStripSplitButton)CreateToolStripItem<ToolStripSplitButton>();
    toolStripSplitButton.DisplayStyle = ToolStripItemDisplayStyle.Image;
    toolStrip.Items.Add(toolStripSplitButton);

    ToolStripMenuItem editColors = new ToolStripMenuItem("Edit Colors", null, (a, b) =>
    {
      ValveColorizer vc = new ValveColorizer();
      vc.ShowDialog();
    });

    editColors.ToolTipText = "Edit colors used to color valves";
    editColors.MouseEnter += base.Item_MouseEnter;
    editColors.MouseLeave += base.Item_MouseLeave;
    toolStripSplitButton.DropDownItems.Add(editColors);

    return toolStrip;
  }
}
