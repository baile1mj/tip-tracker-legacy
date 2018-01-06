Public Class frmLogin

    Friend ReadOnly Property CurrentUser() As String
        Get
            Return txtUserName.Text
        End Get
    End Property

    Private Sub btnExit_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnExit.Click
        End
    End Sub

    Private Sub btnLogin_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnLogin.Click
        Login()
    End Sub

    Private Sub Login()
        txtUserName.Text = UCase(txtUserName.Text)

        Static intBadLogins As Integer

        If txtUserName.Text = "" And txtPassword.Text = "" Then
            txtUserName.Focus()
            Exit Sub
        End If

        If intBadLogins = 5 Then End

        If frmMain.GlobalDataSet.Users.FindByUserName(txtUserName.Text) Is Nothing Then
            MessageBox.Show("The user name could not be found.  Please retype the user name.", "Invalid User Name", MessageBoxButtons.OK)
            txtUserName.Clear()
            txtPassword.Clear()
            txtUserName.Focus()
            intBadLogins += 1
            Exit Sub
        End If

        If frmMain.GlobalDataSet.Users.FindByUserName(txtUserName.Text)("Password").ToString <> txtPassword.Text Then
            MessageBox.Show("The user name and password you entered do not match.", "Invalid Entry", MessageBoxButtons.OK)
            txtUserName.Clear()
            txtPassword.Clear()
            txtUserName.Focus()
            intBadLogins += 1
            Exit Sub
        End If

        Me.Close()
    End Sub

    Private Sub txtUserName_KeyPress(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyPressEventArgs) Handles txtUserName.KeyPress
        If e.KeyChar = Chr(13) Then
            e.Handled = True
            txtPassword.Focus()
        End If
    End Sub

    Private Sub txtUserName_LostFocus(ByVal sender As Object, ByVal e As System.EventArgs) Handles txtUserName.LostFocus
        txtUserName.Text = UCase(txtUserName.Text)
    End Sub

    Private Sub txtPassword_KeyPress(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyPressEventArgs) Handles txtPassword.KeyPress
        If e.KeyChar = Chr(13) Then
            e.Handled = True
            Login()
        End If
    End Sub
End Class