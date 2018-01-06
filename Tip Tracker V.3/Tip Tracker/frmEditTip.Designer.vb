<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class frmEditTip
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
        Me.lblAmount = New System.Windows.Forms.Label
        Me.lblWorkingDate = New System.Windows.Forms.Label
        Me.lblTipType = New System.Windows.Forms.Label
        Me.txtAmount = New System.Windows.Forms.TextBox
        Me.cboTipTypes = New System.Windows.Forms.ComboBox
        Me.btnOK = New System.Windows.Forms.Button
        Me.btnCancel = New System.Windows.Forms.Button
        Me.txtWorkingDate = New System.Windows.Forms.MaskedTextBox
        Me.SuspendLayout()
        '
        'lblAmount
        '
        Me.lblAmount.AutoSize = True
        Me.lblAmount.Location = New System.Drawing.Point(12, 25)
        Me.lblAmount.Name = "lblAmount"
        Me.lblAmount.Size = New System.Drawing.Size(99, 13)
        Me.lblAmount.TabIndex = 5
        Me.lblAmount.Text = "Enter New Amount:"
        '
        'lblWorkingDate
        '
        Me.lblWorkingDate.AutoSize = True
        Me.lblWorkingDate.Location = New System.Drawing.Point(12, 52)
        Me.lblWorkingDate.Name = "lblWorkingDate"
        Me.lblWorkingDate.Size = New System.Drawing.Size(129, 13)
        Me.lblWorkingDate.TabIndex = 6
        Me.lblWorkingDate.Text = "Enter New Working Date:"
        '
        'lblTipType
        '
        Me.lblTipType.AutoSize = True
        Me.lblTipType.Location = New System.Drawing.Point(12, 77)
        Me.lblTipType.Name = "lblTipType"
        Me.lblTipType.Size = New System.Drawing.Size(87, 13)
        Me.lblTipType.TabIndex = 7
        Me.lblTipType.Text = "Enter New Type:"
        '
        'txtAmount
        '
        Me.txtAmount.Location = New System.Drawing.Point(161, 22)
        Me.txtAmount.Name = "txtAmount"
        Me.txtAmount.Size = New System.Drawing.Size(100, 20)
        Me.txtAmount.TabIndex = 0
        '
        'cboTipTypes
        '
        Me.cboTipTypes.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList
        Me.cboTipTypes.FormattingEnabled = True
        Me.cboTipTypes.Items.AddRange(New Object() {"Credit Card", "Room Charge", "Special Function", "Cash"})
        Me.cboTipTypes.Location = New System.Drawing.Point(161, 74)
        Me.cboTipTypes.Name = "cboTipTypes"
        Me.cboTipTypes.Size = New System.Drawing.Size(100, 21)
        Me.cboTipTypes.TabIndex = 2
        '
        'btnOK
        '
        Me.btnOK.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.btnOK.Location = New System.Drawing.Point(105, 113)
        Me.btnOK.Name = "btnOK"
        Me.btnOK.Size = New System.Drawing.Size(75, 23)
        Me.btnOK.TabIndex = 3
        Me.btnOK.Text = "&OK"
        Me.btnOK.UseVisualStyleBackColor = True
        '
        'btnCancel
        '
        Me.btnCancel.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.btnCancel.Location = New System.Drawing.Point(186, 113)
        Me.btnCancel.Name = "btnCancel"
        Me.btnCancel.Size = New System.Drawing.Size(75, 23)
        Me.btnCancel.TabIndex = 4
        Me.btnCancel.Text = "&Cancel"
        Me.btnCancel.UseVisualStyleBackColor = True
        '
        'txtWorkingDate
        '
        Me.txtWorkingDate.Location = New System.Drawing.Point(161, 49)
        Me.txtWorkingDate.Mask = "00/00/0000"
        Me.txtWorkingDate.Name = "txtWorkingDate"
        Me.txtWorkingDate.Size = New System.Drawing.Size(100, 20)
        Me.txtWorkingDate.TabIndex = 1
        Me.txtWorkingDate.ValidatingType = GetType(Date)
        '
        'frmEditTip
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.ClientSize = New System.Drawing.Size(273, 148)
        Me.Controls.Add(Me.txtWorkingDate)
        Me.Controls.Add(Me.btnCancel)
        Me.Controls.Add(Me.btnOK)
        Me.Controls.Add(Me.cboTipTypes)
        Me.Controls.Add(Me.txtAmount)
        Me.Controls.Add(Me.lblTipType)
        Me.Controls.Add(Me.lblWorkingDate)
        Me.Controls.Add(Me.lblAmount)
        Me.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog
        Me.MaximizeBox = False
        Me.MinimizeBox = False
        Me.Name = "frmEditTip"
        Me.ShowIcon = False
        Me.ShowInTaskbar = False
        Me.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent
        Me.Text = "Edit Tip"
        Me.ResumeLayout(False)
        Me.PerformLayout()

    End Sub
    Friend WithEvents lblAmount As System.Windows.Forms.Label
    Friend WithEvents lblWorkingDate As System.Windows.Forms.Label
    Friend WithEvents lblTipType As System.Windows.Forms.Label
    Friend WithEvents txtAmount As System.Windows.Forms.TextBox
    Friend WithEvents cboTipTypes As System.Windows.Forms.ComboBox
    Friend WithEvents btnOK As System.Windows.Forms.Button
    Friend WithEvents btnCancel As System.Windows.Forms.Button
    Friend WithEvents txtWorkingDate As System.Windows.Forms.MaskedTextBox
End Class
