namespace LandscapeSprinklerDesigner
{
	partial class Splash
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
			this.components = new System.ComponentModel.Container();
			this.timer = new System.Windows.Forms.Timer(this.components);
			this.SuspendLayout();
			// 
			// timer
			// 
			this.timer.Interval = 2000;
			this.timer.Tick += new System.EventHandler(this.timer_Tick);
			// 
			// Splash
			// 
			this.AutoScaleDimensions = new System.Drawing.SizeF(17F, 32F);
			this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
			this.ClientSize = new System.Drawing.Size(155, 145);
			this.Font = new System.Drawing.Font("Arial", 20.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
			this.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(242)))), ((int)(((byte)(246)))), ((int)(((byte)(255)))));
			this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
			this.Margin = new System.Windows.Forms.Padding(9, 7, 9, 7);
			this.Name = "Splash";
			this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
			this.Text = "Splash";
			this.Load += new System.EventHandler(this.Splash_Load);
			this.Paint += new System.Windows.Forms.PaintEventHandler(this.Splash_Paint);
			this.ResumeLayout(false);

		}

		#endregion

		private System.Windows.Forms.Timer timer;
	}
}