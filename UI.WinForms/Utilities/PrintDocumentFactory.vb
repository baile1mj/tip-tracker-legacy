Imports System.Drawing.Imaging
Imports System.Drawing.Printing
Imports System.IO
Imports System.Text
Imports Microsoft.Reporting.WinForms

''' <summary>
''' Provides functionality for building a <see cref="PrintDocument"/> instance from a <see cref="Report"/> instance.
''' </summary>
Public Class PrintDocumentFactory
    Implements IDisposable

    Private _currentPageIndex As Integer = 0
    Private ReadOnly _pageImages As New List(Of Stream)
    Private ReadOnly _report As LocalReport
    
    ''' <summary>
    ''' Creates a new factory instance for a specified report.
    ''' </summary>
    ''' <param name="report">The report from which a printable document will be created.</param>
    Public Sub New(report As LocalReport) 
        _report = report
    End Sub

    ''' <summary>
    ''' Builds the printable document from the report.
    ''' </summary>
    ''' <param name="documentName">The name to give the document.</param>
    ''' <returns>The PrintDocument representation of the report.</returns>
    Public Function BuildPrintDocument(documentName As String) As PrintDocument
        Dim deviceInfo = "
<DeviceInfo>
    <OutputFormat>EMF</OutputFormat>
</DeviceInfo>"
        Dim warnings As Warning() = Nothing

        _report.Render("Image", deviceInfo, AddressOf CreateStream, warnings)

        Dim document As New PrintDocument()
        document.DocumentName = documentName

        AddHandler document.PrintPage, AddressOf PrintPage
        
        Return document
    End Function

    ''' <summary>
    ''' A callback method that will be invoked when the report images are rendered.  Each page will be rendered as a separate stream.
    ''' </summary>
    ''' <param name="name">The name of the stream.</param>
    ''' <param name="fileNameExtension">The file name extension to use if a file stream is being created.</param>
    ''' <param name="encoding">An Encoding enumerator value specifying the character encoding of the stream. This may be null
    ''' if the stream does not contain characters.</param>
    ''' <param name="mimeType">A string containing the MIME type of the stream.</param>
    ''' <param name="willSeek">A Boolean value indicated whether the stream needs to support seeking. If the value is false,
    ''' the stream will be forward-only and will be sent to the client in chunks as it is created. If the value is true, the stream
    ''' may be written in any order.</param>
    ''' <returns>A stream that will be returned to the report rendering engine that contains a single page of the document.</returns>
    Private Function CreateStream(name As String, fileNameExtension As String, encoding As Encoding, mimeType As String, willSeek As Boolean) As Stream
        Dim stream As New MemoryStream()
        _pageImages.Add(stream)

        Return stream
    End Function

    ''' <summary>
    ''' A delegate method that will handle the PrintDocument's PrintPage event.
    ''' </summary>
    ''' <param name="sender">The object triggering the event.</param>
    ''' <param name="e">Arguments associated with the event.</param>
    Private Sub PrintPage(sender As Object, e As PrintPageEventArgs)
        If _currentPageIndex >= _pageImages.Count Then
            _currentPageIndex = 0
            e.HasMorePages = True
        End If

        Dim currentPage = _pageImages(_currentPageIndex)
        currentPage.Seek(0, SeekOrigin.Begin)

        Dim pageImage As New Metafile(currentPage)
        Dim adjustedRectangle = New Rectangle(
            e.PageBounds.Left - CInt(e.PageSettings.HardMarginX), 
            e.PageBounds.Top - CInt(e.PageSettings.HardMarginY),
            e.PageBounds.Width,
            e.PageBounds.Height)

        e.Graphics.DrawImage(pageImage, adjustedRectangle)

        _currentPageIndex += 1
        e.HasMorePages = _currentPageIndex < _pageImages.Count
    End Sub

    ''' <inheritdoc />
    Public Sub Dispose() Implements IDisposable.Dispose
        For Each stream In _pageImages 
            stream.Close()
            stream.Dispose()
        Next
    End Sub
End Class
