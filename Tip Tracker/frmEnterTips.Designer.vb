<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class frmEnterTips
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
        Me.components = New System.ComponentModel.Container()
        Dim resources As System.ComponentModel.ComponentResourceManager = New System.ComponentModel.ComponentResourceManager(GetType(frmEnterTips))
        Dim DataGridViewCellStyle1 As System.Windows.Forms.DataGridViewCellStyle = New System.Windows.Forms.DataGridViewCellStyle()
        Me.ServersBindingSource = New System.Windows.Forms.BindingSource(Me.components)
        Me.FileDataSet = New Tip_Tracker.FileDataSet()
        Me.strStatusStrip = New System.Windows.Forms.StatusStrip()
        Me.lblWorkingDate = New System.Windows.Forms.ToolStripStatusLabel()
        Me.lblPeriodStart = New System.Windows.Forms.ToolStripStatusLabel()
        Me.lblPeriodEnd = New System.Windows.Forms.ToolStripStatusLabel()
        Me.lblSystemDate = New System.Windows.Forms.ToolStripStatusLabel()
        Me.lblInfo = New System.Windows.Forms.ToolStripStatusLabel()
        Me.strToolStrip = New System.Windows.Forms.ToolStrip()
        Me.btnFinalize = New System.Windows.Forms.ToolStripButton()
        Me.ToolStripSeparator1 = New System.Windows.Forms.ToolStripSeparator()
        Me.btnSelectWorkingDate = New System.Windows.Forms.ToolStripButton()
        Me.ToolStripSeparator2 = New System.Windows.Forms.ToolStripSeparator()
        Me.btnReports = New System.Windows.Forms.ToolStripDropDownButton()
        Me.mnuPrintRegularTipChits = New System.Windows.Forms.ToolStripMenuItem()
        Me.ToolStripSeparator6 = New System.Windows.Forms.ToolStripSeparator()
        Me.mnuTipReports = New System.Windows.Forms.ToolStripMenuItem()
        Me.mnuSpecialFunctionReports = New System.Windows.Forms.ToolStripMenuItem()
        Me.ToolStripSeparator9 = New System.Windows.Forms.ToolStripSeparator()
        Me.mnuPayrollBalancingReport = New System.Windows.Forms.ToolStripMenuItem()
        Me.ToolStripSeparator3 = New System.Windows.Forms.ToolStripSeparator()
        Me.btnTools = New System.Windows.Forms.ToolStripDropDownButton()
        Me.mnuExportTips = New System.Windows.Forms.ToolStripMenuItem()
        Me.ToolStripSeparator5 = New System.Windows.Forms.ToolStripSeparator()
        Me.mnuManageSpecialFunctions = New System.Windows.Forms.ToolStripMenuItem()
        Me.ToolStripSeparator10 = New System.Windows.Forms.ToolStripSeparator()
        Me.mnuAutoAddServers = New System.Windows.Forms.ToolStripMenuItem()
        Me.ToolStripSeparator11 = New System.Windows.Forms.ToolStripSeparator()
        Me.mnuOptimizeFile = New System.Windows.Forms.ToolStripMenuItem()
        Me.ToolStripSeparator4 = New System.Windows.Forms.ToolStripSeparator()
        Me.lblCurrentTipType = New System.Windows.Forms.ToolStripLabel()
        Me.tabTipsTabControl = New System.Windows.Forms.TabControl()
        Me.tabCreditCard = New System.Windows.Forms.TabPage()
        Me.CreditCardDataGridView = New System.Windows.Forms.DataGridView()
        Me.CCID = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.CCAmount = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.CCServerNumber = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.CCFirstName = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.CCLastName = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.mnuCCTips = New System.Windows.Forms.ContextMenuStrip(Me.components)
        Me.mnuEditCCTip = New System.Windows.Forms.ToolStripMenuItem()
        Me.mnuReassignCCTip = New System.Windows.Forms.ToolStripMenuItem()
        Me.ToolStripSeparator12 = New System.Windows.Forms.ToolStripSeparator()
        Me.mnuDeleteCCTip = New System.Windows.Forms.ToolStripMenuItem()
        Me.CreditCardTipsBindingSource = New System.Windows.Forms.BindingSource(Me.components)
        Me.btnClearCC = New System.Windows.Forms.Button()
        Me.btnAddCC = New System.Windows.Forms.Button()
        Me.lblCCTotal = New System.Windows.Forms.Label()
        Me.lblCCAmount = New System.Windows.Forms.Label()
        Me.lblCCServerNumber = New System.Windows.Forms.Label()
        Me.txtCCAmount = New System.Windows.Forms.TextBox()
        Me.txtCCServerNumber = New System.Windows.Forms.TextBox()
        Me.txtCCServerName = New System.Windows.Forms.TextBox()
        Me.tabRoomCharge = New System.Windows.Forms.TabPage()
        Me.RoomChargeDataGridView = New System.Windows.Forms.DataGridView()
        Me.RCID = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.RCAmount = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.RCServerNumber = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.RCFirstName = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.RCLastName = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.mnuRCTips = New System.Windows.Forms.ContextMenuStrip(Me.components)
        Me.mnuEditRCTip = New System.Windows.Forms.ToolStripMenuItem()
        Me.mnuReassignRCTip = New System.Windows.Forms.ToolStripMenuItem()
        Me.ToolStripSeparator13 = New System.Windows.Forms.ToolStripSeparator()
        Me.mnuDeleteRCTip = New System.Windows.Forms.ToolStripMenuItem()
        Me.RoomChargeTipsBindingSource = New System.Windows.Forms.BindingSource(Me.components)
        Me.btnClearRC = New System.Windows.Forms.Button()
        Me.btnAddRC = New System.Windows.Forms.Button()
        Me.lblRCTotal = New System.Windows.Forms.Label()
        Me.lblRCAmount = New System.Windows.Forms.Label()
        Me.lblRCServerNumber = New System.Windows.Forms.Label()
        Me.txtRCAmount = New System.Windows.Forms.TextBox()
        Me.txtRCServerNumber = New System.Windows.Forms.TextBox()
        Me.txtRCServerName = New System.Windows.Forms.TextBox()
        Me.tabCash = New System.Windows.Forms.TabPage()
        Me.btnQuickAddCashTips = New System.Windows.Forms.Button()
        Me.cboCAServer = New System.Windows.Forms.ComboBox()
        Me.CAServersLookupBindingSource = New System.Windows.Forms.BindingSource(Me.components)
        Me.ServersLookupDataset = New Tip_Tracker.ServersLookupDataset()
        Me.lblCAServerName = New System.Windows.Forms.Label()
        Me.CashDataGridView = New System.Windows.Forms.DataGridView()
        Me.CAID = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.CAAmount = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.CAServerNumber = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.CAFirstName = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.CALastName = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.mnuCATips = New System.Windows.Forms.ContextMenuStrip(Me.components)
        Me.mnuEditCATip = New System.Windows.Forms.ToolStripMenuItem()
        Me.mnuReassignCATip = New System.Windows.Forms.ToolStripMenuItem()
        Me.ToolStripSeparator14 = New System.Windows.Forms.ToolStripSeparator()
        Me.mnuDeleteCATip = New System.Windows.Forms.ToolStripMenuItem()
        Me.CashTipsBindingSource = New System.Windows.Forms.BindingSource(Me.components)
        Me.btnClearCA = New System.Windows.Forms.Button()
        Me.btnAddCA = New System.Windows.Forms.Button()
        Me.lblCATotal = New System.Windows.Forms.Label()
        Me.lblCAAmount = New System.Windows.Forms.Label()
        Me.txtCAAmount = New System.Windows.Forms.TextBox()
        Me.tabSpecialFunction = New System.Windows.Forms.TabPage()
        Me.cboSFServer = New System.Windows.Forms.ComboBox()
        Me.SFServersLookupBindingSource = New System.Windows.Forms.BindingSource(Me.components)
        Me.lblSFServerName = New System.Windows.Forms.Label()
        Me.btnManageFunctions = New System.Windows.Forms.Button()
        Me.btnShowAllTips = New System.Windows.Forms.Button()
        Me.lblSelectSpecialFunction = New System.Windows.Forms.Label()
        Me.cboSelectSpecialFunction = New System.Windows.Forms.ComboBox()
        Me.SpecialFunctionBindingSource = New System.Windows.Forms.BindingSource(Me.components)
        Me.SpecialFunctionDataGridView = New System.Windows.Forms.DataGridView()
        Me.SFID = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.SFAmount = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.SFServerNumber = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.SFFirstName = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.SFLastName = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.SFSpecialFunction = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.mnuSFTips = New System.Windows.Forms.ContextMenuStrip(Me.components)
        Me.mnuEditSFTip = New System.Windows.Forms.ToolStripMenuItem()
        Me.ToolStripSeparator15 = New System.Windows.Forms.ToolStripSeparator()
        Me.mnuReassignSFTip = New System.Windows.Forms.ToolStripMenuItem()
        Me.mnuDeleteSFTip = New System.Windows.Forms.ToolStripMenuItem()
        Me.SpecialFunctionTipsBindingSource = New System.Windows.Forms.BindingSource(Me.components)
        Me.btnClearSF = New System.Windows.Forms.Button()
        Me.btnAddSF = New System.Windows.Forms.Button()
        Me.lblSFTotal = New System.Windows.Forms.Label()
        Me.lblSFAmount = New System.Windows.Forms.Label()
        Me.txtSFAmount = New System.Windows.Forms.TextBox()
        Me.grpServers = New System.Windows.Forms.GroupBox()
        Me.ServersDataGridView = New System.Windows.Forms.DataGridView()
        Me.ServersServerNumber = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.ServersFirstName = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.ServersLastName = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.mnuServersContextMenu = New System.Windows.Forms.ContextMenuStrip(Me.components)
        Me.mnuAddServer = New System.Windows.Forms.ToolStripMenuItem()
        Me.mnuEditSelectedServer = New System.Windows.Forms.ToolStripMenuItem()
        Me.ToolStripSeparator7 = New System.Windows.Forms.ToolStripSeparator()
        Me.mnuMergeDuplicate = New System.Windows.Forms.ToolStripMenuItem()
        Me.ToolStripSeparator8 = New System.Windows.Forms.ToolStripSeparator()
        Me.mnuCopyFromTemplate = New System.Windows.Forms.ToolStripMenuItem()
        Me.DataGridView1 = New System.Windows.Forms.DataGridView()
        Me.DataGridViewTextBoxColumn1 = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.DataGridViewTextBoxColumn2 = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.DataGridViewTextBoxColumn3 = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.DataGridViewTextBoxColumn4 = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.DataGridViewTextBoxColumn5 = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.Button1 = New System.Windows.Forms.Button()
        Me.Button2 = New System.Windows.Forms.Button()
        Me.Label1 = New System.Windows.Forms.Label()
        Me.Label2 = New System.Windows.Forms.Label()
        Me.Label3 = New System.Windows.Forms.Label()
        Me.TextBox1 = New System.Windows.Forms.TextBox()
        Me.TextBox2 = New System.Windows.Forms.TextBox()
        Me.TextBox3 = New System.Windows.Forms.TextBox()
        Me.DataGridView4 = New System.Windows.Forms.DataGridView()
        Me.DataGridViewTextBoxColumn16 = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.DataGridViewTextBoxColumn17 = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.DataGridViewTextBoxColumn18 = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.DataGridViewTextBoxColumn19 = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.DataGridViewTextBoxColumn20 = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.Button7 = New System.Windows.Forms.Button()
        Me.Button8 = New System.Windows.Forms.Button()
        Me.Label10 = New System.Windows.Forms.Label()
        Me.Label11 = New System.Windows.Forms.Label()
        Me.Label12 = New System.Windows.Forms.Label()
        Me.TextBox10 = New System.Windows.Forms.TextBox()
        Me.TextBox11 = New System.Windows.Forms.TextBox()
        Me.TextBox12 = New System.Windows.Forms.TextBox()
        Me.dlgOpenFile = New System.Windows.Forms.OpenFileDialog()
        Me.dlgSaveFile = New System.Windows.Forms.SaveFileDialog()
        Me.ImportFileDataSet = New Tip_Tracker.FileDataSet()
        CType(Me.ServersBindingSource, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.FileDataSet, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.strStatusStrip.SuspendLayout()
        Me.strToolStrip.SuspendLayout()
        Me.tabTipsTabControl.SuspendLayout()
        Me.tabCreditCard.SuspendLayout()
        CType(Me.CreditCardDataGridView, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.mnuCCTips.SuspendLayout()
        CType(Me.CreditCardTipsBindingSource, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.tabRoomCharge.SuspendLayout()
        CType(Me.RoomChargeDataGridView, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.mnuRCTips.SuspendLayout()
        CType(Me.RoomChargeTipsBindingSource, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.tabCash.SuspendLayout()
        CType(Me.CAServersLookupBindingSource, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.ServersLookupDataset, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.CashDataGridView, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.mnuCATips.SuspendLayout()
        CType(Me.CashTipsBindingSource, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.tabSpecialFunction.SuspendLayout()
        CType(Me.SFServersLookupBindingSource, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.SpecialFunctionBindingSource, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.SpecialFunctionDataGridView, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.mnuSFTips.SuspendLayout()
        CType(Me.SpecialFunctionTipsBindingSource, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.grpServers.SuspendLayout()
        CType(Me.ServersDataGridView, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.mnuServersContextMenu.SuspendLayout()
        CType(Me.DataGridView1, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.DataGridView4, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.ImportFileDataSet, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.SuspendLayout()
        '
        'ServersBindingSource
        '
        Me.ServersBindingSource.DataMember = "Servers"
        Me.ServersBindingSource.DataSource = Me.FileDataSet
        '
        'FileDataSet
        '
        Me.FileDataSet.CaseSensitive = True
        Me.FileDataSet.DataSetName = "FileDataSet"
        Me.FileDataSet.SchemaSerializationMode = System.Data.SchemaSerializationMode.IncludeSchema
        '
        'strStatusStrip
        '
        Me.strStatusStrip.Items.AddRange(New System.Windows.Forms.ToolStripItem() {Me.lblWorkingDate, Me.lblPeriodStart, Me.lblPeriodEnd, Me.lblSystemDate, Me.lblInfo})
        Me.strStatusStrip.Location = New System.Drawing.Point(0, 537)
        Me.strStatusStrip.Name = "strStatusStrip"
        Me.strStatusStrip.Size = New System.Drawing.Size(784, 24)
        Me.strStatusStrip.TabIndex = 2
        Me.strStatusStrip.Text = "StatusStrip1"
        '
        'lblWorkingDate
        '
        Me.lblWorkingDate.BorderSides = CType((((System.Windows.Forms.ToolStripStatusLabelBorderSides.Left Or System.Windows.Forms.ToolStripStatusLabelBorderSides.Top) _
            Or System.Windows.Forms.ToolStripStatusLabelBorderSides.Right) _
            Or System.Windows.Forms.ToolStripStatusLabelBorderSides.Bottom), System.Windows.Forms.ToolStripStatusLabelBorderSides)
        Me.lblWorkingDate.BorderStyle = System.Windows.Forms.Border3DStyle.Etched
        Me.lblWorkingDate.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Bold)
        Me.lblWorkingDate.Name = "lblWorkingDate"
        Me.lblWorkingDate.Size = New System.Drawing.Size(93, 19)
        Me.lblWorkingDate.Text = "Working Date:"
        '
        'lblPeriodStart
        '
        Me.lblPeriodStart.BorderSides = CType((((System.Windows.Forms.ToolStripStatusLabelBorderSides.Left Or System.Windows.Forms.ToolStripStatusLabelBorderSides.Top) _
            Or System.Windows.Forms.ToolStripStatusLabelBorderSides.Right) _
            Or System.Windows.Forms.ToolStripStatusLabelBorderSides.Bottom), System.Windows.Forms.ToolStripStatusLabelBorderSides)
        Me.lblPeriodStart.BorderStyle = System.Windows.Forms.Border3DStyle.Etched
        Me.lblPeriodStart.Name = "lblPeriodStart"
        Me.lblPeriodStart.Size = New System.Drawing.Size(75, 19)
        Me.lblPeriodStart.Text = "Period Start:"
        '
        'lblPeriodEnd
        '
        Me.lblPeriodEnd.BorderSides = CType((((System.Windows.Forms.ToolStripStatusLabelBorderSides.Left Or System.Windows.Forms.ToolStripStatusLabelBorderSides.Top) _
            Or System.Windows.Forms.ToolStripStatusLabelBorderSides.Right) _
            Or System.Windows.Forms.ToolStripStatusLabelBorderSides.Bottom), System.Windows.Forms.ToolStripStatusLabelBorderSides)
        Me.lblPeriodEnd.BorderStyle = System.Windows.Forms.Border3DStyle.Etched
        Me.lblPeriodEnd.Name = "lblPeriodEnd"
        Me.lblPeriodEnd.Size = New System.Drawing.Size(71, 19)
        Me.lblPeriodEnd.Text = "Period End:"
        '
        'lblSystemDate
        '
        Me.lblSystemDate.BorderSides = CType((((System.Windows.Forms.ToolStripStatusLabelBorderSides.Left Or System.Windows.Forms.ToolStripStatusLabelBorderSides.Top) _
            Or System.Windows.Forms.ToolStripStatusLabelBorderSides.Right) _
            Or System.Windows.Forms.ToolStripStatusLabelBorderSides.Bottom), System.Windows.Forms.ToolStripStatusLabelBorderSides)
        Me.lblSystemDate.BorderStyle = System.Windows.Forms.Border3DStyle.Etched
        Me.lblSystemDate.Name = "lblSystemDate"
        Me.lblSystemDate.Size = New System.Drawing.Size(79, 19)
        Me.lblSystemDate.Text = "System Date:"
        '
        'lblInfo
        '
        Me.lblInfo.Name = "lblInfo"
        Me.lblInfo.Size = New System.Drawing.Size(31, 19)
        Me.lblInfo.Text = "Info:"
        Me.lblInfo.Visible = False
        '
        'strToolStrip
        '
        Me.strToolStrip.GripStyle = System.Windows.Forms.ToolStripGripStyle.Hidden
        Me.strToolStrip.Items.AddRange(New System.Windows.Forms.ToolStripItem() {Me.btnFinalize, Me.ToolStripSeparator1, Me.btnSelectWorkingDate, Me.ToolStripSeparator2, Me.btnReports, Me.ToolStripSeparator3, Me.btnTools, Me.ToolStripSeparator4, Me.lblCurrentTipType})
        Me.strToolStrip.LayoutStyle = System.Windows.Forms.ToolStripLayoutStyle.HorizontalStackWithOverflow
        Me.strToolStrip.Location = New System.Drawing.Point(0, 0)
        Me.strToolStrip.Name = "strToolStrip"
        Me.strToolStrip.RenderMode = System.Windows.Forms.ToolStripRenderMode.System
        Me.strToolStrip.Size = New System.Drawing.Size(784, 25)
        Me.strToolStrip.TabIndex = 3
        Me.strToolStrip.Text = "ToolStrip1"
        '
        'btnFinalize
        '
        Me.btnFinalize.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image
        Me.btnFinalize.Image = CType(resources.GetObject("btnFinalize.Image"), System.Drawing.Image)
        Me.btnFinalize.ImageTransparentColor = System.Drawing.Color.Magenta
        Me.btnFinalize.Name = "btnFinalize"
        Me.btnFinalize.Size = New System.Drawing.Size(23, 22)
        Me.btnFinalize.Text = "Finalize Today"
        '
        'ToolStripSeparator1
        '
        Me.ToolStripSeparator1.Name = "ToolStripSeparator1"
        Me.ToolStripSeparator1.Size = New System.Drawing.Size(6, 25)
        '
        'btnSelectWorkingDate
        '
        Me.btnSelectWorkingDate.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image
        Me.btnSelectWorkingDate.Image = CType(resources.GetObject("btnSelectWorkingDate.Image"), System.Drawing.Image)
        Me.btnSelectWorkingDate.ImageTransparentColor = System.Drawing.Color.Magenta
        Me.btnSelectWorkingDate.Name = "btnSelectWorkingDate"
        Me.btnSelectWorkingDate.Size = New System.Drawing.Size(23, 22)
        Me.btnSelectWorkingDate.Text = "Select Working Date"
        '
        'ToolStripSeparator2
        '
        Me.ToolStripSeparator2.Name = "ToolStripSeparator2"
        Me.ToolStripSeparator2.Size = New System.Drawing.Size(6, 25)
        '
        'btnReports
        '
        Me.btnReports.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Text
        Me.btnReports.DropDownItems.AddRange(New System.Windows.Forms.ToolStripItem() {Me.mnuPrintRegularTipChits, Me.ToolStripSeparator6, Me.mnuTipReports, Me.mnuSpecialFunctionReports, Me.ToolStripSeparator9, Me.mnuPayrollBalancingReport})
        Me.btnReports.Image = CType(resources.GetObject("btnReports.Image"), System.Drawing.Image)
        Me.btnReports.ImageTransparentColor = System.Drawing.Color.Magenta
        Me.btnReports.Name = "btnReports"
        Me.btnReports.Size = New System.Drawing.Size(60, 22)
        Me.btnReports.Text = "Reports"
        '
        'mnuPrintRegularTipChits
        '
        Me.mnuPrintRegularTipChits.Name = "mnuPrintRegularTipChits"
        Me.mnuPrintRegularTipChits.Size = New System.Drawing.Size(203, 22)
        Me.mnuPrintRegularTipChits.Text = "Print Tip Chits"
        '
        'ToolStripSeparator6
        '
        Me.ToolStripSeparator6.Name = "ToolStripSeparator6"
        Me.ToolStripSeparator6.Size = New System.Drawing.Size(200, 6)
        '
        'mnuTipReports
        '
        Me.mnuTipReports.Name = "mnuTipReports"
        Me.mnuTipReports.Size = New System.Drawing.Size(203, 22)
        Me.mnuTipReports.Text = "Tip Reports"
        '
        'mnuSpecialFunctionReports
        '
        Me.mnuSpecialFunctionReports.Name = "mnuSpecialFunctionReports"
        Me.mnuSpecialFunctionReports.Size = New System.Drawing.Size(203, 22)
        Me.mnuSpecialFunctionReports.Text = "Special Function Detail"
        '
        'ToolStripSeparator9
        '
        Me.ToolStripSeparator9.Name = "ToolStripSeparator9"
        Me.ToolStripSeparator9.Size = New System.Drawing.Size(200, 6)
        '
        'mnuPayrollBalancingReport
        '
        Me.mnuPayrollBalancingReport.Name = "mnuPayrollBalancingReport"
        Me.mnuPayrollBalancingReport.Size = New System.Drawing.Size(203, 22)
        Me.mnuPayrollBalancingReport.Text = "Payroll Balancing Report"
        '
        'ToolStripSeparator3
        '
        Me.ToolStripSeparator3.Name = "ToolStripSeparator3"
        Me.ToolStripSeparator3.Size = New System.Drawing.Size(6, 25)
        '
        'btnTools
        '
        Me.btnTools.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Text
        Me.btnTools.DropDownItems.AddRange(New System.Windows.Forms.ToolStripItem() {Me.mnuExportTips, Me.ToolStripSeparator5, Me.mnuManageSpecialFunctions, Me.ToolStripSeparator10, Me.mnuAutoAddServers, Me.ToolStripSeparator11, Me.mnuOptimizeFile})
        Me.btnTools.ImageTransparentColor = System.Drawing.Color.Magenta
        Me.btnTools.Name = "btnTools"
        Me.btnTools.Size = New System.Drawing.Size(78, 22)
        Me.btnTools.Text = "Operations"
        '
        'mnuExportTips
        '
        Me.mnuExportTips.Name = "mnuExportTips"
        Me.mnuExportTips.Size = New System.Drawing.Size(221, 22)
        Me.mnuExportTips.Text = "Export Tips..."
        '
        'ToolStripSeparator5
        '
        Me.ToolStripSeparator5.Name = "ToolStripSeparator5"
        Me.ToolStripSeparator5.Size = New System.Drawing.Size(218, 6)
        '
        'mnuManageSpecialFunctions
        '
        Me.mnuManageSpecialFunctions.Name = "mnuManageSpecialFunctions"
        Me.mnuManageSpecialFunctions.Size = New System.Drawing.Size(221, 22)
        Me.mnuManageSpecialFunctions.Text = "Manage Special Functions..."
        '
        'ToolStripSeparator10
        '
        Me.ToolStripSeparator10.Name = "ToolStripSeparator10"
        Me.ToolStripSeparator10.Size = New System.Drawing.Size(218, 6)
        '
        'mnuAutoAddServers
        '
        Me.mnuAutoAddServers.Name = "mnuAutoAddServers"
        Me.mnuAutoAddServers.Size = New System.Drawing.Size(221, 22)
        Me.mnuAutoAddServers.Text = "Auto Add Servers..."
        '
        'ToolStripSeparator11
        '
        Me.ToolStripSeparator11.Name = "ToolStripSeparator11"
        Me.ToolStripSeparator11.Size = New System.Drawing.Size(218, 6)
        '
        'mnuOptimizeFile
        '
        Me.mnuOptimizeFile.Name = "mnuOptimizeFile"
        Me.mnuOptimizeFile.Size = New System.Drawing.Size(221, 22)
        Me.mnuOptimizeFile.Text = "Optimize File"
        '
        'ToolStripSeparator4
        '
        Me.ToolStripSeparator4.Name = "ToolStripSeparator4"
        Me.ToolStripSeparator4.Size = New System.Drawing.Size(6, 25)
        '
        'lblCurrentTipType
        '
        Me.lblCurrentTipType.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Bold)
        Me.lblCurrentTipType.Name = "lblCurrentTipType"
        Me.lblCurrentTipType.Size = New System.Drawing.Size(141, 22)
        Me.lblCurrentTipType.Text = "Editing Credit Card Tips"
        '
        'tabTipsTabControl
        '
        Me.tabTipsTabControl.Anchor = CType((((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Bottom) _
            Or System.Windows.Forms.AnchorStyles.Left) _
            Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.tabTipsTabControl.Controls.Add(Me.tabCreditCard)
        Me.tabTipsTabControl.Controls.Add(Me.tabRoomCharge)
        Me.tabTipsTabControl.Controls.Add(Me.tabCash)
        Me.tabTipsTabControl.Controls.Add(Me.tabSpecialFunction)
        Me.tabTipsTabControl.Location = New System.Drawing.Point(12, 28)
        Me.tabTipsTabControl.Name = "tabTipsTabControl"
        Me.tabTipsTabControl.SelectedIndex = 0
        Me.tabTipsTabControl.Size = New System.Drawing.Size(564, 508)
        Me.tabTipsTabControl.TabIndex = 0
        '
        'tabCreditCard
        '
        Me.tabCreditCard.Controls.Add(Me.CreditCardDataGridView)
        Me.tabCreditCard.Controls.Add(Me.btnClearCC)
        Me.tabCreditCard.Controls.Add(Me.btnAddCC)
        Me.tabCreditCard.Controls.Add(Me.lblCCTotal)
        Me.tabCreditCard.Controls.Add(Me.lblCCAmount)
        Me.tabCreditCard.Controls.Add(Me.lblCCServerNumber)
        Me.tabCreditCard.Controls.Add(Me.txtCCAmount)
        Me.tabCreditCard.Controls.Add(Me.txtCCServerNumber)
        Me.tabCreditCard.Controls.Add(Me.txtCCServerName)
        Me.tabCreditCard.Location = New System.Drawing.Point(4, 22)
        Me.tabCreditCard.Name = "tabCreditCard"
        Me.tabCreditCard.Padding = New System.Windows.Forms.Padding(3)
        Me.tabCreditCard.Size = New System.Drawing.Size(556, 482)
        Me.tabCreditCard.TabIndex = 0
        Me.tabCreditCard.Text = "Credit Card"
        Me.tabCreditCard.UseVisualStyleBackColor = True
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
        Me.CreditCardDataGridView.ContextMenuStrip = Me.mnuCCTips
        Me.CreditCardDataGridView.DataSource = Me.CreditCardTipsBindingSource
        Me.CreditCardDataGridView.Location = New System.Drawing.Point(172, 6)
        Me.CreditCardDataGridView.MultiSelect = False
        Me.CreditCardDataGridView.Name = "CreditCardDataGridView"
        Me.CreditCardDataGridView.ReadOnly = True
        Me.CreditCardDataGridView.RowHeadersVisible = False
        Me.CreditCardDataGridView.RowHeadersWidthSizeMode = System.Windows.Forms.DataGridViewRowHeadersWidthSizeMode.DisableResizing
        Me.CreditCardDataGridView.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect
        Me.CreditCardDataGridView.Size = New System.Drawing.Size(378, 473)
        Me.CreditCardDataGridView.TabIndex = 8
        Me.CreditCardDataGridView.TabStop = False
        '
        'CCID
        '
        Me.CCID.DataPropertyName = "TipID"
        Me.CCID.HeaderText = "Tip No."
        Me.CCID.Name = "CCID"
        Me.CCID.ReadOnly = True
        Me.CCID.Width = 75
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
        Me.CCServerNumber.HeaderText = "Server No."
        Me.CCServerNumber.Name = "CCServerNumber"
        Me.CCServerNumber.ReadOnly = True
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
        'mnuCCTips
        '
        Me.mnuCCTips.Items.AddRange(New System.Windows.Forms.ToolStripItem() {Me.mnuEditCCTip, Me.mnuReassignCCTip, Me.ToolStripSeparator12, Me.mnuDeleteCCTip})
        Me.mnuCCTips.Name = "mnuCCTips"
        Me.mnuCCTips.Size = New System.Drawing.Size(168, 76)
        '
        'mnuEditCCTip
        '
        Me.mnuEditCCTip.Name = "mnuEditCCTip"
        Me.mnuEditCCTip.Size = New System.Drawing.Size(167, 22)
        Me.mnuEditCCTip.Text = "Edit Selected"
        '
        'mnuReassignCCTip
        '
        Me.mnuReassignCCTip.Name = "mnuReassignCCTip"
        Me.mnuReassignCCTip.Size = New System.Drawing.Size(167, 22)
        Me.mnuReassignCCTip.Text = "Reassign Selected"
        '
        'ToolStripSeparator12
        '
        Me.ToolStripSeparator12.Name = "ToolStripSeparator12"
        Me.ToolStripSeparator12.Size = New System.Drawing.Size(164, 6)
        '
        'mnuDeleteCCTip
        '
        Me.mnuDeleteCCTip.Name = "mnuDeleteCCTip"
        Me.mnuDeleteCCTip.Size = New System.Drawing.Size(167, 22)
        Me.mnuDeleteCCTip.Text = "Delete Selected"
        '
        'CreditCardTipsBindingSource
        '
        Me.CreditCardTipsBindingSource.DataMember = "Tips"
        Me.CreditCardTipsBindingSource.DataSource = Me.FileDataSet
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
        Me.lblCCTotal.Location = New System.Drawing.Point(3, 462)
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
        Me.txtCCServerNumber.MaxLength = 10
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
        'tabRoomCharge
        '
        Me.tabRoomCharge.Controls.Add(Me.RoomChargeDataGridView)
        Me.tabRoomCharge.Controls.Add(Me.btnClearRC)
        Me.tabRoomCharge.Controls.Add(Me.btnAddRC)
        Me.tabRoomCharge.Controls.Add(Me.lblRCTotal)
        Me.tabRoomCharge.Controls.Add(Me.lblRCAmount)
        Me.tabRoomCharge.Controls.Add(Me.lblRCServerNumber)
        Me.tabRoomCharge.Controls.Add(Me.txtRCAmount)
        Me.tabRoomCharge.Controls.Add(Me.txtRCServerNumber)
        Me.tabRoomCharge.Controls.Add(Me.txtRCServerName)
        Me.tabRoomCharge.Location = New System.Drawing.Point(4, 22)
        Me.tabRoomCharge.Name = "tabRoomCharge"
        Me.tabRoomCharge.Padding = New System.Windows.Forms.Padding(3)
        Me.tabRoomCharge.Size = New System.Drawing.Size(556, 482)
        Me.tabRoomCharge.TabIndex = 4
        Me.tabRoomCharge.Text = "Room Charge"
        Me.tabRoomCharge.UseVisualStyleBackColor = True
        '
        'RoomChargeDataGridView
        '
        Me.RoomChargeDataGridView.AllowUserToAddRows = False
        Me.RoomChargeDataGridView.AllowUserToDeleteRows = False
        Me.RoomChargeDataGridView.AllowUserToResizeColumns = False
        Me.RoomChargeDataGridView.AllowUserToResizeRows = False
        Me.RoomChargeDataGridView.Anchor = CType((((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Bottom) _
            Or System.Windows.Forms.AnchorStyles.Left) _
            Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.RoomChargeDataGridView.AutoGenerateColumns = False
        Me.RoomChargeDataGridView.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.DisableResizing
        Me.RoomChargeDataGridView.Columns.AddRange(New System.Windows.Forms.DataGridViewColumn() {Me.RCID, Me.RCAmount, Me.RCServerNumber, Me.RCFirstName, Me.RCLastName})
        Me.RoomChargeDataGridView.ContextMenuStrip = Me.mnuRCTips
        Me.RoomChargeDataGridView.DataSource = Me.RoomChargeTipsBindingSource
        Me.RoomChargeDataGridView.Location = New System.Drawing.Point(172, 6)
        Me.RoomChargeDataGridView.MultiSelect = False
        Me.RoomChargeDataGridView.Name = "RoomChargeDataGridView"
        Me.RoomChargeDataGridView.ReadOnly = True
        Me.RoomChargeDataGridView.RowHeadersVisible = False
        Me.RoomChargeDataGridView.RowHeadersWidthSizeMode = System.Windows.Forms.DataGridViewRowHeadersWidthSizeMode.DisableResizing
        Me.RoomChargeDataGridView.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect
        Me.RoomChargeDataGridView.Size = New System.Drawing.Size(378, 473)
        Me.RoomChargeDataGridView.TabIndex = 8
        Me.RoomChargeDataGridView.TabStop = False
        '
        'RCID
        '
        Me.RCID.DataPropertyName = "TipID"
        Me.RCID.HeaderText = "Tip No."
        Me.RCID.Name = "RCID"
        Me.RCID.ReadOnly = True
        Me.RCID.Width = 75
        '
        'RCAmount
        '
        Me.RCAmount.DataPropertyName = "Amount"
        Me.RCAmount.HeaderText = "Amount"
        Me.RCAmount.Name = "RCAmount"
        Me.RCAmount.ReadOnly = True
        Me.RCAmount.Width = 75
        '
        'RCServerNumber
        '
        Me.RCServerNumber.DataPropertyName = "ServerNumber"
        Me.RCServerNumber.HeaderText = "Server No."
        Me.RCServerNumber.Name = "RCServerNumber"
        Me.RCServerNumber.ReadOnly = True
        '
        'RCFirstName
        '
        Me.RCFirstName.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.Fill
        Me.RCFirstName.DataPropertyName = "FirstName"
        Me.RCFirstName.FillWeight = 90.0!
        Me.RCFirstName.HeaderText = "First Name"
        Me.RCFirstName.Name = "RCFirstName"
        Me.RCFirstName.ReadOnly = True
        '
        'RCLastName
        '
        Me.RCLastName.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.Fill
        Me.RCLastName.DataPropertyName = "LastName"
        Me.RCLastName.HeaderText = "Last Name"
        Me.RCLastName.Name = "RCLastName"
        Me.RCLastName.ReadOnly = True
        '
        'mnuRCTips
        '
        Me.mnuRCTips.Items.AddRange(New System.Windows.Forms.ToolStripItem() {Me.mnuEditRCTip, Me.mnuReassignRCTip, Me.ToolStripSeparator13, Me.mnuDeleteRCTip})
        Me.mnuRCTips.Name = "mnuCCTips"
        Me.mnuRCTips.Size = New System.Drawing.Size(168, 76)
        '
        'mnuEditRCTip
        '
        Me.mnuEditRCTip.Name = "mnuEditRCTip"
        Me.mnuEditRCTip.Size = New System.Drawing.Size(167, 22)
        Me.mnuEditRCTip.Text = "Edit Selected"
        '
        'mnuReassignRCTip
        '
        Me.mnuReassignRCTip.Name = "mnuReassignRCTip"
        Me.mnuReassignRCTip.Size = New System.Drawing.Size(167, 22)
        Me.mnuReassignRCTip.Text = "Reassign Selected"
        '
        'ToolStripSeparator13
        '
        Me.ToolStripSeparator13.Name = "ToolStripSeparator13"
        Me.ToolStripSeparator13.Size = New System.Drawing.Size(164, 6)
        '
        'mnuDeleteRCTip
        '
        Me.mnuDeleteRCTip.Name = "mnuDeleteRCTip"
        Me.mnuDeleteRCTip.Size = New System.Drawing.Size(167, 22)
        Me.mnuDeleteRCTip.Text = "Delete Selected"
        '
        'RoomChargeTipsBindingSource
        '
        Me.RoomChargeTipsBindingSource.DataMember = "Tips"
        Me.RoomChargeTipsBindingSource.DataSource = Me.FileDataSet
        '
        'btnClearRC
        '
        Me.btnClearRC.Location = New System.Drawing.Point(105, 84)
        Me.btnClearRC.Name = "btnClearRC"
        Me.btnClearRC.Size = New System.Drawing.Size(61, 23)
        Me.btnClearRC.TabIndex = 3
        Me.btnClearRC.Text = "Clear"
        Me.btnClearRC.UseVisualStyleBackColor = True
        '
        'btnAddRC
        '
        Me.btnAddRC.Location = New System.Drawing.Point(38, 84)
        Me.btnAddRC.Name = "btnAddRC"
        Me.btnAddRC.Size = New System.Drawing.Size(61, 23)
        Me.btnAddRC.TabIndex = 2
        Me.btnAddRC.Text = "Add"
        Me.btnAddRC.UseVisualStyleBackColor = True
        '
        'lblRCTotal
        '
        Me.lblRCTotal.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Left), System.Windows.Forms.AnchorStyles)
        Me.lblRCTotal.AutoSize = True
        Me.lblRCTotal.Font = New System.Drawing.Font("Microsoft Sans Serif", 10.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblRCTotal.Location = New System.Drawing.Point(3, 462)
        Me.lblRCTotal.Name = "lblRCTotal"
        Me.lblRCTotal.Size = New System.Drawing.Size(96, 17)
        Me.lblRCTotal.TabIndex = 4
        Me.lblRCTotal.Text = "Total: $0.00"
        '
        'lblRCAmount
        '
        Me.lblRCAmount.AutoSize = True
        Me.lblRCAmount.Location = New System.Drawing.Point(6, 35)
        Me.lblRCAmount.Name = "lblRCAmount"
        Me.lblRCAmount.Size = New System.Drawing.Size(64, 13)
        Me.lblRCAmount.TabIndex = 6
        Me.lblRCAmount.Text = "Tip Amount:"
        '
        'lblRCServerNumber
        '
        Me.lblRCServerNumber.AutoSize = True
        Me.lblRCServerNumber.Location = New System.Drawing.Point(6, 9)
        Me.lblRCServerNumber.Name = "lblRCServerNumber"
        Me.lblRCServerNumber.Size = New System.Drawing.Size(81, 13)
        Me.lblRCServerNumber.TabIndex = 5
        Me.lblRCServerNumber.Text = "Server Number:"
        '
        'txtRCAmount
        '
        Me.txtRCAmount.Location = New System.Drawing.Point(105, 32)
        Me.txtRCAmount.MaxLength = 8
        Me.txtRCAmount.Name = "txtRCAmount"
        Me.txtRCAmount.Size = New System.Drawing.Size(61, 20)
        Me.txtRCAmount.TabIndex = 1
        '
        'txtRCServerNumber
        '
        Me.txtRCServerNumber.Location = New System.Drawing.Point(105, 6)
        Me.txtRCServerNumber.MaxLength = 10
        Me.txtRCServerNumber.Name = "txtRCServerNumber"
        Me.txtRCServerNumber.Size = New System.Drawing.Size(61, 20)
        Me.txtRCServerNumber.TabIndex = 0
        '
        'txtRCServerName
        '
        Me.txtRCServerName.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.txtRCServerName.Location = New System.Drawing.Point(6, 58)
        Me.txtRCServerName.Name = "txtRCServerName"
        Me.txtRCServerName.ReadOnly = True
        Me.txtRCServerName.Size = New System.Drawing.Size(160, 20)
        Me.txtRCServerName.TabIndex = 7
        Me.txtRCServerName.TabStop = False
        '
        'tabCash
        '
        Me.tabCash.Controls.Add(Me.btnQuickAddCashTips)
        Me.tabCash.Controls.Add(Me.cboCAServer)
        Me.tabCash.Controls.Add(Me.lblCAServerName)
        Me.tabCash.Controls.Add(Me.CashDataGridView)
        Me.tabCash.Controls.Add(Me.btnClearCA)
        Me.tabCash.Controls.Add(Me.btnAddCA)
        Me.tabCash.Controls.Add(Me.lblCATotal)
        Me.tabCash.Controls.Add(Me.lblCAAmount)
        Me.tabCash.Controls.Add(Me.txtCAAmount)
        Me.tabCash.Location = New System.Drawing.Point(4, 22)
        Me.tabCash.Name = "tabCash"
        Me.tabCash.Padding = New System.Windows.Forms.Padding(3)
        Me.tabCash.Size = New System.Drawing.Size(556, 482)
        Me.tabCash.TabIndex = 5
        Me.tabCash.Text = "Cash"
        Me.tabCash.UseVisualStyleBackColor = True
        '
        'btnQuickAddCashTips
        '
        Me.btnQuickAddCashTips.Location = New System.Drawing.Point(38, 198)
        Me.btnQuickAddCashTips.Name = "btnQuickAddCashTips"
        Me.btnQuickAddCashTips.Size = New System.Drawing.Size(128, 23)
        Me.btnQuickAddCashTips.TabIndex = 8
        Me.btnQuickAddCashTips.Text = "Quick Add..."
        Me.btnQuickAddCashTips.UseVisualStyleBackColor = True
        '
        'cboCAServer
        '
        Me.cboCAServer.AutoCompleteMode = System.Windows.Forms.AutoCompleteMode.SuggestAppend
        Me.cboCAServer.AutoCompleteSource = System.Windows.Forms.AutoCompleteSource.ListItems
        Me.cboCAServer.DataSource = Me.CAServersLookupBindingSource
        Me.cboCAServer.DisplayMember = "NameString"
        Me.cboCAServer.FormattingEnabled = True
        Me.cboCAServer.Location = New System.Drawing.Point(6, 22)
        Me.cboCAServer.Name = "cboCAServer"
        Me.cboCAServer.Size = New System.Drawing.Size(160, 21)
        Me.cboCAServer.TabIndex = 1
        Me.cboCAServer.ValueMember = "ServerNumber"
        '
        'CAServersLookupBindingSource
        '
        Me.CAServersLookupBindingSource.DataMember = "Servers"
        Me.CAServersLookupBindingSource.DataSource = Me.ServersLookupDataset
        '
        'ServersLookupDataset
        '
        Me.ServersLookupDataset.DataSetName = "ServersLookupDataset"
        Me.ServersLookupDataset.SchemaSerializationMode = System.Data.SchemaSerializationMode.IncludeSchema
        '
        'lblCAServerName
        '
        Me.lblCAServerName.AutoSize = True
        Me.lblCAServerName.Location = New System.Drawing.Point(3, 6)
        Me.lblCAServerName.Name = "lblCAServerName"
        Me.lblCAServerName.Size = New System.Drawing.Size(126, 13)
        Me.lblCAServerName.TabIndex = 0
        Me.lblCAServerName.Text = "Server Name (Last, First):"
        '
        'CashDataGridView
        '
        Me.CashDataGridView.AllowUserToAddRows = False
        Me.CashDataGridView.AllowUserToDeleteRows = False
        Me.CashDataGridView.AllowUserToResizeColumns = False
        Me.CashDataGridView.AllowUserToResizeRows = False
        Me.CashDataGridView.Anchor = CType((((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Bottom) _
            Or System.Windows.Forms.AnchorStyles.Left) _
            Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.CashDataGridView.AutoGenerateColumns = False
        Me.CashDataGridView.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.DisableResizing
        Me.CashDataGridView.Columns.AddRange(New System.Windows.Forms.DataGridViewColumn() {Me.CAID, Me.CAAmount, Me.CAServerNumber, Me.CAFirstName, Me.CALastName})
        Me.CashDataGridView.ContextMenuStrip = Me.mnuCATips
        Me.CashDataGridView.DataSource = Me.CashTipsBindingSource
        Me.CashDataGridView.Location = New System.Drawing.Point(172, 6)
        Me.CashDataGridView.MultiSelect = False
        Me.CashDataGridView.Name = "CashDataGridView"
        Me.CashDataGridView.ReadOnly = True
        Me.CashDataGridView.RowHeadersVisible = False
        Me.CashDataGridView.RowHeadersWidthSizeMode = System.Windows.Forms.DataGridViewRowHeadersWidthSizeMode.DisableResizing
        Me.CashDataGridView.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect
        Me.CashDataGridView.Size = New System.Drawing.Size(378, 473)
        Me.CashDataGridView.TabIndex = 5
        Me.CashDataGridView.TabStop = False
        '
        'CAID
        '
        Me.CAID.DataPropertyName = "TipID"
        Me.CAID.HeaderText = "Tip No."
        Me.CAID.Name = "CAID"
        Me.CAID.ReadOnly = True
        Me.CAID.Width = 75
        '
        'CAAmount
        '
        Me.CAAmount.DataPropertyName = "Amount"
        Me.CAAmount.HeaderText = "Amount"
        Me.CAAmount.Name = "CAAmount"
        Me.CAAmount.ReadOnly = True
        Me.CAAmount.Width = 75
        '
        'CAServerNumber
        '
        Me.CAServerNumber.DataPropertyName = "ServerNumber"
        Me.CAServerNumber.HeaderText = "Server No."
        Me.CAServerNumber.Name = "CAServerNumber"
        Me.CAServerNumber.ReadOnly = True
        '
        'CAFirstName
        '
        Me.CAFirstName.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.Fill
        Me.CAFirstName.DataPropertyName = "FirstName"
        Me.CAFirstName.FillWeight = 90.0!
        Me.CAFirstName.HeaderText = "First Name"
        Me.CAFirstName.Name = "CAFirstName"
        Me.CAFirstName.ReadOnly = True
        '
        'CALastName
        '
        Me.CALastName.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.Fill
        Me.CALastName.DataPropertyName = "LastName"
        Me.CALastName.HeaderText = "Last Name"
        Me.CALastName.Name = "CALastName"
        Me.CALastName.ReadOnly = True
        '
        'mnuCATips
        '
        Me.mnuCATips.Items.AddRange(New System.Windows.Forms.ToolStripItem() {Me.mnuEditCATip, Me.mnuReassignCATip, Me.ToolStripSeparator14, Me.mnuDeleteCATip})
        Me.mnuCATips.Name = "mnuCCTips"
        Me.mnuCATips.Size = New System.Drawing.Size(168, 76)
        '
        'mnuEditCATip
        '
        Me.mnuEditCATip.Name = "mnuEditCATip"
        Me.mnuEditCATip.Size = New System.Drawing.Size(167, 22)
        Me.mnuEditCATip.Text = "Edit Selected"
        '
        'mnuReassignCATip
        '
        Me.mnuReassignCATip.Name = "mnuReassignCATip"
        Me.mnuReassignCATip.Size = New System.Drawing.Size(167, 22)
        Me.mnuReassignCATip.Text = "Reassign Selected"
        '
        'ToolStripSeparator14
        '
        Me.ToolStripSeparator14.Name = "ToolStripSeparator14"
        Me.ToolStripSeparator14.Size = New System.Drawing.Size(164, 6)
        '
        'mnuDeleteCATip
        '
        Me.mnuDeleteCATip.Name = "mnuDeleteCATip"
        Me.mnuDeleteCATip.Size = New System.Drawing.Size(167, 22)
        Me.mnuDeleteCATip.Text = "Delete Selected"
        '
        'CashTipsBindingSource
        '
        Me.CashTipsBindingSource.DataMember = "Tips"
        Me.CashTipsBindingSource.DataSource = Me.FileDataSet
        '
        'btnClearCA
        '
        Me.btnClearCA.Location = New System.Drawing.Point(105, 84)
        Me.btnClearCA.Name = "btnClearCA"
        Me.btnClearCA.Size = New System.Drawing.Size(61, 23)
        Me.btnClearCA.TabIndex = 4
        Me.btnClearCA.Text = "Clear"
        Me.btnClearCA.UseVisualStyleBackColor = True
        '
        'btnAddCA
        '
        Me.btnAddCA.Location = New System.Drawing.Point(38, 84)
        Me.btnAddCA.Name = "btnAddCA"
        Me.btnAddCA.Size = New System.Drawing.Size(61, 23)
        Me.btnAddCA.TabIndex = 3
        Me.btnAddCA.Text = "Add"
        Me.btnAddCA.UseVisualStyleBackColor = True
        '
        'lblCATotal
        '
        Me.lblCATotal.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Left), System.Windows.Forms.AnchorStyles)
        Me.lblCATotal.AutoSize = True
        Me.lblCATotal.Font = New System.Drawing.Font("Microsoft Sans Serif", 10.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblCATotal.Location = New System.Drawing.Point(3, 462)
        Me.lblCATotal.Name = "lblCATotal"
        Me.lblCATotal.Size = New System.Drawing.Size(96, 17)
        Me.lblCATotal.TabIndex = 6
        Me.lblCATotal.Text = "Total: $0.00"
        '
        'lblCAAmount
        '
        Me.lblCAAmount.AutoSize = True
        Me.lblCAAmount.Location = New System.Drawing.Point(6, 52)
        Me.lblCAAmount.Name = "lblCAAmount"
        Me.lblCAAmount.Size = New System.Drawing.Size(64, 13)
        Me.lblCAAmount.TabIndex = 7
        Me.lblCAAmount.Text = "Tip Amount:"
        '
        'txtCAAmount
        '
        Me.txtCAAmount.Location = New System.Drawing.Point(105, 49)
        Me.txtCAAmount.MaxLength = 8
        Me.txtCAAmount.Name = "txtCAAmount"
        Me.txtCAAmount.Size = New System.Drawing.Size(61, 20)
        Me.txtCAAmount.TabIndex = 2
        '
        'tabSpecialFunction
        '
        Me.tabSpecialFunction.Controls.Add(Me.cboSFServer)
        Me.tabSpecialFunction.Controls.Add(Me.lblSFServerName)
        Me.tabSpecialFunction.Controls.Add(Me.btnManageFunctions)
        Me.tabSpecialFunction.Controls.Add(Me.btnShowAllTips)
        Me.tabSpecialFunction.Controls.Add(Me.lblSelectSpecialFunction)
        Me.tabSpecialFunction.Controls.Add(Me.cboSelectSpecialFunction)
        Me.tabSpecialFunction.Controls.Add(Me.SpecialFunctionDataGridView)
        Me.tabSpecialFunction.Controls.Add(Me.btnClearSF)
        Me.tabSpecialFunction.Controls.Add(Me.btnAddSF)
        Me.tabSpecialFunction.Controls.Add(Me.lblSFTotal)
        Me.tabSpecialFunction.Controls.Add(Me.lblSFAmount)
        Me.tabSpecialFunction.Controls.Add(Me.txtSFAmount)
        Me.tabSpecialFunction.Location = New System.Drawing.Point(4, 22)
        Me.tabSpecialFunction.Name = "tabSpecialFunction"
        Me.tabSpecialFunction.Padding = New System.Windows.Forms.Padding(3)
        Me.tabSpecialFunction.Size = New System.Drawing.Size(556, 482)
        Me.tabSpecialFunction.TabIndex = 6
        Me.tabSpecialFunction.Text = "Special Function"
        Me.tabSpecialFunction.UseVisualStyleBackColor = True
        '
        'cboSFServer
        '
        Me.cboSFServer.AutoCompleteMode = System.Windows.Forms.AutoCompleteMode.SuggestAppend
        Me.cboSFServer.AutoCompleteSource = System.Windows.Forms.AutoCompleteSource.ListItems
        Me.cboSFServer.DataSource = Me.SFServersLookupBindingSource
        Me.cboSFServer.DisplayMember = "NameString"
        Me.cboSFServer.FormattingEnabled = True
        Me.cboSFServer.Location = New System.Drawing.Point(6, 59)
        Me.cboSFServer.Name = "cboSFServer"
        Me.cboSFServer.Size = New System.Drawing.Size(160, 21)
        Me.cboSFServer.TabIndex = 1
        Me.cboSFServer.ValueMember = "ServerNumber"
        '
        'SFServersLookupBindingSource
        '
        Me.SFServersLookupBindingSource.DataMember = "Servers"
        Me.SFServersLookupBindingSource.DataSource = Me.ServersLookupDataset
        '
        'lblSFServerName
        '
        Me.lblSFServerName.AutoSize = True
        Me.lblSFServerName.Location = New System.Drawing.Point(3, 43)
        Me.lblSFServerName.Name = "lblSFServerName"
        Me.lblSFServerName.Size = New System.Drawing.Size(72, 13)
        Me.lblSFServerName.TabIndex = 9
        Me.lblSFServerName.Text = "Server Name:"
        '
        'btnManageFunctions
        '
        Me.btnManageFunctions.Location = New System.Drawing.Point(38, 227)
        Me.btnManageFunctions.Name = "btnManageFunctions"
        Me.btnManageFunctions.Size = New System.Drawing.Size(128, 23)
        Me.btnManageFunctions.TabIndex = 6
        Me.btnManageFunctions.Text = "Manage Functions"
        Me.btnManageFunctions.UseVisualStyleBackColor = True
        '
        'btnShowAllTips
        '
        Me.btnShowAllTips.Location = New System.Drawing.Point(38, 198)
        Me.btnShowAllTips.Name = "btnShowAllTips"
        Me.btnShowAllTips.Size = New System.Drawing.Size(128, 23)
        Me.btnShowAllTips.TabIndex = 5
        Me.btnShowAllTips.Text = "Show All Tips"
        Me.btnShowAllTips.UseVisualStyleBackColor = True
        '
        'lblSelectSpecialFunction
        '
        Me.lblSelectSpecialFunction.AutoSize = True
        Me.lblSelectSpecialFunction.Location = New System.Drawing.Point(3, 3)
        Me.lblSelectSpecialFunction.Name = "lblSelectSpecialFunction"
        Me.lblSelectSpecialFunction.Size = New System.Drawing.Size(122, 13)
        Me.lblSelectSpecialFunction.TabIndex = 8
        Me.lblSelectSpecialFunction.Text = "Select Special Function:"
        '
        'cboSelectSpecialFunction
        '
        Me.cboSelectSpecialFunction.AutoCompleteMode = System.Windows.Forms.AutoCompleteMode.SuggestAppend
        Me.cboSelectSpecialFunction.AutoCompleteSource = System.Windows.Forms.AutoCompleteSource.ListItems
        Me.cboSelectSpecialFunction.DataSource = Me.SpecialFunctionBindingSource
        Me.cboSelectSpecialFunction.DisplayMember = "SpecialFunction"
        Me.cboSelectSpecialFunction.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList
        Me.cboSelectSpecialFunction.FormattingEnabled = True
        Me.cboSelectSpecialFunction.Location = New System.Drawing.Point(6, 19)
        Me.cboSelectSpecialFunction.Name = "cboSelectSpecialFunction"
        Me.cboSelectSpecialFunction.Size = New System.Drawing.Size(160, 21)
        Me.cboSelectSpecialFunction.TabIndex = 0
        Me.cboSelectSpecialFunction.ValueMember = "SpecialFunction"
        '
        'SpecialFunctionBindingSource
        '
        Me.SpecialFunctionBindingSource.DataMember = "SpecialFunctions"
        Me.SpecialFunctionBindingSource.DataSource = Me.FileDataSet
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
        Me.SpecialFunctionDataGridView.ContextMenuStrip = Me.mnuSFTips
        Me.SpecialFunctionDataGridView.DataSource = Me.SpecialFunctionTipsBindingSource
        Me.SpecialFunctionDataGridView.Location = New System.Drawing.Point(172, 6)
        Me.SpecialFunctionDataGridView.MultiSelect = False
        Me.SpecialFunctionDataGridView.Name = "SpecialFunctionDataGridView"
        Me.SpecialFunctionDataGridView.ReadOnly = True
        Me.SpecialFunctionDataGridView.RowHeadersVisible = False
        Me.SpecialFunctionDataGridView.RowHeadersWidthSizeMode = System.Windows.Forms.DataGridViewRowHeadersWidthSizeMode.DisableResizing
        Me.SpecialFunctionDataGridView.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect
        Me.SpecialFunctionDataGridView.Size = New System.Drawing.Size(378, 473)
        Me.SpecialFunctionDataGridView.TabIndex = 11
        Me.SpecialFunctionDataGridView.TabStop = False
        '
        'SFID
        '
        Me.SFID.DataPropertyName = "TipID"
        Me.SFID.HeaderText = "Tip No."
        Me.SFID.Name = "SFID"
        Me.SFID.ReadOnly = True
        Me.SFID.Width = 75
        '
        'SFAmount
        '
        Me.SFAmount.DataPropertyName = "Amount"
        Me.SFAmount.HeaderText = "Amount"
        Me.SFAmount.Name = "SFAmount"
        Me.SFAmount.ReadOnly = True
        Me.SFAmount.Width = 75
        '
        'SFServerNumber
        '
        Me.SFServerNumber.DataPropertyName = "ServerNumber"
        Me.SFServerNumber.HeaderText = "Server No."
        Me.SFServerNumber.Name = "SFServerNumber"
        Me.SFServerNumber.ReadOnly = True
        '
        'SFFirstName
        '
        Me.SFFirstName.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.Fill
        Me.SFFirstName.DataPropertyName = "FirstName"
        Me.SFFirstName.FillWeight = 90.0!
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
        'mnuSFTips
        '
        Me.mnuSFTips.Items.AddRange(New System.Windows.Forms.ToolStripItem() {Me.mnuEditSFTip, Me.ToolStripSeparator15, Me.mnuReassignSFTip, Me.mnuDeleteSFTip})
        Me.mnuSFTips.Name = "mnuCCTips"
        Me.mnuSFTips.Size = New System.Drawing.Size(168, 76)
        '
        'mnuEditSFTip
        '
        Me.mnuEditSFTip.Name = "mnuEditSFTip"
        Me.mnuEditSFTip.Size = New System.Drawing.Size(167, 22)
        Me.mnuEditSFTip.Text = "Edit Selected"
        '
        'ToolStripSeparator15
        '
        Me.ToolStripSeparator15.Name = "ToolStripSeparator15"
        Me.ToolStripSeparator15.Size = New System.Drawing.Size(164, 6)
        '
        'mnuReassignSFTip
        '
        Me.mnuReassignSFTip.Name = "mnuReassignSFTip"
        Me.mnuReassignSFTip.Size = New System.Drawing.Size(167, 22)
        Me.mnuReassignSFTip.Text = "Reassign Selected"
        '
        'mnuDeleteSFTip
        '
        Me.mnuDeleteSFTip.Name = "mnuDeleteSFTip"
        Me.mnuDeleteSFTip.Size = New System.Drawing.Size(167, 22)
        Me.mnuDeleteSFTip.Text = "Delete Selected"
        '
        'SpecialFunctionTipsBindingSource
        '
        Me.SpecialFunctionTipsBindingSource.DataMember = "Tips"
        Me.SpecialFunctionTipsBindingSource.DataSource = Me.FileDataSet
        '
        'btnClearSF
        '
        Me.btnClearSF.Location = New System.Drawing.Point(105, 124)
        Me.btnClearSF.Name = "btnClearSF"
        Me.btnClearSF.Size = New System.Drawing.Size(61, 23)
        Me.btnClearSF.TabIndex = 4
        Me.btnClearSF.Text = "Clear"
        Me.btnClearSF.UseVisualStyleBackColor = True
        '
        'btnAddSF
        '
        Me.btnAddSF.Location = New System.Drawing.Point(38, 124)
        Me.btnAddSF.Name = "btnAddSF"
        Me.btnAddSF.Size = New System.Drawing.Size(61, 23)
        Me.btnAddSF.TabIndex = 3
        Me.btnAddSF.Text = "Add"
        Me.btnAddSF.UseVisualStyleBackColor = True
        '
        'lblSFTotal
        '
        Me.lblSFTotal.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Left), System.Windows.Forms.AnchorStyles)
        Me.lblSFTotal.AutoSize = True
        Me.lblSFTotal.Font = New System.Drawing.Font("Microsoft Sans Serif", 10.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblSFTotal.Location = New System.Drawing.Point(3, 462)
        Me.lblSFTotal.Name = "lblSFTotal"
        Me.lblSFTotal.Size = New System.Drawing.Size(96, 17)
        Me.lblSFTotal.TabIndex = 7
        Me.lblSFTotal.Text = "Total: $0.00"
        '
        'lblSFAmount
        '
        Me.lblSFAmount.AutoSize = True
        Me.lblSFAmount.Location = New System.Drawing.Point(6, 89)
        Me.lblSFAmount.Name = "lblSFAmount"
        Me.lblSFAmount.Size = New System.Drawing.Size(64, 13)
        Me.lblSFAmount.TabIndex = 10
        Me.lblSFAmount.Text = "Tip Amount:"
        '
        'txtSFAmount
        '
        Me.txtSFAmount.Location = New System.Drawing.Point(105, 86)
        Me.txtSFAmount.MaxLength = 8
        Me.txtSFAmount.Name = "txtSFAmount"
        Me.txtSFAmount.Size = New System.Drawing.Size(61, 20)
        Me.txtSFAmount.TabIndex = 2
        '
        'grpServers
        '
        Me.grpServers.Anchor = CType(((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Bottom) _
            Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.grpServers.Controls.Add(Me.ServersDataGridView)
        Me.grpServers.Location = New System.Drawing.Point(579, 28)
        Me.grpServers.Name = "grpServers"
        Me.grpServers.Size = New System.Drawing.Size(193, 508)
        Me.grpServers.TabIndex = 1
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
        Me.ServersDataGridView.Size = New System.Drawing.Size(187, 489)
        Me.ServersDataGridView.TabIndex = 0
        Me.ServersDataGridView.TabStop = False
        '
        'ServersServerNumber
        '
        Me.ServersServerNumber.DataPropertyName = "ServerNumber"
        Me.ServersServerNumber.HeaderText = "No."
        Me.ServersServerNumber.Name = "ServersServerNumber"
        Me.ServersServerNumber.ReadOnly = True
        Me.ServersServerNumber.Width = 40
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
        Me.mnuServersContextMenu.Items.AddRange(New System.Windows.Forms.ToolStripItem() {Me.mnuAddServer, Me.mnuEditSelectedServer, Me.ToolStripSeparator7, Me.mnuMergeDuplicate, Me.ToolStripSeparator8, Me.mnuCopyFromTemplate})
        Me.mnuServersContextMenu.Name = "mnuServersContextMenu"
        Me.mnuServersContextMenu.Size = New System.Drawing.Size(185, 104)
        '
        'mnuAddServer
        '
        Me.mnuAddServer.Name = "mnuAddServer"
        Me.mnuAddServer.Size = New System.Drawing.Size(184, 22)
        Me.mnuAddServer.Text = "Add..."
        '
        'mnuEditSelectedServer
        '
        Me.mnuEditSelectedServer.Name = "mnuEditSelectedServer"
        Me.mnuEditSelectedServer.Size = New System.Drawing.Size(184, 22)
        Me.mnuEditSelectedServer.Text = "Edit Selected..."
        '
        'ToolStripSeparator7
        '
        Me.ToolStripSeparator7.Name = "ToolStripSeparator7"
        Me.ToolStripSeparator7.Size = New System.Drawing.Size(181, 6)
        '
        'mnuMergeDuplicate
        '
        Me.mnuMergeDuplicate.Name = "mnuMergeDuplicate"
        Me.mnuMergeDuplicate.Size = New System.Drawing.Size(184, 22)
        Me.mnuMergeDuplicate.Text = "Merge Duplicate"
        '
        'ToolStripSeparator8
        '
        Me.ToolStripSeparator8.Name = "ToolStripSeparator8"
        Me.ToolStripSeparator8.Size = New System.Drawing.Size(181, 6)
        '
        'mnuCopyFromTemplate
        '
        Me.mnuCopyFromTemplate.Name = "mnuCopyFromTemplate"
        Me.mnuCopyFromTemplate.Size = New System.Drawing.Size(184, 22)
        Me.mnuCopyFromTemplate.Text = "Copy From Template"
        '
        'DataGridView1
        '
        Me.DataGridView1.AllowUserToAddRows = False
        Me.DataGridView1.AllowUserToDeleteRows = False
        Me.DataGridView1.AllowUserToResizeColumns = False
        Me.DataGridView1.AllowUserToResizeRows = False
        Me.DataGridView1.Anchor = CType((((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Bottom) _
            Or System.Windows.Forms.AnchorStyles.Left) _
            Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.DataGridView1.AutoGenerateColumns = False
        Me.DataGridView1.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.DisableResizing
        Me.DataGridView1.Columns.AddRange(New System.Windows.Forms.DataGridViewColumn() {Me.DataGridViewTextBoxColumn1, Me.DataGridViewTextBoxColumn2, Me.DataGridViewTextBoxColumn3, Me.DataGridViewTextBoxColumn4, Me.DataGridViewTextBoxColumn5})
        Me.DataGridView1.DataSource = Me.CreditCardTipsBindingSource
        Me.DataGridView1.Location = New System.Drawing.Point(172, 6)
        Me.DataGridView1.Name = "DataGridView1"
        Me.DataGridView1.ReadOnly = True
        Me.DataGridView1.RowHeadersVisible = False
        Me.DataGridView1.RowHeadersWidthSizeMode = System.Windows.Forms.DataGridViewRowHeadersWidthSizeMode.DisableResizing
        Me.DataGridView1.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect
        Me.DataGridView1.Size = New System.Drawing.Size(211, 350)
        Me.DataGridView1.TabIndex = 8
        Me.DataGridView1.TabStop = False
        '
        'DataGridViewTextBoxColumn1
        '
        Me.DataGridViewTextBoxColumn1.DataPropertyName = "TipID"
        Me.DataGridViewTextBoxColumn1.HeaderText = "ID"
        Me.DataGridViewTextBoxColumn1.Name = "DataGridViewTextBoxColumn1"
        Me.DataGridViewTextBoxColumn1.ReadOnly = True
        Me.DataGridViewTextBoxColumn1.Width = 40
        '
        'DataGridViewTextBoxColumn2
        '
        Me.DataGridViewTextBoxColumn2.DataPropertyName = "Amount"
        Me.DataGridViewTextBoxColumn2.HeaderText = "Amount"
        Me.DataGridViewTextBoxColumn2.Name = "DataGridViewTextBoxColumn2"
        Me.DataGridViewTextBoxColumn2.ReadOnly = True
        Me.DataGridViewTextBoxColumn2.Width = 75
        '
        'DataGridViewTextBoxColumn3
        '
        Me.DataGridViewTextBoxColumn3.DataPropertyName = "ServerNumber"
        Me.DataGridViewTextBoxColumn3.HeaderText = "No."
        Me.DataGridViewTextBoxColumn3.Name = "DataGridViewTextBoxColumn3"
        Me.DataGridViewTextBoxColumn3.ReadOnly = True
        Me.DataGridViewTextBoxColumn3.Width = 50
        '
        'DataGridViewTextBoxColumn4
        '
        Me.DataGridViewTextBoxColumn4.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.Fill
        Me.DataGridViewTextBoxColumn4.DataPropertyName = "FirstName"
        Me.DataGridViewTextBoxColumn4.FillWeight = 90.0!
        Me.DataGridViewTextBoxColumn4.HeaderText = "First Name"
        Me.DataGridViewTextBoxColumn4.Name = "DataGridViewTextBoxColumn4"
        Me.DataGridViewTextBoxColumn4.ReadOnly = True
        '
        'DataGridViewTextBoxColumn5
        '
        Me.DataGridViewTextBoxColumn5.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.Fill
        Me.DataGridViewTextBoxColumn5.DataPropertyName = "LastName"
        Me.DataGridViewTextBoxColumn5.HeaderText = "Last Name"
        Me.DataGridViewTextBoxColumn5.Name = "DataGridViewTextBoxColumn5"
        Me.DataGridViewTextBoxColumn5.ReadOnly = True
        '
        'Button1
        '
        Me.Button1.Location = New System.Drawing.Point(105, 84)
        Me.Button1.Name = "Button1"
        Me.Button1.Size = New System.Drawing.Size(61, 23)
        Me.Button1.TabIndex = 3
        Me.Button1.Text = "Clear"
        Me.Button1.UseVisualStyleBackColor = True
        '
        'Button2
        '
        Me.Button2.Location = New System.Drawing.Point(38, 84)
        Me.Button2.Name = "Button2"
        Me.Button2.Size = New System.Drawing.Size(61, 23)
        Me.Button2.TabIndex = 2
        Me.Button2.Text = "Add"
        Me.Button2.UseVisualStyleBackColor = True
        '
        'Label1
        '
        Me.Label1.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Left), System.Windows.Forms.AnchorStyles)
        Me.Label1.AutoSize = True
        Me.Label1.Font = New System.Drawing.Font("Microsoft Sans Serif", 10.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label1.Location = New System.Drawing.Point(3, 339)
        Me.Label1.Name = "Label1"
        Me.Label1.Size = New System.Drawing.Size(96, 17)
        Me.Label1.TabIndex = 4
        Me.Label1.Text = "Total: $0.00"
        '
        'Label2
        '
        Me.Label2.AutoSize = True
        Me.Label2.Location = New System.Drawing.Point(6, 35)
        Me.Label2.Name = "Label2"
        Me.Label2.Size = New System.Drawing.Size(64, 13)
        Me.Label2.TabIndex = 6
        Me.Label2.Text = "Tip Amount:"
        '
        'Label3
        '
        Me.Label3.AutoSize = True
        Me.Label3.Location = New System.Drawing.Point(6, 9)
        Me.Label3.Name = "Label3"
        Me.Label3.Size = New System.Drawing.Size(81, 13)
        Me.Label3.TabIndex = 5
        Me.Label3.Text = "Server Number:"
        '
        'TextBox1
        '
        Me.TextBox1.Location = New System.Drawing.Point(105, 32)
        Me.TextBox1.Name = "TextBox1"
        Me.TextBox1.Size = New System.Drawing.Size(61, 20)
        Me.TextBox1.TabIndex = 1
        '
        'TextBox2
        '
        Me.TextBox2.Location = New System.Drawing.Point(105, 6)
        Me.TextBox2.Name = "TextBox2"
        Me.TextBox2.Size = New System.Drawing.Size(61, 20)
        Me.TextBox2.TabIndex = 0
        '
        'TextBox3
        '
        Me.TextBox3.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.TextBox3.Enabled = False
        Me.TextBox3.Location = New System.Drawing.Point(6, 58)
        Me.TextBox3.Name = "TextBox3"
        Me.TextBox3.ReadOnly = True
        Me.TextBox3.Size = New System.Drawing.Size(160, 20)
        Me.TextBox3.TabIndex = 7
        Me.TextBox3.TabStop = False
        '
        'DataGridView4
        '
        Me.DataGridView4.AllowUserToAddRows = False
        Me.DataGridView4.AllowUserToDeleteRows = False
        Me.DataGridView4.AllowUserToResizeColumns = False
        Me.DataGridView4.AllowUserToResizeRows = False
        Me.DataGridView4.Anchor = CType((((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Bottom) _
            Or System.Windows.Forms.AnchorStyles.Left) _
            Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.DataGridView4.AutoGenerateColumns = False
        Me.DataGridView4.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.DisableResizing
        Me.DataGridView4.Columns.AddRange(New System.Windows.Forms.DataGridViewColumn() {Me.DataGridViewTextBoxColumn16, Me.DataGridViewTextBoxColumn17, Me.DataGridViewTextBoxColumn18, Me.DataGridViewTextBoxColumn19, Me.DataGridViewTextBoxColumn20})
        Me.DataGridView4.DataSource = Me.CreditCardTipsBindingSource
        Me.DataGridView4.Location = New System.Drawing.Point(172, 6)
        Me.DataGridView4.Name = "DataGridView4"
        Me.DataGridView4.ReadOnly = True
        Me.DataGridView4.RowHeadersVisible = False
        Me.DataGridView4.RowHeadersWidthSizeMode = System.Windows.Forms.DataGridViewRowHeadersWidthSizeMode.DisableResizing
        Me.DataGridView4.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect
        Me.DataGridView4.Size = New System.Drawing.Size(211, 350)
        Me.DataGridView4.TabIndex = 8
        Me.DataGridView4.TabStop = False
        '
        'DataGridViewTextBoxColumn16
        '
        Me.DataGridViewTextBoxColumn16.DataPropertyName = "TipID"
        Me.DataGridViewTextBoxColumn16.HeaderText = "ID"
        Me.DataGridViewTextBoxColumn16.Name = "DataGridViewTextBoxColumn16"
        Me.DataGridViewTextBoxColumn16.ReadOnly = True
        Me.DataGridViewTextBoxColumn16.Width = 40
        '
        'DataGridViewTextBoxColumn17
        '
        Me.DataGridViewTextBoxColumn17.DataPropertyName = "Amount"
        Me.DataGridViewTextBoxColumn17.HeaderText = "Amount"
        Me.DataGridViewTextBoxColumn17.Name = "DataGridViewTextBoxColumn17"
        Me.DataGridViewTextBoxColumn17.ReadOnly = True
        Me.DataGridViewTextBoxColumn17.Width = 75
        '
        'DataGridViewTextBoxColumn18
        '
        Me.DataGridViewTextBoxColumn18.DataPropertyName = "ServerNumber"
        Me.DataGridViewTextBoxColumn18.HeaderText = "No."
        Me.DataGridViewTextBoxColumn18.Name = "DataGridViewTextBoxColumn18"
        Me.DataGridViewTextBoxColumn18.ReadOnly = True
        Me.DataGridViewTextBoxColumn18.Width = 50
        '
        'DataGridViewTextBoxColumn19
        '
        Me.DataGridViewTextBoxColumn19.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.Fill
        Me.DataGridViewTextBoxColumn19.DataPropertyName = "FirstName"
        Me.DataGridViewTextBoxColumn19.FillWeight = 90.0!
        Me.DataGridViewTextBoxColumn19.HeaderText = "First Name"
        Me.DataGridViewTextBoxColumn19.Name = "DataGridViewTextBoxColumn19"
        Me.DataGridViewTextBoxColumn19.ReadOnly = True
        '
        'DataGridViewTextBoxColumn20
        '
        Me.DataGridViewTextBoxColumn20.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.Fill
        Me.DataGridViewTextBoxColumn20.DataPropertyName = "LastName"
        Me.DataGridViewTextBoxColumn20.HeaderText = "Last Name"
        Me.DataGridViewTextBoxColumn20.Name = "DataGridViewTextBoxColumn20"
        Me.DataGridViewTextBoxColumn20.ReadOnly = True
        '
        'Button7
        '
        Me.Button7.Location = New System.Drawing.Point(105, 84)
        Me.Button7.Name = "Button7"
        Me.Button7.Size = New System.Drawing.Size(61, 23)
        Me.Button7.TabIndex = 3
        Me.Button7.Text = "Clear"
        Me.Button7.UseVisualStyleBackColor = True
        '
        'Button8
        '
        Me.Button8.Location = New System.Drawing.Point(38, 84)
        Me.Button8.Name = "Button8"
        Me.Button8.Size = New System.Drawing.Size(61, 23)
        Me.Button8.TabIndex = 2
        Me.Button8.Text = "Add"
        Me.Button8.UseVisualStyleBackColor = True
        '
        'Label10
        '
        Me.Label10.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Left), System.Windows.Forms.AnchorStyles)
        Me.Label10.AutoSize = True
        Me.Label10.Font = New System.Drawing.Font("Microsoft Sans Serif", 10.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label10.Location = New System.Drawing.Point(3, 339)
        Me.Label10.Name = "Label10"
        Me.Label10.Size = New System.Drawing.Size(96, 17)
        Me.Label10.TabIndex = 4
        Me.Label10.Text = "Total: $0.00"
        '
        'Label11
        '
        Me.Label11.AutoSize = True
        Me.Label11.Location = New System.Drawing.Point(6, 35)
        Me.Label11.Name = "Label11"
        Me.Label11.Size = New System.Drawing.Size(64, 13)
        Me.Label11.TabIndex = 6
        Me.Label11.Text = "Tip Amount:"
        '
        'Label12
        '
        Me.Label12.AutoSize = True
        Me.Label12.Location = New System.Drawing.Point(6, 9)
        Me.Label12.Name = "Label12"
        Me.Label12.Size = New System.Drawing.Size(81, 13)
        Me.Label12.TabIndex = 5
        Me.Label12.Text = "Server Number:"
        '
        'TextBox10
        '
        Me.TextBox10.Location = New System.Drawing.Point(105, 32)
        Me.TextBox10.Name = "TextBox10"
        Me.TextBox10.Size = New System.Drawing.Size(61, 20)
        Me.TextBox10.TabIndex = 1
        '
        'TextBox11
        '
        Me.TextBox11.Location = New System.Drawing.Point(105, 6)
        Me.TextBox11.Name = "TextBox11"
        Me.TextBox11.Size = New System.Drawing.Size(61, 20)
        Me.TextBox11.TabIndex = 0
        '
        'TextBox12
        '
        Me.TextBox12.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.TextBox12.Enabled = False
        Me.TextBox12.Location = New System.Drawing.Point(6, 58)
        Me.TextBox12.Name = "TextBox12"
        Me.TextBox12.ReadOnly = True
        Me.TextBox12.Size = New System.Drawing.Size(160, 20)
        Me.TextBox12.TabIndex = 7
        Me.TextBox12.TabStop = False
        '
        'dlgOpenFile
        '
        Me.dlgOpenFile.DefaultExt = "tsf"
        Me.dlgOpenFile.Filter = "Tip Tracker SF Data Files (*.tsf)|*.tsf"
        Me.dlgOpenFile.Title = "Import Tips"
        '
        'dlgSaveFile
        '
        Me.dlgSaveFile.DefaultExt = "csv"
        Me.dlgSaveFile.FileName = "Export"
        Me.dlgSaveFile.Filter = "Comma Delimited File (*.csv)|*.csv"
        Me.dlgSaveFile.Title = "Export Tips"
        '
        'ImportFileDataSet
        '
        Me.ImportFileDataSet.CaseSensitive = True
        Me.ImportFileDataSet.DataSetName = "ImportFileDataSet"
        Me.ImportFileDataSet.SchemaSerializationMode = System.Data.SchemaSerializationMode.IncludeSchema
        '
        'frmEnterTips
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.ClientSize = New System.Drawing.Size(784, 561)
        Me.Controls.Add(Me.grpServers)
        Me.Controls.Add(Me.tabTipsTabControl)
        Me.Controls.Add(Me.strToolStrip)
        Me.Controls.Add(Me.strStatusStrip)
        Me.Icon = CType(resources.GetObject("$this.Icon"), System.Drawing.Icon)
        Me.MinimumSize = New System.Drawing.Size(800, 600)
        Me.Name = "frmEnterTips"
        Me.Text = "frmEnterTips"
        CType(Me.ServersBindingSource, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.FileDataSet, System.ComponentModel.ISupportInitialize).EndInit()
        Me.strStatusStrip.ResumeLayout(False)
        Me.strStatusStrip.PerformLayout()
        Me.strToolStrip.ResumeLayout(False)
        Me.strToolStrip.PerformLayout()
        Me.tabTipsTabControl.ResumeLayout(False)
        Me.tabCreditCard.ResumeLayout(False)
        Me.tabCreditCard.PerformLayout()
        CType(Me.CreditCardDataGridView, System.ComponentModel.ISupportInitialize).EndInit()
        Me.mnuCCTips.ResumeLayout(False)
        CType(Me.CreditCardTipsBindingSource, System.ComponentModel.ISupportInitialize).EndInit()
        Me.tabRoomCharge.ResumeLayout(False)
        Me.tabRoomCharge.PerformLayout()
        CType(Me.RoomChargeDataGridView, System.ComponentModel.ISupportInitialize).EndInit()
        Me.mnuRCTips.ResumeLayout(False)
        CType(Me.RoomChargeTipsBindingSource, System.ComponentModel.ISupportInitialize).EndInit()
        Me.tabCash.ResumeLayout(False)
        Me.tabCash.PerformLayout()
        CType(Me.CAServersLookupBindingSource, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.ServersLookupDataset, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.CashDataGridView, System.ComponentModel.ISupportInitialize).EndInit()
        Me.mnuCATips.ResumeLayout(False)
        CType(Me.CashTipsBindingSource, System.ComponentModel.ISupportInitialize).EndInit()
        Me.tabSpecialFunction.ResumeLayout(False)
        Me.tabSpecialFunction.PerformLayout()
        CType(Me.SFServersLookupBindingSource, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.SpecialFunctionBindingSource, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.SpecialFunctionDataGridView, System.ComponentModel.ISupportInitialize).EndInit()
        Me.mnuSFTips.ResumeLayout(False)
        CType(Me.SpecialFunctionTipsBindingSource, System.ComponentModel.ISupportInitialize).EndInit()
        Me.grpServers.ResumeLayout(False)
        CType(Me.ServersDataGridView, System.ComponentModel.ISupportInitialize).EndInit()
        Me.mnuServersContextMenu.ResumeLayout(False)
        CType(Me.DataGridView1, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.DataGridView4, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.ImportFileDataSet, System.ComponentModel.ISupportInitialize).EndInit()
        Me.ResumeLayout(False)
        Me.PerformLayout()

    End Sub
    Friend WithEvents strStatusStrip As System.Windows.Forms.StatusStrip
    Friend WithEvents strToolStrip As System.Windows.Forms.ToolStrip
    Friend WithEvents btnSelectWorkingDate As System.Windows.Forms.ToolStripButton
    Friend WithEvents btnFinalize As System.Windows.Forms.ToolStripButton
    Friend WithEvents lblWorkingDate As System.Windows.Forms.ToolStripStatusLabel
    Friend WithEvents lblPeriodStart As System.Windows.Forms.ToolStripStatusLabel
    Friend WithEvents lblPeriodEnd As System.Windows.Forms.ToolStripStatusLabel
    Friend WithEvents lblSystemDate As System.Windows.Forms.ToolStripStatusLabel
    Friend WithEvents ServersBindingSource As System.Windows.Forms.BindingSource
    Friend WithEvents FileDataSet As Tip_Tracker.FileDataSet
    Friend WithEvents ToolStripSeparator1 As System.Windows.Forms.ToolStripSeparator
    Friend WithEvents ToolStripSeparator2 As System.Windows.Forms.ToolStripSeparator
    Friend WithEvents tabTipsTabControl As System.Windows.Forms.TabControl
    Friend WithEvents tabCreditCard As System.Windows.Forms.TabPage
    Friend WithEvents grpServers As System.Windows.Forms.GroupBox
    Friend WithEvents ServersDataGridView As System.Windows.Forms.DataGridView
    Friend WithEvents btnClearCC As System.Windows.Forms.Button
    Friend WithEvents btnAddCC As System.Windows.Forms.Button
    Friend WithEvents lblCCTotal As System.Windows.Forms.Label
    Friend WithEvents lblCCAmount As System.Windows.Forms.Label
    Friend WithEvents lblCCServerNumber As System.Windows.Forms.Label
    Friend WithEvents txtCCAmount As System.Windows.Forms.TextBox
    Friend WithEvents txtCCServerNumber As System.Windows.Forms.TextBox
    Friend WithEvents txtCCServerName As System.Windows.Forms.TextBox
    Friend WithEvents CreditCardDataGridView As System.Windows.Forms.DataGridView
    Friend WithEvents CreditCardTipsBindingSource As System.Windows.Forms.BindingSource
    Friend WithEvents RoomChargeTipsBindingSource As System.Windows.Forms.BindingSource
    Friend WithEvents CashTipsBindingSource As System.Windows.Forms.BindingSource
    Friend WithEvents SpecialFunctionTipsBindingSource As System.Windows.Forms.BindingSource
    Friend WithEvents tabRoomCharge As System.Windows.Forms.TabPage
    Friend WithEvents RoomChargeDataGridView As System.Windows.Forms.DataGridView
    Friend WithEvents btnClearRC As System.Windows.Forms.Button
    Friend WithEvents btnAddRC As System.Windows.Forms.Button
    Friend WithEvents lblRCTotal As System.Windows.Forms.Label
    Friend WithEvents lblRCAmount As System.Windows.Forms.Label
    Friend WithEvents lblRCServerNumber As System.Windows.Forms.Label
    Friend WithEvents txtRCAmount As System.Windows.Forms.TextBox
    Friend WithEvents txtRCServerNumber As System.Windows.Forms.TextBox
    Friend WithEvents txtRCServerName As System.Windows.Forms.TextBox
    Friend WithEvents DataGridView1 As System.Windows.Forms.DataGridView
    Friend WithEvents DataGridViewTextBoxColumn1 As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents DataGridViewTextBoxColumn2 As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents DataGridViewTextBoxColumn3 As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents DataGridViewTextBoxColumn4 As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents DataGridViewTextBoxColumn5 As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents Button1 As System.Windows.Forms.Button
    Friend WithEvents Button2 As System.Windows.Forms.Button
    Friend WithEvents Label1 As System.Windows.Forms.Label
    Friend WithEvents Label2 As System.Windows.Forms.Label
    Friend WithEvents Label3 As System.Windows.Forms.Label
    Friend WithEvents TextBox1 As System.Windows.Forms.TextBox
    Friend WithEvents TextBox2 As System.Windows.Forms.TextBox
    Friend WithEvents TextBox3 As System.Windows.Forms.TextBox
    Friend WithEvents tabCash As System.Windows.Forms.TabPage
    Friend WithEvents CashDataGridView As System.Windows.Forms.DataGridView
    Friend WithEvents btnClearCA As System.Windows.Forms.Button
    Friend WithEvents btnAddCA As System.Windows.Forms.Button
    Friend WithEvents lblCATotal As System.Windows.Forms.Label
    Friend WithEvents lblCAAmount As System.Windows.Forms.Label
    Friend WithEvents txtCAAmount As System.Windows.Forms.TextBox
    Friend WithEvents tabSpecialFunction As System.Windows.Forms.TabPage
    Friend WithEvents SpecialFunctionDataGridView As System.Windows.Forms.DataGridView
    Friend WithEvents btnClearSF As System.Windows.Forms.Button
    Friend WithEvents btnAddSF As System.Windows.Forms.Button
    Friend WithEvents lblSFTotal As System.Windows.Forms.Label
    Friend WithEvents lblSFAmount As System.Windows.Forms.Label
    Friend WithEvents txtSFAmount As System.Windows.Forms.TextBox
    Friend WithEvents DataGridView4 As System.Windows.Forms.DataGridView
    Friend WithEvents DataGridViewTextBoxColumn16 As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents DataGridViewTextBoxColumn17 As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents DataGridViewTextBoxColumn18 As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents DataGridViewTextBoxColumn19 As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents DataGridViewTextBoxColumn20 As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents Button7 As System.Windows.Forms.Button
    Friend WithEvents Button8 As System.Windows.Forms.Button
    Friend WithEvents Label10 As System.Windows.Forms.Label
    Friend WithEvents Label11 As System.Windows.Forms.Label
    Friend WithEvents Label12 As System.Windows.Forms.Label
    Friend WithEvents TextBox10 As System.Windows.Forms.TextBox
    Friend WithEvents TextBox11 As System.Windows.Forms.TextBox
    Friend WithEvents TextBox12 As System.Windows.Forms.TextBox
    Friend WithEvents lblSelectSpecialFunction As System.Windows.Forms.Label
    Friend WithEvents cboSelectSpecialFunction As System.Windows.Forms.ComboBox
    Friend WithEvents lblCurrentTipType As System.Windows.Forms.ToolStripLabel
    Friend WithEvents SpecialFunctionBindingSource As System.Windows.Forms.BindingSource
    Friend WithEvents btnTools As System.Windows.Forms.ToolStripDropDownButton
    Friend WithEvents mnuExportTips As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents ToolStripSeparator5 As System.Windows.Forms.ToolStripSeparator
    Friend WithEvents mnuManageSpecialFunctions As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents ToolStripSeparator4 As System.Windows.Forms.ToolStripSeparator
    Friend WithEvents btnReports As System.Windows.Forms.ToolStripDropDownButton
    Friend WithEvents mnuPrintRegularTipChits As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents ToolStripSeparator6 As System.Windows.Forms.ToolStripSeparator
    Friend WithEvents ToolStripSeparator3 As System.Windows.Forms.ToolStripSeparator
    Friend WithEvents mnuCCTips As System.Windows.Forms.ContextMenuStrip
    Friend WithEvents mnuDeleteCCTip As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents mnuRCTips As System.Windows.Forms.ContextMenuStrip
    Friend WithEvents mnuDeleteRCTip As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents mnuCATips As System.Windows.Forms.ContextMenuStrip
    Friend WithEvents mnuDeleteCATip As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents mnuSFTips As System.Windows.Forms.ContextMenuStrip
    Friend WithEvents mnuDeleteSFTip As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents dlgOpenFile As System.Windows.Forms.OpenFileDialog
    Friend WithEvents dlgSaveFile As System.Windows.Forms.SaveFileDialog
    Friend WithEvents ImportFileDataSet As Tip_Tracker.FileDataSet
    Friend WithEvents ServersServerNumber As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents ServersFirstName As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents ServersLastName As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents mnuServersContextMenu As System.Windows.Forms.ContextMenuStrip
    Friend WithEvents mnuAddServer As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents mnuEditSelectedServer As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents btnShowAllTips As System.Windows.Forms.Button
    Friend WithEvents ToolStripSeparator7 As System.Windows.Forms.ToolStripSeparator
    Friend WithEvents mnuMergeDuplicate As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents mnuReassignCCTip As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents mnuReassignRCTip As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents mnuReassignCATip As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents mnuReassignSFTip As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents ToolStripSeparator8 As System.Windows.Forms.ToolStripSeparator
    Friend WithEvents mnuCopyFromTemplate As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents btnManageFunctions As System.Windows.Forms.Button
    Friend WithEvents ServersLookupDataset As Tip_Tracker.ServersLookupDataset
    Friend WithEvents lblCAServerName As System.Windows.Forms.Label
    Friend WithEvents CAServersLookupBindingSource As System.Windows.Forms.BindingSource
    Friend WithEvents cboCAServer As System.Windows.Forms.ComboBox
    Friend WithEvents btnQuickAddCashTips As System.Windows.Forms.Button
    Friend WithEvents cboSFServer As System.Windows.Forms.ComboBox
    Friend WithEvents lblSFServerName As System.Windows.Forms.Label
    Friend WithEvents SFServersLookupBindingSource As System.Windows.Forms.BindingSource
    Friend WithEvents mnuTipReports As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents mnuSpecialFunctionReports As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents ToolStripSeparator9 As System.Windows.Forms.ToolStripSeparator
    Friend WithEvents mnuPayrollBalancingReport As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents ToolStripSeparator10 As System.Windows.Forms.ToolStripSeparator
    Friend WithEvents mnuAutoAddServers As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents ToolStripSeparator11 As System.Windows.Forms.ToolStripSeparator
    Friend WithEvents mnuOptimizeFile As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents mnuEditCCTip As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents ToolStripSeparator12 As System.Windows.Forms.ToolStripSeparator
    Friend WithEvents mnuEditRCTip As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents ToolStripSeparator13 As System.Windows.Forms.ToolStripSeparator
    Friend WithEvents mnuEditCATip As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents ToolStripSeparator14 As System.Windows.Forms.ToolStripSeparator
    Friend WithEvents mnuEditSFTip As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents ToolStripSeparator15 As System.Windows.Forms.ToolStripSeparator
    Friend WithEvents lblInfo As System.Windows.Forms.ToolStripStatusLabel
    Friend WithEvents CCID As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents CCAmount As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents CCServerNumber As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents CCFirstName As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents CCLastName As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents RCID As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents RCAmount As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents RCServerNumber As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents RCFirstName As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents RCLastName As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents CAID As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents CAAmount As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents CAServerNumber As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents CAFirstName As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents CALastName As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents SFID As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents SFAmount As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents SFServerNumber As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents SFFirstName As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents SFLastName As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents SFSpecialFunction As System.Windows.Forms.DataGridViewTextBoxColumn
End Class
