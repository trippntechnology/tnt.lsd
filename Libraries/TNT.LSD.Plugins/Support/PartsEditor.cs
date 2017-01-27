using System.Collections.Generic;
using System.Drawing;
using System.Windows.Forms;
using TNT.LSD.Inventory;
using TNT.LSD.Inventory.DAL;

namespace TNT.LSD.Plugins
{
	/// <summary>
	/// Parts Editor form
	/// </summary>
	public partial class PartsEditor : Form
	{
		// X offset of the edit controls
		private const int X_OFFSET = 3;

		// Y offset of the edit controls
		private const int Y_OFFSET = 2;

		// Current List Item selected
		protected ListViewItem m_CurrentItem = null;

		// Current List SubItem selected
		protected ListViewItem.ListViewSubItem m_CurrentSubItem = null;

		// List of static parts that are being manipulated
		protected List<Part> m_Parts;

		/// <summary>
		/// Constructor. Initializes ComboBoxes
		/// </summary>
		public PartsEditor()
		{
			InitializeComponent();

			// Initialize combobox lists
			Dictionary<string, Part> parts = DALPart.GetParts();

			foreach (string key in parts.Keys)
			{
				cbCodes.Items.Add(new CodeItem(parts[key]));
				cbDescriptions.Items.Add(new DescriptionItem(parts[key]));
			}
		}

		/// <summary>
		/// Displays parts in the list view within a modal dialog
		/// </summary>
		/// <param name="parts">Parts to manipulate</param>
		/// <returns>DialogResults</returns>
		public DialogResult ShowDialog(IWin32Window owner, List<Part> parts)
		{
			m_Parts = parts;
			PartsListView.Items.Clear();

			// Populate the list with the current parts
			foreach (Part p in parts)
			{
				ListViewItem lvi = new ListViewItem(new string[] { "", p.Code, p.Description, p.Quantity.ToString() });
				lvi.Tag = p;
				PartsListView.Items.Add(lvi);
			}

			return base.ShowDialog(owner);
		}

		/// <summary>
		/// Finds the item/subitem that is currently under the mouse and shows the 
		/// corresponding edit control
		/// </summary>
		/// <param name="sender">ListView control</param>
		/// <param name="e">Not used</param>
		private void PartsListView_MouseDown(object sender, MouseEventArgs e)
		{
			ListView lv = sender as ListView;
			m_CurrentItem = null;
			m_CurrentSubItem = null;

			// Find list item
			foreach (ListViewItem lvi in lv.Items)
			{
				if (lvi.Bounds.Contains(e.Location))
				{
					m_CurrentItem = lvi;

					for (int index = 1; index < lvi.SubItems.Count; index++)
					{
						ListViewItem.ListViewSubItem lvsi = lvi.SubItems[index];
						if (lvsi.Bounds.Contains(e.Location))
						{
							m_CurrentSubItem = lvsi;
							break;
						}
					}
				}
			}

			ShowActiveEditControl();
		}

		/// <summary>
		/// Shows the edit control for the current listitem/subitem in focus
		/// </summary>
		protected void ShowActiveEditControl()
		{
			cbCodes.Visible = false;
			cbDescriptions.Visible = false;
			tbQuantity.Visible = false;

			if (m_CurrentSubItem != null)
			{
				Control control = cbCodes;

				if (m_CurrentSubItem == m_CurrentItem.SubItems[2])
				{
					control = cbDescriptions;
				}
				else if (m_CurrentSubItem == m_CurrentItem.SubItems[3])
				{
					control = tbQuantity;
				}

				control.Visible = true;
				control.Location = new Point(m_CurrentSubItem.Bounds.X + X_OFFSET, m_CurrentSubItem.Bounds.Y + Y_OFFSET);
				control.Width = m_CurrentSubItem.Bounds.Width;
				control.Height = m_CurrentSubItem.Bounds.Height;
				control.Text = m_CurrentSubItem.Text;

				if (control is ComboBox)
				{
					//(control as ComboBox).Focus();
					(control as ComboBox).SelectAll();
				}
				else if (control is TextBox)
				{
					//(control as TextBox).Focus();
					(control as TextBox).SelectAll();
				}
			}
		}

		/// <summary>
		/// Persists the changes to the Parts listing if DialogResult.OK
		/// </summary>
		/// <param name="sender">Not used</param>
		/// <param name="e">Not used</param>
		private void PartsEditor_FormClosing(object sender, FormClosingEventArgs e)
		{
			if (DialogResult == DialogResult.OK)
			{
				// Save the listing of parts 
				m_Parts.Clear();

				foreach (ListViewItem lvi in PartsListView.Items)
				{
					Part part = lvi.Tag as Part;
					int quantity = 0;

					int.TryParse(lvi.SubItems[3].Text, out quantity);

					part.Quantity = quantity;

					m_Parts.Add(part);
				}
			}
		}

		/// <summary>
		/// Adds a new row to the ListView
		/// </summary>
		/// <param name="sender">Not used</param>
		/// <param name="e">Not used</param>
		private void Add_Click(object sender, System.EventArgs e)
		{
			// Adds new item
			PartsListView.Items.Add(new ListViewItem(new string[] { "", "", "", "0" }));
		}

		/// <summary>
		/// Updates the description/code to match the change to code/description
		/// </summary>
		/// <param name="sender">Control that was changed</param>
		/// <param name="e">Not used</param>
		private void ComboBox_SelectedValueChanged(object sender, System.EventArgs e)
		{
			ComboBox cb = sender as ComboBox;
			ComboBoxItem cbi = cb.SelectedItem as ComboBoxItem;

			m_CurrentSubItem.Text = cb.Text;

			if (cb == cbCodes)
			{
				if (cb.SelectedItem != null)
				{
					m_CurrentItem.SubItems[2].Text = cbi.Part.Description;
					m_CurrentItem.Tag = cbi.Part;
				}
			}
			else if (cb == cbDescriptions)
			{
				if (cb.SelectedItem != null)
				{
					m_CurrentItem.SubItems[1].Text = cbi.Part.Code;
					m_CurrentItem.Tag = cbi.Part;
				}
			}
		}

		/// <summary>
		/// Hides the edit control when it looses focus.
		/// </summary>
		/// <param name="sender">Control that is being left</param>
		/// <param name="e">Not used</param>
		private void Control_Leave(object sender, System.EventArgs e)
		{
			Control control = sender as Control;
			control.Visible = false;
			tbQuantity.Visible = true;
			tbQuantity.SelectAll();
		}

		/// <summary>
		/// Removes the item
		/// </summary>
		/// <param name="sender">Not used</param>
		/// <param name="e">e.Index item being clicked</param>
		private void PartsListView_ItemCheck(object sender, ItemCheckEventArgs e)
		{
			// Remove item with index
			PartsListView.Items.RemoveAt(e.Index);
		}

		/// <summary>
		/// Updates the subitem associated with the TextBox when the TextBox value changes
		/// </summary>
		/// <param name="sender">TextBox</param>
		/// <param name="e">Not used</param>
		private void Quantity_TextChanged(object sender, System.EventArgs e)
		{
			TextBox tb = sender as TextBox;
			m_CurrentSubItem.Text = tb.Text.Trim();
		}

	}
}
