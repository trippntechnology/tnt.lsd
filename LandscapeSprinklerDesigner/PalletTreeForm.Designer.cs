namespace LandscapeSprinklerDesigner
{
	partial class PalletTreeForm
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
			System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(PalletTreeForm));
			this.NodeImages = new System.Windows.Forms.ImageList(this.components);
			this.NodeStateImages = new System.Windows.Forms.ImageList(this.components);
			this.PaletteTreeView = new LSDComponents.PalletNodeTreeView(this.components);
			this.TreeViewContextMenu = new System.Windows.Forms.ContextMenuStrip(this.components);
			this.ExpandAllMenu = new System.Windows.Forms.ToolStripMenuItem();
			this.collapseAllToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
			this.toolStripSeparator1 = new System.Windows.Forms.ToolStripSeparator();
			this.ExpandMenu = new System.Windows.Forms.ToolStripMenuItem();
			this.CollapseMenu = new System.Windows.Forms.ToolStripMenuItem();
			this.splitter1 = new System.Windows.Forms.Splitter();
			this.Description = new System.Windows.Forms.Label();
			this.TreeViewContextMenu.SuspendLayout();
			this.SuspendLayout();
			// 
			// NodeImages
			// 
			this.NodeImages.ImageStream = ((System.Windows.Forms.ImageListStreamer)(resources.GetObject("NodeImages.ImageStream")));
			this.NodeImages.TransparentColor = System.Drawing.Color.Transparent;
			this.NodeImages.Images.SetKeyName(0, "Blank.png");
			// 
			// NodeStateImages
			// 
			this.NodeStateImages.ImageStream = ((System.Windows.Forms.ImageListStreamer)(resources.GetObject("NodeStateImages.ImageStream")));
			this.NodeStateImages.TransparentColor = System.Drawing.Color.Transparent;
			this.NodeStateImages.Images.SetKeyName(0, "Blank.png");
			this.NodeStateImages.Images.SetKeyName(1, "Colapsed.png");
			this.NodeStateImages.Images.SetKeyName(2, "Expanded.png");
			// 
			// PaletteTreeView
			// 
			this.PaletteTreeView.ContextMenuStrip = this.TreeViewContextMenu;
			this.PaletteTreeView.Dock = System.Windows.Forms.DockStyle.Fill;
			this.PaletteTreeView.HideSelection = false;
			this.PaletteTreeView.ImageIndex = 0;
			this.PaletteTreeView.ImageList = this.NodeImages;
			this.PaletteTreeView.Location = new System.Drawing.Point(0, 0);
			this.PaletteTreeView.Name = "PaletteTreeView";
			this.PaletteTreeView.SelectedImageIndex = 0;
			this.PaletteTreeView.ShowLines = false;
			this.PaletteTreeView.ShowPlusMinus = false;
			this.PaletteTreeView.ShowRootLines = false;
			this.PaletteTreeView.Size = new System.Drawing.Size(284, 203);
			this.PaletteTreeView.StateImageList = this.NodeStateImages;
			this.PaletteTreeView.TabIndex = 0;
			this.PaletteTreeView.BeforeCollapse += new System.Windows.Forms.TreeViewCancelEventHandler(this.PalletTreeView_BeforeCollapse);
			this.PaletteTreeView.BeforeExpand += new System.Windows.Forms.TreeViewCancelEventHandler(this.PalletTreeView_BeforeExpand);
			this.PaletteTreeView.AfterSelect += new System.Windows.Forms.TreeViewEventHandler(this.PalletTreeView_AfterSelect);
			this.PaletteTreeView.NodeMouseClick += new System.Windows.Forms.TreeNodeMouseClickEventHandler(this.PalletTreeView_NodeMouseClick);
			this.PaletteTreeView.MouseDown += new System.Windows.Forms.MouseEventHandler(this.PaletteTreeView_MouseDown);
			// 
			// TreeViewContextMenu
			// 
			this.TreeViewContextMenu.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.ExpandAllMenu,
            this.collapseAllToolStripMenuItem,
            this.toolStripSeparator1,
            this.ExpandMenu,
            this.CollapseMenu});
			this.TreeViewContextMenu.Name = "TreeViewContextMenu";
			this.TreeViewContextMenu.Size = new System.Drawing.Size(137, 98);
			this.TreeViewContextMenu.Opening += new System.ComponentModel.CancelEventHandler(this.TreeViewContextMenu_Opening);
			// 
			// ExpandAllMenu
			// 
			this.ExpandAllMenu.Name = "ExpandAllMenu";
			this.ExpandAllMenu.Size = new System.Drawing.Size(152, 22);
			this.ExpandAllMenu.Text = "Expand All";
			this.ExpandAllMenu.Click += new System.EventHandler(this.ExpandAllMenu_Click);
			// 
			// collapseAllToolStripMenuItem
			// 
			this.collapseAllToolStripMenuItem.Name = "collapseAllToolStripMenuItem";
			this.collapseAllToolStripMenuItem.Size = new System.Drawing.Size(152, 22);
			this.collapseAllToolStripMenuItem.Text = "Collapse All";
			this.collapseAllToolStripMenuItem.Click += new System.EventHandler(this.CollapseAllMenu_Click);
			// 
			// toolStripSeparator1
			// 
			this.toolStripSeparator1.Name = "toolStripSeparator1";
			this.toolStripSeparator1.Size = new System.Drawing.Size(149, 6);
			// 
			// ExpandMenu
			// 
			this.ExpandMenu.Name = "ExpandMenu";
			this.ExpandMenu.Size = new System.Drawing.Size(152, 22);
			this.ExpandMenu.Text = "Expand";
			this.ExpandMenu.Click += new System.EventHandler(this.ExpandMenu_Click);
			// 
			// CollapseMenu
			// 
			this.CollapseMenu.Name = "CollapseMenu";
			this.CollapseMenu.Size = new System.Drawing.Size(136, 22);
			this.CollapseMenu.Text = "Collapse";
			this.CollapseMenu.Click += new System.EventHandler(this.CollapseMenu_Click);
			// 
			// splitter1
			// 
			this.splitter1.Dock = System.Windows.Forms.DockStyle.Bottom;
			this.splitter1.Location = new System.Drawing.Point(0, 200);
			this.splitter1.Name = "splitter1";
			this.splitter1.Size = new System.Drawing.Size(284, 3);
			this.splitter1.TabIndex = 1;
			this.splitter1.TabStop = false;
			this.splitter1.SplitterMoved += new System.Windows.Forms.SplitterEventHandler(this.splitter1_SplitterMoved);
			// 
			// Description
			// 
			this.Description.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
			this.Description.Dock = System.Windows.Forms.DockStyle.Bottom;
			this.Description.Location = new System.Drawing.Point(0, 203);
			this.Description.Name = "Description";
			this.Description.Size = new System.Drawing.Size(284, 59);
			this.Description.TabIndex = 3;
			// 
			// PalletTreeForm
			// 
			this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
			this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
			this.ClientSize = new System.Drawing.Size(284, 262);
			this.Controls.Add(this.splitter1);
			this.Controls.Add(this.PaletteTreeView);
			this.Controls.Add(this.Description);
			this.DockAreas = ((WeifenLuo.WinFormsUI.Docking.DockAreas)(((WeifenLuo.WinFormsUI.Docking.DockAreas.Float | WeifenLuo.WinFormsUI.Docking.DockAreas.DockLeft) 
            | WeifenLuo.WinFormsUI.Docking.DockAreas.DockRight)));
			this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
			this.Name = "PalletTreeForm";
			this.Text = "Parts Palette";
			this.Load += new System.EventHandler(this.PalletTreeForm_Load);
			this.TreeViewContextMenu.ResumeLayout(false);
			this.ResumeLayout(false);

		}

		#endregion

		private LSDComponents.PalletNodeTreeView PaletteTreeView;
		private System.Windows.Forms.ImageList NodeImages;
		private System.Windows.Forms.ImageList NodeStateImages;
		private System.Windows.Forms.Splitter splitter1;
		private System.Windows.Forms.Label Description;
		private System.Windows.Forms.ContextMenuStrip TreeViewContextMenu;
		private System.Windows.Forms.ToolStripMenuItem ExpandAllMenu;
		private System.Windows.Forms.ToolStripMenuItem collapseAllToolStripMenuItem;
		private System.Windows.Forms.ToolStripSeparator toolStripSeparator1;
		private System.Windows.Forms.ToolStripMenuItem ExpandMenu;
		private System.Windows.Forms.ToolStripMenuItem CollapseMenu;
	}
}