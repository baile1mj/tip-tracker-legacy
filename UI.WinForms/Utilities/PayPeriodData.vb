Imports System.Collections.ObjectModel

Namespace Utilities

    ''' <summary>
    ''' Contains the tip data for a particular pay period.
    ''' </summary>
    Public Class PayPeriodData

        Private ReadOnly _warnings As New List(Of String)

        ''' <summary>
        ''' Gets the data set containing the data from the file.
        ''' </summary>
        Public ReadOnly Property FileDataSet As FileDataSet

        ''' <summary>
        ''' Gets the collection of warnings that were generated when the settings were loaded.
        ''' </summary>
        ''' <returns></returns>
        Public Function GetWarnings() As ReadOnlyCollection(Of String)
            Return _warnings.AsReadOnly()
        End Function

        ''' <summary>
        ''' Creates a new instance of the data class.
        ''' </summary>
        ''' <param name="dataSet">The data set containing the data for the pay period.</param>
        Private Sub New(ByVal dataSet As FileDataSet)
            'Since the application manages tips by pay period, the period must be defined.
            Dim periodStart, periodEnd As DateTime
            Dim hasPeriodStart As Boolean = DateTime.TryParse(dataSet.Settings.FindBySetting("PeriodStart")("Value").ToString(), periodStart)
            Dim hasPeriodEnd As Boolean = DateTime.TryParse(dataSet.Settings.FindBySetting("PeriodEnd")("Value").ToString(), periodEnd)

            If Not hasPeriodStart Or Not hasPeriodEnd Then Throw New Exception("Pay period start and/or end date is not defined.")

            'We can recover from a missing working date, so if it's not in the file, just set a default value.
            Dim workingDate As DateTime

            If Not DateTime.TryParse(dataSet.Settings.FindBySetting("WorkingDate")("Value").ToString(), workingDate) Then
                _warnings.Add("The working date was not defined.  Valued defaulted to pay period start.")
            End If

            'We can also recover from tip and functions outside the pay period dates, so we'll just adjust the dates if we need to.
            Dim recordDate As DateTime
            Dim hasRecordOutsidePeriod As Boolean = False

            For Each row As DataRow In dataSet.SpecialFunctions.Rows
                recordDate = DateTime.Parse(row("Date").ToString())

                If recordDate < periodStart Then
                    row("Date") = periodStart
                    hasRecordOutsidePeriod = True
                ElseIf recordDate > periodEnd Then
                    row("Date") = periodEnd
                    hasRecordOutsidePeriod = True
                End If
            Next

            If hasRecordOutsidePeriod Then
                _warnings.Add("Encountered one or more functions outside the pay period date range.  Functions before " &
                    "the pay period start were moved to the first day of the pay period and functions after the pay period " &
                    "end were moved to the last day of the pay period.")
            End If

            hasRecordOutsidePeriod = False

            For Each row As DataRow In dataSet.Tips.Rows
                recordDate = DateTime.Parse(row("WorkingDate").ToString())

                If recordDate < periodStart Then
                    row("WorkingDate") = periodStart
                    hasRecordOutsidePeriod = True
                ElseIf recordDate > periodEnd Then
                    row("WorkingDate") = periodEnd
                    hasRecordOutsidePeriod = True
                End If
            Next

            If hasRecordOutsidePeriod Then
                _warnings.Add("Encountered one or more tips outside the pay period date range.  Tips before " &
                    "the pay period start were moved to the first day of the pay period and tips after the pay period " &
                    "end were moved to the last day of the pay period.")
            End If

            For Each row As DataRow In dataSet.Servers.Rows
                'Make sure "Suppress Chit" has a value.
                If IsDBNull(row("SuppressChit")) Then row("SuppressChit") = False
            Next

            FileDataSet = dataSet
        End Sub

        ''' <summary>
        ''' Creates a new instance of the <see cref="PayPeriodData"/> class from a populated data set.
        ''' </summary>
        ''' <param name="dataSet">The data set containing the data for the pay period.</param>
        ''' <returns>An object instance containing the pay period data.</returns>
        Public Shared Function Create(ByVal dataSet As FileDataSet) As PayPeriodData
            If IsNothing(dataSet) Then
                Throw New ArgumentException("The file data set cannot be null.", NameOf(dataSet))
            End If

            Return New PayPeriodData(dataSet)
        End Function

        ''' <summary>
        ''' Creates a new instance of the <see cref="PayPeriodData"/> with no tip or function data.
        ''' </summary>
        ''' <param name="payPeriodStart">The start date of the pay period.</param>
        ''' <param name="payPeriodEnd">The end date of the pay period.</param>
        ''' <param name="servers">The table of servers who can be assigned tips</param>
        ''' <returns>An object instance containing the pay period data.</returns>
        Public Shared Function Create(ByVal payPeriodStart As DateTime, ByVal payPeriodEnd As DateTime, ByVal servers As DataTable) As PayPeriodData
            If payPeriodStart > payPeriodEnd Then
                Throw New ArgumentException("The pay period start date must be on or before the end date.", NameOf(payPeriodStart))
            End If

            Dim dataSet As New FileDataSet()

            dataSet.Settings.Rows.Add("PeriodStart", payPeriodStart)
            dataSet.Settings.Rows.Add("PeriodEnd", payPeriodEnd)
            dataSet.Settings.Rows.Add("WorkingDate", payPeriodStart)

            If Not IsNothing(servers) Then
                dataSet.Servers.Merge(servers)
            End If

            Return New PayPeriodData(dataSet)
        End Function

        ''' <summary>
        ''' Creates a clone of an existing <see cref="PayPeriodData"/> instance.
        ''' </summary>
        ''' <param name="payPeriodData">The instance to clone.</param>
        ''' <returns>The clone of the existing pay period data.</returns>
        Public Shared Function Clone(ByVal payPeriodData As PayPeriodData) As PayPeriodData
            Dim dataSet As DataSet = payPeriodData.FileDataSet.Copy()
            Dim dataCopy As FileDataSet = CType(dataSet, FileDataSet)
            Return Create(dataCopy)
        End Function

    End Class
End Namespace