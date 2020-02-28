using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing.Design;
using System.Linq;
using System.Text.RegularExpressions;
using System.Xml.Serialization;
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
		[XmlIgnore()]
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
			if (m_Pipes.Count > 0)
			{
				reason = "Only one pipe can be connected to a source";
				return false;
			}

			return base.CanAddPipe(pipeType, out reason);
		}

		/// <summary>
		/// Indicates that this is a terminating part (only one connection allowed)
		/// </summary>
		/// <returns></returns>
		public override bool TerminatePipe() => true;

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
				parts[SWKeyCode].Quantity += 1;
			}

			if (IncludeSW)
			{
				#region S&W

				// Get length of key for CL200 pipe
				Match match = Regex.Match(SWKey, "(?<feet>[0-9]*)'");
				int keyLength = 5;

				if (match.Success)
				{
					keyLength = Convert.ToInt32(match.Groups["feet"].Value);
				}

				// Add cl200 pipe and snug cap
				parts["PI200C"].Quantity += System.Math.Max(5, keyLength);
				parts["2SNUGCAP"].Quantity += 1;

				string tee = string.Format("BN{0}TEE", sourceSize.Code);
				string nipples = string.Format("BN{0}X2", sourceSize.Code);
				string sw = string.Format("SW{0}", sourceSize.Code);

				if (SourceType == "Copper")
				{
					GetPart(parts, string.Format("BCT{0}CTS{0}FIPT", sourceSize.Code)).Quantity += 1;
				}
				else if (SourceType == "Poly" || SourceType == "Galvanized")
				{
					GetPart(parts, string.Format("PJ{0}IPSX{0}MIP", sourceSize.Code)).Quantity += 2;
					GetPart(parts, tee).Quantity += 1;
				}
				else if (SourceType == "Poly CTS")
				{
					GetPart(parts, string.Format("PJ{0}CTSX{0}MIP", sourceSize.Code)).Quantity += 2;
					GetPart(parts, string.Format("PJ{0}CTSSTIFFENER", sourceSize.Code)).Quantity += 2;
					GetPart(parts, tee).Quantity += 1;
				}
				else if (SourceType == "PVC")
				{
					GetPart(parts, string.Format("FI{0}SSTTEE", sourceSize.Code)).Quantity += 1;
				}

				GetPart(parts, nipples).Quantity += 1;
				GetPart(parts, string.Format("NI{0}X4TOE", sourceSize.Code)).Quantity += 1;
				GetPart(parts, sw).Quantity += 1;

				#endregion
			}
			else if (SourceType == "Copper")
			{
				GetPart(parts, string.Format("PJ{0}CTSX{0}MIP", sourceSize.Code)).Quantity += 1;
				GetPart(parts, string.Format("BN{0}90", sourceSize.Code)).Quantity += 1;
				GetPart(parts, string.Format("NI{0}X4TOE", sourceSize.Code)).Quantity += 1;
			}
			else if (SourceType == "PVC")
			{
				GetPart(parts, string.Format("FI{0}SSSTEE", sourceSize.Code)).Quantity += 1;
				GetPart(parts, string.Format("FI{0}SSCOUP", sourceSize.Code)).Quantity += 1;
			}
			else if (SourceType == "Galvanized" || SourceType == "Poly")
			{
				GetPart(parts, string.Format("PJ{0}IPSX{0}MIP", sourceSize.Code)).Quantity += 1;
				GetPart(parts, string.Format("BN{0}90", sourceSize.Code)).Quantity += 1;
				GetPart(parts, string.Format("NI{0}X4TOE", sourceSize.Code)).Quantity += 1;
			}
			else if (SourceType == "Poly CTS")
			{
				GetPart(parts, string.Format("PJ{0}CTSX{0}MIP", sourceSize.Code)).Quantity += 1;
				GetPart(parts, string.Format("PJ{0}CTSSTIFFENER", sourceSize.Code)).Quantity += 1;
				GetPart(parts, string.Format("BN{0}90", sourceSize.Code)).Quantity += 1;
				GetPart(parts, string.Format("NI{0}X4TOE", sourceSize.Code)).Quantity += 1;
			}

			if (IncludeSW || SourceType != "PVC")
			{
				if (sourceSize <= mainSize)
				{
					GetPart(parts, string.Format("FI{0}SSCOUPSCH80", mainSize.Code)).Quantity += 1;
				}

				if (sourceSize < mainSize)
				{
					GetPart(parts, string.Format("FI{0}X{1}SSRBSCH80", mainSize.Code, sourceSize.Code)).Quantity += 1;
				}
				else if (sourceSize > mainSize)
				{
					GetPart(parts, string.Format("FI{0}SSCOUPSCH80", sourceSize.Code)).Quantity += 1;
					GetPart(parts, string.Format("FI{0}X{1}SSRB", sourceSize.Code, mainSize.Code)).Quantity += 1;
				}

				GetPart(parts, string.Format("FI{0}SS90", mainSize.Code)).Quantity += 1;

				//// Added elbow
				//if (sourceSizeIndex == mainSizeIndex)
				//{
				//	GetPart(parts, string.Format("FI{0}ST90", sourceSizeCode)).Quantity += 1;
				//}
				//else if (sourceSizeIndex < mainSizeIndex)
				//{
				//	GetPart(parts, string.Format("FI{0}SS90", mainSizeCode)).Quantity += 1;
				//	GetPart(parts, string.Format("FI{0}X{1}STRB", mainSizeCode, sourceSizeCode)).Quantity += 1;
				//}
				//else
				//{
				//	GetPart(parts, string.Format("FI{0}ST90", sourceSizeCode)).Quantity += 1;
				//	GetPart(parts, string.Format("FI{0}X{1}SSRBSCH80", sourceSizeCode, mainSizeCode)).Quantity += 1;
				//}
			}
		}
	}
}
