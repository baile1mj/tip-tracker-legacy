Public Class frmPrintSpecialFunctionReport
    Friend m_dsParentDataset As New FileDataSet
    Private m_dvServersDataView As DataView
    Private m_dvFunctionsDataView As DataView
    Private m_dvTipsDataView As DataView

    Private Sub frmPrintSpecialFunctionReport_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
        PopulateFunctionComboBox()
        PopulateServerComboBox()
    End Sub

    Private Sub PopulateFunctionComboBox()
        cboFunctions.Items.Clear()

        For Each row As DataRow In Me.m_dsParentDataset.SpecialFunctions.Rows
            Dim strFunction As String = row("SpecialFunction").ToString
            cboFunctions.Items.Add(strFunction)
        Next

        cboFunctions.Sorted = True
    End Sub

    Private Sub optAllFunctions_CheckedChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles optAllFunctions.CheckedChanged
        cboFunctions.Enabled = optSelectedFunction.Checked
        cboFunctions.SelectedIndex = -1
    End Sub

    Private Sub optSelectedFunction_CheckedChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles optSelectedFunction.CheckedChanged
        cboFunctions.Enabled = optSelectedFunction.Checked
        cboFunctions.SelectedIndex = -1
    End Sub

    Private Sub PopulateServerComboBox()
        cboServers.Items.Clear()

        For Each row As DataRow In Me.m_dsParentDataset.Servers.Rows
            Dim strServerNumber As String = row("ServerNumber").ToString
            Dim strFirstName As String = row("FirstName").ToString
            Dim strLastName As String = row("LastName").ToString

            cboServers.Items.Add(strServerNumber & ": " & strLastName & ", " & strFirstName)
        Next

        cboServers.Sorted = True
    End Sub

    Private Function ExtractServerNumber(ByVal ComboString As String) As String
        Dim strExtractedNumber As String = ""

        For i As Integer = 1 To Len(ComboString)
            Dim c As Char = GetChar(ComboString, i)

            If c <> ":" Then
                strExtractedNumber += c.ToString
            Else
                Exit For
            End If
        Next

        Return strExtractedNumber
    End Function

    Private Sub optSelectedServer_CheckedChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles optSelectedServer.CheckedChanged
        cboServers.Enabled = optSelectedServer.Checked
    End Sub

    Private Sub optAllServers_CheckedChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles optAllServers.CheckedChanged
        cboServers.Enabled = optSelectedServer.Checked
    End Sub

    Private Sub optServer_CheckedChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles optServer.CheckedChanged
        pnlReportByServer.Enabled = optServer.Checked
        pnlReportByFunction.Enabled = optFunction.Checked
    End Sub

    Private Sub optFunction_CheckedChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles optFunction.CheckedChanged
        pnlReportByServer.Enabled = optServer.Checked
        pnlReportByFunction.Enabled = optFunction.Checked
    End Sub

    Private Sub btnClose_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnClose.Click
        Me.Close()
    End Sub

    Private Sub btnPreview_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnPreview.Click
        If optServer.Checked = True Then
            Windows.Forms.Cursor.Current = Cursors.WaitCursor

            Select Case Me.optAllServers.Checked
                Case True
                    If Me.optServerName.Checked = True Then
                        m_dvServersDataView = New DataView
                        m_dvServersDataView.Table = Me.m_dsParentDataset.Servers
                        m_dvServersDataView.Sort = "LastName, FirstName, ServerNumber"
                    Else
                        m_dvServersDataView = New DataView
                        m_dvServersDataView.Table = Me.m_dsParentDataset.Servers
                        m_dvServersDataView.Sort = "ServerNumber, LastName, FirstName"
                    End If

                Case False
                    If cboServers.SelectedIndex = -1 Then
                        MessageBox.Show("You must select a server to include on the report.", "Select Server", MessageBoxButtons.OK)
                        Exit Sub
                    End If
                    'If the option to include tips for only the selected server is checked, remove
                    'all tips that are for other servers.

                    Dim strServerNumber As String = ExtractServerNumber(cboServers.SelectedItem.ToString)

                    'Filter the servers data view so that only the selected server is returned.
                    m_dvServersDataView = New DataView
                    m_dvServersDataView.Table = Me.m_dsParentDataset.Servers
                    m_dvServersDataView.RowFilter = "ServerNumber = '" & strServerNumber & "'"
            End Select

            dlgPrintPreview.Document = docReportByServer

            Windows.Forms.Cursor.Current = Cursors.Default
            Try
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
        Else
            Windows.Forms.Cursor.Current = Cursors.WaitCursor

            Select Case Me.optAllFunctions.Checked
                Case True
                    If Me.optFunctionName.Checked = True Then
                        m_dvFunctionsDataView = New DataView
                        m_dvFunctionsDataView.Table = Me.m_dsParentDataset.SpecialFunctions
                        m_dvFunctionsDataView.Sort = "SpecialFunction"
                    Else
                        m_dvFunctionsDataView = New DataView
                        m_dvFunctionsDataView.Table = Me.m_dsParentDataset.SpecialFunctions
                        m_dvFunctionsDataView.Sort = "Date"
                    End If

                Case False
                    If cboFunctions.SelectedIndex = -1 Then
                        MessageBox.Show("You must select a function to include on the report.", "Select Function", MessageBoxButtons.OK)
                        Exit Sub
                    End If

                    Dim strFunction As String = cboFunctions.SelectedItem.ToString

                    m_dvFunctionsDataView = New DataView
                    m_dvFunctionsDataView.Table = Me.m_dsParentDataset.SpecialFunctions
                    m_dvFunctionsDataView.RowFilter = "SpecialFunction = '" & strFunction & "'"
            End Select

            dlgPrintPreview.Document = docReportByFunction

            Windows.Forms.Cursor.Current = Cursors.Default
            Try
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
        End If

        Me.Close()
    End Sub

    Private Sub btnPrint_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnPrint.Click
        If optServer.Checked = True Then
            Windows.Forms.Cursor.Current = Cursors.WaitCursor

            Select Case Me.optAllServers.Checked
                Case True
                    If Me.optServerName.Checked = True Then
                        m_dvServersDataView = New DataView
                        m_dvServersDataView.Table = Me.m_dsParentDataset.Servers
                        m_dvServersDataView.Sort = "LastName, FirstName, ServerNumber"
                    Else
                        m_dvServersDataView = New DataView
                        m_dvServersDataView.Table = Me.m_dsParentDataset.Servers
                        m_dvServersDataView.Sort = "ServerNumber, LastName, FirstName"
                    End If

                Case False
                    If cboServers.SelectedIndex = -1 Then
                        MessageBox.Show("You must select a server to include on the report.", "Select Server", MessageBoxButtons.OK)
                        Exit Sub
                    End If
                    'If the option to include tips for only the selected server is checked, remove
                    'all tips that are for other servers.

                    Dim strServerNumber As String = ExtractServerNumber(cboServers.SelectedItem.ToString)

                    'Filter the servers data view so that only the selected server is returned.
                    m_dvServersDataView = New DataView
                    m_dvServersDataView.Table = Me.m_dsParentDataset.Servers
                    m_dvServersDataView.RowFilter = "ServerNumber = '" & strServerNumber & "'"
            End Select

            dlgPrint.Document = docReportByServer

            Windows.Forms.Cursor.Current = Cursors.Default
            Try
                If dlgPrint.ShowDialog <> Windows.Forms.DialogResult.OK Then
                    dlgPrint.Dispose()
                    Me.Close()
                    Exit Sub
                End If

                docReportByServer.Print()
            Catch ex As Exception
                MessageBox.Show("Could not display the report.  Check that there is a printer installed and that a default printer has been selected.", "Cannot Preview Report", MessageBoxButtons.OK)
                Exit Sub
            End Try
        Else
            Windows.Forms.Cursor.Current = Cursors.WaitCursor

            Select Case Me.optAllFunctions.Checked
                Case True
                    If Me.optFunctionName.Checked = True Then
                        m_dvFunctionsDataView = New DataView
                        m_dvFunctionsDataView.Table = Me.m_dsParentDataset.SpecialFunctions
                        m_dvFunctionsDataView.Sort = "SpecialFunction"
                    Else
                        m_dvFunctionsDataView = New DataView
                        m_dvFunctionsDataView.Table = Me.m_dsParentDataset.SpecialFunctions
                        m_dvFunctionsDataView.Sort = "Date"
                    End If

                Case False
                    If cboFunctions.SelectedIndex = -1 Then
                        MessageBox.Show("You must select a function to include on the report.", "Select Function", MessageBoxButtons.OK)
                        Exit Sub
                    End If

                    Dim strFunction As String = cboFunctions.SelectedItem.ToString

                    m_dvFunctionsDataView = New DataView
                    m_dvFunctionsDataView.Table = Me.m_dsParentDataset.SpecialFunctions
                    m_dvFunctionsDataView.RowFilter = "SpecialFunction = '" & strFunction & "'"
            End Select

            dlgPrint.Document = docReportByFunction

            Windows.Forms.Cursor.Current = Cursors.Default
            Try
                If dlgPrint.ShowDialog <> Windows.Forms.DialogResult.OK Then
                    dlgPrint.Dispose()
                    Me.Close()
                    Exit Sub
                End If

                docReportByServer.Print()
            Catch ex As Exception
                MessageBox.Show("Could not display the report.  Check that there is a printer installed and that a default printer has been selected.", "Cannot Preview Report", MessageBoxButtons.OK)
                Exit Sub
            End Try
        End If

        Me.Close()
    End Sub

    Private Sub docReportByServer_PrintPage(ByVal sender As System.Object, ByVal e As System.Drawing.Printing.PrintPageEventArgs) Handles docReportByServer.PrintPage
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
        Const intFunctionPos As Integer = 100

        Dim marginLeft, marginTop, marginRight, marginBottom, paperHeight, paperWidth As Int32
        With docReportByServer.DefaultPageSettings
            With .Margins
                .Top = 75
                .Bottom = 75
                .Left = 75
                .Right = 75
            End With
            ' Initialize local variables to hold margin values that will serve
            ' as the X and Y coordinates for the upper left corner of the printing 
            ' area rectangle.
            marginLeft = .Margins.Left ' X coordinate
            marginTop = .Margins.Top ' Y coordinate
            marginRight = .Margins.Right
            marginBottom = .Margins.Bottom

            paperHeight = .PaperSize.Height
            paperWidth = .PaperSize.Width
        End With

        'Draw report header.
        e.Graphics.DrawString("Server Report", font, Brushes.Black, marginLeft, position)
        position += intLineSpacing
        e.Graphics.DrawString(Format(DateTime.Now, "MM/dd/yyyy h:mm tt"), font, Brushes.Black, marginLeft, position)
        position += intLineSpacing
        e.Graphics.DrawString("Page " & pageNumber + 1, font, Brushes.Black, marginLeft, position)
        position += intExtraLineSpacing

        'Draw data to the page.
        Static CurrentServerIndex As Integer

        'For each server in the data view, draw the server number and name to the page, then increment
        'the position.
        Static decTipTotal As Decimal = 0
        Static decGrandTotal As Decimal = 0
        Dim fmt As New StringFormat(StringFormatFlags.LineLimit)

        Do Until CurrentServerIndex = m_dvServersDataView.Count

            If position >= paperHeight - marginBottom Then
                e.HasMorePages = True
                position = marginTop
                pageNumber += 1
                Exit Sub
            End If

            Dim strServerNumber As String = m_dvServersDataView.Item(CurrentServerIndex)("ServerNumber").ToString
            Dim strFirstName As String = m_dvServersDataView.Item(CurrentServerIndex)("FirstName").ToString
            Dim strLastName As String = m_dvServersDataView.Item(CurrentServerIndex)("LastName").ToString

            'Draw the server number and name to the page.
            e.Graphics.DrawString(strServerNumber & "  " & strLastName & ", " & strFirstName, fontBold, Brushes.Black, marginLeft, position)
            position += intLineSpacing

            m_dvTipsDataView = New DataView
            m_dvTipsDataView.Table = Me.m_dsParentDataset.Tips
            m_dvTipsDataView.RowFilter = "ServerNumber = '" & strServerNumber & "' AND Description = 'Special Function'"
            m_dvTipsDataView.Sort = "SpecialFunction, LastName"

            Static CurrentTipIndex As Integer

            Do Until CurrentTipIndex = m_dvTipsDataView.Count
                If position >= paperHeight - marginBottom Then
                    e.HasMorePages = True
                    position = marginTop
                    pageNumber += 1
                    Exit Sub
                End If

                Dim strFunctionDate As String = Format(CDate(m_dvTipsDataView(CurrentTipIndex)("WorkingDate")), "M/d/yy")
                Dim strFunctionName As String = m_dvTipsDataView(CurrentTipIndex)("SpecialFunction").ToString
                Dim strTipAmount As String = Format(CDec(m_dvTipsDataView(CurrentTipIndex)("Amount")), "c")

                'Draw the function date to the page.
                e.Graphics.DrawString(strFunctionDate, font, Brushes.Black, marginLeft + intDatePos, position)


                'Draw the function name to the page
                e.Graphics.DrawString(strFunctionName, font, Brushes.Black, marginLeft + intDatePos + intFunctionPos, position)

                'Calculate the starting position for the tip amount then draw the amount to the page.
                Dim intAmountWidth As Integer = CInt(e.Graphics.MeasureString(strTipAmount, font, New SizeF(paperWidth - marginLeft - marginRight, paperHeight - marginTop - marginBottom), _
                fmt, Len(strTipAmount), 1).Width)

                Dim intAmountPos As Integer = paperWidth - marginRight - intAmountWidth

                e.Graphics.DrawString(strTipAmount, font, Brushes.Black, intAmountPos, position)

                'Add the tip amount into the tip total for the server.
                decTipTotal += CDec(strTipAmount)

                'Increment the position and the current tip index.
                position += intLineSpacing
                CurrentTipIndex += 1
            Loop

            e.Graphics.DrawString("SERVER TOTAL", fontBold, Brushes.Black, marginLeft + intDatePos + intFunctionPos, position)

            'Measure and draw the tip total for the server to the page.
            Dim intTotalWidth As Integer = CInt(e.Graphics.MeasureString(Format(decTipTotal, "c"), fontBold, New SizeF(paperWidth - marginLeft - marginRight, paperHeight - marginTop - marginBottom), _
            fmt, Len(Format(decTipTotal, "c")), 1).Width)

            Dim intTotalPos As Integer = paperWidth - marginRight - intTotalWidth

            e.Graphics.DrawString(Format(decTipTotal, "c"), fontBold, Brushes.Black, intTotalPos, position)

            'Add the server tip total to the grand total.
            decGrandTotal += decTipTotal

            'Increment the postion, reset the current tip index and increment the current server index.
            position += intExtraLineSpacing
            decTipTotal = 0
            CurrentTipIndex = 0
            CurrentServerIndex += 1
        Loop

        If position >= paperHeight - marginBottom Then
            e.HasMorePages = True
            position = marginTop
            pageNumber += 1
            Exit Sub
        End If

        e.Graphics.DrawString("GRAND TOTAL", fontBold, Brushes.Black, marginLeft + intDatePos + intFunctionPos, position)

        'Measure and draw the tip total for the server to the page.
        Dim intGrandTotalWidth As Integer = CInt(e.Graphics.MeasureString(Format(decGrandTotal, "c"), fontBold, New SizeF(paperWidth - marginLeft - marginRight, paperHeight - marginTop - marginBottom), _
        fmt, Len(Format(decGrandTotal, "c")), 1).Width)

        Dim intGrandTotalPos As Integer = paperWidth - marginRight - intGrandTotalWidth

        e.Graphics.DrawString(Format(decGrandTotal, "c"), fontBold, Brushes.Black, intGrandTotalPos, position)

        position = marginTop
        pageNumber = 0
        CurrentServerIndex = 0
        decTipTotal = 0
        decGrandTotal = 0
    End Sub

    Private Sub docReportByFunction_PrintPage(ByVal sender As System.Object, ByVal e As System.Drawing.Printing.PrintPageEventArgs) Handles docReportByFunction.PrintPage
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
        With docReportByFunction.DefaultPageSettings
            With .Margins
                .Top = 75
                .Bottom = 75
                .Left = 75
                .Right = 75
            End With
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
            m_dvTipsDataView.Table = Me.m_dsParentDataset.Tips
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