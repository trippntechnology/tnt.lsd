using System.Windows.Forms;

namespace LandscapeSprinklerDesigner
{
	public partial class NewLayoutDialog : Form
	{
		public NewLayoutDialog()
		{
			InitializeComponent();
		}

		public DialogResult ShowDialog(IWin32Window owner, object obj)
		{
			propertyGrid1.SelectedObject = obj;
			return this.ShowDialog(owner);
		}
	}
}
