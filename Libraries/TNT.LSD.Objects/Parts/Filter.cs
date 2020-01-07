using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing.Design;
using System.Text.RegularExpressions;
using TNT.LSD.Settings;

namespace TNT.LSD.Objects
{
	public class Filter : MainlinePart
	{
		#region Properties

		[Description("Degree of filtration. Smaller the number the greator the filtration.")]
		[DisplayName("Filtration Degree")]
		[TypeConverter(typeof(TypeConverters.GenericList))]
		public string Mesh { get; set; }

		protected List<string> m_MeshList = new List<string>();

		[Editor(@"System.Windows.Forms.Design.StringCollectionEditor, System.Design, Version=2.0.0.0, Culture=neutral, PublicKeyToken=b03f5f7f11d50a3a", typeof(UITypeEditor))]
#if !PALETTE_PROPERTIES
		[Browsable(false)]
#endif
		public List<string> MeshList { get { return m_MeshList; } set { m_MeshList = value; } }

		#endregion

		#region Constructors

		public Filter(Filter obj)
			: base(obj)
		{
			Mesh = obj.Mesh;
			MeshList = obj.MeshList;
		}

		public Filter()
			: base()
		{
		}

		#endregion

		#region Overrides

		public override TNTObject Clone()
		{
			return new Filter(this);
		}

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

		public override void SetPartQuantity(Dictionary<string, TNT.LSD.Inventory.Part> parts, SystemType systemType)
		{
			int pipeSizeIndex = Pipes != null && Pipes.Count > 0 ? m_PipeSizeList.IndexOf(Pipes[0].PipeSize) : 2;
			int filterSizeIndex = pipeSizeIndex == 3 ? 2 : pipeSizeIndex;
			string pipeSizeCode = SIZE_CODE[pipeSizeIndex];
			string filterSizeCode = SIZE_CODE[filterSizeIndex];
			string mesh = "500";

			Match match = Regex.Match(Mesh, "(?<mesh>[0-9]*)");

			if (match.Success)
			{
				mesh = match.Groups["mesh"].ToString();
			}

			// Add the filter
			GetPart(parts, string.Format("{0}FILTER{1}M", filterSizeCode, mesh)).Quantity += 1;

			GetPart(parts, "VBJUMBO").Quantity += 1;

			if (pipeSizeIndex == filterSizeIndex)
			{
				GetPart(parts, string.Format("FI{0}STFA", pipeSizeCode)).Quantity += 2;
			}
			else if (pipeSizeIndex > filterSizeIndex)
			{
				GetPart(parts, string.Format("FI{0}STFA", pipeSizeCode)).Quantity += 2;
				GetPart(parts, string.Format("FI{0}X{1}STRB", pipeSizeCode, filterSizeCode)).Quantity += 2;
			}

			base.SetPartQuantity(parts, systemType);
		}

		#endregion

	}
}
