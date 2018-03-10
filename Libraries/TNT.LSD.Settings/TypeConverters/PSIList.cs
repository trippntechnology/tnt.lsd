using System.Collections.Generic;

namespace TNT.LSD.Settings.TypeConverters
{
	/// <summary>
	/// Provides a PSI listing.
	/// </summary>
	public class PSIList : BaseTypeConverter
	{
		/// <summary>
		/// Returns a PSI listing
		/// </summary>
		protected override List<string> List { get { return new List<string>(new string[] { "NA", "< 40 PSI", "40 to 60 PSI", "> 60 PSI" }); } }
	}
}
