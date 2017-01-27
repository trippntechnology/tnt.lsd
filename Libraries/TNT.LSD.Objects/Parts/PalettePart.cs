using System;
using System.ComponentModel;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Drawing.Imaging;
using System.IO;
using System.Windows.Forms;
using System.Xml.Serialization;
using TNT.LSD.Objects.ControlPoints;
using TNT.Math;

namespace TNT.LSD.Objects
{
	public abstract class PalettePart : TNTPart
	{
		protected const int PART_COLOR_CIRCLE_SIZE = 6;
		protected const double ROTATION_POINT_RATIO = 1.0;
		protected Image m_Image;
		private float m_RotationAngle = 0;

		#region OnMove Delgates

		virtual protected void RotationPointMoved(TNTControlPoint Sender)
		{
			Vector thisVector = new Vector(ControlPoints[0].Position, Sender.Position);
			Angle angle = thisVector.Angle(new Vector(0, -1));

			if (System.Math.Sign(thisVector.X) < 0)
			{
				angle = new Angle(360 - angle.InDegrees, true);
			}

			// Get angle between points
			RotationAngle = (float)angle.InDegrees;
		}

		virtual protected void CenterPointMoved(TNTControlPoint Sender)
		{
			Vector unitVector = new Vector(new Angle(RotationAngle, true), 1);
			ControlPoints[1].MoveTo((int)(Sender.XPos + unitVector.X * m_Image.Width * ROTATION_POINT_RATIO), (int)(Sender.YPos + unitVector.Y * m_Image.Height * ROTATION_POINT_RATIO));
		}

		#endregion

		#region Properties

		[Description("The Image's rotation angle.")]
		[DisplayName("Rotation Angle")]
		[DefaultValue(0)]
		virtual public float RotationAngle
		{
			get
			{
				return m_RotationAngle;
			}

			set
			{
				if (value < 0)
				{
					m_RotationAngle = 360 + (value % 360);
				}
				else
				{
					m_RotationAngle = value % 360;
				}

				if (ControlPoints != null && ControlPoints.Count > 1)
				{
					// Move the position of the rotation point.
					Vector unitVector = new Vector(new Angle(m_RotationAngle, true), 1);
					double rotationOffsetX = m_Image != null ? unitVector.X * m_Image.Width * ROTATION_POINT_RATIO : 0;
					double rotationOffsetY = m_Image != null ? unitVector.Y * m_Image.Height * ROTATION_POINT_RATIO : 0;
					ControlPoints[1].MoveTo((int)(ControlPoints[0].XPos + rotationOffsetX), (int)(ControlPoints[0].YPos + rotationOffsetY));
				}
			}
		}

		[XmlIgnore()]
#if !PALETTE_PROPERTIES
		[Browsable(false)]
#endif
		public Image Image { get { return m_Image; } set { m_Image = value; } }

		[Browsable(false)]
		public string ImageContent
		{
			get
			{
				return ImageToBase64(m_Image);
			}
			set
			{
				m_Image = Base64ToImage(value);

				// When image is being loaded the location of the rotation point needs to be adjusted. Resetting the rotation angle
				// will fix the location
				RotationAngle = RotationAngle;
			}
		}

		public override bool OverControlPoint
		{
			get
			{
				return ControlPoints[1] == m_SelectedControlPoint;
			}
		}

#if !PALETTE_PROPERTIES
		[Browsable(false)]
#endif
		public string LegendText { get; set; }

		[XmlIgnore()]
#if !PALETTE_PROPERTIES
		[Browsable(false)]
#endif
		public Image LegendImage { get; set; }

		[Browsable(false)]
		public string LegendImageContent
		{
			get
			{
				if (LegendImage != null)
				{
					return ImageToBase64(LegendImage);
				}
				else
				{
					return null;
				}
			}
			set
			{
				LegendImage = Base64ToImage(value);
			}
		}

#if !PALETTE_PROPERTIES
		[Browsable(false)]
#endif
		[DefaultValue(false)]
		public bool ExcludeFromLegend { get; set; }

		#endregion

		#region Constructors

		// Copy constructor
		public PalettePart(PalettePart obj)
			: base(obj)
		{
			m_Image = obj.m_Image;
			RotationAngle = obj.RotationAngle;
			LegendText = obj.LegendText;
			LegendImage = obj.LegendImage;
			ExcludeFromLegend = obj.ExcludeFromLegend;

			if (ControlPoints != null && ControlPoints.Count > 1)
			{
				ControlPoints[0].OnMoved = CenterPointMoved;
				ControlPoints[1].OnMoved = RotationPointMoved;
			}
		}

		public PalettePart()
			: base()
		{
		}

		#endregion

		public override void Draw(Graphics graphics, DrawingOptions drawingOptions)
		{
			Draw(graphics, drawingOptions, new ImageAttributes());
			base.Draw(graphics, drawingOptions);

			// Draw a colored circle on the part if connected to a pipe with a color other than black
			if (m_Pipes != null && m_Pipes.Count > 0 && m_Pipes[0].PipeColor != Color.Black)
			{
				using (SolidBrush sb = new SolidBrush(m_Pipes[0].PipeColor))
				{
					graphics.FillEllipse(sb, new Rectangle(base.Position.X - (PART_COLOR_CIRCLE_SIZE / 2), base.Position.Y - (PART_COLOR_CIRCLE_SIZE / 2), PART_COLOR_CIRCLE_SIZE, PART_COLOR_CIRCLE_SIZE));
				}
			}
		}

