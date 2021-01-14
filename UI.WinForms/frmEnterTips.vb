Imports System.Drawing.Printing
Imports System.IO
Imports TipTracker.Common.Data.PayPeriod
Imports TipTracker.Core
Imports System.Linq
Imports TipTracker.Utilities

Public Class frmEnterTips
    Public ReadOnly Property File As PayPeriodFile
    Public ReadOnly Property Data As PayPeriodData
    
    Private ReadOnly _totalLabelLookup As Dictionary(Of TipTypes, Label)
    
    Public Sub New(file As PayPeriodFile, data As PayPeriodData)
        InitializeComponent()

        _totalLabelLookup = New Dictionary(Of TipTypes, Label) From { _
            {TipTypes.CreditCard, lblCCTotal}    , _
            {TipTypes.RoomCharge, lblRCTotal}, _
            {TipTypes.Cash, lblCATotal}, _
            {TipTypes.SpecialFunction, lblSFTotal}}

        Me.File = file
        Me.Data = data
        FileDataSet = data.FileDataSet
        Text = Path.GetFileNameWithoutExtension(file.FilePath)
    End Sub

    Private Function GetServers() As List(Of Server)
        Return Data.FileDataSet.Servers _
            .AsEnumerable() _
            .Select(Function(r) New Server() With {
                .PosId = r("ServerNumber").ToString(),
                .FirstName = r("FirstName").ToString(),
                .LastName = r("LastName").ToString(),
                .SuppressChit = CBool(r("SuppressChit"))}) _
            .ToList()
    End Function

    Private Sub frmEnterTips_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        'Set the form as an mdi child of frmMain.
        MdiParent = frmMain
        lblSystemDate.Text = "System Date: " & Format(Date.Today, "M/d/yyyy")
        WindowState = FormWindowState.Maximized

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
        ServersBindingSource.Sort = "LastName"

        LoadServerCombos()

        'Update the date labels to show the dates from the settings table.
        UpdateDateLabels()

        'Set the tips binding source sort and filter modes.
        SetSelectionFilters()
        cboSelectSpecialFunction.SelectedIndex = -1

        'Since the tab control doesn't like to focus on the first child control on it's own,
        'the child control needs to be selected manually.
        txtCCServerNumber.Focus()

    End Sub

    Private Sub LoadServerCombos()
        'Clear the server lookup dataset if there are any records already in it.
        ServersLookupDataset.Clear()
        ServersLookupDataset.AcceptChanges()

        'Populate the server lookup data table.
        Dim dvTemp As New DataView

        dvTemp.Table = Data.FileDataSet.Servers
        dvTemp.Sort = "LastName, FirstName"

        If dvTemp.Count <> 0 Then
            For i = 0 To dvTemp.Count - 1
                Dim strServerNumber As String = dvTemp(i)("ServerNumber").ToString
                Dim strFirstName As String = dvTemp(i)("FirstName").ToString
                Dim strLastName As String = dvTemp(i)("LastName").ToString

                'Build name string with LastName, FirstName format.
                Dim drNewRow As DataRow = ServersLookupDataset.Servers.NewRow

                drNewRow("ServerNumber") = strServerNumber
                drNewRow("NameString") = strLastName & ", " & strFirstName

                ServersLookupDataset.Servers.Rows.Add(drNewRow)

                'Build name string with FirstName LastName format.
                drNewRow = ServersLookupDataset.Servers.NewRow

                drNewRow("ServerNumber") = strServerNumber
                drNewRow("NameString") = strFirstName & " " & strLastName

                ServersLookupDataset.Servers.Rows.Add(drNewRow)

                'Build name string with server number included.
                drNewRow = ServersLookupDataset.Servers.NewRow

                drNewRow("ServerNumber") = strServerNumber
                drNewRow("NameString") = strServerNumber & " " & strLastName & ", " & strFirstName

                ServersLookupDataset.Servers.Rows.Add(drNewRow)
            Next

            ServersLookupDataset.AcceptChanges()
            cboCAServer.SelectedIndex = -1
            cboSFServer.SelectedIndex = -1
        End If
    End Sub

    Private Sub UpdateDateLabels()
        Dim dtePeriodStart = CDate(Data.FileDataSet.Settings.FindBySetting("PeriodStart")("Value"))
        Dim dtePeriodEnd = CDate(Data.FileDataSet.Settings.FindBySetting("PeriodEnd")("Value"))
        Dim dteWorkingDate = CDate(Data.FileDataSet.Settings.FindBySetting("WorkingDate")("Value"))

        lblPeriodStart.Text = "Period Start: " & Format(dtePeriodStart, "M/d/yyyy")
        lblPeriodEnd.Text = "Period End: " & Format(dtePeriodEnd, "M/d/yyyy")
        lblWorkingDate.Text = "Working Date: " & Format(dteWorkingDate, "M/d/yyyy")
    End Sub

    Private Sub SetSelectionFilters()
        Dim strWorkingDate As String = Format(CDate(Data.FileDataSet.Settings.FindBySetting("WorkingDate")("Value")), "M/d/yyyy")
        CreditCardTipsBindingSource.Filter = "Description = 'Credit Card' AND WorkingDate = '" & strWorkingDate & "'"
        CreditCardTipsBindingSource.Sort = "TipID"

        RoomChargeTipsBindingSource.Filter = "Description = 'Room Charge' AND WorkingDate = '" & strWorkingDate & "'"
        RoomChargeTipsBindingSource.Sort = "TipID"

        CashTipsBindingSource.Filter = "Description = 'Cash'"
        CashTipsBindingSource.Sort = "TipID"
        
        If cboSelectSpecialFunction.SelectedIndex <> -1 Then
            SpecialFunctionTipsBindingSource.Filter = "Description = 'Special Function' AND SpecialFunction = '" & cboSelectSpecialFunction.SelectedValue.ToString & "'"
            SpecialFunctionTipsBindingSource.Sort = "SpecialFunction, TipID"
        Else
            SpecialFunctionTipsBindingSource.Filter = "Description = 'Special Function'"
            SpecialFunctionTipsBindingSource.Sort = "SpecialFunction, TipID"
        End If

        SpecialFunctionBindingSource.Sort = "SpecialFunction"
        
        UpdateTotal(TipTypes.CreditCard)
        UpdateTotal(TipTypes.RoomCharge)
        UpdateTotal(TipTypes.Cash)
    End Sub

    Private Sub tabTipsTabControl_SelectedIndexChanged(sender As Object, e As EventArgs) Handles tabTipsTabControl.SelectedIndexChanged
        Dim sendingControl = DirectCast(sender, TabControl)
        Dim selectedTab  = sendingControl.SelectedTab
        
        selectedTab.SelectNextControl(selectedTab, true, true, true, true)

        Dim entryControls = selectedTab.Controls.Cast(Of Control)() _
            .Where(Function (c) (TypeOf c Is ComboBox) OrElse (TypeOf c Is TextBoxBase)) _
            .OrderBy(Function (c) c.TabIndex) _
            .ToArray()

        ResetEntryForm(entryControls)
        lblCurrentTipType.Text = "Editing " & tabTipsTabControl.SelectedTab.Text & " Tips"
    End Sub

    Private Sub btnFinalize_Click(sender As Object, e As EventArgs) Handles btnFinalize.Click
        Dim dteWorkingDate = CDate(Data.FileDataSet.Settings.FindBySetting("WorkingDate")("Value"))
        Dim dtePeriodEnd = CDate(Data.FileDataSet.Settings.FindBySetting("PeriodEnd")("Value"))

        If dteWorkingDate = dtePeriodEnd Then
            MessageBox.Show("The current working date is the last day in the pay period.  You cannot " &
            "advance the working date any further.  To work on tips for " &
            Format(DateAdd(DateInterval.Day, 1, dteWorkingDate), "M/d/yyyy") & " you must start a new file " &
            "for the new pay period.", "Cannot Change Working Date", MessageBoxButtons.OK)
            Exit Sub
        End If

        If MessageBox.Show("The working date will be changed to " & Format(DateAdd(DateInterval.Day, 1, dteWorkingDate), "M/d/yyyy") &
        ".  Do you wish to continue?", "Confirm Date Change", MessageBoxButtons.YesNo) <> DialogResult.Yes Then
            Exit Sub
        End If

        Data.FileDataSet.Settings.FindBySetting("WorkingDate")("Value") = DateAdd(DateInterval.Day, 1, dteWorkingDate)
        UpdateDateLabels()
        SetSelectionFilters()
    End Sub

    Private Sub btnSelectWorkingDate_Click(sender As Object, e As EventArgs) Handles btnSelectWorkingDate.Click
        Dim dtePeriodStart = CDate(Data.FileDataSet.Settings.FindBySetting("PeriodStart")("Value"))
        Dim dtePeriodEnd = CDate(Data.FileDataSet.Settings.FindBySetting("PeriodEnd")("Value"))
        Dim dteWorkingDate = CDate(Data.FileDataSet.Settings.FindBySetting("WorkingDate")("Value"))

        With frmSelectDate
            .MinDate = dtePeriodStart
            .MaxDate = dtePeriodEnd
            .CurrentDate = dteWorkingDate

            If .ShowDialog <> DialogResult.OK Then
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

    Private Function GetServerRow(serverTextBox As TextBox) As FileDataSet.ServersRow
        If String.IsNullOrEmpty(serverTextBox.Text) Then
            MessageBox.Show("You must enter a server number.", "Invalid Entry", MessageBoxButtons.OK)
            serverTextBox.Focus()
            Return Nothing
        End If

        Return Data.FileDataSet.Servers.FindByServerNumber(serverTextBox.Text)
    End Function

    Private Function GetServerRow(serverComboBox As ComboBox) As FileDataSet.ServersRow
        If serverComboBox.SelectedIndex = -1 Then
            If serverComboBox.Text = "" Then
                MessageBox.Show("You must select the server this tip belongs to.", "Select Server", MessageBoxButtons.OK)
                serverComboBox.Focus()
                Return Nothing
            Else
                MessageBox.Show("The server name you entered was not found in the data file.  You must add the server before you can add the tip.", "Server Not Found", MessageBoxButtons.OK)
                serverComboBox.Text = ""
                serverComboBox.Focus()
                Return Nothing
            End If
        End If

        Dim serverNumber = serverComboBox.SelectedValue.ToString()
        Return Data.FileDataSet.Servers.FindByServerNumber(serverNumber)
    End Function

    Private Function GetTipAmount(amountTextBox As TextBox) As Decimal?
        If String.IsNullOrEmpty(amountTextBox.Text) Then
            MessageBox.Show("You must enter a tip amount.", "Invalid Entry", MessageBoxButtons.OK)
            amountTextBox.Focus()
            Return Nothing
        End If

        If Not IsNumeric(amountTextBox.Text) Then
            MessageBox.Show("The tip amount must be a number.", "Invalid Entry", MessageBoxButtons.OK)
            amountTextBox.Clear()
            amountTextBox.Focus()
            Return Nothing
        End If

        Return If(amountTextBox.Text.Contains("."),
            CDec(amountTextBox.Text),
            CDec(amountTextBox.Text) / 100)
    End Function

    Private Function GetSelectedFunction(specialFunctionComboBox As ComboBox) As FileDataSet.SpecialFunctionsRow
        If specialFunctionComboBox.SelectedIndex = -1 Then
            MessageBox.Show("You must select a special function.", "Invalid Selection", MessageBoxButtons.OK)
            specialFunctionComboBox.Focus()
            Return Nothing
        End If

        Dim selectedFunction = specialFunctionComboBox.SelectedValue.ToString()
        Return FileDataSet.SpecialFunctions.FindBySpecialFunction(selectedFunction)
    End Function

    Private Sub AddTip(server As FileDataSet.ServersRow, tipAmount As Decimal, tipType As TipTypes,
        Optional specialFunction As FileDataSet.SpecialFunctionsRow = Nothing)
        Dim newTipRow = Data.FileDataSet.Tips.NewTipsRow()

        With newTipRow
            .Amount = tipAmount.ToString("0.00")
            .ServerNumber = server.ServerNumber
            .FirstName = server.FirstName
            .LastName = server.LastName
            .Description = tipType.Name

            If specialFunction IsNot Nothing Then
                .SpecialFunction = specialFunction.SpecialFunction
                .WorkingDate = specialFunction._Date
            ElseIf tipType Is TipTypes.Cash Then
                .WorkingDate = CDate(Data.FileDataSet.Settings.FindBySetting("PeriodEnd").Value)
            Else
                .WorkingDate = CDate(Data.FileDataSet.Settings.FindBySetting("WorkingDate").Value)
            End If
        End With

        Data.FileDataSet.Tips.AddTipsRow(newTipRow)
    End Sub

    Private Sub EditTip(bindingSource As BindingSource, sourceType As TipTypes)

        Dim selectedTip = GetSelectedTip(bindingSource)
        Dim periodStart  = Data.GetPayPeriodStart()
        Dim periodEnd  = Data.GetPayPeriodEnd()
        Dim workingDate  = Data.GetWorkingDate()
        Dim functions = Data.FileDataSet.SpecialFunctions _
            .AsEnumerable() _
            .Select(Function(f) f.SpecialFunction) _
            .ToList()
        Dim specialFunction = selectedTip.SpecialFunctionsRow?.SpecialFunction
        
        Using editTip As New frmEditTip(CDec(selectedTip.Amount), periodStart, periodEnd, workingDate, sourceType, functions, specialFunction)
            If editTip.ShowDialog() <> DialogResult.OK Then Return
            
            Dim newType = editTip.TipType

            If newType.CanSpecifyDate Then
                selectedTip.WorkingDate = editTip.WorkingDate
            ElseIf newType.IsEventOriginated then
                selectedTip.WorkingDate = Data.FileDataSet.SpecialFunctions.FindBySpecialFunction(editTip.SpecialFunction)._Date
                selectedTip.SpecialFunction = editTip.SpecialFunction
            Else
                selectedTip.WorkingDate = periodEnd
            End If

            If Not newType.IsEventOriginated Then selectedTip.SpecialFunctionsRow = Nothing

            selectedTip.Amount = editTip.Amount.ToString("0.00")
            selectedTip.Description = editTip.TipType.Name
            
            UpdateTotal(sourceType, specialFunction)

            If sourceType IsNot editTip.TipType Then UpdateTotal(editTip.TipType, editTip.SpecialFunction)
        End Using
    End Sub

    Private Sub UpdateTotal(tipType As TipTypes, Optional specialFunction As String = Nothing)
        Dim amountLabel = _totalLabelLookup(tipType)
        Dim isCandidateTip As Func(Of FileDataSet.TipsRow, Boolean) = Function(r) _
            r.RowState <> DataRowState.Deleted _
            AndAlso r.RowState <> DataRowState.Detached _
            AndAlso r.Description = tipType.Name
        Dim affectsTotal As Func(Of FileDataSet.TipsRow, Boolean)

        If tipType Is TipTypes.CreditCard OrElse tipType Is TipTypes.RoomCharge Then
            'Credit card and room charge tips are filtered date
            Dim workingDate = CDate(Data.FileDataSet.Settings.FindBySetting("WorkingDate").Value)
            affectsTotal = Function(r) r.WorkingDate = workingDate
        ElseIf tipType Is TipTypes.SpecialFunction AndAlso specialFunction IsNot Nothing Then
            'When a function is selected, only the tips for that function should be totaled.
            affectsTotal = Function(r) r.SpecialFunction = specialFunction
        Else
            'When no function is selected, all special function tips should be shown/totaled.
            'Cash tips apply for the entire pay period, so all cash tips should be totaled.
            affectsTotal = Function(r) True
        End If

        Dim dailyTotal = Data.FileDataSet.Tips _
            .AsEnumerable() _
            .Where(Function(r) isCandidateTip(r) AndAlso affectsTotal(r)) _
            .Select(Function(r) CDec(r.Amount)) _
            .Sum()

        amountLabel.Text = $"Total: {dailyTotal:c}"
    End Sub

    Private Sub ResetEntryForm(ParamArray formControls As Control())
        For Each control In formControls
            control.ResetText()
        Next

        formControls(0).Focus()
    End Sub

    Private Function ConfirmTipDeletion(tipRow As FileDataSet.TipsRow) As Boolean
        Return MessageBox.Show($"Delete ${tipRow.Amount} tip for {tipRow.FirstName} {tipRow.LastName}?",
            "Confirm Delete", MessageBoxButtons.YesNo) = DialogResult.Yes
    End Function

    Private Sub PerformTipDeletion(bindingSource As BindingSource, amountLabel As Label, tipType As TipTypes, Optional specialFunction As String = Nothing)
        Dim selectedRow As FileDataSet.TipsRow = GetSelectedTip(bindingSource)
        If Not ConfirmTipDeletion(selectedRow) Then Exit Sub

        selectedRow.Delete()
        UpdateTotal(tipType, specialFunction)
    End Sub

    Private Function GetSelectedTip(bindingSource As BindingSource) As FileDataSet.TipsRow
        Return DirectCast(DirectCast(bindingSource.Current, DataRowView).Row, FileDataSet.TipsRow)
    End Function

    'Credit card operations begin below:
