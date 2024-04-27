using TNT.Plugin.Manager;
using WeifenLuo.WinFormsUI.Docking;

namespace TNT.LSD.Components;

public class ApplicationData : IApplicationData
{
  public TNTCAD TNTCAD { get; set; }
  public DockPanel DockPanel { get; set; }

  public ApplicationData(TNTCAD tntCad, DockPanel dockPanel)
  {
    TNTCAD = tntCad;
    DockPanel = dockPanel;
  }
}
