using Microsoft.Win32;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Windows.Forms;
using TNT.Utilities;

namespace TNT.LSD.Colorizer
{
	public partial class ValveColorizer : Form
	{

		protected ApplicationRegistry m_ApplicationRegistry = new ApplicationRegistry(Registry.CurrentUser, "Tripp'n Technology", "ValveColorizerPlugin");

		public ValveColorizer()
		{
			InitializeComponent();
		}

		public new DialogResult ShowDialog()
		{
			return ShowDialog(null);
		}

		public new DialogResult ShowDialog(IWin32Window owner)
		{
			InitializeColors(false);
			DialogResult result = DialogResult.Cancel;

			if (owner != null)
				result = base.ShowDialog(owner);
			else
				result = base.ShowDialog();

			if (result == DialogResult.OK)
			{
				List<int> rgbValues = new List<int>();

				foreach (Control ctrl in ctlp.Controls)
				{
					rgbValues.Add(ctrl.BackColor.ToArgb());
				}

				// Store the color values
				m_ApplicationRegistry.WriteList<int>("Colors", rgbValues);
			}

			return result;
		}

		private void InitializeColors(bool reset)
		{
			List<Color> colors = new List<Color>(Constants.DEFAULT_COLORS);

			// Get colors from registry
			List<int> rgbValues = m_ApplicationRegistry.ReadList<int>("Colors");

			if (!reset && rgbValues.Count > 0)
			{
				colors.Clear();
				rgbValues.ForEach(v => colors.Add(Color.FromArgb(v)));
			}

			ctlp.Controls.Clear();
			ctlp.RowCount = ctlp.Controls.Count;
			ctlp.RowStyles.Clear();

			for (int row = 0; row < colors.Count; row++)
			{
				AddColor(colors[row]);
			}
		}

		private void AddColor_Click(object sender, EventArgs e)
		{
			ColorDialog cd = new ColorDialog();

			if (cd.ShowDialog() == DialogResult.OK)
			{
				AddColor(cd.Color);
			}
		}

		private void FormatRows(TableLayoutRowStyleCollection rowStyles)
		{
			foreach (RowStyle rs in rowStyles)
			{
				rs.SizeType = SizeType.Percent;
				rs.Height = (float)(1.0 / rowStyles.Count);
			}
		}

		private Panel CreatePanel(Color backColor)
		{
			Panel panel = new Panel()
			{
				BackColor = backColor,
				Dock = DockStyle.Fill
			};

			panel.Click += Panel_Click;

			return panel;
		}

		private void Panel_Click(object sender, EventArgs e)
		{
			Panel panel = sender as Panel;

			using (ColorDialog cd = new ColorDialog())
			{
				cd.Color = panel.BackColor;

				if (cd.ShowDialog() == System.Windows.Forms.DialogResult.OK)
				{
					panel.BackColor = cd.Color;
				}
			}
		}

		private void RemoveColor_Click(object sender, EventArgs e)
		{
			RemoveColor();
		}

		private void AddColor(Color color)
		{
			ctlp.Controls.Add(CreatePanel(color));
			ctlp.RowCount = ctlp.Controls.Count;
			ctlp.RowStyles.Add(new RowStyle(SizeType.Percent));
			FormatRows(ctlp.RowStyles);
		}

		private void RemoveColor()
		{
			ctlp.Controls.RemoveAt(0);
			ctlp.RowCount = ctlp.Controls.Count;
			ctlp.RowStyles.RemoveAt(0);
			FormatRows(ctlp.RowStyles);
		}

		private void RestoreDefaultColors_Click(object sender, EventArgs e)
		{
			InitializeColors(true);
		}
	}
}
