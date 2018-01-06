<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class frmCreateFile
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
        Me.dtpPeriodStart = New System.Windows.Forms.DateTimePicker
        Me.dtpPeriodEnd = New System.Windows.Forms.DateTimePicker
        Me.btnOK = New System.Windows.Forms.Button
        Me.btnCancel = New System.Windows.Forms.Button
        Me.lblPeriodStart = New System.Windows.Forms.Label
        Me.lblPeriodEnd = New System.Windows.Forms.Label
        Me.SuspendLayout()
        '
        'dtpPeriodStart
        '
        Me.dtpPeriodStart.Anchor = CType((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.dtpPeriodStart.Location = New System.Drawing.Point(112, 25)
        Me.dtpPeriodStart.Name = "dtpPeriodStart"
        Me.dtpPeriodStart.Size = New System.Drawing.Size(200, 20)
        Me.dtpPeriodStart.TabIndex = 0
        '
        'dtpPeriodEnd
        '
        Me.dtpPeriodEnd.Anchor = CType((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.dtpPeriodEnd.Enabled = False
        Me.dtpPeriodEnd.Location = New System.Drawing.Point(112, 51)
        Me.dtpPeriodEnd.Name = "dtpPeriodEnd"
        Me.dtpPeriodEnd.Size = New System.Drawing.Size(200, 20)
        Me.dtpPeriodEnd.TabIndex = 1
        '
        'btnOK
        '
        Me.btnOK.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.btnOK.Location = New System.Drawing.Point(156, 94)
        Me.btnOK.Name = "btnOK"
        Me.btnOK.Size = New System.Drawing.Size(75, 23)
        Me.btnOK.TabIndex = 2
        Me.btnOK.Text = "OK"
        Me.btnOK.UseVisualStyleBackColor = True
        '
        'btnCancel
        '
        Me.btnCancel.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.btnCancel.Location = New System.Drawing.Point(237, 94)
        Me.btnCancel.Name = "btnCancel"
        Me.btnCancel.Size = New System.Drawing.Size(75, 23)
        Me.btnCancel.TabIndex = 3
        Me.btnCancel.Text = "Cancel"
        Me.btnCancel.UseVisualStyleBackColor = True
        '
        'lblPeriodStart
        '
        Me.lblPeriodStart.AutoSize = True
        Me.lblPeriodStart.Location = New System.Drawing.Point(12, 29)
        Me.lblPeriodStart.Name = "lblPeriodStart"
        Me.lblPeriodStart.Size = New System.Drawing.Size(86, 13)
        Me.lblPeriodStart.TabIndex = 4
        Me.lblPeriodStart.Text = "Pay Period Start:"
        '
        'lblPeriodEnd
        '
        Me.lblPeriodEnd.AutoSize = True
        Me.lblPeriodEnd.Location = New System.Drawing.Point(12, 55)
        Me.lblPeriodEnd.Name = "lblPeriodEnd"
        Me.lblPeriodEnd.Size = New System.Drawing.Size(83, 13)
        Me.lblPeriodEnd.TabIndex = 5
        Me.lblPeriodEnd.Text = "Pay Period End:"
        '
        'frmCreateFile
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.ClientSize = New System.Drawing.Size(324, 129)
        Me.ControlBox = False
        Me.Controls.Add(Me.lblPeriodEnd)
        Me.Controls.Add(Me.lblPeriodStart)
        Me.Controls.Add(Me.btnCancel)
        Me.Controls.Add(Me.btnOK)
        Me.Controls.Add(Me.dtpPeriodEnd)
        Me.Controls.Add(Me.dtpPeriodStart)
        Me.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog
        Me.Name = "frmCreateFile"
        Me.ShowInTaskbar = False
        Me.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent
        Me.Text = "Create File"
        Me.ResumeLayout(False)
        Me.PerformLayout()

    End Sub
    Friend WithEvents dtpPeriodStart As System.Windows.Forms.DateTimePicker
    Friend WithEvents dtpPeriodEnd As System.Windows.Forms.DateTimePicker
    Friend WithEvents btnOK As System.Windows.Forms.Button
    Friend WithEvents btnCancel As System.Windows.Forms.Button
    Friend WithEvents lblPeriodStart As System.Windows.Forms.Label
    Friend WithEvents lblPeriodEnd As System.Windows.Forms.Label
End Class
