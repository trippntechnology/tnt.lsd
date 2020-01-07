using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Drawing.Design;
using TNT.LSD.Inventory;
using TNT.LSD.Settings;

namespace TNT.LSD.Objects
{
	/// <summary>
	/// Represents a part that can only be on the lateral line
	/// </summary>
	public abstract class LateralPart : PalettePart
	{
		#region Members

		protected List<string> m_GPMList = new List<string>();

		#endregion

		#region Properties

		#region GPM

		[Description("The flow rate of the sprinkler in Gallons Per Minute")]
#if !PALETTE_PROPERTIES
		[ReadOnly(true)]
#endif
		public string GPM { get; set; }

		[Editor(@"System.Windows.Forms.Design.StringCollectionEditor, System.Design, Version=2.0.0.0, Culture=neutral, PublicKeyToken=b03f5f7f11d50a3a", typeof(UITypeEditor))]
#if !PALETTE_PROPERTIES
		[Browsable(false)]
#endif
		public List<string> GPMList { get { return m_GPMList; } }

		#endregion

		#endregion

		#region Constructors

		public LateralPart(Point position, String fileName)
			: base()
		{
		}

		// Copy constructor
		public LateralPart(LateralPart obj)
			: base(obj)
		{
			m_GPMList = obj.GPMList != null ? new List<string>(obj.GPMList) : null;
			GPM = obj.GPM;
		}

		public LateralPart()
			: base()
		{
		}

		#endregion

		#region Overrides

		/// <summary>
		/// Specifies if the pipe type can be added to this part
		/// </summary>
		/// <param name="pipeType">Pipe's type</param>
		/// <param name="reason">When false, reason pipe can not be added</param>
		/// <returns>True if pipe type can be added, false otherwise</returns>
		public override bool CanAddPipe(Type pipeType, out string reason)
		{
			bool result = base.CanAddPipe(pipeType, out reason);

			if (result && pipeType != typeof(LateralPipe))
			{
				reason = "Part can only be connected using lateral line pipe";
				result = false;
			}

			return result;
		}

		public override double SizePipe(Pipe upstreamPipe)
		{
			RequiredFlow = Convert.ToDouble(GPM);
			return base.SizePipe(upstreamPipe);
		}

		public override void SetPartQuantity(Dictionary<string, Part> parts, SystemType systemType)
		{
			base.SetPartQuantity(parts, systemType);
			parts["FLAG"].Quantity += 1;
		}
		#endregion
	}
}
