using System;
using System.Collections.Generic;
using System.Windows.Forms;
using TNT.LSD.Inventory;
using TNT.LSD.Inventory.DAL;

namespace TNT.LSD.Plugins.Support
{
	[System.Runtime.InteropServices.GuidAttribute("C7C1D154-703E-47A8-9860-B06BD086CC2B")]
	public partial class DGVPartsEditor : Form
	{
		public DGVPartsEditor()
		{
			InitializeComponent();

			// Initialize combobox lists
			codeBindingSource.DataSource = DALPart.GetPartBindingList("code");
			descriptionBindingSource.DataSource = DALPart.GetPartBindingList("description");		}

		public DialogResult ShowDialog(IWin32Window owner, List<Part> parts)
		{
			List<Part> clipboard = new List<Part>(parts);
			_DataGrid.DataSource = clipboard;
			DialogResult dr = base.ShowDialog(owner);

			// Remove entries the don'at have a code or description
			clipboard.RemoveAll(p =>
			{
				return string.IsNullOrEmpty(p.Code) || string.IsNullOrEmpty(p.Description);
			});

			// Check if Ok was pressed and update the list
			if (dr == System.Windows.Forms.DialogResult.OK)
			{
				parts.Clear();
				parts.AddRange(clipboard);
			}

			return dr;
		}

		private void dataGridView1_EditingControlShowing(object sender, DataGridViewEditingControlShowingEventArgs e)
		{
			var cb = e.Control as ComboBox;

			if (cb != null)
			{
				cb.SelectedIndexChanged += Column1_CB_SelectedIndexChanged;
			}
		}

		/// <summary>
		/// Sets the associated row with the value chosen in the ComboBox
		/// </summary>
		void Column1_CB_SelectedIndexChanged(object sender, EventArgs e)
		{
			try
			{
				ComboBox cb = sender as ComboBox;
				int col = _DataGrid.CurrentCell.ColumnIndex;
				int row = _DataGrid.CurrentCell.RowIndex;

				Part part = (cb.DataSource as BindingSource).List[cb.SelectedIndex] as Part;

				if (col == 0)
				{
					_DataGrid.Rows[row].Cells[1].Value = part.Description;
				}
				else if (col == 1)
				{
					_DataGrid.Rows[row].Cells[0].Value = part.Code;
				}

			}
			catch (Exception)
			{
			}
		}

		private void addToolStripMenuItem_Click(object sender, EventArgs e)
		{
			List<Part> parts = _DataGrid.DataSource as List<Part>;
			parts.Add(new Part());
			_DataGrid.DataSource = null;
			_DataGrid.DataSource = parts;
		}

		private void deleteToolStripMenuItem_Click(object sender, EventArgs e)
		{
			DataGridViewSelectedRowCollection selectedRows = _DataGrid.SelectedRows;

			List<Part> parts = _DataGrid.DataSource as List<Part>;

			foreach (DataGridViewCell cell in _DataGrid.SelectedCells)
			{
				parts.RemoveAt(cell.RowIndex);
			}

			_DataGrid.DataSource = null;
			_DataGrid.DataSource = parts;
		}

	}
}
