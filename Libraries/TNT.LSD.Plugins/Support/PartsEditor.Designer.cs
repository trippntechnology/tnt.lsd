namespace TNT.LSD.Plugins
{
	partial class PartsEditor
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
			this.components = new System.ComponentModel.Container();
			System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(PartsEditor));
			this.cbCodes = new System.Windows.Forms.ComboBox();
			this.StateImageList = new System.Windows.Forms.ImageList(this.components);
			this.tbQuantity = new System.Windows.Forms.TextBox();
			this.PartsListView = new System.Windows.Forms.ListView();
			this.columnHeader1 = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
			this.Code = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
			this.Description = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
			this.Quantity = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
			this.ContextMenuListView = new System.Windows.Forms.ContextMenuStrip(this.components);
			this.newToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
			this.cbDescriptions = new System.Windows.Forms.ComboBox();
			this.button1 = new System.Windows.Forms.Button();
			this.button2 = new System.Windows.Forms.Button();
			this.ContextMenuListView.SuspendLayout();
			this.SuspendLayout();
			// 
			// cbCodes
			// 
			this.cbCodes.AutoCompleteMode = System.Windows.Forms.AutoCompleteMode.SuggestAppend;
			this.cbCodes.AutoCompleteSource = System.Windows.Forms.AutoCompleteSource.ListItems;
			this.cbCodes.Location = new System.Drawing.Point(30, 25);
			this.cbCodes.Name = "cbCodes";
			this.cbCodes.Size = new System.Drawing.Size(97, 21);
			this.cbCodes.Sorted = true;
			this.cbCodes.TabIndex = 1;
			this.cbCodes.Visible = false;
			this.cbCodes.SelectedValueChanged += new System.EventHandler(this.ComboBox_SelectedValueChanged);
			this.cbCodes.Leave += new System.EventHandler(this.Control_Leave);
			// 
			// StateImageList
			// 
			this.StateImageList.ImageStream = ((System.Windows.Forms.ImageListStreamer)(resources.GetObject("StateImageList.ImageStream")));
			this.StateImageList.TransparentColor = System.Drawing.Color.Transparent;
			this.StateImageList.Images.SetKeyName(0, "delete20.png");
			this.StateImageList.Images.SetKeyName(1, "delete20.png");
			// 
			// tbQuantity
			// 
			this.tbQuantity.Location = new System.Drawing.Point(332, 26);
			this.tbQuantity.Name = "tbQuantity";
			this.tbQuantity.Size = new System.Drawing.Size(49, 20);
			this.tbQuantity.TabIndex = 2;
			this.tbQuantity.Visible = false;
			this.tbQuantity.TextChanged += new System.EventHandler(this.Quantity_TextChanged);
			// 
			// PartsListView
			// 
			this.PartsListView.CheckBoxes = true;
			this.PartsListView.Columns.AddRange(new System.Windows.Forms.ColumnHeader[] {
            this.columnHeader1,
            this.Code,
            this.Description,
            this.Quantity});
			this.PartsListView.ContextMenuStrip = this.ContextMenuListView;
			this.PartsListView.Dock = System.Windows.Forms.DockStyle.Top;
			this.PartsListView.GridLines = true;
			this.PartsListView.Location = new System.Drawing.Point(0, 0);
			this.PartsListView.Name = "PartsListView";
			this.PartsListView.Size = new System.Drawing.Size(384, 341);
			this.PartsListView.StateImageList = this.StateImageList;
			this.PartsListView.TabIndex = 0;
			this.PartsListView.UseCompatibleStateImageBehavior = false;
			this.PartsListView.View = System.Windows.Forms.View.Details;
			this.PartsListView.ItemCheck += new System.Windows.Forms.ItemCheckEventHandler(this.PartsListView_ItemCheck);
			this.PartsListView.MouseDown += new System.Windows.Forms.MouseEventHandler(this.PartsListView_MouseDown);
			// 
			// columnHeader1
			// 
			this.columnHeader1.Text = "";
			this.columnHeader1.Width = 28;
			// 
			// Code
			// 
			this.Code.Text = "Code";
			this.Code.Width = 97;
			// 
			// Description
			// 
			this.Description.Text = "Description";
			this.Description.Width = 200;
			// 
			// Quantity
			// 
			this.Quantity.Text = "Quantity";
			this.Quantity.Width = 55;
			// 
			// ContextMenuListView
			// 
			this.ContextMenuListView.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.newToolStripMenuItem});
			this.ContextMenuListView.Name = "contextMenuStrip1";
			this.ContextMenuListView.Size = new System.Drawing.Size(97, 26);
			// 
			// newToolStripMenuItem
			// 
			this.newToolStripMenuItem.Image = ((System.Drawing.Image)(resources.GetObject("newToolStripMenuItem.Image")));
			this.newToolStripMenuItem.Name = "newToolStripMenuItem";
			this.newToolStripMenuItem.Size = new System.Drawing.Size(96, 22);
			this.newToolStripMenuItem.Text = "Add";
			this.newToolStripMenuItem.Click += new System.EventHandler(this.Add_Click);
			// 
			// cbDescriptions
			// 
			this.cbDescriptions.AutoCompleteMode = System.Windows.Forms.AutoCompleteMode.SuggestAppend;
			this.cbDescriptions.AutoCompleteSource = System.Windows.Forms.AutoCompleteSource.ListItems;
			this.cbDescriptions.Location = new System.Drawing.Point(128, 25);
			this.cbDescriptions.Name = "cbDescriptions";
			this.cbDescriptions.Size = new System.Drawing.Size(198, 21);
			this.cbDescriptions.Sorted = true;
			this.cbDescriptions.TabIndex = 3;
			this.cbDescriptions.Visible = false;
			this.cbDescriptions.SelectedValueChanged += new System.EventHandler(this.ComboBox_SelectedValueChanged);
			this.cbDescriptions.Leave += new System.EventHandler(this.Control_Leave);
			// 
			// button1
			// 
			this.button1.DialogResult = System.Windows.Forms.DialogResult.OK;
			this.button1.Location = new System.Drawing.Point(216, 349);
			this.button1.Name = "button1";
			this.button1.Size = new System.Drawing.Size(75, 23);
			this.button1.TabIndex = 4;
			this.button1.Text = "OK";
			this.button1.UseVisualStyleBackColor = true;
			// 
			// button2
			// 
			this.button2.DialogResult = System.Windows.Forms.DialogResult.Cancel;
			this.button2.Location = new System.Drawing.Point(297, 349);
			this.button2.Name = "button2";
			this.button2.Size = new System.Drawing.Size(75, 23);
			this.button2.TabIndex = 5;
			this.button2.Text = "Cancel";
			this.button2.UseVisualStyleBackColor = true;
			// 
			// PartsEditor
			// 
			this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
			this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
			this.ClientSize = new System.Drawing.Size(384, 384);
			this.Controls.Add(this.button2);
			this.Controls.Add(this.button1);
			this.Controls.Add(this.cbDescriptions);
			this.Controls.Add(this.tbQuantity);
			this.Controls.Add(this.cbCodes);
			this.Controls.Add(this.PartsListView);
			this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
			this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
			this.KeyPreview = true;
			this.MaximizeBox = false;
			this.MinimizeBox = false;
			this.Name = "PartsEditor";
			this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
			this.Text = "Parts Editor";
			this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.PartsEditor_FormClosing);
			this.ContextMenuListView.ResumeLayout(false);
			this.ResumeLayout(false);
			this.PerformLayout();

		}

		#endregion

		private System.Windows.Forms.ListView PartsListView;
		private System.Windows.Forms.ColumnHeader Code;
		private System.Windows.Forms.ColumnHeader Description;
		private System.Windows.Forms.ColumnHeader Quantity;
		private System.Windows.Forms.ComboBox cbCodes;
		private System.Windows.Forms.ImageList StateImageList;
		private System.Windows.Forms.TextBox tbQuantity;
		private System.Windows.Forms.ComboBox cbDescriptions;
		private System.Windows.Forms.ColumnHeader columnHeader1;
		private System.Windows.Forms.Button button1;
		private System.Windows.Forms.Button button2;
		private System.Windows.Forms.ContextMenuStrip ContextMenuListView;
		private System.Windows.Forms.ToolStripMenuItem newToolStripMenuItem;
	}
}