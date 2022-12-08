Imports System.Linq
Imports TipTracker.Common.Data.PayPeriod
Imports TipTracker.Core

Namespace Utilities
    ''' <summary>
    ''' Provides an anti-corruption layer to help isolate the <see cref="DataSet"/> layer in preparation for
    ''' the application's conversion to use business objects.
    ''' </summary>
    Public Class PayPeriodObjectService
        Private ReadOnly _data As PayPeriodData

        ''' <summary>
        ''' Creates a new instance of the service class.
        ''' </summary>
        ''' <param name="payPeriodData"></param>
        Public Sub New(payPeriodData As PayPeriodData)
            _data = payPeriodData
        End Sub

        ''' <summary>
        ''' Gets the tips as a collection of business objects.
        ''' </summary>
        ''' <returns>The collection of tips for the pay period.</returns>
        Public Function GetTips() As IEnumerable(Of Tip)
            Dim serversById = _data.FileDataSet.Servers.AsEnumerable() _
                .Where(Function(r) r.RowState <> DataRowState.Deleted AndAlso r.RowState <> DataRowState.Detached) _
                .Select(Function(r) New Server With {
                    .PosId = r.ServerNumber,
                    .LastName = r.LastName,
                    .FirstName = r.FirstName,
                    .SuppressChit = r.SuppressChit}) _
                .ToDictionary(Function(s) s.PosId)
            Dim eventsByName = _data.FileDataSet.SpecialFunctions.AsEnumerable() _
                .Where(Function(r) r.RowState <> DataRowState.Deleted AndAlso r.RowState <> DataRowState.Detached) _
                .Select(Function(r) New SpecialEvent With {
                    .Name = r.SpecialFunction,
                    .[Date] = r._Date}) _
                .ToDictionary(Function(e) e.Name)

            Dim tips = _data.FileDataSet.Tips.AsEnumerable() _
                .Where(Function(r) r.RowState <> DataRowState.Deleted AndAlso r.RowState <> DataRowState.Detached) _
                .Select(Function(r) New Tip With {
                    .Amount = r.Amount,
                    .EarnedOn = r.WorkingDate,
                    .EarnedBy = serversById(r.ServerNumber),
                    .Type = TipTypes.Parse(r.Description),
                    .SpecialEvent = If(Not IsNothing(r.SpecialFunctionsRow), eventsByName(r.SpecialFunction), Nothing)}) _
                .ToList()

            For Each tip In tips
                tip.EarnedBy.Tips.Add(tip)
            Next

            Return tips
        End Function

        ''' <summary>
        ''' Reassigns the tips from one server to another.
        ''' </summary>
        ''' <param name="fromServer">The servers whose tips will be reassigned.</param>
        ''' <param name="toServer">The server who will receive the tips.</param>
        Public Sub ReassignTips(fromServer As Server, toServer As Server)
            Dim tipRows = _data.FileDataSet.Servers.FindByServerNumber(fromServer.PosId).GetTipsRows()
            Dim recipientRow = _data.FileDataSet.Servers.FindByServerNumber(toServer.PosId)

            For Each tip In tipRows
                tip.SetParentRow(recipientRow)
            Next
        End Sub

        ''' <summary>
        ''' Gets the pay period as a business object.
        ''' </summary>
        ''' <returns>The pay period.</returns>
        Public Function GetPayPeriod() As PayPeriod
            Return New PayPeriod(_data.PayPeriodStart, _data.PayPeriodEnd, Nothing)
        End Function

        ''' <summary>
        ''' Gets the data store to use for updating event records.
        ''' </summary>
        ''' <returns>The data store for events.</returns>
        Public Function GetEventDataStore() As IDataStore(Of SpecialEvent)
            Return New EventObjectStore(_data)
        End Function

        ''' <summary>
        ''' Gets the data store to use for updating server records.
        ''' </summary>
        ''' <returns>The data store for servers.</returns>
        Public Function GetServerDataStore() As IDataStore(Of Server)
            Return New ServerObjectStore(_data)
        End Function

        ''' <summary>
        ''' Gets the data store to use for updating tip records.
        ''' </summary>
        ''' <returns>The data store for tips.</returns>
        Public Function GetTipDataStore() As IDataStore(Of Tip)
            Return New TipObjectStore(_data)
        End Function
    End Class
End Namespace