using System.Xml.Serialization;

namespace TNT.LSD.Inventory
{
	/// <summary>
	/// Represents a part in the inventory
	/// </summary>
	public class Part
	{
		#region Properties

		/// <summary>
		/// Part's Code
		/// </summary>
		public string Code { get; set; }

		/// <summary>
		/// Part's Description
		/// </summary>
		public string Description { get; set; }

		/// <summary>
		/// Part's Quantity
		/// </summary>
		public double Quantity { get; set; }

		/// <summary>
		/// External part that is equivolent to this part
		/// </summary>
		[XmlIgnore()]
		public Part ExternalPart { get; set; }

		#endregion

		#region Constructors

		/// <summary>
		/// Default destructor
		/// </summary>
		public Part()
		{
		}

		/// <summary>
		/// Copy constructor
		/// </summary>
		/// <param name="obj">Part object to copy</param>
		public Part(Part obj)
		{
			Code = obj.Code;
			Description = obj.Description;
			Quantity = obj.Quantity;

			if (obj.ExternalPart != null)
			{
				ExternalPart = new Part()
				{
					Code = obj.ExternalPart.Code,
					Description = obj.ExternalPart.Description
				};
			}
		}

		#endregion

		/// <summary>
		/// Returns the code and description
		/// </summary>
		/// <returns>Code and description</returns>
		public override string ToString() => $"({Quantity}) {Code}: {Description}";
	}
}
