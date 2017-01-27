using System.Windows.Forms;
using LSDComponents;

namespace LandscapeSprinklerDesigner
{
	public delegate void PropertyEditorChangedDelegate();
	
	public partial class PropertyForm : DockableForm
	{
		#region Properties

		public object SelectedObject 
		{
			set
			{
				if (value != null)
				{
					object[] obj = new object[1];
					obj[0] = value;
					SelectedObjects = obj;
				}
				else
				{
					SelectedObjects = null;
				}
			}
		}

		public object[] SelectedObjects 
		{ 
			set 
			{ 
				PropertyEditor.SelectedObjects = value;
			} 
		}

		#endregion

		#region Events

		public event PropertyEditorChangedDelegate OnPropertyEditorChanged;

		#endregion

		public PropertyForm()
		{
			InitializeComponent();
		}

		private void PropertyEditor_PropertyValueChanged(object s, PropertyValueChangedEventArgs e)
		{
			if (OnPropertyEditorChanged != null)
			{
				OnPropertyEditorChanged();
			}
		}
	}
}
