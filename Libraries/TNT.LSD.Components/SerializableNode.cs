using Newtonsoft.Json;
using System.Drawing;
using System.Drawing.Imaging;
using System.Windows.Forms;

namespace TNT.LSD.Components;

public class SerializableNode
{
  #region Properties

  public static TreeView TreeView { get; set; }
  public List<SerializableNode> Nodes { get; set; }

  public string Text { get; set; }
  public int StateImageIndex { get; set; }
  public PaletteProperties Properties { get; set; }

  [JsonIgnore]
  public Image Image { get; set; }

  public string _Image
  {
    get
    {
      using (MemoryStream ms = new MemoryStream())
      {
        if (Image != null)
        {
          Image.Save(ms, ImageFormat.Png);
        }

        return Convert.ToBase64String(ms.ToArray());
      }
    }

    set
    {
      if (value.Length > 0)
      {
        byte[] array = Convert.FromBase64String(value);

        using (MemoryStream ms = new MemoryStream(array))
        {
          Image = Image.FromStream(ms);
        }
      }
    }
  }

  #endregion

  public static explicit operator PaletteNode(SerializableNode sNode)
  {
    PaletteNode pNode = new PaletteNode();

    if (sNode != null)
    {
      pNode.Text = sNode.Text;
      pNode.StateImageIndex = sNode.StateImageIndex;

      if (sNode.Image != null)
      {
        TreeView.ImageList.Images.Add(sNode.Image);
        pNode.ImageIndex = TreeView.ImageList.Images.Count - 1;
        pNode.SelectedImageIndex = pNode.ImageIndex;
      }

      pNode.Properties = sNode.Properties;

      if (sNode.Nodes != null)
      {
        foreach (SerializableNode sChildNode in sNode.Nodes)
        {
          pNode.Nodes.Add((PaletteNode)sChildNode);
        }
      }
    }

    return pNode;
  }
}
