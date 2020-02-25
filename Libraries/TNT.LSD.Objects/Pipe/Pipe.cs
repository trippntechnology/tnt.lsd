using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Linq;
using System.Xml.Serialization;
using TNT.LSD.Inventory;
using TNT.LSD.Objects.Extensions;
using TNT.LSD.Settings;
using TNT.LSD.Settings.TypeConverters;
using TNT.Math;

namespace TNT.LSD.Objects
{
	/// <summary>
	/// Represents a pipe segment
	/// </summary>
	public abstract class Pipe : BasePart
	{
		const int PIPE_SIZE_INDICATOR_PIXELS = 4;

		#region Properties

		[XmlIgnore()]
		[Browsable(false)]
		public TNTPart Part1 { get; set; }

		[XmlIgnore()]
		[Browsable(false)]
		public TNTPart Part2 { get; set; }

		[Browsable(false)]
		[DefaultValue("")]
		public string _Part1
		{
			get
			{
				if (Part1 != null)
				{
					return Part1.ID.ToString();
				}
				else
				{
					return string.Empty;
				}
			}
			set { Part1 = new TNTPart() { ID = new Guid(value) }; }
		}

		[Browsable(false)]
		[DefaultValue("")]
		public string _Part2
		{
			get
			{
				if (Part2 != null)
				{
					return Part2.ID.ToString();
				}
				else
				{
					return string.Empty;
				}
			}
			set { Part2 = new TNTPart() { ID = new Guid(value) }; }
		}

		[Description("Indicates whether the pipe should be automatically sized based on the flow")]
		[DisplayName("Auto Size")]
		[DefaultValue(true)]
		public bool AutoSize { get; set; }

		[Description("Indicates the pipe color.")]
		[DisplayName("Pipe Color")]
		[DefaultValue(typeof(Color), "Black")]
		[XmlIgnore()]
		public Color PipeColor { get; set; }

		[Browsable(false)]
		[DefaultValue(0)]
		public int _PipeColor
		{
			get { return PipeColor.ToArgb(); }
			set { PipeColor = Color.FromArgb(value); }
		}

		[TypeConverter(typeof(PipeSizeList))]
		[Description("Indicates the pipe size.")]
		[DisplayName("Pipe Size")]
		[DefaultValue("3/4\"")]
		public string PipeSize { get; set; }

		[Description("Indicates the pipe style.")]
		[DisplayName("Pipe Style")]
		[DefaultValue(DashStyle.Solid)]
		public DashStyle LineStyle { get; set; }

		[XmlIgnore()]
		public override bool Selected
		{
			get
			{
				return base.Selected || (Part1 != null && Part1.Selected) || (Part2 != null && Part2.Selected);
			}
			set
			{
				base.Selected = value;
			}
		}

		public override bool CanClone { get { return false; } }

		public override Point Position
		{
			get
			{
				PointF midPoint = Part1.Position.GetMidPoint(Part2.Position);
				return new Point((int)midPoint.X, (int)midPoint.Y);
			}
		}

		public int PipeSizeIndex => m_PipeSizeList.IndexOf(PipeSize);

		#endregion

		#region Constructors

		/// <summary>
		/// Constructor
		/// </summary>
		public Pipe()
			: base()
		{
			AutoSize = true;
			PipeSize = "3/4\"";
		}

		/// <summary>
		/// Constructor
		/// </summary>
		/// <param name="part1">First part of the pipe segment</param>
		/// <param name="part2">Second part of the pipe segment</param>
		public Pipe(TNTPart part1, TNTPart part2)
			: base()
		{
			Part1 = part1;
			Part2 = part2;
			AutoSize = true;
			PipeSize = "3/4\"";
		}

		/// <summary>
		/// Copy constructor
		/// </summary>
		/// <param name="obj">Object to copy</param>
		public Pipe(Pipe obj)
			: base(obj)
		{
			Part1 = obj.Part1;
			Part2 = obj.Part2;
			AutoSize = obj.AutoSize;
			PipeSize = obj.PipeSize;
			this.LineStyle = obj.LineStyle;
			this.PipeColor = obj.PipeColor;
		}

		#endregion

		#region Overrides

