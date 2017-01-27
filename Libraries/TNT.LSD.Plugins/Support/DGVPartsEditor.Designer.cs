namespace TNT.LSD.Plugins.Support
{
	partial class DGVPartsEditor
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
			System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(DGVPartsEditor));
			this._DataGrid = new System.Windows.Forms.DataGridView();
			this.contextMenuStrip1 = new System.Windows.Forms.ContextMenuStrip(this.components);
			this.addToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
			this.deleteToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
			this.button2 = new System.Windows.Forms.Button();
			this.button1 = new System.Windows.Forms.Button();
			this.codeBindingSource = new System.Windows.Forms.BindingSource(this.components);
			this.descriptionBindingSource = new System.Windows.Forms.BindingSource(this.components);
			this.codeDataGridViewTextBoxColumn = new System.Windows.Forms.DataGridViewComboBoxColumn();
			this.descriptionDataGridViewTextBoxColumn = new System.Windows.Forms.DataGridViewComboBoxColumn();
			this.quantityDataGridViewTextBoxColumn = new System.Windows.Forms.DataGridViewTextBoxColumn();
			this.externalPartDataGridViewTextBoxColumn = new System.Windows.Forms.DataGridViewTextBoxColumn();
			((System.ComponentModel.ISupportInitialize)(this._DataGrid)).BeginInit();
			this.contextMenuStrip1.SuspendLayout();
			((System.ComponentModel.ISupportInitialize)(this.codeBindingSource)).BeginInit();
			((System.ComponentModel.ISupportInitialize)(this.descriptionBindingSource)).BeginInit();
			this.SuspendLayout();
			// 
			// _DataGrid
			// 
			this._DataGrid.AllowUserToResizeRows = false;
			this._DataGrid.AutoGenerateColumns = false;
			this._DataGrid.BackgroundColor = System.Drawing.SystemColors.Window;
			this._DataGrid.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
			this._DataGrid.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
			this._DataGrid.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.codeDataGridViewTextBoxColumn,
            this.descriptionDataGridViewTextBoxColumn,
            this.quantityDataGridViewTextBoxColumn,
            this.externalPartDataGridViewTextBoxColumn});
			this._DataGrid.ContextMenuStrip = this.contextMenuStrip1;
			this._DataGrid.DataSource = this.codeBindingSource;
			this._DataGrid.Dock = System.Windows.Forms.DockStyle.Top;
			this._DataGrid.Location = new System.Drawing.Point(0, 0);
			this._DataGrid.Name = "_DataGrid";
			this._DataGrid.RowHeadersVisible = false;
			this._DataGrid.ScrollBars = System.Windows.Forms.ScrollBars.Vertical;
			this._DataGrid.Size = new System.Drawing.Size(528, 343);
			this._DataGrid.TabIndex = 0;
			this._DataGrid.EditingControlShowing += new System.Windows.Forms.DataGridViewEditingControlShowingEventHandler(this.dataGridView1_EditingControlShowing);
			// 
			// contextMenuStrip1
			// 
			this.contextMenuStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.addToolStripMenuItem,
            this.deleteToolStripMenuItem});
			this.contextMenuStrip1.Name = "contextMenuStrip1";
			this.contextMenuStrip1.Size = new System.Drawing.Size(108, 48);
			// 
			// addToolStripMenuItem
			// 
			this.addToolStripMenuItem.Name = "addToolStripMenuItem";
			this.addToolStripMenuItem.Size = new System.Drawing.Size(107, 22);
			this.addToolStripMenuItem.Text = "Add";
			this.addToolStripMenuItem.Click += new System.EventHandler(this.addToolStripMenuItem_Click);
			// 
			// deleteToolStripMenuItem
			// 
			this.deleteToolStripMenuItem.Name = "deleteToolStripMenuItem";
			this.deleteToolStripMenuItem.Size = new System.Drawing.Size(107, 22);
			this.deleteToolStripMenuItem.Text = "Delete";
			this.deleteToolStripMenuItem.Click += new System.EventHandler(this.deleteToolStripMenuItem_Click);
			// 
			// button2
			// 
			this.button2.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
			this.button2.DialogResult = System.Windows.Forms.DialogResult.OK;
			this.button2.Location = new System.Drawing.Point(360, 349);
			this.button2.Name = "button2";
			this.button2.Size = new System.Drawing.Size(75, 23);
			this.button2.TabIndex = 2;
			this.button2.Text = "OK";
			this.button2.UseVisualStyleBackColor = true;
			// 
			// button1
			// 
			this.button1.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
			this.button1.DialogResult = System.Windows.Forms.DialogResult.Cancel;
			this.button1.Location = new System.Drawing.Point(441, 349);
			this.button1.Name = "button1";
			this.button1.Size = new System.Drawing.Size(75, 23);
			this.button1.TabIndex = 3;
			this.button1.Text = "Cancel";
			this.button1.UseVisualStyleBackColor = true;
			// 
			// codeBindingSource
			// 
			this.codeBindingSource.DataSource = typeof(TNT.LSD.Inventory.Parts);
			// 
			// descriptionBindingSource
			// 
			this.descriptionBindingSource.DataSource = typeof(TNT.LSD.Inventory.Parts);
			// 
			// codeDataGridViewTextBoxColumn
			// 
			this.codeDataGridViewTextBoxColumn.DataPropertyName = "Code";
			this.codeDataGridViewTextBoxColumn.DataSource = this.codeBindingSource;
			this.codeDataGridViewTextBoxColumn.DisplayStyle = System.Windows.Forms.DataGridViewComboBoxDisplayStyle.Nothing;
			this.codeDataGridViewTextBoxColumn.HeaderText = "Code";
			this.codeDataGridViewTextBoxColumn.Name = "codeDataGridViewTextBoxColumn";
			this.codeDataGridViewTextBoxColumn.Resizable = System.Windows.Forms.DataGridViewTriState.True;
			this.codeDataGridViewTextBoxColumn.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.Automatic;
			this.codeDataGridViewTextBoxColumn.ValueMember = "Code";
			this.codeDataGridViewTextBoxColumn.Width = 150;
			// 
			// descriptionDataGridViewTextBoxColumn
			// 
			this.descriptionDataGridViewTextBoxColumn.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.Fill;
			this.descriptionDataGridViewTextBoxColumn.DataPropertyName = "Description";
			this.descriptionDataGridViewTextBoxColumn.DataSource = this.descriptionBindingSource;
			this.descriptionDataGridViewTextBoxColumn.DisplayStyle = System.Windows.Forms.DataGridViewComboBoxDisplayStyle.Nothing;
			this.descriptionDataGridViewTextBoxColumn.HeaderText = "Description";
			this.descriptionDataGridViewTextBoxColumn.Name = "descriptionDataGridViewTextBoxColumn";
			this.descriptionDataGridViewTextBoxColumn.Resizable = System.Windows.Forms.DataGridViewTriState.True;
			this.descriptionDataGridViewTextBoxColumn.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.Automatic;
			this.descriptionDataGridViewTextBoxColumn.ValueMember = "Description";
			// 
			// quantityDataGridViewTextBoxColumn
			// 
			this.quantityDataGridViewTextBoxColumn.DataPropertyName = "Quantity";
			this.quantityDataGridViewTextBoxColumn.HeaderText = "Quantity";
			this.quantityDataGridViewTextBoxColumn.Name = "quantityDataGridViewTextBoxColumn";
			this.quantityDataGridViewTextBoxColumn.Width = 75;
			// 
			// externalPartDataGridViewTextBoxColumn
			// 
			this.externalPartDataGridViewTextBoxColumn.DataPropertyName = "ExternalPart";
			this.externalPartDataGridViewTextBoxColumn.HeaderText = "ExternalPart";
			this.externalPartDataGridViewTextBoxColumn.Name = "externalPartDataGridViewTextBoxColumn";
			this.externalPartDataGridViewTextBoxColumn.Visible = false;
			// 
			// DGVPartsEditor
			// 
			this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
			this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
			this.ClientSize = new System.Drawing.Size(528, 384);
			this.Controls.Add(this.button1);
			this.Controls.Add(this.button2);
			this.Controls.Add(this._DataGrid);
			this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
			this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
			this.MaximizeBox = false;
			this.MinimizeBox = false;
			this.Name = "DGVPartsEditor";
			this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
			this.Text = "Parts";
			((System.ComponentModel.ISupportInitialize)(this._DataGrid)).EndInit();
			this.contextMenuStrip1.ResumeLayout(false);
			((System.ComponentModel.ISupportInitialize)(this.codeBindingSource)).EndInit();
			((System.ComponentModel.ISupportInitialize)(this.descriptionBindingSource)).EndInit();
			this.ResumeLayout(false);

		}

		#endregion

		private System.Windows.Forms.DataGridView _DataGrid;
		private System.Windows.Forms.Button button2;
		private System.Windows.Forms.BindingSource codeBindingSource;
		private System.Windows.Forms.ContextMenuStrip contextMenuStrip1;
		private System.Windows.Forms.ToolStripMenuItem addToolStripMenuItem;
		private System.Windows.Forms.ToolStripMenuItem deleteToolStripMenuItem;
		private System.Windows.Forms.Button button1;
		private System.Windows.Forms.BindingSource descriptionBindingSource;
		private System.Windows.Forms.DataGridViewComboBoxColumn codeDataGridViewTextBoxColumn;
		private System.Windows.Forms.DataGridViewComboBoxColumn descriptionDataGridViewTextBoxColumn;
		private System.Windows.Forms.DataGridViewTextBoxColumn quantityDataGridViewTextBoxColumn;
		private System.Windows.Forms.DataGridViewTextBoxColumn externalPartDataGridViewTextBoxColumn;
	}
}