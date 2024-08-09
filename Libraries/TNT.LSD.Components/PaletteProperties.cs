using System.ComponentModel;
using System.Drawing.Design;
using System.Reflection;
using TNT.LSD.Components.DrawingModes;
using TNT.LSD.Components.TypeConverters;

namespace TNT.LSD.Components;

public class PaletteProperties
{
  /// <summary>
  /// This is used by DrawingModeListConverter
  /// </summary>
  internal List<string> m_ModeList = null;
  private string m_DrawingModeType = string.Empty;
  private string m_ImageObjectType = string.Empty;

  public PaletteProperties()
  {
    m_ModeList = Utilities.Utilities.GetNameSpaceClasses("TNT.LSD.Components.DrawingModes", "TNT.LSD.Components.dll");
    DrawingMode = new DrawingModes.NullMode();
  }

  public PaletteProperties(PaletteProperties obj)
  {
    m_ModeList = obj.m_ModeList;
    m_DrawingModeType = obj.m_DrawingModeType;
    m_ImageObjectType = obj.m_ImageObjectType;
    DrawingMode = obj.DrawingMode.Clone();
    Description = obj.Description;
  }

  [DisplayName("Drawing Mode Type")]
  [Description("Type of drawing mode to use.")]
  [TypeConverter(typeof(DrawingModeListConverter))]
  public string DrawingModeType
  {
    get { return m_DrawingModeType; }
    set
    {
      Assembly asm = Assembly.LoadFrom("TNT.LSD.Components.dll");
      m_DrawingModeType = value;

      if (!string.IsNullOrEmpty(m_DrawingModeType))
      {
        DrawingMode = (DrawingMode)asm.CreateInstance(m_DrawingModeType);
      }
    }
  }

  [DisplayName("Drawing Mode")]
  [Description("Drawing Mode used when node is selected")]
  [TypeConverter(typeof(DrawingModeConverter))]
  public DrawingMode DrawingMode { get; set; }

  [Description("Description of the node.")]
  [Editor(typeof(System.ComponentModel.Design.MultilineStringEditor), typeof(UITypeEditor))]
  public string Description { get; set; }
}
