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
	public class TNTPart : BasePart
	{
		#region Static

		protected static string[] SIZE_CODE = { "050", "075", "100", "125", "150", "200" };

		#endregion

		#region Members

		protected List<Pipe> m_Pipes = new List<Pipe>();

		#endregion

		#region Properties

		[Browsable(false)]
		[XmlIgnore()]
		virtual public List<Pipe> Pipes { get { return m_Pipes; } set { m_Pipes = value; } }

		[XmlIgnore()]
		[Browsable(false)]
		virtual public Color Color { get; set; }

		[Browsable(false)]
		public int _Color
		{
			get { return Color.ToArgb(); }
			set { Color = Color.FromArgb(value); }
		}

		[Browsable(false)]
		[XmlIgnore()]
		public bool Sized { get; set; }

		[DisplayName("Required Flow")]
		[Description("Flow required by this part and parts down stream")]
		[ReadOnly(true)]
		[XmlIgnore()]
		virtual public double RequiredFlow { get; set; }

		#endregion

		#region Constructors

		public TNTPart(Point position)
			: base()
		{
			Color = Color.Black;
			CreateControlPoints(position);
		}

		// Copy constructor
		public TNTPart(TNTPart obj)
			: base(obj)
		{
			Color = obj.Color;
		}

		public TNTPart()
			: base()
		{
			Color = Color.Black;
		}

		#endregion

		#region Overrides

		public override TNTObject MouseOver(Point mousePosition, Keys modifierKeys)
		{
			throw new NotImplementedException();
		}

		public override void DrawDistance(Graphics graphics, TNTControlPoint p1, TNTControlPoint p2)
		{
			// Added so that distance between points isn't drawn.
		}

		public override TNTObject Clone()
		{
			return new TNTPart(this);
		}

		virtual protected void CreateControlPoints(Point position)
		{
			ControlPoints.Add(new TNTControlPoint(this, position));
		}

		public override bool InRectangle(Rectangle rectangle)
		{
			bool inRect = rectangle.Contains(ControlPoints[0].Position);
			return inRect;
		}

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

		public override void MoveTo(int x, int y, bool alignPoints)
		{
			base.MoveTo(x, y, alignPoints);
			if (ControlPoints != null && ControlPoints.Count > 0)
			{
				ControlPoints.First().MoveTo(x, y, alignPoints);
			}
		}

		public override void AlignToGrid()
		{
			Point p = ControlPoints.First().Position.SnapToGrid(true);
			ControlPoints.First().MoveTo(p.X, p.Y, true);
		}

		public override void SetPartQuantity(CodedParts parts, SystemType systemType)
		{
			if (m_Pipes == null || m_Pipes.Count < 2)
			{
				// This case will be handed by subclasses implementation
				return;
			}

			int maxPipeSizeIndex = GetMaxPipeSizeIndex();
			string fittingSize = SIZE_CODE[maxPipeSizeIndex];

			string teeCode = string.Format("FI{0}SSSTEE", fittingSize);
			string nintyCode = string.Format("FI{0}SS90", fittingSize);
			string fortyFiveCode = string.Format("FI{0}SS45", fittingSize);

			// Add tees
			int teeCount = m_Pipes.Count - 2;
			parts[string.Format("FI{0}SSSTEE", fittingSize)].Quantity += teeCount;

			Vector v1 = new Vector(Position, m_Pipes[0].GetOtherPart(this).Position);

			#region Add elbows

			for (int index = 1; index < m_Pipes.Count; index++)
			{
				Vector v2 = new Vector(Position, m_Pipes[index].GetOtherPart(this).Position);
				double degree = v1.Angle(v2).InDegrees;

				if (degree < 22)
				{
					// Add 2 90s
					parts[nintyCode].Quantity += 2;
				}
				else if (degree < 68)
				{
					// Add 90 and 45
					parts[nintyCode].Quantity += 1;
					parts[fortyFiveCode].Quantity += 1;
				}
				else if (degree < 122 && teeCount == 0)
				{
					// Add 90
					parts[nintyCode].Quantity += 1;
				}
				else if (degree < 158 && teeCount == 0)
				{
					// Add 45
					parts[fortyFiveCode].Quantity += 1;
				}
			}

			#endregion

			// Remove elbow not needed since a tee is used
			//parts[nintyCode].Quantity -= (teeCount * 2);

			// Add bushings
			foreach (Pipe pipe in m_Pipes)
			{
				int pipeSizeIndex = m_PipeSizeList.IndexOf(pipe.PipeSize);
				string pipeSize = SIZE_CODE[pipeSizeIndex];

				if (pipeSizeIndex < maxPipeSizeIndex)
				{
					parts[string.Format("FI{0}X{1}SSRB", fittingSize, pipeSize)].Quantity += 1;
				}
			}

		}

		public override bool CanAddPipe(Type pipeType, out string reason)
		{
			base.CanAddPipe(pipeType, out reason);

			Pipe pipe = m_Pipes.Find(p => p.GetType() != pipeType);

			if (pipe != null)
			{
				reason = "Part must be connected to same pipe types";
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

		virtual public void AddPipe(Pipe pipe)
		{
			// Only add if it doesn't already exist
			if (!m_Pipes.Contains(pipe))
			{
				m_Pipes.Add(pipe);
			}
		}

		virtual public void RemovePipe(Pipe pipe)
		{
			m_Pipes.Remove(pipe);
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

				m_Pipes.ForEach(p =>
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
		/// Gets the pipe size index of the largest pipe
		/// </summary>
		/// <returns>Pipe size index of the largest pipe</returns>
		virtual protected int GetMaxPipeSizeIndex()
		{
			int sizeIndex = 0;

			foreach (Pipe pipe in m_Pipes)
			{
				sizeIndex = System.Math.Max(m_PipeSizeList.IndexOf(pipe.PipeSize), sizeIndex);
			}

			return sizeIndex;
		}

		/// <summary>
		/// Get the <see cref="PartSize"/> that represents the largest pipe size connecting this part
		/// </summary>
		/// <param name="pipeType"><see cref="System.Type"/> representing the type of pipe that is of interest</param>
		virtual protected PartSize GetMaxPipeSize(System.Type pipeType)
		{
			int index = PartSize.SIZE_050.Index - 1;
			m_Pipes.FindAll(p => p.GetType() == pipeType).ForEach(p => index = System.Math.Max(index, p.PipeSizeIndex));
			return PartSize.GetSize(index);
		}

		/// <summary>
		/// Get the <see cref="PartSize"/> that represents the largest pipe size connecting this part
		/// </summary>
		/// <param name="pipeType"><see cref="System.Type"/> representing the type of pipe that is of interest</param>
		virtual protected PartSize GetMinPipeSize(System.Type pipeType)
		{
			int index = PartSize.SIZE_200.Index + 1;
			m_Pipes.FindAll(p => p.GetType() == pipeType).ForEach(p => index = System.Math.Min(index, p.PipeSizeIndex));
			return PartSize.GetSize(index);
		}
	}
}
