Imports System.Linq
Imports Microsoft.Reporting.WinForms
Imports TipTracker.Core
Imports TipTracker.Core.Reporting

Namespace Reporting
    ''' <summary>
    ''' Provides a method for populating an event detail report.
    ''' </summary>
    Public Class EventReportBuilder
        Inherits ReportBuilder

        Private ReadOnly _tips As IEnumerable(Of Tip)
        Private ReadOnly _sortByDate As Boolean

        ''' <summary>
        ''' Creates a new instance of the service class.
        ''' </summary>
        ''' <param name="tips">The tips on which to report.</param>
        ''' <param name="sortByDate">True if the user has chosen to sort by date; otherwise, false.</param>
        Public Sub New(tips As IEnumerable(Of Tip), sortByDate As Boolean) 
            _tips = tips
            _sortByDate = sortByDate
        End Sub

        ''' <inheritdoc />
        Public Overrides Sub PrepareReport(report As LocalReport)
            Dim tips = _tips _
                .Select(Function(t) New With {
                    .Amount = t.Amount,
                    .EarnedBy = t.EarnedBy.ToString(),
                    .EarnedOn = t.EarnedOn,
                    .Event = t.SpecialEvent.Name,
                    .Type = t.Type.Name})
            Dim servers = _tips _
                .Select(Function (t) t.EarnedBy) _
                .Distinct()
            Dim tipsDataSource = New ReportDataSource("Tips", tips)
            Dim serversDataSource = New ReportDataSource("Servers", servers)

            report.DisplayName = "Event Report"
            report.LoadReportDefinition(ReportDefinitions.EventDetails)
            report.SetParameters(New ReportParameter("SortByDate", _sortByDate.ToString()))
            report.DataSources.Add(tipsDataSource)
            report.DataSources.Add(serversDataSource)
        End Sub
    End Class
End NameSpace