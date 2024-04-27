using System.ComponentModel;
using System.Drawing;
using System.Drawing.Design;
using System.Windows.Forms;

namespace TNT.LSD.Components;

public partial class WaterMarkTextBox : TextBox
{
  private string _WaterMarkText { get; set; }

  [Editor(typeof(System.ComponentModel.Design.MultilineStringEditor), typeof(UITypeEditor))]
  public string WaterMarkText
  {
    get { return _WaterMarkText; }
    set
    {
      _WaterMarkText = value;
      WaterMarkTextBox_LostFocus(this, null);
    }
  }

  [DefaultValue(typeof(Color), "LightGray")]
  public Color WaterMarkColor { get; set; } = Color.LightGray;
  [DefaultValue(typeof(Color), "Black")]
  public Color TextColor { get; set; } = Color.Black;

  public WaterMarkTextBox()
  {
    InitializeComponent();

    GotFocus += WaterMarkTextBox_GotFocus;
    LostFocus += WaterMarkTextBox_LostFocus;
  }

  private void WaterMarkTextBox_LostFocus(object sender, System.EventArgs e)
  {
    if (String.IsNullOrEmpty(Text) || Text == WaterMarkText)
    {
      Text = WaterMarkText;
      ForeColor = WaterMarkColor;
    }
    else
    {
      ForeColor = TextColor;
    }
  }

  private void WaterMarkTextBox_GotFocus(object sender, System.EventArgs e)
  {
    if (String.IsNullOrEmpty(Text) || Text == WaterMarkText)
    {
      Text = string.Empty;
      ForeColor = TextColor;
    }
  }
}