		/// <summary>
		/// Creates undo copy
		/// </summary>
		/// <returns>Undo copy</returns>
		public override TNTObject CreateUndoCopy()
		{
			Pipe newObj = base.CreateUndoCopy() as Pipe;

			newObj.Part1 = Part1;
			newObj.Part2 = Part2;
			newObj.AutoSize = AutoSize;
			newObj.PipeSize = PipeSize;
			newObj.LineStyle = LineStyle;
			newObj.PipeColor = PipeColor;

			return newObj;
		}

		/// <summary>
		/// Assigns obj's properties to this object
		/// </summary>
		/// <param name="obj">Source object</param>
		public override void Assign(TNT.LSD.Objects.TNTObject obj)
		{
			base.Assign(obj);

			Pipe pipe = obj as Pipe;

			if (pipe != null)
			{
				Part1 = pipe.Part1;
				Part2 = pipe.Part2;
				AutoSize = pipe.AutoSize;
				PipeColor = pipe.PipeColor;
				PipeSize = pipe.PipeSize;
				LineStyle = pipe.LineStyle;
			}
		}

		public override void Draw(Graphics graphics, DrawingOptions drawingOptions)
		{
			using (Pen p = new Pen(Color.FromArgb(Selected ? 100 : 255, PipeColor)))
			{
				p.DashStyle = LineStyle;
				graphics.DrawLine(p, Part1.Position, Part2.Position);

				if (PipeSizeIndex > 1)
				{
					var solidBrush = new SolidBrush(Color.FromArgb(255, this.PipeColor));
					var pipeVector = new Vector(Part1.Position, Part2.Position);
					var midPoint = Part1.Position + (pipeVector / 2.0);
					var angle = Convert.ToSingle(pipeVector.Unit.Angle().InDegrees);
					var unitVector = new Vector(new PointF(-1, -1)).Unit;
					var adjVector = unitVector * PIPE_SIZE_INDICATOR_PIXELS;
					p.Width = 1;

					var point = (PointF)adjVector;
					var rect = new RectangleF(point.X, point.Y, point.X * -2, point.Y * -2);

					graphics.TranslateTransform(midPoint.X, midPoint.Y);
					graphics.RotateTransform(angle);

					if (PipeSizeIndex == 2)
					{
						// Diamond
						var p0 = new Vector(0, 1).Unit * PIPE_SIZE_INDICATOR_PIXELS;
						var p1 = new Vector(-1, 0).Unit * PIPE_SIZE_INDICATOR_PIXELS;
						var p2 = new Vector(0, -1).Unit * PIPE_SIZE_INDICATOR_PIXELS;
						var p3 = new Vector(1, 0).Unit * PIPE_SIZE_INDICATOR_PIXELS;
						var points = new PointF[] { (PointF)p0, (PointF)p1, (PointF)p2, (PointF)p3 };
						graphics.FillPolygon(solidBrush, points);
					}
					else if (PipeSizeIndex == 3)
					{
						// Rectangle
						graphics.FillRectangle(solidBrush, rect);
					}
					else if (PipeSizeIndex == 4)
					{
						// Circle
						graphics.FillEllipse(solidBrush, rect);
					}
					else if (PipeSizeIndex == 5)
					{
						// Triangle
						var p0 = new Vector(0, 0);
						var p1 = new Vector(1, 1).Unit * PIPE_SIZE_INDICATOR_PIXELS;
						var p2 = new Vector(1, -1).Unit * PIPE_SIZE_INDICATOR_PIXELS;
						var p3 = new Vector(-1, -1).Unit * PIPE_SIZE_INDICATOR_PIXELS;
						var p4 = new Vector(-1, 1).Unit * PIPE_SIZE_INDICATOR_PIXELS;
						var points = new PointF[] { (PointF)p1, (PointF)p2, (PointF)p0, (PointF)p3, (PointF)p4, (PointF)p0 };
						graphics.FillPolygon(solidBrush, points);
					}

					graphics.RotateTransform(-angle);
					graphics.TranslateTransform(-midPoint.X, -midPoint.Y);
				}
			}

			base.Draw(graphics, drawingOptions);
		}

		public override TNTObject MouseOver(System.Drawing.Point mousePosition, System.Windows.Forms.Keys modifierKeys)
		{
			TNTObject isOver = null;

			// On check to see if mouse is over the pipe segment when the segment has some length, otherwise ignore.
			if (Part1.Position != Part2.Position)
			{
				Pen p = new Pen(Selected ? Color.Red : Color.Black, 10);
				GraphicsPath path = new GraphicsPath();
				List<Point> points = new List<Point>();
				points.Add(Part1.Position);
				points.Add(Part2.Position);

				path.AddLine(Part1.Position, Part2.Position);
				path.Widen(p);

				isOver = path.IsVisible(mousePosition) ? this : null;
			}

			return isOver;
		}

