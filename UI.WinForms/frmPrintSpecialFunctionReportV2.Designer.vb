<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class frmPrintSpecialFunctionReportV2
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
        Me.btnPreview = New System.Windows.Forms.Button
        Me.btnClose = New System.Windows.Forms.Button
        Me.GroupBox1 = New System.Windows.Forms.GroupBox
        Me.cboFunctions = New System.Windows.Forms.ComboBox
        Me.optAllFunctions = New System.Windows.Forms.RadioButton
        Me.optSelectedFunction = New System.Windows.Forms.RadioButton
        Me.GroupBox2 = New System.Windows.Forms.GroupBox
        Me.optFunctionDate = New System.Windows.Forms.RadioButton
        Me.optFunctionName = New System.Windows.Forms.RadioButton
        Me.GroupBox1.SuspendLayout()
        Me.GroupBox2.SuspendLayout()
        Me.SuspendLayout()
        '
        'btnPrint
        '
        Me.btnPrint.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.btnPrint.Location = New System.Drawing.Point(53, 202)
        Me.btnPrint.Name = "btnPrint"
        Me.btnPrint.Size = New System.Drawing.Size(75, 23)
        Me.btnPrint.TabIndex = 0
        Me.btnPrint.Text = "&Print..."
        Me.btnPrint.UseVisualStyleBackColor = True
        '
        'btnPreview
        '
        Me.btnPreview.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.btnPreview.Location = New System.Drawing.Point(134, 202)
        Me.btnPreview.Name = "btnPreview"
        Me.btnPreview.Size = New System.Drawing.Size(75, 23)
        Me.btnPreview.TabIndex = 1
        Me.btnPreview.Text = "Pre&view..."
        Me.btnPreview.UseVisualStyleBackColor = True
        '
        'btnClose
        '
        Me.btnClose.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.btnClose.Location = New System.Drawing.Point(215, 202)
        Me.btnClose.Name = "btnClose"
        Me.btnClose.Size = New System.Drawing.Size(75, 23)
        Me.btnClose.TabIndex = 2
        Me.btnClose.Text = "&Close"
        Me.btnClose.UseVisualStyleBackColor = True
        '
        'GroupBox1
        '
        Me.GroupBox1.Anchor = CType(((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Left) _
                    Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.GroupBox1.Controls.Add(Me.cboFunctions)
        Me.GroupBox1.Controls.Add(Me.optAllFunctions)
        Me.GroupBox1.Controls.Add(Me.optSelectedFunction)
        Me.GroupBox1.Location = New System.Drawing.Point(12, 86)
        Me.GroupBox1.Name = "GroupBox1"
        Me.GroupBox1.Size = New System.Drawing.Size(278, 103)
        Me.GroupBox1.TabIndex = 7
        Me.GroupBox1.TabStop = False
        Me.GroupBox1.Text = "Include"
        '
        'cboFunctions
        '
        Me.cboFunctions.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList
        Me.cboFunctions.Enabled = False
        Me.cboFunctions.FormattingEnabled = True
        Me.cboFunctions.Location = New System.Drawing.Point(71, 72)
        Me.cboFunctions.Name = "cboFunctions"
        Me.cboFunctions.Size = New System.Drawing.Size(159, 21)
        Me.cboFunctions.TabIndex = 7
        '
        'optAllFunctions
        '
        Me.optAllFunctions.AutoSize = True
        Me.optAllFunctions.Checked = True
        Me.optAllFunctions.Location = New System.Drawing.Point(49, 26)
        Me.optAllFunctions.Name = "optAllFunctions"
        Me.optAllFunctions.Size = New System.Drawing.Size(85, 17)
        Me.optAllFunctions.TabIndex = 5
        Me.optAllFunctions.TabStop = True
        Me.optAllFunctions.Text = "All Functions"
        Me.optAllFunctions.UseVisualStyleBackColor = True
        '
        'optSelectedFunction
        '
        Me.optSelectedFunction.AutoSize = True
        Me.optSelectedFunction.Location = New System.Drawing.Point(49, 49)
        Me.optSelectedFunction.Name = "optSelectedFunction"
        Me.optSelectedFunction.Size = New System.Drawing.Size(114, 17)
        Me.optSelectedFunction.TabIndex = 6
        Me.optSelectedFunction.Text = "Selected Function:"
        Me.optSelectedFunction.UseVisualStyleBackColor = True
        '
        'GroupBox2
        '
        Me.GroupBox2.Anchor = CType(((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Left) _
                    Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.GroupBox2.Controls.Add(Me.optFunctionDate)
        Me.GroupBox2.Controls.Add(Me.optFunctionName)
        Me.GroupBox2.Location = New System.Drawing.Point(12, 12)
        Me.GroupBox2.Name = "GroupBox2"
        Me.GroupBox2.Size = New System.Drawing.Size(278, 68)
        Me.GroupBox2.TabIndex = 6
        Me.GroupBox2.TabStop = False
        Me.GroupBox2.Text = "Order By"
        '
        'optFunctionDate
        '
        Me.optFunctionDate.AutoSize = True
        Me.optFunctionDate.Location = New System.Drawing.Point(153, 26)
        Me.optFunctionDate.Name = "optFunctionDate"
        Me.optFunctionDate.Size = New System.Drawing.Size(92, 17)
        Me.optFunctionDate.TabIndex = 1
        Me.optFunctionDate.Text = "Function Date"
        Me.optFunctionDate.UseVisualStyleBackColor = True
        '
        'optFunctionName
        '
        Me.optFunctionName.AutoSize = True
        Me.optFunctionName.Checked = True
        Me.optFunctionName.Location = New System.Drawing.Point(33, 26)
        Me.optFunctionName.Name = "optFunctionName"
        Me.optFunctionName.Size = New System.Drawing.Size(97, 17)
        Me.optFunctionName.TabIndex = 0
        Me.optFunctionName.TabStop = True
        Me.optFunctionName.Text = "Function Name"
        Me.optFunctionName.UseVisualStyleBackColor = True
        '
        'frmPrintSpecialFunctionReportV2
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.ClientSize = New System.Drawing.Size(302, 237)
        Me.Controls.Add(Me.GroupBox1)
        Me.Controls.Add(Me.GroupBox2)
        Me.Controls.Add(Me.btnClose)
        Me.Controls.Add(Me.btnPreview)
        Me.Controls.Add(Me.btnPrint)
        Me.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog
        Me.MaximizeBox = False
        Me.MinimizeBox = False
        Me.Name = "frmPrintSpecialFunctionReportV2"
        Me.ShowIcon = False
        Me.ShowInTaskbar = False
        Me.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent
        Me.Text = "Print Special Function Detail"
        Me.GroupBox1.ResumeLayout(False)
        Me.GroupBox1.PerformLayout()
        Me.GroupBox2.ResumeLayout(False)
        Me.GroupBox2.PerformLayout()
        Me.ResumeLayout(False)

    End Sub
    Friend WithEvents btnPrint As System.Windows.Forms.Button
    Friend WithEvents btnPreview As System.Windows.Forms.Button
    Friend WithEvents btnClose As System.Windows.Forms.Button
    Friend WithEvents GroupBox1 As System.Windows.Forms.GroupBox
    Friend WithEvents cboFunctions As System.Windows.Forms.ComboBox
    Friend WithEvents optAllFunctions As System.Windows.Forms.RadioButton
    Friend WithEvents optSelectedFunction As System.Windows.Forms.RadioButton
    Friend WithEvents GroupBox2 As System.Windows.Forms.GroupBox
    Friend WithEvents optFunctionDate As System.Windows.Forms.RadioButton
    Friend WithEvents optFunctionName As System.Windows.Forms.RadioButton
End Class
