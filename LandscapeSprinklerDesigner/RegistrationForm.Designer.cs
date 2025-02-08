namespace LandscapeSprinklerDesigner
{
	partial class RegistrationForm
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
      System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(RegistrationForm));
      RegisterButton = new Button();
      RegistrationKey = new TNT.LSD.Components.WaterMarkTextBox();
      label1 = new Label();
      label2 = new Label();
      panelDetail = new TNT.LSD.Components.TNTPanel();
      labelValidUntil = new Label();
      labelIssuedTo = new Label();
      label4 = new Label();
      label3 = new Label();
      panelDetail.SuspendLayout();
      SuspendLayout();
      // 
      // RegisterButton
      // 
      RegisterButton.DialogResult = DialogResult.OK;
      RegisterButton.Location = new Point(609, 325);
      RegisterButton.Margin = new Padding(4, 3, 4, 3);
      RegisterButton.Name = "RegisterButton";
      RegisterButton.Size = new Size(88, 27);
      RegisterButton.TabIndex = 2;
      RegisterButton.Text = "Register";
      RegisterButton.UseVisualStyleBackColor = true;
      // 
      // RegistrationKey
      // 
      RegistrationKey.Font = new Font("Courier New", 8.25F, FontStyle.Regular, GraphicsUnit.Point, 0);
      RegistrationKey.ForeColor = Color.LightGray;
      RegistrationKey.Location = new Point(18, 29);
      RegistrationKey.Margin = new Padding(4, 3, 4, 3);
      RegistrationKey.Multiline = true;
      RegistrationKey.Name = "RegistrationKey";
      RegistrationKey.Size = new Size(678, 177);
      RegistrationKey.TabIndex = 3;
      RegistrationKey.WaterMarkText = resources.GetString("RegistrationKey.Text");
      // 
      // label1
      // 
      label1.AutoSize = true;
      label1.Location = new Point(14, 10);
      label1.Margin = new Padding(4, 0, 4, 0);
      label1.Name = "label1";
      label1.Size = new Size(46, 15);
      label1.TabIndex = 5;
      label1.Text = "License";
      // 
      // label2
      // 
      label2.AutoSize = true;
      label2.Location = new Point(14, 224);
      label2.Margin = new Padding(4, 0, 4, 0);
      label2.Name = "label2";
      label2.Size = new Size(42, 15);
      label2.TabIndex = 6;
      label2.Text = "Details";
      // 
      // panelDetail
      // 
      panelDetail.Controls.Add(labelValidUntil);
      panelDetail.Controls.Add(labelIssuedTo);
      panelDetail.Controls.Add(label4);
      panelDetail.Controls.Add(label3);
      panelDetail.Location = new Point(18, 242);
      panelDetail.Margin = new Padding(4, 3, 4, 3);
      panelDetail.Name = "panelDetail";
      panelDetail.Size = new Size(679, 76);
      panelDetail.TabIndex = 7;
      // 
      // labelValidUntil
      // 
      labelValidUntil.BackColor = SystemColors.Control;
      labelValidUntil.BorderStyle = BorderStyle.FixedSingle;
      labelValidUntil.Location = new Point(90, 40);
      labelValidUntil.Margin = new Padding(4, 0, 4, 0);
      labelValidUntil.Name = "labelValidUntil";
      labelValidUntil.Size = new Size(365, 23);
      labelValidUntil.TabIndex = 4;
      labelValidUntil.TextAlign = ContentAlignment.MiddleLeft;
      // 
      // labelIssuedTo
      // 
      labelIssuedTo.BackColor = SystemColors.Control;
      labelIssuedTo.BorderStyle = BorderStyle.FixedSingle;
      labelIssuedTo.Location = new Point(90, 9);
      labelIssuedTo.Margin = new Padding(4, 0, 4, 0);
      labelIssuedTo.Name = "labelIssuedTo";
      labelIssuedTo.Size = new Size(365, 23);
      labelIssuedTo.TabIndex = 2;
      labelIssuedTo.TextAlign = ContentAlignment.MiddleLeft;
      // 
      // label4
      // 
      label4.AutoSize = true;
      label4.Location = new Point(7, 45);
      label4.Margin = new Padding(4, 0, 4, 0);
      label4.Name = "label4";
      label4.Size = new Size(62, 15);
      label4.TabIndex = 1;
      label4.Text = "Valid until:";
      // 
      // label3
      // 
      label3.AutoSize = true;
      label3.Location = new Point(7, 14);
      label3.Margin = new Padding(4, 0, 4, 0);
      label3.Name = "label3";
      label3.Size = new Size(70, 15);
      label3.TabIndex = 0;
      label3.Text = "Licensed to:";
      // 
      // RegistrationForm
      // 
      AutoScaleDimensions = new SizeF(7F, 15F);
      AutoScaleMode = AutoScaleMode.Font;
      ClientSize = new Size(709, 363);
      Controls.Add(panelDetail);
      Controls.Add(label2);
      Controls.Add(label1);
      Controls.Add(RegistrationKey);
      Controls.Add(RegisterButton);
      FormBorderStyle = FormBorderStyle.FixedDialog;
      Icon = (Icon)resources.GetObject("$this.Icon");
      Margin = new Padding(4, 3, 4, 3);
      MaximizeBox = false;
      MinimizeBox = false;
      Name = "RegistrationForm";
      StartPosition = FormStartPosition.CenterParent;
      Text = "Registration";
      Load += RegistrationForm_Load;
      panelDetail.ResumeLayout(false);
      panelDetail.PerformLayout();
      ResumeLayout(false);
      PerformLayout();
    }

    #endregion
    private System.Windows.Forms.Button RegisterButton;
		private TNT.LSD.Components.WaterMarkTextBox RegistrationKey;
		private System.Windows.Forms.Label label1;
		private System.Windows.Forms.Label label2;
		private TNT.LSD.Components.TNTPanel panelDetail;
		private System.Windows.Forms.Label labelValidUntil;
		private System.Windows.Forms.Label labelIssuedTo;
		private System.Windows.Forms.Label label4;
		private System.Windows.Forms.Label label3;
	}
}