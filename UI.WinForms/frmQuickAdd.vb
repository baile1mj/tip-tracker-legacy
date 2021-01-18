Public Class frmQuickAdd
    ''' <summary>
    ''' Gets the tip amount entered by the user.
    ''' </summary>
    ''' <returns>The entered tip amount.</returns>
    Public ReadOnly Property TipAmount As Decimal = 0

    ''' <summary>
    ''' Shows the form as a modal dialog box.
    ''' </summary>
    ''' <param name="serverName">The name of the server to populate on the form.</param>
    ''' <returns>One of the <see cref="DialogResult"/> values.</returns>
    Public Overloads Function ShowDialog(serverName As String) As DialogResult
        txtServerName.Text = serverName
        txtTipAmount.Clear()
        txtTipAmount.Select()
        _TipAmount = 0
        Return ShowDialog()
    End Function

    Private Sub btnCancel_Click(sender As Object, e As EventArgs) Handles btnCancel.Click
        DialogResult = DialogResult.Cancel
        Close()
    End Sub

    Private Sub btnNext_Click(sender As Object, e As EventArgs) Handles btnNext.Click
        If Not SetTipAmount() Then Exit Sub

        DialogResult = DialogResult.OK
        Close()
    End Sub

    Private Sub txtTipAmount_KeyPress(sender As Object, e As KeyPressEventArgs) Handles txtTipAmount.KeyPress
        If e.KeyChar = ChrW(13) Then
            e.Handled = True
            btnNext.PerformClick()
        End If
    End Sub

    Private Function SetTipAmount() As Boolean
        If txtTipAmount.Text = "" Then 
            _TipAmount = 0
            Return True
        End If

        Dim amount As Decimal = 0
        
        If Not Decimal.TryParse(txtTipAmount.Text, amount) Then
            If Not IsNumeric(txtTipAmount.Text) Then
                MessageBox.Show("The tip amount must be numeric.", "Invalid Entry", MessageBoxButtons.OK)
                txtTipAmount.Clear()
                txtTipAmount.Select()
                Return False
            End If
        End If

        If Not txtTipAmount.Text.Contains(".") Then
            amount /= 100
        End If

        _TipAmount = amount
        Return True
    End Function
End Class