#Region "CreditCardOperations"

    Private Sub txtCCServerNumber_LostFocus(sender As Object, e As EventArgs) Handles txtCCServerNumber.LostFocus
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

    Private Sub btnAddCC_Click(sender As Object, e As EventArgs) Handles btnAddCC.Click

        Dim server = GetServerRow(txtCCServerNumber)
        If server Is Nothing Then Exit Sub

        Dim amount = GetTipAmount(txtCCAmount)
        If amount Is Nothing Then Exit Sub

        AddTip(server, amount.Value, TipTypes.CreditCard)
        UpdateTotal(TipTypes.CreditCard)
        ResetEntryForm(txtCCServerNumber, txtCCAmount, txtCCServerName)
    End Sub

    Private Sub btnClearCC_Click(sender As Object, e As EventArgs) Handles btnClearCC.Click
        ResetEntryForm(txtCCAmount, txtCCServerNumber, txtCCServerName)
    End Sub

    Private Sub mnuDeleteCCTip_Click(sender As Object, e As EventArgs) Handles mnuDeleteCCTip.Click
        If CreditCardDataGridView.Rows.Count = 0 Then Exit Sub
        PerformTipDeletion(CreditCardTipsBindingSource, lblCCTotal, TipTypes.CreditCard)
    End Sub

    Private Sub txtCCServerName_GotFocus(sender As Object, e As EventArgs) Handles txtCCServerName.GotFocus
        txtCCServerNumber.Focus()
    End Sub

    Private Sub mnuReassignCCTip_Click(sender As Object, e As EventArgs) Handles mnuReassignCCTip.Click
        If CreditCardDataGridView.Rows.Count = 0 Then Exit Sub

        Dim tipRecipient As Server

        Using selectServer As New frmSelectServer("Select the tip recipient:", GetServers())
            If selectServer.ShowDialog() <> DialogResult.OK Then Exit Sub

            tipRecipient = selectServer.GetSelectedServer()
        End Using

        Dim strSourceServerNumber As String = CreditCardDataGridView.Item("CCServerNumber", CreditCardTipsBindingSource.Position).Value.ToString
        Dim intSourceTipID = CInt(CreditCardDataGridView.Item("CCID", CreditCardTipsBindingSource.Position).Value)

        Dim strDestServerNumber As String = tipRecipient.PosId
        Dim strDestFirstName As String = Data.FileDataSet.Servers.FindByServerNumber(strDestServerNumber)("FirstName").ToString
        Dim strDestLastName As String = Data.FileDataSet.Servers.FindByServerNumber(strDestServerNumber)("LastName").ToString

        If strSourceServerNumber = strDestServerNumber Then
            MessageBox.Show("The selected tip cannot be reassigned to its original owner.", "Invalid Selection", MessageBoxButtons.OK)
            Exit Sub
        End If

        Dim decAmount = CDec(Data.FileDataSet.Tips.FindByTipID(intSourceTipID)("Amount"))
        Dim strDescription As String = Data.FileDataSet.Tips.FindByTipID(intSourceTipID)("Description").ToString
        Dim strSpecialFunction = ""
        If Not IsDBNull(Data.FileDataSet.Tips.FindByTipID(intSourceTipID)("SpecialFunction")) Then
            strSpecialFunction = Data.FileDataSet.Tips.FindByTipID(intSourceTipID)("SpecialFunction").ToString
        End If
        Dim dteWorkingDate = CDate(Data.FileDataSet.Tips.FindByTipID(intSourceTipID)("WorkingDate"))

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


        UpdateTotal(TipTypes.CreditCard)
    End Sub
    
    Private Sub mnuEditCCTip_Click(sender As Object, e As EventArgs) Handles mnuEditCCTip.Click, CreditCardDataGridView.DoubleClick
        If CreditCardDataGridView.Rows.Count = 0 Then Exit Sub
        EditTip(CreditCardTipsBindingSource, TipTypes.CreditCard)
    End Sub
    
