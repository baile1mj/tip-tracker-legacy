Public Class frmAutoAddInput

    Friend Property Seed() As String
        Get
            Return txtSeed.Text
        End Get
        Set(ByVal value As String)
            txtSeed.Text = value
        End Set
    End Property

    Friend Property SuppressChits() As Boolean
        Get
            Return optSuppressChits.Checked
        End Get
        Set(ByVal value As Boolean)
            optSuppressChits.Checked = value
        End Set
    End Property

    Private Sub btnCancel_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnCancel.Click
        Me.Close()
    End Sub

    Private Sub btnOK_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnOK.Click
        If txtSeed.Text = "" Then
            MessageBox.Show("You must enter a seed.", "Invalid Entry", MessageBoxButtons.OK)
            txtSeed.Clear()
            txtSeed.Select()
            Exit Sub
        End If

        If Not IsNumeric(Me.Seed) Then
            MessageBox.Show("You must enter a number.", "Invalid Entry", MessageBoxButtons.OK)
            txtSeed.Clear()
            txtSeed.Select()
            Exit Sub
        End If

        Me.DialogResult = Windows.Forms.DialogResult.OK
        Me.Close()
    End Sub

    Private Sub txtSeed_KeyPress(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyPressEventArgs) Handles txtSeed.KeyPress
        If e.KeyChar = Chr(8) Then Exit Sub

        If e.KeyChar = Chr(13) Then
            e.Handled = True

            If txtSeed.Text = "" Then
                MessageBox.Show("You must enter a seed.", "Invalid Entry", MessageBoxButtons.OK)
                txtSeed.Clear()
                txtSeed.Select()
                Exit Sub
            End If

            If Not IsNumeric(Me.Seed) Then
                MessageBox.Show("You must enter a number.", "Invalid Entry", MessageBoxButtons.OK)
                txtSeed.Clear()
                txtSeed.Select()
                Exit Sub
            End If

            Me.DialogResult = Windows.Forms.DialogResult.OK
            Me.Close()
        End If
        If Not IsNumeric(e.KeyChar) Then
            e.Handled = True
        End If
    End Sub
End Class