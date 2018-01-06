<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class frmAddSpecialFunction
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
        Me.lblFunctionName = New System.Windows.Forms.Label
        Me.txtFunctionName = New System.Windows.Forms.TextBox
        Me.btnOK = New System.Windows.Forms.Button
        Me.btnCancel = New System.Windows.Forms.Button
        Me.lblFunctionDate = New System.Windows.Forms.Label
        Me.txtFunctionDate = New System.Windows.Forms.MaskedTextBox
        Me.SuspendLayout()
        '
        'lblFunctionName
        '
        Me.lblFunctionName.AutoSize = True
        Me.lblFunctionName.Location = New System.Drawing.Point(9, 17)
        Me.lblFunctionName.Name = "lblFunctionName"
        Me.lblFunctionName.Size = New System.Drawing.Size(82, 13)
        Me.lblFunctionName.TabIndex = 4
        Me.lblFunctionName.Text = "Function Name:"
        '
        'txtFunctionName
        '
        Me.txtFunctionName.Anchor = CType(((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Left) _
                    Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.txtFunctionName.Location = New System.Drawing.Point(98, 14)
        Me.txtFunctionName.MaxLength = 30
        Me.txtFunctionName.Name = "txtFunctionName"
        Me.txtFunctionName.Size = New System.Drawing.Size(185, 20)
        Me.txtFunctionName.TabIndex = 0
        '
        'btnOK
        '
        Me.btnOK.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.btnOK.Location = New System.Drawing.Point(127, 81)
        Me.btnOK.Name = "btnOK"
        Me.btnOK.Size = New System.Drawing.Size(75, 23)
        Me.btnOK.TabIndex = 2
        Me.btnOK.Text = "OK"
        Me.btnOK.UseVisualStyleBackColor = True
        '
        'btnCancel
        '
        Me.btnCancel.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.btnCancel.Location = New System.Drawing.Point(208, 81)
        Me.btnCancel.Name = "btnCancel"
        Me.btnCancel.Size = New System.Drawing.Size(75, 23)
        Me.btnCancel.TabIndex = 3
        Me.btnCancel.Text = "Cancel"
        Me.btnCancel.UseVisualStyleBackColor = True
        '
        'lblFunctionDate
        '
        Me.lblFunctionDate.AutoSize = True
        Me.lblFunctionDate.Location = New System.Drawing.Point(12, 43)
        Me.lblFunctionDate.Name = "lblFunctionDate"
        Me.lblFunctionDate.Size = New System.Drawing.Size(77, 13)
        Me.lblFunctionDate.TabIndex = 5
        Me.lblFunctionDate.Text = "Function Date:"
        '
        'txtFunctionDate
        '
        Me.txtFunctionDate.Anchor = CType(((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Left) _
                    Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.txtFunctionDate.Location = New System.Drawing.Point(98, 40)
        Me.txtFunctionDate.Mask = "00/00/0000"
        Me.txtFunctionDate.Name = "txtFunctionDate"
        Me.txtFunctionDate.Size = New System.Drawing.Size(185, 20)
        Me.txtFunctionDate.TabIndex = 1
        Me.txtFunctionDate.ValidatingType = GetType(Date)
        '
        'frmAddSpecialFunction
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.ClientSize = New System.Drawing.Size(295, 116)
        Me.Controls.Add(Me.txtFunctionDate)
        Me.Controls.Add(Me.lblFunctionDate)
        Me.Controls.Add(Me.btnCancel)
        Me.Controls.Add(Me.btnOK)
        Me.Controls.Add(Me.txtFunctionName)
        Me.Controls.Add(Me.lblFunctionName)
        Me.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog
        Me.MaximizeBox = False
        Me.MinimizeBox = False
        Me.Name = "frmAddSpecialFunction"
        Me.ShowInTaskbar = False
        Me.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent
        Me.Text = "Add Special Function"
        Me.ResumeLayout(False)
        Me.PerformLayout()

    End Sub
    Friend WithEvents lblFunctionName As System.Windows.Forms.Label
    Friend WithEvents txtFunctionName As System.Windows.Forms.TextBox
    Friend WithEvents btnOK As System.Windows.Forms.Button
    Friend WithEvents btnCancel As System.Windows.Forms.Button
    Friend WithEvents lblFunctionDate As System.Windows.Forms.Label
    Friend WithEvents txtFunctionDate As System.Windows.Forms.MaskedTextBox
End Class
