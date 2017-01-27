using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Windows.Forms;
using System.Xml;
using TNT.Configuration;
using TNT.LSD.Objects;
using TNT.Utilities;

namespace LSDComponents
{
	public partial class PalletNodeTreeView : TreeView
	{
		private PaletteNode m_NodeToCopy = null;
		private TreeNode m_LastDrawingModeNode = null;
		private TreeNode m_SourceNode = null;
		private Type[] m_ExpectedTypes = null;

		private Type[] ExpectedTypes
		{
			get
			{
				if (m_ExpectedTypes == null)
				{
					List<Type> types = new List<Type>();

					types.AddRange(Utilities.GetTypes(string.Concat(Application.StartupPath, "\\", "LSDComponents.dll"), t =>
					{
						return t.Namespace == "LSDComponents.DrawingModes" && !t.IsAbstract && !t.IsGenericType && t.IsVisible;
					}));

					types.AddRange(Utilities.GetTypes(string.Concat(Application.StartupPath, "\\", "TNT.LSD.Objects.dll"), t =>
					{
						return t.Namespace == "TNT.LSD.Objects" && !t.IsAbstract && t.InheritsFrom(typeof(TNTObject));
					}));

					m_ExpectedTypes = types.ToArray();
				}

				return m_ExpectedTypes;
			}
		}

		public bool CanPaste { get { return m_NodeToCopy != null; } }

		public PalletNodeTreeView()
		{
			InitializeComponent();
		}

		public PalletNodeTreeView(IContainer container)
		{
			container.Add(this);

			InitializeComponent();
		}

		protected override void OnCreateControl()
		{
			base.OnCreateControl();

			try
			{
				PaletteNodeConfigurationSection pnConfigSec = PaletteNodeConfigurationSection.Create();
				Load(pnConfigSec.PaletteFile.Value);
			}
			catch
			{
			}
		}

		#region Drag and Drop

		protected override void OnItemDrag(ItemDragEventArgs e)
		{
			base.OnItemDrag(e);

			m_SourceNode = (TreeNode)e.Item;
			DoDragDrop(e.Item.ToString(), DragDropEffects.Move | DragDropEffects.Copy);
		}

		protected override void OnDragEnter(DragEventArgs e)
		{
			base.OnDragEnter(e);

			if (e.Data.GetDataPresent(DataFormats.Text))
			{
				e.Effect = DragDropEffects.Move;
			}
			else
			{
				e.Effect = DragDropEffects.None;
			}
		}

		protected override void OnDragDrop(DragEventArgs e)
		{
			base.OnDragDrop(e);

			Point pos = PointToClient(new Point(e.X, e.Y));
			TreeNode targetNode = GetNodeAt(pos);

			if (targetNode != null && targetNode != m_SourceNode)
			{
				if (m_SourceNode.Parent != null && m_SourceNode.Parent.Nodes.Count == 1)
				{
					m_SourceNode.Parent.StateImageIndex = 0;
				}

				m_SourceNode.Remove();

				if (m_SourceNode.Index > targetNode.Index)
				{
					targetNode.Nodes.Insert(targetNode.Index, m_SourceNode);
				}
				else
				{
					targetNode.Nodes.Insert(targetNode.Index + 1, m_SourceNode);
				}

				targetNode.Expand();
				targetNode.StateImageIndex = targetNode.IsExpanded ? 2 : 1;

				Invalidate();
			}
		}

		protected override void OnDragOver(DragEventArgs e)
		{
			base.OnDragOver(e);

			Point pos = PointToClient(new Point(e.X, e.Y));
			TreeNode targetNode = GetNodeAt(pos);

			e.Effect = targetNode == null || m_SourceNode == targetNode || m_SourceNode.IsParent(targetNode) ? DragDropEffects.None : DragDropEffects.Move;
		}

		#endregion

		public void Save(string fileName)
		{
			string serialData = Serialize();

			XmlDocument xmlDoc = new XmlDocument();
			xmlDoc.LoadXml(serialData);
			xmlDoc.Save(fileName);
		}

