using LandscapeSprinklerDesigner.Events;
using LandscapeSprinklerDesigner.MenuEvents;
using LSDComponents;
using Microsoft.Win32;
using System;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Runtime.InteropServices;
using System.Threading;
using System.Windows.Forms;
using TNT.Configuration;
using TNT.ToolStripItemManager;
using TNT.Utilities;
using TNT.Utilities.CommandManagement;
using TNT.Web;
using WeifenLuo.WinFormsUI.Docking;

namespace LandscapeSprinklerDesigner
{
	public partial class Main : Form
	{
		#region Members

		private ToolStripItemCheckboxGroupManager toolStripItemDrawingGroupManager;
		private ToolStripItemGroupManager toolStripItemMenuGroupManager;
		private CommandManager m_CommandManager = null;

		private PropertyForm m_PropertyForm = new PropertyForm();
		private LayoutForm m_LayoutForm = new LayoutForm();
		private PartsListForm m_PartsListForm = new PartsListForm();
		private PalletTreeForm m_PalletForm = new PalletTreeForm();
		private DeserializeDockContent m_DeserializeDockContent;
		private static ApplicationRegistry m_ApplicationRegistry = new ApplicationRegistry(Registry.CurrentUser, "Tripp'n Technology", "LandscapeSprinklerDesigner");
		private LayoutSettingsForm m_LayoutSettingsForm = new LayoutSettingsForm();
		private PDFForm m_PDFForm = null;

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

		public static ApplicationRegistry Registery { get { return m_ApplicationRegistry; } }

		protected TNTCAD CAD { get { return m_LayoutForm.CAD; } }

#if !DISABLE_REGISTRATION

		private bool IsAuthorized
		{
			get
			{
				if (m_IsAuthorized == null)
				{
					string path = Assembly.GetExecutingAssembly().Location;
					path = Path.GetDirectoryName(path);
					RegistrationKey regKey = null;

					try
					{
						regKey = Registration.GetRegistrationKey(Path.Combine(path, "license.txt"));
					}
					catch (FileNotFoundException) { }

					m_IsAuthorized = false;

					if (regKey != null && !string.IsNullOrEmpty(regKey.License) && !string.IsNullOrEmpty(regKey.Authorization))
					{
						string volSerialNumber = Registration.GetVolumeSerialNumber();
						GuidAttribute attr = Utilities.GetAssemblyAttribute<GuidAttribute>(Assembly.GetExecutingAssembly());

						string hash = Registration.GenerateSHA1Hash(string.Concat(volSerialNumber, attr.Value, regKey.License));

						m_IsAuthorized = regKey.Authorization == hash;
					}

					if (!(bool)m_IsAuthorized)
					{
						// Use to hide when not licensed
						m_CommandManager["PartsList"].Visible = false;
						(m_CommandManager["PartsList"].Tag as DockContent).Hide();
					}
				}

				return (bool)m_IsAuthorized;
			}
		}

#else

		private bool IsAuthorized
		{
			get { return true; }
		}

#endif

		#endregion

		public Splash SplashForm { get; set; }

