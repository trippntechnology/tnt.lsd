using System.Drawing;
using System.Windows.Forms;

namespace TNT.LSD.Objects.ControlPoints
{
	/// <summary>
	/// Represents a point that adjusts the spray arc
	/// </summary>
	public class AdjustableControlPoint : RotationControlPoint
	{
		#region Constructors

				/// <summary>
		/// Default constructor
		/// </summary>
		public AdjustableControlPoint()
			: base()
		{
		}

		/// <summary>
		/// Initializes parent and offset values
		/// </summary>
		/// <param name="parent">Parent</param>
		/// <param name="xOffset">X offset</param>
		/// <param name="yOffset">Y offset</param>
		public AdjustableControlPoint(TNTObject parent, int xOffset, int yOffset)
			: base(parent, xOffset, yOffset)
		{
		}

		/// <summary>
		/// Copy constructor
		/// </summary>
		/// <param name="parent">Parent</param>
		/// <param name="obj">Object to copy</param>
		public AdjustableControlPoint(TNTObject parent, AdjustableControlPoint obj)
			:base (parent, obj)
		{
		}

		#endregion

		/// <summary>
		/// State info for object when mouse is over object
		/// </summary>
		/// <param name="mousePosition">Not used</param>
		/// <param name="modifierKeys">Not used</param>
		/// <returns></returns>
		public override StateInfo GetStateInfo(Point mousePosition, Keys modifierKeys)
		{
			return new StateInfo(TNTCursors.RotationCursor, "Click and drag to adjust arc");
		}

		/// <summary>
		/// Create a copy of this object with the parent
		/// </summary>
		/// <param name="parent">Parent</param>
		/// <returns>Copy of this object with the parent</returns>
		public override TNTObject Clone(TNTObject parent)
		{
			return new AdjustableControlPoint(parent, this);
		}

	}
}
