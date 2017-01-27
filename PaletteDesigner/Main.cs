using System;
using System.Reflection;
using System.Windows.Forms;
using LSDComponents;
using Microsoft.Win32;
using TNT.Utilities;
using TNT.Utilities.CommandManagement;

namespace PalletDesigner
{
	public partial class Main : Form
	{
		#region Private members

		private ApplicationRegistry m_ApplicationRegistry = new ApplicationRegistry(Registry.CurrentUser, "Tripp'n Technology", "LSDPalletDesigner");
		private string m_CurrentFileName = string.Empty;
		private CommandManager m_CommandManager = null;

		#endregion

		#region Properties

		public string CurrentFileName
		{
			get
			{
				return m_CurrentFileName;
			}

			set
			{
				m_CurrentFileName = value;

				AssemblyTitleAttribute ata = Utilities.GetAssemblyAttribute<AssemblyTitleAttribute>(Assembly.GetExecutingAssembly());
				Text = string.Format("{0}{1}", ata.Title, string.IsNullOrEmpty(m_CurrentFileName) ? "" : string.Format(" ({0})", m_CurrentFileName));

				Utilities.UpdateMRUListing(OpenButton, m_CurrentFileName);
			}
		}

		#endregion

		public Main()
		{
			InitializeComponent();

			#region Command Manager Initialization

			m_CommandManager = new CommandManager(HintChanged);

			Command cmd = m_CommandManager.Create("Open", Open_Click);
			cmd.Add(OpenMenu);
			cmd.Add(OpenButton);

			cmd = m_CommandManager.Create("Save", Save_Click);
			cmd.Add(SaveMenu);
			cmd.Add(SaveButton);

			cmd = m_CommandManager.Create("SaveAs", SaveAs_Click);
			cmd.Add(SaveAsMenu);

			cmd = m_CommandManager.Create("AddSibling", AddSiblingNode_Click);
			cmd.Add(AddSiblingNodeButton);
			cmd.Add(AddSiblingNodeContextMenu);

			cmd = m_CommandManager.Create("AddChild", AddChildNode_Click, c => c.Enabled = Pallet.SelectedNode != null);
			cmd.Add(AddChildNodeButton);
			cmd.Add(AddChildNodeContextMenu);

			cmd = m_CommandManager.Create("DeleteNode", DeleteNode_Click, c => c.Enabled = Pallet.SelectedNode != null);
			cmd.Add(DeleteNodeButton);
			cmd.Add(DeleteNodeContextMenu);

			cmd = m_CommandManager.Create("ShiftNodeUp", ShiftNodeUp_Click, c => c.Enabled = Pallet.SelectedNode != null && Pallet.SelectedNode.PrevNode != null);
			cmd.Add(ShiftNodeUpButton);
			cmd.Add(ShiftNodeUpContextMenu);

			cmd = m_CommandManager.Create("ShiftNodeDown", ShiftNodeDown_Click, c => c.Enabled = Pallet.SelectedNode != null && Pallet.SelectedNode.NextNode != null);
			cmd.Add(ShiftNodeDownButton);
			cmd.Add(ShiftNodeDownContextMenu);

			cmd = m_CommandManager.Create("DemoteNode", DemoteNode_Click, c => c.Enabled = Pallet.SelectedNode != null && Pallet.SelectedNode.Parent != null);
			cmd.Add(DemoteNodeButton);
			cmd.Add(DemoteNodeContextMenu);

			cmd = m_CommandManager.Create("AddImage", AddImage_Click, c => c.Enabled = Pallet.SelectedNode != null);
			cmd.Add(AssignImageButton);
			cmd.Add(AssignImageContextMenu);

			cmd = m_CommandManager.Create("Copy", c => Pallet.Copy(), c => c.Enabled = Pallet.SelectedNode != null);
			cmd.Add(CopyMenu);
			cmd.Add(CopyButton);
			cmd.Add(CopyContextMenu);

			cmd = m_CommandManager.Create("Paste", c => Pallet.Paste(), c => c.Enabled = Pallet.SelectedNode != null && Pallet.CanPaste);
			cmd.Add(PasteMenu);
			cmd.Add(PasteButton);
			cmd.Add(PasteContextMenu);

			cmd = m_CommandManager.Create("Exit", c => Close());
			cmd.Add(ExitMenu);

			#endregion
		}

		private void NodeMouseClick(object sender, TreeNodeMouseClickEventArgs e)
		{
			e.Node.Expand();
		}

