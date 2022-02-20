Imports System.Drawing.Printing
Imports System.IO
Imports System.Linq
Imports Microsoft.Reporting.WinForms
Imports TipTracker.Common.Data.PayPeriod
Imports TipTracker.Core
Imports TipTracker.Core.Reporting
Imports TipTracker.Utilities

Public Class frmEnterTips
    Private Const DATE_FORMAT = "M/d/yyyy"
    Private Const AMOUNT_FORMAT as String = "0.00"
    Private const REPORT_DATE_TIME_FORMAT as String = "MM/dd/yyyy h:mm tt"

    Public ReadOnly Property File As PayPeriodFile
    Public ReadOnly Property Data As PayPeriodData
    Public ReadOnly Property ObjectService As PayPeriodObjectService

    Private ReadOnly _totalLabelLookup As Dictionary(Of TipType, Label)
    Private ReadOnly _templateServers As New List(Of Server)

    Public Sub New(file As PayPeriodFile, data As PayPeriodData, templateServers As IReadOnlyList(Of Server))
        InitializeComponent()

        _totalLabelLookup = New Dictionary(Of TipType, Label) From {
            {TipTypes.CreditCard, lblCCTotal},
            {TipTypes.RoomCharge, lblRCTotal},
            {TipTypes.Cash, lblCATotal},
            {TipTypes.SpecialFunction, lblSFTotal}}
        _templateServers.AddRange(templateServers)

        Me.File = file
        Me.Data = data
        FileDataSet = data.FileDataSet
        ObjectService = New PayPeriodObjectService(data)
        Text = Path.GetFileNameWithoutExtension(file.FilePath)
    End Sub

    Private Sub frmEnterTips_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        'Set the form as an mdi child of frmMain.
        MdiParent = frmMain
        lblSystemDate.Text = "System Date: " & Date.Today.ToString(DATE_FORMAT)
        WindowState = FormWindowState.Maximized

        'Bind the data sources to the display.
        ServerBindingSource.DataSource = New SortableBindingList(Of Server)(ObjectService.GetServerDataStore().GetAll().ToList())

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
        ServerBindingSource.Sort = NameOf(Server.LastName)

        LoadServerCombos()

        'Update the date labels to show the dates from the settings table.
        UpdateDateLabels()

        'Set the tips binding source sort and filter modes.
        SetSelectionFilters()
        cboSelectSpecialFunction.SelectedIndex = -1

        'Since the tab control doesn't like to focus on the first child control on it's own,
        'the child control needs to be selected manually.
        txtCCServerNumber.Select()

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
        lblPeriodStart.Text = "Period Start: " & Data.PayPeriodStart.ToString(DATE_FORMAT)
        lblPeriodEnd.Text = "Period End: " & Data.PayPeriodEnd.ToString(DATE_FORMAT)
        lblWorkingDate.Text = "Working Date: " & Data.WorkingDate.ToString(DATE_FORMAT)
    End Sub

    Private Sub SetSelectionFilters()
        Dim strWorkingDate As String = Data.WorkingDate.ToString(DATE_FORMAT)
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
        Dim dteWorkingDate = Data.WorkingDate
        Dim dtePeriodEnd = Data.PayPeriodEnd
        Dim newWorkingDate = dteWorkingDate.AddDays(1)

        If dteWorkingDate = dtePeriodEnd Then
            MessageBox.Show("The current working date is the last day in the pay period.  You cannot " &
            "advance the working date any further.  To work on tips for " &
            newWorkingDate.ToString(DATE_FORMAT) & " you must start a new file " &
            "for the new pay period.", "Cannot Change Working Date", MessageBoxButtons.OK)
            Exit Sub
        End If

        If MessageBox.Show("The working date will be changed to " & newWorkingDate.ToString(DATE_FORMAT) &
        ".  Do you wish to continue?", "Confirm Date Change", MessageBoxButtons.YesNo) <> DialogResult.Yes Then
            Exit Sub
        End If

        Data.WorkingDate = newWorkingDate
        UpdateDateLabels()
        SetSelectionFilters()
    End Sub

    Private Sub btnSelectWorkingDate_Click(sender As Object, e As EventArgs) Handles btnSelectWorkingDate.Click
        Using dateForm As New frmSelectDate(Data.PayPeriodStart, Data.PayPeriodEnd, Data.WorkingDate)
            If dateForm.ShowDialog() <> DialogResult.OK Then Exit Sub
            If dateForm.SelectedDate = Data.WorkingDate Then Exit Sub

            Data.WorkingDate = dateForm.SelectedDate
        End Using

        UpdateDateLabels()
        SetSelectionFilters()
    End Sub

    Private Function GetServerRow(serverTextBox As TextBox) As FileDataSet.ServersRow
        If String.IsNullOrEmpty(serverTextBox.Text) Then
            MessageBox.Show("You must enter a server number.", "Invalid Entry", MessageBoxButtons.OK)
            serverTextBox.Select()
            Return Nothing
        End If

        Return Data.FileDataSet.Servers.FindByServerNumber(serverTextBox.Text)
    End Function

    Private Function GetServerRow(serverComboBox As ComboBox) As FileDataSet.ServersRow
        If serverComboBox.SelectedIndex = -1 Then
            If serverComboBox.Text = "" Then
                MessageBox.Show("You must select the server this tip belongs to.", "Select Server", MessageBoxButtons.OK)
                serverComboBox.Select()
                Return Nothing
            Else
                MessageBox.Show("The server name you entered was not found in the data file.  You must add the server before you can add the tip.", "Server Not Found", MessageBoxButtons.OK)
                serverComboBox.Text = ""
                serverComboBox.Select()
                Return Nothing
            End If
        End If

        Dim serverNumber = serverComboBox.SelectedValue.ToString()
        Return Data.FileDataSet.Servers.FindByServerNumber(serverNumber)
    End Function

    Private Function GetTipAmount(amountTextBox As TextBox) As Decimal?
        If String.IsNullOrEmpty(amountTextBox.Text) Then
            MessageBox.Show("You must enter a tip amount.", "Invalid Entry", MessageBoxButtons.OK)
            amountTextBox.Select()
            Return Nothing
        End If

        If Not IsNumeric(amountTextBox.Text) Then
            MessageBox.Show("The tip amount must be a number.", "Invalid Entry", MessageBoxButtons.OK)
            amountTextBox.Clear()
            amountTextBox.Select()
            Return Nothing
        End If

        Return If(amountTextBox.Text.Contains("."),
            CDec(amountTextBox.Text),
            CDec(amountTextBox.Text) / 100)
    End Function

    Private Function GetSelectedFunction(specialFunctionComboBox As ComboBox) As FileDataSet.SpecialFunctionsRow
        If specialFunctionComboBox.SelectedIndex = -1 Then
            MessageBox.Show("You must select a special function.", "Invalid Selection", MessageBoxButtons.OK)
            specialFunctionComboBox.Select()
            Return Nothing
        End If

        Dim selectedFunction = specialFunctionComboBox.SelectedValue.ToString()
        Return FileDataSet.SpecialFunctions.FindBySpecialFunction(selectedFunction)
    End Function

    Private Sub AddTip(server As FileDataSet.ServersRow, tipAmount As Decimal, tipType As TipType,
        Optional specialFunction As FileDataSet.SpecialFunctionsRow = Nothing)
        Dim newTipRow = Data.FileDataSet.Tips.NewTipsRow()

        With newTipRow
            .Amount = tipAmount
            .ServerNumber = server.ServerNumber
            .FirstName = server.FirstName
            .LastName = server.LastName
            .Description = tipType.Name

            If specialFunction IsNot Nothing Then
                .SpecialFunction = specialFunction.SpecialFunction
                .WorkingDate = specialFunction._Date
            ElseIf tipType Is TipTypes.Cash Then
                .WorkingDate = Data.PayPeriodEnd
            Else
                .WorkingDate = Data.WorkingDate
            End If
        End With

        Data.FileDataSet.Tips.AddTipsRow(newTipRow)
    End Sub

    Private Sub EditTip(bindingSource As BindingSource, sourceType As TipType)
        Dim selectedTip = GetSelectedTip(bindingSource)
        Dim servers = ObjectService.GetServerDataStore().GetAll().ToList()
        Dim server = servers.First(Function(s) s.PosId = selectedTip.ServerNumber)
        Dim periodStart  = Data.PayPeriodStart
        Dim periodEnd  = Data.PayPeriodEnd
        Dim functions = Data.FileDataSet.SpecialFunctions _
            .AsEnumerable() _
            .Select(Function(f) f.SpecialFunction) _
            .ToList()
        Dim specialFunction = selectedTip.SpecialFunctionsRow?.SpecialFunction
        
        Using editTip As New frmEditTip(selectedTip.Amount, periodStart, periodEnd, selectedTip.WorkingDate, sourceType, server, _
            servers, functions, specialFunction)
            If editTip.ShowDialog() <> DialogResult.OK Then Return
            
            Dim isUnchanged = editTip.Amount = selectedTip.Amount AndAlso editTip.Server Is server AndAlso _
                editTip.WorkingDate = selectedTip.WorkingDate AndAlso editTip.TipType Is sourceType AndAlso _
                editTip.SpecialFunction Is selectedTip.SpecialFunctionsRow?.SpecialFunction

            If isUnchanged Then Exit Sub
            
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

            selectedTip.ServersRowParent = Data.FileDataSet.Servers.FindByServerNumber(editTip.Server.PosId)
            selectedTip.Amount = editTip.Amount
            selectedTip.Description = editTip.TipType.Name
            
            UpdateTotal(sourceType, specialFunction)

            If sourceType IsNot editTip.TipType Then UpdateTotal(editTip.TipType, editTip.SpecialFunction)
        End Using
    End Sub

    Private Sub UpdateTotal(tipType As TipType, Optional specialFunction As String = Nothing)
        Dim amountLabel = _totalLabelLookup(tipType)
        Dim isCandidateTip As Func(Of FileDataSet.TipsRow, Boolean) = Function(r) _
            r.RowState <> DataRowState.Deleted _
            AndAlso r.RowState <> DataRowState.Detached _
            AndAlso r.Description = tipType.Name
        Dim affectsTotal As Func(Of FileDataSet.TipsRow, Boolean)

        If tipType Is TipTypes.CreditCard OrElse tipType Is TipTypes.RoomCharge Then
            'Credit card and room charge tips are filtered date
            Dim workingDate = Data.WorkingDate
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
            .Select(Function(r) r.Amount) _
            .Sum()

        amountLabel.Text = $"Total: {dailyTotal:c}"
    End Sub

    Private Sub ResetEntryForm(ParamArray formControls As Control())
        For Each control In formControls
            control.ResetText()
        Next

        formControls(0).Select()
    End Sub

    Private Function ConfirmTipDeletion(tipRow As FileDataSet.TipsRow) As Boolean
        Return MessageBox.Show($"Delete ${tipRow.Amount} tip for {tipRow.FirstName} {tipRow.LastName}?",
            "Confirm Delete", MessageBoxButtons.YesNo) = DialogResult.Yes
    End Function

    Private Sub PerformTipDeletion(bindingSource As BindingSource, tipType As TipType, Optional specialFunction As String = Nothing)
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
            txtCCServerNumber.Select()
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
        PerformTipDeletion(CreditCardTipsBindingSource, TipTypes.CreditCard)
    End Sub

    Private Sub txtCCServerName_GotFocus(sender As Object, e As EventArgs) Handles txtCCServerName.GotFocus
        txtCCServerNumber.Select()
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
            txtRCServerNumber.Select()
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
        PerformTipDeletion(RoomChargeTipsBindingSource, TipTypes.RoomCharge)
    End Sub

    Private Sub txtRCServerName_GotFocus(sender As Object, e As EventArgs) Handles txtRCServerName.GotFocus
        txtRCServerNumber.Select()
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
        PerformTipDeletion(CashTipsBindingSource, TipTypes.Cash)
    End Sub
    
    Private Sub mnuEditCATip_Click(sender As Object, e As EventArgs) Handles mnuEditCATip.Click, CashDataGridView.DoubleClick
        If CashDataGridView.Rows.Count = 0 Then Exit Sub
        EditTip(CashTipsBindingSource, TipTypes.Cash)
    End Sub

    Private Sub btnQuickAddCashTips_Click(sender As Object, e As EventArgs) Handles btnQuickAddCashTips.Click
        Dim servers = ObjectService.GetServerDataStore().GetAll().ToList().OrderBy(Function(s) s.LastName).ThenBy(Function(s) s.FirstName).ToList()

        If Not servers.Any() Then
            MessageBox.Show("There are no server in the file.", "No Servers", MessageBoxButtons.OK)
            Exit Sub
        End If 

        Using quickAdd As New frmQuickAdd
            For Each server In servers
                If quickAdd.ShowDialog(server.ToString()) <> DialogResult.OK Then Exit Sub

                If quickAdd.TipAmount <> 0 Then
                    Data.FileDataSet.Tips.AddTipsRow(quickAdd.TipAmount, server.PosId, server.FirstName,server.LastName, _
                        TipTypes.Cash.Name, Nothing, Data.PayPeriodEnd)
                    UpdateTotal(TipTypes.Cash)
                End If
            Next
        End Using
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

        PerformTipDeletion(SpecialFunctionTipsBindingSource, TipTypes.SpecialFunction, specialFunction)
    End Sub

    Private Sub SpecialFunctionDataGridView_RowStateChanged(sender As Object, e As DataGridViewRowStateChangedEventArgs) Handles SpecialFunctionDataGridView.RowStateChanged
        UpdateTotal(TipTypes.SpecialFunction, cboSelectSpecialFunction.SelectedValue?.ToString())
    End Sub
    
    Private Sub mnuEditSFTip_Click(sender As Object, e As EventArgs) Handles mnuEditSFTip.Click, SpecialFunctionDataGridView.DoubleClick
        If SpecialFunctionDataGridView.Rows.Count = 0 Then Exit Sub
        EditTip(SpecialFunctionTipsBindingSource, TipTypes.SpecialFunction)
    End Sub
