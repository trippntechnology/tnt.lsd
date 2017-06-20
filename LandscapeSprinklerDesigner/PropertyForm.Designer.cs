namespace LandscapeSprinklerDesigner
{
	partial class PropertyForm
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
			System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(PropertyForm));
			this.PropertyEditor = new System.Windows.Forms.PropertyGrid();
			this.SuspendLayout();
			// 
			// PropertyEditor
			// 
			this.PropertyEditor.Dock = System.Windows.Forms.DockStyle.Fill;
			this.PropertyEditor.LineColor = System.Drawing.SystemColors.InactiveBorder;
			this.PropertyEditor.Location = new System.Drawing.Point(0, 0);
			this.PropertyEditor.Name = "PropertyEditor";
			this.PropertyEditor.PropertySort = System.Windows.Forms.PropertySort.Alphabetical;
			this.PropertyEditor.Size = new System.Drawing.Size(284, 262);
			this.PropertyEditor.TabIndex = 0;
			this.PropertyEditor.ToolbarVisible = false;
			this.PropertyEditor.PropertyValueChanged += new System.Windows.Forms.PropertyValueChangedEventHandler(this.PropertyEditor_PropertyValueChanged);
			// 
			// PropertyForm
			// 
			this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
			this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
			this.ClientSize = new System.Drawing.Size(284, 262);
			this.Controls.Add(this.PropertyEditor);
			this.DockAreas = ((WeifenLuo.WinFormsUI.Docking.DockAreas)(((WeifenLuo.WinFormsUI.Docking.DockAreas.Float | WeifenLuo.WinFormsUI.Docking.DockAreas.DockLeft) 
            | WeifenLuo.WinFormsUI.Docking.DockAreas.DockRight)));
			this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
			this.Name = "PropertyForm";
			this.Text = "Properties Editor";
			this.ResumeLayout(false);

		}

		#endregion

		private System.Windows.Forms.PropertyGrid PropertyEditor;
	}
}