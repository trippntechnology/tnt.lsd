using System.ComponentModel;
using System.Drawing;
using System.Reflection;
using System.Windows.Forms;
using System.Xml.Serialization;
using TNT.LSD.Objects;
using TNT.LSD.Objects.ControlPoints;
using TNT.Utilities.CommandManagement;

namespace LSDComponents.DrawingModes
{
	public abstract class DrawingMode
	{
		#region Members

		private TNTControlPoint m_PointerPoint = null;
		private TNTObject m_DefaultObject = null;
		private string m_DefaultObjectType = string.Empty;

		#endregion

		public DrawingMode()
		{
		}

		public DrawingMode(DrawingMode obj)
		{
			m_PointerPoint = obj.m_PointerPoint;

			if (obj.DefaultObject != null)
			{
				m_DefaultObject = obj.m_DefaultObject.Clone();
			}

			m_DefaultObjectType = obj.m_DefaultObjectType;
		}

		#region Properties

		[XmlIgnore()]
		[Browsable(false)]
		virtual public Command ShowPartsToolTip { get; set; }

		[DisplayName("Default Object Type")]
		[Description("Type of the default object")]
		[TypeConverter(typeof(TypeConverters.ObjectTypeListConverter))]
		public string DefaultObjectType
		{
			get { return m_DefaultObjectType; }
			set
			{
				Assembly asm = Assembly.LoadFrom("TNT.LSD.Objects.dll");
				m_DefaultObjectType = value;

				if (!string.IsNullOrEmpty(m_DefaultObjectType))
				{
					if (DefaultObject == null || DefaultObject.GetType().ToString() != m_DefaultObjectType)
					{
						DefaultObject = (TNTObject)asm.CreateInstance(m_DefaultObjectType);
					}
				}
			}
		}

		[DisplayName("Default Object")]
		[Description("Default object used when node is selected")]
		[TypeConverter(typeof(TypeConverters.TNTObjectConverter))]
		virtual public TNTObject DefaultObject
		{
			get { return m_DefaultObject; }
			set { m_DefaultObject = value; }
		}

		[Description("Layer where the action occurs")]
		public int Layer { get; set; }

		[XmlIgnore()]
		[Browsable(false)]
		public virtual bool UndoEnabled { get; protected set; }

		#endregion

		virtual public void OnMouseMove(TNTCAD cad, MouseEventArgs e, Keys modifierKeys) { }
		virtual public void OnMouseUp(TNTCAD cad, MouseEventArgs e, Keys modifierKeys) { }
		virtual public void OnMouseDown(TNTCAD cad, MouseEventArgs e, Keys modifierKeys) { }
		virtual public void OnMouseDoubleClick(TNTCAD cad, MouseEventArgs e) { }
		virtual public void OnMouseClick(TNTCAD cad, MouseEventArgs e, Keys modifierKeys) { }
		virtual public void OnKeyDown(TNTCAD cad, KeyEventArgs e) { }
		virtual public void OnKeyUp(TNTCAD cad, KeyEventArgs e) { }

		abstract public DrawingMode Clone();

		/// <summary>
		/// Resets members to defaults
		/// </summary>
		virtual public void Reset(TNTCAD cad)
		{
			m_PointerPoint = null;
			UndoEnabled = true;
		}

		/// <summary>
		/// Draws the pointer's point
		/// </summary>
		/// <param name="g">Graphics context</param>
		/// <param name="pos">Position where pointer's point should be drawn</param>
		/// <param name="drawingOptions">Drawing options</param>
		virtual protected void DrawPointerPoint(Graphics g, Point pos, DrawingOptions drawingOptions)
		{
			if (m_PointerPoint == null)
			{
				m_PointerPoint = new TNTControlPoint(null, pos);
			}

			m_PointerPoint.MoveTo(pos.X, pos.Y);
			m_PointerPoint.Draw(g, drawingOptions);
		}

		/// <summary>
		/// Returns a listing of valid classes used with this drawing mode
		/// </summary>
		/// <returns>List of valid classes</returns>
		virtual public string[] DefaultObjects()
		{
			if (DefaultObject != null)
			{
				return new string[] { DefaultObject.GetType().ToString() };
			}
			else
			{
				return new string[0];
			}
		}
	}
}
