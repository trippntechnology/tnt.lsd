namespace LandscapeSprinklerDesigner
{
	partial class UpdateInformation
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
			this.LatestVersionLabel = new System.Windows.Forms.Label();
			this.button1 = new System.Windows.Forms.Button();
			this.Link = new System.Windows.Forms.LinkLabel();
			this.tableLayoutPanel1 = new System.Windows.Forms.TableLayoutPanel();
			this.label1 = new System.Windows.Forms.Label();
			this.label4 = new System.Windows.Forms.Label();
			this.YourVersionLabel = new System.Windows.Forms.Label();
			this.tableLayoutPanel1.SuspendLayout();
			this.SuspendLayout();
			// 
			// CurrentVersionLabel
			// 
			this.LatestVersionLabel.AutoSize = true;
			this.tableLayoutPanel1.SetColumnSpan(this.LatestVersionLabel, 2);
			this.LatestVersionLabel.Dock = System.Windows.Forms.DockStyle.Fill;
			this.LatestVersionLabel.Location = new System.Drawing.Point(108, 27);
			this.LatestVersionLabel.Name = "CurrentVersionLabel";
			this.LatestVersionLabel.Size = new System.Drawing.Size(207, 27);
			this.LatestVersionLabel.TabIndex = 1;
			this.LatestVersionLabel.Text = "Latest Version:";
			this.LatestVersionLabel.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
			// 
			// button1
			// 
			this.button1.DialogResult = System.Windows.Forms.DialogResult.OK;
			this.button1.Location = new System.Drawing.Point(229, 88);
			this.button1.Margin = new System.Windows.Forms.Padding(18, 7, 3, 3);
			this.button1.Name = "button1";
			this.button1.Size = new System.Drawing.Size(75, 23);
			this.button1.TabIndex = 2;
			this.button1.Text = "OK";
			this.button1.UseVisualStyleBackColor = true;
			// 
			// Link
			// 
			this.Link.AutoSize = true;
			this.tableLayoutPanel1.SetColumnSpan(this.Link, 3);
			this.Link.Dock = System.Windows.Forms.DockStyle.Fill;
			this.Link.Location = new System.Drawing.Point(3, 54);
			this.Link.Name = "Link";
			this.Link.Size = new System.Drawing.Size(312, 27);
			this.Link.TabIndex = 3;
			this.Link.TabStop = true;
			this.Link.Text = "Click here to download the latest version";
			this.Link.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
			// 
			// tableLayoutPanel1
			// 
			this.tableLayoutPanel1.ColumnCount = 3;
			this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 33.33333F));
			this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 33.33334F));
			this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 33.33334F));
			this.tableLayoutPanel1.Controls.Add(this.label1, 0, 0);
			this.tableLayoutPanel1.Controls.Add(this.Link, 0, 2);
			this.tableLayoutPanel1.Controls.Add(this.label4, 0, 1);
			this.tableLayoutPanel1.Controls.Add(this.YourVersionLabel, 1, 0);
			this.tableLayoutPanel1.Controls.Add(this.LatestVersionLabel, 1, 1);
			this.tableLayoutPanel1.Controls.Add(this.button1, 2, 3);
			this.tableLayoutPanel1.Dock = System.Windows.Forms.DockStyle.Fill;
			this.tableLayoutPanel1.Location = new System.Drawing.Point(0, 0);
			this.tableLayoutPanel1.Name = "tableLayoutPanel1";
			this.tableLayoutPanel1.RowCount = 4;
			this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 23.80952F));
			this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 23.80952F));
			this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 23.80952F));
			this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 28.57143F));
			this.tableLayoutPanel1.Size = new System.Drawing.Size(318, 117);
			this.tableLayoutPanel1.TabIndex = 4;
			// 
			// label1
			// 
			this.label1.AutoSize = true;
			this.label1.Dock = System.Windows.Forms.DockStyle.Fill;
			this.label1.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
			this.label1.Location = new System.Drawing.Point(3, 0);
			this.label1.Name = "label1";
			this.label1.Size = new System.Drawing.Size(99, 27);
			this.label1.TabIndex = 1;
			this.label1.Text = "Your Version:";
			this.label1.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
			// 
			// label4
			// 
			this.label4.AutoSize = true;
			this.label4.Dock = System.Windows.Forms.DockStyle.Fill;
			this.label4.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
			this.label4.Location = new System.Drawing.Point(3, 27);
			this.label4.Name = "label4";
			this.label4.Size = new System.Drawing.Size(99, 27);
			this.label4.TabIndex = 4;
			this.label4.Text = "Latest Version:";
			this.label4.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
			// 
			// YourVersionLabel
			// 
			this.YourVersionLabel.AutoSize = true;
			this.tableLayoutPanel1.SetColumnSpan(this.YourVersionLabel, 2);
			this.YourVersionLabel.Dock = System.Windows.Forms.DockStyle.Fill;
			this.YourVersionLabel.Location = new System.Drawing.Point(108, 0);
			this.YourVersionLabel.Name = "YourVersionLabel";
			this.YourVersionLabel.Size = new System.Drawing.Size(207, 27);
			this.YourVersionLabel.TabIndex = 3;
			this.YourVersionLabel.Text = "Your Version:";
			this.YourVersionLabel.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
			// 
			// UpdateInformation
			// 
			this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
			this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
			this.ClientSize = new System.Drawing.Size(318, 117);
			this.Controls.Add(this.tableLayoutPanel1);
			this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
			this.MaximizeBox = false;
			this.MinimizeBox = false;
			this.Name = "UpdateInformation";
			this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
			this.Text = "Update Available";
			this.tableLayoutPanel1.ResumeLayout(false);
			this.tableLayoutPanel1.PerformLayout();
			this.ResumeLayout(false);

		}

		#endregion

		private System.Windows.Forms.Label LatestVersionLabel;
		private System.Windows.Forms.TableLayoutPanel tableLayoutPanel1;
		private System.Windows.Forms.Label label1;
		private System.Windows.Forms.LinkLabel Link;
		private System.Windows.Forms.Label label4;
		private System.Windows.Forms.Label YourVersionLabel;
		private System.Windows.Forms.Button button1;

	}
}