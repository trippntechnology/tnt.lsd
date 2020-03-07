using LSDComponents;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Windows.Forms;
using TNT.LSD.Objects;
using TNT.Reflection;

namespace LandscapeSprinklerDesigner
{
	static class Extensions
	{
		static List<ToolStripItem> _ToolStripItems = new List<ToolStripItem>();

		public static void AddProperties(this ContextMenuStrip contextMenuStrip, List<object> objects, TNTCAD cad)
		{
			_ToolStripItems.ForEach(t => contextMenuStrip.Items.Remove(t));

			if (objects == null || objects.Count() == 0)
			{
				return;
			}

			// Get a listing of all unique property names
			List<string> propertyNames = objects.GetDistinctDisplayNames(pd => { return pd.Converter is BaseTypeConverter && pd.IsBrowsable && !pd.IsReadOnly; });

			if (propertyNames.Count > 0)
			{
				ToolStripItem mi = new ToolStripSeparator();
				_ToolStripItems.Add(mi);
				contextMenuStrip.Items.Add(mi);
			}

			// Get the common values for each of the properties and create an menu item with an on click 
			// event
			foreach (string propertyName in propertyNames)
			{
				List<string> values = objects.GetDistinctStandardValues(propertyName);
				string commonValue = objects.GetCommonValue(propertyName);

				if (values != null)
				{
					ToolStripMenuItem propertyMenuItem = new ToolStripMenuItem(propertyName);
					_ToolStripItems.Add(propertyMenuItem);

					foreach (var value in values)
					{
						ToolStripMenuItem tsmi = new ToolStripMenuItem(value.ToString(), null, (sender, e) =>
						{
							ToolStripMenuItem valuesMenuItem = sender as ToolStripMenuItem;

							cad.CreateUndoActions(objects.ConvertAll<TNTObject>(new Converter<object, TNTObject>(o => { return o as TNTObject; })), UndoAction.UndoActionType.uaModify);

							foreach (var obj in objects)
							{
								var pd = obj.GetPropertyDescriptors(p =>
								{
									return p.DisplayName == valuesMenuItem.OwnerItem.Text || p.Name == valuesMenuItem.OwnerItem.Text;
								}).FirstOrDefault();

								PropertyInfo property = obj.GetType().GetProperty(pd.Name);
								property.SetValue(obj, valuesMenuItem.Text, null);
							}

							cad.Refresh();
						});

						if (value == commonValue)
						{
							// All properties share the same value so select it
							tsmi.Checked = true;
						}

						propertyMenuItem.DropDownItems.Add(tsmi);
					}

					contextMenuStrip.Items.Add(propertyMenuItem);
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
