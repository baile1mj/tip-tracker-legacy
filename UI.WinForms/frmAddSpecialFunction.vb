Public Class frmAddSpecialFunction

    Friend Property FunctionName() As String
        Get
            Return txtFunctionName.Text
        End Get
        Set(ByVal value As String)
            txtFunctionName.Text = value
        End Set
    End Property

    Friend Property FunctionDate() As Date
        Get
            Return CDate(txtFunctionDate.Text)
        End Get
        Set(ByVal value As Date)
            txtFunctionDate.Text = Format(value, "MM/dd/yyyy")
        End Set
    End Property

    Private Function RemoveInvalidChars(ByVal FunctionName As String) As String
        Dim strName As String = ""

        If InStr(FunctionName, "'") <> 0 Then
            Dim strNameArray() As String = Strings.Split(FunctionName, "'")


            For i As Integer = LBound(strNameArray) To UBound(strNameArray)
                strName += strNameArray.GetValue(i).ToString
            Next
        Else
            strName = FunctionName
        End If

        Return strName
    End Function

    Private Sub btnCancel_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnCancel.Click
        Me.Close()
    End Sub

    Private Sub btnOK_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnOK.Click
        If txtFunctionName.Text = "" Then
            MessageBox.Show("You must enter the name of the function.", "Enter Function Name", MessageBoxButtons.OK)
            txtFunctionName.Focus()
            Exit Sub
        End If

        If txtFunctionDate.Text = "" Then
            MessageBox.Show("You msut enter the date of the function.", "Enter Function Date", MessageBoxButtons.OK)
            txtFunctionDate.Focus()
            Exit Sub
        End If

        Try
            Dim dteTempDate As Date = CDate(txtFunctionDate.Text)
        Catch ex As Exception
            MessageBox.Show("The value you entered in the date field is not a valid date entry.", "Invalid Entry", MessageBoxButtons.OK)
            txtFunctionDate.Clear()
            txtFunctionDate.Focus()
            Exit Sub
        End Try

        txtFunctionName.Text = RemoveInvalidChars(txtFunctionName.Text)

        Me.DialogResult = Windows.Forms.DialogResult.OK
        Me.Close()
    End Sub

    Private Sub txtFunctionName_KeyPress(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyPressEventArgs) Handles txtFunctionName.KeyPress
        If e.KeyChar = Chr(13) Then
            e.Handled = True

            If txtFunctionName.Text = "" Then
                MessageBox.Show("You must enter the name of the function.", "Enter Function Name", MessageBoxButtons.OK)
                txtFunctionName.Focus()
                Exit Sub
            End If

            txtFunctionDate.Focus()
        End If
    End Sub

    Private Sub txtFunctionDate_KeyPress(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyPressEventArgs) Handles txtFunctionDate.KeyPress
        If e.KeyChar = Chr(13) Then
            e.Handled = True

            btnOK.PerformClick()
        End If
    End Sub

End Class