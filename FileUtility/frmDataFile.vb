Imports System.IO
Imports TipTracker.Common.Data.PayPeriod

Public Class frmDataFile
    Private ReadOnly _defaultDirectory As String

    Private _payPeriodFile As PayPeriodFile
    Private _payPeriodData As PayPeriodData

    Public Sub New(ByVal defaultDirectory As String)
        InitializeComponent()

        If Directory.Exists(defaultDirectory) Then
            _defaultDirectory = defaultDirectory
        Else
            _defaultDirectory = Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments)
        End If
    End Sub

    Private Sub ExitToolStripMenuItem_Click(ByVal sender As Object, ByVal e As EventArgs) Handles ExitToolStripMenuItem.Click
        Close()
    End Sub

    Private Sub SaveToolStripMenuItem_Click(ByVal sender As Object, ByVal e As EventArgs) Handles SaveToolStripMenuItem.Click
        SaveFile()
    End Sub

    Private Function SaveFile() As Boolean
        Try
            _payPeriodFile.Write(_payPeriodData)
            Return True
        Catch ex As Exception
            MessageBox.Show($"An error occurred while saving the data file: ""{ex.Message}"".",
                "Error Saving File", MessageBoxButtons.OK)
            Return False
        End Try
    End Function

    Private Sub SaveXMLToolStripMenuItem_Click(ByVal sender As Object, ByVal e As EventArgs) Handles SaveXMLToolStripMenuItem.Click
        Dim strFileName As String
        Using dlgSave As New SaveFileDialog() With {
            .AddExtension = True,
            .DefaultExt = ".xml",
            .FileName = Path.GetFileNameWithoutExtension(_payPeriodFile.FilePath),
            .Filter = "XML File (*.xml)|*.xml",
            .InitialDirectory = My.Computer.FileSystem.SpecialDirectories.MyDocuments,
            .OverwritePrompt = True,
            .RestoreDirectory = True,
            .SupportMultiDottedExtensions = True,
            .Title = "Save File"}

            If dlgSave.ShowDialog <> DialogResult.OK Then Exit Sub

            strFileName = dlgSave.FileName
        End Using

        Try
            _payPeriodData.FileDataSet.WriteXml(strFileName)
        Catch ex As Exception
            MessageBox.Show($"An error occurred while saving the XML: ""{ex.Message}"".",
                "Error Saving XML", MessageBoxButtons.OK)
            Exit Sub
        End Try
    End Sub

    Private Sub SaveAsToolStripMenuItem_Click(ByVal sender As Object, ByVal e As EventArgs) Handles SaveAsToolStripMenuItem.Click
        If _payPeriodData.FileDataSet.HasChanges Then
            Dim response As DialogResult = MessageBox.Show("Do you wish to save your changes to the data file?",
                "Save Changes", MessageBoxButtons.YesNoCancel)

            If response = DialogResult.Yes Then
                Dim isSuccess As Boolean = SaveFile()
                If Not isSuccess Then Exit Sub
            ElseIf response = DialogResult.Cancel Then
                Exit Sub
            End If
        End If

        Dim strFileName As String
        Using dlgSave As New SaveFileDialog() With {
            .AddExtension = True,
            .DefaultExt = ".ttd",
            .Filter = "Tip Tracker Data Files (*.ttd)|*.ttd",
            .FileName = $"{Path.GetFileNameWithoutExtension(_payPeriodFile.FilePath)} - Copy",
            .InitialDirectory = My.Computer.FileSystem.SpecialDirectories.MyDocuments,
            .OverwritePrompt = True,
            .RestoreDirectory = True,
            .SupportMultiDottedExtensions = True,
            .Title = "Save File"}

            If dlgSave.ShowDialog <> DialogResult.OK Then Exit Sub

            strFileName = dlgSave.FileName
        End Using

        Try
            Dim dataCopy As PayPeriodData = _payPeriodData.Clone()
            Dim newFile As New PayPeriodFile(strFileName)

            newFile.Open()
            newFile.Write(dataCopy)

            _payPeriodFile.Dispose()
            _payPeriodFile = newFile
            _payPeriodData = _payPeriodData
            Text = _payPeriodFile.FilePath
        Catch ex As Exception
            MessageBox.Show($"An error occurred while saving the file copy: ""{ex.Message}"".",
                "Error", MessageBoxButtons.OK)
            Exit Sub
        End Try
    End Sub

    Private Sub frmDataFile_FormClosing(ByVal sender As Object, ByVal e As FormClosingEventArgs) Handles Me.FormClosing
        If _payPeriodData?.FileDataSet.HasChanges Then
            Dim response As DialogResult = MessageBox.Show("Do you wish to save your changes to the data file?",
                "Save Changes", MessageBoxButtons.YesNoCancel)

            If response = DialogResult.Yes Then
                Dim isSuccess As Boolean = SaveFile()
                e.Cancel = Not isSuccess
            ElseIf response = DialogResult.Cancel Then
                e.Cancel = True
            End If
        End If

        If Not e.Cancel Then _payPeriodFile?.Dispose()
    End Sub

    Private Sub ViewErrorsToolStripMenuItem_Click(ByVal sender As Object, ByVal e As EventArgs) Handles ViewErrorsToolStripMenuItem.Click
        Dim errorMessage As String = _payPeriodData.GetDataSetErrorMessage()

        Using errorViewer As New frmErrors(errorMessage)
            errorViewer.ShowDialog()
        End Using
    End Sub

    Private Sub frmDataFile_Load(ByVal sender As Object, ByVal e As EventArgs) Handles MyBase.Load
        Using dlgOpen As New OpenFileDialog() With {
            .CheckFileExists = True,
            .CheckPathExists = True,
            .DefaultExt = ".ttd",
            .Filter = "Tip Tracker Data Files (*.ttd)|*.ttd",
            .InitialDirectory = _defaultDirectory,
            .RestoreDirectory = True,
            .Title = "Open Data File"}

            If dlgOpen.ShowDialog <> DialogResult.OK Then
                Close()
                Exit Sub
            End If

            _payPeriodFile = New PayPeriodFile(dlgOpen.FileName)
        End Using

        Try
            _payPeriodFile.Open()
            _payPeriodData = _payPeriodFile.ReadPayPeriodFileUnsafe()
            Text = _payPeriodFile.FilePath
        Catch ex As Exception
            MessageBox.Show($"An error occurred while reading the selected file: ""{ex.Message}"".",
            "Error Reading File", MessageBoxButtons.OK, MessageBoxIcon.Error)
            Close()
            Exit Sub
        End Try

        ServersBindingSource.DataSource = _payPeriodData.FileDataSet.Servers
        TipsBindingSource.DataSource = _payPeriodData.FileDataSet.Tips
        SpecialFunctionsBindingSource.DataSource = _payPeriodData.FileDataSet.SpecialFunctions
        SettingsBindingSource.DataSource = _payPeriodData.FileDataSet.Settings
    End Sub
End Class
