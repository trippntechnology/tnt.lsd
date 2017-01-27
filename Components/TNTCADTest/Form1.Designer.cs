namespace TNTCADTest
{
	partial class Form1
	{
		/// <summary>
		/// Required designer variable.
		/// </summary>
		private System.ComponentModel.IContainer components = null;

		/// <summary>
		/// Clean up any resources being used.
		/// </summary>
		/// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
		protected override void Dispose(bool disposing)
		{
			if (disposing && (components != null))
			{
				components.Dispose();
			}
			base.Dispose(disposing);
		}

		#region Windows Form Designer generated code

		/// <summary>
		/// Required method for Designer support - do not modify
		/// the contents of this method with the code editor.
		/// </summary>
		private void InitializeComponent()
		{
			LSDComponents.DrawingModes.SelectMode selectMode1 = new LSDComponents.DrawingModes.SelectMode();
			this.panel1 = new System.Windows.Forms.Panel();
			this.tntcad1 = new LSDComponents.TNTCAD();
			this.cbDrawingMode = new System.Windows.Forms.ComboBox();
			this.panel1.SuspendLayout();
			this.SuspendLayout();
			// 
			// panel1
			// 
			this.panel1.AutoScroll = true;
			this.panel1.Controls.Add(this.tntcad1);
			this.panel1.Dock = System.Windows.Forms.DockStyle.Left;
			this.panel1.Location = new System.Drawing.Point(0, 0);
			this.panel1.Name = "panel1";
			this.panel1.Size = new System.Drawing.Size(647, 647);
			this.panel1.TabIndex = 0;
			// 
			// tntcad1
			// 
			this.tntcad1.DrawingLayers = 0;
			this.tntcad1.DrawingMode = selectMode1;
			this.tntcad1.HeightInFeet = 100;
			this.tntcad1.ImageObject = null;
			this.tntcad1.Location = new System.Drawing.Point(0, 0);
			this.tntcad1.Name = "tntcad1";
			this.tntcad1.Size = new System.Drawing.Size(500, 500);
			this.tntcad1.SnapToGrid = true;
			this.tntcad1.TabIndex = 0;
			this.tntcad1.Text = "tntcad1";
			this.tntcad1.WidthInFeet = 100;
			// 
			// cbDrawingMode
			// 
			this.cbDrawingMode.FormattingEnabled = true;
			this.cbDrawingMode.Location = new System.Drawing.Point(653, 12);
			this.cbDrawingMode.Name = "cbDrawingMode";
			this.cbDrawingMode.Size = new System.Drawing.Size(256, 21);
			this.cbDrawingMode.TabIndex = 1;
			this.cbDrawingMode.SelectedIndexChanged += new System.EventHandler(this.cbDrawingMode_SelectedIndexChanged);
			// 
			// Form1
			// 
			this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
			this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
			this.ClientSize = new System.Drawing.Size(921, 647);
			this.Controls.Add(this.cbDrawingMode);
			this.Controls.Add(this.panel1);
			this.Name = "Form1";
			this.Text = "Form1";
			this.Load += new System.EventHandler(this.Form1_Load);
			this.panel1.ResumeLayout(false);
			this.ResumeLayout(false);

		}

		#endregion

		private System.Windows.Forms.Panel panel1;
		private LSDComponents.TNTCAD tntcad1;
		private System.Windows.Forms.ComboBox cbDrawingMode;

	}
}

