using LandscapeSprinklerDesigner.Events;
using LandscapeSprinklerDesigner.MenuEvents;
using LSDComponents;
using Microsoft.Win32;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Windows.Forms;
using TNT.ToolStripItemManager;
using TNT.Utilities;
using WeifenLuo.WinFormsUI.Docking;

namespace LandscapeSprinklerDesigner
{
	public partial class Main : Form
	{
		#region Members

		private ToolStripItemCheckboxGroupManager DrawingGroupManager;
		private ToolStripItemGroupManager MenuGroupManager;
		private ToolStripItemGroupManager LicensedMenuGroupManager;

		private PropertyForm m_PropertyForm = new PropertyForm();
		private LayoutForm m_LayoutForm = new LayoutForm();
		private PartsListForm m_PartsListForm = new PartsListForm();
		private PalletTreeForm m_PalletForm = new PalletTreeForm();
		private LayoutSettingsForm m_LayoutSettingsForm = new LayoutSettingsForm();
		private PDFForm m_PDFForm = null;
		private List<DockContent> dockables = null;

		#endregion

		#region Properties

		private string AssemblyTitle
		{
			get
			{
				AssemblyTitleAttribute ata = Utilities.GetAssemblyAttribute<AssemblyTitleAttribute>(Assembly.GetExecutingAssembly());
				return ata != null ? ata.Title : string.Empty;
			}
		}

		protected TNTCAD CAD { get { return m_LayoutForm.CAD; } }

		#endregion

		public Splash SplashForm { get; set; }

		public Main()
		{
			InitializeComponent();

			Global.userRegistry = new ApplicationRegistry(this, Registry.CurrentUser, Properties.Resources.Company, Properties.Resources.Application);

			dockables = new List<DockContent> { m_LayoutForm, m_LayoutSettingsForm, m_PartsListForm, m_PalletForm, m_PropertyForm };

			m_PalletForm.PalletNodeTagSelected = PalletNodeTagSelected;

			SetupDrawingGroupManager();
			SetupMenuGroupManager();
			SetupLicenseGroupManager();

			m_LayoutForm.PropertyForm = m_PropertyForm;
			m_LayoutForm.LayoutSettingsForm = m_LayoutSettingsForm;
			m_LayoutForm.StatusLabel = toolStripStatusLabel1;
			m_LayoutForm.OnKeyUp += (sender, e, activeLayer) =>
			{
				if (e.KeyCode == Keys.S && e.Modifiers == Keys.None)
				{
					if (activeLayer > 1)
					{
						m_PalletForm?.Toggle();
					}
					else
					{
						DrawingGroupManager.Toggle();
					}
				}
			};

			// Initialize to select mode.
			CAD.DrawingMode = new LSDComponents.DrawingModes.NullMode();

			CAD.OnPartsUpdated += new PartsUpdatedDelegate(m_PartsListForm.SetParts);

			CAD.OnFileNameChanged += FileNameChanged;

			var manager = new TNT.Plugin.Manager.Manager(Controls, pluginOnClickHandler, StatusBarHintChanged);

			manager.Register(Path.Combine(Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location), "plugins"));

			Global.LicenseLive.Observe(license => LicensedMenuGroupManager.LicensedChanged(license?.ExpiresOn >= DateTime.Now));
		}

		private void SetupLicenseGroupManager()
		{
			LicensedMenuGroupManager = new ToolStripItemGroupManager(toolStripStatusLabel1) { IsLicensed = IsLicensed };
			var exObj = Tuple.Create<Form, TNTCAD, LayoutSettingsForm>(this, CAD, m_LayoutSettingsForm);
			LicensedMenuGroupManager.Create<PartsListMenuEvent>(ToToolStripItemArray(PartsListButton, PartsListMenu), externalObject: Tuple.Create<DockContent, DockPanel>(m_PartsListForm, DockPanel));
			LicensedMenuGroupManager.Create<ShowPartsMenuEvent>(ToToolStripItemArray(PartsToolTipButton, PartsToolTipMenu), externalObject: exObj);
			LicensedMenuGroupManager.Create<CalculateAreaMenuEvent>(ToToolStripItemArray(AreaMenu, AreaButton, m_LayoutForm.area), externalObject: exObj);
			LicensedMenuGroupManager.Create<CalculateDistanceMenuEvent>(ToToolStripItemArray(LengthMenuItem, LengthButton, m_LayoutForm.calculateDistance), externalObject: exObj);
		}

