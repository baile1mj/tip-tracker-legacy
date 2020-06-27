Public Class frmSelectDate

    Friend WriteOnly Property CurrentDate() As Date
        Set(ByVal value As Date)
            calCalendar.TodayDate = value
            calCalendar.SelectionStart = value
        End Set
    End Property

    Friend WriteOnly Property MinDate() As Date
        Set(ByVal value As Date)
            calCalendar.MinDate = value
        End Set
    End Property

    Friend WriteOnly Property MaxDate() As Date
        Set(ByVal value As Date)
            calCalendar.MaxDate = value
        End Set
    End Property

    Friend ReadOnly Property SelectedDate() As Date
        Get
            Return calCalendar.SelectionStart
        End Get
    End Property

    Private Sub btnCancel_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnCancel.Click
        Me.Close()
    End Sub

    Private Sub btnOK_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnOK.Click
        Me.DialogResult = Windows.Forms.DialogResult.OK
        Me.Close()
    End Sub
End Class