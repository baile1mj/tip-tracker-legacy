Public Class frmReportByServer
    Private m_dvServersDataView As DataView
    Private m_dvTipsDataView As DataView

    Private Sub frmReportByServer_FormClosing(ByVal sender As Object, ByVal e As System.Windows.Forms.FormClosingEventArgs) Handles Me.FormClosing
        m_dvServersDataView = Nothing
        m_dvTipsDataView = Nothing
    End Sub

    Private Sub frmReportByServer_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
        PopulateComboBox()
        cboSelectServer.Enabled = optSelectedServer.Checked

        With docReport.DefaultPageSettings
            .Margins.Top = 75
            .Margins.Bottom = 75
            .Margins.Left = 75
            .Margins.Right = 75
        End With
    End Sub

    Private Sub PopulateComboBox()
        cboSelectServer.Items.Clear()

        For Each row As DataRow In frmMain.FileDataSet.Servers.Rows
            Dim strServerNumber As String = row("ServerNumber").ToString
            Dim strFirstName As String = row("FirstName").ToString
            Dim strLastName As String = row("LastName").ToString

            cboSelectServer.Items.Add(strServerNumber & ": " & strLastName & ", " & strFirstName)
        Next

        cboSelectServer.Sorted = True
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
        cboSelectServer.Enabled = optSelectedServer.Checked
    End Sub

    Private Sub btnClose_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnClose.Click
        Me.Close()
    End Sub

    Private Sub btnPrint_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnPrint.Click
        Windows.Forms.Cursor.Current = Cursors.WaitCursor

        Select Case Me.optAllServers.Checked
            Case True
                If Me.optServerName.Checked = True Then
                    m_dvServersDataView = New DataView
                    m_dvServersDataView.Table = frmmain.filedataset.Servers
                    m_dvServersDataView.Sort = "LastName, FirstName, ServerNumber"
                Else
                    m_dvServersDataView = New DataView
                    m_dvServersDataView.Table = frmmain.filedataset.Servers
                    m_dvServersDataView.Sort = "ServerNumber, LastName, FirstName"
                End If

                m_dvTipsDataView = New DataView
                m_dvTipsDataView.Table = frmmain.filedataset.Tips
                m_dvTipsDataView.Sort = "SpecialFunction, LastName"

            Case False
                If cboSelectServer.SelectedIndex = -1 Then
                    MessageBox.Show("You must select a server to include on the report.", "Select Server", MessageBoxButtons.OK)
                    Exit Sub
                End If
                'If the option to include tips for only the selected server is checked, remove
                'all tips that are for other servers.

                Dim strServerNumber As String = ExtractServerNumber(cboSelectServer.SelectedItem.ToString)

                'Filter the servers data view so that only the selected server is returned.
                m_dvServersDataView = New DataView
                m_dvServersDataView.Table = frmmain.filedataset.Servers
                m_dvServersDataView.RowFilter = "ServerNumber = '" & strServerNumber & "'"
        End Select

        dlgPrint.Document = docReport

        Try
            Windows.Forms.Cursor.Current = Cursors.Default
            If dlgPrint.ShowDialog() <> Windows.Forms.DialogResult.OK Then
                dlgPrint.Dispose()
                Exit Sub
            End If
            docReport.Print()
            dlgPrint.Dispose()
        Catch ex As Exception
            MessageBox.Show("Could not display the report.  Check that there is a printer installed and that a default printer has been selected.", "Cannot Preview Report", MessageBoxButtons.OK)
            Exit Sub
        End Try

        Me.Close()
    End Sub

    Private Sub btnPreview_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnPreview.Click
        Windows.Forms.Cursor.Current = Cursors.WaitCursor

        Select Case Me.optAllServers.Checked
            Case True
                If Me.optServerName.Checked = True Then
                    m_dvServersDataView = New DataView
                    m_dvServersDataView.Table = frmmain.filedataset.Servers
                    m_dvServersDataView.Sort = "LastName, FirstName, ServerNumber"
                Else
                    m_dvServersDataView = New DataView
                    m_dvServersDataView.Table = frmmain.filedataset.Servers
                    m_dvServersDataView.Sort = "ServerNumber, LastName, FirstName"
                End If

            Case False
                If cboSelectServer.SelectedIndex = -1 Then
                    MessageBox.Show("You must select a server to include on the report.", "Select Server", MessageBoxButtons.OK)
                    Exit Sub
                End If
                'If the option to include tips for only the selected server is checked, remove
                'all tips that are for other servers.

                Dim strServerNumber As String = ExtractServerNumber(cboSelectServer.SelectedItem.ToString)

                'Filter the servers data view so that only the selected server is returned.
                m_dvServersDataView = New DataView
                m_dvServersDataView.Table = frmMain.FileDataSet.Servers
                m_dvServersDataView.RowFilter = "ServerNumber = '" & strServerNumber & "'"
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
        Const intFunctionPos As Integer = 100

        Dim marginLeft, marginTop, marginRight, marginBottom, paperHeight, paperWidth As Int32
        With docReport.DefaultPageSettings
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
            m_dvTipsDataView.Table = frmMain.FileDataSet.Tips
            m_dvTipsDataView.RowFilter = "ServerNumber = '" & strServerNumber & "'"
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
End Class