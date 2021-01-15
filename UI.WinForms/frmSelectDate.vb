Public Class frmSelectDate
    Public ReadOnly Property SelectedDate As Date
        Get
            Return calCalendar.SelectionStart
        End Get
    End Property

    Public Sub new(periodStart As DateTime, periodEnd As DateTime, currentSelection As DateTime)
        InitializeComponent()

        If currentSelection < periodStart OrElse currentSelection > periodEnd Then
            Throw New ArgumentException("The currently selected date cannot be outside the pay period.")
        End If

        calCalendar.MinDate = periodStart
        calCalendar.MaxDate = periodEnd
        calCalendar.SelectionStart = currentSelection
        calCalendar.TodayDate = currentSelection
    End Sub

    Private Sub btnCancel_Click(sender As Object, e As EventArgs) Handles btnCancel.Click
        DialogResult = DialogResult.Cancel
        Close()
    End Sub

    Private Sub btnOK_Click(sender As Object, e As EventArgs) Handles btnOK.Click
        DialogResult = DialogResult.OK
        Close()
    End Sub
End Class