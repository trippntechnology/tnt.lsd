using System.Collections.Generic;
using System.Reflection;

namespace TNT.LSD.Objects.TypeConverters
{
	/// <summary>
	/// Type converter that return list associated with property
	/// </summary>
	public class GenericList : BaseTypeConverter
	{
		/// <summary>
		/// Creates a list of descriptions associated with a property
		/// </summary>
		protected override List<string> List
		{
			get
			{
				List<string> list = new List<string>();

				if (m_Object != null)
				{
					PropertyInfo listProperty = m_Object.GetType().GetProperty(string.Concat(m_PropertyDescriptor.Name, "List"));

					if (listProperty == null)
					{
						return list;
					}

					list = listProperty.GetValue(m_Object, null) as List<string>;
				}

				return list;
			}
		}
	}
}
