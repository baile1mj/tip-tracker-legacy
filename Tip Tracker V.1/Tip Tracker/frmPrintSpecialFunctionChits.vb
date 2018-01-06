Public Class frmPrintSpecialFunctionChits
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
            dv.RowFilter = "ServerNumber = '" & strServerNumber & "' AND Description = 'Special Function'"

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
            dv.RowFilter = "ServerNumber = '" & strServerNumber & "' AND Description = 'Special Function'"

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
        With docTipChits.DefaultPageSettings
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

            Dim m_dvTipsDataView As New DataView
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

                Dim strFunctionDate As String = Format(CDate(m_dvTipsDataView(CurrentTipIndex)("WorkingDate")), "MM/dd/yyyy")
                Dim strFunctionName As String = m_dvTipsDataView(CurrentTipIndex)("SpecialFunction").ToString
                Dim strTipAmount As String = Format(CDec(m_dvTipsDataView(CurrentTipIndex)("Amount")), "0.00")

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

            e.Graphics.DrawString("SPECIAL FUNCTIONS TOTAL", fontBold, Brushes.Black, marginLeft + intDatePos + intFunctionPos, position)

            'Measure and draw the tip total for the server to the page.
            Dim intTotalWidth As Integer = CInt(e.Graphics.MeasureString(Format(decTipTotal, "0.00"), fontBold, New SizeF(paperWidth - marginLeft - marginRight, paperHeight - marginTop - marginBottom), _
            fmt, Len(Format(decTipTotal, "0.00")), 1).Width)

            Dim intTotalPos As Integer = paperWidth - marginRight - intTotalWidth

            e.Graphics.DrawString(Format(decTipTotal, "0.00"), fontBold, Brushes.Black, intTotalPos, position)

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

        position = marginTop
        pageNumber = 0
        CurrentServerIndex = 0
        decTipTotal = 0
        decGrandTotal = 0
    End Sub
End Class