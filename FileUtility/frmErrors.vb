Public Class frmErrors

    Public Sub New(ByVal errorMessage As String)
        InitializeComponent()

        TextBox1.Text = errorMessage
    End Sub

    Private Sub Button1_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Button1.Click
        Me.Close()
    End Sub
End Class