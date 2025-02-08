namespace TNT.LSD.PartsEditor;

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
    this.ContextMenuListView = new System.Windows.Forms.ContextMenuStrip(this.components);
    this.deleteToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
    this.button1 = new System.Windows.Forms.Button();
    this.button2 = new System.Windows.Forms.Button();
    this.dgvPartsList = new System.Windows.Forms.DataGridView();
    this.inventoryBindingSource = new System.Windows.Forms.BindingSource(this.components);
    this.partsBindingSource = new System.Windows.Forms.BindingSource(this.components);
    this.dataGridViewTextBoxColumn1 = new System.Windows.Forms.DataGridViewComboBoxColumn();
    this.dataGridViewTextBoxColumn2 = new System.Windows.Forms.DataGridViewComboBoxColumn();
    this.dataGridViewTextBoxColumn3 = new System.Windows.Forms.DataGridViewTextBoxColumn();
    this.ContextMenuListView.SuspendLayout();
    ((System.ComponentModel.ISupportInitialize)(this.dgvPartsList)).BeginInit();
    ((System.ComponentModel.ISupportInitialize)(this.inventoryBindingSource)).BeginInit();
    ((System.ComponentModel.ISupportInitialize)(this.partsBindingSource)).BeginInit();
    this.SuspendLayout();
    // 
    // ContextMenuListView
    // 
    this.ContextMenuListView.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
          this.deleteToolStripMenuItem});
    this.ContextMenuListView.Name = "contextMenuStrip1";
    this.ContextMenuListView.Size = new System.Drawing.Size(108, 26);
    // 
    // deleteToolStripMenuItem
    // 
    this.deleteToolStripMenuItem.Image = ((System.Drawing.Image)(resources.GetObject("deleteToolStripMenuItem.Image")));
    this.deleteToolStripMenuItem.Name = "deleteToolStripMenuItem";
    this.deleteToolStripMenuItem.Size = new System.Drawing.Size(107, 22);
    this.deleteToolStripMenuItem.Text = "Delete";
    this.deleteToolStripMenuItem.Click += new System.EventHandler(this.deleteToolStripMenuItem_Click);
    // 
    // button1
    // 
    this.button1.DialogResult = System.Windows.Forms.DialogResult.OK;
    this.button1.Location = new System.Drawing.Point(348, 496);
    this.button1.Name = "button1";
    this.button1.Size = new System.Drawing.Size(75, 23);
    this.button1.TabIndex = 4;
    this.button1.Text = "OK";
    this.button1.UseVisualStyleBackColor = true;
    // 
    // button2
    // 
    this.button2.DialogResult = System.Windows.Forms.DialogResult.Cancel;
    this.button2.Location = new System.Drawing.Point(429, 496);
    this.button2.Name = "button2";
    this.button2.Size = new System.Drawing.Size(75, 23);
    this.button2.TabIndex = 5;
    this.button2.Text = "Cancel";
    this.button2.UseVisualStyleBackColor = true;
    // 
    // dgvPartsList
    // 
    this.dgvPartsList.AutoGenerateColumns = false;
    this.dgvPartsList.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
    this.dgvPartsList.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
          this.dataGridViewTextBoxColumn1,
          this.dataGridViewTextBoxColumn2,
          this.dataGridViewTextBoxColumn3});
    this.dgvPartsList.ContextMenuStrip = this.ContextMenuListView;
    this.dgvPartsList.DataSource = this.partsBindingSource;
    this.dgvPartsList.Dock = System.Windows.Forms.DockStyle.Top;
    this.dgvPartsList.EditMode = System.Windows.Forms.DataGridViewEditMode.EditOnEnter;
    this.dgvPartsList.Location = new System.Drawing.Point(0, 0);
    this.dgvPartsList.MultiSelect = false;
    this.dgvPartsList.Name = "dgvPartsList";
    this.dgvPartsList.RowHeadersVisible = false;
    this.dgvPartsList.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
    this.dgvPartsList.Size = new System.Drawing.Size(514, 490);
    this.dgvPartsList.TabIndex = 6;
    this.dgvPartsList.EditingControlShowing += new System.Windows.Forms.DataGridViewEditingControlShowingEventHandler(this.dgvPartsList_EditingControlShowing);
    // 
    // inventoryBindingSource
    // 
    this.inventoryBindingSource.AllowNew = true;
    this.inventoryBindingSource.DataSource = typeof(TNT.LSD.PartsEditor.Inventory);
    // 
    // partsBindingSource
    // 
    this.partsBindingSource.AllowNew = true;
    this.partsBindingSource.DataSource = typeof(TNT.LSD.PartsEditor.Parts);
    // 
    // dataGridViewTextBoxColumn1
    // 
    this.dataGridViewTextBoxColumn1.DataPropertyName = "Code";
    this.dataGridViewTextBoxColumn1.DataSource = this.inventoryBindingSource;
    this.dataGridViewTextBoxColumn1.DisplayStyle = System.Windows.Forms.DataGridViewComboBoxDisplayStyle.Nothing;
    this.dataGridViewTextBoxColumn1.HeaderText = "Code";
    this.dataGridViewTextBoxColumn1.Name = "dataGridViewTextBoxColumn1";
    this.dataGridViewTextBoxColumn1.Resizable = System.Windows.Forms.DataGridViewTriState.True;
    this.dataGridViewTextBoxColumn1.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.Automatic;
    this.dataGridViewTextBoxColumn1.ValueMember = "Code";
    this.dataGridViewTextBoxColumn1.Width = 120;
    // 
    // dataGridViewTextBoxColumn2
    // 
    this.dataGridViewTextBoxColumn2.DataPropertyName = "Description";
    this.dataGridViewTextBoxColumn2.DataSource = this.inventoryBindingSource;
    this.dataGridViewTextBoxColumn2.DisplayStyle = System.Windows.Forms.DataGridViewComboBoxDisplayStyle.Nothing;
    this.dataGridViewTextBoxColumn2.HeaderText = "Description";
    this.dataGridViewTextBoxColumn2.Name = "dataGridViewTextBoxColumn2";
    this.dataGridViewTextBoxColumn2.Resizable = System.Windows.Forms.DataGridViewTriState.True;
    this.dataGridViewTextBoxColumn2.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.Automatic;
    this.dataGridViewTextBoxColumn2.ValueMember = "Description";
    this.dataGridViewTextBoxColumn2.Width = 300;
    // 
    // dataGridViewTextBoxColumn3
    // 
    this.dataGridViewTextBoxColumn3.DataPropertyName = "Quantity";
    this.dataGridViewTextBoxColumn3.HeaderText = "Quantity";
    this.dataGridViewTextBoxColumn3.Name = "dataGridViewTextBoxColumn3";
    this.dataGridViewTextBoxColumn3.Width = 75;
    // 
    // PartsEditor
    // 
    this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
    this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
    this.ClientSize = new System.Drawing.Size(514, 531);
    this.Controls.Add(this.dgvPartsList);
    this.Controls.Add(this.button2);
    this.Controls.Add(this.button1);
    this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
    this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
    this.KeyPreview = true;
    this.MaximizeBox = false;
    this.MinimizeBox = false;
    this.Name = "PartsEditor";
    this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
    this.Text = "Parts Editor";
    this.ContextMenuListView.ResumeLayout(false);
    ((System.ComponentModel.ISupportInitialize)(this.dgvPartsList)).EndInit();
    ((System.ComponentModel.ISupportInitialize)(this.inventoryBindingSource)).EndInit();
    ((System.ComponentModel.ISupportInitialize)(this.partsBindingSource)).EndInit();
    this.ResumeLayout(false);

  }

  #endregion
  private System.Windows.Forms.Button button1;
  private System.Windows.Forms.Button button2;
  private System.Windows.Forms.ContextMenuStrip ContextMenuListView;
  private System.Windows.Forms.BindingSource inventoryBindingSource;
  public System.Windows.Forms.DataGridView dgvPartsList;
  private System.Windows.Forms.DataGridViewComboBoxColumn codeDataGridViewTextBoxColumn;
  private System.Windows.Forms.DataGridViewComboBoxColumn descriptionDataGridViewTextBoxColumn;
  private System.Windows.Forms.DataGridViewTextBoxColumn quantityDataGridViewTextBoxColumn;
  private System.Windows.Forms.BindingSource partsBindingSource;
  private System.Windows.Forms.ToolStripMenuItem deleteToolStripMenuItem;
  private System.Windows.Forms.DataGridViewComboBoxColumn dataGridViewTextBoxColumn1;
  private System.Windows.Forms.DataGridViewComboBoxColumn dataGridViewTextBoxColumn2;
  private System.Windows.Forms.DataGridViewTextBoxColumn dataGridViewTextBoxColumn3;
}