		private void Pallet_BeforeCollapse(object sender, TreeViewCancelEventArgs e)
		{
			e.Node.StateImageIndex = 1;
		}

		private void Pallet_BeforeExpand(object sender, TreeViewCancelEventArgs e)
		{
			e.Node.StateImageIndex = 2;
		}

		private void Main_Load(object sender, EventArgs e)
		{
			#region Restore state from registry

			m_ApplicationRegistry.LoadFormState(this);
			m_ApplicationRegistry.ReadToolStripItems("MRU", OpenButton.DropDownItems);
			Pallet.Width = m_ApplicationRegistry.ReadInteger("PalletWidth", 200);

			#endregion
		}

		private void Main_FormClosing(object sender, FormClosingEventArgs e)
		{
			#region Save state to registry

			m_ApplicationRegistry.WriteInteger("PalletWidth", Pallet.Width);
			m_ApplicationRegistry.WriteToolStripItems("MRU", OpenButton.DropDownItems);
			m_ApplicationRegistry.SaveFormState(this);

			#endregion
		}

		private void OpenMRU_Click(object sender, ToolStripItemClickedEventArgs e)
		{
			Pallet.Load(e.ClickedItem.Text);
			CurrentFileName = e.ClickedItem.Text;
		}

		private void Pallet_AfterSelect(object sender, TreeViewEventArgs e)
		{
			PaletteNode pn = e.Node as PaletteNode;

			if (pn != null)
			{
				PropertyEditor.SelectedObject = pn.Properties;

				// Expand the Part property
				GridItem root = PropertyEditor.SelectedGridItem;

				//Get the parent
				while (root != null && root.Parent != null)
					root = root.Parent;

				if (root != null)
				{
					Expand(root, 2, 0);
				}

			}
		}

		private void Expand(GridItem parent, int depth, int level)
		{
			if (parent != null && level < depth)
			{
				foreach (GridItem g in parent.GridItems)
				{
					if (g.GridItemType == GridItemType.Property)
					{
						g.Expanded = true;
					}

					if (g.GridItems.Count > 0)
					{
						Expand(g, depth, level + 1);
					}
				}
			}
		}

		private void Pallet_MouseDown(object sender, MouseEventArgs e)
		{
			if (e.Button == System.Windows.Forms.MouseButtons.Right)
			{
				Pallet.SelectedNode = Pallet.GetNodeAt(e.X, e.Y);
			}
		}

		private void ExpandAll_Click(object sender, EventArgs e)
		{
			Pallet.ExpandAll();
		}

		#region Command Events

		private void HintChanged(string hint)
		{
			StatusToolTip.Text = hint;
		}

		private void Open_Click(Command cmd)
		{
			if (OpenDialog.ShowDialog() == System.Windows.Forms.DialogResult.OK)
			{
				Pallet.Load(OpenDialog.FileName);
				CurrentFileName = OpenDialog.FileName;
			}
		}

		private void Save_Click(Command cmd)
		{
			if (!string.IsNullOrEmpty(m_CurrentFileName))
			{
				Pallet.Save(m_CurrentFileName);
			}
			else
			{
				SaveAs_Click(cmd);
			}
		}

		private void SaveAs_Click(Command cmd)
		{
			if (SaveDialog.ShowDialog() == System.Windows.Forms.DialogResult.OK)
			{
				Pallet.Save(SaveDialog.FileName);
				CurrentFileName = SaveDialog.FileName;
			}
		}

		#region Context Menu

		private void AddSiblingNode_Click(Command cmd)
		{
			PaletteNode newNode = Pallet.AddSiblingNode();
			PropertyEditor.SelectedObject = newNode.Properties;
		}

		private void AddChildNode_Click(Command cmd)
		{
			PaletteNode newNode = Pallet.AddChildNode();
			PropertyEditor.SelectedObject = newNode.Properties;
		}

		private void DeleteNode_Click(Command cmd)
		{
			Pallet.DeleteSelectedNode();
		}

		private void ShiftNodeUp_Click(Command cmd)
		{
			Pallet.ShiftSelectedNodeUp();
		}

		private void ShiftNodeDown_Click(Command cmd)
		{
			Pallet.ShiftSelectedNodeDown();
		}

		private void DemoteNode_Click(Command cmd)
		{
			Pallet.DemoteSelectedNode();
		}

		private void AddImage_Click(Command cmd)
		{
			if (AddImageDialog.ShowDialog() == System.Windows.Forms.DialogResult.OK)
			{
				Pallet.AddImage(AddImageDialog.FileName);
			}
		}

		#endregion

		#endregion
	}
}