#End Region

    'Room charge operations begin below:
#Region "RoomChargeOperations"

    Private Sub txtRCServerNumber_LostFocus(sender As Object, e As EventArgs) Handles txtRCServerNumber.LostFocus
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

    Private Sub btnAddRC_Click(sender As Object, e As EventArgs) Handles btnAddRC.Click

        Dim server = GetServerRow(txtRCServerNumber)
        If server Is Nothing Then Exit Sub

        Dim amount = GetTipAmount(txtRCAmount)
        If amount Is Nothing Then Exit Sub

        AddTip(server, amount.Value, TipTypes.RoomCharge)
        UpdateTotal(TipTypes.RoomCharge)
        ResetEntryForm(txtRCServerNumber, txtRCAmount, txtRCServerName)
    End Sub

    Private Sub btnClearRC_Click(sender As Object, e As EventArgs) Handles btnClearRC.Click
        ResetEntryForm(txtRCServerNumber, txtRCAmount, txtRCServerName)
    End Sub

    Private Sub mnuDeleteRCTip_Click(sender As Object, e As EventArgs) Handles mnuDeleteRCTip.Click
        If RoomChargeDataGridView.Rows.Count = 0 Then Exit Sub
        PerformTipDeletion(RoomChargeTipsBindingSource, lblRCTotal, TipTypes.RoomCharge)
    End Sub

    Private Sub txtRCServerName_GotFocus(sender As Object, e As EventArgs) Handles txtRCServerName.GotFocus
        txtRCServerNumber.Focus()
    End Sub

    Private Sub mnuReassignRCTip_Click(sender As Object, e As EventArgs) Handles mnuReassignRCTip.Click
        If RoomChargeDataGridView.Rows.Count = 0 Then Exit Sub

        Dim tipRecipient As Server

        Using selectServer As New frmSelectServer("Select the tip recipient:", GetServers())
            If selectServer.ShowDialog() <> DialogResult.OK Then Exit Sub

            tipRecipient = selectServer.GetSelectedServer()
        End Using

        Dim strSourceServerNumber As String = RoomChargeDataGridView.Item("RCServerNumber", RoomChargeTipsBindingSource.Position).Value.ToString
        Dim intSourceTipID = CInt(RoomChargeDataGridView.Item("RCID", RoomChargeTipsBindingSource.Position).Value)

        Dim strDestServerNumber As String = tipRecipient.PosId
        Dim strDestFirstName As String = Data.FileDataSet.Servers.FindByServerNumber(strDestServerNumber)("FirstName").ToString
        Dim strDestLastName As String = Data.FileDataSet.Servers.FindByServerNumber(strDestServerNumber)("LastName").ToString

        If strSourceServerNumber = strDestServerNumber Then
            MessageBox.Show("The selected tip cannot be reassigned to its original owner.", "Invalid Selection", MessageBoxButtons.OK)
            Exit Sub
        End If

        Dim decAmount = CDec(Data.FileDataSet.Tips.FindByTipID(intSourceTipID)("Amount"))
        Dim strDescription As String = Data.FileDataSet.Tips.FindByTipID(intSourceTipID)("Description").ToString
        Dim strSpecialFunction As String
        If Not IsDBNull(Data.FileDataSet.Tips.FindByTipID(intSourceTipID)("SpecialFunction")) Then
            strSpecialFunction = Data.FileDataSet.Tips.FindByTipID(intSourceTipID)("SpecialFunction").ToString
        End If
        Dim dteWorkingDate = CDate(Data.FileDataSet.Tips.FindByTipID(intSourceTipID)("WorkingDate"))

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
    End Sub

    Private Sub mnuEditRCTip_Click(sender As Object, e As EventArgs) Handles mnuEditRCTip.Click, RoomChargeDataGridView.DoubleClick
        If RoomChargeDataGridView.Rows.Count = 0 Then Exit Sub
        EditTip(RoomChargeTipsBindingSource, TipTypes.RoomCharge)
    End Sub
