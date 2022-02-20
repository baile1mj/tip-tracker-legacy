Imports TipTracker.Core

Public Class frmAddEditServer
    Public ReadOnly Property Server As Server

    Public Sub New()
        Me.New(New Server(), True)
    End Sub

    Public Sub New(editServer As Server, allowNumberChange As Boolean)
        InitializeComponent()

        If IsNothing(editServer) Then
            Throw New ArgumentNullException(NameOf(editServer), "Cannot edit a null object.")
        End If

        _Server = editServer
        txtServerNumber.ReadOnly = Not allowNumberChange
        txtServerNumber.DataBindings.Add(NameOf(txtServerNumber.Text), Server, NameOf(Server.PosId))
        txtFirstName.DataBindings.Add(NameOf(txtFirstName.Text), Server, NameOf(Server.FirstName))
        txtLastName.DataBindings.Add(NameOf(txtLastName.Text), Server, NameOf(Server.LastName))
        optSuppressChit.DataBindings.Add(NameOf(optSuppressChit.Checked), Server, NameOf(Server.SuppressChit))
    End Sub
    
    Public Shadows Function ShowDialog() As DialogResult
        If txtServerNumber.ReadOnly Then
            txtFirstName.Select()
        Else
            txtServerNumber.Select()
        End If

        Return MyBase.ShowDialog()
    End Function

    Friend Property ServerNumber As String
        Get
            Return txtServerNumber.Text
        End Get
        Set(value As String)
            txtServerNumber.Text = value
        End Set
    End Property

    Friend Property FirstName As String
        Get
            Return txtFirstName.Text
        End Get
        Set(value As String)
            txtFirstName.Text = value
        End Set
    End Property

    Friend Property LastName As String
        Get
            Return txtLastName.Text
        End Get
        Set(value As String)
            txtLastName.Text = value
        End Set
    End Property

    Friend Property SuppressChit As Boolean
        Get
            Return optSuppressChit.Checked
        End Get
        Set(value As Boolean)
            optSuppressChit.Checked = value
        End Set
    End Property

    Private Sub btnCancel_Click(sender As Object, e As EventArgs) Handles btnCancel.Click
        Close()
    End Sub

    Private Sub btnOk_Click(sender As Object, e As EventArgs) Handles btnOk.Click
        If txtServerNumber.Text = "" Then
            MessageBox.Show("You must enter a server number.", "Invalid Entry", MessageBoxButtons.OK)
            txtServerNumber.Select()
            Exit Sub
        End If

        If txtFirstName.Text = "" Then
            MessageBox.Show("You must enter a first name.", "Invalid Entry", MessageBoxButtons.OK)
            txtFirstName.Select()
            Exit Sub
        End If

        If txtLastName.Text = "" Then
            MessageBox.Show("You must enter a last name.", "Invalid Entry", MessageBoxButtons.OK)
            txtLastName.Select()
            Exit Sub
        End If

        DialogResult = DialogResult.OK
        Close()
    End Sub

    Private Sub txtServerNumber_GotFocus(sender As Object, e As EventArgs) Handles txtServerNumber.GotFocus
        If txtServerNumber.Text <> "" Then txtServerNumber.SelectAll()
    End Sub

    Private Sub txtServerNumber_KeyPress(sender As Object, e As KeyPressEventArgs) Handles txtServerNumber.KeyPress
        If e.KeyChar = ChrW(13) Then
            e.Handled = True
            txtFirstName.Select()
        End If
        If e.KeyChar = "|" Or e.KeyChar = " " Then
            e.Handled = True
        End If
    End Sub

    Private Sub txtFirstName_GotFocus(sender As Object, e As EventArgs) Handles txtFirstName.GotFocus
        If txtFirstName.Text <> "" Then txtFirstName.SelectAll()
    End Sub

    Private Sub txtFirstName_KeyPress(sender As Object, e As KeyPressEventArgs) Handles txtFirstName.KeyPress
        If e.KeyChar = ChrW(13) Then
            e.Handled = True
            txtLastName.Select()
        End If
        If e.KeyChar = "|" Then
            e.Handled = True
        End If
    End Sub

    Private Sub txtLastName_GotFocus(sender As Object, e As EventArgs) Handles txtLastName.GotFocus
        If txtLastName.Text <> "" Then txtLastName.SelectAll()
    End Sub

    Private Sub txtLastName_KeyPress(sender As Object, e As KeyPressEventArgs) Handles txtLastName.KeyPress
        If e.KeyChar = ChrW(13) Then
            e.Handled = True
            btnOk.Select()
        End If
        If e.KeyChar = "|" Then
            e.Handled = True
        End If
    End Sub
End Class