
using System.Drawing;

namespace SimulationWFA
{
    partial class SimulationProject
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(SimulationProject));
            this.menuStrip1 = new System.Windows.Forms.MenuStrip();
            this.fileToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.openToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.exitToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.saveToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.saveAsToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.exitToolStripMenuItem1 = new System.Windows.Forms.ToolStripMenuItem();
            this.optionsToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.editToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.undoToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.redoToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.cutToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.copyToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.pasteToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.findAndReplaceToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.viewToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.codeDesignerToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.helpToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.aboutSimulationProjectToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.openFileDialog1 = new System.Windows.Forms.OpenFileDialog();
            this.ımageList1 = new System.Windows.Forms.ImageList(this.components);
            this.folderBrowserDialog1 = new System.Windows.Forms.FolderBrowserDialog();
            this.projectsPanel = new System.Windows.Forms.Panel();
            this.projectsTreeView = new System.Windows.Forms.TreeView();
            this.projectsLabel = new System.Windows.Forms.Label();
            this.addObjectButton = new System.Windows.Forms.Button();
            this.hieararchyPanel = new System.Windows.Forms.Panel();
            this.Hierarchy = new System.Windows.Forms.Button();
            this.hierarchyLabel = new System.Windows.Forms.Label();
            this.simulationWindowPanel = new System.Windows.Forms.Panel();
            this.ChoosenAlgoTextBox = new System.Windows.Forms.TextBox();
            this.panel6 = new System.Windows.Forms.Panel();
            this.CustomCheckBox = new System.Windows.Forms.CheckBox();
            this.CustomMsTextBox = new System.Windows.Forms.TextBox();
            this.CustomDistanceTextBox = new System.Windows.Forms.TextBox();
            this.label11 = new System.Windows.Forms.Label();
            this.CustomLabel = new System.Windows.Forms.Label();
            this.label12 = new System.Windows.Forms.Label();
            this.DijsktraPanel = new System.Windows.Forms.Panel();
            this.DijkstraCheckBox = new System.Windows.Forms.CheckBox();
            this.DijkstraMsTextBox = new System.Windows.Forms.TextBox();
            this.DijkstraDistanceTextBox = new System.Windows.Forms.TextBox();
            this.label7 = new System.Windows.Forms.Label();
            this.DijkstraLabel = new System.Windows.Forms.Label();
            this.label8 = new System.Windows.Forms.Label();
            this.panel4 = new System.Windows.Forms.Panel();
            this.DFSCheckBox = new System.Windows.Forms.CheckBox();
            this.DFSMsTextBox = new System.Windows.Forms.TextBox();
            this.DFSDistanceTextBox = new System.Windows.Forms.TextBox();
            this.label9 = new System.Windows.Forms.Label();
            this.DFSLabel = new System.Windows.Forms.Label();
            this.label10 = new System.Windows.Forms.Label();
            this.panel3 = new System.Windows.Forms.Panel();
            this.BFSCheckBox = new System.Windows.Forms.CheckBox();
            this.BFSMsTextBox = new System.Windows.Forms.TextBox();
            this.BFSDistanceTextBox = new System.Windows.Forms.TextBox();
            this.label5 = new System.Windows.Forms.Label();
            this.BFSLabel = new System.Windows.Forms.Label();
            this.label6 = new System.Windows.Forms.Label();
            this.PrimsPanel = new System.Windows.Forms.Panel();
            this.PrimsCheckBox = new System.Windows.Forms.CheckBox();
            this.PrimsMsTextBox = new System.Windows.Forms.TextBox();
            this.PrimsDistanceTextBox = new System.Windows.Forms.TextBox();
            this.label3 = new System.Windows.Forms.Label();
            this.PrimsLabel = new System.Windows.Forms.Label();
            this.label4 = new System.Windows.Forms.Label();
            this.AStarPanel = new System.Windows.Forms.Panel();
            this.AStarCheckBox = new System.Windows.Forms.CheckBox();
            this.AStarMsTextBox = new System.Windows.Forms.TextBox();
            this.AStarDistanceTextBox = new System.Windows.Forms.TextBox();
            this.label2 = new System.Windows.Forms.Label();
            this.label1 = new System.Windows.Forms.Label();
            this.AStarLabel = new System.Windows.Forms.Label();
            this.simulationWindowLabel = new System.Windows.Forms.Label();
            this.inspectorLabel = new System.Windows.Forms.Label();
            this.inspectorPanel = new System.Windows.Forms.Panel();
            this.menuStrip1.SuspendLayout();
            this.projectsPanel.SuspendLayout();
            this.hieararchyPanel.SuspendLayout();
            this.simulationWindowPanel.SuspendLayout();
            this.panel6.SuspendLayout();
            this.DijsktraPanel.SuspendLayout();
            this.panel4.SuspendLayout();
            this.panel3.SuspendLayout();
            this.PrimsPanel.SuspendLayout();
            this.AStarPanel.SuspendLayout();
            this.inspectorPanel.SuspendLayout();
            this.SuspendLayout();
            // 
            // menuStrip1
            // 
            this.menuStrip1.BackColor = System.Drawing.Color.Azure;
            this.menuStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.fileToolStripMenuItem,
            this.optionsToolStripMenuItem,
            this.viewToolStripMenuItem,
            this.helpToolStripMenuItem});
            resources.ApplyResources(this.menuStrip1, "menuStrip1");
            this.menuStrip1.Name = "menuStrip1";
            // 
            // fileToolStripMenuItem
            // 
            this.fileToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.openToolStripMenuItem,
            this.exitToolStripMenuItem,
            this.saveToolStripMenuItem,
            this.saveAsToolStripMenuItem,
            this.exitToolStripMenuItem1});
            this.fileToolStripMenuItem.Name = "fileToolStripMenuItem";
            resources.ApplyResources(this.fileToolStripMenuItem, "fileToolStripMenuItem");
            // 
            // openToolStripMenuItem
            // 
            this.openToolStripMenuItem.Name = "openToolStripMenuItem";
            resources.ApplyResources(this.openToolStripMenuItem, "openToolStripMenuItem");
            this.openToolStripMenuItem.Click += new System.EventHandler(this.openToolStripMenuItem_Click);
            // 
            // exitToolStripMenuItem
            // 
            this.exitToolStripMenuItem.Name = "exitToolStripMenuItem";
            resources.ApplyResources(this.exitToolStripMenuItem, "exitToolStripMenuItem");
            // 
            // saveToolStripMenuItem
            // 
            this.saveToolStripMenuItem.Name = "saveToolStripMenuItem";
            resources.ApplyResources(this.saveToolStripMenuItem, "saveToolStripMenuItem");
            this.saveToolStripMenuItem.Click += new System.EventHandler(this.saveToolStripMenuItem_Click);
            // 
            // saveAsToolStripMenuItem
            // 
            this.saveAsToolStripMenuItem.Name = "saveAsToolStripMenuItem";
            resources.ApplyResources(this.saveAsToolStripMenuItem, "saveAsToolStripMenuItem");
            // 
            // exitToolStripMenuItem1
            // 
            this.exitToolStripMenuItem1.Name = "exitToolStripMenuItem1";
            resources.ApplyResources(this.exitToolStripMenuItem1, "exitToolStripMenuItem1");
            this.exitToolStripMenuItem1.Click += new System.EventHandler(this.exitToolStripMenuItem1_Click);
            // 
            // optionsToolStripMenuItem
            // 
            this.optionsToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.editToolStripMenuItem});
            this.optionsToolStripMenuItem.Name = "optionsToolStripMenuItem";
            resources.ApplyResources(this.optionsToolStripMenuItem, "optionsToolStripMenuItem");
            // 
            // editToolStripMenuItem
            // 
            this.editToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.undoToolStripMenuItem,
            this.redoToolStripMenuItem,
            this.cutToolStripMenuItem,
            this.copyToolStripMenuItem,
            this.pasteToolStripMenuItem,
            this.findAndReplaceToolStripMenuItem});
            this.editToolStripMenuItem.Name = "editToolStripMenuItem";
            resources.ApplyResources(this.editToolStripMenuItem, "editToolStripMenuItem");
            // 
            // undoToolStripMenuItem
            // 
            this.undoToolStripMenuItem.Name = "undoToolStripMenuItem";
            resources.ApplyResources(this.undoToolStripMenuItem, "undoToolStripMenuItem");
            // 
            // redoToolStripMenuItem
            // 
            this.redoToolStripMenuItem.Name = "redoToolStripMenuItem";
            resources.ApplyResources(this.redoToolStripMenuItem, "redoToolStripMenuItem");
            // 
            // cutToolStripMenuItem
            // 
            this.cutToolStripMenuItem.Name = "cutToolStripMenuItem";
            resources.ApplyResources(this.cutToolStripMenuItem, "cutToolStripMenuItem");
            // 
            // copyToolStripMenuItem
            // 
            this.copyToolStripMenuItem.Name = "copyToolStripMenuItem";
            resources.ApplyResources(this.copyToolStripMenuItem, "copyToolStripMenuItem");
            // 
            // pasteToolStripMenuItem
            // 
            this.pasteToolStripMenuItem.Name = "pasteToolStripMenuItem";
            resources.ApplyResources(this.pasteToolStripMenuItem, "pasteToolStripMenuItem");
            // 
            // findAndReplaceToolStripMenuItem
            // 
            this.findAndReplaceToolStripMenuItem.Name = "findAndReplaceToolStripMenuItem";
            resources.ApplyResources(this.findAndReplaceToolStripMenuItem, "findAndReplaceToolStripMenuItem");
            // 
            // viewToolStripMenuItem
            // 
            this.viewToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.codeDesignerToolStripMenuItem});
            this.viewToolStripMenuItem.Name = "viewToolStripMenuItem";
            resources.ApplyResources(this.viewToolStripMenuItem, "viewToolStripMenuItem");
            // 
            // codeDesignerToolStripMenuItem
            // 
            this.codeDesignerToolStripMenuItem.Name = "codeDesignerToolStripMenuItem";
            resources.ApplyResources(this.codeDesignerToolStripMenuItem, "codeDesignerToolStripMenuItem");
            // 
            // helpToolStripMenuItem
            // 
            this.helpToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.aboutSimulationProjectToolStripMenuItem});
            this.helpToolStripMenuItem.Name = "helpToolStripMenuItem";
            resources.ApplyResources(this.helpToolStripMenuItem, "helpToolStripMenuItem");
            // 
            // aboutSimulationProjectToolStripMenuItem
            // 
            this.aboutSimulationProjectToolStripMenuItem.Name = "aboutSimulationProjectToolStripMenuItem";
            resources.ApplyResources(this.aboutSimulationProjectToolStripMenuItem, "aboutSimulationProjectToolStripMenuItem");
            // 
            // openFileDialog1
            // 
            this.openFileDialog1.FileName = "openFileDialog1";
            // 
            // ımageList1
            // 
            this.ımageList1.ImageStream = ((System.Windows.Forms.ImageListStreamer)(resources.GetObject("ımageList1.ImageStream")));
            this.ımageList1.TransparentColor = System.Drawing.Color.Transparent;
            this.ımageList1.Images.SetKeyName(0, "folder.png");
            this.ımageList1.Images.SetKeyName(1, "coding.png");
            // 
            // projectsPanel
            // 
            resources.ApplyResources(this.projectsPanel, "projectsPanel");
            this.projectsPanel.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(25)))), ((int)(((byte)(27)))), ((int)(((byte)(29)))));
            this.projectsPanel.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            this.projectsPanel.Controls.Add(this.projectsTreeView);
            this.projectsPanel.Controls.Add(this.projectsLabel);
            this.projectsPanel.Name = "projectsPanel";
            // 
            // projectsTreeView
            // 
            resources.ApplyResources(this.projectsTreeView, "projectsTreeView");
            this.projectsTreeView.BackColor = System.Drawing.Color.DarkGray;
            this.projectsTreeView.ImageList = this.ımageList1;
            this.projectsTreeView.Name = "projectsTreeView";
            this.projectsTreeView.NodeMouseDoubleClick += new System.Windows.Forms.TreeNodeMouseClickEventHandler(this.projectsTreeView_NodeMouseDoubleClick);
            // 
            // projectsLabel
            // 
            resources.ApplyResources(this.projectsLabel, "projectsLabel");
            this.projectsLabel.ForeColor = System.Drawing.SystemColors.ButtonFace;
            this.projectsLabel.Name = "projectsLabel";
            // 
            // addObjectButton
            // 
            resources.ApplyResources(this.addObjectButton, "addObjectButton");
            this.addObjectButton.Name = "addObjectButton";
            this.addObjectButton.UseVisualStyleBackColor = true;
            this.addObjectButton.Click += new System.EventHandler(this.addObjectButton_Click);
            // 
            // hieararchyPanel
            // 
            resources.ApplyResources(this.hieararchyPanel, "hieararchyPanel");
            this.hieararchyPanel.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(25)))), ((int)(((byte)(27)))), ((int)(((byte)(29)))));
            this.hieararchyPanel.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            this.hieararchyPanel.Controls.Add(this.Hierarchy);
            this.hieararchyPanel.Controls.Add(this.addObjectButton);
            this.hieararchyPanel.Controls.Add(this.hierarchyLabel);
            this.hieararchyPanel.Name = "hieararchyPanel";
            // 
            // Hierarchy
            // 
            this.Hierarchy.ForeColor = System.Drawing.SystemColors.ControlLight;
            resources.ApplyResources(this.Hierarchy, "Hierarchy");
            this.Hierarchy.Name = "Hierarchy";
            // 
            // hierarchyLabel
            // 
            resources.ApplyResources(this.hierarchyLabel, "hierarchyLabel");
            this.hierarchyLabel.ForeColor = System.Drawing.SystemColors.ButtonFace;
            this.hierarchyLabel.Name = "hierarchyLabel";
            // 
            // simulationWindowPanel
            // 
            this.simulationWindowPanel.AllowDrop = true;
            resources.ApplyResources(this.simulationWindowPanel, "simulationWindowPanel");
            this.simulationWindowPanel.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(25)))), ((int)(((byte)(27)))), ((int)(((byte)(29)))));
            this.simulationWindowPanel.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            this.simulationWindowPanel.Controls.Add(this.ChoosenAlgoTextBox);
            this.simulationWindowPanel.Controls.Add(this.panel6);
            this.simulationWindowPanel.Controls.Add(this.DijsktraPanel);
            this.simulationWindowPanel.Controls.Add(this.panel4);
            this.simulationWindowPanel.Controls.Add(this.panel3);
            this.simulationWindowPanel.Controls.Add(this.PrimsPanel);
            this.simulationWindowPanel.Controls.Add(this.AStarPanel);
            this.simulationWindowPanel.Controls.Add(this.simulationWindowLabel);
            this.simulationWindowPanel.Name = "simulationWindowPanel";
            // 
            // ChoosenAlgoTextBox
            // 
            this.ChoosenAlgoTextBox.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(25)))), ((int)(((byte)(27)))), ((int)(((byte)(29)))));
            this.ChoosenAlgoTextBox.BorderStyle = System.Windows.Forms.BorderStyle.None;
            resources.ApplyResources(this.ChoosenAlgoTextBox, "ChoosenAlgoTextBox");
            this.ChoosenAlgoTextBox.ForeColor = System.Drawing.Color.White;
            this.ChoosenAlgoTextBox.Name = "ChoosenAlgoTextBox";
            // 
            // panel6
            // 
            this.panel6.BackColor = System.Drawing.Color.DarkGray;
            this.panel6.Controls.Add(this.CustomCheckBox);
            this.panel6.Controls.Add(this.CustomMsTextBox);
            this.panel6.Controls.Add(this.CustomDistanceTextBox);
            this.panel6.Controls.Add(this.label11);
            this.panel6.Controls.Add(this.CustomLabel);
            this.panel6.Controls.Add(this.label12);
            resources.ApplyResources(this.panel6, "panel6");
            this.panel6.Name = "panel6";
            // 
            // CustomCheckBox
            // 
            resources.ApplyResources(this.CustomCheckBox, "CustomCheckBox");
            this.CustomCheckBox.Name = "CustomCheckBox";
            this.CustomCheckBox.UseVisualStyleBackColor = true;
            this.CustomCheckBox.CheckedChanged += new System.EventHandler(this.CustomCheckBox_CheckedChanged);
            // 
            // CustomMsTextBox
            // 
            resources.ApplyResources(this.CustomMsTextBox, "CustomMsTextBox");
            this.CustomMsTextBox.Name = "CustomMsTextBox";
            // 
            // CustomDistanceTextBox
            // 
            resources.ApplyResources(this.CustomDistanceTextBox, "CustomDistanceTextBox");
            this.CustomDistanceTextBox.Name = "CustomDistanceTextBox";
            // 
            // label11
            // 
            resources.ApplyResources(this.label11, "label11");
            this.label11.Name = "label11";
            // 
            // CustomLabel
            // 
            resources.ApplyResources(this.CustomLabel, "CustomLabel");
            this.CustomLabel.ForeColor = System.Drawing.Color.Maroon;
            this.CustomLabel.Name = "CustomLabel";
            // 
            // label12
            // 
            resources.ApplyResources(this.label12, "label12");
            this.label12.Name = "label12";
            // 
            // DijsktraPanel
            // 
            this.DijsktraPanel.BackColor = System.Drawing.Color.DarkGray;
            this.DijsktraPanel.Controls.Add(this.DijkstraCheckBox);
            this.DijsktraPanel.Controls.Add(this.DijkstraMsTextBox);
            this.DijsktraPanel.Controls.Add(this.DijkstraDistanceTextBox);
            this.DijsktraPanel.Controls.Add(this.label7);
            this.DijsktraPanel.Controls.Add(this.DijkstraLabel);
            this.DijsktraPanel.Controls.Add(this.label8);
            resources.ApplyResources(this.DijsktraPanel, "DijsktraPanel");
            this.DijsktraPanel.Name = "DijsktraPanel";
            // 
            // DijkstraCheckBox
            // 
            resources.ApplyResources(this.DijkstraCheckBox, "DijkstraCheckBox");
            this.DijkstraCheckBox.Name = "DijkstraCheckBox";
            this.DijkstraCheckBox.UseVisualStyleBackColor = true;
            this.DijkstraCheckBox.CheckedChanged += new System.EventHandler(this.DijkstraCheckBox_CheckedChanged);
            // 
            // DijkstraMsTextBox
            // 
            resources.ApplyResources(this.DijkstraMsTextBox, "DijkstraMsTextBox");
            this.DijkstraMsTextBox.Name = "DijkstraMsTextBox";
            // 
            // DijkstraDistanceTextBox
            // 
            resources.ApplyResources(this.DijkstraDistanceTextBox, "DijkstraDistanceTextBox");
            this.DijkstraDistanceTextBox.Name = "DijkstraDistanceTextBox";
            // 
            // label7
            // 
            resources.ApplyResources(this.label7, "label7");
            this.label7.Name = "label7";
            // 
            // DijkstraLabel
            // 
            resources.ApplyResources(this.DijkstraLabel, "DijkstraLabel");
            this.DijkstraLabel.ForeColor = System.Drawing.Color.Maroon;
            this.DijkstraLabel.Name = "DijkstraLabel";
            // 
            // label8
            // 
            resources.ApplyResources(this.label8, "label8");
            this.label8.Name = "label8";
            // 
            // panel4
            // 
            this.panel4.BackColor = System.Drawing.Color.DarkGray;
            this.panel4.Controls.Add(this.DFSCheckBox);
            this.panel4.Controls.Add(this.DFSMsTextBox);
            this.panel4.Controls.Add(this.DFSDistanceTextBox);
            this.panel4.Controls.Add(this.label9);
            this.panel4.Controls.Add(this.DFSLabel);
            this.panel4.Controls.Add(this.label10);
            resources.ApplyResources(this.panel4, "panel4");
            this.panel4.Name = "panel4";
            // 
            // DFSCheckBox
            // 
            resources.ApplyResources(this.DFSCheckBox, "DFSCheckBox");
            this.DFSCheckBox.Name = "DFSCheckBox";
            this.DFSCheckBox.UseVisualStyleBackColor = true;
            this.DFSCheckBox.CheckedChanged += new System.EventHandler(this.DFSCheckBox_CheckedChanged);
            // 
            // DFSMsTextBox
            // 
            resources.ApplyResources(this.DFSMsTextBox, "DFSMsTextBox");
            this.DFSMsTextBox.Name = "DFSMsTextBox";
            // 
            // DFSDistanceTextBox
            // 
            resources.ApplyResources(this.DFSDistanceTextBox, "DFSDistanceTextBox");
            this.DFSDistanceTextBox.Name = "DFSDistanceTextBox";
            // 
            // label9
            // 
            resources.ApplyResources(this.label9, "label9");
            this.label9.Name = "label9";
            // 
            // DFSLabel
            // 
            resources.ApplyResources(this.DFSLabel, "DFSLabel");
            this.DFSLabel.ForeColor = System.Drawing.Color.Maroon;
            this.DFSLabel.Name = "DFSLabel";
            // 
            // label10
            // 
            resources.ApplyResources(this.label10, "label10");
            this.label10.Name = "label10";
            // 
            // panel3
            // 
            this.panel3.BackColor = System.Drawing.Color.DarkGray;
            this.panel3.Controls.Add(this.BFSCheckBox);
            this.panel3.Controls.Add(this.BFSMsTextBox);
            this.panel3.Controls.Add(this.BFSDistanceTextBox);
            this.panel3.Controls.Add(this.label5);
            this.panel3.Controls.Add(this.BFSLabel);
            this.panel3.Controls.Add(this.label6);
            resources.ApplyResources(this.panel3, "panel3");
            this.panel3.Name = "panel3";
            // 
            // BFSCheckBox
            // 
            resources.ApplyResources(this.BFSCheckBox, "BFSCheckBox");
            this.BFSCheckBox.Name = "BFSCheckBox";
            this.BFSCheckBox.UseVisualStyleBackColor = true;
            this.BFSCheckBox.CheckedChanged += new System.EventHandler(this.BFSCheckBox_CheckedChanged);
            // 
            // BFSMsTextBox
            // 
            resources.ApplyResources(this.BFSMsTextBox, "BFSMsTextBox");
            this.BFSMsTextBox.Name = "BFSMsTextBox";
            // 
            // BFSDistanceTextBox
            // 
            resources.ApplyResources(this.BFSDistanceTextBox, "BFSDistanceTextBox");
            this.BFSDistanceTextBox.Name = "BFSDistanceTextBox";
            // 
            // label5
            // 
            resources.ApplyResources(this.label5, "label5");
            this.label5.Name = "label5";
            // 
            // BFSLabel
            // 
            resources.ApplyResources(this.BFSLabel, "BFSLabel");
            this.BFSLabel.ForeColor = System.Drawing.Color.Maroon;
            this.BFSLabel.Name = "BFSLabel";
            // 
            // label6
            // 
            resources.ApplyResources(this.label6, "label6");
            this.label6.Name = "label6";
            // 
            // PrimsPanel
            // 
            this.PrimsPanel.BackColor = System.Drawing.Color.DarkGray;
            this.PrimsPanel.Controls.Add(this.PrimsCheckBox);
            this.PrimsPanel.Controls.Add(this.PrimsMsTextBox);
            this.PrimsPanel.Controls.Add(this.PrimsDistanceTextBox);
            this.PrimsPanel.Controls.Add(this.label3);
            this.PrimsPanel.Controls.Add(this.PrimsLabel);
            this.PrimsPanel.Controls.Add(this.label4);
            resources.ApplyResources(this.PrimsPanel, "PrimsPanel");
            this.PrimsPanel.Name = "PrimsPanel";
            // 
            // PrimsCheckBox
            // 
            resources.ApplyResources(this.PrimsCheckBox, "PrimsCheckBox");
            this.PrimsCheckBox.Name = "PrimsCheckBox";
            this.PrimsCheckBox.UseVisualStyleBackColor = true;
            this.PrimsCheckBox.CheckedChanged += new System.EventHandler(this.PrimsCheckBox_CheckedChanged);
            // 
            // PrimsMsTextBox
            // 
            resources.ApplyResources(this.PrimsMsTextBox, "PrimsMsTextBox");
            this.PrimsMsTextBox.Name = "PrimsMsTextBox";
            // 
            // PrimsDistanceTextBox
            // 
            resources.ApplyResources(this.PrimsDistanceTextBox, "PrimsDistanceTextBox");
            this.PrimsDistanceTextBox.Name = "PrimsDistanceTextBox";
            // 
            // label3
            // 
            resources.ApplyResources(this.label3, "label3");
            this.label3.Name = "label3";
            // 
            // PrimsLabel
            // 
            resources.ApplyResources(this.PrimsLabel, "PrimsLabel");
            this.PrimsLabel.ForeColor = System.Drawing.Color.Maroon;
            this.PrimsLabel.Name = "PrimsLabel";
            // 
            // label4
            // 
            resources.ApplyResources(this.label4, "label4");
            this.label4.Name = "label4";
            // 
            // AStarPanel
            // 
            this.AStarPanel.BackColor = System.Drawing.Color.DarkGray;
            this.AStarPanel.Controls.Add(this.AStarCheckBox);
            this.AStarPanel.Controls.Add(this.AStarMsTextBox);
            this.AStarPanel.Controls.Add(this.AStarDistanceTextBox);
            this.AStarPanel.Controls.Add(this.label2);
            this.AStarPanel.Controls.Add(this.label1);
            this.AStarPanel.Controls.Add(this.AStarLabel);
            resources.ApplyResources(this.AStarPanel, "AStarPanel");
            this.AStarPanel.Name = "AStarPanel";
            // 
            // AStarCheckBox
            // 
            resources.ApplyResources(this.AStarCheckBox, "AStarCheckBox");
            this.AStarCheckBox.Name = "AStarCheckBox";
            this.AStarCheckBox.UseVisualStyleBackColor = true;
            this.AStarCheckBox.CheckedChanged += new System.EventHandler(this.AStarCheckBox_CheckedChanged);
            // 
            // AStarMsTextBox
            // 
            resources.ApplyResources(this.AStarMsTextBox, "AStarMsTextBox");
            this.AStarMsTextBox.Name = "AStarMsTextBox";
            // 
            // AStarDistanceTextBox
            // 
            resources.ApplyResources(this.AStarDistanceTextBox, "AStarDistanceTextBox");
            this.AStarDistanceTextBox.Name = "AStarDistanceTextBox";
            // 
            // label2
            // 
            resources.ApplyResources(this.label2, "label2");
            this.label2.Name = "label2";
            // 
            // label1
            // 
            resources.ApplyResources(this.label1, "label1");
            this.label1.Name = "label1";
            // 
            // AStarLabel
            // 
            resources.ApplyResources(this.AStarLabel, "AStarLabel");
            this.AStarLabel.ForeColor = System.Drawing.Color.Maroon;
            this.AStarLabel.Name = "AStarLabel";
            // 
            // simulationWindowLabel
            // 
            resources.ApplyResources(this.simulationWindowLabel, "simulationWindowLabel");
            this.simulationWindowLabel.ForeColor = System.Drawing.SystemColors.ButtonFace;
            this.simulationWindowLabel.Name = "simulationWindowLabel";
            // 
            // inspectorLabel
            // 
            resources.ApplyResources(this.inspectorLabel, "inspectorLabel");
            this.inspectorLabel.ForeColor = System.Drawing.SystemColors.ButtonFace;
            this.inspectorLabel.Name = "inspectorLabel";
            // 
            // inspectorPanel
            // 
            resources.ApplyResources(this.inspectorPanel, "inspectorPanel");
            this.inspectorPanel.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(25)))), ((int)(((byte)(27)))), ((int)(((byte)(29)))));
            this.inspectorPanel.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            this.inspectorPanel.Controls.Add(this.inspectorLabel);
            this.inspectorPanel.Name = "inspectorPanel";
            // 
            // SimulationProject
            // 
            resources.ApplyResources(this, "$this");
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(36)))), ((int)(((byte)(36)))), ((int)(((byte)(36)))));
            this.Controls.Add(this.simulationWindowPanel);
            this.Controls.Add(this.inspectorPanel);
            this.Controls.Add(this.hieararchyPanel);
            this.Controls.Add(this.projectsPanel);
            this.Controls.Add(this.menuStrip1);
            this.MainMenuStrip = this.menuStrip1;
            this.Name = "SimulationProject";
            this.menuStrip1.ResumeLayout(false);
            this.menuStrip1.PerformLayout();
            this.projectsPanel.ResumeLayout(false);
            this.projectsPanel.PerformLayout();
            this.hieararchyPanel.ResumeLayout(false);
            this.hieararchyPanel.PerformLayout();
            this.simulationWindowPanel.ResumeLayout(false);
            this.simulationWindowPanel.PerformLayout();
            this.panel6.ResumeLayout(false);
            this.panel6.PerformLayout();
            this.DijsktraPanel.ResumeLayout(false);
            this.DijsktraPanel.PerformLayout();
            this.panel4.ResumeLayout(false);
            this.panel4.PerformLayout();
            this.panel3.ResumeLayout(false);
            this.panel3.PerformLayout();
            this.PrimsPanel.ResumeLayout(false);
            this.PrimsPanel.PerformLayout();
            this.AStarPanel.ResumeLayout(false);
            this.AStarPanel.PerformLayout();
            this.inspectorPanel.ResumeLayout(false);
            this.inspectorPanel.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.MenuStrip menuStrip1;
        private System.Windows.Forms.ToolStripMenuItem fileToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem openToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem exitToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem optionsToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem viewToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem saveToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem saveAsToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem exitToolStripMenuItem1;
        private System.Windows.Forms.ToolStripMenuItem editToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem undoToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem redoToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem cutToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem copyToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem pasteToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem findAndReplaceToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem codeDesignerToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem helpToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem aboutSimulationProjectToolStripMenuItem;
        private System.Windows.Forms.OpenFileDialog openFileDialog1;
        private System.Windows.Forms.FolderBrowserDialog folderBrowserDialog1;
        private System.Windows.Forms.ImageList ımageList1;
        private System.Windows.Forms.Panel projectsPanel;
        private System.Windows.Forms.TreeView projectsTreeView;
        private System.Windows.Forms.Label projectsLabel;
        private System.Windows.Forms.Panel hieararchyPanel;
        private System.Windows.Forms.Label hierarchyLabel;
        private System.Windows.Forms.Panel simulationWindowPanel;
        private System.Windows.Forms.Label simulationWindowLabel;
        private System.Windows.Forms.Button addObjectButton;
        private System.Windows.Forms.Button Hierarchy;
        private System.Windows.Forms.Label inspectorLabel;
        private System.Windows.Forms.Panel inspectorPanel;
        private System.Windows.Forms.Panel panel6;
        private System.Windows.Forms.Label CustomLabel;
        private System.Windows.Forms.Panel DijsktraPanel;
        private System.Windows.Forms.Label DijkstraLabel;
        private System.Windows.Forms.Panel panel4;
        private System.Windows.Forms.Label DFSLabel;
        private System.Windows.Forms.Panel panel3;
        private System.Windows.Forms.Label BFSLabel;
        private System.Windows.Forms.Panel PrimsPanel;
        private System.Windows.Forms.Label PrimsLabel;
        private System.Windows.Forms.Panel AStarPanel;
        private System.Windows.Forms.Label AStarLabel;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label11;
        private System.Windows.Forms.Label label12;
        private System.Windows.Forms.Label label7;
        private System.Windows.Forms.Label label8;
        private System.Windows.Forms.Label label9;
        private System.Windows.Forms.Label label10;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.CheckBox CustomCheckBox;
        private System.Windows.Forms.TextBox CustomMsTextBox;
        private System.Windows.Forms.TextBox CustomDistanceTextBox;
        private System.Windows.Forms.CheckBox DijkstraCheckBox;
        private System.Windows.Forms.TextBox DijkstraMsTextBox;
        private System.Windows.Forms.TextBox DijkstraDistanceTextBox;
        private System.Windows.Forms.CheckBox DFSCheckBox;
        private System.Windows.Forms.TextBox DFSMsTextBox;
        private System.Windows.Forms.TextBox DFSDistanceTextBox;
        private System.Windows.Forms.CheckBox BFSCheckBox;
        private System.Windows.Forms.TextBox BFSMsTextBox;
        private System.Windows.Forms.TextBox BFSDistanceTextBox;
        private System.Windows.Forms.CheckBox PrimsCheckBox;
        private System.Windows.Forms.TextBox PrimsMsTextBox;
        private System.Windows.Forms.TextBox PrimsDistanceTextBox;
        private System.Windows.Forms.CheckBox AStarCheckBox;
        private System.Windows.Forms.TextBox AStarMsTextBox;
        private System.Windows.Forms.TextBox AStarDistanceTextBox;
        private System.Windows.Forms.TextBox ChoosenAlgoTextBox;
    }
}

