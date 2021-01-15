Imports TipTracker.Common.Data.PayPeriod

Public Class frmManageSpecialFunctions
    Friend m_dsParentDataSet As New FileDataSet

    Private Sub frmManageSpecialFunctions_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
        Me.SpecialFunctionsBindingSource.DataSource = m_dsParentDataSet
        Me.SpecialFunctionsBindingSource.DataMember = m_dsParentDataSet.SpecialFunctions.TableName
        Me.SpecialFunctionsBindingSource.Sort = "SpecialFunction"
    End Sub

    Private Sub btnClose_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnClose.Click
        Me.Close()
    End Sub

    Private Sub btnAdd_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnAdd.Click
        Dim blnInvalidFunction As Boolean

        While blnInvalidFunction = False
            If frmAddSpecialFunction.ShowDialog <> Windows.Forms.DialogResult.OK Then
                frmAddSpecialFunction.Dispose()
                Exit Sub
            End If

            If Not IsNothing(m_dsParentDataSet.SpecialFunctions.FindBySpecialFunction(frmAddSpecialFunction.FunctionName)) Then
                MessageBox.Show("A special function called " & frmAddSpecialFunction.FunctionName &
                " already exists.  Please enter a different name.", "Duplicate Entry", MessageBoxButtons.OK)
                frmAddSpecialFunction.Dispose()
                Continue While
            End If

            Dim dteFunctionDate, dtePeriodStart, dtePeriodEnd As Date

            dteFunctionDate = CDate(frmAddSpecialFunction.FunctionDate)
            dtePeriodStart = CDate(Me.m_dsParentDataSet.Settings.FindBySetting("PeriodStart")("Value"))
            dtePeriodEnd = CDate(Me.m_dsParentDataSet.Settings.FindBySetting("PeriodEnd")("Value"))

            If dteFunctionDate < dtePeriodStart Or dteFunctionDate > dtePeriodEnd Then
                MessageBox.Show("You must select a date within the pay period.", "Invalid Entry", MessageBoxButtons.OK)
                frmAddSpecialFunction.Dispose()
                Continue While
            End If

            blnInvalidFunction = True
        End While

        Dim drNewRow As DataRow = m_dsParentDataSet.SpecialFunctions.NewRow

        drNewRow("SpecialFunction") = frmAddSpecialFunction.FunctionName
        drNewRow("Date") = frmAddSpecialFunction.FunctionDate
        m_dsParentDataSet.SpecialFunctions.Rows.Add(drNewRow)

        frmAddSpecialFunction.Dispose()
    End Sub

    Private Sub btnDelete_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnDelete.Click
        Dim strFunctionName As String = Me.SpecialFunctionsDataGridView.Item("SpecialFunction", Me.SpecialFunctionsBindingSource.Position).Value.ToString

        If MessageBox.Show("You are about to delete " & strFunctionName & ".  If there are any tips associated with this function, they will be deleted as well.  Do you wish to continue?", "Confirm Delete", MessageBoxButtons.YesNo) <> Windows.Forms.DialogResult.Yes Then
            Exit Sub
        End If

        m_dsParentDataSet.SpecialFunctions.FindBySpecialFunction(strFunctionName).Delete()
    End Sub

    Private Sub btnEdit_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnEdit.Click
        Dim strFunctionName As String = Me.SpecialFunctionsDataGridView.Item("SpecialFunction", Me.SpecialFunctionsBindingSource.Position).Value.ToString
        Dim dteFunctionDate As Date = CDate(m_dsParentDataSet.SpecialFunctions.FindBySpecialFunction(strFunctionName)("Date"))

        With frmAddSpecialFunction
            .FunctionName = strFunctionName
            .FunctionDate = dteFunctionDate
            .Text = "Edit Special Function"

            Dim blnValidDate As Boolean

            While blnValidDate = False
                If .ShowDialog <> Windows.Forms.DialogResult.OK Then
                    .Dispose()
                    Exit Sub
                End If

                If .FunctionName = strFunctionName And .FunctionDate = dteFunctionDate Then
                    .Dispose()
                    Exit Sub
                End If

                Dim dteTemp As Date = .FunctionDate
                Dim dtePeriodStart As Date = CDate(Me.m_dsParentDataSet.Settings.FindBySetting("PeriodStart")("Value"))
                Dim dtePeriodEnd As Date = CDate(Me.m_dsParentDataSet.Settings.FindBySetting("PeriodEnd")("Value"))

                If dteTemp < dtePeriodStart Or dteTemp > dtePeriodEnd Then
                    MessageBox.Show("You must select a date within the pay period.", "Invalid Entry", MessageBoxButtons.OK)
                    .txtFunctionDate.Clear()
                    .txtFunctionDate.Select()
                    Continue While
                End If

                blnValidDate = True
            End While

            m_dsParentDataSet.SpecialFunctions.FindBySpecialFunction(strFunctionName)("SpecialFunction") = .FunctionName
            m_dsParentDataSet.SpecialFunctions.FindBySpecialFunction(.FunctionName)("Date") = .FunctionDate

            Dim dvTips As New DataView
            dvTips.Table = Me.m_dsParentDataSet.Tips
            dvTips.RowFilter = "SpecialFunction = '" & .FunctionName & "'"

            If dvTips.Count <> 0 Then
                Dim i As Integer = 0

                Do Until i = dvTips.Count
                    dvTips.Item(i)("WorkingDate") = .FunctionDate
                    i += 1
                Loop
            End If

            .Dispose()
        End With
    End Sub
End Class