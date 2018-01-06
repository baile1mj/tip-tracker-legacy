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
        Me.grpSelectAction = New System.Windows.Forms.GroupBox
        Me.lblSelectServer = New System.Windows.Forms.Label
        Me.lblNumber = New System.Windows.Forms.Label
        Me.lblFirstName = New System.Windows.Forms.Label
        Me.lblLastName = New System.Windows.Forms.Label
        Me.cboSelectServer = New System.Windows.Forms.ComboBox
        Me.txtLastName = New System.Windows.Forms.TextBox
        Me.txtFirstName = New System.Windows.Forms.TextBox
        Me.txtServerNumber = New System.Windows.Forms.TextBox
        Me.optNew = New System.Windows.Forms.RadioButton
        Me.optExisting = New System.Windows.Forms.RadioButton
        Me.grpSelectAction.SuspendLayout()
        Me.SuspendLayout()
        '
        'btnNext
        '
        Me.btnNext.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.btnNext.Location = New System.Drawing.Point(226, 282)
        Me.btnNext.Name = "btnNext"
        Me.btnNext.Size = New System.Drawing.Size(75, 23)
        Me.btnNext.TabIndex = 1
        Me.btnNext.Text = "&Next >"
        Me.btnNext.UseVisualStyleBackColor = True
        '
        'btnCancel
        '
        Me.btnCancel.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.btnCancel.Location = New System.Drawing.Point(307, 282)
        Me.btnCancel.Name = "btnCancel"
        Me.btnCancel.Size = New System.Drawing.Size(75, 23)
        Me.btnCancel.TabIndex = 2
        Me.btnCancel.Text = "&Cancel"
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
        Me.txtImportedServer.Size = New System.Drawing.Size(268, 20)
        Me.txtImportedServer.TabIndex = 4
        Me.txtImportedServer.TabStop = False
        '
        'lblImportedServer
        '
        Me.lblImportedServer.Location = New System.Drawing.Point(8, 8)
        Me.lblImportedServer.Name = "lblImportedServer"
        Me.lblImportedServer.Size = New System.Drawing.Size(100, 47)
        Me.lblImportedServer.TabIndex = 3
        Me.lblImportedServer.Text = "This server does not have a match in the current file:"
        '
        'grpSelectAction
        '
        Me.grpSelectAction.Anchor = CType((((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Bottom) _
                    Or System.Windows.Forms.AnchorStyles.Left) _
                    Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.grpSelectAction.Controls.Add(Me.lblSelectServer)
        Me.grpSelectAction.Controls.Add(Me.lblNumber)
        Me.grpSelectAction.Controls.Add(Me.lblFirstName)
        Me.grpSelectAction.Controls.Add(Me.lblLastName)
        Me.grpSelectAction.Controls.Add(Me.cboSelectServer)
        Me.grpSelectAction.Controls.Add(Me.txtLastName)
        Me.grpSelectAction.Controls.Add(Me.txtFirstName)
        Me.grpSelectAction.Controls.Add(Me.txtServerNumber)
        Me.grpSelectAction.Controls.Add(Me.optNew)
        Me.grpSelectAction.Controls.Add(Me.optExisting)
        Me.grpSelectAction.Location = New System.Drawing.Point(12, 58)
        Me.grpSelectAction.Name = "grpSelectAction"
        Me.grpSelectAction.Size = New System.Drawing.Size(370, 218)
        Me.grpSelectAction.TabIndex = 0
        Me.grpSelectAction.TabStop = False
        Me.grpSelectAction.Text = "Select Action"
        '
        'lblSelectServer
        '
        Me.lblSelectServer.AutoSize = True
        Me.lblSelectServer.Location = New System.Drawing.Point(47, 48)
        Me.lblSelectServer.Name = "lblSelectServer"
        Me.lblSelectServer.Size = New System.Drawing.Size(74, 13)
        Me.lblSelectServer.TabIndex = 5
        Me.lblSelectServer.Text = "Select Server:"
        '
        'lblNumber
        '
        Me.lblNumber.AutoSize = True
        Me.lblNumber.Location = New System.Drawing.Point(47, 145)
        Me.lblNumber.Name = "lblNumber"
        Me.lblNumber.Size = New System.Drawing.Size(47, 13)
        Me.lblNumber.TabIndex = 7
        Me.lblNumber.Text = "Number:"
        '
        'lblFirstName
        '
        Me.lblFirstName.AutoSize = True
        Me.lblFirstName.Location = New System.Drawing.Point(117, 145)
        Me.lblFirstName.Name = "lblFirstName"
        Me.lblFirstName.Size = New System.Drawing.Size(60, 13)
        Me.lblFirstName.TabIndex = 8
        Me.lblFirstName.Text = "First Name:"
        '
        'lblLastName
        '
        Me.lblLastName.AutoSize = True
        Me.lblLastName.Location = New System.Drawing.Point(215, 145)
        Me.lblLastName.Name = "lblLastName"
        Me.lblLastName.Size = New System.Drawing.Size(61, 13)
        Me.lblLastName.TabIndex = 9
        Me.lblLastName.Text = "Last Name:"
        '
        'cboSelectServer
        '
        Me.cboSelectServer.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList
        Me.cboSelectServer.FormattingEnabled = True
        Me.cboSelectServer.Location = New System.Drawing.Point(50, 64)
        Me.cboSelectServer.Name = "cboSelectServer"
        Me.cboSelectServer.Size = New System.Drawing.Size(274, 21)
        Me.cboSelectServer.TabIndex = 0
        '
        'txtLastName
        '
        Me.txtLastName.Location = New System.Drawing.Point(215, 161)
        Me.txtLastName.MaxLength = 20
        Me.txtLastName.Name = "txtLastName"
        Me.txtLastName.Size = New System.Drawing.Size(109, 20)
        Me.txtLastName.TabIndex = 3
        '
        'txtFirstName
        '
        Me.txtFirstName.Location = New System.Drawing.Point(117, 161)
        Me.txtFirstName.MaxLength = 20
        Me.txtFirstName.Name = "txtFirstName"
        Me.txtFirstName.Size = New System.Drawing.Size(92, 20)
        Me.txtFirstName.TabIndex = 2
        '
        'txtServerNumber
        '
        Me.txtServerNumber.Location = New System.Drawing.Point(50, 161)
        Me.txtServerNumber.MaxLength = 10
        Me.txtServerNumber.Name = "txtServerNumber"
        Me.txtServerNumber.Size = New System.Drawing.Size(61, 20)
        Me.txtServerNumber.TabIndex = 1
        '
        'optNew
        '
        Me.optNew.AutoSize = True
        Me.optNew.Location = New System.Drawing.Point(16, 114)
        Me.optNew.Name = "optNew"
        Me.optNew.Size = New System.Drawing.Size(122, 17)
        Me.optNew.TabIndex = 6
        Me.optNew.Text = "This is a new server:"
        Me.optNew.UseVisualStyleBackColor = True
        '
        'optExisting
        '
        Me.optExisting.AutoSize = True
        Me.optExisting.Location = New System.Drawing.Point(16, 19)
        Me.optExisting.Name = "optExisting"
        Me.optExisting.Size = New System.Drawing.Size(143, 17)
        Me.optExisting.TabIndex = 4
        Me.optExisting.Text = "This is an existing server:"
        Me.optExisting.UseVisualStyleBackColor = True
        '
        'frmImportServersComparer
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.ClientSize = New System.Drawing.Size(394, 317)
        Me.ControlBox = False
        Me.Controls.Add(Me.grpSelectAction)
        Me.Controls.Add(Me.lblImportedServer)
        Me.Controls.Add(Me.txtImportedServer)
        Me.Controls.Add(Me.btnCancel)
        Me.Controls.Add(Me.btnNext)
        Me.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedToolWindow
        Me.Name = "frmImportServersComparer"
        Me.ShowInTaskbar = False
        Me.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent
        Me.Text = "Select Server"
        Me.grpSelectAction.ResumeLayout(False)
        Me.grpSelectAction.PerformLayout()
        Me.ResumeLayout(False)
        Me.PerformLayout()

    End Sub
    Friend WithEvents btnNext As System.Windows.Forms.Button
    Friend WithEvents btnCancel As System.Windows.Forms.Button
    Friend WithEvents txtImportedServer As System.Windows.Forms.TextBox
    Friend WithEvents lblImportedServer As System.Windows.Forms.Label
    Friend WithEvents grpSelectAction As System.Windows.Forms.GroupBox
    Friend WithEvents cboSelectServer As System.Windows.Forms.ComboBox
    Friend WithEvents txtLastName As System.Windows.Forms.TextBox
    Friend WithEvents txtFirstName As System.Windows.Forms.TextBox
    Friend WithEvents txtServerNumber As System.Windows.Forms.TextBox
    Friend WithEvents optNew As System.Windows.Forms.RadioButton
    Friend WithEvents optExisting As System.Windows.Forms.RadioButton
    Friend WithEvents lblSelectServer As System.Windows.Forms.Label
    Friend WithEvents lblNumber As System.Windows.Forms.Label
    Friend WithEvents lblFirstName As System.Windows.Forms.Label
    Friend WithEvents lblLastName As System.Windows.Forms.Label
End Class
