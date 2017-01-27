using System;
using System.ComponentModel;
using System.Drawing;
using System.Text.RegularExpressions;
using TNT.Math;
using TNT.LSD.Objects.ControlPoints;

namespace TNT.LSD.Objects
{
	public class ArcSprinkler : Sprinkler
	{
		#region Delegates

		virtual protected void ArcPointMoved(TNTControlPoint Sender)
		{
			// Get vector to the rotation point
			Vector rotVector = new Vector(ControlPoints[0].Position, ControlPoints[1].Position);

			// Get vector to the arc point
			Vector arcVector = new Vector(ControlPoints[0].Position, ControlPoints[2].Position);

			// Get angle of arcVector
			Arc = (int)arcVector.Angle(rotVector, true).InDegrees;
		}

		#endregion

		#region Members

		protected int m_Arc = 0;

		#endregion

		#region Properties

		public override float RotationAngle
		{
			get
			{
				return base.RotationAngle;
			}
			set
			{
				base.RotationAngle = value;

				Arc = Arc;
			}
		}

		[Description("Specifies the area of a circle covered by the sprinkler")]
#if !PALETTE_PROPERTIES
		[ReadOnly(true)]
#endif
		virtual public int Arc
		{
			get { return m_Arc; }
			set
			{
				if (value < 0)
				{
					m_Arc = 360 + (value % 360);
				}
				else
				{
					m_Arc = value % 360;
				}

				if (ControlPoints != null && ControlPoints.Count > 2 && m_Image != null)
				{
					Vector rotVector = new Vector(ControlPoints[0].Position, ControlPoints[1].Position);
					Angle rotAngle = rotVector.Angle(new Vector(0, -1), true);

					Vector arcVector = new Vector(rotAngle + new Angle(Arc, true), 1);
					PointF arcPoint = ControlPoints[0].Position + (arcVector.Unit * m_Image.Width * 2);

					ControlPoints[2].MoveTo((int)arcPoint.X, (int)arcPoint.Y);
				}

				SetGPM();
			}
		}

		#endregion

		#region Constructors

		public ArcSprinkler()
			: base()
		{
		}

		public ArcSprinkler(ArcSprinkler obj)
			: base(obj)
		{
			Arc = obj.Arc;

			if (ControlPoints.Count > 2)
			{
				ControlPoints[2].OnMoved = ArcPointMoved;
			}
		}

		public override TNTObject Clone()
		{
			return new ArcSprinkler(this);
		}

		#endregion

		/// <summary>
		/// Creates undo copy
		/// </summary>
		/// <returns>Undo copy</returns>
		public override TNTObject CreateUndoCopy()
		{
			ArcSprinkler newObj = base.CreateUndoCopy() as ArcSprinkler;

			newObj.Arc = Arc;

			return newObj;
		}

		/// <summary>
		/// Set properties from obj on this object
		/// </summary>
		/// <param name="obj">Source object</param>
		public override void Assign(TNT.LSD.Objects.TNTObject obj)
		{
			base.Assign(obj);

			ArcSprinkler arcSprinkler = obj as ArcSprinkler;

			if (arcSprinkler != null)
			{
				Arc = arcSprinkler.Arc;
			}
		}

		public override void Draw(System.Drawing.Graphics graphics, DrawingOptions drawingOptions)
		{
			base.Draw(graphics, drawingOptions);

			#region Draw Coverage Area

			Regex circular = new Regex("(?<radius>[0-9]*)'");
			Match match = circular.Match(Radius);
			int radius = 0;

			if (match.Success)
			{
				radius = Convert.ToInt32(match.Groups["radius"].Value);
			}

			if (this is ArcSprinkler && drawingOptions.ShowCoverage && radius > 0)
			{
				graphics.TranslateTransform(ControlPoints[0].XPos, ControlPoints[0].YPos);
				Rectangle drawingRect = new Rectangle(-(radius * TNTConstants.PIXELS_PER_FOOT), -(radius * TNTConstants.PIXELS_PER_FOOT), radius * TNTConstants.PIXELS_PER_FOOT * 2, radius * TNTConstants.PIXELS_PER_FOOT * 2);

				graphics.RotateTransform(RotationAngle);
				graphics.FillPie(new SolidBrush(TNTConstants.COVERAGE_COLOR), drawingRect, -90, Arc == 0 ? 360 : Arc);
				graphics.RotateTransform(-RotationAngle);
				graphics.TranslateTransform(-ControlPoints[0].XPos, -ControlPoints[0].YPos);
			}

			#endregion
		}
	}
}
