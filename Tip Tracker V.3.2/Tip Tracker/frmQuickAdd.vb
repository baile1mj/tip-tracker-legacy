Public Class frmQuickAdd

    Private Sub btnCancel_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnCancel.Click
        Me.Close()
    End Sub

    Private Sub btnNext_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnNext.Click
        If AutoInsertDecimal() = False Then Exit Sub

        Me.DialogResult = Windows.Forms.DialogResult.OK
        Me.Close()
    End Sub

    Private Function AutoInsertDecimal() As Boolean
        If txtTipAmount.Text = "" Then txtTipAmount.Text = "0.00"

        If Not IsNumeric(txtTipAmount.Text) Then
            MessageBox.Show("The tip amount must be numeric.", "Invalid Entry", MessageBoxButtons.OK)
            txtTipAmount.Clear()
            txtTipAmount.Focus()
            Return False
        End If

        Dim decAmount As Decimal

        For Each c As Char In txtTipAmount.Text
            If c = "." Then
                decAmount = CDec(txtTipAmount.Text)
                txtTipAmount.Text = Format(decAmount, "0.00")
                Return True
            End If
        Next

        decAmount = CDec(txtTipAmount.Text)
        decAmount = decAmount / 100
        txtTipAmount.Text = Format(decAmount, "0.00")
        Return True
    End Function

    Private Sub txtTipAmount_KeyPress(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyPressEventArgs) Handles txtTipAmount.KeyPress
        If e.KeyChar = ChrW(13) Then
            e.Handled = True

            If AutoInsertDecimal() = False Then Exit Sub

            Me.DialogResult = Windows.Forms.DialogResult.OK
            Me.Close()
        End If
    End Sub
End Class