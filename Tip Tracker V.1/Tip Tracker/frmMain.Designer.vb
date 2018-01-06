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
        Me.components = New System.ComponentModel.Container
        Dim resources As System.ComponentModel.ComponentResourceManager = New System.ComponentModel.ComponentResourceManager(GetType(frmMain))
        Me.strMainMenuStrip = New System.Windows.Forms.MenuStrip
        Me.mnuFile = New System.Windows.Forms.ToolStripMenuItem
        Me.mnuNew = New System.Windows.Forms.ToolStripMenuItem
        Me.mnuOpen = New System.Windows.Forms.ToolStripMenuItem
        Me.ToolStripSeparator1 = New System.Windows.Forms.ToolStripSeparator
        Me.mnuClose = New System.Windows.Forms.ToolStripMenuItem
        Me.ToolStripSeparator2 = New System.Windows.Forms.ToolStripSeparator
        Me.mnuSave = New System.Windows.Forms.ToolStripMenuItem
        Me.mnuSaveAs = New System.Windows.Forms.ToolStripMenuItem
        Me.ToolStripSeparator3 = New System.Windows.Forms.ToolStripSeparator
        Me.mnuExit = New System.Windows.Forms.ToolStripMenuItem
        Me.mnuTools = New System.Windows.Forms.ToolStripMenuItem
        Me.mnuManageTemplateServers = New System.Windows.Forms.ToolStripMenuItem
        Me.ToolStripSeparator6 = New System.Windows.Forms.ToolStripSeparator
        Me.mnuPrintServerList = New System.Windows.Forms.ToolStripMenuItem
        Me.mnuImportServerList = New System.Windows.Forms.ToolStripMenuItem
        Me.mnuExportServerList = New System.Windows.Forms.ToolStripMenuItem
        Me.ToolStripSeparator5 = New System.Windows.Forms.ToolStripSeparator
        Me.mnuManageUsers = New System.Windows.Forms.ToolStripMenuItem
        Me.ToolStripSeparator4 = New System.Windows.Forms.ToolStripSeparator
        Me.mnuSettings = New System.Windows.Forms.ToolStripMenuItem
        Me.mnuWindow = New System.Windows.Forms.ToolStripMenuItem
        Me.mnuCascade = New System.Windows.Forms.ToolStripMenuItem
        Me.mnuMinimizeAll = New System.Windows.Forms.ToolStripMenuItem
        Me.mnuMaximizeAll = New System.Windows.Forms.ToolStripMenuItem
        Me.ToolStripSeparator9 = New System.Windows.Forms.ToolStripSeparator
        Me.mnuHelp = New System.Windows.Forms.ToolStripMenuItem
        Me.mnuContents = New System.Windows.Forms.ToolStripMenuItem
        Me.mnuIndex = New System.Windows.Forms.ToolStripMenuItem
        Me.ToolStripSeparator7 = New System.Windows.Forms.ToolStripSeparator
        Me.mnuAbout = New System.Windows.Forms.ToolStripMenuItem
        Me.ServersBindingSource = New System.Windows.Forms.BindingSource(Me.components)
        Me.FileDataSet = New Tip_Tracker.FileDataSet
        Me.TipsBindingSource = New System.Windows.Forms.BindingSource(Me.components)
        Me.SpecialFunctionBindingSource = New System.Windows.Forms.BindingSource(Me.components)
        Me.GlobalDataSet = New Tip_Tracker.GlobalDataSet
        Me.dlgOpenFile = New System.Windows.Forms.OpenFileDialog
        Me.dlgSaveFile = New System.Windows.Forms.SaveFileDialog
        Me.docServerList = New System.Drawing.Printing.PrintDocument
        Me.dlgPrint = New System.Windows.Forms.PrintDialog
        Me.strMainMenuStrip.SuspendLayout()
        CType(Me.ServersBindingSource, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.FileDataSet, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.TipsBindingSource, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.SpecialFunctionBindingSource, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.GlobalDataSet, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.SuspendLayout()
        '
        'strMainMenuStrip
        '
        Me.strMainMenuStrip.Items.AddRange(New System.Windows.Forms.ToolStripItem() {Me.mnuFile, Me.mnuTools, Me.mnuWindow, Me.mnuHelp})
        Me.strMainMenuStrip.Location = New System.Drawing.Point(0, 0)
        Me.strMainMenuStrip.MdiWindowListItem = Me.mnuWindow
        Me.strMainMenuStrip.Name = "strMainMenuStrip"
        Me.strMainMenuStrip.Size = New System.Drawing.Size(632, 24)
        Me.strMainMenuStrip.TabIndex = 1
        Me.strMainMenuStrip.Text = "Main Menu"
        '
        'mnuFile
        '
        Me.mnuFile.DropDownItems.AddRange(New System.Windows.Forms.ToolStripItem() {Me.mnuNew, Me.mnuOpen, Me.ToolStripSeparator1, Me.mnuClose, Me.ToolStripSeparator2, Me.mnuSave, Me.mnuSaveAs, Me.ToolStripSeparator3, Me.mnuExit})
        Me.mnuFile.Name = "mnuFile"
        Me.mnuFile.Size = New System.Drawing.Size(35, 20)
        Me.mnuFile.Text = "&File"
        '
        'mnuNew
        '
        Me.mnuNew.Name = "mnuNew"
        Me.mnuNew.Size = New System.Drawing.Size(126, 22)
        Me.mnuNew.Text = "New..."
        '
        'mnuOpen
        '
        Me.mnuOpen.Name = "mnuOpen"
        Me.mnuOpen.Size = New System.Drawing.Size(126, 22)
        Me.mnuOpen.Text = "Open..."
        '
        'ToolStripSeparator1
        '
        Me.ToolStripSeparator1.Name = "ToolStripSeparator1"
        Me.ToolStripSeparator1.Size = New System.Drawing.Size(123, 6)
        '
        'mnuClose
        '
        Me.mnuClose.Name = "mnuClose"
        Me.mnuClose.Size = New System.Drawing.Size(126, 22)
        Me.mnuClose.Text = "Close"
        '
        'ToolStripSeparator2
        '
        Me.ToolStripSeparator2.Name = "ToolStripSeparator2"
        Me.ToolStripSeparator2.Size = New System.Drawing.Size(123, 6)
        '
        'mnuSave
        '
        Me.mnuSave.Name = "mnuSave"
        Me.mnuSave.Size = New System.Drawing.Size(126, 22)
        Me.mnuSave.Text = "Save"
        '
        'mnuSaveAs
        '
        Me.mnuSaveAs.Name = "mnuSaveAs"
        Me.mnuSaveAs.Size = New System.Drawing.Size(126, 22)
        Me.mnuSaveAs.Text = "Save As..."
        '
        'ToolStripSeparator3
        '
        Me.ToolStripSeparator3.Name = "ToolStripSeparator3"
        Me.ToolStripSeparator3.Size = New System.Drawing.Size(123, 6)
        '
        'mnuExit
        '
        Me.mnuExit.Name = "mnuExit"
        Me.mnuExit.Size = New System.Drawing.Size(126, 22)
        Me.mnuExit.Text = "Exit"
        '
        'mnuTools
        '
        Me.mnuTools.DropDownItems.AddRange(New System.Windows.Forms.ToolStripItem() {Me.mnuManageTemplateServers, Me.ToolStripSeparator6, Me.mnuPrintServerList, Me.mnuImportServerList, Me.mnuExportServerList, Me.ToolStripSeparator5, Me.mnuManageUsers, Me.ToolStripSeparator4, Me.mnuSettings})
        Me.mnuTools.Name = "mnuTools"
        Me.mnuTools.Size = New System.Drawing.Size(45, 20)
        Me.mnuTools.Text = "&Tools"
        '
        'mnuManageTemplateServers
        '
        Me.mnuManageTemplateServers.Name = "mnuManageTemplateServers"
        Me.mnuManageTemplateServers.Size = New System.Drawing.Size(217, 22)
        Me.mnuManageTemplateServers.Text = "Manage Template Servers..."
        '
        'ToolStripSeparator6
        '
        Me.ToolStripSeparator6.Name = "ToolStripSeparator6"
        Me.ToolStripSeparator6.Size = New System.Drawing.Size(214, 6)
        '
        'mnuPrintServerList
        '
        Me.mnuPrintServerList.Name = "mnuPrintServerList"
        Me.mnuPrintServerList.Size = New System.Drawing.Size(217, 22)
        Me.mnuPrintServerList.Text = "Print Server List"
        '
        'mnuImportServerList
        '
        Me.mnuImportServerList.Name = "mnuImportServerList"
        Me.mnuImportServerList.ShortcutKeys = CType(((System.Windows.Forms.Keys.Control Or System.Windows.Forms.Keys.Shift) _
                    Or System.Windows.Forms.Keys.I), System.Windows.Forms.Keys)
        Me.mnuImportServerList.Size = New System.Drawing.Size(217, 22)
        Me.mnuImportServerList.Text = "Import Server List"
        Me.mnuImportServerList.Visible = False
        '
        'mnuExportServerList
        '
        Me.mnuExportServerList.Name = "mnuExportServerList"
        Me.mnuExportServerList.Size = New System.Drawing.Size(217, 22)
        Me.mnuExportServerList.Text = "Export Server List"
        '
        'ToolStripSeparator5
        '
        Me.ToolStripSeparator5.Name = "ToolStripSeparator5"
        Me.ToolStripSeparator5.Size = New System.Drawing.Size(214, 6)
        '
        'mnuManageUsers
        '
        Me.mnuManageUsers.Name = "mnuManageUsers"
        Me.mnuManageUsers.Size = New System.Drawing.Size(217, 22)
        Me.mnuManageUsers.Text = "Manage Users..."
        '
        'ToolStripSeparator4
        '
        Me.ToolStripSeparator4.Name = "ToolStripSeparator4"
        Me.ToolStripSeparator4.Size = New System.Drawing.Size(214, 6)
        '
        'mnuSettings
        '
        Me.mnuSettings.Name = "mnuSettings"
        Me.mnuSettings.Size = New System.Drawing.Size(217, 22)
        Me.mnuSettings.Text = "Settings..."
        '
        'mnuWindow
        '
        Me.mnuWindow.DropDownItems.AddRange(New System.Windows.Forms.ToolStripItem() {Me.mnuCascade, Me.mnuMinimizeAll, Me.mnuMaximizeAll, Me.ToolStripSeparator9})
        Me.mnuWindow.Name = "mnuWindow"
        Me.mnuWindow.Size = New System.Drawing.Size(58, 20)
        Me.mnuWindow.Text = "&Window"
        '
        'mnuCascade
        '
        Me.mnuCascade.Name = "mnuCascade"
        Me.mnuCascade.Size = New System.Drawing.Size(134, 22)
        Me.mnuCascade.Text = "Cascade"
        '
        'mnuMinimizeAll
        '
        Me.mnuMinimizeAll.Name = "mnuMinimizeAll"
        Me.mnuMinimizeAll.Size = New System.Drawing.Size(134, 22)
        Me.mnuMinimizeAll.Text = "Minimize All"
        '
        'mnuMaximizeAll
        '
        Me.mnuMaximizeAll.Name = "mnuMaximizeAll"
        Me.mnuMaximizeAll.Size = New System.Drawing.Size(134, 22)
        Me.mnuMaximizeAll.Text = "Maximize All"
        '
        'ToolStripSeparator9
        '
        Me.ToolStripSeparator9.Name = "ToolStripSeparator9"
        Me.ToolStripSeparator9.Size = New System.Drawing.Size(131, 6)
        '
        'mnuHelp
        '
        Me.mnuHelp.DropDownItems.AddRange(New System.Windows.Forms.ToolStripItem() {Me.mnuContents, Me.mnuIndex, Me.ToolStripSeparator7, Me.mnuAbout})
        Me.mnuHelp.Name = "mnuHelp"
        Me.mnuHelp.Size = New System.Drawing.Size(41, 20)
        Me.mnuHelp.Text = "&Help"
        '
        'mnuContents
        '
        Me.mnuContents.Name = "mnuContents"
        Me.mnuContents.Size = New System.Drawing.Size(163, 22)
        Me.mnuContents.Text = "Contents"
        '
        'mnuIndex
        '
        Me.mnuIndex.Name = "mnuIndex"
        Me.mnuIndex.Size = New System.Drawing.Size(163, 22)
        Me.mnuIndex.Text = "Index"
        '
        'ToolStripSeparator7
        '
        Me.ToolStripSeparator7.Name = "ToolStripSeparator7"
        Me.ToolStripSeparator7.Size = New System.Drawing.Size(160, 6)
        '
        'mnuAbout
        '
        Me.mnuAbout.Name = "mnuAbout"
        Me.mnuAbout.Size = New System.Drawing.Size(163, 22)
        Me.mnuAbout.Text = "About Tip Tracker"
        '
        'ServersBindingSource
        '
        Me.ServersBindingSource.DataMember = "Servers"
        Me.ServersBindingSource.DataSource = Me.FileDataSet
        '
        'FileDataSet
        '
        Me.FileDataSet.DataSetName = "FileDataSet"
        Me.FileDataSet.SchemaSerializationMode = System.Data.SchemaSerializationMode.IncludeSchema
        '
        'TipsBindingSource
        '
        Me.TipsBindingSource.DataMember = "Tips"
        Me.TipsBindingSource.DataSource = Me.FileDataSet
        '
        'SpecialFunctionBindingSource
        '
        Me.SpecialFunctionBindingSource.DataMember = "SpecialFunctions"
        Me.SpecialFunctionBindingSource.DataSource = Me.FileDataSet
        '
        'GlobalDataSet
        '
        Me.GlobalDataSet.DataSetName = "GlobalDataSet"
        Me.GlobalDataSet.SchemaSerializationMode = System.Data.SchemaSerializationMode.IncludeSchema
        '
        'dlgOpenFile
        '
        Me.dlgOpenFile.DefaultExt = "*.ttd"
        Me.dlgOpenFile.Filter = "Tip Tracker Data Files (*.ttd)|*.ttd"
        Me.dlgOpenFile.Title = "Open Data File"
        '
        'dlgSaveFile
        '
        Me.dlgSaveFile.DefaultExt = "*.ttd"
        Me.dlgSaveFile.Filter = "Tip Tracker Data Files (*.ttd)|*.ttd"
        Me.dlgSaveFile.Title = "Save Data File"
        '
        'docServerList
        '
        Me.docServerList.DocumentName = "Server List"
        '
        'dlgPrint
        '
        Me.dlgPrint.AllowPrintToFile = False
        Me.dlgPrint.Document = Me.docServerList
        '
        'frmMain
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.BackgroundImage = CType(resources.GetObject("$this.BackgroundImage"), System.Drawing.Image)
        Me.ClientSize = New System.Drawing.Size(632, 453)
        Me.Controls.Add(Me.strMainMenuStrip)
        Me.Icon = CType(resources.GetObject("$this.Icon"), System.Drawing.Icon)
        Me.IsMdiContainer = True
        Me.MainMenuStrip = Me.strMainMenuStrip
        Me.MinimumSize = New System.Drawing.Size(640, 480)
        Me.Name = "frmMain"
        Me.Text = "Tip Tracker"
        Me.WindowState = System.Windows.Forms.FormWindowState.Maximized
        Me.strMainMenuStrip.ResumeLayout(False)
        Me.strMainMenuStrip.PerformLayout()
        CType(Me.ServersBindingSource, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.FileDataSet, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.TipsBindingSource, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.SpecialFunctionBindingSource, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.GlobalDataSet, System.ComponentModel.ISupportInitialize).EndInit()
        Me.ResumeLayout(False)
        Me.PerformLayout()

    End Sub
    Friend WithEvents strMainMenuStrip As System.Windows.Forms.MenuStrip
    Friend WithEvents mnuFile As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents mnuHelp As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents mnuNew As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents mnuOpen As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents ToolStripSeparator1 As System.Windows.Forms.ToolStripSeparator
    Friend WithEvents mnuClose As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents ToolStripSeparator2 As System.Windows.Forms.ToolStripSeparator
    Friend WithEvents mnuSave As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents mnuSaveAs As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents ToolStripSeparator3 As System.Windows.Forms.ToolStripSeparator
    Friend WithEvents mnuExit As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents mnuTools As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents mnuManageTemplateServers As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents mnuManageUsers As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents ToolStripSeparator4 As System.Windows.Forms.ToolStripSeparator
    Friend WithEvents ServersBindingSource As System.Windows.Forms.BindingSource
    Friend WithEvents TipsBindingSource As System.Windows.Forms.BindingSource
    Friend WithEvents FileDataSet As Tip_Tracker.FileDataSet
    Friend WithEvents FirstNameDataGridViewTextBoxColumn1 As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents LastNameDataGridViewTextBoxColumn1 As System.Windows.Forms.DataGridViewTextBoxColumn
    Friend WithEvents SpecialFunctionBindingSource As System.Windows.Forms.BindingSource
    Friend WithEvents mnuContents As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents mnuIndex As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents ToolStripSeparator7 As System.Windows.Forms.ToolStripSeparator
    Friend WithEvents mnuAbout As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents GlobalDataSet As Tip_Tracker.GlobalDataSet
    Friend WithEvents mnuSettings As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents dlgOpenFile As System.Windows.Forms.OpenFileDialog
    Friend WithEvents dlgSaveFile As System.Windows.Forms.SaveFileDialog
    Friend WithEvents mnuWindow As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents mnuMinimizeAll As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents mnuMaximizeAll As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents ToolStripSeparator9 As System.Windows.Forms.ToolStripSeparator
    Friend WithEvents mnuCascade As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents mnuExportServerList As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents ToolStripSeparator5 As System.Windows.Forms.ToolStripSeparator
    Friend WithEvents mnuPrintServerList As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents docServerList As System.Drawing.Printing.PrintDocument
    Friend WithEvents dlgPrint As System.Windows.Forms.PrintDialog
    Friend WithEvents ToolStripSeparator6 As System.Windows.Forms.ToolStripSeparator
    Friend WithEvents mnuImportServerList As System.Windows.Forms.ToolStripMenuItem

End Class
