using LandscapeSprinklerDesigner.Events;
using LSDComponents;
using Microsoft.Win32;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Runtime.InteropServices;
using System.Threading;
using System.Windows.Forms;
using TNT.Configuration;
using TNT.LSD.Objects;
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
		private CommandManager m_CommandManager = null;

		private PropertyForm m_PropertyForm = new PropertyForm();
		private LayoutForm m_LayoutForm = new LayoutForm();
		private PartsListForm m_PartsListForm = new PartsListForm();
		private PalletTreeForm m_PalletForm = new PalletTreeForm();
		private DeserializeDockContent m_DeserializeDockContent;
		private static ApplicationRegistry m_ApplicationRegistry = new ApplicationRegistry(Registry.CurrentUser, "Tripp'n Technology", "LandscapeSprinklerDesigner");
		private LayoutSettingsForm m_LayoutSettingsForm = new LayoutSettingsForm();
		private PDFForm m_PDFForm = null;

		// Set to null when enabling the registration process
		private bool? m_IsAuthorized = null;

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

			InitializeCommandManager();
			SetupDrawingGroupManager();

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

		private void InitializeCommandManager()
		{
			m_CommandManager = new CommandManager(StatusBarHintChanged);

			Command cmd = m_CommandManager.Create("New", New_Click);
			cmd.Add(NewMenu);
			cmd.Add(NewButton);

			cmd = m_CommandManager.Create("Open", Open_Click);
			cmd.Add(OpenMenu);
			cmd.Add(OpenButton);

			cmd = m_CommandManager.Create("Save", c => CAD.Save(false));
			cmd.Add(SaveMenu);
			cmd.Add(SaveButton);

			cmd = m_CommandManager.Create("SaveAs", c => CAD.Save(true));
			cmd.Add(SaveAsMenu);

			cmd = m_CommandManager.Create("Exit", c => Close());
			cmd.Add(ExitMenu);

			cmd = m_CommandManager.Create("Undo", c => CAD.Undo(), c => c.Enabled = CAD.HasUnsavedChanges && CAD.DrawingMode.UndoEnabled);
			cmd.Add(UndoMenu);
			cmd.Add(UndoButton);

			cmd = m_CommandManager.Create("Delete", c => CAD.Delete(), EnableOnSelectedUpdate);
			cmd.Add(DeleteMenu);
			cmd.Add(DeleteButton);

			cmd = m_CommandManager.Create("Clone", c => CAD.Copy(), c => c.Enabled = (from o in CAD.SelectedObjects where o.CanClone select o).ToList().Count > 0);
			cmd.Add(CloneMenu);
			cmd.Add(CloneButton);

			cmd = m_CommandManager.Create("SelectAll", c =>
			{
				if (CAD.DrawingMode.GetType() == typeof(LSDComponents.DrawingModes.SelectMode))
				{
					CAD.SelectAll();
				}
			}, c =>
			{
				c.Enabled = CAD.DrawingMode.GetType() == typeof(LSDComponents.DrawingModes.SelectMode);
			});
			cmd.Add(SelectAllMenu);

			cmd = m_CommandManager.Create("Properties", ViewMenu_Click, ToggleViewState);
			cmd.Add(PropertiesButton);
			cmd.Add(PropertiesMenu);
			cmd.Text = m_PropertyForm.Text;
			cmd.Image = m_PropertyForm.Icon.ToBitmap();
			cmd.Tag = m_PropertyForm;

			cmd = m_CommandManager.Create("PartsList", ViewMenu_Click, ToggleViewState);
			cmd.Add(PartsListButton);
			cmd.Add(PartsListMenu);
			cmd.Text = m_PartsListForm.Text;
			cmd.Image = m_PartsListForm.Icon.ToBitmap();
			cmd.Tag = m_PartsListForm;

			cmd = m_CommandManager.Create("PaletteTree", ViewMenu_Click, ToggleViewState);
			cmd.Add(PaletteTreeButton);
			cmd.Add(PaletteTreeMenu);
			cmd.Text = m_PalletForm.Text;
			cmd.Image = m_PalletForm.Icon.ToBitmap();
			cmd.Tag = m_PalletForm;
			m_PalletForm.PalletNodeTagSelected = PalletNodeTagSelected;

			cmd = m_CommandManager.Create("LayoutSettings", ViewMenu_Click, ToggleViewState);
			cmd.Add(LayoutSettingsButton);
			cmd.Add(LayoutSettingsMenu);
			cmd.Text = m_LayoutSettingsForm.Text;
			cmd.Image = m_LayoutSettingsForm.Icon.ToBitmap();
			cmd.Tag = m_LayoutSettingsForm;

			cmd = m_CommandManager.Create("AlwaysShowDistances", c =>
			{
				CAD.DrawingOptions.AlwaysShowDistances = c.Checked;
				CAD.Repaint();
			});
			cmd.Add(ShowDistancesMenu);
			cmd.Add(ShowDistancesButton);

			cmd = m_CommandManager.Create("ShowCoverage", c =>
			{
				CAD.DrawingOptions.ShowCoverage = c.Checked;
				CAD.Repaint();
			});
			cmd.Add(CoverageMenu);
			cmd.Add(CoverageButton);

			cmd = m_CommandManager.Create("LabelHeads", c =>
			{
				CAD.DrawingOptions.LabelHeads = c.Checked;
				CAD.Repaint();
			});
			cmd.Add(LabelHeadsMenu);
			cmd.Add(LabelHeadsButton);

			cmd = m_CommandManager.Create("ShowPartsToolTip");
			cmd.Add(PartsToolTipMenu);
			cmd.Add(PartsToolTipButton);
			CAD.ShowPartsToolTip = cmd;

			cmd = m_CommandManager.Create("SnapToGrid", SnapToGrid_Click);
			cmd.Add(SnapToGridMenu);
			cmd.Add(SnapToGridButton);

			cmd = m_CommandManager.Create("BringToFront", c => CAD.BringToFront(), EnableOnSelectedUpdate);
			cmd.Add(BringToFrontMenu);
			cmd.Add(BringToFrontButton);

			cmd = m_CommandManager.Create("SendToBack", c => CAD.SendToBack(), EnableOnSelectedUpdate);
			cmd.Add(SendToBackMenu);
			cmd.Add(SendToBackButton);

			cmd = m_CommandManager.Create("AlignToGrid", c => CAD.AlignToGrid(), EnableOnSelectedUpdate);
			cmd.Add(m_LayoutForm.aligntogrid);
			cmd.Add(AlignToGridMenu);
			cmd.Add(AlignToGridButton);

			cmd = m_CommandManager.Create("SpaceEqually", c => CAD.SpaceSelectedEqually(), c => c.Enabled = (from o in CAD.SelectedObjects where o is TNTPart select o).ToList().Count > 2);
			cmd.Add(m_LayoutForm.space);
			cmd.Add(SpaceEquallyMenu);
			cmd.Add(SpaceEquallyButton);

			cmd = m_CommandManager.Create("AutoPipeSize", c => CAD.DrawingOptions.AutoSizePipes = c.Checked);
			cmd.Add(AutoSizeMenu);
			cmd.Add(AutoSizeButton);

			cmd = m_CommandManager.Create("GetArea", c =>
			{
				var name = Path.GetFileNameWithoutExtension(CAD.CurrentFileName);
				double area = CAD.GetArea();
				double sqrFt = Math.Round(area, 2);
				double acre = Math.Round(area / 43560.1742405, 2);
				Clipboard.SetText($"{name}\t{DateTime.Now.ToShortDateString()}\t{acre}");
				MessageBox.Show(string.Format("{0} square feet. {1} acres", sqrFt, acre), "Selected Area");
			}, c => c.Enabled = CAD.AreaAvailable);
			cmd.Add(m_LayoutForm.area);
			cmd.Add(AreaMenu);
			cmd.Add(AreaButton);

			cmd = m_CommandManager.Create("GetLength", c =>
				{
					double length = Math.Round(CAD.GetLength(), 2);
					MessageBox.Show(string.Format("{0} feet.", length), "Selected Length");
				}, c => c.Enabled = CAD.LengthAvailable);
			cmd.Add(LengthMenuItem);
			cmd.Add(LengthButton);

			cmd = m_CommandManager.Create("CheckForUpdate", c =>
			{
				try
				{
					CheckVersion((curVer, appInfo) =>
								{
									if (appInfo != null)
									{
										Version latestVer = new Version(appInfo.Version);

										if (latestVer > curVer)
										{
											new UpdateInformation().ShowDialog(this, curVer.ToString(), latestVer.ToString(), appInfo.URL.ToString());
											//MessageBox.Show(this, string.Format("Version {0} is available for download", appInfo.Version.ToString()), "Update Available", MessageBoxButtons.OK, MessageBoxIcon.Information);
										}
										else
										{
											MessageBox.Show(this, "The latest version is installed", "Version Information", MessageBoxButtons.OK, MessageBoxIcon.Information);
										}
									}
								});
				}
				catch (Exception ex)
				{
					System.Diagnostics.Debug.WriteLine(ex.Message);
					MessageBox.Show(this, "The update server is unavailable. Please verify you're connected to the internet and try again.", "Update Server Unavailable", MessageBoxButtons.OK, MessageBoxIcon.Warning);
				}
			});
			cmd.Add(CheckForUpdateMenu);

			cmd = m_CommandManager.Create("Register", c =>
			{
				using (RegistrationForm form = new RegistrationForm())
				{
					if (form.ShowDialog(this) == System.Windows.Forms.DialogResult.OK)
					{
						m_IsAuthorized = null;
						c.Visible = !IsAuthorized;
						m_CommandManager["PartsList"].Visible = IsAuthorized;

						if (IsAuthorized)
						{
							(m_CommandManager["PartsList"].Tag as DockContent).Show();
						}
					}
				}
			});
			cmd.Add(RegisterMenu);

			cmd = m_CommandManager.Create("RotateClockwise90", RotateSelectedPaletteParts, EnablePalettePartsSelected);
			cmd.Tag = 90;
			cmd.Add(m_LayoutForm.rotateclock);
			cmd.Add(Rotate90ClockwiseMenu);
			cmd.Add(Rotate90ClockwiseButton);

			cmd = m_CommandManager.Create("RotateCounterclockwise90", RotateSelectedPaletteParts, EnablePalettePartsSelected);
			cmd.Tag = -90;
			cmd.Add(m_LayoutForm.rotatecounter);
			cmd.Add(Rotate90CounterclockwiseMenu);
			cmd.Add(Rotate90CounterclockwiseButton);

			cmd = m_CommandManager.Create("Rotate180", RotateSelectedPaletteParts, EnablePalettePartsSelected);
			cmd.Tag = 180;
			cmd.Add(m_LayoutForm.rotate180);
			cmd.Add(Rotate180Menu);
			cmd.Add(Rotate180Button);

			cmd = m_CommandManager.Create("SumGPM", (c) =>
				{
					var lateralParts = (from o in CAD.SelectedObjects where o is LateralPart select o as LateralPart).ToList();
					double gpm = 0;

					lateralParts.ForEach(p => gpm += Convert.ToDouble(p.GPM));

					MessageBox.Show(this, string.Format("GPM of selected parts: {0}", gpm), "Selected GPM");
				}, (c) =>
				{
					var lateralParts = (from o in CAD.SelectedObjects where o is LateralPart select o as LateralPart).ToList();
					c.Enabled = lateralParts.Count > 0;
				});
			cmd.Add(ButtonSumGPM);

			cmd = m_CommandManager.Create("Background", (c) =>
			{
				CAD.DrawBackground = c.Checked;
			});
			cmd.Add(LayoutButton);
			cmd.Add(LayoutMenu);
			cmd.CheckOnClick = true;

			cmd = m_CommandManager.Create("ShowGrid", (c) =>
			{
				CAD.Settings.DrawGrid = c.Checked;
			});
			cmd.Add(ShowGridButton);
			cmd.Add(ShowGridMenu);
			cmd.CheckOnClick = true;
		}

		private void Main_Load(object sender, EventArgs e)
		{
			#region Restore state from Registery

			m_ApplicationRegistry.LoadFormState(this);
			m_ApplicationRegistry.ReadToolStripItems("MRU", OpenButton.DropDownItems);
			tbScale.Value = m_ApplicationRegistry.ReadInteger("Scale", tbScale.Value);
			m_CommandManager["SnapToGrid"].Checked = m_ApplicationRegistry.ReadBoolean("SnapToGrid", true);
			SnapToGrid_Click(m_CommandManager["SnapToGrid"]);
			m_CommandManager["AutoPipeSize"].Checked = m_ApplicationRegistry.ReadBoolean("AutoPipeSize", true);
			CAD.DrawingOptions.AutoSizePipes = m_CommandManager["AutoPipeSize"].Checked;
			m_CommandManager["LabelHeads"].Checked = m_ApplicationRegistry.ReadBoolean("LabelHeads", true);
			CAD.DrawingOptions.LabelHeads = m_CommandManager["LabelHeads"].Checked;
			m_CommandManager["AlwaysShowDistances"].Checked = m_ApplicationRegistry.ReadBoolean("AlwaysShowDistances", false);
			CAD.DrawingOptions.AlwaysShowDistances = m_CommandManager["AlwaysShowDistances"].Checked;

			#endregion

			string configFile = Path.Combine(Path.GetDirectoryName(Application.ExecutablePath), "DockPanel.config");

			if (File.Exists(configFile))
			{
				DockPanel.LoadFromXml(configFile, m_DeserializeDockContent);
			}

			m_LayoutForm.Show(DockPanel);

			m_CommandManager["Properties"].Checked = !m_PropertyForm.IsHidden;
			m_CommandManager["PartsList"].Checked = !m_PartsListForm.IsHidden;
			m_CommandManager["PaletteTree"].Checked = !m_PalletForm.IsHidden;
			m_CommandManager["LayoutSettings"].Checked = !m_LayoutSettingsForm.IsHidden;

			statusStrip1.Items.Add(new ToolStripControlHost(tbScale));

			m_CommandManager["Register"].Visible = !IsAuthorized;

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

			m_ApplicationRegistry.WriteBoolean("AutoPipeSize", m_CommandManager["AutoPipeSize"].Checked);
			m_ApplicationRegistry.WriteBoolean("AlwaysShowDistances", m_CommandManager["AlwaysShowDistances"].Checked);
			m_ApplicationRegistry.WriteBoolean("LabelHeads", m_CommandManager["LabelHeads"].Checked);
			m_ApplicationRegistry.WriteInteger("Scale", tbScale.Value);
			m_ApplicationRegistry.WriteBoolean("SnapToGrid", SnapToGridButton.Checked);
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
			m_CommandManager["ShowGrid"].Checked = CAD.Settings.DrawGrid;
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

		#region CommandManager Events

		private void ViewMenu_Click(Command cmd)
		{
			DockContent dc = cmd.Tag as DockContent;

			if (dc != null)
			{
				if (cmd.Checked)
				{
					dc.Show(DockPanel);
				}
				else
				{
					dc.Hide();
				}
			}
		}

		private void SnapToGrid_Click(Command cmd)
		{
			CAD.SnapToGrid = cmd.Checked;
		}

		private void Open_Click(Command cmd)
		{
			openFileDialog.InitialDirectory = m_ApplicationRegistry.ReadString("InitialDirectory", string.Empty);

			if (HandleUnsavedChanges() && openFileDialog.ShowDialog() == System.Windows.Forms.DialogResult.OK)
			{
				LoadLayout(openFileDialog.FileName);
				m_ApplicationRegistry.WriteString("InitialDirectory", Path.GetDirectoryName(openFileDialog.FileName));
			}
		}

		private void New_Click(Command cmd)
		{
			if (HandleUnsavedChanges())
			{
				NewLayoutDialog nld = new NewLayoutDialog();
				TNTCADState state = new TNTCADState(CAD);

				for (int index = 0; index < CAD.State.ObjectLayers.Count; index++)
				{
					state.ObjectLayers.Add(new List<TNTObject>());
				}

				if (nld.ShowDialog(this, state.Settings) == System.Windows.Forms.DialogResult.OK)
				{
					CAD.State = state;
					CAD.CurrentFileName = string.Empty;
				}

				m_LayoutSettingsForm.Settings = CAD.Settings;
				CAD.Settings.DrawGrid = m_CommandManager["ShowGrid"].Checked;
				CAD.Refresh();
			}
		}

		private void EnableOnSelectedUpdate(Command cmd)
		{
			cmd.Enabled = CAD.SelectedObjects.Count > 0;
		}

		private void ToggleViewState(Command cmd)
		{
			DockContent dc = cmd.Tag as DockContent;

			if (dc != null)
			{
				cmd.Checked = !dc.IsHidden;
			}
		}

		private void EnablePalettePartsSelected(Command cmd)
		{
			var paletteParts = (from s in CAD.SelectedObjects where s is PalettePart select s as PalettePart).ToList();
			cmd.Enabled = CAD.SelectedObjects.Count != 0 && CAD.SelectedObjects.Count == paletteParts.Count;
		}

		private void RotateSelectedPaletteParts(Command c)
		{
			int angle = (int)c.Tag;
			CAD.SelectedObjects.ForEach(o =>
				{
					PalettePart p = o as PalettePart;
					p.RotationAngle += angle;
				});

			CAD.ShowPropertyChanges();
		}

		#endregion

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
