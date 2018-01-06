Public Class frmSelectFunction

    Friend ReadOnly Property SelectedFunction() As String
        Get
            Return cboFunctions.SelectedItem.ToString
        End Get
    End Property

    Private Sub btnCancel_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnCancel.Click
        Me.Close()
    End Sub

    Private Sub btnOK_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnOK.Click
        If cboFunctions.SelectedIndex = -1 Then
            MessageBox.Show("You must select a special function.", "Select Function", MessageBoxButtons.OK)
            Exit Sub
        End If

        Me.DialogResult = Windows.Forms.DialogResult.OK
        Me.Close()
    End Sub
End Class