<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class frmPrintRegularTipChits
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
        Me.btnPrint = New System.Windows.Forms.Button
        Me.btnPrintPreview = New System.Windows.Forms.Button
        Me.btnClose = New System.Windows.Forms.Button
        Me.grpInclude = New System.Windows.Forms.GroupBox
        Me.optAll = New System.Windows.Forms.RadioButton
        Me.optSelected = New System.Windows.Forms.RadioButton
        Me.cboSelectServer = New System.Windows.Forms.ComboBox
        Me.grpInclude.SuspendLayout()
        Me.SuspendLayout()
        '
        'btnPrint
        '
        Me.btnPrint.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.btnPrint.Location = New System.Drawing.Point(98, 114)
        Me.btnPrint.Name = "btnPrint"
        Me.btnPrint.Size = New System.Drawing.Size(75, 23)
        Me.btnPrint.TabIndex = 0
        Me.btnPrint.Text = "&Print..."
        Me.btnPrint.UseVisualStyleBackColor = True
        '
        'btnPrintPreview
        '
        Me.btnPrintPreview.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.btnPrintPreview.Location = New System.Drawing.Point(179, 114)
        Me.btnPrintPreview.Name = "btnPrintPreview"
        Me.btnPrintPreview.Size = New System.Drawing.Size(75, 23)
        Me.btnPrintPreview.TabIndex = 1
        Me.btnPrintPreview.Text = "Pre&view..."
        Me.btnPrintPreview.UseVisualStyleBackColor = True
        '
        'btnClose
        '
        Me.btnClose.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.btnClose.Location = New System.Drawing.Point(260, 114)
        Me.btnClose.Name = "btnClose"
        Me.btnClose.Size = New System.Drawing.Size(75, 23)
        Me.btnClose.TabIndex = 2
        Me.btnClose.Text = "&Close"
        Me.btnClose.UseVisualStyleBackColor = True
        '
        'grpInclude
        '
        Me.grpInclude.Anchor = CType((((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Bottom) _
                    Or System.Windows.Forms.AnchorStyles.Left) _
                    Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.grpInclude.Controls.Add(Me.optAll)
        Me.grpInclude.Controls.Add(Me.optSelected)
        Me.grpInclude.Controls.Add(Me.cboSelectServer)
        Me.grpInclude.Location = New System.Drawing.Point(12, 12)
        Me.grpInclude.Name = "grpInclude"
        Me.grpInclude.Size = New System.Drawing.Size(323, 96)
        Me.grpInclude.TabIndex = 3
        Me.grpInclude.TabStop = False
        Me.grpInclude.Text = "Servers to Include"
        '
        'optAll
        '
        Me.optAll.AutoSize = True
        Me.optAll.Location = New System.Drawing.Point(28, 27)
        Me.optAll.Name = "optAll"
        Me.optAll.Size = New System.Drawing.Size(36, 17)
        Me.optAll.TabIndex = 4
        Me.optAll.TabStop = True
        Me.optAll.Text = "All"
        Me.optAll.UseVisualStyleBackColor = True
        '
        'optSelected
        '
        Me.optSelected.AutoSize = True
        Me.optSelected.Location = New System.Drawing.Point(28, 50)
        Me.optSelected.Name = "optSelected"
        Me.optSelected.Size = New System.Drawing.Size(70, 17)
        Me.optSelected.TabIndex = 5
        Me.optSelected.TabStop = True
        Me.optSelected.Text = "Selected:"
        Me.optSelected.UseVisualStyleBackColor = True
        '
        'cboSelectServer
        '
        Me.cboSelectServer.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList
        Me.cboSelectServer.FormattingEnabled = True
        Me.cboSelectServer.Location = New System.Drawing.Point(104, 49)
        Me.cboSelectServer.Name = "cboSelectServer"
        Me.cboSelectServer.Size = New System.Drawing.Size(190, 21)
        Me.cboSelectServer.TabIndex = 6
        '
        'frmPrintRegularTipChits
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.ClientSize = New System.Drawing.Size(347, 149)
        Me.Controls.Add(Me.grpInclude)
        Me.Controls.Add(Me.btnClose)
        Me.Controls.Add(Me.btnPrintPreview)
        Me.Controls.Add(Me.btnPrint)
        Me.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog
        Me.MaximizeBox = False
        Me.MinimizeBox = False
        Me.Name = "frmPrintRegularTipChits"
        Me.ShowInTaskbar = False
        Me.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent
        Me.Text = "Print Tip Chits"
        Me.grpInclude.ResumeLayout(False)
        Me.grpInclude.PerformLayout()
        Me.ResumeLayout(False)

    End Sub
    Friend WithEvents btnPrint As System.Windows.Forms.Button
    Friend WithEvents btnPrintPreview As System.Windows.Forms.Button
    Friend WithEvents btnClose As System.Windows.Forms.Button
    Friend WithEvents grpInclude As System.Windows.Forms.GroupBox
    Friend WithEvents optAll As System.Windows.Forms.RadioButton
    Friend WithEvents optSelected As System.Windows.Forms.RadioButton
    Friend WithEvents cboSelectServer As System.Windows.Forms.ComboBox
End Class
