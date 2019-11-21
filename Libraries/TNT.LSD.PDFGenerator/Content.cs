using System.Collections.Generic;
using System.Drawing;
using TNT.LSD.Inventory;

namespace TNT.LSD.PDFGenerator
{
	/// <summary>
	/// Represents the content of the PDF
	/// </summary>
	public class Content
	{
		/// <summary>
		/// Image that represents the design
		/// </summary>
		public Image Design { get; set; }

		/// <summary>
		/// Image that shows the coverage
		/// </summary>
		public Image Coverage { get; set; }

		/// <summary>
		/// Design's number
		/// </summary>
		public string DesignNumber { get; set; }

		/// <summary>
		/// Owner's name
		/// </summary>
		public string OwnerName { get; set; }

		/// <summary>
		/// Represents an object that has a dynamic set of properties
		/// </summary>
		public object DynamicProperties { get; set; }

		/// <summary>
		/// Comments
		/// </summary>
		public string Comments { get; set; }

		/// <summary>
		/// Parts list
		/// </summary>
		public List<Part> Parts { get; set; }
	}
}
