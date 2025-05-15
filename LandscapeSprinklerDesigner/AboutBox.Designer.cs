namespace LandscapeSprinklerDesigner
{
	partial class AboutBox
	{
		/// <summary>
		/// Required designer variable.
		/// </summary>
		private System.ComponentModel.IContainer components = null;

		/// <summary>
		/// Clean up any resources being used.
		/// </summary>
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
      System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(AboutBox));
      tableLayoutPanel = new TableLayoutPanel();
      logoPictureBox = new PictureBox();
      VersionLabel = new Label();
      listView1 = new ListView();
      File = new ColumnHeader();
      Version = new ColumnHeader();
      Owner = new ColumnHeader();
      tableLayoutPanel1 = new TableLayoutPanel();
      button1 = new Button();
      CopyrightLabel = new Label();
      tableLayoutPanel.SuspendLayout();
      ((System.ComponentModel.ISupportInitialize)logoPictureBox).BeginInit();
      tableLayoutPanel1.SuspendLayout();
      SuspendLayout();
      // 
      // tableLayoutPanel
      // 
      tableLayoutPanel.ColumnCount = 2;
      tableLayoutPanel.ColumnStyles.Add(new ColumnStyle(SizeType.Absolute, 198F));
      tableLayoutPanel.ColumnStyles.Add(new ColumnStyle(SizeType.Absolute, 638F));
      tableLayoutPanel.Controls.Add(logoPictureBox, 0, 0);
      tableLayoutPanel.Controls.Add(VersionLabel, 1, 0);
      tableLayoutPanel.Controls.Add(listView1, 1, 1);
      tableLayoutPanel.Controls.Add(tableLayoutPanel1, 1, 2);
      tableLayoutPanel.Dock = DockStyle.Fill;
      tableLayoutPanel.Location = new Point(10, 10);
      tableLayoutPanel.Margin = new Padding(4, 3, 4, 3);
      tableLayoutPanel.Name = "tableLayoutPanel";
      tableLayoutPanel.RowCount = 3;
      tableLayoutPanel.RowStyles.Add(new RowStyle(SizeType.Percent, 5F));
      tableLayoutPanel.RowStyles.Add(new RowStyle(SizeType.Percent, 85F));
      tableLayoutPanel.RowStyles.Add(new RowStyle(SizeType.Percent, 10F));
      tableLayoutPanel.Size = new Size(838, 370);
      tableLayoutPanel.TabIndex = 0;
      // 
      // logoPictureBox
      // 
      logoPictureBox.Dock = DockStyle.Fill;
      logoPictureBox.Image = (Image)resources.GetObject("logoPictureBox.Image");
      logoPictureBox.Location = new Point(4, 3);
      logoPictureBox.Margin = new Padding(4, 3, 4, 3);
      logoPictureBox.Name = "logoPictureBox";
      tableLayoutPanel.SetRowSpan(logoPictureBox, 3);
      logoPictureBox.Size = new Size(190, 364);
      logoPictureBox.SizeMode = PictureBoxSizeMode.StretchImage;
      logoPictureBox.TabIndex = 13;
      logoPictureBox.TabStop = false;
      // 
      // VersionLabel
      // 
      VersionLabel.AutoSize = true;
      VersionLabel.Dock = DockStyle.Fill;
      VersionLabel.Location = new Point(202, 0);
      VersionLabel.Margin = new Padding(4, 0, 4, 0);
      VersionLabel.Name = "VersionLabel";
      VersionLabel.Size = new Size(632, 18);
      VersionLabel.TabIndex = 14;
      VersionLabel.Text = "VersionLabel";
      // 
      // listView1
      // 
      listView1.Columns.AddRange(new ColumnHeader[] { File, Version, Owner });
      listView1.Dock = DockStyle.Fill;
      listView1.Location = new Point(202, 21);
      listView1.Margin = new Padding(4, 3, 4, 3);
      listView1.Name = "listView1";
      listView1.Size = new Size(632, 308);
      listView1.TabIndex = 27;
      listView1.UseCompatibleStateImageBehavior = false;
      listView1.View = View.Details;
      listView1.ColumnClick += listView1_ColumnClick;
      // 
      // File
      // 
      File.Text = "File";
      File.Width = 150;
      // 
      // Version
      // 
      Version.Text = "Version";
      Version.Width = 100;
      // 
      // Owner
      // 
      Owner.Text = "Owner";
      Owner.Width = 287;
      // 
      // tableLayoutPanel1
      // 
      tableLayoutPanel1.ColumnCount = 2;
      tableLayoutPanel1.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 80F));
      tableLayoutPanel1.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 20F));
      tableLayoutPanel1.Controls.Add(button1, 1, 0);
      tableLayoutPanel1.Controls.Add(CopyrightLabel, 0, 0);
      tableLayoutPanel1.Dock = DockStyle.Fill;
      tableLayoutPanel1.Location = new Point(202, 335);
      tableLayoutPanel1.Margin = new Padding(4, 3, 4, 3);
      tableLayoutPanel1.Name = "tableLayoutPanel1";
      tableLayoutPanel1.RowCount = 1;
      tableLayoutPanel1.RowStyles.Add(new RowStyle(SizeType.Percent, 100F));
      tableLayoutPanel1.Size = new Size(632, 32);
      tableLayoutPanel1.TabIndex = 28;
      // 
      // button1
      // 
      button1.Anchor = AnchorStyles.Top | AnchorStyles.Bottom | AnchorStyles.Right;
      button1.DialogResult = DialogResult.OK;
      button1.Location = new Point(554, 3);
      button1.Margin = new Padding(4, 3, 4, 3);
      button1.Name = "button1";
      button1.Size = new Size(74, 26);
      button1.TabIndex = 0;
      button1.Text = "OK";
      button1.UseVisualStyleBackColor = true;
      // 
      // CopyrightLabel
      // 
      CopyrightLabel.AutoSize = true;
      CopyrightLabel.Dock = DockStyle.Fill;
      CopyrightLabel.Location = new Point(4, 0);
      CopyrightLabel.Margin = new Padding(4, 0, 4, 0);
      CopyrightLabel.Name = "CopyrightLabel";
      CopyrightLabel.Size = new Size(497, 32);
      CopyrightLabel.TabIndex = 1;
      CopyrightLabel.Text = "label1";
      CopyrightLabel.TextAlign = ContentAlignment.MiddleCenter;
      // 
      // AboutBox
      // 
      AutoScaleDimensions = new SizeF(7F, 15F);
      AutoScaleMode = AutoScaleMode.Font;
      ClientSize = new Size(858, 390);
      Controls.Add(tableLayoutPanel);
      FormBorderStyle = FormBorderStyle.FixedDialog;
      Margin = new Padding(4, 3, 4, 3);
      MaximizeBox = false;
      MinimizeBox = false;
      Name = "AboutBox";
      Padding = new Padding(10);
      ShowIcon = false;
      ShowInTaskbar = false;
      StartPosition = FormStartPosition.CenterParent;
      Text = "About";
      Load += AboutBox_Load;
      tableLayoutPanel.ResumeLayout(false);
      tableLayoutPanel.PerformLayout();
      ((System.ComponentModel.ISupportInitialize)logoPictureBox).EndInit();
      tableLayoutPanel1.ResumeLayout(false);
      tableLayoutPanel1.PerformLayout();
      ResumeLayout(false);

    }

    #endregion

    private System.Windows.Forms.TableLayoutPanel tableLayoutPanel;
		private System.Windows.Forms.PictureBox logoPictureBox;
		private System.Windows.Forms.Label VersionLabel;
		private System.Windows.Forms.ListView listView1;
		private System.Windows.Forms.ColumnHeader File;
		private System.Windows.Forms.ColumnHeader Version;
		private System.Windows.Forms.ColumnHeader Owner;
		private System.Windows.Forms.TableLayoutPanel tableLayoutPanel1;
		private System.Windows.Forms.Button button1;
		private System.Windows.Forms.Label CopyrightLabel;
	}
}
