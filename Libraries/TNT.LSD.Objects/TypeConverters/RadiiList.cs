using System.Collections.Generic;
using TNT.LSD.Settings.TypeConverters;
using TNT.Reflection;

namespace TNT.LSD.Objects.TypeConverters
{
	/// <summary>
	/// Type converter that generates a listing of available radii
	/// </summary>
	public class RadiiList : BaseTypeConverter
	{
		/// <summary>
		/// Returns the list of radii that are valid for this sprinkler object
		/// </summary>
		protected override List<string> List
		{
			get
			{
				Sprinkler sprinkler = m_Object as Sprinkler;

				if (sprinkler != null)
				{
					return sprinkler.Radii;
				}

				return new List<string>();
			}
		}
	}
}
