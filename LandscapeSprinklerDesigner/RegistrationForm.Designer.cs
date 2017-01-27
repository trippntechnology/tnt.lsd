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
			this.label1 = new System.Windows.Forms.Label();
			this.LicenseText = new System.Windows.Forms.TextBox();
			this.RegisterButton = new System.Windows.Forms.Button();
			this.SuspendLayout();
			// 
			// label1
			// 
			this.label1.AutoSize = true;
			this.label1.Location = new System.Drawing.Point(12, 9);
			this.label1.Name = "label1";
			this.label1.Size = new System.Drawing.Size(68, 13);
			this.label1.TabIndex = 0;
			this.label1.Text = "License Key:";
			// 
			// LicenseText
			// 
			this.LicenseText.Location = new System.Drawing.Point(86, 6);
			this.LicenseText.Name = "LicenseText";
			this.LicenseText.Size = new System.Drawing.Size(186, 20);
			this.LicenseText.TabIndex = 1;
			// 
			// RegisterButton
			// 
			this.RegisterButton.DialogResult = System.Windows.Forms.DialogResult.OK;
			this.RegisterButton.Location = new System.Drawing.Point(197, 44);
			this.RegisterButton.Name = "RegisterButton";
			this.RegisterButton.Size = new System.Drawing.Size(75, 23);
			this.RegisterButton.TabIndex = 2;
			this.RegisterButton.Text = "Register";
			this.RegisterButton.UseVisualStyleBackColor = true;
			// 
			// RegistrationForm
			// 
			this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
			this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
			this.ClientSize = new System.Drawing.Size(284, 81);
			this.Controls.Add(this.RegisterButton);
			this.Controls.Add(this.LicenseText);
			this.Controls.Add(this.label1);
			this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
			this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
			this.MaximizeBox = false;
			this.MinimizeBox = false;
			this.Name = "RegistrationForm";
			this.Text = "Registration";
			this.ResumeLayout(false);
			this.PerformLayout();

		}

		#endregion

		private System.Windows.Forms.Label label1;
		private System.Windows.Forms.TextBox LicenseText;
		private System.Windows.Forms.Button RegisterButton;
	}
}