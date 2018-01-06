Public Class frmMain
    Private m_strCurrentFile As String = ""
    Private m_strCurrentUser As String = "<None>"
    Private m_fileStream As FileStream

    Private ReadOnly Property GlobalSettingsFile() As String
        Get
            Return My.Computer.Registry.GetValue("HKEY_LOCAL_MACHINE\Software\MJB\TipTrackerSF\", "GlobalFile", "").ToString
        End Get
    End Property

    Private Property DefaultDataDirectory() As String
        Get
            Try
                Return Me.GlobalDataSet.Settings.FindBySetting("DefaultDirectory")("Value").ToString
                Debug.WriteLine(Me.GlobalDataSet.Settings.FindBySetting("DefaultDirectory")("Value").ToString)
            Catch ex As Exception
                Return My.Computer.FileSystem.SpecialDirectories.MyDocuments
            End Try
        End Get
        Set(ByVal value As String)
            Try
                Me.GlobalDataSet.Settings.FindBySetting("DefaultDirectory")("Value") = value
            Catch ex As Exception
                Dim drNewRow As DataRow = Me.GlobalDataSet.Settings.NewRow

                drNewRow("Setting") = "DefaultDirectory"
                drNewRow("Value") = value

                Me.GlobalDataSet.Settings.Rows.Add(drNewRow)
                SaveGlobalFile()
            End Try

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

        'Check for a file already open.  If there is one and it has unsaved changes, prompt the user
        'to save the changes.
        If DataFileAlreadyOpen() = True Then
            If Me.FileDataSet.HasChanges Then
                Select Case MessageBox.Show("Save changes to the data file " & Me.CurrentDataFile & "?", "Save Changes", MessageBoxButtons.YesNoCancel)
                    Case Windows.Forms.DialogResult.Yes
                        SaveDataFile()
                        UnloadDataFile()
                    Case Windows.Forms.DialogResult.No
                        UnloadDataFile()
                    Case Windows.Forms.DialogResult.Cancel
                        e.Cancel = True
                        Exit Sub
                End Select
            End If
        End If
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

        'Set binding source sort modes.
        Me.ServersBindingSource.Sort = "ServerNumber"
        Me.SpecialFunctionsBindingSource.Sort = "SpecialFunction"
        Me.SpecialFunctionTipsBindingSource.Sort = "ServerNumber"

        'TODO:  Add login routine.
        'Me.Visible = True
        'frmLogin.ShowDialog()
        'Me.CurrentUser = frmLogin.CurrentUser
        'frmLogin.Dispose()

        'TODO:  Set authorizations.

    End Sub

    Private Sub EnableMenuCommands(ByVal Enabled As Boolean)
        mnuClose.Enabled = Enabled
        mnuSave.Enabled = Enabled
        mnuSaveAs.Enabled = Enabled
        mnuView.Enabled = Enabled
        pnlTipEntryPanel.Enabled = Enabled
        grpServers.Enabled = Enabled
    End Sub

    Private Sub UpdateTitleBar()
        Dim strTitleBarText As String = "Tip Tracker SF"

        If Me.CurrentDataFile <> "" Then
            'Extract the file name then change the text of the form to show the file name.
            Dim intBegin As Integer = 0
            Dim intEnd As Integer = 0

            For i As Integer = Len(Me.CurrentDataFile) To 1 Step -1
                Dim c As Char = GetChar(Me.CurrentDataFile, i)
                If c = "." Then
                    intEnd = i
                End If
                If c = "\" Then
                    intBegin = i
                    Exit For
                End If
            Next

            If intBegin <> 1 And intEnd <> 1 And intBegin <> 0 And intEnd <> 0 And intBegin < intEnd Then
                Dim strFileName As String = Mid(Me.CurrentDataFile, intBegin + 1, intEnd - intBegin - 1)
                strTitleBarText += " - " & strFileName
            Else
                strTitleBarText += " - " & "########"
            End If
        End If

        Me.Text = strTitleBarText
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

    Private Function DataFileAlreadyOpen() As Boolean
        If Me.CurrentDataFile <> "" Or Me.FileDataSet.Tips.Rows.Count <> 0 Or Me.FileDataSet.Servers.Rows.Count <> 0 Then
            Return True
        Else
            Return False
        End If
    End Function

    Private Sub LoadDataFile(ByVal FileToOpen As String)
        If Not File.Exists(FileToOpen) Then
            MessageBox.Show("Could not find the requested file.", "File Not Found", MessageBoxButtons.OK)
            Exit Sub
        End If

        Try
            Dim objFileDecoder As New clsFileEncoder

            Me.FileDataSet.ReadXml(objFileDecoder.DecodeFile(FileToOpen))

            objFileDecoder.Dispose()
            objFileDecoder = Nothing

            Me.FileDataSet.AcceptChanges()
        Catch ex As IOException
            MessageBox.Show("Could not load the request file.  The file is in use by another process.", "Error Loading File", MessageBoxButtons.OK)
            Exit Sub
        Catch ex As Exception
            MessageBox.Show("Could not load the requested file.  File may be corrupted or may have had its contents changed.", "Error Loading File", MessageBoxButtons.OK)
            Exit Sub
        End Try

        Me.CurrentDataFile = FileToOpen
        UpdateTitleBar()
        EnableMenuCommands(True)
        m_fileStream = File.Open(Me.CurrentDataFile, FileMode.Open)

        Me.SpecialFunctionTipsBindingSource.Filter = ""
        Me.cboSelectSpecialFunction.SelectedIndex = -1
        UpdateSFTotals()
    End Sub

    Private Sub SaveDataFile()
        Me.FileDataSet.AcceptChanges()

        Try
            m_fileStream.Close()

            Dim objFileEncoder As New clsFileEncoder
            Dim objDSStream As New MemoryStream

            Me.FileDataSet.WriteXml(objDSStream)

            objDSStream.Flush()

            objFileEncoder.EncodeFile(Me.CurrentDataFile, objDSStream)

            objDSStream.Close()
            objDSStream.Dispose()

            objFileEncoder.Dispose()

            m_fileStream = File.Open(Me.CurrentDataFile, FileMode.Open)
        Catch ex As Exception
            MessageBox.Show("Could not save the data file.  Contact support", "Error Writing File", MessageBoxButtons.OK, MessageBoxIcon.Warning)
            End
        End Try
    End Sub

    Private Sub UnloadDataFile()
        Me.FileDataSet.Clear()
        Me.FileDataSet.AcceptChanges()

        Me.CurrentDataFile = ""
        UpdateTitleBar()
        EnableMenuCommands(False)
        Try
            m_fileStream.Close()
        Catch ex As Exception
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

    Private Sub mnuClose_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles mnuClose.Click
        'Check for a file already open.  If there is one and it has unsaved changes, prompt the user
        'to save the changes.
        If DataFileAlreadyOpen() = True Then
            If Me.FileDataSet.HasChanges Then
                Select Case MessageBox.Show("Save changes to the data file " & Me.CurrentDataFile & "?", "Save Changes", MessageBoxButtons.YesNoCancel)
                    Case Windows.Forms.DialogResult.Yes
                        SaveDataFile()
                        UnloadDataFile()
                    Case Windows.Forms.DialogResult.No
                        UnloadDataFile()
                    Case Windows.Forms.DialogResult.Cancel
                        Exit Sub
                End Select
            Else
                UnloadDataFile()
            End If
        End If
    End Sub

    Private Sub mnuSave_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles mnuSave.Click
        SaveDataFile()
    End Sub

    Private Sub mnuSaveAs_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles mnuSaveAs.Click
        dlgSaveFile.InitialDirectory = Me.DefaultDataDirectory
        dlgSaveFile.FileName = ""

        If dlgSaveFile.ShowDialog <> Windows.Forms.DialogResult.OK Then
            dlgSaveFile.Dispose()
            Exit Sub
        End If

        Dim strNewFileName As String = dlgSaveFile.FileName
        Me.DefaultDataDirectory = System.IO.Path.GetDirectoryName(dlgSaveFile.FileName)

        Me.CurrentDataFile = strNewFileName
        SaveDataFile()
        dlgSaveFile.Dispose()
        UpdateTitleBar()
    End Sub

    Private Sub mnuNew_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles mnuNew.Click
        dlgSaveFile.InitialDirectory = Me.DefaultDataDirectory
        dlgSaveFile.FileName = Format(DateTime.Today, "yyyy_MM_dd")
     
        If dlgSaveFile.ShowDialog <> Windows.Forms.DialogResult.OK Then
            dlgSaveFile.Dispose()
            Exit Sub
        End If

        'Reset the default data directory.
        Me.DefaultDataDirectory = System.IO.Path.GetDirectoryName(dlgSaveFile.FileName)
        SaveGlobalFile()

        'Check for a file already open.  If there is one and it has unsaved changes, prompt the user
        'to save the changes.
        If DataFileAlreadyOpen() = True Then
            If Me.FileDataSet.HasChanges Then
                Select Case MessageBox.Show("Save changes to the data file " & Me.CurrentDataFile & "?", "Save Changes", MessageBoxButtons.YesNoCancel)
                    Case Windows.Forms.DialogResult.Yes
                        SaveDataFile()
                        UnloadDataFile()
                    Case Windows.Forms.DialogResult.No
                        UnloadDataFile()
                    Case Windows.Forms.DialogResult.Cancel
                        Exit Sub
                End Select
            Else
                UnloadDataFile()
            End If
        End If

        Me.FileDataSet.Servers.Merge(Me.GlobalDataSet.Servers)

        Try
            Dim objFileEncoder As New clsFileEncoder
            Dim objDSStream As New MemoryStream

            Me.FileDataSet.WriteXml(objDSStream)

            objDSStream.Flush()
            objDSStream.Seek(0, SeekOrigin.Begin)

            objFileEncoder.EncodeFile(dlgSaveFile.FileName, objDSStream)

            objFileEncoder.Dispose()

            objDSStream.Close()
            objDSStream.Dispose()
        Catch ex As Exception
            MessageBox.Show("Could not create file.  Contact support.", "Error Creating File", MessageBoxButtons.OK)
            Exit Sub
        End Try

        Me.FileDataSet.Clear()
        Me.FileDataSet.AcceptChanges()

        LoadDataFile(dlgSaveFile.FileName)
        dlgSaveFile.Dispose()
    End Sub

    Private Sub mnuOpen_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles mnuOpen.Click
        dlgOpenFile.InitialDirectory = Me.DefaultDataDirectory
        dlgOpenFile.FileName = ""

        If dlgOpenFile.ShowDialog <> Windows.Forms.DialogResult.OK Then
            dlgOpenFile.Dispose()
            Exit Sub
        End If

        'Check for a file already open.  If there is one and it has unsaved changes, prompt the user
        'to save the changes.
        If DataFileAlreadyOpen() = True Then
            If Me.FileDataSet.HasChanges Then
                Select Case MessageBox.Show("Save changes to the data file " & Me.CurrentDataFile & "?", "Save Changes", MessageBoxButtons.YesNoCancel)
                    Case Windows.Forms.DialogResult.Yes
                        SaveDataFile()
                        UnloadDataFile()
                    Case Windows.Forms.DialogResult.No
                        UnloadDataFile()
                    Case Windows.Forms.DialogResult.Cancel
                        Exit Sub
                End Select
            Else
                UnloadDataFile()
            End If
        End If

        Dim strFile As String = dlgOpenFile.FileName
        Me.DefaultDataDirectory = System.IO.Path.GetDirectoryName(dlgOpenFile.FileName)

        dlgOpenFile.Dispose()

        LoadDataFile(strFile)
    End Sub

    Private Sub mnuManageUsers_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles mnuManageUsers.Click
        MessageBox.Show("User logins and user management have been disabled in this release.", "Command Unavailable", MessageBoxButtons.OK)

        'frmManageUsers.ShowDialog()
        'frmManageUsers.Dispose()
    End Sub

    'Special function operations begin below:
    Private Sub AddSpecialFunctionTip()
        If txtSFServerNumber.Text = "" Then
            MessageBox.Show("You must enter a server number.", "Invalid Entry", MessageBoxButtons.OK)
            txtSFServerNumber.Focus()
            Exit Sub
        End If

        If txtSFAmount.Text = "" Then
            MessageBox.Show("You must enter a tip amount.", "Invalid Entry", MessageBoxButtons.OK)
            txtSFAmount.Focus()
            Exit Sub
        End If

        If cboSelectSpecialFunction.SelectedIndex = -1 Then
            MessageBox.Show("You must select a special function.", "Invalid Selection", MessageBoxButtons.OK)
            cboSelectSpecialFunction.Focus()
            Exit Sub
        End If

        AutoInsertSFDecimal()

        Dim drNewRow As DataRow = Me.FileDataSet.Tips.NewRow

        drNewRow("Amount") = txtSFAmount.Text
        drNewRow("ServerNumber") = txtSFServerNumber.Text
        drNewRow("FirstName") = Me.FileDataSet.Servers.FindByServerNumber(txtSFServerNumber.Text)("FirstName").ToString
        drNewRow("LastName") = Me.FileDataSet.Servers.FindByServerNumber(txtSFServerNumber.Text)("LastName").ToString
        drNewRow("Description") = "Special Function"
        drNewRow("SpecialFunction") = cboSelectSpecialFunction.SelectedValue
        drNewRow("WorkingDate") = CDate(Me.FileDataSet.SpecialFunctions.FindBySpecialFunction(cboSelectSpecialFunction.SelectedValue.ToString)("Date"))

        Me.FileDataSet.Tips.Rows.Add(drNewRow)
        UpdateSFTotals()

        txtSFAmount.Clear()
        txtSFServerNumber.Clear()
        txtSFServerName.Clear()
        txtSFServerNumber.Focus()
    End Sub

    Private Sub UpdateSFTotals()
        Dim decAmount As Decimal = 0

        For Each row As DataGridViewRow In Me.SpecialFunctionDataGridView.Rows
            decAmount += CDec(row.Cells.Item("SFAmount").Value)
        Next

        lblSFTotal.Text = "Total: " & Format(decAmount, "c")
    End Sub

    Private Sub txtSFServerNumber_KeyPress(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyPressEventArgs) Handles txtSFServerNumber.KeyPress
        If e.KeyChar = ChrW(13) Then
            e.Handled = True
            txtSFAmount.Focus()
        End If
    End Sub

    Private Sub txtSFAmount_KeyPress(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyPressEventArgs) Handles txtSFAmount.KeyPress
        If e.KeyChar = ChrW(13) Then
            e.Handled = True
            AddSpecialFunctionTip()
        End If
    End Sub

    Private Sub txtSFServerNumber_LostFocus(ByVal sender As Object, ByVal e As System.EventArgs) Handles txtSFServerNumber.LostFocus
        If txtSFServerNumber.Text = "" Then Exit Sub

        If Not (Me.FileDataSet.Servers.FindByServerNumber(txtSFServerNumber.Text) Is Nothing) Then
            Dim strFirstName As String = Me.FileDataSet.Servers.FindByServerNumber(txtSFServerNumber.Text)("FirstName").ToString
            Dim strLastName As String = Me.FileDataSet.Servers.FindByServerNumber(txtSFServerNumber.Text)("LastName").ToString

            txtSFServerName.Text = strFirstName & " " & strLastName
        Else
            MessageBox.Show("The server number you entered was not found in the data table.", "Server Not Found", MessageBoxButtons.OK)
            txtSFServerNumber.Clear()
            txtSFAmount.Clear()
            txtSFServerNumber.Focus()
            Exit Sub
        End If
    End Sub

    Private Sub txtSFAmount_LostFocus(ByVal sender As Object, ByVal e As System.EventArgs) Handles txtSFAmount.LostFocus
        AutoInsertSFDecimal()
    End Sub

    Private Sub AutoInsertSFDecimal()
        If txtSFAmount.Text = "" Then Exit Sub

        If Not IsNumeric(txtSFAmount.Text) Then
            MessageBox.Show("The tip amount must be numeric.", "Invalid Entry", MessageBoxButtons.OK)
            txtSFAmount.Clear()
            txtSFAmount.Focus()
            Exit Sub
        End If

        Dim decAmount As Decimal

        For Each c As Char In txtSFAmount.Text
            If c = "." Then
                decAmount = CDec(txtSFAmount.Text)
                txtSFAmount.Text = Format(decAmount, "0.00")
                Exit Sub
            End If
        Next

        decAmount = CDec(txtSFAmount.Text)
        decAmount = decAmount / 100
        txtSFAmount.Text = Format(decAmount, "0.00")
    End Sub

    Private Sub btnAddSF_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnAddSF.Click
        AddSpecialFunctionTip()
    End Sub

    Private Sub btnClearSF_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnClearSF.Click
        txtSFAmount.Clear()
        txtSFServerNumber.Clear()
        txtSFServerName.Clear()
        txtSFServerNumber.Focus()
    End Sub

    Private Sub mnuDeleteSFTip_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles mnuDeleteSFTip.Click
        If Me.SpecialFunctionDataGridView.Rows.Count = 0 Then Exit Sub

        Dim intTipID As Integer = CInt(Me.SpecialFunctionDataGridView.Item("SFID", Me.SpecialFunctionTipsBindingSource.Position).Value)

        Dim strTipAmount As String = Me.FileDataSet.Tips.FindByTipID(intTipID)("Amount").ToString
        Dim strFirstName As String = Me.FileDataSet.Tips.FindByTipID(intTipID)("FirstName").ToString
        Dim strLastName As String = Me.FileDataSet.Tips.FindByTipID(intTipID)("LastName").ToString

        If MessageBox.Show("Are you sure you want to delete this $" & strTipAmount & " tip for " & _
        strFirstName & " " & strLastName & "?", "Confirm Delete", MessageBoxButtons.YesNo) <> Windows.Forms.DialogResult.Yes Then
            Exit Sub
        End If

        Me.FileDataSet.Tips.FindByTipID(intTipID).Delete()
        UpdateSFTotals()
    End Sub

    Private Sub txtSFServerName_GotFocus(ByVal sender As Object, ByVal e As System.EventArgs) Handles txtSFServerName.GotFocus
        txtSFServerNumber.Focus()
    End Sub

    Private Sub mnuManageSpecialFunctions_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles mnuManageSpecialFunctions.Click
        With frmManageSpecialFunctions
            .ShowDialog()
            .Dispose()
        End With

        Me.SpecialFunctionTipsBindingSource.Filter = ""
        Me.cboSelectSpecialFunction.SelectedIndex = -1
        UpdateSFTotals()
    End Sub

    Private Sub cboSelectSpecialFunction_SelectedIndexChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles cboSelectSpecialFunction.SelectedIndexChanged
        If cboSelectSpecialFunction.SelectedIndex = -1 Then Exit Sub

        Dim strSpecialFunction As String = cboSelectSpecialFunction.SelectedValue.ToString

        Me.SpecialFunctionTipsBindingSource.Filter = "SpecialFunction = '" & strSpecialFunction & "'"
        UpdateSFTotals()
    End Sub

    Private Sub mnuShowAllTips_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles mnuShowAllTips.Click
        Me.SpecialFunctionTipsBindingSource.Filter = ""
        Me.cboSelectSpecialFunction.SelectedIndex = -1
        UpdateSFTotals()
    End Sub

    Private Sub mnuAbout_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles mnuAbout.Click
        frmAboutTipTrackerSF.ShowDialog()
        frmAboutTipTrackerSF.Dispose()
    End Sub

    Private Sub mnuAddServer_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles mnuAddServer.Click
        If Me.CurrentDataFile = "" Then Exit Sub

        Dim blnErrorState As Boolean = True

        frmAddEditServer.Text = "Add Server"

        While blnErrorState = True
            If frmAddEditServer.ShowDialog <> Windows.Forms.DialogResult.OK Then
                frmAddEditServer.Dispose()
                Exit Sub
            End If

            If Not (Me.FileDataSet.Servers.FindByServerNumber(frmAddEditServer.ServerNumber) Is Nothing) Then
                MessageBox.Show("The server number you entered already exists in the data file.  Please enter a different number.", "Invalid Entry", MessageBoxButtons.OK)
                frmAddEditServer.ServerNumber = ""
            Else
                blnErrorState = False
            End If
        End While

        Dim drNewRow As DataRow = Me.FileDataSet.Servers.NewRow

        drNewRow("ServerNumber") = frmAddEditServer.ServerNumber
        drNewRow("FirstName") = frmAddEditServer.FirstName
        drNewRow("LastName") = frmAddEditServer.LastName

        Me.FileDataSet.Servers.Rows.Add(drNewRow)

        If Me.GlobalDataSet.Servers.FindByServerNumber(frmAddEditServer.ServerNumber) Is Nothing Then
            If MessageBox.Show("This server does not exist in the servers template.  Add the server to the template?", "Add Server", MessageBoxButtons.YesNo) <> Windows.Forms.DialogResult.Yes Then
                frmAddEditServer.Dispose()
                Exit Sub
            Else
                drNewRow = Me.GlobalDataSet.Servers.NewRow

                drNewRow("ServerNumber") = frmAddEditServer.ServerNumber
                drNewRow("FirstName") = frmAddEditServer.FirstName
                drNewRow("LastName") = frmAddEditServer.LastName

                Me.GlobalDataSet.Servers.Rows.Add(drNewRow)
                SaveGlobalFile()
            End If
        End If

        frmAddEditServer.Dispose()
    End Sub

    Private Sub mnuEditSelectedServer_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles mnuEditSelectedServer.Click
        If Me.ServersDataGridView.Rows.Count = 0 Then Exit Sub

        Dim strServerNumber As String = Me.ServersDataGridView.Item("ServersServerNumber", Me.ServersBindingSource.Position).Value.ToString
        Dim strFirstName As String = Me.FileDataSet.Servers.FindByServerNumber(strServerNumber)("FirstName").ToString
        Dim strLastName As String = Me.FileDataSet.Servers.FindByServerNumber(strServerNumber)("LastName").ToString

        With frmAddEditServer
            .Text = "Edit Server"
            .txtServerNumber.ReadOnly = True
            .ServerNumber = strServerNumber
            .FirstName = strFirstName
            .LastName = strLastName

            If .ShowDialog <> Windows.Forms.DialogResult.OK Then
                .Dispose()
                Exit Sub
            End If

            If .ServerNumber = strServerNumber And .FirstName = strFirstName And .LastName = strLastName Then
                .Dispose()
                Exit Sub
            End If

            Me.FileDataSet.Servers.FindByServerNumber(strServerNumber)("FirstName") = .FirstName
            Me.FileDataSet.Servers.FindByServerNumber(strServerNumber)("LastName") = .LastName

            .Dispose()
        End With
    End Sub

    Private Sub mnuReportByServer_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles mnuReportByServer.Click
        frmReportByServer.ShowDialog()
        frmReportByServer.Dispose()
    End Sub

    Private Sub mnuShowTipsForServer_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles mnuShowTipsForServer.Click
        If Me.ServersDataGridView.Rows.Count = 0 Then Exit Sub

        Dim strServerNumber As String = Me.ServersDataGridView.Item("ServersServerNumber", Me.ServersBindingSource.Position).Value.ToString

        Me.SpecialFunctionTipsBindingSource.Filter = "ServerNumber = '" & strServerNumber & "'"
        Me.cboSelectSpecialFunction.SelectedIndex = -1
        UpdateSFTotals()
    End Sub

    Private Sub mnuReportByFunction_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles mnuReportByFunction.Click
        frmReportByFunction.ShowDialog()
        frmReportByFunction.Dispose()
    End Sub

    Private Sub mnuImportServerList_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles mnuImportServerList.Click
        If MessageBox.Show("This function will add the imported servers to the template.  Continue?", "Confirm Import", MessageBoxButtons.YesNo) <> Windows.Forms.DialogResult.Yes Then
            Exit Sub
        End If

        Dim dsTempDS As New GlobalDataSet
        Dim strFileName As String

        With dlgOpenFile
            .Title = "Import Server List"
            .InitialDirectory = My.Computer.FileSystem.SpecialDirectories.Desktop
            .RestoreDirectory = True
            .Filter = "XML Files (*.xml)|*.xml"
            .DefaultExt = "*.xml"

            If .ShowDialog <> Windows.Forms.DialogResult.OK Then
                .Dispose()
                Exit Sub
            End If

            strFileName = .FileName

            .Dispose()
        End With

        'Try reading the file.
        Try
            dsTempDS.ReadXml(strFileName)
        Catch ex As ConstraintException
            MessageBox.Show("The selected file contains data that cannot be loaded.", "Constraint Exception", MessageBoxButtons.OK)
            GoTo CleanUp
        Catch ex As Exception
            MessageBox.Show("Unable to read the requested file.", "General Exception", MessageBoxButtons.OK)
            GoTo CleanUp
        End Try

        'Check that there are servers in the dataset.
        If dsTempDS.Servers.Rows.Count = 0 Then
            MessageBox.Show("There are no servers in the file to import.", "No Servers", MessageBoxButtons.OK)
            GoTo CleanUp
        End If

        For Each row As DataRow In dsTempDS.Servers.Rows
            row.RowError = "NotFound"
        Next

        'Check each server to see if they can be found.
        For Each row As DataRow In dsTempDS.Servers.Rows
            Dim strServerNumber As String = row("ServerNumber").ToString
            Dim strFirstName As String = row("FirstName").ToString
            Dim strLastName As String = row("LastName").ToString

            'Check for an existing server number.  If one is found and the first name or last name
            'don't match, prompt the user to enter a new number so the server can be added.
            If Not (Me.GlobalDataSet.Servers.FindByServerNumber(strServerNumber) Is Nothing) Then
                If strFirstName <> Me.GlobalDataSet.Servers.FindByServerNumber(strServerNumber)("FirstName").ToString Or _
                   strLastName <> Me.GlobalDataSet.Servers.FindByServerNumber(strServerNumber)("LastName").ToString Then
                    Dim blnError As Boolean = True

                    'Create an error state and set it to true.  Don't exit the loop until the user has entered
                    'a valid server number (i.e. one that isn't already in use).
                    While blnError = True
                        With frmAddEditServer
                            .Text = "Add Imported Server"
                            .FirstName = strFirstName
                            .LastName = strLastName

                            If .ShowDialog <> Windows.Forms.DialogResult.OK Then
                                GoTo CleanUp
                            End If

                            If Not (Me.GlobalDataSet.Servers.FindByServerNumber(.ServerNumber) Is Nothing) Then
                                MessageBox.Show("The server number you entered is already in use.  Please enter a different number.", "Duplicate Entry", MessageBoxButtons.OK)
                                .Dispose()
                            Else
                                row("ServerNumber") = .ServerNumber
                                .Dispose()
                                blnError = False
                            End If
                        End With
                    End While
                End If
            End If
        Next

        Me.GlobalDataSet.Servers.Merge(dsTempDS.Servers)

        MessageBox.Show("Import complete.  Please check the servers template for duplicates.", "Import Complete", MessageBoxButtons.OK, MessageBoxIcon.Information)
        frmManageServers.ShowDialog()
        frmManageServers.Dispose()

        Me.GlobalDataSet.AcceptChanges()
        SaveGlobalFile()

CleanUp:

        dsTempDS = Nothing
    End Sub

End Class