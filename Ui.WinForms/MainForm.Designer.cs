namespace TipTracker.Ui
{
    partial class MainForm
    {
        /// <summary>
        ///  Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        ///  Clean up any resources being used.
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
        ///  Required method for Designer support - do not modify
        ///  the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            components = new System.ComponentModel.Container();
            mnuMainMenu = new MenuStrip();
            mnuFile = new ToolStripMenuItem();
            mnuNew = new ToolStripMenuItem();
            mnuOpen = new ToolStripMenuItem();
            toolStripSeparator3 = new ToolStripSeparator();
            mnuClose = new ToolStripMenuItem();
            toolStripSeparator2 = new ToolStripSeparator();
            mnuSave = new ToolStripMenuItem();
            mnuSaveAs = new ToolStripMenuItem();
            toolStripSeparator1 = new ToolStripSeparator();
            mnuExit = new ToolStripMenuItem();
            edutToolStripMenuItem = new ToolStripMenuItem();
            mnuUndo = new ToolStripMenuItem();
            mnuRedo = new ToolStripMenuItem();
            toolStripSeparator4 = new ToolStripSeparator();
            mnuCut = new ToolStripMenuItem();
            mnuCopy = new ToolStripMenuItem();
            mnuPaste = new ToolStripMenuItem();
            toolsToolStripMenuItem = new ToolStripMenuItem();
            manageServersToolStripMenuItem = new ToolStripMenuItem();
            manageFunctionsToolStripMenuItem = new ToolStripMenuItem();
            exportTipsToolStripMenuItem = new ToolStripMenuItem();
            autoAddServersToolStripMenuItem = new ToolStripMenuItem();
            optimizeFileToolStripMenuItem = new ToolStripMenuItem();
            settingsToolStripMenuItem = new ToolStripMenuItem();
            reportsToolStripMenuItem = new ToolStripMenuItem();
            tipChitsToolStripMenuItem = new ToolStripMenuItem();
            tipReportsToolStripMenuItem = new ToolStripMenuItem();
            specialFunctionDetailToolStripMenuItem = new ToolStripMenuItem();
            payrollBalancingReportToolStripMenuItem = new ToolStripMenuItem();
            serverListToolStripMenuItem = new ToolStripMenuItem();
            helpToolStripMenuItem = new ToolStripMenuItem();
            contentsToolStripMenuItem = new ToolStripMenuItem();
            indexToolStripMenuItem = new ToolStripMenuItem();
            toolStripSeparator5 = new ToolStripSeparator();
            aboutTripTrackerToolStripMenuItem = new ToolStripMenuItem();
            strMainStatusBar = new StatusStrip();
            lblBusinessDate = new ToolStripStatusLabel();
            lblPeriodDates = new ToolStripStatusLabel();
            spMainSplitContainer = new SplitContainer();
            tabControl1 = new TabControl();
            tabPage1 = new TabPage();
            tipEditor2 = new TipTracker.Ui.Controls.QuickTipEditor();
            grpServerList = new GroupBox();
            dgvServers = new DataGridView();
            serverListBindingSource = new BindingSource(components);
            Number = new DataGridViewTextBoxColumn();
            lastNameDataGridViewTextBoxColumn = new DataGridViewTextBoxColumn();
            firstNameDataGridViewTextBoxColumn = new DataGridViewTextBoxColumn();
            mnuMainMenu.SuspendLayout();
            strMainStatusBar.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)spMainSplitContainer).BeginInit();
            spMainSplitContainer.Panel1.SuspendLayout();
            spMainSplitContainer.Panel2.SuspendLayout();
            spMainSplitContainer.SuspendLayout();
            tabControl1.SuspendLayout();
            tabPage1.SuspendLayout();
            grpServerList.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)dgvServers).BeginInit();
            ((System.ComponentModel.ISupportInitialize)serverListBindingSource).BeginInit();
            SuspendLayout();
            // 
            // mnuMainMenu
            // 
            mnuMainMenu.Items.AddRange(new ToolStripItem[] { mnuFile, edutToolStripMenuItem, toolsToolStripMenuItem, reportsToolStripMenuItem, helpToolStripMenuItem });
            mnuMainMenu.Location = new Point(0, 0);
            mnuMainMenu.Name = "mnuMainMenu";
            mnuMainMenu.Size = new Size(1008, 24);
            mnuMainMenu.TabIndex = 0;
            mnuMainMenu.Text = "menuStrip1";
            // 
            // mnuFile
            // 
            mnuFile.DropDownItems.AddRange(new ToolStripItem[] { mnuNew, mnuOpen, toolStripSeparator3, mnuClose, toolStripSeparator2, mnuSave, mnuSaveAs, toolStripSeparator1, mnuExit });
            mnuFile.Name = "mnuFile";
            mnuFile.Size = new Size(37, 20);
            mnuFile.Text = "&File";
            // 
            // mnuNew
            // 
            mnuNew.Name = "mnuNew";
            mnuNew.Size = new Size(123, 22);
            mnuNew.Text = "&New...";
            // 
            // mnuOpen
            // 
            mnuOpen.Name = "mnuOpen";
            mnuOpen.Size = new Size(123, 22);
            mnuOpen.Text = "&Open";
            // 
            // toolStripSeparator3
            // 
            toolStripSeparator3.Name = "toolStripSeparator3";
            toolStripSeparator3.Size = new Size(120, 6);
            // 
            // mnuClose
            // 
            mnuClose.Name = "mnuClose";
            mnuClose.Size = new Size(123, 22);
            mnuClose.Text = "Close";
            // 
            // toolStripSeparator2
            // 
            toolStripSeparator2.Name = "toolStripSeparator2";
            toolStripSeparator2.Size = new Size(120, 6);
            // 
            // mnuSave
            // 
            mnuSave.Name = "mnuSave";
            mnuSave.Size = new Size(123, 22);
            mnuSave.Text = "Save";
            // 
            // mnuSaveAs
            // 
            mnuSaveAs.Name = "mnuSaveAs";
            mnuSaveAs.Size = new Size(123, 22);
            mnuSaveAs.Text = "Save As...";
            // 
            // toolStripSeparator1
            // 
            toolStripSeparator1.Name = "toolStripSeparator1";
            toolStripSeparator1.Size = new Size(120, 6);
            // 
            // mnuExit
            // 
            mnuExit.Name = "mnuExit";
            mnuExit.Size = new Size(123, 22);
            mnuExit.Text = "Exit";
            // 
            // edutToolStripMenuItem
            // 
            edutToolStripMenuItem.DropDownItems.AddRange(new ToolStripItem[] { mnuUndo, mnuRedo, toolStripSeparator4, mnuCut, mnuCopy, mnuPaste });
            edutToolStripMenuItem.Name = "edutToolStripMenuItem";
            edutToolStripMenuItem.Size = new Size(39, 20);
            edutToolStripMenuItem.Text = "&Edit";
            // 
            // mnuUndo
            // 
            mnuUndo.Name = "mnuUndo";
            mnuUndo.Size = new Size(103, 22);
            mnuUndo.Text = "Undo";
            // 
            // mnuRedo
            // 
            mnuRedo.Name = "mnuRedo";
            mnuRedo.Size = new Size(103, 22);
            mnuRedo.Text = "Redo";
            // 
            // toolStripSeparator4
            // 
            toolStripSeparator4.Name = "toolStripSeparator4";
            toolStripSeparator4.Size = new Size(100, 6);
            // 
            // mnuCut
            // 
            mnuCut.Name = "mnuCut";
            mnuCut.Size = new Size(103, 22);
            mnuCut.Text = "Cut";
            // 
            // mnuCopy
            // 
            mnuCopy.Name = "mnuCopy";
            mnuCopy.Size = new Size(103, 22);
            mnuCopy.Text = "Copy";
            // 
            // mnuPaste
            // 
            mnuPaste.Name = "mnuPaste";
            mnuPaste.Size = new Size(103, 22);
            mnuPaste.Text = "Paste";
            // 
            // toolsToolStripMenuItem
            // 
            toolsToolStripMenuItem.DropDownItems.AddRange(new ToolStripItem[] { manageServersToolStripMenuItem, manageFunctionsToolStripMenuItem, exportTipsToolStripMenuItem, autoAddServersToolStripMenuItem, optimizeFileToolStripMenuItem, settingsToolStripMenuItem });
            toolsToolStripMenuItem.Name = "toolsToolStripMenuItem";
            toolsToolStripMenuItem.Size = new Size(46, 20);
            toolsToolStripMenuItem.Text = "&Tools";
            // 
            // manageServersToolStripMenuItem
            // 
            manageServersToolStripMenuItem.Name = "manageServersToolStripMenuItem";
            manageServersToolStripMenuItem.Size = new Size(181, 22);
            manageServersToolStripMenuItem.Text = "Manage Servers...";
            // 
            // manageFunctionsToolStripMenuItem
            // 
            manageFunctionsToolStripMenuItem.Name = "manageFunctionsToolStripMenuItem";
            manageFunctionsToolStripMenuItem.Size = new Size(181, 22);
            manageFunctionsToolStripMenuItem.Text = "Manage Functions...";
            // 
            // exportTipsToolStripMenuItem
            // 
            exportTipsToolStripMenuItem.Name = "exportTipsToolStripMenuItem";
            exportTipsToolStripMenuItem.Size = new Size(181, 22);
            exportTipsToolStripMenuItem.Text = "Export Tips...";
            // 
            // autoAddServersToolStripMenuItem
            // 
            autoAddServersToolStripMenuItem.Name = "autoAddServersToolStripMenuItem";
            autoAddServersToolStripMenuItem.Size = new Size(181, 22);
            autoAddServersToolStripMenuItem.Text = "Auto Add Servers...";
            // 
            // optimizeFileToolStripMenuItem
            // 
            optimizeFileToolStripMenuItem.Name = "optimizeFileToolStripMenuItem";
            optimizeFileToolStripMenuItem.Size = new Size(181, 22);
            optimizeFileToolStripMenuItem.Text = "Optimize File...";
            // 
            // settingsToolStripMenuItem
            // 
            settingsToolStripMenuItem.Name = "settingsToolStripMenuItem";
            settingsToolStripMenuItem.Size = new Size(181, 22);
            settingsToolStripMenuItem.Text = "Settings..";
            // 
            // reportsToolStripMenuItem
            // 
            reportsToolStripMenuItem.DropDownItems.AddRange(new ToolStripItem[] { tipChitsToolStripMenuItem, tipReportsToolStripMenuItem, specialFunctionDetailToolStripMenuItem, payrollBalancingReportToolStripMenuItem, serverListToolStripMenuItem });
            reportsToolStripMenuItem.Name = "reportsToolStripMenuItem";
            reportsToolStripMenuItem.Size = new Size(59, 20);
            reportsToolStripMenuItem.Text = "&Reports";
            // 
            // tipChitsToolStripMenuItem
            // 
            tipChitsToolStripMenuItem.Name = "tipChitsToolStripMenuItem";
            tipChitsToolStripMenuItem.Size = new Size(203, 22);
            tipChitsToolStripMenuItem.Text = "Tip Chits";
            // 
            // tipReportsToolStripMenuItem
            // 
            tipReportsToolStripMenuItem.Name = "tipReportsToolStripMenuItem";
            tipReportsToolStripMenuItem.Size = new Size(203, 22);
            tipReportsToolStripMenuItem.Text = "Tip Reports";
            // 
            // specialFunctionDetailToolStripMenuItem
            // 
            specialFunctionDetailToolStripMenuItem.Name = "specialFunctionDetailToolStripMenuItem";
            specialFunctionDetailToolStripMenuItem.Size = new Size(203, 22);
            specialFunctionDetailToolStripMenuItem.Text = "Special Function Detail";
            // 
            // payrollBalancingReportToolStripMenuItem
            // 
            payrollBalancingReportToolStripMenuItem.Name = "payrollBalancingReportToolStripMenuItem";
            payrollBalancingReportToolStripMenuItem.Size = new Size(203, 22);
            payrollBalancingReportToolStripMenuItem.Text = "Payroll Balancing Report";
            // 
            // serverListToolStripMenuItem
            // 
            serverListToolStripMenuItem.Name = "serverListToolStripMenuItem";
            serverListToolStripMenuItem.Size = new Size(203, 22);
            serverListToolStripMenuItem.Text = "Server List";
            // 
            // helpToolStripMenuItem
            // 
            helpToolStripMenuItem.DropDownItems.AddRange(new ToolStripItem[] { contentsToolStripMenuItem, indexToolStripMenuItem, toolStripSeparator5, aboutTripTrackerToolStripMenuItem });
            helpToolStripMenuItem.Name = "helpToolStripMenuItem";
            helpToolStripMenuItem.Size = new Size(44, 20);
            helpToolStripMenuItem.Text = "Help";
            // 
            // contentsToolStripMenuItem
            // 
            contentsToolStripMenuItem.Name = "contentsToolStripMenuItem";
            contentsToolStripMenuItem.Size = new Size(169, 22);
            contentsToolStripMenuItem.Text = "Contents";
            // 
            // indexToolStripMenuItem
            // 
            indexToolStripMenuItem.Name = "indexToolStripMenuItem";
            indexToolStripMenuItem.Size = new Size(169, 22);
            indexToolStripMenuItem.Text = "Index";
            // 
            // toolStripSeparator5
            // 
            toolStripSeparator5.Name = "toolStripSeparator5";
            toolStripSeparator5.Size = new Size(166, 6);
            // 
            // aboutTripTrackerToolStripMenuItem
            // 
            aboutTripTrackerToolStripMenuItem.Name = "aboutTripTrackerToolStripMenuItem";
            aboutTripTrackerToolStripMenuItem.Size = new Size(169, 22);
            aboutTripTrackerToolStripMenuItem.Text = "About Trip Tracker";
            // 
            // strMainStatusBar
            // 
            strMainStatusBar.Items.AddRange(new ToolStripItem[] { lblBusinessDate, lblPeriodDates });
            strMainStatusBar.Location = new Point(0, 707);
            strMainStatusBar.Name = "strMainStatusBar";
            strMainStatusBar.Size = new Size(1008, 22);
            strMainStatusBar.TabIndex = 1;
            strMainStatusBar.Text = "statusStrip1";
            // 
            // lblBusinessDate
            // 
            lblBusinessDate.Name = "lblBusinessDate";
            lblBusinessDate.Size = new Size(143, 17);
            lblBusinessDate.Text = "Business Date: 00/00/0000";
            // 
            // lblPeriodDates
            // 
            lblPeriodDates.Name = "lblPeriodDates";
            lblPeriodDates.Size = new Size(196, 17);
            lblPeriodDates.Text = "Pay Period: 00/00/0000 - 00/00/0000";
            // 
            // spMainSplitContainer
            // 
            spMainSplitContainer.Dock = DockStyle.Fill;
            spMainSplitContainer.Location = new Point(0, 24);
            spMainSplitContainer.Name = "spMainSplitContainer";
            // 
            // spMainSplitContainer.Panel1
            // 
            spMainSplitContainer.Panel1.Controls.Add(tabControl1);
            // 
            // spMainSplitContainer.Panel2
            // 
            spMainSplitContainer.Panel2.Controls.Add(grpServerList);
            spMainSplitContainer.Panel2MinSize = 270;
            spMainSplitContainer.Size = new Size(1008, 683);
            spMainSplitContainer.SplitterDistance = 727;
            spMainSplitContainer.TabIndex = 2;
            // 
            // tabControl1
            // 
            tabControl1.Controls.Add(tabPage1);
            tabControl1.Dock = DockStyle.Fill;
            tabControl1.Location = new Point(0, 0);
            tabControl1.Name = "tabControl1";
            tabControl1.SelectedIndex = 0;
            tabControl1.Size = new Size(727, 683);
            tabControl1.TabIndex = 0;
            // 
            // tabPage1
            // 
            tabPage1.Controls.Add(tipEditor2);
            tabPage1.Location = new Point(4, 24);
            tabPage1.Name = "tabPage1";
            tabPage1.Padding = new Padding(3);
            tabPage1.Size = new Size(719, 655);
            tabPage1.TabIndex = 1;
            tabPage1.Text = "tabPage1";
            tabPage1.UseVisualStyleBackColor = true;
            // 
            // tipEditor2
            // 
            tipEditor2.Dock = DockStyle.Fill;
            tipEditor2.Location = new Point(3, 3);
            tipEditor2.Name = "tipEditor2";
            tipEditor2.Size = new Size(713, 649);
            tipEditor2.TabIndex = 0;
            // 
            // grpServerList
            // 
            grpServerList.Controls.Add(dgvServers);
            grpServerList.Dock = DockStyle.Fill;
            grpServerList.Location = new Point(0, 0);
            grpServerList.Name = "grpServerList";
            grpServerList.Size = new Size(277, 683);
            grpServerList.TabIndex = 0;
            grpServerList.TabStop = false;
            grpServerList.Text = "Servers";
            // 
            // dgvServers
            // 
            dgvServers.AllowUserToAddRows = false;
            dgvServers.AllowUserToDeleteRows = false;
            dgvServers.AllowUserToResizeColumns = false;
            dgvServers.AllowUserToResizeRows = false;
            dgvServers.AutoGenerateColumns = false;
            dgvServers.BackgroundColor = SystemColors.Control;
            dgvServers.BorderStyle = BorderStyle.Fixed3D;
            dgvServers.ColumnHeadersHeightSizeMode = DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            dgvServers.Columns.AddRange(new DataGridViewColumn[] { Number, lastNameDataGridViewTextBoxColumn, firstNameDataGridViewTextBoxColumn });
            dgvServers.DataSource = serverListBindingSource;
            dgvServers.Dock = DockStyle.Fill;
            dgvServers.EditMode = DataGridViewEditMode.EditProgrammatically;
            dgvServers.Location = new Point(3, 19);
            dgvServers.MultiSelect = false;
            dgvServers.Name = "dgvServers";
            dgvServers.ReadOnly = true;
            dgvServers.RowHeadersVisible = false;
            dgvServers.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
            dgvServers.ShowCellErrors = false;
            dgvServers.ShowCellToolTips = false;
            dgvServers.ShowEditingIcon = false;
            dgvServers.ShowRowErrors = false;
            dgvServers.Size = new Size(271, 661);
            dgvServers.TabIndex = 0;
            // 
            // serverListBindingSource
            // 
            serverListBindingSource.DataSource = typeof(ViewData.ServerView);
            // 
            // Number
            // 
            Number.AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill;
            Number.DataPropertyName = "Number";
            Number.FillWeight = 25F;
            Number.HeaderText = "No";
            Number.Name = "Number";
            Number.ReadOnly = true;
            // 
            // lastNameDataGridViewTextBoxColumn
            // 
            lastNameDataGridViewTextBoxColumn.AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill;
            lastNameDataGridViewTextBoxColumn.DataPropertyName = "LastName";
            lastNameDataGridViewTextBoxColumn.FillWeight = 40F;
            lastNameDataGridViewTextBoxColumn.HeaderText = "Last Name";
            lastNameDataGridViewTextBoxColumn.Name = "lastNameDataGridViewTextBoxColumn";
            lastNameDataGridViewTextBoxColumn.ReadOnly = true;
            // 
            // firstNameDataGridViewTextBoxColumn
            // 
            firstNameDataGridViewTextBoxColumn.AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill;
            firstNameDataGridViewTextBoxColumn.DataPropertyName = "FirstName";
            firstNameDataGridViewTextBoxColumn.FillWeight = 35F;
            firstNameDataGridViewTextBoxColumn.HeaderText = "First Name";
            firstNameDataGridViewTextBoxColumn.Name = "firstNameDataGridViewTextBoxColumn";
            firstNameDataGridViewTextBoxColumn.ReadOnly = true;
            // 
            // MainForm
            // 
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(1008, 729);
            Controls.Add(spMainSplitContainer);
            Controls.Add(strMainStatusBar);
            Controls.Add(mnuMainMenu);
            MainMenuStrip = mnuMainMenu;
            MinimumSize = new Size(1024, 768);
            Name = "MainForm";
            Text = "Tip Tracker";
            mnuMainMenu.ResumeLayout(false);
            mnuMainMenu.PerformLayout();
            strMainStatusBar.ResumeLayout(false);
            strMainStatusBar.PerformLayout();
            spMainSplitContainer.Panel1.ResumeLayout(false);
            spMainSplitContainer.Panel2.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)spMainSplitContainer).EndInit();
            spMainSplitContainer.ResumeLayout(false);
            tabControl1.ResumeLayout(false);
            tabPage1.ResumeLayout(false);
            grpServerList.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)dgvServers).EndInit();
            ((System.ComponentModel.ISupportInitialize)serverListBindingSource).EndInit();
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion

        private MenuStrip mnuMainMenu;
        private ToolStripMenuItem mnuFile;
        private StatusStrip strMainStatusBar;
        private ToolStripMenuItem mnuNew;
        private ToolStripMenuItem mnuOpen;
        private ToolStripSeparator toolStripSeparator3;
        private ToolStripMenuItem mnuClose;
        private ToolStripSeparator toolStripSeparator2;
        private ToolStripMenuItem mnuSave;
        private ToolStripMenuItem mnuSaveAs;
        private ToolStripSeparator toolStripSeparator1;
        private ToolStripMenuItem mnuExit;
        private ToolStripMenuItem edutToolStripMenuItem;
        private ToolStripMenuItem mnuUndo;
        private ToolStripMenuItem mnuRedo;
        private ToolStripMenuItem mnuCut;
        private ToolStripMenuItem mnuCopy;
        private ToolStripMenuItem mnuPaste;
        private ToolStripMenuItem toolsToolStripMenuItem;
        private ToolStripMenuItem manageServersToolStripMenuItem;
        private ToolStripMenuItem manageFunctionsToolStripMenuItem;
        private ToolStripMenuItem exportTipsToolStripMenuItem;
        private ToolStripMenuItem autoAddServersToolStripMenuItem;
        private ToolStripMenuItem optimizeFileToolStripMenuItem;
        private ToolStripMenuItem settingsToolStripMenuItem;
        private ToolStripMenuItem reportsToolStripMenuItem;
        private ToolStripMenuItem tipChitsToolStripMenuItem;
        private ToolStripMenuItem tipReportsToolStripMenuItem;
        private ToolStripMenuItem specialFunctionDetailToolStripMenuItem;
        private ToolStripMenuItem payrollBalancingReportToolStripMenuItem;
        private ToolStripMenuItem serverListToolStripMenuItem;
        private ToolStripMenuItem helpToolStripMenuItem;
        private ToolStripMenuItem contentsToolStripMenuItem;
        private ToolStripMenuItem indexToolStripMenuItem;
        private ToolStripMenuItem aboutTripTrackerToolStripMenuItem;
        private ToolStripStatusLabel lblBusinessDate;
        private ToolStripStatusLabel lblPeriodDates;
        private ToolStripSeparator toolStripSeparator4;
        private SplitContainer spMainSplitContainer;
        private GroupBox grpServerList;
        private DataGridView dgvServers;
        private ToolStripSeparator toolStripSeparator5;
        private TabControl tabControl1;
        private TabPage tabPage1;
        private Controls.QuickTipEditor tipEditor2;
        private DataGridViewTextBoxColumn idDataGridViewTextBoxColumn;
        private BindingSource serverListBindingSource;
        private DataGridViewTextBoxColumn Number;
        private DataGridViewTextBoxColumn lastNameDataGridViewTextBoxColumn;
        private DataGridViewTextBoxColumn firstNameDataGridViewTextBoxColumn;
    }
}
