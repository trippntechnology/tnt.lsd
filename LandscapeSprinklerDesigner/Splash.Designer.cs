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
      components = new System.ComponentModel.Container();
      timer = new System.Windows.Forms.Timer(components);
      versionPlaceholder = new Panel();
      SuspendLayout();
      // 
      // timer
      // 
      timer.Interval = 1000;
      timer.Tick += timer_Tick;
      // 
      // versionPlaceholder
      // 
      versionPlaceholder.BackColor = Color.Transparent;
      versionPlaceholder.Location = new Point(414, 161);
      versionPlaceholder.Name = "versionPlaceholder";
      versionPlaceholder.Size = new Size(200, 100);
      versionPlaceholder.TabIndex = 0;
      // 
      // Splash
      // 
      AutoScaleDimensions = new SizeF(17F, 32F);
      AutoScaleMode = AutoScaleMode.Font;
      BackgroundImage = Resource.splash;
      BackgroundImageLayout = ImageLayout.None;
      ClientSize = new Size(709, 558);
      Controls.Add(versionPlaceholder);
      Font = new Font("Arial", 20.25F, FontStyle.Bold, GraphicsUnit.Point, 0);
      ForeColor = Color.FromArgb(242, 246, 255);
      FormBorderStyle = FormBorderStyle.None;
      Margin = new Padding(9, 7, 9, 7);
      Name = "Splash";
      StartPosition = FormStartPosition.CenterScreen;
      Text = "Splash";
      Load += Splash_Load;
      Paint += Splash_Paint;
      MouseDown += Splash_MouseDown;
      ResumeLayout(false);
    }

    #endregion

    private System.Windows.Forms.Timer timer;
		private System.Windows.Forms.Panel versionPlaceholder;
	}
}