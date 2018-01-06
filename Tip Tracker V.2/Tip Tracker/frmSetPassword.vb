Public Class frmSetPassword
    Private m_blnHasCurrentPassword As Boolean
    Private m_strCurrentPassword As String = ""

    Friend Property HasCurrentPassword() As Boolean
        Get
            Return m_blnHasCurrentPassword
        End Get
        Set(ByVal value As Boolean)
            m_blnHasCurrentPassword = value
        End Set
    End Property

    Friend WriteOnly Property CurrentPassword() As String
        Set(ByVal value As String)
            m_strCurrentPassword = value
        End Set
    End Property

    Friend ReadOnly Property NewPassword() As String
        Get
            Return txtNewPassword.Text
        End Get
    End Property

    Private Sub btnCancel_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnCancel.Click
        Me.Close()
    End Sub

    Private Sub frmSetPassword_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        If Me.HasCurrentPassword = False Then
            lblCurrentPassword.Visible = False
            txtCurrentPassword.Visible = False
        Else
            lblCurrentPassword.Visible = False
            txtCurrentPassword.Visible = False
        End If
    End Sub

    Private Sub btnOK_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnOK.Click
        If Me.HasCurrentPassword = True And Me.txtCurrentPassword.Text <> m_strCurrentPassword Then
            MessageBox.Show("The password you entered is incorrect.  Please reenter your password.", "Incorrect Password", MessageBoxButtons.OK)
            txtCurrentPassword.Clear()
            txtCurrentPassword.Focus()
            Exit Sub
        End If

        If txtNewPassword.Text <> txtConfirmPassword.Text Then
            MessageBox.Show("The new password and its confirmation do not match.  Please reenter the new password.", "Invalid Entry", MessageBoxButtons.OK)
            txtNewPassword.Clear()
            txtConfirmPassword.Clear()
            txtNewPassword.Focus()
            Exit Sub
        End If

        If txtNewPassword.Text = "" Then
            MessageBox.Show("Your password entry may not be blank.", "Invalid Entry", MessageBoxButtons.OK)
            txtNewPassword.Focus()
            Exit Sub
        End If

        Me.DialogResult = Windows.Forms.DialogResult.OK
        Me.Close()
    End Sub
End Class