		private void SetupMenuGroupManager()
		{
			MenuGroupManager = new ToolStripItemGroupManager(toolStripStatusLabel1);
			var exObj = Tuple.Create<Form, TNTCAD, LayoutSettingsForm>(this, CAD, m_LayoutSettingsForm);
			MenuGroupManager.Create<NewMenuEvent>(ToToolStripItemArray(NewButton, NewMenu), externalObject: exObj);
			MenuGroupManager.Create<ShowGridMenuEvent>(ToToolStripItemArray(ShowGridButton, ShowGridMenu), externalObject: exObj);
			MenuGroupManager.Create<OpenMenuEvent>(ToToolStripItemArray(OpenMenu, OpenButton), externalObject: exObj).LoadLayout = LoadLayout;
			MenuGroupManager.Create<SaveMenuEvent>(ToToolStripItemArray(SaveMenu, SaveButton), externalObject: exObj);
			MenuGroupManager.Create<SaveAsMenuEvent>(ToToolStripItemArray(SaveAsMenu), externalObject: exObj);
			MenuGroupManager.Create<ExitMenuEvent>(ToToolStripItemArray(ExitMenu), externalObject: exObj);
			MenuGroupManager.Create<UndoMenuEvent>(ToToolStripItemArray(UndoMenu, UndoButton), externalObject: exObj);
			MenuGroupManager.Create<DeleteMenuEvent>(ToToolStripItemArray(DeleteMenu, DeleteButton), externalObject: exObj);
			MenuGroupManager.Create<CloneMenuEvent>(ToToolStripItemArray(CloneMenu, CloneButton), externalObject: exObj);
			MenuGroupManager.Create<SelectAllMenuEvent>(ToToolStripItemArray(SelectAllMenu), externalObject: exObj);
			MenuGroupManager.Create<PropertiesMenuEvent>(ToToolStripItemArray(PropertiesButton, PropertiesMenu), externalObject: Tuple.Create<DockContent, DockPanel>(m_PropertyForm, DockPanel));
			MenuGroupManager.Create<PartsPaletteEvent>(ToToolStripItemArray(PaletteTreeButton, PaletteTreeMenu), externalObject: Tuple.Create<DockContent, DockPanel>(m_PalletForm, DockPanel));
			MenuGroupManager.Create<LayoutSettingsEvent>(ToToolStripItemArray(LayoutSettingsButton, LayoutSettingsMenu), externalObject: Tuple.Create<DockContent, DockPanel>(m_LayoutSettingsForm, DockPanel));
			MenuGroupManager.Create<LabelHeadsMenuEvent>(ToToolStripItemArray(LabelHeadsMenu, LabelHeadsButton), externalObject: exObj).RestoreState(Global.userRegistry);
			MenuGroupManager.Create<ShowDistanceMenuEvent>(ToToolStripItemArray(ShowDistancesMenu, ShowDistancesButton), externalObject: exObj).RestoreState(Global.userRegistry);
			MenuGroupManager.Create<ShowCoverageMenuEvent>(ToToolStripItemArray(CoverageButton, CoverageMenu), externalObject: exObj);
			MenuGroupManager.Create<SnapToGridMenuEvent>(ToToolStripItemArray(SnapToGridButton, SnapToGridMenu, m_LayoutForm.snaptogrid), externalObject: exObj).RestoreState(Global.userRegistry);
			MenuGroupManager.Create<MoveToBackMenuEvent>(ToToolStripItemArray(SendToBackButton, SendToBackMenu), externalObject: exObj);
			MenuGroupManager.Create<MoveToFrontMenuEvent>(ToToolStripItemArray(BringToFrontMenu, BringToFrontButton), externalObject: exObj);
			MenuGroupManager.Create<AlignToGridMenuEvent>(ToToolStripItemArray(AlignToGridMenu, AlignToGridButton, m_LayoutForm.space), externalObject: exObj);
			MenuGroupManager.Create<AutoSizeMenuEvent>(ToToolStripItemArray(AutoSizeMenu, AutoSizeButton), externalObject: exObj).RestoreState(Global.userRegistry);
			MenuGroupManager.Create<SpaceEquallyMenuEvent>(ToToolStripItemArray(SpaceEquallyMenu, SpaceEquallyButton, m_LayoutForm.aligntogrid), externalObject: exObj);
			MenuGroupManager.Create<RotateLeftMenuEvent>(ToToolStripItemArray(Rotate90CounterclockwiseButton, Rotate90CounterclockwiseMenu, m_LayoutForm.rotatecounter), externalObject: exObj);
			MenuGroupManager.Create<RotateRightMenuEvent>(ToToolStripItemArray(Rotate90ClockwiseButton, Rotate90ClockwiseMenu, m_LayoutForm.rotateclock), externalObject: exObj);
			MenuGroupManager.Create<Rotate180MenuEvent>(ToToolStripItemArray(Rotate180Menu, Rotate180Button, m_LayoutForm.rotate180), externalObject: exObj);
			MenuGroupManager.Create<SumGPMMenuEvent>(ToToolStripItemArray(ButtonSumGPM, m_LayoutForm.sumGpm, SumGPM), externalObject: exObj);
			MenuGroupManager.Create<CheckForUpdateMenuItem>(ToToolStripItemArray(CheckForUpdateMenu), externalObject: exObj);
			MenuGroupManager.Create<RegisterMenuEvent>(ToToolStripItemArray(RegisterMenu), externalObject: exObj);
		}

