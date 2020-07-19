Imports System.Drawing.Printing
Imports System.IO
Imports TipTracker.Common.Data.PayPeriod

Public Class frmEnterTips
    Public ReadOnly Property File As PayPeriodFile
    Public ReadOnly Property Data As PayPeriodData

    Public Sub New(ByVal file As PayPeriodFile, ByVal data As PayPeriodData)
        InitializeComponent()

        Me.File = file
        Me.Data = data
        FileDataSet = data.FileDataSet
        Text = Path.GetFileNameWithoutExtension(file.FilePath)
    End Sub

    Private Sub frmEnterTips_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
        'Set the form as an mdi child of frmMain.
        Me.MdiParent = frmMain
        Me.lblSystemDate.Text = "System Date: " & Format(DateTime.Today, "M/d/yyyy")
        Me.WindowState = FormWindowState.Maximized

        'Bind the data sources to the display.
        ServersBindingSource.DataSource = Data.FileDataSet
        ServersBindingSource.DataMember = Data.FileDataSet.Servers.TableName

        CreditCardTipsBindingSource.DataSource = Data.FileDataSet
        CreditCardTipsBindingSource.DataMember = Data.FileDataSet.Tips.TableName

        RoomChargeTipsBindingSource.DataSource = Data.FileDataSet
        RoomChargeTipsBindingSource.DataMember = Data.FileDataSet.Tips.TableName

        CashTipsBindingSource.DataSource = Data.FileDataSet
        CashTipsBindingSource.DataMember = Data.FileDataSet.Tips.TableName

        SpecialFunctionTipsBindingSource.DataSource = Data.FileDataSet
        SpecialFunctionTipsBindingSource.DataMember = Data.FileDataSet.Tips.TableName

        SpecialFunctionBindingSource.DataSource = Data.FileDataSet
        SpecialFunctionBindingSource.DataMember = Data.FileDataSet.SpecialFunctions.TableName


        'Set the servers binding source sort mode.
        Me.ServersBindingSource.Sort = "LastName"

        LoadServerCombos()

        'Update the date labels to show the dates from the settings table.
        UpdateDateLabels()

        'Set the tips binding source sort and filter modes.
        SetSelectionFilters()
        cboSelectSpecialFunction.SelectedIndex = -1

        'Since the tab control doesn't like to focus on the first child control on it's own,
        'the child control needs to be selected manually.
        Me.txtCCServerNumber.Focus()

    End Sub

    Private Sub LoadServerCombos()
        'Clear the server lookup dataset if there are any records already in it.
        Me.ServersLookupDataset.Clear()
        Me.ServersLookupDataset.AcceptChanges()

        'Populate the server lookup data table.
        Dim dvTemp As New DataView

        dvTemp.Table = Data.FileDataSet.Servers
        dvTemp.Sort = "LastName, FirstName"

        If dvTemp.Count <> 0 Then
            For i As Integer = 0 To dvTemp.Count - 1
                Dim strServerNumber As String = dvTemp(i)("ServerNumber").ToString
                Dim strFirstName As String = dvTemp(i)("FirstName").ToString
                Dim strLastName As String = dvTemp(i)("LastName").ToString

                'Build name string with LastName, FirstName format.
                Dim drNewRow As DataRow = Me.ServersLookupDataset.Servers.NewRow

                drNewRow("ServerNumber") = strServerNumber
                drNewRow("NameString") = strLastName & ", " & strFirstName

                Me.ServersLookupDataset.Servers.Rows.Add(drNewRow)

                'Build name string with FirstName LastName format.
                drNewRow = Me.ServersLookupDataset.Servers.NewRow

                drNewRow("ServerNumber") = strServerNumber
                drNewRow("NameString") = strFirstName & " " & strLastName

                Me.ServersLookupDataset.Servers.Rows.Add(drNewRow)

                'Build name string with server number included.
                drNewRow = Me.ServersLookupDataset.Servers.NewRow

                drNewRow("ServerNumber") = strServerNumber
                drNewRow("NameString") = strServerNumber & " " & strLastName & ", " & strFirstName

                Me.ServersLookupDataset.Servers.Rows.Add(drNewRow)
            Next

            Me.ServersLookupDataset.AcceptChanges()
            cboCAServer.SelectedIndex = -1
            cboSFServer.SelectedIndex = -1
        End If
    End Sub

    Private Sub UpdateDateLabels()
        Dim dtePeriodStart As Date = CDate(Data.FileDataSet.Settings.FindBySetting("PeriodStart")("Value"))
        Dim dtePeriodEnd As Date = CDate(Data.FileDataSet.Settings.FindBySetting("PeriodEnd")("Value"))
        Dim dteWorkingDate As Date = CDate(Data.FileDataSet.Settings.FindBySetting("WorkingDate")("Value"))

        Me.lblPeriodStart.Text = "Period Start: " & Format(dtePeriodStart, "M/d/yyyy")
        Me.lblPeriodEnd.Text = "Period End: " & Format(dtePeriodEnd, "M/d/yyyy")
        Me.lblWorkingDate.Text = "Working Date: " & Format(dteWorkingDate, "M/d/yyyy")
    End Sub

    Private Sub SetSelectionFilters()
        Dim strWorkingDate As String = Format(CDate(Data.FileDataSet.Settings.FindBySetting("WorkingDate")("Value")), "M/d/yyyy")
        Me.CreditCardTipsBindingSource.Filter = "Description = 'Credit Card' AND WorkingDate = '" & strWorkingDate & "'"
        Me.CreditCardTipsBindingSource.Sort = "TipID"

        Me.RoomChargeTipsBindingSource.Filter = "Description = 'Room Charge' AND WorkingDate = '" & strWorkingDate & "'"
        Me.RoomChargeTipsBindingSource.Sort = "TipID"

        Me.CashTipsBindingSource.Filter = "Description = 'Cash'"
        Me.CashTipsBindingSource.Sort = "TipID"


        If cboSelectSpecialFunction.SelectedIndex <> -1 Then
            Me.SpecialFunctionTipsBindingSource.Filter = "Description = 'Special Function' AND SpecialFunction = '" & cboSelectSpecialFunction.SelectedValue.ToString & "'"
            Me.SpecialFunctionTipsBindingSource.Sort = "SpecialFunction, TipID"
        Else
            Me.SpecialFunctionTipsBindingSource.Filter = "Description = 'Special Function'"
            Me.SpecialFunctionTipsBindingSource.Sort = "SpecialFunction, TipID"
        End If

        Me.SpecialFunctionBindingSource.Sort = "SpecialFunction"

        UpdateCCTotals()
        UpdateRCTotals()
        UpdateCATotals()
    End Sub

    Private Sub tabTipsTabControl_SelectedIndexChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles tabTipsTabControl.SelectedIndexChanged
        Dim creditCard As TabPage = Me.tabCreditCard
        Dim roomCharge As TabPage = Me.tabRoomCharge
        Dim cash As TabPage = Me.tabCash
        Dim specialFunction As TabPage = Me.tabSpecialFunction

        If tabTipsTabControl.SelectedTab Is creditCard Then
            Me.SelectNextControl(creditCard, True, True, True, True)
        ElseIf tabTipsTabControl.SelectedTab Is roomCharge Then
            Me.SelectNextControl(roomCharge, True, True, True, True)
        ElseIf tabTipsTabControl.SelectedTab Is cash Then
            Me.SelectNextControl(cash, True, True, True, True)
        ElseIf tabTipsTabControl.SelectedTab Is specialFunction Then
            Me.SelectNextControl(specialFunction, True, True, True, True)
        End If

        lblCurrentTipType.Text = "Editing " & Me.tabTipsTabControl.SelectedTab.Text & " Tips"

        ''Make the tip ID columns invisible.
        'Me.CCID.Visible = False
        'Me.RCID.Visible = False
        'Me.CAID.Visible = False
        'Me.SFID.Visible = False

        txtCCAmount.Clear()
        txtCCServerNumber.Clear()
        txtCCServerName.Clear()

        txtRCAmount.Clear()
        txtRCServerNumber.Clear()
        txtRCServerName.Clear()

        Me.cboCAServer.SelectedIndex = -1
        Me.cboSFServer.SelectedIndex = -1

        txtCAAmount.Clear()
        txtSFAmount.Clear()

        Me.cboSelectSpecialFunction.SelectedIndex = -1
    End Sub

    Private Sub btnFinalize_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnFinalize.Click
        Dim dteWorkingDate As Date = CDate(Data.FileDataSet.Settings.FindBySetting("WorkingDate")("Value"))
        Dim dtePeriodEnd As Date = CDate(Data.FileDataSet.Settings.FindBySetting("PeriodEnd")("Value"))

        If dteWorkingDate = dtePeriodEnd Then
            MessageBox.Show("The current working date is the last day in the pay period.  You cannot " &
            "advance the working date any further.  To work on tips for " &
            Format(DateAdd(DateInterval.Day, 1, dteWorkingDate), "M/d/yyyy") & " you must start a new file " &
            "for the new pay period.", "Cannot Change Working Date", MessageBoxButtons.OK)
            Exit Sub
        End If

        If MessageBox.Show("The working date will be changed to " & Format(DateAdd(DateInterval.Day, 1, dteWorkingDate), "M/d/yyyy") &
        ".  Do you wish to continue?", "Confirm Date Change", MessageBoxButtons.YesNo) <> Windows.Forms.DialogResult.Yes Then
            Exit Sub
        End If

        Data.FileDataSet.Settings.FindBySetting("WorkingDate")("Value") = DateAdd(DateInterval.Day, 1, dteWorkingDate)
        UpdateDateLabels()
        SetSelectionFilters()
    End Sub

    Private Sub btnSelectWorkingDate_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnSelectWorkingDate.Click
        Dim dtePeriodStart As Date = CDate(Data.FileDataSet.Settings.FindBySetting("PeriodStart")("Value"))
        Dim dtePeriodEnd As Date = CDate(Data.FileDataSet.Settings.FindBySetting("PeriodEnd")("Value"))
        Dim dteWorkingDate As Date = CDate(Data.FileDataSet.Settings.FindBySetting("WorkingDate")("Value"))

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

            Data.FileDataSet.Settings.FindBySetting("WorkingDate")("Value") = .SelectedDate

            .Dispose()
        End With

        UpdateDateLabels()
        SetSelectionFilters()
    End Sub

    'Credit card operations begin below:
