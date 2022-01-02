Imports System.Linq
Imports TipTracker.Core
Imports TipTracker.Reporting

Public Class frmPrintSpecialFunctionReportV2
    Private ReadOnly _tips As IEnumerable(Of Tip)

    Public sub New(tips As IEnumerable(Of Tip)) 
        InitializeComponent()

        _tips = tips _
            .OrderBy(Function(t) t.Event.Name) _
            .ThenBy(Function(t) t.ToString())
        
        Dim functions = _tips _
            .Select(Function(t) t.Event) _
            .Distinct() _
            .Where(Function (e) e IsNot Nothing) _
            .OrderBy(Function(e) e.Name) _
            .ThenBy(Function(e) e.Date) _
            .ToArray()

        cboFunctions.Items.AddRange(functions)
    End sub

    Private Function GetReportBuilder() As ReportBuilder
        Dim eventTips = If(cboFunctions.SelectedItem IsNot Nothing, _
            _tips.Where(Function(t) t.Event.Name = CType(cboFunctions.SelectedItem, [Event]).Name).ToList(), _
            _tips)

        Return New EventReportBuilder(eventTips, optFunctionDate.Checked)
    End Function

    Private Sub optSelectedFunction_CheckedChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles optSelectedFunction.CheckedChanged
        cboFunctions.Enabled = optSelectedFunction.Checked
        cboFunctions.SelectedIndex = -1
    End Sub

    Private Sub btnClose_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnClose.Click
        Me.Close()
    End Sub

    Private Sub btnPrint_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnPrint.Click
        If optSelectedFunction.Checked = True And cboFunctions.SelectedIndex = -1 Then
            MessageBox.Show("You must select a function.", "Select Function", MessageBoxButtons.OK)
            Exit Sub
        End If

        Cursor.Current = Cursors.WaitCursor

        Dim report = GetReportBuilder().BuildPreparedReport()
        Dim documentFactory As New PrintDocumentFactory(report)
        Dim document = documentFactory.BuildPrintDocument(report.DisplayName)
        Dim dlgPrint As New PrintDialog

        With dlgPrint
            .AllowCurrentPage = False
            .AllowPrintToFile = False
            .AllowSelection = False
            .AllowSomePages = False
            .Document = document
            .ShowNetwork = True
            .UseEXDialog = True
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

        Cursor.Current = Cursors.Default
    End Sub

    Private Sub btnPreview_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnPreview.Click
        If optSelectedFunction.Checked = True And cboFunctions.SelectedIndex = -1 Then
            MessageBox.Show("You must select a function.", "Select Function", MessageBoxButtons.OK)
            Exit Sub
        End If

        Cursor.Current = Cursors.WaitCursor
        Dim builder = GetReportBuilder()

        Try
       
            Using dialog As New frmReportPreview(AddressOf builder.PrepareReport)
                dialog.ShowDialog()
            End Using
        Catch ex As Exception
            MessageBox.Show("Could not display the document.  Check that there is a printer installed and that a default printer has been selected.", "Cannot Preview Document", MessageBoxButtons.OK)
        End Try

        Cursor.Current = Cursors.Default
    End Sub
End Class