#End Region

    'End of tip operations

    Private Sub mnuManageSpecialFunctions_Click(sender As Object, e As EventArgs) Handles mnuManageSpecialFunctions.Click
        Using form As New frmManageSpecialFunctions(ObjectService.GetEventDataStore())
            form.ShowDialog()
        End Using

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
        cboSFServer.Select()
    End Sub

    Private Sub mnuExportTips_Click(sender As Object, e As EventArgs) Handles mnuExportTips.Click
        frmExportTips.m_dsParentDataSet = Data.FileDataSet
        frmExportTips.ShowDialog()
        frmExportTips.Dispose()
    End Sub

    Private Sub mnuAddServer_Click(sender As Object, e As EventArgs) Handles mnuAddServer.Click
        Dim dataStore  = ObjectService.GetServerDataStore()
        Dim newServer As Server

        Using dialog As New frmAddEditServer()
            dialog.Text = "Add Server"
            
            Do
                If dialog.ShowDialog <> DialogResult.OK Then Exit Sub
                
                newServer = dialog.Server
                If Not dataStore.Contains(newServer) Then Exit Do

                MessageBox.Show("The server number you entered already exists in the data file.  " & 
                    "Please enter a different number.", "Invalid Entry", MessageBoxButtons.OK)
            Loop
        End Using

        dataStore.Add(newServer)
        ServerBindingSource.Add(newServer)
        LoadServerCombos()

        Dim alreadyInTemplate = _templateServers.Any(Function(s) s.Is(newServer))
        If alreadyInTemplate Then Exit Sub

        Dim addResponse = MessageBox.Show(newServer.ToString() & " does not exist in the servers template.  " &
            "Add the server to the template?", "Add Template Server", MessageBoxButtons.YesNo)
        If addResponse <> DialogResult.Yes Then Exit Sub

        RaiseEvent NewTemplateServerRequested(Me, newServer)
    End Sub

    Private Sub mnuEditSelectedServer_Click(sender As Object, e As EventArgs) Handles mnuEditSelectedServer.Click, ServersDataGridView.DoubleClick
        Dim selected = DirectCast(ServerBindingSource.Current, Server)
        Dim updated As Server

        Using dialog As New frmAddEditServer(selected.Clone(), false)
            If dialog.ShowDialog() <> DialogResult.OK Then Exit Sub

            updated = dialog.Server
        End Using

        If updated.Matches(selected) Then Exit Sub

        ServerBindingSource.RemoveCurrent()
        ServerBindingSource.Add(updated)
        ObjectService.GetServerDataStore().Update(selected, updated)
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
        Dim selected = DirectCast(ServerBindingSource.Current, Server)
        Dim recipient As Server

        Using selectServer As New frmSelectServer("Select the merge recipient:", ObjectService.GetServerDataStore().GetAll().ToList())
            If selectServer.ShowDialog() <> DialogResult.OK Then Exit Sub

            recipient = selectServer.GetSelectedServer()
        End Using

        ObjectService.ReassignTips(selected, recipient)
        ObjectService.GetServerDataStore().Delete(selected)
        ServerBindingSource.Remove(selected)
    End Sub

    Private Sub mnuCopyFromTemplate_Click(sender As Object, e As EventArgs) Handles mnuCopyFromTemplate.Click
        Dim fileServers = ObjectService.GetServerDataStore().GetAll().ToList().ToDictionary(Function(s) s.PosId)
        Dim conflicts = New List(Of Server)
        Dim missingServers = New List(Of Server)

        For Each templateServer In _templateServers
            If Not fileServers.ContainsKey(templateServer.PosId) Then
                missingServers.Add(templateServer)

            ElseIf Not fileServers(templateServer.PosId).Matches(templateServer) Then
                conflicts.Add(templateServer)
            End If
        Next

        Dim store = ObjectService.GetServerDataStore()

        For Each newServer In missingServers
            store.Add(newServer)
            ServerBindingSource.Add(newServer)
        Next

        If Not conflicts.Any() Then Exit Sub

        Dim conflictList = String.Join(Environment.NewLine, conflicts.Select(Function(s) "• " & s.ToString()))
        Dim message = "The following servers were not added because they conflict with servers already in the file:" &
            Environment.NewLine & conflictList

        MessageBox.Show(message, "Conflicts Found", MessageBoxButtons.OK, MessageBoxIcon.Information)


    End Sub

    Private Sub mnuPrintTipChits_Click(sender As Object, e As EventArgs) Handles mnuPrintRegularTipChits.Click
        Dim types = TipTypes.Values
        Dim payPeriod = objectService.GetPayPeriod()
        Dim servers = objectService.GetTips() _
            .Where(Function (t) Not t.EarnedBy.SuppressChit) _
            .Select(Function (t) t.EarnedBy) _
            .Distinct() _
            .Select(Function(s) s.Clone(New TipChitDataBuilder(payPeriod, s, types).GetPreparedTips())) _
            .OrderBy(Function(s) s.LastName) _
            .ThenBy(Function(s) s.FirstName) _
            .ThenBy(Function(s) s.PosId)
        
        Using printer As New frmPrintRegularTipChits(servers)
            printer.ShowDialog()
        End Using
    End Sub

    Private Sub btnManageFunctions_Click(sender As Object, e As EventArgs) Handles btnManageFunctions.Click
        mnuManageSpecialFunctions_Click(sender, e)

        If cboSFServer.SelectedIndex <> -1 Then
            cboSFServer.SelectedIndex = -1
        Else
            cboSFServer.Text = ""
        End If
    End Sub

    Private Sub mnuTipReports_Click(sender As Object, e As EventArgs) Handles mnuTipReports.Click
        Using options As New frmPrintTipReportsV2(ObjectService.GetPayPeriod(), ObjectService.GetTips())
            options.ShowDialog()
        End Using
    End Sub

    Private Sub mnuSpecialFunctionReports_Click(sender As Object, e As EventArgs) Handles mnuSpecialFunctionReports.Click
        Dim tips   = ObjectService _
            .GetTips() _
            .Where(Function(t) t.Event IsNot Nothing)

        Using dialog As New frmPrintSpecialFunctionReportV2(tips)
            dialog.ShowDialog()
        End Using
    End Sub

    ''' <summary>
    ''' Prepares a payroll totals report instance with the data necessary to generate the report.
    ''' </summary>
    ''' <param name="report"></param>
    Private Sub PreparePayrollTotals(report As LocalReport) 
        Dim tips = ObjectService.GetTips()
        Dim totalsByType = tips _
            .GroupBy(Function(t) t.Type) _
            .Select(Function(g) New TipTypeTotal(g.Key, g.Count(), g.Sum(Function(t) t.Amount))) _
            .OrderBy(Function(t) t.Type.Classification.SortIndex) _
            .ThenBy(Function(t) t.Type.Name) _
            .Select(Function(s) s.ToAnonymous())

        Dim payPeriod = objectService.GetPayPeriod()

        report.DisplayName = "Payroll Totals Report"
        report.LoadReportDefinition(ReportDefinitions.PayrollTotals)

        report.SetParameters(New ReportParameter("StartDate", payPeriod.Start.ToString(DATE_FORMAT)))
        report.SetParameters(New ReportParameter("EndDate", payPeriod.End.ToString(DATE_FORMAT)))
        report.DataSources.Add(New ReportDataSource("TipTypeTotals", totalsByType))
        report.DataSources.Add(New ReportDataSource("TipClassifications", TipClassification.Classes))
    End Sub

    Private Sub mnuPayrollBalancingReport_Click(sender As Object, e As EventArgs) Handles mnuPayrollBalancingReport.Click
        Cursor.Current = Cursors.WaitCursor
        
        Try
            Using viewer = New frmReportPreview(AddressOf PreparePayrollTotals)
                viewer.ShowDialog()
            End Using
        Catch ex As Exception
            MessageBox.Show("Failed to generate the requested report.  Please verify that a printer is installed and that a " & _
                "default printer has been selected.", "Report Generation Failed", MessageBoxButtons.OK)
        End Try
        
        Cursor.Current = Cursors.Default
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

        Dim rowNotDeleted = Function (r As DataRow) Not(r.RowState = DataRowState.Deleted OrElse r.RowState = DataRowState.Detached)
        Dim allServers = Data.FileDataSet.Servers.AsEnumerable().Where(rowNotDeleted).Select(Function(s) s.ServerNumber)
        Dim serversWithTips = Data.FileDataSet.Tips.AsEnumerable().Where(rowNotDeleted).Select(Function (r) r.ServerNumber).Distinct()
        Dim serversToRemove = allServers.Except(serversWithTips)

        serversToRemove.ToList().ForEach(Sub (s) Data.FileDataSet.Servers.FindByServerNumber(s).Delete())
        
        Using dvTips = Data.FileDataSet.Tips.AsDataView()
            Dim dteDate = Data.PayPeriodStart

            Do Until dteDate > Data.PayPeriodEnd
                dvTips.RowFilter = "WorkingDate = '" & dteDate.ToString("MM/dd/yyyy") & "' AND Description <> 'Special Function'"
                dvTips.Sort = "ServerNumber, Description"

                Dim intTip = 0

                Do Until intTip > dvTips.Count - 2
                    Dim thisTip = DirectCast(dvTips.Item(intTip).Row, FileDataSet.TipsRow)
                    Dim nextTip = DirectCast(dvTips.Item(intTip + 1).Row, FileDataSet.TipsRow)
                    Dim decTotal = thisTip.Amount

                    If thisTip.ServerNumber = nextTip.ServerNumber AndAlso thisTip.Description = nextTip.Description Then
                        decTotal += nextTip.Amount
                        nextTip.Delete()
                        thisTip.Amount = decTotal
                        Continue Do
                    End If
                    intTip += 1
                Loop
                dteDate = dteDate.AddDays(1)
            Loop
        End Using

        Cursor.Current = Cursors.Default
        lblInfo.Visible = False
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

    Public Sub OnTemplateServersUpdated(sender As Object, updatedList As IReadOnlyList(Of Server))
        _templateServers.Clear()
        _templateServers.AddRange(updatedList)
    End Sub

    Public Event NewTemplateServerRequested As EventHandler(Of Server)
End Class