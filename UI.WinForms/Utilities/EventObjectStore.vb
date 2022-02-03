Imports System.Linq
Imports TipTracker.Common.Data.PayPeriod
Imports TipTracker.Core

Namespace Utilities

    ''' <summary>
    ''' Provides an anti-corruption layer facade for working with events stored in the application's DataSet.
    ''' </summary>
    Public Class EventObjectStore
        Implements IDataStore(Of [Event])

        Private Readonly _data As PayPeriodData

        ''' <summary>
        ''' Creates a new instance of the service class.
        ''' </summary>
        ''' <param name="payPeriodData">The backing data store containing the events.</param>
        Public sub new(payPeriodData As PayPeriodData)
            _data = payPeriodData
        End sub

        ''' <inheritdoc />
        Public Sub Add(newItem As [Event]) Implements IDataStore(Of [Event]).Add
            Dim newRow = _data.FileDataSet.SpecialFunctions.NewSpecialFunctionsRow()

            newRow.SpecialFunction = newItem.Name
            newRow._Date = newItem.Date

            _data.FileDataSet.SpecialFunctions.AddSpecialFunctionsRow(newRow)
        End Sub

        ''' <inheritdoc />
        Public Sub Update(existing As [Event], newValue As [Event]) Implements IDataStore(Of [Event]).Update
            Dim existingRow = _data.FileDataSet.SpecialFunctions _
                .AsQueryable() _
                .First(Function (r) r.SpecialFunction = existing.Name)

            existingRow.SpecialFunction = newValue.Name
            existingRow._Date = newValue.Date
        End Sub

        ''' <inheritdoc />
        Public Sub Delete(item As [Event]) Implements IDataStore(Of [Event]).Delete
            Dim existingRow = _data.FileDataSet.SpecialFunctions _
                .AsQueryable() _
                .First(Function (r) r.SpecialFunction = item.Name)
            existingRow.Delete()
        End Sub

        ''' <inheritdoc />
        Public Function GetAll() As IEnumerable(Of [Event]) Implements IDataStore(Of [Event]).GetAll
            Dim tipsRelation = _data.FileDataSet.SpecialFunctions.ChildRelations(0)
            Dim events = _data.FileDataSet.SpecialFunctions _
                .AsEnumerable() _
                .Select(Function (r) New With {
                    .Event = New [Event] With { .Name = r.SpecialFunction, .[Date] = r._Date },
                    .TipRows = r.GetChildRows(tipsRelation)}) _
                .ToList()
        
            For Each item In events
                Dim tips = item.TipRows _
                    .Select(Function(r) DirectCast(r, FileDataSet.TipsRow)) _
                    .Select(Function(r) New Tip With {
                        .Type = TipTypes.SpecialFunction,
                        .Amount = r.Amount,
                        .EarnedBy = Nothing,
                        .EarnedOn = r.WorkingDate,
                        .[Event] = item.Event
                        }) _
                    .ToList()
                item.Event.Tips = tips
            Next
        
            Return events _
                .Select(Function(anon) anon.Event)
        End Function
    End Class
End NameSpace