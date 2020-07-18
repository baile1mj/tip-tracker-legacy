﻿Imports System.IO

Namespace Utilities
    ''' <summary>
    ''' Provides methods for reading and writing pay period data files.
    ''' </summary>
    Public Class PayPeriodFile
        Inherits ObfuscatedDataSetFile(Of FileDataSet)
        Implements IDisposable

        Private _fileStream As FileStream

        ''' <summary>
        ''' Creates a new instance of the pay period data file class.
        ''' </summary>
        ''' <param name="filePath">The path to the file in the file system.</param>
        Public Sub New(ByVal filePath As String)
            MyBase.New(filePath)
        End Sub

        ''' <summary>
        ''' Opens the file with exclusive access.
        ''' </summary>
        Public Sub Open()
            _fileStream = File.Open(FilePath, FileMode.OpenOrCreate, FileAccess.ReadWrite, FileShare.Read)
        End Sub

        ''' <summary>
        ''' Closes the file and releases the exclusive lock.
        ''' </summary>
        Public Sub Close()
            _fileStream?.Close()
        End Sub

        ''' <summary>
        ''' Reads the data from the file.
        ''' </summary>
        ''' <returns>The pay period data contained in the file.</returns>
        Public Function ReadPayPeriodFile() As PayPeriodData
            If IsNothing(_fileStream) Then
                Throw New InvalidOperationException("The file has not been opened for reading.")
            End If

            Dim dataSet As FileDataSet = Read(_fileStream)
            Return PayPeriodData.Create(dataSet)
        End Function

        ''' <summary>
        ''' Writes pay period data to the file.
        ''' </summary>
        ''' <param name="payPeriodData">The data to write.</param>
        Public Overloads Sub Write(ByVal payPeriodData As PayPeriodData)
            Write(payPeriodData.FileDataSet, _fileStream)
            payPeriodData.FileDataSet.AcceptChanges()
        End Sub

        ''' <inheritdoc />
        Public Sub Dispose() Implements IDisposable.Dispose
            _fileStream?.Close()
            _fileStream?.Dispose()
        End Sub
    End Class
End Namespace