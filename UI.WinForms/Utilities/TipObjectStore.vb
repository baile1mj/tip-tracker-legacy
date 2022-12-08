Imports TipTracker.Common.Data.PayPeriod
Imports TipTracker.Core

Namespace Utilities
    ''' <summary>
    ''' Provides an anti-corruption layer facade for working with tips stored in the application's DataSet.
    ''' </summary>
    Public Class TipObjectStore
        Implements IDataStore(Of Tip)

        Private ReadOnly _data As PayPeriodData

        ''' <summary>
        ''' Creates a new instance of the service class.
        ''' </summary>
        ''' <param name="payPeriodData">The backing data store containing the tips.</param>
        Public Sub New(payPeriodData As PayPeriodData)
            _data = payPeriodData
        End Sub

        ''' <inheritdoc />
        Public Function GetAll() As IEnumerable(Of Tip) Implements IDataStore(Of Tip).GetAll
            _data.FileDataSet.Tips _
                .AsEnumerable() _
                .Where(Function(r) r.NotDeletedOrDetached()) _
                .Select(Function(row) row.ToTip())
        End Function

        ''' <inheritdoc />
        Public Function Contains(item As Tip) As Boolean Implements IDataStore(Of Tip).Contains
            Throw New NotImplementedException
        End Function

        ''' <inheritdoc />
        Public Sub Add(newItem As Tip) Implements IDataStore(Of Tip).Add
            Dim row = _data.FileDataSet.Tips.NewTipsRow()

            With row
                .Amount = newItem.Amount
                .WorkingDate = newItem.EarnedOn
                .Description = newItem.Type.ToString()
                .ServerNumber = newItem.EarnedBy.PosId
                .FirstName = newItem.EarnedBy.FirstName
                .LastName = newItem.EarnedBy.LastName
            End With

            If newItem.SpecialEvent IsNot Nothing Then
                row.SpecialFunction = newItem.SpecialEvent.Name
            End If

            _data.FileDataSet.Tips.AddTipsRow(row)
        End Sub

        ''' <inheritdoc />
        Public Sub Update(existing As Tip, newValue As Tip) Implements IDataStore(Of Tip).Update
            Throw New NotImplementedException
        End Sub

        ''' <inheritdoc />
        Public Sub Delete(item As Tip) Implements IDataStore(Of Tip).Delete
            Throw New NotImplementedException
        End Sub
    End Class
End Namespace