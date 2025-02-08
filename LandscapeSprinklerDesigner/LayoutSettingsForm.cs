using System.ComponentModel;
using System.Windows.Forms;

namespace LandscapeSprinklerDesigner
{
	//public delegate void PropertyEditorChangedDelegate();
	
	public partial class LayoutSettingsForm : DockableForm
	{
    #region Properties

    [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
    public object Settings
		{
			set
			{
				object[] obj = new object[1];
				obj[0] = value;
				SettingsEditor.SelectedObjects = obj;
			}
		}
		#endregion

		#region Events

		public event PropertyEditorChangedDelegate OnLayoutSettingsChanged;

		#endregion

		public LayoutSettingsForm()
		{
			InitializeComponent();
		}

		private void SettingsEditor_PropertyValueChanged(object s, PropertyValueChangedEventArgs e)
		{
			if (OnLayoutSettingsChanged != null)
			{
				OnLayoutSettingsChanged();
			}
		}
	}
}
