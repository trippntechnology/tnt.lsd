using LandscapeSprinklerDesigner.Events;
using LandscapeSprinklerDesigner.MenuEvents;
using LandscapeSprinklerDesigner.Service;
using LandscapeSprinklerDesigner.Utils;
using Microsoft.Win32;
using System.ComponentModel;
using System.Diagnostics;
using System.Reflection;
using TNT.Commons;
using TNT.LSD.Components;
using TNT.ToolStripItemManager;
using TNT.Utilities;
using WeifenLuo.WinFormsUI.Docking;

namespace LandscapeSprinklerDesigner;

public partial class Main : Form
{
    #region Members

    private DrawEvent? _previousDrawEvent;
    private DrawEvent? _currentDrawEvent;

    private ToolStripItemRadioGroupManager _drawingGroupManager = new ToolStripItemRadioGroupManager();
    private ToolStripItemGroupManager _menuGroupManager = new ToolStripItemGroupManager();

    private PropertyForm m_PropertyForm = new PropertyForm();
    private LayoutForm m_LayoutForm = new LayoutForm();
    private PartsListForm m_PartsListForm = new PartsListForm();
    private PalletTreeForm m_PalletForm = new PalletTreeForm();
    private LayoutSettingsForm m_LayoutSettingsForm = new LayoutSettingsForm();
    private PDFForm? m_PDFForm = null;
    private List<DockContent>? dockables = null;

    #endregion

    #region Properties

    private string AssemblyTitle
    {
        get
        {
            AssemblyTitleAttribute? ata = Utilities.GetAssemblyAttribute<AssemblyTitleAttribute>(Assembly.GetExecutingAssembly());
            return ata != null ? ata.Title : string.Empty;
        }
    }

    protected TNTCAD CAD { get { return m_LayoutForm.CAD; } }

    #endregion

