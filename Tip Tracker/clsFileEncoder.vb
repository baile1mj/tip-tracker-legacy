Imports System.IO
Imports System.Text

Public Class clsFileEncoder
    Implements IDisposable

    Private m_decodedStream As New MemoryStream

    Private Function LegacyEncodeString(ByVal StringToEncode As String) As String
        Windows.Forms.Cursor.Current = Cursors.WaitCursor
        Dim strEncodedString As String = ""

        For Each c As Char In StringToEncode
            strEncodedString += ChrW(Asc(c) + 300)
        Next

        Return strEncodedString
        Windows.Forms.Cursor.Current = Cursors.Default
    End Function

    Private Function LegacyDecodeString(ByVal StringToDecode As String) As String
        Windows.Forms.Cursor.Current = Cursors.WaitCursor
        Dim strDecodedString As String = ""

        For Each c As Char In StringToDecode
            strDecodedString += Chr(AscW(c) - 300)
        Next

        Return strDecodedString
        Windows.Forms.Cursor.Current = Cursors.Default
    End Function

    Friend Sub LegacyEncodeFile(ByVal FilePath As String, ByVal Stream As MemoryStream)
        Dim objStreamReader As New StreamReader(Stream)

        Stream.Flush()
        Stream.Seek(0, SeekOrigin.Begin)

        Dim strFileContents As String = objStreamReader.ReadToEnd

        objStreamReader.Close()
        objStreamReader.Dispose()

        Stream.Close()
        Stream.Dispose()

        Dim objStreamWriter As New StreamWriter(FilePath)
        Dim strEncodedContents As String = Me.LegacyEncodeString(strFileContents)

        objStreamWriter.Write(strEncodedContents)

        objStreamWriter.Flush()
        objStreamWriter.Close()
        objStreamWriter.Dispose()
    End Sub

    Friend Function LegacyDecodeFile(ByVal FilePath As String) As MemoryStream
        Dim objStreamReader As New StreamReader(FilePath)

        Dim strEncodedContents As String = objStreamReader.ReadToEnd

        objStreamReader.Close()
        objStreamReader.Dispose()

        Dim strFileContents As String = Me.LegacyDecodeString(strEncodedContents)

        Dim objStreamWriter As New StreamWriter(m_decodedStream)

        objStreamWriter.Write(strFileContents)
        objStreamWriter.Flush()

        m_decodedStream.Flush()
        m_decodedStream.Seek(0, SeekOrigin.Begin)

        Return m_decodedStream
    End Function

    Private Function EncodeString(ByVal StringToEncode As String) As String
        Dim bytes() As Byte
        Dim strEncodedString As String

        bytes = ASCIIEncoding.ASCII.GetBytes(StringToEncode)
        strEncodedString = Convert.ToBase64String(bytes)

        Return strEncodedString
    End Function

    Private Function DecodeString(ByVal StringToDecode As String) As String
        Dim bytes() As Byte
        Dim strDecodedString As String

        bytes = Convert.FromBase64String(StringToDecode)
        strDecodedString = ASCIIEncoding.ASCII.GetString(bytes)

        Return strDecodedString
    End Function

    Friend Sub EncodeFile(ByVal FilePath As String, ByVal Stream As MemoryStream)
        Dim objStreamReader As New StreamReader(Stream)

        Stream.Flush()
        Stream.Seek(0, SeekOrigin.Begin)

        Dim strFileContents As String = objStreamReader.ReadToEnd

        objStreamReader.Close()
        objStreamReader.Dispose()

        Stream.Close()
        Stream.Dispose()

        Dim objStreamWriter As New StreamWriter(FilePath)
        Dim strEncodedContents As String = Me.EncodeString(strFileContents)

        objStreamWriter.Write(strEncodedContents)

        objStreamWriter.Flush()
        objStreamWriter.Close()
        objStreamWriter.Dispose()
    End Sub

    Friend Function DecodeFile(ByVal FilePath As String) As MemoryStream
        Dim objStreamReader As New StreamReader(FilePath)

        Dim strEncodedContents As String = objStreamReader.ReadToEnd

        objStreamReader.Close()
        objStreamReader.Dispose()

        Dim strFileContents As String = Me.DecodeString(strEncodedContents)

        Dim objStreamWriter As New StreamWriter(m_decodedStream)

        objStreamWriter.Write(strFileContents)
        objStreamWriter.Flush()

        m_decodedStream.Flush()
        m_decodedStream.Seek(0, SeekOrigin.Begin)

        Return m_decodedStream
    End Function

    Friend Sub Dispose() Implements IDisposable.Dispose
        m_decodedStream.Close()
        m_decodedStream.Dispose()
        m_decodedStream = Nothing
    End Sub

End Class
