Imports System.Linq
Imports Microsoft.Reporting.WinForms
Imports TipTracker.Core
Imports TipTracker.Core.Reporting
Imports TipTracker.Utilities

Namespace Reporting
    ''' <summary>
    ''' Provides methods to build tip chits for printing or display.
    ''' </summary>
    Public Class TipChitBuilder
        Inherits ReportBuilder

        Private ReadOnly _servers As IEnumerable(Of Server)

        ''' <summary>
        ''' Creates a new instance of the service class.
        ''' </summary>
        ''' <param name="servers">The collection of servers for which to build tip chits.</param>
        Public Sub New(servers As IEnumerable(Of Server))
            _servers = servers
        End Sub

        ''' <inheritdoc />
        Public Overrides Sub PrepareReport(report As LocalReport)
            Dim tips = _servers.SelectMany(Function (s) s.Tips)
            Dim reportDefinition  = ReportDefinitions.TipChit
            Dim serverDataSource  = New ReportDataSource("Servers", _servers _
                .Select(Function(s) New With {
                    .PosId = s.PosId,
                    .LastName = s.LastName,
                    .FirstName = s.FirstName}))
            Dim tipsDataSource = New ReportDataSource("Tips", tips _
                .Select(Function(t) New With {
                    .Amount = t.Amount,
                    .EarnedBy = t.EarnedBy.PosId,
                    .EarnedOn = t.EarnedOn,
                    .Type = t.Type.Name,
                    .[Event] = If (IsNothing(t.SpecialEvent), String.Empty, t.SpecialEvent.Name)}))
            Dim tipTypesDataSource = New ReportDataSource("TipTypes", TipTypes.Values)

            report.DisplayName = "Tip Chits"
            report.LoadReportDefinition(reportDefinition)
            report.DataSources.Add(serverDataSource)
            report.DataSources.Add(tipsDataSource)
            report.DataSources.Add(tipTypesDataSource)
        End Sub
    End Class
End NameSpace