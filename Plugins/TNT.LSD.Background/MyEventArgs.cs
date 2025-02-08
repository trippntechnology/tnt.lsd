using System.Windows.Forms;

namespace TNT.LSD.Background;

class MyEventArgs : EventArgs
{
  public DialogResult DialogResult { get; set; }
}
