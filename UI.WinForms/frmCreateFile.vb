Public Class frmCreateFile

    Friend ReadOnly Property PeriodStart() As Date
        Get
            Return dtpPeriodStart.Value
        End Get
    End Property

    Friend ReadOnly Property PeriodEnd() As Date
        Get
            Return dtpPeriodEnd.Value
        End Get
    End Property

    Private Sub btnCancel_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnCancel.Click
        Me.Close()
    End Sub

    Private Sub btnOK_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnOK.Click
        Me.DialogResult = Windows.Forms.DialogResult.OK
        Me.Close()
    End Sub

    Private Sub dtpPeriodStart_ValueChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles dtpPeriodStart.ValueChanged
        dtpPeriodEnd.Value = DateAdd(DateInterval.Day, 13, dtpPeriodStart.Value)
    End Sub

    Private Sub frmCreateFile_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
        dtpPeriodStart.Value = DateTime.Today
        dtpPeriodEnd.Value = DateAdd(DateInterval.Day, 13, dtpPeriodStart.Value)
    End Sub
End Class