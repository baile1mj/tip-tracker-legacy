Public Class frmPrintServerReport
    Friend m_dsParentDataSet As New FileDataSet
    Private m_dvServers As New DataView

    Private Sub btnCancel_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnCancel.Click
        Me.Close()
    End Sub

    Private Sub frmPrintServerReport_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
        PopulateComboBox()

        optSummary.Checked = True
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

    Private Sub btnPreview_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnPreview.Click
        If optSelected.Checked = True And cboSelectServer.SelectedIndex = -1 Then
            MessageBox.Show("You must select a server.", "Select Server", MessageBoxButtons.OK)
            Exit Sub
        End If

        m_dvServers = New DataView
        m_dvServers.Table = Me.m_dsParentDataSet.Servers
        m_dvServers.Sort = "LastName, FirstName"

        If optSelected.Checked = True Then
            Dim strServerNumber As String = ExtractServerNumber(cboSelectServer.SelectedItem.ToString)
            m_dvServers.RowFilter = "ServerNumber = '" & strServerNumber & "'"
        End If

        If optSummary.Checked = True Then
            dlgPrintPreview.Document = docSummary
        Else
            dlgPrintPreview.Document = docDetail
        End If

        If m_dvServers.Count = 0 Then
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

        m_dvServers = New DataView
        m_dvServers.Table = Me.m_dsParentDataSet.Servers
        m_dvServers.Sort = "LastName, FirstName"

        If optSelected.Checked = True Then
            Dim strServerNumber As String = ExtractServerNumber(cboSelectServer.SelectedItem.ToString)
            m_dvServers.RowFilter = "ServerNumber = '" & strServerNumber & "'"
        End If

        If optSummary.Checked = True Then
            dlgPrint.Document = docSummary
        Else
            dlgPrint.Document = docDetail
        End If

        If m_dvServers.Count = 0 Then
            MessageBox.Show("There are no tips to report.", "No Tips", MessageBoxButtons.OK)
            Me.Close()
            Exit Sub
        End If

        Try
            Windows.Forms.Cursor.Current = Cursors.Default

            If dlgPrint.ShowDialog <> Windows.Forms.DialogResult.OK Then
                dlgPrint.Dispose()
                Me.Close()
                Exit Sub
            End If

            If optSummary.Checked = True Then
                docSummary.Print()
            Else
                docDetail.Print()
            End If

            Me.Close()
        Catch ex As Exception
            MessageBox.Show("Could not print the document.  Check that there is a printer installed and that a default printer has been selected.", "Cannot Preview Document", MessageBoxButtons.OK)
            Exit Sub
        End Try

        Me.Close()
    End Sub

    Private Sub docSummary_PrintPage(ByVal sender As System.Object, ByVal e As System.Drawing.Printing.PrintPageEventArgs) Handles docSummary.PrintPage
        Dim font As New System.Drawing.Font("Calibri", 10)
        Dim fontBold As New System.Drawing.Font("Calibri", 10, FontStyle.Bold)

        Dim fmt As New StringFormat(StringFormatFlags.LineLimit)

        'Initialize local static variables that contain the current line position and
        'the current page number.
        Static position As Single = 75
        Static intPageNumber As Integer = 1

        Const intLineSpacing As Integer = 20
        Const intExtraLineSpacing As Integer = 50

        Const intIndent As Integer = 50

        'Initialize local constants that contain the normal line spacing and padded 
        'line spacing (double spacing).
        Dim intPrintAreaHeight, intPrintAreaWidth, marginLeft, marginTop As Int32
        With docSummary.DefaultPageSettings
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
        e.Graphics.DrawString("Server Summary Report", font, Brushes.Black, marginLeft, position)
        position += intLineSpacing

        e.Graphics.DrawString(Format(DateTime.Now, "g"), font, Brushes.Black, marginLeft, position)
        position += intLineSpacing

        e.Graphics.DrawString("Page " & intPageNumber, font, Brushes.Black, marginLeft, position)
        position += intExtraLineSpacing

        Static decGrandTotal As Decimal

        Static intServer As Integer
        Do Until intServer = m_dvServers.Count
            Dim strServerNumber As String = m_dvServers(intServer)("ServerNumber").ToString
            Dim strFirstName As String = m_dvServers(intServer)("FirstName").ToString
            Dim strLastName As String = m_dvServers(intServer)("LastName").ToString

            If position >= marginTop + intPrintAreaHeight Then
                e.HasMorePages = True
                position = 75
                intPageNumber += 1
                Exit Sub
            End If

            e.Graphics.DrawString(strServerNumber & "  " & strLastName & ", " & strFirstName, fontBold, _
                Brushes.Black, marginLeft, position)

            position += intLineSpacing

            Const intCreditCard As Integer = 1
            Const intRoomCharge As Integer = 2
            Const intCash As Integer = 3
            Const intSpecialFunction As Integer = 4
            Const intTotal As Integer = 5

            Static intType As Integer = 1
            Static decServerTotal As Decimal

            Do Until intType = intTotal + 1
                If position >= marginTop + intPrintAreaHeight Then
                    e.HasMorePages = True
                    position = 75
                    intPageNumber += 1
                    Exit Sub
                End If

                Select Case intType
                    Case intCreditCard
                        Dim dvCreditCard As New DataView
                        dvCreditCard.Table = Me.m_dsParentDataSet.Tips
                        dvCreditCard.RowFilter = "ServerNumber = '" & strServerNumber & "' AND Description = 'Credit Card'"

                        Dim decCCTotal As Decimal = 0
                        If dvCreditCard.Count <> 0 Then
                            For i As Integer = 0 To dvCreditCard.Count - 1
                                decCCTotal += CDec(dvCreditCard(i)("Amount"))
                            Next
                        End If

                        decServerTotal += decCCTotal
                        decGrandTotal += decCCTotal

                        e.Graphics.DrawString("Credit Card Tips", font, Brushes.Black, marginLeft + intIndent, _
                            position)

                        Dim intStrLen As Integer = CInt(e.Graphics.MeasureString(Format(decCCTotal, "0.00"), _
                            font, New SizeF(intPrintAreaWidth, intPrintAreaHeight), fmt, _
                            Len(Format(decCCTotal, "0.00")), 1).Width)

                        e.Graphics.DrawString(Format(decCCTotal, "0.00"), font, Brushes.Black, _
                            marginLeft + intPrintAreaWidth - intStrLen, position)

                        position += intLineSpacing
                    Case intRoomCharge
                        Dim dvRoomCharge As New DataView
                        dvRoomCharge.Table = Me.m_dsParentDataSet.Tips
                        dvRoomCharge.RowFilter = "ServerNumber = '" & strServerNumber & "' AND Description = 'Room Charge'"

                        Dim decRCTotal As Decimal = 0
                        If dvRoomCharge.Count <> 0 Then
                            For i As Integer = 0 To dvRoomCharge.Count - 1
                                decRCTotal += CDec(dvRoomCharge(i)("Amount"))
                            Next
                        End If

                        decServerTotal += decRCTotal
                        decGrandTotal += decRCTotal

                        e.Graphics.DrawString("Room Charge Tips", font, Brushes.Black, marginLeft + intIndent, _
                            position)

                        Dim intStrLen As Integer = CInt(e.Graphics.MeasureString(Format(decRCTotal, "0.00"), _
                            font, New SizeF(intPrintAreaWidth, intPrintAreaHeight), fmt, _
                            Len(Format(decRCTotal, "0.00")), 1).Width)

                        e.Graphics.DrawString(Format(decRCTotal, "0.00"), font, Brushes.Black, _
                            marginLeft + intPrintAreaWidth - intStrLen, position)

                        position += intLineSpacing
                    Case intCash
                        Dim dvCash As New DataView
                        dvCash.Table = Me.m_dsParentDataSet.Tips
                        dvCash.RowFilter = "ServerNumber = '" & strServerNumber & "' AND Description = 'Cash'"

                        Dim decCATotal As Decimal = 0
                        If dvCash.Count <> 0 Then
                            For i As Integer = 0 To dvCash.Count - 1
                                decCATotal += CDec(dvCash(i)("Amount"))
                            Next
                        End If

                        decServerTotal += decCATotal
                        decGrandTotal += decCATotal

                        e.Graphics.DrawString("Cash Tips Claimed", font, Brushes.Black, marginLeft + intIndent, _
                            position)

                        Dim intStrLen As Integer = CInt(e.Graphics.MeasureString(Format(decCATotal, "0.00"), _
                            font, New SizeF(intPrintAreaWidth, intPrintAreaHeight), fmt, _
                            Len(Format(decCATotal, "0.00")), 1).Width)

                        e.Graphics.DrawString(Format(decCATotal, "0.00"), font, Brushes.Black, _
                            marginLeft + intPrintAreaWidth - intStrLen, position)

                        position += intLineSpacing
                    Case intSpecialFunction
                        Dim dvSpecialFunction As New DataView
                        dvSpecialFunction.Table = Me.m_dsParentDataSet.Tips
                        dvSpecialFunction.RowFilter = "ServerNumber = '" & strServerNumber & "' AND Description = 'Special Function'"

                        Dim decSFTotal As Decimal = 0
                        If dvSpecialFunction.Count <> 0 Then
                            For i As Integer = 0 To dvSpecialFunction.Count - 1
                                decSFTotal += CDec(dvSpecialFunction(i)("Amount"))
                            Next
                        End If

                        decServerTotal += decSFTotal
                        decGrandTotal += decSFTotal

                        e.Graphics.DrawString("Special Function Tips", font, Brushes.Black, marginLeft + intIndent, _
                            position)

                        Dim intStrLen As Integer = CInt(e.Graphics.MeasureString(Format(decSFTotal, "0.00"), _
                            font, New SizeF(intPrintAreaWidth, intPrintAreaHeight), fmt, _
                            Len(Format(decSFTotal, "0.00")), 1).Width)

                        e.Graphics.DrawString(Format(decSFTotal, "0.00"), font, Brushes.Black, _
                            marginLeft + intPrintAreaWidth - intStrLen, position)

                        position += intLineSpacing
                    Case intTotal
                        e.Graphics.DrawString("TOTAL TIPS FOR " & strServerNumber, fontBold, Brushes.Black, marginLeft + intIndent, position)

                        Dim intStrLen As Integer = CInt(e.Graphics.MeasureString(Format(decServerTotal, "0.00"), _
                            fontBold, New SizeF(intPrintAreaWidth, intPrintAreaHeight), fmt, _
                            Len(Format(decServerTotal, "0.00")), 1).Width)

                        e.Graphics.DrawString(Format(decServerTotal, "0.00"), fontBold, Brushes.Black, _
                            marginLeft + intPrintAreaWidth - intStrLen, position)

                        position += intLineSpacing
                    Case Else
                        MsgBox("Something really bad happened.")
                End Select
                intType += 1
            Loop
            position += intLineSpacing
            intType = 1
            decServerTotal = 0

            intServer += 1
        Loop

        If position >= marginTop + intPrintAreaHeight Then
            e.HasMorePages = True
            position = 75
            intPageNumber += 1
            Exit Sub
        End If

        e.Graphics.DrawString("TOTAL TIPS FOR ALL SERVERS", fontBold, Brushes.Black, marginLeft, position)

        Dim intTotalLen As Integer = CInt(e.Graphics.MeasureString(Format(decGrandTotal, "0.00"), _
            fontBold, New SizeF(intPrintAreaWidth, intPrintAreaHeight), fmt, _
            Len(Format(decGrandTotal, "0.00")), 1).Width)

        e.Graphics.DrawString(Format(decGrandTotal, "0.00"), fontBold, Brushes.Black, _
            marginLeft + intPrintAreaWidth - intTotalLen, position)

        'Printing complete.  Reset the static variables and set e.HasMorePages to False.
        e.HasMorePages = False
        position = 75
        intServer = 0
        decGrandTotal = 0
    End Sub

    Private Sub docDetail_PrintPage(ByVal sender As System.Object, ByVal e As System.Drawing.Printing.PrintPageEventArgs) Handles docDetail.PrintPage
        Dim font As New System.Drawing.Font("Calibri", 10)
        Dim fontBold As New System.Drawing.Font("Calibri", 10, FontStyle.Bold)

        Dim fmt As New StringFormat(StringFormatFlags.LineLimit)

        'Initialize local static variables that contain the current line position and
        'the current page number.
        Static position As Single = 75
        Static intPageNumber As Integer = 1

        Const intLineSpacing As Integer = 20
        Const intExtraLineSpacing As Integer = 50

        Const intDatePos As Integer = 50
        Const intDescriptionPos As Integer = 150

        'Initialize local constants that contain the normal line spacing and padded 
        'line spacing (double spacing).
        Dim intPrintAreaHeight, intPrintAreaWidth, marginLeft, marginTop As Int32
        With docDetail.DefaultPageSettings
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
        e.Graphics.DrawString("Server Detail Report", font, Brushes.Black, marginLeft, position)
        position += intLineSpacing

        e.Graphics.DrawString(Format(DateTime.Now, "g"), font, Brushes.Black, marginLeft, position)
        position += intLineSpacing

        e.Graphics.DrawString("Page " & intPageNumber, font, Brushes.Black, marginLeft, position)
        position += intExtraLineSpacing

        Static decGrandTotal As Decimal

        Static intServer As Integer
        Do Until intServer = m_dvServers.Count
            Dim strServerNumber As String = m_dvServers(intServer)("ServerNumber").ToString
            Dim strFirstName As String = m_dvServers(intServer)("FirstName").ToString
            Dim strLastName As String = m_dvServers(intServer)("LastName").ToString

            If position >= marginTop + intPrintAreaHeight Then
                e.HasMorePages = True
                position = 75
                intPageNumber += 1
                Exit Sub
            End If

            e.Graphics.DrawString(strServerNumber & "  " & strLastName & ", " & strFirstName, fontBold, _
                Brushes.Black, marginLeft, position)

            position += intLineSpacing

            Dim dvTips As New DataView
            dvTips.Table = Me.m_dsParentDataSet.Tips
            dvTips.RowFilter = "ServerNumber = '" & strServerNumber & "'"
            dvTips.Sort = "Description, SpecialFunction, WorkingDate"

            Static decTipTotal As Decimal

            Static intTip As Integer

            Dim intStrLen As Integer

            Do Until intTip = dvTips.Count
                Dim dteWorkingDate As Date = CDate(dvTips.Item(intTip)("WorkingDate"))
                Dim strDescription As String = dvTips.Item(intTip)("Description").ToString
                Dim strFunction As String = dvTips.Item(intTip)("SpecialFunction").ToString
                Dim decAmount As Decimal = CDec(dvTips.Item(intTip)("Amount"))

                If position >= marginTop + intPrintAreaHeight Then
                    e.HasMorePages = True
                    position = 75
                    intPageNumber += 1
                    Exit Sub
                End If

                decTipTotal += decAmount

                e.Graphics.DrawString(Format(dteWorkingDate, "MM/dd/yyyy"), font, Brushes.Black, marginLeft + intDatePos, position)
              
                If strDescription = "Special Function" Then
                    e.Graphics.DrawString(strDescription & ": " & strFunction, font, Brushes.Black, marginLeft + intDescriptionPos, position)
                Else
                    e.Graphics.DrawString(strDescription, font, Brushes.Black, marginLeft + intDescriptionPos, position)
                End If

                intStrLen = CInt(e.Graphics.MeasureString(Format(decAmount, "0.00"), font, _
                    New SizeF(intPrintAreaWidth, intPrintAreaHeight), fmt, Len(Format(decAmount, "0.00")), 1).Width)

                e.Graphics.DrawString(Format(decAmount, "0.00"), font, Brushes.Black, marginLeft + intPrintAreaWidth - intStrLen, position)

                position += intLineSpacing
                intTip += 1
            Loop



            If position >= marginTop + intPrintAreaHeight Then
                e.HasMorePages = True
                position = 75
                intPageNumber += 1
                Exit Sub
            End If

            intStrLen = CInt(e.Graphics.MeasureString(Format(decTipTotal, "0.00"), fontBold, _
                            New SizeF(intPrintAreaWidth, intPrintAreaHeight), fmt, Len(Format(decTipTotal, "0.00")), 1).Width)

            e.Graphics.DrawString("TOTAL TIPS FOR " & strServerNumber, fontBold, Brushes.Black, marginLeft + intDatePos, position)
            e.Graphics.DrawString(Format(decTipTotal, "0.00"), fontBold, Brushes.Black, marginLeft + intPrintAreaWidth - intStrLen, position)

            decGrandTotal += decTipTotal

            position += intExtraLineSpacing

            decTipTotal = 0
            intTip = 0
            intServer += 1
        Loop

        If position >= marginTop + intPrintAreaHeight Then
            e.HasMorePages = True
            position = 75
            intPageNumber += 1
            Exit Sub
        End If

        Dim intLength As Integer = CInt(e.Graphics.MeasureString(Format(decGrandTotal, "0.00"), fontBold, _
            New SizeF(intPrintAreaWidth, intPrintAreaHeight), fmt, Len(Format(decGrandTotal, "0.00")), 1).Width)

        e.Graphics.DrawString("TOTAL TIPS FOR ALL SERVERS", fontBold, Brushes.Black, marginLeft, position)
        e.Graphics.DrawString(Format(decGrandTotal, "0.00"), fontBold, Brushes.Black, marginLeft + intPrintAreaWidth - intLength, position)

        decGrandTotal += decGrandTotal

        'Printing complete.  Reset the static variables and set e.HasMorePages to False.
        e.HasMorePages = False
        position = 75
        intServer = 0
        decGrandTotal = 0
    End Sub
End Class