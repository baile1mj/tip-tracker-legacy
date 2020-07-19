Imports System.IO

Namespace Data
    ''' <summary>
    ''' Provides base functionality for reading and writing data sets stored as obfuscated files.
    ''' </summary>
    ''' <typeparam name="T">The type of data set stored as a file.</typeparam>
    Public MustInherit Class ObfuscatedDataSetFile(Of T As {DataSet, New})
        ''' <summary>
        ''' Gets the path to the data set file in the file system.
        ''' </summary>
        Public ReadOnly FilePath As String

        ''' <summary>
        ''' Creates a new instance of the file class.
        ''' </summary>
        ''' <param name="filePath">The path to the file.</param>
        Protected Sub New(ByVal filePath As String)
            If String.IsNullOrEmpty(filePath) Then
                Throw New ArgumentException("The file path cannot be empty or null.", NameOf(filePath))
            End If

            Me.FilePath = filePath
        End Sub

        ''' <summary>
        ''' Gets a value indicating whether the file exists.
        ''' </summary>
        ''' <returns></returns>
        Public Function Exists() As Boolean
            Return File.Exists(FilePath)
        End Function

        ''' <summary>
        ''' Writes the data set to the file.
        ''' </summary>
        ''' <param name="dataSet">The data set to write.</param>
        Protected Sub Write(ByVal dataSet As T)
            Dim obfuscator As New FileObfuscator()
            Dim dataStream As New MemoryStream()

            dataSet.WriteXml(dataStream)
            dataStream.Seek(0, SeekOrigin.Begin)

            Dim fileContents As String = obfuscator.Obfuscate(dataStream.ToArray())

            File.WriteAllText(FilePath, fileContents)
        End Sub

        ''' <summary>
        ''' Writes the data set to the file using a provided file stream.
        ''' </summary>
        ''' <param name="dataSet">The data set to write.</param>
        ''' <param name="fileStream">The file stream to use.</param>
        Protected Sub Write(ByVal dataSet As T, ByVal fileStream As FileStream)
            Dim obfuscator As New FileObfuscator()
            Dim dataStream As New MemoryStream()

            dataSet.WriteXml(dataStream)
            dataStream.Seek(0, SeekOrigin.Begin)

            Dim fileContents As String = obfuscator.Obfuscate(dataStream.ToArray())
            Dim streamWriter As New StreamWriter(fileStream)

            fileStream.Seek(0, 0)
            streamWriter.Write(fileContents)
            streamWriter.Flush()
        End Sub

        ''' <summary>
        ''' Reads the data set from the file.
        ''' </summary>
        ''' <returns>The data set contained in the file.</returns>
        Protected Function Read() As T
            Using fileStream As FileStream = File.OpenRead(FilePath)
                Return Read(fileStream)
            End Using
        End Function

        ''' <summary>
        ''' Reads the data set from the file.
        ''' </summary>
        ''' <param name="fileStream">The file stream to use for reading the file.</param>
        ''' <param name="unsafe">True to turn off constraint checking; otherwise, false (default).</param>
        ''' <returns>The data set contained in the file.</returns>
        Protected Function Read(fileStream As FileStream, Optional ByVal unsafe As Boolean = False) As T
            Dim reader As New StreamReader(fileStream)
            Dim fileContents As String = reader.ReadToEnd()
            Dim obfuscator As New FileObfuscator()
            Dim contentBytes As Byte()

            'We should be able to de-obfuscate the file, but we'll fall back to the legacy scheme if that fails.
            Try
                contentBytes = obfuscator.DeObfuscate(fileContents)
            Catch exception As Exception
                Try
                    contentBytes = obfuscator.LegacyDeObfuscate(fileContents)
                Catch legacyException As Exception
                    'If all de-obfuscation attempts fail, return a null object and let the caller figure out what to do.
                    Return Nothing
                End Try
            End Try

            Dim dataStream As New MemoryStream(contentBytes)
            Dim dataSet As New T()

            Try
                dataSet.ReadXml(dataStream)
            Catch ex As Exception
                If Not unsafe Then Throw ex
            End Try

            Return dataSet
        End Function

        ''' <summary>
        ''' Gets a value indicating whether the current state of the data set matches the
        ''' contents of the file.
        ''' </summary>
        ''' <param name="dataSet">The data set in its current state.</param>
        ''' <returns>True if the data set matches the file; otherwise, false.</returns>
        Public Function IsChanged(ByVal dataSet As T) As Boolean
            Dim obfuscator As New FileObfuscator()
            Dim dataStream As New MemoryStream()

            dataSet.WriteXml(dataStream)
            dataStream.Seek(0, SeekOrigin.Begin)

            Dim settingsContents As String = obfuscator.Obfuscate(dataStream.ToArray())
            Dim fileContents As String = File.ReadAllText(FilePath)

            Return Not settingsContents.Equals(fileContents)
        End Function
    End Class
End Namespace