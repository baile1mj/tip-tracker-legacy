Imports System.Linq
Imports TipTracker.Common.Data.PayPeriod
Imports TipTracker.Core

Namespace Utilities
    ''' <summary>
    ''' Provides an anti-corruption layer to help isolate the <see cref="DataSet"/> layer in preparation for
    ''' the application's conversion to use business objects.
    ''' </summary>
    Public Class BusinessObjectService
        Private ReadOnly _data As PayPeriodData

        ''' <summary>
        ''' Creates a new instance of the service class.
        ''' </summary>
        ''' <param name="payPeriodData"></param>
        Public Sub New (payPeriodData As PayPeriodData)
            _data = payPeriodData
        End Sub

        ''' <summary>
        ''' Gets the tips as a collection of business objects.
        ''' </summary>
        ''' <returns>The collection of tips for the pay period.</returns>
        Public Function GetTips() As IEnumerable(Of Tip)
            Dim serversById = _data.FileDataSet.Servers.AsEnumerable() _
                .Select(Function(r) New Server With {
                    .PosId = r.ServerNumber,
                    .LastName = r.LastName,
                    .FirstName = r.FirstName,
                    .SuppressChit = r.SuppressChit}) _
                .ToDictionary(Function(s) s.PosId)
            Dim eventsByName = _data.FileDataSet.SpecialFunctions.AsEnumerable() _
                .Select(Function (r) New [Event] With {
                    .Name = r.SpecialFunction,
                    .[Date] = r._Date}) _
                .ToDictionary(Function(e) e.Name)

            Dim tips = _data.FileDataSet.Tips.AsEnumerable() _
                .Select(Function (r) New Tip With {
                    .Amount = r.Amount,
                    .EarnedOn = r.WorkingDate,
                    .EarnedBy = serversById(r.ServerNumber),
                    .Type = TipTypes.Parse(r.Description),
                    .[Event] = If(Not IsNothing(r.SpecialFunctionsRow), eventsByName(r.SpecialFunction), Nothing)}) _
                .ToList()

            For Each tip In tips 
                tip.EarnedBy.Tips.Add(tip)
            Next

            Return tips
        End Function

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
        Public Function GetEventDataStore() As IDataStore(Of [Event])
            Return New EventObjectStore(_data)
        End Function

        ''' <summary>
        ''' Gets the data store to use for updating server records.
        ''' </summary>
        ''' <returns></returns>
        Public Function GetServerDataStore() As IDataStore(Of Server) 
            Throw New NotImplementedException()
        End Function
    End Class
End Namespace