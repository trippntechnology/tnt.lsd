using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing.Design;

namespace TNT.LSD.Objects
{
	public class Controller : DryPart
	{
		#region Properties

		[Browsable(false)]
		public override string Model { get { return base.Model; } set { base.Model = value; } }

		protected string m_ZoneCapacity;

		[Description("Specifies the number of valves this controller is capable of controlling")]
		[DisplayName("Zone Capacity")]
		[TypeConverter(typeof(TypeConverters.GenericList))]
		virtual public string ZoneCapacity
		{
			get { return m_ZoneCapacity; }
			set
			{
				m_ZoneCapacity = value;

				if (ZoneCapacityList != null)
				{
					int zoneCapacityIndex = ZoneCapacityList.IndexOf(m_ZoneCapacity);

					if (zoneCapacityIndex > -1 && ModelCodes.Count > zoneCapacityIndex)
					{
						ModelCode = ModelCodes[zoneCapacityIndex];
					}
				}
			}
		}

		protected List<string> m_ZoneCapacityList = new List<string>();

		[Editor(@"System.Windows.Forms.Design.StringCollectionEditor, System.Design, Version=2.0.0.0, Culture=neutral, PublicKeyToken=b03f5f7f11d50a3a", typeof(UITypeEditor))]
#if !PALETTE_PROPERTIES
		[Browsable(false)]
#endif
		virtual public List<string> ZoneCapacityList { get { return m_ZoneCapacityList; } set { m_ZoneCapacityList = value; } }

		#endregion

		#region Constructors

		public Controller(Controller obj)
			: base(obj)
		{
			ZoneCapacityList = obj.ZoneCapacityList;
			ZoneCapacity = obj.ZoneCapacity;
		}

		public Controller()
			: base()
		{
		}

		#endregion

		#region Overrides

		public override TNTObject Clone()
		{
			return new Controller(this);
		}

		/// <summary>
		/// Creates undo copy
		/// </summary>
		/// <returns>Undo copy</returns>
		public override TNTObject CreateUndoCopy()
		{
			Controller newObj = base.CreateUndoCopy() as Controller;

			newObj.ZoneCapacity = ZoneCapacity;

			return newObj;
		}

		/// <summary>
		/// Assigns obj's properties to this object
		/// </summary>
		/// <param name="obj">Source object</param>
		public override void Assign(TNTObject obj)
		{
			Controller controller = obj as Controller;

			if (controller != null)
			{
				ZoneCapacity = controller.ZoneCapacity;
			}

			base.Assign(obj);
		}

		#endregion
	}
}