#End Region

    'Cash operations begin below:
#Region "CashOperations"

    Private Sub btnAddCA_Click(sender As Object, e As EventArgs) Handles btnAddCA.Click

        Dim server = GetServerRow(cboCAServer)
        If server Is Nothing Then Exit Sub

        Dim amount = GetTipAmount(txtCAAmount)
        If amount Is Nothing Then Exit Sub

        AddTip(server, amount.Value, TipTypes.Cash)
        UpdateTotal(TipTypes.Cash)
        ResetEntryForm(cboCAServer, txtCAAmount)
    End Sub

    Private Sub btnClearCA_Click(sender As Object, e As EventArgs) Handles btnClearCA.Click
        ResetEntryForm(cboCAServer, txtCAAmount)
    End Sub

    Private Sub mnuDeleteCATip_Click(sender As Object, e As EventArgs) Handles mnuDeleteCATip.Click
        If CashDataGridView.Rows.Count = 0 Then Exit Sub
        PerformTipDeletion(CashTipsBindingSource, lblCATotal, TipTypes.Cash)
    End Sub

    Private Sub mnuReassignCATip_Click(sender As Object, e As EventArgs) Handles mnuReassignCATip.Click
        If CashDataGridView.Rows.Count = 0 Then Exit Sub

        Dim tipRecipient As Server

        Using selectServer As New frmSelectServer("Select the tip recipient:", GetServers())
            If selectServer.ShowDialog() <> DialogResult.OK Then Exit Sub

            tipRecipient = selectServer.GetSelectedServer()
        End Using

        Dim strSourceServerNumber As String = CashDataGridView.Item("CAServerNumber", CashTipsBindingSource.Position).Value.ToString
        Dim intSourceTipID = CInt(CashDataGridView.Item("CAID", CashTipsBindingSource.Position).Value)

        Dim strDestServerNumber As String = tipRecipient.PosId
        Dim strDestFirstName As String = Data.FileDataSet.Servers.FindByServerNumber(strDestServerNumber)("FirstName").ToString
        Dim strDestLastName As String = Data.FileDataSet.Servers.FindByServerNumber(strDestServerNumber)("LastName").ToString

        If strSourceServerNumber = strDestServerNumber Then
            MessageBox.Show("The selected tip cannot be reassigned to its original owner.", "Invalid Selection", MessageBoxButtons.OK)
            Exit Sub
        End If

        Dim decAmount = CDec(Data.FileDataSet.Tips.FindByTipID(intSourceTipID)("Amount"))
        Dim strDescription As String = Data.FileDataSet.Tips.FindByTipID(intSourceTipID)("Description").ToString
        Dim strSpecialFunction As String
        If Not IsDBNull(Data.FileDataSet.Tips.FindByTipID(intSourceTipID)("SpecialFunction")) Then
            strSpecialFunction = Data.FileDataSet.Tips.FindByTipID(intSourceTipID)("SpecialFunction").ToString
        End If
        Dim dteWorkingDate = CDate(Data.FileDataSet.Tips.FindByTipID(intSourceTipID)("WorkingDate"))

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
    End Sub

    Private Sub mnuEditCATip_Click(sender As Object, e As EventArgs) Handles mnuEditCATip.Click, CashDataGridView.DoubleClick
        If CashDataGridView.Rows.Count = 0 Then Exit Sub
        EditTip(CashTipsBindingSource, TipTypes.Cash)
    End Sub

    Private Sub btnQuickAddCashTips_Click(sender As Object, e As EventArgs) Handles btnQuickAddCashTips.Click
        Dim dvServers As New DataView
        dvServers.Table = Data.FileDataSet.Servers
        dvServers.Sort = "LastName, FirstName"

        If dvServers.Count = 0 Then
            MessageBox.Show("There are no server in the file.", "No Servers", MessageBoxButtons.OK)
            Exit Sub

        Else
            For i = 0 To dvServers.Count - 1
                Dim strServerNumber As String = dvServers(i)("ServerNumber").ToString
                Dim strFirstName As String = dvServers(i)("FirstName").ToString
                Dim strLastName As String = dvServers(i)("LastName").ToString

                frmQuickAdd.txtServerName.Text = strLastName & ", " & strFirstName
                If frmQuickAdd.ShowDialog <> DialogResult.OK Then
                    frmQuickAdd.Dispose()
                    Exit Sub
                End If

                Dim decTipAmount = CDec(frmQuickAdd.txtTipAmount.Text)

                If decTipAmount <> 0 Then
                    Dim drNewRow As DataRow = Data.FileDataSet.Tips.NewRow

                    drNewRow("Amount") = decTipAmount
                    drNewRow("ServerNumber") = strServerNumber
                    drNewRow("FirstName") = strFirstName
                    drNewRow("LastName") = strLastName
                    drNewRow("Description") = "Cash"
                    drNewRow("WorkingDate") = CDate(Data.FileDataSet.Settings.FindBySetting("PeriodEnd")("Value"))

                    Data.FileDataSet.Tips.Rows.Add(drNewRow)

                    UpdateTotal(TipTypes.Cash)
                End If

                frmQuickAdd.Dispose()
            Next
        End If
    End Sub