		public Main()
		{
			InitializeComponent();

			m_PalletForm.PalletNodeTagSelected = PalletNodeTagSelected;

			SetupDrawingGroupManager();
			SetupMenuGroupManager();

			m_DeserializeDockContent = new DeserializeDockContent(GetContentFromPersistString);
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
						toolStripItemDrawingGroupManager.Toggle();
					}
				}
			};

			// Initialize to select mode.
			CAD.DrawingMode = new LSDComponents.DrawingModes.NullMode();

			CAD.OnPartsUpdated += new PartsUpdatedDelegate(m_PartsListForm.SetParts);

			CAD.OnFileNameChanged += FileNameChanged;

			var manager = new TNT.Plugin.Manager.Manager(Controls, pluginOnClickHandler, StatusBarHintChanged);

			manager.Register(Path.Combine(Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location), "plugins"));
		}

		private void SetupMenuGroupManager()
		{
			toolStripItemMenuGroupManager = new ToolStripItemGroupManager(toolStripStatusLabel1);
			var exObj = Tuple.Create<Form, TNTCAD, LayoutSettingsForm>(this, CAD, m_LayoutSettingsForm);
			toolStripItemMenuGroupManager.Create<NewMenuEvent>(ToToolStripItemArray(NewButton, NewMenu), externalObject: exObj);
			toolStripItemMenuGroupManager.Create<ShowGridMenuEvent>(ToToolStripItemArray(ShowGridButton, ShowGridMenu), externalObject: exObj);
			toolStripItemMenuGroupManager.Create<OpenMenuEvent>(ToToolStripItemArray(OpenMenu, OpenButton), externalObject: exObj).LoadLayout = LoadLayout;
			toolStripItemMenuGroupManager.Create<SaveMenuEvent>(ToToolStripItemArray(SaveMenu, SaveButton), externalObject: exObj);
			toolStripItemMenuGroupManager.Create<SaveAsMenuEvent>(ToToolStripItemArray(SaveAsMenu), externalObject: exObj);
			toolStripItemMenuGroupManager.Create<ExitMenuEvent>(ToToolStripItemArray(ExitMenu), onClick: (a, e) => { Close(); });
			toolStripItemMenuGroupManager.Create<UndoMenuEvent>(ToToolStripItemArray(UndoMenu, UndoButton), externalObject: exObj);
			toolStripItemMenuGroupManager.Create<DeleteMenuEvent>(ToToolStripItemArray(DeleteMenu, DeleteButton), externalObject: exObj);
			toolStripItemMenuGroupManager.Create<CloneMenuEvent>(ToToolStripItemArray(CloneMenu, CloneButton), externalObject: exObj);
			toolStripItemMenuGroupManager.Create<SelectAllMenuEvent>(ToToolStripItemArray(SelectAllMenu), externalObject: exObj);
			toolStripItemMenuGroupManager.Create<PropertiesMenuEvent>(ToToolStripItemArray(PropertiesButton, PropertiesMenu), externalObject: Tuple.Create<DockContent, DockPanel>(m_PropertyForm, DockPanel));
			toolStripItemMenuGroupManager.Create<PartsListMenuEvent>(ToToolStripItemArray(PartsListButton, PartsListMenu), externalObject: Tuple.Create<DockContent, DockPanel>(m_PartsListForm, DockPanel));
			toolStripItemMenuGroupManager.Create<PartsPaletteEvent>(ToToolStripItemArray(PaletteTreeButton, PaletteTreeMenu), externalObject: Tuple.Create<DockContent, DockPanel>(m_PalletForm, DockPanel));
			toolStripItemMenuGroupManager.Create<LayoutSettingsEvent>(ToToolStripItemArray(LayoutSettingsButton, LayoutSettingsMenu), externalObject: Tuple.Create<DockContent, DockPanel>(m_LayoutSettingsForm, DockPanel));
			toolStripItemMenuGroupManager.Create<LabelHeadsMenuEvent>(ToToolStripItemArray(LabelHeadsMenu, LabelHeadsButton), externalObject: exObj).RestoreState(m_ApplicationRegistry);
			toolStripItemMenuGroupManager.Create<ShowDistanceMenuEvent>(ToToolStripItemArray(ShowDistancesMenu, ShowDistancesButton), externalObject: exObj).RestoreState(m_ApplicationRegistry);
			toolStripItemMenuGroupManager.Create<ShowCoverageMenuEvent>(ToToolStripItemArray(CoverageButton, CoverageMenu), externalObject: exObj);
			toolStripItemMenuGroupManager.Create<ShowPartsMenuEvent>(ToToolStripItemArray(PartsToolTipButton, PartsToolTipMenu), externalObject: exObj);
			toolStripItemMenuGroupManager.Create<SnapToGridMenuEvent>(ToToolStripItemArray(SnapToGridButton, SnapToGridMenu, m_LayoutForm.snaptogrid), externalObject: exObj).RestoreState(m_ApplicationRegistry);
			toolStripItemMenuGroupManager.Create<MoveToBackMenuEvent>(ToToolStripItemArray(SendToBackButton, SendToBackMenu), externalObject: exObj);
			toolStripItemMenuGroupManager.Create<MoveToFrontMenuEvent>(ToToolStripItemArray(BringToFrontMenu, BringToFrontButton), externalObject: exObj);
			toolStripItemMenuGroupManager.Create<AlignToGridMenuEvent>(ToToolStripItemArray(AlignToGridMenu, AlignToGridButton, m_LayoutForm.space), externalObject: exObj);
			toolStripItemMenuGroupManager.Create<AutoSizeMenuEvent>(ToToolStripItemArray(AutoSizeMenu, AutoSizeButton), externalObject: exObj).RestoreState(m_ApplicationRegistry);
			toolStripItemMenuGroupManager.Create<SpaceEquallyMenuEvent>(ToToolStripItemArray(SpaceEquallyMenu, SpaceEquallyButton, m_LayoutForm.aligntogrid), externalObject: exObj);
			toolStripItemMenuGroupManager.Create<CalculateAreaMenuEvent>(ToToolStripItemArray(AreaMenu, AreaButton, m_LayoutForm.area), externalObject: exObj);
			toolStripItemMenuGroupManager.Create<CalculateDistanceMenuEvent>(ToToolStripItemArray(LengthMenuItem, LengthButton, m_LayoutForm.calculateDistance), externalObject: exObj);
			toolStripItemMenuGroupManager.Create<RotateLeftMenuEvent>(ToToolStripItemArray(Rotate90CounterclockwiseButton, Rotate90CounterclockwiseMenu, m_LayoutForm.rotatecounter), externalObject: exObj);
			toolStripItemMenuGroupManager.Create<RotateRightMenuEvent>(ToToolStripItemArray(Rotate90ClockwiseButton, Rotate90ClockwiseMenu, m_LayoutForm.rotateclock), externalObject: exObj);
			toolStripItemMenuGroupManager.Create<Rotate180MenuEvent>(ToToolStripItemArray(Rotate180Menu, Rotate180Button, m_LayoutForm.rotate180), externalObject: exObj);
			toolStripItemMenuGroupManager.Create<SumGPMMenuEvent>(ToToolStripItemArray(ButtonSumGPM, m_LayoutForm.sumGpm, SumGPM), externalObject: exObj);
			toolStripItemMenuGroupManager.Create<ShowLandscapeMenuEvent>(ToToolStripItemArray(LayoutButton, LayoutMenu), externalObject: exObj);
			toolStripItemMenuGroupManager.Create<CheckForUpdateMenuItem>(ToToolStripItemArray(CheckForUpdateMenu), externalObject: exObj);
			toolStripItemMenuGroupManager.Create<RegisterMenuEvent>(ToToolStripItemArray(RegisterMenu), externalObject: exObj);
		}

		private ToolStripItem[] ToToolStripItemArray(params ToolStripItem[] args) => args;

		private void SetupDrawingGroupManager()
		{
			toolStripItemDrawingGroupManager = new ToolStripItemCheckboxGroupManager(toolStripStatusLabel1);
			var externalObj = Tuple.Create(CAD, m_PropertyForm, m_PalletForm);
			toolStripItemDrawingGroupManager.CreateHome<DrawSelectEvent>(new ToolStripItem[] { selectButton }, null, externalObj);
			toolStripItemDrawingGroupManager.Create<DrawRectangleEvent>(new ToolStripItem[] { rectangleButton }, null, externalObj);
			toolStripItemDrawingGroupManager.Create<DrawLineEvent>(new ToolStripItem[] { lineButton }, null, externalObj);
			toolStripItemDrawingGroupManager.Create<DrawCircleEvent>(new ToolStripItem[] { circleButton }, null, externalObj);
			toolStripItemDrawingGroupManager.Create<DrawCurveEvent>(new ToolStripItem[] { curveButton }, null, externalObj);
			toolStripItemDrawingGroupManager.Create<DrawPolyEvent>(new ToolStripItem[] { polyButton }, null, externalObj);
			toolStripItemDrawingGroupManager.Create<DrawTextEvent>(new ToolStripItem[] { textButton }, null, externalObj);
			toolStripItemDrawingGroupManager.Create<DrawLegendEvent>(new ToolStripItem[] { legendButton }, null, externalObj);
		}

		private void pluginOnClickHandler(object sender, EventArgs e)
		{
			ToolStripItem tsi = sender as ToolStripItem;
			TNT.Plugin.Manager.Plugin p = tsi.Tag as TNT.Plugin.Manager.Plugin;

			//ApplicationData data = new ApplicationData("This is the name field in the app data");
			p.Execute(this, sender as ToolStripItem, new ApplicationData(CAD, this.DockPanel), true);

			CAD.Repaint(0);
		}

		private void Main_Load(object sender, EventArgs e)
		{
			#region Restore state from Registery

			m_ApplicationRegistry.LoadFormState(this);
			m_ApplicationRegistry.ReadToolStripItems("MRU", OpenButton.DropDownItems);
			tbScale.Value = m_ApplicationRegistry.ReadInteger("Scale", tbScale.Value);

			#endregion

			string configFile = Path.Combine(Path.GetDirectoryName(Application.ExecutablePath), "DockPanel.config");

			if (File.Exists(configFile))
			{
				DockPanel.LoadFromXml(configFile, m_DeserializeDockContent);
			}

			m_LayoutForm.Show(DockPanel);

			statusStrip1.Items.Add(new ToolStripControlHost(tbScale));

			#region Check for update thread

			new Thread(o =>
				{
					try
					{
						ToolStripStatusLabel upgradeLabel = o as ToolStripStatusLabel;

						CheckVersion((v, a) =>
							{
								if (a != null)
								{
									Version latestVer = new Version(a.Version);

									if (latestVer > v)
									{
										upgradeLabel.Text = string.Format("Download version {0}", a.Version.ToString());
										upgradeLabel.ToolTipText = a.URL.ToString();
										upgradeLabel.Visible = true;
									}
								}
							});
					}
					catch (Exception ex)
					{
						System.Diagnostics.Debug.WriteLine(ex.Message);
					}
				}).Start(UpgradeAvailableStatusLink);

			#endregion
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

			var persistedMenuEvent = (from p in toolStripItemMenuGroupManager.Values where p is PersistedMenuEvent select p as PersistedMenuEvent).ToList();
			persistedMenuEvent.ForEach(p => p.SaveState(m_ApplicationRegistry));

			m_ApplicationRegistry.WriteInteger("Scale", tbScale.Value);
			m_ApplicationRegistry.WriteToolStripItems("MRU", OpenButton.DropDownItems);
			m_ApplicationRegistry.SaveFormState(this);

			#endregion
		}

		private IDockContent GetContentFromPersistString(string persistString)
		{
			IDockContent dc = null;

			if (persistString == typeof(PropertyForm).ToString())
			{
				dc = m_PropertyForm;
			}
			else if (persistString == typeof(PartsListForm).ToString())
			{
				dc = m_PartsListForm;
			}
			else if (persistString == typeof(PalletTreeForm).ToString())
			{
				dc = m_PalletForm;
			}
			else if (persistString == typeof(LayoutSettingsForm).ToString())
			{
				dc = m_LayoutSettingsForm;
			}
			//else if (persistString == typeof(GridForm).ToString())
			//{
			//  dc = m_GridForm;
			//}
			//else
			//{
			//  // DummyDoc overrides GetPersistString to add extra information into persistString.
			//  // Any DockContent may override this value to add any needed information for deserialization.

			//  string[] parsedStrings = persistString.Split(new char[] { ',' });
			//  if (parsedStrings.Length != 3)
			//    return null;

			//  if (parsedStrings[0] != typeof(DummyDoc).ToString())
			//    return null;

			//  DummyDoc dummyDoc = new DummyDoc();
			//  if (parsedStrings[1] != string.Empty)
			//    dummyDoc.FileName = parsedStrings[1];
			//  if (parsedStrings[2] != string.Empty)
			//    dummyDoc.Text = parsedStrings[2];

			//  return dummyDoc;
			//}

			return dc;
		}

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
			toolStripItemMenuGroupManager["Show Grid"].IfNotNull(it => { it.Checked = CAD.Settings.DrawGrid; });
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
			var checkedItem = toolStripItemDrawingGroupManager.FirstOrDefault(i => i.Value.Checked).Value;
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

		private void CheckVersion(Action<Version, TNT.Web.LSD.Models.Application> action)
		{
			RESTClient restClient = XmlSection<RESTClient>.Deserialize("TNT.Web");
			GuidAttribute attr = Utilities.GetAssemblyAttribute<GuidAttribute>(Assembly.GetExecutingAssembly());
			AssemblyFileVersionAttribute verAttr = Utilities.GetAssemblyAttribute<AssemblyFileVersionAttribute>(Assembly.GetExecutingAssembly());
			TNT.Web.LSD.Models.Response<TNT.Web.LSD.Models.Application> appResponse = restClient.Get<TNT.Web.LSD.Models.Response<TNT.Web.LSD.Models.Application>>(string.Format("Application/{0}", attr.Value));

			if (action != null && appResponse.Success)
			{
				action(new Version(verAttr.Version), appResponse.Payload);
			}
		}

		private void landscapeSprinklerDesignOnTheWebToolStripMenuItem_Click(object sender, EventArgs e)
		{
			Process.Start("http://LandscapeSprinklerDesign.com");
		}
	}
}
