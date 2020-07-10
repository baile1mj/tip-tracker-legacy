Imports System.IO
Imports Tip_Tracker.Utilities

Public Class frmMain
    Private _globalSettingsFile As GlobalSettingsFile
    Private _globalSettings As GlobalSettings
    Private m_strCurrentFile As String = ""
    Private m_objGlobalStream As File

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
        _globalSettingsFile.Write(_globalSettings)
    End Sub

    ''' <summary>
    ''' Gets a copy of the template servers.
    ''' </summary>
    ''' <returns>A <see cref="DataTable"/> containing the template servers.</returns>
    Public Function GetTemplateServers() As DataTable
        Return _globalSettings.GlobalDataSet.Servers.Copy()
    End Function

    Private Property CurrentDataFile() As String
        Get
            Return m_strCurrentFile
        End Get
        Set(ByVal value As String)
            m_strCurrentFile = value
        End Set
    End Property

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
            _globalSettings = _globalSettingsFile.Read()
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
                _globalSettings = _globalSettingsFile.Read()
                MessageBox.Show("Another user changed the global settings file and your changes have been lost.  " &
                    "Please redo your changes.", "Concurrent Changes Detected", MessageBoxButtons.OK, MessageBoxIcon.Error)
            Else
                _globalSettings.GlobalDataSet.Servers.Rows.Clear()

                For Each row As DataRow In serverManager.ServersTable.Rows
                    _globalSettings.GlobalDataSet.Servers.ImportRow(row)
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
            _globalSettingsFile.Write(_globalSettings)
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
            _globalSettings = _globalSettingsFile.Read()
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
            _globalSettingsFile.Write(_globalSettings)
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

    Friend Function NumberOfFilesOpen() As Integer
        'Returns the number of data files open.  Since each data file calls a new instance of
        'frmEnterTips when it is opened, the number of mdi children is also the number of open
        'data files.
        Dim intFilesOpen As Integer = 0

        For Each form As Form In MdiChildren
            intFilesOpen += 1
        Next

        Return intFilesOpen
    End Function

    Friend Sub LastFileClosing()
        'To be called when the last data file closes.  If there are no data files open, the menu
        'commands that provide data file functionality need to be disabled.
        EnableMenuCommands(False)
    End Sub

    Private Sub mnuClose_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles mnuClose.Click
        ActiveMdiChild.Close()
    End Sub

    Private Sub mnuSave_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles mnuSave.Click
        For Each form As frmEnterTips In MdiChildren
            If form Is ActiveMdiChild Then
                form.SaveData()
            End If
        Next
    End Sub

    Private Sub mnuSaveAs_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles mnuSaveAs.Click
        dlgSaveFile.InitialDirectory = _globalSettings.DefaultDataDirectory
        dlgSaveFile.FileName = ""

        If dlgSaveFile.ShowDialog <> Windows.Forms.DialogResult.OK Then
            dlgSaveFile.Dispose()
            Exit Sub
        End If

        Dim strNewFileName As String = dlgSaveFile.FileName

        For Each form As frmEnterTips In MdiChildren
            If form Is ActiveMdiChild Then
                form.SaveDataAs(strNewFileName)
            End If
        Next

        dlgSaveFile.Dispose()
    End Sub

    Private Sub mnuNew_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles mnuNew.Click
        Dim strFileName As String

        'Show frmCreateFile to have the user set the pay period start and end dates.
        If frmCreateFile.ShowDialog <> Windows.Forms.DialogResult.OK Then
            frmCreateFile.Dispose()
            dlgSaveFile.Dispose()
            Exit Sub
        End If

        Dim dtePeriodStart As Date = frmCreateFile.PeriodStart
        Dim dtePeriodEnd As Date = frmCreateFile.PeriodEnd

        'Prepare dlgSaveFile to be shown.
        dlgSaveFile.InitialDirectory = _globalSettings.DefaultDataDirectory
        dlgSaveFile.Title = "Create New Data File"
        dlgSaveFile.FileName = Format(dtePeriodStart, "yyyy_MM_dd")

        frmCreateFile.Dispose()

        If dlgSaveFile.ShowDialog <> Windows.Forms.DialogResult.OK Then
            dlgSaveFile.Dispose()
            Exit Sub
        End If

        strFileName = dlgSaveFile.FileName

        Dim drNewRow As DataRow

        'Store the working date, period start, and period end in the settings table
        'of the file dataset.
        drNewRow = FileDataSet.Settings.NewRow
        drNewRow("Setting") = "PeriodStart"
        drNewRow("Value") = dtePeriodStart.ToString
        FileDataSet.Settings.Rows.Add(drNewRow)

        drNewRow = FileDataSet.Settings.NewRow
        drNewRow("Setting") = "PeriodEnd"
        drNewRow("Value") = dtePeriodEnd.ToString
        FileDataSet.Settings.Rows.Add(drNewRow)

        drNewRow = FileDataSet.Settings.NewRow
        drNewRow("Setting") = "WorkingDate"
        drNewRow("Value") = dtePeriodStart.ToString
        FileDataSet.Settings.Rows.Add(drNewRow)

        drNewRow = Nothing

        'Copy the template servers into the file dataset.
        FileDataSet.Servers.Merge(_globalSettings.GlobalDataSet.Servers)

        Try
            'Encode the dataset then write the file to the selected directory.
            Dim objFileEncoder As New clsFileEncoder
            Dim objDSStream As New MemoryStream

            FileDataSet.WriteXml(objDSStream)
            objDSStream.Flush()
            objDSStream.Seek(0, SeekOrigin.Begin)

            objFileEncoder.EncodeFile(strFileName, objDSStream)

            'Clean up the dataset, file encoding object and dataset stream.
            FileDataSet.Clear()
            FileDataSet.AcceptChanges()

            objFileEncoder.Dispose()
            objDSStream.Close()
            objDSStream.Dispose()
        Catch ex As IOException
            MessageBox.Show("The file name you entered already exists and is in use.  The file could not be created.", "Error Creating File", MessageBoxButtons.OK)
            FileDataSet.Clear()
            FileDataSet.AcceptChanges()
            Exit Sub
        Catch ex As Exception
            MessageBox.Show("Could not create the requested file.  Please contact support.", "Error Creating File", MessageBoxButtons.OK)
            FileDataSet.Clear()
            FileDataSet.AcceptChanges()
            Exit Sub
        End Try

        'Call a new instance of frmEnterTips and pass the filename into the form's
        'current file property, then show the form.
        Dim form As New frmEnterTips
        form.CurrentFile = strFileName

        form.Show()
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
        Try
            Dim objFileDecoder As New clsFileEncoder
            Dim objDSStream As New MemoryStream

            objDSStream = objFileDecoder.DecodeFile(Path)

            objDSStream.Flush()
            objDSStream.Seek(0, SeekOrigin.Begin)

            FileDataSet.ReadXml(objDSStream)

            objFileDecoder.Dispose()
            objDSStream.Close()
            objDSStream.Dispose()
        Catch ex As FormatException
            If MessageBox.Show("The requested file was saved in the legacy format.  The file will be converted.  Please be patient as the conversion may take several minutes.", "Legacy Format", MessageBoxButtons.OKCancel) = Windows.Forms.DialogResult.Cancel Then
                Exit Sub
            End If

            Try
                Dim objFileDecoder As New clsFileEncoder
                Dim objDSStream As New MemoryStream

                objDSStream = objFileDecoder.LegacyDecodeFile(Path)

                objDSStream.Flush()
                objDSStream.Seek(0, SeekOrigin.Begin)

                FileDataSet.ReadXml(objDSStream)

                objFileDecoder.Dispose()
                objDSStream.Close()
                objDSStream.Dispose()

                Dim objFileEncoder As New clsFileEncoder
                objDSStream = New MemoryStream

                FileDataSet.WriteXml(objDSStream)

                objFileEncoder.EncodeFile(Path, objDSStream)

                objFileEncoder.Dispose()
                objDSStream.Close()
                objDSStream.Dispose()
                FileDataSet.AcceptChanges()

            Catch subEx As Exception
                MessageBox.Show("Could not convert the requested file.  The file may be an invalid type or may be corrupted.", "Error Converting File", MessageBoxButtons.OK, MessageBoxIcon.Error)
                Exit Sub
            End Try
        Catch ex As IOException
            MessageBox.Show("The requested file could not be opened because it is already in use.", "Error Opening File", MessageBoxButtons.OK)
            FileDataSet.Clear()
            FileDataSet.AcceptChanges()
            Exit Sub
        Catch ex As Exception
            MessageBox.Show("Could not open file.  Contact Support", "Error", MessageBoxButtons.OK)
            FileDataSet.Clear()
            FileDataSet.AcceptChanges()
            Exit Sub
        End Try

        If ValidateFile() = False Then
            MessageBox.Show("The file you selected is not a valid Tip Tracker data file.", "Invalid File Type", MessageBoxButtons.OK)
            FileDataSet.Clear()
            FileDataSet.AcceptChanges()
            Exit Sub
        End If

        'Check for functions not in the pay period.
        Dim lstInvalidFunctions As New List(Of String)

        Dim dtePeriodStart As Date = CDate(FileDataSet.Settings.FindBySetting("PeriodStart")("Value"))
        Dim dtePeriodEnd As Date = CDate(FileDataSet.Settings.FindBySetting("PeriodEnd")("Value"))

        Dim dvFunctions As New DataView
        dvFunctions.Table = FileDataSet.SpecialFunctions
        dvFunctions.RowFilter = "Date < '" & Format(dtePeriodStart, "MM/dd/yyyy") & "' OR Date > '" & Format(dtePeriodEnd, "MM/dd/yyyy") & "'"

        If dvFunctions.Count > 0 Then
            MessageBox.Show("Selected file contains functions that are not in the pay period.  Function dates before" &
                 " the period beginning date will be changed to the first day in the pay period and function dates after" &
                 " the period ending date will be changed to the last day in the pay period.  Please be patient as the" &
                 " conversion may take several minutes.", "Invalid Functions Found", MessageBoxButtons.OK)

            Dim i As Integer = 0

            Do Until i = dvFunctions.Count
                Dim dteDate As Date = CDate(dvFunctions(i)("Date"))

                If dteDate < dtePeriodStart Then
                    dvFunctions(i)("Date") = dtePeriodStart
                End If
                If dteDate > dtePeriodEnd Then
                    dvFunctions(i)("Date") = dtePeriodEnd
                End If
            Loop

            FileDataSet.AcceptChanges()

            Try
                Dim objDSStream As New MemoryStream
                Dim objFileEncoder As New clsFileEncoder
                objDSStream = New MemoryStream

                FileDataSet.WriteXml(objDSStream)

                objFileEncoder.EncodeFile(Path, objDSStream)

                objFileEncoder.Dispose()
                objDSStream.Close()
                objDSStream.Dispose()
                FileDataSet.AcceptChanges()

            Catch subEx As Exception
                MessageBox.Show("Could not convert the requested file.  The file may be an invalid type or may be corrupted.", "Error Converting File", MessageBoxButtons.OK, MessageBoxIcon.Error)
                Debug.WriteLine(subEx.ToString)
                Exit Sub
            End Try
        End If

        'Check for tips not in the pay period.
        Dim dvTips As New DataView
        dvTips.Table = FileDataSet.Tips
        dvTips.RowFilter = "WorkingDate < '" & Format(dtePeriodStart, "MM/dd/yyyy") & "' OR WorkingDate > '" & Format(dtePeriodEnd, "MM/dd/yyyy") & "'"

        If dvTips.Count > 0 Then
            MessageBox.Show("Selected file contains tips that are not in the pay period.  Tip dates before" &
                 " the period beginning date will be changed to the first day in the pay period and function dates after" &
                 " the period ending date will be changed to the last day in the pay period.  Please be patient as the" &
                 " conversion may take several minutes.", "Invalid Tips Found", MessageBoxButtons.OK)

            Dim i As Integer = 0

            Do Until i = dvTips.Count
                Dim dteDate As Date = CDate(dvTips(i)("WorkingDate"))

                If dteDate < dtePeriodStart Then
                    dvTips(i)("WorkingDate") = dtePeriodStart
                End If
                If dteDate > dtePeriodEnd Then
                    dvTips(i)("WorkingDate") = dtePeriodEnd
                End If
            Loop

            FileDataSet.AcceptChanges()

            Try
                Dim objDSStream As New MemoryStream
                Dim objFileEncoder As New clsFileEncoder
                objDSStream = New MemoryStream

                FileDataSet.WriteXml(objDSStream)

                objFileEncoder.EncodeFile(Path, objDSStream)

                objFileEncoder.Dispose()
                objDSStream.Close()
                objDSStream.Dispose()
                FileDataSet.AcceptChanges()

            Catch subEx As Exception
                MessageBox.Show("Could not convert the requested file.  The file may be an invalid type or may be corrupted.", "Error Converting File", MessageBoxButtons.OK, MessageBoxIcon.Error)
                Debug.WriteLine(subEx.ToString)
                Exit Sub
            End Try
        End If

        'Check for servers where suppress chit option is null.
        Dim blnFoundNull As Boolean = False

        For Each row As DataRow In FileDataSet.Servers
            If IsDBNull(row("SuppressChit")) Then
                row("SuppressChit") = False
                blnFoundNull = True
            End If
        Next

        If blnFoundNull = True Then
            MessageBox.Show("Null values were found in the 'Suppress Tip Chit' option for some of the servers.  The option for these servers has been unchecked.", "Null Values Found", MessageBoxButtons.OK)

            Try
                Dim objDSStream As New MemoryStream
                Dim objFileEncoder As New clsFileEncoder
                objDSStream = New MemoryStream

                FileDataSet.WriteXml(objDSStream)

                objFileEncoder.EncodeFile(Path, objDSStream)

                objFileEncoder.Dispose()
                objDSStream.Close()
                objDSStream.Dispose()
                FileDataSet.AcceptChanges()

            Catch subEx As Exception
                MessageBox.Show("Could not save file.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error)
                Debug.WriteLine(subEx.ToString)
                Exit Sub
            End Try
        End If

        FileDataSet.Clear()
        FileDataSet.AcceptChanges()

        Dim form As New frmEnterTips
        form.CurrentFile = Path

        Try
            form.Show()
            EnableMenuCommands(True)
        Catch ex As Exception
        End Try
    End Sub

    Private Function ValidateFile() As Boolean
        If FileDataSet.Settings.Rows.Count = 0 Then Return False

        For Each row As DataRow In FileDataSet.Settings
            Select Case row("Setting").ToString
                Case "PeriodStart"
                    If row("Value").ToString = "" Or row("Value") Is Nothing Then
                        Return False
                    Else
                        Try
                            Dim dtePeriodStart As Date = CDate(row("Value"))
                        Catch ex As Exception
                            Return False
                        End Try
                    End If
                Case "PeriodEnd"
                    If row("Value").ToString = "" Or row("Value") Is Nothing Then
                        Return False
                    Else
                        Try
                            Dim dtePeriodEnd As Date = CDate(row("Value"))
                        Catch ex As Exception
                            Return False
                        End Try
                    End If
                Case "WorkingDate"
                    If row("Value").ToString = "" Or row("Value") Is Nothing Then
                        Return False
                    Else
                        Try
                            Dim dteWorkingDate As Date = CDate(row("Value"))
                        Catch ex As Exception
                            Return False
                        End Try
                    End If
                Case Else
                    Return False
            End Select
        Next

        Return True
    End Function

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

    Private Sub mnuPrintServerList_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles mnuPrintServerList.Click
        With docServerList.DefaultPageSettings
            .Margins.Top = 50
            .Margins.Bottom = 50
            .Margins.Left = 50
            .Margins.Right = 50
        End With

        If dlgPrint.ShowDialog = Windows.Forms.DialogResult.OK Then
            docServerList.Print()
        End If

        dlgPrint.Dispose()
    End Sub

    Private Sub docServerList_PrintPage(ByVal sender As System.Object, ByVal e As System.Drawing.Printing.PrintPageEventArgs) Handles docServerList.PrintPage
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

        Dim intPrintAreaHeight, intPrintAreaWidth, marginLeft, marginTop As Int32
        With docServerList.DefaultPageSettings
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
            Microsoft.VisualBasic.Interaction.Shell(My.Application.Info.DirectoryPath & "\TT Debug.exe", AppWinStyle.NormalFocus, False, 1)
        Catch ex As FileNotFoundException
            MessageBox.Show("Could not find the maintenance utility external executable.  Please reinstall the application.", "File Not Found", MessageBoxButtons.OK, MessageBoxIcon.Error)
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
