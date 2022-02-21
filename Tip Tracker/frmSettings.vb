Public Class frmSettings

    Friend Property DefaultDirectory() As String
        Get
            Return txtDefaultDataDirectory.Text
        End Get
        Set(ByVal value As String)
            txtDefaultDataDirectory.Text = value
        End Set
    End Property

    Private Sub btnSetDefaultDirectory_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnSetDefaultDirectory.Click
        If dlgBrowseFolders.ShowDialog <> Windows.Forms.DialogResult.OK Then
            dlgBrowseFolders.Dispose()
            Exit Sub
        End If

        Me.DefaultDirectory = dlgBrowseFolders.SelectedPath

        dlgBrowseFolders.Dispose()
    End Sub

    Private Sub btnCancel_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnCancel.Click
        Me.Close()
    End Sub

    Private Sub btnOK_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnOK.Click
        If Me.DefaultDirectory = "" Then
            MessageBox.Show("You must select a default data directory.", "Invalid Selection", MessageBoxButtons.OK)
            Exit Sub
        End If

        Me.DialogResult = Windows.Forms.DialogResult.OK
        Me.Close()
    End Sub
End Class