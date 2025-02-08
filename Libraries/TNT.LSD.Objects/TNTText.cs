using Newtonsoft.Json;
using System.ComponentModel;
using System.Drawing.Design;

namespace TNT.LSD.Objects
{
  public class TNTText : TNTRectangle
  {
    [Description("Indicates the text to display.")]
    [Editor(typeof(System.ComponentModel.Design.MultilineStringEditor), typeof(UITypeEditor))]
    public string Text { get; set; }

    [DisplayName("Font")]
    [Description("Indicates the font.")]
    [DefaultValue(typeof(Font), "Arial, 12pt")]
    [TypeConverter(typeof(FilteredFontConverter))]
    [JsonIgnore]
    public Font Font { get; set; }

    [Browsable(false)]
    public SerializableFont _Font
    {
      get { return new SerializableFont(Font); }
      set
      {
        SerializableFont sf = value as SerializableFont;

        Font = new Font(sf.Name, sf.Size);
      }
    }

    [DisplayName("Text Color")]
    [Description("Indicates the font color.")]
    [DefaultValue(typeof(Color), "Black")]
    [JsonIgnore]
    public Color TextColor { get; set; }

    [Browsable(false)]
    public int _TextColor
    {
      get { return TextColor.ToArgb(); }
      set { TextColor = (Color)Color.FromArgb(value); }
    }

    [DisplayName("Horizontal Alignment")]
    [Description("Indicates the horizontal alignment of the text.")]
    [DefaultValue(typeof(StringAlignment), "Center")]
    public StringAlignment HorizontalAlignment { get; set; }

    [DisplayName("Vertical Alignment")]
    [Description("Indicates the vertical alignment of the text.")]
    [DefaultValue(typeof(StringAlignment), "Center")]
    public StringAlignment VerticalAlignment { get; set; }

    #region Constructors

    public TNTText(Point point)
      : base(new Rectangle(point, new Size(100, 100)), Color.Transparent)
    {
      Font = new Font("Arial", 12);
      TextColor = Color.Black;
      HorizontalAlignment = StringAlignment.Center;
      VerticalAlignment = StringAlignment.Center;
    }

    public TNTText(TNTText obj)
      : base(obj)
    {
      Text = obj.Text;
      Font = new Font(obj.Font.Name, obj.Font.Size);
      TextColor = Color.Black;
      HorizontalAlignment = StringAlignment.Center;
      VerticalAlignment = StringAlignment.Center;
    }

    public TNTText()
      : base()
    {
      Font = new Font("Arial", 12);
      TextColor = Color.Black;
      HorizontalAlignment = StringAlignment.Center;
      VerticalAlignment = StringAlignment.Center;
    }

    #endregion

    public override void Draw(Graphics graphics, DrawingOptions drawingOptions)
    {
      base.Draw(graphics, drawingOptions);

      StringFormat sf = new StringFormat();
      sf.Alignment = HorizontalAlignment;
      sf.LineAlignment = VerticalAlignment;
      graphics.DrawString(Text, Font, new SolidBrush(TextColor), GetRectangle(), sf);
    }

    public override void DrawDistances(Graphics graphics)
    {
      // Overridden to prevent measurements from being drawn
    }

    /// <summary>
    /// Creates a copy of the object for an undo action
    /// </summary>
    /// <returns>Copy of the object for an undo action</returns>
    public override TNTObject CreateUndoCopy()
    {
      TNTText newText = base.CreateUndoCopy() as TNTText;

      newText.Font = new System.Drawing.Font(Font, Font.Style);
      newText.TextColor = TextColor;
      newText.HorizontalAlignment = HorizontalAlignment;
      newText.VerticalAlignment = VerticalAlignment;
      newText.Text = Text;

      return newText;
    }

    /// <summary>
    /// Assigns selected properties from obj to this object
    /// </summary>
    /// <param name="obj">Object containing properties to assign</param>
    public override void Assign(TNTObject obj)
    {
      base.Assign(obj);

      TNTText text = obj as TNTText;

      if (text != null)
      {
        Font = text.Font;
        TextColor = text.TextColor;
        HorizontalAlignment = text.HorizontalAlignment;
        VerticalAlignment = text.VerticalAlignment;
        Text = text.Text;
      }
    }

    public override TNTObject Clone()
    {
      return new TNTText(this);
    }
  }

  public class FilteredFontConverter : FontConverter
  {
    public override PropertyDescriptorCollection GetProperties(ITypeDescriptorContext context, object value, Attribute[] attributes)
    {
      //in this method you can collect all properties that should be          
      //visible in property grid. So we can also remove unwanted items
      string[] properties = { "GdiCharSet", "GdiVerticalFont", "Unit", "Strikeout", "Underline" };

      PropertyDescriptorCollection props = base.GetProperties(context, value, attributes);

      foreach (string prop in properties)
      {
        PropertyDescriptor p = props.Find(prop, true);

        if (p != null)
        {
          props.Remove(p);
        }
      }

      return props;
    }

    public override bool GetPropertiesSupported(ITypeDescriptorContext context)
    {
      //here you can return true for displaying subitems or false         
      //if you want hide all subitems         
      return true;
    }
  }

  public class SerializableFont
  {
    public string Name { get; set; }
    public float Size { get; set; }
    public bool Bold { get; set; }
    public bool Italic { get; set; }

    public SerializableFont()
    {
    }

    public SerializableFont(Font font)
    {
      Name = font.Name;
      Size = font.Size;
      Bold = font.Bold;
      Italic = font.Italic;
    }
  }
}
