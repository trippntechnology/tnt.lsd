using System.Drawing;
using System.Windows.Forms;

namespace TNT.LSD.Objects.ControlPoints
{
	/// <summary>
	/// Represents a rotation control point
	/// </summary>
	public class RotationControlPoint : TNTControlPoint
	{
		/// <summary>
		/// Default constructor
		/// </summary>
		public RotationControlPoint()
			: base()
		{
		}

		/// <summary>
		/// Initializes parent and offset values
		/// </summary>
		/// <param name="parent">Parent</param>
		/// <param name="xOffset">X offset</param>
		/// <param name="yOffset">Y offset</param>
		public RotationControlPoint(TNTObject parent, int xOffset, int yOffset)
			: base(parent, xOffset, yOffset)
		{
		}

		/// <summary>
		/// Copy constructor
		/// </summary>
		/// <param name="parent">Parent</param>
		/// <param name="obj">Object to copy</param>
		public RotationControlPoint(TNTObject parent, RotationControlPoint obj)
			:base (parent, obj)
		{
		}

		/// <summary>
		/// State info for object when mouse is over object
		/// </summary>
		/// <param name="mousePosition">Not used</param>
		/// <param name="modifierKeys">Not used</param>
		/// <returns></returns>
		public override StateInfo GetStateInfo(Point mousePosition, Keys modifierKeys)
		{
			return new StateInfo(TNTCursors.RotationCursor, "Click and drag to rotate image");
		}

		/// <summary>
		/// Create a copy of this object with the parent
		/// </summary>
		/// <param name="parent">Parent</param>
		/// <returns>Copy of this object with the parent</returns>
		public override TNTObject Clone(TNTObject parent)
		{
			return new RotationControlPoint(parent, this);
		}
	}
}
