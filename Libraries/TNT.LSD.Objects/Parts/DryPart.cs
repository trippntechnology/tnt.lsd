using System;
using System.ComponentModel;
using System.Xml.Serialization;

namespace TNT.LSD.Objects
{
	public class DryPart : GenericPart
	{
		#region Properties

		[Browsable(false)]
		[XmlIgnore()]
		public override double RequiredFlow { get { return base.RequiredFlow; } set { base.RequiredFlow = value; } }

		#endregion

		#region Constructors

		// Copy constructor
		public DryPart(DryPart obj)
			: base(obj)
		{
		}

		public DryPart()
			: base()
		{
		}

		#endregion

		#region Overrides

		public override bool CanAddPipe(Type pipeType, out string reason)
		{
			reason = "Does not connect to pipe";
			return false;
		}

		public override TNTObject Clone()
		{
			return new DryPart(this);
		}

		#endregion
	}
}
