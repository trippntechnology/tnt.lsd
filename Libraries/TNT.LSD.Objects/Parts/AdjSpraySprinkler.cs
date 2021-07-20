using System;

namespace TNT.LSD.Objects
{
	public class AdjSpraySprinkler : AdjustableSprinkler
	{
		#region Constructors

		public AdjSpraySprinkler()
			: base()
		{
		}

		public AdjSpraySprinkler(AdjSpraySprinkler obj)
			: base(obj)
		{
		}

		#endregion

		#region Overrides

		public override TNTObject Clone()
		{
			return new AdjSpraySprinkler(this);
		}

		protected override void SetGPM()
		{
			if (!string.IsNullOrEmpty(Radius))
			{
				int index = Radii.IndexOf(Radius);

				if (GPMList.Count > index)
				{
					double fullGPM = Convert.ToDouble(GPMList[index]);
					double fullRatio = Arc == 0 ? 1 : Arc / 360.0;
					GPM = fullGPM * fullRatio;
				}
			}
		}

		#endregion
	}
}
