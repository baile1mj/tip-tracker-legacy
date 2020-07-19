Imports TipTracker.Common.Data.PayPeriod

Public Class frmSelectServer
    Friend m_dsParentDataSet As New FileDataSet

    Friend ReadOnly Property ServerNumber() As String
        Get
            Return ExtractServerNumber()
        End Get
    End Property

    Private Sub frmSelectServer_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
        PopulateComboBox()
    End Sub

    Private Sub PopulateComboBox()
        cboSelectServer.Items.Clear()

        For Each row As DataRow In m_dsParentDataSet.Servers.Rows
            If row.RowState <> DataRowState.Deleted Then
                Dim strServerNumber As String = row("ServerNumber").ToString
                Dim strFirstName As String = row("FirstName").ToString
                Dim strLastName As String = row("LastName").ToString

                cboSelectServer.Items.Add(strLastName & ", " & strFirstName & ": " & strServerNumber)
            End If

            cboSelectServer.Sorted = True
        Next
    End Sub

    Private Function ExtractServerNumber() As String
        Dim strExtractedNumber As String = ""

        For i As Integer = Len(cboSelectServer.SelectedItem.ToString) To 1 Step -1
            Dim c As Char = GetChar(cboSelectServer.SelectedItem.ToString, i)

            If c = ":" Then
                strExtractedNumber = Microsoft.VisualBasic.Right(cboSelectServer.SelectedItem.ToString, Len(cboSelectServer.SelectedItem.ToString) - i)
            End If
        Next

        strExtractedNumber = Trim(strExtractedNumber)

        Return strExtractedNumber
    End Function

    Private Sub btnCancel_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnCancel.Click
        Me.Close()
    End Sub

    Private Sub btnOK_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnOK.Click
        If cboSelectServer.SelectedIndex = -1 Then
            MessageBox.Show("You must select a server.", "Select Server", MessageBoxButtons.OK)
            Exit Sub
        End If

        Me.DialogResult = Windows.Forms.DialogResult.OK
        Me.Close()
    End Sub

End Class