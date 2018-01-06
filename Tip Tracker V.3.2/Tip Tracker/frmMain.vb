Imports System.IO

Public Class frmMain
    Private m_strCurrentFile As String = ""
    Private m_objGlobalStream As File

    Private ReadOnly Property GlobalSettingsFile() As String
        Get
            Return GlobalFile.GetGlobalFilePath()
        End Get
    End Property

    Private Property DefaultDataDirectory() As String
        Get
            Return Me.GlobalDataSet.Settings.FindBySetting("DefaultDirectory")("Value").ToString
        End Get

        Set(ByVal value As String)
            Me.GlobalDataSet.Settings.FindBySetting("DefaultDirectory")("Value") = value
        End Set
    End Property

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

        'If a global settings file has not been created, display the configurator dialog to create one.
        If Not File.Exists(Me.GlobalSettingsFile) Then
            MessageBox.Show("The global settings file could not be found.  Please create a new file or open an existing file.", "File Not Found", MessageBoxButtons.OK, MessageBoxIcon.Warning)

            If frmConfigurator.ShowDialog <> Windows.Forms.DialogResult.OK Then
                MessageBox.Show("The global settings file path was not set.  Tip Tracker cannot run without a global settings file.  The application will terminate.", "File Not Found", MessageBoxButtons.OK, MessageBoxIcon.Error)
                frmConfigurator.Dispose()
                End
            End If

            frmConfigurator.Dispose()
        End If

        'Verify integrity of global settings file.
        While GlobalFileLoaded() = False
            Debug.WriteLine("Cannot load global file.")
        End While

        'If a file was double-clicked, try to open the file.
        For Each param As String In My.Application.CommandLineArgs
            Debug.WriteLine(param)
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

    Private Function GlobalFileLoaded() As Boolean
        Me.GlobalDataSet.Clear()
        Me.GlobalDataSet.AcceptChanges()

        If Not File.Exists(Me.GlobalSettingsFile) Then
            If MessageBox.Show("Global settings file not found.  Do you wish to rebuild the file?.", "File Not Found", MessageBoxButtons.YesNo, MessageBoxIcon.Warning) <> Windows.Forms.DialogResult.Yes Then
                End
            Else
                Dim blnContinue As Boolean = False

                If frmConfigurator.ShowDialog() <> Windows.Forms.DialogResult.OK Then
                    frmConfigurator.Dispose()
                    End
                End If

                frmConfigurator.Dispose()
            End If
        End If

        Try
            Dim objFileEncoder As New clsFileEncoder

            Me.GlobalDataSet.ReadXml(objFileEncoder.DecodeFile(Me.GlobalSettingsFile))

            objFileEncoder.Dispose()
            objFileEncoder = Nothing
        Catch ex As FormatException
            Try
                Dim objFileEncoder As New clsFileEncoder

                Me.GlobalDataSet.ReadXml(objFileEncoder.LegacyDecodeFile(Me.GlobalSettingsFile))
                SaveGlobalFile()
                objFileEncoder.Dispose()
                objFileEncoder = Nothing
            Catch subEx As Exception
                MessageBox.Show("The global settings file could not be read.  Contact support.", "Error Converting File", MessageBoxButtons.OK)
                End
            End Try
        Catch ex As Exception
            MessageBox.Show("Cannot load global settings file.  File may be corrupt or its contents may have been changed.  Contact support.", "Error Loading File", MessageBoxButtons.OK, MessageBoxIcon.Error)
            End
        End Try

        If Me.GlobalDataSet.Servers.Rows.Count = 0 Then
            MessageBox.Show("There are no servers in the server template table.", "No Servers", MessageBoxButtons.OK)
        End If

        If Me.GlobalDataSet.Settings.Rows.Count = 0 Then
            Dim drNewRow As DataRow = Me.GlobalDataSet.Settings.NewRow

            drNewRow("Setting") = "DefaultDirectory"
            drNewRow("Value") = ""

            Me.GlobalDataSet.Settings.Rows.Add(drNewRow)
        End If

        For Each row As DataRow In Me.GlobalDataSet.Settings.Rows
            Select Case row("Setting").ToString
                Case "DefaultDirectory"
                    If row("Value").ToString Is Nothing Or row("Value").ToString = "" Then
                        MessageBox.Show("You must select a default data directory.", "Select Directory", MessageBoxButtons.OK)
                        frmSettings.btnCancel.Enabled = False
                        frmSettings.ShowDialog()
                        row("Value") = frmSettings.DefaultDirectory
                        frmSettings.Dispose()
                        SaveGlobalFile()
                    End If
                Case Else
                    MessageBox.Show("The settings table is corrupted and will be rebuilt.", "Invalid Settings", MessageBoxButtons.OK)
                    Me.GlobalDataSet.Settings.Clear()
                    SaveGlobalFile()
                    Return False
            End Select
        Next

        'Check for servers where suppress chit option is null.
        Dim blnFoundNull As Boolean = False

        For Each row As DataRow In Me.GlobalDataSet.Servers
            If IsDBNull(row("SuppressChit")) Then
                row("SuppressChit") = False
                blnFoundNull = True
            End If
        Next

        If blnFoundNull = True Then
            MessageBox.Show("Null values were found in the 'Suppress Tip Chit' option for some of the template servers.  The option for these servers has been unchecked.", "Null Values Found", MessageBoxButtons.OK)
            SaveGlobalFile()
        End If

        Return True
    End Function

    Friend Function SaveGlobalFile() As Boolean
        Me.GlobalDataSet.AcceptChanges()

        Try
            Dim objFileEncoder As New clsFileEncoder
            Dim objDSStream As New MemoryStream

            Me.GlobalDataSet.WriteXml(objDSStream)

            objDSStream.Flush()

            objFileEncoder.EncodeFile(Me.GlobalSettingsFile, objDSStream)

            objDSStream.Close()
            objDSStream.Dispose()

            objFileEncoder.Dispose()
        Catch ex As Exception
            MessageBox.Show("Could not save global settings file.  Contact support", "Error Writing File", MessageBoxButtons.OK, MessageBoxIcon.Warning)
            End
        End Try
    End Function

    Private Function GlobalFileChanged() As Boolean
        Dim strDatasetContents As String = ""
        Dim objDSStream As New MemoryStream()
        Dim objDSStreamReader As New StreamReader(objDSStream)

        Try
            Me.GlobalDataSet.WriteXml(objDSStream)

            objDSStream.Seek(0, SeekOrigin.Begin)
            strDatasetContents = objDSStreamReader.ReadToEnd()
        Catch ex As Exception
            Return True
        End Try

        Dim objFileEncoder As New clsFileEncoder
        Dim strFileContents As String = ""

        Try
            objDSStreamReader = New StreamReader(objFileEncoder.DecodeFile(Me.GlobalSettingsFile))

            strFileContents = objDSStreamReader.ReadToEnd

            objFileEncoder.Dispose()
            objFileEncoder = Nothing
        Catch ex As Exception
            Return True
        End Try

        Return strDatasetContents <> strFileContents
    End Function

    Private Sub mnuExit_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles mnuExit.Click
        Me.Close()
    End Sub

    Private Sub mnuManageTemplateServers_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles mnuManageTemplateServers.Click
        If GlobalFileChanged() Then
            If GlobalFileLoaded() = False Then
                MessageBox.Show("Tip Tracker has encountered an error and needs to close.  Another user has changed the contents of the global settings file and the file cannot be reloaded.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error)
                For Each form As Windows.Forms.Form In Me.MdiChildren
                    form.Close()
                Next

                End
            End If
        End If

        frmManageServers.ShowDialog()
        frmManageServers.Dispose()

        Me.GlobalDataSet.AcceptChanges()
        SaveGlobalFile()
    End Sub

    Private Sub mnuSettings_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles mnuSettings.Click
        If GlobalFileChanged() Then
            If GlobalFileLoaded() = False Then
                MessageBox.Show("Tip Tracker has encountered an error and needs to close.  Another user has changed the contents of the global settings file and the file cannot be reloaded.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error)
                For Each form As Windows.Forms.Form In Me.MdiChildren
                    form.Close()
                Next

                End
            End If
        End If

        frmSettings.DefaultDirectory = Me.DefaultDataDirectory

        If frmSettings.ShowDialog <> Windows.Forms.DialogResult.OK Then
            frmSettings.Dispose()
            Exit Sub
        End If

        Me.DefaultDataDirectory = frmSettings.DefaultDirectory

        SaveGlobalFile()
    End Sub

    Private Sub mnuMinimizeAll_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles mnuMinimizeAll.Click
        For Each form As Windows.Forms.Form In Me.MdiChildren
            form.WindowState = FormWindowState.Minimized
        Next
    End Sub

    Private Sub mnuMaximizeAll_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles mnuMaximizeAll.Click
        For Each form As Windows.Forms.Form In Me.MdiChildren
            form.WindowState = FormWindowState.Maximized
        Next
    End Sub

    Private Sub mnuCascade_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles mnuCascade.Click
        For Each form As Windows.Forms.Form In Me.MdiChildren
            form.WindowState = FormWindowState.Normal
        Next

        Me.LayoutMdi(MdiLayout.Cascade)
    End Sub

    Friend Function NumberOfFilesOpen() As Integer
        'Returns the number of data files open.  Since each data file calls a new instance of
        'frmEnterTips when it is opened, the number of mdi children is also the number of open
        'data files.
        Dim intFilesOpen As Integer = 0

        For Each form As Form In Me.MdiChildren
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
        Me.ActiveMdiChild.Close()
    End Sub

    Private Sub mnuSave_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles mnuSave.Click
        For Each form As frmEnterTips In Me.MdiChildren
            If form Is Me.ActiveMdiChild Then
                form.SaveData()
            End If
        Next
    End Sub

    Private Sub mnuSaveAs_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles mnuSaveAs.Click
        dlgSaveFile.InitialDirectory = Me.DefaultDataDirectory
        dlgSaveFile.FileName = ""

        If dlgSaveFile.ShowDialog <> Windows.Forms.DialogResult.OK Then
            dlgSaveFile.Dispose()
            Exit Sub
        End If

        Dim strNewFileName As String = dlgSaveFile.FileName

        For Each form As frmEnterTips In Me.MdiChildren
            If form Is Me.ActiveMdiChild Then
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
        dlgSaveFile.InitialDirectory = Me.DefaultDataDirectory
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
        drNewRow = Me.FileDataSet.Settings.NewRow
        drNewRow("Setting") = "PeriodStart"
        drNewRow("Value") = dtePeriodStart.ToString
        Me.FileDataSet.Settings.Rows.Add(drNewRow)

        drNewRow = Me.FileDataSet.Settings.NewRow
        drNewRow("Setting") = "PeriodEnd"
        drNewRow("Value") = dtePeriodEnd.ToString
        Me.FileDataSet.Settings.Rows.Add(drNewRow)

        drNewRow = Me.FileDataSet.Settings.NewRow
        drNewRow("Setting") = "WorkingDate"
        drNewRow("Value") = dtePeriodStart.ToString
        Me.FileDataSet.Settings.Rows.Add(drNewRow)

        drNewRow = Nothing

        'Copy the template servers into the file dataset.
        Me.FileDataSet.Servers.Merge(Me.GlobalDataSet.Servers)

        Try
            'Encode the dataset then write the file to the selected directory.
            Dim objFileEncoder As New clsFileEncoder
            Dim objDSStream As New MemoryStream

            Me.FileDataSet.WriteXml(objDSStream)
            objDSStream.Flush()
            objDSStream.Seek(0, SeekOrigin.Begin)

            objFileEncoder.EncodeFile(strFileName, objDSStream)

            'Clean up the dataset, file encoding object and dataset stream.
            Me.FileDataSet.Clear()
            Me.FileDataSet.AcceptChanges()

            objFileEncoder.Dispose()
            objDSStream.Close()
            objDSStream.Dispose()
        Catch ex As IOException
            MessageBox.Show("The file name you entered already exists and is in use.  The file could not be created.", "Error Creating File", MessageBoxButtons.OK)
            Me.FileDataSet.Clear()
            Me.FileDataSet.AcceptChanges()
            Exit Sub
        Catch ex As Exception
            MessageBox.Show("Could not create the requested file.  Please contact support.", "Error Creating File", MessageBoxButtons.OK)
            Me.FileDataSet.Clear()
            Me.FileDataSet.AcceptChanges()
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
            If Not Directory.Exists(Me.DefaultDataDirectory) Then
                .InitialDirectory = My.Computer.FileSystem.SpecialDirectories.MyDocuments
            Else
                .InitialDirectory = Me.DefaultDataDirectory
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

            Me.FileDataSet.ReadXml(objDSStream)

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

                Me.FileDataSet.ReadXml(objDSStream)

                objFileDecoder.Dispose()
                objDSStream.Close()
                objDSStream.Dispose()

                Dim objFileEncoder As New clsFileEncoder
                objDSStream = New MemoryStream

                Me.FileDataSet.WriteXml(objDSStream)

                objFileEncoder.EncodeFile(Path, objDSStream)

                objFileEncoder.Dispose()
                objDSStream.Close()
                objDSStream.Dispose()
                Me.FileDataSet.AcceptChanges()

            Catch subEx As Exception
                MessageBox.Show("Could not convert the requested file.  The file may be an invalid type or may be corrupted.", "Error Converting File", MessageBoxButtons.OK, MessageBoxIcon.Error)
                Exit Sub
            End Try
        Catch ex As IOException
            MessageBox.Show("The requested file could not be opened because it is already in use.", "Error Opening File", MessageBoxButtons.OK)
            Me.FileDataSet.Clear()
            Me.FileDataSet.AcceptChanges()
            Exit Sub
        Catch ex As Exception
            MessageBox.Show("Could not open file.  Contact Support", "Error", MessageBoxButtons.OK)
            Me.FileDataSet.Clear()
            Me.FileDataSet.AcceptChanges()
            Exit Sub
        End Try

        If ValidateFile() = False Then
            MessageBox.Show("The file you selected is not a valid Tip Tracker data file.", "Invalid File Type", MessageBoxButtons.OK)
            Me.FileDataSet.Clear()
            Me.FileDataSet.AcceptChanges()
            Exit Sub
        End If

        'Check for functions not in the pay period.
        Dim lstInvalidFunctions As New List(Of String)

        Dim dtePeriodStart As Date = CDate(Me.FileDataSet.Settings.FindBySetting("PeriodStart")("Value"))
        Dim dtePeriodEnd As Date = CDate(Me.FileDataSet.Settings.FindBySetting("PeriodEnd")("Value"))

        Dim dvFunctions As New DataView
        dvFunctions.Table = Me.FileDataSet.SpecialFunctions
        dvFunctions.RowFilter = "Date < '" & Format(dtePeriodStart, "MM/dd/yyyy") & "' OR Date > '" & Format(dtePeriodEnd, "MM/dd/yyyy") & "'"

        If dvFunctions.Count > 0 Then
            MessageBox.Show("Selected file contains functions that are not in the pay period.  Function dates before" & _
                 " the period beginning date will be changed to the first day in the pay period and function dates after" & _
                 " the period ending date will be changed to the last day in the pay period.  Please be patient as the" & _
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

            Me.FileDataSet.AcceptChanges()

            Try
                Dim objDSStream As New MemoryStream
                Dim objFileEncoder As New clsFileEncoder
                objDSStream = New MemoryStream

                Me.FileDataSet.WriteXml(objDSStream)

                objFileEncoder.EncodeFile(Path, objDSStream)

                objFileEncoder.Dispose()
                objDSStream.Close()
                objDSStream.Dispose()
                Me.FileDataSet.AcceptChanges()

            Catch subEx As Exception
                MessageBox.Show("Could not convert the requested file.  The file may be an invalid type or may be corrupted.", "Error Converting File", MessageBoxButtons.OK, MessageBoxIcon.Error)
                Debug.WriteLine(subEx.ToString)
                Exit Sub
            End Try
        End If

        'Check for tips not in the pay period.
        Dim dvTips As New DataView
        dvTips.Table = Me.FileDataSet.Tips
        dvTips.RowFilter = "WorkingDate < '" & Format(dtePeriodStart, "MM/dd/yyyy") & "' OR WorkingDate > '" & Format(dtePeriodEnd, "MM/dd/yyyy") & "'"

        If dvTips.Count > 0 Then
            MessageBox.Show("Selected file contains tips that are not in the pay period.  Tip dates before" & _
                 " the period beginning date will be changed to the first day in the pay period and function dates after" & _
                 " the period ending date will be changed to the last day in the pay period.  Please be patient as the" & _
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

            Me.FileDataSet.AcceptChanges()

            Try
                Dim objDSStream As New MemoryStream
                Dim objFileEncoder As New clsFileEncoder
                objDSStream = New MemoryStream

                Me.FileDataSet.WriteXml(objDSStream)

                objFileEncoder.EncodeFile(Path, objDSStream)

                objFileEncoder.Dispose()
                objDSStream.Close()
                objDSStream.Dispose()
                Me.FileDataSet.AcceptChanges()

            Catch subEx As Exception
                MessageBox.Show("Could not convert the requested file.  The file may be an invalid type or may be corrupted.", "Error Converting File", MessageBoxButtons.OK, MessageBoxIcon.Error)
                Debug.WriteLine(subEx.ToString)
                Exit Sub
            End Try
        End If

        'Check for servers where suppress chit option is null.
        Dim blnFoundNull As Boolean = False

        For Each row As DataRow In Me.FileDataSet.Servers
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

                Me.FileDataSet.WriteXml(objDSStream)

                objFileEncoder.EncodeFile(Path, objDSStream)

                objFileEncoder.Dispose()
                objDSStream.Close()
                objDSStream.Dispose()
                Me.FileDataSet.AcceptChanges()

            Catch subEx As Exception
                MessageBox.Show("Could not save file.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error)
                Debug.WriteLine(subEx.ToString)
                Exit Sub
            End Try
        End If

        Me.FileDataSet.Clear()
        Me.FileDataSet.AcceptChanges()

        Dim form As New frmEnterTips
        form.CurrentFile = Path

        Try
            form.Show()
            EnableMenuCommands(True)
        Catch ex As Exception
        End Try
    End Sub

    Private Function ValidateFile() As Boolean
        If Me.FileDataSet.Settings.Rows.Count = 0 Then Return False

        For Each row As DataRow In Me.FileDataSet.Settings
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

        With dlgSaveFile
            .InitialDirectory = My.Computer.FileSystem.SpecialDirectories.Desktop
            .RestoreDirectory = True
            .Title = "Export Server List"
            .Filter = "XML Files (*.xml)|*.xml"
            .DefaultExt = "*.xml"
            .FileName = "Server List"

            If .ShowDialog <> Windows.Forms.DialogResult.OK Then
                .Dispose()
                Exit Sub
            End If

            .Filter = "Tip Tracker Data Files (*.ttd)|*.ttd"
            .DefaultExt = "*.ttd"

            strFileName = .FileName
        End With

        Try
            Me.GlobalDataSet.Servers.WriteXml(strFileName)
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
        dv.Table = Me.GlobalDataSet.Servers
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
