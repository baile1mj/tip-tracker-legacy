Public Class frmManageUsers

    Private Sub frmManageUsers_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
        Me.UsersBindingSource.DataSource = frmMain.GlobalDataSet
        Me.UsersBindingSource.DataMember = frmMain.GlobalDataSet.Users.TableName
        Me.UsersBindingSource.Sort = "UserName"

        Me.UsersDataGridView.Columns("UserName").DataPropertyName = frmMain.GlobalDataSet.Users.Columns("UserName").ColumnName
        Me.UsersDataGridView.Columns("FirstName").DataPropertyName = frmMain.GlobalDataSet.Users.Columns("FirstName").ColumnName
        Me.UsersDataGridView.Columns("LastName").DataPropertyName = frmMain.GlobalDataSet.Users.Columns("LastName").ColumnName
    End Sub

    Private Sub btnClose_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnClose.Click
        Me.Close()
    End Sub

    Private Sub btnEdit_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnEdit.Click
        Dim strUserName As String = Me.UsersDataGridView.Item("UserName", Me.UsersBindingSource.Position).Value.ToString

        txtUserName.Text = strUserName
        txtFirstName.Text = frmMain.GlobalDataSet.Users.FindByUserName(strUserName)("FirstName").ToString
        txtLastName.Text = frmMain.GlobalDataSet.Users.FindByUserName(strUserName)("LastName").ToString

    End Sub
End Class