Imports System.Drawing.Printing

Public Class frmPrintTipReportsV2
    'The following constants indicate the index of each print order that
    'can be selected in cboPrintOrder.
    Private Const intServerTipType As Integer = 0
    Private Const intServerDate As Integer = 1
    Private Const intDateTipType As Integer = 2
    Private Const intDateServer As Integer = 3

    'Since there are multiple instances of frmEnterTips, a reference to the dataset
    'of the form that called this instance of frmPrintTipReports needs to be created.
    Friend m_dsParentDataSet As New FileDataSet

    Private Sub frmPrintTipReportsV2_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
        Dim dvServers As New DataView

        dvServers.Table = Me.m_dsParentDataSet.Servers
        dvServers.Sort = "LastName, FirstName"

        If dvServers.Count <> 0 Then
            For i As Integer = 0 To dvServers.Count - 1
                Dim strLastName As String = dvServers.Item(i)("LastName").ToString
                Dim strFirstName As String = dvServers.Item(i)("FirstName").ToString
                Dim strServerNumber As String = dvServers.Item(i)("ServerNumber").ToString

                Dim drNewRow As DataRow = Me.ServersLookupDataset.Servers.NewRow

                drNewRow("ServerNumber") = strServerNumber
                drNewRow("NameString") = strLastName & ", " & strFirstName

                Me.ServersLookupDataset.Servers.Rows.Add(drNewRow)
            Next
        End If

        Me.ServersLookupDataset.AcceptChanges()

        cboServers.SelectedIndex = -1
    End Sub

    Private Sub optSelectedServer_CheckedChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles optSelectedServer.CheckedChanged
        cboServers.Enabled = optSelectedServer.Checked
        cboServers.SelectedIndex = -1
    End Sub

    Private Sub optSelectedDate_CheckedChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles optSelectedDate.CheckedChanged
        txtSelectedDate.Enabled = optSelectedDate.Checked
        txtSelectedDate.Text = ""
        If optSelectedDate.Checked = True Then
            txtSelectedDate.Focus()
        End If
    End Sub

    Private Sub btnCancel_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnClose.Click
        Me.Close()
    End Sub

    Private Function CheckForErrors() As Boolean
        If optSelectedDate.Checked = True Then
            If txtSelectedDate.Text = "" Then
                MessageBox.Show("You must enter a date or select the option to include all dates.", "Invalid Selection", MessageBoxButtons.OK)
                txtSelectedDate.Focus()
                Return True
            End If

            Dim dteSelectedDate As Date
            Dim dtePeriodStart As Date = CDate(Me.m_dsParentDataSet.Settings.FindBySetting("PeriodStart")("Value"))
            Dim dtePeriodEnd As Date = CDate(Me.m_dsParentDataSet.Settings.FindBySetting("PeriodEnd")("Value"))

            Try
                dteSelectedDate = CDate(txtSelectedDate.Text)
            Catch ex As Exception
                MessageBox.Show("That is not the proper format for a date.", "Invalid Entry", MessageBoxButtons.OK)
                txtSelectedDate.Clear()
                txtSelectedDate.Focus()
                Return True
            End Try

            If dteSelectedDate < dtePeriodStart Or dteSelectedDate > dtePeriodEnd Then
                MessageBox.Show("You must select a date within the pay period.", "Invalid Entry", MessageBoxButtons.OK)
                txtSelectedDate.Clear()
                txtSelectedDate.Focus()
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

    Private Sub btnPrint_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnPrint.Click
        If CheckForErrors() = True Then Exit Sub

        Dim docReport As New PrintDocument

        docReport.DocumentName = "Tip Report"
        With docReport.DefaultPageSettings.Margins
            .Top = 75
            .Bottom = 75
            .Left = 75
            .Right = 75
        End With

        'Determine how the report should be subtotaled and set the print document accordingly.
        If optSummary.Checked = True Then
            'If the user selects a summary report, determine whether the report should be 
            'ordered by server or by date.
            If optByServer.Checked = True Then
                AddHandler docReport.PrintPage, AddressOf SummaryByServer
            Else
                AddHandler docReport.PrintPage, AddressOf SummaryByDate
            End If
        Else
            If optByServer.Checked = True Then
                AddHandler docReport.PrintPage, AddressOf DetailByServer
            Else
                AddHandler docReport.PrintPage, AddressOf DetailByDate
            End If
        End If

        dlgPrint.Document = docReport

        If dlgPrint.ShowDialog() <> Windows.Forms.DialogResult.OK Then
            dlgPrint.Dispose()
            docReport = Nothing
            Exit Sub
        End If

        Try
            docReport.Print()
        Catch ex As Exception
            MessageBox.Show("Could not print the document.  Check that there is a printer installed and that a default printer has been selected.", "Cannot Print Document", MessageBoxButtons.OK)
        End Try

        dlgPrint.Dispose()

        docReport = Nothing
    End Sub

    Private Sub btnPreview_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnPreview.Click
        If CheckForErrors() = True Then Exit Sub

        Dim docReport As New PrintDocument

        docReport.DocumentName = "Tip Report"
        With docReport.DefaultPageSettings.Margins
            .Top = 75
            .Bottom = 75
            .Left = 75
            .Right = 75
        End With

        'Determine how the report should be subtotaled and set the print document accordingly.
        If optSummary.Checked = True Then
            'If the user selects a summary report, determine whether the report should be 
            'ordered by server or by date.
            If optByServer.Checked = True Then
                AddHandler docReport.PrintPage, AddressOf SummaryByServer
            Else
                AddHandler docReport.PrintPage, AddressOf SummaryByDate
            End If
        Else
            If optByServer.Checked = True Then
                AddHandler docReport.PrintPage, AddressOf DetailByServer
            Else
                AddHandler docReport.PrintPage, AddressOf DetailByDate
            End If
        End If

        Dim dlgPreview As New PrintPreviewDialog
        dlgPreview.ShowIcon = False
        dlgPreview.Document = docReport

        Try
            Windows.Forms.Cursor.Current = Cursors.Default

            ' Convert the dialog into a Form.
            Dim dlg As Form = DirectCast(dlgPreview, Form)

            ' Set a "restore" size.
            dlg.Width = 600
            dlg.Height = 400

            ' Start the dialog maximized.
            dlg.WindowState = FormWindowState.Maximized
            dlg.Icon = frmMain.Icon

            dlg.ShowDialog()
            dlg.Dispose()
        Catch ex As Exception
            MessageBox.Show("Could not display the document.  Check that there is a printer installed and that a default printer has been selected.", "Cannot Preview Document", MessageBoxButtons.OK)
        End Try

        docReport = Nothing
    End Sub

    Private Sub SummaryByServer(ByVal sender As System.Object, ByVal e As PrintPageEventArgs)
        Dim font As New System.Drawing.Font("Calibri", 10)
        Dim fontBold As New System.Drawing.Font("Calibri", 10, FontStyle.Bold)

        Dim fmt As New StringFormat(StringFormatFlags.LineLimit)

        'Initialize local static variables that contain the current line position and
        'the current page number.
        Static intPosition As Single = e.MarginBounds.Top
        Static intPageNumber As Integer = 1

        Const intLineSpacing As Integer = 15
        Const intExtraLineSpacing As Integer = 30

        'Initialize local constants that contain the normal line spacing and padded 
        'line spacing (double spacing).
        Dim intPrintAreaHeight, intPrintAreaWidth, marginLeft, marginTop As Int32

        With e.MarginBounds
            ' Initialize local variables that contain the bounds of the printing 
            ' area rectangle.
            intPrintAreaHeight = .Height
            intPrintAreaWidth = .Width

            ' Initialize local variables to hold margin values that will serve
            ' as the X and Y coordinates for the upper left corner of the printing 
            ' area rectangle.
            marginLeft = .Left ' X coordinate
            marginTop = .Top ' Y coordinate
        End With

        'Draw the header to the page.  Header will be drawn on every page.
        e.Graphics.DrawString("Tip Report - Summary by Server", font, Brushes.Black, marginLeft, intPosition)
        intPosition += intLineSpacing

        e.Graphics.DrawString(Format(DateTime.Now, "MM/dd/yyyy") & " " & Format(DateTime.Now, "t"), font, Brushes.Black, marginLeft, intPosition)
        intPosition += intLineSpacing

        If optAllServers.Checked = True Then
            e.Graphics.DrawString("All Servers", font, Brushes.Black, marginLeft, intPosition)
        Else
            Dim strServerNumber As String = cboServers.SelectedValue.ToString
            Dim strName As String = Me.m_dsParentDataSet.Servers.FindByServerNumber(strServerNumber)("FirstName").ToString & _
                " " & Me.m_dsParentDataSet.Servers.FindByServerNumber(strServerNumber)("LastName").ToString

            e.Graphics.DrawString("Selected Server: " & strName, font, Brushes.Black, marginLeft, intPosition)
        End If
        intPosition += intLineSpacing

        If optAllDates.Checked = True Then
            e.Graphics.DrawString("All Dates", font, Brushes.Black, marginLeft, intPosition)
        Else
            e.Graphics.DrawString("Selected Date: " & Format(CDate(txtSelectedDate.Text), "MM/dd/yyyy"), _
                font, Brushes.Black, marginLeft, intPosition)
        End If
        intPosition += intLineSpacing

        Dim strIncludedTypes As String = "Types Included: "
        If optCreditCard.Checked = True Then strIncludedTypes += "CC "
        If optRoomCharge.Checked = True Then strIncludedTypes += "RC "
        If optSpecialFunction.Checked = True Then strIncludedTypes += "SF "
        If optCash.Checked = True Then strIncludedTypes += "CA "

        e.Graphics.DrawString(strIncludedTypes, font, Brushes.Black, marginLeft, intPosition)
        intPosition += intLineSpacing

        e.Graphics.DrawString("Page " & intPageNumber, font, Brushes.Black, marginLeft, intPosition)
        intPosition += intExtraLineSpacing

        'Create a dataview to list all of the servers whose tips will be printed.
        Dim dvServers As New DataView
        dvServers.Table = Me.m_dsParentDataSet.Servers
        dvServers.Sort = "LastName, FirstName"

        'If only one server is selected, filter the dataview so that server is the 
        'only one included in it.
        If optSelectedServer.Checked = True Then
            dvServers.RowFilter = "ServerNumber = '" & cboServers.SelectedValue.ToString & "'"
        End If

        Static intCurrentServer As Integer = 0
        Static intCurrentStep As Integer = 1

        'The report will be printed in steps.  To avoid repeating a step when the page changes
        'the steps will be tracked by the counter intCurrentStep.  This will also skip over tips
        'that aren't selected.
        Const intName As Integer = 1
        Const intCreditCard As Integer = 2
        Const intRoomCharge As Integer = 3
        Const intSpecialFunction As Integer = 4
        Const intChargeTips As Integer = 5
        Const intCash As Integer = 6
        Const intTotalTips As Integer = 7

        Static decCreditCardGrandTotal As Decimal = 0
        Static decRoomChargeGrandTotal As Decimal = 0
        Static decSpecialFunctionGrandTotal As Decimal = 0
        Static decCashGrandTotal As Decimal = 0

        Do Until intCurrentServer = dvServers.Count
            Static decChargeTipTotal As Decimal = 0
            Static decTipTotal As Decimal = 0

            Dim strServerNumber As String = dvServers.Item(intCurrentServer)("ServerNumber").ToString
            Dim strFirstName As String = dvServers.Item(intCurrentServer)("FirstName").ToString
            Dim strLastName As String = dvServers.Item(intCurrentServer)("LastName").ToString

            Dim dvCounter As New DataView
            dvCounter.Table = Me.m_dsParentDataSet.Tips
            dvCounter.RowFilter = "ServerNumber = '" & strServerNumber & "'"

            If optSelectedDate.Checked = True Then
                dvCounter.RowFilter += " AND WorkingDate = '" & Format(CDate(txtSelectedDate.Text), "MM/dd/yyyy") & "'"
            End If

            If dvCounter.Count = 0 Then
                intCurrentServer += 1
                Continue Do
            End If

            Dim dvTips As New DataView
            dvTips.Table = Me.m_dsParentDataSet.Tips

            If dvTips.Count = 0 Then
                intCurrentServer += 1
                Continue Do
            End If

            While intCurrentStep <= intTotalTips
                dvTips.RowFilter = "ServerNumber = '" & strServerNumber & "'"

                Select Case intCurrentStep
                    Case intName 'Always performed.
                        If intPosition >= marginTop + intPrintAreaHeight Then
                            e.HasMorePages = True
                            intPageNumber += 1
                            intPosition = marginTop
                            Exit Sub
                        End If

                        e.Graphics.DrawString(strServerNumber & " " & strLastName & ", " & strFirstName, _
                            fontBold, Brushes.Black, marginLeft, intPosition)

                        intPosition += intLineSpacing

                    Case intCreditCard
                        If optCreditCard.Checked = False Then
                            intCurrentStep += 1
                            Continue While
                        End If

                        If intPosition >= marginTop + intPrintAreaHeight Then
                            e.HasMorePages = True
                            intPageNumber += 1
                            intPosition = marginTop
                            Exit Sub
                        End If

                        If optSelectedDate.Checked = False Then
                            dvTips.RowFilter += " AND Description = 'Credit Card'"
                        Else
                            dvTips.RowFilter += " AND Description = 'Credit Card' AND WorkingDate = '" & _
                                Format(CDate(txtSelectedDate.Text), "MM/dd/yyyy") & "'"
                        End If

                        Dim decCreditCards As Decimal = 0
                        Dim i As Integer = 0

                        Do Until i = dvTips.Count
                            decCreditCards += CDec(dvTips.Item(i)("Amount"))
                            i += 1
                        Loop

                        e.Graphics.DrawString("Credit Card", font, Brushes.Black, marginLeft, intPosition)

                        Dim strCreditCards As String = Format(decCreditCards, "0.00")
                        Dim intStrLen As Integer = CInt(e.Graphics.MeasureString(strCreditCards, font, _
                            New SizeF(intPrintAreaWidth, intPrintAreaHeight), fmt, Len(strCreditCards), 1).Width)

                        e.Graphics.DrawString(strCreditCards, font, Brushes.Black, marginLeft + intPrintAreaWidth - intStrLen, _
                            intPosition)

                        decChargeTipTotal += decCreditCards
                        decCreditCardGrandTotal += decCreditCards
                        intPosition += intLineSpacing

                    Case intRoomCharge
                        If optRoomCharge.Checked = False Then
                            intCurrentStep += 1
                            Continue While
                        End If

                        If intPosition >= marginTop + intPrintAreaHeight Then
                            e.HasMorePages = True
                            intPageNumber += 1
                            intPosition = marginTop
                            Exit Sub
                        End If

                        If optSelectedDate.Checked = False Then
                            dvTips.RowFilter += " AND Description = 'Room Charge'"
                        Else
                            dvTips.RowFilter += " AND Description = 'Room Charge' AND WorkingDate = '" & _
                                Format(CDate(txtSelectedDate.Text), "MM/dd/yyyy") & "'"
                        End If

                        Dim decRoomCharges As Decimal = 0
                        Dim i As Integer = 0

                        Do Until i = dvTips.Count
                            decRoomCharges += CDec(dvTips.Item(i)("Amount"))
                            i += 1
                        Loop

                        e.Graphics.DrawString("Room Charge", font, Brushes.Black, marginLeft, intPosition)

                        Dim strRoomCharges As String = Format(decRoomCharges, "0.00")
                        Dim intStrLen As Integer = CInt(e.Graphics.MeasureString(strRoomCharges, font, _
                            New SizeF(intPrintAreaWidth, intPrintAreaHeight), fmt, Len(strRoomCharges), 1).Width)

                        e.Graphics.DrawString(strRoomCharges, font, Brushes.Black, marginLeft + intPrintAreaWidth - intStrLen, _
                            intPosition)

                        decChargeTipTotal += decRoomCharges
                        decRoomChargeGrandTotal += decRoomCharges
                        intPosition += intLineSpacing

                    Case intSpecialFunction
                        If optSpecialFunction.Checked = False Then
                            intCurrentStep += 1
                            Continue While
                        End If

                        If intPosition >= marginTop + intPrintAreaHeight Then
                            e.HasMorePages = True
                            intPageNumber += 1
                            intPosition = marginTop
                            Exit Sub
                        End If

                        If optSelectedDate.Checked = False Then
                            dvTips.RowFilter += " AND Description = 'Special Function'"
                        Else
                            dvTips.RowFilter += " AND Description = 'Special Function' AND WorkingDate = '" & _
                                Format(CDate(txtSelectedDate.Text), "MM/dd/yyyy") & "'"
                        End If

                        Dim decSpecialFunctions As Decimal = 0
                        Dim i As Integer = 0

                        Do Until i = dvTips.Count
                            decSpecialFunctions += CDec(dvTips.Item(i)("Amount"))
                            i += 1
                        Loop

                        e.Graphics.DrawString("Special Function", font, Brushes.Black, marginLeft, intPosition)

                        Dim strSpecialFunctions As String = Format(decSpecialFunctions, "0.00")
                        Dim intStrLen As Integer = CInt(e.Graphics.MeasureString(strSpecialFunctions, font, _
                            New SizeF(intPrintAreaWidth, intPrintAreaHeight), fmt, Len(strSpecialFunctions), 1).Width)

                        e.Graphics.DrawString(strSpecialFunctions, font, Brushes.Black, marginLeft + intPrintAreaWidth - intStrLen, _
                            intPosition)

                        decChargeTipTotal += decSpecialFunctions
                        decSpecialFunctionGrandTotal += decSpecialFunctions
                        intPosition += intLineSpacing

                    Case intChargeTips 'Always performed.
                        If intPosition >= marginTop + intPrintAreaHeight Then
                            e.HasMorePages = True
                            intPageNumber += 1
                            intPosition = marginTop
                            Exit Sub
                        End If

                        e.Graphics.DrawString("Charge Tips", fontBold, Brushes.Black, marginLeft, intPosition)

                        Dim strChargeTips As String = Format(decChargeTipTotal, "0.00")
                        Dim intStrLen As Integer = CInt(e.Graphics.MeasureString(strChargeTips, fontBold, _
                            New SizeF(intPrintAreaWidth, intPrintAreaHeight), fmt, Len(strChargeTips), 1).Width)

                        e.Graphics.DrawString(strChargeTips, fontBold, Brushes.Black, marginLeft + intPrintAreaWidth - intStrLen, _
                            intPosition)

                        decTipTotal += decChargeTipTotal
                        intPosition += intLineSpacing

                    Case intCash
                        If optCash.Checked = False Then
                            intCurrentStep += 1
                            Continue While
                        End If

                        If intPosition >= marginTop + intPrintAreaHeight Then
                            e.HasMorePages = True
                            intPageNumber += 1
                            intPosition = marginTop
                            Exit Sub
                        End If

                        If optSelectedDate.Checked = False Then
                            dvTips.RowFilter += " AND Description = 'Cash'"
                        Else
                            dvTips.RowFilter += " AND Description = 'Cash' AND WorkingDate = '" & _
                                Format(CDate(txtSelectedDate.Text), "MM/dd/yyyy") & "'"
                        End If

                        Dim decCash As Decimal = 0
                        Dim i As Integer = 0

                        Do Until i = dvTips.Count
                            decCash += CDec(dvTips.Item(i)("Amount"))
                            i += 1
                        Loop

                        e.Graphics.DrawString("Cash", font, Brushes.Black, marginLeft, intPosition)

                        Dim strCash As String = Format(decCash, "0.00")
                        Dim intStrLen As Integer = CInt(e.Graphics.MeasureString(strCash, font, _
                            New SizeF(intPrintAreaWidth, intPrintAreaHeight), fmt, Len(strCash), 1).Width)

                        e.Graphics.DrawString(strCash, font, Brushes.Black, marginLeft + intPrintAreaWidth - intStrLen, _
                            intPosition)

                        decTipTotal += decCash
                        decCashGrandTotal += decCash
                        intPosition += intLineSpacing

                    Case intTotalTips 'Always performed.
                        If intPosition >= marginTop + intPrintAreaHeight Then
                            e.HasMorePages = True
                            intPageNumber += 1
                            intPosition = marginTop
                            Exit Sub
                        End If

                        e.Graphics.DrawString("Total Tips", fontBold, Brushes.Black, marginLeft, intPosition)

                        Dim strTipTotal As String = Format(decTipTotal, "0.00")
                        Dim intStrLen As Integer = CInt(e.Graphics.MeasureString(strTipTotal, fontBold, _
                            New SizeF(intPrintAreaWidth, intPrintAreaHeight), fmt, Len(strTipTotal), 1).Width)

                        e.Graphics.DrawString(strTipTotal, fontBold, Brushes.Black, marginLeft + intPrintAreaWidth - intStrLen, _
                            intPosition)

                        intPosition += intExtraLineSpacing
                End Select

                intCurrentStep += 1
            End While

            intCurrentServer += 1
            intCurrentStep = 1
            decChargeTipTotal = 0
            decTipTotal = 0
        Loop

        Dim intStringLength As Integer

        If intPosition >= marginTop + intPrintAreaHeight Then
            e.HasMorePages = True
            intPageNumber += 1
            intPosition = marginTop
            Exit Sub
        End If

        Static blnHeaderPrinted As Boolean
        Static blnCCPrinted As Boolean
        Static blnRCPrinted As Boolean
        Static blnSFPrinted As Boolean
        Static blnChargePrinted As Boolean
        Static blnCashPrinted As Boolean
        Static blnTotalPrinted As Boolean

        If optSelectedServer.Checked = False Then

            If blnHeaderPrinted = False Then
                e.Graphics.DrawString("TOTAL FOR ALL SERVERS", fontBold, Brushes.Black, marginLeft, intPosition)
                intPosition += intLineSpacing
                blnHeaderPrinted = True
            End If

            If optCreditCard.Checked = True And blnCCPrinted = False Then
                'Draw credit card grand total.
                If intPosition >= marginTop + intPrintAreaHeight Then
                    e.HasMorePages = True
                    intPageNumber += 1
                    intPosition = marginTop
                    Exit Sub
                End If

                e.Graphics.DrawString("Credit Card", font, Brushes.Black, marginLeft, intPosition)

                Dim strCreditCardGrand As String = Format(decCreditCardGrandTotal, "0.00")

                intStringLength = CInt(e.Graphics.MeasureString(strCreditCardGrand, font, _
                    New SizeF(intPrintAreaWidth, intPrintAreaHeight), fmt, Len(strCreditCardGrand), 1).Width)

                e.Graphics.DrawString(strCreditCardGrand, font, Brushes.Black, marginLeft + intPrintAreaWidth - intStringLength, intPosition)

                blnCCPrinted = True
                intPosition += intLineSpacing
            End If
            If optRoomCharge.Checked = True And blnRCPrinted = False Then
                'Draw room charge grand total.
                If intPosition >= marginTop + intPrintAreaHeight Then
                    e.HasMorePages = True
                    intPageNumber += 1
                    intPosition = marginTop
                    Exit Sub
                End If

                e.Graphics.DrawString("Room Charge", font, Brushes.Black, marginLeft, intPosition)

                Dim strRoomChargeGrand As String = Format(decRoomChargeGrandTotal, "0.00")

                intStringLength = CInt(e.Graphics.MeasureString(strRoomChargeGrand, font, _
                    New SizeF(intPrintAreaWidth, intPrintAreaHeight), fmt, Len(strRoomChargeGrand), 1).Width)

                e.Graphics.DrawString(strRoomChargeGrand, font, Brushes.Black, marginLeft + intPrintAreaWidth - intStringLength, intPosition)

                blnRCPrinted = True
                intPosition += intLineSpacing
            End If

            If optSpecialFunction.Checked = True And blnSFPrinted = False Then
                'Draw special function grand total.
                If intPosition >= marginTop + intPrintAreaHeight Then
                    e.HasMorePages = True
                    intPageNumber += 1
                    intPosition = marginTop
                    Exit Sub
                End If

                e.Graphics.DrawString("Special Function", font, Brushes.Black, marginLeft, intPosition)

                Dim strSpecialFunctionGrand As String = Format(decSpecialFunctionGrandTotal, "0.00")

                intStringLength = CInt(e.Graphics.MeasureString(strSpecialFunctionGrand, font, _
                    New SizeF(intPrintAreaWidth, intPrintAreaHeight), fmt, Len(strSpecialFunctionGrand), 1).Width)

                e.Graphics.DrawString(strSpecialFunctionGrand, font, Brushes.Black, marginLeft + intPrintAreaWidth - intStringLength, intPosition)

                blnSFPrinted = True
                intPosition += intLineSpacing
            End If

            'Draw charge tips grand total.
            If blnChargePrinted = False Then
                If intPosition >= marginTop + intPrintAreaHeight Then
                    e.HasMorePages = True
                    intPageNumber += 1
                    intPosition = marginTop
                    Exit Sub
                End If

                e.Graphics.DrawString("Charge Tips", fontBold, Brushes.Black, marginLeft, intPosition)

                Dim strChargeTipsGrand As String = _
                    Format(decCreditCardGrandTotal + decRoomChargeGrandTotal + decSpecialFunctionGrandTotal, "0.00")

                intStringLength = CInt(e.Graphics.MeasureString(strChargeTipsGrand, fontBold, _
                    New SizeF(intPrintAreaWidth, intPrintAreaHeight), fmt, Len(strChargeTipsGrand), 1).Width)

                e.Graphics.DrawString(strChargeTipsGrand, fontBold, Brushes.Black, marginLeft + intPrintAreaWidth - intStringLength, intPosition)

                blnChargePrinted = True
                intPosition += intLineSpacing
            End If

            If optCash.Checked = True And blnCashPrinted = False Then
                'Draw cash grand total.
                If intPosition >= marginTop + intPrintAreaHeight Then
                    e.HasMorePages = True
                    intPageNumber += 1
                    intPosition = marginTop
                    Exit Sub
                End If

                e.Graphics.DrawString("Cash", font, Brushes.Black, marginLeft, intPosition)

                Dim strCashGrand As String = Format(decCashGrandTotal, "0.00")

                intStringLength = CInt(e.Graphics.MeasureString(strCashGrand, font, _
                    New SizeF(intPrintAreaWidth, intPrintAreaHeight), fmt, Len(strCashGrand), 1).Width)

                e.Graphics.DrawString(strCashGrand, font, Brushes.Black, marginLeft + intPrintAreaWidth - intStringLength, intPosition)

                blnCashPrinted = True
                intPosition += intLineSpacing
            End If

            'Draw grand total.
            If blnTotalPrinted = False Then
                If intPosition >= marginTop + intPrintAreaHeight Then
                    e.HasMorePages = True
                    intPageNumber += 1
                    intPosition = marginTop
                    Exit Sub
                End If

                e.Graphics.DrawString("GRAND TOTAL", fontBold, Brushes.Black, marginLeft, intPosition)

                Dim strGrandTotal As String = _
                    Format(decCreditCardGrandTotal + decRoomChargeGrandTotal + decSpecialFunctionGrandTotal + decCashGrandTotal, _
                    "0.00")

                intStringLength = CInt(e.Graphics.MeasureString(strGrandTotal, fontBold, _
                    New SizeF(intPrintAreaWidth, intPrintAreaHeight), fmt, Len(strGrandTotal), 1).Width)

                e.Graphics.DrawString(strGrandTotal, fontBold, Brushes.Black, marginLeft + intPrintAreaWidth - intStringLength, intPosition)
                blnTotalPrinted = True
            End If
        End If

        'End of report.  Reset all static variables.
        blnHeaderPrinted = False
        blnCCPrinted = False
        blnRCPrinted = False
        blnSFPrinted = False
        blnChargePrinted = False
        blnCashPrinted = False
        blnTotalPrinted = False

        decCreditCardGrandTotal = 0
        decRoomChargeGrandTotal = 0
        decSpecialFunctionGrandTotal = 0
        decCashGrandTotal = 0

        intCurrentServer = 0
        e.HasMorePages = False
        intPosition = marginTop
        intPageNumber = 1
    End Sub

    Private Sub SummaryByDate(ByVal sender As System.Object, ByVal e As PrintPageEventArgs)
        Dim font As New System.Drawing.Font("Calibri", 10)
        Dim fontBold As New System.Drawing.Font("Calibri", 10, FontStyle.Bold)

        Dim fmt As New StringFormat(StringFormatFlags.LineLimit)

        'Initialize local static variables that contain the current line position and
        'the current page number.
        Static intPosition As Single = 75
        Static intPageNumber As Integer = 1

        Const intLineSpacing As Integer = 15
        Const intExtraLineSpacing As Integer = 30

        'Initialize local constants that contain the normal line spacing and padded 
        'line spacing (double spacing).
        Dim intPrintAreaHeight, intPrintAreaWidth, marginLeft, marginTop As Int32

        With e.MarginBounds
            ' Initialize local variables that contain the bounds of the printing 
            ' area rectangle.
            intPrintAreaHeight = .Height
            intPrintAreaWidth = .Width

            ' Initialize local variables to hold margin values that will serve
            ' as the X and Y coordinates for the upper left corner of the printing 
            ' area rectangle.
            marginLeft = .Left ' X coordinate
            marginTop = .Top ' Y coordinate
        End With

        'Draw the header to the page.  Header will be drawn on every page.
        e.Graphics.DrawString("Tip Report - Summary by Date", font, Brushes.Black, marginLeft, intPosition)
        intPosition += intLineSpacing

        e.Graphics.DrawString(Format(DateTime.Now, "MM/dd/yyyy") & " " & Format(DateTime.Now, "t"), font, Brushes.Black, marginLeft, intPosition)
        intPosition += intLineSpacing

        If optAllServers.Checked = True Then
            e.Graphics.DrawString("All Servers", font, Brushes.Black, marginLeft, intPosition)
        Else
            Dim strServerNumber As String = cboServers.SelectedValue.ToString
            Dim strName As String = Me.m_dsParentDataSet.Servers.FindByServerNumber(strServerNumber)("FirstName").ToString & _
                " " & Me.m_dsParentDataSet.Servers.FindByServerNumber(strServerNumber)("LastName").ToString

            e.Graphics.DrawString("Selected Server: " & strName, font, Brushes.Black, marginLeft, intPosition)
        End If
        intPosition += intLineSpacing

        If optAllDates.Checked = True Then
            e.Graphics.DrawString("All Dates", font, Brushes.Black, marginLeft, intPosition)
        Else
            e.Graphics.DrawString("Selected Date: " & Format(CDate(txtSelectedDate.Text), "MM/dd/yyyy"), _
                font, Brushes.Black, marginLeft, intPosition)
        End If
        intPosition += intLineSpacing

        Dim strIncludedTypes As String = "Types Included: "
        If optCreditCard.Checked = True Then strIncludedTypes += "CC "
        If optRoomCharge.Checked = True Then strIncludedTypes += "RC "
        If optSpecialFunction.Checked = True Then strIncludedTypes += "SF "
        If optCash.Checked = True Then strIncludedTypes += "CA "

        e.Graphics.DrawString(strIncludedTypes, font, Brushes.Black, marginLeft, intPosition)
        intPosition += intLineSpacing

        e.Graphics.DrawString("Page " & intPageNumber, font, Brushes.Black, marginLeft, intPosition)
        intPosition += intExtraLineSpacing

        'Initialize a list variable to hold the list of dates that will be printed.  If only
        'one date is selected, that date will be added to the list.  If all dates are selected,
        'each date in the pay period will be added to the list.
        Dim lstDates As New List(Of Date)
        Static dteDateCounter As Date

        If optAllDates.Checked = True Then
            Dim dtePeriodStart As Date = CDate(Me.m_dsParentDataSet.Settings.FindBySetting("PeriodStart")("Value"))
            Dim dtePeriodEnd As Date = CDate(Me.m_dsParentDataSet.Settings.FindBySetting("PeriodEnd")("Value"))

            dteDateCounter = dtePeriodStart

            Do Until dteDateCounter > dtePeriodEnd
                lstDates.Add(dteDateCounter)
                dteDateCounter = DateAdd(DateInterval.Day, 1, dteDateCounter)
            Loop
        Else
            Dim dteSelectedDate As Date = CDate(txtSelectedDate.Text)

            lstDates.Add(dteSelectedDate)
        End If

        'Loop through each day in the list and total the tips for each tip type.
        'The report will be printed in steps.  Since the date is ignored for cash tips
        '(because they are cumulative for the entire pay period) there is no step for
        'adding cash tips by date.
        Const intDate As Integer = 1
        Const intCreditCard As Integer = 2
        Const intRoomCharge As Integer = 3
        Const intSpecialFunction As Integer = 4
        Const intTotalTips As Integer = 5

        Static intCounter As Integer = 0
        Static intStep As Integer = 1

        Static decCreditCardGrandTotal As Decimal = 0
        Static decRoomChargeGrandTotal As Decimal = 0
        Static decSpecialFunctionGrandTotal As Decimal = 0
        Static decCashGrandTotal As Decimal = 0

        Dim intStrLen As Integer

        Do Until intCounter = lstDates.Count
            Static decDailyTotal As Decimal = 0

            While intStep <= intTotalTips
                Dim dvTips As New DataView
                dvTips.Table = Me.m_dsParentDataSet.Tips
                dvTips.RowFilter = "WorkingDate = '" & Format(lstDates.Item(intCounter), "MM/dd/yyyy") & "'"

                Select Case intStep
                    Case intDate
                        If intPosition >= marginTop + intPrintAreaHeight Then
                            e.HasMorePages = True
                            intPageNumber += 1
                            intPosition = marginTop
                            Exit Sub
                        End If

                        Dim dteCurrentDate As Date = lstDates.Item(intCounter)

                        e.Graphics.DrawString(Format(dteCurrentDate, "MM/dd/yyyy"), fontBold, Brushes.Black, marginLeft, _
                            intPosition)

                        intPosition += intLineSpacing
                    Case intCreditCard
                        If optCreditCard.Checked = False Then
                            intStep += 1
                            Continue While
                        End If

                        If intPosition >= marginTop + intPrintAreaHeight Then
                            e.HasMorePages = True
                            intPageNumber += 1
                            intPosition = marginTop
                            Exit Sub
                        End If

                        If optSelectedServer.Checked = False Then
                            dvTips.RowFilter += " AND Description = 'Credit Card'"
                        Else
                            dvTips.RowFilter += " AND Description = 'Credit Card' AND ServerNumber = '" & _
                                cboServers.SelectedValue.ToString & "'"
                        End If

                        Dim decCreditCards As Decimal = 0
                        Dim i As Integer = 0

                        Do Until i = dvTips.Count
                            decCreditCards += CDec(dvTips.Item(i)("Amount"))
                            i += 1
                        Loop

                        e.Graphics.DrawString("Credit Card", font, Brushes.Black, marginLeft, intPosition)

                        Dim strCreditCards As String = Format(decCreditCards, "0.00")

                        intStrLen = CInt(e.Graphics.MeasureString(strCreditCards, font, _
                            New SizeF(intPrintAreaWidth, intPrintAreaHeight), fmt, Len(strCreditCards), 1).Width)

                        e.Graphics.DrawString(strCreditCards, font, Brushes.Black, marginLeft + intPrintAreaWidth - intStrLen, _
                            intPosition)

                        decDailyTotal += decCreditCards
                        decCreditCardGrandTotal += decCreditCards
                        intPosition += intLineSpacing
                    Case intRoomCharge
                        If optRoomCharge.Checked = False Then
                            intStep += 1
                            Continue While
                        End If

                        If intPosition >= marginTop + intPrintAreaHeight Then
                            e.HasMorePages = True
                            intPageNumber += 1
                            intPosition = marginTop
                            Exit Sub
                        End If

                        If optSelectedServer.Checked = False Then
                            dvTips.RowFilter += " AND Description = 'Room Charge'"
                        Else
                            dvTips.RowFilter += " AND Description = 'Room Charge' AND ServerNumber = '" & _
                                cboServers.SelectedValue.ToString & "'"
                        End If

                        Dim decRoomCharges As Decimal = 0
                        Dim i As Integer = 0

                        Do Until i = dvTips.Count
                            decRoomCharges += CDec(dvTips.Item(i)("Amount"))
                            i += 1
                        Loop

                        e.Graphics.DrawString("Room Charge", font, Brushes.Black, marginLeft, intPosition)

                        Dim strRoomCharges As String = Format(decRoomCharges, "0.00")

                        intStrLen = CInt(e.Graphics.MeasureString(strRoomCharges, font, _
                            New SizeF(intPrintAreaWidth, intPrintAreaHeight), fmt, Len(strRoomCharges), 1).Width)

                        e.Graphics.DrawString(strRoomCharges, font, Brushes.Black, marginLeft + intPrintAreaWidth - intStrLen, _
                            intPosition)

                        decDailyTotal += decRoomCharges
                        decRoomChargeGrandTotal += decRoomCharges
                        intPosition += intLineSpacing
                    Case intSpecialFunction
                        If optSpecialFunction.Checked = False Then
                            intStep += 1
                            Continue While
                        End If

                        If intPosition >= marginTop + intPrintAreaHeight Then
                            e.HasMorePages = True
                            intPageNumber += 1
                            intPosition = marginTop
                            Exit Sub
                        End If

                        If optSelectedServer.Checked = False Then
                            dvTips.RowFilter += " AND Description = 'Special Function'"
                        Else
                            dvTips.RowFilter += " AND Description = 'Special Function' AND ServerNumber = '" & _
                                cboServers.SelectedValue.ToString & "'"
                        End If

                        Dim decSpecialFunctions As Decimal = 0
                        Dim i As Integer = 0

                        Do Until i = dvTips.Count
                            decSpecialFunctions += CDec(dvTips.Item(i)("Amount"))
                            i += 1
                        Loop

                        e.Graphics.DrawString("Special Function", font, Brushes.Black, marginLeft, intPosition)

                        Dim strSpecialFunctions As String = Format(decSpecialFunctions, "0.00")

                        intStrLen = CInt(e.Graphics.MeasureString(strSpecialFunctions, font, _
                            New SizeF(intPrintAreaWidth, intPrintAreaHeight), fmt, Len(strSpecialFunctions), 1).Width)

                        e.Graphics.DrawString(strSpecialFunctions, font, Brushes.Black, marginLeft + intPrintAreaWidth - intStrLen, _
                            intPosition)

                        decDailyTotal += decSpecialFunctions
                        decSpecialFunctionGrandTotal += decSpecialFunctions
                        intPosition += intLineSpacing
                    Case intTotalTips
                        If intPosition >= marginTop + intPrintAreaHeight Then
                            e.HasMorePages = True
                            intPageNumber += 1
                            intPosition = marginTop
                            Exit Sub
                        End If

                        e.Graphics.DrawString("Total Tips", fontBold, Brushes.Black, marginLeft, intPosition)

                        Dim strChargeTips As String = Format(decDailyTotal, "0.00")

                        intStrLen = CInt(e.Graphics.MeasureString(strChargeTips, font, _
                            New SizeF(intPrintAreaWidth, intPrintAreaHeight), fmt, Len(strChargeTips), 1).Width)

                        e.Graphics.DrawString(strChargeTips, fontBold, Brushes.Black, marginLeft + intPrintAreaWidth - intStrLen, _
                            intPosition)

                        intPosition += intExtraLineSpacing
                End Select
                intStep += 1
            End While

            decDailyTotal = 0
            intStep = 1
            intCounter += 1
        Loop

        If intPosition >= marginTop + intPrintAreaHeight Then
            e.HasMorePages = True
            intPageNumber += 1
            intPosition = marginTop
            Exit Sub
        End If

        Static blnHeaderPrinted As Boolean
        Static blnCCPrinted As Boolean
        Static blnRCPrinted As Boolean
        Static blnSFPrinted As Boolean
        Static blnChargePrinted As Boolean
        Static blnCashPrinted As Boolean
        Static blnTotalPrinted As Boolean

        If blnHeaderPrinted = False Then
            If optAllDates.Checked = True And blnHeaderPrinted = False Then
                e.Graphics.DrawString("TOTAL FOR ALL DATES", fontBold, Brushes.Black, marginLeft, intPosition)
                intPosition += intLineSpacing
            Else
                e.Graphics.DrawString("TOTAL FOR SELECTED DATE", fontBold, Brushes.Black, marginLeft, intPosition)
                intPosition += intLineSpacing
            End If
            blnHeaderPrinted = True
        End If

        'Print credit card totals.
        If optCreditCard.Checked = True And blnCCPrinted = False Then
            If intPosition >= marginTop + intPrintAreaHeight Then
                e.HasMorePages = True
                intPageNumber += 1
                intPosition = marginTop
                Exit Sub
            End If

            e.Graphics.DrawString("Credit Card", font, Brushes.Black, marginLeft, intPosition)

            Dim strCreditCardGrandTotal As String = Format(decCreditCardGrandTotal, "0.00")

            intStrLen = CInt(e.Graphics.MeasureString(strCreditCardGrandTotal, font, New SizeF(intPrintAreaWidth, _
                intPrintAreaHeight), fmt, Len(strCreditCardGrandTotal), 1).Width)

            e.Graphics.DrawString(strCreditCardGrandTotal, font, Brushes.Black, marginLeft + intPrintAreaWidth - intStrLen, intPosition)
            intPosition += intLineSpacing
            blnCCPrinted = True
        End If

        'Print room charge totals.
        If optRoomCharge.Checked = True And blnRCPrinted = False Then
            If intPosition >= marginTop + intPrintAreaHeight Then
                e.HasMorePages = True
                intPageNumber += 1
                intPosition = marginTop
                Exit Sub
            End If

            e.Graphics.DrawString("Room Charge", font, Brushes.Black, marginLeft, intPosition)

            Dim strRoomChargeGrandTotal As String = Format(decRoomChargeGrandTotal, "0.00")

            intStrLen = CInt(e.Graphics.MeasureString(strRoomChargeGrandTotal, font, New SizeF(intPrintAreaWidth, _
                intPrintAreaHeight), fmt, Len(strRoomChargeGrandTotal), 1).Width)

            e.Graphics.DrawString(strRoomChargeGrandTotal, font, Brushes.Black, marginLeft + intPrintAreaWidth - intStrLen, intPosition)
            intPosition += intLineSpacing
            blnRCPrinted = True
        End If

        'Print special function totals.
        If optSpecialFunction.Checked = True And blnSFPrinted = False Then
            If intPosition >= marginTop + intPrintAreaHeight Then
                e.HasMorePages = True
                intPageNumber += 1
                intPosition = marginTop
                Exit Sub
            End If

            e.Graphics.DrawString("Special Function", font, Brushes.Black, marginLeft, intPosition)

            Dim strSpecialFunctionGrandTotal As String = Format(decSpecialFunctionGrandTotal, "0.00")

            intStrLen = CInt(e.Graphics.MeasureString(strSpecialFunctionGrandTotal, font, New SizeF(intPrintAreaWidth, _
                intPrintAreaHeight), fmt, Len(strSpecialFunctionGrandTotal), 1).Width)

            e.Graphics.DrawString(strSpecialFunctionGrandTotal, font, Brushes.Black, marginLeft + intPrintAreaWidth - intStrLen, intPosition)
            intPosition += intLineSpacing
            blnSFPrinted = True
        End If

        'Print charge tip totals.
        If blnChargePrinted = False Then
            If intPosition >= marginTop + intPrintAreaHeight Then
                e.HasMorePages = True
                intPageNumber += 1
                intPosition = marginTop
                Exit Sub
            End If


            e.Graphics.DrawString("Charge Tips", fontBold, Brushes.Black, marginLeft, intPosition)

            Dim strChargeTipGrandTotal As String = Format(decCreditCardGrandTotal + decRoomChargeGrandTotal + _
                decSpecialFunctionGrandTotal, "0.00")

            intStrLen = CInt(e.Graphics.MeasureString(strChargeTipGrandTotal, fontBold, New SizeF(intPrintAreaWidth, _
                intPrintAreaHeight), fmt, Len(strChargeTipGrandTotal), 1).Width)

            e.Graphics.DrawString(strChargeTipGrandTotal, fontBold, Brushes.Black, marginLeft + intPrintAreaWidth - intStrLen, intPosition)
            intPosition += intLineSpacing
            blnChargePrinted = True
        End If

        Static decCashTotal As Decimal = 0

        'Total cash tips.
        If blnCashPrinted = False Then
            Dim dvCash As New DataView

            dvCash.Table = Me.m_dsParentDataSet.Tips
            dvCash.RowFilter = "Description = 'Cash'"

            If optSelectedServer.Checked = True Then
                dvCash.RowFilter += " AND ServerNumber = '" & cboServers.SelectedValue.ToString & "'"
            End If

            If dvCash.Count <> 0 Then
                Dim i As Integer = 0
                Do Until i = dvCash.Count
                    decCashTotal += CDec(dvCash.Item(i)("Amount"))
                    i += 1
                Loop
            End If

            'Print cash total if option is selected.
            If optCash.Checked = True Then
                If intPosition >= marginTop + intPrintAreaHeight Then
                    e.HasMorePages = True
                    intPageNumber += 1
                    intPosition = marginTop
                    Exit Sub
                End If

                e.Graphics.DrawString("Total Cash Tips", font, Brushes.Black, marginLeft, intPosition)

                Dim strCashTotal As String = Format(decCashTotal, "0.00")

                intStrLen = CInt(e.Graphics.MeasureString(strCashTotal, font, New SizeF(intPrintAreaWidth, intPrintAreaHeight), _
                    fmt, Len(strCashTotal), 1).Width)

                e.Graphics.DrawString(strCashTotal, font, Brushes.Black, marginLeft + intPrintAreaWidth - intStrLen, intPosition)
                intPosition += intLineSpacing
            End If
            blnCashPrinted = True
        End If

        'Total all tips.
        If blnTotalPrinted = False Then
            If intPosition >= marginTop + intPrintAreaHeight Then
                e.HasMorePages = True
                intPageNumber += 1
                intPosition = marginTop
                Exit Sub
            End If

            e.Graphics.DrawString("GRAND TOTAL", fontBold, Brushes.Black, marginLeft, intPosition)

            Dim strGrandTotal As String = Format(decCreditCardGrandTotal + decRoomChargeGrandTotal + _
                decSpecialFunctionGrandTotal + decCashTotal, "0.00")

            intStrLen = CInt(e.Graphics.MeasureString(strGrandTotal, fontBold, New SizeF(intPrintAreaWidth, _
                intPrintAreaHeight), fmt, Len(strGrandTotal), 1).Width)

            e.Graphics.DrawString(strGrandTotal, fontBold, Brushes.Black, marginLeft + intPrintAreaWidth - intStrLen, intPosition)
            intPosition += intLineSpacing
            blnTotalPrinted = True
        End If

        'End of report.  Reset all static variables.
        blnHeaderPrinted = False
        blnCCPrinted = False
        blnRCPrinted = False
        blnSFPrinted = False
        blnChargePrinted = False
        blnCashPrinted = False
        blnTotalPrinted = False

        intCounter = 0
        decCreditCardGrandTotal = 0
        decRoomChargeGrandTotal = 0
        decSpecialFunctionGrandTotal = 0
        decCashGrandTotal = 0
        decCashTotal = 0

        e.HasMorePages = False
        intPosition = marginTop
        intPageNumber = 1
    End Sub

    Private Sub DetailByServer(ByVal sender As System.Object, ByVal e As PrintPageEventArgs)
        Dim font As New System.Drawing.Font("Calibri", 10)
        Dim fontBold As New System.Drawing.Font("Calibri", 10, FontStyle.Bold)

        Dim fmt As New StringFormat(StringFormatFlags.LineLimit)

        'Initialize local static variables that contain the current line position and
        'the current page number.
        Static intPosition As Single = e.MarginBounds.Top
        Static intPageNumber As Integer = 1

        Const intLineSpacing As Integer = 15
        Const intExtraLineSpacing As Integer = 30
        Const intIndent As Integer = 100

        'Initialize local constants that contain the normal line spacing and padded 
        'line spacing (double spacing).
        Dim intPrintAreaHeight, intPrintAreaWidth, marginLeft, marginTop As Int32

        With e.MarginBounds
            ' Initialize local variables that contain the bounds of the printing 
            ' area rectangle.
            intPrintAreaHeight = .Height
            intPrintAreaWidth = .Width

            ' Initialize local variables to hold margin values that will serve
            ' as the X and Y coordinates for the upper left corner of the printing 
            ' area rectangle.
            marginLeft = .Left ' X coordinate
            marginTop = .Top ' Y coordinate
        End With

        'Draw the header to the page.  Header will be drawn on every page.
        e.Graphics.DrawString("Tip Report - Detail by Server", font, Brushes.Black, marginLeft, intPosition)
        intPosition += intLineSpacing

        e.Graphics.DrawString(Format(DateTime.Now, "MM/dd/yyyy") & " " & Format(DateTime.Now, "t"), font, Brushes.Black, marginLeft, intPosition)
        intPosition += intLineSpacing

        If optAllServers.Checked = True Then
            e.Graphics.DrawString("All Servers", font, Brushes.Black, marginLeft, intPosition)
        Else
            Dim strServerNumber As String = cboServers.SelectedValue.ToString
            Dim strName As String = Me.m_dsParentDataSet.Servers.FindByServerNumber(strServerNumber)("FirstName").ToString & _
                " " & Me.m_dsParentDataSet.Servers.FindByServerNumber(strServerNumber)("LastName").ToString

            e.Graphics.DrawString("Selected Server: " & strName, font, Brushes.Black, marginLeft, intPosition)
        End If
        intPosition += intLineSpacing

        If optAllDates.Checked = True Then
            e.Graphics.DrawString("All Dates", font, Brushes.Black, marginLeft, intPosition)
        Else
            e.Graphics.DrawString("Selected Date: " & Format(CDate(txtSelectedDate.Text), "MM/dd/yyyy"), _
                font, Brushes.Black, marginLeft, intPosition)
        End If
        intPosition += intLineSpacing

        Dim strIncludedTypes As String = "Types Included: "
        If optCreditCard.Checked = True Then strIncludedTypes += "CC "
        If optRoomCharge.Checked = True Then strIncludedTypes += "RC "
        If optSpecialFunction.Checked = True Then strIncludedTypes += "SF "
        If optCash.Checked = True Then strIncludedTypes += "CA "

        e.Graphics.DrawString(strIncludedTypes, font, Brushes.Black, marginLeft, intPosition)
        intPosition += intLineSpacing

        e.Graphics.DrawString("Page " & intPageNumber, font, Brushes.Black, marginLeft, intPosition)
        intPosition += intExtraLineSpacing

        'Create a dataview to list all of the servers whose tips will be printed.
        Dim dvServers As New DataView
        dvServers.Table = Me.m_dsParentDataSet.Servers
        dvServers.Sort = "LastName, FirstName"

        'If only one server is selected, filter the dataview so that server is the 
        'only one included in it.
        If optSelectedServer.Checked = True Then
            dvServers.RowFilter = "ServerNumber = '" & cboServers.SelectedValue.ToString & "'"
        End If

        Static intCurrentServer As Integer = 0
        Static intCurrentStep As Integer = 1

        'The report will be printed in steps.  To avoid repeating a step when the page changes
        'the steps will be tracked by the counter intCurrentStep.  This will also skip over tips
        'that aren't selected.
        Const intName As Integer = 1
        Const intCreditCard As Integer = 2
        Const intRoomCharge As Integer = 3
        Const intSpecialFunction As Integer = 4
        Const intChargeTips As Integer = 5
        Const intCash As Integer = 6
        Const intTotalTips As Integer = 7

        Static decCreditCardGrandTotal As Decimal = 0
        Static decRoomChargeGrandTotal As Decimal = 0
        Static decSpecialFunctionGrandTotal As Decimal = 0
        Static decCashGrandTotal As Decimal = 0

        Do Until intCurrentServer = dvServers.Count
            Static decChargeTipTotal As Decimal = 0
            Static decTipTotal As Decimal = 0

            Dim strServerNumber As String = dvServers.Item(intCurrentServer)("ServerNumber").ToString
            Dim strFirstName As String = dvServers.Item(intCurrentServer)("FirstName").ToString
            Dim strLastName As String = dvServers.Item(intCurrentServer)("LastName").ToString

            Dim dvCounter As New DataView
            dvCounter.Table = Me.m_dsParentDataSet.Tips
            dvCounter.RowFilter = "ServerNumber = '" & strServerNumber & "'"

            If optSelectedDate.Checked = True Then
                dvCounter.RowFilter += " AND WorkingDate = '" & Format(CDate(txtSelectedDate.Text), "MM/dd/yyyy") & "'"
            End If

            If dvCounter.Count = 0 Then
                intCurrentServer += 1
                Continue Do
            End If

            Dim dvTips As New DataView
            dvTips.Table = Me.m_dsParentDataSet.Tips

            If dvTips.Count = 0 Then
                intCurrentServer += 1
                Continue Do
            End If

            While intCurrentStep <= intTotalTips
                dvTips.RowFilter = "ServerNumber = '" & strServerNumber & "'"

                Select Case intCurrentStep
                    Case intName 'Always performed.
                        If intPosition >= marginTop + intPrintAreaHeight Then
                            e.HasMorePages = True
                            intPageNumber += 1
                            intPosition = marginTop
                            Exit Sub
                        End If

                        e.Graphics.DrawString(strServerNumber & " " & strLastName & ", " & strFirstName, _
                            fontBold, Brushes.Black, marginLeft, intPosition)

                        intPosition += intLineSpacing

                    Case intCreditCard
                        If optCreditCard.Checked = False Then
                            intCurrentStep += 1
                            Continue While
                        End If

                        If intPosition >= marginTop + intPrintAreaHeight Then
                            e.HasMorePages = True
                            intPageNumber += 1
                            intPosition = marginTop
                            Exit Sub
                        End If

                        If optSelectedDate.Checked = False Then
                            dvTips.RowFilter += " AND Description = 'Credit Card'"
                        Else
                            dvTips.RowFilter += " AND Description = 'Credit Card' AND WorkingDate = '" & _
                                Format(CDate(txtSelectedDate.Text), "MM/dd/yyyy") & "'"
                        End If

                        If dvTips.Count = 0 Then
                            intCurrentStep += 1
                            Continue While
                        End If


                        Static decCreditCards As Decimal = 0
                        Dim intStrLen As Integer = 0

                        Static cc As Integer = 0

                        Do Until cc = dvTips.Count
                            If intPosition >= marginTop + intPrintAreaHeight Then
                                e.HasMorePages = True
                                intPageNumber += 1
                                intPosition = marginTop
                                Exit Sub
                            End If

                            Dim strWorkingDate As String = Format(CDate(dvTips.Item(cc)("WorkingDate")), "MM/dd/yyyy")
                            Dim strDescription As String = dvTips.Item(cc)("Description").ToString
                            Dim strAmount As String = Format(CDec(dvTips.Item(cc)("Amount")), "0.00")


                            e.Graphics.DrawString(strWorkingDate, font, Brushes.Black, marginLeft, intPosition)
                            e.Graphics.DrawString(strDescription, font, Brushes.Black, marginLeft + intIndent, intPosition)

                            intStrLen = CInt(e.Graphics.MeasureString(strAmount, font, New SizeF(intPrintAreaWidth, _
                                intPrintAreaHeight), fmt, Len(strAmount), 1).Width)

                            e.Graphics.DrawString(strAmount, font, Brushes.Black, marginLeft + intPrintAreaWidth - intStrLen, intPosition)

                            decCreditCards += CDec(dvTips.Item(cc)("Amount"))
                            intPosition += intLineSpacing
                            cc += 1
                        Loop

                        If intPosition >= marginTop + intPrintAreaHeight Then
                            e.HasMorePages = True
                            intPageNumber += 1
                            intPosition = marginTop
                            Exit Sub
                        End If

                        e.Graphics.DrawString("Credit Card Subtotal", font, Brushes.Black, marginLeft, intPosition)

                        Dim strCreditCards As String = Format(decCreditCards, "0.00")

                        intStrLen = CInt(e.Graphics.MeasureString(strCreditCards, font, _
                            New SizeF(intPrintAreaWidth, intPrintAreaHeight), fmt, Len(strCreditCards), 1).Width)

                        e.Graphics.DrawString(strCreditCards, font, Brushes.Black, marginLeft + intPrintAreaWidth - intStrLen, _
                            intPosition)

                        decChargeTipTotal += decCreditCards
                        decCreditCardGrandTotal += decCreditCards
                        intPosition += intExtraLineSpacing

                        decCreditCards = 0
                        cc = 0

                    Case intRoomCharge
                        If optRoomCharge.Checked = False Then
                            intCurrentStep += 1
                            Continue While
                        End If

                        If intPosition >= marginTop + intPrintAreaHeight Then
                            e.HasMorePages = True
                            intPageNumber += 1
                            intPosition = marginTop
                            Exit Sub
                        End If

                        If optSelectedDate.Checked = False Then
                            dvTips.RowFilter += " AND Description = 'Room Charge'"
                        Else
                            dvTips.RowFilter += " AND Description = 'Room Charge' AND WorkingDate = '" & _
                                Format(CDate(txtSelectedDate.Text), "MM/dd/yyyy") & "'"
                        End If

                        If dvTips.Count = 0 Then
                            intCurrentStep += 1
                            Continue While
                        End If


                        Static decRoomCharges As Decimal = 0
                        Dim intStrLen As Integer = 0

                        Static rc As Integer = 0

                        Do Until rc = dvTips.Count
                            If intPosition >= marginTop + intPrintAreaHeight Then
                                e.HasMorePages = True
                                intPageNumber += 1
                                intPosition = marginTop
                                Exit Sub
                            End If

                            Dim strWorkingDate As String = Format(CDate(dvTips.Item(rc)("WorkingDate")), "MM/dd/yyyy")
                            Dim strDescription As String = dvTips.Item(rc)("Description").ToString
                            Dim strAmount As String = Format(CDec(dvTips.Item(rc)("Amount")), "0.00")


                            e.Graphics.DrawString(strWorkingDate, font, Brushes.Black, marginLeft, intPosition)
                            e.Graphics.DrawString(strDescription, font, Brushes.Black, marginLeft + intIndent, intPosition)

                            intStrLen = CInt(e.Graphics.MeasureString(strAmount, font, New SizeF(intPrintAreaWidth, _
                                intPrintAreaHeight), fmt, Len(strAmount), 1).Width)

                            e.Graphics.DrawString(strAmount, font, Brushes.Black, marginLeft + intPrintAreaWidth - intStrLen, intPosition)

                            decRoomCharges += CDec(dvTips.Item(rc)("Amount"))
                            intPosition += intLineSpacing
                            rc += 1
                        Loop

                        If intPosition >= marginTop + intPrintAreaHeight Then
                            e.HasMorePages = True
                            intPageNumber += 1
                            intPosition = marginTop
                            Exit Sub
                        End If

                        e.Graphics.DrawString("Room Charge Subtotal", font, Brushes.Black, marginLeft, intPosition)

                        Dim strRoomCharges As String = Format(decRoomCharges, "0.00")

                        intStrLen = CInt(e.Graphics.MeasureString(strRoomCharges, font, _
                            New SizeF(intPrintAreaWidth, intPrintAreaHeight), fmt, Len(strRoomCharges), 1).Width)

                        e.Graphics.DrawString(strRoomCharges, font, Brushes.Black, marginLeft + intPrintAreaWidth - intStrLen, _
                            intPosition)

                        decChargeTipTotal += decRoomCharges
                        decRoomChargeGrandTotal += decRoomCharges
                        intPosition += intExtraLineSpacing

                        decRoomCharges = 0
                        rc = 0

                    Case intSpecialFunction
                        If optSpecialFunction.Checked = False Then
                            intCurrentStep += 1
                            Continue While
                        End If

                        If intPosition >= marginTop + intPrintAreaHeight Then
                            e.HasMorePages = True
                            intPageNumber += 1
                            intPosition = marginTop
                            Exit Sub
                        End If

                        If optSelectedDate.Checked = False Then
                            dvTips.RowFilter += " AND Description = 'Special Function'"
                        Else
                            dvTips.RowFilter += " AND Description = 'Special Function' AND WorkingDate = '" & _
                                Format(CDate(txtSelectedDate.Text), "MM/dd/yyyy") & "'"
                        End If

                        If dvTips.Count = 0 Then
                            intCurrentStep += 1
                            Continue While
                        End If


                        Static decSpecialFunctions As Decimal = 0
                        Dim intStrLen As Integer = 0

                        Static sf As Integer = 0

                        Do Until sf = dvTips.Count
                            If intPosition >= marginTop + intPrintAreaHeight Then
                                e.HasMorePages = True
                                intPageNumber += 1
                                intPosition = marginTop
                                Exit Sub
                            End If

                            Dim strWorkingDate As String = Format(CDate(dvTips.Item(sf)("WorkingDate")), "MM/dd/yyyy")
                            Dim strDescription As String = dvTips.Item(sf)("Description").ToString & ": " & dvTips.Item(sf)("SpecialFunction").ToString
                            Dim strAmount As String = Format(CDec(dvTips.Item(sf)("Amount")), "0.00")


                            e.Graphics.DrawString(strWorkingDate, font, Brushes.Black, marginLeft, intPosition)
                            e.Graphics.DrawString(strDescription, font, Brushes.Black, marginLeft + intIndent, intPosition)

                            intStrLen = CInt(e.Graphics.MeasureString(strAmount, font, New SizeF(intPrintAreaWidth, _
                                intPrintAreaHeight), fmt, Len(strAmount), 1).Width)

                            e.Graphics.DrawString(strAmount, font, Brushes.Black, marginLeft + intPrintAreaWidth - intStrLen, intPosition)

                            decSpecialFunctions += CDec(dvTips.Item(sf)("Amount"))
                            intPosition += intLineSpacing
                            sf += 1
                        Loop

                        If intPosition >= marginTop + intPrintAreaHeight Then
                            e.HasMorePages = True
                            intPageNumber += 1
                            intPosition = marginTop
                            Exit Sub
                        End If

                        e.Graphics.DrawString("Special Function Subtotal", font, Brushes.Black, marginLeft, intPosition)

                        Dim strSpecialFunctions As String = Format(decSpecialFunctions, "0.00")

                        intStrLen = CInt(e.Graphics.MeasureString(strSpecialFunctions, font, _
                            New SizeF(intPrintAreaWidth, intPrintAreaHeight), fmt, Len(strSpecialFunctions), 1).Width)

                        e.Graphics.DrawString(strSpecialFunctions, font, Brushes.Black, marginLeft + intPrintAreaWidth - intStrLen, _
                            intPosition)

                        decChargeTipTotal += decSpecialFunctions
                        decSpecialFunctionGrandTotal += decSpecialFunctions
                        intPosition += intExtraLineSpacing

                        decSpecialFunctions = 0
                        sf = 0

                    Case intChargeTips 'Always performed.
                        If intPosition >= marginTop + intPrintAreaHeight Then
                            e.HasMorePages = True
                            intPageNumber += 1
                            intPosition = marginTop
                            Exit Sub
                        End If

                        e.Graphics.DrawString("Charge Tips", fontBold, Brushes.Black, marginLeft, intPosition)

                        Dim strChargeTips As String = Format(decChargeTipTotal, "0.00")
                        Dim intStrLen As Integer = CInt(e.Graphics.MeasureString(strChargeTips, fontBold, _
                            New SizeF(intPrintAreaWidth, intPrintAreaHeight), fmt, Len(strChargeTips), 1).Width)

                        e.Graphics.DrawString(strChargeTips, fontBold, Brushes.Black, marginLeft + intPrintAreaWidth - intStrLen, _
                            intPosition)

                        decTipTotal += decChargeTipTotal
                        intPosition += intExtraLineSpacing

                    Case intCash
                        If optCash.Checked = False Then
                            intCurrentStep += 1
                            Continue While
                        End If

                        If intPosition >= marginTop + intPrintAreaHeight Then
                            e.HasMorePages = True
                            intPageNumber += 1
                            intPosition = marginTop
                            Exit Sub
                        End If

                        If optSelectedDate.Checked = False Then
                            dvTips.RowFilter += " AND Description = 'Cash '"
                        Else
                            dvTips.RowFilter += " AND Description = 'Cash ' AND WorkingDate = '" & _
                                Format(CDate(txtSelectedDate.Text), "MM/dd/yyyy") & "'"
                        End If

                        If dvTips.Count = 0 Then
                            intCurrentStep += 1
                            Continue While
                        End If


                        Static decCash As Decimal = 0
                        Dim intStrLen As Integer = 0

                        Static ca As Integer = 0

                        Do Until ca = dvTips.Count
                            If intPosition >= marginTop + intPrintAreaHeight Then
                                e.HasMorePages = True
                                intPageNumber += 1
                                intPosition = marginTop
                                Exit Sub
                            End If

                            Dim strWorkingDate As String = Format(CDate(dvTips.Item(ca)("WorkingDate")), "MM/dd/yyyy")
                            Dim strDescription As String = dvTips.Item(ca)("Description").ToString
                            Dim strAmount As String = Format(CDec(dvTips.Item(ca)("Amount")), "0.00")


                            e.Graphics.DrawString(strWorkingDate, font, Brushes.Black, marginLeft, intPosition)
                            e.Graphics.DrawString(strDescription, font, Brushes.Black, marginLeft + intIndent, intPosition)

                            intStrLen = CInt(e.Graphics.MeasureString(strAmount, font, New SizeF(intPrintAreaWidth, _
                                intPrintAreaHeight), fmt, Len(strAmount), 1).Width)

                            e.Graphics.DrawString(strAmount, font, Brushes.Black, marginLeft + intPrintAreaWidth - intStrLen, intPosition)

                            decCash += CDec(dvTips.Item(ca)("Amount"))
                            intPosition += intLineSpacing
                            ca += 1
                        Loop

                        If intPosition >= marginTop + intPrintAreaHeight Then
                            e.HasMorePages = True
                            intPageNumber += 1
                            intPosition = marginTop
                            Exit Sub
                        End If

                        e.Graphics.DrawString("Cash  Subtotal", font, Brushes.Black, marginLeft, intPosition)

                        Dim stcaash As String = Format(decCash, "0.00")

                        intStrLen = CInt(e.Graphics.MeasureString(stcaash, font, _
                            New SizeF(intPrintAreaWidth, intPrintAreaHeight), fmt, Len(stcaash), 1).Width)

                        e.Graphics.DrawString(stcaash, font, Brushes.Black, marginLeft + intPrintAreaWidth - intStrLen, _
                            intPosition)

                        decTipTotal += decCash
                        decCashGrandTotal += decCash
                        intPosition += intExtraLineSpacing

                        decCash = 0
                        ca = 0

                    Case intTotalTips 'Always performed.
                        If intPosition >= marginTop + intPrintAreaHeight Then
                            e.HasMorePages = True
                            intPageNumber += 1
                            intPosition = marginTop
                            Exit Sub
                        End If

                        e.Graphics.DrawString("Total Tips", fontBold, Brushes.Black, marginLeft, intPosition)

                        Dim strTipTotal As String = Format(decTipTotal, "0.00")
                        Dim intStrLen As Integer = CInt(e.Graphics.MeasureString(strTipTotal, fontBold, _
                            New SizeF(intPrintAreaWidth, intPrintAreaHeight), fmt, Len(strTipTotal), 1).Width)

                        e.Graphics.DrawString(strTipTotal, fontBold, Brushes.Black, marginLeft + intPrintAreaWidth - intStrLen, _
                            intPosition)

                        intPosition += intExtraLineSpacing
                End Select

                intCurrentStep += 1
            End While

            intCurrentServer += 1
            intCurrentStep = 1
            decChargeTipTotal = 0
            decTipTotal = 0
        Loop

        Dim intStringLength As Integer

        If intPosition >= marginTop + intPrintAreaHeight Then
            e.HasMorePages = True
            intPageNumber += 1
            intPosition = marginTop
            Exit Sub
        End If

        Static blnHeaderPrinted As Boolean
        Static blnCCPrinted As Boolean
        Static blnRCPrinted As Boolean
        Static blnSFPrinted As Boolean
        Static blnChargePrinted As Boolean
        Static blnCashPrinted As Boolean
        Static blnTotalPrinted As Boolean

        If optSelectedServer.Checked = False Then
            If blnHeaderPrinted = False Then
                e.Graphics.DrawString("TOTAL FOR ALL SERVERS", fontBold, Brushes.Black, marginLeft, intPosition)
                intPosition += intLineSpacing

                blnHeaderPrinted = True
            End If

            If optCreditCard.Checked = True And blnCCPrinted = False Then
                'Draw credit card grand total.
                If intPosition >= marginTop + intPrintAreaHeight Then
                    e.HasMorePages = True
                    intPageNumber += 1
                    intPosition = marginTop
                    Exit Sub
                End If

                e.Graphics.DrawString("Credit Card", font, Brushes.Black, marginLeft, intPosition)

                Dim strCreditCardGrand As String = Format(decCreditCardGrandTotal, "0.00")

                intStringLength = CInt(e.Graphics.MeasureString(strCreditCardGrand, font, _
                    New SizeF(intPrintAreaWidth, intPrintAreaHeight), fmt, Len(strCreditCardGrand), 1).Width)

                e.Graphics.DrawString(strCreditCardGrand, font, Brushes.Black, marginLeft + intPrintAreaWidth - intStringLength, intPosition)

                blnCCPrinted = True
                intPosition += intLineSpacing
            End If
            If optRoomCharge.Checked = True And blnRCPrinted = False Then
                'Draw room charge grand total.
                If intPosition >= marginTop + intPrintAreaHeight Then
                    e.HasMorePages = True
                    intPageNumber += 1
                    intPosition = marginTop
                    Exit Sub
                End If

                e.Graphics.DrawString("Room Charge", font, Brushes.Black, marginLeft, intPosition)

                Dim strRoomChargeGrand As String = Format(decRoomChargeGrandTotal, "0.00")

                intStringLength = CInt(e.Graphics.MeasureString(strRoomChargeGrand, font, _
                    New SizeF(intPrintAreaWidth, intPrintAreaHeight), fmt, Len(strRoomChargeGrand), 1).Width)

                e.Graphics.DrawString(strRoomChargeGrand, font, Brushes.Black, marginLeft + intPrintAreaWidth - intStringLength, intPosition)

                blnRCPrinted = True
                intPosition += intLineSpacing
            End If

            If optSpecialFunction.Checked = True And blnSFPrinted = False Then
                'Draw special function grand total.
                If intPosition >= marginTop + intPrintAreaHeight Then
                    e.HasMorePages = True
                    intPageNumber += 1
                    intPosition = marginTop
                    Exit Sub
                End If

                e.Graphics.DrawString("Special Function", font, Brushes.Black, marginLeft, intPosition)

                Dim strSpecialFunctionGrand As String = Format(decSpecialFunctionGrandTotal, "0.00")

                intStringLength = CInt(e.Graphics.MeasureString(strSpecialFunctionGrand, font, _
                    New SizeF(intPrintAreaWidth, intPrintAreaHeight), fmt, Len(strSpecialFunctionGrand), 1).Width)

                e.Graphics.DrawString(strSpecialFunctionGrand, font, Brushes.Black, marginLeft + intPrintAreaWidth - intStringLength, intPosition)

                blnSFPrinted = True
                intPosition += intLineSpacing
            End If

            'Draw charge tips grand total.
            If blnChargePrinted = False Then
                If intPosition >= marginTop + intPrintAreaHeight Then
                    e.HasMorePages = True
                    intPageNumber += 1
                    intPosition = marginTop
                    Exit Sub
                End If

                e.Graphics.DrawString("Charge Tips", fontBold, Brushes.Black, marginLeft, intPosition)

                Dim strChargeTipsGrand As String = _
                    Format(decCreditCardGrandTotal + decRoomChargeGrandTotal + decSpecialFunctionGrandTotal, "0.00")

                intStringLength = CInt(e.Graphics.MeasureString(strChargeTipsGrand, fontBold, _
                    New SizeF(intPrintAreaWidth, intPrintAreaHeight), fmt, Len(strChargeTipsGrand), 1).Width)

                e.Graphics.DrawString(strChargeTipsGrand, fontBold, Brushes.Black, marginLeft + intPrintAreaWidth - intStringLength, intPosition)

                blnChargePrinted = True
                intPosition += intLineSpacing
            End If

            If optCash.Checked = True And blnCashPrinted = False Then
                'Draw cash grand total.
                If intPosition >= marginTop + intPrintAreaHeight Then
                    e.HasMorePages = True
                    intPageNumber += 1
                    intPosition = marginTop
                    Exit Sub
                End If

                e.Graphics.DrawString("Cash", font, Brushes.Black, marginLeft, intPosition)

                Dim strCashGrand As String = Format(decCashGrandTotal, "0.00")

                intStringLength = CInt(e.Graphics.MeasureString(strCashGrand, font, _
                    New SizeF(intPrintAreaWidth, intPrintAreaHeight), fmt, Len(strCashGrand), 1).Width)

                e.Graphics.DrawString(strCashGrand, font, Brushes.Black, marginLeft + intPrintAreaWidth - intStringLength, intPosition)

                blnCashPrinted = True
                intPosition += intLineSpacing
            End If

            'Draw grand total.
            If blnTotalPrinted = False Then
                If intPosition >= marginTop + intPrintAreaHeight Then
                    e.HasMorePages = True
                    intPageNumber += 1
                    intPosition = marginTop
                    Exit Sub
                End If

                e.Graphics.DrawString("GRAND TOTAL", fontBold, Brushes.Black, marginLeft, intPosition)

                Dim strGrandTotal As String = _
                    Format(decCreditCardGrandTotal + decRoomChargeGrandTotal + decSpecialFunctionGrandTotal + decCashGrandTotal, _
                    "0.00")

                intStringLength = CInt(e.Graphics.MeasureString(strGrandTotal, fontBold, _
                    New SizeF(intPrintAreaWidth, intPrintAreaHeight), fmt, Len(strGrandTotal), 1).Width)

                e.Graphics.DrawString(strGrandTotal, fontBold, Brushes.Black, marginLeft + intPrintAreaWidth - intStringLength, intPosition)
                blnTotalPrinted = True
            End If
        End If

        'End of report.  Reset all static variables.
        blnHeaderPrinted = False
        blnCCPrinted = False
        blnRCPrinted = False
        blnSFPrinted = False
        blnChargePrinted = False
        blnCashPrinted = False
        blnTotalPrinted = False

        decCreditCardGrandTotal = 0
        decRoomChargeGrandTotal = 0
        decSpecialFunctionGrandTotal = 0
        decCashGrandTotal = 0

        intCurrentServer = 0
        e.HasMorePages = False
        intPosition = marginTop
        intPageNumber = 1
    End Sub

    Private Sub DetailByDate(ByVal sender As System.Object, ByVal e As PrintPageEventArgs)
        Dim font As New System.Drawing.Font("Calibri", 10)
        Dim fontBold As New System.Drawing.Font("Calibri", 10, FontStyle.Bold)

        Dim fmt As New StringFormat(StringFormatFlags.LineLimit)

        'Initialize local static variables that contain the current line position and
        'the current page number.
        Static intPosition As Single = 75
        Static intPageNumber As Integer = 1

        Const intLineSpacing As Integer = 15
        Const intExtraLineSpacing As Integer = 30
        Const intIndent As Integer = 350

        'Initialize local constants that contain the normal line spacing and padded 
        'line spacing (double spacing).
        Dim intPrintAreaHeight, intPrintAreaWidth, marginLeft, marginTop As Int32

        With e.MarginBounds
            ' Initialize local variables that contain the bounds of the printing 
            ' area rectangle.
            intPrintAreaHeight = .Height
            intPrintAreaWidth = .Width

            ' Initialize local variables to hold margin values that will serve
            ' as the X and Y coordinates for the upper left corner of the printing 
            ' area rectangle.
            marginLeft = .Left ' X coordinate
            marginTop = .Top ' Y coordinate
        End With

        'Draw the header to the page.  Header will be drawn on every page.
        e.Graphics.DrawString("Tip Report - Detail by Date", font, Brushes.Black, marginLeft, intPosition)
        intPosition += intLineSpacing

        e.Graphics.DrawString(Format(DateTime.Now, "MM/dd/yyyy") & " " & Format(DateTime.Now, "t"), font, Brushes.Black, marginLeft, intPosition)
        intPosition += intLineSpacing

        If optAllServers.Checked = True Then
            e.Graphics.DrawString("All Servers", font, Brushes.Black, marginLeft, intPosition)
        Else
            Dim strServerNumber As String = cboServers.SelectedValue.ToString
            Dim strName As String = Me.m_dsParentDataSet.Servers.FindByServerNumber(strServerNumber)("FirstName").ToString & _
                " " & Me.m_dsParentDataSet.Servers.FindByServerNumber(strServerNumber)("LastName").ToString

            e.Graphics.DrawString("Selected Server: " & strName, font, Brushes.Black, marginLeft, intPosition)
        End If
        intPosition += intLineSpacing

        If optAllDates.Checked = True Then
            e.Graphics.DrawString("All Dates", font, Brushes.Black, marginLeft, intPosition)
        Else
            e.Graphics.DrawString("Selected Date: " & Format(CDate(txtSelectedDate.Text), "MM/dd/yyyy"), _
                font, Brushes.Black, marginLeft, intPosition)
        End If
        intPosition += intLineSpacing

        Dim strIncludedTypes As String = "Types Included: "
        If optCreditCard.Checked = True Then strIncludedTypes += "CC "
        If optRoomCharge.Checked = True Then strIncludedTypes += "RC "
        If optSpecialFunction.Checked = True Then strIncludedTypes += "SF "
        If optCash.Checked = True Then strIncludedTypes += "CA "

        e.Graphics.DrawString(strIncludedTypes, font, Brushes.Black, marginLeft, intPosition)
        intPosition += intLineSpacing

        e.Graphics.DrawString("Page " & intPageNumber, font, Brushes.Black, marginLeft, intPosition)
        intPosition += intExtraLineSpacing

        'Initialize a list variable to hold the list of dates that will be printed.  If only
        'one date is selected, that date will be added to the list.  If all dates are selected,
        'each date in the pay period will be added to the list.
        Dim lstDates As New List(Of Date)
        Static dteDateCounter As Date

        If optAllDates.Checked = True Then
            Dim dtePeriodStart As Date = CDate(Me.m_dsParentDataSet.Settings.FindBySetting("PeriodStart")("Value"))
            Dim dtePeriodEnd As Date = CDate(Me.m_dsParentDataSet.Settings.FindBySetting("PeriodEnd")("Value"))

            dteDateCounter = dtePeriodStart

            Do Until dteDateCounter > dtePeriodEnd
                lstDates.Add(dteDateCounter)
                dteDateCounter = DateAdd(DateInterval.Day, 1, dteDateCounter)
            Loop
        Else
            Dim dteSelectedDate As Date = CDate(txtSelectedDate.Text)

            lstDates.Add(dteSelectedDate)
        End If

        'Loop through each day in the list and total the tips for each tip type.
        'The report will be printed in steps.  Since the date is ignored for cash tips
        '(because they are cumulative for the entire pay period) there is no step for
        'adding cash tips by date.
        Const intDate As Integer = 1
        Const intCreditCard As Integer = 2
        Const intRoomCharge As Integer = 3
        Const intSpecialFunction As Integer = 4
        Const intTotalTips As Integer = 5

        Static intCounter As Integer = 0
        Static intStep As Integer = 1

        Static decCreditCardGrandTotal As Decimal = 0
        Static decRoomChargeGrandTotal As Decimal = 0
        Static decSpecialFunctionGrandTotal As Decimal = 0
        Static decCashGrandTotal As Decimal = 0

        Dim intStrLen As Integer

        Do Until intCounter = lstDates.Count
            Static decDailyTotal As Decimal = 0

            While intStep <= intTotalTips
                Dim dvTips As New DataView
                dvTips.Table = Me.m_dsParentDataSet.Tips
                dvTips.RowFilter = "WorkingDate = '" & Format(lstDates.Item(intCounter), "MM/dd/yyyy") & "' AND Description <> 'Cash'"

                If optSelectedServer.Checked = True Then
                    Dim strServerNumber As String = cboServers.SelectedValue.ToString
                    dvTips.RowFilter += " AND ServerNumber = '" & strServerNumber & "'"
                End If

                If dvTips.Count = 0 Then
                    intCounter += 1
                    Continue Do
                End If

                Select Case intStep
                    Case intDate
                        If intPosition >= marginTop + intPrintAreaHeight Then
                            e.HasMorePages = True
                            intPageNumber += 1
                            intPosition = marginTop
                            Exit Sub
                        End If

                        Dim dteCurrentDate As Date = lstDates.Item(intCounter)

                        e.Graphics.DrawString(Format(dteCurrentDate, "MM/dd/yyyy"), fontBold, Brushes.Black, marginLeft, _
                            intPosition)

                        intPosition += intExtraLineSpacing
                    Case intCreditCard
                        If optCreditCard.Checked = False Then
                            intStep += 1
                            Continue While
                        End If

                        dvTips.RowFilter += " AND Description = 'Credit Card'"

                        If dvTips.Count = 0 Then
                            intStep += 1
                            Continue While
                        End If

                        dvTips.Sort = "LastName, FirstName, ServerNumber, Amount"

                        Static decCreditCards As Decimal = 0
                        Static cc As Integer = 0

                        Do Until cc = dvTips.Count
                            Dim strServerNumber As String = dvTips.Item(cc)("ServerNumber").ToString
                            Dim strLastName As String = dvTips.Item(cc)("LastName").ToString
                            Dim strFirstName As String = dvTips.Item(cc)("FirstName").ToString
                            Dim strDescription As String = dvTips.Item(cc)("Description").ToString
                            Dim decAmount As Decimal = CDec(dvTips.Item(cc)("Amount"))

                            If intPosition >= marginTop + intPrintAreaHeight Then
                                e.HasMorePages = True
                                intPageNumber += 1
                                intPosition = marginTop
                                Exit Sub
                            End If

                            Dim strAmount As String = Format(decAmount, "0.00")

                            e.Graphics.DrawString(strServerNumber & "  " & strLastName & ", " & strFirstName, font, Brushes.Black, marginLeft, intPosition)

                            e.Graphics.DrawString(strDescription, font, Brushes.Black, marginLeft + intIndent, intPosition)

                            intStrLen = CInt(e.Graphics.MeasureString(strAmount, font, _
                                New SizeF(intPrintAreaWidth, intPrintAreaHeight), fmt, Len(strAmount), 1).Width)

                            e.Graphics.DrawString(strAmount, font, Brushes.Black, marginLeft + intPrintAreaWidth - intStrLen, _
                                intPosition)

                            intPosition += intLineSpacing

                            decCreditCards += CDec(dvTips.Item(cc)("Amount"))
                            cc += 1
                        Loop

                        e.Graphics.DrawString("Credit Card Subtotal", font, Brushes.Black, marginLeft, intPosition)

                        Dim strCreditCards As String = Format(decCreditCards, "0.00")

                        intStrLen = CInt(e.Graphics.MeasureString(strCreditCards, font, _
                            New SizeF(intPrintAreaWidth, intPrintAreaHeight), fmt, Len(strCreditCards), 1).Width)

                        e.Graphics.DrawString(strCreditCards, font, Brushes.Black, marginLeft + intPrintAreaWidth - intStrLen, _
                            intPosition)

                        decDailyTotal += decCreditCards
                        decCreditCardGrandTotal += decCreditCards
                        intPosition += intExtraLineSpacing

                        decCreditCards = 0
                        cc = 0
                    Case intRoomCharge
                        If optRoomCharge.Checked = False Then
                            intStep += 1
                            Continue While
                        End If

                        dvTips.RowFilter += " AND Description = 'Room Charge'"

                        If dvTips.Count = 0 Then
                            intStep += 1
                            Continue While
                        End If

                        dvTips.Sort = "LastName, FirstName, ServerNumber, Amount"

                        Static decRoomCharges As Decimal = 0
                        Static rc As Integer = 0

                        Do Until rc = dvTips.Count
                            Dim strServerNumber As String = dvTips.Item(rc)("ServerNumber").ToString
                            Dim strLastName As String = dvTips.Item(rc)("LastName").ToString
                            Dim strFirstName As String = dvTips.Item(rc)("FirstName").ToString
                            Dim strDescription As String = dvTips.Item(rc)("Description").ToString
                            Dim decAmount As Decimal = CDec(dvTips.Item(rc)("Amount"))

                            If intPosition >= marginTop + intPrintAreaHeight Then
                                e.HasMorePages = True
                                intPageNumber += 1
                                intPosition = marginTop
                                Exit Sub
                            End If

                            Dim strAmount As String = Format(decAmount, "0.00")

                            e.Graphics.DrawString(strServerNumber & "  " & strLastName & ", " & strFirstName, font, Brushes.Black, marginLeft, intPosition)

                            e.Graphics.DrawString(strDescription, font, Brushes.Black, marginLeft + intIndent, intPosition)

                            intStrLen = CInt(e.Graphics.MeasureString(strAmount, font, _
                                New SizeF(intPrintAreaWidth, intPrintAreaHeight), fmt, Len(strAmount), 1).Width)

                            e.Graphics.DrawString(strAmount, font, Brushes.Black, marginLeft + intPrintAreaWidth - intStrLen, _
                                intPosition)

                            intPosition += intLineSpacing

                            decRoomCharges += CDec(dvTips.Item(rc)("Amount"))
                            rc += 1
                        Loop

                        e.Graphics.DrawString("Room Charge Subtotal", font, Brushes.Black, marginLeft, intPosition)

                        Dim strRoomCharges As String = Format(decRoomCharges, "0.00")

                        intStrLen = CInt(e.Graphics.MeasureString(strRoomCharges, font, _
                            New SizeF(intPrintAreaWidth, intPrintAreaHeight), fmt, Len(strRoomCharges), 1).Width)

                        e.Graphics.DrawString(strRoomCharges, font, Brushes.Black, marginLeft + intPrintAreaWidth - intStrLen, _
                            intPosition)

                        decDailyTotal += decRoomCharges
                        decRoomChargeGrandTotal += decRoomCharges
                        intPosition += intExtraLineSpacing

                        decRoomCharges = 0
                        rc = 0
                    Case intSpecialFunction
                        If optSpecialFunction.Checked = False Then
                            intStep += 1
                            Continue While
                        End If

                        dvTips.RowFilter += " AND Description = 'Special Function'"

                        If dvTips.Count = 0 Then
                            intStep += 1
                            Continue While
                        End If

                        dvTips.Sort = "LastName, FirstName, ServerNumber, Amount"

                        Static decSpecialFunctions As Decimal = 0
                        Static sf As Integer = 0

                        Do Until sf = dvTips.Count
                            Dim strServerNumber As String = dvTips.Item(sf)("ServerNumber").ToString
                            Dim strLastName As String = dvTips.Item(sf)("LastName").ToString
                            Dim strFirstName As String = dvTips.Item(sf)("FirstName").ToString
                            Dim strDescription As String = dvTips.Item(sf)("Description").ToString & ": " & dvTips.Item(sf)("SpecialFunction").ToString
                            Dim decAmount As Decimal = CDec(dvTips.Item(sf)("Amount"))

                            If intPosition >= marginTop + intPrintAreaHeight Then
                                e.HasMorePages = True
                                intPageNumber += 1
                                intPosition = marginTop
                                Exit Sub
                            End If

                            Dim strAmount As String = Format(decAmount, "0.00")

                            e.Graphics.DrawString(strServerNumber & "  " & strLastName & ", " & strFirstName, font, Brushes.Black, marginLeft, intPosition)

                            e.Graphics.DrawString(strDescription, font, Brushes.Black, marginLeft + intIndent, intPosition)

                            intStrLen = CInt(e.Graphics.MeasureString(strAmount, font, _
                                New SizeF(intPrintAreaWidth, intPrintAreaHeight), fmt, Len(strAmount), 1).Width)

                            e.Graphics.DrawString(strAmount, font, Brushes.Black, marginLeft + intPrintAreaWidth - intStrLen, _
                                intPosition)

                            intPosition += intLineSpacing

                            decSpecialFunctions += CDec(dvTips.Item(sf)("Amount"))
                            sf += 1
                        Loop

                        e.Graphics.DrawString("Special Function Subtotal", font, Brushes.Black, marginLeft, intPosition)

                        Dim strSpecialFunctions As String = Format(decSpecialFunctions, "0.00")

                        intStrLen = CInt(e.Graphics.MeasureString(strSpecialFunctions, font, _
                            New SizeF(intPrintAreaWidth, intPrintAreaHeight), fmt, Len(strSpecialFunctions), 1).Width)

                        e.Graphics.DrawString(strSpecialFunctions, font, Brushes.Black, marginLeft + intPrintAreaWidth - intStrLen, _
                            intPosition)

                        decDailyTotal += decSpecialFunctions
                        decSpecialFunctionGrandTotal += decSpecialFunctions
                        intPosition += intExtraLineSpacing

                        decSpecialFunctions = 0
                        sf = 0
                    Case intTotalTips
                        If intPosition >= marginTop + intPrintAreaHeight Then
                            e.HasMorePages = True
                            intPageNumber += 1
                            intPosition = marginTop
                            Exit Sub
                        End If

                        e.Graphics.DrawString("Total Tips", fontBold, Brushes.Black, marginLeft, intPosition)

                        Dim strChargeTips As String = Format(decDailyTotal, "0.00")

                        intStrLen = CInt(e.Graphics.MeasureString(strChargeTips, font, _
                            New SizeF(intPrintAreaWidth, intPrintAreaHeight), fmt, Len(strChargeTips), 1).Width)

                        e.Graphics.DrawString(strChargeTips, fontBold, Brushes.Black, marginLeft + intPrintAreaWidth - intStrLen, _
                            intPosition)

                        intPosition += intExtraLineSpacing
                End Select
                intStep += 1
            End While

            decDailyTotal = 0
            intStep = 1
            intCounter += 1
        Loop

        If intPosition >= marginTop + intPrintAreaHeight Then
            e.HasMorePages = True
            intPageNumber += 1
            intPosition = marginTop
            Exit Sub
        End If

        Static blnHeaderPrinted As Boolean
        Static blnCCPrinted As Boolean
        Static blnRCPrinted As Boolean
        Static blnSFPrinted As Boolean
        Static blnChargePrinted As Boolean
        Static blnCashPrinted As Boolean
        Static blnTotalPrinted As Boolean

        If blnHeaderPrinted = False Then
            If optAllDates.Checked = True And blnHeaderPrinted = False Then
                e.Graphics.DrawString("TOTAL FOR ALL DATES", fontBold, Brushes.Black, marginLeft, intPosition)
                intPosition += intLineSpacing
            Else
                e.Graphics.DrawString("TOTAL FOR SELECTED DATE", fontBold, Brushes.Black, marginLeft, intPosition)
                intPosition += intLineSpacing
            End If
            blnHeaderPrinted = True
        End If

        'Print credit card totals.
        If optCreditCard.Checked = True And blnCCPrinted = False Then
            If intPosition >= marginTop + intPrintAreaHeight Then
                e.HasMorePages = True
                intPageNumber += 1
                intPosition = marginTop
                Exit Sub
            End If

            e.Graphics.DrawString("Credit Card", font, Brushes.Black, marginLeft, intPosition)

            Dim strCreditCardGrandTotal As String = Format(decCreditCardGrandTotal, "0.00")

            intStrLen = CInt(e.Graphics.MeasureString(strCreditCardGrandTotal, font, New SizeF(intPrintAreaWidth, _
                intPrintAreaHeight), fmt, Len(strCreditCardGrandTotal), 1).Width)

            e.Graphics.DrawString(strCreditCardGrandTotal, font, Brushes.Black, marginLeft + intPrintAreaWidth - intStrLen, intPosition)
            intPosition += intLineSpacing
            blnCCPrinted = True
        End If

        'Print room charge totals.
        If optRoomCharge.Checked = True And blnRCPrinted = False Then
            If intPosition >= marginTop + intPrintAreaHeight Then
                e.HasMorePages = True
                intPageNumber += 1
                intPosition = marginTop
                Exit Sub
            End If

            e.Graphics.DrawString("Room Charge", font, Brushes.Black, marginLeft, intPosition)

            Dim strRoomChargeGrandTotal As String = Format(decRoomChargeGrandTotal, "0.00")

            intStrLen = CInt(e.Graphics.MeasureString(strRoomChargeGrandTotal, font, New SizeF(intPrintAreaWidth, _
                intPrintAreaHeight), fmt, Len(strRoomChargeGrandTotal), 1).Width)

            e.Graphics.DrawString(strRoomChargeGrandTotal, font, Brushes.Black, marginLeft + intPrintAreaWidth - intStrLen, intPosition)
            intPosition += intLineSpacing
            blnRCPrinted = True
        End If

        'Print special function totals.
        If optSpecialFunction.Checked = True And blnSFPrinted = False Then
            If intPosition >= marginTop + intPrintAreaHeight Then
                e.HasMorePages = True
                intPageNumber += 1
                intPosition = marginTop
                Exit Sub
            End If

            e.Graphics.DrawString("Special Function", font, Brushes.Black, marginLeft, intPosition)

            Dim strSpecialFunctionGrandTotal As String = Format(decSpecialFunctionGrandTotal, "0.00")

            intStrLen = CInt(e.Graphics.MeasureString(strSpecialFunctionGrandTotal, font, New SizeF(intPrintAreaWidth, _
                intPrintAreaHeight), fmt, Len(strSpecialFunctionGrandTotal), 1).Width)

            e.Graphics.DrawString(strSpecialFunctionGrandTotal, font, Brushes.Black, marginLeft + intPrintAreaWidth - intStrLen, intPosition)
            intPosition += intLineSpacing
            blnSFPrinted = True
        End If

        'Print charge tip totals.
        If blnChargePrinted = False Then
            If intPosition >= marginTop + intPrintAreaHeight Then
                e.HasMorePages = True
                intPageNumber += 1
                intPosition = marginTop
                Exit Sub
            End If


            e.Graphics.DrawString("Charge Tips", fontBold, Brushes.Black, marginLeft, intPosition)

            Dim strChargeTipGrandTotal As String = Format(decCreditCardGrandTotal + decRoomChargeGrandTotal + _
                decSpecialFunctionGrandTotal, "0.00")

            intStrLen = CInt(e.Graphics.MeasureString(strChargeTipGrandTotal, fontBold, New SizeF(intPrintAreaWidth, _
                intPrintAreaHeight), fmt, Len(strChargeTipGrandTotal), 1).Width)

            e.Graphics.DrawString(strChargeTipGrandTotal, fontBold, Brushes.Black, marginLeft + intPrintAreaWidth - intStrLen, intPosition)
            intPosition += intLineSpacing
            blnChargePrinted = True
        End If

        Static decCashTotal As Decimal = 0

        'Total cash tips.
        If blnCashPrinted = False Then
            Dim dvCash As New DataView

            dvCash.Table = Me.m_dsParentDataSet.Tips
            dvCash.RowFilter = "Description = 'Cash'"

            If optSelectedServer.Checked = True Then
                dvCash.RowFilter += " AND ServerNumber = '" & cboServers.SelectedValue.ToString & "'"
            End If

            If dvCash.Count <> 0 Then
                Dim i As Integer = 0
                Do Until i = dvCash.Count
                    decCashTotal += CDec(dvCash.Item(i)("Amount"))
                    i += 1
                Loop
            End If

            'Print cash total if option is selected.
            If optCash.Checked = True Then
                If intPosition >= marginTop + intPrintAreaHeight Then
                    e.HasMorePages = True
                    intPageNumber += 1
                    intPosition = marginTop
                    Exit Sub
                End If

                e.Graphics.DrawString("Total Cash Tips", font, Brushes.Black, marginLeft, intPosition)

                Dim strCashTotal As String = Format(decCashTotal, "0.00")

                intStrLen = CInt(e.Graphics.MeasureString(strCashTotal, font, New SizeF(intPrintAreaWidth, intPrintAreaHeight), _
                    fmt, Len(strCashTotal), 1).Width)

                e.Graphics.DrawString(strCashTotal, font, Brushes.Black, marginLeft + intPrintAreaWidth - intStrLen, intPosition)
                intPosition += intLineSpacing
            End If
            blnCashPrinted = True
        End If

        'Total all tips.
        If blnTotalPrinted = False Then
            If intPosition >= marginTop + intPrintAreaHeight Then
                e.HasMorePages = True
                intPageNumber += 1
                intPosition = marginTop
                Exit Sub
            End If

            e.Graphics.DrawString("GRAND TOTAL", fontBold, Brushes.Black, marginLeft, intPosition)

            Dim strGrandTotal As String = Format(decCreditCardGrandTotal + decRoomChargeGrandTotal + _
                decSpecialFunctionGrandTotal + decCashTotal, "0.00")

            intStrLen = CInt(e.Graphics.MeasureString(strGrandTotal, fontBold, New SizeF(intPrintAreaWidth, _
                intPrintAreaHeight), fmt, Len(strGrandTotal), 1).Width)

            e.Graphics.DrawString(strGrandTotal, fontBold, Brushes.Black, marginLeft + intPrintAreaWidth - intStrLen, intPosition)
            intPosition += intLineSpacing
            blnTotalPrinted = True
        End If

        'End of report.  Reset all static variables.
        blnHeaderPrinted = False
        blnCCPrinted = False
        blnRCPrinted = False
        blnSFPrinted = False
        blnChargePrinted = False
        blnCashPrinted = False
        blnTotalPrinted = False

        intCounter = 0
        decCreditCardGrandTotal = 0
        decRoomChargeGrandTotal = 0
        decSpecialFunctionGrandTotal = 0
        decCashGrandTotal = 0
        decCashTotal = 0

        e.HasMorePages = False
        intPosition = marginTop
        intPageNumber = 1
    End Sub

End Class