using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;

namespace TNT.LSD.Objects.TypeConverters
{
	/// <summary>
	/// Base type converter
	/// </summary>
	public abstract class BaseTypeConverter : TypeConverter
	{
		/// <summary>
		/// Member object to hold the PropertyDescriptor passed into GetPropertyValues so that it can be used
		/// in the List property's getter.
		/// </summary>
		protected PropertyDescriptor m_PropertyDescriptor = null;

		/// <summary>
		/// Member object to hold the object passed into GetPropertyValues so that it can be used
		/// in the List property's getter.
		/// </summary>
		protected object m_Object = null;

		/// <summary>
		/// List that contains the string values associated with the converter. Must be initialized
		/// by subclasses.
		/// </summary>
		protected abstract List<string> List { get; }

		/// <summary>
		/// Returns the string value located at the index within List if it exists, string.empty otherwise.
		/// </summary>
		/// <param name="index">Index in the array were value resides</param>
		/// <returns>String value located at the index within List if it exists, string.empty otherwise</returns>
		virtual public string this[int index]
		{
			get
			{
				if (List != null && index < List.Count)
				{
					return List[index];
				}

				return string.Empty;
			}
		}
	
		#region TypeConverter Overrides
		
		/// <summary>
		/// Specifies that this object supports a standard set of values that can be picked from a list, 
		/// using the specified context.
		/// </summary>
		/// <param name="context">An System.ComponentModel.ITypeDescriptorContext that provides a format context.</param>
		/// <returns>True if TypeConverter.GetStandardValues() should be called to find a common set of 
		/// values the object supports; otherwise, false.</returns>
		public override bool GetStandardValuesSupported(ITypeDescriptorContext context)
		{
			return true;
		}

		/// <summary>
		/// Returns an array of values that can be chosen for the instance in this context. Instance may be an array in
		/// which case only the intersection of common values are returned. 
		/// </summary>
		/// <param name="context">Indicates the context for the request</param>
		/// <returns>Array of common values that can be chosen for this instance/instances</returns>
		public override TypeConverter.StandardValuesCollection GetStandardValues(ITypeDescriptorContext context)
		{
			//string propertyName = context.PropertyDescriptor.Name;
			string[] standardValues = new string[0];

			if (context.Instance.GetType().IsArray)
			{
				TNTObject[] objs = context.Instance as TNTObject[];
				List<string> intersection = null;

				foreach (object obj in objs)
				{
					// Get the common descriptions accross all instances
					if (intersection == null)
					{
						intersection = GetPropertyValues(context.PropertyDescriptor, obj);
					}
					else
					{
						intersection = intersection.Intersect(GetPropertyValues(context.PropertyDescriptor, obj)).ToList();
					}
				}

				standardValues = intersection.ToArray();
			}
			else
			{
				standardValues = GetPropertyValues(context.PropertyDescriptor, context.Instance).ToArray();
			}

			return new StandardValuesCollection(standardValues);
		}

		#endregion

		/// <summary>
		/// Implement to return a list of valid values associated with the Property Descriptor being queried
		/// </summary>
		/// <param name="propertyDescriptor">Property descriptor being queried</param>
		/// <param name="obj">Object that the property belongs to</param>
		/// <returns>Return a list of valid values associated with the Property Descriptor being queried</returns>
		virtual protected List<string> GetPropertyValues(PropertyDescriptor propertyDescriptor, object obj)
		{
			m_PropertyDescriptor = propertyDescriptor;
			m_Object = obj;

			return List;
		}

		/// <summary>
		/// Returns the index of the value in the List if exists
		/// </summary>
		/// <param name="value">Value who's index should be found</param>
		/// <returns>Zero based index if value exists, -1 otherwise</returns>
		virtual public int IndexOf(string value)
		{
			if (List != null)
			{
				return List.IndexOf(value);
			}

			return -1;
		}

	}
}
