namespace TNT.LSD.Colorizer
{
	partial class ValveColorizer
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
			System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(ValveColorizer));
			this.tableLayoutPanel1 = new System.Windows.Forms.TableLayoutPanel();
			this.ctlp = new System.Windows.Forms.TableLayoutPanel();
			this.DefaultsButton = new System.Windows.Forms.Button();
			this.CancelButton = new System.Windows.Forms.Button();
			this.ApplyButton = new System.Windows.Forms.Button();
			this.tableLayoutPanel1.SuspendLayout();
			this.SuspendLayout();
			// 
			// tableLayoutPanel1
			// 
			this.tableLayoutPanel1.ColumnCount = 2;
			this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 75F));
			this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 25F));
			this.tableLayoutPanel1.Controls.Add(this.ctlp, 0, 0);
			this.tableLayoutPanel1.Controls.Add(this.DefaultsButton, 1, 0);
			this.tableLayoutPanel1.Controls.Add(this.CancelButton, 1, 2);
			this.tableLayoutPanel1.Controls.Add(this.ApplyButton, 0, 2);
			this.tableLayoutPanel1.Dock = System.Windows.Forms.DockStyle.Fill;
			this.tableLayoutPanel1.Location = new System.Drawing.Point(0, 0);
			this.tableLayoutPanel1.Name = "tableLayoutPanel1";
			this.tableLayoutPanel1.RowCount = 3;
			this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 9F));
			this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 82F));
			this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 9F));
			this.tableLayoutPanel1.Size = new System.Drawing.Size(284, 324);
			this.tableLayoutPanel1.TabIndex = 2;
			// 
			// ctlp
			// 
			this.ctlp.ColumnCount = 1;
			this.ctlp.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle());
			this.ctlp.Dock = System.Windows.Forms.DockStyle.Fill;
			this.ctlp.Location = new System.Drawing.Point(3, 3);
			this.ctlp.Name = "ctlp";
			this.ctlp.RowCount = 16;
			this.tableLayoutPanel1.SetRowSpan(this.ctlp, 2);
			this.ctlp.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 6.25F));
			this.ctlp.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 6.25F));
			this.ctlp.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 6.25F));
			this.ctlp.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 6.25F));
			this.ctlp.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 6.25F));
			this.ctlp.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 6.25F));
			this.ctlp.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 6.25F));
			this.ctlp.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 6.25F));
			this.ctlp.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 6.25F));
			this.ctlp.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 6.25F));
			this.ctlp.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 6.25F));
			this.ctlp.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 6.25F));
			this.ctlp.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 6.25F));
			this.ctlp.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 6.25F));
			this.ctlp.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 6.25F));
			this.ctlp.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 6.25F));
			this.ctlp.Size = new System.Drawing.Size(207, 288);
			this.ctlp.TabIndex = 4;
			// 
			// DefaultsButton
			// 
			this.DefaultsButton.Location = new System.Drawing.Point(216, 3);
			this.DefaultsButton.Name = "DefaultsButton";
			this.DefaultsButton.Size = new System.Drawing.Size(65, 21);
			this.DefaultsButton.TabIndex = 8;
			this.DefaultsButton.Text = "Defaults";
			this.DefaultsButton.UseVisualStyleBackColor = true;
			this.DefaultsButton.Click += new System.EventHandler(this.RestoreDefaultColors_Click);
			// 
			// CancelButton
			// 
			this.CancelButton.DialogResult = System.Windows.Forms.DialogResult.Cancel;
			this.CancelButton.Location = new System.Drawing.Point(216, 297);
			this.CancelButton.Name = "CancelButton";
			this.CancelButton.Size = new System.Drawing.Size(65, 21);
			this.CancelButton.TabIndex = 5;
			this.CancelButton.Text = "Cancel";
			this.CancelButton.UseVisualStyleBackColor = true;
			// 
			// ApplyButton
			// 
			this.ApplyButton.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
			this.ApplyButton.DialogResult = System.Windows.Forms.DialogResult.OK;
			this.ApplyButton.Location = new System.Drawing.Point(135, 297);
			this.ApplyButton.Name = "ApplyButton";
			this.ApplyButton.Size = new System.Drawing.Size(75, 21);
			this.ApplyButton.TabIndex = 2;
			this.ApplyButton.Text = "OK";
			this.ApplyButton.UseVisualStyleBackColor = true;
			// 
			// ValveColorizer
			// 
			this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
			this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
			this.ClientSize = new System.Drawing.Size(284, 324);
			this.Controls.Add(this.tableLayoutPanel1);
			this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
			this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
			this.MaximizeBox = false;
			this.MinimizeBox = false;
			this.Name = "ValveColorizer";
			this.Text = "Edit Colors";
			this.tableLayoutPanel1.ResumeLayout(false);
			this.ResumeLayout(false);

		}

		#endregion

		private System.Windows.Forms.TableLayoutPanel tableLayoutPanel1;
		private System.Windows.Forms.Button ApplyButton;
		private System.Windows.Forms.TableLayoutPanel ctlp;
		private System.Windows.Forms.Button CancelButton;
		private System.Windows.Forms.Button DefaultsButton;

	}
}