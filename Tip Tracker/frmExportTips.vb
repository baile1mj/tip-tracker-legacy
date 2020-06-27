Imports System.IO

Public Class frmExportTips
    'Since there are multiple instances of frmEnterTips, a reference to the dataset
    'of the form that called this instance of frmPrintTipReports needs to be created.
    Friend m_dsParentDataSet As New FileDataSet

    Private Sub frmExportTips_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
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

    Private Sub btnCancel_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnCancel.Click
        Me.Close()
    End Sub

    Private Function HasErrors() As Boolean
        If cboSortLevel1.SelectedIndex = -1 Then
            MessageBox.Show("You must select at least one sort order.", "Invalid Selection", MessageBoxButtons.OK)
            Return True
        End If

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

        For Each checkBox As Windows.Forms.CheckBox In grpTipsToInclude.Controls
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

    Private Sub btnOK_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnOK.Click
        If HasErrors() = True Then Exit Sub

        Dim dvTips As New DataView
        dvTips.Table = Me.m_dsParentDataSet.Tips

        Select Case cboSortLevel1.SelectedItem.ToString
            Case "Order Entered"
                dvTips.Sort = "TipID"
            Case "Name"
                dvTips.Sort = "LastName, FirstName"
            Case "Date"
                dvTips.Sort = "WorkingDate"
            Case "Tip Type"
                dvTips.Sort = "Description, SpecialFunction"
            Case "Amount"
                dvTips.Sort = "Amount"
        End Select

        If cboSortLevel2.SelectedIndex <> -1 Then
            Select Case cboSortLevel2.SelectedItem.ToString
                Case "Order Entered"
                    dvTips.Sort += ", TipID"
                Case "Name"
                    dvTips.Sort += ", LastName, FirstName"
                Case "Date"
                    dvTips.Sort += ", WorkingDate"
                Case "Tip Type"
                    dvTips.Sort += ", Description, SpecialFunction"
                Case "Amount"
                    dvTips.Sort += ", Amount"
            End Select
        End If

        If cboSortLevel3.SelectedIndex <> -1 Then
            Select Case cboSortLevel3.SelectedItem.ToString
                Case "Order Entered"
                    dvTips.Sort += ", TipID"
                Case "Name"
                    dvTips.Sort += ", LastName, FirstName"
                Case "Date"
                    dvTips.Sort += ", WorkingDate"
                Case "Tip Type"
                    dvTips.Sort += ", Description, SpecialFunction"
                Case "Amount"
                    dvTips.Sort += ", Amount"
            End Select
        End If

        Dim strTypesFilter As String = ""

        For Each checkbox As Windows.Forms.CheckBox In grpTipsToInclude.Controls
            If checkbox.Checked = False Then
                Dim strType As String = ""

                Select Case checkbox.Name
                    Case "optCreditCard"
                        strType = "Credit Card"
                    Case "optRoomCharge"
                        strType = "Room Charge"
                    Case "optSpecialFunction"
                        strType = "Special Function"
                    Case "optCash"
                        strType = "Cash"
                End Select

                If strTypesFilter = "" Then
                    strTypesFilter = "Description <> '" & strType & "'"
                Else
                    strTypesFilter += " AND Description <> '" & strType & "'"
                End If
            End If
        Next

        If optSelectedServer.Checked = True Then
            If strTypesFilter = "" Then
                strTypesFilter += "ServerNumber = '" & cboServers.SelectedValue.ToString & "'"
            Else
                strTypesFilter += " AND ServerNumber = '" & cboServers.SelectedValue.ToString & "'"
            End If
        End If

        If optSelectedDate.Checked = True Then
            If strTypesFilter = "" Then
                strTypesFilter += "WorkingDate = '" & Format(CDate(txtSelectedDate.Text), "MM/dd/yyyy") & "'"
            Else
                strTypesFilter += " AND WorkingDate = '" & Format(CDate(txtSelectedDate.Text), "MM/dd/yyyy") & "'"
            End If

        End If

        dvTips.RowFilter = strTypesFilter

        If dvTips.Count = 0 Then
            MessageBox.Show("No tips that match your filter criteria were found.", "No Tips To Export", MessageBoxButtons.OK)
            Exit Sub
        End If

        Dim dlgSaveFile As New SaveFileDialog

        With dlgSaveFile
            .AddExtension = True
            .CreatePrompt = False
            .DefaultExt = "*.csv"
            .FileName = "Export"
            .Filter = "Comma delimited files (*.csv)|*.csv"
            .InitialDirectory = My.Computer.FileSystem.SpecialDirectories.MyDocuments
            .OverwritePrompt = True
            .SupportMultiDottedExtensions = True
            .Title = "Export File"
        End With

        If dlgSaveFile.ShowDialog <> Windows.Forms.DialogResult.OK Then
            dlgSaveFile.Dispose()
            Exit Sub
        End If

        Dim strPath As String = dlgSaveFile.FileName

        dlgSaveFile.Dispose()

        Windows.Forms.Cursor.Current = Cursors.WaitCursor

        Dim strFileContents As String = "TipID,Amount,ServerNumber,FirstName,LastName,Description,SpecialFunction,WorkingDate" & vbCrLf

        For i As Integer = 0 To dvTips.Count - 1
            If dvTips(i).Row.RowState <> DataRowState.Deleted Then
                Dim strTipID As String = dvTips(i)("TipID").ToString
                Dim strAmount As String = dvTips(i)("Amount").ToString
                Dim strServerNumber As String = dvTips(i)("ServerNumber").ToString
                Dim strFirstName As String = dvTips(i)("FirstName").ToString
                Dim strLastName As String = dvTips(i)("LastName").ToString
                Dim strDescription As String = dvTips(i)("Description").ToString
                Dim strSpecialFunction As String = dvTips(i)("SpecialFunction").ToString
                Dim strWorkingDate As String = Format(CDate(dvTips(i)("WorkingDate")), "MM/dd/yyyy")

                strFileContents += strTipID & "," & strAmount & "," & strServerNumber & "," & strFirstName & _
                "," & strLastName & "," & strDescription & "," & strSpecialFunction & "," & strWorkingDate & vbCrLf
            End If
        Next

        Dim objStreamWriter As New StreamWriter(strPath)
        objStreamWriter.Write(strFileContents)
        objStreamWriter.Flush()
        objStreamWriter.Close()
        objStreamWriter.Dispose()

        Windows.Forms.Cursor.Current = Cursors.Default

        Me.Close()
    End Sub

    Private Sub cboSortLevel1_SelectedIndexChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles cboSortLevel1.SelectedIndexChanged
        If cboSortLevel1.SelectedIndex <> -1 Then
            cboSortLevel2.Enabled = True
            cboSortLevel2.Items.Clear()

            cboSortLevel3.Enabled = False
            cboSortLevel3.Items.Clear()

            For i As Integer = 0 To cboSortLevel1.Items.Count - 1
                If cboSortLevel1.SelectedIndex <> i Then
                    cboSortLevel2.Items.Add(cboSortLevel1.Items.Item(i).ToString)
                End If
            Next
        Else
            cboSortLevel2.Enabled = False
            cboSortLevel3.Enabled = False
        End If
    End Sub

    Private Sub cboSortLevel2_SelectedIndexChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles cboSortLevel2.SelectedIndexChanged
        If cboSortLevel2.SelectedIndex <> -1 Then
            cboSortLevel3.Enabled = True
            cboSortLevel3.Items.Clear()

            For i As Integer = 0 To cboSortLevel2.Items.Count - 1
                If cboSortLevel2.SelectedIndex <> i Then
                    cboSortLevel3.Items.Add(cboSortLevel2.Items.Item(i).ToString)
                End If
            Next
        Else
            cboSortLevel2.Enabled = False
        End If
    End Sub

    Private Sub btnClear_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnClear.Click
        cboSortLevel1.SelectedIndex = -1
        cboSortLevel2.SelectedIndex = -1
        cboSortLevel3.SelectedIndex = -1
    End Sub
End Class