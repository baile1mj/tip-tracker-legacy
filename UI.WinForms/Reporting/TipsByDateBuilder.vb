Imports System.Linq
Imports Microsoft.Reporting.WinForms
Imports TipTracker.Core
Imports TipTracker.Core.Reporting
Imports TipTracker.Utilities

Namespace Reporting
    ''' <summary>
    ''' Provides methods to build a tip report for tips sorted by date.
    ''' </summary>
    Public Class TipsByDateBuilder
        Inherits ReportBuilder

        Private ReadOnly _tips As IEnumerable(Of Tip)
        Private ReadOnly _isSummary As Boolean

        ''' <summary>
        ''' Creates a new instance of the service class.
        ''' </summary>
        ''' <param name="tips"></param>
        ''' <param name="isSummary"></param>
        Public Sub New(tips As IEnumerable(Of Tip), isSummary As Boolean)
            _tips = tips
            _isSummary = isSummary
        End Sub
        
        ''' <inheritdoc />
        Public Overrides Sub PrepareReport(report As LocalReport)
            Dim reportDefinition  = ReportDefinitions.TipsByDate
            Dim tipsDataSource = New ReportDataSource("Tips", _tips _
                .OrderBy(Function(t) t.Type.Classification.SortIndex) _
                .Select(Function(t) New With {
                    .Amount = t.Amount,
                    .EarnedBy = t.EarnedBy.ToString(),
                    .EarnedOn = t.EarnedOn,
                    .Type = t.Type.Name,
                    .[Event] = If (IsNothing(t.SpecialEvent), String.Empty, t.SpecialEvent.Name)}))
            Dim sortedTypes = TipTypes.Values _
                .OrderBy(Function(t) t.Classification.SortIndex) _
                .Select(Function (t) New With {
                    .Name = t.Name,
                    .Classification = t.Classification.Description,
                    .IsPayable = t.IsPayable,
                    .IsClaimedTip = t.IsClaimedTip,
                    .IsEventOriginated = t.IsEventOriginated,
                    .CanSpecifyDate = t.CanSpecifyDate})
            Dim tipTypesDataSource = New ReportDataSource("TipTypes", sortedTypes)

            report.DisplayName = "Tip Report"
            report.LoadReportDefinition(reportDefinition)
            report.SetParameters(New ReportParameter("IsSummary", _isSummary.ToString()))
            report.DataSources.Add(tipsDataSource)
            report.DataSources.Add(tipTypesDataSource)
        End Sub
    End Class
End Namespace

