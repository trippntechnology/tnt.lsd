using System.Collections.Generic;

namespace TNT.LSD.Objects.TypeConverters
{
	/// <summary>
	/// Provides a listing of valid thread genders
	/// </summary>
	class ThreadGenderList : BaseTypeConverter
	{
		/// <summary>
		/// Returns a list of pipe genders
		/// </summary>
		protected override List<string> List { get { return new List<string>(new List<string>(new string[] { "MIPT", "FIPT" })); } }
	}
}
