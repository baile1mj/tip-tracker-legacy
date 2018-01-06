Module mdlEncoding

    Friend Function EncodeString(ByVal StringToEncode As String) As String
        Windows.Forms.Cursor.Current = Cursors.WaitCursor
        Dim strEncodedString As String = ""

        For Each c As Char In StringToEncode
            For i As Integer = 0 To 255
                If c = Chr(i) Then
                    strEncodedString += ChrW(i + 300)
                    Exit For
                End If
            Next
        Next

        Return strEncodedString
        Windows.Forms.Cursor.Current = Cursors.Default
    End Function

    Friend Function DecodeString(ByVal StringToDecode As String) As String
        Windows.Forms.Cursor.Current = Cursors.WaitCursor
        Dim strDecodedString As String = ""

        For Each c As Char In StringToDecode
            For i As Integer = 300 To 555
                If c = ChrW(i) Then
                    strDecodedString += Chr(i - 300)
                    Exit For
                End If
            Next
        Next
        Return strDecodedString
        Windows.Forms.Cursor.Current = Cursors.Default
    End Function
End Module
