Imports System.Text

Namespace Data
    ''' <summary>
    ''' Provides functionality for obfuscating and de-obfuscating data files.
    ''' </summary>
    ''' <remarks>This class provides de-obfuscation methods for both the current and legacy schemes.</remarks>
    Public Class FileObfuscator

        ''' <summary>
        ''' Obfuscates text contained in a byte array.
        ''' </summary>
        ''' <param name="text">The text to obfuscate.</param>
        ''' <returns>A string containing the obfuscated text.</returns>
        Public Function Obfuscate(ByVal text As Byte()) As String
            Return Convert.ToBase64String(text)
        End Function

        ''' <summary>
        ''' De-obfuscates obfuscated text.
        ''' </summary>
        ''' <param name="text">The text to de-obfuscate.</param>
        ''' <returns>A byte array containing the de-obfuscated text.</returns>
        Public Function DeObfuscate(ByVal text As String) As Byte()
            Return Convert.FromBase64String(text)
        End Function

        ''' <summary>
        ''' De-obfuscates obfuscated text using the legacy obfuscation scheme.
        ''' </summary>
        ''' <param name="text">The text to de-obfuscate.</param>
        ''' <returns>A byte array containing the de-obfuscated text.</returns>
        Public Function LegacyDeObfuscate(ByVal text As String) As Byte()
            'The original obfuscation scheme involved shifting the char code.
            Const CHAR_CODE_OFFSET As Integer = 300

            Dim contentChars As Char() = text.ToCharArray()

            For i As Integer = 0 To contentChars.Length - 1
                'Since legacy encoded files increase the char code, we need to decrease to decode.
                contentChars(i) = Chr(AscW(contentChars(i)) - CHAR_CODE_OFFSET)
            Next

            Return Encoding.ASCII.GetBytes(contentChars)
        End Function
    End Class
End NameSpace