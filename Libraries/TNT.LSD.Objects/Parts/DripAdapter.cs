using System.ComponentModel;
using System.Diagnostics.CodeAnalysis;
using TNT.LSD.Inventory;
using TNT.LSD.Settings;

namespace TNT.LSD.Objects;

/// <summary>
/// Stub type
/// </summary>
public enum StubType { PVC, FunnyPipe }

/// <summary>
/// Represents a drip adapter
/// </summary>
public class DripAdapter : LateralPart
{
  /// <summary>
  /// Type of pipe stubbed to the surface
  /// </summary>
  [Description("Type of pipe stubbed to the surface")]
  [ReadOnly(true)]
  public StubType Stub { get; set; }

  #region Constructors

  /// <summary>
  /// Copy constructor
  /// </summary>
  /// <param name="obj">Object to copy</param>
  public DripAdapter(DripAdapter obj)
    : base(obj)
  {
    Stub = obj.Stub;
  }

  /// <summary>
  /// Default constructor
  /// </summary>
  public DripAdapter()
    : base()
  {
  }

  #endregion

  /// <summary>
  /// Clones this <see cref="DripAdapter"/>
  /// </summary>
  /// <returns></returns>
  public override TNTObject Clone() => new DripAdapter(this);

  /// <summary>
  /// Adds the parts needed to stub off the pipe
  /// </summary>
  [ExcludeFromCodeCoverage]
  public override void SetPartQuantity(CodedParts parts, SystemType systemType)
  {
    base.SetPartQuantity(parts, systemType);

    var maxPipeSize = GetMaxPipeSize(typeof(LateralPipe));
    var minPipeSize = Pipes.Count > 1 ? GetMinPipeSize(typeof(LateralPipe)) : null;

    // Only add parts if there is a lateral line connected
			if (maxPipeSize == null) return;

    if (systemType == SystemType.PVC)
    {
      AddPVCFittings(parts, maxPipeSize, minPipeSize);
    }
    else
    {
      AddPOLYFittings(parts, maxPipeSize, minPipeSize);
    }
  }

  /// <summary>
  /// Add PVC fittings
  /// </summary>
  public static void AddPVCFittings(CodedParts parts, PartSize maxPipeSize, PartSize minPipeSize)
  {
    if (minPipeSize == null)// && maxPipeSize <= PartSize.SIZE_100)
    {
      parts.Add($"FI{maxPipeSize.Code}X{PartSize.SIZE_050}ST90", 1);
    }
    else
    {
      parts.Add($"FI{maxPipeSize.Code}X{PartSize.SIZE_050}SSTTEE", 1);
    }

    parts.Add("FPSBE050", 1);
    parts.Add("FUNNYPIPE", 1);
    parts.Add("HRFIG8", 1);
  }

  /// <summary>
  /// Add POLY fittings
  /// </summary>
  public static void AddPOLYFittings(CodedParts parts, PartSize maxPipeSize, PartSize minPipeSize)
  {
    if (minPipeSize == null)
    {
      parts.Add($"PF{maxPipeSize}X{PartSize.SIZE_050}BT90", 1);
      parts.AddHoseClamp(maxPipeSize.Code, 1);
    }
    else
    {
      parts.Add($"PF{maxPipeSize.Code}HDSADDLE", 1);
    }

    parts.Add("FPSBE050", 1);
    parts.Add("FUNNYPIPE", 1);
    parts.Add("HRFIG8", 1);
  }
}
