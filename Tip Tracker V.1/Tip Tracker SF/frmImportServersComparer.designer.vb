<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class frmImportServersComparer
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
        Me.btnNext = New System.Windows.Forms.Button
        Me.btnCancel = New System.Windows.Forms.Button
        Me.txtImportedServer = New System.Windows.Forms.TextBox
        Me.lblImportedServer = New System.Windows.Forms.Label
        Me.optNew = New System.Windows.Forms.RadioButton
        Me.optExisting = New System.Windows.Forms.RadioButton
        Me.lblExistingServer = New System.Windows.Forms.Label
        Me.txtExistingServer = New System.Windows.Forms.TextBox
        Me.lblQuestion = New System.Windows.Forms.Label
        Me.SuspendLayout()
        '
        'btnNext
        '
        Me.btnNext.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.btnNext.Location = New System.Drawing.Point(233, 188)
        Me.btnNext.Name = "btnNext"
        Me.btnNext.Size = New System.Drawing.Size(75, 23)
        Me.btnNext.TabIndex = 1
        Me.btnNext.Text = "Next >"
        Me.btnNext.UseVisualStyleBackColor = True
        '
        'btnCancel
        '
        Me.btnCancel.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.btnCancel.Location = New System.Drawing.Point(314, 188)
        Me.btnCancel.Name = "btnCancel"
        Me.btnCancel.Size = New System.Drawing.Size(75, 23)
        Me.btnCancel.TabIndex = 2
        Me.btnCancel.Text = "Cancel"
        Me.btnCancel.UseVisualStyleBackColor = True
        '
        'txtImportedServer
        '
        Me.txtImportedServer.Anchor = CType(((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Left) _
                    Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.txtImportedServer.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.txtImportedServer.Location = New System.Drawing.Point(114, 21)
        Me.txtImportedServer.Name = "txtImportedServer"
        Me.txtImportedServer.ReadOnly = True
        Me.txtImportedServer.Size = New System.Drawing.Size(275, 20)
        Me.txtImportedServer.TabIndex = 4
        Me.txtImportedServer.TabStop = False
        '
        'lblImportedServer
        '
        Me.lblImportedServer.Location = New System.Drawing.Point(8, 8)
        Me.lblImportedServer.Name = "lblImportedServer"
        Me.lblImportedServer.Size = New System.Drawing.Size(100, 47)
        Me.lblImportedServer.TabIndex = 3
        Me.lblImportedServer.Text = "This server was found in the imported server list:"
        '
        'optNew
        '
        Me.optNew.AutoSize = True
        Me.optNew.Location = New System.Drawing.Point(91, 161)
        Me.optNew.Name = "optNew"
        Me.optNew.Size = New System.Drawing.Size(237, 17)
        Me.optNew.TabIndex = 6
        Me.optNew.Text = "No.  The imported server is a different server."
        Me.optNew.UseVisualStyleBackColor = True
        '
        'optExisting
        '
        Me.optExisting.AutoSize = True
        Me.optExisting.Location = New System.Drawing.Point(91, 138)
        Me.optExisting.Name = "optExisting"
        Me.optExisting.Size = New System.Drawing.Size(181, 17)
        Me.optExisting.TabIndex = 4
        Me.optExisting.Text = "Yes.  These are the same server."
        Me.optExisting.UseVisualStyleBackColor = True
        '
        'lblExistingServer
        '
        Me.lblExistingServer.Location = New System.Drawing.Point(8, 55)
        Me.lblExistingServer.Name = "lblExistingServer"
        Me.lblExistingServer.Size = New System.Drawing.Size(100, 47)
        Me.lblExistingServer.TabIndex = 10
        Me.lblExistingServer.Text = "This server was found in the template list:"
        '
        'txtExistingServer
        '
        Me.txtExistingServer.Anchor = CType(((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Left) _
                    Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.txtExistingServer.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.txtExistingServer.Location = New System.Drawing.Point(114, 68)
        Me.txtExistingServer.Name = "txtExistingServer"
        Me.txtExistingServer.ReadOnly = True
        Me.txtExistingServer.Size = New System.Drawing.Size(275, 20)
        Me.txtExistingServer.TabIndex = 11
        Me.txtExistingServer.TabStop = False
        '
        'lblQuestion
        '
        Me.lblQuestion.AutoSize = True
        Me.lblQuestion.Location = New System.Drawing.Point(8, 122)
        Me.lblQuestion.Name = "lblQuestion"
        Me.lblQuestion.Size = New System.Drawing.Size(136, 13)
        Me.lblQuestion.TabIndex = 12
        Me.lblQuestion.Text = "Are these the same server?"
        '
        'frmImportServersComparer
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.ClientSize = New System.Drawing.Size(401, 223)
        Me.ControlBox = False
        Me.Controls.Add(Me.lblQuestion)
        Me.Controls.Add(Me.lblExistingServer)
        Me.Controls.Add(Me.txtExistingServer)
        Me.Controls.Add(Me.lblImportedServer)
        Me.Controls.Add(Me.txtImportedServer)
        Me.Controls.Add(Me.btnCancel)
        Me.Controls.Add(Me.btnNext)
        Me.Controls.Add(Me.optExisting)
        Me.Controls.Add(Me.optNew)
        Me.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedToolWindow
        Me.Name = "frmImportServersComparer"
        Me.ShowInTaskbar = False
        Me.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent
        Me.Text = "Select Server"
        Me.ResumeLayout(False)
        Me.PerformLayout()

    End Sub
    Friend WithEvents btnNext As System.Windows.Forms.Button
    Friend WithEvents btnCancel As System.Windows.Forms.Button
    Friend WithEvents txtImportedServer As System.Windows.Forms.TextBox
    Friend WithEvents lblImportedServer As System.Windows.Forms.Label
    Friend WithEvents optNew As System.Windows.Forms.RadioButton
    Friend WithEvents optExisting As System.Windows.Forms.RadioButton
    Friend WithEvents lblExistingServer As System.Windows.Forms.Label
    Friend WithEvents txtExistingServer As System.Windows.Forms.TextBox
    Friend WithEvents lblQuestion As System.Windows.Forms.Label
End Class
