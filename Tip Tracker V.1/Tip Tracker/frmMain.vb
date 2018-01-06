Imports System.IO

Public Class frmMain
    Private m_strCurrentFile As String = ""
    Private m_strCurrentUser As String = "<None>"

    'TODO: Charge tips vs. cash tips.
    'TODO: Ignore date on cash tips.
    'TODO: Treat spec. func. as charge tip.
    'TODO: Update reports.
    'TODO: Help files.

    Private ReadOnly Property GlobalSettingsFile() As String
        Get
            Return My.Computer.Registry.GetValue("HKEY_LOCAL_MACHINE\Software\MJB\TipTracker\", "GlobalFile", "").ToString
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

    Private Property CurrentUser() As String
        Get
            Return m_strCurrentUser
        End Get
        Set(ByVal value As String)
            m_strCurrentUser = value
        End Set
    End Property

    Private Sub frmMain_FormClosing(ByVal sender As Object, ByVal e As System.Windows.Forms.FormClosingEventArgs) Handles Me.FormClosing
        SaveGlobalFile()
    End Sub

    Private Sub frmMain_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
        'Disable the menu commands that can only be used if a file is open.
        EnableMenuCommands(False)

        'If a global settings file has not been created, display the configurator dialog to create one.
        Dim blnContinue As Boolean = False

        While blnContinue = False
            Try
                If Not File.Exists(Me.GlobalSettingsFile) Then
                    MessageBox.Show("Could not find the global settings file.  Please use the configurator to recreate it or open an existing file.", "File Not Found", MessageBoxButtons.OK)
                Else
                    Exit While
                End If
            Catch ex As Exception
                MessageBox.Show("The global settings file path has not been set.  Please use the configurator to open or create the file.", "File Not Found", MessageBoxButtons.OK, MessageBoxIcon.Warning)
            End Try

            frmConfigurator.ShowDialog()
            frmConfigurator.Dispose()
            blnContinue = True
        End While

        'Verify integrity of global settings file.
        While GlobalFileLoaded() = False
            Debug.WriteLine("Cannot load global file.")
        End While

        'TODO:  Add login routine.
        'Me.Visible = True
        'frmLogin.ShowDialog()
        'Me.CurrentUser = frmLogin.CurrentUser
        'frmLogin.Dispose()

        'TODO:  Set authorizations.


        'TODO: Update title bar text to show current user.

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
                While blnContinue = False
                    frmConfigurator.ShowDialog()
                    frmConfigurator.Dispose()
                    blnContinue = True
                End While
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
                MessageBox.Show("File could not be converted from legacy encoding.  Contact support.", "Error Converting File", MessageBoxButtons.OK)
                End
            End Try
        Catch ex As Exception
            MessageBox.Show("Cannot load global settings file.  Contact support.", "Error Loading File", MessageBoxButtons.OK, MessageBoxIcon.Error)
            End
        End Try

        If Me.GlobalDataSet.Servers.Rows.Count = 0 Then
            MessageBox.Show("There are no servers in the server template table.", "No Servers", MessageBoxButtons.OK)
        End If

        If Me.GlobalDataSet.Users.Rows.Count = 0 Then
            MessageBox.Show("There are no users in the users table.  Default user will be added", "No Users", MessageBoxButtons.OK)

            Dim drNewRow As DataRow = Me.GlobalDataSet.Users.NewRow

            drNewRow("UserName") = "INSTALLER"
            drNewRow("FirstName") = "Installer"
            drNewRow("LastName") = "Installer"
            drNewRow("Password") = "ttsupport"
            drNewRow("Authorizations") = ""

            Me.GlobalDataSet.Users.Rows.Add(drNewRow)
            SaveGlobalFile()
        Else
            For Each row As DataRow In Me.GlobalDataSet.Users.Rows
                If row("UserName").ToString = "INSTALLER" Then
                    Exit For
                End If
                MessageBox.Show("The default user has been deleted from the users table.  User record will be re-created.", "Default User Deleted", MessageBoxButtons.OK)
                Dim drNewRow As DataRow = Me.GlobalDataSet.Users.NewRow

                drNewRow("UserName") = "INSTALLER"
                drNewRow("FirstName") = "Installer"
                drNewRow("LastName") = "Installer"
                drNewRow("Password") = "ttsupport"
                drNewRow("Authorizations") = ""

                Me.GlobalDataSet.Users.Rows.Add(drNewRow)
                SaveGlobalFile()
            Next
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

        Return True
    End Function

    Private Sub SaveGlobalFile()
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
    End Sub

    Private Sub mnuExit_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles mnuExit.Click
        Me.Close()
    End Sub

    Private Sub mnuManageTemplateServers_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles mnuManageTemplateServers.Click
        frmManageServers.ShowDialog()
        frmManageServers.Dispose()

        Me.GlobalDataSet.AcceptChanges()
        SaveGlobalFile()
    End Sub

    Private Sub mnuSettings_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles mnuSettings.Click
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
        dlgOpenFile.InitialDirectory = Me.DefaultDataDirectory

        If dlgOpenFile.ShowDialog <> Windows.Forms.DialogResult.OK Then
            dlgOpenFile.Dispose()
            Exit Sub
        End If

        Dim strFile As String = dlgOpenFile.FileName

        dlgOpenFile.Dispose()

        Try
            Dim objFileDecoder As New clsFileEncoder
            Dim objDSStream As New MemoryStream

            objDSStream = objFileDecoder.DecodeFile(strFile)

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

                objDSStream = objFileDecoder.LegacyDecodeFile(strFile)

                objDSStream.Flush()
                objDSStream.Seek(0, SeekOrigin.Begin)

                Me.FileDataSet.ReadXml(objDSStream)

                objFileDecoder.Dispose()
                objDSStream.Close()
                objDSStream.Dispose()

                Dim objFileEncoder As New clsFileEncoder
                objDSStream = New MemoryStream

                Me.FileDataSet.WriteXml(objDSStream)

                objFileEncoder.EncodeFile(strFile, objDSStream)

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

        Me.FileDataSet.Clear()
        Me.FileDataSet.AcceptChanges()

        Dim form As New frmEnterTips
        form.CurrentFile = strFile

        form.Show()
        EnableMenuCommands(True)
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

    Private Sub mnuManageUsers_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles mnuManageUsers.Click
        'TODO:  Code user management.
        MessageBox.Show("User logins and user management are disabled in this release.", "Command Unavailable", MessageBoxButtons.OK)

        'frmManageUsers.ShowDialog()
        'frmManageUsers.Dispose()
    End Sub

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
        Dim font As New System.Drawing.Font("Courier New", 10)

        'Initialize local static variables that contain the current line position and
        'the current page number.
        Static position As Single = 50

        'Initialize local constants that contain the normal line spacing and padded 
        'line spacing (double spacing).
        Const intLineSpacing As Integer = 20
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

        For Each row As DataRow In Me.GlobalDataSet.Servers.Rows
            If position < marginTop + intPrintAreaHeight Then
                Dim strServerNumber As String = row("ServerNumber").ToString
                Dim strName As String = row("FirstName").ToString & " " & Microsoft.VisualBasic.Left(row("LastName").ToString, 1) & "."

                'Draw the first column to the page.
                e.Graphics.DrawString(strServerNumber, font, Brushes.Black, intCol1NumberPos, position)
                e.Graphics.DrawString(strName, font, Brushes.Black, intCol1NamePos, position)

                'Draw the second column to the page.
                e.Graphics.DrawString(strServerNumber, font, Brushes.Black, intCol2NumberPos, position)
                e.Graphics.DrawString(strName, font, Brushes.Black, intCol2NamePos, position)

                position += intLineSpacing
            Else
                e.HasMorePages = True
            End If
        Next
    End Sub

    Private Sub mnuImportServerList_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles mnuImportServerList.Click
        If Me.GlobalDataSet.Servers.Rows.Count <> 0 Then
            MsgBox("You may only use this command if there are no existing servers in the template.")
            Exit Sub
        End If

        Dim strFileName As String

        With dlgOpenFile
            .Filter = "Global Data Files (*.dat)|*.dat"
            .DefaultExt = "*.dat"
            .Title = "Select File to Import"

            .InitialDirectory = My.Computer.FileSystem.SpecialDirectories.Desktop
            .RestoreDirectory = True

            If .ShowDialog <> Windows.Forms.DialogResult.OK Then
                .Dispose()
                Exit Sub
            End If

            strFileName = .FileName

            .Dispose()
        End With

        Dim dsTemp As New GlobalDataSet

        Try
            Dim objFileEncoder As New clsFileEncoder

            dsTemp.ReadXml(objFileEncoder.DecodeFile(strFileName))

            objFileEncoder.Dispose()
            objFileEncoder = Nothing
        Catch ex As System.IO.FileNotFoundException
            MsgBox("File not found.")
            GoTo Cleanup
        Catch ex As ConstraintException
            MsgBox("Constraint violation.")
            GoTo Cleanup
        Catch ex As FormatException
            Try
                Dim objFileEncoder As New clsFileEncoder

                dsTemp.ReadXml(objFileEncoder.LegacyDecodeFile(strFileName))

                objFileEncoder.Dispose()
                objFileEncoder = Nothing
            Catch subEx As Exception
                MessageBox.Show("Could not import server list.  Contact support.", "Import Error", MessageBoxButtons.OK)
                dsTemp = Nothing
                GoTo Cleanup
            End Try

        Catch ex As Exception
            MsgBox("Other Exception.")
            GoTo Cleanup
        End Try

        If dsTemp.Servers.Rows.Count = 0 Then
            MsgBox("There are no servers to import.")
            GoTo Cleanup
        Else
            If MsgBox("There are " & dsTemp.Servers.Rows.Count & " servers.  Continue?", MsgBoxStyle.OkCancel) = MsgBoxResult.Cancel Then
                GoTo Cleanup
            End If
        End If

        Me.GlobalDataSet.Servers.Merge(dsTemp.Servers)

        MsgBox("Merge complete.")

        SaveGlobalFile()

Cleanup:
        dsTemp = Nothing
    End Sub

    Private Sub mnuAbout_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles mnuAbout.Click
        frmAbout.ShowDialog()
        frmAbout.Dispose()
    End Sub
End Class
