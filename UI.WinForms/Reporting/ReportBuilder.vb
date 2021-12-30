Imports Microsoft.Reporting.WinForms

Namespace Reporting
    ''' <summary>
    ''' Provides base functionality for building reports for printing.
    ''' </summary>
    Public MustInherit Class ReportBuilder
        ''' <summary>
        ''' Prepares and populates the data in a report instance, making it ready for printing or display.
        ''' </summary>
        ''' <param name="report">The report to prepare.</param>
        Public MustOverride Sub PrepareReport(report as LocalReport)
    
        ''' <summary>
        ''' Builds a report instance and prepares it for printing or display.
        ''' </summary>
        ''' <returns>The prepared report.</returns>
        Public Function BuildPreparedReport() As LocalReport
            Dim report As New LocalReport()
            PrepareReport(report)

            Return report
        End Function
    End Class
End NameSpace