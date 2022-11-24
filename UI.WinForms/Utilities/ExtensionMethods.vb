Imports System.Linq
Imports System.Runtime.CompilerServices
Imports TipTracker.Common.Data.PayPeriod
Imports TipTracker.Core

Namespace Utilities
    ''' <summary>
    ''' Provides utility extension methods.
    ''' </summary>
    Module ExtensionMethods
        ''' <summary>
        ''' Gets a value indicating whether a row is deleted or detached from its data set so
        ''' that the application does not attempt to use row data that is no longer relevant.
        ''' </summary>
        ''' <param name="row">The row to check.</param>
        ''' <returns>True if the row is neither deleted nor detached; otherwise, false.</returns>
        <Extension>
        Public Function NotDeletedOrDetached(row As DataRow) As Boolean
            Return row.RowState <> DataRowState.Deleted AndAlso row.RowState <> DataRowState.Detached
        End Function

        ''' <summary>
        ''' Converts a server data set row to its corresponding server object.
        ''' </summary>
        ''' <param name="row">The row to convert.</param>
        ''' <returns>The result of the conversion.</returns>
        <Extension>
        Public Function ToServer(row As FileDataSet.ServersRow) As Server
            Return New Server With {
                .FirstName = row.FirstName,
                .LastName = row.LastName,
                .PosId = row.ServerNumber,
                .SuppressChit = row.SuppressChit}
        End Function

        ''' <summary>
        ''' Converts a tip data set row to its corresponding tip object.
        ''' </summary>
        ''' <param name="row">The row to convert.</param>
        ''' <returns>The result of the conversion.</returns>
        <Extension>
        Public Function ToTip(row As FileDataSet.TipsRow) As Tip
            Return New Tip With {
                .Amount = row.Amount,
                .EarnedOn = row.WorkingDate,
                .OrdinalId = row.TipID,
                .Type = TipTypes.Parse(row.Description),
                .EarnedBy = row.ServersRowParent.ToServer(),
                .[Event] = If(row.IsSpecialFunctionNull, Nothing, row.SpecialFunctionsRow?.ToEvent())}
        End Function

        ''' <summary>
        ''' Converts a event data set row to its corresponding event object.
        ''' </summary>
        ''' <param name="row">The row to convert.</param>
        ''' <returns>The result of the conversion.</returns>
        <Extension>
        Public Function ToEvent(row As FileDataSet.SpecialFunctionsRow, Optional includeTips As Boolean = False) As [Event]
            Dim specialFunction = New [Event] With {
                .[Date] = row._Date,
                .Name = row.SpecialFunction}

            If includeTips Then
                specialFunction.Tips = row.GetTipsRows() _
                    .AsEnumerable() _
                    .Where(Function(t) Not t.IsSpecialFunctionNull AndAlso t.SpecialFunctionsRow Is row) _
                    .Select(Function(t) t.ToTip()) _
                    .ToList()
            End If

            Return specialFunction
        End Function
    End Module
End Namespace