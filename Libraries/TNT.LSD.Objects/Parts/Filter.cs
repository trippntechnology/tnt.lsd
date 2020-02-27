using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing.Design;
using System.Text.RegularExpressions;
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
		public override void Assign(TNT.LSD.Objects.TNTObject obj)
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
		public override void SetPartQuantity(Dictionary<string, TNT.LSD.Inventory.Part> parts, SystemType systemType)
		{
			base.SetPartQuantity(parts, systemType);

			var mainPipeSize = GetMaxPipeSize(typeof(MainlinePipe));
			if (mainPipeSize == null) return;

			var filterSize = mainPipeSize <= PartSize.SIZE_125 ? PartSize.SIZE_100 : (mainPipeSize == PartSize.SIZE_150 ? PartSize.SIZE_150 : PartSize.SIZE_200);

			// Get the mesh size
			string mesh = "500";
			Match match = Regex.Match(Mesh, "(?<mesh>[0-9]*)");

			if (match.Success)
			{
				mesh = match.Groups["mesh"].ToString();
			}

			// Add the filter, valve box, and ball valve
			GetPart(parts, $"{filterSize.Code}FILTER{mesh}M").Quantity += 1;
			GetPart(parts, "VBJUMBO").Quantity += 1;
			GetPart(parts, $"BV{filterSize.Code}FT").Quantity += 1;

			// Add manifold transitions or F/MAs
			if (filterSize == PartSize.SIZE_100)
			{
				parts[$"AF18017"].Quantity += 1;
				parts[$"AF18011"].Quantity += 1;

				if (mainPipeSize == PartSize.SIZE_075)
				{
					parts[$"AF18013"].Quantity += 2;
				}
				else if (mainPipeSize == PartSize.SIZE_100)
				{
					parts[$"AF18012"].Quantity += 2;
				}
				else if (mainPipeSize == PartSize.SIZE_125)
				{
					parts[$"AF18012"].Quantity += 2;
					parts[$"FI{PartSize.SIZE_125.Code}SSCOUP"].Quantity += 2;
				}
			}
			else// if (filterSize == PartSize.SIZE_150)
			{
				parts[$"FI{filterSize.Code}STFA"].Quantity += 1;
				parts[$"FI{filterSize.Code}TSMA"].Quantity += 1;
			}
		}

		#endregion
	}
}
