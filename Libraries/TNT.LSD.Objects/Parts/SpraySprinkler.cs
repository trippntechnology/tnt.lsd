using System;
using System.Drawing;
using System.Text.RegularExpressions;

namespace TNT.LSD.Objects
{
	public class SpraySprinkler: ArcSprinkler
	{
		#region Constructors

		public SpraySprinkler()
			: base()
		{
		}

		public SpraySprinkler(SpraySprinkler obj)
			: base(obj)
		{
		}

		#endregion

		#region Overrides

		public override TNTObject Clone()
		{
			return new SpraySprinkler(this);
		}

		public override void Assign(TNT.LSD.Objects.TNTObject obj)
		{
			base.Assign(obj);
		}

		#endregion
	}
}