		public override bool InRectangle(System.Drawing.Rectangle rectangle)
		{
			return rectangle.Contains(Part1.Position) && rectangle.Contains(Part2.Position);
		}

		public override void AlignToGrid()
		{
			// Pipe will align using the parts
		}

		public override void ResolveReferences(List<TNTObject> objects)
		{
			TNTPart part = (from o in objects where o is TNTPart && o.ID == Part1.ID select o as TNTPart).SingleOrDefault();

			if (part != null)
			{
				Part1 = part;
				part.AddPipe(this);
			}

			part = (from o in objects where o is TNTPart && o.ID == Part2.ID select o as TNTPart).SingleOrDefault();

			if (part != null)
			{
				Part2 = part;
				part.AddPipe(this);
			}
		}

		public override void DrawDistances(Graphics graphics)
		{
			base.DrawDistance(graphics, Part1.ControlPoints.First(), Part2.ControlPoints.First());
		}

		/// <summary>
		/// Called to deterimine if this pipe type can be added to this part
		/// </summary>
		/// <param name="pipeType">Pipe type</param>
		/// <param name="reason">When false, reason pipe can not be added</param>
		/// <returns>True if pipe type can be added, false otherwise</returns>
		public override bool CanAddPipe(Type pipeType, out string reason)
		{
			base.CanAddPipe(pipeType, out reason);

			if (pipeType != this.GetType())
			{
				reason = "Cannot connect pipes together that are different types";
				return false;
			}

			return true;
		}

		/// <summary>
		/// Add pipe quantities
		/// </summary>
		/// <param name="parts">List of parts who's quantities should be updated</param>
		/// <param name="systemType">Indicates the type of system</param>
		public override void SetPartQuantity(Dictionary<string, Part> parts, SystemType systemType)
		{
			List<string> codes;
			switch (systemType)
			{
				case SystemType.PVC:
					codes = new List<string>(new string[] { "", "PI075", "PI100", "PI125", "PI150", "PI200" });
					break;
				default:
					codes = new List<string>(new string[] { "", "POLY075", "POLY100", "POLY125", "POLY150", "POLY200" });
					break;
			}

			int index = m_PipeSizeList.IndexOf(PipeSize);

			Part part = parts[codes[index]];

			if (part != null)
			{
				part.Quantity += (int)Part1.ControlPoints[0].DistanceFrom(Part2.ControlPoints[0]);
			}
		}

		#endregion

		/// <summary>
		/// Gets the other part
		/// </summary>
		/// <param name="part">Part</param>
		/// <returns>The part opposite part</returns>
		virtual public TNTPart GetOtherPart(TNTPart part)
		{
			if (Part1.Equals(part))
			{
				return Part2;
			}
			else
			{
				return Part1;
			}
		}

		/// <summary>
		/// Sizes the pipe for the specified GPM if AutoSize is true
		/// </summary>
		/// <param name="gpm">GPM</param>
		virtual public void SizeFor(double gpm)
		{
			if (!AutoSize)
			{
				return;
			}

			if (gpm > 30)
			{
				PipeSize = m_PipeSizeList[5];
			}
			else if (gpm > 22)
			{
				PipeSize = m_PipeSizeList[4];
			}
			else if (gpm > 12)
			{
				PipeSize = m_PipeSizeList[3];
			}
			else if (gpm > 8)
			{
				PipeSize = m_PipeSizeList[2];
			}
			else
			{
				PipeSize = m_PipeSizeList[1];
			}
		}

		/// <summary>
		/// Associates the pipe to the parts and parts to the pipe
		/// </summary>
		/// <param name="part1">First part</param>
		/// <param name="part2">Second part</param>
		virtual public void CreatePartsPipeRelationship(TNTPart part1, TNTPart part2)
		{
			Part1 = part1;
			Part2 = part2;
			part1.AddPipe(this);
			part2.AddPipe(this);
		}

		/// <summary>
		/// Removes the association between the pipe and the parts
		/// </summary>
		virtual public void RemovePartsPipeRelationship()
		{
			Part1.RemovePipe(this);
			Part2.RemovePipe(this);
			Part1 = null;
			Part2 = null;
		}
	}
}
