using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing.Design;
using System.Text.RegularExpressions;
using TNT.LSD.Inventory;
using TNT.LSD.Settings;

namespace TNT.LSD.Objects
{
	/// <summary>
	/// Represents a filter part
	/// </summary>
	public class Filter : MainlinePart
	{
		#region Properties

		/// <summary>
		/// Degree of filtration
		/// </summary>
		[Description("Degree of filtration. Smaller the number the greator the filtration.")]
		[DisplayName("Filtration Degree")]
		[TypeConverter(typeof(TypeConverters.GenericList))]
		public string Mesh { get; set; }

		/// <summary>
		/// List of available meshes
		/// </summary>
		[Editor(@"System.Windows.Forms.Design.StringCollectionEditor, System.Design, Version=2.0.0.0, Culture=neutral, PublicKeyToken=b03f5f7f11d50a3a", typeof(UITypeEditor))]
#if !PALETTE_PROPERTIES
		[Browsable(false)]
#endif
		public List<string> MeshList { get; set; }

		#endregion

		#region Constructors

		/// <summary>
		/// Copy constructor
		/// </summary>
		/// <param name="obj"><see cref="Filter"/> to copy</param>
		public Filter(Filter obj)
			: base(obj)
		{
			Mesh = obj.Mesh;
			MeshList = obj.MeshList;
		}

		/// <summary>
		/// Default Constructor
		/// </summary>
		public Filter()
			: base()
		{
		}

		#endregion

		#region Overrides

		/// <summary>
		/// Clones this <see cref="Filter"/>
		/// </summary>
		public override TNTObject Clone() => new Filter(this);

		/// <summary>
		/// Create undo copy
		/// </summary>
		/// <returns>Undo copy</returns>
		public override TNTObject CreateUndoCopy()
		{
			Filter newObj = base.CreateUndoCopy() as Filter;

			newObj.Mesh = Mesh;

			return newObj;
		}

		/// <summary>
		/// Assigns obj's properties to this object
		/// </summary>
		/// <param name="obj">Source object</param>
		public override void Assign(TNTObject obj)
		{
			Filter filter = obj as Filter;

			if (filter != null)
			{
				Mesh = filter.Mesh;
			}

			base.Assign(obj);
		}

		/// <summary>
		/// Set the parts associated with a filter
		/// </summary>
		public override void SetPartQuantity(CodedParts parts, SystemType systemType)
		{
			base.SetPartQuantity(parts, systemType);

			var mainPipeSize = GetMaxPipeSize(typeof(MainlinePipe));
			if (mainPipeSize == null) return;

			var filterSize = mainPipeSize <= PartSize.SIZE_125 ? PartSize.SIZE_100 : (mainPipeSize == PartSize.SIZE_150 ? PartSize.SIZE_150 : PartSize.SIZE_200);

			SetFilterBoxBallValve(parts, filterSize, Mesh);

			// Add manifold transitions or F/MAs
			AddFittings(parts, systemType, mainPipeSize, filterSize);
		}

		public static void AddFittings(CodedParts parts, SystemType systemType, PartSize mainPipeSize, PartSize filterSize)
		{
			if (filterSize == PartSize.SIZE_100)
			{
				parts.Add($"AF18017", 1); // Female transition nipple
				parts.Add($"AF18011", 1); // Male transition nipple

				if (systemType == SystemType.PVC)
				{
					if (mainPipeSize == PartSize.SIZE_075)
					{
						parts.Add($"AF18013", 2); // 3/4 slip adapter
					}
					else if (mainPipeSize == PartSize.SIZE_100)
					{
						parts.Add($"AF18012", 2); // 1-1/4 and 1 slip adapter
					}
					else if (mainPipeSize == PartSize.SIZE_125)
					{
						parts.Add($"AF18012", 2); // 1-1/4 and 1 slip adapter
						parts.Add($"FI{PartSize.SIZE_125}SSCOUP", 2); // 1-1/4 couplers
					}
				}
				else
				{
					if (mainPipeSize == PartSize.SIZE_075)
					{
						parts.Add($"AF18015", 2); // 3/4 barbed adapter
					}
					else if (mainPipeSize == PartSize.SIZE_100)
					{
						parts.Add($"AF18014", 2); // 1 barbed adapter
					}
					else if (mainPipeSize == PartSize.SIZE_125)
					{
						parts.Add($"AF18018", 2); // 1-1/4 barbed adapter
					}

					parts.AddHoseClamp(mainPipeSize.Code, 2);
				}
			}
			else
			{
				if (systemType == SystemType.PVC)
				{
					parts.Add($"FI{filterSize}STFA", 1);
					parts.Add($"FI{filterSize}TSMA", 1);
				}
			}
		}

		/// <summary>
		/// Adds the filter, valve box and ball valve
		/// </summary>
		public static void SetFilterBoxBallValve(CodedParts parts, PartSize filterSize, string meshSize)
		{
			// Get the mesh size
			string mesh = "500";
			Match match = Regex.Match(meshSize, "(?<mesh>[0-9]*)");

			if (match.Success)
			{
				mesh = match.Groups["mesh"].ToString();
			}

			// Add the filter, valve box, and ball valve
			parts.Add($"{filterSize}FILTER{mesh}M", 1);
			parts.Add($"VBJUMBO", 1);
			parts.Add($"BV{filterSize}FT", 1);
		}

		#endregion
	}
}
