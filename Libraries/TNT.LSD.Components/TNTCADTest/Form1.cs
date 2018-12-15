using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using LSDComponents.DrawingModes;
using TNTObjects;


namespace TNTCADTest
{
	public partial class Form1 : Form
	{
		public Form1()
		{
			InitializeComponent();
			tntcad1.DrawingLayers = 1;
		}

		private void Form1_Load(object sender, EventArgs e)
		{
			cbDrawingMode.Items.Add(new CBItem(new SelectMode(), "Select"));
			cbDrawingMode.Items.Add(new CBItem(new LineMode(), "Line"));
			cbDrawingMode.Items.Add(new CBItem(new ImageMode(), "Image"));
			cbDrawingMode.Items.Add(new CBItem(new RectangleMode(), "Rectangle"));
			cbDrawingMode.Items.Add(new CBItem(new TextMode(), "Text"));

			tntcad1.ImageObject = new TNTImage(new Point(0, 0), "test.png");

			tntcad1.AddObject(new TNTRectangle(new Rectangle(new Point(100, 100), new Size(100, 100)), Color.Black));
			tntcad1.AddObject(new TNTCircle(new Rectangle(new Point(100,100), new Size(100,100)), Color.Red));
		}

		private void cbDrawingMode_SelectedIndexChanged(object sender, EventArgs e)
		{
			ComboBox cb = sender as ComboBox;
			CBItem item = cb.SelectedItem as CBItem;
			tntcad1.DrawingMode = item.DrawingMode;
		}
	}

	public class CBItem
	{
		public DrawingMode DrawingMode { get; set; }
		public string Text { get; set; }

		public CBItem(DrawingMode drawingMode, string text)
		{
			DrawingMode = drawingMode;
			Text = text;
		}

		public override string ToString()
		{
			return Text;
		}
	}
}
