Imports System.Linq
Imports TipTracker.Common.Data.PayPeriod
Imports TipTracker.Core
Imports TipTracker.Utilities

Public Class frmEditTip
    Const DATE_FORMAT = "MM/dd/yyyy"
    Const AMOUNT_FORMAT = "0.00"

    Private _payPeriodStart As DateTime
    Private _payPeriodEnd As DateTime
    Private _amount As Decimal
    Private _workingDate As DateTime
    Private ReadOnly _workingDateTextBoxInitialValue As String

    Friend Property Amount As Decimal
        Get
            Return _amount
        End Get
        Set(value As Decimal)
            _amount = value
            txtAmount.Text = _amount.ToString(AMOUNT_FORMAT)
        End Set
    End Property

    Friend Property WorkingDate As Date
        Get
            Return _workingDate
        End Get
        Set(value As Date)
            _workingDate = value
            txtWorkingDate.Text = Format(_workingDate, DATE_FORMAT)
        End Set
    End Property

    Public property Server As Server
        Get
            Return DirectCast(cboServers.SelectedItem, Server)
        End Get
        Set(value As Server)
            cboServers.SelectedItem = value
        End Set
    End Property

    Public Property TipType As TipTypes
        Get
            Return DirectCast(cboTipTypes.SelectedItem, TipTypes)
        End Get
        Set(value As TipTypes)
            cboTipTypes.SelectedItem = value
        End Set
    End Property

    Public Property SpecialFunction As String
        Get
            Return cboSpecialFunction.SelectedItem?.ToString()
        End Get
        Set(value As String)
            cboSpecialFunction.SelectedItem = value
        End Set
    End Property

    Public Sub New(amount As Decimal, periodStart As DateTime, periodEnd As DateTime, workingDate As DateTime,
        currentType As TipTypes, currentServer As Server, allServers As List(Of Server), functions As IEnumerable(Of String), _
        Optional currentFunction As String = "")
        InitializeComponent()

        _workingDateTextBoxInitialValue = txtWorkingDate.Text

        cboServers.Items.AddRange(allServers.ToArray())
        cboTipTypes.Items.AddRange(TipTypes.Values.ToArray())
        cboSpecialFunction.Items.Add("") 'Add blank function to allow no selection.
        cboSpecialFunction.Items.AddRange(functions.ToArray())

        _payPeriodStart = periodStart
        _payPeriodEnd = periodEnd

        Me.Amount = amount
        Me.WorkingDate = workingDate
        Me.cboServers.SelectedItem = currentServer
        Me.cboTipTypes.SelectedItem = currentType
        Me.cboSpecialFunction.SelectedItem = currentFunction
    End Sub

    Private Sub txtAmount_KeyPress(sender As Object, e As KeyPressEventArgs) Handles txtAmount.KeyPress
        If e.KeyChar = ChrW(13) Then
            e.Handled = True
            txtWorkingDate.Select()
        End If
    End Sub

    Private Sub txtAmount_LostFocus(sender As Object, e As EventArgs) Handles txtAmount.LostFocus
        If txtAmount.Text = "" Then Exit Sub

        If Not IsNumeric(txtAmount.Text) Then
            TriggerInvalidMessage("Please enter a valid amount.", txtAmount)
            Exit Sub
        End If

        If txtAmount.Text.Contains(".") Then
            Amount = CDec(txtAmount.Text)
        Else
            Amount = CDec(txtAmount.Text) / 100
        End If

        txtAmount.Text = Amount.ToString(AMOUNT_FORMAT)
    End Sub

    Private Sub txtWorkingDate_KeyPress(sender As Object, e As KeyPressEventArgs) Handles txtWorkingDate.KeyPress
        If e.KeyChar = Chr(13) Then
            e.Handled = True
            cboTipTypes.Select()
        End If
    End Sub

    Private Sub txtWorkingDate_LostFocus(sender As Object, e As EventArgs) Handles txtWorkingDate.LostFocus
        If Not txtWorkingDate.Enabled Then Exit Sub
        If txtWorkingDate.Text = _workingDateTextBoxInitialValue Then Exit Sub

        Dim chosenDate As DateTime
        
        If Not txtWorkingDate.MaskCompleted Or Not DateTime.TryParse(txtWorkingDate.Text, chosenDate) Then
            TriggerInvalidMessage("Please enter a valid date.", txtWorkingDate)
            Exit Sub
        End If

        If chosenDate < _payPeriodStart Or chosenDate > _payPeriodEnd Then
            TriggerInvalidMessage($"Working date must fall within the pay period: " &
                $"{_payPeriodStart.ToString(DATE_FORMAT)}-{_payPeriodEnd.ToString(DATE_FORMAT)}.", txtWorkingDate)
            Exit Sub
        End If

        WorkingDate = chosenDate
    End Sub

    Private Sub txtWorkingDate_ReadOnlyChanged(sender As Object, e As EventArgs) Handles txtWorkingDate.ReadOnlyChanged
        If txtWorkingDate.ReadOnly Then
            txtWorkingDate.Clear()
        Else
            txtWorkingDate.Text = WorkingDate.ToString(DATE_FORMAT)
        End If
    End Sub

    Private Sub cboTipTypes_SelectedValueChanged(sender As Object, e As EventArgs) Handles cboTipTypes.SelectedValueChanged
        Dim selectedType = DirectCast(cboTipTypes.SelectedItem, TipTypes)

        txtWorkingDate.Enabled = selectedType.CanSpecifyDate
        cboSpecialFunction.Enabled = selectedType.IsEventOriginated
        TipType = selectedType
    End Sub

    Private Sub cboSpecialFunction_EnabledChanged(sender As Object, e As EventArgs) Handles cboSpecialFunction.EnabledChanged
        If Not cboSpecialFunction.Enabled Then cboSpecialFunction.SelectedItem = Nothing
    End Sub

    Private Sub cboSpecialFunction_SelectedIndexChanged(sender As Object, e As EventArgs) Handles cboSpecialFunction.SelectedIndexChanged
        SpecialFunction = cboSpecialFunction.SelectedItem?.ToString()
    End Sub

    Private Function ValidateForm() As Boolean
        If txtAmount.Text = "" Then
            TriggerInvalidMessage("You must enter a tip amount.", txtAmount)
            Return False
        End If

        If Amount = 0 Then
            TriggerInvalidMessage("You may not enter a $0.00 tip.", txtAmount)
            Return False
        End If

        If cboServers.SelectedIndex = -1 Then
            TriggerInvalidMessage("You must select a server.", cboServers)
            Return False
        End If

        If cboTipTypes.SelectedIndex = -1 Then 
            TriggerInvalidMessage("You must select a tip type.", cboTipTypes)
            Return False
        End If

        If TipType.CanSpecifyDate AndAlso Not txtWorkingDate.MaskCompleted Then
            TriggerInvalidMessage("You must select a working date.", txtWorkingDate)
            Return False
        End If

        If TipType.IsEventOriginated AndAlso string.IsNullOrEmpty(cboSpecialFunction.SelectedItem.ToString()) Then
            TriggerInvalidMessage("You must select a special function", cboSpecialFunction)
            Return False
        End If

        Return True
    End Function

    Private Sub TriggerInvalidMessage(message As String, invalidControl As TextBoxBase)
        MessageBox.Show(message, "Invalid Entry", MessageBoxButtons.OK)
        invalidControl.Clear()
        invalidControl.Select()
    End Sub

    Private Sub TriggerInvalidMessage(message As String, invalidControl As ComboBox)
        MessageBox.Show(message, "Invalid Selection", MessageBoxButtons.OK)
        invalidControl.Select()
    End Sub

    Private Sub btnCancel_Click(sender As Object, e As EventArgs) Handles btnCancel.Click
        DialogResult = DialogResult.Cancel
        Close()
    End Sub

    Private Sub btnOK_Click(sender As Object, e As EventArgs) Handles btnOK.Click
        If Not ValidateForm() Then Exit Sub
        If cboSpecialFunction.SelectedIndex = 0 Then cboSpecialFunction.SelectedItem = Nothing 'Ignore the placeholder item.

        DialogResult = DialogResult.OK
        Close()
    End Sub
    
End Class