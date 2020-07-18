<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class frmAutoAddInput
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
        Dim resources As System.ComponentModel.ComponentResourceManager = New System.ComponentModel.ComponentResourceManager(GetType(frmAutoAddInput))
        Me.lblEnterSeed = New System.Windows.Forms.Label
        Me.txtSeed = New System.Windows.Forms.TextBox
        Me.btnOK = New System.Windows.Forms.Button
        Me.btnCancel = New System.Windows.Forms.Button
        Me.pnlIntro = New System.Windows.Forms.TableLayoutPanel
        Me.lblIntro = New System.Windows.Forms.Label
        Me.optSuppressChits = New System.Windows.Forms.CheckBox
        Me.pnlIntro.SuspendLayout()
        Me.SuspendLayout()
        '
        'lblEnterSeed
        '
        Me.lblEnterSeed.AutoSize = True
        Me.lblEnterSeed.Location = New System.Drawing.Point(9, 93)
        Me.lblEnterSeed.Name = "lblEnterSeed"
        Me.lblEnterSeed.Size = New System.Drawing.Size(63, 13)
        Me.lblEnterSeed.TabIndex = 3
        Me.lblEnterSeed.Text = "Enter Seed:"
        '
        'txtSeed
        '
        Me.txtSeed.Location = New System.Drawing.Point(78, 90)
        Me.txtSeed.MaxLength = 5
        Me.txtSeed.Name = "txtSeed"
        Me.txtSeed.Size = New System.Drawing.Size(88, 20)
        Me.txtSeed.TabIndex = 0
        '
        'btnOK
        '
        Me.btnOK.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.btnOK.Location = New System.Drawing.Point(156, 125)
        Me.btnOK.Name = "btnOK"
        Me.btnOK.Size = New System.Drawing.Size(75, 23)
        Me.btnOK.TabIndex = 1
        Me.btnOK.Text = "&OK"
        Me.btnOK.UseVisualStyleBackColor = True
        '
        'btnCancel
        '
        Me.btnCancel.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.btnCancel.Location = New System.Drawing.Point(237, 125)
        Me.btnCancel.Name = "btnCancel"
        Me.btnCancel.Size = New System.Drawing.Size(75, 23)
        Me.btnCancel.TabIndex = 2
        Me.btnCancel.Text = "&Cancel"
        Me.btnCancel.UseVisualStyleBackColor = True
        '
        'pnlIntro
        '
        Me.pnlIntro.Anchor = CType(((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Left) _
                    Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.pnlIntro.ColumnCount = 1
        Me.pnlIntro.ColumnStyles.Add(New System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 50.0!))
        Me.pnlIntro.Controls.Add(Me.lblIntro, 0, 0)
        Me.pnlIntro.Location = New System.Drawing.Point(12, 12)
        Me.pnlIntro.Name = "pnlIntro"
        Me.pnlIntro.RowCount = 1
        Me.pnlIntro.RowStyles.Add(New System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 50.0!))
        Me.pnlIntro.Size = New System.Drawing.Size(300, 78)
        Me.pnlIntro.TabIndex = 4
        '
        'lblIntro
        '
        Me.lblIntro.Anchor = CType((((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Bottom) _
                    Or System.Windows.Forms.AnchorStyles.Left) _
                    Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.lblIntro.AutoSize = True
        Me.lblIntro.Location = New System.Drawing.Point(3, 0)
        Me.lblIntro.Name = "lblIntro"
        Me.lblIntro.Size = New System.Drawing.Size(294, 78)
        Me.lblIntro.TabIndex = 0
        Me.lblIntro.Text = resources.GetString("lblIntro.Text")
        '
        'optSuppressChits
        '
        Me.optSuppressChits.AutoSize = True
        Me.optSuppressChits.Location = New System.Drawing.Point(198, 92)
        Me.optSuppressChits.Name = "optSuppressChits"
        Me.optSuppressChits.Size = New System.Drawing.Size(114, 17)
        Me.optSuppressChits.TabIndex = 5
        Me.optSuppressChits.Text = "Suppress Tip Chits"
        Me.optSuppressChits.UseVisualStyleBackColor = True
        '
        'frmAutoAddInput
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.ClientSize = New System.Drawing.Size(324, 160)
        Me.Controls.Add(Me.optSuppressChits)
        Me.Controls.Add(Me.pnlIntro)
        Me.Controls.Add(Me.btnCancel)
        Me.Controls.Add(Me.btnOK)
        Me.Controls.Add(Me.txtSeed)
        Me.Controls.Add(Me.lblEnterSeed)
        Me.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog
        Me.MaximizeBox = False
        Me.MinimizeBox = False
        Me.Name = "frmAutoAddInput"
        Me.ShowIcon = False
        Me.ShowInTaskbar = False
        Me.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent
        Me.Text = "Auto Add Servers"
        Me.pnlIntro.ResumeLayout(False)
        Me.pnlIntro.PerformLayout()
        Me.ResumeLayout(False)
        Me.PerformLayout()

    End Sub
    Friend WithEvents lblEnterSeed As System.Windows.Forms.Label
    Friend WithEvents txtSeed As System.Windows.Forms.TextBox
    Friend WithEvents btnOK As System.Windows.Forms.Button
    Friend WithEvents btnCancel As System.Windows.Forms.Button
    Friend WithEvents pnlIntro As System.Windows.Forms.TableLayoutPanel
    Friend WithEvents lblIntro As System.Windows.Forms.Label
    Friend WithEvents optSuppressChits As System.Windows.Forms.CheckBox
End Class
