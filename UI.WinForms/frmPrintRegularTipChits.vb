Imports System.Linq
Imports TipTracker.Core
Imports TipTracker.Reporting

Public Class frmPrintRegularTipChits
    Private ReadOnly _servers as List(Of Server)
    
    Public Sub New(servers As IEnumerable(Of Server))
        InitializeComponent()

        _servers = servers.ToList()

        cboSelectServer.Items.AddRange(_servers.ToArray())
        cboSelectServer.SelectedItem = Nothing

        optAll.Checked = true
    End sub
    
    Private Function GetReportBuilder() As ReportBuilder
        Dim servers = If(cboSelectServer.SelectedItem IsNot Nothing, _
            New List(Of Server) From {CType(cboSelectServer.SelectedItem, Server)}, _
            _servers)

        Return New TipChitBuilder(servers)
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

        Dim builder = GetReportBuilder()
        Using viewer As New frmReportPreview(AddressOf builder.PrepareReport)            
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

        Dim report = GetReportBuilder().BuildPreparedReport()
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