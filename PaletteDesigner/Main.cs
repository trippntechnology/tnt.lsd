using Microsoft.Win32;
using PaletteDesigner.Events;
using System.ComponentModel;
using System.Reflection;
using TNT.Commons;
using TNT.LSD.Components;
using TNT.ToolStripItemManager;
using TNT.Utilities;

namespace PalletDesigner;

public partial class Main : Form
{
    private ToolStripItemGroupManager toolStripItemGroupManager;
    private ApplicationRegistry m_ApplicationRegistry;
    private string m_CurrentFileName = string.Empty;

    [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
    public string CurrentFileName
    {
        get
        {
            return m_CurrentFileName;
        }

        set
        {
            m_CurrentFileName = value;

            var ata = Utilities.GetAssemblyAttribute<AssemblyTitleAttribute>(Assembly.GetExecutingAssembly());
            ata?.Also(it => Text = string.Format("{0}{1}", it.Title, string.IsNullOrEmpty(m_CurrentFileName) ? "" : string.Format(" ({0})", m_CurrentFileName)));

            Utilities.UpdateMRUListing(OpenButton, m_CurrentFileName);
        }
    }

    public Main()
    {
        m_ApplicationRegistry = new ApplicationRegistry(this, Registry.CurrentUser, "Tripp'n Technology", "LSDPalletDesigner");

        InitializeComponent();

        toolStripItemGroupManager = new ToolStripItemGroupManager()
        {
            OnClick = toolStripItemGroup =>
            {
                if (toolStripItemGroup is AppToolStripItemGroup appToolStripItemGroup)
                {
                    appToolStripItemGroup.OnMouseClick(this);
                }
            },
            OnIdle = (toolStripItemGroup, args) =>
            {
                if (toolStripItemGroup is NodeToolStripItemGroup nodeToolStripItemGroup)
                {
                    nodeToolStripItemGroup.OnApplicationIdle(this);
                }
            },
            OnToolTipChange = toolTipText => { StatusToolTip.Text = toolTipText; }
        };

        toolStripItemGroupManager.Create<Open>(new ToolStripItem[] { OpenButton, OpenMenu });
        toolStripItemGroupManager.Create<Save>(new ToolStripItem[] { SaveButton, SaveMenu });
        toolStripItemGroupManager.Create<SaveAs>(new ToolStripItem[] { SaveAsMenu });
        toolStripItemGroupManager.Create<Exit>(new ToolStripItem[] { ExitMenu });
        toolStripItemGroupManager.Create<Copy>(new ToolStripItem[] { CopyMenu, CopyButton, CopyContextMenu });
        toolStripItemGroupManager.Create<Paste>(new ToolStripItem[] { PasteButton, PasteContextMenu, PasteMenu });
        toolStripItemGroupManager.Create<AddSiblingNode>(new ToolStripItem[] { AddSiblingNodeButton, AddSiblingNodeContextMenu });
        toolStripItemGroupManager.Create<AddChildNode>(new ToolStripItem[] { AddChildNodeButton, AddChildNodeContextMenu });
        toolStripItemGroupManager.Create<DeleteNode>(new ToolStripItem[] { DeleteNodeButton, DeleteNodeContextMenu });
        toolStripItemGroupManager.Create<ShiftNodeUp>(new ToolStripItem[] { ShiftNodeUpButton, ShiftNodeUpContextMenu });
        toolStripItemGroupManager.Create<ShiftNodeDown>(new ToolStripItem[] { ShiftNodeDownButton, ShiftNodeDownContextMenu });
        toolStripItemGroupManager.Create<DemoteNode>(new ToolStripItem[] { DemoteNodeButton, DemoteNodeContextMenu });
        toolStripItemGroupManager.Create<AddImage>(new ToolStripItem[] { AssignImageButton, AssignImageContextMenu });
        toolStripItemGroupManager.Create<ExportImage>(new ToolStripItem[] { exportImageToolStripMenuItem });
    }

    private void NodeMouseClick(object sender, TreeNodeMouseClickEventArgs e) => e.Node?.Also(node => node.Expand());

    private void Pallet_BeforeCollapse(object sender, TreeViewCancelEventArgs e) => e.Node?.Also(node => node.StateImageIndex = 1);

    private void Pallet_BeforeExpand(object sender, TreeViewCancelEventArgs e) => e.Node?.Also(node => node.StateImageIndex = 2);

    private void Main_Load(object sender, EventArgs e)
    {
        m_ApplicationRegistry.ReadToolStripItems("MRU", OpenButton.DropDownItems);
        Pallet.Width = m_ApplicationRegistry.ReadInteger("PalletWidth", 200);
    }

    private void Main_FormClosing(object sender, FormClosingEventArgs e)
    {
        m_ApplicationRegistry.WriteInteger("PalletWidth", Pallet.Width);
        m_ApplicationRegistry.WriteToolStripItems("MRU", OpenButton.DropDownItems);
    }

    private void OpenMRU_Click(object sender, ToolStripItemClickedEventArgs e)
    {
        e.ClickedItem?.Text?.Also(text =>
        {
            Pallet.Load(text);
            CurrentFileName = text;
        });
    }

    private void Pallet_AfterSelect(object sender, TreeViewEventArgs e)
    {
        (e.Node as PaletteNode)?.Also(pn =>
        {
            PropertyEditor.SelectedObject = pn.Properties;

            PropertyEditor.SelectedGridItem?.Also(root =>
            {

                //Get the parent
                while (root.Parent != null)
                    root = root.Parent;

                if (root != null)
                {
                    Expand(root, 2, 0);
                }
            });

        });
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

