Imports System.IO

Public Class frmDataFile
    Private m_strCurrentFile As String = ""

    Private Sub ExitToolStripMenuItem_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles ExitToolStripMenuItem.Click
        Me.Close()
    End Sub

    Private Sub SaveToolStripMenuItem_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles SaveToolStripMenuItem.Click
        If m_strCurrentFile = "" Then Exit Sub

        Try
            Dim objFileEncoder As New clsFileEncoder
            Dim objDSStream As New MemoryStream

            Me.FileDataSet.WriteXml(objDSStream)

            objFileEncoder.EncodeFile(m_strCurrentFile, objDSStream)

            objFileEncoder.Dispose()
            objDSStream.Close()
            objDSStream.Dispose()
            Me.FileDataSet.AcceptChanges()
        Catch ex As Exception
            MessageBox.Show("Could not save file.", "Error", MessageBoxButtons.OK)
            Exit Sub
        End Try

    End Sub

    Private Sub SaveXMLToolStripMenuItem_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles SaveXMLToolStripMenuItem.Click
        If m_strCurrentFile = "" Then Exit Sub

        Dim strFileName As String = ""
        Dim dlgSave As New SaveFileDialog

        With dlgSave
            .AddExtension = True
            .DefaultExt = ".xml"
            .Filter = "XML File (*.xml)|*.xml"
            .InitialDirectory = My.Computer.FileSystem.SpecialDirectories.MyDocuments
            .OverwritePrompt = True
            .RestoreDirectory = True
            .SupportMultiDottedExtensions = True
            .Title = "Save File"
        End With

        If dlgSave.ShowDialog <> Windows.Forms.DialogResult.OK Then
            dlgSave.Dispose()
            Exit Sub
        End If

        strFileName = dlgSave.FileName

        Try
            Me.FileDataSet.WriteXml(strFileName)
        Catch ex As Exception
            MessageBox.Show("Could not save XML.", "Error", MessageBoxButtons.OK)
            Exit Sub
        End Try
    End Sub

    Private Sub CloseFileToolStripMenuItem_Click(ByVal sender As System.Object, ByVal e As System.EventArgs)
        If m_strCurrentFile = "" Then Exit Sub

        If Me.FileDataSet.HasChanges Then
            If MessageBox.Show("Do you want to discard your changes?", "Discard Changes", MessageBoxButtons.YesNo) = _
                Windows.Forms.DialogResult.No Then
                Exit Sub
            End If
        End If

        Me.FileDataSet.Clear()
        Me.FileDataSet.AcceptChanges()

        m_strCurrentFile = ""
    End Sub

    Private Sub SaveAsToolStripMenuItem_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles SaveAsToolStripMenuItem.Click
        If m_strCurrentFile = "" Then Exit Sub

        Dim strFileName As String = ""
        Dim dlgSave As New SaveFileDialog

        With dlgSave
            .AddExtension = True
            .DefaultExt = ".ttd"
            .Filter = "Tip Tracker Data Files (*.ttd)|*.ttd"
            .InitialDirectory = My.Computer.FileSystem.SpecialDirectories.MyDocuments
            .OverwritePrompt = True
            .RestoreDirectory = True
            .SupportMultiDottedExtensions = True
            .Title = "Save File"
        End With

        If dlgSave.ShowDialog <> Windows.Forms.DialogResult.OK Then
            dlgSave.Dispose()
            Exit Sub
        End If

        strFileName = dlgSave.FileName

        Try
            Dim objFileEncoder As New clsFileEncoder
            Dim objDSStream As New MemoryStream

            Me.FileDataSet.WriteXml(objDSStream)

            objFileEncoder.EncodeFile(strFileName, objDSStream)

            objFileEncoder.Dispose()
            objDSStream.Close()
            objDSStream.Dispose()

            Me.FileDataSet.AcceptChanges()
        Catch ex As Exception
            MessageBox.Show("Could not save file.", "Error", MessageBoxButtons.OK)
            Exit Sub
        End Try

        m_strCurrentFile = strFileName
    End Sub

    Private Sub frmDataFile_FormClosing(ByVal sender As Object, ByVal e As System.Windows.Forms.FormClosingEventArgs) Handles Me.FormClosing
        If Me.FileDataSet.HasChanges Then
            Select Case MessageBox.Show("Do you want to discard your changes?", "Discard Changes", MessageBoxButtons.YesNoCancel)
                Case Windows.Forms.DialogResult.No
                    Try
                        Dim objFileEncoder As New clsFileEncoder
                        Dim objDSStream As New MemoryStream

                        Me.FileDataSet.WriteXml(objDSStream)

                        objFileEncoder.EncodeFile(m_strCurrentFile, objDSStream)

                        objFileEncoder.Dispose()
                        objDSStream.Close()
                        objDSStream.Dispose()
                        Me.FileDataSet.AcceptChanges()
                    Catch ex As Exception
                        MessageBox.Show("Could not save file." & vbCrLf & vbCrLf & ex.ToString, "Error", MessageBoxButtons.OK)
                        Exit Sub
                    End Try
                Case Windows.Forms.DialogResult.Yes
                    Exit Sub
                Case Windows.Forms.DialogResult.Cancel
                    e.Cancel = True
                    Exit Sub
            End Select
        End If
    End Sub

    Private Sub ViewErrorsToolStripMenuItem_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles ViewErrorsToolStripMenuItem.Click
        If Me.m_strCurrentFile = "" Then Exit Sub

        If Me.FileDataSet.HasErrors = False Then
            MsgBox("No errors were found.", MsgBoxStyle.OkOnly, "No Errors")
            Exit Sub
        End If

        Dim strServers As String = ""
        Dim strTips As String = ""
        Dim strSettings As String = ""
        Dim strFunctions As String = ""

        If Me.FileDataSet.Servers.HasErrors = False Then
            strServers = "No errors found."
        Else
            For Each row As DataRow In Me.FileDataSet.Servers
                If row.HasErrors Then
                    strServers += vbCrLf & "Server Number: " & row("ServerNumber").ToString
                    strServers += vbCrLf & "First Name: " & row("FirstName").ToString
                    strServers += vbCrLf & "Last Name: " & row("LastName").ToString
                    strServers += vbCrLf
                End If
            Next
        End If

        If Me.FileDataSet.Tips.HasErrors = False Then
            strTips = "No errors found."
        Else
            For Each row As DataRow In Me.FileDataSet.Tips
                If row.HasErrors Then
                    strTips += vbCrLf & "Tip ID: " & row("TipID").ToString
                    strTips += vbCrLf & "Amount: " & row("Amount").ToString
                    strTips += vbCrLf & "Server Number: " & row("ServerNumber").ToString
                    strTips += vbCrLf & "First Name: " & row("FirstName").ToString
                    strTips += vbCrLf & "Last Name: " & row("LastName").ToString
                    strTips += vbCrLf & "Description: " & row("Description").ToString
                    strTips += vbCrLf & "Function: " & row("SpecialFunction").ToString
                    strTips += vbCrLf & "Working Date: " & row("WorkingDate").ToString
                    strTips += vbCrLf
                End If
            Next
        End If

        If Me.FileDataSet.Settings.HasErrors = False Then
            strSettings = "No errors found."
        Else
            For Each row As DataRow In Me.FileDataSet.Settings
                If row.HasErrors Then
                    strSettings += vbCrLf & "Setting: " & row("Setting").ToString
                    strSettings += vbCrLf & "Value: " & row("Value").ToString
                    strSettings += vbCrLf
                End If
            Next
        End If

        If Me.FileDataSet.SpecialFunctions.HasErrors = False Then
            strFunctions = "No errors found."
        Else
            For Each row As DataRow In Me.FileDataSet.SpecialFunctions
                If row.HasErrors Then
                    strFunctions += vbCrLf & "Function: " & row("SpecialFunction").ToString
                    strFunctions += vbCrLf & "Date: " & row("Date").ToString
                    strFunctions += vbCrLf
                End If
            Next
        End If

        With frmErrors
            .TextBox1.Text += "Servers Table:" & vbCrLf & strServers
            .TextBox1.Text += vbCrLf & vbCrLf & "Tips Table: " & vbCrLf & strTips
            .TextBox1.Text += vbCrLf & vbCrLf & "Settings Table: " & vbCrLf & strSettings
            .TextBox1.Text += vbCrLf & vbCrLf & "Functions Table: " & vbCrLf & strFunctions

            .ShowDialog()
            .Dispose()
        End With



    End Sub

    Private Sub frmDataFile_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
        Dim strFileName As String = ""
        Dim dlgOpen As New OpenFileDialog

        With dlgOpen
            .CheckFileExists = True
            .CheckPathExists = True
            .DefaultExt = ".ttd"
            .Filter = "Tip Tracker Data Files (*.ttd)|*.ttd"
            .InitialDirectory = My.Computer.FileSystem.SpecialDirectories.MyDocuments
            .RestoreDirectory = True
            .Title = "Open Data File"
        End With

        If dlgOpen.ShowDialog <> Windows.Forms.DialogResult.OK Then
            dlgOpen.Dispose()
            Me.Close()
            Exit Sub
        End If

        If Me.FileDataSet.HasChanges Then
            If MessageBox.Show("Do you want to discard your changes?", "Discard Changes", MessageBoxButtons.YesNo) = _
                Windows.Forms.DialogResult.No Then
                Exit Sub
            End If
        End If

        Me.FileDataSet.Clear()
        Me.FileDataSet.AcceptChanges()

        strFileName = dlgOpen.FileName
        m_strCurrentFile = strFileName

        dlgOpen.Dispose()

        Try
            Dim objFileDecoder As New clsFileEncoder
            Dim objDSStream As New MemoryStream

            objDSStream = objFileDecoder.DecodeFile(strFileName)

            objDSStream.Flush()
            objDSStream.Seek(0, SeekOrigin.Begin)

            Me.FileDataSet.ReadXml(objDSStream)

            objFileDecoder.Dispose()
            objDSStream.Close()
            objDSStream.Dispose()

            Me.FileDataSet.AcceptChanges()
        Catch ex As Exception
            MsgBox(ex.ToString, MsgBoxStyle.OkOnly, "Error")
        End Try
    End Sub
End Class
