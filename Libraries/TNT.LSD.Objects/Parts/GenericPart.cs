using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing.Design;
using System.Xml.Serialization;
using TNT.LSD.Inventory;
using TNT.LSD.Settings;

namespace TNT.LSD.Objects
{
	public abstract class GenericPart : PalettePart
	{
		#region Properties

		#region Model

		protected virtual void onModelIndexChanged(int index) { }

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
					{
						ModelCode = m_ModelCodes[descriptionIndex];
						onModelIndexChanged(descriptionIndex);
					}
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

		#endregion

		#region Constructors

		public GenericPart(GenericPart obj)
			: base(obj)
		{
			ModelCodes = obj.ModelCodes == null ? null : new List<string>(obj.ModelCodes);
			ModelDescriptions = obj.ModelDescriptions == null ? null : new List<string>(obj.ModelDescriptions);
			Model = obj.Model;
			ModelCode = obj.ModelCode;
		}

		public GenericPart()
			: base()
		{
		}

		#endregion

		#region Overrides

		/// <summary>
		/// Creates undo copy
		/// </summary>
		/// <returns>Undo copy</returns>
		public override TNTObject CreateUndoCopy()
		{
			GenericPart newObj = base.CreateUndoCopy() as GenericPart;

			newObj.Model = Model;

			return newObj;
		}

		/// <summary>
		/// Set obj's properties on this part
		/// </summary>
		/// <param name="obj">Source obj</param>
		public override void Assign(TNT.LSD.Objects.TNTObject obj)
		{
			GenericPart gp = obj as GenericPart;

			if (gp != null)
			{
				Model = gp.Model;
			}

			base.Assign(obj);
		}

		public override void SetPartQuantity(CodedParts parts, SystemType systemType)
		{
			base.SetPartQuantity(parts, systemType);

			string[] modelCodes = ModelCode.Split(';');

			foreach (string code in modelCodes)
			{
				GetPart(parts, code).Quantity += 1;
			}
		}

		#endregion
	}
}
