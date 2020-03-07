namespace TNT.LSD.Background
{
	partial class BackgroundImporter
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
			System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(BackgroundImporter));
			this.FindFileDialog = new System.Windows.Forms.OpenFileDialog();
			this.tableLayoutPanel1 = new System.Windows.Forms.TableLayoutPanel();
			this.ImportButton = new System.Windows.Forms.Button();
			this.PixelPerFootTextBox = new System.Windows.Forms.TextBox();
			this.FindFileButton = new System.Windows.Forms.Button();
			this.FileNameTextBox = new System.Windows.Forms.TextBox();
			this.label2 = new System.Windows.Forms.Label();
			this.label1 = new System.Windows.Forms.Label();
			this.tableLayoutPanel1.SuspendLayout();
			this.SuspendLayout();
			// 
			// FindFileDialog
			// 
			this.FindFileDialog.AutoUpgradeEnabled = false;
			this.FindFileDialog.Filter = "JPG Files;PNG Files;BMP Files|*.jpg;*.png;*.bmp";
			this.FindFileDialog.RestoreDirectory = true;
			this.FindFileDialog.Title = "Find Background Image";
			// 
			// tableLayoutPanel1
			// 
			this.tableLayoutPanel1.ColumnCount = 3;
			this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 26.0479F));
			this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 73.9521F));
			this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 36F));
			this.tableLayoutPanel1.Controls.Add(this.ImportButton, 1, 2);
			this.tableLayoutPanel1.Controls.Add(this.PixelPerFootTextBox, 1, 1);
			this.tableLayoutPanel1.Controls.Add(this.FindFileButton, 2, 0);
			this.tableLayoutPanel1.Controls.Add(this.FileNameTextBox, 1, 0);
			this.tableLayoutPanel1.Controls.Add(this.label2, 0, 1);
			this.tableLayoutPanel1.Controls.Add(this.label1, 0, 0);
			this.tableLayoutPanel1.Dock = System.Windows.Forms.DockStyle.Fill;
			this.tableLayoutPanel1.Location = new System.Drawing.Point(0, 0);
			this.tableLayoutPanel1.Name = "tableLayoutPanel1";
			this.tableLayoutPanel1.RowCount = 3;
			this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 50F));
			this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 50F));
			this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 30F));
			this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 20F));
			this.tableLayoutPanel1.Size = new System.Drawing.Size(365, 81);
			this.tableLayoutPanel1.TabIndex = 8;
			// 
			// ImportButton
			// 
			this.ImportButton.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
			this.tableLayoutPanel1.SetColumnSpan(this.ImportButton, 2);
			this.ImportButton.DialogResult = System.Windows.Forms.DialogResult.OK;
			this.ImportButton.Location = new System.Drawing.Point(287, 53);
			this.ImportButton.Name = "ImportButton";
			this.ImportButton.Size = new System.Drawing.Size(75, 23);
			this.ImportButton.TabIndex = 10;
			this.ImportButton.Text = "Import";
			this.ImportButton.UseVisualStyleBackColor = true;
			// 
			// PixelPerFootTextBox
			// 
			this.PixelPerFootTextBox.Dock = System.Windows.Forms.DockStyle.Left;
			this.PixelPerFootTextBox.Location = new System.Drawing.Point(88, 28);
			this.PixelPerFootTextBox.Name = "PixelPerFootTextBox";
			this.PixelPerFootTextBox.Size = new System.Drawing.Size(100, 20);
			this.PixelPerFootTextBox.TabIndex = 8;
			this.PixelPerFootTextBox.Text = "1.0";
			// 
			// FindFileButton
			// 
			this.FindFileButton.Location = new System.Drawing.Point(331, 3);
			this.FindFileButton.Name = "FindFileButton";
			this.FindFileButton.Size = new System.Drawing.Size(25, 19);
			this.FindFileButton.TabIndex = 6;
			this.FindFileButton.Text = "...";
			this.FindFileButton.UseVisualStyleBackColor = true;
			this.FindFileButton.Click += new System.EventHandler(this.FindFileButton_Click);
			// 
			// FileNameTextBox
			// 
			this.FileNameTextBox.Dock = System.Windows.Forms.DockStyle.Fill;
			this.FileNameTextBox.Location = new System.Drawing.Point(88, 3);
			this.FileNameTextBox.Name = "FileNameTextBox";
			this.FileNameTextBox.ReadOnly = true;
			this.FileNameTextBox.Size = new System.Drawing.Size(237, 20);
			this.FileNameTextBox.TabIndex = 5;
			// 
			// label2
			// 
			this.label2.AutoSize = true;
			this.label2.Dock = System.Windows.Forms.DockStyle.Fill;
			this.label2.Location = new System.Drawing.Point(3, 25);
			this.label2.Name = "label2";
			this.label2.Size = new System.Drawing.Size(79, 25);
			this.label2.TabIndex = 4;
			this.label2.Text = "Pixels Per Foot:";
			this.label2.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
			// 
			// label1
			// 
			this.label1.AutoSize = true;
			this.label1.Dock = System.Windows.Forms.DockStyle.Fill;
			this.label1.Location = new System.Drawing.Point(3, 0);
			this.label1.Name = "label1";
			this.label1.Size = new System.Drawing.Size(79, 25);
			this.label1.TabIndex = 2;
			this.label1.Text = "File Name:";
			this.label1.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
			// 
			// BackgroundImporter
			// 
			this.AcceptButton = this.ImportButton;
			this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
			this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
			this.ClientSize = new System.Drawing.Size(365, 81);
			this.Controls.Add(this.tableLayoutPanel1);
			this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
			this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
			this.MaximizeBox = false;
			this.MinimizeBox = false;
			this.Name = "BackgroundImporter";
			this.Text = "Import Background Image";
			this.tableLayoutPanel1.ResumeLayout(false);
			this.tableLayoutPanel1.PerformLayout();
			this.ResumeLayout(false);

		}

		#endregion

		private System.Windows.Forms.OpenFileDialog FindFileDialog;
		private System.Windows.Forms.TableLayoutPanel tableLayoutPanel1;
		private System.Windows.Forms.Button ImportButton;
		private System.Windows.Forms.TextBox PixelPerFootTextBox;
		private System.Windows.Forms.Button FindFileButton;
		private System.Windows.Forms.TextBox FileNameTextBox;
		private System.Windows.Forms.Label label2;
		private System.Windows.Forms.Label label1;
	}
}