		virtual public void Draw(Graphics graphics, DrawingOptions options, ImageAttributes attrs)
		{
			// For some reason, a RotationAngle of exactly 90 causes an out of memory error. By adding 0.1 to the value
			// the issue is resolved.
			float rotationAngle = RotationAngle == (float)90.0 ? RotationAngle + (float)0.1 : RotationAngle;

			try
			{
				Rectangle drawingRect = new Rectangle(-m_Image.Width / 2, -m_Image.Height / 2, m_Image.Width, m_Image.Height);

				if (Selected)
				{
					// This creates a shadow of the image.
					int shadowOffset = 2;

					//create the grayscale ColorMatrix
					ColorMatrix colorMatrix = new ColorMatrix(
						new float[][]
						{
							 new float[] {.3f, .3f, .3f, 0, 0},
							 new float[] {.59f, .59f, .59f, 0, 0},
							 new float[] {.11f, .11f, .11f, 0, 0},
							 new float[] {0, 0, 0, .5f, 0},
							 new float[] {0, 0, 0, 0, 1}
						});

					ImageAttributes attributes = new ImageAttributes();
					attributes.SetColorMatrix(colorMatrix);//, ColorMatrixFlag.Default, ColorAdjustType.Bitmap);

					try
					{
						graphics.TranslateTransform(ControlPoints[0].XPos + shadowOffset, ControlPoints[0].YPos + shadowOffset);
						graphics.RotateTransform(rotationAngle);
						graphics.DrawImage(m_Image, drawingRect, 0, 0, m_Image.Width, m_Image.Height, GraphicsUnit.Pixel, attributes);
					}
					catch
					{
					}
					finally
					{
						graphics.RotateTransform(-rotationAngle);
						graphics.TranslateTransform(-ControlPoints[0].XPos - shadowOffset, -ControlPoints[0].YPos - shadowOffset);
					}
				}

				graphics.TranslateTransform(ControlPoints[0].XPos, ControlPoints[0].YPos);
				graphics.RotateTransform(rotationAngle);
				graphics.DrawImage(m_Image, drawingRect, 0, 0, m_Image.Width, m_Image.Height, GraphicsUnit.Pixel, attrs);
			}
			catch
			{
			}
			finally
			{
				graphics.RotateTransform(-rotationAngle);
				graphics.TranslateTransform(-ControlPoints[0].XPos, -ControlPoints[0].YPos);
			}
		}

		public override TNTObject MouseOver(Point mousePosition, Keys modifierKeys)
		{
			TNTObject isOver = null;

			GraphicsPath path = new GraphicsPath();
			Rectangle rect = new Rectangle(ControlPoints[0].XPos - m_Image.Width / 2, ControlPoints[0].YPos - m_Image.Height / 2, m_Image.Width, m_Image.Height);

			path.AddRectangle(rect);

			Region region = new Region(path);
			isOver = region.IsVisible(mousePosition) ? this : null;

			if (isOver == null && Selected)
			{
				isOver = GetControlPoint(mousePosition, modifierKeys);
			}

			return isOver;
		}

		protected override void CreateControlPoints(Point position)
		{
			PointF unitVector = new PointF(0, -1);
			double rotationOffsetX = unitVector.X * (m_Image != null ? m_Image.Width : 0);
			double rotationOffsetY = unitVector.Y * (m_Image != null ? m_Image.Height * ROTATION_POINT_RATIO : 0);

			ControlPoints.Add(new TNTControlPoint(this, position, true));
			ControlPoints.Add(new RotationControlPoint(this, (int)(position.X + rotationOffsetX), (int)(position.Y + rotationOffsetY)));

			ControlPoints[0].OnMoved = CenterPointMoved;
			ControlPoints[1].OnMoved = RotationPointMoved;
		}

		/// <summary>
		/// Creates undo object
		/// </summary>
		/// <returns>Undo object</returns>
		public override TNTObject CreateUndoCopy()
		{
			PalettePart newObj = base.CreateUndoCopy() as PalettePart;

			newObj.Image = Image;
			newObj.RotationAngle = RotationAngle;

			return newObj;
		}

		/// <summary>
		/// Assigns obj properties to this object
		/// </summary>
		/// <param name="obj">Source object</param>
		public override void Assign(TNTObject obj)
		{
			base.Assign(obj);

			PalettePart i = obj as PalettePart;

			if (i != null)
			{
				m_Image = i.m_Image;
				RotationAngle = i.RotationAngle;

				ControlPoints[0].OnMoved = CenterPointMoved;
				ControlPoints[1].OnMoved = RotationPointMoved;
			}
		}

		public override void ResolveReferences(System.Collections.Generic.List<TNTObject> objects)
		{
			base.ResolveReferences(objects);

			ControlPoints[0].OnMoved = CenterPointMoved;
			ControlPoints[1].OnMoved = RotationPointMoved;
		}

		#region Image Conversion Methods

		protected string ImageToBase64(Image image)
		{
			if (image == null)
			{
				return string.Empty;
			}

			using (MemoryStream ms = new MemoryStream())
			{
				image.Save(ms, ImageFormat.Png);
				return Convert.ToBase64String(ms.ToArray());
			}
		}

		protected Image Base64ToImage(string base64)
		{
			Image image = null;

			if (!string.IsNullOrEmpty(base64))
			{
				byte[] imageBytes = Convert.FromBase64String(base64);
				using (MemoryStream ms = new MemoryStream(imageBytes, 0, imageBytes.Length))
				{
					ms.Write(imageBytes, 0, imageBytes.Length);
					image = Image.FromStream(ms, true);
				}
			}

			return image;
		}

		#endregion
	}
}
