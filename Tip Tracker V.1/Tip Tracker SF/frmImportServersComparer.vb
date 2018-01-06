Public Class frmImportServersComparer
    Friend m_dsParentDataSet As New GlobalDataSet

    Private m_strImportedServerNumber As String
    Private m_strImportedFirstName As String
    Private m_strImportedLastName As String

    Private m_strExistingServerNumber As String
    Private m_strExistingFirstName As String
    Private m_strExistingLastName As String

    Friend Property ImportedServerNumber() As String
        Get
            Return m_strImportedServerNumber
        End Get
        Set(ByVal value As String)
            m_strImportedServerNumber = value
        End Set
    End Property

    Friend Property ImportedFirstName() As String
        Get
            Return m_strImportedFirstName
        End Get
        Set(ByVal value As String)
            m_strImportedFirstName = value
        End Set
    End Property

    Friend Property ImportedLastName() As String
        Get
            Return m_strImportedLastName
        End Get
        Set(ByVal value As String)
            m_strImportedLastName = value
        End Set
    End Property

    Friend Property ExistingServerNumber() As String
        Get
            Return m_strExistingServerNumber
        End Get
        Set(ByVal value As String)
            m_strExistingServerNumber = value
        End Set
    End Property

    Friend Property ExistingFirstName() As String
        Get
            Return m_strExistingFirstName
        End Get
        Set(ByVal value As String)
            m_strExistingFirstName = value
        End Set
    End Property

    Friend Property ExistingLastName() As String
        Get
            Return m_strExistingLastName
        End Get
        Set(ByVal value As String)
            m_strExistingLastName = value
        End Set
    End Property

    Private Sub frmImportServersComparer_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
        Me.txtExistingServer.Text = Me.ExistingServerNumber & "  " & Me.ExistingFirstName & " " & Me.ExistingLastName
        Me.txtImportedServer.Text = Me.ImportedServerNumber & "  " & Me.ImportedFirstName & " " & Me.ImportedLastName
    End Sub

    Private Sub btnCancel_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnCancel.Click
        Me.Close()
    End Sub

    Private Sub btnNext_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnNext.Click
        Me.DialogResult = Windows.Forms.DialogResult.OK
        Me.Close()
    End Sub
End Class