using LSDComponents.DrawingModes;
using System.Drawing;
using System.Linq;
using System.Windows.Forms;
using TNT.LSD.Objects;
using WeifenLuo.WinFormsUI.Docking;

namespace LandscapeSprinklerDesigner
{
	public partial class LayoutForm : DockContent
	{
		#region Members

		private PropertyForm m_PropertyForm = null;
		private LayoutSettingsForm m_LayoutSettingsForm = null;
		private ToolStripStatusLabel m_StatusLabel = null;

		#endregion

		#region Properties

		public PropertyForm PropertyForm
		{
			get { return m_PropertyForm; }
			set
			{
				m_PropertyForm = value;
				m_PropertyForm.OnPropertyEditorChanged += new PropertyEditorChangedDelegate(OnPropertyEditorChanged);
			}
		}

		public LayoutSettingsForm LayoutSettingsForm
		{
			get { return m_LayoutSettingsForm; }
			set
			{
				m_LayoutSettingsForm = value;
				m_LayoutSettingsForm.Settings = CAD.Settings;
				m_LayoutSettingsForm.OnLayoutSettingsChanged += new PropertyEditorChangedDelegate(OnPropertyEditorChanged);
			}
		}

		public ToolStripStatusLabel StatusLabel
		{
			set
			{
				m_StatusLabel = value;
			}
		}

		public PalletTreeForm PalletTreeForm { get; set; }

		#endregion

		public LayoutForm()
		{
			InitializeComponent();
		}

		private void CAD_OnObjectsSelected(object[] objs)
		{
			if (m_PropertyForm != null)
			{
				m_PropertyForm.SelectedObjects = objs;
			}
		}

		private void OnPropertyEditorChanged()
		{
			CAD.ShowPropertyChanges();
		}

		private void CAD_TextChanged(object sender, System.EventArgs e)
		{
			if (m_StatusLabel != null)
			{
				m_StatusLabel.Text = CAD.Text;
			}
		}

		private void LayoutForm_KeyUp(object sender, KeyEventArgs e)
		{
			CAD.OnKeyUp(e);

			if (PalletTreeForm != null)
			{
				PalletTreeForm.OnKeyUp(e);
			}
		}

		private void LayoutForm_KeyDown(object sender, KeyEventArgs e)
		{
			CAD.OnKeyDown(e);
		}

		private void CADContextMenu_Opening(object sender, System.ComponentModel.CancelEventArgs e)
		{
			// Only allow context menu to show when SelectMode is being used.
			e.Cancel = !(CAD.DrawingMode is SelectMode || CAD.DrawingMode is ImageMode);

			if (!e.Cancel)
			{
				var objects = (from o in CAD.SelectedObjects where o is PalettePart select o as object).ToList();
				CADContextMenu.AddProperties(objects, CAD);
			}
		}

		private void tntPanel1_Scroll(object sender, ScrollEventArgs e)
		{
			Panel container = sender as Panel;
			Point pos = new Point(-container.AutoScrollPosition.X, -container.AutoScrollPosition.Y);
			if (e.Type == ScrollEventType.ThumbTrack)
			{
				if (e.ScrollOrientation == ScrollOrientation.HorizontalScroll)
					pos.X = e.NewValue;
				else
					pos.Y = e.NewValue;

				container.AutoScrollPosition = pos;
			}
		}
	}
}
