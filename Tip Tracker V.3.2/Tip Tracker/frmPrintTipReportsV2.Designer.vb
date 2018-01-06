<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class frmPrintTipReportsV2
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
        Me.btnPrint = New System.Windows.Forms.Button
        Me.btnPreview = New System.Windows.Forms.Button
        Me.btnClose = New System.Windows.Forms.Button
        Me.grpReportType = New System.Windows.Forms.GroupBox
        Me.optDetail = New System.Windows.Forms.RadioButton
        Me.optSummary = New System.Windows.Forms.RadioButton
        Me.gprServers = New System.Windows.Forms.GroupBox
        Me.cboServers = New System.Windows.Forms.ComboBox
        Me.ServersBindingSource = New System.Windows.Forms.BindingSource(Me.components)
        Me.ServersLookupDataset = New Tip_Tracker.ServersLookupDataset
        Me.optSelectedServer = New System.Windows.Forms.RadioButton
        Me.optAllServers = New System.Windows.Forms.RadioButton
        Me.grpDates = New System.Windows.Forms.GroupBox
        Me.txtSelectedDate = New System.Windows.Forms.MaskedTextBox
        Me.optSelectedDate = New System.Windows.Forms.RadioButton
        Me.optAllDates = New System.Windows.Forms.RadioButton
        Me.grpTips = New System.Windows.Forms.GroupBox
        Me.optCash = New System.Windows.Forms.CheckBox
        Me.optSpecialFunction = New System.Windows.Forms.CheckBox
        Me.optRoomCharge = New System.Windows.Forms.CheckBox
        Me.optCreditCard = New System.Windows.Forms.CheckBox
        Me.grpPrintOrder = New System.Windows.Forms.GroupBox
        Me.optByDate = New System.Windows.Forms.RadioButton
        Me.optByServer = New System.Windows.Forms.RadioButton
        Me.dlgPrint = New System.Windows.Forms.PrintDialog
        Me.grpReportType.SuspendLayout()
        Me.gprServers.SuspendLayout()
        CType(Me.ServersBindingSource, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.ServersLookupDataset, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.grpDates.SuspendLayout()
        Me.grpTips.SuspendLayout()
        Me.grpPrintOrder.SuspendLayout()
        Me.SuspendLayout()
        '
        'btnPrint
        '
        Me.btnPrint.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.btnPrint.Location = New System.Drawing.Point(241, 285)
        Me.btnPrint.Name = "btnPrint"
        Me.btnPrint.Size = New System.Drawing.Size(75, 23)
        Me.btnPrint.TabIndex = 0
        Me.btnPrint.Text = "&Print..."
        Me.btnPrint.UseVisualStyleBackColor = True
        '
        'btnPreview
        '
        Me.btnPreview.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.btnPreview.Location = New System.Drawing.Point(322, 285)
        Me.btnPreview.Name = "btnPreview"
        Me.btnPreview.Size = New System.Drawing.Size(75, 23)
        Me.btnPreview.TabIndex = 1
        Me.btnPreview.Text = "Pre&view..."
        Me.btnPreview.UseVisualStyleBackColor = True
        '
        'btnClose
        '
        Me.btnClose.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.btnClose.Location = New System.Drawing.Point(403, 285)
        Me.btnClose.Name = "btnClose"
        Me.btnClose.Size = New System.Drawing.Size(75, 23)
        Me.btnClose.TabIndex = 2
        Me.btnClose.Text = "&Close"
        Me.btnClose.UseVisualStyleBackColor = True
        '
        'grpReportType
        '
        Me.grpReportType.Controls.Add(Me.optDetail)
        Me.grpReportType.Controls.Add(Me.optSummary)
        Me.grpReportType.Location = New System.Drawing.Point(12, 12)
        Me.grpReportType.Name = "grpReportType"
        Me.grpReportType.Size = New System.Drawing.Size(230, 70)
        Me.grpReportType.TabIndex = 3
        Me.grpReportType.TabStop = False
        Me.grpReportType.Text = "Report Type"
        '
        'optDetail
        '
        Me.optDetail.AutoSize = True
        Me.optDetail.Location = New System.Drawing.Point(135, 27)
        Me.optDetail.Name = "optDetail"
        Me.optDetail.Size = New System.Drawing.Size(52, 17)
        Me.optDetail.TabIndex = 1
        Me.optDetail.Text = "Detail"
        Me.optDetail.UseVisualStyleBackColor = True
        '
        'optSummary
        '
        Me.optSummary.AutoSize = True
        Me.optSummary.Checked = True
        Me.optSummary.Location = New System.Drawing.Point(44, 27)
        Me.optSummary.Name = "optSummary"
        Me.optSummary.Size = New System.Drawing.Size(68, 17)
        Me.optSummary.TabIndex = 0
        Me.optSummary.TabStop = True
        Me.optSummary.Text = "Summary"
        Me.optSummary.UseVisualStyleBackColor = True
        '
        'gprServers
        '
        Me.gprServers.Controls.Add(Me.cboServers)
        Me.gprServers.Controls.Add(Me.optSelectedServer)
        Me.gprServers.Controls.Add(Me.optAllServers)
        Me.gprServers.Location = New System.Drawing.Point(12, 171)
        Me.gprServers.Name = "gprServers"
        Me.gprServers.Size = New System.Drawing.Size(230, 100)
        Me.gprServers.TabIndex = 0
        Me.gprServers.TabStop = False
        Me.gprServers.Text = "Servers"
        '
        'cboServers
        '
        Me.cboServers.DataSource = Me.ServersBindingSource
        Me.cboServers.DisplayMember = "NameString"
        Me.cboServers.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList
        Me.cboServers.Enabled = False
        Me.cboServers.FormattingEnabled = True
        Me.cboServers.Location = New System.Drawing.Point(44, 63)
        Me.cboServers.Name = "cboServers"
        Me.cboServers.Size = New System.Drawing.Size(172, 21)
        Me.cboServers.TabIndex = 2
        Me.cboServers.ValueMember = "ServerNumber"
        '
        'ServersBindingSource
        '
        Me.ServersBindingSource.AllowNew = False
        Me.ServersBindingSource.DataMember = "Servers"
        Me.ServersBindingSource.DataSource = Me.ServersLookupDataset
        '
        'ServersLookupDataset
        '
        Me.ServersLookupDataset.DataSetName = "ServersLookupDataset"
        Me.ServersLookupDataset.SchemaSerializationMode = System.Data.SchemaSerializationMode.IncludeSchema
        '
        'optSelectedServer
        '
        Me.optSelectedServer.AutoSize = True
        Me.optSelectedServer.Location = New System.Drawing.Point(22, 40)
        Me.optSelectedServer.Name = "optSelectedServer"
        Me.optSelectedServer.Size = New System.Drawing.Size(104, 17)
        Me.optSelectedServer.TabIndex = 1
        Me.optSelectedServer.Text = "Selected Server:"
        Me.optSelectedServer.UseVisualStyleBackColor = True
        '
        'optAllServers
        '
        Me.optAllServers.AutoSize = True
        Me.optAllServers.Checked = True
        Me.optAllServers.Location = New System.Drawing.Point(22, 17)
        Me.optAllServers.Name = "optAllServers"
        Me.optAllServers.Size = New System.Drawing.Size(75, 17)
        Me.optAllServers.TabIndex = 0
        Me.optAllServers.TabStop = True
        Me.optAllServers.Text = "All Servers"
        Me.optAllServers.UseVisualStyleBackColor = True
        '
        'grpDates
        '
        Me.grpDates.Controls.Add(Me.txtSelectedDate)
        Me.grpDates.Controls.Add(Me.optSelectedDate)
        Me.grpDates.Controls.Add(Me.optAllDates)
        Me.grpDates.Location = New System.Drawing.Point(248, 12)
        Me.grpDates.Name = "grpDates"
        Me.grpDates.Size = New System.Drawing.Size(230, 100)
        Me.grpDates.TabIndex = 0
        Me.grpDates.TabStop = False
        Me.grpDates.Text = "Dates"
        '
        'txtSelectedDate
        '
        Me.txtSelectedDate.Enabled = False
        Me.txtSelectedDate.Location = New System.Drawing.Point(119, 53)
        Me.txtSelectedDate.Mask = "00/00/0000"
        Me.txtSelectedDate.Name = "txtSelectedDate"
        Me.txtSelectedDate.Size = New System.Drawing.Size(100, 20)
        Me.txtSelectedDate.TabIndex = 2
        Me.txtSelectedDate.ValidatingType = GetType(Date)
        '
        'optSelectedDate
        '
        Me.optSelectedDate.AutoSize = True
        Me.optSelectedDate.Location = New System.Drawing.Point(17, 53)
        Me.optSelectedDate.Name = "optSelectedDate"
        Me.optSelectedDate.Size = New System.Drawing.Size(96, 17)
        Me.optSelectedDate.TabIndex = 1
        Me.optSelectedDate.Text = "Selected Date:"
        Me.optSelectedDate.UseVisualStyleBackColor = True
        '
        'optAllDates
        '
        Me.optAllDates.AutoSize = True
        Me.optAllDates.Checked = True
        Me.optAllDates.Location = New System.Drawing.Point(17, 30)
        Me.optAllDates.Name = "optAllDates"
        Me.optAllDates.Size = New System.Drawing.Size(67, 17)
        Me.optAllDates.TabIndex = 0
        Me.optAllDates.TabStop = True
        Me.optAllDates.Text = "All Dates"
        Me.optAllDates.UseVisualStyleBackColor = True
        '
        'grpTips
        '
        Me.grpTips.Controls.Add(Me.optCash)
        Me.grpTips.Controls.Add(Me.optSpecialFunction)
        Me.grpTips.Controls.Add(Me.optRoomCharge)
        Me.grpTips.Controls.Add(Me.optCreditCard)
        Me.grpTips.Location = New System.Drawing.Point(248, 118)
        Me.grpTips.Name = "grpTips"
        Me.grpTips.Size = New System.Drawing.Size(230, 153)
        Me.grpTips.TabIndex = 0
        Me.grpTips.TabStop = False
        Me.grpTips.Text = "Tips"
        '
        'optCash
        '
        Me.optCash.AutoSize = True
        Me.optCash.Checked = True
        Me.optCash.CheckState = System.Windows.Forms.CheckState.Checked
        Me.optCash.Location = New System.Drawing.Point(63, 102)
        Me.optCash.Name = "optCash"
        Me.optCash.Size = New System.Drawing.Size(50, 17)
        Me.optCash.TabIndex = 3
        Me.optCash.Text = "Cash"
        Me.optCash.UseVisualStyleBackColor = True
        '
        'optSpecialFunction
        '
        Me.optSpecialFunction.AutoSize = True
        Me.optSpecialFunction.Checked = True
        Me.optSpecialFunction.CheckState = System.Windows.Forms.CheckState.Checked
        Me.optSpecialFunction.Location = New System.Drawing.Point(63, 79)
        Me.optSpecialFunction.Name = "optSpecialFunction"
        Me.optSpecialFunction.Size = New System.Drawing.Size(105, 17)
        Me.optSpecialFunction.TabIndex = 2
        Me.optSpecialFunction.Text = "Special Function"
        Me.optSpecialFunction.UseVisualStyleBackColor = True
        '
        'optRoomCharge
        '
        Me.optRoomCharge.AutoSize = True
        Me.optRoomCharge.Checked = True
        Me.optRoomCharge.CheckState = System.Windows.Forms.CheckState.Checked
        Me.optRoomCharge.Location = New System.Drawing.Point(63, 56)
        Me.optRoomCharge.Name = "optRoomCharge"
        Me.optRoomCharge.Size = New System.Drawing.Size(91, 17)
        Me.optRoomCharge.TabIndex = 1
        Me.optRoomCharge.Text = "Room Charge"
        Me.optRoomCharge.UseVisualStyleBackColor = True
        '
        'optCreditCard
        '
        Me.optCreditCard.AutoSize = True
        Me.optCreditCard.Checked = True
        Me.optCreditCard.CheckState = System.Windows.Forms.CheckState.Checked
        Me.optCreditCard.Location = New System.Drawing.Point(63, 33)
        Me.optCreditCard.Name = "optCreditCard"
        Me.optCreditCard.Size = New System.Drawing.Size(78, 17)
        Me.optCreditCard.TabIndex = 0
        Me.optCreditCard.Text = "Credit Card"
        Me.optCreditCard.UseVisualStyleBackColor = True
        '
        'grpPrintOrder
        '
        Me.grpPrintOrder.Controls.Add(Me.optByDate)
        Me.grpPrintOrder.Controls.Add(Me.optByServer)
        Me.grpPrintOrder.Location = New System.Drawing.Point(12, 88)
        Me.grpPrintOrder.Name = "grpPrintOrder"
        Me.grpPrintOrder.Size = New System.Drawing.Size(230, 80)
        Me.grpPrintOrder.TabIndex = 4
        Me.grpPrintOrder.TabStop = False
        Me.grpPrintOrder.Text = "Print Order"
        '
        'optByDate
        '
        Me.optByDate.AutoSize = True
        Me.optByDate.Location = New System.Drawing.Point(135, 32)
        Me.optByDate.Name = "optByDate"
        Me.optByDate.Size = New System.Drawing.Size(63, 17)
        Me.optByDate.TabIndex = 1
        Me.optByDate.Text = "By Date"
        Me.optByDate.UseVisualStyleBackColor = True
        '
        'optByServer
        '
        Me.optByServer.AutoSize = True
        Me.optByServer.Checked = True
        Me.optByServer.Location = New System.Drawing.Point(44, 32)
        Me.optByServer.Name = "optByServer"
        Me.optByServer.Size = New System.Drawing.Size(71, 17)
        Me.optByServer.TabIndex = 0
        Me.optByServer.TabStop = True
        Me.optByServer.Text = "By Server"
        Me.optByServer.UseVisualStyleBackColor = True
        '
        'dlgPrint
        '
        Me.dlgPrint.AllowCurrentPage = True
        Me.dlgPrint.AllowPrintToFile = False
        '
        'frmPrintTipReportsV2
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.ClientSize = New System.Drawing.Size(490, 320)
        Me.Controls.Add(Me.grpPrintOrder)
        Me.Controls.Add(Me.gprServers)
        Me.Controls.Add(Me.grpDates)
        Me.Controls.Add(Me.grpTips)
        Me.Controls.Add(Me.grpReportType)
        Me.Controls.Add(Me.btnClose)
        Me.Controls.Add(Me.btnPreview)
        Me.Controls.Add(Me.btnPrint)
        Me.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog
        Me.MaximizeBox = False
        Me.MinimizeBox = False
        Me.Name = "frmPrintTipReportsV2"
        Me.ShowInTaskbar = False
        Me.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent
        Me.Text = "Print Tip Report"
        Me.grpReportType.ResumeLayout(False)
        Me.grpReportType.PerformLayout()
        Me.gprServers.ResumeLayout(False)
        Me.gprServers.PerformLayout()
        CType(Me.ServersBindingSource, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.ServersLookupDataset, System.ComponentModel.ISupportInitialize).EndInit()
        Me.grpDates.ResumeLayout(False)
        Me.grpDates.PerformLayout()
        Me.grpTips.ResumeLayout(False)
        Me.grpTips.PerformLayout()
        Me.grpPrintOrder.ResumeLayout(False)
        Me.grpPrintOrder.PerformLayout()
        Me.ResumeLayout(False)

    End Sub
    Friend WithEvents btnPrint As System.Windows.Forms.Button
    Friend WithEvents btnPreview As System.Windows.Forms.Button
    Friend WithEvents btnClose As System.Windows.Forms.Button
    Friend WithEvents grpReportType As System.Windows.Forms.GroupBox
    Friend WithEvents optDetail As System.Windows.Forms.RadioButton
    Friend WithEvents optSummary As System.Windows.Forms.RadioButton
    Friend WithEvents gprServers As System.Windows.Forms.GroupBox
    Friend WithEvents optSelectedServer As System.Windows.Forms.RadioButton
    Friend WithEvents optAllServers As System.Windows.Forms.RadioButton
    Friend WithEvents grpDates As System.Windows.Forms.GroupBox
    Friend WithEvents optSelectedDate As System.Windows.Forms.RadioButton
    Friend WithEvents optAllDates As System.Windows.Forms.RadioButton
    Friend WithEvents grpTips As System.Windows.Forms.GroupBox
    Friend WithEvents optCash As System.Windows.Forms.CheckBox
    Friend WithEvents optSpecialFunction As System.Windows.Forms.CheckBox
    Friend WithEvents optRoomCharge As System.Windows.Forms.CheckBox
    Friend WithEvents optCreditCard As System.Windows.Forms.CheckBox
    Friend WithEvents txtSelectedDate As System.Windows.Forms.MaskedTextBox
    Friend WithEvents grpPrintOrder As System.Windows.Forms.GroupBox
    Friend WithEvents ServersBindingSource As System.Windows.Forms.BindingSource
    Friend WithEvents cboServers As System.Windows.Forms.ComboBox
    Friend WithEvents ServersLookupDataset As Tip_Tracker.ServersLookupDataset
    Friend WithEvents dlgPrint As System.Windows.Forms.PrintDialog
    Friend WithEvents optByDate As System.Windows.Forms.RadioButton
    Friend WithEvents optByServer As System.Windows.Forms.RadioButton
End Class
