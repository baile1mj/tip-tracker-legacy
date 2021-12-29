Imports System.Linq
Imports Microsoft.Reporting.WinForms
Imports TipTracker.Core
Imports TipTracker.Core.Reporting
Imports TipTracker.Utilities

Public Class frmPrintRegularTipChits
    Private ReadOnly _servers as List(Of Server)
    
    Public Sub New(servers As IEnumerable(Of Server))
        InitializeComponent()

        _servers = servers.ToList()

        cboSelectServer.Items.AddRange(_servers.ToArray())
        cboSelectServer.SelectedItem = Nothing

        optAll.Checked = true
    End sub

    Private Sub PrepareReport(report As LocalReport) 
        Dim servers = If(cboSelectServer.SelectedItem IsNot Nothing, _
            New List(Of Server) From { CType(cboSelectServer.SelectedItem, Server) }, _
            _servers)
        Dim tips = servers.SelectMany(Function (s) s.Tips)
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
                .[Event] = If (IsNothing(t.Event), String.Empty, t.Event.Name)}))
        Dim tipTypesDataSource = New ReportDataSource("TipTypes", TipTypes.Values)

        report.DisplayName = "Tip Chits"
        report.LoadReportDefinition(reportDefinition)
        report.DataSources.Add(serverDataSource)
        report.DataSources.Add(tipsDataSource)
        report.DataSources.Add(tipTypesDataSource)
    End Sub

    Private Function BuildReport() As LocalReport
        Dim report As New LocalReport()
        PrepareReport(report)

        Return report
    End Function

    Private Sub optAll_CheckedChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles optAll.CheckedChanged
        cboSelectServer.Enabled = optSelected.Checked
        cboSelectServer.SelectedIndex = -1
    End Sub

    Private Sub optSelected_CheckedChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles optSelected.CheckedChanged
        cboSelectServer.Enabled = optSelected.Checked
        cboSelectServer.SelectedIndex = -1
    End Sub

    Private Sub btnClose_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnClose.Click
        Me.Close()
    End Sub

    Private Sub btnPrintPreview_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnPrintPreview.Click
        If optSelected.Checked = True And cboSelectServer.SelectedIndex = -1 Then
            MessageBox.Show("You must select a server.", "Select Server", MessageBoxButtons.OK)
            Exit Sub
        End If

        Cursor.Current = Cursors.WaitCursor

        Using viewer As New frmReportPreview(AddressOf PrepareReport)            
            viewer.ShowDialog()
        End Using
        
        Cursor.Current = Cursors.Default
    End Sub

    Private Sub btnPrint_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnPrint.Click
        If optSelected.Checked = True And cboSelectServer.SelectedIndex = -1 Then
            MessageBox.Show("You must select a server.", "Select Server", MessageBoxButtons.OK)
            Exit Sub
        End If

        Cursor.Current = Cursors.WaitCursor

        Dim report = BuildReport()
        Dim documentFactory As New PrintDocumentFactory(report)
        Dim document = documentFactory.BuildPrintDocument("Tip Chits")
        Dim dlgPrint As New PrintDialog

        With dlgPrint
            .AllowCurrentPage = False
            .AllowPrintToFile = False
            .AllowSelection = False
            .AllowSomePages = False
            .Document = document
            .ShowNetwork = True
            .UseEXDialog = False
        End With

        Try
            If dlgPrint.ShowDialog = DialogResult.OK Then
                document.Print()
            End If
        Catch ex As Exception
            MessageBox.Show("Could not print the document.  Check that there is a printer installed and that a default printer has been selected.", "Cannot Print Document", MessageBoxButtons.OK)
            Exit Sub
        End Try

        Cursor.Current = Cursors.Default

        dlgPrint.Dispose()
        document.Dispose()
        documentFactory.Dispose()
        report.Dispose()
    End Sub
End Class