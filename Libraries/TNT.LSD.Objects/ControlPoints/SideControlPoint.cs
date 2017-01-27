using System.Drawing;
using System.Drawing.Drawing2D;
using System.IO;
using System.Windows.Forms;

namespace TNT.LSD.Objects.ControlPoints
{
	/// <summary>
	/// Represents a side control point
	/// </summary>
	public class SideControlPoint : TNTControlPoint
	{
		/// <summary>
		/// Returns the image that represents this object
		/// </summary>
		protected override Image Image
		{
			get
			{
				if (m_Image == null)
				{
					Stream s = this.GetType().Assembly.GetManifestResourceStream("TNT.LSD.Objects.Images.SideControlPoint.png");
					m_Image = new Bitmap(s);
				}

				return m_Image;
			}
		}

		#region Constructors

		/// <summary>
		/// Copy Constructor
		/// </summary>
		public SideControlPoint()
		{
		}

		/// <summary>
		/// Initializes SideControlPoint
		/// </summary>
		/// <param name="parent">Parent</param>
		/// <param name="xOffset">X offset</param>
		/// <param name="yOffset">Y offset</param>
		public SideControlPoint(TNTObject parent, int xOffset, int yOffset)
			: base(parent, xOffset, yOffset)
		{
		}

		/// <summary>
		/// Initializes SideControlPoint
		/// </summary>
		/// <param name="parent">Parent</param>
		/// <param name="xOffset">X offset</param>
		/// <param name="yOffset">Y offset</param>
		/// <param name="hidden">Indicates whether the point is hidden</param>
		public SideControlPoint(TNTObject parent, int xOffset, int yOffset, bool hidden)
			: base(parent, xOffset, yOffset, hidden)
		{
		}

		/// <summary>
		/// Initializes SideControlPoint
		/// </summary>
		/// <param name="parent">Parent</param>
		/// <param name="position">Position</param>
		public SideControlPoint(TNTObject parent, Point position)
			: base(parent, position)
		{
		}

		/// <summary>
		/// Initializes SideControlPoint
		/// </summary>
		/// <param name="parent">Parent</param>
		/// <param name="position">Position</param>
		/// <param name="hidden">Indicates whether the point is hidden</param>
		public SideControlPoint(TNTObject parent, Point position, bool hidden)
			: base(parent, position, hidden)
		{
		}

		/// <summary>
		/// Copy constructor
		/// </summary>
		/// <param name="parent">New parent</param>
		/// <param name="obj">Object to copy</param>
		public SideControlPoint(TNTObject parent, SideControlPoint obj)
			: base(parent, obj)
		{
		}

		#endregion

		/// <summary>
		/// Determines if mouse is over the object
		/// </summary>
		/// <param name="mousePosition">Mouse position</param>
		/// <param name="modifierKeys">Not used</param>
		/// <returns></returns>
		public override TNTObject MouseOver(Point mousePosition, Keys modifierKeys)
		{
			TNTObject obj = null;

			if (!Hidden)
			{
				int radius = this.Image.Width / 2;
				GraphicsPath path = new GraphicsPath();
				path.AddRectangle(new Rectangle(-radius, -radius, this.Image.Width, this.Image.Height));
				Region region = new Region(path);
				region.Translate(m_Position.X, m_Position.Y);

				obj = region.IsVisible(mousePosition) ? this : null;
			}

			return obj;
		}

		/// <summary>
		/// Creates a copy of this object with the new parent
		/// </summary>
		/// <param name="parent">Parent</param>
		/// <returns>Copy of this object with the new parent</returns>
		public override TNTObject Clone(TNTObject parent)
		{
			return new SideControlPoint(parent, this);
		}
	}
}