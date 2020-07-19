Imports TipTracker.Common.Data.PayPeriod

Public Class frmEditTip
    Friend m_dsParentDataSet As New FileDataSet

    Private Const intCreditCard As Integer = 0
    Private Const intRoomCharge As Integer = 1
    Private Const intSpecialFunction As Integer = 2
    Private Const intCash As Integer = 3

    Friend Property TipAmount() As Decimal
        Get
            Return CDec(txtAmount.Text)
        End Get
        Set(ByVal value As Decimal)
            txtAmount.Text = value.ToString
        End Set
    End Property

    Friend Property WorkingDate() As Date
        Get
            Return CDate(txtWorkingDate.Text)
        End Get
        Set(ByVal value As Date)
            txtWorkingDate.Text = Format(value, "MM/dd/yyyy")
        End Set
    End Property

    Friend Property TipType() As String
        Get
            Select Case cboTipTypes.SelectedItem.ToString
                Case "Credit Card"
                    Return "Credit Card"
                Case "Room Charge"
                    Return "Room Charge"
                Case "Special Function"
                    Return "Special Function"
                Case "Cash"
                    Return "Cash"
                Case Else
                    Return "Unknown"
            End Select
        End Get
        Set(ByVal value As String)
            Select Case value
                Case "Credit Card"
                    cboTipTypes.SelectedIndex = intCreditCard
                Case "Room Charge"
                    cboTipTypes.SelectedIndex = intRoomCharge
                Case "Special Function"
                    cboTipTypes.SelectedIndex = intSpecialFunction
                Case "Cash"
                    cboTipTypes.SelectedIndex = intCash
            End Select
        End Set
    End Property

    Private Sub AutoInsertDecimal()
        If txtAmount.Text = "" Then Exit Sub

        Dim decAmount As Decimal

        For Each c As Char In txtAmount.Text
            If c = "." Then
                decAmount = CDec(txtAmount.Text)
                txtAmount.Text = Format(decAmount, "0.00")
                Exit Sub
            End If
        Next

        decAmount = CDec(txtAmount.Text)
        decAmount = decAmount / 100
        txtAmount.Text = Format(decAmount, "0.00")
    End Sub

    Private Sub txtAmount_KeyPress(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyPressEventArgs) Handles txtAmount.KeyPress
        If e.KeyChar = Chr(8) Then Exit Sub

        If e.KeyChar = Chr(13) Then
            e.Handled = True
            txtWorkingDate.Focus()
        End If
    End Sub

    Private Sub txtAmount_LostFocus(ByVal sender As Object, ByVal e As System.EventArgs) Handles txtAmount.LostFocus
        If txtAmount.Text = "" Then Exit Sub
        If Not IsNumeric(txtAmount.Text) Then Exit Sub
        AutoInsertDecimal()
    End Sub

    Private Function HasErrors() As Boolean
        If txtAmount.Text = "" Then
            MessageBox.Show("You must enter a tip amount.", "Invalid Entry", MessageBoxButtons.OK)
            txtAmount.Focus()
            Return True
        End If
        If Not IsNumeric(txtAmount.Text) Then
            MessageBox.Show("The tip amount entry must be a number.", "Invalid Entry", MessageBoxButtons.OK)
            txtAmount.Clear()
            txtAmount.Focus()
            Return True
        End If
        If CDec(txtAmount.Text) = 0 Then
            MessageBox.Show("You may not enter a $0.00 tip.", "Invalid Entry", MessageBoxButtons.OK)
            txtAmount.Clear()
            txtAmount.Focus()
            Return True
        End If

        If txtWorkingDate.Text = "" Then
            MessageBox.Show("You must enter a working date.", "Invalid Entry", MessageBoxButtons.OK)
            txtWorkingDate.Focus()
            Return True
        End If

        Dim dteTemp As Date
        Try
            dteTemp = CDate(txtWorkingDate.Text)
        Catch ex As Exception
            MessageBox.Show("That is not the proper format for a date", "Invalid Entry", MessageBoxButtons.OK)
            txtWorkingDate.Clear()
            txtWorkingDate.Focus()
            Return True
        End Try

        Dim dtePeriodStart As Date = CDate(Me.m_dsParentDataSet.Settings.FindBySetting("PeriodStart")("Value"))
        Dim dtePeriodEnd As Date = CDate(Me.m_dsParentDataSet.Settings.FindBySetting("PeriodEnd")("Value"))
        If dteTemp < dtePeriodStart Or dteTemp > dtePeriodEnd Then
            MessageBox.Show("You must enter a date within the pay period.", "Invalid Entry", MessageBoxButtons.OK)
            txtWorkingDate.Clear()
            txtWorkingDate.Focus()
            Return True
        End If

        If cboTipTypes.SelectedIndex = -1 Then
            MessageBox.Show("You must select a tip type.", "Invalid Selection", MessageBoxButtons.OK)
            cboTipTypes.Focus()
            Return True
        End If

        Return False
    End Function

    Private Sub CloseFormOK()
        If HasErrors() = True Then Exit Sub

        Me.DialogResult = Windows.Forms.DialogResult.OK
        Me.Close()
    End Sub

    Private Sub btnCancel_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnCancel.Click
        Me.Close()
    End Sub

    Private Sub btnOK_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnOK.Click
        CloseFormOK()
    End Sub

    Private Sub txtWorkingDate_KeyPress(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyPressEventArgs) Handles txtWorkingDate.KeyPress
        If e.KeyChar = Chr(13) Then
            e.Handled = True
            cboTipTypes.Focus()
        End If
    End Sub

End Class