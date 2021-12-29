Imports Microsoft.Reporting.WinForms

Public Class frmReportPreview
    
    Private ReadOnly _viewer As New ReportViewer()
    
    Public Sub New (initializationDelegate As Action(Of LocalReport))
        InitializeComponent()

        If initializationDelegate Is Nothing Then
            Throw New ArgumentNullException(NameOf (initializationDelegate), "You must provide a method to initialize the report.")
        End If
        
        initializationDelegate.Invoke(_viewer.LocalReport)
        
        Controls.Add(_viewer)
        
        _viewer.Dock = DockStyle.Fill
        _viewer.SetDisplayMode(DisplayMode.PrintLayout)
        _viewer.ShowExportButton = False
        _viewer.ShowRefreshButton = False
        _viewer.ShowStopButton = False
        _viewer.ShowPromptAreaButton = False
        _viewer.ShowFindControls = False
        _viewer.RefreshReport()
    End sub

End Class