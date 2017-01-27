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
			this.components = new System.ComponentModel.Container();
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
			this.Pallet = new LSDComponents.PalletNodeTreeView(this.components);
			this.statusStrip1 = new System.Windows.Forms.StatusStrip();
			this.StatusToolTip = new System.Windows.Forms.ToolStripStatusLabel();
			this.menuStrip1.SuspendLayout();
			this.PalletContextMenu.SuspendLayout();
			this.toolStrip1.SuspendLayout();
			this.statusStrip1.SuspendLayout();
			this.SuspendLayout();
			// 
			// NodeImages
			// 
			this.NodeImages.ImageStream = ((System.Windows.Forms.ImageListStreamer)(resources.GetObject("NodeImages.ImageStream")));
			this.NodeImages.TransparentColor = System.Drawing.Color.Transparent;
			this.NodeImages.Images.SetKeyName(0, "Blank.png");
			// 
			// menuStrip1
			// 
			this.menuStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.fileToolStripMenuItem,
            this.editToolStripMenuItem});
			this.menuStrip1.Location = new System.Drawing.Point(0, 0);
			this.menuStrip1.Name = "menuStrip1";
			this.menuStrip1.Size = new System.Drawing.Size(752, 24);
			this.menuStrip1.TabIndex = 1;
			this.menuStrip1.Text = "menuStrip1";
			// 
			// fileToolStripMenuItem
			// 
			this.fileToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.OpenMenu,
            this.SaveMenu,
            this.SaveAsMenu,
            this.toolStripSeparator1,
            this.ExitMenu});
			this.fileToolStripMenuItem.Name = "fileToolStripMenuItem";
			this.fileToolStripMenuItem.Size = new System.Drawing.Size(37, 20);
			this.fileToolStripMenuItem.Text = "&File";
			// 
			// OpenMenu
			// 
			this.OpenMenu.Name = "OpenMenu";
			this.OpenMenu.Size = new System.Drawing.Size(114, 22);
			this.OpenMenu.Text = "&Open";
			// 
			// SaveMenu
			// 
			this.SaveMenu.Name = "SaveMenu";
			this.SaveMenu.Size = new System.Drawing.Size(114, 22);
			this.SaveMenu.Text = "&Save";
			// 
			// SaveAsMenu
			// 
			this.SaveAsMenu.Name = "SaveAsMenu";
			this.SaveAsMenu.Size = new System.Drawing.Size(114, 22);
			this.SaveAsMenu.Text = "Save &As";
			// 
			// toolStripSeparator1
			// 
			this.toolStripSeparator1.Name = "toolStripSeparator1";
			this.toolStripSeparator1.Size = new System.Drawing.Size(111, 6);
			// 
			// ExitMenu
			// 
			this.ExitMenu.Name = "ExitMenu";
			this.ExitMenu.Size = new System.Drawing.Size(114, 22);
			this.ExitMenu.Text = "E&xit";
			this.ExitMenu.ToolTipText = "Closes the application";
			// 
			// editToolStripMenuItem
			// 
			this.editToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.menuNode,
            this.toolStripSeparator7,
            this.CopyMenu,
            this.PasteMenu});
			this.editToolStripMenuItem.Name = "editToolStripMenuItem";
			this.editToolStripMenuItem.Size = new System.Drawing.Size(39, 20);
			this.editToolStripMenuItem.Text = "&Edit";
			// 
			// menuNode
			// 
			this.menuNode.DropDown = this.PalletContextMenu;
			this.menuNode.Name = "menuNode";
			this.menuNode.Size = new System.Drawing.Size(103, 22);
			this.menuNode.Text = "&Node";
			// 
			// PalletContextMenu
			// 
			this.PalletContextMenu.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.AddSiblingNodeContextMenu,
            this.AddChildNodeContextMenu,
            this.DeleteNodeContextMenu,
            this.toolStripMenuItem1,
            this.ShiftNodeUpContextMenu,
            this.ShiftNodeDownContextMenu,
            this.DemoteNodeContextMenu,
            this.toolStripSeparator2,
            this.AssignImageContextMenu,
            this.toolStripMenuItem2,
            this.CopyContextMenu,
            this.PasteContextMenu,
            this.toolStripSeparator3,
            this.toolStripMenuItem3});
			this.PalletContextMenu.Name = "PalletContextMenu";
			this.PalletContextMenu.OwnerItem = this.menuNode;
			this.PalletContextMenu.Size = new System.Drawing.Size(168, 248);
			// 
			// AddSiblingNodeContextMenu
			// 
			this.AddSiblingNodeContextMenu.Image = ((System.Drawing.Image)(resources.GetObject("AddSiblingNodeContextMenu.Image")));
			this.AddSiblingNodeContextMenu.Name = "AddSiblingNodeContextMenu";
			this.AddSiblingNodeContextMenu.Size = new System.Drawing.Size(167, 22);
			this.AddSiblingNodeContextMenu.Text = "Add Sibling Node";
			this.AddSiblingNodeContextMenu.ToolTipText = "Add a sibling node";
			// 
			// AddChildNodeContextMenu
			// 
			this.AddChildNodeContextMenu.Image = global::PalletDesigner.Properties.Resources.child_node_add;
			this.AddChildNodeContextMenu.Name = "AddChildNodeContextMenu";
			this.AddChildNodeContextMenu.Size = new System.Drawing.Size(167, 22);
			this.AddChildNodeContextMenu.Text = "Add Child Node";
			this.AddChildNodeContextMenu.ToolTipText = "Add child node";
			// 
			// DeleteNodeContextMenu
			// 
			this.DeleteNodeContextMenu.Image = global::PalletDesigner.Properties.Resources.delete;
			this.DeleteNodeContextMenu.Name = "DeleteNodeContextMenu";
			this.DeleteNodeContextMenu.Size = new System.Drawing.Size(167, 22);
			this.DeleteNodeContextMenu.Text = "Delete Node";
			this.DeleteNodeContextMenu.ToolTipText = "Deletes the selected node and all children";
			// 
			// toolStripMenuItem1
			// 
			this.toolStripMenuItem1.Name = "toolStripMenuItem1";
			this.toolStripMenuItem1.Size = new System.Drawing.Size(164, 6);
			// 
			// ShiftNodeUpContextMenu
			// 
			this.ShiftNodeUpContextMenu.Image = global::PalletDesigner.Properties.Resources.node_move_up;
			this.ShiftNodeUpContextMenu.Name = "ShiftNodeUpContextMenu";
			this.ShiftNodeUpContextMenu.Size = new System.Drawing.Size(167, 22);
			this.ShiftNodeUpContextMenu.Text = "Shift Node Up";
			this.ShiftNodeUpContextMenu.ToolTipText = "Shifts node before previous sibling";
			// 
			// ShiftNodeDownContextMenu
			// 
			this.ShiftNodeDownContextMenu.Image = global::PalletDesigner.Properties.Resources.node_move_down;
			this.ShiftNodeDownContextMenu.Name = "ShiftNodeDownContextMenu";
			this.ShiftNodeDownContextMenu.Size = new System.Drawing.Size(167, 22);
			this.ShiftNodeDownContextMenu.Text = "Shift Node Down";
			this.ShiftNodeDownContextMenu.ToolTipText = "Shifts node down after next sibling";
			// 
			// DemoteNodeContextMenu
			// 
			this.DemoteNodeContextMenu.Image = global::PalletDesigner.Properties.Resources.node_demote;
			this.DemoteNodeContextMenu.Name = "DemoteNodeContextMenu";
			this.DemoteNodeContextMenu.Size = new System.Drawing.Size(167, 22);
			this.DemoteNodeContextMenu.Text = "Demote Node";
			this.DemoteNodeContextMenu.ToolTipText = "Demotes selected node as a sibling to its parent";
			// 
			// toolStripSeparator2
			// 
			this.toolStripSeparator2.Name = "toolStripSeparator2";
			this.toolStripSeparator2.Size = new System.Drawing.Size(164, 6);
			// 
			// AssignImageContextMenu
			// 
			this.AssignImageContextMenu.Image = global::PalletDesigner.Properties.Resources.picture_add;
			this.AssignImageContextMenu.Name = "AssignImageContextMenu";
			this.AssignImageContextMenu.Size = new System.Drawing.Size(167, 22);
			this.AssignImageContextMenu.Text = "Assign Image";
			this.AssignImageContextMenu.ToolTipText = "Assigns an image to the node";
			// 
			// toolStripMenuItem2
			// 
			this.toolStripMenuItem2.Name = "toolStripMenuItem2";
			this.toolStripMenuItem2.Size = new System.Drawing.Size(164, 6);
			// 
			// CopyContextMenu
			// 
			this.CopyContextMenu.Image = ((System.Drawing.Image)(resources.GetObject("CopyContextMenu.Image")));
			this.CopyContextMenu.Name = "CopyContextMenu";
			this.CopyContextMenu.Size = new System.Drawing.Size(167, 22);
			this.CopyContextMenu.Text = "Copy";
			this.CopyContextMenu.ToolTipText = "Copies the selected node and its children";
			// 
			// PasteContextMenu
			// 
			this.PasteContextMenu.Image = ((System.Drawing.Image)(resources.GetObject("PasteContextMenu.Image")));
			this.PasteContextMenu.Name = "PasteContextMenu";
			this.PasteContextMenu.Size = new System.Drawing.Size(167, 22);
			this.PasteContextMenu.Text = "Paste";
			this.PasteContextMenu.ToolTipText = "Pastes the prevously copied node and its children";
			// 
			// toolStripSeparator3
			// 
			this.toolStripSeparator3.Name = "toolStripSeparator3";
			this.toolStripSeparator3.Size = new System.Drawing.Size(164, 6);
			// 
			// toolStripMenuItem3
			// 
			this.toolStripMenuItem3.Name = "toolStripMenuItem3";
			this.toolStripMenuItem3.Size = new System.Drawing.Size(167, 22);
			this.toolStripMenuItem3.Text = "Expand All";
			this.toolStripMenuItem3.Click += new System.EventHandler(this.ExpandAll_Click);
			// 
			// toolStripSeparator7
			// 
			this.toolStripSeparator7.Name = "toolStripSeparator7";
			this.toolStripSeparator7.Size = new System.Drawing.Size(100, 6);
			// 
			// CopyMenu
			// 
			this.CopyMenu.Name = "CopyMenu";
			this.CopyMenu.Size = new System.Drawing.Size(103, 22);
			this.CopyMenu.Text = "Copy";
			// 
			// PasteMenu
			// 
			this.PasteMenu.Name = "PasteMenu";
			this.PasteMenu.Size = new System.Drawing.Size(103, 22);
			this.PasteMenu.Text = "Paste";
			// 
			// PropertyEditor
			// 
			this.PropertyEditor.Dock = System.Windows.Forms.DockStyle.Fill;
			this.PropertyEditor.Location = new System.Drawing.Point(203, 49);
			this.PropertyEditor.Name = "PropertyEditor";
			this.PropertyEditor.PropertySort = System.Windows.Forms.PropertySort.NoSort;
			this.PropertyEditor.Size = new System.Drawing.Size(549, 482);
			this.PropertyEditor.TabIndex = 2;
			this.PropertyEditor.ToolbarVisible = false;
			// 
			// splitter1
			// 
			this.splitter1.Location = new System.Drawing.Point(200, 49);
			this.splitter1.Name = "splitter1";
			this.splitter1.Size = new System.Drawing.Size(3, 482);
			this.splitter1.TabIndex = 3;
			this.splitter1.TabStop = false;
			// 
			// SaveDialog
			// 
			this.SaveDialog.DefaultExt = "pallet";
			this.SaveDialog.Filter = "LSD Palette|*.palette";
			this.SaveDialog.RestoreDirectory = true;
			this.SaveDialog.Title = "Save LSD Palette";
			// 
			// OpenDialog
			// 
			this.OpenDialog.DefaultExt = "palette";
			this.OpenDialog.Filter = "LSD Palette|*.palette";
			this.OpenDialog.RestoreDirectory = true;
			this.OpenDialog.Title = "Open LSD Palette";
			// 
			// AddImageDialog
			// 
			this.AddImageDialog.Title = "Open Image";
			// 
			// NodeStateImages
			// 
			this.NodeStateImages.ImageStream = ((System.Windows.Forms.ImageListStreamer)(resources.GetObject("NodeStateImages.ImageStream")));
			this.NodeStateImages.TransparentColor = System.Drawing.Color.Transparent;
			this.NodeStateImages.Images.SetKeyName(0, "Blank.png");
			this.NodeStateImages.Images.SetKeyName(1, "Colapsed.png");
			this.NodeStateImages.Images.SetKeyName(2, "Expanded.png");
			// 
			// toolStrip1
			// 
			this.toolStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.OpenButton,
            this.SaveButton,
            this.toolStripSeparator4,
            this.AddSiblingNodeButton,
            this.AddChildNodeButton,
            this.DeleteNodeButton,
            this.toolStripSeparator5,
            this.ShiftNodeUpButton,
            this.ShiftNodeDownButton,
            this.DemoteNodeButton,
            this.toolStripSeparator6,
            this.AssignImageButton,
            this.CopyButton,
            this.PasteButton});
			this.toolStrip1.Location = new System.Drawing.Point(0, 24);
			this.toolStrip1.Name = "toolStrip1";
			this.toolStrip1.Size = new System.Drawing.Size(752, 25);
			this.toolStrip1.TabIndex = 5;
			this.toolStrip1.Text = "toolStrip1";
			// 
			// OpenButton
			// 
			this.OpenButton.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
			this.OpenButton.Image = ((System.Drawing.Image)(resources.GetObject("OpenButton.Image")));
			this.OpenButton.ImageTransparentColor = System.Drawing.Color.Magenta;
			this.OpenButton.Name = "OpenButton";
			this.OpenButton.Size = new System.Drawing.Size(32, 22);
			this.OpenButton.Text = "Open";
			this.OpenButton.ToolTipText = "Open a palette file";
			this.OpenButton.DropDownItemClicked += new System.Windows.Forms.ToolStripItemClickedEventHandler(this.OpenMRU_Click);
			// 
			// SaveButton
			// 
			this.SaveButton.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
			this.SaveButton.Image = ((System.Drawing.Image)(resources.GetObject("SaveButton.Image")));
			this.SaveButton.ImageTransparentColor = System.Drawing.Color.Magenta;
			this.SaveButton.Name = "SaveButton";
			this.SaveButton.Size = new System.Drawing.Size(23, 22);
			this.SaveButton.Text = "Save";
			this.SaveButton.ToolTipText = "Save a palette";
			// 
			// toolStripSeparator4
			// 
			this.toolStripSeparator4.Name = "toolStripSeparator4";
			this.toolStripSeparator4.Size = new System.Drawing.Size(6, 25);
			// 
			// AddSiblingNodeButton
			// 
			this.AddSiblingNodeButton.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
			this.AddSiblingNodeButton.Image = ((System.Drawing.Image)(resources.GetObject("AddSiblingNodeButton.Image")));
			this.AddSiblingNodeButton.ImageTransparentColor = System.Drawing.Color.Magenta;
			this.AddSiblingNodeButton.Name = "AddSiblingNodeButton";
			this.AddSiblingNodeButton.Size = new System.Drawing.Size(23, 22);
			this.AddSiblingNodeButton.Text = "toolStripButton1";
			// 
			// AddChildNodeButton
			// 
			this.AddChildNodeButton.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
			this.AddChildNodeButton.Image = ((System.Drawing.Image)(resources.GetObject("AddChildNodeButton.Image")));
			this.AddChildNodeButton.ImageTransparentColor = System.Drawing.Color.Magenta;
			this.AddChildNodeButton.Name = "AddChildNodeButton";
			this.AddChildNodeButton.Size = new System.Drawing.Size(23, 22);
			this.AddChildNodeButton.Text = "toolStripButton1";
			// 
			// DeleteNodeButton
			// 
			this.DeleteNodeButton.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
			this.DeleteNodeButton.Image = ((System.Drawing.Image)(resources.GetObject("DeleteNodeButton.Image")));
			this.DeleteNodeButton.ImageTransparentColor = System.Drawing.Color.Magenta;
			this.DeleteNodeButton.Name = "DeleteNodeButton";
			this.DeleteNodeButton.Size = new System.Drawing.Size(23, 22);
			this.DeleteNodeButton.Text = "toolStripButton1";
			// 
			// toolStripSeparator5
			// 
			this.toolStripSeparator5.Name = "toolStripSeparator5";
			this.toolStripSeparator5.Size = new System.Drawing.Size(6, 25);
			// 
			// ShiftNodeUpButton
			// 
			this.ShiftNodeUpButton.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
			this.ShiftNodeUpButton.Image = ((System.Drawing.Image)(resources.GetObject("ShiftNodeUpButton.Image")));
			this.ShiftNodeUpButton.ImageTransparentColor = System.Drawing.Color.Magenta;
			this.ShiftNodeUpButton.Name = "ShiftNodeUpButton";
			this.ShiftNodeUpButton.Size = new System.Drawing.Size(23, 22);
			this.ShiftNodeUpButton.Text = "toolStripButton1";
			// 
			// ShiftNodeDownButton
			// 
			this.ShiftNodeDownButton.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
			this.ShiftNodeDownButton.Image = ((System.Drawing.Image)(resources.GetObject("ShiftNodeDownButton.Image")));
			this.ShiftNodeDownButton.ImageTransparentColor = System.Drawing.Color.Magenta;
			this.ShiftNodeDownButton.Name = "ShiftNodeDownButton";
			this.ShiftNodeDownButton.Size = new System.Drawing.Size(23, 22);
			this.ShiftNodeDownButton.Text = "toolStripButton1";
			// 
			// DemoteNodeButton
			// 
			this.DemoteNodeButton.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
			this.DemoteNodeButton.Image = ((System.Drawing.Image)(resources.GetObject("DemoteNodeButton.Image")));
			this.DemoteNodeButton.ImageTransparentColor = System.Drawing.Color.Magenta;
			this.DemoteNodeButton.Name = "DemoteNodeButton";
			this.DemoteNodeButton.Size = new System.Drawing.Size(23, 22);
			this.DemoteNodeButton.Text = "toolStripButton1";
			// 
			// toolStripSeparator6
			// 
			this.toolStripSeparator6.Name = "toolStripSeparator6";
			this.toolStripSeparator6.Size = new System.Drawing.Size(6, 25);
			// 
			// AssignImageButton
			// 
			this.AssignImageButton.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
			this.AssignImageButton.Image = ((System.Drawing.Image)(resources.GetObject("AssignImageButton.Image")));
			this.AssignImageButton.ImageTransparentColor = System.Drawing.Color.Magenta;
			this.AssignImageButton.Name = "AssignImageButton";
			this.AssignImageButton.Size = new System.Drawing.Size(23, 22);
			this.AssignImageButton.Text = "toolStripButton1";
			// 
			// CopyButton
			// 
			this.CopyButton.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
			this.CopyButton.Image = ((System.Drawing.Image)(resources.GetObject("CopyButton.Image")));
			this.CopyButton.ImageTransparentColor = System.Drawing.Color.Magenta;
			this.CopyButton.Name = "CopyButton";
			this.CopyButton.Size = new System.Drawing.Size(23, 22);
			this.CopyButton.Text = "toolStripButton1";
			// 
			// PasteButton
			// 
			this.PasteButton.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
			this.PasteButton.Image = ((System.Drawing.Image)(resources.GetObject("PasteButton.Image")));
			this.PasteButton.ImageTransparentColor = System.Drawing.Color.Magenta;
			this.PasteButton.Name = "PasteButton";
			this.PasteButton.Size = new System.Drawing.Size(23, 22);
			this.PasteButton.Text = "toolStripButton1";
			// 
			// Pallet
			// 
			this.Pallet.AllowDrop = true;
			this.Pallet.ContextMenuStrip = this.PalletContextMenu;
			this.Pallet.Dock = System.Windows.Forms.DockStyle.Left;
			this.Pallet.HideSelection = false;
			this.Pallet.ImageIndex = 0;
			this.Pallet.ImageList = this.NodeImages;
			this.Pallet.Indent = 16;
			this.Pallet.LabelEdit = true;
			this.Pallet.Location = new System.Drawing.Point(0, 49);
			this.Pallet.Name = "Pallet";
			this.Pallet.SelectedImageIndex = 0;
			this.Pallet.ShowLines = false;
			this.Pallet.ShowPlusMinus = false;
			this.Pallet.Size = new System.Drawing.Size(200, 482);
			this.Pallet.StateImageList = this.NodeStateImages;
			this.Pallet.TabIndex = 4;
			this.Pallet.BeforeCollapse += new System.Windows.Forms.TreeViewCancelEventHandler(this.Pallet_BeforeCollapse);
			this.Pallet.BeforeExpand += new System.Windows.Forms.TreeViewCancelEventHandler(this.Pallet_BeforeExpand);
			this.Pallet.AfterSelect += new System.Windows.Forms.TreeViewEventHandler(this.Pallet_AfterSelect);
			this.Pallet.NodeMouseClick += new System.Windows.Forms.TreeNodeMouseClickEventHandler(this.NodeMouseClick);
			this.Pallet.MouseDown += new System.Windows.Forms.MouseEventHandler(this.Pallet_MouseDown);
			// 
			// statusStrip1
			// 
			this.statusStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.StatusToolTip});
			this.statusStrip1.Location = new System.Drawing.Point(0, 531);
			this.statusStrip1.Name = "statusStrip1";
			this.statusStrip1.Size = new System.Drawing.Size(752, 22);
			this.statusStrip1.TabIndex = 6;
			this.statusStrip1.Text = "statusStrip1";
			// 
			// StatusToolTip
			// 
			this.StatusToolTip.Name = "StatusToolTip";
			this.StatusToolTip.Size = new System.Drawing.Size(118, 17);
			this.StatusToolTip.Text = "toolStripStatusLabel1";
			// 
			// Main
			// 
			this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
			this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
			this.ClientSize = new System.Drawing.Size(752, 553);
			this.Controls.Add(this.PropertyEditor);
			this.Controls.Add(this.splitter1);
			this.Controls.Add(this.Pallet);
			this.Controls.Add(this.statusStrip1);
			this.Controls.Add(this.toolStrip1);
			this.Controls.Add(this.menuStrip1);
			this.MainMenuStrip = this.menuStrip1;
			this.Name = "Main";
			this.Text = "LSD Palette Designer";
			this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.Main_FormClosing);
			this.Load += new System.EventHandler(this.Main_Load);
			this.menuStrip1.ResumeLayout(false);
			this.menuStrip1.PerformLayout();
			this.PalletContextMenu.ResumeLayout(false);
			this.toolStrip1.ResumeLayout(false);
			this.toolStrip1.PerformLayout();
			this.statusStrip1.ResumeLayout(false);
			this.statusStrip1.PerformLayout();
			this.ResumeLayout(false);
			this.PerformLayout();

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
		private System.Windows.Forms.OpenFileDialog OpenDialog;
		private System.Windows.Forms.ImageList NodeImages;
		private System.Windows.Forms.OpenFileDialog AddImageDialog;
		private System.Windows.Forms.ToolStripMenuItem menuNode;
		private LSDComponents.PalletNodeTreeView Pallet;
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
	}
}

