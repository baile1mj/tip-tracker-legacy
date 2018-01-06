Public Class frmAddEditServer

    Friend Property ServerNumber() As String
        Get
            Return txtServerNumber.Text
        End Get
        Set(ByVal value As String)
            txtServerNumber.Text = value
        End Set
    End Property

    Friend Property FirstName() As String
        Get
            Return txtFirstName.Text
        End Get
        Set(ByVal value As String)
            txtFirstName.Text = value
        End Set
    End Property

    Friend Property LastName() As String
        Get
            Return txtLastName.Text
        End Get
        Set(ByVal value As String)
            txtLastName.Text = value
        End Set
    End Property

    Friend Property SuppressChit() As Boolean
        Get
            Return optSuppressChit.Checked
        End Get
        Set(ByVal value As Boolean)
            optSuppressChit.Checked = value
        End Set
    End Property

    Private Sub btnCancel_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnCancel.Click
        Me.Close()
    End Sub

    Private Sub btnOk_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnOk.Click
        If txtServerNumber.Text = "" Then
            MessageBox.Show("You must enter a server number.", "Invalid Entry", MessageBoxButtons.OK)
            txtServerNumber.Focus()
            Exit Sub
        End If

        If txtFirstName.Text = "" Then
            MessageBox.Show("You must enter a first name.", "Invalid Entry", MessageBoxButtons.OK)
            txtFirstName.Focus()
            Exit Sub
        End If

        If txtLastName.Text = "" Then
            MessageBox.Show("You must enter a last name.", "Invalid Entry", MessageBoxButtons.OK)
            txtLastName.Focus()
            Exit Sub
        End If

        Me.DialogResult = Windows.Forms.DialogResult.OK
        Me.Close()
    End Sub

    Private Sub txtServerNumber_GotFocus(ByVal sender As Object, ByVal e As System.EventArgs) Handles txtServerNumber.GotFocus
        If txtServerNumber.Text <> "" Then txtServerNumber.SelectAll()
    End Sub

    Private Sub txtServerNumber_KeyPress(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyPressEventArgs) Handles txtServerNumber.KeyPress
        If e.KeyChar = ChrW(13) Then
            e.Handled = True
            txtFirstName.Focus()
        End If
        If e.KeyChar = "|" Or e.KeyChar = " " Then
            e.Handled = True
        End If
    End Sub

    Private Sub txtFirstName_GotFocus(ByVal sender As Object, ByVal e As System.EventArgs) Handles txtFirstName.GotFocus
        If txtFirstName.Text <> "" Then txtFirstName.SelectAll()
    End Sub

    Private Sub txtFirstName_KeyPress(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyPressEventArgs) Handles txtFirstName.KeyPress
        If e.KeyChar = ChrW(13) Then
            e.Handled = True
            txtLastName.Focus()
        End If
        If e.KeyChar = "|" Then
            e.Handled = True
        End If
    End Sub

    Private Sub txtLastName_GotFocus(ByVal sender As Object, ByVal e As System.EventArgs) Handles txtLastName.GotFocus
        If txtLastName.Text <> "" Then txtLastName.SelectAll()
    End Sub

    Private Sub txtLastName_KeyPress(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyPressEventArgs) Handles txtLastName.KeyPress
        If e.KeyChar = ChrW(13) Then
            e.Handled = True
            btnOk.Focus()
        End If
        If e.KeyChar = "|" Then
            e.Handled = True
        End If
    End Sub
End Class