<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class frmManageSpecialFunctions
    Inherits System.Windows.Forms.Form

    'Form overrides dispose to clean up the component list.
    <System.Diagnostics.DebuggerNonUserCode()> _
    Protected Overrides Sub Dispose(ByVal disposing As Boolean)
        If disposing AndAlso components IsNot Nothing Then
            components.Dispose()
        End If
        MyBase.Dispose(disposing)
    End Sub

    'Required by the Windows Form Designer
    Private components As System.ComponentModel.IContainer

    'NOTE: The following procedure is required by the Windows Form Designer
    'It can be modified using the Windows Form Designer.  
    'Do not modify it using the code editor.
    <System.Diagnostics.DebuggerStepThrough()> _
    Private Sub InitializeComponent()
        Me.components = New System.ComponentModel.Container
        Me.SpecialFunctionsDataGridView = New System.Windows.Forms.DataGridView
        Me.SpecialFunction = New System.Windows.Forms.DataGridViewTextBoxColumn
        Me.SpecialFunctionsBindingSource = New System.Windows.Forms.BindingSource(Me.components)
        Me.btnClose = New System.Windows.Forms.Button
        Me.btnAdd = New System.Windows.Forms.Button
        Me.btnDelete = New System.Windows.Forms.Button
        CType(Me.SpecialFunctionsDataGridView, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.SpecialFunctionsBindingSource, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.SuspendLayout()
        '
        'SpecialFunctionsDataGridView
        '
        Me.SpecialFunctionsDataGridView.AllowUserToAddRows = False
        Me.SpecialFunctionsDataGridView.AllowUserToDeleteRows = False
        Me.SpecialFunctionsDataGridView.AllowUserToResizeColumns = False
        Me.SpecialFunctionsDataGridView.AllowUserToResizeRows = False
        Me.SpecialFunctionsDataGridView.AutoGenerateColumns = False
        Me.SpecialFunctionsDataGridView.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.DisableResizing
        Me.SpecialFunctionsDataGridView.Columns.AddRange(New System.Windows.Forms.DataGridViewColumn() {Me.SpecialFunction})
        Me.SpecialFunctionsDataGridView.DataSource = Me.SpecialFunctionsBindingSource
        Me.SpecialFunctionsDataGridView.Location = New System.Drawing.Point(12, 12)
        Me.SpecialFunctionsDataGridView.MultiSelect = False
        Me.SpecialFunctionsDataGridView.Name = "SpecialFunctionsDataGridView"
        Me.SpecialFunctionsDataGridView.ReadOnly = True
        Me.SpecialFunctionsDataGridView.RowHeadersVisible = False
        Me.SpecialFunctionsDataGridView.RowHeadersWidthSizeMode = System.Windows.Forms.DataGridViewRowHeadersWidthSizeMode.DisableResizing
        Me.SpecialFunctionsDataGridView.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect
        Me.SpecialFunctionsDataGridView.Size = New System.Drawing.Size(386, 220)
        Me.SpecialFunctionsDataGridView.TabIndex = 0
        '
        'SpecialFunction
        '
        Me.SpecialFunction.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.Fill
        Me.SpecialFunction.DataPropertyName = "SpecialFunction"
        Me.SpecialFunction.HeaderText = "Special Function"
        Me.SpecialFunction.Name = "SpecialFunction"
        Me.SpecialFunction.ReadOnly = True
        '
        'btnClose
        '
        Me.btnClose.Location = New System.Drawing.Point(323, 238)
        Me.btnClose.Name = "btnClose"
        Me.btnClose.Size = New System.Drawing.Size(75, 23)
        Me.btnClose.TabIndex = 1
        Me.btnClose.Text = "Close"
        Me.btnClose.UseVisualStyleBackColor = True
        '
        'btnAdd
        '
        Me.btnAdd.Location = New System.Drawing.Point(12, 238)
        Me.btnAdd.Name = "btnAdd"
        Me.btnAdd.Size = New System.Drawing.Size(75, 23)
        Me.btnAdd.TabIndex = 2
        Me.btnAdd.Text = "Add..."
        Me.btnAdd.UseVisualStyleBackColor = True
        '
        'btnDelete
        '
        Me.btnDelete.Location = New System.Drawing.Point(93, 238)
        Me.btnDelete.Name = "btnDelete"
        Me.btnDelete.Size = New System.Drawing.Size(75, 23)
        Me.btnDelete.TabIndex = 3
        Me.btnDelete.Text = "Delete"
        Me.btnDelete.UseVisualStyleBackColor = True
        '
        'frmManageSpecialFunctions
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.ClientSize = New System.Drawing.Size(410, 273)
        Me.Controls.Add(Me.btnDelete)
        Me.Controls.Add(Me.btnAdd)
        Me.Controls.Add(Me.btnClose)
        Me.Controls.Add(Me.SpecialFunctionsDataGridView)
        Me.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog
        Me.MaximizeBox = False
        Me.MinimizeBox = False
        Me.Name = "frmManageSpecialFunctions"
        Me.ShowInTaskbar = False
        Me.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent
        Me.Text = "Manage Special Functions"
        CType(Me.SpecialFunctionsDataGridView, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.SpecialFunctionsBindingSource, System.ComponentModel.ISupportInitialize).EndInit()
        Me.ResumeLayout(False)

    End Sub
    Friend WithEvents SpecialFunctionsDataGridView As System.Windows.Forms.DataGridView
    Friend WithEvents btnClose As System.Windows.Forms.Button
    Friend WithEvents btnAdd As System.Windows.Forms.Button
    Friend WithEvents btnDelete As System.Windows.Forms.Button
    Friend WithEvents SpecialFunctionsBindingSource As System.Windows.Forms.BindingSource
    Friend WithEvents SpecialFunction As System.Windows.Forms.DataGridViewTextBoxColumn
End Class