#Region "CreditCardOperations"

    Private Sub AddCreditCardTip()
        If txtCCServerNumber.Text = "" Then
            MessageBox.Show("You must enter a server number.", "Invalid Entry", MessageBoxButtons.OK)
            txtCCServerNumber.Focus()
            Exit Sub
        End If

        If Not IsNumeric(txtCCAmount.Text) Then
            MessageBox.Show("The tip amount must be a number.", "Invalid Entry", MessageBoxButtons.OK)
            txtCCAmount.Clear()
            txtCCAmount.Focus()
            Exit Sub
        End If

        If txtCCAmount.Text = "" Then
            MessageBox.Show("You must enter a tip amount.", "Invalid Entry", MessageBoxButtons.OK)
            txtCCAmount.Focus()
            Exit Sub
        End If

        AutoInsertCCDecimal()

        Dim drNewRow As DataRow = Data.FileDataSet.Tips.NewRow

        drNewRow("Amount") = txtCCAmount.Text
        drNewRow("ServerNumber") = txtCCServerNumber.Text
        drNewRow("FirstName") = Data.FileDataSet.Servers.FindByServerNumber(txtCCServerNumber.Text)("FirstName").ToString
        drNewRow("LastName") = Data.FileDataSet.Servers.FindByServerNumber(txtCCServerNumber.Text)("LastName").ToString
        drNewRow("Description") = "Credit Card"
        drNewRow("WorkingDate") = CDate(Data.FileDataSet.Settings.FindBySetting("WorkingDate")("Value"))

        Data.FileDataSet.Tips.Rows.Add(drNewRow)
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

        If Not (Data.FileDataSet.Servers.FindByServerNumber(txtCCServerNumber.Text) Is Nothing) Then
            Dim strFirstName As String = Data.FileDataSet.Servers.FindByServerNumber(txtCCServerNumber.Text)("FirstName").ToString
            Dim strLastName As String = Data.FileDataSet.Servers.FindByServerNumber(txtCCServerNumber.Text)("LastName").ToString

            txtCCServerName.Text = strFirstName & " " & strLastName
        Else
            MessageBox.Show("The server number you entered was not found in the data table.", "Server Not Found", MessageBoxButtons.OK)
            txtCCServerNumber.Clear()
            txtCCAmount.Clear()
            txtCCServerNumber.Focus()
            Exit Sub
        End If
    End Sub

    Private Sub AutoInsertCCDecimal()
        If txtCCAmount.Text = "" Then Exit Sub

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

        Dim strTipAmount As String = Data.FileDataSet.Tips.FindByTipID(intTipID)("Amount").ToString
        Dim strFirstName As String = Data.FileDataSet.Tips.FindByTipID(intTipID)("FirstName").ToString
        Dim strLastName As String = Data.FileDataSet.Tips.FindByTipID(intTipID)("LastName").ToString

        If MessageBox.Show("Are you sure you want to delete this $" & strTipAmount & " tip for " &
        strFirstName & " " & strLastName & "?", "Confirm Delete", MessageBoxButtons.YesNo) <> Windows.Forms.DialogResult.Yes Then
            Exit Sub
        End If

        Data.FileDataSet.Tips.FindByTipID(intTipID).Delete()
        UpdateCCTotals()
    End Sub

    Private Sub txtCCServerName_GotFocus(ByVal sender As Object, ByVal e As System.EventArgs) Handles txtCCServerName.GotFocus
        txtCCServerNumber.Focus()
    End Sub

    Private Sub mnuReassignCCTip_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles mnuReassignCCTip.Click
        If Me.CreditCardDataGridView.Rows.Count = 0 Then Exit Sub

        frmSelectServer.m_dsParentDataSet = Data.FileDataSet
        frmSelectServer.lblSelectServer.Text = "Select the tip recipient:"

        If frmSelectServer.ShowDialog <> Windows.Forms.DialogResult.OK Then
            frmSelectServer.Dispose()
            Exit Sub
        End If

        Dim strSourceServerNumber As String = Me.CreditCardDataGridView.Item("CCServerNumber", Me.CreditCardTipsBindingSource.Position).Value.ToString
        Dim intSourceTipID As Integer = CInt(Me.CreditCardDataGridView.Item("CCID", Me.CreditCardTipsBindingSource.Position).Value)

        Dim strDestServerNumber As String = frmSelectServer.ServerNumber
        Dim strDestFirstName As String = Data.FileDataSet.Servers.FindByServerNumber(strDestServerNumber)("FirstName").ToString
        Dim strDestLastName As String = Data.FileDataSet.Servers.FindByServerNumber(strDestServerNumber)("LastName").ToString

        If strSourceServerNumber = strDestServerNumber Then
            MessageBox.Show("The selected tip cannot be reassigned to its original owner.", "Invalid Selection", MessageBoxButtons.OK)
            Exit Sub
        End If

        Dim decAmount As Decimal = CDec(Data.FileDataSet.Tips.FindByTipID(intSourceTipID)("Amount"))
        Dim strDescription As String = Data.FileDataSet.Tips.FindByTipID(intSourceTipID)("Description").ToString
        Dim strSpecialFunction As String = ""
        If Not IsDBNull(Data.FileDataSet.Tips.FindByTipID(intSourceTipID)("SpecialFunction")) Then
            strSpecialFunction = Data.FileDataSet.Tips.FindByTipID(intSourceTipID)("SpecialFunction").ToString
        End If
        Dim dteWorkingDate As Date = CDate(Data.FileDataSet.Tips.FindByTipID(intSourceTipID)("WorkingDate"))

        Dim drNewRow As DataRow = Data.FileDataSet.Tips.NewRow

        drNewRow("Amount") = decAmount
        drNewRow("ServerNumber") = strDestServerNumber
        drNewRow("FirstName") = strDestFirstName
        drNewRow("LastName") = strDestLastName
        drNewRow("Description") = strDescription
        If Not IsDBNull(Data.FileDataSet.Tips.FindByTipID(intSourceTipID)("SpecialFunction")) Then
            drNewRow("SpecialFunction") = strSpecialFunction
        End If
        drNewRow("WorkingDate") = dteWorkingDate

        Data.FileDataSet.Tips.Rows.Add(drNewRow)

        Data.FileDataSet.Tips.FindByTipID(intSourceTipID).Delete()

        frmSelectServer.Dispose()
        UpdateCCTotals()
    End Sub

    Private Sub mnuEditCCTip_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles mnuEditCCTip.Click, CreditCardDataGridView.DoubleClick
        If Me.CreditCardDataGridView.Rows.Count = 0 Then Exit Sub

        Dim intSourceTipID As Integer = CInt(Me.CreditCardDataGridView.Item("CCID", Me.CreditCardTipsBindingSource.Position).Value)

        Dim decAmount As Decimal = CDec(Data.FileDataSet.Tips.FindByTipID(intSourceTipID)("Amount"))
        Dim dteWorkingDate As Date = CDate(Data.FileDataSet.Tips.FindByTipID(intSourceTipID)("WorkingDate"))
        Dim strDescription As String = Data.FileDataSet.Tips.FindByTipID(intSourceTipID)("Description").ToString

        With frmEditTip
            .TipAmount = decAmount
            .WorkingDate = dteWorkingDate
            .TipType = strDescription
            .m_dsParentDataSet = Data.FileDataSet
        End With

        If frmEditTip.ShowDialog <> Windows.Forms.DialogResult.OK Then
            frmEditTip.Dispose()
            Exit Sub
        End If

        Dim strFunction As String

        If frmEditTip.TipAmount = decAmount And frmEditTip.WorkingDate = dteWorkingDate And frmEditTip.TipType = strDescription Then
            frmEditTip.Dispose()
            Exit Sub
        End If

        Dim decNewAmount As Decimal = frmEditTip.TipAmount
        Dim strNewDescription As String = frmEditTip.TipType
        Dim dteNewDate As Date = frmEditTip.WorkingDate
        Dim strServerNumber As String = Data.FileDataSet.Tips.FindByTipID(intSourceTipID)("ServerNumber").ToString
        Dim strFirstName As String = Data.FileDataSet.Servers.FindByServerNumber(strServerNumber)("FirstName").ToString
        Dim strLastName As String = Data.FileDataSet.Servers.FindByServerNumber(strServerNumber)("LastName").ToString

        Dim drNewRow As DataRow = Data.FileDataSet.Tips.NewRow

        If frmEditTip.TipType = "Special Function" Then
            For Each row As DataRow In Data.FileDataSet.SpecialFunctions
                frmSelectFunction.cboFunctions.Items.Add(row("SpecialFunction").ToString)
            Next

            If frmSelectFunction.ShowDialog <> Windows.Forms.DialogResult.OK Then
                frmSelectFunction.Dispose()
                frmEditTip.Dispose()
                Exit Sub
            End If

            strFunction = frmSelectFunction.SelectedFunction
            dteNewDate = CDate(Data.FileDataSet.SpecialFunctions.FindBySpecialFunction(strFunction)("Date"))
        End If

        If frmEditTip.TipType = "Cash" Then
            dteNewDate = CDate(Data.FileDataSet.Settings.FindBySetting("PeriodEnd")("Value"))
        End If

        frmSelectFunction.Dispose()

        drNewRow("Amount") = decNewAmount
        drNewRow("ServerNumber") = strServerNumber
        drNewRow("FirstName") = strFirstName
        drNewRow("LastName") = strLastName
        drNewRow("Description") = strNewDescription
        If strNewDescription = "Special Function" Then
            drNewRow("SpecialFunction") = strFunction
        End If
        drNewRow("WorkingDate") = dteNewDate

        Data.FileDataSet.Tips.Rows.Add(drNewRow)
        Data.FileDataSet.Tips.FindByTipID(intSourceTipID).Delete()

        frmEditTip.Dispose()
        UpdateCCTotals()
        UpdateRCTotals()
        UpdateSFTotals()
        UpdateCATotals()
    End Sub
#End Region

    'Room charge operations begin below:
#Region "RoomChargeOperations"
    Private Sub AddRoomChargeTip()
        If txtRCServerNumber.Text = "" Then
            MessageBox.Show("You must enter a server number.", "Invalid Entry", MessageBoxButtons.OK)
            txtRCServerNumber.Focus()
            Exit Sub
        End If

        If Not IsNumeric(txtRCAmount.Text) Then
            MessageBox.Show("The tip amount must be a number.", "Invalid Entry", MessageBoxButtons.OK)
            txtRCAmount.Clear()
            txtRCAmount.Focus()
            Exit Sub
        End If

        If txtRCAmount.Text = "" Then
            MessageBox.Show("You must enter a tip amount.", "Invalid Entry", MessageBoxButtons.OK)
            txtRCAmount.Focus()
            Exit Sub
        End If

        AutoInsertRCDecimal()

        Dim drNewRow As DataRow = Data.FileDataSet.Tips.NewRow

        drNewRow("Amount") = txtRCAmount.Text
        drNewRow("ServerNumber") = txtRCServerNumber.Text
        drNewRow("FirstName") = Data.FileDataSet.Servers.FindByServerNumber(txtRCServerNumber.Text)("FirstName").ToString
        drNewRow("LastName") = Data.FileDataSet.Servers.FindByServerNumber(txtRCServerNumber.Text)("LastName").ToString
        drNewRow("Description") = "Room Charge"
        drNewRow("WorkingDate") = CDate(Data.FileDataSet.Settings.FindBySetting("WorkingDate")("Value"))

        Data.FileDataSet.Tips.Rows.Add(drNewRow)
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

        If Not (Data.FileDataSet.Servers.FindByServerNumber(txtRCServerNumber.Text) Is Nothing) Then
            Dim strFirstName As String = Data.FileDataSet.Servers.FindByServerNumber(txtRCServerNumber.Text)("FirstName").ToString
            Dim strLastName As String = Data.FileDataSet.Servers.FindByServerNumber(txtRCServerNumber.Text)("LastName").ToString

            txtRCServerName.Text = strFirstName & " " & strLastName
        Else
            MessageBox.Show("The server number you entered was not found in the data table.", "Server Not Found", MessageBoxButtons.OK)
            txtRCServerNumber.Clear()
            txtRCAmount.Clear()
            txtRCServerNumber.Focus()
            Exit Sub
        End If
    End Sub

    Private Sub AutoInsertRCDecimal()
        If txtRCAmount.Text = "" Then Exit Sub

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

        Dim strTipAmount As String = Data.FileDataSet.Tips.FindByTipID(intTipID)("Amount").ToString
        Dim strFirstName As String = Data.FileDataSet.Tips.FindByTipID(intTipID)("FirstName").ToString
        Dim strLastName As String = Data.FileDataSet.Tips.FindByTipID(intTipID)("LastName").ToString

        If MessageBox.Show("Are you sure you want to delete this $" & strTipAmount & " tip for " &
        strFirstName & " " & strLastName & "?", "Confirm Delete", MessageBoxButtons.YesNo) <> Windows.Forms.DialogResult.Yes Then
            Exit Sub
        End If

        Data.FileDataSet.Tips.FindByTipID(intTipID).Delete()
        UpdateRCTotals()
    End Sub

    Private Sub txtRCServerName_GotFocus(ByVal sender As Object, ByVal e As System.EventArgs) Handles txtRCServerName.GotFocus
        txtRCServerNumber.Focus()
    End Sub

    Private Sub mnuReassignRCTip_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles mnuReassignRCTip.Click
        If Me.RoomChargeDataGridView.Rows.Count = 0 Then Exit Sub

        frmSelectServer.m_dsParentDataSet = Data.FileDataSet
        frmSelectServer.lblSelectServer.Text = "Select the tip recipient:"

        If frmSelectServer.ShowDialog <> Windows.Forms.DialogResult.OK Then
            frmSelectServer.Dispose()
            Exit Sub
        End If

        Dim strSourceServerNumber As String = Me.RoomChargeDataGridView.Item("RCServerNumber", Me.RoomChargeTipsBindingSource.Position).Value.ToString
        Dim intSourceTipID As Integer = CInt(Me.RoomChargeDataGridView.Item("RCID", Me.RoomChargeTipsBindingSource.Position).Value)

        Dim strDestServerNumber As String = frmSelectServer.ServerNumber
        Dim strDestFirstName As String = Data.FileDataSet.Servers.FindByServerNumber(strDestServerNumber)("FirstName").ToString
        Dim strDestLastName As String = Data.FileDataSet.Servers.FindByServerNumber(strDestServerNumber)("LastName").ToString

        If strSourceServerNumber = strDestServerNumber Then
            MessageBox.Show("The selected tip cannot be reassigned to its original owner.", "Invalid Selection", MessageBoxButtons.OK)
            Exit Sub
        End If

        Dim decAmount As Decimal = CDec(Data.FileDataSet.Tips.FindByTipID(intSourceTipID)("Amount"))
        Dim strDescription As String = Data.FileDataSet.Tips.FindByTipID(intSourceTipID)("Description").ToString
        Dim strSpecialFunction As String
        If Not IsDBNull(Data.FileDataSet.Tips.FindByTipID(intSourceTipID)("SpecialFunction")) Then
            strSpecialFunction = Data.FileDataSet.Tips.FindByTipID(intSourceTipID)("SpecialFunction").ToString
        End If
        Dim dteWorkingDate As Date = CDate(Data.FileDataSet.Tips.FindByTipID(intSourceTipID)("WorkingDate"))

        Dim drNewRow As DataRow = Data.FileDataSet.Tips.NewRow

        drNewRow("Amount") = decAmount
        drNewRow("ServerNumber") = strDestServerNumber
        drNewRow("FirstName") = strDestFirstName
        drNewRow("LastName") = strDestLastName
        drNewRow("Description") = strDescription
        If Not IsDBNull(Data.FileDataSet.Tips.FindByTipID(intSourceTipID)("SpecialFunction")) Then
            drNewRow("SpecialFunction") = strSpecialFunction
        End If
        drNewRow("WorkingDate") = dteWorkingDate

        Data.FileDataSet.Tips.Rows.Add(drNewRow)

        Data.FileDataSet.Tips.FindByTipID(intSourceTipID).Delete()

        frmSelectServer.Dispose()
    End Sub

    Private Sub mnuEditRCTip_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles mnuEditRCTip.Click, RoomChargeDataGridView.DoubleClick
        If Me.RoomChargeDataGridView.Rows.Count = 0 Then Exit Sub

        Dim intSourceTipID As Integer = CInt(Me.RoomChargeDataGridView.Item("RCID", Me.RoomChargeTipsBindingSource.Position).Value)

        Dim decAmount As Decimal = CDec(Data.FileDataSet.Tips.FindByTipID(intSourceTipID)("Amount"))
        Dim dteWorkingDate As Date = CDate(Data.FileDataSet.Tips.FindByTipID(intSourceTipID)("WorkingDate"))
        Dim strDescription As String = Data.FileDataSet.Tips.FindByTipID(intSourceTipID)("Description").ToString

        With frmEditTip
            .TipAmount = decAmount
            .WorkingDate = dteWorkingDate
            .TipType = strDescription
            .m_dsParentDataSet = Data.FileDataSet
        End With

        If frmEditTip.ShowDialog <> Windows.Forms.DialogResult.OK Then
            frmEditTip.Dispose()
            Exit Sub
        End If

        Dim strFunction As String

        If frmEditTip.TipAmount = decAmount And frmEditTip.WorkingDate = dteWorkingDate And frmEditTip.TipType = strDescription Then
            frmEditTip.Dispose()
            Exit Sub
        End If

        Dim decNewAmount As Decimal = frmEditTip.TipAmount
        Dim strNewDescription As String = frmEditTip.TipType
        Dim dteNewDate As Date = frmEditTip.WorkingDate
        Dim strServerNumber As String = Data.FileDataSet.Tips.FindByTipID(intSourceTipID)("ServerNumber").ToString
        Dim strFirstName As String = Data.FileDataSet.Servers.FindByServerNumber(strServerNumber)("FirstName").ToString
        Dim strLastName As String = Data.FileDataSet.Servers.FindByServerNumber(strServerNumber)("LastName").ToString

        Dim drNewRow As DataRow = Data.FileDataSet.Tips.NewRow

        If frmEditTip.TipType = "Special Function" Then
            For Each row As DataRow In Data.FileDataSet.SpecialFunctions
                frmSelectFunction.cboFunctions.Items.Add(row("SpecialFunction").ToString)
            Next

            If frmSelectFunction.ShowDialog <> Windows.Forms.DialogResult.OK Then
                frmSelectFunction.Dispose()
                frmEditTip.Dispose()
                Exit Sub
            End If

            strFunction = frmSelectFunction.SelectedFunction
            dteNewDate = CDate(Data.FileDataSet.SpecialFunctions.FindBySpecialFunction(strFunction)("Date"))
        End If

        If frmEditTip.TipType = "Cash" Then
            dteNewDate = CDate(Data.FileDataSet.Settings.FindBySetting("PeriodEnd")("Value"))
        End If

        frmSelectFunction.Dispose()

        drNewRow("Amount") = decNewAmount
        drNewRow("ServerNumber") = strServerNumber
        drNewRow("FirstName") = strFirstName
        drNewRow("LastName") = strLastName
        drNewRow("Description") = strNewDescription
        If strNewDescription = "Special Function" Then
            drNewRow("SpecialFunction") = strFunction
        End If
        drNewRow("WorkingDate") = dteNewDate

        Data.FileDataSet.Tips.Rows.Add(drNewRow)
        Data.FileDataSet.Tips.FindByTipID(intSourceTipID).Delete()

        frmEditTip.Dispose()
        UpdateCCTotals()
        UpdateRCTotals()
        UpdateSFTotals()
        UpdateCATotals()
    End Sub
#End Region

    'Cash operations begin below:
