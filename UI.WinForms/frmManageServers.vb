Imports System.Linq
Imports TipTracker.Core
Imports TipTracker.Utilities

Public Class frmManageServers

    Public ReadOnly Servers As IList(Of Server)

    Public Sub New(ByVal existingServers As List(Of Server))
        InitializeComponent()

        'Make sure we always have a list to work with.
        If IsNothing(existingServers) Then existingServers = New List(Of Server)()

        'Set up the bindable collection.
        Servers = New SortableBindingList(Of Server)(existingServers)
    End Sub

    Public Sub New(ByVal serversTable As DataTable)
        InitializeComponent()

        If IsNothing(serversTable) Then
            Throw New ArgumentException("Cannot modify a null object.", NameOf(serversTable))
        End If

        Dim existingServers As List(Of Server) = serversTable _
            .AsEnumerable() _
            .Select(Function(ByVal r) New Server() With {
                .PosId = r("ServerNumber").ToString(),
                .FirstName = r("FirstName").ToString(),
                .LastName = r("LastName").ToString(),
                .SuppressChit = CBool(r("SuppressChit"))}) _
            .ToList()
        Servers = New SortableBindingList(Of Server)(existingServers)
    End Sub

    Private Sub frmManageServers_Load(ByVal sender As Object, ByVal e As EventArgs) Handles MyBase.Load
        ServerBindingSource.DataSource = Servers
        ServerBindingSource.Sort = NameOf(Server.PosId)
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

                Dim existingServer As Server = Servers _
                    .FirstOrDefault(Function(ByVal s) s.PosId = frmAddEditServer.ServerNumber)

                If existingServer IsNot Nothing Then
                    MessageBox.Show("The server number you entered already exists in the data file.  Please enter a different number.",
                        "Invalid Entry", MessageBoxButtons.OK)
                    frmAddEditServer.ServerNumber = ""
                Else
                    blnErrorState = False
                End If
            End While

            Dim newServer As New Server() With {
                .PosId = frmAddEditServer.ServerNumber,
                .FirstName = frmAddEditServer.FirstName,
                .LastName = frmAddEditServer.LastName,
                .SuppressChit = frmAddEditServer.SuppressChit}

            Servers.Add(newServer)
            ServerBindingSource.Position = Servers.IndexOf(newServer)
        End Using
    End Sub

    Private Sub btnEdit_Click(ByVal sender As Object, ByVal e As EventArgs) Handles btnEdit.Click

        Dim server As Server = Servers(ServersDataGridView.CurrentRow.Index)

        Using frmAddEditServer As New frmAddEditServer(server.Clone(), False)
            With frmAddEditServer
                .Text = "Edit Server"

                If .ShowDialog <> DialogResult.OK Then Exit Sub

                If Not server.Matches(frmAddEditServer.Server) Then
                    Servers(ServersDataGridView.CurrentRow.Index) = frmAddEditServer.Server
                End If

                ServerBindingSource.Position = Servers.IndexOf(frmAddEditServer.Server)
            End With
        End Using
    End Sub

    Private Sub btnDelete_Click(ByVal sender As Object, ByVal e As EventArgs) Handles btnDelete.Click
        Dim server As Server = Servers(ServersDataGridView.CurrentRow.Index)

        If MessageBox.Show("Are you sure you want to delete server " & server.PosId & " " &
        server.FirstName & " " & server.LastName & "?", "Confirm Delete", MessageBoxButtons.YesNo) <> DialogResult.Yes Then
            Exit Sub
        End If

        Servers.Remove(server)
    End Sub

    Private Sub btnClose_Click(ByVal sender As Object, ByVal e As EventArgs) Handles btnClose.Click
        Close()
    End Sub
End Class