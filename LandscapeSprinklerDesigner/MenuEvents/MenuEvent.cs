using LSDComponents;
using System;
using System.Drawing;
using System.IO;
using System.Reflection;
using System.Windows.Forms;
using TNT.ToolStripItemManager;
using TNT.Utilities;

namespace LandscapeSprinklerDesigner.MenuEvents
{
	abstract class MenuEvent : ToolStripItemGroup
	{
		public Tuple<Form, TNTCAD, LayoutSettingsForm> _Tuple => ExternalObject as Tuple<Form, TNTCAD, LayoutSettingsForm>;

		public Form Owner => _Tuple.Item1;
		public TNTCAD CAD => _Tuple.Item2;
		public LayoutSettingsForm LayoutSettings => _Tuple.Item3;

		public MenuEvent(Image image = null) : base(image)
		{
		}

		virtual protected string AssemblyTitle
		{
			get
			{
				AssemblyTitleAttribute ata = Utilities.GetAssemblyAttribute<AssemblyTitleAttribute>(Assembly.GetExecutingAssembly());
				return ata != null ? ata.Title : string.Empty;
			}
		}

		virtual protected bool HandleUnsavedChanges()
		{
			bool handled = true;

			CAD.Repaint();

			if (CAD.HasUnsavedChanges)
			{
				string msg = string.Format("The layout \"{0}\" has been modified.\nDo you want to save your changes?", string.IsNullOrEmpty(CAD.CurrentFileName) ? "Untitled" : Path.GetFileName(CAD.CurrentFileName));
				DialogResult dr = MessageBox.Show(msg, AssemblyTitle, MessageBoxButtons.YesNoCancel, MessageBoxIcon.Question);

				switch (dr)
				{
					case DialogResult.Yes:
						handled = CAD.Save(false);
						break;
					case DialogResult.No:
						break;
					default:
						handled = false;
						break;
				}
			}

			return handled;
		}
	}
}