#Region "CashOperations"
    Private Sub AddCashTip()
        If cboCAServer.SelectedIndex = -1 Then
            If cboCAServer.Text = "" Then
                MessageBox.Show("You must select the server this tip belongs to.", "Select Server", MessageBoxButtons.OK)
                cboCAServer.Focus()
                Exit Sub
            Else
                MessageBox.Show("The server name you entered was not found in the data file.  You must add the server before you can add the tip.", "Server Not Found", MessageBoxButtons.OK)
                cboCAServer.Text = ""
                cboCAServer.Focus()
                Exit Sub
            End If
        End If

        If Not IsNumeric(txtCAAmount.Text) Then
            MessageBox.Show("The tip amount must be a number.", "Invalid Entry", MessageBoxButtons.OK)
            txtCAAmount.Clear()
            txtCAAmount.Focus()
            Exit Sub
        End If

        If txtCAAmount.Text = "" Then
            MessageBox.Show("You must enter a tip amount.", "Invalid Entry", MessageBoxButtons.OK)
            txtCAAmount.Focus()
            Exit Sub
        End If

        AutoInsertCADecimal()

        Dim drNewRow As DataRow = Data.FileDataSet.Tips.NewRow

        drNewRow("Amount") = txtCAAmount.Text
        drNewRow("ServerNumber") = cboCAServer.SelectedValue.ToString
        drNewRow("FirstName") = Data.FileDataSet.Servers.FindByServerNumber(cboCAServer.SelectedValue.ToString)("FirstName").ToString
        drNewRow("LastName") = Data.FileDataSet.Servers.FindByServerNumber(cboCAServer.SelectedValue.ToString)("LastName").ToString
        drNewRow("Description") = "Cash"
        drNewRow("WorkingDate") = CDate(Data.FileDataSet.Settings.FindBySetting("PeriodEnd")("Value"))

        Data.FileDataSet.Tips.Rows.Add(drNewRow)
        UpdateCATotals()

        txtCAAmount.Clear()
        cboCAServer.SelectedIndex = -1
        cboCAServer.Focus()
    End Sub

    Private Sub UpdateCATotals()
        Dim decAmount As Decimal = 0

        For Each row As DataGridViewRow In Me.CashDataGridView.Rows
            decAmount += CDec(row.Cells.Item("CAAmount").Value)
        Next

        lblCATotal.Text = "Total: " & Format(decAmount, "c")
    End Sub

    Private Sub txtCAAmount_KeyPress(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyPressEventArgs) Handles txtCAAmount.KeyPress
        If e.KeyChar = ChrW(13) Then
            e.Handled = True
            AddCashTip()
        End If
    End Sub

    Private Sub AutoInsertCADecimal()
        If txtCAAmount.Text = "" Then Exit Sub

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
        If cboCAServer.SelectedIndex = -1 Then
            cboCAServer.Text = ""
        Else
            cboCAServer.SelectedIndex = -1
        End If
        cboCAServer.Focus()
    End Sub

    Private Sub mnuDeleteCATip_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles mnuDeleteCATip.Click
        If Me.CashDataGridView.Rows.Count = 0 Then Exit Sub

        Dim intTipID As Integer = CInt(Me.CashDataGridView.Item("CAID", Me.CashTipsBindingSource.Position).Value)

        Dim strTipAmount As String = Data.FileDataSet.Tips.FindByTipID(intTipID)("Amount").ToString
        Dim strFirstName As String = Data.FileDataSet.Tips.FindByTipID(intTipID)("FirstName").ToString
        Dim strLastName As String = Data.FileDataSet.Tips.FindByTipID(intTipID)("LastName").ToString

        If MessageBox.Show("Are you sure you want to delete this $" & strTipAmount & " tip for " &
        strFirstName & " " & strLastName & "?", "Confirm Delete", MessageBoxButtons.YesNo) <> Windows.Forms.DialogResult.Yes Then
            Exit Sub
        End If

        Data.FileDataSet.Tips.FindByTipID(intTipID).Delete()
        UpdateCATotals()
    End Sub

    Private Sub mnuReassignCATip_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles mnuReassignCATip.Click
        If Me.CashDataGridView.Rows.Count = 0 Then Exit Sub

        frmSelectServer.m_dsParentDataSet = Data.FileDataSet
        frmSelectServer.lblSelectServer.Text = "Select the tip recipient:"

        If frmSelectServer.ShowDialog <> Windows.Forms.DialogResult.OK Then
            frmSelectServer.Dispose()
            Exit Sub
        End If

        Dim strSourceServerNumber As String = Me.CashDataGridView.Item("CAServerNumber", Me.CashTipsBindingSource.Position).Value.ToString
        Dim intSourceTipID As Integer = CInt(Me.CashDataGridView.Item("CAID", Me.CashTipsBindingSource.Position).Value)

        Dim strDestServerNumber As String = frmSelectServer.ServerNumber
        Dim strDestFirstName As String = Data.FileDataSet.Servers.FindByServerNumber(strDestServerNumber)("FirstName").ToString
        Dim strDestLastName As String = Data.FileDataSet.Servers.FindByServerNumber(strDestServerNumber)("LastName").ToString

        If strSourceServerNumber = strDestServerNumber Then
            MessageBox.Show("The selected tip cannot be reassigned to its original owner.", "Invalid Selection", MessageBoxButtons.OK)
            Exit Sub
        End If

        Dim decAmount As Decimal = CDec(Data.FileDataSet.Tips.FindByTipID(intSourceTipID)("Amount"))
        Dim strDescription As String = Data.FileDataSet.Tips.FindByTipID(intSourceTipID)("Description").ToString
        Dim strSpecialFunction As String
        If Not IsDBNull(Data.FileDataSet.Tips.FindByTipID(intSourceTipID)("SpecialFunction")) Then
            strSpecialFunction = Data.FileDataSet.Tips.FindByTipID(intSourceTipID)("SpecialFunction").ToString
        End If
        Dim dteWorkingDate As Date = CDate(Data.FileDataSet.Tips.FindByTipID(intSourceTipID)("WorkingDate"))

        Dim drNewRow As DataRow = Data.FileDataSet.Tips.NewRow

        drNewRow("Amount") = decAmount
        drNewRow("ServerNumber") = strDestServerNumber
        drNewRow("FirstName") = strDestFirstName
        drNewRow("LastName") = strDestLastName
        drNewRow("Description") = strDescription
        If Not IsDBNull(Data.FileDataSet.Tips.FindByTipID(intSourceTipID)("SpecialFunction")) Then
            drNewRow("SpecialFunction") = strSpecialFunction
        End If
        drNewRow("WorkingDate") = dteWorkingDate

        Data.FileDataSet.Tips.Rows.Add(drNewRow)

        Data.FileDataSet.Tips.FindByTipID(intSourceTipID).Delete()

        frmSelectServer.Dispose()
    End Sub

    Private Sub mnuEditCATip_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles mnuEditCATip.Click, CashDataGridView.DoubleClick
        If Me.CashDataGridView.Rows.Count = 0 Then Exit Sub

        Dim intSourceTipID As Integer = CInt(Me.CashDataGridView.Item("CAID", Me.CashTipsBindingSource.Position).Value)

        Dim decAmount As Decimal = CDec(Data.FileDataSet.Tips.FindByTipID(intSourceTipID)("Amount"))
        Dim dteWorkingDate As Date = CDate(Data.FileDataSet.Tips.FindByTipID(intSourceTipID)("WorkingDate"))
        Dim strDescription As String = Data.FileDataSet.Tips.FindByTipID(intSourceTipID)("Description").ToString

        With frmEditTip
            .TipAmount = decAmount
            .WorkingDate = dteWorkingDate
            .TipType = strDescription
            .m_dsParentDataSet = Data.FileDataSet
        End With

        If frmEditTip.ShowDialog <> Windows.Forms.DialogResult.OK Then
            frmEditTip.Dispose()
            Exit Sub
        End If

        Dim strFunction As String

        If frmEditTip.TipAmount = decAmount And frmEditTip.WorkingDate = dteWorkingDate And frmEditTip.TipType = strDescription Then
            frmEditTip.Dispose()
            Exit Sub
        End If

        Dim decNewAmount As Decimal = frmEditTip.TipAmount
        Dim strNewDescription As String = frmEditTip.TipType
        Dim dteNewDate As Date = frmEditTip.WorkingDate
        Dim strServerNumber As String = Data.FileDataSet.Tips.FindByTipID(intSourceTipID)("ServerNumber").ToString
        Dim strFirstName As String = Data.FileDataSet.Servers.FindByServerNumber(strServerNumber)("FirstName").ToString
        Dim strLastName As String = Data.FileDataSet.Servers.FindByServerNumber(strServerNumber)("LastName").ToString

        Dim drNewRow As DataRow = Data.FileDataSet.Tips.NewRow

        If frmEditTip.TipType = "Special Function" Then
            For Each row As DataRow In Data.FileDataSet.SpecialFunctions
                frmSelectFunction.cboFunctions.Items.Add(row("SpecialFunction").ToString)
            Next

            If frmSelectFunction.ShowDialog <> Windows.Forms.DialogResult.OK Then
                frmSelectFunction.Dispose()
                frmEditTip.Dispose()
                Exit Sub
            End If

            strFunction = frmSelectFunction.SelectedFunction
            dteNewDate = CDate(Data.FileDataSet.SpecialFunctions.FindBySpecialFunction(strFunction)("Date"))
        End If

        If frmEditTip.TipType = "Cash" Then
            dteNewDate = CDate(Data.FileDataSet.Settings.FindBySetting("PeriodEnd")("Value"))
        End If

        frmSelectFunction.Dispose()

        drNewRow("Amount") = decNewAmount
        drNewRow("ServerNumber") = strServerNumber
        drNewRow("FirstName") = strFirstName
        drNewRow("LastName") = strLastName
        drNewRow("Description") = strNewDescription
        If strNewDescription = "Special Function" Then
            drNewRow("SpecialFunction") = strFunction
        End If
        drNewRow("WorkingDate") = dteNewDate

        Data.FileDataSet.Tips.Rows.Add(drNewRow)
        Data.FileDataSet.Tips.FindByTipID(intSourceTipID).Delete()

        frmEditTip.Dispose()
        UpdateCCTotals()
        UpdateRCTotals()
        UpdateSFTotals()
        UpdateCATotals()
    End Sub

    Private Sub btnQuickAddCashTips_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnQuickAddCashTips.Click
        Dim dvServers As New DataView
        dvServers.Table = Data.FileDataSet.Servers
        dvServers.Sort = "LastName, FirstName"

        If dvServers.Count = 0 Then
            MessageBox.Show("There are no server in the file.", "No Servers", MessageBoxButtons.OK)
            Exit Sub

        Else
            For i As Integer = 0 To dvServers.Count - 1
                Dim strServerNumber As String = dvServers(i)("ServerNumber").ToString
                Dim strFirstName As String = dvServers(i)("FirstName").ToString
                Dim strLastName As String = dvServers(i)("LastName").ToString

                frmQuickAdd.txtServerName.Text = strLastName & ", " & strFirstName
                If frmQuickAdd.ShowDialog <> Windows.Forms.DialogResult.OK Then
                    frmQuickAdd.Dispose()
                    Exit Sub
                End If

                Dim decTipAmount As Decimal = CDec(frmQuickAdd.txtTipAmount.Text)

                If decTipAmount <> 0 Then
                    Dim drNewRow As DataRow = Data.FileDataSet.Tips.NewRow

                    drNewRow("Amount") = decTipAmount
                    drNewRow("ServerNumber") = strServerNumber
                    drNewRow("FirstName") = strFirstName
                    drNewRow("LastName") = strLastName
                    drNewRow("Description") = "Cash"
                    drNewRow("WorkingDate") = CDate(Data.FileDataSet.Settings.FindBySetting("PeriodEnd")("Value"))

                    Data.FileDataSet.Tips.Rows.Add(drNewRow)
                    UpdateCATotals()
                End If

                frmQuickAdd.Dispose()
            Next
        End If
    End Sub
