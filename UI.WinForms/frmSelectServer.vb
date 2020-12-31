Imports TipTracker.Core

Public Class frmSelectServer

    Private Class ServerWrapper
        Friend ReadOnly Property Server As Server

        Friend Sub New(server As Server)
            _Server = server
        End Sub

        Public Overrides Function ToString() As String
            Return $"{Server.LastName}, {Server.FirstName}: {Server.PosId}"
        End Function
    End Class

    Public Sub New(displayText As String, servers As List(Of Server))
        InitializeComponent()
        lblSelectServer.Text = displayText
        servers.ForEach(Sub(s) cboSelectServer.Items.Add(New ServerWrapper(s)))
    End Sub

    Public Function GetSelectedServer() As Server
        If cboSelectServer.SelectedItem Is Nothing Then Return Nothing

        Return DirectCast(cboSelectServer.SelectedItem, ServerWrapper).Server
    End Function

    Private Sub btnCancel_Click(sender As Object, e As EventArgs) Handles btnCancel.Click
        Close()
    End Sub

    Private Sub btnOK_Click(sender As Object, e As EventArgs) Handles btnOK.Click
        If cboSelectServer.SelectedItem Is Nothing Then
            MessageBox.Show("You must select a server.", "Select Server", MessageBoxButtons.OK)
            Exit Sub
        End If

        DialogResult = DialogResult.OK
        Close()
    End Sub

End Class