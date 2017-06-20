namespace LandscapeSprinklerDesigner
{
	partial class LayoutSettingsForm
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
			System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(LayoutSettingsForm));
			this.SettingsEditor = new System.Windows.Forms.PropertyGrid();
			this.SuspendLayout();
			// 
			// SettingsEditor
			// 
			this.SettingsEditor.Dock = System.Windows.Forms.DockStyle.Fill;
			this.SettingsEditor.LineColor = System.Drawing.SystemColors.InactiveBorder;
			this.SettingsEditor.Location = new System.Drawing.Point(0, 0);
			this.SettingsEditor.Name = "SettingsEditor";
			this.SettingsEditor.PropertySort = System.Windows.Forms.PropertySort.Categorized;
			this.SettingsEditor.Size = new System.Drawing.Size(284, 262);
			this.SettingsEditor.TabIndex = 0;
			this.SettingsEditor.ToolbarVisible = false;
			this.SettingsEditor.PropertyValueChanged += new System.Windows.Forms.PropertyValueChangedEventHandler(this.SettingsEditor_PropertyValueChanged);
			// 
			// LayoutSettingsForm
			// 
			this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
			this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
			this.ClientSize = new System.Drawing.Size(284, 262);
			this.Controls.Add(this.SettingsEditor);
			this.DockAreas = ((WeifenLuo.WinFormsUI.Docking.DockAreas)(((WeifenLuo.WinFormsUI.Docking.DockAreas.Float | WeifenLuo.WinFormsUI.Docking.DockAreas.DockLeft) 
            | WeifenLuo.WinFormsUI.Docking.DockAreas.DockRight)));
			this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
			this.Name = "LayoutSettingsForm";
			this.Text = "Layout Settings";
			this.ResumeLayout(false);

		}

		#endregion

		private System.Windows.Forms.PropertyGrid SettingsEditor;
	}
}