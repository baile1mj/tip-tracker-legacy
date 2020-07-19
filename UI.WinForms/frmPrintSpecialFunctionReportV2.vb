Imports System.Drawing.Printing
Imports TipTracker.Common.Data.PayPeriod

Public Class frmPrintSpecialFunctionReportV2
    'Since there are multiple instances of frmEnterTips, a reference to the dataset
    'of the form that called this instance of frmPrintTipReports needs to be created.
    Friend m_dsParentDataSet As New FileDataSet

    Private Sub frmPrintSpecialFunctionReportV2_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
        PopulateComboBox()
    End Sub

    Private Sub PopulateComboBox()
        cboFunctions.Items.Clear()

        Dim dvFunctions As New DataView
        dvFunctions.Table = Me.m_dsParentDataSet.SpecialFunctions

        Dim i As Integer = 0

        Do Until i = dvFunctions.Count
            Dim strFunction As String = dvFunctions.Item(i)("SpecialFunction").ToString

            cboFunctions.Items.Add(strFunction)
            i += 1
        Loop

        cboFunctions.Sorted = True
    End Sub

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

        Dim docReport As New PrintDocument

        docReport.DocumentName = "Function Report"
        With docReport.DefaultPageSettings.Margins
            .Top = 75
            .Bottom = 75
            .Left = 75
            .Right = 75
        End With

        If optFunctionName.Checked = True Then
            AddHandler docReport.PrintPage, AddressOf PrintByName
        Else
            AddHandler docReport.PrintPage, AddressOf PrintByDate
        End If

        Dim dlgPrint As New PrintDialog
        dlgPrint.Document = docReport

        Try
            If dlgPrint.ShowDialog <> Windows.Forms.DialogResult.OK Then
                dlgPrint.Dispose()
                docReport = Nothing
                Exit Sub
            End If

            docReport.Print()
        Catch ex As Exception
            MessageBox.Show("Could not display the document.  Check that there is a printer installed and that a default printer has been selected.", "Cannot Preview Document", MessageBoxButtons.OK)
        End Try

        docReport = Nothing
    End Sub

    Private Sub btnPreview_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnPreview.Click
        If optSelectedFunction.Checked = True And cboFunctions.SelectedIndex = -1 Then
            MessageBox.Show("You must select a function.", "Select Function", MessageBoxButtons.OK)
            Exit Sub
        End If

        Dim docReport As New PrintDocument

        docReport.DocumentName = "Function Report"
        With docReport.DefaultPageSettings.Margins
            .Top = 75
            .Bottom = 75
            .Left = 75
            .Right = 75
        End With

        If optFunctionName.Checked = True Then
            AddHandler docReport.PrintPage, AddressOf PrintByName
        Else
            AddHandler docReport.PrintPage, AddressOf PrintByDate
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

    Private Sub PrintByName(ByVal sender As Object, ByVal e As PrintPageEventArgs)
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
        e.Graphics.DrawString("Special Function Detail By Name", font, Brushes.Black, marginLeft, intPosition)
        intPosition += intLineSpacing

        e.Graphics.DrawString(Format(DateTime.Now, "MM/dd/yyyy") & " " & Format(DateTime.Now, "t"), font, Brushes.Black, marginLeft, intPosition)
        intPosition += intLineSpacing

        If optAllFunctions.Checked = True Then
            e.Graphics.DrawString("All Functions", font, Brushes.Black, marginLeft, intPosition)
        Else
            e.Graphics.DrawString("Selected Function: " & cboFunctions.SelectedItem.ToString, font, Brushes.Black, marginLeft, intPosition)
        End If
        intPosition += intLineSpacing

        e.Graphics.DrawString("Page " & intPageNumber, font, Brushes.Black, marginLeft, intPosition)
        intPosition += intExtraLineSpacing

        'Create a dataview to hold each function's tips to be printed.  Loop through the dataview and print the tips
        'to the page.

        Dim dvFunctions As New DataView
        dvFunctions.Table = Me.m_dsParentDataSet.SpecialFunctions
        dvFunctions.Sort = "SpecialFunction"

        If optSelectedFunction.Checked = True Then
            dvFunctions.RowFilter = "SpecialFunction = '" & cboFunctions.SelectedItem.ToString & "'"
        End If

        Static intFunctionCounter As Integer = 0
        Static decFunctionTotal As Decimal = 0
        Static decGrandTotal As Decimal = 0

        Dim intStrLen As Integer

        Do Until intFunctionCounter = dvFunctions.Count
            Dim strFunction As String = dvFunctions.Item(intFunctionCounter)("SpecialFunction").ToString
            Dim strFunctionDate As String = Format(CDate(dvFunctions.Item(intFunctionCounter)("Date")), "MM/dd/yyyy")

            Dim dvTips As New DataView

            dvTips.Table = Me.m_dsParentDataSet.Tips
            dvTips.RowFilter = "SpecialFunction = '" & strFunction & "'"
            dvTips.Sort = "LastName, FirstName, WorkingDate, Amount"

            Static intTipCounter As Integer = 0
            Static blnFunctionPrinted As Boolean
            Static blnTotalPrinted As Boolean

            If blnFunctionPrinted = False Then
                If intPosition >= marginTop + intPrintAreaHeight Then
                    intPosition = marginTop
                    intPageNumber += 1
                    e.HasMorePages = True
                    Exit Sub
                End If

                e.Graphics.DrawString(strFunction & ";  " & strFunctionDate, fontBold, Brushes.Black, marginLeft, intPosition)
                intPosition += intLineSpacing
                blnFunctionPrinted = True
            End If

            Do Until intTipCounter = dvTips.Count
                Dim strServerNumber As String = dvTips.Item(intTipCounter)("ServerNumber").ToString
                Dim strFirstName As String = dvTips.Item(intTipCounter)("FirstName").ToString
                Dim strLastName As String = dvTips.Item(intTipCounter)("LastName").ToString
                Dim decAmount As Decimal = CDec(dvTips.Item(intTipCounter)("Amount"))

                If intPosition >= marginTop + intPrintAreaHeight Then
                    intPosition = marginTop
                    intPageNumber += 1
                    e.HasMorePages = True
                    Exit Sub
                End If

                Dim strAmount As String = Format(decAmount, "0.00")

                e.Graphics.DrawString(strServerNumber & "  " & strLastName & ", " & strFirstName, font, Brushes.Black, _
                    marginLeft, intPosition)

                intStrLen = CInt(e.Graphics.MeasureString(strAmount, font, New SizeF(intPrintAreaWidth, intPrintAreaHeight), _
                    fmt, Len(strAmount), 1).Width)

                e.Graphics.DrawString(strAmount, font, Brushes.Black, marginLeft + intPrintAreaWidth - intStrLen, intPosition)

                intPosition += intLineSpacing
                decFunctionTotal += decAmount
                intTipCounter += 1
            Loop

            If blnTotalPrinted = False Then
                If intPosition >= marginTop + intPrintAreaHeight Then
                    intPosition = marginTop
                    intPageNumber += 1
                    e.HasMorePages = True
                    Exit Sub
                End If

                Dim strFunctionTotal As String = Format(decFunctionTotal, "0.00")

                e.Graphics.DrawString("Total Tips for Function", fontBold, Brushes.Black, marginLeft, intPosition)

                intStrLen = CInt(e.Graphics.MeasureString(strFunctionTotal, fontBold, New SizeF(intPrintAreaWidth, intPrintAreaHeight), _
                                    fmt, Len(strFunctionTotal), 1).Width)

                e.Graphics.DrawString(strFunctionTotal, fontBold, Brushes.Black, marginLeft + intPrintAreaWidth - intStrLen, intPosition)

                decGrandTotal += decFunctionTotal
                intPosition += intExtraLineSpacing
                blnTotalPrinted = True
            End If

            blnFunctionPrinted = False
            blnTotalPrinted = False

            decFunctionTotal = 0

            intTipCounter = 0
            intFunctionCounter += 1
        Loop

        If optSelectedFunction.Checked = False Then
            If intPosition >= marginTop + intPrintAreaHeight Then
                intPosition = marginTop
                intPageNumber += 1
                e.HasMorePages = True
                Exit Sub
            End If

            Dim strGrandTotal As String = Format(decGrandTotal, "0.00")

            e.Graphics.DrawString("TOTAL FOR ALL FUNCTIONS", fontBold, Brushes.Black, marginLeft, intPosition)

            intStrLen = CInt(e.Graphics.MeasureString(strGrandTotal, fontBold, New SizeF(intPrintAreaWidth, intPrintAreaHeight), _
                                fmt, Len(strGrandTotal), 1).Width)

            e.Graphics.DrawString(strGrandTotal, fontBold, Brushes.Black, marginLeft + intPrintAreaWidth - intStrLen, intPosition)
        End If

        intFunctionCounter = 0
        decGrandTotal = 0

        e.HasMorePages = False
        intPageNumber = 1
        intPosition = marginTop
    End Sub

    Private Sub PrintByDate(ByVal sender As Object, ByVal e As PrintPageEventArgs)
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
        e.Graphics.DrawString("Special Function Detail By Date", font, Brushes.Black, marginLeft, intPosition)
        intPosition += intLineSpacing

        e.Graphics.DrawString(Format(DateTime.Now, "MM/dd/yyyy") & " " & Format(DateTime.Now, "t"), font, Brushes.Black, marginLeft, intPosition)
        intPosition += intLineSpacing

        If optAllFunctions.Checked = True Then
            e.Graphics.DrawString("All Functions", font, Brushes.Black, marginLeft, intPosition)
        Else
            e.Graphics.DrawString("Selected Function: " & cboFunctions.SelectedItem.ToString, font, Brushes.Black, marginLeft, intPosition)
        End If
        intPosition += intLineSpacing

        e.Graphics.DrawString("Page " & intPageNumber, font, Brushes.Black, marginLeft, intPosition)
        intPosition += intExtraLineSpacing

        'Create a dataview to hold each function's tips to be printed.  Loop through the dataview and print the tips
        'to the page.

        Dim dvFunctions As New DataView
        dvFunctions.Table = Me.m_dsParentDataSet.SpecialFunctions
        dvFunctions.Sort = "Date"

        If optSelectedFunction.Checked = True Then
            dvFunctions.RowFilter = "SpecialFunction = '" & cboFunctions.SelectedItem.ToString & "'"
        End If

        Static intFunctionCounter As Integer = 0
        Static decFunctionTotal As Decimal = 0
        Static decGrandTotal As Decimal = 0

        Dim intStrLen As Integer

        Do Until intFunctionCounter = dvFunctions.Count
            Dim strFunction As String = dvFunctions.Item(intFunctionCounter)("SpecialFunction").ToString
            Dim strFunctionDate As String = Format(CDate(dvFunctions.Item(intFunctionCounter)("Date")), "MM/dd/yyyy")

            Dim dvTips As New DataView

            dvTips.Table = Me.m_dsParentDataSet.Tips
            dvTips.RowFilter = "SpecialFunction = '" & strFunction & "'"
            dvTips.Sort = "LastName, FirstName, WorkingDate, Amount"

            Static intTipCounter As Integer = 0
            Static blnFunctionPrinted As Boolean
            Static blnTotalPrinted As Boolean

            If blnFunctionPrinted = False Then
                If intPosition >= marginTop + intPrintAreaHeight Then
                    intPosition = marginTop
                    intPageNumber += 1
                    e.HasMorePages = True
                    Exit Sub
                End If

                e.Graphics.DrawString(strFunction & ";  " & strFunctionDate, fontBold, Brushes.Black, marginLeft, intPosition)
                intPosition += intLineSpacing
                blnFunctionPrinted = True
            End If

            Do Until intTipCounter = dvTips.Count
                Dim strServerNumber As String = dvTips.Item(intTipCounter)("ServerNumber").ToString
                Dim strFirstName As String = dvTips.Item(intTipCounter)("FirstName").ToString
                Dim strLastName As String = dvTips.Item(intTipCounter)("LastName").ToString
                Dim decAmount As Decimal = CDec(dvTips.Item(intTipCounter)("Amount"))

                If intPosition >= marginTop + intPrintAreaHeight Then
                    intPosition = marginTop
                    intPageNumber += 1
                    e.HasMorePages = True
                    Exit Sub
                End If

                Dim strAmount As String = Format(decAmount, "0.00")

                e.Graphics.DrawString(strServerNumber & "  " & strLastName & ", " & strFirstName, font, Brushes.Black, _
                    marginLeft, intPosition)

                intStrLen = CInt(e.Graphics.MeasureString(strAmount, font, New SizeF(intPrintAreaWidth, intPrintAreaHeight), _
                    fmt, Len(strAmount), 1).Width)

                e.Graphics.DrawString(strAmount, font, Brushes.Black, marginLeft + intPrintAreaWidth - intStrLen, intPosition)

                intPosition += intLineSpacing
                decFunctionTotal += decAmount
                intTipCounter += 1
            Loop

            If blnTotalPrinted = False Then
                If intPosition >= marginTop + intPrintAreaHeight Then
                    intPosition = marginTop
                    intPageNumber += 1
                    e.HasMorePages = True
                    Exit Sub
                End If

                Dim strFunctionTotal As String = Format(decFunctionTotal, "0.00")

                e.Graphics.DrawString("Total Tips for Function", fontBold, Brushes.Black, marginLeft, intPosition)

                intStrLen = CInt(e.Graphics.MeasureString(strFunctionTotal, fontBold, New SizeF(intPrintAreaWidth, intPrintAreaHeight), _
                                    fmt, Len(strFunctionTotal), 1).Width)

                e.Graphics.DrawString(strFunctionTotal, fontBold, Brushes.Black, marginLeft + intPrintAreaWidth - intStrLen, intPosition)

                decGrandTotal += decFunctionTotal
                intPosition += intExtraLineSpacing
                blnTotalPrinted = True
            End If

            blnFunctionPrinted = False
            blnTotalPrinted = False

            decFunctionTotal = 0

            intTipCounter = 0
            intFunctionCounter += 1
        Loop

        If optSelectedFunction.Checked = False Then
            If intPosition >= marginTop + intPrintAreaHeight Then
                intPosition = marginTop
                intPageNumber += 1
                e.HasMorePages = True
                Exit Sub
            End If

            Dim strGrandTotal As String = Format(decGrandTotal, "0.00")

            e.Graphics.DrawString("TOTAL FOR ALL FUNCTIONS", fontBold, Brushes.Black, marginLeft, intPosition)

            intStrLen = CInt(e.Graphics.MeasureString(strGrandTotal, fontBold, New SizeF(intPrintAreaWidth, intPrintAreaHeight), _
                                fmt, Len(strGrandTotal), 1).Width)

            e.Graphics.DrawString(strGrandTotal, fontBold, Brushes.Black, marginLeft + intPrintAreaWidth - intStrLen, intPosition)
        End If

        intFunctionCounter = 0
        decGrandTotal = 0

        e.HasMorePages = False
        intPageNumber = 1
        intPosition = marginTop
    End Sub

End Class