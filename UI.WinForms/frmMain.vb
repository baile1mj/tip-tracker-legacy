Imports System.Drawing.Printing
Imports System.IO
Imports System.Reflection
Imports TipTracker.Common.Data
Imports TipTracker.Common.Data.GlobalSettings
Imports TipTracker.Common.Data.PayPeriod
Imports TipTracker.Core

Public Class frmMain
    Private _globalSettingsFile As GlobalSettingsFile
    Private _globalSettings As GlobalSettings

    ''' <summary>
    ''' Gets a value indicating whether a server is in the template.
    ''' </summary>
    ''' <param name="serverNumber">The number identifying the server to check.</param>
    ''' <returns>True if the server is in the template; otherwise, false.</returns>
    Public Function IsServerInTemplate(ByVal serverNumber As String) As Boolean
        Return Not IsNothing(_globalSettings.GlobalDataSet.Servers.FindByServerNumber(serverNumber))
    End Function

    ''' <summary>
    ''' Adds a new server to the template.
    ''' </summary>
    ''' <param name="serverNumber">The new server's number.</param>
    ''' <param name="firstName">The new server's first name.</param>
    ''' <param name="lastName">The new server's last name.</param>
    ''' <param name="suppressChit">True to suppress chit printing for the new server; otherwise false.</param>
    Public Sub AddServerToTemplate(ByVal serverNumber As String, ByVal firstName As String, ByVal lastName As String, ByVal suppressChit As Boolean)
        _globalSettings.GlobalDataSet.Servers.AddServersRow(serverNumber, firstName, lastName, suppressChit)
        _globalSettings.GlobalDataSet.AcceptChanges()
        _globalSettingsFile.WriteGlobalSettings(_globalSettings)
    End Sub

    ''' <summary>
    ''' Gets a copy of the template servers.
    ''' </summary>
    ''' <returns>A <see cref="DataTable"/> containing the template servers.</returns>
    Public Function GetTemplateServers() As DataTable
        Return _globalSettings.GlobalDataSet.Servers.Copy()
    End Function

    Private Sub frmMain_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
        'Disable the menu commands that can only be used if a file is open.
        EnableMenuCommands(False)

        Dim globalFilePath As String = MachineSettings.GetGlobalFilePath()

        'When the global settings file path is missing, the user must either specify it or exit the application.
        If String.IsNullOrEmpty(globalFilePath) Then
            Using pathSelector As New frmConfigurator(globalFilePath)
                If pathSelector.ShowDialog() <> DialogResult.OK Then
                    MessageBox.Show("The global settings file path was not set.  Tip Tracker cannot run without a global " &
                        "settings file.  The application will exit.", "File Not Found", MessageBoxButtons.OK, MessageBoxIcon.Error)
                    Application.Exit()
                    End
                End If

                globalFilePath = pathSelector.GlobalSettingsFilePath
                MachineSettings.SetGlobalFilePath(globalFilePath)
            End Using
        End If

        _globalSettingsFile = New GlobalSettingsFile(globalFilePath)

        'Ensure that there is a global settings file to load so we can load it.
        If Not _globalSettingsFile.Exists() Then
            Try
                _globalSettingsFile.CreateNew()
            Catch ex As Exception
                'There isn't much we can do if the file fails to create.
                MessageBox.Show("Failed to initialize the global settings file.  Verify that the file path is correct and that " &
                    "you have permission to write to the directory.  If the problem persists, contact your organization's " &
                    "IT support resource.", "Initialization Failed", MessageBoxButtons.OK, MessageBoxIcon.Error)
                Application.Exit()
                End
            End Try
        End If

        'We might fail to read the file if there's some issue with the machine or its permissions.
        Try
            _globalSettings = _globalSettingsFile.ReadGlobalSettings()
        Catch ex As Exception
            'There isn't much we can do if the can't be read.
            MessageBox.Show("Failed to load the global settings file.  Verify that the file path is correct and that " &
                "you have permission to read the file.  If the problem persists, contact your organization's " &
                "IT support resource.", "Initialization Failed", MessageBoxButtons.OK, MessageBoxIcon.Error)
            Application.Exit()
            Return
        End Try

        'If there's not default directory, require the user to select one.
        While String.IsNullOrEmpty(_globalSettings.DefaultDataDirectory)
            MessageBox.Show("You must select a default data directory.", "Select Directory", MessageBoxButtons.OK)
            mnuSettings.PerformClick()
        End While

        'If a file was double-clicked, try to open the file.
        For Each param As String In My.Application.CommandLineArgs
            Try
                ' pass the file path if it exists
                OpenFile(param)
            Catch
                'do nothing, just open the application with no file
            End Try
        Next param
    End Sub

    Private Sub EnableMenuCommands(ByVal Enabled As Boolean)
        mnuClose.Enabled = Enabled
        mnuSave.Enabled = Enabled
        mnuSaveAs.Enabled = Enabled
        mnuWindow.Visible = Enabled
    End Sub

    Private Sub mnuExit_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles mnuExit.Click
        Close()
    End Sub

    Private Sub mnuManageTemplateServers_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles mnuManageTemplateServers.Click
        Dim serversTable As DataTable = _globalSettings.GlobalDataSet.Servers.Copy()

        Using serverManager As New frmManageServers(serversTable)
            'The dialog only has a close button, so we'll assume there are changes.
            serverManager.ShowDialog()

            'If the file has been updated, the user will need to recreate their changes.  Since the global settings will
            'be removed in the next version, there's no point in trying to handle concurrency more gracefully.
            If _globalSettingsFile.IsChanged(_globalSettings) Then
                _globalSettings = _globalSettingsFile.ReadGlobalSettings()
                MessageBox.Show("Another user changed the global settings file and your changes have been lost.  " &
                    "Please redo your changes.", "Concurrent Changes Detected", MessageBoxButtons.OK, MessageBoxIcon.Error)
            Else
                _globalSettings.GlobalDataSet.Servers.Rows.Clear()

                For Each server As Server In serverManager.Servers
                    Dim newRow As GlobalDataSet.ServersRow = _globalSettings.GlobalDataSet.Servers.NewServersRow
                    newRow.ServerNumber = server.PosId
                    newRow.FirstName = server.FirstName
                    newRow.LastName = server.LastName
                    newRow.SuppressChit = server.SuppressChit

                    _globalSettings.GlobalDataSet.Servers.AddServersRow(newRow)
                Next

                _globalSettings.GlobalDataSet.AcceptChanges()
            End If
        End Using

        'There's no point in saving the file if nothing changed.
        If Not _globalSettingsFile.IsChanged(_globalSettings) Then
            Exit Sub
        End If

        'If we can't save the settings, the application can still run, but the user should be notified.
        Try
            _globalSettingsFile.WriteGlobalSettings(_globalSettings)
        Catch ex As Exception
            MessageBox.Show("Failed to save the updated server list.  It is recommended that you save all open data files " &
                $"and restart the application.  Verify that you have permission to write to {_globalSettingsFile.FilePath}. " &
                "If this problem persists, contact your organization's IT support resource.", "Settings Update Failed",
                MessageBoxButtons.OK, MessageBoxIcon.Error)
        End Try
    End Sub

    Private Sub mnuSettings_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles mnuSettings.Click
        Dim currentDefaultDirectory As String = _globalSettings.DefaultDataDirectory

        'If the file has been updated, reload it.
        If _globalSettingsFile.IsChanged(_globalSettings) Then
            _globalSettings = _globalSettingsFile.ReadGlobalSettings()
        End If

        'Prompt the user to select a new default data directory.
        Using frmSettings As New frmSettings(currentDefaultDirectory)
            If frmSettings.ShowDialog <> DialogResult.OK Then
                Exit Sub
            End If

            _globalSettings.DefaultDataDirectory = frmSettings.DefaultDirectory
        End Using

        'If we can't save the settings, the application can still run, but the user should be notified.
        Try
            _globalSettingsFile.WriteGlobalSettings(_globalSettings)
        Catch ex As Exception
            MessageBox.Show("Failed to save the updated setting.  It is recommended that you save all open data files " &
                $"and restart the application.  Verify that you have permission to write to {_globalSettingsFile.FilePath}. " &
                "If this problem persists, contact your organization's IT support resource.", "Settings Update Failed",
                MessageBoxButtons.OK, MessageBoxIcon.Error)
        End Try
    End Sub

    Private Sub mnuMinimizeAll_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles mnuMinimizeAll.Click
        For Each form As Windows.Forms.Form In MdiChildren
            form.WindowState = FormWindowState.Minimized
        Next
    End Sub

    Private Sub mnuMaximizeAll_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles mnuMaximizeAll.Click
        For Each form As Windows.Forms.Form In MdiChildren
            form.WindowState = FormWindowState.Maximized
        Next
    End Sub

    Private Sub mnuCascade_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles mnuCascade.Click
        For Each form As Windows.Forms.Form In MdiChildren
            form.WindowState = FormWindowState.Normal
        Next

        LayoutMdi(MdiLayout.Cascade)
    End Sub

    Private Sub mnuClose_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles mnuClose.Click
        ActiveMdiChild.Close()
    End Sub

    Private Sub mnuSave_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles mnuSave.Click
        Dim active As frmEnterTips = CType(ActiveMdiChild, frmEnterTips)

        Try
            active.File.Write(active.Data)
        Catch ex As Exception
            MessageBox.Show("Failed to save the changes to the file.  Please verify that you have " &
                "permission to write to the file and that the destination folder is connected if it is " &
                "removable.  If the problem persists, contact your organization's IT support resource.",
                "Error Saving File", MessageBoxButtons.OK, MessageBoxIcon.Error)
        End Try
    End Sub

    Private Sub mnuSaveAs_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles mnuSaveAs.Click
        Dim activeChild As frmEnterTips = CType(ActiveMdiChild, frmEnterTips)
        Dim fileNameSuggestion As String = $"{Path.GetFileNameWithoutExtension(activeChild.File.FilePath)} - Copy"

        Using saveFileDialog As New SaveFileDialog() With {
                .AddExtension = True,
                .CheckPathExists = True,
                .DefaultExt = "*.ttd",
                .FileName = fileNameSuggestion,
                .Filter = "Tip Tracker Data Files (*.ttd)|*.ttd",
                .InitialDirectory = _globalSettings.DefaultDataDirectory,
                .SupportMultiDottedExtensions = True,
                .Title = "Save Data File"
            }

            If saveFileDialog.ShowDialog <> DialogResult.OK Then Exit Sub

            Dim strNewFileName As String = saveFileDialog.FileName
            Dim newFile As PayPeriodFile = New PayPeriodFile(strNewFileName)
            Dim copy As PayPeriodData = PayPeriodData.Clone(activeChild.Data)

            Try
                newFile.Open()
                newFile.Write(copy)
            Catch ex As Exception
                MessageBox.Show("Failed to save the new file.  Please verify that you have " &
                    "permission to write to the folder and that the destination folder is connected if it is " &
                    "removable.  If the problem persists, contact your organization's IT support resource.",
                    "Error Saving File", MessageBoxButtons.OK, MessageBoxIcon.Error)
                Exit Sub
            End Try

            'TODO: consolidate this since it's used in multiple methods.
            Dim copyForm As New frmEnterTips(newFile, copy)
            AddHandler copyForm.FormClosing, AddressOf ChildFormClosing

            copyForm.Show()
        End Using
    End Sub

    Private Sub mnuNew_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles mnuNew.Click
        Dim dtePeriodStart As Date
        Dim dtePeriodEnd As Date

        Using payPeriodDetails As New frmCreateFile()
            If Not payPeriodDetails.ShowDialog() = DialogResult.OK Then Exit Sub

            dtePeriodStart = payPeriodDetails.PeriodStart
            dtePeriodEnd = payPeriodDetails.PeriodEnd
        End Using

        Dim filePath As String

        Using saveDialog As New SaveFileDialog() With {
            .AddExtension = True,
            .CheckPathExists = True,
            .DefaultExt = "*.ttd",
            .FileName = Format(dtePeriodStart, "yyyy_MM_dd"),
            .Filter = "Tip Tracker Data Files (*.ttd)|*.ttd",
            .InitialDirectory = _globalSettings.DefaultDataDirectory,
            .SupportMultiDottedExtensions = True,
            .Title = "Create New Data File"
        }
            If Not saveDialog.ShowDialog() = DialogResult.OK Then Exit Sub

            filePath = saveDialog.FileName
        End Using

        Dim newFile As New PayPeriodFile(filePath)
        Dim newFileData As PayPeriodData = PayPeriodData.Create(dtePeriodStart, dtePeriodEnd, GetTemplateServers())

        Try
            newFile.Open()
            newFile.Write(newFileData)
        Catch ex As Exception
            MessageBox.Show("Failed to create the new file.  Verify that you have permission to write to the " &
                "specified folder.  If this problem persists, please contact your organization's IT support resource",
                "File Creation Failed", MessageBoxButtons.OK, MessageBoxIcon.Error)
        End Try

        'TODO: consolidate this since it's used in multiple methods.
        Dim newFileForm As New frmEnterTips(newFile, newFileData)
        AddHandler newFileForm.FormClosing, AddressOf ChildFormClosing

        newFileForm.Show()
        EnableMenuCommands(True)
    End Sub

    Private Sub mnuOpen_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles mnuOpen.Click
        Dim dlgOpenFile As New OpenFileDialog

        With dlgOpenFile
            .AddExtension = True
            .CheckFileExists = True
            .CheckPathExists = True
            .DefaultExt = "*.ttd"
            .Filter = "Tip Tracker Data Files (*.ttd)|*.ttd"
            If Not Directory.Exists(_globalSettings.DefaultDataDirectory) Then
                .InitialDirectory = My.Computer.FileSystem.SpecialDirectories.MyDocuments
            Else
                .InitialDirectory = _globalSettings.DefaultDataDirectory
            End If
            .Multiselect = False
            .RestoreDirectory = True
            .SupportMultiDottedExtensions = True
            .Title = "Open Data File"
        End With

        If dlgOpenFile.ShowDialog <> Windows.Forms.DialogResult.OK Then
            dlgOpenFile.Dispose()
            Exit Sub
        End If

        OpenFile(dlgOpenFile.FileName)

        dlgOpenFile.Dispose()
    End Sub

    Private Sub OpenFile(ByVal Path As String)
        'Create a file instance.
        Dim payPeriodFile As New PayPeriodFile(Path)
        Dim fileData As PayPeriodData

        'Try opening the file.
        Try
            payPeriodFile.Open()
        Catch ex As Exception
            MessageBox.Show("Failed to open the requested file.  The file may have moved, may be in use by another " &
                "user, or you may not have permission to access this file.  If the problem persists, please contact " &
                "your organization's IT support resource.", "Error Opening File", MessageBoxButtons.OK,
                MessageBoxIcon.Error)
            Return
        End Try

        'Try reading the settings.
        Try
            fileData = payPeriodFile.ReadPayPeriodFile()
        Catch ex As Exception
            MessageBox.Show("Failed to read the requested file.  The file is corrupt or is not a valid Tip " &
                "Tracker data file.", "Invalid File Type", MessageBoxButtons.OK, MessageBoxIcon.Information)
            Return
        End Try

        'Notify on any warnings that the file generated.
        For Each message As String In fileData.GetWarnings()
            MessageBox.Show(message, "Data Validation Information", MessageBoxButtons.OK, MessageBoxIcon.Information)
        Next

        'Open the editing form.
        'TODO: consolidate this since it's used in multiple methods.
        Dim editor As New frmEnterTips(payPeriodFile, fileData)
        AddHandler editor.FormClosing, AddressOf ChildFormClosing
        editor.Show()

        'If this is the first file to be opened, enable the file-specific menu commands.
        If MdiChildren.Length = 1 Then EnableMenuCommands(True)
    End Sub

    Private Sub ChildFormClosing(ByVal sender As Object, ByVal e As FormClosingEventArgs)
        Dim form As frmEnterTips = CType(sender, frmEnterTips)

        'If the file has changes, see what the user wants to do with them.
        If form.Data.FileDataSet.HasChanges() Then
            Dim userResponse As DialogResult = MessageBox.Show($"Save changes to the file {form.Text}?", "Save Changes", MessageBoxButtons.YesNoCancel)

            If userResponse = DialogResult.Yes Then
                Try
                    form.File.Write(form.Data)
                    form.Data.FileDataSet.AcceptChanges()
                Catch ex As Exception
                    MessageBox.Show("Failed to save the changes to the file.  Please verify that you have " &
                        "permission to write to the file and that the destination folder is connected if it is " &
                        "removable.  If the problem persists, contact your organization's IT support resource.",
                        "Error Saving File", MessageBoxButtons.OK, MessageBoxIcon.Error)
                    e.Cancel = True
                    Exit Sub
                End Try
            ElseIf userResponse = DialogResult.Cancel Then
                e.Cancel = True
                Exit Sub
            End If
        End If

        form.File.Dispose()
        form.Dispose()

        'Disable any file-specific commands if there are no more files to which they apply.
        If MdiChildren.Length = 0 Then
            EnableMenuCommands(False)
        End If
    End Sub

    Private Sub mnuExportServerList_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles mnuExportServerList.Click
        Dim strFileName As String

        Using dlgSaveFile As New SaveFileDialog()
            With dlgSaveFile
                .InitialDirectory = My.Computer.FileSystem.SpecialDirectories.Desktop
                .RestoreDirectory = True
                .Title = "Export Server List"
                .Filter = "XML Files (*.xml)|*.xml"
                .DefaultExt = "*.xml"
                .FileName = "Server List"

                If .ShowDialog <> DialogResult.OK Then
                    Exit Sub
                End If

                strFileName = .FileName
            End With
        End Using

        Try
            'TODO: move the file writing functionality into a separate class.
            _globalSettings.GlobalDataSet.Servers.WriteXml(strFileName)
        Catch ex As Exception
            MessageBox.Show("An error occurred while trying to save the server list.", "Write Error", MessageBoxButtons.OK, MessageBoxIcon.Error)
        End Try
    End Sub

    Private Sub mnuPrintServerList_Click(ByVal sender As Object, ByVal e As EventArgs) Handles mnuPrintServerList.Click
        Dim serverList As New PrintDocument()

        With serverList
            .DocumentName = "Server List"
            .DefaultPageSettings.Margins = New Margins(50, 50, 50, 50)
        End With

        AddHandler serverList.PrintPage, AddressOf docServerList_PrintPage

        Using printDialog As New PrintDialog() With {
            .Document = serverList,
            .UseEXDialog = True}

            If printDialog.ShowDialog() = DialogResult.OK Then
                serverList.Print()
            End If
        End Using
    End Sub

    Private Sub docServerList_PrintPage(ByVal sender As Object, ByVal e As PrintPageEventArgs)
        Dim font As New System.Drawing.Font("Calibri", 10)

        'Initialize local static variables that contain the current line position and
        'the current page number.
        Static position As Single = 50

        'Initialize local constants that contain the normal line spacing and padded 
        'line spacing (double spacing).
        Const intLineSpacing As Integer = 14
        Const intCol1NumberPos As Integer = 50
        Const intCol1NamePos As Integer = 150
        Const intCol2NumberPos As Integer = 450
        Const intCol2NamePos As Integer = 550

        Dim intPrintAreaHeight, intPrintAreaWidth, marginLeft, marginTop As Integer

        With e.PageSettings
            ' Initialize local variables that contain the bounds of the printing 
            ' area rectangle.
            intPrintAreaHeight = .PaperSize.Height - .Margins.Top - .Margins.Bottom
            intPrintAreaWidth = .PaperSize.Width - .Margins.Left - .Margins.Right

            ' Initialize local variables to hold margin values that will serve
            ' as the X and Y coordinates for the upper left corner of the printing 
            ' area rectangle.
            marginLeft = .Margins.Left ' X coordinate
            marginTop = .Margins.Top ' Y coordinate
        End With

        Dim dv As New DataView
        dv.Table = _globalSettings.GlobalDataSet.Servers
        dv.Sort = "ServerNumber"

        Static i As Integer = 0

        Do Until i = dv.Count
            If position >= marginTop + intPrintAreaHeight Then
                e.HasMorePages = True
                position = 50
                Exit Sub
            End If

            Dim strServerNumber As String = dv(i)("ServerNumber").ToString
            Dim strName As String = dv(i)("FirstName").ToString & " " & Microsoft.VisualBasic.Left(dv(i)("LastName").ToString, 1) & "."

            'Draw the first column to the page.
            e.Graphics.DrawString(strServerNumber, font, Brushes.Black, intCol1NumberPos, position)
            e.Graphics.DrawString(strName, font, Brushes.Black, intCol1NamePos, position)

            'Draw the second column to the page.
            e.Graphics.DrawString(strServerNumber, font, Brushes.Black, intCol2NumberPos, position)
            e.Graphics.DrawString(strName, font, Brushes.Black, intCol2NamePos, position)

            position += intLineSpacing
            i += 1
        Loop

        e.HasMorePages = False
        position = 50
        i = 0
        dv = Nothing
    End Sub

    Private Sub mnuAbout_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles mnuAbout.Click
        frmAbout.ShowDialog()
        frmAbout.Dispose()
    End Sub

    Private Sub mnuRunMaintenanceUtility_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles mnuRunMaintenanceUtility.Click
        Try
            Dim fileUtilityAssembly As Assembly = Assembly.GetAssembly(GetType(FileDebug.frmMain))
            Dim fileUtilityPath As String = fileUtilityAssembly.Location

            Shell(fileUtilityPath, AppWinStyle.NormalFocus, False, 1)
        Catch ex As FileNotFoundException
            MessageBox.Show("Could not find the maintenance utility external executable.  Please reinstall " &
                "the application.", "File Not Found", MessageBoxButtons.OK, MessageBoxIcon.Error)
        End Try
    End Sub

    Private Sub mnuContents_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles mnuContents.Click
        If Not File.Exists(My.Application.Info.DirectoryPath & "\TTHelp.chm") Then
            MessageBox.Show("The help file could not be located.  Please reinstall Tip Tracker.", "File Not Found", MessageBoxButtons.OK, MessageBoxIcon.Warning)
            Exit Sub
        End If

        Windows.Forms.Help.ShowHelp(Parent, My.Application.Info.DirectoryPath & "\TTHelp.chm")
    End Sub

    Private Sub mnuIndex_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles mnuIndex.Click
        If Not File.Exists(My.Application.Info.DirectoryPath & "\TTHelp.chm") Then
            MessageBox.Show("The help file could not be located.  Please reinstall Tip Tracker.", "File Not Found", MessageBoxButtons.OK, MessageBoxIcon.Warning)
            Exit Sub
        End If

        Windows.Forms.Help.ShowHelpIndex(Parent, My.Application.Info.DirectoryPath & "\TTHelp.chm")
    End Sub
End Class
