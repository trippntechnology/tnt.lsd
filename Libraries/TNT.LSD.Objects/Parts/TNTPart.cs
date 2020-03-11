using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Linq;
using System.Windows.Forms;
using System.Xml.Serialization;
using TNT.LSD.Inventory;
using TNT.LSD.Objects.ControlPoints;
using TNT.LSD.Objects.Extensions;
using TNT.LSD.Settings;
using TNT.Math;

namespace TNT.LSD.Objects
{
	/// <summary>
	/// Base class for a TNT part
	/// </summary>
	public class TNTPart : BasePart
	{
		#region Properties

		/// <summary>
		/// <see cref="List{T}"/> of <see cref="Pipe"/>
		/// </summary>
		[Browsable(false)]
		[XmlIgnore()]
		virtual public List<Pipe> Pipes { get; set; } = new List<Pipe>();

		/// <summary>
		/// <see cref="Color "/> associated with this part
		/// </summary>
		[XmlIgnore()]
		[Browsable(false)]
		virtual public Color Color { get; set; }

		/// <summary>
		/// Serializes <see cref="Color"/> from/to ARGB
		/// </summary>
		[Browsable(false)]
		public int _Color
		{
			get { return Color.ToArgb(); }
			set { Color = Color.FromArgb(value); }
		}

		/// <summary>
		/// Indicates whether this part has been sized
		/// </summary>
		[Browsable(false)]
		[XmlIgnore()]
		public bool Sized { get; set; }

		/// <summary>
		/// Value indicating the required flow for this part
		/// </summary>
		[DisplayName("Required Flow")]
		[Description("Flow required by this part and parts down stream")]
		[ReadOnly(true)]
		[XmlIgnore()]
		virtual public double RequiredFlow { get; set; }

		#endregion

		#region Constructors

		/// <summary>
		/// Creates a part at a specified <paramref name="position"/>
		/// </summary>
		public TNTPart(Point position)
			: base()
		{
			Color = Color.Black;
			CreateControlPoints(position);
		}

		/// <summary>
		/// Copy constructor
		/// </summary>
		public TNTPart(TNTPart obj)
			: base(obj)
		{
			Color = obj.Color;
		}

		/// <summary>
		/// Default constructor
		/// </summary>
		public TNTPart()
			: base()
		{
			Color = Color.Black;
		}

		#endregion

		#region Overrides

		/// <summary>
		/// Not implemented
		/// </summary>
		public override TNTObject MouseOver(Point mousePosition, Keys modifierKeys) => throw new NotImplementedException();

		/// <summary>
		/// Added to prevent call from subclass to base class
		/// </summary>
		public override void DrawDistance(Graphics graphics, TNTControlPoint p1, TNTControlPoint p2)
		{
			// Added so that distance between points isn't drawn.
		}

		/// <summary>
		/// Clones this part
		/// </summary>
		public override TNTObject Clone() => new TNTPart(this);

		/// <summary>
		/// Create a <see cref="TNTControlPoint"/>
		/// </summary>
		virtual protected void CreateControlPoints(Point position) => ControlPoints.Add(new TNTControlPoint(this, position));

		/// <summary>
		/// Indicates if this <see cref="TNTPart"/> is within <paramref name="rectangle"/>
		/// </summary>
		public override bool InRectangle(Rectangle rectangle)
		{
			bool inRect = rectangle.Contains(ControlPoints[0].Position);
			return inRect;
		}

		/// <summary>
		/// Moves this <see cref="TNTPart"/> from it's current position to the position indicated by <paramref name="x"/> and <paramref name="y"/>
		/// </summary>
		public override void MoveTo(int x, int y)
		{
			base.MoveTo(x, y);

			if (ControlPoints.Count == 0)
			{
				// This is needed for parts that are moved from the palette to the canvas. When the mouse 
				// begins to move, there isn't control points.
				CreateControlPoints(new Point(x, y));
			}
			else
			{
				ControlPoints.First().MoveTo(x, y);
			}
		}

		/// <summary>
		/// Moves this <see cref="TNTPart"/> from it's current position to the position indicated by <paramref name="x"/> and <paramref name="y"/>
		/// and aligns the points if <paramref name="alignPoints"/> is true
		/// </summary>
		public override void MoveTo(int x, int y, bool alignPoints)
		{
			base.MoveTo(x, y, alignPoints);
			if (ControlPoints != null && ControlPoints.Count > 0)
			{
				ControlPoints.First().MoveTo(x, y, alignPoints);
			}
		}

