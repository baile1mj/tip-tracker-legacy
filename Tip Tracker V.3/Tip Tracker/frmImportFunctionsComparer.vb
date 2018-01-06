Public Class frmImportFunctionsComparer
    Friend m_dsParentDataSet As New FileDataSet
    Private m_strSpecialFunction As String
    Private m_strFunctionDate As String

    Friend Property SpecialFunction() As String
        Get
            Return m_strSpecialFunction
        End Get
        Set(ByVal value As String)
            m_strSpecialFunction = value
        End Set
    End Property

    Friend Property FunctionDate() As String
        Get
            Return m_strFunctionDate
        End Get
        Set(ByVal value As String)
            m_strFunctionDate = value
        End Set
    End Property

    Private Sub frmImportFunctionsComparer_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
        txtImportedFunction.Text = Me.SpecialFunction & " (" & Me.FunctionDate & ")"
        Me.optNew.Checked = True
        PopulateComboBox()
    End Sub

    Private Sub PopulateComboBox()
        cboSelectFunction.Items.Clear()

        For Each row As DataRow In m_dsParentDataSet.SpecialFunctions.Rows
            If row.RowState <> DataRowState.Deleted Then
                Dim strSpecialFunction As String = row("SpecialFunction").ToString
                Dim strFunctionDate As String = Format(CDate(row("Date")), "MM/dd/yyyy")
                cboSelectFunction.Items.Add(strSpecialFunction & " (" & strFunctionDate & ")")
            End If
        Next

        cboSelectFunction.Sorted = True
    End Sub

    Private Function ExtractSpecialFunction(ByVal ComboString As String) As String
        Dim strExtractedNumber As String = ""

        Dim intLocation As Integer

        For intLocation = 1 To Len(ComboString)
            Dim c As Char = GetChar(ComboString, intLocation)

            If c = "(" Then
                Exit For
            End If
        Next

        strExtractedNumber = Microsoft.VisualBasic.Left(ComboString, Len(ComboString) - intLocation)
        strExtractedNumber = Trim(strExtractedNumber)

        Return strExtractedNumber
    End Function

    Private Sub optNew_CheckedChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles optNew.CheckedChanged
        Select Case optExisting.Checked
            Case True
                cboSelectFunction.Enabled = True
                cboSelectFunction.Focus()
                txtFunctionName.Enabled = False
                txtFunctionDate.Enabled = False
                txtFunctionName.Clear()
                txtFunctionDate.Clear()
            Case Else
                cboSelectFunction.Enabled = False
                cboSelectFunction.SelectedIndex = -1
                txtFunctionName.Enabled = True
                txtFunctionDate.Enabled = True
                txtFunctionName.Focus()
                txtFunctionName.Text = Me.SpecialFunction
                txtFunctionDate.Text = Me.FunctionDate
        End Select
    End Sub

    Private Sub btnCancel_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnCancel.Click
        Me.Close()
    End Sub

    Private Sub txtFunctionName_KeyPress(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyPressEventArgs) Handles txtFunctionName.KeyPress
        If e.KeyChar = ChrW(13) Then
            e.Handled = True
            txtFunctionDate.Focus()
        End If
    End Sub

    Private Sub txtFunctionDate_KeyPress(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyPressEventArgs) Handles txtFunctionDate.KeyPress
        If e.KeyChar = ChrW(13) Then
            e.Handled = True
            btnNext.Focus()
        End If
    End Sub

    Private Sub btnNext_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnNext.Click
        Select Case optExisting.Checked
            Case True
                If cboSelectFunction.SelectedIndex = -1 Then
                    MessageBox.Show("You must select a function.", "Select Function", MessageBoxButtons.OK)
                    cboSelectFunction.Focus()
                    Exit Sub
                End If

                Dim strSpecialFunction As String = ExtractSpecialFunction(cboSelectFunction.SelectedItem.ToString)

                Me.SpecialFunction = strSpecialFunction
                Me.FunctionDate = Format(m_dsParentDataSet.SpecialFunctions.FindBySpecialFunction(strSpecialFunction)("Date").ToString, "MM/dd/yyyy")

            Case Else
                If txtFunctionName.Text = "" Then
                    MessageBox.Show("You must enter a function name.", "Enter Function Name", MessageBoxButtons.OK)
                    txtFunctionName.Focus()
                    Exit Sub
                End If
                If txtFunctionDate.Text = "" Then
                    MessageBox.Show("You must enter a function date.", "Enter Function Date", MessageBoxButtons.OK)
                    txtFunctionDate.Focus()
                    Exit Sub
                End If
                If Not (m_dsParentDataSet.SpecialFunctions.FindBySpecialFunction(txtFunctionName.Text) Is Nothing) Then
                    MessageBox.Show("The special function you entered is already in use.  Please use a different function name or choose an existing function.", "Invalid Entry", MessageBoxButtons.OK)
                    txtFunctionName.Clear()
                    txtFunctionName.Focus()
                    Exit Sub
                End If

                Me.SpecialFunction = txtFunctionName.Text
                Me.FunctionDate = txtFunctionDate.Text

        End Select
        Me.DialogResult = Windows.Forms.DialogResult.OK
        Me.Close()
    End Sub

    Private Sub txtFunctionDate_LostFocus(ByVal sender As Object, ByVal e As System.EventArgs) Handles txtFunctionDate.LostFocus
        Try
            Dim dteTemp As Date = CDate(txtFunctionDate.Text)

            txtFunctionDate.Text = Format(dteTemp, "MM/dd/yyyy")
        Catch ex As Exception
            MessageBox.Show("That is not the proper format for a date.", "Invalid Entry", MessageBoxButtons.OK)
            txtFunctionDate.Clear()
            txtFunctionDate.Focus()
        End Try
    End Sub
End Class