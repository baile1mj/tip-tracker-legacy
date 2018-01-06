Public Class frmReportByFunction
    Private m_dvFunctionsDataView As DataView
    Private m_dvTipsDataView As DataView

    Private Sub frmReportByServer_FormClosing(ByVal sender As Object, ByVal e As System.Windows.Forms.FormClosingEventArgs) Handles Me.FormClosing
        m_dvFunctionsDataView = Nothing
        m_dvTipsDataView = Nothing
    End Sub

    Private Sub frmReportByServer_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
        PopulateComboBox()
        cboSelectFunction.Enabled = optSelectedFunction.Checked

        With docReport.DefaultPageSettings
            .Margins.Top = 75
            .Margins.Bottom = 75
            .Margins.Left = 75
            .Margins.Right = 75
        End With
    End Sub

    Private Sub PopulateComboBox()
        cboSelectFunction.Items.Clear()

        For Each row As DataRow In frmMain.FileDataSet.SpecialFunctions.Rows
            Dim strFunction As String = row("SpecialFunction").ToString
            cboSelectFunction.Items.Add(strFunction)
        Next

        cboSelectFunction.Sorted = True
    End Sub

    Private Sub optSelectedFunction_CheckedChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles optSelectedFunction.CheckedChanged
        cboSelectFunction.Enabled = optSelectedFunction.Checked
        cboSelectFunction.SelectedIndex = -1
    End Sub

    Private Sub btnClose_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnClose.Click
        Me.Close()
    End Sub

    Private Sub btnPrint_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnPrint.Click
        Windows.Forms.Cursor.Current = Cursors.WaitCursor

        Select Case Me.optAllFunctions.Checked
            Case True
                If Me.optFunctionName.Checked = True Then
                    m_dvFunctionsDataView = New DataView
                    m_dvFunctionsDataView.Table = frmMain.FileDataSet.SpecialFunctions
                    m_dvFunctionsDataView.Sort = "SpecialFunction"
                Else
                    m_dvFunctionsDataView = New DataView
                    m_dvFunctionsDataView.Table = frmMain.FileDataSet.SpecialFunctions
                    m_dvFunctionsDataView.Sort = "Date"
                End If

            Case False
                If cboSelectFunction.SelectedIndex = -1 Then
                    MessageBox.Show("You must select a server to include on the report.", "Select Server", MessageBoxButtons.OK)
                    Exit Sub
                End If
                'If the option to include tips for only the selected server is checked, remove
                'all tips that are for other servers.

                Dim strFunction As String = cboSelectFunction.SelectedItem.ToString

                'Filter the servers data view so that only the selected server is returned.
                m_dvFunctionsDataView = New DataView
                m_dvFunctionsDataView.Table = frmMain.FileDataSet.SpecialFunctions
                m_dvFunctionsDataView.RowFilter = "SpecialFunction = '" & strFunction & "'"
        End Select

        dlgPrintPreview.Document = docReport

        Try
            Windows.Forms.Cursor.Current = Cursors.Default
            dlgPrint.Document = docReport
            If dlgPrint.ShowDialog <> Windows.Forms.DialogResult.OK Then
                dlgPrint.Dispose()
                Exit Sub
            End If
            docReport.Print()
            dlgPrintPreview.Dispose()
        Catch ex As Exception
            MessageBox.Show("Could not display the report.  Check that there is a printer installed and that a default printer has been selected.", "Cannot Preview Report", MessageBoxButtons.OK)
            Exit Sub
        End Try

        Me.Close()
    End Sub

    Private Sub btnPreview_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnPreview.Click
        Windows.Forms.Cursor.Current = Cursors.WaitCursor

        Select Case Me.optAllFunctions.Checked
            Case True
                If Me.optFunctionName.Checked = True Then
                    m_dvFunctionsDataView = New DataView
                    m_dvFunctionsDataView.Table = frmMain.FileDataSet.SpecialFunctions
                    m_dvFunctionsDataView.Sort = "SpecialFunction"
                Else
                    m_dvFunctionsDataView = New DataView
                    m_dvFunctionsDataView.Table = frmMain.FileDataSet.SpecialFunctions
                    m_dvFunctionsDataView.Sort = "Date"
                End If

            Case False
                If cboSelectFunction.SelectedIndex = -1 Then
                    MessageBox.Show("You must select a function to include on the report.", "Select Function", MessageBoxButtons.OK)
                    Exit Sub
                End If
                'If the option to include tips for only the selected function is checked, remove
                'all tips that are for other functions.

                Dim strFunction As String = cboSelectFunction.SelectedItem.ToString

                'Filter the functions data view so that only the selected server is returned.
                m_dvFunctionsDataView = New DataView
                m_dvFunctionsDataView.Table = frmMain.FileDataSet.SpecialFunctions
                m_dvFunctionsDataView.RowFilter = "SpecialFunction = '" & strFunction & "'"
        End Select

        dlgPrintPreview.Document = docReport

        Try
            Windows.Forms.Cursor.Current = Cursors.Default

            ' Convert the dialog into a Form.
            Dim dlg As Form = DirectCast(dlgPrintPreview, Form)

            ' Set a "restore" size.
            dlg.Width = 600
            dlg.Height = 400

            ' Start the dialog maximized.
            dlg.WindowState = FormWindowState.Maximized

            dlg.ShowDialog()
            dlg.Dispose()
        Catch ex As Exception
            MessageBox.Show("Could not display the report.  Check that there is a printer installed and that a default printer has been selected.", "Cannot Preview Report", MessageBoxButtons.OK)
            Exit Sub
        End Try

        Me.Close()
    End Sub

    Private Sub docReport_PrintPage(ByVal sender As System.Object, ByVal e As System.Drawing.Printing.PrintPageEventArgs) Handles docReport.PrintPage
        Dim font As New System.Drawing.Font("Calibri", 10)
        Dim fontBold As New System.Drawing.Font("Calibri", 10, FontStyle.Bold)

        'Initialize local static variables that contain the current line position and
        'the current page number.
        Static position As Single = 75
        Static pageNumber As Integer

        'Initialize local constants that contain the normal line spacing and padded 
        'line spacing (double spacing).
        Const intLineSpacing As Integer = 20
        Const intExtraLineSpacing As Integer = 50
        Const intDatePos As Integer = 50
        Const intServerPos As Integer = 100

        Dim intPrintAreaHeight, intPrintAreaWidth, marginLeft, marginTop As Int32
        With docReport.DefaultPageSettings
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

        'Draw report header.
        e.Graphics.DrawString("Special Function Report", font, Brushes.Black, marginLeft, position)
        position += intLineSpacing
        e.Graphics.DrawString(Format(DateTime.Now, "MM/dd/yyyy h:mm tt"), font, Brushes.Black, marginLeft, position)
        position += intLineSpacing
        e.Graphics.DrawString("Page " & pageNumber + 1, font, Brushes.Black, marginLeft, position)
        position += intExtraLineSpacing

        'Draw data to the page.
        Static CurrentFunctionIndex As Integer

        'For each server in the data view, draw the server number and name to the page, then increment
        'the position.
        Static decTipTotal As Decimal = 0
        Static decGrandTotal As Decimal = 0
        Dim fmt As New StringFormat(StringFormatFlags.LineLimit)

        Do Until CurrentFunctionIndex = m_dvFunctionsDataView.Count

            If position >= intPrintAreaHeight + marginTop Then
                e.HasMorePages = True
                position = marginTop
                pageNumber += 1
                Exit Sub
            End If

            Dim strFunctionName As String = m_dvFunctionsDataView.Item(CurrentFunctionIndex)("SpecialFunction").ToString
            Dim strWorkingDate As String = _
            Format(CDate(m_dvFunctionsDataView.Item(CurrentFunctionIndex)("Date")), "M/d/yy")

            'Draw the function name and date to the page.
            e.Graphics.DrawString("Function Name : " & strFunctionName, fontBold, Brushes.Black, marginLeft, position)
            position += intLineSpacing
            e.Graphics.DrawString("Function Date: " & strWorkingDate, fontBold, Brushes.Black, marginLeft, position)
            position += intLineSpacing

            m_dvTipsDataView = New DataView
            m_dvTipsDataView.Table = frmMain.FileDataSet.Tips
            m_dvTipsDataView.RowFilter = "SpecialFunction = '" & strFunctionName & "'"
            m_dvTipsDataView.Sort = "LastName, FirstName"

            Static CurrentTipIndex As Integer

            Do Until CurrentTipIndex = m_dvTipsDataView.Count
                If position >= intPrintAreaHeight + marginTop Then
                    e.HasMorePages = True
                    position = marginTop
                    pageNumber += 1
                    Exit Sub
                End If

                Dim strLastName As String = m_dvTipsDataView(CurrentTipIndex)("LastName").ToString
                Dim strFirstName As String = m_dvTipsDataView(CurrentTipIndex)("FirstName").ToString
                Dim strTipAmount As String = Format(CDec(m_dvTipsDataView(CurrentTipIndex)("Amount")), "c")

                'Draw the server name to the page
                e.Graphics.DrawString(strLastName & ", " & strFirstName, font, Brushes.Black, marginLeft + intDatePos + intServerPos, position)

                'Calculate the starting position for the tip amount then draw the amount to the page.
                Dim intAmountWidth As Integer = CInt(e.Graphics.MeasureString(strTipAmount, font, New SizeF(intPrintAreaWidth, intPrintAreaHeight), _
                fmt, Len(strTipAmount), 1).Width)

                Dim intAmountPos As Integer = intPrintAreaWidth + marginLeft - intAmountWidth

                e.Graphics.DrawString(strTipAmount, font, Brushes.Black, intAmountPos, position)

                'Add the tip amount into the tip total for the server.
                decTipTotal += CDec(strTipAmount)

                'Increment the position and the current tip index.
                position += intLineSpacing
                CurrentTipIndex += 1
            Loop

            e.Graphics.DrawString("FUNCTION TOTAL", fontBold, Brushes.Black, marginLeft + intDatePos + intServerPos, position)

            'Measure and draw the tip total for the server to the page.
            Dim intTotalWidth As Integer = CInt(e.Graphics.MeasureString(Format(decTipTotal, "c"), fontBold, New SizeF(intPrintAreaWidth, intPrintAreaHeight), _
            fmt, Len(Format(decTipTotal, "c")), 1).Width)

            Dim intTotalPos As Integer = intPrintAreaWidth + marginLeft - intTotalWidth

            e.Graphics.DrawString(Format(decTipTotal, "c"), fontBold, Brushes.Black, intTotalPos, position)

            'Add the server tip total to the grand total.
            decGrandTotal += decTipTotal

            'Increment the postion, reset the current tip index and increment the current server index.
            position += intExtraLineSpacing
            decTipTotal = 0
            CurrentTipIndex = 0
            CurrentFunctionIndex += 1
        Loop

        If position >= intPrintAreaHeight + marginTop Then
            e.HasMorePages = True
            position = marginTop
            pageNumber += 1
            Exit Sub
        End If

        e.Graphics.DrawString("GRAND TOTAL", fontBold, Brushes.Black, marginLeft + intDatePos + intServerPos, position)

        'Measure and draw the tip total for the server to the page.
        Dim intGrandTotalWidth As Integer = CInt(e.Graphics.MeasureString(Format(decGrandTotal, "c"), fontBold, New SizeF(intPrintAreaWidth, intPrintAreaHeight), _
        fmt, Len(Format(decGrandTotal, "c")), 1).Width)

        Dim intGrandTotalPos As Integer = intPrintAreaWidth + marginLeft - intGrandTotalWidth

        e.Graphics.DrawString(Format(decGrandTotal, "c"), fontBold, Brushes.Black, intGrandTotalPos, position)

        position = marginTop
        pageNumber = 0
        CurrentFunctionIndex = 0
        decTipTotal = 0
        decGrandTotal = 0

       
    End Sub
End Class