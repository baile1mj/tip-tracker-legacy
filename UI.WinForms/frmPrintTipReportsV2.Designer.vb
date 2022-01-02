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
        Me.btnPrint = New System.Windows.Forms.Button()
        Me.btnPreview = New System.Windows.Forms.Button()
        Me.btnClose = New System.Windows.Forms.Button()
        Me.grpReportType = New System.Windows.Forms.GroupBox()
        Me.optDetail = New System.Windows.Forms.RadioButton()
        Me.optSummary = New System.Windows.Forms.RadioButton()
        Me.gprServers = New System.Windows.Forms.GroupBox()
        Me.cboServers = New System.Windows.Forms.ComboBox()
        Me.optSelectedServer = New System.Windows.Forms.RadioButton()
        Me.optAllServers = New System.Windows.Forms.RadioButton()
        Me.grpDates = New System.Windows.Forms.GroupBox()
        Me.txtSelectedDate = New System.Windows.Forms.MaskedTextBox()
        Me.optSelectedDate = New System.Windows.Forms.RadioButton()
        Me.optAllDates = New System.Windows.Forms.RadioButton()
        Me.grpTips = New System.Windows.Forms.GroupBox()
        Me.optCash = New System.Windows.Forms.CheckBox()
        Me.optSpecialFunction = New System.Windows.Forms.CheckBox()
        Me.optRoomCharge = New System.Windows.Forms.CheckBox()
        Me.optCreditCard = New System.Windows.Forms.CheckBox()
        Me.grpPrintOrder = New System.Windows.Forms.GroupBox()
        Me.optByDate = New System.Windows.Forms.RadioButton()
        Me.optByServer = New System.Windows.Forms.RadioButton()
        Me.grpReportType.SuspendLayout
        Me.gprServers.SuspendLayout
        Me.grpDates.SuspendLayout
        Me.grpTips.SuspendLayout
        Me.grpPrintOrder.SuspendLayout
        Me.SuspendLayout
        '
        'btnPrint
        '
        Me.btnPrint.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Right),System.Windows.Forms.AnchorStyles)
        Me.btnPrint.Location = New System.Drawing.Point(241, 285)
        Me.btnPrint.Name = "btnPrint"
        Me.btnPrint.Size = New System.Drawing.Size(75, 23)
        Me.btnPrint.TabIndex = 0
        Me.btnPrint.Text = "&Print..."
        Me.btnPrint.UseVisualStyleBackColor = true
        '
        'btnPreview
        '
        Me.btnPreview.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Right),System.Windows.Forms.AnchorStyles)
        Me.btnPreview.Location = New System.Drawing.Point(322, 285)
        Me.btnPreview.Name = "btnPreview"
        Me.btnPreview.Size = New System.Drawing.Size(75, 23)
        Me.btnPreview.TabIndex = 1
        Me.btnPreview.Text = "Pre&view..."
        Me.btnPreview.UseVisualStyleBackColor = true
        '
        'btnClose
        '
        Me.btnClose.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Right),System.Windows.Forms.AnchorStyles)
        Me.btnClose.Location = New System.Drawing.Point(403, 285)
        Me.btnClose.Name = "btnClose"
        Me.btnClose.Size = New System.Drawing.Size(75, 23)
        Me.btnClose.TabIndex = 2
        Me.btnClose.Text = "&Close"
        Me.btnClose.UseVisualStyleBackColor = true
        '
        'grpReportType
        '
        Me.grpReportType.Controls.Add(Me.optDetail)
        Me.grpReportType.Controls.Add(Me.optSummary)
        Me.grpReportType.Location = New System.Drawing.Point(12, 12)
        Me.grpReportType.Name = "grpReportType"
        Me.grpReportType.Size = New System.Drawing.Size(230, 70)
        Me.grpReportType.TabIndex = 3
        Me.grpReportType.TabStop = false
        Me.grpReportType.Text = "Report Type"
        '
        'optDetail
        '
        Me.optDetail.AutoSize = true
        Me.optDetail.Location = New System.Drawing.Point(135, 27)
        Me.optDetail.Name = "optDetail"
        Me.optDetail.Size = New System.Drawing.Size(52, 17)
        Me.optDetail.TabIndex = 1
        Me.optDetail.Text = "Detail"
        Me.optDetail.UseVisualStyleBackColor = true
        '
        'optSummary
        '
        Me.optSummary.AutoSize = true
        Me.optSummary.Checked = true
        Me.optSummary.Location = New System.Drawing.Point(44, 27)
        Me.optSummary.Name = "optSummary"
        Me.optSummary.Size = New System.Drawing.Size(68, 17)
        Me.optSummary.TabIndex = 0
        Me.optSummary.TabStop = true
        Me.optSummary.Text = "Summary"
        Me.optSummary.UseVisualStyleBackColor = true
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
        Me.gprServers.TabStop = false
        Me.gprServers.Text = "Servers"
        '
        'cboServers
        '
        Me.cboServers.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList
        Me.cboServers.Enabled = false
        Me.cboServers.FormattingEnabled = true
        Me.cboServers.Location = New System.Drawing.Point(44, 63)
        Me.cboServers.Name = "cboServers"
        Me.cboServers.Size = New System.Drawing.Size(172, 21)
        Me.cboServers.TabIndex = 2
        Me.cboServers.ValueMember = "ServerNumber"
        '
        'optSelectedServer
        '
        Me.optSelectedServer.AutoSize = true
        Me.optSelectedServer.Location = New System.Drawing.Point(22, 40)
        Me.optSelectedServer.Name = "optSelectedServer"
        Me.optSelectedServer.Size = New System.Drawing.Size(104, 17)
        Me.optSelectedServer.TabIndex = 1
        Me.optSelectedServer.Text = "Selected Server:"
        Me.optSelectedServer.UseVisualStyleBackColor = true
        '
        'optAllServers
        '
        Me.optAllServers.AutoSize = true
        Me.optAllServers.Checked = true
        Me.optAllServers.Location = New System.Drawing.Point(22, 17)
        Me.optAllServers.Name = "optAllServers"
        Me.optAllServers.Size = New System.Drawing.Size(75, 17)
        Me.optAllServers.TabIndex = 0
        Me.optAllServers.TabStop = true
        Me.optAllServers.Text = "All Servers"
        Me.optAllServers.UseVisualStyleBackColor = true
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
        Me.grpDates.TabStop = false
        Me.grpDates.Text = "Dates"
        '
        'txtSelectedDate
        '
        Me.txtSelectedDate.Enabled = false
        Me.txtSelectedDate.Location = New System.Drawing.Point(119, 53)
        Me.txtSelectedDate.Mask = "00/00/0000"
        Me.txtSelectedDate.Name = "txtSelectedDate"
        Me.txtSelectedDate.Size = New System.Drawing.Size(100, 20)
        Me.txtSelectedDate.TabIndex = 2
        Me.txtSelectedDate.ValidatingType = GetType(Date)
        '
        'optSelectedDate
        '
        Me.optSelectedDate.AutoSize = true
        Me.optSelectedDate.Location = New System.Drawing.Point(17, 53)
        Me.optSelectedDate.Name = "optSelectedDate"
        Me.optSelectedDate.Size = New System.Drawing.Size(96, 17)
        Me.optSelectedDate.TabIndex = 1
        Me.optSelectedDate.Text = "Selected Date:"
        Me.optSelectedDate.UseVisualStyleBackColor = true
        '
        'optAllDates
        '
        Me.optAllDates.AutoSize = true
        Me.optAllDates.Checked = true
        Me.optAllDates.Location = New System.Drawing.Point(17, 30)
        Me.optAllDates.Name = "optAllDates"
        Me.optAllDates.Size = New System.Drawing.Size(67, 17)
        Me.optAllDates.TabIndex = 0
        Me.optAllDates.TabStop = true
        Me.optAllDates.Text = "All Dates"
        Me.optAllDates.UseVisualStyleBackColor = true
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
        Me.grpTips.TabStop = false
        Me.grpTips.Text = "Tips"
        '
        'optCash
        '
        Me.optCash.AutoSize = true
        Me.optCash.Checked = true
        Me.optCash.CheckState = System.Windows.Forms.CheckState.Checked
        Me.optCash.Location = New System.Drawing.Point(63, 102)
        Me.optCash.Name = "optCash"
        Me.optCash.Size = New System.Drawing.Size(50, 17)
        Me.optCash.TabIndex = 3
        Me.optCash.Text = "Cash"
        Me.optCash.UseVisualStyleBackColor = true
        '
        'optSpecialFunction
        '
        Me.optSpecialFunction.AutoSize = true
        Me.optSpecialFunction.Checked = true
        Me.optSpecialFunction.CheckState = System.Windows.Forms.CheckState.Checked
        Me.optSpecialFunction.Location = New System.Drawing.Point(63, 79)
        Me.optSpecialFunction.Name = "optSpecialFunction"
        Me.optSpecialFunction.Size = New System.Drawing.Size(105, 17)
        Me.optSpecialFunction.TabIndex = 2
        Me.optSpecialFunction.Text = "Special Function"
        Me.optSpecialFunction.UseVisualStyleBackColor = true
        '
        'optRoomCharge
        '
        Me.optRoomCharge.AutoSize = true
        Me.optRoomCharge.Checked = true
        Me.optRoomCharge.CheckState = System.Windows.Forms.CheckState.Checked
        Me.optRoomCharge.Location = New System.Drawing.Point(63, 56)
        Me.optRoomCharge.Name = "optRoomCharge"
        Me.optRoomCharge.Size = New System.Drawing.Size(91, 17)
        Me.optRoomCharge.TabIndex = 1
        Me.optRoomCharge.Text = "Room Charge"
        Me.optRoomCharge.UseVisualStyleBackColor = true
        '
        'optCreditCard
        '
        Me.optCreditCard.AutoSize = true
        Me.optCreditCard.Checked = true
        Me.optCreditCard.CheckState = System.Windows.Forms.CheckState.Checked
        Me.optCreditCard.Location = New System.Drawing.Point(63, 33)
        Me.optCreditCard.Name = "optCreditCard"
        Me.optCreditCard.Size = New System.Drawing.Size(78, 17)
        Me.optCreditCard.TabIndex = 0
        Me.optCreditCard.Text = "Credit Card"
        Me.optCreditCard.UseVisualStyleBackColor = true
        '
        'grpPrintOrder
        '
        Me.grpPrintOrder.Controls.Add(Me.optByDate)
        Me.grpPrintOrder.Controls.Add(Me.optByServer)
        Me.grpPrintOrder.Location = New System.Drawing.Point(12, 88)
        Me.grpPrintOrder.Name = "grpPrintOrder"
        Me.grpPrintOrder.Size = New System.Drawing.Size(230, 80)
        Me.grpPrintOrder.TabIndex = 4
        Me.grpPrintOrder.TabStop = false
        Me.grpPrintOrder.Text = "Print Order"
        '
        'optByDate
        '
        Me.optByDate.AutoSize = true
        Me.optByDate.Location = New System.Drawing.Point(135, 32)
        Me.optByDate.Name = "optByDate"
        Me.optByDate.Size = New System.Drawing.Size(63, 17)
        Me.optByDate.TabIndex = 1
        Me.optByDate.Text = "By Date"
        Me.optByDate.UseVisualStyleBackColor = true
        '
        'optByServer
        '
        Me.optByServer.AutoSize = true
        Me.optByServer.Checked = true
        Me.optByServer.Location = New System.Drawing.Point(44, 32)
        Me.optByServer.Name = "optByServer"
        Me.optByServer.Size = New System.Drawing.Size(71, 17)
        Me.optByServer.TabIndex = 0
        Me.optByServer.TabStop = true
        Me.optByServer.Text = "By Server"
        Me.optByServer.UseVisualStyleBackColor = true
        '
        'frmPrintTipReportsV2
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6!, 13!)
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
        Me.MaximizeBox = false
        Me.MinimizeBox = false
        Me.Name = "frmPrintTipReportsV2"
        Me.ShowInTaskbar = false
        Me.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent
        Me.Text = "Print Tip Report"
        Me.grpReportType.ResumeLayout(false)
        Me.grpReportType.PerformLayout
        Me.gprServers.ResumeLayout(false)
        Me.gprServers.PerformLayout
        Me.grpDates.ResumeLayout(false)
        Me.grpDates.PerformLayout
        Me.grpTips.ResumeLayout(false)
        Me.grpTips.PerformLayout
        Me.grpPrintOrder.ResumeLayout(false)
        Me.grpPrintOrder.PerformLayout
        Me.ResumeLayout(false)

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
    Friend WithEvents cboServers As System.Windows.Forms.ComboBox
    Friend WithEvents optByDate As System.Windows.Forms.RadioButton
    Friend WithEvents optByServer As System.Windows.Forms.RadioButton
End Class