		public void Load(string fileName)
		{
			try
			{
				XmlDocument xmlDoc = new XmlDocument();
				xmlDoc.Load(fileName);

				Deserialize(xmlDoc.InnerXml);
			}
			catch (Exception ex)
			{
				MessageBox.Show(ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
			}
		}

		public new void OnKeyUp(KeyEventArgs e)
		{
			base.OnKeyUp(e);

			if (e.KeyCode == Keys.S && e.Modifiers == Keys.None)
			{
				DrawingModes.DrawingMode drawingMode = (SelectedNode != null && (SelectedNode is PaletteNode)) ? (SelectedNode as PaletteNode).Properties.DrawingMode : null;

				if (drawingMode != null && drawingMode.GetType() != typeof(DrawingModes.SelectMode))
				{
					// Get SelectMode node
					TreeNode selectModeNode = GetSelectModeNode();

					if (selectModeNode != null)
					{
						m_LastDrawingModeNode = SelectedNode;
						SelectedNode = selectModeNode;
					}
				}
				else if (m_LastDrawingModeNode != null)
				{
					SelectedNode = m_LastDrawingModeNode;
				}
			}
		}

		protected override void OnAfterLabelEdit(NodeLabelEditEventArgs e)
		{
			base.OnAfterLabelEdit(e);

			PaletteNode pn = e.Node as PaletteNode;

			if (pn != null && pn.Properties.DrawingMode.DefaultObject is PalettePart)
			{
				(pn.Properties.DrawingMode.DefaultObject as PalettePart).LegendText = e.Label;
			}
		}

		protected override void OnKeyDown(KeyEventArgs e)
		{
			base.OnKeyDown(e);

			if (LabelEdit && e.KeyCode == Keys.F2 && SelectedNode != null)
			{
				SelectedNode.BeginEdit();
			}
		}

		#region Node Manipulation Methods

		public void AddImage(string fileName)
		{
			Image image = Image.FromFile(fileName);

			ImageList.Images.Add(image);
			SelectedNode.ImageIndex = ImageList.Images.Count - 1;
			SelectedNode.SelectedImageIndex = SelectedNode.ImageIndex;

			PaletteNode pn = SelectedNode as PaletteNode;

			if (pn != null)
			{
				PalettePart pp = pn.Properties.DrawingMode.DefaultObject as PalettePart;

				if (pp != null)
				{
					pp.LegendImage = image;

					if (pp.Image == null)
					{
						pp.Image = image;
					}
				}
			}
		}

		public PaletteNode AddSiblingNode()
		{
			PaletteNode newNode = null;
			TreeNode parentNode = null;

			if (SelectedNode != null)
			{
				// See if parent exists
				parentNode = SelectedNode.Parent;
			}

			if (parentNode != null)
			{
				// Create child
				newNode = new PaletteNode("New Node");
				parentNode.Nodes.Insert(0, newNode);
			}
			else if (SelectedNode != null)
			{
				// selectedNode is root node. Create new root
				newNode = new PaletteNode("New Node");
				Nodes.Insert(SelectedNode.Index, newNode);
			}
			else
			{
				newNode = new PaletteNode("New Node");
				Nodes.Add(newNode);
			}

			SelectedNode = newNode;
			newNode.StateImageIndex = 0;
			newNode.Properties = new PaletteProperties();

			return newNode;
		}

		public PaletteNode AddChildNode()
		{
			PaletteNode newNode = null;

			if (SelectedNode != null)
			{
				newNode = new PaletteNode("New Node");
				SelectedNode.Nodes.Add(newNode);
				SelectedNode.StateImageIndex = SelectedNode.IsExpanded ? 2 : 1;
			}
			else
			{
				newNode = new PaletteNode("New Node");
				Nodes.Add(newNode);
			}

			SelectedNode = newNode;
			newNode.StateImageIndex = 0;
			newNode.Properties = new PaletteProperties();

			return newNode;
		}

		public void DeleteSelectedNode()
		{
			if (SelectedNode.Parent != null && SelectedNode.Parent.Nodes.Count == 1)
			{
				SelectedNode.Parent.StateImageIndex = 0;
			}

			SelectedNode.Remove();
		}

		public void ShiftSelectedNodeUp()
		{
			TreeNode selectedNode = SelectedNode;
			TreeNode parent = selectedNode.Parent;
			TreeNodeCollection nodes = parent == null ? Nodes : parent.Nodes;

			nodes.Remove(selectedNode);
			nodes.Insert(selectedNode.Index - 1, selectedNode);
			SelectedNode = selectedNode;
		}

		public void ShiftSelectedNodeDown()
		{
			TreeNode selectedNode = SelectedNode;
			TreeNode parent = selectedNode.Parent;
			TreeNodeCollection nodes = parent == null ? Nodes : parent.Nodes;

			nodes.Remove(selectedNode);
			nodes.Insert(selectedNode.Index + 1, selectedNode);
			SelectedNode = selectedNode;
		}

		public void DemoteSelectedNode()
		{
			TreeNode selectedNode = SelectedNode;
			TreeNode parent = selectedNode.Parent;

			if (parent != null)
			{
				TreeNodeCollection nodes = parent.Parent == null ? Nodes : parent.Parent.Nodes;

				parent.Nodes.Remove(selectedNode);

				if (parent.Nodes.Count == 0)
				{
					parent.StateImageIndex = 0;
				}

				nodes.Insert(parent.Index, selectedNode);
				SelectedNode = selectedNode;
			}
		}

		public void Copy()
		{
			m_NodeToCopy = (PaletteNode)SelectedNode;
		}

		public void Paste()
		{
			if (m_NodeToCopy != null)
			{
				SelectedNode.Nodes.Add(m_NodeToCopy.Clone() as TreeNode);
			}
		}

		#endregion

		#region Private methods

		private string Serialize()
		{
			List<SerializableNode> sNodes = new List<SerializableNode>();

			foreach (PaletteNode pn in Nodes)
			{
				sNodes.Add((SerializableNode)pn);
			}

			return Utilities.Serialize<List<SerializableNode>>(sNodes, ExpectedTypes);
		}

		private void Deserialize(string serialData)
		{
			SerializableNode.TreeView = this;
			List<SerializableNode> nodes = Utilities.Deserialize<List<SerializableNode>>(serialData, ExpectedTypes);

			Nodes.Clear();

			foreach (SerializableNode sNode in nodes)
			{
				Nodes.Add((PaletteNode)sNode);
			}
		}

		private TreeNode GetSelectModeNode()
		{
			TreeNode selectModeNode = null;

			if (SelectedNode != null)
			{
				TreeNode firstParent = SelectedNode.GetFirstParent();

				if (firstParent != null)
				{
					PaletteProperties paletteProperties = null;

					// Find node with SelectMode
					for (int index = 0; index < firstParent.Nodes.Count && selectModeNode == null; index++)
					{
						paletteProperties = ((PaletteNode)firstParent.Nodes[index]).Properties;

						if (paletteProperties != null && paletteProperties.DrawingMode.GetType() == typeof(DrawingModes.SelectMode))
						{
							selectModeNode = firstParent.Nodes[index];
						}
					}
				}
			}

			return selectModeNode;
		}

		#endregion
	}
}
