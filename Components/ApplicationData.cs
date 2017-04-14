using TNT.Plugin.Manager;

namespace LSDComponents
{
	public class ApplicationData: IApplicationData
	{
		public TNTCAD TNTCAD { get; set; }

		public ApplicationData(TNTCAD tntCad)
		{
			this.TNTCAD = tntCad;
		}
	}
}
