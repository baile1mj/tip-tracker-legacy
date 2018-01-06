Public Class frmPrintRegularTipChits
    Friend m_dsParentDataset As New FileDataSet
    Private m_dtServers As New FileDataSet.ServersDataTable
    Private m_dvServersDataView As New DataView

    Private Sub frmPrintTipChits_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
        PopulateComboBox()
        optAll.Checked = True
    End Sub

    Private Sub PopulateComboBox()
        cboSelectServer.Items.Clear()

        For Each row As DataRow In m_dsParentDataset.Servers.Rows
            If row.RowState <> DataRowState.Deleted Then
                Dim strServerNumber As String = row("ServerNumber").ToString
                Dim strFirstName As String = row("FirstName").ToString
                Dim strLastName As String = row("LastName").ToString

                cboSelectServer.Items.Add(strServerNumber & ": " & strFirstName & " " & strLastName)
            End If
        Next

        cboSelectServer.Sorted = True
    End Sub

    Private Function ExtractServerNumber(ByVal ComboBoxString As String) As String
        Dim strExtractedString As String = ""

        For i As Integer = 1 To Len(ComboBoxString)
            Dim c As Char = GetChar(ComboBoxString, i)
            If c = ":" Then
                strExtractedString = Microsoft.VisualBasic.Left(ComboBoxString, i - 1)
                Exit For
            End If
        Next

        Return strExtractedString
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

        With docTipChits.DefaultPageSettings.Margins
            .Top = 75
            .Bottom = 75
            .Left = 75
            .Right = 75
        End With

        m_dtServers.Merge(Me.m_dsParentDataset.Servers)
        m_dtServers.AcceptChanges()

        For Each row As DataRow In m_dtServers.Rows
            Dim strServerNumber As String = row("ServerNumber").ToString

            Dim dv As New DataView

            dv.Table = Me.m_dsParentDataset.Tips
            dv.RowFilter = "ServerNumber = '" & strServerNumber & "'"

            If dv.Count = 0 Then row.Delete()
        Next

        m_dtServers.AcceptChanges()

        m_dvServersDataView.Table = m_dtServers
        m_dvServersDataView.Sort = "LastName, FirstName"

        If optSelected.Checked = True Then
            Dim strServerNumber As String = ExtractServerNumber(cboSelectServer.SelectedItem.ToString)

            m_dvServersDataView.RowFilter = "ServerNumber = '" & strServerNumber & "'"
        End If

        If m_dvServersDataView.Count = 0 Then
            MessageBox.Show("There are no tips to report.", "No Tips", MessageBoxButtons.OK)
            Me.Close()
            Exit Sub
        End If

        Try
            Windows.Forms.Cursor.Current = Cursors.Default

            ' Convert the dialog into a Form.
            Dim dlg As Form = DirectCast(dlgPrintPreview, Form)

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
            Exit Sub
        End Try

        Me.Close()
    End Sub

    Private Sub btnPrint_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnPrint.Click
        If optSelected.Checked = True And cboSelectServer.SelectedIndex = -1 Then
            MessageBox.Show("You must select a server.", "Select Server", MessageBoxButtons.OK)
            Exit Sub
        End If

        With docTipChits.DefaultPageSettings.Margins
            .Top = 75
            .Bottom = 75
            .Left = 75
            .Right = 75
        End With

        m_dtServers.Merge(Me.m_dsParentDataset.Servers)
        m_dtServers.AcceptChanges()

        For Each row As DataRow In m_dtServers.Rows
            Dim strServerNumber As String = row("ServerNumber").ToString

            Dim dv As New DataView

            dv.Table = Me.m_dsParentDataset.Tips
            dv.RowFilter = "ServerNumber = '" & strServerNumber & "'"

            If dv.Count = 0 Then row.Delete()
        Next

        m_dtServers.AcceptChanges()

        m_dvServersDataView.Table = m_dtServers
        m_dvServersDataView.Sort = "LastName, FirstName"

        If optSelected.Checked = True Then
            Dim strServerNumber As String = ExtractServerNumber(cboSelectServer.SelectedItem.ToString)

            m_dvServersDataView.RowFilter = "ServerNumber = '" & strServerNumber & "'"
        End If

        If m_dvServersDataView.Count = 0 Then
            MessageBox.Show("There are no tips to report.", "No Tips", MessageBoxButtons.OK)
            Me.Close()
            Exit Sub
        End If

        Try
            If dlgPrint.ShowDialog <> Windows.Forms.DialogResult.OK Then
                dlgPrint.Dispose()
                Me.Close()
                Exit Sub
            End If

            docTipChits.Print()
        Catch ex As Exception
            MessageBox.Show("Could not print the document.  Check that there is a printer installed and that a default printer has been selected.", "Cannot Print Document", MessageBoxButtons.OK)
            Exit Sub
        End Try

        Me.Close()
    End Sub

    Private Sub docTipChits_PrintPage(ByVal sender As System.Object, ByVal e As System.Drawing.Printing.PrintPageEventArgs) Handles docTipChits.PrintPage
        Dim font As New System.Drawing.Font("Calibri", 10)
        Dim fontBold As New System.Drawing.Font("Calibri", 10, FontStyle.Bold)

        Dim fmt As New StringFormat(StringFormatFlags.LineLimit)

        'Initialize local static variables that contain the current line position and
        'the current page number.
        Static position As Single = 50

        'Initialize local constants that contain the normal line spacing and padded 
        'line spacing (double spacing).
        Const intLineSpacing As Integer = 20
        Const intExtraLineSpacing As Integer = 50
        Const intDatePos As Integer = 150
        Const intRoomChargePos As Integer = 400
        Const intCreditCardPos As Integer = 550
        Const intDailyPos As Integer = 750

        Dim intPrintAreaHeight, intPrintAreaWidth, marginLeft, marginTop As Int32
        With docTipChits.DefaultPageSettings
            ' Initialize local variables that contain the bounds of the printing 
            ' area rectangle.
            intPrintAreaHeight = .PaperSize.Height - .Margins.Top - .Margins.Bottom
            intPrintAreaWidth = .PaperSize.Width - .Margins.Left - .Margins.Right

            ' Initialize local variables to hold margin values that will serve
            ' as the X and Y coordinates for the upper left corner of the printing 
            ' area rectangle.
            marginLeft = .Margins.Left ' X coordinate
            marginTop = .Margins.Top ' Y coordinate
        End With

        'SERVER REPORTING BEGINS HERE:
        'Go through each server in the dataview and report the tips.
        Static intDVCounter As Integer = 0
        Do Until intDVCounter = m_dvServersDataView.Count
            'There will be two servers printed per page
            'FIRST SERVER BEGINS HERE:
            Dim strServerNumber As String = m_dvServersDataView.Item(intDVCounter)("ServerNumber").ToString
            Dim strFirstName As String = m_dvServersDataView.Item(intDVCounter)("FirstName").ToString
            Dim strLastName As String = m_dvServersDataView.Item(intDVCounter)("LastName").ToString

            Dim dtePeriodStart As Date = CDate(Me.m_dsParentDataset.Settings.FindBySetting("PeriodStart")("Value"))
            Dim dtePeriodEnd As Date = CDate(Me.m_dsParentDataset.Settings.FindBySetting("PeriodEnd")("Value"))

            Dim dteTempDate As Date = dtePeriodStart

            e.Graphics.DrawString(strServerNumber & "  " & strLastName & ", " & strFirstName, _
                fontBold, Brushes.Black, marginLeft, position)

            position += intExtraLineSpacing

            Dim intStrLen As Integer

            'Draw the room charge column header.
            intStrLen = CInt(e.Graphics.MeasureString("ROOM CHARGE", fontBold, New SizeF(intPrintAreaWidth, _
                intPrintAreaHeight), fmt, Len("ROOM CHARGE"), 1).Width)

            e.Graphics.DrawString("ROOM CHARGE", fontBold, Brushes.Black, intRoomChargePos - intStrLen, position)

            'Draw the credit card column header.
            intStrLen = CInt(e.Graphics.MeasureString("CREDIT CARD", fontBold, New SizeF(intPrintAreaWidth, _
               intPrintAreaHeight), fmt, Len("CREDIT CARD"), 1).Width)

            e.Graphics.DrawString("CREDIT CARD", fontBold, Brushes.Black, intCreditCardPos - intStrLen, position)

            'Draw the daily total column header.
            intStrLen = CInt(e.Graphics.MeasureString("DAILY TOTAL", fontBold, New SizeF(intPrintAreaWidth, _
                           intPrintAreaHeight), fmt, Len("DAILY TOTAL"), 1).Width)

            e.Graphics.DrawString("DAILY TOTAL", fontBold, Brushes.Black, intDailyPos - intStrLen, position)

            position += intLineSpacing

            Dim decRCGrand As Decimal = 0
            Dim decCCGrand As Decimal = 0

            Dim tempDV As New DataView

            'Loop through each date in the period and total the charge tips for the day.
            Do Until dteTempDate = DateAdd(DateInterval.Day, 1, dtePeriodEnd)
                'Print the date to the page.
                e.Graphics.DrawString(Format(dteTempDate, "MM/dd/yyyy"), font, Brushes.Black, _
                    intDatePos, position)

                tempDV.Table = Me.m_dsParentDataset.Tips
                tempDV.RowFilter = "ServerNumber = '" & strServerNumber & _
                    "' AND Description = 'Credit Card' AND WorkingDate = '" & Format(dteTempDate, "M/d/yy") & "'"

                'Calculate the daily total for the charge tips.
                Dim decCCTotal As Decimal = 0
                If tempDV.Count <> 0 Then
                    For intTempDVCounter As Integer = 0 To tempDV.Count - 1
                        decCCTotal += CDec(tempDV.Item(intTempDVCounter)("Amount"))
                    Next
                End If

                tempDV.RowFilter = "ServerNumber = '" & strServerNumber & _
                    "' AND Description = 'Room Charge' AND WorkingDate = '" & Format(dteTempDate, "M/d/yy") & "'"

                'Calculate the daily total for the charge tips.
                Dim decRCTotal As Decimal = 0
                If tempDV.Count <> 0 Then
                    For intTempDVCounter As Integer = 0 To tempDV.Count - 1
                        decRCTotal += CDec(tempDV.Item(intTempDVCounter)("Amount"))
                    Next
                End If

                'Draw the room charge daily total to the page.
                intStrLen = CInt(e.Graphics.MeasureString(Format(decRCTotal, "0.00"), font, _
                    New SizeF(intPrintAreaWidth, intPrintAreaHeight), fmt, _
                    Len(Format(decRCTotal, "0.00")), 1).Width)

                e.Graphics.DrawString(Format(decRCTotal, "0.00"), font, Brushes.Black, _
                    intRoomChargePos - intStrLen, position)

                'Draw the credit card daily total to the page.
                intStrLen = CInt(e.Graphics.MeasureString(Format(decCCTotal, "0.00"), font, _
                                   New SizeF(intPrintAreaWidth, intPrintAreaHeight), fmt, _
                                   Len(Format(decCCTotal, "0.00")), 1).Width)

                e.Graphics.DrawString(Format(decCCTotal, "0.00"), font, Brushes.Black, _
                    intCreditCardPos - intStrLen, position)

                'Draw the charge tip daily total to the page.
                intStrLen = CInt(e.Graphics.MeasureString(Format(decRCTotal + decCCTotal, "0.00"), font, _
                   New SizeF(intPrintAreaWidth, intPrintAreaHeight), fmt, _
                   Len(Format(decRCTotal + decCCTotal, "0.00")), 1).Width)

                e.Graphics.DrawString(Format(decRCTotal + decCCTotal, "0.00"), font, Brushes.Black, _
                    intDailyPos - intStrLen, position)

                'Add the daily totals to the grand totals.
                decRCGrand += decRCTotal
                decCCGrand += decCCTotal

                position += intLineSpacing
                dteTempDate = DateAdd(DateInterval.Day, 1, dteTempDate)
            Loop

            position += intLineSpacing

            'Draw the charge tip totals to the page.
            e.Graphics.DrawString("TOTAL CHARGE TIPS", fontBold, Brushes.Black, marginLeft, position)

            'Room charge.
            intStrLen = CInt(e.Graphics.MeasureString(Format(decRCGrand, "0.00"), fontBold, New SizeF(intPrintAreaWidth, _
                intPrintAreaHeight), fmt, Len(Format(decRCGrand, "0.00")), 1).Width)

            e.Graphics.DrawString(Format(decRCGrand, "0.00"), fontBold, Brushes.Black, intRoomChargePos - intStrLen, _
                position)

            'Credit card.
            intStrLen = CInt(e.Graphics.MeasureString(Format(decCCGrand, "0.00"), fontBold, New SizeF(intPrintAreaWidth, _
                           intPrintAreaHeight), fmt, Len(Format(decCCGrand, "0.00")), 1).Width)

            e.Graphics.DrawString(Format(decCCGrand, "0.00"), fontBold, Brushes.Black, intCreditCardPos - intStrLen, _
                position)

            'Daily.
            intStrLen = CInt(e.Graphics.MeasureString(Format(decRCGrand + decCCGrand, "0.00"), fontBold, New SizeF(intPrintAreaWidth, _
                            intPrintAreaHeight), fmt, Len(Format(decRCGrand + decCCGrand, "0.00")), 1).Width)

            e.Graphics.DrawString(Format(decRCGrand + decCCGrand, "0.00"), fontBold, Brushes.Black, intDailyPos - intStrLen, _
                position)

            'Calculate the total for cash tips.
            tempDV = New DataView
            tempDV.Table = Me.m_dsParentDataset.Tips
            tempDV.RowFilter = "Description = 'Cash' AND ServerNumber = '" & strServerNumber & "'"

            position += intLineSpacing

            Dim decCashTotal As Decimal = 0
            If tempDV.Count <> 0 Then
                For i As Integer = 0 To tempDV.Count - 1
                    decCashTotal += CDec(tempDV.Item(i)("Amount"))
                Next
            End If

            'Draw the cash total to the page.
            e.Graphics.DrawString("Cash Tips Claimed", font, Brushes.Black, marginLeft, position)

            intStrLen = CInt(e.Graphics.MeasureString(Format(decCashTotal, "0.00"), font, New SizeF(intPrintAreaWidth, _
                intPrintAreaHeight), fmt, Len(Format(decCashTotal, "0.00")), 1).Width)

            e.Graphics.DrawString(Format(decCashTotal, "0.00"), font, Brushes.Black, intDailyPos - intStrLen, position)

            position += intLineSpacing

            'Calculate the total for special function tips.
            tempDV = New DataView
            tempDV.Table = Me.m_dsParentDataset.Tips
            tempDV.RowFilter = "Description = 'Special Function' AND ServerNumber = '" & strServerNumber & "'"

            Dim decSFTotal As Decimal = 0
            If tempDV.Count <> 0 Then
                For i As Integer = 0 To tempDV.Count - 1
                    decSFTotal += CDec(tempDV.Item(i)("Amount"))
                Next
            End If

            'Draw the special function total to the page.
            e.Graphics.DrawString("Special Functions Tips", font, Brushes.Black, marginLeft, position)

            intStrLen = CInt(e.Graphics.MeasureString(Format(decSFTotal, "0.00"), font, New SizeF(intPrintAreaWidth, _
                intPrintAreaHeight), fmt, Len(Format(decSFTotal, "0.00")), 1).Width)

            e.Graphics.DrawString(Format(decSFTotal, "0.00"), font, Brushes.Black, intDailyPos - intStrLen, position)

            position += intLineSpacing

            'Draw the grand total to the page.
            e.Graphics.DrawString("TOTAL TIPS FOR PAY PERIOD", fontBold, Brushes.Black, marginLeft, position)

            intStrLen = CInt(e.Graphics.MeasureString(Format(decRCGrand + decCCGrand + decCashTotal + decSFTotal, "0.00"), fontBold, New SizeF(intPrintAreaWidth, _
                           intPrintAreaHeight), fmt, Len(Format(decRCGrand + decCCGrand + decCashTotal + decSFTotal, "0.00")), 1).Width)

            e.Graphics.DrawString(Format(decRCGrand + decCCGrand + decCashTotal + decSFTotal, "0.00"), fontBold, Brushes.Black, intDailyPos - intStrLen, position)

            intDVCounter += 1

            If intDVCounter = m_dvServersDataView.Count Then
                Exit Do
            End If

            'SECOND SERVER BEGINS HERE:
            position = 600

            strServerNumber = m_dvServersDataView.Item(intDVCounter)("ServerNumber").ToString
            strFirstName = m_dvServersDataView.Item(intDVCounter)("FirstName").ToString
            strLastName = m_dvServersDataView.Item(intDVCounter)("LastName").ToString

            dtePeriodStart = CDate(Me.m_dsParentDataset.Settings.FindBySetting("PeriodStart")("Value"))
            dtePeriodEnd = CDate(Me.m_dsParentDataset.Settings.FindBySetting("PeriodEnd")("Value"))

            dteTempDate = dtePeriodStart

            e.Graphics.DrawString(strServerNumber & "  " & strLastName & ", " & strFirstName, _
                fontBold, Brushes.Black, marginLeft, position)

            position += intExtraLineSpacing

            'Draw the room charge column header.
            intStrLen = CInt(e.Graphics.MeasureString("ROOM CHARGE", fontBold, New SizeF(intPrintAreaWidth, _
                intPrintAreaHeight), fmt, Len("ROOM CHARGE"), 1).Width)

            e.Graphics.DrawString("ROOM CHARGE", fontBold, Brushes.Black, intRoomChargePos - intStrLen, position)

            'Draw the credit card column header.
            intStrLen = CInt(e.Graphics.MeasureString("CREDIT CARD", fontBold, New SizeF(intPrintAreaWidth, _
               intPrintAreaHeight), fmt, Len("CREDIT CARD"), 1).Width)

            e.Graphics.DrawString("CREDIT CARD", fontBold, Brushes.Black, intCreditCardPos - intStrLen, position)

            'Draw the daily total column header.
            intStrLen = CInt(e.Graphics.MeasureString("DAILY TOTAL", fontBold, New SizeF(intPrintAreaWidth, _
                           intPrintAreaHeight), fmt, Len("DAILY TOTAL"), 1).Width)

            e.Graphics.DrawString("DAILY TOTAL", fontBold, Brushes.Black, intDailyPos - intStrLen, position)

            position += intLineSpacing

            decRCGrand = 0
            decCCGrand = 0

            tempDV = New DataView

            'Loop through each date in the period and total the charge tips for the day.
            Do Until dteTempDate = DateAdd(DateInterval.Day, 1, dtePeriodEnd)
                'Print the date to the page.
                e.Graphics.DrawString(Format(dteTempDate, "MM/dd/yyyy"), font, Brushes.Black, _
                    intDatePos, position)

                tempDV.Table = Me.m_dsParentDataset.Tips
                tempDV.RowFilter = "ServerNumber = '" & strServerNumber & _
                    "' AND Description = 'Credit Card' AND WorkingDate = '" & Format(dteTempDate, "M/d/yy") & "'"

                'Calculate the daily total for the charge tips.
                Dim decCCTotal As Decimal = 0
                If tempDV.Count <> 0 Then
                    For intTempDVCounter As Integer = 0 To tempDV.Count - 1
                        decCCTotal += CDec(tempDV.Item(intTempDVCounter)("Amount"))
                    Next
                End If

                tempDV.RowFilter = "ServerNumber = '" & strServerNumber & _
                    "' AND Description = 'Room Charge' AND WorkingDate = '" & Format(dteTempDate, "M/d/yy") & "'"

                'Calculate the daily total for the charge tips.
                Dim decRCTotal As Decimal = 0
                If tempDV.Count <> 0 Then
                    For intTempDVCounter As Integer = 0 To tempDV.Count - 1
                        decRCTotal += CDec(tempDV.Item(intTempDVCounter)("Amount"))
                    Next
                End If

                'Draw the room charge daily total to the page.
                intStrLen = CInt(e.Graphics.MeasureString(Format(decRCTotal, "0.00"), font, _
                    New SizeF(intPrintAreaWidth, intPrintAreaHeight), fmt, _
                    Len(Format(decRCTotal, "0.00")), 1).Width)

                e.Graphics.DrawString(Format(decRCTotal, "0.00"), font, Brushes.Black, _
                    intRoomChargePos - intStrLen, position)

                'Draw the credit card daily total to the page.
                intStrLen = CInt(e.Graphics.MeasureString(Format(decCCTotal, "0.00"), font, _
                                   New SizeF(intPrintAreaWidth, intPrintAreaHeight), fmt, _
                                   Len(Format(decCCTotal, "0.00")), 1).Width)

                e.Graphics.DrawString(Format(decCCTotal, "0.00"), font, Brushes.Black, _
                    intCreditCardPos - intStrLen, position)

                'Draw the charge tip daily total to the page.
                intStrLen = CInt(e.Graphics.MeasureString(Format(decRCTotal + decCCTotal, "0.00"), font, _
                   New SizeF(intPrintAreaWidth, intPrintAreaHeight), fmt, _
                   Len(Format(decRCTotal + decCCTotal, "0.00")), 1).Width)

                e.Graphics.DrawString(Format(decRCTotal + decCCTotal, "0.00"), font, Brushes.Black, _
                    intDailyPos - intStrLen, position)

                'Add the daily totals to the grand totals.
                decRCGrand += decRCTotal
                decCCGrand += decCCTotal

                position += intLineSpacing
                dteTempDate = DateAdd(DateInterval.Day, 1, dteTempDate)
            Loop

            position += intLineSpacing

            'Draw the charge tip totals to the page.
            e.Graphics.DrawString("TOTAL CHARGE TIPS", fontBold, Brushes.Black, marginLeft, position)

            'Room charge.
            intStrLen = CInt(e.Graphics.MeasureString(Format(decRCGrand, "0.00"), fontBold, New SizeF(intPrintAreaWidth, _
                intPrintAreaHeight), fmt, Len(Format(decRCGrand, "0.00")), 1).Width)

            e.Graphics.DrawString(Format(decRCGrand, "0.00"), fontBold, Brushes.Black, intRoomChargePos - intStrLen, _
                position)

            'Credit card.
            intStrLen = CInt(e.Graphics.MeasureString(Format(decCCGrand, "0.00"), fontBold, New SizeF(intPrintAreaWidth, _
                           intPrintAreaHeight), fmt, Len(Format(decCCGrand, "0.00")), 1).Width)

            e.Graphics.DrawString(Format(decCCGrand, "0.00"), fontBold, Brushes.Black, intCreditCardPos - intStrLen, _
                position)

            'Daily.
            intStrLen = CInt(e.Graphics.MeasureString(Format(decRCGrand + decCCGrand, "0.00"), fontBold, New SizeF(intPrintAreaWidth, _
                            intPrintAreaHeight), fmt, Len(Format(decRCGrand + decCCGrand, "0.00")), 1).Width)

            e.Graphics.DrawString(Format(decRCGrand + decCCGrand, "0.00"), fontBold, Brushes.Black, intDailyPos - intStrLen, _
                position)

            'Calculate the total for cash tips.
            tempDV = New DataView
            tempDV.Table = Me.m_dsParentDataset.Tips
            tempDV.RowFilter = "Description = 'Cash' AND ServerNumber = '" & strServerNumber & "'"

            position += intLineSpacing

            decCashTotal = 0
            If tempDV.Count <> 0 Then
                For i As Integer = 0 To tempDV.Count - 1
                    decCashTotal += CDec(tempDV.Item(i)("Amount"))
                Next
            End If

            'Draw the cash total to the page.
            e.Graphics.DrawString("Cash Tips Claimed", font, Brushes.Black, marginLeft, position)

            intStrLen = CInt(e.Graphics.MeasureString(Format(decCashTotal, "0.00"), font, New SizeF(intPrintAreaWidth, _
                intPrintAreaHeight), fmt, Len(Format(decCashTotal, "0.00")), 1).Width)

            e.Graphics.DrawString(Format(decCashTotal, "0.00"), font, Brushes.Black, intDailyPos - intStrLen, position)

            position += intLineSpacing

            'Calculate the total for special function tips.
            tempDV = New DataView
            tempDV.Table = Me.m_dsParentDataset.Tips
            tempDV.RowFilter = "Description = 'Special Function' AND ServerNumber = '" & strServerNumber & "'"

            decSFTotal = 0
            If tempDV.Count <> 0 Then
                For i As Integer = 0 To tempDV.Count - 1
                    decSFTotal += CDec(tempDV.Item(i)("Amount"))
                Next
            End If

            'Draw the special function total to the page.
            e.Graphics.DrawString("Special Functions Tips", font, Brushes.Black, marginLeft, position)

            intStrLen = CInt(e.Graphics.MeasureString(Format(decSFTotal, "0.00"), font, New SizeF(intPrintAreaWidth, _
                intPrintAreaHeight), fmt, Len(Format(decSFTotal, "0.00")), 1).Width)

            e.Graphics.DrawString(Format(decSFTotal, "0.00"), font, Brushes.Black, intDailyPos - intStrLen, position)

            position += intLineSpacing

            'Draw the grand total to the page.
            e.Graphics.DrawString("TOTAL TIPS FOR PAY PERIOD", fontBold, Brushes.Black, marginLeft, position)

            intStrLen = CInt(e.Graphics.MeasureString(Format(decRCGrand + decCCGrand + decCashTotal + decSFTotal, "0.00"), fontBold, New SizeF(intPrintAreaWidth, _
                           intPrintAreaHeight), fmt, Len(Format(decRCGrand + decCCGrand + decCashTotal + decSFTotal, "0.00")), 1).Width)

            e.Graphics.DrawString(Format(decRCGrand + decCCGrand + decCashTotal + decSFTotal, "0.00"), fontBold, Brushes.Black, intDailyPos - intStrLen, position)

            intDVCounter += 1

            If intDVCounter <> m_dvServersDataView.Count Then
                e.HasMorePages = True
                position = 50
                Exit Sub
            End If
        Loop

        e.HasMorePages = False
        position = 50
        intDVCounter = 0
    End Sub
End Class