Public Class frmManageServers

    Public Property ServersTable As DataTable

    Public Sub New(ByVal serversTable As DataTable)
        InitializeComponent()

        If IsNothing(serversTable) Then
            Throw New ArgumentException("Cannot modify a null object.", NameOf(serversTable))
        End If

        Me.ServersTable = serversTable
    End Sub

    Private Sub frmManageServers_Load(ByVal sender As Object, ByVal e As EventArgs) Handles MyBase.Load
        ServersBindingSource.DataSource = ServersTable
        ServersBindingSource.Sort = "ServerNumber"

        ServersDataGridView.Columns("ServerNumber").DataPropertyName = ServersTable.Columns("ServerNumber").ColumnName
        ServersDataGridView.Columns("FirstName").DataPropertyName = ServersTable.Columns("FirstName").ColumnName
        ServersDataGridView.Columns("LastName").DataPropertyName = ServersTable.Columns("LastName").ColumnName
        ServersDataGridView.Columns("NoChit").DataPropertyName = ServersTable.Columns("SuppressChit").ColumnName
    End Sub

    Private Sub btnAdd_Click(ByVal sender As Object, ByVal e As EventArgs) Handles btnAdd.Click
        Dim blnErrorState As Boolean = True

        Using frmAddEditServer As New frmAddEditServer()
            frmAddEditServer.Text = "Add Server"

            While blnErrorState = True
                If frmAddEditServer.ShowDialog <> DialogResult.OK Then
                    frmAddEditServer.Dispose()
                    Exit Sub
                End If

                If Not (ServersTable.Rows.Find(frmAddEditServer.ServerNumber) Is Nothing) Then
                    MessageBox.Show("The server number you entered already exists in the data file.  Please enter a different number.", "Invalid Entry", MessageBoxButtons.OK)
                    frmAddEditServer.ServerNumber = ""
                Else
                    blnErrorState = False
                End If
            End While

            Dim drNewRow As DataRow = ServersTable.NewRow

            drNewRow("ServerNumber") = frmAddEditServer.ServerNumber
            drNewRow("FirstName") = frmAddEditServer.FirstName
            drNewRow("LastName") = frmAddEditServer.LastName
            drNewRow("SuppressChit") = frmAddEditServer.SuppressChit

            ServersTable.Rows.Add(drNewRow)
        End Using
    End Sub

    Private Sub btnEdit_Click(ByVal sender As Object, ByVal e As EventArgs) Handles btnEdit.Click
        Dim strServerNumber As String = ServersDataGridView.Item("ServerNumber", ServersBindingSource.Position).Value.ToString
        Dim serverRow As DataRow = ServersTable.Rows.Find(strServerNumber)
        Dim strFirstName As String = serverRow("FirstName").ToString
        Dim strLastName As String = serverRow("LastName").ToString
        Dim blnSuppressChit As Boolean = CBool(serverRow("SuppressChit"))

        Using frmAddEditServer As New frmAddEditServer()
            With frmAddEditServer
                .Text = "Edit Server"
                .txtServerNumber.ReadOnly = True
                .ServerNumber = strServerNumber
                .FirstName = strFirstName
                .LastName = strLastName
                .SuppressChit = blnSuppressChit

                If .ShowDialog <> DialogResult.OK Then
                    Exit Sub
                End If

                If .ServerNumber = strServerNumber And .FirstName = strFirstName And .LastName = strLastName And .SuppressChit = blnSuppressChit Then
                    Exit Sub
                End If

                serverRow("FirstName") = .FirstName
                serverRow("LastName") = .LastName
                serverRow("SuppressChit") = .SuppressChit
            End With
        End Using
    End Sub

    Private Sub btnDelete_Click(ByVal sender As Object, ByVal e As EventArgs) Handles btnDelete.Click
        Dim strServerNumber As String = ServersDataGridView.Item("ServerNumber", ServersBindingSource.Position).Value.ToString
        Dim strFirstName As String = ServersTable.Rows.Find(strServerNumber)("FirstName").ToString
        Dim strLastName As String = ServersTable.Rows.Find(strServerNumber)("LastName").ToString

        If MessageBox.Show("Are you sure you want to delete server " & strServerNumber & " " &
        strFirstName & " " & strLastName & "?", "Confirm Delete", MessageBoxButtons.YesNo) <> DialogResult.Yes Then
            Exit Sub
        End If

        ServersTable.Rows.Find(strServerNumber).Delete()
    End Sub

    Private Sub btnClose_Click(ByVal sender As Object, ByVal e As EventArgs) Handles btnClose.Click
        Close()
    End Sub
End Class