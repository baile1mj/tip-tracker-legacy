<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class frmMain
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
        Dim resources As System.ComponentModel.ComponentResourceManager = New System.ComponentModel.ComponentResourceManager(GetType(frmMain))
        Me.lblInstruction = New System.Windows.Forms.Label
        Me.btnDataFile = New System.Windows.Forms.Button
        Me.btnGlobalFile = New System.Windows.Forms.Button
        Me.btnExit = New System.Windows.Forms.Button
        Me.btnDecodeFile = New System.Windows.Forms.Button
        Me.btnEncodeFile = New System.Windows.Forms.Button
        Me.btnForceGlobalFileReset = New System.Windows.Forms.Button
        Me.SuspendLayout()
        '
        'lblInstruction
        '
        Me.lblInstruction.AutoSize = True
        Me.lblInstruction.Location = New System.Drawing.Point(12, 9)
        Me.lblInstruction.Name = "lblInstruction"
        Me.lblInstruction.Size = New System.Drawing.Size(87, 13)
        Me.lblInstruction.TabIndex = 4
        Me.lblInstruction.Text = "Select an option:"
        '
        'btnDataFile
        '
        Me.btnDataFile.Location = New System.Drawing.Point(77, 64)
        Me.btnDataFile.Name = "btnDataFile"
        Me.btnDataFile.Size = New System.Drawing.Size(152, 23)
        Me.btnDataFile.TabIndex = 1
        Me.btnDataFile.Text = "&Data File Maintenance"
        Me.btnDataFile.UseVisualStyleBackColor = True
        '
        'btnGlobalFile
        '
        Me.btnGlobalFile.Location = New System.Drawing.Point(77, 35)
        Me.btnGlobalFile.Name = "btnGlobalFile"
        Me.btnGlobalFile.Size = New System.Drawing.Size(152, 23)
        Me.btnGlobalFile.TabIndex = 0
        Me.btnGlobalFile.Text = "&Global File Maintenance"
        Me.btnGlobalFile.UseVisualStyleBackColor = True
        '
        'btnExit
        '
        Me.btnExit.Location = New System.Drawing.Point(77, 209)
        Me.btnExit.Name = "btnExit"
        Me.btnExit.Size = New System.Drawing.Size(152, 23)
        Me.btnExit.TabIndex = 3
        Me.btnExit.Text = "E&xit"
        Me.btnExit.UseVisualStyleBackColor = True
        '
        'btnDecodeFile
        '
        Me.btnDecodeFile.Location = New System.Drawing.Point(77, 93)
        Me.btnDecodeFile.Name = "btnDecodeFile"
        Me.btnDecodeFile.Size = New System.Drawing.Size(152, 23)
        Me.btnDecodeFile.TabIndex = 2
        Me.btnDecodeFile.Text = "De&code File"
        Me.btnDecodeFile.UseVisualStyleBackColor = True
        '
        'btnEncodeFile
        '
        Me.btnEncodeFile.Location = New System.Drawing.Point(77, 122)
        Me.btnEncodeFile.Name = "btnEncodeFile"
        Me.btnEncodeFile.Size = New System.Drawing.Size(152, 23)
        Me.btnEncodeFile.TabIndex = 5
        Me.btnEncodeFile.Text = "&Encode File"
        Me.btnEncodeFile.UseVisualStyleBackColor = True
        '
        'btnForceGlobalFileReset
        '
        Me.btnForceGlobalFileReset.Location = New System.Drawing.Point(77, 151)
        Me.btnForceGlobalFileReset.Name = "btnForceGlobalFileReset"
        Me.btnForceGlobalFileReset.Size = New System.Drawing.Size(152, 23)
        Me.btnForceGlobalFileReset.TabIndex = 6
        Me.btnForceGlobalFileReset.Text = "&Force Global File Reset"
        Me.btnForceGlobalFileReset.UseVisualStyleBackColor = True
        '
        'frmMain
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.ClientSize = New System.Drawing.Size(306, 244)
        Me.Controls.Add(Me.btnForceGlobalFileReset)
        Me.Controls.Add(Me.btnEncodeFile)
        Me.Controls.Add(Me.btnDecodeFile)
        Me.Controls.Add(Me.btnExit)
        Me.Controls.Add(Me.btnGlobalFile)
        Me.Controls.Add(Me.btnDataFile)
        Me.Controls.Add(Me.lblInstruction)
        Me.Icon = CType(resources.GetObject("$this.Icon"), System.Drawing.Icon)
        Me.MaximizeBox = False
        Me.MinimizeBox = False
        Me.Name = "frmMain"
        Me.Text = "Tip Tracker Maintenance Utility"
        Me.ResumeLayout(False)
        Me.PerformLayout()

    End Sub
    Friend WithEvents lblInstruction As System.Windows.Forms.Label
    Friend WithEvents btnDataFile As System.Windows.Forms.Button
    Friend WithEvents btnGlobalFile As System.Windows.Forms.Button
    Friend WithEvents btnExit As System.Windows.Forms.Button
    Friend WithEvents btnDecodeFile As System.Windows.Forms.Button
    Friend WithEvents btnEncodeFile As System.Windows.Forms.Button
    Friend WithEvents btnForceGlobalFileReset As System.Windows.Forms.Button
End Class
