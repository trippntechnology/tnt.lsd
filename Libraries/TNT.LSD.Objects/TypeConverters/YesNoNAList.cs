using System.Collections.Generic;

namespace TNT.LSD.Objects.TypeConverters
{
	/// <summary>
	/// List containing "Yes", "No", and "NA"
	/// </summary>
	public class YesNoNAList : BaseTypeConverter
	{
		/// <summary>
		/// List consisting of "Yes", "No", and "NA"
		/// </summary>
		protected override List<string> List { get { return new List<string>(new string[] { "NA", "Yes", "No" }); } }
	}
}
