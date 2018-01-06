<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class frmImportFunctionsComparer
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
        Me.grpSelectAction = New System.Windows.Forms.GroupBox
        Me.lblSelectFunction = New System.Windows.Forms.Label
        Me.cboSelectFunction = New System.Windows.Forms.ComboBox
        Me.optNew = New System.Windows.Forms.RadioButton
        Me.optExisting = New System.Windows.Forms.RadioButton
        Me.lblImportedFunction = New System.Windows.Forms.Label
        Me.txtImportedFunction = New System.Windows.Forms.TextBox
        Me.btnCancel = New System.Windows.Forms.Button
        Me.btnNext = New System.Windows.Forms.Button
        Me.txtFunctionName = New System.Windows.Forms.TextBox
        Me.txtFunctionDate = New System.Windows.Forms.MaskedTextBox
        Me.lblFunctionName = New System.Windows.Forms.Label
        Me.lblFunctionDate = New System.Windows.Forms.Label
        Me.grpSelectAction.SuspendLayout()
        Me.SuspendLayout()
        '
        'grpSelectAction
        '
        Me.grpSelectAction.Anchor = CType((((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Bottom) _
                    Or System.Windows.Forms.AnchorStyles.Left) _
                    Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.grpSelectAction.Controls.Add(Me.lblFunctionDate)
        Me.grpSelectAction.Controls.Add(Me.lblFunctionName)
        Me.grpSelectAction.Controls.Add(Me.txtFunctionDate)
        Me.grpSelectAction.Controls.Add(Me.txtFunctionName)
        Me.grpSelectAction.Controls.Add(Me.lblSelectFunction)
        Me.grpSelectAction.Controls.Add(Me.cboSelectFunction)
        Me.grpSelectAction.Controls.Add(Me.optNew)
        Me.grpSelectAction.Controls.Add(Me.optExisting)
        Me.grpSelectAction.Location = New System.Drawing.Point(16, 59)
        Me.grpSelectAction.Name = "grpSelectAction"
        Me.grpSelectAction.Size = New System.Drawing.Size(366, 218)
        Me.grpSelectAction.TabIndex = 5
        Me.grpSelectAction.TabStop = False
        Me.grpSelectAction.Text = "Select Action"
        '
        'lblSelectFunction
        '
        Me.lblSelectFunction.AutoSize = True
        Me.lblSelectFunction.Location = New System.Drawing.Point(47, 48)
        Me.lblSelectFunction.Name = "lblSelectFunction"
        Me.lblSelectFunction.Size = New System.Drawing.Size(84, 13)
        Me.lblSelectFunction.TabIndex = 5
        Me.lblSelectFunction.Text = "Select Function:"
        '
        'cboSelectFunction
        '
        Me.cboSelectFunction.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList
        Me.cboSelectFunction.FormattingEnabled = True
        Me.cboSelectFunction.Location = New System.Drawing.Point(50, 64)
        Me.cboSelectFunction.Name = "cboSelectFunction"
        Me.cboSelectFunction.Size = New System.Drawing.Size(274, 21)
        Me.cboSelectFunction.TabIndex = 0
        '
        'optNew
        '
        Me.optNew.AutoSize = True
        Me.optNew.Location = New System.Drawing.Point(16, 114)
        Me.optNew.Name = "optNew"
        Me.optNew.Size = New System.Drawing.Size(131, 17)
        Me.optNew.TabIndex = 6
        Me.optNew.Text = "This is a new function:"
        Me.optNew.UseVisualStyleBackColor = True
        '
        'optExisting
        '
        Me.optExisting.AutoSize = True
        Me.optExisting.Location = New System.Drawing.Point(16, 19)
        Me.optExisting.Name = "optExisting"
        Me.optExisting.Size = New System.Drawing.Size(152, 17)
        Me.optExisting.TabIndex = 4
        Me.optExisting.Text = "This is an existing function:"
        Me.optExisting.UseVisualStyleBackColor = True
        '
        'lblImportedFunction
        '
        Me.lblImportedFunction.Location = New System.Drawing.Point(12, 9)
        Me.lblImportedFunction.Name = "lblImportedFunction"
        Me.lblImportedFunction.Size = New System.Drawing.Size(100, 47)
        Me.lblImportedFunction.TabIndex = 8
        Me.lblImportedFunction.Text = "This function does not have a match in the current file:"
        '
        'txtImportedFunction
        '
        Me.txtImportedFunction.Anchor = CType(((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Left) _
                    Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.txtImportedFunction.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.txtImportedFunction.Location = New System.Drawing.Point(118, 22)
        Me.txtImportedFunction.Name = "txtImportedFunction"
        Me.txtImportedFunction.ReadOnly = True
        Me.txtImportedFunction.Size = New System.Drawing.Size(264, 20)
        Me.txtImportedFunction.TabIndex = 9
        Me.txtImportedFunction.TabStop = False
        '
        'btnCancel
        '
        Me.btnCancel.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.btnCancel.Location = New System.Drawing.Point(307, 283)
        Me.btnCancel.Name = "btnCancel"
        Me.btnCancel.Size = New System.Drawing.Size(75, 23)
        Me.btnCancel.TabIndex = 7
        Me.btnCancel.Text = "Cancel"
        Me.btnCancel.UseVisualStyleBackColor = True
        '
        'btnNext
        '
        Me.btnNext.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.btnNext.Location = New System.Drawing.Point(226, 283)
        Me.btnNext.Name = "btnNext"
        Me.btnNext.Size = New System.Drawing.Size(75, 23)
        Me.btnNext.TabIndex = 6
        Me.btnNext.Text = "Next >"
        Me.btnNext.UseVisualStyleBackColor = True
        '
        'txtFunctionName
        '
        Me.txtFunctionName.Location = New System.Drawing.Point(50, 159)
        Me.txtFunctionName.Name = "txtFunctionName"
        Me.txtFunctionName.Size = New System.Drawing.Size(168, 20)
        Me.txtFunctionName.TabIndex = 7
        '
        'txtFunctionDate
        '
        Me.txtFunctionDate.Location = New System.Drawing.Point(224, 159)
        Me.txtFunctionDate.Mask = "00/00/0000"
        Me.txtFunctionDate.Name = "txtFunctionDate"
        Me.txtFunctionDate.Size = New System.Drawing.Size(100, 20)
        Me.txtFunctionDate.TabIndex = 8
        Me.txtFunctionDate.ValidatingType = GetType(Date)
        '
        'lblFunctionName
        '
        Me.lblFunctionName.AutoSize = True
        Me.lblFunctionName.Location = New System.Drawing.Point(47, 143)
        Me.lblFunctionName.Name = "lblFunctionName"
        Me.lblFunctionName.Size = New System.Drawing.Size(82, 13)
        Me.lblFunctionName.TabIndex = 9
        Me.lblFunctionName.Text = "Function Name:"
        '
        'lblFunctionDate
        '
        Me.lblFunctionDate.AutoSize = True
        Me.lblFunctionDate.Location = New System.Drawing.Point(221, 143)
        Me.lblFunctionDate.Name = "lblFunctionDate"
        Me.lblFunctionDate.Size = New System.Drawing.Size(77, 13)
        Me.lblFunctionDate.TabIndex = 10
        Me.lblFunctionDate.Text = "Function Date:"
        '
        'frmImportFunctionsComparer
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.ClientSize = New System.Drawing.Size(394, 317)
        Me.ControlBox = False
        Me.Controls.Add(Me.grpSelectAction)
        Me.Controls.Add(Me.lblImportedFunction)
        Me.Controls.Add(Me.txtImportedFunction)
        Me.Controls.Add(Me.btnCancel)
        Me.Controls.Add(Me.btnNext)
        Me.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedToolWindow
        Me.Name = "frmImportFunctionsComparer"
        Me.ShowInTaskbar = False
        Me.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent
        Me.Text = "Select Special Function"
        Me.grpSelectAction.ResumeLayout(False)
        Me.grpSelectAction.PerformLayout()
        Me.ResumeLayout(False)
        Me.PerformLayout()

    End Sub
    Friend WithEvents grpSelectAction As System.Windows.Forms.GroupBox
    Friend WithEvents lblSelectFunction As System.Windows.Forms.Label
    Friend WithEvents cboSelectFunction As System.Windows.Forms.ComboBox
    Friend WithEvents optNew As System.Windows.Forms.RadioButton
    Friend WithEvents optExisting As System.Windows.Forms.RadioButton
    Friend WithEvents lblImportedFunction As System.Windows.Forms.Label
    Friend WithEvents txtImportedFunction As System.Windows.Forms.TextBox
    Friend WithEvents btnCancel As System.Windows.Forms.Button
    Friend WithEvents btnNext As System.Windows.Forms.Button
    Friend WithEvents lblFunctionDate As System.Windows.Forms.Label
    Friend WithEvents lblFunctionName As System.Windows.Forms.Label
    Friend WithEvents txtFunctionDate As System.Windows.Forms.MaskedTextBox
    Friend WithEvents txtFunctionName As System.Windows.Forms.TextBox
End Class
