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
			this.RegisterButton = new System.Windows.Forms.Button();
			this.LicenseText = new TNT.LSD.Components.WaterMarkTextBox();
			this.label1 = new System.Windows.Forms.Label();
			this.label2 = new System.Windows.Forms.Label();
			this.panelDetail = new TNT.LSD.Components.TNTPanel();
			this.labelValidUntil = new System.Windows.Forms.Label();
			this.labelIssuedTo = new System.Windows.Forms.Label();
			this.label4 = new System.Windows.Forms.Label();
			this.label3 = new System.Windows.Forms.Label();
			this.panelDetail.SuspendLayout();
			this.SuspendLayout();
			// 
			// RegisterButton
			// 
			this.RegisterButton.DialogResult = System.Windows.Forms.DialogResult.OK;
			this.RegisterButton.Location = new System.Drawing.Point(522, 282);
			this.RegisterButton.Name = "RegisterButton";
			this.RegisterButton.Size = new System.Drawing.Size(75, 23);
			this.RegisterButton.TabIndex = 2;
			this.RegisterButton.Text = "Register";
			this.RegisterButton.UseVisualStyleBackColor = true;
			// 
			// LicenseText
			// 
			this.LicenseText.Font = new System.Drawing.Font("Courier New", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
			this.LicenseText.ForeColor = System.Drawing.Color.LightGray;
			this.LicenseText.Location = new System.Drawing.Point(15, 25);
			this.LicenseText.Multiline = true;
			this.LicenseText.Name = "LicenseText";
			this.LicenseText.Size = new System.Drawing.Size(582, 154);
			this.LicenseText.TabIndex = 3;
			this.LicenseText.Text = resources.GetString("LicenseText.Text");
			this.LicenseText.WaterMarkText = resources.GetString("LicenseText.WaterMarkText");
			this.LicenseText.TextChanged += new System.EventHandler(this.LicenseText_TextChanged);
			// 
			// label1
			// 
			this.label1.AutoSize = true;
			this.label1.Location = new System.Drawing.Point(12, 9);
			this.label1.Name = "label1";
			this.label1.Size = new System.Drawing.Size(44, 13);
			this.label1.TabIndex = 5;
			this.label1.Text = "License";
			// 
			// label2
			// 
			this.label2.AutoSize = true;
			this.label2.Location = new System.Drawing.Point(12, 194);
			this.label2.Name = "label2";
			this.label2.Size = new System.Drawing.Size(39, 13);
			this.label2.TabIndex = 6;
			this.label2.Text = "Details";
			// 
			// panelDetail
			// 
			this.panelDetail.Controls.Add(this.labelValidUntil);
			this.panelDetail.Controls.Add(this.labelIssuedTo);
			this.panelDetail.Controls.Add(this.label4);
			this.panelDetail.Controls.Add(this.label3);
			this.panelDetail.Location = new System.Drawing.Point(15, 210);
			this.panelDetail.Name = "panelDetail";
			this.panelDetail.Size = new System.Drawing.Size(582, 66);
			this.panelDetail.TabIndex = 7;
			// 
			// labelValidUntil
			// 
			this.labelValidUntil.BackColor = System.Drawing.SystemColors.Control;
			this.labelValidUntil.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
			this.labelValidUntil.Location = new System.Drawing.Point(77, 35);
			this.labelValidUntil.Name = "labelValidUntil";
			this.labelValidUntil.Size = new System.Drawing.Size(313, 20);
			this.labelValidUntil.TabIndex = 4;
			this.labelValidUntil.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
			// 
			// labelIssuedTo
			// 
			this.labelIssuedTo.BackColor = System.Drawing.SystemColors.Control;
			this.labelIssuedTo.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
			this.labelIssuedTo.Location = new System.Drawing.Point(77, 8);
			this.labelIssuedTo.Name = "labelIssuedTo";
			this.labelIssuedTo.Size = new System.Drawing.Size(313, 20);
			this.labelIssuedTo.TabIndex = 2;
			this.labelIssuedTo.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
			// 
			// label4
			// 
			this.label4.AutoSize = true;
			this.label4.Location = new System.Drawing.Point(6, 39);
			this.label4.Name = "label4";
			this.label4.Size = new System.Drawing.Size(55, 13);
			this.label4.TabIndex = 1;
			this.label4.Text = "Valid until:";
			// 
			// label3
			// 
			this.label3.AutoSize = true;
			this.label3.Location = new System.Drawing.Point(6, 12);
			this.label3.Name = "label3";
			this.label3.Size = new System.Drawing.Size(65, 13);
			this.label3.TabIndex = 0;
			this.label3.Text = "Licensed to:";
			// 
			// RegistrationForm
			// 
			this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
			this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
			this.ClientSize = new System.Drawing.Size(608, 315);
			this.Controls.Add(this.panelDetail);
			this.Controls.Add(this.label2);
			this.Controls.Add(this.label1);
			this.Controls.Add(this.LicenseText);
			this.Controls.Add(this.RegisterButton);
			this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
			this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
			this.MaximizeBox = false;
			this.MinimizeBox = false;
			this.Name = "RegistrationForm";
			this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
			this.Text = "Registration";
			this.panelDetail.ResumeLayout(false);
			this.panelDetail.PerformLayout();
			this.ResumeLayout(false);
			this.PerformLayout();

		}

		#endregion
		private System.Windows.Forms.Button RegisterButton;
		private TNT.LSD.Components.WaterMarkTextBox LicenseText;
		private System.Windows.Forms.Label label1;
		private System.Windows.Forms.Label label2;
		private TNT.LSD.Components.TNTPanel panelDetail;
		private System.Windows.Forms.Label labelValidUntil;
		private System.Windows.Forms.Label labelIssuedTo;
		private System.Windows.Forms.Label label4;
		private System.Windows.Forms.Label label3;
	}
}