#End Region

    'Special function operations begin below:
#Region "SpecialFunctionOperations"
    Private Sub AddSpecialFunctionTip()
        If cboSFServer.SelectedIndex = -1 Then
            If cboSFServer.Text = "" Then
                MessageBox.Show("You must select the server this tip belongs to.", "Select Server", MessageBoxButtons.OK)
                cboSFServer.Focus()
                Exit Sub
            Else
                MessageBox.Show("The server name you entered was not found in the data file.  You must add the server before you can add the tip.", "Server Not Found", MessageBoxButtons.OK)
                cboSFServer.Text = ""
                cboSFServer.Focus()
                Exit Sub
            End If
        End If

        If Not IsNumeric(txtSFAmount.Text) Then
            MessageBox.Show("The tip amount must be a number.", "Invalid Entry", MessageBoxButtons.OK)
            txtSFAmount.Clear()
            txtSFAmount.Focus()
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

        Dim drNewRow As DataRow = Data.FileDataSet.Tips.NewRow

        drNewRow("Amount") = txtSFAmount.Text
        drNewRow("ServerNumber") = cboSFServer.SelectedValue.ToString
        drNewRow("FirstName") = Data.FileDataSet.Servers.FindByServerNumber(cboSFServer.SelectedValue.ToString)("FirstName").ToString
        drNewRow("LastName") = Data.FileDataSet.Servers.FindByServerNumber(cboSFServer.SelectedValue.ToString)("LastName").ToString
        drNewRow("Description") = "Special Function"
        drNewRow("SpecialFunction") = cboSelectSpecialFunction.SelectedValue
        drNewRow("WorkingDate") = CDate(Data.FileDataSet.SpecialFunctions.FindBySpecialFunction(cboSelectSpecialFunction.SelectedValue.ToString)("Date"))

        Data.FileDataSet.Tips.Rows.Add(drNewRow)
        UpdateSFTotals()

        txtSFAmount.Clear()
        cboSFServer.SelectedIndex = -1
        cboSFServer.Focus()
    End Sub

    Private Sub UpdateSFTotals()
        Dim decAmount As Decimal = 0

        For Each row As DataGridViewRow In Me.SpecialFunctionDataGridView.Rows
            decAmount += CDec(row.Cells.Item("SFAmount").Value)
        Next

        lblSFTotal.Text = "Total: " & Format(decAmount, "c")
    End Sub

    Private Sub txtSFAmount_KeyPress(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyPressEventArgs) Handles txtSFAmount.KeyPress
        If e.KeyChar = ChrW(13) Then
            e.Handled = True
            AddSpecialFunctionTip()
        End If
    End Sub

    Private Sub AutoInsertSFDecimal()
        If txtSFAmount.Text = "" Then Exit Sub

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
        If cboSFServer.SelectedIndex = -1 Then
            cboSFServer.Text = ""
        Else
            cboSFServer.SelectedIndex = -1
        End If
        cboSFServer.Focus()
    End Sub

    Private Sub mnuDeleteSFTip_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles mnuDeleteSFTip.Click
        If Me.SpecialFunctionDataGridView.Rows.Count = 0 Then Exit Sub

        Dim intTipID As Integer = CInt(Me.SpecialFunctionDataGridView.Item("SFID", Me.SpecialFunctionTipsBindingSource.Position).Value)

        Dim strTipAmount As String = Data.FileDataSet.Tips.FindByTipID(intTipID)("Amount").ToString
        Dim strFirstName As String = Data.FileDataSet.Tips.FindByTipID(intTipID)("FirstName").ToString
        Dim strLastName As String = Data.FileDataSet.Tips.FindByTipID(intTipID)("LastName").ToString

        If MessageBox.Show("Are you sure you want to delete this $" & strTipAmount & " tip for " &
        strFirstName & " " & strLastName & "?", "Confirm Delete", MessageBoxButtons.YesNo) <> Windows.Forms.DialogResult.Yes Then
            Exit Sub
        End If

        Data.FileDataSet.Tips.FindByTipID(intTipID).Delete()
    End Sub

    Private Sub SpecialFunctionDataGridView_RowStateChanged(ByVal sender As Object, ByVal e As System.Windows.Forms.DataGridViewRowStateChangedEventArgs) Handles SpecialFunctionDataGridView.RowStateChanged
        UpdateSFTotals()
    End Sub

    Private Sub mnuReassignSFTip_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles mnuReassignSFTip.Click
        If Me.SpecialFunctionDataGridView.Rows.Count = 0 Then Exit Sub

        frmSelectServer.m_dsParentDataSet = Data.FileDataSet
        frmSelectServer.lblSelectServer.Text = "Select the tip recipient:"

        If frmSelectServer.ShowDialog <> Windows.Forms.DialogResult.OK Then
            frmSelectServer.Dispose()
            Exit Sub
        End If

        Dim strSourceServerNumber As String = Me.SpecialFunctionDataGridView.Item("SFServerNumber", Me.SpecialFunctionTipsBindingSource.Position).Value.ToString
        Dim intSourceTipID As Integer = CInt(Me.SpecialFunctionDataGridView.Item("SFID", Me.SpecialFunctionTipsBindingSource.Position).Value)

        Dim strDestServerNumber As String = frmSelectServer.ServerNumber
        Dim strDestFirstName As String = Data.FileDataSet.Servers.FindByServerNumber(strDestServerNumber)("FirstName").ToString
        Dim strDestLastName As String = Data.FileDataSet.Servers.FindByServerNumber(strDestServerNumber)("LastName").ToString

        If strSourceServerNumber = strDestServerNumber Then
            MessageBox.Show("The selected tip cannot be reassigned to its original owner.", "Invalid Selection", MessageBoxButtons.OK)
            Exit Sub
        End If

        Dim decAmount As Decimal = CDec(Data.FileDataSet.Tips.FindByTipID(intSourceTipID)("Amount"))
        Dim strDescription As String = Data.FileDataSet.Tips.FindByTipID(intSourceTipID)("Description").ToString
        Dim strSpecialFunction As String
        If Not IsDBNull(Data.FileDataSet.Tips.FindByTipID(intSourceTipID)("SpecialFunction")) Then
            strSpecialFunction = Data.FileDataSet.Tips.FindByTipID(intSourceTipID)("SpecialFunction").ToString
        End If
        Dim dteWorkingDate As Date = CDate(Data.FileDataSet.Tips.FindByTipID(intSourceTipID)("WorkingDate"))

        Dim drNewRow As DataRow = Data.FileDataSet.Tips.NewRow

        drNewRow("Amount") = decAmount
        drNewRow("ServerNumber") = strDestServerNumber
        drNewRow("FirstName") = strDestFirstName
        drNewRow("LastName") = strDestLastName
        drNewRow("Description") = strDescription
        If Not IsDBNull(Data.FileDataSet.Tips.FindByTipID(intSourceTipID)("SpecialFunction")) Then
            drNewRow("SpecialFunction") = strSpecialFunction
        End If
        drNewRow("WorkingDate") = dteWorkingDate

        Data.FileDataSet.Tips.Rows.Add(drNewRow)

        Data.FileDataSet.Tips.FindByTipID(intSourceTipID).Delete()

        frmSelectServer.Dispose()
    End Sub

    Private Sub mnuEditSFTip_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles mnuEditSFTip.Click, SpecialFunctionDataGridView.DoubleClick
        If Me.SpecialFunctionDataGridView.Rows.Count = 0 Then Exit Sub

        Dim intSourceTipID As Integer = CInt(Me.SpecialFunctionDataGridView.Item("SFID", Me.SpecialFunctionTipsBindingSource.Position).Value)

        Dim decAmount As Decimal = CDec(Data.FileDataSet.Tips.FindByTipID(intSourceTipID)("Amount"))
        Dim dteWorkingDate As Date = CDate(Data.FileDataSet.Tips.FindByTipID(intSourceTipID)("WorkingDate"))
        Dim strDescription As String = Data.FileDataSet.Tips.FindByTipID(intSourceTipID)("Description").ToString

        With frmEditTip
            .TipAmount = decAmount
            .WorkingDate = dteWorkingDate
            .TipType = strDescription
            .m_dsParentDataSet = Data.FileDataSet
        End With

        If frmEditTip.ShowDialog <> Windows.Forms.DialogResult.OK Then
            frmEditTip.Dispose()
            Exit Sub
        End If

        Dim strFunction As String

        If frmEditTip.TipAmount = decAmount And frmEditTip.WorkingDate = dteWorkingDate And frmEditTip.TipType = strDescription And frmEditTip.TipType <> "Special Function" Then
            frmEditTip.Dispose()
            Exit Sub
        End If

        Dim decNewAmount As Decimal = frmEditTip.TipAmount
        Dim strNewDescription As String = frmEditTip.TipType
        Dim dteNewDate As Date = frmEditTip.WorkingDate
        Dim strServerNumber As String = Data.FileDataSet.Tips.FindByTipID(intSourceTipID)("ServerNumber").ToString
        Dim strFirstName As String = Data.FileDataSet.Servers.FindByServerNumber(strServerNumber)("FirstName").ToString
        Dim strLastName As String = Data.FileDataSet.Servers.FindByServerNumber(strServerNumber)("LastName").ToString

        Dim drNewRow As DataRow = Data.FileDataSet.Tips.NewRow

        If frmEditTip.TipType = "Special Function" Then
            For Each row As DataRow In Data.FileDataSet.SpecialFunctions
                frmSelectFunction.cboFunctions.Items.Add(row("SpecialFunction").ToString)
            Next

            If frmSelectFunction.ShowDialog <> Windows.Forms.DialogResult.OK Then
                frmSelectFunction.Dispose()
                frmEditTip.Dispose()
                Exit Sub
            End If

            strFunction = frmSelectFunction.SelectedFunction
            dteNewDate = CDate(Data.FileDataSet.SpecialFunctions.FindBySpecialFunction(strFunction)("Date"))
        End If

        If frmEditTip.TipType = "Cash" Then
            dteNewDate = CDate(Data.FileDataSet.Settings.FindBySetting("PeriodEnd")("Value"))
        End If

        frmSelectFunction.Dispose()

        drNewRow("Amount") = decNewAmount
        drNewRow("ServerNumber") = strServerNumber
        drNewRow("FirstName") = strFirstName
        drNewRow("LastName") = strLastName
        drNewRow("Description") = strNewDescription
        If strNewDescription = "Special Function" Then
            drNewRow("SpecialFunction") = strFunction
        End If
        drNewRow("WorkingDate") = dteNewDate

        Data.FileDataSet.Tips.Rows.Add(drNewRow)
        Data.FileDataSet.Tips.FindByTipID(intSourceTipID).Delete()

        frmEditTip.Dispose()
        UpdateCCTotals()
        UpdateRCTotals()
        UpdateSFTotals()
        UpdateCATotals()
    End Sub
