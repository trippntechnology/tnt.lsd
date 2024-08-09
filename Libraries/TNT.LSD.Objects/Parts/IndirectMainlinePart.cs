using Newtonsoft.Json;
using System.ComponentModel;
using System.Drawing.Design;
using TNT.LSD.Inventory;
using TNT.LSD.Settings;

namespace TNT.LSD.Objects
{
  /// <summary>
  /// Represents a generic mainline part
  /// </summary>
  public class IndirectMainlinePart : MainlinePart
  {
    #region Properties

    #region Model

    /// <summary>
    /// Backing property for <see cref="Model"/>
    /// </summary>
    protected string m_Model;

    /// <summary>
    /// Specifies the model of the part
    /// </summary>
    [Description("Specifies the model to use")]
    [TypeConverter(typeof(TypeConverters.PartDescriptionList))]
    virtual public string Model
    {
      get { return m_Model; }
      set
      {
        m_Model = value;

        if (ModelDescriptions != null)
        {
          int descriptionIndex = ModelDescriptions.IndexOf(m_Model);

          if (descriptionIndex > -1 && ModelCodes.Count > descriptionIndex)
            ModelCode = ModelCodes[descriptionIndex];
        }
      }
    }

    /// <summary>
    /// The description of the part
    /// </summary>
    [JsonIgnore]
    [Browsable(false)]
    virtual public List<string> ModelDescriptions { get; set; }

    /// <summary>
    /// Code associated with this part
    /// </summary>
    [Description("Internal code associated with this part")]
    [DisplayName("Model Code")]
    [ReadOnly(true)]
    virtual public string ModelCode { get; set; }

    /// <summary>
    /// Available model codes
    /// </summary>
    [Editor(@"System.Windows.Forms.Design.StringCollectionEditor, System.Design, Version=2.0.0.0, Culture=neutral, PublicKeyToken=b03f5f7f11d50a3a", typeof(UITypeEditor))]
#if !PALETTE_PROPERTIES
		[Browsable(false)]
#endif
    virtual public List<string> ModelCodes { get; set; }

    #endregion

    #region Fitting Thread Size

    /// <summary>
    /// Indicates the inlet thread size
    /// </summary>
    [Description("Specifies the thread size on the fitting coming off the mainline")]
    [DisplayName("Fitting Thread Size")]
    [TypeConverter(typeof(TypeConverters.SizeList))]
#if !PALETTE_PROPERTIES
		[Browsable(false)]
#endif
    public string FittingThreadSize { get; set; }

    #endregion

    #endregion

    #region Constructors

    /// <summary>
    /// Copy constructor
    /// </summary>
    public IndirectMainlinePart(IndirectMainlinePart obj)
      : base(obj)
    {
      ModelCodes = obj.ModelCodes == null ? null : new List<string>(obj.ModelCodes);
      ModelDescriptions = obj.ModelDescriptions == null ? null : new List<string>(obj.ModelDescriptions);
      Model = obj.Model;
      ModelCode = obj.ModelCode;
      FittingThreadSize = obj.FittingThreadSize;
    }

    /// <summary>
    /// Default constructor
    /// </summary>
    public IndirectMainlinePart()
      : base()
    {
    }

    #endregion

    #region Overrides

    /// <summary>
    /// Clones this <see cref="IndirectMainlinePart"/>
    /// </summary>
    /// <returns></returns>
    public override TNTObject Clone() => new IndirectMainlinePart(this);

    /// <summary>
    /// Sets the part quantities for this part
    /// </summary>
    public override void SetPartQuantity(CodedParts parts, SystemType systemType)
    {
      base.SetPartQuantity(parts, systemType);

      string[] codes = ModelCode.Split(';');

      foreach (string code in codes)
      {
        // Add each code
        parts.Add(code, 1);
      }

      // Add fittings
      if (Pipes != null && Pipes.Count > 0)
      {
        var pipeSize = GetMaxPipeSize();
        var fittingSize = pipeSize;
        var outletThreadSize = PartSize.GetSizeByReadable(FittingThreadSize);

        if (systemType == SystemType.PVC)
        {
          AddPVCParts(parts, fittingSize, outletThreadSize);
        }
        else
        {
          AddPOLYParts(parts, fittingSize, outletThreadSize);
        }
      }
    }

    private void AddPOLYParts(CodedParts parts, PartSize fittingSize, PartSize outletThreadSize)
    {
      if (Pipes.Count == 1)
      {
        // Add 90
        if (fittingSize == outletThreadSize)
        {
          parts.Add($"PF{fittingSize}BT90", 1);
        }
        else
        {
          if (parts.ContainsKey($"PF{fittingSize}X{outletThreadSize}BT90"))
          {
            parts.Add($"PF{fittingSize}X{outletThreadSize}BT90", 1);
          }
          else
          {
            parts.Add($"PF{fittingSize}BT90", 1);
            parts.Add($"FI{fittingSize}X{outletThreadSize}TTRB", 1);
          }
        }
        parts.AddHoseClamp(fittingSize.Code, 1);
      }
      else
      {
        // Add Tee
        if (fittingSize == outletThreadSize)
        {
          parts.Add($"PF{fittingSize}BBTTEE", 1);
        }
        else
        {
          if (parts.ContainsKey($"PF{fittingSize}X{outletThreadSize}BBTTEE"))
          {
            parts.Add($"PF{fittingSize}X{outletThreadSize}BBTTEE", 1);
          }
          else
          {
            parts.Add($"PF{fittingSize}BBTTEE", 1);
            parts.Add($"FI{fittingSize}X{outletThreadSize}TTRB", 1);
          }
        }
        parts.AddHoseClamp(fittingSize.Code, 2);
      }
    }

    private void AddPVCParts(CodedParts parts, PartSize fittingSize, PartSize outletThreadSize)
    {
      if (Pipes.Count == 1)
      {
        // Add 90
        if (fittingSize == outletThreadSize)
        {
          parts.Add($"FI{fittingSize}ST90", 1);
        }
        else
        {
          if (parts.ContainsKey($"FI{fittingSize}X{outletThreadSize}ST90"))
          {
            parts.Add($"FI{fittingSize}X{outletThreadSize}ST90", 1);
          }
          else
          {
            // Bushing to size outlet
            parts.Add($"FI{fittingSize}SS90", 1);
            parts.Add($"FI{fittingSize}X{outletThreadSize}STRB", 1);
          }
        }
      }
      else
      {
        // Add Tee
        if (fittingSize == outletThreadSize)
        {
          parts.Add($"FI{fittingSize}SSTTEE", 1);
        }
        else
        {
          if (parts.ContainsKey($"FI{fittingSize}X{outletThreadSize}SSTTEE"))
          {
            parts.Add($"FI{fittingSize}X{outletThreadSize}SSTTEE", 1);
          }
          else
          {
            // Bushing to size outlet
            parts.Add($"FI{fittingSize}SSSTEE", 1);
            parts.Add($"FI{fittingSize}X{outletThreadSize}STRB", 1);
          }
        }
      }
    }

    #endregion
  }
}
