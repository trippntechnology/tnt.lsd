namespace LandscapeSprinklerDesigner
{
	partial class LayoutForm
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
			LSDComponents.DrawingModes.SelectMode selectMode1 = new LSDComponents.DrawingModes.SelectMode();
			System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(LayoutForm));
			this.tntPanel1 = new LSDComponents.TNTPanel();
			this.CAD = new LSDComponents.TNTCAD();
			this.CADContextMenu = new System.Windows.Forms.ContextMenuStrip(this.components);
			this.snaptogrid = new System.Windows.Forms.ToolStripMenuItem();
			this.toolStripSeparator1 = new System.Windows.Forms.ToolStripSeparator();
			this.aligntogrid = new System.Windows.Forms.ToolStripMenuItem();
			this.space = new System.Windows.Forms.ToolStripMenuItem();
			this.rotateclock = new System.Windows.Forms.ToolStripMenuItem();
			this.rotatecounter = new System.Windows.Forms.ToolStripMenuItem();
			this.rotate180 = new System.Windows.Forms.ToolStripMenuItem();
			this.toolStripMenuItem2 = new System.Windows.Forms.ToolStripSeparator();
			this.area = new System.Windows.Forms.ToolStripMenuItem();
			this.calculateDistance = new System.Windows.Forms.ToolStripMenuItem();
			this.tntPanel1.SuspendLayout();
			this.CADContextMenu.SuspendLayout();
			this.SuspendLayout();
			// 
			// tntPanel1
			// 
			this.tntPanel1.AutoScroll = true;
			this.tntPanel1.Controls.Add(this.CAD);
			this.tntPanel1.Dock = System.Windows.Forms.DockStyle.Fill;
			this.tntPanel1.Location = new System.Drawing.Point(0, 0);
			this.tntPanel1.Name = "tntPanel1";
			this.tntPanel1.Size = new System.Drawing.Size(570, 359);
			this.tntPanel1.TabIndex = 3;
			this.tntPanel1.Scroll += new System.Windows.Forms.ScrollEventHandler(this.tntPanel1_Scroll);
			// 
			// CAD
			// 
			this.CAD.ContextMenuStrip = this.CADContextMenu;
			this.CAD.DrawingLayers = 2;
			this.CAD.DrawingMode = selectMode1;
			this.CAD.Location = new System.Drawing.Point(0, 0);
			this.CAD.Name = "CAD";
			this.CAD.Size = new System.Drawing.Size(1600, 1600);
			this.CAD.SnapToGrid = true;
			this.CAD.TabIndex = 4;
			this.CAD.Text = "tntcad1";
			this.CAD.OnObjectsSelected += new LSDComponents.ObjectsSelectedDelegate(this.CAD_OnObjectsSelected);
			this.CAD.TextChanged += new System.EventHandler(this.CAD_TextChanged);
			// 
			// CADContextMenu
			// 
			this.CADContextMenu.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.snaptogrid,
            this.toolStripSeparator1,
            this.aligntogrid,
            this.space,
            this.rotateclock,
            this.rotatecounter,
            this.rotate180,
            this.toolStripMenuItem2,
            this.area,
            this.calculateDistance});
			this.CADContextMenu.Name = "CADContextMenu";
			this.CADContextMenu.Size = new System.Drawing.Size(181, 214);
			this.CADContextMenu.Opening += new System.ComponentModel.CancelEventHandler(this.CADContextMenu_Opening);
			// 
			// snaptogrid
			// 
			this.snaptogrid.Name = "snaptogrid";
			this.snaptogrid.Size = new System.Drawing.Size(180, 22);
			this.snaptogrid.Text = "toolStripMenuItem1";
			// 
			// toolStripSeparator1
			// 
			this.toolStripSeparator1.Name = "toolStripSeparator1";
			this.toolStripSeparator1.Size = new System.Drawing.Size(177, 6);
			// 
			// aligntogrid
			// 
			this.aligntogrid.Name = "aligntogrid";
			this.aligntogrid.Size = new System.Drawing.Size(180, 22);
			this.aligntogrid.Text = "item1";
			// 
			// space
			// 
			this.space.Name = "space";
			this.space.Size = new System.Drawing.Size(180, 22);
			this.space.Text = "item2";
			// 
			// rotateclock
			// 
			this.rotateclock.Name = "rotateclock";
			this.rotateclock.Size = new System.Drawing.Size(180, 22);
			this.rotateclock.Text = "item3";
			// 
			// rotatecounter
			// 
			this.rotatecounter.Name = "rotatecounter";
			this.rotatecounter.Size = new System.Drawing.Size(180, 22);
			this.rotatecounter.Text = "item4";
			// 
			// rotate180
			// 
			this.rotate180.Name = "rotate180";
			this.rotate180.Size = new System.Drawing.Size(180, 22);
			this.rotate180.Text = "item5";
			// 
			// toolStripMenuItem2
			// 
			this.toolStripMenuItem2.Name = "toolStripMenuItem2";
			this.toolStripMenuItem2.Size = new System.Drawing.Size(177, 6);
			// 
			// area
			// 
			this.area.Name = "area";
			this.area.Size = new System.Drawing.Size(180, 22);
			this.area.Text = "item6";
			// 
			// calculateDistance
			// 
			this.calculateDistance.Name = "calculateDistance";
			this.calculateDistance.Size = new System.Drawing.Size(180, 22);
			this.calculateDistance.Text = "toolStripMenuItem1";
			// 
			// LayoutForm
			// 
			this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
			this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
			this.AutoScroll = true;
			this.ClientSize = new System.Drawing.Size(570, 359);
			this.CloseButton = false;
			this.CloseButtonVisible = false;
			this.Controls.Add(this.tntPanel1);
			this.DockAreas = WeifenLuo.WinFormsUI.Docking.DockAreas.Document;
			this.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
			this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
			this.KeyPreview = true;
			this.MaximizeBox = false;
			this.MinimizeBox = false;
			this.Name = "LayoutForm";
			this.Text = "Layout";
			this.KeyDown += new System.Windows.Forms.KeyEventHandler(this.LayoutForm_KeyDown);
			this.KeyUp += new System.Windows.Forms.KeyEventHandler(this.LayoutForm_KeyUp);
			this.tntPanel1.ResumeLayout(false);
			this.CADContextMenu.ResumeLayout(false);
			this.ResumeLayout(false);

		}

		#endregion

		private LSDComponents.TNTPanel tntPanel1;
		public LSDComponents.TNTCAD CAD;
		public System.Windows.Forms.ToolStripMenuItem aligntogrid;
		public System.Windows.Forms.ToolStripMenuItem space;
		public System.Windows.Forms.ContextMenuStrip CADContextMenu;
		public System.Windows.Forms.ToolStripMenuItem rotateclock;
		public System.Windows.Forms.ToolStripMenuItem rotatecounter;
		public System.Windows.Forms.ToolStripMenuItem rotate180;
		public System.Windows.Forms.ToolStripSeparator toolStripMenuItem2;
		public System.Windows.Forms.ToolStripMenuItem area;
		public System.Windows.Forms.ToolStripMenuItem snaptogrid;
		private System.Windows.Forms.ToolStripSeparator toolStripSeparator1;
		public System.Windows.Forms.ToolStripMenuItem calculateDistance;
	}
}