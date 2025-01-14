using System.ComponentModel;
using TNT.LSD.Components;

namespace LandscapeSprinklerDesigner;

public delegate void PalletNodeTagSelectedEvent(PaletteProperties paletteProperties);

public partial class PalletTreeForm : DockableForm
{
  private TreeNode m_TreeNode;

  #region Events

  [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
  public PalletNodeTagSelectedEvent PalletNodeTagSelected { get; set; }

  #endregion

  public PalletTreeForm()
  {
    InitializeComponent();
  }

  public void Toggle() => PaletteTreeView.Toggle();

  public void UnselectAll()
  {
    PaletteTreeView.SelectedNode = null;
  }

  private void PalletTreeForm_Load(object sender, EventArgs e)
  {
    #region Load state from registry

    Description.Height = Global.userRegistry.ReadInteger(Name, "DescriptionHeight", Description.Height);

    #endregion
  }

  private void PalletTreeView_BeforeCollapse(object sender, TreeViewCancelEventArgs e)
  {
    e.Node.StateImageIndex = 1;
  }

  private void PalletTreeView_BeforeExpand(object sender, TreeViewCancelEventArgs e)
  {
    e.Node.StateImageIndex = 2;
  }

  private void PalletTreeView_NodeMouseClick(object sender, TreeNodeMouseClickEventArgs e)
  {
    e.Node.Expand();
  }

  private void splitter1_SplitterMoved(object sender, SplitterEventArgs e)
  {
    #region Save state to registry

    Global.userRegistry.WriteInteger(Name, "DescriptionHeight", Description.Height);

    #endregion
  }

  private void PalletTreeView_AfterSelect(object sender, TreeViewEventArgs e)
  {
    PaletteNode paletteNode = e.Node as PaletteNode;

    if (paletteNode != null && PalletNodeTagSelected != null)
    {
      Description.Text = paletteNode.Properties.Description;
      PalletNodeTagSelected(paletteNode.Properties);
    }
  }

  private void ExpandAllMenu_Click(object sender, EventArgs e)
  {
    PaletteTreeView.ExpandAll();
  }

  private void CollapseAllMenu_Click(object sender, EventArgs e)
  {
    PaletteTreeView.CollapseAll();
  }

  private void ExpandMenu_Click(object sender, EventArgs e)
  {
    if (m_TreeNode != null)
    {
      m_TreeNode.ExpandAll();
    }
  }

  private void CollapseMenu_Click(object sender, EventArgs e)
  {
    if (m_TreeNode != null)
    {
      m_TreeNode.Collapse();
    }
  }

  private void TreeViewContextMenu_Opening(object sender, System.ComponentModel.CancelEventArgs e)
  {
    ExpandMenu.Enabled = m_TreeNode != null;
    CollapseMenu.Enabled = m_TreeNode != null;
  }

  private void PaletteTreeView_MouseDown(object sender, MouseEventArgs e)
  {
    TreeViewHitTestInfo info = PaletteTreeView.HitTest(e.X, e.Y);
    m_TreeNode = info.Node;
  }
}
