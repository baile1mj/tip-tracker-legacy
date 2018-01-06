Public Class frmPrintTipReport
    Friend m_dsParentDataSet As New FileDataSet
    Private m_dvTips As New DataView

    Private Sub frmPrintTipReport_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
        m_dvTips.Table = m_dsParentDataSet.Tips
        m_dvTips.Sort = "LastName, FirstName"

        PopulateWorkingDates()
    End Sub

    Private Sub PopulateWorkingDates()
        Dim dtePeriodStart As Date = CDate(m_dsParentDataSet.Settings.FindBySetting("PeriodStart")("Value"))
        Dim dtePeriodEnd As Date = CDate(m_dsParentDataSet.Settings.FindBySetting("PeriodEnd")("Value"))
        Dim dteTemp As Date = dtePeriodStart

        Do Until dteTemp = DateAdd(DateInterval.Day, 1, dtePeriodEnd)
            cboWorkingDates.Items.Add(Format(dteTemp, "ddd M/dd/yy"))

            dteTemp = DateAdd(DateInterval.Day, 1, dteTemp)
        Loop
    End Sub

    Private Sub optAllDates_CheckedChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles optAllDates.CheckedChanged
        cboWorkingDates.Enabled = optSelectedDate.Checked
    End Sub

    Private Sub optSelectedDate_CheckedChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles optSelectedDate.CheckedChanged
        cboWorkingDates.Enabled = optSelectedDate.Checked
    End Sub

    Private Sub optAllTipTypes_CheckedChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles optAllTipTypes.CheckedChanged
        cboTipTypes.Enabled = optSelectedTipType.Checked
    End Sub

    Private Sub optSelectedTipType_CheckedChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles optSelectedTipType.CheckedChanged
        cboTipTypes.Enabled = optSelectedTipType.Checked
    End Sub

    Private Sub cboTipTypes_EnabledChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles cboTipTypes.EnabledChanged
        If cboTipTypes.Enabled = False Then cboTipTypes.SelectedIndex = -1
    End Sub

    Private Sub cboWorkingDates_EnabledChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles cboWorkingDates.EnabledChanged
        If cboWorkingDates.Enabled = False Then cboWorkingDates.SelectedIndex = -1
    End Sub

    Private Sub btnCancel_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnCancel.Click
        Me.Close()
    End Sub

    Private Sub btnPreview_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnPreview.Click
        If optSelectedDate.Checked = True And cboWorkingDates.SelectedIndex = -1 Then
            MessageBox.Show("You must select a working date.", "Select Working Date", MessageBoxButtons.OK)
            Exit Sub
        End If

        If optSelectedTipType.Checked = True And cboTipTypes.SelectedIndex = -1 Then
            MessageBox.Show("You must select a tip type.", "Select Tip Type", MessageBoxButtons.OK)
            Exit Sub
        End If

        If optSummary.Checked = True Then
            dlgPreview.Document = docDateSummary
        Else
            dlgPreview.Document = docDateDetail
        End If

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
            Exit Sub
        End Try

        Me.Close()
    End Sub

    Private Sub btnPrint_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnPrint.Click
        If optSelectedDate.Checked = True And cboWorkingDates.SelectedIndex = -1 Then
            MessageBox.Show("You must select a working date.", "Select Working Date", MessageBoxButtons.OK)
            Exit Sub
        End If

        If optSelectedTipType.Checked = True And cboTipTypes.SelectedIndex = -1 Then
            MessageBox.Show("You must select a tip type.", "Select Tip Type", MessageBoxButtons.OK)
            Exit Sub
        End If

        If optSummary.Checked = True Then
            dlgPrint.Document = docDateSummary
        Else
            dlgPrint.Document = docDateDetail
        End If

        Try
            Windows.Forms.Cursor.Current = Cursors.Default

            If dlgPrint.ShowDialog <> Windows.Forms.DialogResult.OK Then
                dlgPrint.Dispose()
                Me.Close()
                Exit Sub
            End If

            If optSummary.Checked = True Then
                docDateSummary.Print()
            Else
                docDateDetail.Print()
            End If

            dlgPrint.Dispose()
        Catch ex As Exception
            MessageBox.Show("Could not display the document.  Check that there is a printer installed and that a default printer has been selected.", "Cannot Preview Document", MessageBoxButtons.OK)
            Exit Sub
        End Try

        Me.Close()
    End Sub

    Private Sub docDateSummary_PrintPage(ByVal sender As System.Object, ByVal e As System.Drawing.Printing.PrintPageEventArgs) Handles docDateSummary.PrintPage
        Dim font As New System.Drawing.Font("Calibri", 10)
        Dim fontBold As New System.Drawing.Font("Calibri", 10, FontStyle.Bold)

        Dim fmt As New StringFormat(StringFormatFlags.LineLimit)

        'Initialize local static variables that contain the current line position and
        'the current page number.
        Static position As Single = 75
        Static intPageNumber As Integer = 1

        Const intLineSpacing As Integer = 20
        Const intExtraLineSpacing As Integer = 40

        Const intIndent As Integer = 50

        'Initialize local constants that contain the normal line spacing and padded 
        'line spacing (double spacing).
        Dim intPrintAreaHeight, intPrintAreaWidth, marginLeft, marginTop As Int32
        With docDateSummary.DefaultPageSettings
            .Margins.Top = 75
            .Margins.Bottom = 75
            .Margins.Left = 75
            .Margins.Right = 75

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

        'Draw the headers to the page.
        e.Graphics.DrawString("Tip Summary Report (By Date)", font, Brushes.Black, marginLeft, position)
        position += intLineSpacing

        e.Graphics.DrawString(Format(DateTime.Now, "g"), font, Brushes.Black, marginLeft, position)
        position += intLineSpacing

        e.Graphics.DrawString("Page " & intPageNumber, font, Brushes.Black, marginLeft, position)
        position += intExtraLineSpacing

        If optSelectedDate.Checked = True Then 'Prints the tips for only the selected date, ignoring selected card type.
            Dim dteSelectedDate As Date = CDate(cboWorkingDates.SelectedItem.ToString)

            m_dvTips.RowFilter = "WorkingDate = '" & Format(dteSelectedDate, "MM/dd/yyyy") & "'"

            Dim decCash As Decimal = 0
            Dim decCreditCard As Decimal = 0
            Dim decRoomCharge As Decimal = 0
            Dim decTotal As Decimal = 0

            For i As Integer = 0 To m_dvTips.Count - 1
                Select Case m_dvTips.Item(i)("Description").ToString
                    Case "Cash"
                        decCash += CDec(m_dvTips(i)("Amount"))
                    Case "Credit Card"
                        decCreditCard += CDec(m_dvTips(i)("Amount"))
                    Case "Room Charge"
                        decRoomCharge += CDec(m_dvTips(i)("Amount"))
                End Select
            Next

            Dim intStrLen As Integer = 0
            Dim strAmount As String = ""

            If position >= marginTop + intPrintAreaHeight Then
                e.HasMorePages = True
                position = 75
                intPageNumber += 1
                Exit Sub
            End If

            e.Graphics.DrawString(Format(dteSelectedDate, "MM/dd/yyyy"), fontBold, Brushes.Black, marginLeft, position)
            position += intLineSpacing

            'Cash
            decTotal += decCash
            e.Graphics.DrawString("Cash", font, Brushes.Black, marginLeft + intIndent, position)

            strAmount = Format(decCash, "0.00")
            intStrLen = CInt(e.Graphics.MeasureString(strAmount, font, New SizeF(intPrintAreaWidth, _
                intPrintAreaHeight), fmt, Len(strAmount), 1).Width)

            e.Graphics.DrawString(strAmount, font, Brushes.Black, marginLeft + intPrintAreaWidth - intStrLen, position)

            position += intLineSpacing

            'Credit Card
            decTotal += decCreditCard
            e.Graphics.DrawString("Credit Card", font, Brushes.Black, marginLeft + intIndent, position)

            strAmount = Format(decCreditCard, "0.00")
            intStrLen = CInt(e.Graphics.MeasureString(strAmount, font, New SizeF(intPrintAreaWidth, _
                intPrintAreaHeight), fmt, Len(strAmount), 1).Width)

            e.Graphics.DrawString(strAmount, font, Brushes.Black, marginLeft + intPrintAreaWidth - intStrLen, position)

            position += intLineSpacing

            'Room Charge
            decTotal += decRoomCharge
            e.Graphics.DrawString("Room Charge", font, Brushes.Black, marginLeft + intIndent, position)

            strAmount = Format(decRoomCharge, "0.00")
            intStrLen = CInt(e.Graphics.MeasureString(strAmount, font, New SizeF(intPrintAreaWidth, _
                intPrintAreaHeight), fmt, Len(strAmount), 1).Width)

            e.Graphics.DrawString(strAmount, font, Brushes.Black, marginLeft + intPrintAreaWidth - intStrLen, position)

            position += intLineSpacing

            'Total
            e.Graphics.DrawString("TOTAL TIPS FOR " & Format(dteSelectedDate, "MM/dd/yyyy"), fontBold, Brushes.Black, marginLeft, position)

            strAmount = Format(decTotal, "0.00")
            intStrLen = CInt(e.Graphics.MeasureString(strAmount, fontBold, New SizeF(intPrintAreaWidth, _
                intPrintAreaHeight), fmt, Len(strAmount), 1).Width)

            e.Graphics.DrawString(strAmount, fontBold, Brushes.Black, marginLeft + intPrintAreaWidth - intStrLen, position)

            position += intExtraLineSpacing
        Else 'Prints the tips for all the dates in the pay period, omitting types that aren't selected.
            Dim dtePeriodStart As Date = CDate(Me.m_dsParentDataSet.Settings.FindBySetting("PeriodStart")("Value"))
            Dim dtePeriodEnd As Date = CDate(Me.m_dsParentDataSet.Settings.FindBySetting("PeriodEnd")("Value"))

            Dim dteTemp As Date = dtePeriodStart
            Dim lstDatesInPeriod As New List(Of Date)

            Do Until dteTemp = DateAdd(DateInterval.Day, 1, dtePeriodEnd)
                lstDatesInPeriod.Add(dteTemp)
                dteTemp = DateAdd(DateInterval.Day, 1, dteTemp)
            Loop

            Static intDateCounter As Integer = 0
            Static decGrandTotal As Decimal = 0

            If lstDatesInPeriod.Count <> 0 Then
                Do Until intDateCounter = lstDatesInPeriod.Count

                    dteTemp = lstDatesInPeriod(intDateCounter)

                    If position >= marginTop + intPrintAreaHeight Then
                        e.HasMorePages = True
                        position = 75
                        intPageNumber += 1
                        Exit Sub
                    End If

                    e.Graphics.DrawString(Format(dteTemp, "MM/dd/yyyy"), fontBold, Brushes.Black, marginLeft, position)
                    position += intLineSpacing

                    Dim strFilterString As String = "WorkingDate = '" & Format(dteTemp, "MM/dd/yyyy") & "'"
                    m_dvTips.RowFilter = strFilterString

                    Static intTypeCounter As Integer = 1

                    Const intCash As Integer = 1
                    Const intCreditCard As Integer = 2
                    Const intRoomCharge As Integer = 3
                    Const intTotal As Integer = 4

                    Dim intStrLen As Integer

                    Dim decCash As Decimal = 0
                    Dim decCreditCard As Decimal = 0
                    Dim decRoomCharge As Decimal = 0

                    Static decTotal As Decimal = 0

                    Do Until intTypeCounter = intTotal + 1
                        Select Case intTypeCounter
                            Case intCash
                                If optSelectedTipType.Checked = True Then
                                    If cboTipTypes.SelectedItem.ToString <> "Cash" Then
                                        Exit Select
                                    End If
                                End If

                                m_dvTips.RowFilter &= " AND Description = 'Cash'"

                                If m_dvTips.Count <> 0 Then
                                    For i As Integer = 0 To m_dvTips.Count - 1
                                        decCash += CDec(m_dvTips.Item(i)("Amount"))
                                    Next
                                End If

                                intStrLen = CInt(e.Graphics.MeasureString(Format(decCash, "0.00"), font, New SizeF(intPrintAreaWidth, intPrintAreaHeight), fmt, Len(Format(decCash, "0.00")), 1).Width)

                                If position >= marginTop + intPrintAreaHeight Then
                                    e.HasMorePages = True
                                    position = 75
                                    intPageNumber += 1
                                    Exit Sub
                                End If

                                e.Graphics.DrawString("Cash", font, Brushes.Black, marginLeft + intIndent, position)
                                e.Graphics.DrawString(Format(decCash, "0.00"), font, Brushes.Black, marginLeft + intPrintAreaWidth - intStrLen, position)
                                position += intLineSpacing

                                decTotal += decCash

                                m_dvTips.RowFilter = strFilterString

                            Case intCreditCard
                                If optSelectedTipType.Checked = True Then
                                    If cboTipTypes.SelectedItem.ToString <> "Credit Card" Then
                                        Exit Select
                                    End If
                                End If

                                m_dvTips.RowFilter &= " AND Description = 'Credit Card'"

                                If m_dvTips.Count <> 0 Then
                                    For i As Integer = 0 To m_dvTips.Count - 1
                                        decCreditCard += CDec(m_dvTips.Item(i)("Amount"))
                                    Next
                                End If

                                intStrLen = CInt(e.Graphics.MeasureString(Format(decCreditCard, "0.00"), font, New SizeF(intPrintAreaWidth, intPrintAreaHeight), fmt, Len(Format(decCreditCard, "0.00")), 1).Width)

                                If position >= marginTop + intPrintAreaHeight Then
                                    e.HasMorePages = True
                                    position = 75
                                    intPageNumber += 1
                                    Exit Sub
                                End If

                                e.Graphics.DrawString("Credit Card", font, Brushes.Black, marginLeft + intIndent, position)
                                e.Graphics.DrawString(Format(decCreditCard, "0.00"), font, Brushes.Black, marginLeft + intPrintAreaWidth - intStrLen, position)
                                position += intLineSpacing

                                decTotal += decCreditCard

                                m_dvTips.RowFilter = strFilterString

                            Case intRoomCharge
                                If optSelectedTipType.Checked = True Then
                                    If cboTipTypes.SelectedItem.ToString <> "Room Charge" Then
                                        Exit Select
                                    End If
                                End If

                                m_dvTips.RowFilter &= " AND Description = 'Room Charge'"

                                If m_dvTips.Count <> 0 Then
                                    For i As Integer = 0 To m_dvTips.Count - 1
                                        decRoomCharge += CDec(m_dvTips.Item(i)("Amount"))
                                    Next
                                End If

                                intStrLen = CInt(e.Graphics.MeasureString(Format(decRoomCharge, "0.00"), font, New SizeF(intPrintAreaWidth, intPrintAreaHeight), fmt, Len(Format(decRoomCharge, "0.00")), 1).Width)

                                If position >= marginTop + intPrintAreaHeight Then
                                    e.HasMorePages = True
                                    position = 75
                                    intPageNumber += 1
                                    Exit Sub
                                End If

                                e.Graphics.DrawString("Room Charge", font, Brushes.Black, marginLeft + intIndent, position)
                                e.Graphics.DrawString(Format(decRoomCharge, "0.00"), font, Brushes.Black, marginLeft + intPrintAreaWidth - intStrLen, position)
                                position += intLineSpacing

                                decTotal += decRoomCharge

                                m_dvTips.RowFilter = strFilterString

                            Case intTotal
                                intStrLen = CInt(e.Graphics.MeasureString(Format(decTotal, "0.00"), font, New SizeF(intPrintAreaWidth, intPrintAreaHeight), fmt, Len(Format(decTotal, "0.00")), 1).Width)

                                If position >= marginTop + intPrintAreaHeight Then
                                    e.HasMorePages = True
                                    position = 75
                                    intPageNumber += 1
                                    Exit Sub
                                End If

                                e.Graphics.DrawString("TOTAL FOR " & Format(dteTemp, "MM/dd/yyyy"), fontBold, Brushes.Black, marginLeft + intIndent, position)
                                e.Graphics.DrawString(Format(decTotal, "0.00"), fontBold, Brushes.Black, marginLeft + intPrintAreaWidth - intStrLen, position)

                                position += intExtraLineSpacing

                                decGrandTotal += decTotal
                                decTotal = 0

                            Case Else
                                MsgBox("You shouldn't see this message.  Tell the programmer to fix his code.")
                        End Select
                        intTypeCounter += 1
                    Loop
                    intTypeCounter = 1
                    intDateCounter += 1
                Loop

                e.Graphics.DrawString("TOTAL FOR ALL DATES", fontBold, Brushes.Black, marginLeft, position)

                Dim intLen As Integer = CInt(e.Graphics.MeasureString(Format(decGrandTotal, "0.00"), fontBold, _
                    New SizeF(intPrintAreaWidth, intPrintAreaHeight), fmt, Len(Format(decGrandTotal, "0.00")), 1).Width)

                e.Graphics.DrawString(Format(decGrandTotal, "0.00"), fontBold, Brushes.Black, marginLeft + intPrintAreaWidth - intLen, position)

                intDateCounter = 0
                decGrandTotal = 0
            End If
        End If

        e.HasMorePages = False
        position = 75
        intPageNumber = 1
    End Sub

    Private Sub docDateDetail_PrintPage(ByVal sender As System.Object, ByVal e As System.Drawing.Printing.PrintPageEventArgs) Handles docDateDetail.PrintPage
        Dim font As New System.Drawing.Font("Calibri", 10)
        Dim fontBold As New System.Drawing.Font("Calibri", 10, FontStyle.Bold)

        Dim fmt As New StringFormat(StringFormatFlags.LineLimit)

        'Initialize local static variables that contain the current line position and
        'the current page number.
        Static position As Single = 75
        Static intPageNumber As Integer = 1

        Const intLineSpacing As Integer = 20
        Const intExtraLineSpacing As Integer = 40

        Const intDescriptionPos As Integer = 50
        Const intNamePos As Integer = 150

        'Initialize local constants that contain the normal line spacing and padded 
        'line spacing (double spacing).
        Dim intPrintAreaHeight, intPrintAreaWidth, marginLeft, marginTop As Int32
        With docDateSummary.DefaultPageSettings
            .Margins.Top = 75
            .Margins.Bottom = 75
            .Margins.Left = 75
            .Margins.Right = 75

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

        'Draw the headers to the page.
        e.Graphics.DrawString("Tip Detail Report (By Date)", font, Brushes.Black, marginLeft, position)
        position += intLineSpacing

        e.Graphics.DrawString(Format(DateTime.Now, "g"), font, Brushes.Black, marginLeft, position)
        position += intLineSpacing

        e.Graphics.DrawString("Page " & intPageNumber, font, Brushes.Black, marginLeft, position)
        position += intExtraLineSpacing

        'Sort the data view by date.
        m_dvTips.Sort = "WorkingDate, Description, SpecialFunction, LastName, FirstName"

        If optSelectedDate.Checked = True Then 'Prints the tips for only the selected date, ignoring selected card type.
            Dim dteSelectedDate As Date = CDate(cboWorkingDates.SelectedItem.ToString)

            m_dvTips.RowFilter = "WorkingDate = '" & Format(dteSelectedDate, "MM/dd/yyyy") & "'"

            e.Graphics.DrawString(Format(dteSelectedDate, "MM/dd/yyyy"), fontBold, Brushes.Black, marginLeft, position)
            position += intLineSpacing

            Static intTipCounter As Integer = 0
            Static decTipTotal As Decimal = 0

            If m_dvTips.Count <> 0 Then
                Do Until intTipCounter = m_dvTips.Count
                    Dim strServerNumber As String = m_dvTips.Item(intTipCounter)("ServerNumber").ToString
                    Dim strFirstName As String = m_dvTips.Item(intTipCounter)("FirstName").ToString
                    Dim strLastName As String = m_dvTips.Item(intTipCounter)("LastName").ToString
                    Dim strDescription As String = m_dvTips.Item(intTipCounter)("Description").ToString
                    Dim decAmount As Decimal = CDec(m_dvTips.Item(intTipCounter)("Amount"))

                    Dim strSelectedType As String = ""

                    If optSelectedTipType.Checked = True Then
                        strSelectedType = cboTipTypes.SelectedItem.ToString
                    End If

                    If strDescription <> "Special Function" Then
                        If optSelectedTipType.Checked = False Or (optSelectedTipType.Checked = True And strSelectedType = strDescription) Then
                            If position >= marginTop + intPrintAreaHeight Then
                                e.HasMorePages = True
                                position = 75
                                intPageNumber += 1
                                Exit Sub
                            End If

                            e.Graphics.DrawString(strDescription, font, Brushes.Black, marginLeft + intDescriptionPos, position)

                            e.Graphics.DrawString(strLastName & ", " & strFirstName & _
                                " ", font, Brushes.Black, marginLeft + +intDescriptionPos + intNamePos, position)

                            Dim intStrLen As Integer = CInt(e.Graphics.MeasureString(Format(decAmount, "0.00"), font, _
                                New SizeF(intPrintAreaWidth, intPrintAreaHeight), fmt, Len(Format(decAmount, "0.00")), _
                                1).Width)

                            e.Graphics.DrawString(Format(decAmount, "0.00"), font, Brushes.Black, _
                                marginLeft + intPrintAreaWidth - intStrLen, position)

                            position += intLineSpacing

                            decTipTotal += decAmount
                        End If
                    End If

                    intTipCounter += 1
                Loop
            End If

            Dim intLen As Integer

            If position >= marginTop + intPrintAreaHeight Then
                e.HasMorePages = True
                position = 75
                intPageNumber += 1
                Exit Sub
            End If

            e.Graphics.DrawString("TOTAL TIPS FOR " & Format(dteSelectedDate, "M/d/yyyy"), fontBold, _
                               Brushes.Black, marginLeft + intDescriptionPos, position)

            intLen = CInt(e.Graphics.MeasureString(Format(decTipTotal, "0.00"), fontBold, _
                New SizeF(intPrintAreaWidth, intPrintAreaHeight), fmt, Len(Format(decTipTotal, "0.00")), _
                1).Width)

            e.Graphics.DrawString(Format(decTipTotal, "0.00"), fontBold, Brushes.Black, _
                marginLeft + intPrintAreaWidth - intLen, position)

            position += intLineSpacing

            decTipTotal = 0
            intTipCounter = 0

        Else 'Prints the tips for all the dates in the pay period, omitting types that aren't selected.
            Dim dtePeriodStart As Date = CDate(Me.m_dsParentDataSet.Settings.FindBySetting("PeriodStart")("Value"))
            Dim dtePeriodEnd As Date = CDate(Me.m_dsParentDataSet.Settings.FindBySetting("PeriodEnd")("Value"))

            Dim dteTemp As Date = dtePeriodStart
            Dim lstDatesInPeriod As New List(Of Date)

            Do Until dteTemp = DateAdd(DateInterval.Day, 1, dtePeriodEnd)
                lstDatesInPeriod.Add(dteTemp)
                dteTemp = DateAdd(DateInterval.Day, 1, dteTemp)
            Loop


            Static intDateCounter As Integer = 0
            Static decGrandTotal As Decimal = 0

            If lstDatesInPeriod.Count <> 0 Then
                Do Until intDateCounter = lstDatesInPeriod.Count - 1

                    dteTemp = lstDatesInPeriod(intDateCounter)

                    m_dvTips.RowFilter = "WorkingDate = '" & Format(dteTemp, "MM/dd/yyyy") & "'"

                    If position >= marginTop + intPrintAreaHeight Then
                        e.HasMorePages = True
                        position = 75
                        intPageNumber += 1
                        Exit Sub
                    End If

                    e.Graphics.DrawString(Format(dteTemp, "MM/dd/yyyy"), fontBold, Brushes.Black, marginLeft, position)
                    position += intLineSpacing

                    Static intCounter As Integer = 0
                    Static decTotal As Decimal = 0

                    Do Until intCounter = m_dvTips.Count
                        Dim strServerNumber As String = m_dvTips.Item(intCounter)("ServerNumber").ToString
                        Dim strFirstName As String = m_dvTips.Item(intCounter)("FirstName").ToString
                        Dim strLastName As String = m_dvTips.Item(intCounter)("LastName").ToString
                        Dim strDescription As String = m_dvTips.Item(intCounter)("Description").ToString
                        Dim decAmount As Decimal = CDec(m_dvTips.Item(intCounter)("Amount"))

                        Dim strSelectedType As String = ""

                        If optSelectedTipType.Checked = True Then
                            strSelectedType = cboTipTypes.SelectedItem.ToString
                        End If

                        If strDescription <> "Special Function" Then
                            If optSelectedTipType.Checked = False Or (optSelectedTipType.Checked = True And strSelectedType = strDescription) Then
                                If position >= marginTop + intPrintAreaHeight Then
                                    e.HasMorePages = True
                                    position = 75
                                    intPageNumber += 1
                                    Exit Sub
                                End If

                                e.Graphics.DrawString(strDescription, font, Brushes.Black, marginLeft + intDescriptionPos, position)

                                e.Graphics.DrawString(strLastName & ", " & strFirstName & _
                                    " ", font, Brushes.Black, marginLeft + +intDescriptionPos + intNamePos, position)

                                Dim intStrLen As Integer = CInt(e.Graphics.MeasureString(Format(decAmount, "0.00"), font, _
                                    New SizeF(intPrintAreaWidth, intPrintAreaHeight), fmt, Len(Format(decAmount, "0.00")), _
                                    1).Width)

                                e.Graphics.DrawString(Format(decAmount, "0.00"), font, Brushes.Black, _
                                    marginLeft + intPrintAreaWidth - intStrLen, position)

                                position += intLineSpacing

                                decTotal += decAmount
                            End If
                        End If

                        intCounter += 1
                    Loop

                    Dim intAmountLen As Integer

                    If position >= marginTop + intPrintAreaHeight Then
                        e.HasMorePages = True
                        position = 75
                        intPageNumber += 1
                        Exit Sub
                    End If

                    e.Graphics.DrawString("TOTAL TIPS FOR " & Format(dteTemp, "M/d/yyyy"), fontBold, _
                        Brushes.Black, marginLeft + intDescriptionPos, position)

                    intAmountLen = CInt(e.Graphics.MeasureString(Format(decTotal, "0.00"), fontBold, _
                        New SizeF(intPrintAreaWidth, intPrintAreaHeight), fmt, Len(Format(decTotal, "0.00")), _
                        1).Width)

                    e.Graphics.DrawString(Format(decTotal, "0.00"), fontBold, Brushes.Black, _
                        marginLeft + intPrintAreaWidth - intAmountLen, position)

                    decGrandTotal += decTotal

                    position += intExtraLineSpacing

                    decTotal = 0
                    intCounter = 0
                    intDateCounter += 1
                Loop

                If position >= marginTop + intPrintAreaHeight Then
                    e.HasMorePages = True
                    position = 75
                    intPageNumber += 1
                    Exit Sub
                End If

                e.Graphics.DrawString("TOTAL FOR ALL DATES", fontBold, Brushes.Black, marginLeft, position)

                Dim intLen As Integer = CInt(e.Graphics.MeasureString(Format(decGrandTotal, "0.00"), fontBold, _
                    New SizeF(intPrintAreaWidth, intPrintAreaHeight), fmt, Len(Format(decGrandTotal, "0.00")), 1).Width)

                e.Graphics.DrawString(Format(decGrandTotal, "0.00"), fontBold, Brushes.Black, marginLeft + intPrintAreaWidth - intLen, position)

                decGrandTotal = 0
            End If
        End If

        e.HasMorePages = False
        position = 75
        intPageNumber = 1
    End Sub

End Class