		private bool IsLicensed(bool allowMessageBox, ToolStripItemGroup itemGroup)
		{
			var license = Global.GetLicense();
			var isLicensed = false;

			if (license != null && license.ExpiresOn > DateTime.Now)
			{
				isLicensed = true;
			}
			else if (allowMessageBox)
			{
				if (license == null)
				{
					MessageBox.Show(this, "This feature is not licensed", "License Unavailable", MessageBoxButtons.OK, MessageBoxIcon.Warning);
				}
				else if (license.ExpiresOn < DateTime.Now)
				{
					MessageBox.Show(this, "The license for this feature has expired.", "License Expired", MessageBoxButtons.OK, MessageBoxIcon.Warning);
				}
			}

			return isLicensed;
		}

		private ToolStripItem[] ToToolStripItemArray(params ToolStripItem[] args) => args;

		private void SetupDrawingGroupManager()
		{
			DrawingGroupManager = new ToolStripItemCheckboxGroupManager(toolStripStatusLabel1);
			var externalObj = Tuple.Create(CAD, m_PropertyForm, m_PalletForm);
			DrawingGroupManager.CreateHome<DrawSelectEvent>(new ToolStripItem[] { selectButton }, null, externalObj);
			DrawingGroupManager.Create<DrawRectangleEvent>(new ToolStripItem[] { rectangleButton }, null, externalObj);
			DrawingGroupManager.Create<DrawLineEvent>(new ToolStripItem[] { lineButton }, null, externalObj);
			DrawingGroupManager.Create<DrawCircleEvent>(new ToolStripItem[] { circleButton }, null, externalObj);
			DrawingGroupManager.Create<DrawCurveEvent>(new ToolStripItem[] { curveButton }, null, externalObj);
			DrawingGroupManager.Create<DrawPolyEvent>(new ToolStripItem[] { polyButton }, null, externalObj);
			DrawingGroupManager.Create<DrawTextEvent>(new ToolStripItem[] { textButton }, null, externalObj);
			DrawingGroupManager.Create<DrawLegendEvent>(new ToolStripItem[] { legendButton }, null, externalObj);
		}

		private void pluginOnClickHandler(object sender, EventArgs e)
		{
			ToolStripItem tsi = sender as ToolStripItem;
			TNT.Plugin.Manager.Plugin p = tsi.Tag as TNT.Plugin.Manager.Plugin;

			//ApplicationData data = new ApplicationData("This is the name field in the app data");
			p.Execute(this, sender as ToolStripItem, new ApplicationData(CAD, this.DockPanel), Global.LicenseLive.Value?.ExpiresOn >= DateTime.Now);

			CAD.Repaint(0);
		}

		private void Main_Load(object sender, EventArgs e)
		{
			#region Restore state from Registry

			Global.userRegistry.ReadToolStripItems("MRU", OpenButton.DropDownItems);
			tbScale.Value = Global.userRegistry.ReadInteger("Scale", tbScale.Value);

			#endregion

			string configFile = Path.Combine(Path.GetDirectoryName(Application.ExecutablePath), "DockPanel.config");

			if (File.Exists(configFile))
			{
				DockPanel.LoadFromXml(configFile, new DeserializeDockContent(GetContentFromPersistString));
			}

			m_LayoutForm.Show(DockPanel);

			statusStrip1.Items.Add(new ToolStripControlHost(tbScale));
		}

