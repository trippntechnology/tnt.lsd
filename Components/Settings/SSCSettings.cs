using System.ComponentModel;
using System.Drawing.Design;
using System.Text.RegularExpressions;
using TNT.LSD.Objects.TypeConverters;
using TNT.Utilities.CustomAttributes;

namespace LSDComponents.Settings
{
	public class SSCSettings: CADSettings
	{
		#region Owner

		[PropertyReflectorAttribute()]
		[Category("\t\t\tOwner")]
		[DisplayName("Design Number")]
		[Description("Indicates the design number.")]
		virtual public string Number { get; set; }

		[PropertyReflectorAttribute()]
		[Category("\t\t\tOwner")]
		[Description("Name of the property owner.")]
		virtual public string Name { get; set; }

		[PropertyReflectorAttribute()]
		[Category("\t\t\tOwner")]
		[DisplayName("Telephone Number")]
		[Description("Telephone number where owner can be reached.")]
		virtual public string TelephoneNumber
		{
			get { return m_TelephoneNumber; }
			set
			{
				string tmp = Regex.Replace(value, "[^0-9]*", "");

				if (tmp.Length == 7)
				{
					m_TelephoneNumber = string.Format("{0}-{1}", tmp.Substring(0, 3), tmp.Substring(3, 4));
				}
				else if (tmp.Length == 10)
				{
					m_TelephoneNumber = string.Format("({0}) {1}-{2}", tmp.Substring(0, 3), tmp.Substring(3, 3), tmp.Substring(6, 4));
				}
				else
				{
					m_TelephoneNumber = value;
				}
			}
		}

		[PropertyReflectorAttribute()]
		[Category("\t\t\tOwner")]
		[DisplayName("eMail Address")]
		[Description("Owner's email address.")]
		virtual public string EmailAddress { get; set; }

		#endregion

		#region Culinary properties

		[PropertyReflectorAttribute()]
		[TypeConverter(typeof(YesNoNAList))]
		[Category("\t\tCulinary Source")]
		[DisplayName("Include Culinary Stop and Waste")]
		[Description("Stop and Waste values provide a way to turn the water on and off to the sprinkler system.")]
		virtual public string IncludeCulinarySW { get; set; }

		[PropertyReflectorAttribute()]
		[TypeConverter(typeof(PSIList))]
		[Category("\t\tCulinary Source")]
		[DisplayName("Working Culinary PSI")]
		[Description("Working (dynamic) water pressure.")]
		virtual public string CulinaryPSI { get; set; }

		[PropertyReflectorAttribute()]
		[TypeConverter(typeof(PipeSizeList))]
		[Category("\t\tCulinary Source")]
		[DisplayName("Culinary Supply Size")]
		[Description("Size of pipe that provides the water supply.")]
		virtual public string CulinarySize { get; set; }

		[PropertyReflectorAttribute()]
		[TypeConverter(typeof(PipeTypeList))]
		[Category("\t\tCulinary Source")]
		[DisplayName("Culinary Supply Type")]
		[Description("Type of pipe that provides the water supply.")]
		virtual public string CulinaryType { get; set; }

		#endregion

		#region Secondary properties

		[PropertyReflectorAttribute()]
		[TypeConverter(typeof(YesNoNAList))]
		[Category("\t\tSecondary Source")]
		[DisplayName("Include Secondary Stop and Waste")]
		[Description("Stop and Waste values provide a way to turn the water on and off to the sprinkler system.")]
		virtual public string IncludeSecondarySW { get; set; }

		[PropertyReflectorAttribute()]
		[TypeConverter(typeof(PSIList))]
		[Category("\t\tSecondary Source")]
		[DisplayName("Working Secondary PSI")]
		[Description("Working (dynamic) water pressure.")]
		virtual public string SecondaryPSI { get; set; }

		[PropertyReflectorAttribute()]
		[TypeConverter(typeof(PipeSizeList))]
		[Category("\t\tSecondary Source")]
		[DisplayName("Secondary Supply Size")]
		[Description("Size of pipe that provides the water supply.")]
		virtual public string SecondarySize { get; set; }

		[PropertyReflectorAttribute()]
		[TypeConverter(typeof(PipeTypeList))]
		[Category("\t\tSecondary Source")]
		[DisplayName("Secondary Supply Type")]
		[Description("Type of pipe that provides the water supply.")]
		virtual public string SecondaryType { get; set; }

		#endregion

		[Category("\tMisc")]
		[Description("Additional comments")]
		[Editor(typeof(System.ComponentModel.Design.MultilineStringEditor), typeof(UITypeEditor))]
		virtual public string Comment { get; set; }

		[Category("Part Options")]
		[DisplayName("Show External Codes")]
		[Description("If true, codes displayed in the parts listing will be those mapped to internal codes.")]
		virtual public bool ShowExternalCodes { get; set; }

		public override string ToString()
		{
			return string.Format("{0}{1}", Name, string.IsNullOrEmpty(Number) ? "" : string.Format(" ({0})", Number));
		}
	}
}
