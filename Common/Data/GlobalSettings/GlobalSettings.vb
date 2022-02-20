Imports TipTracker.Common.Data.PayPeriod
Imports TipTracker.Core

Namespace Data.GlobalSettings
    ''' <summary>
    ''' Represents the settings shared by all users within an organization.
    ''' </summary>
    Public Class GlobalSettings
        ''' <summary>
        ''' Gets the data set that stores the global settings.
        ''' </summary>
        ''' <returns>The data set containing the shared settings.</returns>
        Public ReadOnly GlobalDataSet As GlobalDataSet

        ''' <summary>
        ''' Gets the default directory for data files.
        ''' </summary>
        ''' <returns>The default data directory.</returns>
        Public Property DefaultDataDirectory As String
            Get
                Return GlobalDataSet.Settings.FindBySetting("DefaultDirectory")("Value").ToString
            End Get

            Set(ByVal value As String)
                GlobalDataSet.Settings.FindBySetting("DefaultDirectory")("Value") = value
            End Set
        End Property

        ''' <summary>
        ''' Creates a new instance of the settings class.
        ''' </summary>
        Public Sub New()
            Me.New(New GlobalDataSet())
        End Sub

        ''' <summary>
        ''' Creates a new instance of the settings class.
        ''' </summary>
        ''' <param name="dataSet">The data set containing the settings for this instance.</param>
        Public Sub New(ByVal dataSet As GlobalDataSet)
            If IsNothing(dataSet) Then
                Throw New ArgumentException("The settings data set cannot be null.", NameOf(dataSet))
            End If

            Const DEFAULT_DIRECTORY_SETTING As String = "DefaultDirectory"

            'Remove any unknown settings.
            For Each row As DataRow In dataSet.Settings.Rows
                If row("Setting").ToString() <> DEFAULT_DIRECTORY_SETTING Then
                    row.Delete()
                End If
            Next

            'Make sure there is an entry for the default directory.
            If dataSet.Settings.Rows.Count = 0 Then
                dataSet.Settings.Rows.Add(DEFAULT_DIRECTORY_SETTING, String.Empty)
            End If

            'If any servers are missing a value for "suppress chit", set the value to false.
            Const SUPPRESS_CHIT_COLUMN_NAME As String = "SuppressChit"

            For Each row As DataRow In dataSet.Servers.Rows
                If IsDBNull(SUPPRESS_CHIT_COLUMN_NAME) Then
                    row(SUPPRESS_CHIT_COLUMN_NAME) = False
                End If
            Next

            GlobalDataSet = dataSet
        End Sub

        ''' <summary>
        ''' Gets a value indicating whether any servers are defined in the settings.
        ''' </summary>
        ''' <returns>True if there are servers defined; otherwise, false.</returns>
        Public Function HasServers() As Boolean
            Return GlobalDataSet.Servers.Rows.Count > 0
        End Function

        ''' <summary>
        ''' Gets the template collection of servers to use when creating a new
        ''' pay period.
        ''' </summary>
        ''' <returns>The collection of servers.</returns>
        Public Function GetTemplateServers() As List(Of Server)
            Return GlobalDataSet.Servers _
                .AsEnumerable() _
                .Where(Function(r) r.RowState <> DataRowState.Deleted AndAlso r.RowState <> DataRowState.Detached) _
                .Select(Function(r) New Server() With {
                    .PosId = r("ServerNumber").ToString(),
                    .FirstName = r("FirstName").ToString(),
                    .LastName = r("LastName").ToString(),
                    .SuppressChit = CBool(r("SuppressChit"))}) _
                .ToList()
        End Function

        ''' <summary>
        ''' Updates the template collection of servers.
        ''' </summary>
        ''' <param name="updatedServers">The updated collection of servers to use as a template.</param>
        Public Sub UpdateTemplateServers(updatedServers As IList(Of Server))
            GlobalDataSet.Servers.Rows.Clear()

            For Each server As Server In updatedServers
                Dim newRow As GlobalDataSet.ServersRow = GlobalDataSet.Servers.NewServersRow
                newRow.ServerNumber = server.PosId
                newRow.FirstName = server.FirstName
                newRow.LastName = server.LastName
                newRow.SuppressChit = server.SuppressChit

                GlobalDataSet.Servers.AddServersRow(newRow)
            Next

            GlobalDataSet.AcceptChanges()
        End Sub

        ''' <summary>
        ''' Creates a new pay period.
        ''' </summary>
        ''' <param name="startDate">The pay period start date.</param>
        ''' <param name="endDate">The pay period end date.</param>
        ''' <returns>The newly-created pay period.</returns>
        Public Function CreatePayPeriod(startDate As DateTime, endDate As DateTime) As PayPeriodData
            Return PayPeriodData.Create(startDate, endDate, GlobalDataSet.Servers.Copy())
        End Function
    End Class
End Namespace