Public Class frmManageServers

    Private Sub frmManageServers_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
        Me.ServersBindingSource.DataSource = frmMain.GlobalDataSet
        Me.ServersBindingSource.DataMember = frmMain.GlobalDataSet.Servers.TableName

        Me.ServersBindingSource.Sort = "ServerNumber"

        Me.ServersDataGridView.Columns("ServerNumber").DataPropertyName = frmMain.GlobalDataSet.Servers.Columns("ServerNumber").ColumnName
        Me.ServersDataGridView.Columns("FirstName").DataPropertyName = frmMain.GlobalDataSet.Servers.Columns("FirstName").ColumnName
        Me.ServersDataGridView.Columns("LastName").DataPropertyName = frmMain.GlobalDataSet.Servers.Columns("LastName").ColumnName
    End Sub

    Private Sub btnAdd_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnAdd.Click
        Dim blnErrorState As Boolean = True

        frmAddEditServer.Text = "Add Server"

        While blnErrorState = True
            If frmAddEditServer.ShowDialog <> Windows.Forms.DialogResult.OK Then
                frmAddEditServer.Dispose()
                Exit Sub
            End If

            If Not (frmMain.GlobalDataSet.Servers.FindByServerNumber(frmAddEditServer.ServerNumber) Is Nothing) Then
                MessageBox.Show("The server number you entered already exists in the data file.  Please enter a different number.", "Invalid Entry", MessageBoxButtons.OK)
                frmAddEditServer.ServerNumber = ""
            Else
                blnErrorState = False
            End If
        End While

        Dim drNewRow As DataRow = frmMain.GlobalDataSet.Servers.NewRow

        drNewRow("ServerNumber") = frmAddEditServer.ServerNumber
        drNewRow("FirstName") = frmAddEditServer.FirstName
        drNewRow("LastName") = frmAddEditServer.LastName

        frmMain.GlobalDataSet.Servers.Rows.Add(drNewRow)

        blnErrorState = False

        frmAddEditServer.Dispose()
    End Sub

    Private Sub btnEdit_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnEdit.Click
        Dim strServerNumber As String = Me.ServersDataGridView.Item("ServerNumber", Me.ServersBindingSource.Position).Value.ToString
        Dim strFirstName As String = frmMain.GlobalDataSet.Servers.FindByServerNumber(strServerNumber)("FirstName").ToString
        Dim strLastName As String = frmMain.GlobalDataSet.Servers.FindByServerNumber(strServerNumber)("LastName").ToString

        With frmAddEditServer
            .Text = "Edit Server"
            .txtServerNumber.ReadOnly = True
            .ServerNumber = strServerNumber
            .FirstName = strFirstName
            .LastName = strLastName

            If .ShowDialog <> Windows.Forms.DialogResult.OK Then
                .Dispose()
                Exit Sub
            End If

            If .ServerNumber = strServerNumber And .FirstName = strFirstName And .LastName = strLastName Then
                .Dispose()
                Exit Sub
            End If

            frmMain.GlobalDataSet.Servers.FindByServerNumber(strServerNumber)("FirstName") = .FirstName
            frmMain.GlobalDataSet.Servers.FindByServerNumber(strServerNumber)("LastName") = .LastName

            .Dispose()
        End With
    End Sub

    Private Sub btnDelete_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnDelete.Click
        Dim strServerNumber As String = Me.ServersDataGridView.Item("ServerNumber", Me.ServersBindingSource.Position).Value.ToString
        Dim strFirstName As String = frmMain.GlobalDataSet.Servers.FindByServerNumber(strServerNumber)("FirstName").ToString
        Dim strLastName As String = frmMain.GlobalDataSet.Servers.FindByServerNumber(strServerNumber)("LastName").ToString

        If MessageBox.Show("Are you sure you want to delete server " & strServerNumber & " " & _
        strFirstName & " " & strLastName & "?", "Confirm Delete", MessageBoxButtons.YesNo) <> Windows.Forms.DialogResult.Yes Then
            Exit Sub
        End If

        frmMain.GlobalDataSet.Servers.FindByServerNumber(strServerNumber).Delete()
    End Sub

    Private Sub btnClose_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnClose.Click
        Me.Close()
    End Sub
End Class