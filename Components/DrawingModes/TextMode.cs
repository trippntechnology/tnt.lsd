using System.Drawing;
using TNT.LSD.Objects;
using TNT.LSD.Objects.Extensions;

namespace LSDComponents.DrawingModes
{
	public class TextMode : DrawingMode
	{
		public TextMode()
			: base()
		{
			DefaultObject = new TNTText();
			DefaultObjectType = DefaultObject.GetType().ToString();
		}

		public TextMode(TextMode obj)
			: base(obj)
		{
		}

		#region Overridden methods

		public override DrawingMode Clone()
		{
			return new TextMode(this);	
		}

		public override void OnMouseMove(TNTCAD cad, System.Windows.Forms.MouseEventArgs e, System.Windows.Forms.Keys modifierKeys)
		{
			cad.Cursor = TNTCursors.TextTool;
			cad.Refresh();

			Graphics g = cad.CreateGraphics();
			Point adjPos = new Point(e.X, e.Y).ToWorldCoordinateSpace(g, cad.SnapToGrid);

			Brush b = new SolidBrush(Color.FromArgb(50, Color.Gray));
			g.FillRectangle(b, new Rectangle(adjPos, new Size(100, 100)));

			DrawPointerPoint(g, adjPos, cad.DrawingOptions);
		}

		public override void OnMouseClick(TNTCAD cad, System.Windows.Forms.MouseEventArgs e, System.Windows.Forms.Keys modifierKeys)
		{
			if (e.Button == System.Windows.Forms.MouseButtons.Left)
			{
				TNTText defaults = DefaultObject as TNTText;
				Point adjPos = new Point(e.X, e.Y).ToWorldCoordinateSpace(cad.CreateGraphics(), cad.SnapToGrid);

				cad.UnselectAll();
				TNTText text = new TNTText(adjPos);

				if (DefaultObject != null)
				{
					text.LineColor = defaults.LineColor;
					text.LineStyle = defaults.LineStyle;
					text.LineWidth = defaults.LineWidth;
					text.FillColor = defaults.FillColor;
					text.FillOpacity = defaults.FillOpacity;
					text.Font = defaults.Font;
					text.TextColor = defaults.TextColor;
					text.HorizontalAlignment = defaults.HorizontalAlignment;
					text.VerticalAlignment = defaults.VerticalAlignment;
					text.Text = defaults.Text;
				}

				text.Selected = true;
				cad.AddObject(text);
				cad.TriggerOnObjectsSelected();
			}
		}

		#endregion
	}
}
