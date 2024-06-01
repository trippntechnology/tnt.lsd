namespace TNT.LSD.Checker;

partial class OutputForm
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
    System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(OutputForm));
    this.console = new System.Windows.Forms.RichTextBox();
    this.SuspendLayout();
    // 
    // console
    // 
    this.console.BackColor = System.Drawing.SystemColors.WindowText;
    this.console.Dock = System.Windows.Forms.DockStyle.Fill;
    this.console.ForeColor = System.Drawing.SystemColors.Window;
    this.console.Location = new System.Drawing.Point(0, 0);
    this.console.Name = "console";
    this.console.ReadOnly = true;
    this.console.Size = new System.Drawing.Size(800, 450);
    this.console.TabIndex = 0;
    this.console.Text = "";
    // 
    // OutputForm
    // 
    this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
    this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
    this.ClientSize = new System.Drawing.Size(800, 450);
    this.Controls.Add(this.console);
    this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
    this.Name = "OutputForm";
    this.Text = "Design Check";
    this.ResumeLayout(false);

  }

  #endregion

  private System.Windows.Forms.RichTextBox console;
}