using System.Collections.Generic;
using System.Reflection;
using Inventory = TNT.LSD.Inventory;

namespace TNT.LSD.Objects.TypeConverters
{
	/// <summary>
	/// Type converter that generates a listing of part descriptions given a list of part codes
	/// </summary>
	public class PartDescriptionList : TypeConverters.BaseTypeConverter
	{
		/// <summary>
		/// Creates a list of descriptions associated with part codes
		/// </summary>
		protected override List<string> List
		{
			get
			{
				List<string> list = new List<string>();

				if (m_Object != null)
				{
					PropertyInfo codesProp = m_Object.GetType().GetProperty(string.Concat(m_PropertyDescriptor.Name, "Codes"));
					PropertyInfo descriptionsProp = m_Object.GetType().GetProperty(string.Concat(m_PropertyDescriptor.Name, "Descriptions"));

					if (codesProp == null || descriptionsProp == null)
					{
						return list;
					}

					List<string> codeList = codesProp.GetValue(m_Object, null) as List<string>;
					list = Inventory.DAL.DALPart.GetDescriptions(codeList);

					descriptionsProp.SetValue(m_Object, list, null);
				}

				return list;
			}
		}
	}
}
