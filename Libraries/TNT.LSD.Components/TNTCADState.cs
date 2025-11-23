using Newtonsoft.Json;
using System.ComponentModel;
using System.Drawing;
using System.Drawing.Imaging;
using System.Reflection;
using TNT.Commons;
using TNT.LSD.Inventory;
using TNT.LSD.Inventory.DAL;
using TNT.LSD.Objects;
using TNT.LSD.Settings;

namespace TNT.LSD.Components;

using ObjectList = List<TNTObject>;
using ObjectListList = List<List<TNTObject>>;

public class TNTCADState
{
  protected Legend m_HiddenLegend = null;

  #region Properties

  [ReadOnly(true)]
  public virtual string Version
  {
    get
    {
      Assembly asm = Assembly.GetEntryAssembly();
      return asm.GetName().Version.ToString();
    }
    set { }
  }

  [Browsable(false)]
  [JsonIgnore]
  public virtual Image BackgroundImage { get; set; }

  public string _BackgroundImage
  {
    get
    {
      using (MemoryStream ms = new MemoryStream())
      {
        if (BackgroundImage != null)
        {
          BackgroundImage.Save(ms, ImageFormat.Png);
        }

        return Convert.ToBase64String(ms.ToArray());
      }
    }

    set
    {
      if (value.Length > 0)
      {
        byte[] array = Convert.FromBase64String(value);

        using (MemoryStream ms = new MemoryStream(array))
        {
          BackgroundImage = Image.FromStream(ms);
        }
      }
    }
  }

  private LSDSettings? _Settings = null;
  [TypeConverter(typeof(ExpandableObjectConverter))]
  public virtual LSDSettings? Settings
  {
    get
    {
      return _Settings;
    }
    set
    {
      _Settings = value;
      _Settings?.Also(s =>
      {
        s.OnDrawLayers = DrawLayers;
        s.OnSetHeightInFeet = SetHeightInFeet;
        s.OnSetWidthInFeet = SetWidthInFeet;
      });
    }
  }

  [Browsable(false)]
  public virtual ObjectListList ObjectLayers { get; set; }

  [JsonIgnore]
  public TNTCAD CAD { get; set; }

  #region Layout

  public bool DrawGrid { get { return Settings.DrawGrid; } set { Settings.DrawGrid = value; } }

  [JsonIgnore]
  public int GridColorAlphaValue { get { return Settings.GridColorAlphaValue; } set { Settings.GridColorAlphaValue = value; } }

  [JsonIgnore]
  public Color GridColor { get { return Settings.GridColor; } set { Settings.GridColor = value; } }

  public bool DrawUnits { get { return Settings.DrawUnits; } }

  [JsonIgnore]
  public int HeightInFeet
  {
    get { return Settings.HeightInFeet; }
    set { Settings.HeightInFeet = value; }
  }

  [JsonIgnore]
  public int WidthInFeet
  {
    get { return Settings.WidthInFeet; }
    set { Settings.WidthInFeet = value; }
  }

  public int MainlineDrains { get { return Settings.MainlineDrains; } }

  public int LateralDrains { get { return Settings.LateralDrains; } }

  public bool RoundPipe { get { return Settings.RoundPipe; } }

  public bool ShowExternalCodes { get { return Settings is SSCSettings ? (Settings as SSCSettings).ShowExternalCodes : false; } }

  [JsonIgnore]
  public List<Part> StaticParts { get { return Settings.StaticParts; } }

  public SystemType SystemType
  {
    get { return Settings.SystemType; }
    set { Settings.SystemType = value; }
  }

  #endregion

  #endregion

  #region Constructors

  public TNTCADState()
    : this(null)
  {
  }

  public TNTCADState(TNTCAD parent)
    : base()
  {
    CAD = parent;
    ObjectLayers = new ObjectListList();

    Settings = AppSettingsUtils.DeserializeSection<LSDSettings>("appsettings.json", "LSDSettings");

    if (Settings != null)
    {
      if (Settings.StaticParts != null)
      {
        Settings.StaticParts.ForEach(p =>
          {
            p.Description = DALPart.GetDescription(p.Code);
          });
      }
    }
  }

  private void SetWidthInFeet(int widthInFeet)
  {
    if (CAD != null)
    {
      CAD.Width = (int)(widthInFeet * TNTConstants.PIXELS_PER_FOOT * CAD.DisplayScale / 100.0);
    }
  }

  private void SetHeightInFeet(int heightInFeet)
  {
    if (CAD != null)
    {
      CAD.Height = (int)(heightInFeet * TNTConstants.PIXELS_PER_FOOT * CAD.DisplayScale / 100.0);
    }
  }

  private void DrawLayers(int layer)
  {
    CAD?.DrawLayers(layer);
  }

  #endregion

  public virtual void ResolveReferences()
  {
    foreach (ObjectList ol in ObjectLayers)
    {
      foreach (TNTObject o in ol)
      {
        o.ResolveReferences(ol);
      }
    }
  }

  public virtual void SelectAll()
  {
    foreach (ObjectList ol in ObjectLayers)
    {
      ol.ForEach(o => o.Selected = true);
    }
  }
}
