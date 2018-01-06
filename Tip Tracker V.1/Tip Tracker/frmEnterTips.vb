Imports System.IO

Public Class frmEnterTips
    Private m_dataFileStream As FileStream
    Private m_strCurrentFile As String

    Friend Property CurrentFile() As String
        Get
            Return m_strCurrentFile
        End Get
        Set(ByVal value As String)
            m_strCurrentFile = value
        End Set
    End Property

    Private Sub frmEnterTips_FormClosing(ByVal sender As Object, ByVal e As System.Windows.Forms.FormClosingEventArgs) Handles Me.FormClosing
        'Check for changes in the dataset then prompt the user to save if there are changes.
        If Me.FileDataSet.HasChanges Then
            Select Case MessageBox.Show("Save changes to the file " & Me.Text & "?", "Save Changes", MessageBoxButtons.YesNoCancel)
                Case Windows.Forms.DialogResult.Yes
                    SaveData()
                Case Windows.Forms.DialogResult.No
                    Me.FileDataSet.Clear()
                    Me.FileDataSet.AcceptChanges()
                Case Windows.Forms.DialogResult.Cancel
                    e.Cancel = True
                    Exit Sub
            End Select
        End If

        'Perform cleanup
        Try
            m_dataFileStream.Close()
            m_dataFileStream.Dispose()
        Catch ex As Exception

        End Try

        'Check to see if this is the last data file that is open.  If it is call LastFileClosing()
        'to disable the menu commands that require a file to be open.
        If frmMain.NumberOfFilesOpen = 1 Then
            frmMain.LastFileClosing()
        End If
    End Sub

    Private Sub frmEnterTips_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
        'Set the form as an mdi child of frmMain.
        Me.MdiParent = frmMain
        Me.lblSystemDate.Text = "System Date: " & Format(DateTime.Today, "M/d/yyyy")
        Me.WindowState = FormWindowState.Maximized

        'Make the tip ID columns invisible.
        Me.CCID.Visible = False
        Me.RCID.Visible = False
        Me.CAID.Visible = False
        Me.SFID.Visible = False

        'Set the servers binding source sort mode.
        Me.ServersBindingSource.Sort = "ServerNumber"

        'Extract the file name then change the text of the form to show the file name.
        Dim intBegin As Integer = 0
        Dim intEnd As Integer = 0

        For i As Integer = Len(Me.CurrentFile) To 1 Step -1
            Dim c As Char = GetChar(Me.CurrentFile, i)
            If c = "." Then
                intEnd = i
            End If
            If c = "\" Then
                intBegin = i
                Exit For
            End If
        Next

        If intBegin <> 1 And intEnd <> 1 And intBegin <> 0 And intEnd <> 0 And intBegin < intEnd Then
            Dim strFileName As String = Mid(Me.CurrentFile, intBegin + 1, intEnd - intBegin - 1)
            Me.Text = strFileName
        Else
            Me.Text = "######"
        End If

        'Call the subroutine to decode the xml file and read it into the dataset.
        If LoadData() = False Then
            Me.Close()
        End If

        'Update the date labels to show the dates from the settings table.
        UpdateDateLabels()

        'Set the tips binding source sort and filter modes.
        SetSelectionFilters()
        cboSelectSpecialFunction.SelectedIndex = -1

        'TODO: Check user authorizations and disable buttons/menus that user shouldn't access.


    End Sub

    Private Function LoadData() As Boolean
        Try
            Dim objFileDecoder As New clsFileEncoder
            Dim objDSStream As New MemoryStream

            objDSStream = objFileDecoder.DecodeFile(Me.CurrentFile)

            objDSStream.Flush()
            objDSStream.Seek(0, SeekOrigin.Begin)

            Me.FileDataSet.ReadXml(objDSStream)

            objFileDecoder.Dispose()
            objDSStream.Close()
            objDSStream.Dispose()

            Me.FileDataSet.AcceptChanges()

            'Hook the file so that another process cannot change it while it is open.
            m_dataFileStream = File.Open(Me.CurrentFile, FileMode.Open)

            Return True

        Catch ex As Exception
            MessageBox.Show("Could not load the requsted data file.  Contact support.", "Error Loading File", MessageBoxButtons.OK)
            Return False
        End Try
    End Function

    Friend Sub SaveData()
        Try
            m_dataFileStream.Close()

            Dim objFileEncoder As New clsFileEncoder
            Dim objDSStream As New MemoryStream

            Me.FileDataSet.WriteXml(objDSStream)

            objFileEncoder.EncodeFile(Me.CurrentFile, objDSStream)

            objFileEncoder.Dispose()
            objDSStream.Close()
            objDSStream.Dispose()
            Me.FileDataSet.AcceptChanges()
        Catch ex As Exception
            MessageBox.Show("Could not save file.  Contact support.", "Error Saving File", MessageBoxButtons.OK)
        End Try

        Try
            m_dataFileStream = File.Open(Me.CurrentFile, FileMode.Open)
        Catch ex As Exception
            MessageBox.Show("Could not reopen filestream.  Contact support.", "Technical Error", MessageBoxButtons.OK)
        End Try
    End Sub

    Friend Sub SaveDataAs(ByVal NewFileName As String)
        'Save dataset under a different file name.
        Try
            m_dataFileStream.Close()

            Dim objFileEncoder As New clsFileEncoder
            Dim objDSStream As New MemoryStream

            Me.FileDataSet.WriteXml(objDSStream)

            objFileEncoder.EncodeFile(NewFileName, objDSStream)

            objFileEncoder.Dispose()
            objDSStream.Close()
            objDSStream.Dispose()
            Me.FileDataSet.AcceptChanges()

            Me.CurrentFile = NewFileName

            Dim intBegin As Integer = 0
            Dim intEnd As Integer = 0

            For i As Integer = Len(Me.CurrentFile) To 1 Step -1
                Dim c As Char = GetChar(Me.CurrentFile, i)
                If c = "." Then
                    intEnd = i
                End If
                If c = "\" Then
                    intBegin = i
                    Exit For
                End If
            Next

            If intBegin <> 1 And intEnd <> 1 And intBegin <> 0 And intEnd <> 0 And intBegin < intEnd Then
                Dim strFileName As String = Mid(Me.CurrentFile, intBegin + 1, intEnd - intBegin - 1)
                Me.Text = strFileName
            Else
                Me.Text = "######"
            End If

        Catch ex As Exception
            MessageBox.Show("Could not save file.  Contact support.", "Error Saving File", MessageBoxButtons.OK)
            Exit Sub
        End Try

        Try
            m_dataFileStream = File.Open(NewFileName, FileMode.Open)
        Catch ex As Exception
            MessageBox.Show("Could not reopen filestream.  Contact support.", "Technical Error", MessageBoxButtons.OK)
        End Try
    End Sub

    Private Sub UpdateDateLabels()
        Dim dtePeriodStart As Date = CDate(Me.FileDataSet.Settings.FindBySetting("PeriodStart")("Value"))
        Dim dtePeriodEnd As Date = CDate(Me.FileDataSet.Settings.FindBySetting("PeriodEnd")("Value"))
        Dim dteWorkingDate As Date = CDate(Me.FileDataSet.Settings.FindBySetting("WorkingDate")("Value"))

        Me.lblPeriodStart.Text = "Period Start: " & Format(dtePeriodStart, "M/d/yyyy")
        Me.lblPeriodEnd.Text = "Period End: " & Format(dtePeriodEnd, "M/d/yyyy")
        Me.lblWorkingDate.Text = "Working Date: " & Format(dteWorkingDate, "M/d/yyyy")
    End Sub

    Private Sub SetSelectionFilters()
        Dim strWorkingDate As String = Format(CDate(Me.FileDataSet.Settings.FindBySetting("WorkingDate")("Value")), "M/d/yyyy")
        Me.CreditCardTipsBindingSource.Filter = "Description = 'Credit Card' AND WorkingDate = '" & strWorkingDate & "'"
        Me.CreditCardTipsBindingSource.Sort = "ServerNumber"

        Me.RoomChargeTipsBindingSource.Filter = "Description = 'Room Charge' AND WorkingDate = '" & strWorkingDate & "'"
        Me.RoomChargeTipsBindingSource.Sort = "ServerNumber"

        Me.CashTipsBindingSource.Filter = "Description = 'Cash'"
        Me.CashTipsBindingSource.Sort = "ServerNumber"

        Me.SpecialFunctionTipsBindingSource.Filter = "Description = 'Special Function'"
        Me.SpecialFunctionTipsBindingSource.Sort = "SpecialFunction, LastName"

        Me.SpecialFunctionBindingSource.Sort = "SpecialFunction"

        UpdateCCTotals()
        UpdateRCTotals()
        UpdateCATotals()
    End Sub

    Private Sub tabTipsTabControl_SelectedIndexChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles tabTipsTabControl.SelectedIndexChanged
        Dim strTipType As String = Me.tabTipsTabControl.SelectedTab.Text

        lblCurrentTipType.Text = "Editing " & strTipType & " Tips"

        'Make the tip ID columns invisible.
        Me.CCID.Visible = False
        Me.RCID.Visible = False
        Me.CAID.Visible = False
        Me.SFID.Visible = False
    End Sub

    Private Sub btnFinalize_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnFinalize.Click
        Dim dteWorkingDate As Date = CDate(Me.FileDataSet.Settings.FindBySetting("WorkingDate")("Value"))
        Dim dtePeriodEnd As Date = CDate(Me.FileDataSet.Settings.FindBySetting("PeriodEnd")("Value"))

        If dteWorkingDate = dtePeriodEnd Then
            MessageBox.Show("The current working date is the last day in the pay period.  You cannot " & _
            "advance the working date any further.  To work on tips for " & _
            Format(DateAdd(DateInterval.Day, 1, dteWorkingDate), "M/d/yyyy") & " you must start a new file " & _
            "for the new pay period.", "Cannot Change Working Date", MessageBoxButtons.OK)
            Exit Sub
        End If

        If MessageBox.Show("The working date will be changed to " & Format(DateAdd(DateInterval.Day, 1, dteWorkingDate), "M/d/yyyy") & _
        ".  Do you wish to continue?", "Confirm Date Change", MessageBoxButtons.YesNo) <> Windows.Forms.DialogResult.Yes Then
            Exit Sub
        End If

        Me.FileDataSet.Settings.FindBySetting("WorkingDate")("Value") = DateAdd(DateInterval.Day, 1, dteWorkingDate)
        UpdateDateLabels()
        SetSelectionFilters()
    End Sub

    Private Sub btnSelectWorkingDate_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnSelectWorkingDate.Click
        Dim dtePeriodStart As Date = CDate(Me.FileDataSet.Settings.FindBySetting("PeriodStart")("Value"))
        Dim dtePeriodEnd As Date = CDate(Me.FileDataSet.Settings.FindBySetting("PeriodEnd")("Value"))
        Dim dteWorkingDate As Date = CDate(Me.FileDataSet.Settings.FindBySetting("WorkingDate")("Value"))

        With frmSelectDate
            .MinDate = dtePeriodStart
            .MaxDate = dtePeriodEnd
            .CurrentDate = dteWorkingDate

            If .ShowDialog <> Windows.Forms.DialogResult.OK Then
                .Dispose()
                Exit Sub
            End If

            If .SelectedDate = dteWorkingDate Then
                .Dispose()
                Exit Sub
            End If

            Me.FileDataSet.Settings.FindBySetting("WorkingDate")("Value") = .SelectedDate

            .Dispose()
        End With

        UpdateDateLabels()
        SetSelectionFilters()
    End Sub

    'Credit card operations begin below:
    Private Sub AddCreditCardTip()
        If txtCCServerNumber.Text = "" Then
            MessageBox.Show("You must enter a server number.", "Invalid Entry", MessageBoxButtons.OK)
            txtCCServerNumber.Focus()
            Exit Sub
        End If

        If txtCCAmount.Text = "" Then
            MessageBox.Show("You must enter a tip amount.", "Invalid Entry", MessageBoxButtons.OK)
            txtCCAmount.Focus()
            Exit Sub
        End If

        AutoInsertCCDecimal()

        Dim drNewRow As DataRow = Me.FileDataSet.Tips.NewRow

        drNewRow("Amount") = txtCCAmount.Text
        drNewRow("ServerNumber") = txtCCServerNumber.Text
        drNewRow("FirstName") = Me.FileDataSet.Servers.FindByServerNumber(txtCCServerNumber.Text)("FirstName").ToString
        drNewRow("LastName") = Me.FileDataSet.Servers.FindByServerNumber(txtCCServerNumber.Text)("LastName").ToString
        drNewRow("Description") = "Credit Card"
        drNewRow("WorkingDate") = CDate(Me.FileDataSet.Settings.FindBySetting("WorkingDate")("Value"))

        Me.FileDataSet.Tips.Rows.Add(drNewRow)
        UpdateCCTotals()

        txtCCAmount.Clear()
        txtCCServerNumber.Clear()
        txtCCServerName.Clear()
        txtCCServerNumber.Focus()
    End Sub

    Private Sub UpdateCCTotals()
        Dim decAmount As Decimal = 0

        For Each row As DataGridViewRow In Me.CreditCardDataGridView.Rows
            decAmount += CDec(row.Cells.Item("CCAmount").Value)
        Next

        lblCCTotal.Text = "Total: " & Format(decAmount, "c")
    End Sub

    Private Sub txtCCServerNumber_KeyPress(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyPressEventArgs) Handles txtCCServerNumber.KeyPress
        If e.KeyChar = ChrW(13) Then
            e.Handled = True
            txtCCAmount.Focus()
        End If
    End Sub

    Private Sub txtCCAmount_KeyPress(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyPressEventArgs) Handles txtCCAmount.KeyPress
        If e.KeyChar = ChrW(13) Then
            e.Handled = True
            AddCreditCardTip()
        End If
    End Sub

    Private Sub txtCCServerNumber_LostFocus(ByVal sender As Object, ByVal e As System.EventArgs) Handles txtCCServerNumber.LostFocus
        If txtCCServerNumber.Text = "" Then Exit Sub

        If Not (Me.FileDataSet.Servers.FindByServerNumber(txtCCServerNumber.Text) Is Nothing) Then
            Dim strFirstName As String = Me.FileDataSet.Servers.FindByServerNumber(txtCCServerNumber.Text)("FirstName").ToString
            Dim strLastName As String = Me.FileDataSet.Servers.FindByServerNumber(txtCCServerNumber.Text)("LastName").ToString

            txtCCServerName.Text = strFirstName & " " & strLastName
        Else
            MessageBox.Show("The server number you entered was not found in the data table.", "Server Not Found", MessageBoxButtons.OK)
            txtCCServerNumber.Clear()
            txtCCAmount.Clear()
            txtCCServerNumber.Focus()
            Exit Sub
        End If
    End Sub

    Private Sub txtCCAmount_LostFocus(ByVal sender As Object, ByVal e As System.EventArgs) Handles txtCCAmount.LostFocus
        AutoInsertCCDecimal()
    End Sub

    Private Sub AutoInsertCCDecimal()
        If txtCCAmount.Text = "" Then Exit Sub

        If Not IsNumeric(txtCCAmount.Text) Then
            MessageBox.Show("The tip amount must be numeric.", "Invalid Entry", MessageBoxButtons.OK)
            txtCCAmount.Clear()
            txtCCAmount.Focus()
            Exit Sub
        End If

        Dim decAmount As Decimal

        For Each c As Char In txtCCAmount.Text
            If c = "." Then
                decAmount = CDec(txtCCAmount.Text)
                txtCCAmount.Text = Format(decAmount, "0.00")
                Exit Sub
            End If
        Next

        decAmount = CDec(txtCCAmount.Text)
        decAmount = decAmount / 100
        txtCCAmount.Text = Format(decAmount, "0.00")
    End Sub

    Private Sub btnAddCC_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnAddCC.Click
        AddCreditCardTip()
    End Sub

    Private Sub btnClearCC_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnClearCC.Click
        txtCCAmount.Clear()
        txtCCServerNumber.Clear()
        txtCCServerName.Clear()
        txtCCServerNumber.Focus()
    End Sub

    Private Sub mnuDeleteCCTip_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles mnuDeleteCCTip.Click
        If Me.CreditCardDataGridView.Rows.Count = 0 Then Exit Sub

        Dim intTipID As Integer = CInt(Me.CreditCardDataGridView.Item("CCID", Me.CreditCardTipsBindingSource.Position).Value)

        Dim strTipAmount As String = Me.FileDataSet.Tips.FindByTipID(intTipID)("Amount").ToString
        Dim strFirstName As String = Me.FileDataSet.Tips.FindByTipID(intTipID)("FirstName").ToString
        Dim strLastName As String = Me.FileDataSet.Tips.FindByTipID(intTipID)("LastName").ToString

        If MessageBox.Show("Are you sure you want to delete this $" & strTipAmount & " tip for " & _
        strFirstName & " " & strLastName & "?", "Confirm Delete", MessageBoxButtons.YesNo) <> Windows.Forms.DialogResult.Yes Then
            Exit Sub
        End If

        Me.FileDataSet.Tips.FindByTipID(intTipID).Delete()
        UpdateCCTotals()
    End Sub

    Private Sub txtCCServerName_GotFocus(ByVal sender As Object, ByVal e As System.EventArgs) Handles txtCCServerName.GotFocus
        txtCCServerNumber.Focus()
    End Sub

    Private Sub mnuReassignCCTip_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles mnuReassignCCTip.Click
        If Me.CreditCardDataGridView.Rows.Count = 0 Then Exit Sub

        frmSelectServer.m_dsParentDataSet = Me.FileDataSet
        frmSelectServer.lblSelectServer.Text = "Select the tip recipient:"

        If frmSelectServer.ShowDialog <> Windows.Forms.DialogResult.OK Then
            frmSelectServer.Dispose()
            Exit Sub
        End If

        Dim strSourceServerNumber As String = Me.CreditCardDataGridView.Item("CCServerNumber", Me.CreditCardTipsBindingSource.Position).Value.ToString
        Dim intSourceTipID As Integer = CInt(Me.CreditCardDataGridView.Item("CCID", Me.CreditCardTipsBindingSource.Position).Value)

        Dim strDestServerNumber As String = frmSelectServer.ServerNumber
        Dim strDestFirstName As String = Me.FileDataSet.Servers.FindByServerNumber(strDestServerNumber)("FirstName").ToString
        Dim strDestLastName As String = Me.FileDataSet.Servers.FindByServerNumber(strDestServerNumber)("LastName").ToString

        If strSourceServerNumber = strDestServerNumber Then
            MessageBox.Show("The selected tip cannot be reassigned to its original owner.", "Invalid Selection", MessageBoxButtons.OK)
            Exit Sub
        End If

        Dim decAmount As Decimal = CDec(Me.FileDataSet.Tips.FindByTipID(intSourceTipID)("Amount"))
        Dim strDescription As String = Me.FileDataSet.Tips.FindByTipID(intSourceTipID)("Description").ToString
        Dim strSpecialFunction As String
        If Not IsDBNull(Me.FileDataSet.Tips.FindByTipID(intSourceTipID)("SpecialFunction")) Then
            strSpecialFunction = Me.FileDataSet.Tips.FindByTipID(intSourceTipID)("SpecialFunction").ToString
        End If
        Dim dteWorkingDate As Date = CDate(Me.FileDataSet.Tips.FindByTipID(intSourceTipID)("WorkingDate"))

        Dim drNewRow As DataRow = Me.FileDataSet.Tips.NewRow

        drNewRow("Amount") = decAmount
        drNewRow("ServerNumber") = strDestServerNumber
        drNewRow("FirstName") = strDestFirstName
        drNewRow("LastName") = strDestLastName
        drNewRow("Description") = strDescription
        If Not IsDBNull(Me.FileDataSet.Tips.FindByTipID(intSourceTipID)("SpecialFunction")) Then
            drNewRow("SpecialFunction") = strSpecialFunction
        End If
        drNewRow("WorkingDate") = dteWorkingDate

        Me.FileDataSet.Tips.Rows.Add(drNewRow)

        Me.FileDataSet.Tips.FindByTipID(intSourceTipID).Delete()

        frmSelectServer.Dispose()
    End Sub

    'Room charge operations begin below:
    Private Sub AddRoomChargeTip()
        If txtRCServerNumber.Text = "" Then
            MessageBox.Show("You must enter a server number.", "Invalid Entry", MessageBoxButtons.OK)
            txtRCServerNumber.Focus()
            Exit Sub
        End If

        If txtRCAmount.Text = "" Then
            MessageBox.Show("You must enter a tip amount.", "Invalid Entry", MessageBoxButtons.OK)
            txtRCAmount.Focus()
            Exit Sub
        End If

        AutoInsertRCDecimal()

        Dim drNewRow As DataRow = Me.FileDataSet.Tips.NewRow

        drNewRow("Amount") = txtRCAmount.Text
        drNewRow("ServerNumber") = txtRCServerNumber.Text
        drNewRow("FirstName") = Me.FileDataSet.Servers.FindByServerNumber(txtRCServerNumber.Text)("FirstName").ToString
        drNewRow("LastName") = Me.FileDataSet.Servers.FindByServerNumber(txtRCServerNumber.Text)("LastName").ToString
        drNewRow("Description") = "Room Charge"
        drNewRow("WorkingDate") = CDate(Me.FileDataSet.Settings.FindBySetting("WorkingDate")("Value"))

        Me.FileDataSet.Tips.Rows.Add(drNewRow)
        UpdateRCTotals()

        txtRCAmount.Clear()
        txtRCServerNumber.Clear()
        txtRCServerName.Clear()
        txtRCServerNumber.Focus()
    End Sub

    Private Sub UpdateRCTotals()
        Dim decAmount As Decimal = 0

        For Each row As DataGridViewRow In Me.RoomChargeDataGridView.Rows
            decAmount += CDec(row.Cells.Item("RCAmount").Value)
        Next

        lblRCTotal.Text = "Total: " & Format(decAmount, "c")
    End Sub

    Private Sub txtRCServerNumber_KeyPress(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyPressEventArgs) Handles txtRCServerNumber.KeyPress
        If e.KeyChar = ChrW(13) Then
            e.Handled = True
            txtRCAmount.Focus()
        End If
    End Sub

    Private Sub txtRCAmount_KeyPress(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyPressEventArgs) Handles txtRCAmount.KeyPress
        If e.KeyChar = ChrW(13) Then
            e.Handled = True
            AddRoomChargeTip()
        End If
    End Sub

    Private Sub txtRCServerNumber_LostFocus(ByVal sender As Object, ByVal e As System.EventArgs) Handles txtRCServerNumber.LostFocus
        If txtRCServerNumber.Text = "" Then Exit Sub

        If Not (Me.FileDataSet.Servers.FindByServerNumber(txtRCServerNumber.Text) Is Nothing) Then
            Dim strFirstName As String = Me.FileDataSet.Servers.FindByServerNumber(txtRCServerNumber.Text)("FirstName").ToString
            Dim strLastName As String = Me.FileDataSet.Servers.FindByServerNumber(txtRCServerNumber.Text)("LastName").ToString

            txtRCServerName.Text = strFirstName & " " & strLastName
        Else
            MessageBox.Show("The server number you entered was not found in the data table.", "Server Not Found", MessageBoxButtons.OK)
            txtRCServerNumber.Clear()
            txtRCAmount.Clear()
            txtRCServerNumber.Focus()
            Exit Sub
        End If
    End Sub

    Private Sub txtRCAmount_LostFocus(ByVal sender As Object, ByVal e As System.EventArgs) Handles txtRCAmount.LostFocus
        AutoInsertRCDecimal()
    End Sub

    Private Sub AutoInsertRCDecimal()
        If txtRCAmount.Text = "" Then Exit Sub

        If Not IsNumeric(txtRCAmount.Text) Then
            MessageBox.Show("The tip amount must be numeric.", "Invalid Entry", MessageBoxButtons.OK)
            txtRCAmount.Clear()
            txtRCAmount.Focus()
            Exit Sub
        End If

        Dim decAmount As Decimal

        For Each c As Char In txtRCAmount.Text
            If c = "." Then
                decAmount = CDec(txtRCAmount.Text)
                txtRCAmount.Text = Format(decAmount, "0.00")
                Exit Sub
            End If
        Next

        decAmount = CDec(txtRCAmount.Text)
        decAmount = decAmount / 100
        txtRCAmount.Text = Format(decAmount, "0.00")
    End Sub

    Private Sub btnAddRC_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnAddRC.Click
        AddRoomChargeTip()
    End Sub

    Private Sub btnClearRC_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnClearRC.Click
        txtRCAmount.Clear()
        txtRCServerNumber.Clear()
        txtRCServerName.Clear()
        txtRCServerNumber.Focus()
    End Sub

    Private Sub mnuDeleteRCTip_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles mnuDeleteRCTip.Click
        If Me.RoomChargeDataGridView.Rows.Count = 0 Then Exit Sub
        Dim intTipID As Integer = CInt(Me.RoomChargeDataGridView.Item("RCID", Me.RoomChargeTipsBindingSource.Position).Value)

        Dim strTipAmount As String = Me.FileDataSet.Tips.FindByTipID(intTipID)("Amount").ToString
        Dim strFirstName As String = Me.FileDataSet.Tips.FindByTipID(intTipID)("FirstName").ToString
        Dim strLastName As String = Me.FileDataSet.Tips.FindByTipID(intTipID)("LastName").ToString

        If MessageBox.Show("Are you sure you want to delete this $" & strTipAmount & " tip for " & _
        strFirstName & " " & strLastName & "?", "Confirm Delete", MessageBoxButtons.YesNo) <> Windows.Forms.DialogResult.Yes Then
            Exit Sub
        End If

        Me.FileDataSet.Tips.FindByTipID(intTipID).Delete()
        UpdateRCTotals()
    End Sub

    Private Sub txtRCServerName_GotFocus(ByVal sender As Object, ByVal e As System.EventArgs) Handles txtRCServerName.GotFocus
        txtRCServerNumber.Focus()
    End Sub

    Private Sub mnuReassignRCTip_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles mnuReassignRCTip.Click
        If Me.RoomChargeDataGridView.Rows.Count = 0 Then Exit Sub

        frmSelectServer.m_dsParentDataSet = Me.FileDataSet
        frmSelectServer.lblSelectServer.Text = "Select the tip recipient:"

        If frmSelectServer.ShowDialog <> Windows.Forms.DialogResult.OK Then
            frmSelectServer.Dispose()
            Exit Sub
        End If

        Dim strSourceServerNumber As String = Me.RoomChargeDataGridView.Item("RCServerNumber", Me.RoomChargeTipsBindingSource.Position).Value.ToString
        Dim intSourceTipID As Integer = CInt(Me.RoomChargeDataGridView.Item("RCID", Me.RoomChargeTipsBindingSource.Position).Value)

        Dim strDestServerNumber As String = frmSelectServer.ServerNumber
        Dim strDestFirstName As String = Me.FileDataSet.Servers.FindByServerNumber(strDestServerNumber)("FirstName").ToString
        Dim strDestLastName As String = Me.FileDataSet.Servers.FindByServerNumber(strDestServerNumber)("LastName").ToString

        If strSourceServerNumber = strDestServerNumber Then
            MessageBox.Show("The selected tip cannot be reassigned to its original owner.", "Invalid Selection", MessageBoxButtons.OK)
            Exit Sub
        End If

        Dim decAmount As Decimal = CDec(Me.FileDataSet.Tips.FindByTipID(intSourceTipID)("Amount"))
        Dim strDescription As String = Me.FileDataSet.Tips.FindByTipID(intSourceTipID)("Description").ToString
        Dim strSpecialFunction As String
        If Not IsDBNull(Me.FileDataSet.Tips.FindByTipID(intSourceTipID)("SpecialFunction")) Then
            strSpecialFunction = Me.FileDataSet.Tips.FindByTipID(intSourceTipID)("SpecialFunction").ToString
        End If
        Dim dteWorkingDate As Date = CDate(Me.FileDataSet.Tips.FindByTipID(intSourceTipID)("WorkingDate"))

        Dim drNewRow As DataRow = Me.FileDataSet.Tips.NewRow

        drNewRow("Amount") = decAmount
        drNewRow("ServerNumber") = strDestServerNumber
        drNewRow("FirstName") = strDestFirstName
        drNewRow("LastName") = strDestLastName
        drNewRow("Description") = strDescription
        If Not IsDBNull(Me.FileDataSet.Tips.FindByTipID(intSourceTipID)("SpecialFunction")) Then
            drNewRow("SpecialFunction") = strSpecialFunction
        End If
        drNewRow("WorkingDate") = dteWorkingDate

        Me.FileDataSet.Tips.Rows.Add(drNewRow)

        Me.FileDataSet.Tips.FindByTipID(intSourceTipID).Delete()

        frmSelectServer.Dispose()
    End Sub

    'Cash operations begin below:
    Private Sub AddCashTip()
        If txtCAServerNumber.Text = "" Then
            MessageBox.Show("You must enter a server number.", "Invalid Entry", MessageBoxButtons.OK)
            txtCAServerNumber.Focus()
            Exit Sub
        End If

        If txtCAAmount.Text = "" Then
            MessageBox.Show("You must enter a tip amount.", "Invalid Entry", MessageBoxButtons.OK)
            txtCAAmount.Focus()
            Exit Sub
        End If

        AutoInsertCADecimal()

        Dim drNewRow As DataRow = Me.FileDataSet.Tips.NewRow

        drNewRow("Amount") = txtCAAmount.Text
        drNewRow("ServerNumber") = txtCAServerNumber.Text
        drNewRow("FirstName") = Me.FileDataSet.Servers.FindByServerNumber(txtCAServerNumber.Text)("FirstName").ToString
        drNewRow("LastName") = Me.FileDataSet.Servers.FindByServerNumber(txtCAServerNumber.Text)("LastName").ToString
        drNewRow("Description") = "Cash"
        drNewRow("WorkingDate") = CDate(Me.FileDataSet.Settings.FindBySetting("PeriodEnd")("Value"))

        Me.FileDataSet.Tips.Rows.Add(drNewRow)
        UpdateCATotals()

        txtCAAmount.Clear()
        txtCAServerNumber.Clear()
        txtCAServerName.Clear()
        txtCAServerNumber.Focus()
    End Sub

    Private Sub UpdateCATotals()
        Dim decAmount As Decimal = 0

        For Each row As DataGridViewRow In Me.CashDataGridView.Rows
            decAmount += CDec(row.Cells.Item("CAAmount").Value)
        Next

        lblCATotal.Text = "Total: " & Format(decAmount, "c")
    End Sub

    Private Sub txtCAServerNumber_KeyPress(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyPressEventArgs) Handles txtCAServerNumber.KeyPress
        If e.KeyChar = ChrW(13) Then
            e.Handled = True
            txtCAAmount.Focus()
        End If
    End Sub

    Private Sub txtCAAmount_KeyPress(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyPressEventArgs) Handles txtCAAmount.KeyPress
        If e.KeyChar = ChrW(13) Then
            e.Handled = True
            AddCashTip()
        End If
    End Sub

    Private Sub txtCAServerNumber_LostFocus(ByVal sender As Object, ByVal e As System.EventArgs) Handles txtCAServerNumber.LostFocus
        If txtCAServerNumber.Text = "" Then Exit Sub

        If Not (Me.FileDataSet.Servers.FindByServerNumber(txtCAServerNumber.Text) Is Nothing) Then
            Dim strFirstName As String = Me.FileDataSet.Servers.FindByServerNumber(txtCAServerNumber.Text)("FirstName").ToString
            Dim strLastName As String = Me.FileDataSet.Servers.FindByServerNumber(txtCAServerNumber.Text)("LastName").ToString

            txtCAServerName.Text = strFirstName & " " & strLastName
        Else
            MessageBox.Show("The server number you entered was not found in the data table.", "Server Not Found", MessageBoxButtons.OK)
            txtCAServerNumber.Clear()
            txtCAAmount.Clear()
            txtCAServerNumber.Focus()
            Exit Sub
        End If
    End Sub

    Private Sub txtCAAmount_LostFocus(ByVal sender As Object, ByVal e As System.EventArgs) Handles txtCAAmount.LostFocus
        AutoInsertCADecimal()
    End Sub

    Private Sub AutoInsertCADecimal()
        If txtCAAmount.Text = "" Then Exit Sub

        If Not IsNumeric(txtCAAmount.Text) Then
            MessageBox.Show("The tip amount must be numeric.", "Invalid Entry", MessageBoxButtons.OK)
            txtCAAmount.Clear()
            txtCAAmount.Focus()
            Exit Sub
        End If

        Dim decAmount As Decimal

        For Each c As Char In txtCAAmount.Text
            If c = "." Then
                decAmount = CDec(txtCAAmount.Text)
                txtCAAmount.Text = Format(decAmount, "0.00")
                Exit Sub
            End If
        Next

        decAmount = CDec(txtCAAmount.Text)
        decAmount = decAmount / 100
        txtCAAmount.Text = Format(decAmount, "0.00")
    End Sub

    Private Sub btnAddCA_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnAddCA.Click
        AddCashTip()
    End Sub

    Private Sub btnClearCA_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnClearCA.Click
        txtCAAmount.Clear()
        txtCAServerNumber.Clear()
        txtCAServerName.Clear()
        txtCAServerNumber.Focus()
    End Sub

    Private Sub mnuDeleteCATip_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles mnuDeleteCATip.Click
        If Me.CashDataGridView.Rows.Count = 0 Then Exit Sub

        Dim intTipID As Integer = CInt(Me.CashDataGridView.Item("CAID", Me.CashTipsBindingSource.Position).Value)

        Dim strTipAmount As String = Me.FileDataSet.Tips.FindByTipID(intTipID)("Amount").ToString
        Dim strFirstName As String = Me.FileDataSet.Tips.FindByTipID(intTipID)("FirstName").ToString
        Dim strLastName As String = Me.FileDataSet.Tips.FindByTipID(intTipID)("LastName").ToString

        If MessageBox.Show("Are you sure you want to delete this $" & strTipAmount & " tip for " & _
        strFirstName & " " & strLastName & "?", "Confirm Delete", MessageBoxButtons.YesNo) <> Windows.Forms.DialogResult.Yes Then
            Exit Sub
        End If

        Me.FileDataSet.Tips.FindByTipID(intTipID).Delete()
        UpdateCATotals()
    End Sub

    Private Sub txtCAServerName_GotFocus(ByVal sender As Object, ByVal e As System.EventArgs) Handles txtCAServerName.GotFocus
        txtCAServerNumber.Focus()
    End Sub

    Private Sub mnuReassignCATip_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles mnuReassignCATip.Click
        If Me.CashDataGridView.Rows.Count = 0 Then Exit Sub

        frmSelectServer.m_dsParentDataSet = Me.FileDataSet
        frmSelectServer.lblSelectServer.Text = "Select the tip recipient:"

        If frmSelectServer.ShowDialog <> Windows.Forms.DialogResult.OK Then
            frmSelectServer.Dispose()
            Exit Sub
        End If

        Dim strSourceServerNumber As String = Me.CashDataGridView.Item("CAServerNumber", Me.CashTipsBindingSource.Position).Value.ToString
        Dim intSourceTipID As Integer = CInt(Me.CashDataGridView.Item("CAID", Me.CashTipsBindingSource.Position).Value)

        Dim strDestServerNumber As String = frmSelectServer.ServerNumber
        Dim strDestFirstName As String = Me.FileDataSet.Servers.FindByServerNumber(strDestServerNumber)("FirstName").ToString
        Dim strDestLastName As String = Me.FileDataSet.Servers.FindByServerNumber(strDestServerNumber)("LastName").ToString

        If strSourceServerNumber = strDestServerNumber Then
            MessageBox.Show("The selected tip cannot be reassigned to its original owner.", "Invalid Selection", MessageBoxButtons.OK)
            Exit Sub
        End If

        Dim decAmount As Decimal = CDec(Me.FileDataSet.Tips.FindByTipID(intSourceTipID)("Amount"))
        Dim strDescription As String = Me.FileDataSet.Tips.FindByTipID(intSourceTipID)("Description").ToString
        Dim strSpecialFunction As String
        If Not IsDBNull(Me.FileDataSet.Tips.FindByTipID(intSourceTipID)("SpecialFunction")) Then
            strSpecialFunction = Me.FileDataSet.Tips.FindByTipID(intSourceTipID)("SpecialFunction").ToString
        End If
        Dim dteWorkingDate As Date = CDate(Me.FileDataSet.Tips.FindByTipID(intSourceTipID)("WorkingDate"))

        Dim drNewRow As DataRow = Me.FileDataSet.Tips.NewRow

        drNewRow("Amount") = decAmount
        drNewRow("ServerNumber") = strDestServerNumber
        drNewRow("FirstName") = strDestFirstName
        drNewRow("LastName") = strDestLastName
        drNewRow("Description") = strDescription
        If Not IsDBNull(Me.FileDataSet.Tips.FindByTipID(intSourceTipID)("SpecialFunction")) Then
            drNewRow("SpecialFunction") = strSpecialFunction
        End If
        drNewRow("WorkingDate") = dteWorkingDate

        Me.FileDataSet.Tips.Rows.Add(drNewRow)

        Me.FileDataSet.Tips.FindByTipID(intSourceTipID).Delete()

        frmSelectServer.Dispose()
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
    End Sub

    Private Sub txtSFServerName_GotFocus(ByVal sender As Object, ByVal e As System.EventArgs) Handles txtSFServerName.GotFocus
        txtSFServerNumber.Focus()
    End Sub

    Private Sub SpecialFunctionDataGridView_RowStateChanged(ByVal sender As Object, ByVal e As System.Windows.Forms.DataGridViewRowStateChangedEventArgs) Handles SpecialFunctionDataGridView.RowStateChanged
        UpdateSFTotals()
    End Sub

    Private Sub mnuReassignSFTip_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles mnuReassignSFTip.Click
        If Me.SpecialFunctionDataGridView.Rows.Count = 0 Then Exit Sub

        frmSelectServer.m_dsParentDataSet = Me.FileDataSet
        frmSelectServer.lblSelectServer.Text = "Select the tip recipient:"

        If frmSelectServer.ShowDialog <> Windows.Forms.DialogResult.OK Then
            frmSelectServer.Dispose()
            Exit Sub
        End If

        Dim strSourceServerNumber As String = Me.SpecialFunctionDataGridView.Item("SFServerNumber", Me.SpecialFunctionTipsBindingSource.Position).Value.ToString
        Dim intSourceTipID As Integer = CInt(Me.SpecialFunctionDataGridView.Item("SFID", Me.SpecialFunctionTipsBindingSource.Position).Value)

        Dim strDestServerNumber As String = frmSelectServer.ServerNumber
        Dim strDestFirstName As String = Me.FileDataSet.Servers.FindByServerNumber(strDestServerNumber)("FirstName").ToString
        Dim strDestLastName As String = Me.FileDataSet.Servers.FindByServerNumber(strDestServerNumber)("LastName").ToString

        If strSourceServerNumber = strDestServerNumber Then
            MessageBox.Show("The selected tip cannot be reassigned to its original owner.", "Invalid Selection", MessageBoxButtons.OK)
            Exit Sub
        End If

        Dim decAmount As Decimal = CDec(Me.FileDataSet.Tips.FindByTipID(intSourceTipID)("Amount"))
        Dim strDescription As String = Me.FileDataSet.Tips.FindByTipID(intSourceTipID)("Description").ToString
        Dim strSpecialFunction As String
        If Not IsDBNull(Me.FileDataSet.Tips.FindByTipID(intSourceTipID)("SpecialFunction")) Then
            strSpecialFunction = Me.FileDataSet.Tips.FindByTipID(intSourceTipID)("SpecialFunction").ToString
        End If
        Dim dteWorkingDate As Date = CDate(Me.FileDataSet.Tips.FindByTipID(intSourceTipID)("WorkingDate"))

        Dim drNewRow As DataRow = Me.FileDataSet.Tips.NewRow

        drNewRow("Amount") = decAmount
        drNewRow("ServerNumber") = strDestServerNumber
        drNewRow("FirstName") = strDestFirstName
        drNewRow("LastName") = strDestLastName
        drNewRow("Description") = strDescription
        If Not IsDBNull(Me.FileDataSet.Tips.FindByTipID(intSourceTipID)("SpecialFunction")) Then
            drNewRow("SpecialFunction") = strSpecialFunction
        End If
        drNewRow("WorkingDate") = dteWorkingDate

        Me.FileDataSet.Tips.Rows.Add(drNewRow)

        Me.FileDataSet.Tips.FindByTipID(intSourceTipID).Delete()

        frmSelectServer.Dispose()
    End Sub

    'End of tip operations

    Private Sub mnuManageSpecialFunctions_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles mnuManageSpecialFunctions.Click
        With frmManageSpecialFunctions
            .m_dsParentDataSet = Me.FileDataSet
            .ShowDialog()
            .Dispose()
        End With

        Me.SpecialFunctionTipsBindingSource.Filter = "Description = 'Special Function'"
        Me.cboSelectSpecialFunction.SelectedIndex = -1
    End Sub

    Private Sub cboSelectSpecialFunction_SelectedIndexChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles cboSelectSpecialFunction.SelectedIndexChanged
        If cboSelectSpecialFunction.SelectedIndex = -1 Then Exit Sub

        Dim strSpecialFunction As String = cboSelectSpecialFunction.SelectedValue.ToString

        Me.SpecialFunctionTipsBindingSource.Filter = "SpecialFunction = '" & strSpecialFunction & "'"
        Me.SpecialFunctionTipsBindingSource.Sort = "LastName"
    End Sub

    Private Sub mnuExportTips_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles mnuExportTips.Click
        dlgSaveFile.InitialDirectory = My.Computer.FileSystem.SpecialDirectories.MyDocuments

        If dlgSaveFile.ShowDialog <> Windows.Forms.DialogResult.OK Then
            dlgSaveFile.Dispose()
            Exit Sub
        End If

        Windows.Forms.Cursor.Current = Cursors.WaitCursor

        Dim strFileContents As String = "TipID,Amount,ServerNumber,FirstName,LastName,Description,SpecialFunction,WorkingDate" & vbCrLf

        For Each row As DataRow In Me.FileDataSet.Tips.Rows
            If row.RowState <> DataRowState.Deleted Then
                Dim strTipID As String = row("TipID").ToString
                Dim strAmount As String = row("Amount").ToString
                Dim strServerNumber As String = row("ServerNumber").ToString
                Dim strFirstName As String = row("FirstName").ToString
                Dim strLastName As String = row("LastName").ToString
                Dim strDescription As String = row("Description").ToString
                Dim strSpecialFunction As String = row("SpecialFunction").ToString
                Dim strWorkingDate As String = Format(CDate(row("WorkingDate")), "MM/dd/yyyy")

                strFileContents += strTipID & "," & strAmount & "," & strServerNumber & "," & strFirstName & _
                "," & strLastName & "," & strDescription & "," & strSpecialFunction & "," & strWorkingDate & vbCrLf
            End If
        Next

        Dim objStreamWriter As New StreamWriter(dlgSaveFile.FileName)
        objStreamWriter.Write(strFileContents)
        objStreamWriter.Flush()
        objStreamWriter.Close()
        objStreamWriter.Dispose()

        Windows.Forms.Cursor.Current = Cursors.Default
    End Sub

    Private Sub mnuImportTips_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles mnuImportTips.Click
        dlgOpenFile.InitialDirectory = My.Computer.FileSystem.SpecialDirectories.Desktop
        dlgOpenFile.RestoreDirectory = True

        If dlgOpenFile.ShowDialog <> Windows.Forms.DialogResult.OK Then
            dlgOpenFile.Dispose()
            Exit Sub
        End If

        Dim strFileToImport As String = dlgOpenFile.FileName

        dlgOpenFile.Dispose()

        'If the file cannot be opened, then it is an invalid file.
        Try
            Dim objFileDecoder As New clsFileEncoder
            Dim objDSStream As New MemoryStream

            objDSStream = objFileDecoder.DecodeFile(strFileToImport)

            objDSStream.Flush()
            objDSStream.Seek(0, SeekOrigin.Begin)

            Me.ImportFileDataSet.ReadXml(objDSStream)

            objFileDecoder.Dispose()
            objDSStream.Close()
            objDSStream.Dispose()

            Me.ImportFileDataSet.AcceptChanges()
        Catch ex As ConstraintException
            MessageBox.Show("Error importing from requested file.  There are either no functions or no servers to match the tips included in the file.", "Data Constraint Error", MessageBoxButtons.OK)
            Exit Sub
        Catch ex As Exception
            MessageBox.Show("Could not import from the requsted file.  Either the file is not a valid Tip Tracker SF data file or the file is corrupted.", "Error Loading File", MessageBoxButtons.OK)
            Exit Sub
        End Try

        'If there are no tips in the file, there is no point in continuing.
        If Me.ImportFileDataSet.Tips.Rows.Count = 0 Then
            MessageBox.Show("There are no tips contained in requested file.", "No Tips To Import", MessageBoxButtons.OK)
            Me.ImportFileDataSet.Clear()
            Me.ImportFileDataSet.AcceptChanges()
            Exit Sub
        End If

        'If there are tips other than special function tips, then the file most likely is not a
        '.tsf file.  Therefore, it should be edited in Tip Tracker as a normal .ttd file would 
        'be edited, rather than by importing it.

        Dim dv As New DataView

        dv.Table = Me.ImportFileDataSet.Tips
        dv.RowFilter = "Description = 'Cash' OR Description = 'Credit Card' OR Description = 'Room Charge'"

        If Me.ImportFileDataSet.Settings.Rows.Count <> 0 Or dv.Count <> 0 Then
            MessageBox.Show("The file you reqested is a Tip Tracker data file (*.ttd), not a Tip Tracker SF data file (*.tsf).  You may not import tips from this type of file.", "Invalid File Type", MessageBoxButtons.OK)
            Me.ImportFileDataSet.Clear()
            Me.ImportFileDataSet.AcceptChanges()
            dv = Nothing
            Exit Sub
        End If

        dv = Nothing

        'Remove any functions and any servers that do not have tips.
        For Each row As DataRow In Me.ImportFileDataSet.Servers
            Dim strServerNumber As String = row("ServerNumber").ToString

            dv = New DataView
            dv.Table = Me.ImportFileDataSet.Tips
            dv.RowFilter = "ServerNumber = '" & strServerNumber & "'"

            If dv.Count = 0 Then
                row.Delete()
            End If
        Next

        Me.ImportFileDataSet.AcceptChanges()

        For Each row As DataRow In Me.ImportFileDataSet.SpecialFunctions
            Dim strFunctionName As String = row("SpecialFunction").ToString

            dv = New DataView
            dv.Table = Me.ImportFileDataSet.Tips
            dv.RowFilter = "SpecialFunction = '" & strFunctionName & "'"

            If dv.Count = 0 Then
                row.Delete()
            End If
        Next

        Me.ImportFileDataSet.AcceptChanges()

        'Now that all of the tests have passed (i.e. it is a .tsf file, there are no constraint
        'exceptions, it contains tips, etc.), the data needs to be checked to ensure that there will
        'be no duplicates (which may cause a constraint exception).  Each imported server or function  
        'should be matched to an existing one or added as a new server or function

        'Go through each row to match the imported servers with the existing server.  For each
        'server that isn't found, set the row error state to "NotFound".
        For Each row As DataRow In Me.ImportFileDataSet.Servers.Rows
            Dim strImportedServerNumber As String = row("ServerNumber").ToString
            Dim strImportedFirstName As String = row("FirstName").ToString
            Dim strImportedLastName As String = row("LastName").ToString

            Dim strExistingServerNumber As String
            Dim strExistingFirstName As String
            Dim strExistingLastName As String

            Try
                strExistingServerNumber = Me.FileDataSet.Servers.FindByServerNumber(strImportedServerNumber)("ServerNumber").ToString
                strExistingFirstName = Me.FileDataSet.Servers.FindByServerNumber(strImportedServerNumber)("FirstName").ToString
                strExistingLastName = Me.FileDataSet.Servers.FindByServerNumber(strImportedServerNumber)("LastName").ToString

                If strImportedServerNumber <> strExistingServerNumber Or strImportedFirstName <> strExistingFirstName Or strImportedLastName <> strExistingLastName Then
                    row.RowError = "NotFound"
                End If
            Catch ex As Exception
                row.RowError = "NotFound"
            End Try
        Next

        For Each row As DataRow In Me.ImportFileDataSet.Servers.Rows
            Dim strImportedServerNumber As String = row("ServerNumber").ToString
            Dim strImportedFirstName As String = row("FirstName").ToString
            Dim strImportedLastName As String = row("LastName").ToString

            If row.RowError = "NotFound" Then
                With frmImportServersComparer
                    .m_dsParentDataSet = Me.FileDataSet
                    .ServerNumber = strImportedServerNumber
                    .FirstName = strImportedFirstName
                    .LastName = strImportedLastName

                    If .ShowDialog() <> Windows.Forms.DialogResult.OK Then
                        .Dispose()
                        Me.ImportFileDataSet.Clear()
                        Me.ImportFileDataSet.AcceptChanges()
                        Exit Sub
                    End If

                    row("ServerNumber") = .ServerNumber
                    row("FirstName") = .FirstName
                    row("LastName") = .LastName
                    row.RowError = Nothing

                    Me.ImportFileDataSet.AcceptChanges()
                    .Dispose()
                End With
            End If
        Next

        'Check each of the functions to see if it already exists in the file.  If
        'it does, ask the user to assign the tip to the existing function or to rename 
        'the function being imported.
        For Each row As DataRow In Me.ImportFileDataSet.SpecialFunctions.Rows
            Dim strImportedFunction As String = row("SpecialFunction").ToString
            Dim dteImportedDate As Date = CDate(row("Date"))

            Dim strExistingFunction As String
            Dim dteExistingDate As Date

            Try
                strExistingFunction = Me.FileDataSet.SpecialFunctions.FindBySpecialFunction(strImportedFunction)("SpecialFunction").ToString
                dteExistingDate = CDate(Me.FileDataSet.SpecialFunctions.FindBySpecialFunction(strImportedFunction)("Date"))

                If dteImportedDate <> dteExistingDate Then
                    row.RowError = "NotFound"
                End If
            Catch ex As Exception
                row.RowError = "NotFound"
            End Try
        Next

        For Each row As DataRow In Me.ImportFileDataSet.SpecialFunctions.Rows
            Dim strImportedFunction As String = row("SpecialFunction").ToString
            Dim dteImportedDate As Date = CDate(row("Date"))

            If row.RowError = "NotFound" Then
                With frmImportFunctionsComparer
                    .m_dsParentDataSet = Me.FileDataSet
                    .SpecialFunction = strImportedFunction
                    .FunctionDate = Format(dteImportedDate, "MM/dd/yyyy")

                    If .ShowDialog <> Windows.Forms.DialogResult.OK Then
                        .Dispose()
                        Me.ImportFileDataSet.Clear()
                        Me.ImportFileDataSet.AcceptChanges()
                        Exit Sub
                    End If

                    row("SpecialFunction") = .SpecialFunction
                    row("Date") = .FunctionDate
                    row.RowError = Nothing

                    Me.ImportFileDataSet.AcceptChanges()
                    .Dispose()
                End With
            End If
        Next

        Windows.Forms.Cursor.Current = Cursors.WaitCursor

        'Since all information for the servers is updated through the cascade rule in the
        'dataset, everything is correct in tips table.  However, only the function name
        'is used as the primary key in the special functions table.  Therefore, each tip
        'associated with the function needs to have its working date changed to the function date.
        For Each row As DataRow In Me.ImportFileDataSet.SpecialFunctions.Rows
            Dim strFunctionName As String = row("SpecialFunction").ToString
            Dim dteFunctionDate As Date = CDate(row("Date"))

            dv = New DataView
            dv.Table = Me.ImportFileDataSet.Tips
            dv.RowFilter = "SpecialFunction = '" & strFunctionName & "'"

            For i As Integer = 0 To dv.Count - 1
                dv.Item(i)("WorkingDate") = dteFunctionDate
            Next
        Next

        Me.FileDataSet.Servers.Merge(Me.ImportFileDataSet.Servers)
        Me.FileDataSet.SpecialFunctions.Merge(Me.ImportFileDataSet.SpecialFunctions)

        For Each row As DataRow In Me.ImportFileDataSet.Tips
            Dim decAmount As Decimal = CDec(row("Amount"))
            Dim strServerNumber As String = row("ServerNumber").ToString
            Dim strFirstName As String = row("FirstName").ToString
            Dim strLastName As String = row("LastName").ToString
            Dim strDescription As String = row("Description").ToString
            Dim strSpecialFunction As String = row("SpecialFunction").ToString
            Dim dteAmount As Date = CDate(row("WorkingDate"))

            Dim drNewRow As DataRow = Me.FileDataSet.Tips.NewRow

            drNewRow("Amount") = decAmount
            drNewRow("ServerNumber") = strServerNumber
            drNewRow("FirstName") = strFirstName
            drNewRow("LastName") = strLastName
            drNewRow("Description") = strDescription
            drNewRow("SpecialFunction") = strSpecialFunction
            drNewRow("WorkingDate") = dteAmount

            Me.FileDataSet.Tips.Rows.Add(drNewRow)
        Next

        Windows.Forms.Cursor.Current = Cursors.Default

        Me.SpecialFunctionTipsBindingSource.Filter = "Description = 'Special Function'"
        Me.SpecialFunctionTipsBindingSource.Sort = "SpecialFunction, LastName"
        Me.cboSelectSpecialFunction.SelectedIndex = -1

        MessageBox.Show("The tips were successfully imported.", "Message", MessageBoxButtons.OK, MessageBoxIcon.Information)

        Me.ImportFileDataSet.Clear()
        Me.ImportFileDataSet.AcceptChanges()
    End Sub

    Private Sub mnuAddServer_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles mnuAddServer.Click
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

        If frmMain.GlobalDataSet.Servers.FindByServerNumber(frmAddEditServer.ServerNumber) Is Nothing Then
            If MessageBox.Show("This server does not exist in the servers template.  Add the server to the template?", "Add Server", MessageBoxButtons.YesNo) <> Windows.Forms.DialogResult.Yes Then
                frmAddEditServer.Dispose()
                Exit Sub
            Else
                drNewRow = frmMain.GlobalDataSet.Servers.NewRow

                drNewRow("ServerNumber") = frmAddEditServer.ServerNumber
                drNewRow("FirstName") = frmAddEditServer.FirstName
                drNewRow("LastName") = frmAddEditServer.LastName

                frmMain.GlobalDataSet.Servers.Rows.Add(drNewRow)
                frmMain.GlobalDataSet.AcceptChanges()
            End If
        End If

        frmAddEditServer.Dispose()
    End Sub

    Private Sub mnuEditSelectedServer_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles mnuEditSelectedServer.Click
        Dim strServerNumber As String = Me.ServersDataGridView.Item("ServersServerNumber", Me.ServersBindingSource.Position).Value.ToString
        Dim strFirstName As String = Me.FileDataSet.Servers.FindByServerNumber(strServerNumber)("FirstName").ToString
        Dim strLastName As String = Me.FileDataSet.Servers.FindByServerNumber(strServerNumber)("LastName").ToString

        With frmAddEditServer
            .Text = "Edit Server"
            .txtServerNumber.ReadOnly = True
            .txtServerNumber.TabStop = False
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

    Private Sub btnShowAllTips_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnShowAllTips.Click
        Me.SpecialFunctionTipsBindingSource.Filter = "Description = 'Special Function'"
        Me.SpecialFunctionTipsBindingSource.Sort = "SpecialFunction, LastName"
        Me.cboSelectSpecialFunction.SelectedIndex = -1
    End Sub

    Private Sub mnuMergeDuplicate_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles mnuMergeDuplicate.Click
        frmSelectServer.m_dsParentDataSet = Me.FileDataSet
        frmSelectServer.lblSelectServer.Text = "Select the merge recipient:"

        If frmSelectServer.ShowDialog <> Windows.Forms.DialogResult.OK Then
            frmSelectServer.Dispose()
            Exit Sub
        End If

        Dim strSourceServerNumber As String = Me.ServersDataGridView.Item("ServersServerNumber", Me.ServersBindingSource.Position).Value.ToString
        Dim strDestServerNumber As String = frmSelectServer.ServerNumber
        Dim strDestFirstName As String = Me.FileDataSet.Servers.FindByServerNumber(strDestServerNumber)("FirstName").ToString
        Dim strDestLastName As String = Me.FileDataSet.Servers.FindByServerNumber(strDestServerNumber)("LastName").ToString

        If strSourceServerNumber = strDestServerNumber Then
            MessageBox.Show("You may not merge a server with itself.", "Invalid Selection", MessageBoxButtons.OK)
            Exit Sub
        End If


        Dim dv As New DataView
        dv.Table = Me.FileDataSet.Tips
        dv.RowFilter = "ServerNumber = '" & strSourceServerNumber & "'"

        Dim drNewRow As DataRow

        For i As Integer = 0 To dv.Count - 1
            drNewRow = Me.FileDataSet.Tips.NewRow

            drNewRow("Amount") = CDec(dv(i)("Amount"))
            drNewRow("ServerNumber") = strDestServerNumber
            drNewRow("FirstName") = strDestFirstName
            drNewRow("LastName") = strDestLastName
            drNewRow("Description") = dv(i)("Description").ToString
            If Not IsDBNull(dv(i)("SpecialFunction")) Then
                drNewRow("SpecialFunction") = dv(i)("SpecialFunction").ToString
            End If
            drNewRow("WorkingDate") = CDate(dv(i)("WorkingDate"))

            Me.FileDataSet.Tips.Rows.Add(drNewRow)
        Next

        Me.FileDataSet.Servers.FindByServerNumber(strSourceServerNumber).Delete()
        Me.FileDataSet.Servers.AcceptChanges()

        MessageBox.Show("The merge was completed successfully.", "Message", MessageBoxButtons.OK, MessageBoxIcon.Information)

        frmSelectServer.Dispose()
    End Sub

    Private Sub mnuCopyFromTemplate_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles mnuCopyFromTemplate.Click
        Me.FileDataSet.Servers.Merge(frmMain.GlobalDataSet.Servers)
    End Sub

    Private Sub mnuPrintTipChits_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles mnuPrintRegularTipChits.Click
        frmPrintRegularTipChits.m_dsParentDataset = Me.FileDataSet
        frmPrintRegularTipChits.ShowDialog()
        frmPrintRegularTipChits.Dispose()
    End Sub

    Private Sub mnuPrintSpecialFunctionChits_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles mnuPrintSpecialFunctionChits.Click
        frmPrintSpecialFunctionChits.m_dsParentDataset = Me.FileDataSet
        frmPrintSpecialFunctionChits.ShowDialog()
        frmPrintSpecialFunctionChits.Dispose()
    End Sub

    Private Sub mnuServerReport_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles mnuServerReport.Click
        frmPrintServerReport.m_dsParentDataSet = Me.FileDataSet
        frmPrintServerReport.ShowDialog()
        frmPrintServerReport.Dispose()
    End Sub

    Private Sub mnuRegularTips_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles mnuRegularTips.Click
        frmPrintTipReport.m_dsParentDataSet = Me.FileDataSet
        frmPrintTipReport.ShowDialog()
        frmPrintTipReport.Dispose()
    End Sub

    Private Sub mnuSpecialFunctionTips_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles mnuSpecialFunctionTips.Click
        frmPrintSpecialFunctionReport.m_dsParentDataset = Me.FileDataSet
        frmPrintSpecialFunctionReport.ShowDialog()
        frmPrintSpecialFunctionReport.Dispose()
    End Sub

    Private Sub btnManageFunctions_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnManageFunctions.Click
        With frmManageSpecialFunctions
            .m_dsParentDataSet = Me.FileDataSet
            .ShowDialog()
            .Dispose()
        End With

        Me.SpecialFunctionTipsBindingSource.Filter = "Description = 'Special Function'"
        Me.cboSelectSpecialFunction.SelectedIndex = -1
    End Sub
End Class