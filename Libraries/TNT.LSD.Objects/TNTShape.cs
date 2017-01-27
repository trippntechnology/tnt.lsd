using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Windows.Forms;
using System.Xml.Serialization;
using TNT.LSD.Objects.ControlPoints;
using TNT.LSD.Objects.Extensions;

namespace TNT.LSD.Objects
{
	public class TNTShape : TNTObject
	{
		#region Members

		protected Color m_FillColor;

		#endregion

		#region Properties

		[DisplayName("Line Color")]
		[Description("Indicates the line color.")]
		[DefaultValue(typeof(Color), "Black")]
		[XmlIgnore()]
		public Color LineColor { get; set; }

		[Browsable(false)]
		public int _LineColor
		{
			get { return LineColor.ToArgb(); }
			set { LineColor = Color.FromArgb(value); }
		}

		[DisplayName("Fill Color")]
		[Description("Indicates the fill color.")]
		[DefaultValue(typeof(Color), "Transparent")]
		[XmlIgnore()]
		public Color FillColor
		{
			get { return m_FillColor; }
			set
			{
				m_FillColor = value;

				FillOpacity = FillColor == Color.Transparent || FillColor == Color.FromArgb(0, 255, 255, 255) ? 0 : FillOpacity == 0 ? 255 : FillOpacity;
			}
		}

		[Browsable(false)]
		public int _FillColor
		{
			get { return FillColor.ToArgb(); }
			set { FillColor = Color.FromArgb(value); }
		}

		[DisplayName("Fill Opacity")]
		[Description("Specifies the opacity of the fill color.")]
		[DefaultValue(0)]
		public int FillOpacity { get; set; }

		[DisplayName("Line Width")]
		[Description("Indicates the line width.")]
		[DefaultValue(1)]
		public int LineWidth { get; set; }

		[Description("Indicates the line style.")]
		[DisplayName("Line Style")]
		[DefaultValue(DashStyle.Solid)]
		public DashStyle LineStyle { get; set; }

		//[Description("Indicates the angle that the shape should be rotated clockwise.")]
		//[DisplayName("Rotation Angle")]
		//[DefaultValue(0)]
		//public int RotationAngle { get; set; }

		#endregion

		#region Constructors

		public TNTShape()
			: base()
		{
			LineColor = Color.Black;
			FillColor = Color.Transparent;
			LineWidth = 1;
			LineStyle = DashStyle.Solid;
			FillOpacity = 0;
		}

		public TNTShape(TNTShape obj)
			: base(obj)
		{
			LineColor = obj.LineColor;
			FillColor = obj.FillColor;
			LineWidth = obj.LineWidth;
			LineStyle = obj.LineStyle;
			FillOpacity = obj.FillOpacity;

			foreach (TNTControlPoint cp in ControlPoints)
			{
				cp.OnMoved = ControlPointMoved;
			}
		}

		public TNTShape(Rectangle rect, Color lineColor)
			: base()
		{
			LineColor = lineColor;
			FillColor = Color.Transparent;
			LineWidth = 1;
			LineStyle = DashStyle.Solid;
			FillOpacity = 0;
			CreateControlPoints(rect);

			foreach (TNTControlPoint cp in ControlPoints)
			{
				cp.OnMoved = ControlPointMoved;
			}
		}

		#endregion

		#region Protected

		virtual protected void CreateControlPoints(Rectangle rect)
		{
			ControlPoints.Add(new TNTControlPoint(this, rect.Location));
			ControlPoints.Add(new SideControlPoint(this, new Point(rect.Left + ((rect.Right - rect.Left) / 2), rect.Top)));
			ControlPoints.Add(new TNTControlPoint(this, new Point(rect.Right, rect.Top)));
			ControlPoints.Add(new SideControlPoint(this, new Point(rect.Right, rect.Top + ((rect.Bottom - rect.Top) / 2))));
			ControlPoints.Add(new TNTControlPoint(this, new Point(rect.Right, rect.Bottom)));
			ControlPoints.Add(new SideControlPoint(this, new Point(rect.Left + ((rect.Right - rect.Left) / 2), rect.Bottom)));
			ControlPoints.Add(new TNTControlPoint(this, new Point(rect.Left, rect.Bottom)));
			ControlPoints.Add(new SideControlPoint(this, new Point(rect.Left, rect.Top + ((rect.Bottom - rect.Top) / 2))));
		}

		virtual protected Rectangle GetRectangle()
		{
			int left = System.Math.Min(ControlPoints[0].XPos, ControlPoints[2].XPos);
			int top = System.Math.Min(ControlPoints[0].YPos, ControlPoints[6].YPos);
			int right = System.Math.Max(ControlPoints[0].XPos, ControlPoints[2].XPos);
			int bottom = System.Math.Max(ControlPoints[0].YPos, ControlPoints[6].YPos);

			return new Rectangle(left, top, right - left, bottom - top);
		}

		protected int GetMidPoint(int x1, int x2)
		{
			return System.Math.Min(x1, x2) + System.Math.Abs(x1 - x2) / 2;
		}

		#endregion

		#region Overrides

		public override void DrawDistances(Graphics graphics)
		{
			for (int index = 2; index < 5; index += 2)
			{
				DrawDistance(graphics, ControlPoints[index - 2], ControlPoints[index]);
			}
		}

