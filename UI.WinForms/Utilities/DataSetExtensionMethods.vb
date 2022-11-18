Imports System.Linq
Imports System.Runtime.CompilerServices
Imports TipTracker.Common.Data.PayPeriod
Imports TipTracker.Core

Namespace Utilities
    ''' <summary>
    ''' Provides extension methods for working with typed DataSet objects.
    ''' </summary>
    Module DataSetExtensionMethods
        ''' <summary>
        ''' Gets a value indicating whether a data row is not deleted or detached from the data set.
        ''' </summary>
        ''' <param name="row">The row to check.</param>
        ''' <returns>True if the row is neither deleted nor detached; otherwise, false.</returns>
        <Extension>
        Public Function NotDeletedOrDetached(row As DataRow) As Boolean
            Return row.RowState <> DataRowState.Deleted AndAlso row.RowState <> DataRowState.Detached
        End Function

        ''' <summary>
        ''' Converts a <see cref="FileDataSet.ServersRow"/> to a <see cref="Server"/> instance.
        ''' </summary>
        ''' <param name="row">The row to convert.</param>
        ''' <returns>The resulting <see cref="Server"/>.</returns>
        <Extension>
        Public Function ToServer(row As FileDataSet.ServersRow) As Server
            Return New Server() With {
                .PosId = row("ServerNumber").ToString(),
                .FirstName = row("FirstName").ToString(),
                .LastName = row("LastName").ToString(),
                .SuppressChit = CBool(row("SuppressChit"))}
        End Function

        ''' <summary>
        ''' Converts a <see cref="FileDataSet.SpecialFunctions"/> row to a <see cref="[Event]"/> instance.
        ''' </summary>
        ''' <param name="row">The row to convert.</param>
        ''' <param name="includeTips">True to include tips attached to the event; false to exclude tips.</param>
        ''' <returns>The resulting <see cref="[Event]"/>.</returns>
        <Extension>
        Public Function ToEvent(row As FileDataSet.SpecialFunctionsRow, Optional includeTips As Boolean = False) As [Event]
            Dim converted = New [Event] With {
                .Name = row.SpecialFunction,
                .[Date] = row._Date}

            If includeTips Then
                converted.Tips = row.GetTipsRows() _
                    .Where(Function(t) t.RowState <> DataRowState.Deleted AndAlso t.RowState <> DataRowState.Detached) _
                    .Select(Function(t) t.ToTip()) _
                    .ToList()
            End If

            Return converted
        End Function

        ''' <summary>
        ''' Converts a <see cref="FileDataSet.SpecialFunctions"/> row to a <see cref="Tip"/> instance.
        ''' </summary>
        ''' <param name="row">The row to convert.</param>
        ''' <returns>The resulting <see cref="Tip"/>.</returns>
        <Extension>
        Public Function ToTip(row As FileDataSet.TipsRow) As Tip
            Return New Tip() With {
                .Amount = row.Amount,
                .EarnedBy = row.ServersRowParent.ToServer(),
                .Type = TipTypes.Parse(row.Description),
                .EarnedOn = row.WorkingDate,
                .OrdinalId = row.TipID,
                .[Event] = If(row.IsSpecialFunctionNull(), Nothing, row.SpecialFunctionsRow.ToEvent(False))}
        End Function
    End Module
End Namespace