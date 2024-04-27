using System.Windows.Forms;

namespace TNT.LSD.Components;

public class PaletteNode : TreeNode
{
  #region Members
  #endregion

  #region Constructors

  public PaletteNode()
  {
  }

  public PaletteNode(string text)
    : base(text)
  {
  }

  public PaletteNode(PaletteNode obj)
    : base(obj.Text, obj.ImageIndex, obj.SelectedImageIndex)
  {
    StateImageIndex = obj.StateImageIndex;
    Properties = new PaletteProperties(obj.Properties);

    foreach (TreeNode node in obj.Nodes)
    {
      Nodes.Add((TreeNode)node.Clone());
    }
  }

  #endregion

  #region Properties

  public PaletteProperties Properties { get; set; }

  #endregion

  public override object Clone()
  {
    return new PaletteNode(this);
  }

  public static explicit operator SerializableNode(PaletteNode node)
  {
    SerializableNode sNode = new SerializableNode();

    if (node != null)
    {
      sNode.Text = node.Text;
      sNode.StateImageIndex = node.StateImageIndex > 1 ? 1 : node.StateImageIndex;
      sNode.Properties = node.Properties;

      if (node.ImageIndex > -1)
      {
        sNode.Image = node.TreeView.ImageList.Images[node.ImageIndex];
      }

      foreach (PaletteNode childNode in node.Nodes)
      {
        if (sNode.Nodes == null)
        {
          sNode.Nodes = new List<SerializableNode>();
        }

        sNode.Nodes.Add((SerializableNode)childNode);
      }
    }

    return sNode;
  }
}
