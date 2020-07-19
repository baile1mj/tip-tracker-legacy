Imports System.Text

Namespace Data
    ''' <summary>
    ''' Provides a method for building a message about errors contained in a DataTable.
    ''' </summary>
    Public Class DataTableErrorMessageBuilder
        Private ReadOnly _table As DataTable

        ''' <summary>
        ''' Creates a new instance of the builder class.
        ''' </summary>
        ''' <param name="dataTable">The data table for which the message will be generated.</param>
        Public Sub New(ByVal dataTable As DataTable)
            If IsNothing(dataTable) Then _
                Throw New ArgumentNullException(NameOf(DataSet), "Cannot determine errors from null table.")

            _table = dataTable
        End Sub

        ''' <summary>
        ''' Gets the error message for the DataTable.
        ''' </summary>
        ''' <returns>A string containing any errors present in the table.</returns>
        Public Function GetErrorMessage() As String
            Dim messageBuilder As New StringBuilder()

            messageBuilder.AppendLine(BuildErrorMessageHeader())
            messageBuilder.AppendLine(BuildRowErrors())

            Return messageBuilder.ToString()
        End Function

        ''' <summary>
        ''' Builds a header for the message.
        ''' </summary>
        ''' <returns>The error message header.</returns>
        Private Function BuildErrorMessageHeader() As String
            Dim headerBuilder As New StringBuilder()

            headerBuilder.AppendLine(_table.TableName)
            headerBuilder.AppendLine("==============================")

            Return headerBuilder.ToString().Trim()
        End Function

        ''' <summary>
        ''' Builds the error message for any rows containing errors.
        ''' </summary>
        ''' <returns>The row errors.</returns>
        Private Function BuildRowErrors() As String
            If Not _table.HasErrors Then Return "No errors"

            Dim rowErrorBuilder As New StringBuilder()

            For Each row As DataRow In _table.Rows
                If Not row.HasErrors Then Continue For

                rowErrorBuilder.AppendLine($"Error: {row.RowError}")

                For Each column As DataColumn In _table.Columns
                    Dim columnValue As Object = row(column.ColumnName)

                    If IsDBNull(columnValue) Then columnValue = "NULL"

                    rowErrorBuilder.AppendLine($"{vbTab}{column.ColumnName}: {columnValue}")
                Next

                rowErrorBuilder.AppendLine()
            Next

            Return rowErrorBuilder.ToString().TrimEnd()
        End Function
    End Class

End Namespace