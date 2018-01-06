<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class frmConfigurator
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
        Me.lblInstructions = New System.Windows.Forms.Label
        Me.txtGlobalFilePath = New System.Windows.Forms.TextBox
        Me.lblGlobalFilePath = New System.Windows.Forms.Label
        Me.btnNew = New System.Windows.Forms.Button
        Me.btnExisting = New System.Windows.Forms.Button
        Me.btnClose = New System.Windows.Forms.Button
        Me.dlgOpenFile = New System.Windows.Forms.OpenFileDialog
        Me.dlgSaveFile = New System.Windows.Forms.SaveFileDialog
        Me.GlobalDataSet = New Tip_Tracker.GlobalDataSet
        CType(Me.GlobalDataSet, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.SuspendLayout()
        '
        'lblInstructions
        '
        Me.lblInstructions.AutoSize = True
        Me.lblInstructions.Location = New System.Drawing.Point(12, 9)
        Me.lblInstructions.Name = "lblInstructions"
        Me.lblInstructions.Size = New System.Drawing.Size(378, 13)
        Me.lblInstructions.TabIndex = 3
        Me.lblInstructions.Text = "NOTE: You must either create a new global settings file or open an existing file." & _
            ""
        '
        'txtGlobalFilePath
        '
        Me.txtGlobalFilePath.Anchor = CType(((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Left) _
                    Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.txtGlobalFilePath.Location = New System.Drawing.Point(12, 54)
        Me.txtGlobalFilePath.Name = "txtGlobalFilePath"
        Me.txtGlobalFilePath.ReadOnly = True
        Me.txtGlobalFilePath.Size = New System.Drawing.Size(405, 20)
        Me.txtGlobalFilePath.TabIndex = 5
        Me.txtGlobalFilePath.TabStop = False
        '
        'lblGlobalFilePath
        '
        Me.lblGlobalFilePath.AutoSize = True
        Me.lblGlobalFilePath.Location = New System.Drawing.Point(12, 38)
        Me.lblGlobalFilePath.Name = "lblGlobalFilePath"
        Me.lblGlobalFilePath.Size = New System.Drawing.Size(84, 13)
        Me.lblGlobalFilePath.TabIndex = 4
        Me.lblGlobalFilePath.Text = "Global File Path:"
        '
        'btnNew
        '
        Me.btnNew.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Left), System.Windows.Forms.AnchorStyles)
        Me.btnNew.Location = New System.Drawing.Point(12, 89)
        Me.btnNew.Name = "btnNew"
        Me.btnNew.Size = New System.Drawing.Size(75, 23)
        Me.btnNew.TabIndex = 0
        Me.btnNew.Text = "New..."
        Me.btnNew.UseVisualStyleBackColor = True
        '
        'btnExisting
        '
        Me.btnExisting.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Left), System.Windows.Forms.AnchorStyles)
        Me.btnExisting.Location = New System.Drawing.Point(93, 89)
        Me.btnExisting.Name = "btnExisting"
        Me.btnExisting.Size = New System.Drawing.Size(75, 23)
        Me.btnExisting.TabIndex = 1
        Me.btnExisting.Text = "Existing..."
        Me.btnExisting.UseVisualStyleBackColor = True
        '
        'btnClose
        '
        Me.btnClose.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.btnClose.Enabled = False
        Me.btnClose.Location = New System.Drawing.Point(342, 89)
        Me.btnClose.Name = "btnClose"
        Me.btnClose.Size = New System.Drawing.Size(75, 23)
        Me.btnClose.TabIndex = 2
        Me.btnClose.Text = "Close"
        Me.btnClose.UseVisualStyleBackColor = True
        '
        'dlgOpenFile
        '
        Me.dlgOpenFile.DefaultExt = "dat"
        Me.dlgOpenFile.FileName = "ttgsf.dat"
        Me.dlgOpenFile.Filter = "DAT Files (*.DAT)|*.dat"
        Me.dlgOpenFile.Title = "Open Global Settings File"
        '
        'dlgSaveFile
        '
        Me.dlgSaveFile.FileName = "ttgsf.dat"
        Me.dlgSaveFile.Filter = "DAT Files (*.dat)|*.dat"
        Me.dlgSaveFile.SupportMultiDottedExtensions = True
        Me.dlgSaveFile.Title = "Create Global Settings File"
        '
        'GlobalDataSet
        '
        Me.GlobalDataSet.DataSetName = "GlobalDataSet"
        Me.GlobalDataSet.SchemaSerializationMode = System.Data.SchemaSerializationMode.IncludeSchema
        '
        'frmConfigurator
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.ClientSize = New System.Drawing.Size(429, 124)
        Me.ControlBox = False
        Me.Controls.Add(Me.btnClose)
        Me.Controls.Add(Me.btnExisting)
        Me.Controls.Add(Me.btnNew)
        Me.Controls.Add(Me.lblGlobalFilePath)
        Me.Controls.Add(Me.txtGlobalFilePath)
        Me.Controls.Add(Me.lblInstructions)
        Me.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog
        Me.MaximizeBox = False
        Me.MinimizeBox = False
        Me.Name = "frmConfigurator"
        Me.Text = "Configurator"
        CType(Me.GlobalDataSet, System.ComponentModel.ISupportInitialize).EndInit()
        Me.ResumeLayout(False)
        Me.PerformLayout()

    End Sub
    Friend WithEvents lblInstructions As System.Windows.Forms.Label
    Friend WithEvents txtGlobalFilePath As System.Windows.Forms.TextBox
    Friend WithEvents lblGlobalFilePath As System.Windows.Forms.Label
    Friend WithEvents btnNew As System.Windows.Forms.Button
    Friend WithEvents btnExisting As System.Windows.Forms.Button
    Friend WithEvents btnClose As System.Windows.Forms.Button
    Friend WithEvents dlgOpenFile As System.Windows.Forms.OpenFileDialog
    Friend WithEvents dlgSaveFile As System.Windows.Forms.SaveFileDialog
    Friend WithEvents GlobalDataSet As Tip_Tracker.GlobalDataSet
End Class
