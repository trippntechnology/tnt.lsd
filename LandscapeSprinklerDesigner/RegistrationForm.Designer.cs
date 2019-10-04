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
			this.RegisterButton = new System.Windows.Forms.Button();
			this.LicenseText = new LSDComponents.WaterMarkTextBox();
			this.SuspendLayout();
			// 
			// label1
			// 
			this.label1.AutoSize = true;
			this.label1.Location = new System.Drawing.Point(12, 9);
			this.label1.Name = "label1";
			this.label1.Size = new System.Drawing.Size(47, 13);
			this.label1.TabIndex = 0;
			this.label1.Text = "License:";
			// 
			// RegisterButton
			// 
			this.RegisterButton.DialogResult = System.Windows.Forms.DialogResult.OK;
			this.RegisterButton.Location = new System.Drawing.Point(572, 169);
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
			this.LicenseText.Location = new System.Drawing.Point(65, 9);
			this.LicenseText.Multiline = true;
			this.LicenseText.Name = "LicenseText";
			this.LicenseText.Size = new System.Drawing.Size(582, 154);
			this.LicenseText.TabIndex = 3;
			this.LicenseText.Text = resources.GetString("LicenseText.Text");
			this.LicenseText.WaterMarkText = resources.GetString("LicenseText.WaterMarkText");
			// 
			// RegistrationForm
			// 
			this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
			this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
			this.ClientSize = new System.Drawing.Size(657, 198);
			this.Controls.Add(this.LicenseText);
			this.Controls.Add(this.RegisterButton);
			this.Controls.Add(this.label1);
			this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
			this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
			this.MaximizeBox = false;
			this.MinimizeBox = false;
			this.Name = "RegistrationForm";
			this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
			this.Text = "Registration";
			this.ResumeLayout(false);
			this.PerformLayout();

		}

		#endregion

		private System.Windows.Forms.Label label1;
		private System.Windows.Forms.Button RegisterButton;
		private LSDComponents.WaterMarkTextBox LicenseText;
	}
}