using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Windows.Forms;
using TNT.LSD.Inventory;
using TNT.LSD.Inventory.DAL;

namespace TNT.LSD.PartsEditor
{
	/// <summary>
	/// Parts Editor form
	/// </summary>
	public partial class PartsEditor : Form
	{
		protected List<Part> MasterParts = null;

		/// <summary>
		/// Constructor. Initializes ComboBoxes
		/// </summary>
		public PartsEditor()
		{
			InitializeComponent();
		}

		/// <summary>
		/// Displays parts in the list view within a modal dialog
		/// </summary>
		/// <param name="owner">Window that instantiated this dialog</param>
		/// <param name="parts">Parts to manipulate</param>
		/// <returns>DialogResults</returns>
		public DialogResult ShowDialog(IWin32Window owner, List<Part> parts)
		{
			Inventory inventory = new Inventory(DALPart.GetParts());
			inventoryBindingSource.DataSource = inventory;
			this.MasterParts = parts;

			dgvPartsList.DataSource = new BindingList<Part>(parts.ConvertAll(p => new Part(p)));

			DialogResult dialogResult = base.ShowDialog(owner);

			if (dialogResult == DialogResult.OK)
			{
				this.MasterParts.Clear();
				this.MasterParts.AddRange(dgvPartsList.DataSource as BindingList<Part>);
			}

			return dialogResult;
		}

		private void dgvPartsList_EditingControlShowing(object sender, DataGridViewEditingControlShowingEventArgs e)
		{
			var cb = e.Control as ComboBox;

			if (cb != null)
			{
				cb.SelectionChangeCommitted += SelectionChangeCommitted;
			}
		}

		private void SelectionChangeCommitted(object sender, EventArgs e)
		{
			try
			{
				ComboBox cb = sender as ComboBox;
				DataGridView dgv = dgvPartsList;
				int col = dgv.CurrentCell.ColumnIndex;
				int row = dgv.CurrentCell.RowIndex;

				Part part = (cb.DataSource as BindingSource).List[cb.SelectedIndex] as Part;
				object currentValue = dgv.Rows[row].Cells[col].Value;

				if (currentValue != cb.SelectedValue)
				{
					if (col == 0)
					{
						dgv.Rows[row].Cells[1].Value = part.Description;
					}
					else if (col == 1)
					{
						dgv.Rows[row].Cells[0].Value = part.Code;
					}

					dgv.Rows[row].Cells[2].Value = 0;
				}
			}
			catch (Exception ex)
			{
			}
		}

		private void deleteToolStripMenuItem_Click(object sender, EventArgs e)
		{
			DataGridView dgv = dgvPartsList;
			int col = dgv.CurrentCell.ColumnIndex;
			int row = dgv.CurrentCell.RowIndex;
			BindingList<Part> parts = dgv.DataSource as BindingList<Part>;

			parts.RemoveAt(row);
		}
	}
}