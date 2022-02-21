Public Class frmImportServersComparer
    Friend m_dsParentDataSet As New FileDataSet
    Private m_strServerNumber As String
    Private m_strFirstName As String
    Private m_strLastName As String

    Friend Property ServerNumber() As String
        Get
            Return m_strServerNumber
        End Get
        Set(ByVal value As String)
            m_strServerNumber = value
        End Set
    End Property

    Friend Property FirstName() As String
        Get
            Return m_strFirstName
        End Get
        Set(ByVal value As String)
            m_strFirstName = value
        End Set
    End Property

    Friend Property LastName() As String
        Get
            Return m_strLastName
        End Get
        Set(ByVal value As String)
            m_strLastName = value
        End Set
    End Property

    Private Sub frmImportServersComparer_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
        txtImportedServer.Text = Me.ServerNumber & ": " & Me.FirstName & " " & Me.LastName
        Me.optExisting.Checked = True
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
        Next

        cboSelectServer.Sorted = True
    End Sub

    Private Function ExtractServerNumber(ByVal ComboString As String) As String
        Dim strExtractedNumber As String = ""

        Dim intLocation As Integer

        For intLocation = Len(ComboString) To 1 Step -1
            Dim c As Char = GetChar(ComboString, intLocation)

            If c = ":" Then
                Exit For
            End If
        Next

        strExtractedNumber = Microsoft.VisualBasic.Right(ComboString, Len(ComboString) - intLocation)
        strExtractedNumber = Trim(strExtractedNumber)

        Return strExtractedNumber
    End Function

    Private Sub optExisting_CheckedChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles optExisting.CheckedChanged
        Select Case optExisting.Checked
            Case True
                cboSelectServer.Enabled = True
                cboSelectServer.Focus()
                txtServerNumber.Enabled = False
                txtFirstName.Enabled = False
                txtLastName.Enabled = False
                txtServerNumber.Clear()
                txtFirstName.Clear()
                txtLastName.Clear()
            Case Else
                cboSelectServer.Enabled = False
                cboSelectServer.SelectedIndex = -1
                txtServerNumber.Enabled = True
                txtFirstName.Enabled = True
                txtLastName.Enabled = True
                txtServerNumber.Focus()
                txtServerNumber.Text = Me.ServerNumber
                txtFirstName.Text = Me.FirstName
                txtLastName.Text = Me.LastName
        End Select
    End Sub

    Private Sub btnCancel_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnCancel.Click
        Me.Close()
    End Sub

    Private Sub txtServerNumber_KeyPress(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyPressEventArgs) Handles txtServerNumber.KeyPress
        If e.KeyChar = ChrW(13) Then
            e.Handled = True
            txtFirstName.Focus()
        End If
        If e.KeyChar = "|" Then
            e.Handled = True
        End If
    End Sub

    Private Sub txtFirstName_KeyPress(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyPressEventArgs) Handles txtFirstName.KeyPress
        If e.KeyChar = ChrW(13) Then
            e.Handled = True
            txtLastName.Focus()
        End If
    End Sub

    Private Sub txtLastName_KeyPress(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyPressEventArgs) Handles txtLastName.KeyPress
        If e.KeyChar = ChrW(13) Then
            e.Handled = True
            btnNext.Focus()
        End If
    End Sub

    Private Sub btnNext_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnNext.Click
        Select Case optExisting.Checked
            Case True
                If cboSelectServer.SelectedIndex = -1 Then
                    MessageBox.Show("You must select a server.", "Select Server", MessageBoxButtons.OK)
                    cboSelectServer.Focus()
                    Exit Sub
                End If

                Dim strServerNumber As String = ExtractServerNumber(cboSelectServer.SelectedItem.ToString)

                Me.ServerNumber = strServerNumber
                Me.FirstName = m_dsParentDataSet.Servers.FindByServerNumber(strServerNumber)("FirstName").ToString
                Me.LastName = m_dsParentDataSet.Servers.FindByServerNumber(strServerNumber)("LastName").ToString

            Case Else
                If txtServerNumber.Text = "" Then
                    MessageBox.Show("You must enter a server number.", "Enter Server Number", MessageBoxButtons.OK)
                    txtServerNumber.Focus()
                    Exit Sub
                End If
                If txtFirstName.Text = "" Then
                    MessageBox.Show("You must enter a first name.", "Enter First Name", MessageBoxButtons.OK)
                    txtFirstName.Focus()
                    Exit Sub
                End If
                If txtLastName.Text = "" Then
                    MessageBox.Show("You must enter a last name.", "Enter Last Name", MessageBoxButtons.OK)
                    txtLastName.Focus()
                    Exit Sub
                End If

                If Not (m_dsParentDataSet.Servers.FindByServerNumber(txtServerNumber.Text) Is Nothing) Then
                    MessageBox.Show("The server number you entered is already in use.  Please use a different number or choose an existing server.", "Invalid Entry", MessageBoxButtons.OK)
                    txtServerNumber.Clear()
                    txtServerNumber.Focus()
                    Exit Sub
                End If

                Me.ServerNumber = txtServerNumber.Text
                Me.FirstName = txtFirstName.Text
                Me.LastName = txtLastName.Text
        End Select
        Me.DialogResult = Windows.Forms.DialogResult.OK
        Me.Close()
    End Sub

End Class