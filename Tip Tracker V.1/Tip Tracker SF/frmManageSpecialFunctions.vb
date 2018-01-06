Public Class frmManageSpecialFunctions

    Private Sub frmManageSpecialFunctions_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
        Me.SpecialFunctionsBindingSource.DataSource = frmMain.FileDataSet
        Me.SpecialFunctionsBindingSource.DataMember = frmMain.FileDataSet.SpecialFunctions.TableName
        Me.SpecialFunctionsBindingSource.Sort = "SpecialFunction"
    End Sub

    Private Sub btnClose_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnClose.Click
        Me.Close()
    End Sub

    Private Sub btnAdd_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnAdd.Click
        If frmAddSpecialFunction.ShowDialog <> Windows.Forms.DialogResult.OK Then
            frmAddSpecialFunction.Dispose()
            Exit Sub
        End If

        If Not IsNothing(frmMain.FileDataSet.SpecialFunctions.FindBySpecialFunction(frmAddSpecialFunction.FunctionName)) Then
            MessageBox.Show("A special function called " & frmAddSpecialFunction.FunctionName & _
            " already exists.  Please enter a different name.", "Duplicate Entry", MessageBoxButtons.OK)
            frmAddSpecialFunction.Dispose()
            Exit Sub
        End If

        Dim drNewRow As DataRow = frmMain.FileDataSet.SpecialFunctions.NewRow

        drNewRow("SpecialFunction") = frmAddSpecialFunction.FunctionName
        drNewRow("Date") = frmAddSpecialFunction.FunctionDate
        frmMain.FileDataSet.SpecialFunctions.Rows.Add(drNewRow)

        frmAddSpecialFunction.Dispose()
    End Sub

    Private Sub btnDelete_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnDelete.Click
        Dim strFunctionName As String = Me.SpecialFunctionsDataGridView.Item("SpecialFunction", Me.SpecialFunctionsBindingSource.Position).Value.ToString

        If MessageBox.Show("You are about to delete " & strFunctionName & ".  If there are any tips associated with this function, they will be deleted as well.  Do you wish to continue?", "Confirm Delete", MessageBoxButtons.YesNo) <> Windows.Forms.DialogResult.Yes Then
            Exit Sub
        End If

        frmMain.FileDataSet.SpecialFunctions.FindBySpecialFunction(strFunctionName).Delete()
    End Sub

End Class