#End Region

    'End of tip operations

    Private Sub mnuManageSpecialFunctions_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles mnuManageSpecialFunctions.Click
        With frmManageSpecialFunctions
            .m_dsParentDataSet = Data.FileDataSet
            .ShowDialog()
            .Dispose()
        End With

        Me.SpecialFunctionTipsBindingSource.Filter = "Description = 'Special Function'"
        Me.cboSelectSpecialFunction.SelectedIndex = -1
    End Sub

    Private Sub cboSelectSpecialFunction_SelectedIndexChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles cboSelectSpecialFunction.SelectedIndexChanged
        If cboSelectSpecialFunction.SelectedIndex = -1 Then
            Try
                Me.SpecialFunctionTipsBindingSource.Filter = "Description = 'Special Function'"
            Catch ex As Exception
            End Try

            Exit Sub
        End If

        Dim strSpecialFunction As String = cboSelectSpecialFunction.SelectedValue.ToString
        'If Me.mnuSortByOrderEntered.Checked Then
        Me.SpecialFunctionTipsBindingSource.Filter = "SpecialFunction = '" & strSpecialFunction & "'"
        Me.SpecialFunctionTipsBindingSource.Sort = "TipID"
        'Else
        '    Me.SpecialFunctionTipsBindingSource.Filter = "SpecialFunction = '" & strSpecialFunction & "'"
        '    Me.SpecialFunctionTipsBindingSource.Sort = "LastName"
        'End If

        cboSFServer.Focus()
    End Sub

    Private Sub mnuExportTips_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles mnuExportTips.Click
        frmExportTips.m_dsParentDataSet = Data.FileDataSet
        frmExportTips.ShowDialog()
        frmExportTips.Dispose()
    End Sub

    Private Sub mnuAddServer_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles mnuAddServer.Click
        Dim blnErrorState As Boolean = True

        Using frmAddEditServer As New frmAddEditServer()
            frmAddEditServer.Text = "Add Server"

            While blnErrorState = True
                If frmAddEditServer.ShowDialog <> DialogResult.OK Then Exit Sub

                If Not (Data.FileDataSet.Servers.FindByServerNumber(frmAddEditServer.ServerNumber) Is Nothing) Then
                    MessageBox.Show("The server number you entered already exists in the data file.  Please enter a different number.", "Invalid Entry", MessageBoxButtons.OK)
                    frmAddEditServer.ServerNumber = ""
                Else
                    blnErrorState = False
                End If
            End While

            Dim drNewRow As DataRow = Data.FileDataSet.Servers.NewRow

            drNewRow("ServerNumber") = frmAddEditServer.ServerNumber
            drNewRow("FirstName") = frmAddEditServer.FirstName
            drNewRow("LastName") = frmAddEditServer.LastName
            drNewRow("SuppressChit") = frmAddEditServer.SuppressChit

            Data.FileDataSet.Servers.Rows.Add(drNewRow)

            Dim frmMain As frmMain = DirectCast(MdiParent, frmMain)

            If frmMain.IsServerInTemplate(frmAddEditServer.ServerNumber) Then Exit Sub

            If MessageBox.Show("This server does not exist in the servers template.  Add the server to the template?",
                "Add Server", MessageBoxButtons.YesNo) = DialogResult.Yes Then
                frmMain.AddServerToTemplate(frmAddEditServer.ServerNumber, frmAddEditServer.FirstName, frmAddEditServer.LastName, frmAddEditServer.SuppressChit)
            End If
        End Using

        LoadServerCombos()
    End Sub

    Private Sub mnuEditSelectedServer_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles mnuEditSelectedServer.Click, ServersDataGridView.DoubleClick
        Dim strServerNumber As String = Me.ServersDataGridView.Item("ServersServerNumber", Me.ServersBindingSource.Position).Value.ToString
        Dim strFirstName As String = Data.FileDataSet.Servers.FindByServerNumber(strServerNumber)("FirstName").ToString
        Dim strLastName As String = Data.FileDataSet.Servers.FindByServerNumber(strServerNumber)("LastName").ToString
        Dim blnSuppressChit As Boolean = CBool(Data.FileDataSet.Servers.FindByServerNumber(strServerNumber)("SuppressChit"))

        With frmAddEditServer
            .Text = "Edit Server"
            .txtServerNumber.ReadOnly = True
            .txtServerNumber.TabStop = False
            .ServerNumber = strServerNumber
            .FirstName = strFirstName
            .LastName = strLastName
            .SuppressChit = blnSuppressChit

            If .ShowDialog <> Windows.Forms.DialogResult.OK Then
                .Dispose()
                Exit Sub
            End If

            If .ServerNumber = strServerNumber And .FirstName = strFirstName And .LastName = strLastName And .SuppressChit = blnSuppressChit Then
                .Dispose()
                Exit Sub
            End If

            Data.FileDataSet.Servers.FindByServerNumber(strServerNumber)("FirstName") = .FirstName
            Data.FileDataSet.Servers.FindByServerNumber(strServerNumber)("LastName") = .LastName
            Data.FileDataSet.Servers.FindByServerNumber(strServerNumber)("SuppressChit") = .SuppressChit

            .Dispose()
        End With

        LoadServerCombos()
    End Sub

    Private Sub btnShowAllTips_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnShowAllTips.Click
        Me.SpecialFunctionTipsBindingSource.Filter = "Description = 'Special Function'"

        'If Me.mnuSortByOrderEntered.Checked Then
        Me.SpecialFunctionTipsBindingSource.Sort = "SpecialFunction, TipID"
        'Else
        'Me.SpecialFunctionTipsBindingSource.Sort = "SpecialFunction, LastName"
        'End If

        Me.cboSelectSpecialFunction.SelectedIndex = -1

        If cboSFServer.SelectedIndex <> -1 Then
            cboSFServer.SelectedIndex = -1
        Else
            cboSFServer.Text = ""
        End If
    End Sub

    Private Sub mnuMergeDuplicate_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles mnuMergeDuplicate.Click
        frmSelectServer.m_dsParentDataSet = Data.FileDataSet
        frmSelectServer.lblSelectServer.Text = "Select the merge recipient:"

        If frmSelectServer.ShowDialog <> Windows.Forms.DialogResult.OK Then
            frmSelectServer.Dispose()
            Exit Sub
        End If

        Dim strSourceServerNumber As String = Me.ServersDataGridView.Item("ServersServerNumber", Me.ServersBindingSource.Position).Value.ToString
        Dim strDestServerNumber As String = frmSelectServer.ServerNumber
        Dim strDestFirstName As String = Data.FileDataSet.Servers.FindByServerNumber(strDestServerNumber)("FirstName").ToString
        Dim strDestLastName As String = Data.FileDataSet.Servers.FindByServerNumber(strDestServerNumber)("LastName").ToString

        If strSourceServerNumber = strDestServerNumber Then
            MessageBox.Show("You may not merge a server with itself.", "Invalid Selection", MessageBoxButtons.OK)
            Exit Sub
        End If


        Dim dv As New DataView
        dv.Table = Data.FileDataSet.Tips
        dv.RowFilter = "ServerNumber = '" & strSourceServerNumber & "'"

        Dim drNewRow As DataRow

        For i As Integer = 0 To dv.Count - 1
            drNewRow = Data.FileDataSet.Tips.NewRow

            drNewRow("Amount") = CDec(dv(i)("Amount"))
            drNewRow("ServerNumber") = strDestServerNumber
            drNewRow("FirstName") = strDestFirstName
            drNewRow("LastName") = strDestLastName
            drNewRow("Description") = dv(i)("Description").ToString
            If Not IsDBNull(dv(i)("SpecialFunction")) Then
                drNewRow("SpecialFunction") = dv(i)("SpecialFunction").ToString
            End If
            drNewRow("WorkingDate") = CDate(dv(i)("WorkingDate"))

            Data.FileDataSet.Tips.Rows.Add(drNewRow)
        Next

        Data.FileDataSet.Servers.FindByServerNumber(strSourceServerNumber).Delete()
        Data.FileDataSet.Servers.AcceptChanges()

        MessageBox.Show("The merge was completed successfully.", "Message", MessageBoxButtons.OK, MessageBoxIcon.Information)

        LoadServerCombos()
        frmSelectServer.Dispose()
    End Sub

    Private Sub mnuCopyFromTemplate_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles mnuCopyFromTemplate.Click
        Data.FileDataSet.Servers.Merge(DirectCast(MdiParent, frmMain).GetTemplateServers())
    End Sub

    Private Sub mnuPrintTipChits_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles mnuPrintRegularTipChits.Click
        frmPrintRegularTipChits.m_dsParentDataset = Data.FileDataSet
        frmPrintRegularTipChits.ShowDialog()
        frmPrintRegularTipChits.Dispose()
    End Sub

    Private Sub btnManageFunctions_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnManageFunctions.Click
        With frmManageSpecialFunctions
            .m_dsParentDataSet = Data.FileDataSet
            .ShowDialog()
            .Dispose()
        End With

        Me.SpecialFunctionBindingSource.ResetBindings(False)
        Me.SpecialFunctionTipsBindingSource.Filter = "Description = 'Special Function'"
        Me.cboSelectSpecialFunction.SelectedIndex = -1

        If cboSFServer.SelectedIndex <> -1 Then
            cboSFServer.SelectedIndex = -1
        Else
            cboSFServer.Text = ""
        End If
    End Sub

    Private Sub mnuTipReports_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles mnuTipReports.Click
        frmPrintTipReportsV2.m_dsParentDataSet = Data.FileDataSet
        frmPrintTipReportsV2.ShowDialog()
        frmPrintTipReportsV2.Dispose()
    End Sub

    Private Sub mnuSpecialFunctionReports_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles mnuSpecialFunctionReports.Click
        frmPrintSpecialFunctionReportV2.m_dsParentDataSet = Data.FileDataSet
        frmPrintSpecialFunctionReportV2.ShowDialog()
        frmPrintSpecialFunctionReportV2.Dispose()
    End Sub

    Private Sub mnuPayrollBalancingReport_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles mnuPayrollBalancingReport.Click
        Dim docReport As New PrintDocument
        Dim dlgPrint As New PrintDialog

        With dlgPrint
            .AllowCurrentPage = False
            .AllowPrintToFile = False
            .AllowSelection = False
            .AllowSomePages = False
            .PrintToFile = False
            .ShowNetwork = True
            .UseEXDialog = False
        End With

        docReport.DocumentName = "Tip Report"
        With docReport.DefaultPageSettings.Margins
            .Top = 75
            .Bottom = 75
            .Left = 75
            .Right = 75
        End With

        AddHandler docReport.PrintPage, AddressOf PrintPayrollBalancingReport

        dlgPrint.Document = docReport

        If dlgPrint.ShowDialog() <> Windows.Forms.DialogResult.OK Then
            dlgPrint.Dispose()
            docReport = Nothing
            Exit Sub
        End If

        Try
            docReport.Print()
        Catch ex As Exception
            MessageBox.Show("Could not print the document.  Check that there is a printer installed and that a default printer has been selected.", "Cannot Print Document", MessageBoxButtons.OK)
        End Try

        dlgPrint.Dispose()

        docReport = Nothing
    End Sub

    Private Sub PrintPayrollBalancingReport(ByVal sender As Object, ByVal e As PrintPageEventArgs)
        Dim font As New System.Drawing.Font("Calibri", 12)
        Dim fontBold As New System.Drawing.Font("Calibri", 12, FontStyle.Bold)

        Dim fmt As New StringFormat(StringFormatFlags.LineLimit)

        'Initialize local static variables that contain the current line position and
        'the current page number.
        Dim intPosition As Single = e.MarginBounds.Top
        Dim intPageNumber As Integer = 1

        Const intLineSpacing As Integer = 18
        Const intExtraLineSpacing As Integer = 36
        Const intIndent As Integer = 425

        Dim intStrLen As Integer

        'Initialize local constants that contain the normal line spacing and padded 
        'line spacing (double spacing).
        Dim intPrintAreaHeight, intPrintAreaWidth, marginLeft, marginTop As Int32

        With e.MarginBounds
            ' Initialize local variables that contain the bounds of the printing 
            ' area rectangle.
            intPrintAreaHeight = .Height
            intPrintAreaWidth = .Width

            ' Initialize local variables to hold margin values that will serve
            ' as the X and Y coordinates for the upper left corner of the printing 
            ' area rectangle.
            marginLeft = .Left ' X coordinate
            marginTop = .Top ' Y coordinate
        End With

        'Draw the header to the page.  Header will be drawn on every page.
        e.Graphics.DrawString("Payroll Balancing Report", font, Brushes.Black, marginLeft, intPosition)
        intPosition += intLineSpacing

        e.Graphics.DrawString(Format(DateTime.Now, "MM/dd/yyyy") & " " & Format(DateTime.Now, "t"), font, Brushes.Black, marginLeft, intPosition)
        intPosition += intLineSpacing

        e.Graphics.DrawString("Page " & intPageNumber, font, Brushes.Black, marginLeft, intPosition)
        intPosition += intExtraLineSpacing

        Dim dvTips As New DataView

        dvTips.Table = Data.FileDataSet.Tips
        dvTips.Sort = "Description"

        Dim decCCTotal As Decimal = 0
        Dim decRCTotal As Decimal = 0
        Dim decSFTotal As Decimal = 0
        Dim decChargeTips As Decimal = 0
        Dim decCATotal As Decimal = 0

        Dim intCCs As Integer = 0
        Dim intRCs As Integer = 0
        Dim intSFs As Integer = 0
        Dim intCAs As Integer = 0

        For i As Integer = 0 To dvTips.Count - 1
            Dim strDescription As String = dvTips.Item(i)("Description").ToString
            Dim decAmount As Decimal = CDec(dvTips.Item(i)("Amount"))

            Select Case strDescription
                Case "Credit Card"
                    decCCTotal += decAmount
                    intCCs += 1
                Case "Room Charge"
                    decRCTotal += decAmount
                    intRCs += 1
                Case "Special Function"
                    decSFTotal += decAmount
                    intSFs += 1
                Case "Cash"
                    decCATotal += decAmount
                    intCAs += 1
            End Select
        Next

        decChargeTips = decCCTotal + decRCTotal + decSFTotal

        'Draw Credit Card total.
        e.Graphics.DrawString("Credit Card", font, Brushes.Black, marginLeft, intPosition)

        intStrLen = CInt(e.Graphics.MeasureString(intCCs.ToString, font, New SizeF(intPrintAreaWidth, intPrintAreaHeight), fmt,
            Len(intCCs.ToString), 1).Width)

        e.Graphics.DrawString(intCCs.ToString, font, Brushes.Black, marginLeft + intIndent - intStrLen, intPosition)

        Dim strCCTotal As String = Format(decCCTotal, "0.00")

        intStrLen = CInt(e.Graphics.MeasureString(strCCTotal, font, New SizeF(intPrintAreaWidth, intPrintAreaHeight), fmt,
            Len(intCCs.ToString), 1).Width)

        e.Graphics.DrawString(strCCTotal, font, Brushes.Black, marginLeft + intPrintAreaWidth - intStrLen, intPosition)

        intPosition += intLineSpacing

        'Draw Room Charge total.
        e.Graphics.DrawString("Room Charge", font, Brushes.Black, marginLeft, intPosition)

        intStrLen = CInt(e.Graphics.MeasureString(intRCs.ToString, font, New SizeF(intPrintAreaWidth, intPrintAreaHeight), fmt,
            Len(intRCs.ToString), 1).Width)

        e.Graphics.DrawString(intRCs.ToString, font, Brushes.Black, marginLeft + intIndent - intStrLen, intPosition)

        Dim strRCTotal As String = Format(decRCTotal, "0.00")

        intStrLen = CInt(e.Graphics.MeasureString(strRCTotal, font, New SizeF(intPrintAreaWidth, intPrintAreaHeight), fmt,
            Len(intRCs.ToString), 1).Width)

        e.Graphics.DrawString(strRCTotal, font, Brushes.Black, marginLeft + intPrintAreaWidth - intStrLen, intPosition)

        intPosition += intLineSpacing

        'Draw Special Function total.
        e.Graphics.DrawString("Special Function", font, Brushes.Black, marginLeft, intPosition)

        intStrLen = CInt(e.Graphics.MeasureString(intSFs.ToString, font, New SizeF(intPrintAreaWidth, intPrintAreaHeight), fmt,
            Len(intSFs.ToString), 1).Width)

        e.Graphics.DrawString(intSFs.ToString, font, Brushes.Black, marginLeft + intIndent - intStrLen, intPosition)

        Dim strSFTotal As String = Format(decSFTotal, "0.00")

        intStrLen = CInt(e.Graphics.MeasureString(strSFTotal, font, New SizeF(intPrintAreaWidth, intPrintAreaHeight), fmt,
            Len(intSFs.ToString), 1).Width)

        e.Graphics.DrawString(strSFTotal, font, Brushes.Black, marginLeft + intPrintAreaWidth - intStrLen, intPosition)

        intPosition += intExtraLineSpacing

        'Draw total charge tips.
        e.Graphics.DrawString("Total Charge Tips", fontBold, Brushes.Black, marginLeft, intPosition)

        Dim strCharges As String = CStr(intCCs + intRCs + intSFs)
        intStrLen = CInt(e.Graphics.MeasureString(strCharges, fontBold, New SizeF(intPrintAreaWidth, intPrintAreaHeight), fmt,
                    Len(intSFs.ToString), 1).Width)

        e.Graphics.DrawString(strCharges, fontBold, Brushes.Black, marginLeft + intIndent - intStrLen, intPosition)

        Dim strChargeTotal As String = Format(decChargeTips, "0.00")

        intStrLen = CInt(e.Graphics.MeasureString(strChargeTotal, fontBold, New SizeF(intPrintAreaWidth, intPrintAreaHeight), fmt,
            Len(intSFs.ToString), 1).Width)

        e.Graphics.DrawString(strChargeTotal, fontBold, Brushes.Black, marginLeft + intPrintAreaWidth - intStrLen, intPosition)

        intPosition += intExtraLineSpacing

        'Draw Cash total.
        e.Graphics.DrawString("Cash", font, Brushes.Black, marginLeft, intPosition)

        intStrLen = CInt(e.Graphics.MeasureString(intCAs.ToString, font, New SizeF(intPrintAreaWidth, intPrintAreaHeight), fmt,
            Len(intCAs.ToString), 1).Width)

        e.Graphics.DrawString(intCAs.ToString, font, Brushes.Black, marginLeft + intIndent - intStrLen, intPosition)

        Dim strCATotal As String = Format(decCATotal, "0.00")

        intStrLen = CInt(e.Graphics.MeasureString(strCATotal, font, New SizeF(intPrintAreaWidth, intPrintAreaHeight), fmt,
            Len(intCAs.ToString), 1).Width)

        e.Graphics.DrawString(strCATotal, font, Brushes.Black, marginLeft + intPrintAreaWidth - intStrLen, intPosition)

        intPosition += intExtraLineSpacing

        'Draw total tips.
        e.Graphics.DrawString("TOTAL TIPS", fontBold, Brushes.Black, marginLeft, intPosition)

        Dim strTotal As String = CStr(intCCs + intRCs + intSFs + intCAs)
        intStrLen = CInt(e.Graphics.MeasureString(strCharges, fontBold, New SizeF(intPrintAreaWidth, intPrintAreaHeight), fmt,
                    Len(intSFs.ToString), 1).Width)

        e.Graphics.DrawString(strTotal, fontBold, Brushes.Black, marginLeft + intIndent - intStrLen, intPosition)

        Dim strGrandTotal As String = Format(decChargeTips + decCATotal, "0.00")

        intStrLen = CInt(e.Graphics.MeasureString(strGrandTotal, fontBold, New SizeF(intPrintAreaWidth, intPrintAreaHeight), fmt,
            Len(intSFs.ToString), 1).Width)

        e.Graphics.DrawString(strGrandTotal, fontBold, Brushes.Black, marginLeft + intPrintAreaWidth - intStrLen, intPosition)

        intPosition += intLineSpacing

        e.HasMorePages = False
    End Sub

    Private Sub mnuAutoAddServers_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles mnuAutoAddServers.Click
        Dim blnError As Boolean = True
        Dim intSeed As Integer
        Dim blnSuppressChits As Boolean

        While blnError = True
            If frmAutoAddInput.ShowDialog <> Windows.Forms.DialogResult.OK Then
                frmAutoAddInput.Dispose()
                Exit Sub
            End If

            intSeed = CInt(frmAutoAddInput.Seed)
            blnSuppressChits = frmAutoAddInput.SuppressChits

            If Not IsNothing(Data.FileDataSet.Servers.FindByServerNumber(CStr(intSeed))) Then
                MessageBox.Show("The number you entered is already in use.  Please enter a different number.", "Invalid Entry", MessageBoxButtons.OK)
                frmAutoAddInput.Seed = ""
                Continue While
            End If

            blnError = False
        End While

        frmAutoAddInput.Dispose()

        Dim intCurrentServerNumber As Integer = intSeed

        Do
            If Not IsNothing(Data.FileDataSet.Servers.FindByServerNumber(CStr(intCurrentServerNumber))) Then
                Do Until IsNothing(Data.FileDataSet.Servers.FindByServerNumber(CStr(intCurrentServerNumber)))
                    intCurrentServerNumber += 1
                Loop
            End If

            frmAutoAddServers.ServerNumber = CStr(intCurrentServerNumber)
            frmAutoAddServers.SuppressChits = blnSuppressChits

            If frmAutoAddServers.ShowDialog <> Windows.Forms.DialogResult.OK Then
                frmAutoAddServers.Dispose()
                Exit Do
            End If

            Dim strServerNumber As String = frmAutoAddServers.ServerNumber
            Dim strFirstName As String = frmAutoAddServers.FirstName
            Dim strLastName As String = frmAutoAddServers.LastName
            blnSuppressChits = frmAutoAddServers.SuppressChits

            frmAutoAddServers.Dispose()

            Dim drNewRow As DataRow = Data.FileDataSet.Servers.NewRow

            drNewRow("ServerNumber") = strServerNumber
            drNewRow("FirstName") = strFirstName
            drNewRow("LastName") = strLastName
            drNewRow("SuppressChit") = blnSuppressChits

            Data.FileDataSet.Servers.Rows.Add(drNewRow)
        Loop

        LoadServerCombos()
    End Sub

    Private Sub mnuOptimizeFile_Click(ByVal sender As Object, ByVal e As EventArgs) Handles mnuOptimizeFile.Click
        If MessageBox.Show("This function will compact credit card, room charge, and cash tips so that there is only one entry per" &
        " server per day.  It will also remove all servers who do not have tips.  It is recommended that this function" &
        " only be performed after the pay period is balanced and all reports have been printed.  Optimization may take several" &
        " minutes depending on the number of tips and servers in the file.  Do you wish to continue?", "Optimize File",
        MessageBoxButtons.YesNo) <> DialogResult.Yes Then
            Exit Sub
        End If

        Cursor.Current = Cursors.WaitCursor

        Dim dvServers, dvtips As New DataView
        dvServers.Table = Data.FileDataSet.Servers

        Dim intServer As Integer = 0

        Do Until intServer = dvServers.Count
            lblInfo.Visible = True
            lblInfo.Text = "Checking " & dvServers.Item(intServer)("FirstName").ToString & " " & dvServers.Item(intServer)("LastName").ToString

            dvtips.Table = Data.FileDataSet.Tips
            dvtips.RowFilter = "ServerNumber = '" & dvServers.Item(intServer)("ServerNumber").ToString & "'"

            If dvtips.Count = 0 Then
                dvServers.Item(intServer).Delete()
                Continue Do
            End If
            intServer += 1
        Loop

        CompactTips()
        Cursor.Current = Cursors.Default
        lblInfo.Visible = False
        dvServers.Dispose()
        dvtips.Dispose()
    End Sub

    Private Sub CompactTips()
        Dim dvTips As New DataView
        dvTips.Table = Data.FileDataSet.Tips

        Dim dteDate As Date = CDate(Data.FileDataSet.Settings.FindBySetting("PeriodStart")("Value"))

        Do Until dteDate > CDate(Data.FileDataSet.Settings.FindBySetting("PeriodEnd")("Value"))
            dvTips.RowFilter = "WorkingDate = '" & Format(dteDate, "MM/dd/yyyy") & "' AND Description <> 'Special Function'"
            dvTips.Sort = "ServerNumber, Description"

            Dim intTip As Integer = 0

            Do Until intTip = dvTips.Count
                If intTip <> dvTips.Count - 1 Then
                    Dim strThisServer As String = dvTips.Item(intTip)("ServerNumber").ToString
                    Dim strThisDescription As String = dvTips.Item(intTip)("Description").ToString
                    Dim strNextServer As String = dvTips.Item(intTip + 1)("ServerNumber").ToString
                    Dim strNextDescription As String = dvTips.Item(intTip + 1)("Description").ToString
                    Dim decTotal As Decimal = CDec(dvTips.Item(intTip)("Amount"))

                    If strThisServer = strNextServer And strThisDescription = strNextDescription Then
                        decTotal += CDec(dvTips.Item(intTip + 1)("Amount"))
                        dvTips.Item(intTip + 1).Delete()

                        dvTips.Item(intTip)("Amount") = decTotal
                        Continue Do
                    End If
                End If
                intTip += 1
            Loop
            dteDate = DateAdd(DateInterval.Day, 1, dteDate)
        Loop

        dvTips.Dispose()
    End Sub

    Private Sub ServersDataGridView_CellMouseDown(ByVal sender As Object, ByVal e As System.Windows.Forms.DataGridViewCellMouseEventArgs) Handles ServersDataGridView.CellMouseDown
        If e.ColumnIndex >= 0 And e.RowIndex >= 0 Then
            Me.ServersDataGridView.CurrentCell = Me.ServersDataGridView.Rows(e.RowIndex).Cells(e.ColumnIndex)
        End If
    End Sub

    Private Sub CreditCardDataGridView_CellMouseDown(ByVal sender As Object, ByVal e As System.Windows.Forms.DataGridViewCellMouseEventArgs) Handles CreditCardDataGridView.CellMouseDown
        If e.ColumnIndex >= 0 And e.RowIndex >= 0 Then
            Me.CreditCardDataGridView.CurrentCell = Me.CreditCardDataGridView.Rows(e.RowIndex).Cells(e.ColumnIndex)
        End If
    End Sub

    Private Sub RoomChargeDataGridView_CellMouseDown(ByVal sender As Object, ByVal e As System.Windows.Forms.DataGridViewCellMouseEventArgs) Handles RoomChargeDataGridView.CellMouseDown
        If e.ColumnIndex >= 0 And e.RowIndex >= 0 Then
            Me.RoomChargeDataGridView.CurrentCell = Me.RoomChargeDataGridView.Rows(e.RowIndex).Cells(e.ColumnIndex)
        End If
    End Sub

    Private Sub CashDataGridView_CellMouseDown(ByVal sender As Object, ByVal e As System.Windows.Forms.DataGridViewCellMouseEventArgs) Handles CashDataGridView.CellMouseDown
        If e.ColumnIndex >= 0 And e.RowIndex >= 0 Then
            Me.CashDataGridView.CurrentCell = Me.CashDataGridView.Rows(e.RowIndex).Cells(e.ColumnIndex)
        End If
    End Sub

    Private Sub SpecialFunctionDataGridView_CellMouseDown(ByVal sender As Object, ByVal e As System.Windows.Forms.DataGridViewCellMouseEventArgs) Handles SpecialFunctionDataGridView.CellMouseDown
        If e.ColumnIndex >= 0 And e.RowIndex >= 0 Then
            Me.SpecialFunctionDataGridView.CurrentCell = Me.SpecialFunctionDataGridView.Rows(e.RowIndex).Cells(e.ColumnIndex)
        End If
    End Sub

End Class