#End Region

    'Special function operations begin below:
#Region "SpecialFunctionOperations"
    Private Sub btnAddSF_Click(sender As Object, e As EventArgs) Handles btnAddSF.Click

        Dim specialFunction = GetSelectedFunction(cboSelectSpecialFunction)
        If specialFunction Is Nothing Then Exit Sub

        Dim server = GetServerRow(cboSFServer)
        If server Is Nothing Then Exit Sub

        Dim amount = GetTipAmount(txtSFAmount)
        If amount Is Nothing Then Exit Sub

        AddTip(server, amount.Value, TipTypes.SpecialFunction, specialFunction)
        UpdateTotal(TipTypes.SpecialFunction, specialFunction.SpecialFunction)
        ResetEntryForm(cboSFServer, txtSFAmount)
    End Sub

    Private Sub btnClearSF_Click(sender As Object, e As EventArgs) Handles btnClearSF.Click
        ResetEntryForm(cboSFServer, txtSFAmount)
    End Sub

    Private Sub mnuDeleteSFTip_Click(sender As Object, e As EventArgs) Handles mnuDeleteSFTip.Click
        If SpecialFunctionDataGridView.Rows.Count = 0 Then Exit Sub
        Dim specialFunction = cboSelectSpecialFunction.SelectedValue?.ToString()

        PerformTipDeletion(SpecialFunctionTipsBindingSource, lblSFTotal, TipTypes.SpecialFunction, specialFunction)
    End Sub

    Private Sub SpecialFunctionDataGridView_RowStateChanged(sender As Object, e As DataGridViewRowStateChangedEventArgs) Handles SpecialFunctionDataGridView.RowStateChanged

        UpdateTotal(TipTypes.SpecialFunction, cboSelectSpecialFunction.SelectedValue?.ToString())
    End Sub

    Private Sub mnuReassignSFTip_Click(sender As Object, e As EventArgs) Handles mnuReassignSFTip.Click
        If SpecialFunctionDataGridView.Rows.Count = 0 Then Exit Sub

        Dim tipRecipient As Server

        Using selectServer As New frmSelectServer("Select the tip recipient:", GetServers())
            If selectServer.ShowDialog() <> DialogResult.OK Then Exit Sub

            tipRecipient = selectServer.GetSelectedServer()
        End Using

        Dim strSourceServerNumber As String = SpecialFunctionDataGridView.Item("SFServerNumber", SpecialFunctionTipsBindingSource.Position).Value.ToString
        Dim intSourceTipID = CInt(SpecialFunctionDataGridView.Item("SFID", SpecialFunctionTipsBindingSource.Position).Value)

        Dim strDestServerNumber As String = tipRecipient.PosId
        Dim strDestFirstName As String = Data.FileDataSet.Servers.FindByServerNumber(strDestServerNumber)("FirstName").ToString
        Dim strDestLastName As String = Data.FileDataSet.Servers.FindByServerNumber(strDestServerNumber)("LastName").ToString

        If strSourceServerNumber = strDestServerNumber Then
            MessageBox.Show("The selected tip cannot be reassigned to its original owner.", "Invalid Selection", MessageBoxButtons.OK)
            Exit Sub
        End If

        Dim decAmount = CDec(Data.FileDataSet.Tips.FindByTipID(intSourceTipID)("Amount"))
        Dim strDescription As String = Data.FileDataSet.Tips.FindByTipID(intSourceTipID)("Description").ToString
        Dim strSpecialFunction As String
        If Not IsDBNull(Data.FileDataSet.Tips.FindByTipID(intSourceTipID)("SpecialFunction")) Then
            strSpecialFunction = Data.FileDataSet.Tips.FindByTipID(intSourceTipID)("SpecialFunction").ToString
        End If
        Dim dteWorkingDate = CDate(Data.FileDataSet.Tips.FindByTipID(intSourceTipID)("WorkingDate"))

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
    End Sub

    Private Sub mnuEditSFTip_Click(sender As Object, e As EventArgs) Handles mnuEditSFTip.Click, SpecialFunctionDataGridView.DoubleClick
        If SpecialFunctionDataGridView.Rows.Count = 0 Then Exit Sub
        EditTip(SpecialFunctionTipsBindingSource, TipTypes.SpecialFunction)
    End Sub
