using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing.Design;
using System.Xml.Serialization;
using TNT.LSD.Inventory;
using TNT.LSD.Settings;

namespace TNT.LSD.Objects
{
	public class IndirectMainlinePart : MainlinePart
	{
		#region Properties

		#region Model

		protected string m_Model;

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

					if (descriptionIndex > -1 && m_ModelCodes.Count > descriptionIndex)
						ModelCode = m_ModelCodes[descriptionIndex];
				}
			}
		}

		[XmlIgnore()]
		[Browsable(false)]
		virtual public List<string> ModelDescriptions { get; set; }

		[Description("Internal code associated with this part")]
		[DisplayName("Model Code")]
		[ReadOnly(true)]
		virtual public string ModelCode { get; set; }

		protected List<string> m_ModelCodes = new List<string>();

		[Editor(@"System.Windows.Forms.Design.StringCollectionEditor, System.Design, Version=2.0.0.0, Culture=neutral, PublicKeyToken=b03f5f7f11d50a3a", typeof(UITypeEditor))]
#if !PALETTE_PROPERTIES
		[Browsable(false)]
#endif
		virtual public List<string> ModelCodes { get { return m_ModelCodes; } set { m_ModelCodes = value; } }

		#endregion

		#region Fitting Thread Size

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

		public IndirectMainlinePart(IndirectMainlinePart obj)
			: base(obj)
		{
			ModelCodes = obj.ModelCodes == null ? null : new List<string>(obj.ModelCodes);
			ModelDescriptions = obj.ModelDescriptions == null ? null : new List<string>(obj.ModelDescriptions);
			Model = obj.Model;
			ModelCode = obj.ModelCode;
			FittingThreadSize = obj.FittingThreadSize;
		}

		public IndirectMainlinePart()
			: base()
		{
		}

		#endregion

		#region Overrides

		public override TNTObject Clone()
		{
			return new IndirectMainlinePart(this);
		}

		public override void SetPartQuantity(CodedParts parts, SystemType systemType)
		{
			base.SetPartQuantity(parts, systemType);

			string[] codes = ModelCode.Split(';');

			foreach (string code in codes)
			{
				GetPart(parts, code).Quantity += 1;
			}

			if (Pipes != null && Pipes.Count > 0)
			{
				int pipeSizeIndex = m_PipeSizeList.IndexOf(Pipes[0].PipeSize);
				string pipeSizeCode = PartSize.GetSize(pipeSizeIndex).Code;
				int fittingThreadIndex = (new TypeConverters.SizeList()).IndexOf(FittingThreadSize);
				string ftSizeCode = PartSize.GetSize(fittingThreadIndex).Code;
				string fittingCode = Pipes.Count == 1 ? "ST90" : "SSTTEE";

				if (pipeSizeIndex == fittingThreadIndex)
				{
					GetPart(parts, string.Format("FI{0}{1}", pipeSizeCode, fittingCode)).Quantity += 1;
				}
				else //if (pipeSizeIndex - fittingThreadIndex == 1)
				{
					string partKey = string.Format("FI{0}X{1}{2}", pipeSizeCode, ftSizeCode, fittingCode);

					if (parts.ContainsKey(partKey))
					{
						// Part key exists so increment count
						GetPart(parts, partKey).Quantity += 1;
					}
					else
					{
						// Couldn't find part key. Create the part using a ST reducer
						GetPart(parts, string.Format("FI{0}{1}", pipeSizeCode, Pipes.Count == 1 ? "SS90" : "SSSTEE")).Quantity += 1;
						GetPart(parts, string.Format("FI{0}X{1}STRB", pipeSizeCode, ftSizeCode)).Quantity += 1;
					}
				}
			}
		}

		#endregion
	}
}
