using LSDComponents;
using Microsoft.Win32;
using PaletteDesigner.Events;
using System.Reflection;
using TNT.ToolStripItemManager;
using TNT.Utilities;

namespace PalletDesigner
{
  public partial class Main : Form
  {
    #region Private members

    private ToolStripItemGroupManager toolStripItemGroupManager;
    private ApplicationRegistry m_ApplicationRegistry;
    private string m_CurrentFileName = string.Empty;

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

      m_ApplicationRegistry = new ApplicationRegistry(this, Registry.CurrentUser, "Tripp'n Technology", "LSDPalletDesigner");

      InitializeComponent();

      #region Events

      toolStripItemGroupManager = new ToolStripItemGroupManager(StatusToolTip);

      toolStripItemGroupManager.Create<Open>(new ToolStripItem[] { OpenButton, OpenMenu }, OpenButton.Image, this);
      toolStripItemGroupManager.Create<Save>(new ToolStripItem[] { SaveButton, SaveMenu }, externalObject: this);
      toolStripItemGroupManager.Create<SaveAs>(new ToolStripItem[] { SaveAsMenu }, externalObject: this);
      toolStripItemGroupManager.Create<Exit>(new ToolStripItem[] { ExitMenu });
      toolStripItemGroupManager.Create<Copy>(new ToolStripItem[] { CopyMenu, CopyButton, CopyContextMenu }, CopyContextMenu.Image, Pallet);
      toolStripItemGroupManager.Create<Paste>(new ToolStripItem[] { PasteButton, PasteContextMenu, PasteMenu }, PasteContextMenu.Image, Pallet);
      toolStripItemGroupManager.Create<AddSiblingNode>(new ToolStripItem[] { AddSiblingNodeButton, AddSiblingNodeContextMenu }, AddSiblingNodeContextMenu.Image,
        new Tuple<object, object>(Pallet, PropertyEditor));
      toolStripItemGroupManager.Create<AddChildNode>(new ToolStripItem[] { AddChildNodeButton, AddChildNodeContextMenu }, AddChildNodeContextMenu.Image,
        new Tuple<object, object>(Pallet, PropertyEditor));
      toolStripItemGroupManager.Create<DeleteNode>(new ToolStripItem[] { DeleteNodeButton, DeleteNodeContextMenu }, DeleteNodeContextMenu.Image,
        new Tuple<object, object>(Pallet, PropertyEditor));
      toolStripItemGroupManager.Create<ShiftNodeUp>(new ToolStripItem[] { ShiftNodeUpButton, ShiftNodeUpContextMenu }, ShiftNodeUpContextMenu.Image,
        new Tuple<object, object>(Pallet, PropertyEditor));
      toolStripItemGroupManager.Create<ShiftNodeDown>(new ToolStripItem[] { ShiftNodeDownButton, ShiftNodeDownContextMenu }, ShiftNodeDownContextMenu.Image,
        new Tuple<object, object>(Pallet, PropertyEditor));
      toolStripItemGroupManager.Create<DemoteNode>(new ToolStripItem[] { DemoteNodeButton, DemoteNodeContextMenu }, DemoteNodeContextMenu.Image,
        new Tuple<object, object>(Pallet, PropertyEditor));
      toolStripItemGroupManager.Create<AddImage>(new ToolStripItem[] { AssignImageButton, AssignImageContextMenu }, AssignImageContextMenu.Image,
        new Tuple<object, object>(Pallet, AddImageDialog));
      toolStripItemGroupManager.Create<ExportImage>(new ToolStripItem[] { exportImageToolStripMenuItem },
        externalObject: new Tuple<object, object>(Pallet, PropertyEditor));

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

      m_ApplicationRegistry.ReadToolStripItems("MRU", OpenButton.DropDownItems);
      Pallet.Width = m_ApplicationRegistry.ReadInteger("PalletWidth", 200);

      #endregion
    }

    private void Main_FormClosing(object sender, FormClosingEventArgs e)
    {
      #region Save state to registry

      m_ApplicationRegistry.WriteInteger("PalletWidth", Pallet.Width);
      m_ApplicationRegistry.WriteToolStripItems("MRU", OpenButton.DropDownItems);

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
  }
}

