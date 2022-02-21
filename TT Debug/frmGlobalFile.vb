Imports System.IO

Public Class frmGlobalFile
    Private m_strPath As String = ""

    Private Sub frmGlobalFile_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
        Dim globalPath As String = frmMain.GetGlobalFilePath()

        If Directory.Exists(Path.GetDirectoryName(globalPath)) Then
            ' Try
            Dim reader As StreamReader = New StreamReader(globalPath)

            Me.m_strPath = reader.ReadToEnd().Trim()

            reader.Close()
            reader.Dispose()

            Dim objFileEncoder As New clsFileEncoder

            Me.GlobalDataSet.ReadXml(objFileEncoder.DecodeFile(m_strPath))

            objFileEncoder.Dispose()
            objFileEncoder = Nothing

            Me.GlobalDataSet.AcceptChanges()
            'Catch ex As Exception
            'MessageBox.Show("Cannot load global settings file.  File may be corrupt or its contents may have been changed.", "Error Loading File", MessageBoxButtons.OK, MessageBoxIcon.Error)
            ' Me.Close()
            ' End Try
        Else
            MessageBox.Show("Could not find global file.  Run Tip Tracker to create a new file.", "File Not Found", MessageBoxButtons.OK, MessageBoxIcon.Error)
        End If
    End Sub

    Private Sub ExitToolStripMenuItem_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles ExitToolStripMenuItem.Click
        Me.Close()
    End Sub

    Private Sub SaveToolStripMenuItem_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles SaveToolStripMenuItem.Click
        Try
            Dim objFileEncoder As New clsFileEncoder
            Dim objDSStream As New MemoryStream

            Me.GlobalDataSet.WriteXml(objDSStream)

            objFileEncoder.EncodeFile(m_strPath, objDSStream)

            objFileEncoder.Dispose()
            objDSStream.Close()
            objDSStream.Dispose()
            Me.GlobalDataSet.AcceptChanges()
        Catch ex As Exception
            MessageBox.Show("Could not save file.", "Error", MessageBoxButtons.OK)
            Exit Sub
        End Try
    End Sub

    Private Sub SaveXMLToolStripMenuItem_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles SaveXMLToolStripMenuItem.Click
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
            Me.GlobalDataSet.WriteXml(strFileName)
        Catch ex As Exception
            MessageBox.Show("Could not save XML.", "Error", MessageBoxButtons.OK)
            Exit Sub
        End Try
    End Sub

    Private Sub frmDataFile_FormClosing(ByVal sender As Object, ByVal e As System.Windows.Forms.FormClosingEventArgs) Handles Me.FormClosing
        If Me.GlobalDataSet.HasChanges Then
            Select Case MessageBox.Show("Do you want to discard your changes?", "Discard Changes", MessageBoxButtons.YesNoCancel)
                Case Windows.Forms.DialogResult.No
                    Try
                        Dim objFileEncoder As New clsFileEncoder
                        Dim objDSStream As New MemoryStream

                        Me.GlobalDataSet.WriteXml(objDSStream)

                        objFileEncoder.EncodeFile(m_strPath, objDSStream)

                        objFileEncoder.Dispose()
                        objDSStream.Close()
                        objDSStream.Dispose()
                        Me.GlobalDataSet.AcceptChanges()
                    Catch ex As Exception
                        MessageBox.Show("Could not save file.", "Error", MessageBoxButtons.OK)
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

End Class
