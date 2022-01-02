Imports System.Linq
Imports TipTracker.Core
Imports TipTracker.Reporting
Imports TipTracker.Utilities

Public Class frmPrintTipReportsV2
    Private ReadOnly _payPeriod As PayPeriod
    Private ReadOnly _tips As IEnumerable(Of Tip)

    Public Sub New(payPeriod As PayPeriod, tips As IEnumerable(Of Tip))
        InitializeComponent()
        _payPeriod = payPeriod
        _tips = tips

        Dim servers = _tips _
            .GroupBy(Function(t) t.EarnedBy) _
            .Select(Function(g) g.Key) _
            .Distinct() _
            .OrderBy(Function(s) s.LastName) _
            .ThenBy(Function(s) s.FirstName) _
            .ThenBy(Function(s) s.PosId) _
            .ToArray()

        cboServers.Items.AddRange(servers)
    End Sub

    Private Function FilterTips() As List(Of Tip)
        Dim filteredTips = _tips.AsEnumerable()

        If optSelectedServer.Checked Then
            filteredTips = filteredTips _
                .Where(Function(t) t.EarnedBy Is CType(cboServers.SelectedItem, Server))
        End If

        If optSelectedDate.Checked Then
            filteredTips = filteredTips _
                .Where(Function(t) t.EarnedOn = Date.Parse(txtSelectedDate.Text))
        End If

        If Not optCreditCard.Checked Then
            filteredTips = filteredTips.Where(Function(t) t.Type IsNot TipTypes.CreditCard)
        End If

        If Not optRoomCharge.Checked Then
            filteredTips = filteredTips.Where(Function(t) t.Type IsNot TipTypes.RoomCharge)
        End If

        If Not optSpecialFunction.Checked Then
            filteredTips = filteredTips.Where(Function(t) t.Type IsNot TipTypes.SpecialFunction)
        End If

        If Not optCash.Checked Then
            filteredTips = filteredTips.Where(Function(t) t.Type IsNot TipTypes.Cash)
        End If

        Return filteredTips.ToList()
    End Function

    Private Function GetBuilder(filteredTips As IEnumerable(Of Tip)) As ReportBuilder
        If optByServer.Checked Then
            Return New TipsByServerBuilder(filteredTips, optSummary.Checked)
        Else
            Return New TipsByDateBuilder(filteredTips, optSummary.Checked)
        End If
    End Function

    Private Sub optSelectedServer_CheckedChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles optSelectedServer.CheckedChanged
        cboServers.Enabled = optSelectedServer.Checked
        cboServers.SelectedIndex = -1
    End Sub

    Private Sub optSelectedDate_CheckedChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles optSelectedDate.CheckedChanged
        txtSelectedDate.Enabled = optSelectedDate.Checked
        txtSelectedDate.Text = ""
        If optSelectedDate.Checked = True Then
            txtSelectedDate.Select()
        End If
    End Sub

    Private Sub btnCancel_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnClose.Click
        Me.Close()
    End Sub

    Private Function CheckForErrors() As Boolean
        If optSelectedDate.Checked = True Then
            If txtSelectedDate.Text = "" Then
                MessageBox.Show("You must enter a date or select the option to include all dates.", "Invalid Selection", MessageBoxButtons.OK)
                txtSelectedDate.Select()
                Return True
            End If

            Dim dteSelectedDate As Date
            Dim dtePeriodStart =_payPeriod.Start
            Dim dtePeriodEnd = _payPeriod.End

            Try
                dteSelectedDate = CDate(txtSelectedDate.Text)
            Catch ex As Exception
                MessageBox.Show("That is not the proper format for a date.", "Invalid Entry", MessageBoxButtons.OK)
                txtSelectedDate.Clear()
                txtSelectedDate.Select()
                Return True
            End Try

            If dteSelectedDate < dtePeriodStart Or dteSelectedDate > dtePeriodEnd Then
                MessageBox.Show("You must select a date within the pay period.", "Invalid Entry", MessageBoxButtons.OK)
                txtSelectedDate.Clear()
                txtSelectedDate.Select()
                Return True
            End If
        End If

        If optSelectedServer.Checked = True And cboServers.SelectedIndex = -1 Then
            MessageBox.Show("You must select a server or select the option to include all servers.", "Invalid Selection", MessageBoxButtons.OK)
            Return True
        End If

        Dim intTipTypes As Integer = 0

        For Each checkBox As Windows.Forms.CheckBox In grpTips.Controls
            If checkBox.Checked = True Then
                intTipTypes += 1
            End If
        Next

        If intTipTypes = 0 Then
            MessageBox.Show("You must select at least one tip type to include.", "Invalid Selection", MessageBoxButtons.OK)
            Return True
        End If

        Return False

    End Function

    Private Sub PrintOrPreviewButtonClicked(sender As Object, e As EventArgs) Handles btnPreview.Click, btnPrint.Click
        If CheckForErrors() Then Exit Sub
        Dim tips = FilterTips()

        If Not tips.Any() Then
            MessageBox.Show("No tips were found that meet the selected criteria.", "No Tips Found", MessageBoxButtons.OK)
            Return
        End If

        If sender Is btnPrint Then
            PrintReport(tips)
        Else
            PreviewReport(tips)
        End If
    End Sub

    Private Sub PrintReport(tips As IEnumerable(Of Tip)) 
        Cursor.Current = Cursors.WaitCursor

        Dim report = GetBuilder(tips).BuildPreparedReport()
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

    Private Sub PreviewReport(tips As IEnumerable(Of Tip)) 
        Cursor.Current = Cursors.WaitCursor

        Try
            Dim reportBuilder = GetBuilder(tips)

            Using dialog As New frmReportPreview(AddressOf reportBuilder.PrepareReport)
                dialog.ShowDialog()
            End Using

        Catch ex As Exception
            MessageBox.Show("Could not display the document.  Check that there is a printer installed and that a default printer has been selected.", "Cannot Preview Document", MessageBoxButtons.OK)
        End Try

        Cursor.Current = Cursors.Default
    End Sub
End Class