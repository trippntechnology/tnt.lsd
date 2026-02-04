namespace TNT.LSD.SSC;

partial class PDFForm
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
        System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(PDFForm));
        Browser = new Microsoft.Web.WebView2.WinForms.WebView2();
        ((System.ComponentModel.ISupportInitialize)Browser).BeginInit();
        SuspendLayout();
        // 
        // Browser
        // 
        Browser.AllowExternalDrop = true;
        Browser.CreationProperties = null;
        Browser.DefaultBackgroundColor = System.Drawing.Color.White;
        Browser.Dock = System.Windows.Forms.DockStyle.Fill;
        Browser.Location = new System.Drawing.Point(0, 0);
        Browser.Margin = new System.Windows.Forms.Padding(4, 3, 4, 3);
        Browser.Name = "Browser";
        Browser.Size = new System.Drawing.Size(911, 787);
        Browser.TabIndex = 0;
        Browser.ZoomFactor = 1D;
        // 
        // PDFForm
        // 
        AutoScaleDimensions = new System.Drawing.SizeF(7F, 15F);
        AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
        ClientSize = new System.Drawing.Size(911, 787);
        Controls.Add(Browser);
        DockAreas = WeifenLuo.WinFormsUI.Docking.DockAreas.Document;
        Icon = (System.Drawing.Icon)resources.GetObject("$this.Icon");
        Margin = new System.Windows.Forms.Padding(4, 3, 4, 3);
        Name = "PDFForm";
        Text = "PDFForm";
        ((System.ComponentModel.ISupportInitialize)Browser).EndInit();
        ResumeLayout(false);

    }

    #endregion

    private Microsoft.Web.WebView2.WinForms.WebView2 Browser;
}