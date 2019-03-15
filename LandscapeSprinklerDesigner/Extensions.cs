using LSDComponents;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Reflection;
using System.Windows.Forms;
using static System.ComponentModel.TypeConverter;

namespace LandscapeSprinklerDesigner
{
	class ExtensionTypeDescriptor : ITypeDescriptorContext
	{
		public IContainer Container => throw new NotImplementedException();

		public object Instance { get; private set; }

		public PropertyDescriptor PropertyDescriptor { get; private set; }

		public ExtensionTypeDescriptor(object obj, PropertyDescriptor propertyDescriptor)
		{
			this.Instance = obj;
			this.PropertyDescriptor = propertyDescriptor;
		}

		public object GetService(Type serviceType)
		{
			throw new NotImplementedException();
		}

		public void OnComponentChanged()
		{
			throw new NotImplementedException();
		}

		public bool OnComponentChanging()
		{
			throw new NotImplementedException();
		}
	}

	static class Extensions
	{
		static List<ToolStripItem> _ToolStripItems = new List<ToolStripItem>();

		/// <summary>
		/// Get a listing of browsable, non-readonly properties belonging to <paramref name="obj"/>
		/// </summary>
		/// <param name="obj">Object whose properties should be returned</param>
		/// <returns>Listing of browsable, non-readable properties belonging to <paramref name="obj"/></returns>
		public static List<string> GetProperties(this object obj)
		{
			PropertyDescriptorCollection pdc = TypeDescriptor.GetProperties(obj);
			return (from PropertyDescriptor pd in pdc where pd.IsBrowsable && !pd.IsReadOnly select pd.DisplayName).ToList();
		}

		/// <summary>
		/// Gets the values returned by <see cref="TypeConverter"/> GetStandardValues for a given <paramref name="propertyName"/>
		/// of a given <paramref name="obj"/>
		/// </summary>
		/// <param name="obj">Object whose has the property with values</param>
		/// <param name="propertyName">The name of the property with the standard values</param>
		/// <returns>The Standard Values for a property of a given object</returns>
		public static List<string> GetStandardValues(this object obj, string propertyName)
		{
			List<string> values = null;
			PropertyDescriptorCollection pdc = TypeDescriptor.GetProperties(obj);
			PropertyDescriptor pd = pdc[propertyName];
			ExtensionTypeDescriptor mtd = new ExtensionTypeDescriptor(obj, pd);
			StandardValuesCollection svc = pd?.Converter?.GetStandardValues(mtd);

			if (svc != null)
			{
				values = new List<string>();

				foreach (var v in svc)
				{
					values.Add(v.ToString());
				}
			}

			return values;
		}

		public static void AddProperties(this ContextMenuStrip contextMenuStrip, List<object> objects, TNTCAD cad)
		{
			_ToolStripItems.ForEach(t => contextMenuStrip.Items.Remove(t));

			if (objects == null || objects.Count() == 0)
			{
				return;
			}

			List<string> propertyNames = null;

			// Get a listing of all unique property names
			for (int i = 0; i < objects.Count; i++)
			{
				if (i == 0)
				{
					propertyNames = objects[i].GetProperties();
				}
				else
				{
					propertyNames = propertyNames.Intersect(objects[i].GetProperties()).ToList();
				}
			}

			if (propertyNames.Count> 0)
			{
				ToolStripItem mi = new ToolStripSeparator();
				_ToolStripItems.Add(mi);
				contextMenuStrip.Items.Add(mi);
			}

			// Get the common values for each of the properties and create an menu item with an on click 
			// event
			foreach (string propertyName in propertyNames)
			{
				List<string> values = null;

				for (int i = 0; i < objects.Count; i++)
				{
					if (i == 0)
					{
						values = objects[i].GetStandardValues(propertyName);
					}
					else
					{
						values = values?.Intersect(objects[i].GetStandardValues(propertyName)).ToList();
					}
				}

				if (values != null)
				{
					ToolStripMenuItem propertyMenuItem = new ToolStripMenuItem(propertyName);
					_ToolStripItems.Add(propertyMenuItem);

					foreach (var v in values)
					{
						ToolStripMenuItem tsmi = new ToolStripMenuItem(v.ToString(), null, (sender, e) =>
						{
							ToolStripMenuItem valuesMenuItem = sender as ToolStripMenuItem;

							foreach (var obj in objects)
							{
								PropertyInfo property = obj.GetType().GetProperty(valuesMenuItem.OwnerItem.Text);
								property.SetValue(obj, valuesMenuItem.Text, null);
							}

							cad.Refresh();
						});

						propertyMenuItem.DropDownItems.Add(tsmi);
					}


					contextMenuStrip.Items.Add(propertyMenuItem);
					//menuItem.DropDownItems.Add(propertyMenuItem);
				}
			}
		}

		public static void AddProperties(this ToolStripMenuItem menuItem, List<object> objects)
		{
			menuItem.DropDownItems.Clear();

			if (objects == null || objects.Count() == 0)
			{
				return;
			}

			List<string> propertyNames = null;

			// Get a listing of all unique property names
			for (int i = 0; i < objects.Count; i++)
			{
				if (i == 0)
				{
					propertyNames = objects[i].GetProperties();
				}
				else
				{
					propertyNames = propertyNames.Intersect(objects[i].GetProperties()).ToList();
				}
			}

			// Get the common values for each of the properties and create an menu item with an on click 
			// event
			foreach (string propertyName in propertyNames)
			{
				List<string> values = null;

				for (int i = 0; i < objects.Count; i++)
				{
					if (i == 0)
					{
						values = objects[i].GetStandardValues(propertyName);
					}
					else
					{
						values = values?.Intersect(objects[i].GetStandardValues(propertyName)).ToList();
					}
				}

				if (values != null)
				{
					ToolStripMenuItem propertyMenuItem = new ToolStripMenuItem(propertyName);

					foreach (var v in values)
					{
						ToolStripMenuItem tsmi = new ToolStripMenuItem(v.ToString(), null, (sender, e) =>
						{
							ToolStripMenuItem valuesMenuItem = sender as ToolStripMenuItem;

							foreach (var obj in objects)
							{
								PropertyInfo property = obj.GetType().GetProperty(valuesMenuItem.OwnerItem.Text);
								property.SetValue(obj, valuesMenuItem.Text, null);
							}

						});

						propertyMenuItem.DropDownItems.Add(tsmi);
					}

					menuItem.DropDownItems.Add(propertyMenuItem);
				}
			}
		}

		public static void IfNotNull<T>(this T it, Action<T> action)
		{
			if (it != null)
			{
				action?.Invoke(it);
			}
		}
	}
}
