<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class frmReportByFunction
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
        Dim resources As System.ComponentModel.ComponentResourceManager = New System.ComponentModel.ComponentResourceManager(GetType(frmReportByFunction))
        Me.grpInclude = New System.Windows.Forms.GroupBox
        Me.cboSelectFunction = New System.Windows.Forms.ComboBox
        Me.optAllFunctions = New System.Windows.Forms.RadioButton
        Me.optSelectedFunction = New System.Windows.Forms.RadioButton
        Me.grpOrderBy = New System.Windows.Forms.GroupBox
        Me.optFunctionDate = New System.Windows.Forms.RadioButton
        Me.optFunctionName = New System.Windows.Forms.RadioButton
        Me.btnClose = New System.Windows.Forms.Button
        Me.btnPreview = New System.Windows.Forms.Button
        Me.btnPrint = New System.Windows.Forms.Button
        Me.dlgPrint = New System.Windows.Forms.PrintDialog
        Me.dlgPrintPreview = New System.Windows.Forms.PrintPreviewDialog
        Me.docReport = New System.Drawing.Printing.PrintDocument
        Me.grpInclude.SuspendLayout()
        Me.grpOrderBy.SuspendLayout()
        Me.SuspendLayout()
        '
        'grpInclude
        '
        Me.grpInclude.Anchor = CType(((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Left) _
                    Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.grpInclude.Controls.Add(Me.cboSelectFunction)
        Me.grpInclude.Controls.Add(Me.optAllFunctions)
        Me.grpInclude.Controls.Add(Me.optSelectedFunction)
        Me.grpInclude.Location = New System.Drawing.Point(11, 117)
        Me.grpInclude.Name = "grpInclude"
        Me.grpInclude.Size = New System.Drawing.Size(336, 137)
        Me.grpInclude.TabIndex = 9
        Me.grpInclude.TabStop = False
        Me.grpInclude.Text = "Include"
        '
        'cboSelectFunction
        '
        Me.cboSelectFunction.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList
        Me.cboSelectFunction.FormattingEnabled = True
        Me.cboSelectFunction.Location = New System.Drawing.Point(154, 69)
        Me.cboSelectFunction.Name = "cboSelectFunction"
        Me.cboSelectFunction.Size = New System.Drawing.Size(159, 21)
        Me.cboSelectFunction.TabIndex = 7
        '
        'optAllFunctions
        '
        Me.optAllFunctions.AutoSize = True
        Me.optAllFunctions.Checked = True
        Me.optAllFunctions.Location = New System.Drawing.Point(34, 47)
        Me.optAllFunctions.Name = "optAllFunctions"
        Me.optAllFunctions.Size = New System.Drawing.Size(85, 17)
        Me.optAllFunctions.TabIndex = 5
        Me.optAllFunctions.TabStop = True
        Me.optAllFunctions.Text = "All Functions"
        Me.optAllFunctions.UseVisualStyleBackColor = True
        '
        'optSelectedFunction
        '
        Me.optSelectedFunction.AutoSize = True
        Me.optSelectedFunction.Location = New System.Drawing.Point(34, 70)
        Me.optSelectedFunction.Name = "optSelectedFunction"
        Me.optSelectedFunction.Size = New System.Drawing.Size(114, 17)
        Me.optSelectedFunction.TabIndex = 6
        Me.optSelectedFunction.Text = "Selected Function:"
        Me.optSelectedFunction.UseVisualStyleBackColor = True
        '
        'grpOrderBy
        '
        Me.grpOrderBy.Anchor = CType(((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Left) _
                    Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.grpOrderBy.Controls.Add(Me.optFunctionDate)
        Me.grpOrderBy.Controls.Add(Me.optFunctionName)
        Me.grpOrderBy.Location = New System.Drawing.Point(11, 11)
        Me.grpOrderBy.Name = "grpOrderBy"
        Me.grpOrderBy.Size = New System.Drawing.Size(336, 100)
        Me.grpOrderBy.TabIndex = 8
        Me.grpOrderBy.TabStop = False
        Me.grpOrderBy.Text = "Order By"
        '
        'optFunctionDate
        '
        Me.optFunctionDate.AutoSize = True
        Me.optFunctionDate.Location = New System.Drawing.Point(181, 42)
        Me.optFunctionDate.Name = "optFunctionDate"
        Me.optFunctionDate.Size = New System.Drawing.Size(92, 17)
        Me.optFunctionDate.TabIndex = 1
        Me.optFunctionDate.Text = "Function Date"
        Me.optFunctionDate.UseVisualStyleBackColor = True
        '
        'optFunctionName
        '
        Me.optFunctionName.AutoSize = True
        Me.optFunctionName.Checked = True
        Me.optFunctionName.Location = New System.Drawing.Point(63, 42)
        Me.optFunctionName.Name = "optFunctionName"
        Me.optFunctionName.Size = New System.Drawing.Size(97, 17)
        Me.optFunctionName.TabIndex = 0
        Me.optFunctionName.TabStop = True
        Me.optFunctionName.Text = "Function Name"
        Me.optFunctionName.UseVisualStyleBackColor = True
        '
        'btnClose
        '
        Me.btnClose.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.btnClose.Location = New System.Drawing.Point(272, 260)
        Me.btnClose.Name = "btnClose"
        Me.btnClose.Size = New System.Drawing.Size(75, 23)
        Me.btnClose.TabIndex = 7
        Me.btnClose.Text = "Close"
        Me.btnClose.UseVisualStyleBackColor = True
        '
        'btnPreview
        '
        Me.btnPreview.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.btnPreview.Location = New System.Drawing.Point(191, 260)
        Me.btnPreview.Name = "btnPreview"
        Me.btnPreview.Size = New System.Drawing.Size(75, 23)
        Me.btnPreview.TabIndex = 6
        Me.btnPreview.Text = "Preview..."
        Me.btnPreview.UseVisualStyleBackColor = True
        '
        'btnPrint
        '
        Me.btnPrint.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.btnPrint.Location = New System.Drawing.Point(110, 260)
        Me.btnPrint.Name = "btnPrint"
        Me.btnPrint.Size = New System.Drawing.Size(75, 23)
        Me.btnPrint.TabIndex = 5
        Me.btnPrint.Text = "Print..."
        Me.btnPrint.UseVisualStyleBackColor = True
        '
        'dlgPrintPreview
        '
        Me.dlgPrintPreview.AutoScrollMargin = New System.Drawing.Size(0, 0)
        Me.dlgPrintPreview.AutoScrollMinSize = New System.Drawing.Size(0, 0)
        Me.dlgPrintPreview.ClientSize = New System.Drawing.Size(400, 300)
        Me.dlgPrintPreview.Enabled = True
        Me.dlgPrintPreview.Icon = CType(resources.GetObject("dlgPrintPreview.Icon"), System.Drawing.Icon)
        Me.dlgPrintPreview.Name = "dlgPrintPreview"
        Me.dlgPrintPreview.ShowIcon = False
        Me.dlgPrintPreview.Visible = False
        '
        'docReport
        '
        Me.docReport.DocumentName = "Server Report"
        '
        'frmReportByFunction
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.ClientSize = New System.Drawing.Size(358, 294)
        Me.Controls.Add(Me.grpInclude)
        Me.Controls.Add(Me.grpOrderBy)
        Me.Controls.Add(Me.btnClose)
        Me.Controls.Add(Me.btnPreview)
        Me.Controls.Add(Me.btnPrint)
        Me.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog
        Me.MaximizeBox = False
        Me.MinimizeBox = False
        Me.Name = "frmReportByFunction"
        Me.ShowIcon = False
        Me.ShowInTaskbar = False
        Me.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent
        Me.Text = "Report By Function"
        Me.grpInclude.ResumeLayout(False)
        Me.grpInclude.PerformLayout()
        Me.grpOrderBy.ResumeLayout(False)
        Me.grpOrderBy.PerformLayout()
        Me.ResumeLayout(False)

    End Sub
    Friend WithEvents grpInclude As System.Windows.Forms.GroupBox
    Friend WithEvents cboSelectFunction As System.Windows.Forms.ComboBox
    Friend WithEvents optAllFunctions As System.Windows.Forms.RadioButton
    Friend WithEvents optSelectedFunction As System.Windows.Forms.RadioButton
    Friend WithEvents grpOrderBy As System.Windows.Forms.GroupBox
    Friend WithEvents optFunctionDate As System.Windows.Forms.RadioButton
    Friend WithEvents optFunctionName As System.Windows.Forms.RadioButton
    Friend WithEvents btnClose As System.Windows.Forms.Button
    Friend WithEvents btnPreview As System.Windows.Forms.Button
    Friend WithEvents btnPrint As System.Windows.Forms.Button
    Friend WithEvents dlgPrint As System.Windows.Forms.PrintDialog
    Friend WithEvents dlgPrintPreview As System.Windows.Forms.PrintPreviewDialog
    Friend WithEvents docReport As System.Drawing.Printing.PrintDocument
End Class
