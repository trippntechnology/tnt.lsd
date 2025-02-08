namespace TNT.LSD.PDFGenerator.Tests
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
      Browser = new WebBrowser();
      SuspendLayout();
      // 
      // Browser
      // 
      Browser.Dock = DockStyle.Fill;
      Browser.Location = new Point(0, 0);
      Browser.Margin = new Padding(4, 3, 4, 3);
      Browser.MinimumSize = new Size(23, 23);
      Browser.Name = "Browser";
      Browser.Size = new Size(754, 783);
      Browser.TabIndex = 0;
      // 
      // Form1
      // 
      AutoScaleDimensions = new SizeF(7F, 15F);
      AutoScaleMode = AutoScaleMode.Font;
      ClientSize = new Size(754, 783);
      Controls.Add(Browser);
      Margin = new Padding(4, 3, 4, 3);
      Name = "Form1";
      Text = "Form1";
      TopMost = true;
      ResumeLayout(false);
    }

    #endregion

    private System.Windows.Forms.WebBrowser Browser;
	}
}

