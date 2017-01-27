using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using TNT.Math;
using TNT.LSD.Objects.ControlPoints;

namespace TNT.LSD.Objects
{
	public class AdjustableSprinkler : ArcSprinkler
	{
		#region Properties

		public override bool OverControlPoint
		{
			get
			{
				return ControlPoints[1] == m_SelectedControlPoint || ControlPoints[2] == m_SelectedControlPoint;
			}
		}

		[Description("Mimimum allowed arc")]
		[DisplayName("Minimum Arc")]
		[DefaultValue(0)]
#if !PALETTE_PROPERTIES
		[ReadOnly(true)]
#endif
		public int MinimumArc { get; set; }

		[Description("Maximum allowed arc")]
		[DisplayName("Maximum Arc")]
		[DefaultValue(0)]
#if !PALETTE_PROPERTIES
		[ReadOnly(true)]
#endif
		public int MaximumArc { get; set; }

		[ReadOnly(false)]
		public override int Arc
		{
			get
			{
				return base.Arc;
			}
			set
			{
				if (MinimumArc == 0 && MaximumArc == 0)
				{
					base.Arc = value;
				}
				else if (value >= MinimumArc && value <= MaximumArc)
				{
					base.Arc = value;
				}
				else if (value < MinimumArc)
				{
					base.Arc = MinimumArc;
				}
				else if (value > MaximumArc)
				{
					base.Arc = MaximumArc;
				}
			}
		}

		#endregion

		#region Constructors

		public AdjustableSprinkler()
			: base()
		{
			MinimumArc = 0;
			MaximumArc = 0;
		}

		public AdjustableSprinkler(AdjustableSprinkler obj)
			: base(obj)
		{
			MinimumArc = obj.MinimumArc;
			MaximumArc = obj.MaximumArc;
		}

		#endregion

		#region Overrides

		protected override void CenterPointMoved(TNTControlPoint Sender)
		{
			base.CenterPointMoved(Sender);

			Vector unitVector = new Vector(new Angle(RotationAngle + Arc, true), 2);
			ControlPoints[2].MoveTo((int)(Sender.XPos + unitVector.X * m_Image.Width * ROTATION_POINT_RATIO), (int)(Sender.YPos + unitVector.Y * m_Image.Height * ROTATION_POINT_RATIO));
		}

		public override TNTObject Clone()
		{
			return new AdjustableSprinkler(this);
		}

		protected override void CreateControlPoints(System.Drawing.Point position)
		{
			base.CreateControlPoints(position);

			// Added adjustable point
			PointF unitVector = new PointF(0, -1);
			Vector adjVector = new Vector(new Angle(Arc, true), m_Image != null ? m_Image.Height * ROTATION_POINT_RATIO : 0);
			PointF adjPoint = position + adjVector;

			TNTControlPoint cp = new AdjustableControlPoint(this, (int)adjPoint.X, (int)adjPoint.Y);
			cp.OnMoved = ArcPointMoved;
			ControlPoints.Add(cp);
		}

		/// <summary>
		/// Create a copy for undo action
		/// </summary>
		/// <returns>Copy for undo action</returns>
		public override TNTObject CreateUndoCopy()
		{
			AdjustableSprinkler newObj = base.CreateUndoCopy() as AdjustableSprinkler;

			newObj.MinimumArc = MinimumArc;
			newObj.MaximumArc = MaximumArc;

			return newObj;
		}

		/// <summary>
		/// Assignes properties from obj to this object
		/// </summary>
		/// <param name="obj">Source object</param>
		public override void Assign(TNT.LSD.Objects.TNTObject obj)
		{
			base.Assign(obj);

			if (obj is AdjustableSprinkler)
			{
				AdjustableSprinkler adjSprinkler = obj as AdjustableSprinkler;
				MinimumArc = adjSprinkler.MinimumArc;
				MaximumArc = adjSprinkler.MaximumArc;
			}

			ControlPoints[2].OnMoved = ArcPointMoved;
		}

		public override void ResolveReferences(List<TNTObject> objects)
		{
			base.ResolveReferences(objects);
			ControlPoints[2].OnMoved = ArcPointMoved;
		}
		#endregion
	}
}
