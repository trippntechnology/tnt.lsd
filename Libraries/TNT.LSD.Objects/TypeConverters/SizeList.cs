using System.Collections.Generic;
using TNT.LSD.Settings.TypeConverters;

namespace TNT.LSD.Objects.TypeConverters
{
	/// <summary>
	/// Type converter that generates a listing of available sizes.
	/// </summary>
	public class SizeList : BaseTypeConverter
	{
		/// <summary>
		/// Returns a list of valid sizes
		/// </summary>
		protected override List<string> List { get { return new List<string>(new string[] { "1/2\"", "3/4\"", "1\"", "1-1/4\"", "1-1/2\"", "2\"" }); } }
	}
}
