using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using WeifenLuo.WinFormsUI.Docking;

namespace LandscapeSprinklerDesigner
{
	public partial class DockableForm : DockContent
	{
    [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
    public ToolStripMenuItem ViewMenuItem { get; set; }

		public DockableForm()
		{
			InitializeComponent();
		}

		private void DockableForm_FormClosing(object sender, FormClosingEventArgs e)
		{
			if (ViewMenuItem != null)
			{
				ViewMenuItem.Checked = false;
			}

			e.Cancel = true;
			Hide();
		}
	}
}
