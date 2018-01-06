<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class frmMain
    Inherits System.Windows.Forms.Form

    'Form overrides dispose to clean up the component list.
    <System.Diagnostics.DebuggerNonUserCode()> _
    Protected Overrides Sub Dispose(ByVal disposing As Boolean)
        If disposing AndAlso components IsNot Nothing Then
            components.Dispose()
        End If
        MyBase.Dispose(disposing)
    End Sub

    'Required by the Windows Form Designer
    Private components As System.ComponentModel.IContainer

    'NOTE: The following procedure is required by the Windows Form Designer
    'It can be modified using the Windows Form Designer.  
    'Do not modify it using the code editor.
    <System.Diagnostics.DebuggerStepThrough()> _
    Private Sub InitializeComponent()
        Me.components = New System.ComponentModel.Container
        Dim DataGridViewCellStyle1 As System.Windows.Forms.DataGridViewCellStyle = New System.Windows.Forms.DataGridViewCellStyle
        Dim resources As System.ComponentModel.ComponentResourceManager = New System.ComponentModel.ComponentResourceManager(GetType(frmMain))
        Me.strMainMenu = New System.Windows.Forms.MenuStrip
        Me.FileToolStripMenuItem = New System.Windows.Forms.ToolStripMenuItem
        Me.mnuNew = New System.Windows.Forms.ToolStripMenuItem
        Me.mnuOpen = New System.Windows.Forms.ToolStripMenuItem
        Me.ToolStripSeparator2 = New System.Windows.Forms.ToolStripSeparator
        Me.mnuClose = New System.Windows.Forms.ToolStripMenuItem
        Me.ToolStripSeparator4 = New System.Windows.Forms.ToolStripSeparator
        Me.mnuSave = New System.Windows.Forms.ToolStripMenuItem
        Me.mnuSaveAs = New System.Windows.Forms.ToolStripMenuItem
        Me.ToolStripSeparator3 = New System.Windows.Forms.ToolStripSeparator
        Me.mnuExit = New System.Windows.Forms.ToolStripMenuItem
        Me.mnuView = New System.Windows.Forms.ToolStripMenuItem
        Me.mnuShowAllTips = New System.Windows.Forms.ToolStripMenuItem
        Me.ToolStripSeparator7 = New System.Windows.Forms.ToolStripSeparator
        Me.mnuManageSpecialFunctions = New System.Windows.Forms.ToolStripMenuItem
        Me.mnuReports = New System.Windows.Forms.ToolStripMenuItem
        Me.mnuReportByServer = New System.Windows.Forms.ToolStripMenuItem
        Me.mnuReportByFunction = New System.Windows.Forms.ToolStripMenuItem
        Me.mnuTools = New System.Windows.Forms.ToolStripMenuItem
        Me.mnuManageTemplateServers = New System.Windows.Forms.ToolStripMenuItem
        Me.mnuImportServerList = New System.Windows.Forms.ToolStripMenuItem
        Me.ToolStripSeparator6 = New System.Windows.Forms.ToolStripSeparator
        Me.mnuManageUsers = New System.Windows.Forms.ToolStripMenuItem
        Me.mnuHelp = New System.Windows.Forms.ToolStripMenuItem
        Me.mnuContents = New System.Windows.Forms.ToolStripMenuItem
        Me.mnuIndex = New System.Windows.Forms.ToolStripMenuItem
        Me.ToolStripSeparator1 = New System.Windows.Forms.ToolStripSeparator
        Me.mnuAbout = New System.Windows.Forms.ToolStripMenuItem
        Me.grpServers = New System.Windows.Forms.GroupBox
        Me.ServersDataGridView = New System.Windows.Forms.DataGridView
        Me.ServersServerNumber = New System.Windows.Forms.DataGridViewTextBoxColumn
        Me.ServersFirstName = New System.Windows.Forms.DataGridViewTextBoxColumn
        Me.ServersLastName = New System.Windows.Forms.DataGridViewTextBoxColumn
        Me.mnuServersContextMenu = New System.Windows.Forms.ContextMenuStrip(Me.components)
        Me.mnuAddServer = New System.Windows.Forms.ToolStripMenuItem
        Me.mnuEditSelectedServer = New System.Windows.Forms.ToolStripMenuItem
        Me.ToolStripSeparator5 = New System.Windows.Forms.ToolStripSeparator
        Me.mnuShowTipsForServer = New System.Windows.Forms.ToolStripMenuItem
        Me.ServersBindingSource = New System.Windows.Forms.BindingSource(Me.components)
        Me.FileDataSet = New Tip_Tracker_SF.FileDataSet
        Me.pnlTipEntryPanel = New System.Windows.Forms.Panel
        Me.lblSelectSpecialFunction = New System.Windows.Forms.Label
        Me.cboSelectSpecialFunction = New System.Windows.Forms.ComboBox
        Me.SpecialFunctionsBindingSource = New System.Windows.Forms.BindingSource(Me.components)
        Me.SpecialFunctionDataGridView = New System.Windows.Forms.DataGridView
        Me.SFID = New System.Windows.Forms.DataGridViewTextBoxColumn
        Me.SFAmount = New System.Windows.Forms.DataGridViewTextBoxColumn
        Me.SFServerNumber = New System.Windows.Forms.DataGridViewTextBoxColumn
        Me.SFFirstName = New System.Windows.Forms.DataGridViewTextBoxColumn
        Me.SFLastName = New System.Windows.Forms.DataGridViewTextBoxColumn
        Me.SFSpecialFunction = New System.Windows.Forms.DataGridViewTextBoxColumn
        Me.mnuSFContextMenu = New System.Windows.Forms.ContextMenuStrip(Me.components)
        Me.mnuDeleteSFTip = New System.Windows.Forms.ToolStripMenuItem
        Me.SpecialFunctionTipsBindingSource = New System.Windows.Forms.BindingSource(Me.components)
        Me.btnClearSF = New System.Windows.Forms.Button
        Me.btnAddSF = New System.Windows.Forms.Button
        Me.lblSFTotal = New System.Windows.Forms.Label
        Me.lblSFAmount = New System.Windows.Forms.Label
        Me.lblSFServerNumber = New System.Windows.Forms.Label
        Me.txtSFAmount = New System.Windows.Forms.TextBox
        Me.txtSFServerNumber = New System.Windows.Forms.TextBox
        Me.txtSFServerName = New System.Windows.Forms.TextBox
        Me.CreditCardDataGridView = New System.Windows.Forms.DataGridView
        Me.CCID = New System.Windows.Forms.DataGridViewTextBoxColumn
        Me.CCAmount = New System.Windows.Forms.DataGridViewTextBoxColumn
        Me.CCServerNumber = New System.Windows.Forms.DataGridViewTextBoxColumn
        Me.CCFirstName = New System.Windows.Forms.DataGridViewTextBoxColumn
        Me.CCLastName = New System.Windows.Forms.DataGridViewTextBoxColumn
        Me.btnClearCC = New System.Windows.Forms.Button
        Me.btnAddCC = New System.Windows.Forms.Button
        Me.lblCCTotal = New System.Windows.Forms.Label
        Me.lblCCAmount = New System.Windows.Forms.Label
        Me.lblCCServerNumber = New System.Windows.Forms.Label
        Me.txtCCAmount = New System.Windows.Forms.TextBox
        Me.txtCCServerNumber = New System.Windows.Forms.TextBox
        Me.txtCCServerName = New System.Windows.Forms.TextBox
        Me.dlgOpenFile = New System.Windows.Forms.OpenFileDialog
        Me.dlgSaveFile = New System.Windows.Forms.SaveFileDialog
        Me.GlobalDataSet = New Tip_Tracker_SF.GlobalDataSet
        Me.strMainMenu.SuspendLayout()
        Me.grpServers.SuspendLayout()
        CType(Me.ServersDataGridView, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.mnuServersContextMenu.SuspendLayout()
        CType(Me.ServersBindingSource, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.FileDataSet, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.pnlTipEntryPanel.SuspendLayout()
        CType(Me.SpecialFunctionsBindingSource, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.SpecialFunctionDataGridView, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.mnuSFContextMenu.SuspendLayout()
        CType(Me.SpecialFunctionTipsBindingSource, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.CreditCardDataGridView, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.GlobalDataSet, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.SuspendLayout()
        '
        'strMainMenu
        '
        Me.strMainMenu.Items.AddRange(New System.Windows.Forms.ToolStripItem() {Me.FileToolStripMenuItem, Me.mnuView, Me.mnuTools, Me.mnuHelp})
        Me.strMainMenu.Location = New System.Drawing.Point(0, 0)
        Me.strMainMenu.Name = "strMainMenu"
        Me.strMainMenu.Size = New System.Drawing.Size(632, 24)
        Me.strMainMenu.TabIndex = 0
        Me.strMainMenu.Text = "MenuStrip1"
        '
        'FileToolStripMenuItem
        '
        Me.FileToolStripMenuItem.DropDownItems.AddRange(New System.Windows.Forms.ToolStripItem() {Me.mnuNew, Me.mnuOpen, Me.ToolStripSeparator2, Me.mnuClose, Me.ToolStripSeparator4, Me.mnuSave, Me.mnuSaveAs, Me.ToolStripSeparator3, Me.mnuExit})
        Me.FileToolStripMenuItem.Name = "FileToolStripMenuItem"
        Me.FileToolStripMenuItem.Size = New System.Drawing.Size(35, 20)
        Me.FileToolStripMenuItem.Text = "&File"
        '
        'mnuNew
        '
        Me.mnuNew.Name = "mnuNew"
        Me.mnuNew.Size = New System.Drawing.Size(126, 22)
        Me.mnuNew.Text = "New"
        '
        'mnuOpen
        '
        Me.mnuOpen.Name = "mnuOpen"
        Me.mnuOpen.Size = New System.Drawing.Size(126, 22)
        Me.mnuOpen.Text = "Open..."
        '
        'ToolStripSeparator2
        '
        Me.ToolStripSeparator2.Name = "ToolStripSeparator2"
        Me.ToolStripSeparator2.Size = New System.Drawing.Size(123, 6)
        '
        'mnuClose
        '
        Me.mnuClose.Name = "mnuClose"
        Me.mnuClose.Size = New System.Drawing.Size(126, 22)
        Me.mnuClose.Text = "Close"
        '
        'ToolStripSeparator4
        '
        Me.ToolStripSeparator4.Name = "ToolStripSeparator4"
        Me.ToolStripSeparator4.Size = New System.Drawing.Size(123, 6)
        '
        'mnuSave
        '
        Me.mnuSave.Name = "mnuSave"
        Me.mnuSave.Size = New System.Drawing.Size(126, 22)
        Me.mnuSave.Text = "Save"
        '
        'mnuSaveAs
        '
        Me.mnuSaveAs.Name = "mnuSaveAs"
        Me.mnuSaveAs.Size = New System.Drawing.Size(126, 22)
        Me.mnuSaveAs.Text = "Save As..."
        '
        'ToolStripSeparator3
        '
        Me.ToolStripSeparator3.Name = "ToolStripSeparator3"
        Me.ToolStripSeparator3.Size = New System.Drawing.Size(123, 6)
        '
        'mnuExit
        '
        Me.mnuExit.Name = "mnuExit"
        Me.mnuExit.Size = New System.Drawing.Size(126, 22)
        Me.mnuExit.Text = "Exit"
        '
        'mnuView
        '
        Me.mnuView.DropDownItems.AddRange(New System.Windows.Forms.ToolStripItem() {Me.mnuShowAllTips, Me.ToolStripSeparator7, Me.mnuManageSpecialFunctions, Me.mnuReports})
        Me.mnuView.Name = "mnuView"
        Me.mnuView.Size = New System.Drawing.Size(42, 20)
        Me.mnuView.Text = "&View"
        '
        'mnuShowAllTips
        '
        Me.mnuShowAllTips.Name = "mnuShowAllTips"
        Me.mnuShowAllTips.Size = New System.Drawing.Size(170, 22)
        Me.mnuShowAllTips.Text = "Show All Tips"
        '
        'ToolStripSeparator7
        '
        Me.ToolStripSeparator7.Name = "ToolStripSeparator7"
        Me.ToolStripSeparator7.Size = New System.Drawing.Size(167, 6)
        '
        'mnuManageSpecialFunctions
        '
        Me.mnuManageSpecialFunctions.Name = "mnuManageSpecialFunctions"
        Me.mnuManageSpecialFunctions.Size = New System.Drawing.Size(170, 22)
        Me.mnuManageSpecialFunctions.Text = "Special Functions..."
        '
        'mnuReports
        '
        Me.mnuReports.DropDownItems.AddRange(New System.Windows.Forms.ToolStripItem() {Me.mnuReportByServer, Me.mnuReportByFunction})
        Me.mnuReports.Name = "mnuReports"
        Me.mnuReports.Size = New System.Drawing.Size(170, 22)
        Me.mnuReports.Text = "Reports"
        '
        'mnuReportByServer
        '
        Me.mnuReportByServer.Name = "mnuReportByServer"
        Me.mnuReportByServer.Size = New System.Drawing.Size(133, 22)
        Me.mnuReportByServer.Text = "By Server"
        '
        'mnuReportByFunction
        '
        Me.mnuReportByFunction.Name = "mnuReportByFunction"
        Me.mnuReportByFunction.Size = New System.Drawing.Size(133, 22)
        Me.mnuReportByFunction.Text = "By Function"
        '
        'mnuTools
        '
        Me.mnuTools.DropDownItems.AddRange(New System.Windows.Forms.ToolStripItem() {Me.mnuManageTemplateServers, Me.mnuImportServerList, Me.ToolStripSeparator6, Me.mnuManageUsers})
        Me.mnuTools.Name = "mnuTools"
        Me.mnuTools.Size = New System.Drawing.Size(45, 20)
        Me.mnuTools.Text = "&Tools"
        '
        'mnuManageTemplateServers
        '
        Me.mnuManageTemplateServers.Name = "mnuManageTemplateServers"
        Me.mnuManageTemplateServers.Size = New System.Drawing.Size(211, 22)
        Me.mnuManageTemplateServers.Text = "Manage Template Servers..."
        '
        'mnuImportServerList
        '
        Me.mnuImportServerList.Name = "mnuImportServerList"
        Me.mnuImportServerList.Size = New System.Drawing.Size(211, 22)
        Me.mnuImportServerList.Text = "Import Server List"
        '
        'ToolStripSeparator6
        '
        Me.ToolStripSeparator6.Name = "ToolStripSeparator6"
        Me.ToolStripSeparator6.Size = New System.Drawing.Size(208, 6)
        '
        'mnuManageUsers
        '
        Me.mnuManageUsers.Name = "mnuManageUsers"
        Me.mnuManageUsers.Size = New System.Drawing.Size(211, 22)
        Me.mnuManageUsers.Text = "Manage Users..."
        '
        'mnuHelp
        '
        Me.mnuHelp.DropDownItems.AddRange(New System.Windows.Forms.ToolStripItem() {Me.mnuContents, Me.mnuIndex, Me.ToolStripSeparator1, Me.mnuAbout})
        Me.mnuHelp.Name = "mnuHelp"
        Me.mnuHelp.Size = New System.Drawing.Size(41, 20)
        Me.mnuHelp.Text = "&Help"
        '
        'mnuContents
        '
        Me.mnuContents.Name = "mnuContents"
        Me.mnuContents.Size = New System.Drawing.Size(179, 22)
        Me.mnuContents.Text = "Contents"
        Me.mnuContents.Visible = False
        '
        'mnuIndex
        '
        Me.mnuIndex.Name = "mnuIndex"
        Me.mnuIndex.Size = New System.Drawing.Size(179, 22)
        Me.mnuIndex.Text = "Index"
        Me.mnuIndex.Visible = False
        '
        'ToolStripSeparator1
        '
        Me.ToolStripSeparator1.Name = "ToolStripSeparator1"
        Me.ToolStripSeparator1.Size = New System.Drawing.Size(176, 6)
        Me.ToolStripSeparator1.Visible = False
        '
        'mnuAbout
        '
        Me.mnuAbout.Name = "mnuAbout"
        Me.mnuAbout.Size = New System.Drawing.Size(179, 22)
        Me.mnuAbout.Text = "About Tip Tracker SF"
        '
        'grpServers
        '
        Me.grpServers.Anchor = CType(((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Bottom) _
                    Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.grpServers.Controls.Add(Me.ServersDataGridView)
        Me.grpServers.Location = New System.Drawing.Point(427, 27)
        Me.grpServers.Name = "grpServers"
        Me.grpServers.Size = New System.Drawing.Size(193, 414)
        Me.grpServers.TabIndex = 2
        Me.grpServers.TabStop = False
        Me.grpServers.Text = "Servers"
        '
        'ServersDataGridView
        '
        Me.ServersDataGridView.AllowUserToAddRows = False
        Me.ServersDataGridView.AllowUserToDeleteRows = False
        Me.ServersDataGridView.AllowUserToResizeColumns = False
        Me.ServersDataGridView.AllowUserToResizeRows = False
        Me.ServersDataGridView.AutoGenerateColumns = False
        Me.ServersDataGridView.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.DisableResizing
        Me.ServersDataGridView.Columns.AddRange(New System.Windows.Forms.DataGridViewColumn() {Me.ServersServerNumber, Me.ServersFirstName, Me.ServersLastName})
        Me.ServersDataGridView.ContextMenuStrip = Me.mnuServersContextMenu
        Me.ServersDataGridView.DataSource = Me.ServersBindingSource
        Me.ServersDataGridView.Dock = System.Windows.Forms.DockStyle.Fill
        Me.ServersDataGridView.Location = New System.Drawing.Point(3, 16)
        Me.ServersDataGridView.MultiSelect = False
        Me.ServersDataGridView.Name = "ServersDataGridView"
        Me.ServersDataGridView.ReadOnly = True
        Me.ServersDataGridView.RowHeadersVisible = False
        Me.ServersDataGridView.RowHeadersWidthSizeMode = System.Windows.Forms.DataGridViewRowHeadersWidthSizeMode.DisableResizing
        Me.ServersDataGridView.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect
        Me.ServersDataGridView.Size = New System.Drawing.Size(187, 395)
        Me.ServersDataGridView.TabIndex = 0
        Me.ServersDataGridView.TabStop = False
        '
        'ServersServerNumber
        '
        Me.ServersServerNumber.DataPropertyName = "ServerNumber"
        Me.ServersServerNumber.HeaderText = "No."
        Me.ServersServerNumber.Name = "ServersServerNumber"
        Me.ServersServerNumber.ReadOnly = True
        Me.ServersServerNumber.Width = 50
        '
        'ServersFirstName
        '
        Me.ServersFirstName.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.Fill
        Me.ServersFirstName.DataPropertyName = "FirstName"
        Me.ServersFirstName.FillWeight = 90.0!
        Me.ServersFirstName.HeaderText = "First Name"
        Me.ServersFirstName.Name = "ServersFirstName"
        Me.ServersFirstName.ReadOnly = True
        '
        'ServersLastName
        '
        Me.ServersLastName.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.Fill
        Me.ServersLastName.DataPropertyName = "LastName"
        Me.ServersLastName.HeaderText = "Last Name"
        Me.ServersLastName.Name = "ServersLastName"
        Me.ServersLastName.ReadOnly = True
        '
        'mnuServersContextMenu
        '
        Me.mnuServersContextMenu.Items.AddRange(New System.Windows.Forms.ToolStripItem() {Me.mnuAddServer, Me.mnuEditSelectedServer, Me.ToolStripSeparator5, Me.mnuShowTipsForServer})
        Me.mnuServersContextMenu.Name = "mnuServersContextMenu"
        Me.mnuServersContextMenu.Size = New System.Drawing.Size(150, 76)
        '
        'mnuAddServer
        '
        Me.mnuAddServer.Name = "mnuAddServer"
        Me.mnuAddServer.Size = New System.Drawing.Size(149, 22)
        Me.mnuAddServer.Text = "Add..."
        '
        'mnuEditSelectedServer
        '
        Me.mnuEditSelectedServer.Name = "mnuEditSelectedServer"
        Me.mnuEditSelectedServer.Size = New System.Drawing.Size(149, 22)
        Me.mnuEditSelectedServer.Text = "Edit Selected..."
        '
        'ToolStripSeparator5
        '
        Me.ToolStripSeparator5.Name = "ToolStripSeparator5"
        Me.ToolStripSeparator5.Size = New System.Drawing.Size(146, 6)
        '
        'mnuShowTipsForServer
        '
        Me.mnuShowTipsForServer.Name = "mnuShowTipsForServer"
        Me.mnuShowTipsForServer.Size = New System.Drawing.Size(149, 22)
        Me.mnuShowTipsForServer.Text = "Show Tips"
        '
        'ServersBindingSource
        '
        Me.ServersBindingSource.DataMember = "Servers"
        Me.ServersBindingSource.DataSource = Me.FileDataSet
        '
        'FileDataSet
        '
        Me.FileDataSet.DataSetName = "FileDataSet"
        Me.FileDataSet.SchemaSerializationMode = System.Data.SchemaSerializationMode.IncludeSchema
        '
        'pnlTipEntryPanel
        '
        Me.pnlTipEntryPanel.Anchor = CType((((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Bottom) _
                    Or System.Windows.Forms.AnchorStyles.Left) _
                    Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.pnlTipEntryPanel.Controls.Add(Me.lblSelectSpecialFunction)
        Me.pnlTipEntryPanel.Controls.Add(Me.cboSelectSpecialFunction)
        Me.pnlTipEntryPanel.Controls.Add(Me.SpecialFunctionDataGridView)
        Me.pnlTipEntryPanel.Controls.Add(Me.btnClearSF)
        Me.pnlTipEntryPanel.Controls.Add(Me.btnAddSF)
        Me.pnlTipEntryPanel.Controls.Add(Me.lblSFTotal)
        Me.pnlTipEntryPanel.Controls.Add(Me.lblSFAmount)
        Me.pnlTipEntryPanel.Controls.Add(Me.lblSFServerNumber)
        Me.pnlTipEntryPanel.Controls.Add(Me.txtSFAmount)
        Me.pnlTipEntryPanel.Controls.Add(Me.txtSFServerNumber)
        Me.pnlTipEntryPanel.Controls.Add(Me.txtSFServerName)
        Me.pnlTipEntryPanel.Location = New System.Drawing.Point(12, 27)
        Me.pnlTipEntryPanel.Name = "pnlTipEntryPanel"
        Me.pnlTipEntryPanel.Size = New System.Drawing.Size(409, 414)
        Me.pnlTipEntryPanel.TabIndex = 3
        '
        'lblSelectSpecialFunction
        '
        Me.lblSelectSpecialFunction.AutoSize = True
        Me.lblSelectSpecialFunction.Location = New System.Drawing.Point(3, 3)
        Me.lblSelectSpecialFunction.Name = "lblSelectSpecialFunction"
        Me.lblSelectSpecialFunction.Size = New System.Drawing.Size(122, 13)
        Me.lblSelectSpecialFunction.TabIndex = 21
        Me.lblSelectSpecialFunction.Text = "Select Special Function:"
        '
        'cboSelectSpecialFunction
        '
        Me.cboSelectSpecialFunction.AutoCompleteMode = System.Windows.Forms.AutoCompleteMode.SuggestAppend
        Me.cboSelectSpecialFunction.AutoCompleteSource = System.Windows.Forms.AutoCompleteSource.ListItems
        Me.cboSelectSpecialFunction.DataSource = Me.SpecialFunctionsBindingSource
        Me.cboSelectSpecialFunction.DisplayMember = "SpecialFunction"
        Me.cboSelectSpecialFunction.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList
        Me.cboSelectSpecialFunction.FormattingEnabled = True
        Me.cboSelectSpecialFunction.Location = New System.Drawing.Point(3, 19)
        Me.cboSelectSpecialFunction.Name = "cboSelectSpecialFunction"
        Me.cboSelectSpecialFunction.Size = New System.Drawing.Size(160, 21)
        Me.cboSelectSpecialFunction.TabIndex = 20
        Me.cboSelectSpecialFunction.ValueMember = "SpecialFunction"
        '
        'SpecialFunctionsBindingSource
        '
        Me.SpecialFunctionsBindingSource.DataMember = "SpecialFunctions"
        Me.SpecialFunctionsBindingSource.DataSource = Me.FileDataSet
        '
        'SpecialFunctionDataGridView
        '
        Me.SpecialFunctionDataGridView.AllowUserToAddRows = False
        Me.SpecialFunctionDataGridView.AllowUserToDeleteRows = False
        Me.SpecialFunctionDataGridView.AllowUserToResizeColumns = False
        Me.SpecialFunctionDataGridView.AllowUserToResizeRows = False
        Me.SpecialFunctionDataGridView.Anchor = CType((((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Bottom) _
                    Or System.Windows.Forms.AnchorStyles.Left) _
                    Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.SpecialFunctionDataGridView.AutoGenerateColumns = False
        Me.SpecialFunctionDataGridView.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.DisableResizing
        Me.SpecialFunctionDataGridView.Columns.AddRange(New System.Windows.Forms.DataGridViewColumn() {Me.SFID, Me.SFAmount, Me.SFServerNumber, Me.SFFirstName, Me.SFLastName, Me.SFSpecialFunction})
        Me.SpecialFunctionDataGridView.ContextMenuStrip = Me.mnuSFContextMenu
        Me.SpecialFunctionDataGridView.DataSource = Me.SpecialFunctionTipsBindingSource
        Me.SpecialFunctionDataGridView.Location = New System.Drawing.Point(169, 3)
        Me.SpecialFunctionDataGridView.MultiSelect = False
        Me.SpecialFunctionDataGridView.Name = "SpecialFunctionDataGridView"
        Me.SpecialFunctionDataGridView.ReadOnly = True
        Me.SpecialFunctionDataGridView.RowHeadersVisible = False
        Me.SpecialFunctionDataGridView.RowHeadersWidthSizeMode = System.Windows.Forms.DataGridViewRowHeadersWidthSizeMode.DisableResizing
        Me.SpecialFunctionDataGridView.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect
        Me.SpecialFunctionDataGridView.Size = New System.Drawing.Size(237, 408)
        Me.SpecialFunctionDataGridView.TabIndex = 19
        Me.SpecialFunctionDataGridView.TabStop = False
        '
        'SFID
        '
        Me.SFID.DataPropertyName = "TipID"
        Me.SFID.HeaderText = "TipID"
        Me.SFID.Name = "SFID"
        Me.SFID.ReadOnly = True
        Me.SFID.Visible = False
        Me.SFID.Width = 40
        '
        'SFAmount
        '
        Me.SFAmount.DataPropertyName = "Amount"
        Me.SFAmount.HeaderText = "Amount"
        Me.SFAmount.Name = "SFAmount"
        Me.SFAmount.ReadOnly = True
        Me.SFAmount.Width = 50
        '
        'SFServerNumber
        '
        Me.SFServerNumber.DataPropertyName = "ServerNumber"
        Me.SFServerNumber.HeaderText = "No."
        Me.SFServerNumber.Name = "SFServerNumber"
        Me.SFServerNumber.ReadOnly = True
        Me.SFServerNumber.Width = 50
        '
        'SFFirstName
        '
        Me.SFFirstName.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.Fill
        Me.SFFirstName.DataPropertyName = "FirstName"
        Me.SFFirstName.HeaderText = "First Name"
        Me.SFFirstName.Name = "SFFirstName"
        Me.SFFirstName.ReadOnly = True
        '
        'SFLastName
        '
        Me.SFLastName.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.Fill
        Me.SFLastName.DataPropertyName = "LastName"
        Me.SFLastName.HeaderText = "Last Name"
        Me.SFLastName.Name = "SFLastName"
        Me.SFLastName.ReadOnly = True
        '
        'SFSpecialFunction
        '
        Me.SFSpecialFunction.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.Fill
        Me.SFSpecialFunction.DataPropertyName = "SpecialFunction"
        Me.SFSpecialFunction.HeaderText = "Special Function"
        Me.SFSpecialFunction.Name = "SFSpecialFunction"
        Me.SFSpecialFunction.ReadOnly = True
        '
        'mnuSFContextMenu
        '
        Me.mnuSFContextMenu.Items.AddRange(New System.Windows.Forms.ToolStripItem() {Me.mnuDeleteSFTip})
        Me.mnuSFContextMenu.Name = "mnuSFContextMenu"
        Me.mnuSFContextMenu.Size = New System.Drawing.Size(172, 26)
        '
        'mnuDeleteSFTip
        '
        Me.mnuDeleteSFTip.Name = "mnuDeleteSFTip"
        Me.mnuDeleteSFTip.Size = New System.Drawing.Size(171, 22)
        Me.mnuDeleteSFTip.Text = "Delete Selected Tip"
        '
        'SpecialFunctionTipsBindingSource
        '
        Me.SpecialFunctionTipsBindingSource.DataMember = "Tips"
        Me.SpecialFunctionTipsBindingSource.DataSource = Me.FileDataSet
        '
        'btnClearSF
        '
        Me.btnClearSF.Location = New System.Drawing.Point(102, 130)
        Me.btnClearSF.Name = "btnClearSF"
        Me.btnClearSF.Size = New System.Drawing.Size(61, 23)
        Me.btnClearSF.TabIndex = 14
        Me.btnClearSF.Text = "Clear"
        Me.btnClearSF.UseVisualStyleBackColor = True
        '
        'btnAddSF
        '
        Me.btnAddSF.Location = New System.Drawing.Point(35, 130)
        Me.btnAddSF.Name = "btnAddSF"
        Me.btnAddSF.Size = New System.Drawing.Size(61, 23)
        Me.btnAddSF.TabIndex = 13
        Me.btnAddSF.Text = "Add"
        Me.btnAddSF.UseVisualStyleBackColor = True
        '
        'lblSFTotal
        '
        Me.lblSFTotal.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Left), System.Windows.Forms.AnchorStyles)
        Me.lblSFTotal.AutoSize = True
        Me.lblSFTotal.Font = New System.Drawing.Font("Microsoft Sans Serif", 10.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblSFTotal.Location = New System.Drawing.Point(3, 394)
        Me.lblSFTotal.Name = "lblSFTotal"
        Me.lblSFTotal.Size = New System.Drawing.Size(96, 17)
        Me.lblSFTotal.TabIndex = 15
        Me.lblSFTotal.Text = "Total: $0.00"
        '
        'lblSFAmount
        '
        Me.lblSFAmount.AutoSize = True
        Me.lblSFAmount.Location = New System.Drawing.Point(3, 81)
        Me.lblSFAmount.Name = "lblSFAmount"
        Me.lblSFAmount.Size = New System.Drawing.Size(64, 13)
        Me.lblSFAmount.TabIndex = 17
        Me.lblSFAmount.Text = "Tip Amount:"
        '
        'lblSFServerNumber
        '
        Me.lblSFServerNumber.AutoSize = True
        Me.lblSFServerNumber.Location = New System.Drawing.Point(3, 55)
        Me.lblSFServerNumber.Name = "lblSFServerNumber"
        Me.lblSFServerNumber.Size = New System.Drawing.Size(81, 13)
        Me.lblSFServerNumber.TabIndex = 16
        Me.lblSFServerNumber.Text = "Server Number:"
        '
        'txtSFAmount
        '
        Me.txtSFAmount.Location = New System.Drawing.Point(102, 78)
        Me.txtSFAmount.MaxLength = 8
        Me.txtSFAmount.Name = "txtSFAmount"
        Me.txtSFAmount.Size = New System.Drawing.Size(61, 20)
        Me.txtSFAmount.TabIndex = 12
        '
        'txtSFServerNumber
        '
        Me.txtSFServerNumber.Location = New System.Drawing.Point(102, 52)
        Me.txtSFServerNumber.MaxLength = 10
        Me.txtSFServerNumber.Name = "txtSFServerNumber"
        Me.txtSFServerNumber.Size = New System.Drawing.Size(61, 20)
        Me.txtSFServerNumber.TabIndex = 11
        '
        'txtSFServerName
        '
        Me.txtSFServerName.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.txtSFServerName.Location = New System.Drawing.Point(3, 104)
        Me.txtSFServerName.Name = "txtSFServerName"
        Me.txtSFServerName.ReadOnly = True
        Me.txtSFServerName.Size = New System.Drawing.Size(160, 20)
        Me.txtSFServerName.TabIndex = 18
        Me.txtSFServerName.TabStop = False
        '
        'CreditCardDataGridView
        '
        Me.CreditCardDataGridView.AllowUserToAddRows = False
        Me.CreditCardDataGridView.AllowUserToDeleteRows = False
        Me.CreditCardDataGridView.AllowUserToResizeColumns = False
        Me.CreditCardDataGridView.AllowUserToResizeRows = False
        Me.CreditCardDataGridView.Anchor = CType((((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Bottom) _
                    Or System.Windows.Forms.AnchorStyles.Left) _
                    Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.CreditCardDataGridView.AutoGenerateColumns = False
        Me.CreditCardDataGridView.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.DisableResizing
        Me.CreditCardDataGridView.Columns.AddRange(New System.Windows.Forms.DataGridViewColumn() {Me.CCID, Me.CCAmount, Me.CCServerNumber, Me.CCFirstName, Me.CCLastName})
        Me.CreditCardDataGridView.Location = New System.Drawing.Point(172, 6)
        Me.CreditCardDataGridView.MultiSelect = False
        Me.CreditCardDataGridView.Name = "CreditCardDataGridView"
        Me.CreditCardDataGridView.ReadOnly = True
        Me.CreditCardDataGridView.RowHeadersVisible = False
        Me.CreditCardDataGridView.RowHeadersWidthSizeMode = System.Windows.Forms.DataGridViewRowHeadersWidthSizeMode.DisableResizing
        Me.CreditCardDataGridView.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect
        Me.CreditCardDataGridView.Size = New System.Drawing.Size(211, 350)
        Me.CreditCardDataGridView.TabIndex = 8
        Me.CreditCardDataGridView.TabStop = False
        '
        'CCID
        '
        Me.CCID.DataPropertyName = "TipID"
        Me.CCID.HeaderText = "ID"
        Me.CCID.Name = "CCID"
        Me.CCID.ReadOnly = True
        Me.CCID.Width = 40
        '
        'CCAmount
        '
        Me.CCAmount.DataPropertyName = "Amount"
        DataGridViewCellStyle1.Format = "C2"
        DataGridViewCellStyle1.NullValue = Nothing
        Me.CCAmount.DefaultCellStyle = DataGridViewCellStyle1
        Me.CCAmount.HeaderText = "Amount"
        Me.CCAmount.Name = "CCAmount"
        Me.CCAmount.ReadOnly = True
        Me.CCAmount.Width = 75
        '
        'CCServerNumber
        '
        Me.CCServerNumber.DataPropertyName = "ServerNumber"
        Me.CCServerNumber.HeaderText = "No."
        Me.CCServerNumber.Name = "CCServerNumber"
        Me.CCServerNumber.ReadOnly = True
        Me.CCServerNumber.Width = 50
        '
        'CCFirstName
        '
        Me.CCFirstName.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.Fill
        Me.CCFirstName.DataPropertyName = "FirstName"
        Me.CCFirstName.FillWeight = 90.0!
        Me.CCFirstName.HeaderText = "First Name"
        Me.CCFirstName.Name = "CCFirstName"
        Me.CCFirstName.ReadOnly = True
        '
        'CCLastName
        '
        Me.CCLastName.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.Fill
        Me.CCLastName.DataPropertyName = "LastName"
        Me.CCLastName.HeaderText = "Last Name"
        Me.CCLastName.Name = "CCLastName"
        Me.CCLastName.ReadOnly = True
        '
        'btnClearCC
        '
        Me.btnClearCC.Location = New System.Drawing.Point(105, 84)
        Me.btnClearCC.Name = "btnClearCC"
        Me.btnClearCC.Size = New System.Drawing.Size(61, 23)
        Me.btnClearCC.TabIndex = 3
        Me.btnClearCC.Text = "Clear"
        Me.btnClearCC.UseVisualStyleBackColor = True
        '
        'btnAddCC
        '
        Me.btnAddCC.Location = New System.Drawing.Point(38, 84)
        Me.btnAddCC.Name = "btnAddCC"
        Me.btnAddCC.Size = New System.Drawing.Size(61, 23)
        Me.btnAddCC.TabIndex = 2
        Me.btnAddCC.Text = "Add"
        Me.btnAddCC.UseVisualStyleBackColor = True
        '
        'lblCCTotal
        '
        Me.lblCCTotal.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Left), System.Windows.Forms.AnchorStyles)
        Me.lblCCTotal.AutoSize = True
        Me.lblCCTotal.Font = New System.Drawing.Font("Microsoft Sans Serif", 10.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblCCTotal.Location = New System.Drawing.Point(3, 339)
        Me.lblCCTotal.Name = "lblCCTotal"
        Me.lblCCTotal.Size = New System.Drawing.Size(96, 17)
        Me.lblCCTotal.TabIndex = 4
        Me.lblCCTotal.Text = "Total: $0.00"
        '
        'lblCCAmount
        '
        Me.lblCCAmount.AutoSize = True
        Me.lblCCAmount.Location = New System.Drawing.Point(6, 35)
        Me.lblCCAmount.Name = "lblCCAmount"
        Me.lblCCAmount.Size = New System.Drawing.Size(64, 13)
        Me.lblCCAmount.TabIndex = 6
        Me.lblCCAmount.Text = "Tip Amount:"
        '
        'lblCCServerNumber
        '
        Me.lblCCServerNumber.AutoSize = True
        Me.lblCCServerNumber.Location = New System.Drawing.Point(6, 9)
        Me.lblCCServerNumber.Name = "lblCCServerNumber"
        Me.lblCCServerNumber.Size = New System.Drawing.Size(81, 13)
        Me.lblCCServerNumber.TabIndex = 5
        Me.lblCCServerNumber.Text = "Server Number:"
        '
        'txtCCAmount
        '
        Me.txtCCAmount.Location = New System.Drawing.Point(105, 32)
        Me.txtCCAmount.MaxLength = 8
        Me.txtCCAmount.Name = "txtCCAmount"
        Me.txtCCAmount.Size = New System.Drawing.Size(61, 20)
        Me.txtCCAmount.TabIndex = 1
        '
        'txtCCServerNumber
        '
        Me.txtCCServerNumber.Location = New System.Drawing.Point(105, 6)
        Me.txtCCServerNumber.Name = "txtCCServerNumber"
        Me.txtCCServerNumber.Size = New System.Drawing.Size(61, 20)
        Me.txtCCServerNumber.TabIndex = 0
        '
        'txtCCServerName
        '
        Me.txtCCServerName.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.txtCCServerName.Location = New System.Drawing.Point(6, 58)
        Me.txtCCServerName.Name = "txtCCServerName"
        Me.txtCCServerName.ReadOnly = True
        Me.txtCCServerName.Size = New System.Drawing.Size(160, 20)
        Me.txtCCServerName.TabIndex = 7
        Me.txtCCServerName.TabStop = False
        '
        'dlgOpenFile
        '
        Me.dlgOpenFile.DefaultExt = "*.tsf"
        Me.dlgOpenFile.Filter = "Tip Tracker SF Data Files (*.tsf)|*.tsf"
        Me.dlgOpenFile.RestoreDirectory = True
        Me.dlgOpenFile.Title = "Open Data File"
        '
        'dlgSaveFile
        '
        Me.dlgSaveFile.DefaultExt = "*.tsf"
        Me.dlgSaveFile.Filter = "Tip Tracker SF Data Files (*.tsf)|*.tsf"
        Me.dlgSaveFile.RestoreDirectory = True
        Me.dlgSaveFile.Title = "Save Data File"
        '
        'GlobalDataSet
        '
        Me.GlobalDataSet.DataSetName = "GlobalDataSet"
        Me.GlobalDataSet.SchemaSerializationMode = System.Data.SchemaSerializationMode.IncludeSchema
        '
        'frmMain
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.ClientSize = New System.Drawing.Size(632, 453)
        Me.Controls.Add(Me.pnlTipEntryPanel)
        Me.Controls.Add(Me.grpServers)
        Me.Controls.Add(Me.strMainMenu)
        Me.Icon = CType(resources.GetObject("$this.Icon"), System.Drawing.Icon)
        Me.MainMenuStrip = Me.strMainMenu
        Me.MinimumSize = New System.Drawing.Size(640, 480)
        Me.Name = "frmMain"
        Me.Text = "Tip Tracker SF"
        Me.WindowState = System.Windows.Forms.FormWindowState.Maximized
        Me.strMainMenu.ResumeLayout(False)
        Me.strMainMenu.PerformLayout()
        Me.grpServers.ResumeLayout(False)
        CType(Me.ServersDataGridView, System.ComponentModel.ISupportInitialize).EndInit()
        Me.mnuServersContextMenu.ResumeLayout(False)
        CType(Me.ServersBindingSource, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.FileDataSet, System.ComponentModel.ISupportInitialize).EndInit()
        Me.pnlTipEntryPanel.ResumeLayout(False)
        Me.pnlTipEntryPanel.PerformLayout()
        CType(Me.SpecialFunctionsBindingSource, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.SpecialFunctionDataGridView, System.ComponentModel.ISupportInitialize).EndInit()
        Me.mnuSFContextMenu.ResumeLayout(False)
        CType(Me.SpecialFunctionTipsBindingSource, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.CreditCardDataGridView, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.GlobalDataSet, System.ComponentModel.ISupportInitialize).EndInit()
        Me.ResumeLayout(False)
        Me.PerformLayout()

    End Sub
    Friend WithEvents strMainMenu As System.Windows.Forms.MenuStrip
    Friend WithEvents FileToolStripMenuItem As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents mnuExit As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents mnuNew As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents mnuOpen As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents ToolStripSeparator2 As System.Windows.Forms.ToolStripSeparator
    Friend WithEvents mnuSave As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents mnuSaveAs As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents ToolStripSeparator3 As System.Windows.Forms.ToolStripSeparator
    Friend WithEvents mnuHelp As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents mnuContents As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents mnuIndex As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents ToolStripSeparator1 As System.Windows.Forms.ToolStripSeparator
    Friend WithEvents mnuAbout As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents mnuTools As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents grpServers As System.Windows.Forms.GroupBox
    Friend WithEvents ServersDataGridView As System.Windows.Forms.DataGridView
    Friend WithEvents pnlTipEntryPanel As System.Windows.Forms.Panel
    Friend WithEvents CreditCardDataGridView As System.Windows.Forms.DataGridView
    Friend WithEvents CCID As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents CCAmount As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents CCServerNumber As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents CCFirstName As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents CCLastName As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents btnClearCC As System.Windows.Forms.Button
    Friend WithEvents btnAddCC As System.Windows.Forms.Button
    Friend WithEvents lblCCTotal As System.Windows.Forms.Label
    Friend WithEvents lblCCAmount As System.Windows.Forms.Label
    Friend WithEvents lblCCServerNumber As System.Windows.Forms.Label
    Friend WithEvents txtCCAmount As System.Windows.Forms.TextBox
    Friend WithEvents txtCCServerNumber As System.Windows.Forms.TextBox
    Friend WithEvents txtCCServerName As System.Windows.Forms.TextBox
    Friend WithEvents lblSelectSpecialFunction As System.Windows.Forms.Label
    Friend WithEvents cboSelectSpecialFunction As System.Windows.Forms.ComboBox
    Friend WithEvents SpecialFunctionDataGridView As System.Windows.Forms.DataGridView
    Friend WithEvents btnClearSF As System.Windows.Forms.Button
    Friend WithEvents btnAddSF As System.Windows.Forms.Button
    Friend WithEvents lblSFTotal As System.Windows.Forms.Label
    Friend WithEvents lblSFAmount As System.Windows.Forms.Label
    Friend WithEvents lblSFServerNumber As System.Windows.Forms.Label
    Friend WithEvents txtSFAmount As System.Windows.Forms.TextBox
    Friend WithEvents txtSFServerNumber As System.Windows.Forms.TextBox
    Friend WithEvents txtSFServerName As System.Windows.Forms.TextBox
    Friend WithEvents dlgOpenFile As System.Windows.Forms.OpenFileDialog
    Friend WithEvents dlgSaveFile As System.Windows.Forms.SaveFileDialog
    Friend WithEvents mnuManageUsers As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents FileDataSet As Tip_Tracker_SF.FileDataSet
    Friend WithEvents GlobalDataSet As Tip_Tracker_SF.GlobalDataSet
    Friend WithEvents mnuClose As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents ToolStripSeparator4 As System.Windows.Forms.ToolStripSeparator
    Friend WithEvents ServersBindingSource As System.Windows.Forms.BindingSource
    Friend WithEvents SpecialFunctionTipsBindingSource As System.Windows.Forms.BindingSource
    Friend WithEvents mnuSFContextMenu As System.Windows.Forms.ContextMenuStrip
    Friend WithEvents mnuDeleteSFTip As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents SpecialFunctionsBindingSource As System.Windows.Forms.BindingSource
    Friend WithEvents mnuView As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents mnuShowAllTips As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents ToolStripSeparator7 As System.Windows.Forms.ToolStripSeparator
    Friend WithEvents mnuManageSpecialFunctions As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents mnuManageTemplateServers As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents mnuServersContextMenu As System.Windows.Forms.ContextMenuStrip
    Friend WithEvents mnuAddServer As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents mnuEditSelectedServer As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents mnuReports As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents mnuReportByServer As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents mnuReportByFunction As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents ServersServerNumber As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents ServersFirstName As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents ServersLastName As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents ToolStripSeparator5 As System.Windows.Forms.ToolStripSeparator
    Friend WithEvents mnuShowTipsForServer As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents SFID As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents SFAmount As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents SFServerNumber As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents SFFirstName As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents SFLastName As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents SFSpecialFunction As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents mnuImportServerList As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents ToolStripSeparator6 As System.Windows.Forms.ToolStripSeparator
End Class
