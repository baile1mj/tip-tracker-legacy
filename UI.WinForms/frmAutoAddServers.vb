Public Class frmAutoAddServers

    Friend Property ServerNumber() As String
        Get
            Return txtServerNumber.Text
        End Get
        Set(ByVal value As String)
            txtServerNumber.Text = value
        End Set
    End Property

    Friend ReadOnly Property FirstName() As String
        Get
            Return txtFirstName.Text
        End Get
    End Property

    Friend ReadOnly Property LastName() As String
        Get
            Return txtLastName.Text
        End Get
    End Property

    Friend Property SuppressChits() As Boolean
        Get
            Return optSuppressChits.Checked
        End Get
        Set(ByVal value As Boolean)
            optSuppressChits.Checked = True
        End Set
    End Property

    Private Sub btnCancel_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnDone.Click
        Me.Close()
    End Sub

    Private Sub btnNext_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnNext.Click
        If txtFirstName.Text = "" Then
            MessageBox.Show("You must enter a first name.", "Invalid Entry", MessageBoxButtons.OK)
            txtFirstName.Select()
            Exit Sub
        End If

        If txtLastName.Text = "" Then
            MessageBox.Show("You must enter a last name.", "Invalid Entry", MessageBoxButtons.OK)
            txtLastName.Select()
            Exit Sub
        End If

        Me.DialogResult = Windows.Forms.DialogResult.OK
        Me.Close()
    End Sub

    Private Sub txtFirstName_KeyPress(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyPressEventArgs) Handles txtFirstName.KeyPress
        If e.KeyChar = Chr(13) Then
            e.Handled = True
            txtLastName.Select()
        End If
    End Sub

    Private Sub txtLastName_KeyPress(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyPressEventArgs) Handles txtLastName.KeyPress
        If e.KeyChar = Chr(13) Then
            e.Handled = True

            If txtFirstName.Text = "" Then
                MessageBox.Show("You must enter a first name.", "Invalid Entry", MessageBoxButtons.OK)
                txtFirstName.Select()
                Exit Sub
            End If

            If txtLastName.Text = "" Then
                MessageBox.Show("You must enter a last name.", "Invalid Entry", MessageBoxButtons.OK)
                txtLastName.Select()
                Exit Sub
            End If

            Me.DialogResult = Windows.Forms.DialogResult.OK
            Me.Close()
        End If
    End Sub
End Class