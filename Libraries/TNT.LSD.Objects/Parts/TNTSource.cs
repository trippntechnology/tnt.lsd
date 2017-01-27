using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing.Design;
using System.Text.RegularExpressions;
using System.Xml.Serialization;

namespace TNT.LSD.Objects
{
	public class TNTSource : MainlinePart
	{
		#region Properties

		[Description("Indicates the number Gallons of water available per minute (GPM) at this source.")]
		[DisplayName("Available GPM")]
		public int AvailableGPM { get; set; }

		[Description("Indicates whether a Stop && Waste valve is needed to connect to this source.")]
		[DisplayName("Include Stop && Waste")]
		public bool IncludeSW { get; set; }

		[TypeConverter(typeof(TypeConverters.PipeSizeList))]
		[Description("Indicates the pipe size of the source being connected to.")]
		[DisplayName("Source Size")]
		public string SourceSize { get; set; }

		[TypeConverter(typeof(TypeConverters.PipeTypeList))]
		[Description("Indicates the pipe type of the source being connected to.")]
		[DisplayName("Source Type")]
		public string SourceType { get; set; }

		#region S&W Key

		protected string m_SWKey;

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

		[XmlIgnore()]
		[Browsable(false)]
		public List<string> SWKeyDescriptions { get; set; }

		[Description("The part code associated with the Stop && Waste Key")]
		[DisplayName("Stop && Waste Code")]
		[ReadOnly(true)]
#if !PALETTE_PROPERTIES
		[Browsable(false)]
#endif
		public string SWKeyCode { get; set; }

		[Editor(@"System.Windows.Forms.Design.StringCollectionEditor, System.Design, Version=2.0.0.0, Culture=neutral, PublicKeyToken=b03f5f7f11d50a3a", typeof(UITypeEditor))]
#if !PALETTE_PROPERTIES
		[Browsable(false)]
#endif
		public List<string> SWKeyCodes { get; set; }

		#endregion

		#endregion

		#region Constructors

		public TNTSource()
			: base()
		{
			SourceSize = "3/4\"";
			SourceType = "Copper";
		}

		// Copy constructor
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

		public override TNTObject Clone()
		{
			return new TNTSource(this);
		}

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

		public override bool CanAddPipe(System.Type pipeType, out string reason)
		{
			if (m_Pipes.Count > 0)
			{
				reason = "Only one pipe can be connected to a source";
				return false;
			}

			return base.CanAddPipe(pipeType, out reason);
		}

		public override bool TerminatePipe()
		{
			return true;
		}

		public override void SetPartQuantity(Dictionary<string, TNT.LSD.Inventory.Part> parts)
		{
			if (!string.IsNullOrEmpty(SWKeyCode))
			{
				parts[SWKeyCode].Quantity += 1;
			}

			int sourceSizeIndex = m_PipeSizeList.IndexOf(SourceSize);
			int mainSizeIndex = Pipes != null && Pipes.Count > 0 ? m_PipeSizeList.IndexOf(Pipes[0].PipeSize) : -1;
			string sourceSizeCode = SIZE_CODE[sourceSizeIndex];
			string mainSizeCode = mainSizeIndex > -1 ? SIZE_CODE[mainSizeIndex] : string.Empty;

			if (IncludeSW)
			{
				#region S&W

				Match match = Regex.Match(SWKey, "(?<feet>[0-9]*)'");
				int keyLength = 5;

				if (match.Success)
				{
					keyLength = Convert.ToInt32(match.Groups["feet"].Value);
				}

				// Add cl200 pipe and snug cap
				parts["PI200C"].Quantity += System.Math.Max(5, keyLength);
				parts["2SNUGCAP"].Quantity += 1;

				string tee = string.Format("BN{0}TEE", sourceSizeCode);
				string nipples = string.Format("BN{0}X2", sourceSizeCode);
				string sw = string.Format("SW{0}", sourceSizeCode);

				if (SourceType == "Copper")
				{
					GetPart(parts, string.Format("BCT{0}CTS{0}FIPT", sourceSizeCode)).Quantity += 1;
				}
				else if (SourceType == "Poly" || SourceType == "Galvanized")
				{
					GetPart(parts, string.Format("PJ{0}IPSX{0}MIP", sourceSizeCode)).Quantity += 2;
					GetPart(parts, tee).Quantity += 1;
				}
				else if (SourceType == "Poly CTS")
				{
					GetPart(parts, string.Format("PJ{0}CTSX{0}MIP", sourceSizeCode)).Quantity += 2;
					GetPart(parts, string.Format("PJ{0}CTSSTIFFENER", sourceSizeCode)).Quantity += 2;
					GetPart(parts, tee).Quantity += 1;
				}
				else if (SourceType == "PVC")
				{
					GetPart(parts, string.Format("FI{0}SSTTEE", sourceSizeCode)).Quantity += 1;
					GetPart(parts, tee).Quantity += 1;
				}

				GetPart(parts, nipples).Quantity += 2;
				GetPart(parts, sw).Quantity += 1;

				#endregion
			}
			else if (SourceType == "Copper")
			{
				GetPart(parts, string.Format("PJ{0}CTSX{0}MIP", sourceSizeCode)).Quantity += 1;
			}
			else if (SourceType == "PVC")
			{
				GetPart(parts, string.Format("FI{0}SSSTEE", sourceSizeCode)).Quantity += 1;
				GetPart(parts, string.Format("FI{0}SSCOUP", sourceSizeCode)).Quantity += 1;
			}
			else if (SourceType == "Galvanized" || SourceType == "Poly")
			{
				GetPart(parts, string.Format("PJ{0}IPSX{0}MIP", sourceSizeCode)).Quantity += 1;
			}
			else if (SourceType == "Poly CTS")
			{
				GetPart(parts, string.Format("PJ{0}CTSX{0}MIP", sourceSizeCode)).Quantity += 1;
				GetPart(parts, string.Format("PJ{0}CTSSTIFFENER", sourceSizeCode)).Quantity += 1;
			}

			if (mainSizeIndex > -1 && (IncludeSW || SourceType != "PVC"))
			{
				// Added elbow
				if (sourceSizeIndex == mainSizeIndex)
				{
					GetPart(parts, string.Format("FI{0}ST90", sourceSizeCode)).Quantity += 1;
				}
				else if (sourceSizeIndex < mainSizeIndex)
				{
					GetPart(parts, string.Format("FI{0}SS90", mainSizeCode)).Quantity += 1;
					GetPart(parts, string.Format("FI{0}X{1}STRB", mainSizeCode, sourceSizeCode)).Quantity += 1;
				}
				else
				{
					GetPart(parts, string.Format("FI{0}ST90", sourceSizeCode)).Quantity += 1;
					GetPart(parts, string.Format("FI{0}X{1}SSRB", sourceSizeCode, mainSizeCode)).Quantity += 1;
				}
			}
		}
	}
}