    [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
    public Splash? SplashForm { get; set; }

    public Main()
    {
        InitializeComponent();

        DockPanel.Theme = new VS2015LightTheme();

        Global.userRegistry = new ApplicationRegistry(this, Registry.CurrentUser, Resource.Company, Resource.Application);

        dockables = new List<DockContent> { m_LayoutForm, m_LayoutSettingsForm, m_PartsListForm, m_PalletForm, m_PropertyForm };

        m_PalletForm.PalletNodeTagSelected = PalletNodeTagSelected;

        SetupDrawingGroupManager();
        SetupMenuGroupManager();

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
                    var drawEvent = _currentDrawEvent;
                    _currentDrawEvent = _previousDrawEvent;
                    _previousDrawEvent = drawEvent;
                    _currentDrawEvent?.Also(de =>
                    {
                        CAD.SetDrawingMode(de.DrawingMode);
                        de.Checked = true;
                    });
                }
            }
        };

        // Initialize to select mode.
        CAD.DrawingMode = new TNT.LSD.Components.DrawingModes.NullMode();

        CAD.OnPartsUpdated += new PartsUpdatedDelegate(m_PartsListForm.SetParts);

        CAD.OnFileNameChanged += FileNameChanged;

        var manager = new TNT.Plugin.Manager.Manager(Controls, pluginOnClickHandler, StatusBarHintChanged);

        var assemblyDirectory = Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location) ?? string.Empty;
        manager.Register(Path.Combine(assemblyDirectory, "plugins"));


        // Clear and re-add ToolStrips to ensure correct order
        toolStripContainer1.TopToolStripPanel.Controls.Clear();

        var toolStrips = new[] { ToolStrip1, pluginToolStrip, ToolStrip2, ToolStrip3, toolStrip4 };
        int x = 0;
        foreach (var ts in toolStrips)
        {
            ts.Location = new Point(x, 0);
            toolStripContainer1.TopToolStripPanel.Controls.Add(ts);
            x += ts.PreferredSize.Width + 4;
        }
    }

    private void SetupMenuGroupManager()
    {
        Logger.Info("Setting up menu group manager.");

        _menuGroupManager = new ToolStripItemGroupManager()
        {
            OnClick = toolStripItemGroup =>
            {
                //if (toolStripItemGroup is ILicensed licensedEvent && !IsLicensed()) return;
                Logger.Info($"Menu item group clicked: {toolStripItemGroup.GetType().Name}");

                if (toolStripItemGroup is MenuEvent menuEvent)
                {
                    menuEvent.OnMouseClicked(m_PropertyForm, CAD, m_LayoutSettingsForm);
                }
                //else if (toolStripItemGroup is DockMenuEvent dockMenuEvent)
                //{
                //    dockMenuEvent.OnMouseClicked(DockPanel);
                //}

            },
            OnCheckChanged = (toolStripItemGroup, isChecked) =>
            {
                Logger.Info($"Menu item group check changed: {toolStripItemGroup.GetType().Name}, Checked={isChecked}");
                if (toolStripItemGroup is DockMenuEvent dockMenuEvent)
                {
                    DockContent? dockContent = dockMenuEvent switch
                    {
                        LayoutSettingsEvent => m_LayoutSettingsForm,
                        PartsListMenuEvent => m_PartsListForm,
                        PropertiesMenuEvent => m_PropertyForm,
                        PartsPaletteEvent => m_PalletForm,
                        _ => null
                    };

                    if (isChecked)
                    {
                        dockContent?.Show(DockPanel);
                    }
                    else
                    {
                        dockContent?.Hide();
                    }
                }
            },
            OnIdle = (toolStripItemGroup, args) =>
            {
                if (toolStripItemGroup is MenuEvent menuEvent)
                {
                    menuEvent.OnApplicationIdle(CAD);
                }
                else if (toolStripItemGroup is DockMenuEvent dockMenuEvent)
                {
                    dockMenuEvent.OnApplicationIdle();
                }
            },

            OnToolTipChange = toolTipText => toolStripStatusLabel1.Text = toolTipText,
        };

        _menuGroupManager.Create<LayoutSettingsEvent>([LayoutSettingsButton, LayoutSettingsMenu]).DockContent = m_LayoutSettingsForm;
        _menuGroupManager.Create<PartsListMenuEvent>([PartsListButton, PartsListMenu]).DockContent = m_PartsListForm;
        _menuGroupManager.Create<PropertiesMenuEvent>([PropertiesButton, PropertiesMenu]).DockContent = m_PropertyForm;
        _menuGroupManager.Create<PartsPaletteEvent>([PaletteTreeButton, PaletteTreeMenu]).DockContent = m_PalletForm;


        _menuGroupManager.Create<CalculateAreaMenuEvent>([AreaMenu, AreaButton, m_LayoutForm.area]);
        _menuGroupManager.Create<CalculateDistanceMenuEvent>([LengthMenuItem, LengthButton, m_LayoutForm.calculateDistance]);
        _menuGroupManager.Create<ShowPartsMenuEvent>([PartsToolTipButton, PartsToolTipMenu]);


        _menuGroupManager.Create<AlignToGridMenuEvent>([AlignToGridMenu, AlignToGridButton, m_LayoutForm.space]);
        _menuGroupManager.Create<AutoSizeMenuEvent>([AutoSizeMenu, AutoSizeButton]).RestoreState(Global.userRegistry!, CAD);
        _menuGroupManager.Create<CheckForUpdateMenuItem>([CheckForUpdateMenu]);
        _menuGroupManager.Create<CloneMenuEvent>([CloneMenu, CloneButton]);
        _menuGroupManager.Create<DeleteMenuEvent>([DeleteMenu, DeleteButton]);
        _menuGroupManager.Create<ExitMenuEvent>([ExitMenu]);
        _menuGroupManager.Create<InfoMenuEvent>([ButtonSumGPM, m_LayoutForm.sumGpm, SumGPM]);
        _menuGroupManager.Create<LabelHeadsMenuEvent>([LabelHeadsMenu, LabelHeadsButton]).RestoreState(Global.userRegistry!, CAD);
        _menuGroupManager.Create<MoveToBackMenuEvent>([SendToBackButton, SendToBackMenu]);
        _menuGroupManager.Create<MoveToFrontMenuEvent>([BringToFrontMenu, BringToFrontButton]);
        _menuGroupManager.Create<NewMenuEvent>([NewButton, NewMenu]);
        _menuGroupManager.Create<OpenMenuEvent>([OpenMenu, OpenButton]).LoadLayout = LoadLayout;
        _menuGroupManager.Create<RegisterMenuEvent>([RegisterMenu]);
        _menuGroupManager.Create<Rotate180MenuEvent>([Rotate180Menu, Rotate180Button, m_LayoutForm.rotate180]);
        _menuGroupManager.Create<RotateLeftMenuEvent>([Rotate90CounterclockwiseButton, Rotate90CounterclockwiseMenu, m_LayoutForm.rotatecounter]);
        _menuGroupManager.Create<RotateRightMenuEvent>([Rotate90ClockwiseButton, Rotate90ClockwiseMenu, m_LayoutForm.rotateclock]);
        _menuGroupManager.Create<SaveAsMenuEvent>([SaveAsMenu]);
        _menuGroupManager.Create<SaveMenuEvent>([SaveMenu, SaveButton]);
        _menuGroupManager.Create<SelectAllMenuEvent>([SelectAllMenu]);
        _menuGroupManager.Create<ShowCoverageMenuEvent>([CoverageButton, CoverageMenu]);
        _menuGroupManager.Create<ShowDistanceMenuEvent>([ShowDistancesMenu, ShowDistancesButton]).RestoreState(Global.userRegistry!, CAD);
        _menuGroupManager.Create<ShowGridMenuEvent>([ShowGridButton, ShowGridMenu]);
        _menuGroupManager.Create<SnapToGridMenuEvent>([SnapToGridButton, SnapToGridMenu, m_LayoutForm.snaptogrid]).RestoreState(Global.userRegistry!, CAD);
        _menuGroupManager.Create<SpaceEquallyMenuEvent>([SpaceEquallyMenu, SpaceEquallyButton, m_LayoutForm.aligntogrid]);
        _menuGroupManager.Create<UndoMenuEvent>([UndoMenu, UndoButton]);
    }

    private bool IsLicensed(bool allowMessageBox = true)
    {
        var licenseeInfo = FileUtil.GetLicenseeInfo();
        bool? isLicensed = DateTime.Now < licenseeInfo?.ValidUntil.DateTime;

        if (allowMessageBox)
        {
            if (isLicensed == null)
            {
                MessageBox.Show(this, "This feature is not licensed", "License Unavailable", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
            else if (isLicensed == false)
            {
                MessageBox.Show(this, "The license for this feature has expired.", "License Expired", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
        }

        return isLicensed ?? false;
    }

    private ToolStripItem[] ToToolStripItemArray(params ToolStripItem[] args) => args;

    private void SetupDrawingGroupManager()
    {
        _drawingGroupManager = new ToolStripItemRadioGroupManager()
        {
            OnClick = toolStripItemGroup =>
            {
                var drawEvent = toolStripItemGroup as DrawEvent;

                if (drawEvent?.Checked != true) return;

                _previousDrawEvent = _currentDrawEvent;
                _currentDrawEvent = drawEvent;

                this.CAD.SetDrawingMode(drawEvent.DrawingMode);
                this.m_PropertyForm.SelectedObject = drawEvent.DrawingMode.DefaultObject;
                this.m_PalletForm.UnselectAll();
            },
            OnToolTipChange = toolTipText => toolStripStatusLabel1.Text = toolTipText,
        };
        _drawingGroupManager.Create<DrawCircleEvent>([circleButton]);
        _drawingGroupManager.Create<DrawCurveEvent>([curveButton]);
        _drawingGroupManager.Create<DrawLegendEvent>([legendButton]);
        _drawingGroupManager.Create<DrawLineEvent>([lineButton]);
        _drawingGroupManager.Create<DrawPolyEvent>([polyButton]);
        _drawingGroupManager.Create<DrawRectangleEvent>([rectangleButton]);
        _drawingGroupManager.Create<DrawSelectEvent>([selectButton]).Also(de =>
        {
            de.Checked = true;
            _currentDrawEvent = de;
        });
        _drawingGroupManager.Create<DrawTextEvent>([textButton]);
    }

    private void pluginOnClickHandler(object? sender, EventArgs e)
    {
        bool isLicensed = FileUtil.HasValidLicense();
        var tsi = sender as ToolStripItem;
        var p = tsi?.Tag as TNT.Plugin.Manager.Plugin;
        var toolStripItem = sender as ToolStripItem;

        if (toolStripItem == null) return;

        p?.Execute(this, toolStripItem, new ApplicationData(CAD, this.DockPanel), hasLicense: isLicensed);

        CAD.Repaint(0);
    }

    private void Main_Load(object sender, EventArgs e)
    {
        #region Restore state from Registry

        Global.userRegistry?.Also(userRegistry =>
        {
            userRegistry?.ReadToolStripItems("MRU", OpenButton.DropDownItems);
            tbScale.Value = userRegistry?.ReadInteger("Scale", tbScale.Value) ?? 100;
        });

        #endregion

        string configFile = Path.Combine(Path.GetDirectoryName(Application.ExecutablePath) ?? string.Empty, "DockPanel.config");

        if (File.Exists(configFile))
        {
            DockPanel.LoadFromXml(configFile, new DeserializeDockContent(GetContentFromPersistString));
        }

        m_LayoutForm.Show(DockPanel);

        statusStrip1.Items.Add(new ToolStripControlHost(tbScale));

        TntService.GetLicenseeInfoFlow().collect(licenseeInfo =>
        {
            licenseeInfo?.Also(info => FileUtil.SaveLicenseInfo(info));
            var isLicensed = licenseeInfo?.ValidUntil.DateTime.Let(validDateTime => DateTime.Now < validDateTime) ?? false;

            _menuGroupManager.OfType<ILicensed>().ToList().ForEach(licensedMenuEvent =>
            {
                licensedMenuEvent.OnLicensedChanged(isLicensed);
            });
        });
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

        string configFile = Path.Combine(Path.GetDirectoryName(Application.ExecutablePath) ?? string.Empty, "DockPanel.config");
        DockPanel.SaveAsXml(configFile);

        #region Save state to Registry

        var persistedMenuEvent = (from p in _menuGroupManager where p is PersistedMenuEvent select p as PersistedMenuEvent).ToList();
        Global.userRegistry?.Also(userRegistry =>
        {
            persistedMenuEvent.ForEach(p => p.SaveState(userRegistry));
            userRegistry.WriteInteger("Scale", tbScale.Value);
            userRegistry.WriteToolStripItems("MRU", OpenButton.DropDownItems);
        });

        #endregion
    }

    private IDockContent? GetContentFromPersistString(string persistString) => dockables?.Find(d => d.GetType().ToString() == persistString);

    private void tbScale_ValueChanged(object sender, EventArgs e)
    {
        (sender as TrackBar)?.Value.Also(value =>
           {
               ScaleButton.Text = string.Format("{0}%", value);
               CAD.DisplayScale = value;
           });
    }

    private void OpenMRU_Click(object sender, ToolStripItemClickedEventArgs e)
    {
        if (HandleUnsavedChanges())
        {
            e.ClickedItem?.Text?.Also(layout => LoadLayout(layout));
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
        _menuGroupManager.Find(m => m is ShowGridMenuEvent)?.Also(it => { it.Checked = CAD.Settings.DrawGrid; });
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
        (sender as ToolStripMenuItem)?.Also(mi =>
        {
            int scale = Convert.ToInt32(mi.Tag);
            tbScale.Value = scale;
        });
    }

    private void PalletNodeTagSelected(PaletteProperties paletteProperties)
    {
        // Uncheck the landscape drawing tool if there's one selected.
        var checkedItem = _drawingGroupManager.FirstOrDefault(i => i.Checked);
        if (checkedItem != null) { checkedItem.Checked = false; }
        CAD.SetPalletNodeTag(paletteProperties);
        m_PropertyForm.SelectedObject = paletteProperties.DrawingMode.DefaultObject ?? new object();
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
        (sender as ToolStripStatusLabel)?.ToolTipText?.Also(toolTipText => Process.Start(toolTipText));
    }

    private void OnTheWebToolStripMenuItem_Click(object sender, EventArgs e)
    {
        try
        {
            var psi = new ProcessStartInfo
            {
                FileName = "https://www.LandscapeSprinklerDesigner.com",
                UseShellExecute = true
            };
            Process.Start(psi);
        }
        catch
        {
        }
    }
}
