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
        Me.lblAmount = New System.Windows.Forms.Label()
        Me.lblWorkingDate = New System.Windows.Forms.Label()
        Me.lblTipType = New System.Windows.Forms.Label()
        Me.txtAmount = New System.Windows.Forms.TextBox()
        Me.cboTipTypes = New System.Windows.Forms.ComboBox()
        Me.btnOK = New System.Windows.Forms.Button()
        Me.btnCancel = New System.Windows.Forms.Button()
        Me.txtWorkingDate = New System.Windows.Forms.MaskedTextBox()
        Me.TableLayoutPanel1 = New System.Windows.Forms.TableLayoutPanel()
        Me.lblSpecialFunction = New System.Windows.Forms.Label()
        Me.cboSpecialFunction = New System.Windows.Forms.ComboBox()
        Me.TableLayoutPanel1.SuspendLayout
        Me.SuspendLayout
        '
        'lblAmount
        '
        Me.lblAmount.AutoSize = true
        Me.lblAmount.Dock = System.Windows.Forms.DockStyle.Fill
        Me.lblAmount.Location = New System.Drawing.Point(3, 0)
        Me.lblAmount.Name = "lblAmount"
        Me.lblAmount.Size = New System.Drawing.Size(86, 26)
        Me.lblAmount.TabIndex = 0
        Me.lblAmount.Text = "Amount"
        Me.lblAmount.TextAlign = System.Drawing.ContentAlignment.MiddleLeft
        '
        'lblWorkingDate
        '
        Me.lblWorkingDate.AutoSize = true
        Me.lblWorkingDate.Dock = System.Windows.Forms.DockStyle.Fill
        Me.lblWorkingDate.Location = New System.Drawing.Point(3, 26)
        Me.lblWorkingDate.Name = "lblWorkingDate"
        Me.lblWorkingDate.Size = New System.Drawing.Size(86, 26)
        Me.lblWorkingDate.TabIndex = 1
        Me.lblWorkingDate.Text = "Working Date"
        Me.lblWorkingDate.TextAlign = System.Drawing.ContentAlignment.MiddleLeft
        '
        'lblTipType
        '
        Me.lblTipType.AutoSize = true
        Me.lblTipType.Dock = System.Windows.Forms.DockStyle.Fill
        Me.lblTipType.Location = New System.Drawing.Point(3, 52)
        Me.lblTipType.Name = "lblTipType"
        Me.lblTipType.Size = New System.Drawing.Size(86, 27)
        Me.lblTipType.TabIndex = 2
        Me.lblTipType.Text = "Tip Type"
        Me.lblTipType.TextAlign = System.Drawing.ContentAlignment.MiddleLeft
        '
        'txtAmount
        '
        Me.txtAmount.Dock = System.Windows.Forms.DockStyle.Fill
        Me.txtAmount.Location = New System.Drawing.Point(95, 3)
        Me.txtAmount.Name = "txtAmount"
        Me.txtAmount.Size = New System.Drawing.Size(273, 20)
        Me.txtAmount.TabIndex = 4
        '
        'cboTipTypes
        '
        Me.cboTipTypes.Dock = System.Windows.Forms.DockStyle.Fill
        Me.cboTipTypes.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList
        Me.cboTipTypes.FormattingEnabled = true
        Me.cboTipTypes.Location = New System.Drawing.Point(95, 55)
        Me.cboTipTypes.Name = "cboTipTypes"
        Me.cboTipTypes.Size = New System.Drawing.Size(273, 21)
        Me.cboTipTypes.TabIndex = 6
        '
        'btnOK
        '
        Me.btnOK.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Right),System.Windows.Forms.AnchorStyles)
        Me.btnOK.Location = New System.Drawing.Point(227, 150)
        Me.btnOK.Name = "btnOK"
        Me.btnOK.Size = New System.Drawing.Size(75, 23)
        Me.btnOK.TabIndex = 1
        Me.btnOK.Text = "&OK"
        Me.btnOK.UseVisualStyleBackColor = true
        '
        'btnCancel
        '
        Me.btnCancel.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Right),System.Windows.Forms.AnchorStyles)
        Me.btnCancel.Location = New System.Drawing.Point(308, 150)
        Me.btnCancel.Name = "btnCancel"
        Me.btnCancel.Size = New System.Drawing.Size(75, 23)
        Me.btnCancel.TabIndex = 2
        Me.btnCancel.Text = "&Cancel"
        Me.btnCancel.UseVisualStyleBackColor = true
        '
        'txtWorkingDate
        '
        Me.txtWorkingDate.Dock = System.Windows.Forms.DockStyle.Fill
        Me.txtWorkingDate.Location = New System.Drawing.Point(95, 29)
        Me.txtWorkingDate.Mask = "00/00/0099"
        Me.txtWorkingDate.Name = "txtWorkingDate"
        Me.txtWorkingDate.Size = New System.Drawing.Size(273, 20)
        Me.txtWorkingDate.TabIndex = 5
        Me.txtWorkingDate.ValidatingType = GetType(Date)
        '
        'TableLayoutPanel1
        '
        Me.TableLayoutPanel1.Anchor = CType((((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Bottom)  _
            Or System.Windows.Forms.AnchorStyles.Left)  _
            Or System.Windows.Forms.AnchorStyles.Right),System.Windows.Forms.AnchorStyles)
        Me.TableLayoutPanel1.ColumnCount = 2
        Me.TableLayoutPanel1.ColumnStyles.Add(New System.Windows.Forms.ColumnStyle())
        Me.TableLayoutPanel1.ColumnStyles.Add(New System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 100!))
        Me.TableLayoutPanel1.Controls.Add(Me.lblAmount, 0, 0)
        Me.TableLayoutPanel1.Controls.Add(Me.txtWorkingDate, 1, 1)
        Me.TableLayoutPanel1.Controls.Add(Me.txtAmount, 1, 0)
        Me.TableLayoutPanel1.Controls.Add(Me.cboTipTypes, 1, 2)
        Me.TableLayoutPanel1.Controls.Add(Me.lblWorkingDate, 0, 1)
        Me.TableLayoutPanel1.Controls.Add(Me.lblTipType, 0, 2)
        Me.TableLayoutPanel1.Controls.Add(Me.lblSpecialFunction, 0, 3)
        Me.TableLayoutPanel1.Controls.Add(Me.cboSpecialFunction, 1, 3)
        Me.TableLayoutPanel1.Location = New System.Drawing.Point(12, 12)
        Me.TableLayoutPanel1.Name = "TableLayoutPanel1"
        Me.TableLayoutPanel1.RowCount = 5
        Me.TableLayoutPanel1.RowStyles.Add(New System.Windows.Forms.RowStyle())
        Me.TableLayoutPanel1.RowStyles.Add(New System.Windows.Forms.RowStyle())
        Me.TableLayoutPanel1.RowStyles.Add(New System.Windows.Forms.RowStyle())
        Me.TableLayoutPanel1.RowStyles.Add(New System.Windows.Forms.RowStyle())
        Me.TableLayoutPanel1.RowStyles.Add(New System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100!))
        Me.TableLayoutPanel1.Size = New System.Drawing.Size(371, 132)
        Me.TableLayoutPanel1.TabIndex = 0
        '
        'lblSpecialFunction
        '
        Me.lblSpecialFunction.AutoSize = true
        Me.lblSpecialFunction.Dock = System.Windows.Forms.DockStyle.Fill
        Me.lblSpecialFunction.Location = New System.Drawing.Point(3, 79)
        Me.lblSpecialFunction.Name = "lblSpecialFunction"
        Me.lblSpecialFunction.Size = New System.Drawing.Size(86, 27)
        Me.lblSpecialFunction.TabIndex = 3
        Me.lblSpecialFunction.Text = "Special Function"
        Me.lblSpecialFunction.TextAlign = System.Drawing.ContentAlignment.MiddleLeft
        '
        'cboSpecialFunction
        '
        Me.cboSpecialFunction.Dock = System.Windows.Forms.DockStyle.Fill
        Me.cboSpecialFunction.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList
        Me.cboSpecialFunction.FormattingEnabled = true
        Me.cboSpecialFunction.Location = New System.Drawing.Point(95, 82)
        Me.cboSpecialFunction.Name = "cboSpecialFunction"
        Me.cboSpecialFunction.Size = New System.Drawing.Size(273, 21)
        Me.cboSpecialFunction.TabIndex = 7
        '
        'frmEditTip
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6!, 13!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.ClientSize = New System.Drawing.Size(395, 185)
        Me.Controls.Add(Me.TableLayoutPanel1)
        Me.Controls.Add(Me.btnCancel)
        Me.Controls.Add(Me.btnOK)
        Me.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog
        Me.MaximizeBox = false
        Me.MinimizeBox = false
        Me.Name = "frmEditTip"
        Me.ShowIcon = false
        Me.ShowInTaskbar = false
        Me.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent
        Me.Text = "Edit Tip"
        Me.TableLayoutPanel1.ResumeLayout(false)
        Me.TableLayoutPanel1.PerformLayout
        Me.ResumeLayout(false)

End Sub

    Private WithEvents TableLayoutPanel1 As TableLayoutPanel
    Private WithEvents lblSpecialFunction As Label
    Private WithEvents cboSpecialFunction As ComboBox
    Private WithEvents lblAmount As Label
    Private WithEvents lblWorkingDate As Label
    Private WithEvents lblTipType As Label
    Private WithEvents txtAmount As TextBox
    Private WithEvents cboTipTypes As ComboBox
    Private WithEvents btnOK As Button
    Private WithEvents btnCancel As Button
    Private WithEvents txtWorkingDate As MaskedTextBox
End Class
