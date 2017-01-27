using System.Collections.Generic;
using System.ComponentModel;

namespace TNT.LSD.Objects.TypeConverters
{
	/// <summary>
	/// Type converter that generates a listing of available pipe types.
	/// </summary>
	public class PipeTypeList : BaseTypeConverter
	{
		/// <summary>
		/// Returns a list of valid pipe types
		/// </summary>
		protected override List<string> List { get { return new List<string>(new string[] { "NA", "Copper", "PVC", "Galvanized", "Poly", "Poly CTS" }); } }

		/// <summary>
		/// Returns a list of valid pipe types associated with a PropertyDescriptor
		/// </summary>
		/// <param name="propertyDescriptor">Property descriptor being queried</param>
		/// <param name="obj">Object that the property belongs to</param>
		/// <returns>List of valid pipe types associated with a PropertyDescriptor</returns>
		protected override List<string> GetPropertyValues(PropertyDescriptor propertyDescriptor, object obj)
		{
			switch (propertyDescriptor.ComponentType.FullName)
			{
				case "TNT.LSD.Objects.TNTSource":
					return List.GetRange(1, List.Count - 1);
				default:
					return List;
			}
		}
	}
}
