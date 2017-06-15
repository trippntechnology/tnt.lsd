using TNT.Plugin.Manager;
using WeifenLuo.WinFormsUI.Docking;

namespace LSDComponents
{
	public class ApplicationData: IApplicationData
	{
		public TNTCAD TNTCAD { get; set; }
		public DockPanel DockPanel { get; set; }

		public ApplicationData(TNTCAD tntCad, DockPanel dockPanel)
		{
			this.TNTCAD = tntCad;
			this.DockPanel = dockPanel;
		}
	}
}
