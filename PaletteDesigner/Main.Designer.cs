namespace PalletDesigner
{
	partial class Main
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
      components = new System.ComponentModel.Container();
      System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(Main));
			this.NodeImages = new System.Windows.Forms.ImageList(this.components);
			this.menuStrip1 = new System.Windows.Forms.MenuStrip();
			this.fileToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
			this.OpenMenu = new System.Windows.Forms.ToolStripMenuItem();
			this.SaveMenu = new System.Windows.Forms.ToolStripMenuItem();
			this.SaveAsMenu = new System.Windows.Forms.ToolStripMenuItem();
			this.toolStripSeparator1 = new System.Windows.Forms.ToolStripSeparator();
			this.ExitMenu = new System.Windows.Forms.ToolStripMenuItem();
			this.editToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
			this.menuNode = new System.Windows.Forms.ToolStripMenuItem();
			this.PalletContextMenu = new System.Windows.Forms.ContextMenuStrip(this.components);
			this.AddSiblingNodeContextMenu = new System.Windows.Forms.ToolStripMenuItem();
			this.AddChildNodeContextMenu = new System.Windows.Forms.ToolStripMenuItem();
			this.DeleteNodeContextMenu = new System.Windows.Forms.ToolStripMenuItem();
			this.toolStripMenuItem1 = new System.Windows.Forms.ToolStripSeparator();
			this.ShiftNodeUpContextMenu = new System.Windows.Forms.ToolStripMenuItem();
			this.ShiftNodeDownContextMenu = new System.Windows.Forms.ToolStripMenuItem();
			this.DemoteNodeContextMenu = new System.Windows.Forms.ToolStripMenuItem();
			this.toolStripSeparator2 = new System.Windows.Forms.ToolStripSeparator();
			this.AssignImageContextMenu = new System.Windows.Forms.ToolStripMenuItem();
			this.toolStripMenuItem2 = new System.Windows.Forms.ToolStripSeparator();
			this.CopyContextMenu = new System.Windows.Forms.ToolStripMenuItem();
			this.PasteContextMenu = new System.Windows.Forms.ToolStripMenuItem();
			this.toolStripSeparator3 = new System.Windows.Forms.ToolStripSeparator();
			this.toolStripMenuItem3 = new System.Windows.Forms.ToolStripMenuItem();
			this.toolStripSeparator7 = new System.Windows.Forms.ToolStripSeparator();
			this.CopyMenu = new System.Windows.Forms.ToolStripMenuItem();
			this.PasteMenu = new System.Windows.Forms.ToolStripMenuItem();
			this.PropertyEditor = new System.Windows.Forms.PropertyGrid();
			this.splitter1 = new System.Windows.Forms.Splitter();
			this.SaveDialog = new System.Windows.Forms.SaveFileDialog();
			this.OpenDialog = new System.Windows.Forms.OpenFileDialog();
			this.AddImageDialog = new System.Windows.Forms.OpenFileDialog();
			this.NodeStateImages = new System.Windows.Forms.ImageList(this.components);
			this.toolStrip1 = new System.Windows.Forms.ToolStrip();
			this.OpenButton = new System.Windows.Forms.ToolStripSplitButton();
			this.SaveButton = new System.Windows.Forms.ToolStripButton();
			this.toolStripSeparator4 = new System.Windows.Forms.ToolStripSeparator();
			this.AddSiblingNodeButton = new System.Windows.Forms.ToolStripButton();
			this.AddChildNodeButton = new System.Windows.Forms.ToolStripButton();
			this.DeleteNodeButton = new System.Windows.Forms.ToolStripButton();
			this.toolStripSeparator5 = new System.Windows.Forms.ToolStripSeparator();
			this.ShiftNodeUpButton = new System.Windows.Forms.ToolStripButton();
			this.ShiftNodeDownButton = new System.Windows.Forms.ToolStripButton();
			this.DemoteNodeButton = new System.Windows.Forms.ToolStripButton();
			this.toolStripSeparator6 = new System.Windows.Forms.ToolStripSeparator();
			this.AssignImageButton = new System.Windows.Forms.ToolStripButton();
			this.CopyButton = new System.Windows.Forms.ToolStripButton();
			this.PasteButton = new System.Windows.Forms.ToolStripButton();
			this.Pallet = new TNT.LSD.Components.PalletNodeTreeView(this.components);
			this.statusStrip1 = new System.Windows.Forms.StatusStrip();
			this.StatusToolTip = new System.Windows.Forms.ToolStripStatusLabel();
			this.exportImageToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
			this.menuStrip1.SuspendLayout();
			this.PalletContextMenu.SuspendLayout();
			this.toolStrip1.SuspendLayout();
			this.statusStrip1.SuspendLayout();
			this.SuspendLayout();
      // 
      // NodeImages
      // 
      NodeImages.ColorDepth = ColorDepth.Depth32Bit;
      NodeImages.ImageStream = (ImageListStreamer)resources.GetObject("NodeImages.ImageStream");
      NodeImages.TransparentColor = Color.Transparent;
      NodeImages.Images.SetKeyName(0, "Blank.png");
      // 
      // menuStrip1
      // 
      menuStrip1.Items.AddRange(new ToolStripItem[] { fileToolStripMenuItem, editToolStripMenuItem });
      menuStrip1.Location = new Point(0, 0);
      menuStrip1.Name = "menuStrip1";
      menuStrip1.Padding = new Padding(7, 2, 0, 2);
      menuStrip1.Size = new Size(877, 24);
      menuStrip1.TabIndex = 1;
      menuStrip1.Text = "menuStrip1";
      // 
      // fileToolStripMenuItem
      // 
      fileToolStripMenuItem.DropDownItems.AddRange(new ToolStripItem[] { OpenMenu, SaveMenu, SaveAsMenu, toolStripSeparator1, ExitMenu });
      fileToolStripMenuItem.Name = "fileToolStripMenuItem";
      fileToolStripMenuItem.Size = new Size(37, 20);
      fileToolStripMenuItem.Text = "&File";
      // 
      // OpenMenu
      // 
      OpenMenu.Name = "OpenMenu";
      OpenMenu.Size = new Size(114, 22);
      OpenMenu.Text = "&Open";
      // 
      // SaveMenu
      // 
      SaveMenu.Name = "SaveMenu";
      SaveMenu.Size = new Size(114, 22);
      SaveMenu.Text = "&Save";
      // 
      // SaveAsMenu
      // 
      SaveAsMenu.Name = "SaveAsMenu";
      SaveAsMenu.Size = new Size(114, 22);
      SaveAsMenu.Text = "Save &As";
      // 
      // toolStripSeparator1
      // 
      toolStripSeparator1.Name = "toolStripSeparator1";
      toolStripSeparator1.Size = new Size(111, 6);
      // 
      // ExitMenu
      // 
      ExitMenu.Name = "ExitMenu";
      ExitMenu.Size = new Size(114, 22);
      ExitMenu.Text = "E&xit";
      ExitMenu.ToolTipText = "Closes the application";
      // 
      // editToolStripMenuItem
      // 
      editToolStripMenuItem.DropDownItems.AddRange(new ToolStripItem[] { menuNode, toolStripSeparator7, CopyMenu, PasteMenu });
      editToolStripMenuItem.Name = "editToolStripMenuItem";
      editToolStripMenuItem.Size = new Size(39, 20);
      editToolStripMenuItem.Text = "&Edit";
      // 
      // menuNode
      // 
      menuNode.DropDown = PalletContextMenu;
      menuNode.Name = "menuNode";
      menuNode.Size = new Size(180, 22);
      menuNode.Text = "&Node";
      // 
      // PalletContextMenu
      // 
      PalletContextMenu.Items.AddRange(new ToolStripItem[] { AddSiblingNodeContextMenu, AddChildNodeContextMenu, DeleteNodeContextMenu, toolStripMenuItem1, ShiftNodeUpContextMenu, ShiftNodeDownContextMenu, DemoteNodeContextMenu, toolStripSeparator2, AssignImageContextMenu, exportImageToolStripMenuItem, toolStripMenuItem2, CopyContextMenu, PasteContextMenu, toolStripSeparator3, toolStripMenuItem3 });
      PalletContextMenu.Name = "PalletContextMenu";
      PalletContextMenu.Size = new Size(168, 270);
      // 
      // AddSiblingNodeContextMenu
      // 
      AddSiblingNodeContextMenu.Image = (Image)resources.GetObject("AddSiblingNodeContextMenu.Image");
      AddSiblingNodeContextMenu.Name = "AddSiblingNodeContextMenu";
      AddSiblingNodeContextMenu.Size = new Size(180, 22);
      AddSiblingNodeContextMenu.Text = "Add Sibling Node";
      AddSiblingNodeContextMenu.ToolTipText = "Add a sibling node";
      // 
      // AddChildNodeContextMenu
      // 
      AddChildNodeContextMenu.Image = PaletteDesigner.Resource.child_node_add;
      AddChildNodeContextMenu.Name = "AddChildNodeContextMenu";
      AddChildNodeContextMenu.Size = new Size(180, 22);
      AddChildNodeContextMenu.Text = "Add Child Node";
      AddChildNodeContextMenu.ToolTipText = "Add child node";
      // 
      // DeleteNodeContextMenu
      // 
      DeleteNodeContextMenu.Image = PaletteDesigner.Resource.delete;
      DeleteNodeContextMenu.Name = "DeleteNodeContextMenu";
      DeleteNodeContextMenu.Size = new Size(180, 22);
      DeleteNodeContextMenu.Text = "Delete Node";
      DeleteNodeContextMenu.ToolTipText = "Deletes the selected node and all children";
      // 
      // toolStripMenuItem1
      // 
      toolStripMenuItem1.Name = "toolStripMenuItem1";
      toolStripMenuItem1.Size = new Size(177, 6);
      // 
      // ShiftNodeUpContextMenu
      // 
      ShiftNodeUpContextMenu.Image = PaletteDesigner.Resource.node_move_up;
      ShiftNodeUpContextMenu.Name = "ShiftNodeUpContextMenu";
      ShiftNodeUpContextMenu.Size = new Size(180, 22);
      ShiftNodeUpContextMenu.Text = "Shift Node Up";
      ShiftNodeUpContextMenu.ToolTipText = "Shifts node before previous sibling";
      // 
      // ShiftNodeDownContextMenu
      // 
      ShiftNodeDownContextMenu.Image = PaletteDesigner.Resource.node_move_down;
      ShiftNodeDownContextMenu.Name = "ShiftNodeDownContextMenu";
      ShiftNodeDownContextMenu.Size = new Size(180, 22);
      ShiftNodeDownContextMenu.Text = "Shift Node Down";
      ShiftNodeDownContextMenu.ToolTipText = "Shifts node down after next sibling";
      // 
      // DemoteNodeContextMenu
      // 
      DemoteNodeContextMenu.Image = PaletteDesigner.Resource.node_demote;
      DemoteNodeContextMenu.Name = "DemoteNodeContextMenu";
      DemoteNodeContextMenu.Size = new Size(180, 22);
      DemoteNodeContextMenu.Text = "Demote Node";
      DemoteNodeContextMenu.ToolTipText = "Demotes selected node as a sibling to its parent";
      // 
      // toolStripSeparator2
      // 
      toolStripSeparator2.Name = "toolStripSeparator2";
      toolStripSeparator2.Size = new Size(177, 6);
      // 
      // AssignImageContextMenu
      // 
      AssignImageContextMenu.Image = PaletteDesigner.Resource.picture_add;
      AssignImageContextMenu.Name = "AssignImageContextMenu";
      AssignImageContextMenu.Size = new Size(180, 22);
      AssignImageContextMenu.Text = "Assign Image";
      AssignImageContextMenu.ToolTipText = "Assigns an image to the node";
      // 
      // exportImageToolStripMenuItem
      // 
      exportImageToolStripMenuItem.Name = "exportImageToolStripMenuItem";
      exportImageToolStripMenuItem.Size = new Size(180, 22);
      exportImageToolStripMenuItem.Text = "Export Image";
      // 
      // toolStripMenuItem2
      // 
      toolStripMenuItem2.Name = "toolStripMenuItem2";
      toolStripMenuItem2.Size = new Size(177, 6);
      // 
      // CopyContextMenu
      // 
      CopyContextMenu.Image = (Image)resources.GetObject("CopyContextMenu.Image");
      CopyContextMenu.Name = "CopyContextMenu";
      CopyContextMenu.Size = new Size(180, 22);
      CopyContextMenu.Text = "Copy";
      CopyContextMenu.ToolTipText = "Copies the selected node and its children";
      // 
      // PasteContextMenu
      // 
      PasteContextMenu.Image = (Image)resources.GetObject("PasteContextMenu.Image");
      PasteContextMenu.Name = "PasteContextMenu";
      PasteContextMenu.Size = new Size(180, 22);
      PasteContextMenu.Text = "Paste";
      PasteContextMenu.ToolTipText = "Pastes the prevously copied node and its children";
      // 
      // toolStripSeparator3
      // 
      toolStripSeparator3.Name = "toolStripSeparator3";
      toolStripSeparator3.Size = new Size(177, 6);
      // 
      // toolStripMenuItem3
      // 
      toolStripMenuItem3.Name = "toolStripMenuItem3";
      toolStripMenuItem3.Size = new Size(180, 22);
      toolStripMenuItem3.Text = "Expand All";
      toolStripMenuItem3.Click += ExpandAll_Click;
      // 
      // toolStripSeparator7
      // 
      toolStripSeparator7.Name = "toolStripSeparator7";
      toolStripSeparator7.Size = new Size(177, 6);
      // 
      // CopyMenu
      // 
      CopyMenu.Name = "CopyMenu";
      CopyMenu.Size = new Size(180, 22);
      CopyMenu.Text = "Copy";
      // 
      // PasteMenu
      // 
      PasteMenu.Name = "PasteMenu";
      PasteMenu.Size = new Size(180, 22);
      PasteMenu.Text = "Paste";
      // 
      // PropertyEditor
      // 
      PropertyEditor.Dock = DockStyle.Fill;
      PropertyEditor.LineColor = SystemColors.ControlDark;
      PropertyEditor.Location = new Point(237, 49);
      PropertyEditor.Margin = new Padding(4, 3, 4, 3);
      PropertyEditor.Name = "PropertyEditor";
      PropertyEditor.PropertySort = PropertySort.NoSort;
      PropertyEditor.Size = new Size(640, 567);
      PropertyEditor.TabIndex = 2;
      PropertyEditor.ToolbarVisible = false;
      // 
      // splitter1
      // 
      splitter1.Location = new Point(233, 49);
      splitter1.Margin = new Padding(4, 3, 4, 3);
      splitter1.Name = "splitter1";
      splitter1.Size = new Size(4, 567);
      splitter1.TabIndex = 3;
      splitter1.TabStop = false;
      // 
      // SaveDialog
      // 
      SaveDialog.DefaultExt = "pallet";
      SaveDialog.Filter = "LSD Palette|*.palette";
      SaveDialog.RestoreDirectory = true;
      SaveDialog.Title = "Save LSD Palette";
      // 
      // OpenDialog
      // 
      OpenDialog.DefaultExt = "palette";
      OpenDialog.Filter = "LSD Palette|*.palette";
      OpenDialog.RestoreDirectory = true;
      OpenDialog.Title = "Open LSD Palette";
      // 
      // AddImageDialog
      // 
      AddImageDialog.Title = "Open Image";
      // 
      // NodeStateImages
      // 
      NodeStateImages.ColorDepth = ColorDepth.Depth32Bit;
      NodeStateImages.ImageStream = (ImageListStreamer)resources.GetObject("NodeStateImages.ImageStream");
      NodeStateImages.TransparentColor = Color.Transparent;
      NodeStateImages.Images.SetKeyName(0, "Blank.png");
      NodeStateImages.Images.SetKeyName(1, "Colapsed.png");
      NodeStateImages.Images.SetKeyName(2, "Expanded.png");
      // 
      // toolStrip1
      // 
      toolStrip1.Items.AddRange(new ToolStripItem[] { OpenButton, SaveButton, toolStripSeparator4, AddSiblingNodeButton, AddChildNodeButton, DeleteNodeButton, toolStripSeparator5, ShiftNodeUpButton, ShiftNodeDownButton, DemoteNodeButton, toolStripSeparator6, AssignImageButton, CopyButton, PasteButton });
      toolStrip1.Location = new Point(0, 24);
      toolStrip1.Name = "toolStrip1";
      toolStrip1.Size = new Size(877, 25);
      toolStrip1.TabIndex = 5;
      toolStrip1.Text = "toolStrip1";
      // 
      // OpenButton
      // 
      OpenButton.DisplayStyle = ToolStripItemDisplayStyle.Image;
      OpenButton.Image = (Image)resources.GetObject("OpenButton.Image");
      OpenButton.ImageTransparentColor = Color.Magenta;
      OpenButton.Name = "OpenButton";
      OpenButton.Size = new Size(32, 22);
      OpenButton.Text = "Open";
      OpenButton.ToolTipText = "Open a palette file";
      OpenButton.DropDownItemClicked += OpenMRU_Click;
      // 
      // SaveButton
      // 
      SaveButton.DisplayStyle = ToolStripItemDisplayStyle.Image;
      SaveButton.Image = (Image)resources.GetObject("SaveButton.Image");
      SaveButton.ImageTransparentColor = Color.Magenta;
      SaveButton.Name = "SaveButton";
      SaveButton.Size = new Size(23, 22);
      SaveButton.Text = "Save";
      SaveButton.ToolTipText = "Save a palette";
      // 
      // toolStripSeparator4
      // 
      toolStripSeparator4.Name = "toolStripSeparator4";
      toolStripSeparator4.Size = new Size(6, 25);
      // 
      // AddSiblingNodeButton
      // 
      AddSiblingNodeButton.DisplayStyle = ToolStripItemDisplayStyle.Image;
      AddSiblingNodeButton.Image = PaletteDesigner.Resource.child_node_add;
      AddSiblingNodeButton.ImageTransparentColor = Color.Magenta;
      AddSiblingNodeButton.Name = "AddSiblingNodeButton";
      AddSiblingNodeButton.Size = new Size(23, 22);
      AddSiblingNodeButton.Text = "toolStripButton1";
      // 
      // AddChildNodeButton
      // 
      AddChildNodeButton.DisplayStyle = ToolStripItemDisplayStyle.Image;
      AddChildNodeButton.Image = (Image)resources.GetObject("AddChildNodeButton.Image");
      AddChildNodeButton.ImageTransparentColor = Color.Magenta;
      AddChildNodeButton.Name = "AddChildNodeButton";
      AddChildNodeButton.Size = new Size(23, 22);
      AddChildNodeButton.Text = "toolStripButton1";
      // 
      // DeleteNodeButton
      // 
      DeleteNodeButton.DisplayStyle = ToolStripItemDisplayStyle.Image;
      DeleteNodeButton.Image = (Image)resources.GetObject("DeleteNodeButton.Image");
      DeleteNodeButton.ImageTransparentColor = Color.Magenta;
      DeleteNodeButton.Name = "DeleteNodeButton";
      DeleteNodeButton.Size = new Size(23, 22);
      DeleteNodeButton.Text = "toolStripButton1";
      // 
      // toolStripSeparator5
      // 
      toolStripSeparator5.Name = "toolStripSeparator5";
      toolStripSeparator5.Size = new Size(6, 25);
      // 
      // ShiftNodeUpButton
      // 
      ShiftNodeUpButton.DisplayStyle = ToolStripItemDisplayStyle.Image;
      ShiftNodeUpButton.Image = (Image)resources.GetObject("ShiftNodeUpButton.Image");
      ShiftNodeUpButton.ImageTransparentColor = Color.Magenta;
      ShiftNodeUpButton.Name = "ShiftNodeUpButton";
      ShiftNodeUpButton.Size = new Size(23, 22);
      ShiftNodeUpButton.Text = "toolStripButton1";
      // 
      // ShiftNodeDownButton
      // 
      ShiftNodeDownButton.DisplayStyle = ToolStripItemDisplayStyle.Image;
      ShiftNodeDownButton.Image = (Image)resources.GetObject("ShiftNodeDownButton.Image");
      ShiftNodeDownButton.ImageTransparentColor = Color.Magenta;
      ShiftNodeDownButton.Name = "ShiftNodeDownButton";
      ShiftNodeDownButton.Size = new Size(23, 22);
      ShiftNodeDownButton.Text = "toolStripButton1";
      // 
      // DemoteNodeButton
      // 
      DemoteNodeButton.DisplayStyle = ToolStripItemDisplayStyle.Image;
      DemoteNodeButton.Image = (Image)resources.GetObject("DemoteNodeButton.Image");
      DemoteNodeButton.ImageTransparentColor = Color.Magenta;
      DemoteNodeButton.Name = "DemoteNodeButton";
      DemoteNodeButton.Size = new Size(23, 22);
      DemoteNodeButton.Text = "toolStripButton1";
      // 
      // toolStripSeparator6
      // 
      toolStripSeparator6.Name = "toolStripSeparator6";
      toolStripSeparator6.Size = new Size(6, 25);
      // 
      // AssignImageButton
      // 
      AssignImageButton.DisplayStyle = ToolStripItemDisplayStyle.Image;
      AssignImageButton.Image = (Image)resources.GetObject("AssignImageButton.Image");
      AssignImageButton.ImageTransparentColor = Color.Magenta;
      AssignImageButton.Name = "AssignImageButton";
      AssignImageButton.Size = new Size(23, 22);
      AssignImageButton.Text = "toolStripButton1";
      // 
      // CopyButton
      // 
      CopyButton.DisplayStyle = ToolStripItemDisplayStyle.Image;
      CopyButton.Image = (Image)resources.GetObject("CopyButton.Image");
      CopyButton.ImageTransparentColor = Color.Magenta;
      CopyButton.Name = "CopyButton";
      CopyButton.Size = new Size(23, 22);
      CopyButton.Text = "toolStripButton1";
      // 
      // PasteButton
      // 
      PasteButton.DisplayStyle = ToolStripItemDisplayStyle.Image;
      PasteButton.Image = (Image)resources.GetObject("PasteButton.Image");
      PasteButton.ImageTransparentColor = Color.Magenta;
      PasteButton.Name = "PasteButton";
      PasteButton.Size = new Size(23, 22);
      PasteButton.Text = "toolStripButton1";
      // 
      // Pallet
      // 
      Pallet.AllowDrop = true;
      Pallet.ContextMenuStrip = PalletContextMenu;
      Pallet.Dock = DockStyle.Left;
      Pallet.HideSelection = false;
      Pallet.ImageIndex = 0;
      Pallet.ImageList = NodeImages;
      Pallet.Indent = 16;
      Pallet.LabelEdit = true;
      Pallet.Location = new Point(0, 49);
      Pallet.Margin = new Padding(4, 3, 4, 3);
      Pallet.Name = "Pallet";
      Pallet.SelectedImageIndex = 0;
      Pallet.ShowLines = false;
      Pallet.ShowPlusMinus = false;
      Pallet.Size = new Size(233, 567);
      Pallet.StateImageList = NodeStateImages;
      Pallet.TabIndex = 4;
      Pallet.BeforeCollapse += Pallet_BeforeCollapse;
      Pallet.BeforeExpand += Pallet_BeforeExpand;
      Pallet.AfterSelect += Pallet_AfterSelect;
      Pallet.NodeMouseClick += NodeMouseClick;
      Pallet.MouseDown += Pallet_MouseDown;
      // 
      // statusStrip1
      // 
      statusStrip1.Items.AddRange(new ToolStripItem[] { StatusToolTip });
      statusStrip1.Location = new Point(0, 616);
      statusStrip1.Name = "statusStrip1";
      statusStrip1.Padding = new Padding(1, 0, 16, 0);
      statusStrip1.Size = new Size(877, 22);
      statusStrip1.TabIndex = 6;
      statusStrip1.Text = "statusStrip1";
      // 
      // StatusToolTip
      // 
      StatusToolTip.Name = "StatusToolTip";
      StatusToolTip.Size = new Size(118, 17);
      StatusToolTip.Text = "toolStripStatusLabel1";
      // 
      // Main
      // 
      AutoScaleDimensions = new SizeF(7F, 15F);
      AutoScaleMode = AutoScaleMode.Font;
      ClientSize = new Size(877, 638);
      Controls.Add(PropertyEditor);
      Controls.Add(splitter1);
      Controls.Add(Pallet);
      Controls.Add(statusStrip1);
      Controls.Add(toolStrip1);
      Controls.Add(menuStrip1);
      MainMenuStrip = menuStrip1;
      Margin = new Padding(4, 3, 4, 3);
      Name = "Main";
      Text = "LSD Palette Designer";
      FormClosing += Main_FormClosing;
      Load += Main_Load;
      menuStrip1.ResumeLayout(false);
      menuStrip1.PerformLayout();
      PalletContextMenu.ResumeLayout(false);
      toolStrip1.ResumeLayout(false);
      toolStrip1.PerformLayout();
      statusStrip1.ResumeLayout(false);
      statusStrip1.PerformLayout();
      ResumeLayout(false);
      PerformLayout();
    }

    #endregion

    private System.Windows.Forms.MenuStrip menuStrip1;
		private System.Windows.Forms.ToolStripMenuItem fileToolStripMenuItem;
		private System.Windows.Forms.ToolStripMenuItem OpenMenu;
		private System.Windows.Forms.ToolStripMenuItem SaveMenu;
		private System.Windows.Forms.ToolStripSeparator toolStripSeparator1;
		private System.Windows.Forms.ToolStripMenuItem ExitMenu;
		private System.Windows.Forms.ToolStripMenuItem editToolStripMenuItem;
		private System.Windows.Forms.PropertyGrid PropertyEditor;
		private System.Windows.Forms.Splitter splitter1;
		private System.Windows.Forms.SaveFileDialog SaveDialog;
		private System.Windows.Forms.ImageList NodeImages;
		private System.Windows.Forms.OpenFileDialog AddImageDialog;
		private System.Windows.Forms.ToolStripMenuItem menuNode;
		private System.Windows.Forms.ImageList NodeStateImages;
		private System.Windows.Forms.ToolStrip toolStrip1;
		private System.Windows.Forms.ToolStripSplitButton OpenButton;
		private System.Windows.Forms.ToolStripButton SaveButton;
		private System.Windows.Forms.ToolStripMenuItem SaveAsMenu;
		private System.Windows.Forms.ContextMenuStrip PalletContextMenu;
		private System.Windows.Forms.ToolStripMenuItem AddSiblingNodeContextMenu;
		private System.Windows.Forms.ToolStripMenuItem DeleteNodeContextMenu;
		private System.Windows.Forms.ToolStripMenuItem ShiftNodeUpContextMenu;
		private System.Windows.Forms.ToolStripMenuItem ShiftNodeDownContextMenu;
		private System.Windows.Forms.ToolStripMenuItem DemoteNodeContextMenu;
		private System.Windows.Forms.ToolStripMenuItem AddChildNodeContextMenu;
		private System.Windows.Forms.ToolStripSeparator toolStripMenuItem1;
		private System.Windows.Forms.ToolStripMenuItem AssignImageContextMenu;
		private System.Windows.Forms.ToolStripSeparator toolStripMenuItem2;
		private System.Windows.Forms.ToolStripSeparator toolStripSeparator2;
		private System.Windows.Forms.ToolStripMenuItem toolStripMenuItem3;
		private System.Windows.Forms.ToolStripMenuItem CopyContextMenu;
		private System.Windows.Forms.ToolStripMenuItem PasteContextMenu;
		private System.Windows.Forms.ToolStripSeparator toolStripSeparator3;
		private System.Windows.Forms.StatusStrip statusStrip1;
		private System.Windows.Forms.ToolStripStatusLabel StatusToolTip;
		private System.Windows.Forms.ToolStripSeparator toolStripSeparator4;
		private System.Windows.Forms.ToolStripButton AddSiblingNodeButton;
		private System.Windows.Forms.ToolStripButton AddChildNodeButton;
		private System.Windows.Forms.ToolStripButton DeleteNodeButton;
		private System.Windows.Forms.ToolStripSeparator toolStripSeparator5;
		private System.Windows.Forms.ToolStripButton ShiftNodeUpButton;
		private System.Windows.Forms.ToolStripButton ShiftNodeDownButton;
		private System.Windows.Forms.ToolStripButton DemoteNodeButton;
		private System.Windows.Forms.ToolStripSeparator toolStripSeparator6;
		private System.Windows.Forms.ToolStripButton AssignImageButton;
		private System.Windows.Forms.ToolStripSeparator toolStripSeparator7;
		private System.Windows.Forms.ToolStripMenuItem CopyMenu;
		private System.Windows.Forms.ToolStripMenuItem PasteMenu;
		private System.Windows.Forms.ToolStripButton CopyButton;
		private System.Windows.Forms.ToolStripButton PasteButton;
		public System.Windows.Forms.OpenFileDialog OpenDialog;
		public TNT.LSD.Components.PalletNodeTreeView Pallet;
		private System.Windows.Forms.ToolStripMenuItem exportImageToolStripMenuItem;
	}
}