#End Region

    'End of tip operations

    Private Sub mnuManageSpecialFunctions_Click(sender As Object, e As EventArgs) Handles mnuManageSpecialFunctions.Click
        With frmManageSpecialFunctions
            .m_dsParentDataSet = Data.FileDataSet
            .ShowDialog()
            .Dispose()
        End With

        SpecialFunctionTipsBindingSource.Filter = "Description = 'Special Function'"
        cboSelectSpecialFunction.SelectedIndex = -1
    End Sub

    Private Sub cboSelectSpecialFunction_SelectedIndexChanged(sender As Object, e As EventArgs) Handles cboSelectSpecialFunction.SelectedIndexChanged
        If cboSelectSpecialFunction.SelectedIndex = -1 Then
            Try
                SpecialFunctionTipsBindingSource.Filter = "Description = 'Special Function'"
            Catch ex As Exception
            End Try

            Exit Sub
        End If

        Dim strSpecialFunction As String = cboSelectSpecialFunction.SelectedValue.ToString
        SpecialFunctionTipsBindingSource.Filter = "SpecialFunction = '" & strSpecialFunction & "'"
        SpecialFunctionTipsBindingSource.Sort = "TipID"
        cboSFServer.Focus()
    End Sub

    Private Sub mnuExportTips_Click(sender As Object, e As EventArgs) Handles mnuExportTips.Click
        frmExportTips.m_dsParentDataSet = Data.FileDataSet
        frmExportTips.ShowDialog()
        frmExportTips.Dispose()
    End Sub

    Private Sub mnuAddServer_Click(sender As Object, e As EventArgs) Handles mnuAddServer.Click
        Dim blnErrorState = True

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

            Dim frmMain = DirectCast(MdiParent, frmMain)

            If frmMain.IsServerInTemplate(frmAddEditServer.ServerNumber) Then Exit Sub

            If MessageBox.Show("This server does not exist in the servers template.  Add the server to the template?",
                "Add Server", MessageBoxButtons.YesNo) = DialogResult.Yes Then
                frmMain.AddServerToTemplate(frmAddEditServer.ServerNumber, frmAddEditServer.FirstName, frmAddEditServer.LastName, frmAddEditServer.SuppressChit)
            End If
        End Using

        LoadServerCombos()
    End Sub

    Private Sub mnuEditSelectedServer_Click(sender As Object, e As EventArgs) Handles mnuEditSelectedServer.Click, ServersDataGridView.DoubleClick
        Dim strServerNumber As String = ServersDataGridView.Item("ServersServerNumber", ServersBindingSource.Position).Value.ToString
        Dim strFirstName As String = Data.FileDataSet.Servers.FindByServerNumber(strServerNumber)("FirstName").ToString
        Dim strLastName As String = Data.FileDataSet.Servers.FindByServerNumber(strServerNumber)("LastName").ToString
        Dim blnSuppressChit = CBool(Data.FileDataSet.Servers.FindByServerNumber(strServerNumber)("SuppressChit"))

        With frmAddEditServer
            .Text = "Edit Server"
            .txtServerNumber.ReadOnly = True
            .txtServerNumber.TabStop = False
            .ServerNumber = strServerNumber
            .FirstName = strFirstName
            .LastName = strLastName
            .SuppressChit = blnSuppressChit

            If .ShowDialog <> DialogResult.OK Then
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

    Private Sub btnShowAllTips_Click(sender As Object, e As EventArgs) Handles btnShowAllTips.Click
        SpecialFunctionTipsBindingSource.Filter = "Description = 'Special Function'"
        SpecialFunctionTipsBindingSource.Sort = "SpecialFunction, TipID"
        cboSelectSpecialFunction.SelectedIndex = -1

        If cboSFServer.SelectedIndex <> -1 Then
            cboSFServer.SelectedIndex = -1
        Else
            cboSFServer.Text = ""
        End If
    End Sub

    Private Sub mnuMergeDuplicate_Click(sender As Object, e As EventArgs) Handles mnuMergeDuplicate.Click
        Dim mergeTarget As Server

        Using selectServer As New frmSelectServer("Select the merge recipient:", GetServers())
            If selectServer.ShowDialog() <> DialogResult.OK Then Exit Sub

            mergeTarget = selectServer.GetSelectedServer()
        End Using

        Dim strSourceServerNumber As String = ServersDataGridView.Item("ServersServerNumber", ServersBindingSource.Position).Value.ToString
        Dim strDestServerNumber As String = mergeTarget.PosId
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

        For i = 0 To dv.Count - 1
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
    End Sub

    Private Sub mnuCopyFromTemplate_Click(sender As Object, e As EventArgs) Handles mnuCopyFromTemplate.Click
        Data.FileDataSet.Servers.Merge(DirectCast(MdiParent, frmMain).GetTemplateServers())
    End Sub

    Private Sub mnuPrintTipChits_Click(sender As Object, e As EventArgs) Handles mnuPrintRegularTipChits.Click
        frmPrintRegularTipChits.m_dsParentDataset = Data.FileDataSet
        frmPrintRegularTipChits.ShowDialog()
        frmPrintRegularTipChits.Dispose()
    End Sub

    Private Sub btnManageFunctions_Click(sender As Object, e As EventArgs) Handles btnManageFunctions.Click
        With frmManageSpecialFunctions
            .m_dsParentDataSet = Data.FileDataSet
            .ShowDialog()
            .Dispose()
        End With

        SpecialFunctionBindingSource.ResetBindings(False)
        SpecialFunctionTipsBindingSource.Filter = "Description = 'Special Function'"
        cboSelectSpecialFunction.SelectedIndex = -1

        If cboSFServer.SelectedIndex <> -1 Then
            cboSFServer.SelectedIndex = -1
        Else
            cboSFServer.Text = ""
        End If
    End Sub

    Private Sub mnuTipReports_Click(sender As Object, e As EventArgs) Handles mnuTipReports.Click
        frmPrintTipReportsV2.m_dsParentDataSet = Data.FileDataSet
        frmPrintTipReportsV2.ShowDialog()
        frmPrintTipReportsV2.Dispose()
    End Sub

    Private Sub mnuSpecialFunctionReports_Click(sender As Object, e As EventArgs) Handles mnuSpecialFunctionReports.Click
        frmPrintSpecialFunctionReportV2.m_dsParentDataSet = Data.FileDataSet
        frmPrintSpecialFunctionReportV2.ShowDialog()
        frmPrintSpecialFunctionReportV2.Dispose()
    End Sub

    Private Sub mnuPayrollBalancingReport_Click(sender As Object, e As EventArgs) Handles mnuPayrollBalancingReport.Click
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

        If dlgPrint.ShowDialog() <> DialogResult.OK Then
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

    Private Sub PrintPayrollBalancingReport(sender As Object, e As PrintPageEventArgs)
        Dim font As New Font("Calibri", 12)
        Dim fontBold As New Font("Calibri", 12, FontStyle.Bold)

        Dim fmt As New StringFormat(StringFormatFlags.LineLimit)

        'Initialize local static variables that contain the current line position and
        'the current page number.
        Dim intPosition As Single = e.MarginBounds.Top
        Dim intPageNumber = 1

        Const intLineSpacing = 18
        Const intExtraLineSpacing = 36
        Const intIndent = 425

        Dim intStrLen As Integer

        'Initialize local constants that contain the normal line spacing and padded 
        'line spacing (double spacing).
        Dim intPrintAreaHeight, intPrintAreaWidth, marginLeft, marginTop As Integer

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

        e.Graphics.DrawString(Format(Date.Now, "MM/dd/yyyy") & " " & Format(Date.Now, "t"), font, Brushes.Black, marginLeft, intPosition)
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

        Dim intCCs = 0
        Dim intRCs = 0
        Dim intSFs = 0
        Dim intCAs = 0

        For i = 0 To dvTips.Count - 1
            Dim strDescription As String = dvTips.Item(i)("Description").ToString
            Dim decAmount = CDec(dvTips.Item(i)("Amount"))

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

        Dim strCharges = CStr(intCCs + intRCs + intSFs)
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

        Dim strTotal = CStr(intCCs + intRCs + intSFs + intCAs)
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

    Private Sub mnuAutoAddServers_Click(sender As Object, e As EventArgs) Handles mnuAutoAddServers.Click
        Dim blnError = True
        Dim intSeed As Integer
        Dim blnSuppressChits As Boolean

        While blnError = True
            If frmAutoAddInput.ShowDialog <> DialogResult.OK Then
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

            If frmAutoAddServers.ShowDialog <> DialogResult.OK Then
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

    Private Sub mnuOptimizeFile_Click(sender As Object, e As EventArgs) Handles mnuOptimizeFile.Click
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

        Dim intServer = 0

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

        Dim dteDate = CDate(Data.FileDataSet.Settings.FindBySetting("PeriodStart")("Value"))

        Do Until dteDate > CDate(Data.FileDataSet.Settings.FindBySetting("PeriodEnd")("Value"))
            dvTips.RowFilter = "WorkingDate = '" & Format(dteDate, "MM/dd/yyyy") & "' AND Description <> 'Special Function'"
            dvTips.Sort = "ServerNumber, Description"

            Dim intTip = 0

            Do Until intTip = dvTips.Count
                If intTip <> dvTips.Count - 1 Then
                    Dim strThisServer As String = dvTips.Item(intTip)("ServerNumber").ToString
                    Dim strThisDescription As String = dvTips.Item(intTip)("Description").ToString
                    Dim strNextServer As String = dvTips.Item(intTip + 1)("ServerNumber").ToString
                    Dim strNextDescription As String = dvTips.Item(intTip + 1)("Description").ToString
                    Dim decTotal = CDec(dvTips.Item(intTip)("Amount"))

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

    Private Sub SelectDataGridViewRowOnRightClick(sender As Object, e As DataGridViewCellMouseEventArgs) Handles ServersDataGridView.CellMouseDown,
        CreditCardDataGridView.CellMouseDown, RoomChargeDataGridView.CellMouseDown, CashDataGridView.CellMouseDown,
        SpecialFunctionDataGridView.CellMouseDown
        If e.Button = MouseButtons.Left OrElse TypeOf sender IsNot DataGridView Then Exit Sub

        If e.ColumnIndex >= 0 And e.RowIndex >= 0 Then
            Dim dgv = DirectCast(sender, DataGridView)
            dgv.CurrentCell = dgv.Rows(e.RowIndex).Cells(e.ColumnIndex)
        End If
    End Sub

    Private Sub UnsetComboBoxSelectionOnEmptyText(sender As Object, e As EventArgs) Handles cboCAServer.TextChanged, cboSFServer.TextChanged,
        cboSelectSpecialFunction.TextChanged
        If TypeOf sender IsNot ComboBox Then Exit Sub

        Dim sendingComboBox = DirectCast(sender, ComboBox)
        If sendingComboBox.Text.Length = 0 Then sendingComboBox.SelectedIndex = -1
    End Sub

    Private Sub SelectAmountControlOnEnterKey(sender As Object, e As KeyPressEventArgs) Handles txtCCServerNumber.KeyPress, txtRCServerNumber.KeyPress, cboCAServer.KeyPress
        If e.KeyChar = ChrW(13) Then
            e.Handled = True
            Dim sendingControl = DirectCast(sender, Control)
            sendingControl.Parent.SelectNextControl(sendingControl, True, True, True, True)

        End If
    End Sub

    Private Sub AddTipOnEnterKeyInAmountControl(sender As Object, e As KeyPressEventArgs) Handles txtCCAmount.KeyPress, txtRCAmount.KeyPress, txtCAAmount.KeyPress, txtSFAmount.KeyPress
        If e.KeyChar = ChrW(13) Then
            e.Handled = True

            Dim sendingControl = DirectCast(sender, Control)
            Dim siblings = sendingControl.Parent.Controls.Cast(Of Control)

            For Each sibling In siblings
                If TypeOf sibling Is Button And sibling.Text = "Add" Then
                    DirectCast(sibling, Button).PerformClick()
                    Exit Sub
                End If
            Next
        End If
    End Sub

End Class