Imports System.Linq
Imports TipTracker.Common.Data.PayPeriod
Imports TipTracker.Core

Namespace Utilities
    Public Class ServerObjectStore
        Implements IDataStore(Of Server)

        Private ReadOnly _data As PayPeriodData

        Public Sub New (payPeriodData As PayPeriodData)
            _data = payPeriodData
        End sub

        ''' <inheritdoc />
        Public Function GetAll() As IEnumerable(Of Server) Implements IDataStore(Of Server).GetAll
            Return _data.FileDataSet.Servers _
                .AsEnumerable() _
                .Where(Function(r) r.NotDeletedOrDetached()) _
                .Select(Function(r) r.ToServer()) _
                .ToList()
        End Function

        ''' <inheritdoc />
        Public Function Contains(item As Server) As Boolean Implements IDataStore(Of Server).Contains
            Return _data.FileDataSet.Servers _
                .AsQueryable() _
                .Where(Function(r) r.RowState <> DataRowState.Deleted AndAlso r.RowState <> DataRowState.Detached) _
                .Any(Function(r) r.ServerNumber = item.PosId)
        End Function

        ''' <inheritdoc />
        Public Sub Add(newItem As Server) Implements IDataStore(Of Server).Add
            Dim row =_data.FileDataSet.Servers.NewServersRow()

            With row
                .ServerNumber = newItem.PosId
                .FirstName = newItem.FirstName
                .LastName = newItem.LastName
                .SuppressChit = newItem.SuppressChit
            End With
            
            _data.FileDataSet.Servers.AddServersRow(row)
        End Sub

        ''' <inheritdoc />
        Public Sub Update(existing As Server, newValue As Server) Implements IDataStore(Of Server).Update
            Dim row = _data.FileDataSet.Servers.FindByServerNumber(existing.PosId)

            With row
                .ServerNumber = newValue.PosId
                .FirstName = newValue.FirstName
                .LastName = newValue.LastName
                .SuppressChit = newValue.SuppressChit
            End With
        End Sub

        ''' <inheritdoc />
        Public Sub Delete(item As Server) Implements IDataStore(Of Server).Delete
            _data.FileDataSet.Servers.FindByServerNumber(item.PosId).Delete()
        End Sub
    End Class
End NameSpace