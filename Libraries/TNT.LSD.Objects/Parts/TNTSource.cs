using Newtonsoft.Json;
using System.ComponentModel;
using System.Drawing.Design;
using System.Text.RegularExpressions;
using TNT.LSD.Inventory;
using TNT.LSD.Settings;
using TNT.LSD.Settings.TypeConverters;

namespace TNT.LSD.Objects
{
  /// <summary>
  /// Represents a source part
  /// </summary>
  public class TNTSource : MainlinePart
  {
    #region Properties

    /// <summary>
    /// GPM available at source
    /// </summary>
    [Description("Indicates the number Gallons of water available per minute (GPM) at this source.")]
    [DisplayName("Available GPM")]
    public int AvailableGPM { get; set; }

    /// <summary>
    /// Specifies whether a S&W should be included
    /// </summary>
    [Description("Indicates whether a Stop && Waste valve is needed to connect to this source.")]
    [DisplayName("Include Stop && Waste")]
    public bool IncludeSW { get; set; }

    /// <summary>
    /// Indicates the source (pipe) size
    /// </summary>
    [TypeConverter(typeof(PipeSizeList))]
    [Description("Indicates the pipe size of the source being connected to.")]
    [DisplayName("Source Size")]
    public string SourceSize { get; set; }

    /// <summary>
    /// Indicates the type of pipe of source
    /// </summary>
    [TypeConverter(typeof(PipeTypeList))]
    [Description("Indicates the pipe type of the source being connected to.")]
    [DisplayName("Source Type")]
    public string SourceType { get; set; }

    #region S&W Key

    /// <summary>
    /// Backing property
    /// </summary>
    protected string m_SWKey;

    /// <summary>
    /// Indicates the S&W key size
    /// </summary>
    [Description("Indicates whether a stop and waste key should be included.")]
    [DisplayName("Stop && Waste Key")]
    [TypeConverter(typeof(TypeConverters.PartDescriptionList))]
    public string SWKey
    {
      get { return m_SWKey; }
      set
      {
        m_SWKey = value;

        if (SWKeyDescriptions != null)
        {
          int descriptionIndex = SWKeyDescriptions.IndexOf(m_SWKey);

          if (descriptionIndex > -1 && SWKeyCodes.Count > descriptionIndex)
            SWKeyCode = SWKeyCodes[descriptionIndex];
        }
      }
    }

    /// <summary>
    /// S&W key descriptions
    /// </summary>
    [JsonIgnore]
    [Browsable(false)]
    public List<string> SWKeyDescriptions { get; set; }

    /// <summary>
    /// S&W code
    /// </summary>
    [Description("The part code associated with the Stop && Waste Key")]
    [DisplayName("Stop && Waste Code")]
    [ReadOnly(true)]
    public string SWKeyCode { get; set; }

    /// <summary>
    /// S&W key codes
    /// </summary>
    [Editor(@"System.Windows.Forms.Design.StringCollectionEditor, System.Design, Version=2.0.0.0, Culture=neutral, PublicKeyToken=b03f5f7f11d50a3a", typeof(UITypeEditor))]
#if !PALETTE_PROPERTIES
		[Browsable(false)]
#endif
    public List<string> SWKeyCodes { get; set; }

    #endregion

    #endregion

    #region Constructors

    /// <summary>
    /// Default constructor
    /// </summary>
    public TNTSource()
      : base()
    {
      SourceSize = "3/4\"";
      SourceType = "Copper";
    }

    /// <summary>
    /// Copy constructor
    /// </summary>
    public TNTSource(TNTSource obj)
      : base(obj)
    {
      AvailableGPM = obj.AvailableGPM;
      IncludeSW = obj.IncludeSW;
      SourceSize = obj.SourceSize;
      SourceType = obj.SourceType;

      SWKeyCodes = obj.SWKeyCodes;
      SWKeyDescriptions = obj.SWKeyDescriptions;
      SWKey = obj.SWKey;
      SWKeyCode = obj.SWKeyCode;
    }

    #endregion

    /// <summary>
    /// Clones this object
    /// </summary>
    public override TNTObject Clone() => new TNTSource(this);

    /// <summary>
    /// Creates undo copy
    /// </summary>
    /// <returns>Undo copy</returns>
    public override TNTObject CreateUndoCopy()
    {
      TNTSource newObj = base.CreateUndoCopy() as TNTSource;

      newObj.AvailableGPM = AvailableGPM;
      newObj.IncludeSW = IncludeSW;
      newObj.SourceSize = SourceSize;
      newObj.SourceType = SourceType;
      newObj.SWKey = SWKey;

      return newObj;
    }

    /// <summary>
    /// Assigns obj's properties to this object
    /// </summary>
    /// <param name="obj">Source obj</param>
    public override void Assign(TNTObject obj)
    {
      base.Assign(obj);

      TNTSource source = obj as TNTSource;

      if (source != null)
      {
        AvailableGPM = source.AvailableGPM;
        IncludeSW = source.IncludeSW;
        SourceSize = source.SourceSize;
        SourceType = source.SourceType;
        SWKey = source.SWKey;
      }
    }

    /// <summary>
    /// Indicates if a pipe can be added. Only allows for one connection
    /// </summary>
    public override bool CanAddPipe(System.Type pipeType, out string reason)
    {
      if (Pipes.Count > 0)
      {
        reason = "Only one pipe can be connected to a source";
        return false;
      }

      return base.CanAddPipe(pipeType, out reason);
    }

