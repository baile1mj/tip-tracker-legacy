<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class frmPrintSpecialFunctionReport
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
        Dim resources As System.ComponentModel.ComponentResourceManager = New System.ComponentModel.ComponentResourceManager(GetType(frmPrintSpecialFunctionReport))
        Me.grpReportBy = New System.Windows.Forms.GroupBox
        Me.optFunction = New System.Windows.Forms.RadioButton
        Me.optServer = New System.Windows.Forms.RadioButton
        Me.pnlReportByServer = New System.Windows.Forms.Panel
        Me.grpInclude = New System.Windows.Forms.GroupBox
        Me.cboServers = New System.Windows.Forms.ComboBox
        Me.optAllServers = New System.Windows.Forms.RadioButton
        Me.optSelectedServer = New System.Windows.Forms.RadioButton
        Me.grpOrderBy = New System.Windows.Forms.GroupBox
        Me.optServerNumber = New System.Windows.Forms.RadioButton
        Me.optServerName = New System.Windows.Forms.RadioButton
        Me.pnlReportByFunction = New System.Windows.Forms.Panel
        Me.GroupBox1 = New System.Windows.Forms.GroupBox
        Me.cboFunctions = New System.Windows.Forms.ComboBox
        Me.optAllFunctions = New System.Windows.Forms.RadioButton
        Me.optSelectedFunction = New System.Windows.Forms.RadioButton
        Me.GroupBox2 = New System.Windows.Forms.GroupBox
        Me.optFunctionDate = New System.Windows.Forms.RadioButton
        Me.optFunctionName = New System.Windows.Forms.RadioButton
        Me.btnClose = New System.Windows.Forms.Button
        Me.btnPreview = New System.Windows.Forms.Button
        Me.btnPrint = New System.Windows.Forms.Button
        Me.docReportByServer = New System.Drawing.Printing.PrintDocument
        Me.docReportByFunction = New System.Drawing.Printing.PrintDocument
        Me.dlgPrintPreview = New System.Windows.Forms.PrintPreviewDialog
        Me.dlgPrint = New System.Windows.Forms.PrintDialog
        Me.grpReportBy.SuspendLayout()
        Me.pnlReportByServer.SuspendLayout()
        Me.grpInclude.SuspendLayout()
        Me.grpOrderBy.SuspendLayout()
        Me.pnlReportByFunction.SuspendLayout()
        Me.GroupBox1.SuspendLayout()
        Me.GroupBox2.SuspendLayout()
        Me.SuspendLayout()
        '
        'grpReportBy
        '
        Me.grpReportBy.Anchor = CType(((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Left) _
                    Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.grpReportBy.Controls.Add(Me.optFunction)
        Me.grpReportBy.Controls.Add(Me.optServer)
        Me.grpReportBy.Location = New System.Drawing.Point(12, 12)
        Me.grpReportBy.Name = "grpReportBy"
        Me.grpReportBy.Size = New System.Drawing.Size(447, 55)
        Me.grpReportBy.TabIndex = 0
        Me.grpReportBy.TabStop = False
        Me.grpReportBy.Text = "Report By"
        '
        'optFunction
        '
        Me.optFunction.AutoSize = True
        Me.optFunction.Location = New System.Drawing.Point(243, 19)
        Me.optFunction.Name = "optFunction"
        Me.optFunction.Size = New System.Drawing.Size(66, 17)
        Me.optFunction.TabIndex = 1
        Me.optFunction.TabStop = True
        Me.optFunction.Text = "Function"
        Me.optFunction.UseVisualStyleBackColor = True
        '
        'optServer
        '
        Me.optServer.AutoSize = True
        Me.optServer.Location = New System.Drawing.Point(137, 19)
        Me.optServer.Name = "optServer"
        Me.optServer.Size = New System.Drawing.Size(56, 17)
        Me.optServer.TabIndex = 0
        Me.optServer.TabStop = True
        Me.optServer.Text = "Server"
        Me.optServer.UseVisualStyleBackColor = True
        '
        'pnlReportByServer
        '
        Me.pnlReportByServer.Controls.Add(Me.grpInclude)
        Me.pnlReportByServer.Controls.Add(Me.grpOrderBy)
        Me.pnlReportByServer.Location = New System.Drawing.Point(12, 73)
        Me.pnlReportByServer.Name = "pnlReportByServer"
        Me.pnlReportByServer.Size = New System.Drawing.Size(222, 189)
        Me.pnlReportByServer.TabIndex = 1
        '
        'grpInclude
        '
        Me.grpInclude.Anchor = CType(((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Left) _
                    Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.grpInclude.Controls.Add(Me.cboServers)
        Me.grpInclude.Controls.Add(Me.optAllServers)
        Me.grpInclude.Controls.Add(Me.optSelectedServer)
        Me.grpInclude.Location = New System.Drawing.Point(3, 77)
        Me.grpInclude.Name = "grpInclude"
        Me.grpInclude.Size = New System.Drawing.Size(216, 103)
        Me.grpInclude.TabIndex = 5
        Me.grpInclude.TabStop = False
        Me.grpInclude.Text = "Include"
        '
        'cboServers
        '
        Me.cboServers.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList
        Me.cboServers.FormattingEnabled = True
        Me.cboServers.Location = New System.Drawing.Point(51, 72)
        Me.cboServers.Name = "cboServers"
        Me.cboServers.Size = New System.Drawing.Size(159, 21)
        Me.cboServers.TabIndex = 7
        '
        'optAllServers
        '
        Me.optAllServers.AutoSize = True
        Me.optAllServers.Checked = True
        Me.optAllServers.Location = New System.Drawing.Point(29, 26)
        Me.optAllServers.Name = "optAllServers"
        Me.optAllServers.Size = New System.Drawing.Size(75, 17)
        Me.optAllServers.TabIndex = 5
        Me.optAllServers.TabStop = True
        Me.optAllServers.Text = "All Servers"
        Me.optAllServers.UseVisualStyleBackColor = True
        '
        'optSelectedServer
        '
        Me.optSelectedServer.AutoSize = True
        Me.optSelectedServer.Location = New System.Drawing.Point(29, 49)
        Me.optSelectedServer.Name = "optSelectedServer"
        Me.optSelectedServer.Size = New System.Drawing.Size(104, 17)
        Me.optSelectedServer.TabIndex = 6
        Me.optSelectedServer.Text = "Selected Server:"
        Me.optSelectedServer.UseVisualStyleBackColor = True
        '
        'grpOrderBy
        '
        Me.grpOrderBy.Anchor = CType(((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Left) _
                    Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.grpOrderBy.Controls.Add(Me.optServerNumber)
        Me.grpOrderBy.Controls.Add(Me.optServerName)
        Me.grpOrderBy.Location = New System.Drawing.Point(3, 3)
        Me.grpOrderBy.Name = "grpOrderBy"
        Me.grpOrderBy.Size = New System.Drawing.Size(216, 68)
        Me.grpOrderBy.TabIndex = 4
        Me.grpOrderBy.TabStop = False
        Me.grpOrderBy.Text = "Order By"
        '
        'optServerNumber
        '
        Me.optServerNumber.AutoSize = True
        Me.optServerNumber.Location = New System.Drawing.Point(51, 42)
        Me.optServerNumber.Name = "optServerNumber"
        Me.optServerNumber.Size = New System.Drawing.Size(96, 17)
        Me.optServerNumber.TabIndex = 1
        Me.optServerNumber.Text = "Server Number"
        Me.optServerNumber.UseVisualStyleBackColor = True
        '
        'optServerName
        '
        Me.optServerName.AutoSize = True
        Me.optServerName.Checked = True
        Me.optServerName.Location = New System.Drawing.Point(51, 19)
        Me.optServerName.Name = "optServerName"
        Me.optServerName.Size = New System.Drawing.Size(87, 17)
        Me.optServerName.TabIndex = 0
        Me.optServerName.TabStop = True
        Me.optServerName.Text = "Server Name"
        Me.optServerName.UseVisualStyleBackColor = True
        '
        'pnlReportByFunction
        '
        Me.pnlReportByFunction.Controls.Add(Me.GroupBox1)
        Me.pnlReportByFunction.Controls.Add(Me.GroupBox2)
        Me.pnlReportByFunction.Location = New System.Drawing.Point(240, 73)
        Me.pnlReportByFunction.Name = "pnlReportByFunction"
        Me.pnlReportByFunction.Size = New System.Drawing.Size(222, 189)
        Me.pnlReportByFunction.TabIndex = 2
        '
        'GroupBox1
        '
        Me.GroupBox1.Anchor = CType(((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Left) _
                    Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.GroupBox1.Controls.Add(Me.cboFunctions)
        Me.GroupBox1.Controls.Add(Me.optAllFunctions)
        Me.GroupBox1.Controls.Add(Me.optSelectedFunction)
        Me.GroupBox1.Location = New System.Drawing.Point(3, 77)
        Me.GroupBox1.Name = "GroupBox1"
        Me.GroupBox1.Size = New System.Drawing.Size(216, 103)
        Me.GroupBox1.TabIndex = 5
        Me.GroupBox1.TabStop = False
        Me.GroupBox1.Text = "Include"
        '
        'cboFunctions
        '
        Me.cboFunctions.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList
        Me.cboFunctions.FormattingEnabled = True
        Me.cboFunctions.Location = New System.Drawing.Point(51, 72)
        Me.cboFunctions.Name = "cboFunctions"
        Me.cboFunctions.Size = New System.Drawing.Size(159, 21)
        Me.cboFunctions.TabIndex = 7
        '
        'optAllFunctions
        '
        Me.optAllFunctions.AutoSize = True
        Me.optAllFunctions.Checked = True
        Me.optAllFunctions.Location = New System.Drawing.Point(29, 26)
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
        Me.optSelectedFunction.Location = New System.Drawing.Point(29, 49)
        Me.optSelectedFunction.Name = "optSelectedFunction"
        Me.optSelectedFunction.Size = New System.Drawing.Size(114, 17)
        Me.optSelectedFunction.TabIndex = 6
        Me.optSelectedFunction.Text = "Selected Function:"
        Me.optSelectedFunction.UseVisualStyleBackColor = True
        '
        'GroupBox2
        '
        Me.GroupBox2.Anchor = CType(((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Left) _
                    Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.GroupBox2.Controls.Add(Me.optFunctionDate)
        Me.GroupBox2.Controls.Add(Me.optFunctionName)
        Me.GroupBox2.Location = New System.Drawing.Point(3, 3)
        Me.GroupBox2.Name = "GroupBox2"
        Me.GroupBox2.Size = New System.Drawing.Size(216, 68)
        Me.GroupBox2.TabIndex = 4
        Me.GroupBox2.TabStop = False
        Me.GroupBox2.Text = "Order By"
        '
        'optFunctionDate
        '
        Me.optFunctionDate.AutoSize = True
        Me.optFunctionDate.Location = New System.Drawing.Point(51, 42)
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
        Me.optFunctionName.Location = New System.Drawing.Point(51, 19)
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
        Me.btnClose.Location = New System.Drawing.Point(387, 263)
        Me.btnClose.Name = "btnClose"
        Me.btnClose.Size = New System.Drawing.Size(75, 23)
        Me.btnClose.TabIndex = 10
        Me.btnClose.Text = "Close"
        Me.btnClose.UseVisualStyleBackColor = True
        '
        'btnPreview
        '
        Me.btnPreview.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.btnPreview.Location = New System.Drawing.Point(306, 263)
        Me.btnPreview.Name = "btnPreview"
        Me.btnPreview.Size = New System.Drawing.Size(75, 23)
        Me.btnPreview.TabIndex = 9
        Me.btnPreview.Text = "Preview..."
        Me.btnPreview.UseVisualStyleBackColor = True
        '
        'btnPrint
        '
        Me.btnPrint.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.btnPrint.Location = New System.Drawing.Point(225, 263)
        Me.btnPrint.Name = "btnPrint"
        Me.btnPrint.Size = New System.Drawing.Size(75, 23)
        Me.btnPrint.TabIndex = 8
        Me.btnPrint.Text = "Print..."
        Me.btnPrint.UseVisualStyleBackColor = True
        '
        'docReportByServer
        '
        Me.docReportByServer.DocumentName = "Special Function Report (By Server)"
        '
        'docReportByFunction
        '
        Me.docReportByFunction.DocumentName = "Special Function Report (By Function)"
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
        'frmPrintSpecialFunctionReport
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.ClientSize = New System.Drawing.Size(471, 298)
        Me.Controls.Add(Me.btnClose)
        Me.Controls.Add(Me.btnPreview)
        Me.Controls.Add(Me.btnPrint)
        Me.Controls.Add(Me.pnlReportByFunction)
        Me.Controls.Add(Me.pnlReportByServer)
        Me.Controls.Add(Me.grpReportBy)
        Me.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog
        Me.MaximizeBox = False
        Me.MinimizeBox = False
        Me.Name = "frmPrintSpecialFunctionReport"
        Me.ShowInTaskbar = False
        Me.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent
        Me.Text = "Print Special Function Report"
        Me.grpReportBy.ResumeLayout(False)
        Me.grpReportBy.PerformLayout()
        Me.pnlReportByServer.ResumeLayout(False)
        Me.grpInclude.ResumeLayout(False)
        Me.grpInclude.PerformLayout()
        Me.grpOrderBy.ResumeLayout(False)
        Me.grpOrderBy.PerformLayout()
        Me.pnlReportByFunction.ResumeLayout(False)
        Me.GroupBox1.ResumeLayout(False)
        Me.GroupBox1.PerformLayout()
        Me.GroupBox2.ResumeLayout(False)
        Me.GroupBox2.PerformLayout()
        Me.ResumeLayout(False)

    End Sub
    Friend WithEvents grpReportBy As System.Windows.Forms.GroupBox
    Friend WithEvents optFunction As System.Windows.Forms.RadioButton
    Friend WithEvents optServer As System.Windows.Forms.RadioButton
    Friend WithEvents pnlReportByServer As System.Windows.Forms.Panel
    Friend WithEvents grpOrderBy As System.Windows.Forms.GroupBox
    Friend WithEvents optServerNumber As System.Windows.Forms.RadioButton
    Friend WithEvents optServerName As System.Windows.Forms.RadioButton
    Friend WithEvents grpInclude As System.Windows.Forms.GroupBox
    Friend WithEvents cboServers As System.Windows.Forms.ComboBox
    Friend WithEvents optAllServers As System.Windows.Forms.RadioButton
    Friend WithEvents optSelectedServer As System.Windows.Forms.RadioButton
    Friend WithEvents pnlReportByFunction As System.Windows.Forms.Panel
    Friend WithEvents GroupBox1 As System.Windows.Forms.GroupBox
    Friend WithEvents cboFunctions As System.Windows.Forms.ComboBox
    Friend WithEvents optAllFunctions As System.Windows.Forms.RadioButton
    Friend WithEvents optSelectedFunction As System.Windows.Forms.RadioButton
    Friend WithEvents GroupBox2 As System.Windows.Forms.GroupBox
    Friend WithEvents optFunctionDate As System.Windows.Forms.RadioButton
    Friend WithEvents optFunctionName As System.Windows.Forms.RadioButton
    Friend WithEvents btnClose As System.Windows.Forms.Button
    Friend WithEvents btnPreview As System.Windows.Forms.Button
    Friend WithEvents btnPrint As System.Windows.Forms.Button
    Friend WithEvents docReportByServer As System.Drawing.Printing.PrintDocument
    Friend WithEvents docReportByFunction As System.Drawing.Printing.PrintDocument
    Friend WithEvents dlgPrintPreview As System.Windows.Forms.PrintPreviewDialog
    Friend WithEvents dlgPrint As System.Windows.Forms.PrintDialog
End Class
