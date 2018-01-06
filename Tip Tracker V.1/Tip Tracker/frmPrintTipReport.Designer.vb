<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class frmPrintTipReport
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
        Dim resources As System.ComponentModel.ComponentResourceManager = New System.ComponentModel.ComponentResourceManager(GetType(frmPrintTipReport))
        Me.btnPrint = New System.Windows.Forms.Button
        Me.btnPreview = New System.Windows.Forms.Button
        Me.btnCancel = New System.Windows.Forms.Button
        Me.grpReportType = New System.Windows.Forms.GroupBox
        Me.optDetail = New System.Windows.Forms.RadioButton
        Me.optSummary = New System.Windows.Forms.RadioButton
        Me.grpIncludeWorkingDate = New System.Windows.Forms.GroupBox
        Me.cboWorkingDates = New System.Windows.Forms.ComboBox
        Me.optSelectedDate = New System.Windows.Forms.RadioButton
        Me.optAllDates = New System.Windows.Forms.RadioButton
        Me.grpIncludeTipType = New System.Windows.Forms.GroupBox
        Me.cboTipTypes = New System.Windows.Forms.ComboBox
        Me.optSelectedTipType = New System.Windows.Forms.RadioButton
        Me.optAllTipTypes = New System.Windows.Forms.RadioButton
        Me.dlgPrint = New System.Windows.Forms.PrintDialog
        Me.dlgPreview = New System.Windows.Forms.PrintPreviewDialog
        Me.docDateSummary = New System.Drawing.Printing.PrintDocument
        Me.docDateDetail = New System.Drawing.Printing.PrintDocument
        Me.grpReportType.SuspendLayout()
        Me.grpIncludeWorkingDate.SuspendLayout()
        Me.grpIncludeTipType.SuspendLayout()
        Me.SuspendLayout()
        '
        'btnPrint
        '
        Me.btnPrint.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.btnPrint.Location = New System.Drawing.Point(162, 197)
        Me.btnPrint.Name = "btnPrint"
        Me.btnPrint.Size = New System.Drawing.Size(75, 23)
        Me.btnPrint.TabIndex = 0
        Me.btnPrint.Text = "Print..."
        Me.btnPrint.UseVisualStyleBackColor = True
        '
        'btnPreview
        '
        Me.btnPreview.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.btnPreview.Location = New System.Drawing.Point(243, 197)
        Me.btnPreview.Name = "btnPreview"
        Me.btnPreview.Size = New System.Drawing.Size(75, 23)
        Me.btnPreview.TabIndex = 1
        Me.btnPreview.Text = "Preview..."
        Me.btnPreview.UseVisualStyleBackColor = True
        '
        'btnCancel
        '
        Me.btnCancel.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.btnCancel.Location = New System.Drawing.Point(324, 197)
        Me.btnCancel.Name = "btnCancel"
        Me.btnCancel.Size = New System.Drawing.Size(75, 23)
        Me.btnCancel.TabIndex = 2
        Me.btnCancel.Text = "Cancel"
        Me.btnCancel.UseVisualStyleBackColor = True
        '
        'grpReportType
        '
        Me.grpReportType.Anchor = CType(((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Left) _
                    Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.grpReportType.Controls.Add(Me.optDetail)
        Me.grpReportType.Controls.Add(Me.optSummary)
        Me.grpReportType.Location = New System.Drawing.Point(12, 12)
        Me.grpReportType.Name = "grpReportType"
        Me.grpReportType.Size = New System.Drawing.Size(387, 63)
        Me.grpReportType.TabIndex = 4
        Me.grpReportType.TabStop = False
        Me.grpReportType.Text = "Report Type"
        '
        'optDetail
        '
        Me.optDetail.AutoSize = True
        Me.optDetail.Location = New System.Drawing.Point(224, 23)
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
        Me.optSummary.Location = New System.Drawing.Point(110, 23)
        Me.optSummary.Name = "optSummary"
        Me.optSummary.Size = New System.Drawing.Size(68, 17)
        Me.optSummary.TabIndex = 0
        Me.optSummary.TabStop = True
        Me.optSummary.Text = "Summary"
        Me.optSummary.UseVisualStyleBackColor = True
        '
        'grpIncludeWorkingDate
        '
        Me.grpIncludeWorkingDate.Controls.Add(Me.cboWorkingDates)
        Me.grpIncludeWorkingDate.Controls.Add(Me.optSelectedDate)
        Me.grpIncludeWorkingDate.Controls.Add(Me.optAllDates)
        Me.grpIncludeWorkingDate.Location = New System.Drawing.Point(12, 81)
        Me.grpIncludeWorkingDate.Name = "grpIncludeWorkingDate"
        Me.grpIncludeWorkingDate.Size = New System.Drawing.Size(190, 100)
        Me.grpIncludeWorkingDate.TabIndex = 6
        Me.grpIncludeWorkingDate.TabStop = False
        Me.grpIncludeWorkingDate.Text = "Include Working Date"
        '
        'cboWorkingDates
        '
        Me.cboWorkingDates.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList
        Me.cboWorkingDates.FormattingEnabled = True
        Me.cboWorkingDates.Location = New System.Drawing.Point(54, 65)
        Me.cboWorkingDates.Name = "cboWorkingDates"
        Me.cboWorkingDates.Size = New System.Drawing.Size(121, 21)
        Me.cboWorkingDates.TabIndex = 2
        '
        'optSelectedDate
        '
        Me.optSelectedDate.AutoSize = True
        Me.optSelectedDate.Location = New System.Drawing.Point(29, 42)
        Me.optSelectedDate.Name = "optSelectedDate"
        Me.optSelectedDate.Size = New System.Drawing.Size(70, 17)
        Me.optSelectedDate.TabIndex = 1
        Me.optSelectedDate.Text = "Selected:"
        Me.optSelectedDate.UseVisualStyleBackColor = True
        '
        'optAllDates
        '
        Me.optAllDates.AutoSize = True
        Me.optAllDates.Checked = True
        Me.optAllDates.Location = New System.Drawing.Point(29, 19)
        Me.optAllDates.Name = "optAllDates"
        Me.optAllDates.Size = New System.Drawing.Size(36, 17)
        Me.optAllDates.TabIndex = 0
        Me.optAllDates.TabStop = True
        Me.optAllDates.Text = "All"
        Me.optAllDates.UseVisualStyleBackColor = True
        '
        'grpIncludeTipType
        '
        Me.grpIncludeTipType.Controls.Add(Me.cboTipTypes)
        Me.grpIncludeTipType.Controls.Add(Me.optSelectedTipType)
        Me.grpIncludeTipType.Controls.Add(Me.optAllTipTypes)
        Me.grpIncludeTipType.Location = New System.Drawing.Point(209, 81)
        Me.grpIncludeTipType.Name = "grpIncludeTipType"
        Me.grpIncludeTipType.Size = New System.Drawing.Size(190, 100)
        Me.grpIncludeTipType.TabIndex = 7
        Me.grpIncludeTipType.TabStop = False
        Me.grpIncludeTipType.Text = "Include Tip Type"
        '
        'cboTipTypes
        '
        Me.cboTipTypes.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList
        Me.cboTipTypes.FormattingEnabled = True
        Me.cboTipTypes.Items.AddRange(New Object() {"Cash", "Credit Card", "Room Charge"})
        Me.cboTipTypes.Location = New System.Drawing.Point(63, 65)
        Me.cboTipTypes.Name = "cboTipTypes"
        Me.cboTipTypes.Size = New System.Drawing.Size(121, 21)
        Me.cboTipTypes.Sorted = True
        Me.cboTipTypes.TabIndex = 2
        '
        'optSelectedTipType
        '
        Me.optSelectedTipType.AutoSize = True
        Me.optSelectedTipType.Location = New System.Drawing.Point(19, 42)
        Me.optSelectedTipType.Name = "optSelectedTipType"
        Me.optSelectedTipType.Size = New System.Drawing.Size(67, 17)
        Me.optSelectedTipType.TabIndex = 1
        Me.optSelectedTipType.Text = "Selected"
        Me.optSelectedTipType.UseVisualStyleBackColor = True
        '
        'optAllTipTypes
        '
        Me.optAllTipTypes.AutoSize = True
        Me.optAllTipTypes.Checked = True
        Me.optAllTipTypes.Location = New System.Drawing.Point(19, 19)
        Me.optAllTipTypes.Name = "optAllTipTypes"
        Me.optAllTipTypes.Size = New System.Drawing.Size(36, 17)
        Me.optAllTipTypes.TabIndex = 0
        Me.optAllTipTypes.TabStop = True
        Me.optAllTipTypes.Text = "All"
        Me.optAllTipTypes.UseVisualStyleBackColor = True
        '
        'dlgPrint
        '
        Me.dlgPrint.AllowPrintToFile = False
        '
        'dlgPreview
        '
        Me.dlgPreview.AutoScrollMargin = New System.Drawing.Size(0, 0)
        Me.dlgPreview.AutoScrollMinSize = New System.Drawing.Size(0, 0)
        Me.dlgPreview.ClientSize = New System.Drawing.Size(400, 300)
        Me.dlgPreview.Enabled = True
        Me.dlgPreview.Icon = CType(resources.GetObject("dlgPreview.Icon"), System.Drawing.Icon)
        Me.dlgPreview.Name = "dlgPreview"
        Me.dlgPreview.Visible = False
        '
        'docDateSummary
        '
        Me.docDateSummary.DocumentName = "Tip Report (summary by working date)"
        '
        'docDateDetail
        '
        Me.docDateDetail.DocumentName = "Tip Report (detail by working date)"
        '
        'frmPrintTipReport
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.ClientSize = New System.Drawing.Size(411, 232)
        Me.Controls.Add(Me.grpIncludeTipType)
        Me.Controls.Add(Me.grpIncludeWorkingDate)
        Me.Controls.Add(Me.grpReportType)
        Me.Controls.Add(Me.btnCancel)
        Me.Controls.Add(Me.btnPreview)
        Me.Controls.Add(Me.btnPrint)
        Me.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog
        Me.MaximizeBox = False
        Me.MinimizeBox = False
        Me.Name = "frmPrintTipReport"
        Me.ShowInTaskbar = False
        Me.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent
        Me.Text = "Print Tip Report"
        Me.grpReportType.ResumeLayout(False)
        Me.grpReportType.PerformLayout()
        Me.grpIncludeWorkingDate.ResumeLayout(False)
        Me.grpIncludeWorkingDate.PerformLayout()
        Me.grpIncludeTipType.ResumeLayout(False)
        Me.grpIncludeTipType.PerformLayout()
        Me.ResumeLayout(False)

    End Sub
    Friend WithEvents btnPrint As System.Windows.Forms.Button
    Friend WithEvents btnPreview As System.Windows.Forms.Button
    Friend WithEvents btnCancel As System.Windows.Forms.Button
    Friend WithEvents grpReportType As System.Windows.Forms.GroupBox
    Friend WithEvents optDetail As System.Windows.Forms.RadioButton
    Friend WithEvents optSummary As System.Windows.Forms.RadioButton
    Friend WithEvents grpIncludeWorkingDate As System.Windows.Forms.GroupBox
    Friend WithEvents grpIncludeTipType As System.Windows.Forms.GroupBox
    Friend WithEvents cboWorkingDates As System.Windows.Forms.ComboBox
    Friend WithEvents optSelectedDate As System.Windows.Forms.RadioButton
    Friend WithEvents optAllDates As System.Windows.Forms.RadioButton
    Friend WithEvents cboTipTypes As System.Windows.Forms.ComboBox
    Friend WithEvents optSelectedTipType As System.Windows.Forms.RadioButton
    Friend WithEvents optAllTipTypes As System.Windows.Forms.RadioButton
    Friend WithEvents dlgPrint As System.Windows.Forms.PrintDialog
    Friend WithEvents dlgPreview As System.Windows.Forms.PrintPreviewDialog
    Friend WithEvents docDateSummary As System.Drawing.Printing.PrintDocument
    Friend WithEvents docDateDetail As System.Drawing.Printing.PrintDocument
End Class
