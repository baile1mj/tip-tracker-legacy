Imports System.IO
Imports Tip_Tracker.Utilities

Public Class frmConfigurator
    
    Private Property GlobalSettingsFile() As String
        Get
            Return MachineSettings.GetGlobalFilePath()
        End Get
        Set(ByVal value As String)
            MachineSettings.SetGlobalFilePath(value)
        End Set
    End Property

    Private Sub btnOK_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnOK.Click
        If Me.txtGlobalFilePath.Text = "" Then
            MessageBox.Show("You must either create a new global settings file or open an existing one.", "No Global File Set", MessageBoxButtons.OK)
            Exit Sub
        End If

        If Not File.Exists(Me.txtGlobalFilePath.Text) Then
            MessageBox.Show("The file you selected does not exist.  Please create a new file or select an existing one.", "No Global File Set", MessageBoxButtons.OK)
            Exit Sub
        End If

        Me.DialogResult = Windows.Forms.DialogResult.OK
        Me.Close()
    End Sub

    Private Sub btnNew_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnNew.Click
        dlgSaveFile.InitialDirectory = My.Application.Info.DirectoryPath

        If dlgSaveFile.ShowDialog <> Windows.Forms.DialogResult.OK Then
            dlgSaveFile.Dispose()
            Exit Sub
        End If

        txtGlobalFilePath.Text = ""

        Dim strFileToCreate As String = dlgSaveFile.FileName

        Try

            Dim objFileEncoder As New clsFileEncoder
            Dim objDSStream As New MemoryStream

            Me.GlobalDataSet.WriteXml(objDSStream)

            objDSStream.Flush()

            objFileEncoder.EncodeFile(strFileToCreate, objDSStream)

            objDSStream.Close()
            objDSStream.Dispose()

            objFileEncoder.Dispose()
        Catch ex As Exception
            MessageBox.Show("Could not create global settings file.  Contact support", "Error Writing File", MessageBoxButtons.OK, MessageBoxIcon.Warning)
            End
        End Try

        Me.GlobalSettingsFile = strFileToCreate
        txtGlobalFilePath.Text = Me.GlobalSettingsFile
    End Sub

    Private Sub btnExisting_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnExisting.Click
        dlgOpenFile.InitialDirectory = My.Application.Info.DirectoryPath

        If dlgOpenFile.ShowDialog <> Windows.Forms.DialogResult.OK Then
            dlgOpenFile.Dispose()
            Exit Sub
        End If

        txtGlobalFilePath.Text = ""

        Me.GlobalSettingsFile = dlgOpenFile.FileName
        txtGlobalFilePath.Text = Me.GlobalSettingsFile
        dlgOpenFile.Dispose()
    End Sub

    Private Sub btnCancel_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnCancel.Click
        Me.Close()
    End Sub
End Class