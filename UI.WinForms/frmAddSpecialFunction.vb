Public Class frmAddSpecialFunction

    Public ReadOnly Property EventName As String
        Get
            Return txtFunctionName.Text.Replace("'", "")
        End Get
    End Property

    Public ReadOnly Property EventDate As DateTime
        Get
            Return DateTime.Parse(txtFunctionDate.Text)
        End Get
    End Property
    
    Public Sub New()
        Me.New(String.Empty, DateTime.MinValue)
    End Sub

    Public Sub New(eventName As String, eventDate As DateTime)
        InitializeComponent()

        txtFunctionName.Text = eventName
        
        If Not eventDate = DateTime.MinValue
            txtFunctionDate.Text = eventDate.ToString("MM/dd/yyyy")
        End If
    End Sub
    
    Private Sub btnCancel_Click(sender As Object, e As EventArgs) Handles btnCancel.Click
        Close()
    End Sub

    Private Sub btnOK_Click(sender As Object, e As EventArgs) Handles btnOK.Click
        If txtFunctionName.Text = "" Then
            MessageBox.Show("You must enter the name of the function.", "Enter Function Name", MessageBoxButtons.OK)
            txtFunctionName.Select()
            Exit Sub
        End If

        If txtFunctionDate.Text = "" Then
            MessageBox.Show("You must enter the date of the function.", "Enter Function Date", MessageBoxButtons.OK)
            txtFunctionDate.Select()
            Exit Sub
        End If

        If Not DateTime.TryParse(txtFunctionDate.Text, DateTime.MinValue)
            MessageBox.Show("The value you entered in the date field is not a valid date entry.", "Invalid Entry", MessageBoxButtons.OK)
            txtFunctionDate.Clear()
            txtFunctionDate.Select()
            Exit Sub
        End If

        DialogResult = DialogResult.OK
        Close()
    End Sub

    Private Sub txtFunctionName_KeyPress(sender As Object, e As KeyPressEventArgs) Handles txtFunctionName.KeyPress
        If e.KeyChar = Chr(13) Then
            e.Handled = True

            If txtFunctionName.Text = "" Then
                MessageBox.Show("You must enter the name of the function.", "Enter Function Name", MessageBoxButtons.OK)
                txtFunctionName.Select()
                Exit Sub
            End If

            txtFunctionDate.Select()
        End If
    End Sub

    Private Sub txtFunctionDate_KeyPress(sender As Object, e As KeyPressEventArgs) Handles txtFunctionDate.KeyPress
        If e.KeyChar = Chr(13) Then
            e.Handled = True

            btnOK.PerformClick()
        End If
    End Sub
End Class