    /// <summary>
    /// Indicates that this is a terminating part (only one connection allowed)
    /// </summary>
    public override bool TerminatePipe() => true;

    /// <summary>
    /// Adds parts between source and S&W. Connecting part is BN90 female threaded
    /// </summary>
    private void AddSW(CodedParts parts, SystemType systemType, PartSize mainSize, PartSize sourceSize)
    {
      // Get length of key for CL200 pipe
      Match match = Regex.Match(SWKey, "(?<feet>[0-9]*)'");
      int keyLength = 5;

      if (match.Success)
      {
        keyLength = Convert.ToInt32(match.Groups["feet"].Value);
      }

      // Add cl200 pipe and snug cap
      parts.Add("PI200C", System.Math.Max(5, keyLength));
      parts.Add("2SNUGCAP", 1);

      string tee = string.Format("BN{0}TEE", sourceSize);

      if (SourceType == "Copper")
      {
        // Copper compression tee
        parts.Add(string.Format("BCT{0}CTS{0}FIPT", sourceSize), 1);
      }
      else if (SourceType == "Poly" || SourceType == "Galvanized")
      {
        // Pack joints and tee
        parts.Add(string.Format("PJ{0}IPSX{0}MIP", sourceSize), 2);
        parts.Add(tee, 1);
      }
      else if (SourceType == "Poly CTS")
      {
        // Pack joints, tee, and stiffiners
        parts.Add(string.Format("PJ{0}CTSX{0}MIP", sourceSize), 2);
        parts.Add(string.Format("PJ{0}CTSSTIFFENER", sourceSize), 2);
        parts.Add(tee, 1);
      }
      else if (SourceType == "PVC")
      {
        // PVC tee
        parts.Add(string.Format("FI{0}STFA", sourceSize), 1);
      }

      // Brass nipple in and out of SW
      parts.Add(string.Format("BN{0}X2", sourceSize), 2);
      // SW
      parts.Add(string.Format("SW{0}", sourceSize), 1);
      // Elbow off nipple
      parts.Add(string.Format("BN{0}90", sourceSize), 1);
    }

    /// <summary>
    /// Sets the parts associated with this source
    /// </summary>
    public override void SetPartQuantity(CodedParts parts, SystemType systemType)
    {
      var mainSize = Pipes != null && Pipes.Count > 0 ? PartSize.GetSize(Pipes[0].PipeSizeIndex) : null;

      if (mainSize == null) return;

      var sourceSize = PartSize.GetSizeByReadable(SourceSize);

      // Add S&W key if specified
      if (!string.IsNullOrEmpty(SWKeyCode))
      {
        parts.Add(SWKeyCode, 1);
      }

      if (IncludeSW)
      {
        AddSW(parts, systemType, mainSize, sourceSize);  // Ends with female thread connection
      }
      else if (SourceType != "PVC")
      {
        if (SourceType == "Copper")
        {
          parts.Add(string.Format("PJ{0}CTSX{0}MIP", sourceSize), 1);
        }
        else if (SourceType == "Galvanized" || SourceType == "Poly")
        {
          parts.Add(string.Format("PJ{0}IPSX{0}MIP", sourceSize), 1);
        }
        else if (SourceType == "Poly CTS")
        {
          parts.Add(string.Format("PJ{0}CTSX{0}MIP", sourceSize), 1);
          parts.Add(string.Format("PJ{0}CTSSTIFFENER", sourceSize), 1);
        }

        // Brass elbow to redirect
        parts.Add(string.Format("BN{0}90", sourceSize), 1);
      }

      if (systemType == SystemType.PVC)
      {
        if (sourceSize < mainSize)
        {
          if (IncludeSW || SourceType != "PVC") parts.Add($"NI{sourceSize}X4", 1);

          parts.Add($"FI{mainSize}X{sourceSize}STRB", 1);
          parts.Add($"FI{mainSize}SSCOUP", 1);
        }
        else if (sourceSize > mainSize)
        {
          if (IncludeSW || SourceType != "PVC") parts.Add($"NI{sourceSize}X4TOE", 1);

          parts.Add($"FI{sourceSize}X{mainSize}SSRB", 1);
          parts.Add($"FI{sourceSize}SSCOUP", 1);
        }
        else
        {
          // Same
          if (IncludeSW || SourceType != "PVC") parts.Add($"NI{sourceSize}X4TOE", 1);

          parts.Add($"FI{sourceSize}SSCOUP", 1);
        }
      }
      else
      {
        // Add FA to PVC source that didn't need SW
        if (!IncludeSW && SourceType == "PVC") parts.Add($"FI{sourceSize}STFA", 1);

        // Thread to barbed ma out of SW
        if (sourceSize > mainSize)
        {
          parts.Add($"PF{mainSize}BTMA", 1);
          parts.Add($"FI{sourceSize}X{mainSize}TTRB", 1);
        }
        else if (sourceSize < mainSize)
        {
          parts.Add($"PF{mainSize}BTFA", 1);
          parts.Add($"FI{mainSize}X{sourceSize}TTRB", 1);
          parts.Add($"NI{sourceSize}X4", 1);
        }
        else
        {
          // Same
          parts.Add($"PF{mainSize}BTMA", 1);
        }

        var hcCode = mainSize == PartSize.SIZE_125 ? PartSize.SIZE_150 : mainSize;
        parts.AddHoseClamp(hcCode.Code, 1);
      }
    }
  }
}