		private bool HandleUnsavedChanges()
		{
			bool handled = true;

			CAD.Repaint();

			if (CAD.HasUnsavedChanges)
			{
				string msg = string.Format("The layout \"{0}\" has been modified.\nDo you want to save your changes?", string.IsNullOrEmpty(CAD.CurrentFileName) ? "Untitled" : Path.GetFileName(CAD.CurrentFileName));
				DialogResult dr = MessageBox.Show(msg, AssemblyTitle, MessageBoxButtons.YesNoCancel, MessageBoxIcon.Question);

				switch (dr)
				{
					case DialogResult.Yes:
						handled = CAD.Save(false);
						break;
					case DialogResult.No:
						break;
					default:
						handled = false;
						break;
				}
			}

			return handled;
		}

		private void Main_FormClosing(object sender, FormClosingEventArgs e)
		{
			if (!HandleUnsavedChanges())
			{
				e.Cancel = true;
				return;
			}

			string configFile = Path.Combine(Path.GetDirectoryName(Application.ExecutablePath), "DockPanel.config");
			DockPanel.SaveAsXml(configFile);

			#region Save state to Registry

			var persistedMenuEvent = (from p in MenuGroupManager.Values where p is PersistedMenuEvent select p as PersistedMenuEvent).ToList();
			persistedMenuEvent.ForEach(p => p.SaveState(Global.userRegistry));

			Global.userRegistry.WriteInteger("Scale", tbScale.Value);
			Global.userRegistry.WriteToolStripItems("MRU", OpenButton.DropDownItems);

			#endregion
		}

		private IDockContent GetContentFromPersistString(string persistString) => dockables.Find(d => d.GetType().ToString() == persistString);

		private void tbScale_ValueChanged(object sender, EventArgs e)
		{
			TrackBar tb = sender as TrackBar;

			ScaleButton.Text = string.Format("{0}%", tb.Value);
			CAD.DisplayScale = tb.Value;
		}

		private void OpenMRU_Click(object sender, ToolStripItemClickedEventArgs e)
		{
			if (HandleUnsavedChanges())
			{
				LoadLayout(e.ClickedItem.Text);
			}
		}

		public void LoadLayout(string fileName)
		{
			if (m_PDFForm != null)
			{
				// Hide the PDF form when loading a new layout
				m_PDFForm.Hide();
			}

			CAD.Open(fileName);
			m_LayoutSettingsForm.Settings = CAD.Settings;
			MenuGroupManager["Show Grid"].IfNotNull(it => { it.Checked = CAD.Settings.DrawGrid; });
		}

		private void About_Click(object sender, EventArgs e)
		{
			using (AboutBox ab = new AboutBox())
			{
				ab.ShowDialog();
			}
		}

		private void Scale_Click(object sender, EventArgs e)
		{
			ToolStripMenuItem mi = sender as ToolStripMenuItem;

			if (mi != null)
			{
				int scale = Convert.ToInt32(mi.Tag);
				tbScale.Value = scale;
			}
		}

		private void PalletNodeTagSelected(PaletteProperties paletteProperties)
		{
			// Uncheck the landscape drawing tool if there's one selected.
			var checkedItem = DrawingGroupManager.FirstOrDefault(i => i.Value.Checked).Value;
			if (checkedItem != null) { checkedItem.Checked = false; }
			CAD.SetPalletNodeTag(paletteProperties);
			m_PropertyForm.SelectedObject = paletteProperties.DrawingMode.DefaultObject;
		}

		private void StatusBarHintChanged(string hint)
		{
			toolStripStatusLabel1.Text = hint;
		}

		public void FileNameChanged(string fileName)
		{
			Text = string.Format("{0}{1}", AssemblyTitle, string.IsNullOrEmpty(fileName) ? "" : string.Format(" ({0})", fileName));

			Utilities.UpdateMRUListing(OpenButton, fileName);
		}

		private void UpgradeAvailableStatusLink_Click(object sender, EventArgs e)
		{
			ToolStripStatusLabel upgradeLabel = sender as ToolStripStatusLabel;
			Process.Start(upgradeLabel.ToolTipText);
		}

		private void OnTheWebToolStripMenuItem_Click(object sender, EventArgs e) => Process.Start("https://www.LandscapeSprinklerDesigner.com");
	}
}
