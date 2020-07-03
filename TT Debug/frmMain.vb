Imports System.IO
Imports System.IO.File
Imports Tip_Tracker.Utilities

Public Class frmMain
    Private m_strGlobalFilePath As String = GetGlobalFilePath()

    Private Sub btnExit_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnExit.Click
        Application.Exit()
    End Sub

    Private Sub btnDataFile_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnDataFile.Click
        frmDataFile.Show()
    End Sub

    Private Sub btnGlobalFile_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnGlobalFile.Click
        frmGlobalFile.Show()
    End Sub

    Private Sub btnDecodeFile_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnDecodeFile.Click
        Dim strSourceFile As String = ""
        Dim dlgOpen As New OpenFileDialog

        With dlgOpen
            .CheckFileExists = True
            .CheckPathExists = True
            .DefaultExt = ".ttd"
            .Filter = "Tip Tracker Data File (*.ttd)|*.ttd|Tip Tracker Global File (*.dat)|*.dat"
            .InitialDirectory = My.Computer.FileSystem.SpecialDirectories.MyDocuments
            .RestoreDirectory = True
            .Title = "Select File to Decode"
        End With

        If dlgOpen.ShowDialog <> Windows.Forms.DialogResult.OK Then
            dlgOpen.Dispose()
            Exit Sub
        End If

        strSourceFile = dlgOpen.FileName
        dlgOpen.Dispose()

        Dim strDestinationFile As String = ""
        Dim dlgSave As New SaveFileDialog

        With dlgSave
            .AddExtension = True
            .DefaultExt = ".txt"
            .Filter = "Plain Text File (*.txt)|*.txt"
            .InitialDirectory = My.Computer.FileSystem.SpecialDirectories.MyDocuments
            .OverwritePrompt = True
            .RestoreDirectory = True
            .SupportMultiDottedExtensions = True
            .Title = "Save Decoded File"
        End With

        If dlgSave.ShowDialog <> Windows.Forms.DialogResult.OK Then
            dlgSave.Dispose()
            Exit Sub
        End If

        strDestinationFile = dlgSave.FileName
        dlgSave.Dispose()

        Try
            Dim objEncoder As New clsFileEncoder
            Dim objStreamReader As New StreamReader(strSourceFile)
            Dim objStreamWriter As New StreamWriter(strDestinationFile)

            Dim strEncoded, strDecoded As String

            strEncoded = objStreamReader.ReadToEnd
            strDecoded = objEncoder.DecodeString(strEncoded)

            objStreamWriter.Write(strDecoded)

            objStreamWriter.Flush()
            objStreamWriter.Close()
            objStreamWriter.Dispose()

            objStreamReader.Close()
            objStreamReader.Dispose()

            objEncoder.Dispose()
        Catch ex As Exception
            MessageBox.Show("Could not decode file.  File may already be plain text.", "Error", MessageBoxButtons.OK)
        End Try
    End Sub

    Private Sub btnEncodeFile_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnEncodeFile.Click
        Dim strSourceFile As String = ""
        Dim dlgOpen As New OpenFileDialog

        With dlgOpen
            .CheckFileExists = True
            .CheckPathExists = True
            .DefaultExt = ".txt"
            .Filter = "Plain Text File (*.txt)|*.txt|XML File (*.xml)|*.xml"
            .InitialDirectory = My.Computer.FileSystem.SpecialDirectories.MyDocuments
            .RestoreDirectory = True
            .Title = "Select File to Encode"
        End With

        If dlgOpen.ShowDialog <> Windows.Forms.DialogResult.OK Then
            dlgOpen.Dispose()
            Exit Sub
        End If

        strSourceFile = dlgOpen.FileName
        dlgOpen.Dispose()

        Dim strDestinationFile As String = ""
        Dim dlgSave As New SaveFileDialog

        With dlgSave
            .AddExtension = True
            .DefaultExt = ".ttd"
            .Filter = "Tip Tracker Data File (*.ttd)|*.ttd|Tip Tracker Global File (*.dat)|*.dat"
            .InitialDirectory = My.Computer.FileSystem.SpecialDirectories.MyDocuments
            .OverwritePrompt = True
            .RestoreDirectory = True
            .SupportMultiDottedExtensions = True
            .Title = "Save Encoded File"
        End With

        If dlgSave.ShowDialog <> Windows.Forms.DialogResult.OK Then
            dlgSave.Dispose()
            Exit Sub
        End If

        strDestinationFile = dlgSave.FileName
        dlgSave.Dispose()

        Try
            Dim objEncoder As New clsFileEncoder
            Dim objStreamReader As New StreamReader(strSourceFile)
            Dim objStreamWriter As New StreamWriter(strDestinationFile)

            Dim strEncoded, strDecoded As String

            strDecoded = objStreamReader.ReadToEnd
            strEncoded = objEncoder.EncodeString(strDecoded)

            objStreamWriter.Write(strEncoded)

            objStreamWriter.Flush()
            objStreamWriter.Close()
            objStreamWriter.Dispose()

            objStreamReader.Close()
            objStreamReader.Dispose()

            objEncoder.Dispose()
        Catch ex As Exception
            MessageBox.Show("Could not decode file.  File may already be plain text.", "Error", MessageBoxButtons.OK)
        End Try
    End Sub

    Private Sub btnForceGlobalFileReset_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnForceGlobalFileReset.Click
        If MessageBox.Show("It is recommended that you close all instances of Tip Tracker before continuing.  Please click OK when you are ready to proceed.", "Information", MessageBoxButtons.OKCancel) <> Windows.Forms.DialogResult.OK Then
            Exit Sub
        End If

        Dim globalFilePath As String = MachineSettings.GetGlobalFilePath()

        If Directory.Exists(Dir) Then
            Dim writer As New StreamWriter(globalFilePath, False)

            Try
                writer.Write("")
                writer.Flush()
                writer.Close()
                writer.Dispose()

                MessageBox.Show("The global file path has been removed.  Please run Tip Tracker to configure the global file path.", "Reset Successful", MessageBoxButtons.OK)
            Catch ex As Exception
                MessageBox.Show("Unable to reset the global file path.  You must reset the path manually.", "Cannot Reset Path", MessageBoxButtons.OK, MessageBoxIcon.Error)
            End Try
        Else
            MessageBox.Show("The global file path has not been set.  Please run Tip Tracker to configure the global file path.")
        End If
    End Sub

    Friend Function GetGlobalFilePath() As String
        Dim dir As String = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.CommonApplicationData), "Tip Tracker")
        Const FILE As String = "GlobalFile.location"

        Return Path.Combine(dir, FILE)
    End Function
End Class