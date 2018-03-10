using System.Collections.Generic;
using System.ComponentModel;

namespace TNT.LSD.Settings.TypeConverters
{
	/// <summary>
	/// Type converter that generates a listing of available pipe sizes.
	/// </summary>
	public class PipeSizeList : BaseTypeConverter
	{
		/// <summary>
		/// Returns a list of valid pipe sizes
		/// </summary>
		protected override List<string> List { get { return new List<string>(new string[] { "NA", "3/4\"", "1\"", "1-1/4\"", "1-1/2\"", "2\"" }); } }

		/// <summary>
		/// Returns a list of valid pipe sizes associated with a PropertyDescriptor
		/// </summary>
		/// <param name="propertyDescriptor">Property descriptor being queried</param>
		///<param name="obj">Object that the property belongs to</param>
		/// <returns>List of valid pipe sizes associated with a PropertyDescriptor</returns>
		protected override List<string> GetPropertyValues(PropertyDescriptor propertyDescriptor, object obj)
		{
			switch (propertyDescriptor.ComponentType.FullName)
			{
				case "TNT.LSD.Objects.Pipe":
				case "TNT.LSD.Objects.TNTSource":
					return List.GetRange(1, List.Count - 1);
				default:
					return List;
			}
		}

		/// <summary>
		/// Gets the code associated with the size
		/// </summary>
		/// <param name="size">Size to convert</param>
		/// <returns>Code associated with the size</returns>
		public string SizeToCode(string size)
		{
			int sizeIndex = IndexOf(size);
			return Constants.SIZE_CODE[sizeIndex];
		}
	}
}