		public override TNTObject MouseOver(Point mousePosition, Keys modifierKeys)
		{
			TNTObject isOver = null;

			GraphicsPath path = new GraphicsPath();

			path.AddRectangle(GetRectangle());

			Region region = new Region(path);
			isOver = region.IsVisible(mousePosition) ? this : null;

			if (isOver == null && Selected)
			{
				isOver = GetControlPoint(mousePosition, modifierKeys);
			}

			return isOver;
		}

		public override void MoveBy(int deltaX, int deltaY, bool alignPoints)
		{
			base.MoveBy(deltaX, deltaY, alignPoints);
		}

		/// <summary>
		/// Gets a clone of this object
		/// </summary>
		/// <param name="isForUndo">Not used</param>
		/// <returns>Clone of this object</returns>
		public override TNTObject Clone()
		{
			return new TNTShape(this);
		}

		public override bool InRectangle(System.Drawing.Rectangle rectangle)
		{
			bool inRect = true;

			for (int index = 0; index < ControlPoints.Count && inRect; index++)
			{
				inRect = rectangle.Contains(ControlPoints[index].Position);
			}

			return inRect;
		}

		public override void ResolveReferences(List<TNTObject> objects)
		{
			foreach (TNTControlPoint cp in ControlPoints)
			{
				cp.OnMoved = ControlPointMoved;
			}
		}

		/// <summary>
		/// Creates a copy of the object for an undo event
		/// </summary>
		/// <returns>Copy of the object for an undo event</returns>
		public override TNTObject CreateUndoCopy()
		{
			TNTShape newShape = base.CreateUndoCopy() as TNTShape;

			newShape.ControlPoints.ForEach(cp => cp.OnMoved = ControlPointMoved);
			var cps = newShape.ControlPoints;

			newShape.LineColor = LineColor;
			newShape.FillColor = FillColor;
			newShape.LineWidth = LineWidth;
			newShape.LineStyle = LineStyle;
			newShape.FillOpacity = FillOpacity;

			return newShape;
		}

		public override void Assign(TNT.LSD.Objects.TNTObject obj)
		{
			base.Assign(obj);

			TNTShape s = obj as TNTShape;

			if (s != null)
			{
				LineColor = s.LineColor;
				FillColor = s.FillColor;
				LineWidth = s.LineWidth;
				LineStyle = s.LineStyle;
				FillOpacity = s.FillOpacity;
			}
		}

		public override void AlignToGrid()
		{
			Point cp0 = ControlPoints[0].Position.SnapToGrid();
			Point cp4 = ControlPoints[4].Position.SnapToGrid();

			ControlPoints[0].MoveTo(cp0.X, cp0.Y, true);
			ControlPoints[4].MoveTo(cp4.X, cp4.Y, true);
		}

		#endregion

		#region Callbacks

		protected virtual void ControlPointMoved(TNTControlPoint Sender)
		{
			if (m_ActiveControlPoint == null)
			{
				m_ActiveControlPoint = Sender;
				int activeIndex = -1;

				for (int index = 0; index < ControlPoints.Count; index++)
				{
					if (ControlPoints[index] == Sender)
					{
						activeIndex = index;
						break;
					}
				}

				int activeX = m_ActiveControlPoint.Position.X;
				int activeY = m_ActiveControlPoint.Position.Y;

				switch (activeIndex)
				{
					case 0:
						ControlPoints[1].YPos = activeY;
						ControlPoints[2].YPos = activeY;
						ControlPoints[6].XPos = activeX;
						ControlPoints[7].XPos = activeX;
						break;
					case 1:
						ControlPoints[0].YPos = activeY;
						ControlPoints[2].YPos = activeY;
						break;
					case 2:
						ControlPoints[0].YPos = activeY;
						ControlPoints[1].YPos = activeY;
						ControlPoints[3].XPos = activeX;
						ControlPoints[4].XPos = activeX;
						break;
					case 3:
						ControlPoints[2].XPos = activeX;
						ControlPoints[4].XPos = activeX;
						break;
					case 4:
						ControlPoints[2].XPos = activeX;
						ControlPoints[3].XPos = activeX;
						ControlPoints[5].YPos = activeY;
						ControlPoints[6].YPos = activeY;
						break;
					case 5:
						ControlPoints[4].YPos = activeY;
						ControlPoints[6].YPos = activeY;
						break;
					case 6:
						ControlPoints[4].YPos = activeY;
						ControlPoints[5].YPos = activeY;
						ControlPoints[7].XPos = activeX;
						ControlPoints[0].XPos = activeX;
						break;
					case 7:
						ControlPoints[6].XPos = activeX;
						ControlPoints[0].XPos = activeX;
						break;
				}

				// Adjust move side points to the midpoint
				ControlPoints[1].XPos = GetMidPoint(ControlPoints[0].XPos, ControlPoints[2].XPos);
				ControlPoints[3].YPos = GetMidPoint(ControlPoints[2].YPos, ControlPoints[4].YPos);
				ControlPoints[5].XPos = GetMidPoint(ControlPoints[6].XPos, ControlPoints[4].XPos);
				ControlPoints[7].YPos = GetMidPoint(ControlPoints[0].YPos, ControlPoints[6].YPos);

				m_ActiveControlPoint = null;
			}
		}

		#endregion

	}
}
