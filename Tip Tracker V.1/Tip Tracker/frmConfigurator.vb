Imports System.IO

Public Class frmConfigurator

    Private Property GlobalSettingsFile() As String
        Get
            Return My.Computer.Registry.GetValue("HKEY_LOCAL_MACHINE\Software\MJB\TipTracker\", "GlobalFile", "").ToString
        End Get
        Set(ByVal value As String)
            My.Computer.Registry.SetValue("HKEY_LOCAL_MACHINE\Software\MJB\TipTracker\", "GlobalFile", value)
        End Set
    End Property

    Private Sub btnClose_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnClose.Click
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
        btnClose.Enabled = True
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
        btnClose.Enabled = True
        dlgOpenFile.Dispose()
    End Sub

End Class