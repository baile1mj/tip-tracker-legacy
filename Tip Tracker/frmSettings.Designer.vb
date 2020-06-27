<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class frmSettings
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
        Me.grpDefaultDataDirectory = New System.Windows.Forms.GroupBox
        Me.btnSetDefaultDirectory = New System.Windows.Forms.Button
        Me.txtDefaultDataDirectory = New System.Windows.Forms.TextBox
        Me.btnOK = New System.Windows.Forms.Button
        Me.btnCancel = New System.Windows.Forms.Button
        Me.dlgBrowseFolders = New System.Windows.Forms.FolderBrowserDialog
        Me.grpDefaultDataDirectory.SuspendLayout()
        Me.SuspendLayout()
        '
        'grpDefaultDataDirectory
        '
        Me.grpDefaultDataDirectory.Anchor = CType(((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Left) _
                    Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.grpDefaultDataDirectory.Controls.Add(Me.btnSetDefaultDirectory)
        Me.grpDefaultDataDirectory.Controls.Add(Me.txtDefaultDataDirectory)
        Me.grpDefaultDataDirectory.Location = New System.Drawing.Point(12, 12)
        Me.grpDefaultDataDirectory.Name = "grpDefaultDataDirectory"
        Me.grpDefaultDataDirectory.Size = New System.Drawing.Size(337, 91)
        Me.grpDefaultDataDirectory.TabIndex = 0
        Me.grpDefaultDataDirectory.TabStop = False
        Me.grpDefaultDataDirectory.Text = "Default Data Directory"
        '
        'btnSetDefaultDirectory
        '
        Me.btnSetDefaultDirectory.Location = New System.Drawing.Point(6, 56)
        Me.btnSetDefaultDirectory.Name = "btnSetDefaultDirectory"
        Me.btnSetDefaultDirectory.Size = New System.Drawing.Size(75, 23)
        Me.btnSetDefaultDirectory.TabIndex = 0
        Me.btnSetDefaultDirectory.Text = "&Set..."
        Me.btnSetDefaultDirectory.UseVisualStyleBackColor = True
        '
        'txtDefaultDataDirectory
        '
        Me.txtDefaultDataDirectory.Anchor = CType(((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Left) _
                    Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.txtDefaultDataDirectory.Location = New System.Drawing.Point(6, 30)
        Me.txtDefaultDataDirectory.Name = "txtDefaultDataDirectory"
        Me.txtDefaultDataDirectory.ReadOnly = True
        Me.txtDefaultDataDirectory.Size = New System.Drawing.Size(325, 20)
        Me.txtDefaultDataDirectory.TabIndex = 1
        Me.txtDefaultDataDirectory.TabStop = False
        '
        'btnOK
        '
        Me.btnOK.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.btnOK.Location = New System.Drawing.Point(193, 115)
        Me.btnOK.Name = "btnOK"
        Me.btnOK.Size = New System.Drawing.Size(75, 23)
        Me.btnOK.TabIndex = 1
        Me.btnOK.Text = "&OK"
        Me.btnOK.UseVisualStyleBackColor = True
        '
        'btnCancel
        '
        Me.btnCancel.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.btnCancel.Location = New System.Drawing.Point(274, 115)
        Me.btnCancel.Name = "btnCancel"
        Me.btnCancel.Size = New System.Drawing.Size(75, 23)
        Me.btnCancel.TabIndex = 2
        Me.btnCancel.Text = "&Cancel"
        Me.btnCancel.UseVisualStyleBackColor = True
        '
        'dlgBrowseFolders
        '
        Me.dlgBrowseFolders.Description = "Select the folder to be used for storing the data files."
        '
        'frmSettings
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.ClientSize = New System.Drawing.Size(361, 150)
        Me.ControlBox = False
        Me.Controls.Add(Me.btnCancel)
        Me.Controls.Add(Me.btnOK)
        Me.Controls.Add(Me.grpDefaultDataDirectory)
        Me.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog
        Me.MaximizeBox = False
        Me.MinimizeBox = False
        Me.Name = "frmSettings"
        Me.ShowInTaskbar = False
        Me.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent
        Me.Text = "Settings"
        Me.grpDefaultDataDirectory.ResumeLayout(False)
        Me.grpDefaultDataDirectory.PerformLayout()
        Me.ResumeLayout(False)

    End Sub
    Friend WithEvents grpDefaultDataDirectory As System.Windows.Forms.GroupBox
    Friend WithEvents btnOK As System.Windows.Forms.Button
    Friend WithEvents btnCancel As System.Windows.Forms.Button
    Friend WithEvents txtDefaultDataDirectory As System.Windows.Forms.TextBox
    Friend WithEvents btnSetDefaultDirectory As System.Windows.Forms.Button
    Friend WithEvents dlgBrowseFolders As System.Windows.Forms.FolderBrowserDialog
End Class
