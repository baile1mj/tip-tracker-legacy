<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class frmReportByServer
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
        Dim resources As System.ComponentModel.ComponentResourceManager = New System.ComponentModel.ComponentResourceManager(GetType(frmReportByServer))
        Me.btnPrint = New System.Windows.Forms.Button
        Me.btnPreview = New System.Windows.Forms.Button
        Me.btnClose = New System.Windows.Forms.Button
        Me.grpOrderBy = New System.Windows.Forms.GroupBox
        Me.optServerNumber = New System.Windows.Forms.RadioButton
        Me.optServerName = New System.Windows.Forms.RadioButton
        Me.grpInclude = New System.Windows.Forms.GroupBox
        Me.cboSelectServer = New System.Windows.Forms.ComboBox
        Me.optAllServers = New System.Windows.Forms.RadioButton
        Me.optSelectedServer = New System.Windows.Forms.RadioButton
        Me.dlgPrint = New System.Windows.Forms.PrintDialog
        Me.dlgPrintPreview = New System.Windows.Forms.PrintPreviewDialog
        Me.docReport = New System.Drawing.Printing.PrintDocument
        Me.grpOrderBy.SuspendLayout()
        Me.grpInclude.SuspendLayout()
        Me.SuspendLayout()
        '
        'btnPrint
        '
        Me.btnPrint.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.btnPrint.Location = New System.Drawing.Point(111, 261)
        Me.btnPrint.Name = "btnPrint"
        Me.btnPrint.Size = New System.Drawing.Size(75, 23)
        Me.btnPrint.TabIndex = 0
        Me.btnPrint.Text = "Print..."
        Me.btnPrint.UseVisualStyleBackColor = True
        '
        'btnPreview
        '
        Me.btnPreview.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.btnPreview.Location = New System.Drawing.Point(192, 261)
        Me.btnPreview.Name = "btnPreview"
        Me.btnPreview.Size = New System.Drawing.Size(75, 23)
        Me.btnPreview.TabIndex = 1
        Me.btnPreview.Text = "Preview..."
        Me.btnPreview.UseVisualStyleBackColor = True
        '
        'btnClose
        '
        Me.btnClose.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.btnClose.Location = New System.Drawing.Point(273, 261)
        Me.btnClose.Name = "btnClose"
        Me.btnClose.Size = New System.Drawing.Size(75, 23)
        Me.btnClose.TabIndex = 2
        Me.btnClose.Text = "Close"
        Me.btnClose.UseVisualStyleBackColor = True
        '
        'grpOrderBy
        '
        Me.grpOrderBy.Anchor = CType(((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Left) _
                    Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.grpOrderBy.Controls.Add(Me.optServerNumber)
        Me.grpOrderBy.Controls.Add(Me.optServerName)
        Me.grpOrderBy.Location = New System.Drawing.Point(12, 12)
        Me.grpOrderBy.Name = "grpOrderBy"
        Me.grpOrderBy.Size = New System.Drawing.Size(336, 100)
        Me.grpOrderBy.TabIndex = 3
        Me.grpOrderBy.TabStop = False
        Me.grpOrderBy.Text = "Order By"
        '
        'optServerNumber
        '
        Me.optServerNumber.AutoSize = True
        Me.optServerNumber.Location = New System.Drawing.Point(179, 42)
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
        Me.optServerName.Location = New System.Drawing.Point(61, 42)
        Me.optServerName.Name = "optServerName"
        Me.optServerName.Size = New System.Drawing.Size(87, 17)
        Me.optServerName.TabIndex = 0
        Me.optServerName.TabStop = True
        Me.optServerName.Text = "Server Name"
        Me.optServerName.UseVisualStyleBackColor = True
        '
        'grpInclude
        '
        Me.grpInclude.Anchor = CType(((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Left) _
                    Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.grpInclude.Controls.Add(Me.cboSelectServer)
        Me.grpInclude.Controls.Add(Me.optAllServers)
        Me.grpInclude.Controls.Add(Me.optSelectedServer)
        Me.grpInclude.Location = New System.Drawing.Point(12, 118)
        Me.grpInclude.Name = "grpInclude"
        Me.grpInclude.Size = New System.Drawing.Size(336, 137)
        Me.grpInclude.TabIndex = 4
        Me.grpInclude.TabStop = False
        Me.grpInclude.Text = "Include"
        '
        'cboSelectServer
        '
        Me.cboSelectServer.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList
        Me.cboSelectServer.FormattingEnabled = True
        Me.cboSelectServer.Location = New System.Drawing.Point(144, 69)
        Me.cboSelectServer.Name = "cboSelectServer"
        Me.cboSelectServer.Size = New System.Drawing.Size(159, 21)
        Me.cboSelectServer.TabIndex = 7
        '
        'optAllServers
        '
        Me.optAllServers.AutoSize = True
        Me.optAllServers.Checked = True
        Me.optAllServers.Location = New System.Drawing.Point(34, 47)
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
        Me.optSelectedServer.Location = New System.Drawing.Point(34, 70)
        Me.optSelectedServer.Name = "optSelectedServer"
        Me.optSelectedServer.Size = New System.Drawing.Size(104, 17)
        Me.optSelectedServer.TabIndex = 6
        Me.optSelectedServer.Text = "Selected Server:"
        Me.optSelectedServer.UseVisualStyleBackColor = True
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
        Me.docReport.DocumentName = "Special Function Report"
        '
        'frmReportByServer
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.ClientSize = New System.Drawing.Size(360, 296)
        Me.Controls.Add(Me.grpInclude)
        Me.Controls.Add(Me.grpOrderBy)
        Me.Controls.Add(Me.btnClose)
        Me.Controls.Add(Me.btnPreview)
        Me.Controls.Add(Me.btnPrint)
        Me.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog
        Me.MaximizeBox = False
        Me.MinimizeBox = False
        Me.Name = "frmReportByServer"
        Me.ShowInTaskbar = False
        Me.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent
        Me.Text = "Report By Server"
        Me.grpOrderBy.ResumeLayout(False)
        Me.grpOrderBy.PerformLayout()
        Me.grpInclude.ResumeLayout(False)
        Me.grpInclude.PerformLayout()
        Me.ResumeLayout(False)

    End Sub
    Friend WithEvents btnPrint As System.Windows.Forms.Button
    Friend WithEvents btnPreview As System.Windows.Forms.Button
    Friend WithEvents btnClose As System.Windows.Forms.Button
    Friend WithEvents grpOrderBy As System.Windows.Forms.GroupBox
    Friend WithEvents grpInclude As System.Windows.Forms.GroupBox
    Friend WithEvents optServerNumber As System.Windows.Forms.RadioButton
    Friend WithEvents optServerName As System.Windows.Forms.RadioButton
    Friend WithEvents cboSelectServer As System.Windows.Forms.ComboBox
    Friend WithEvents optAllServers As System.Windows.Forms.RadioButton
    Friend WithEvents optSelectedServer As System.Windows.Forms.RadioButton
    Friend WithEvents dlgPrint As System.Windows.Forms.PrintDialog
    Friend WithEvents dlgPrintPreview As System.Windows.Forms.PrintPreviewDialog
    Friend WithEvents docReport As System.Drawing.Printing.PrintDocument
End Class