		/// <summary>
		/// Aligns the <see cref="ControlPoints"/> to the grid
		/// </summary>
		public override void AlignToGrid()
		{
			Point p = ControlPoints.First().Position.SnapToGrid(true);
			ControlPoints.First().MoveTo(p.X, p.Y, true);
		}

		/// <summary>
		/// Determines the parts needed at the pipe level when two pipes are connected
		/// </summary>
		public override void SetPartQuantity(CodedParts parts, SystemType systemType)
		{
			if (Pipes == null || Pipes.Count < 2) return; // This case will be handed by subclasses implementation

			var fittingSize = GetMaxPipeSize();
			var pairs = GetPipePairs();

			if (systemType == SystemType.PVC)
			{
				var nintyCode = $"FI{fittingSize}SS90";
				var fortyFiveCode = $"FI{fittingSize}SS45";

				if (Pipes.Count > 2)
				{
					parts.Add($"FI{fittingSize}SSSTEE", Pipes.Count - 2);

					// Find inline and perpendicular pipes pairs
					var inlinePair = pairs.Find(p2 => p2.Angle == pairs.Max(p1 => p1.Angle));
					var perpPairs = pairs.FindAll(p => p.Item1 == inlinePair.Item1 && p.Item2 != inlinePair.Item1);

					perpPairs.ForEach(p =>
					{
						if (p.Angle < 68 || (p.Angle > 112 && p.Angle < 158))
						{
							parts.Add(fortyFiveCode, 1);
						}
					});
				}
				else if (Pipes.Count == 2)
				{
					var angle = pairs[0].Angle;

					if (angle < 68)
					{
						parts.Add(nintyCode, 1);
						parts.Add(fortyFiveCode, 1);
					}
					else if (angle < 112)
					{
						parts.Add(nintyCode, 1);
					}
					else if (angle < 158)
					{
						parts.Add(fortyFiveCode, 1);
					}
				}

				// Add bushings
				foreach (Pipe pipe in Pipes)
				{
					var pipeSize = PartSize.GetSize(pipe.PipeSizeIndex);

					if (pipeSize < fittingSize)
					{
						parts.Add($"FI{fittingSize}X{pipeSize}SSRB", 1);
					}
				}
			}
			else
			{
				var nintyCode = $"PF{fittingSize}BB90";

				if (Pipes.Count > 2)
				{
					int teeCount = Pipes.Count - 2;
					parts.Add($"PF{fittingSize}TEE", teeCount);
					parts.AddHoseClamp(fittingSize.Code, teeCount * 3);
				}
				else if (Pipes.Count == 2)
				{
					var angle = pairs[0].Angle;

					if (angle < 112)
					{
						parts.Add(nintyCode, 1);
						parts.AddHoseClamp(fittingSize.Code, 2);
					}
				}

				// Add bushings
				foreach (Pipe pipe in Pipes)
				{
					var pipeSize = PartSize.GetSize(pipe.PipeSizeIndex);

					if (pipeSize < fittingSize)
					{
						parts.Add($"PF{fittingSize}X{pipeSize}BBRB", 1);
						parts.AddHoseClamp(fittingSize.Code, 1);
						parts.AddHoseClamp(pipeSize.Code, 1);
					}
				}
			}
		}

		private List<PipePair> GetPipePairs()
		{
			if (Pipes.Count == 2)
			{
				return new List<PipePair>() { new PipePair(Pipes[0], Pipes[1], this.Position, this) };
			}
			else if (Pipes.Count > 2)
			{
				// Get all combinations
				return (from a in Pipes from b in Pipes select new PipePair(a, b, this.Position, this)).ToList();
			}
			else
			{
				return new List<PipePair>();
			}
		}

		/// <summary>
		/// Checks to see if the <paramref name="pipeType"/> is the same as an existing part connected. 
		/// </summary>
		/// <returns>True if pipe are same, false otherwise </returns>
		public override bool CanAddPipe(Type pipeType, out string reason)
		{
			base.CanAddPipe(pipeType, out reason);

			Pipe pipe = Pipes.Find(p => p.GetType() != pipeType);

			if (pipe != null)
			{
				reason = "Part must be connected to same pipe types";
				return false;
			}
			else if (Pipes.Count > 3)
			{
				reason = "Only four connections are allowed";
				return false;
			}

			return true;
		}

