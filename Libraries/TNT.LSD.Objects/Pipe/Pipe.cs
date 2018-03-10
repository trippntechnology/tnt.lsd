using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Linq;
using System.Xml.Serialization;
using TNT.LSD.Inventory;
using TNT.LSD.Objects.Extensions;
using TNT.LSD.Settings.TypeConverters;
using TNT.Math;

namespace TNT.LSD.Objects
{
	/// <summary>
	/// Represents a pipe segment
	/// </summary>
	public abstract class Pipe : BasePart
	{
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
				int pipeSizeIndex = m_PipeSizeList.IndexOf(PipeSize);
				p.DashStyle = LineStyle;
				p.Width = pipeSizeIndex;
				graphics.DrawLine(p, Part1.Position, Part2.Position);

				if (pipeSizeIndex > 1)
				{
					// Pipe is larger than 3/4" so draw ticks
					Vector pipeVector = new Vector(Part1.Position, Part2.Position);
					PointF midPoint = Part1.Position + (pipeVector / 2.0);
					Vector tickVector = pipeVector.Unit.Rotate(new Angle(90, true));
					p.Width = 1;

					if (pipeSizeIndex == 3)
					{
						midPoint = midPoint - (pipeVector.Unit);
					}
					else if (pipeSizeIndex == 4)
					{
						midPoint = midPoint - (pipeVector.Unit * 2);
					}
					else if (pipeSizeIndex == 5)
					{
						midPoint = midPoint - (pipeVector.Unit * 3);
					}

					for (int index = 1; index < pipeSizeIndex; index++)
					{
						graphics.DrawLine(p, midPoint - (tickVector * 5), midPoint + (tickVector * 5));

						midPoint = midPoint + pipeVector.Unit * 2;
					}
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
		public override void SetPartQuantity(Dictionary<string, Part> parts)
		{
			List<string> codes = new List<string>(new string[] { "", "PI075", "PI100", "PI125", "PI150", "PI200" });
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