		/// <summary>
		/// Creates undo object
		/// </summary>
		/// <returns>Undo object</returns>
		public override TNTObject CreateUndoCopy()
		{
			TNTPart newObj = base.CreateUndoCopy() as TNTPart;

			newObj.Color = Color;
			newObj.Pipes = new List<Pipe>(Pipes);

			return newObj;
		}

		/// <summary>
		/// Assigns obj's properties to this object
		/// </summary>
		/// <param name="obj">Source object</param>
		public override void Assign(TNT.LSD.Objects.TNTObject obj)
		{
			base.Assign(obj);

			TNTPart part = obj as TNTPart;

			if (part != null)
			{
				Color = part.Color;
				Pipes = part.Pipes;
			}
		}

		#endregion

		/// <summary>
		/// Adds a <see cref="Pipe"/> if it hasn't already been added
		/// </summary>
		/// <param name="pipe"></param>
		virtual public void AddPipe(Pipe pipe)
		{
			// Only add if it doesn't already exist
			if (!Pipes.Contains(pipe))
			{
				Pipes.Add(pipe);
			}
		}

		/// <summary>
		/// Removes a <see cref="Pipe"/>
		/// </summary>
		/// <param name="pipe"></param>
		virtual public void RemovePipe(Pipe pipe)
		{
			Pipes.Remove(pipe);
		}

		/// <summary>
		/// Sizes the pipe from this to the pipe's connection if not already sized
		/// </summary>
		/// <returns>GPM required from this part onward</returns>
		public virtual double SizePipe(Pipe upstreamPipe)
		{
			if (!Sized)
			{
				Sized = true;

				Pipes.ForEach(p =>
				{
					if (upstreamPipe != null && upstreamPipe == p)
					{
						// Ignore this pipe
						return;
					}

					// Get the downstream part for this pipe segment
					TNTPart other = p.GetOtherPart(this);

					if (p is LateralPipe)
					{
						RequiredFlow += other.SizePipe(p);
					}
					else
					{
						RequiredFlow = System.Math.Max(other.SizePipe(p), RequiredFlow);
					}
				});
			}

			// Size the upstream pipe to handle the required flow
			if (upstreamPipe != null)
			{
				upstreamPipe.SizeFor(RequiredFlow);
			}

			return RequiredFlow;
		}

		/// <summary>
		/// Get the <see cref="PartSize"/> that represents the largest pipe size connecting this part
		/// </summary>
		/// <param name="pipeType"><see cref="System.Type"/> representing the type of pipe that is of interest</param>
		virtual protected PartSize GetMaxPipeSize(System.Type pipeType = null)
		{
			int index = PartSize.SIZE_050.Index - 1;
			Pipes.FindAll(p => pipeType == null || p.GetType() == pipeType).ForEach(p => index = System.Math.Max(index, p.PipeSizeIndex));
			return PartSize.GetSize(index);
		}

		/// <summary>
		/// Get the <see cref="PartSize"/> that represents the largest pipe size connecting this part
		/// </summary>
		/// <param name="pipeType"><see cref="System.Type"/> representing the type of pipe that is of interest</param>
		virtual protected PartSize GetMinPipeSize(System.Type pipeType = null)
		{
			int index = PartSize.SIZE_200.Index + 1;
			Pipes.FindAll(p => pipeType == null || p.GetType() == pipeType).ForEach(p => index = System.Math.Min(index, p.PipeSizeIndex));
			return PartSize.GetSize(index);
		}

		/// <summary>
		/// Used to determine the angles between two <see cref="Pipe"/>
		/// </summary>
		internal class PipePair : Tuple<Pipe, Pipe>
		{
			private readonly Point origin;
			private readonly TNTPart part;

			/// <summary>
			/// Returns the angle between the two <see cref="Pipe"/>
			/// </summary>
			public double Angle
			{
				get
				{
					var pipeDirection1 = new Vector(this.origin, Item1.GetOtherPart(this.part).Position);
					var pipeDirection2 = new Vector(this.origin, Item2.GetOtherPart(this.part).Position);
					return pipeDirection1.Angle(pipeDirection2).InDegrees;
				}
			}

			/// <summary>
			/// Constructor
			/// </summary>
			public PipePair(Pipe item1, Pipe item2, Point Position, TNTPart part) : base(item1, item2)
			{
				origin = Position;
				this.part = part;
			}
		}
	}
}
