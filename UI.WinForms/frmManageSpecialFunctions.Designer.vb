Imports TipTracker.Common.Data.PayPeriod

<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()>
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
        Me.components = New System.ComponentModel.Container()
        Dim DataGridViewCellStyle1 As System.Windows.Forms.DataGridViewCellStyle = New System.Windows.Forms.DataGridViewCellStyle()
        Me.SpecialFunctionsDataGridView = New System.Windows.Forms.DataGridView()
        Me.btnClose = New System.Windows.Forms.Button()
        Me.btnAdd = New System.Windows.Forms.Button()
        Me.btnDelete = New System.Windows.Forms.Button()
        Me.btnEdit = New System.Windows.Forms.Button()
        Me.EventBindingSource = New System.Windows.Forms.BindingSource(Me.components)
        Me.NameDataGridViewTextBoxColumn = New System.Windows.Forms.DataGridViewTextBoxColumn()
        Me.DateDataGridViewTextBoxColumn = New System.Windows.Forms.DataGridViewTextBoxColumn()
        CType(Me.SpecialFunctionsDataGridView,System.ComponentModel.ISupportInitialize).BeginInit
        CType(Me.EventBindingSource,System.ComponentModel.ISupportInitialize).BeginInit
        Me.SuspendLayout
        '
        'SpecialFunctionsDataGridView
        '
        Me.SpecialFunctionsDataGridView.AllowUserToAddRows = false
        Me.SpecialFunctionsDataGridView.AllowUserToDeleteRows = false
        Me.SpecialFunctionsDataGridView.AllowUserToResizeColumns = false
        Me.SpecialFunctionsDataGridView.AllowUserToResizeRows = false
        Me.SpecialFunctionsDataGridView.AutoGenerateColumns = false
        Me.SpecialFunctionsDataGridView.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.DisableResizing
        Me.SpecialFunctionsDataGridView.Columns.AddRange(New System.Windows.Forms.DataGridViewColumn() {Me.NameDataGridViewTextBoxColumn, Me.DateDataGridViewTextBoxColumn})
        Me.SpecialFunctionsDataGridView.DataSource = Me.EventBindingSource
        Me.SpecialFunctionsDataGridView.Location = New System.Drawing.Point(12, 12)
        Me.SpecialFunctionsDataGridView.MultiSelect = false
        Me.SpecialFunctionsDataGridView.Name = "SpecialFunctionsDataGridView"
        Me.SpecialFunctionsDataGridView.ReadOnly = true
        Me.SpecialFunctionsDataGridView.RowHeadersVisible = false
        Me.SpecialFunctionsDataGridView.RowHeadersWidthSizeMode = System.Windows.Forms.DataGridViewRowHeadersWidthSizeMode.DisableResizing
        Me.SpecialFunctionsDataGridView.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect
        Me.SpecialFunctionsDataGridView.Size = New System.Drawing.Size(386, 220)
        Me.SpecialFunctionsDataGridView.TabIndex = 0
        '
        'btnClose
        '
        Me.btnClose.Location = New System.Drawing.Point(323, 238)
        Me.btnClose.Name = "btnClose"
        Me.btnClose.Size = New System.Drawing.Size(75, 23)
        Me.btnClose.TabIndex = 1
        Me.btnClose.Text = "&Close"
        Me.btnClose.UseVisualStyleBackColor = true
        '
        'btnAdd
        '
        Me.btnAdd.Location = New System.Drawing.Point(12, 238)
        Me.btnAdd.Name = "btnAdd"
        Me.btnAdd.Size = New System.Drawing.Size(75, 23)
        Me.btnAdd.TabIndex = 2
        Me.btnAdd.Text = "&Add..."
        Me.btnAdd.UseVisualStyleBackColor = true
        '
        'btnDelete
        '
        Me.btnDelete.Location = New System.Drawing.Point(174, 238)
        Me.btnDelete.Name = "btnDelete"
        Me.btnDelete.Size = New System.Drawing.Size(75, 23)
        Me.btnDelete.TabIndex = 3
        Me.btnDelete.Text = "&Delete"
        Me.btnDelete.UseVisualStyleBackColor = true
        '
        'btnEdit
        '
        Me.btnEdit.Location = New System.Drawing.Point(93, 238)
        Me.btnEdit.Name = "btnEdit"
        Me.btnEdit.Size = New System.Drawing.Size(75, 23)
        Me.btnEdit.TabIndex = 4
        Me.btnEdit.Text = "&Edit..."
        Me.btnEdit.UseVisualStyleBackColor = true
        '
        'EventBindingSource
        '
        Me.EventBindingSource.DataSource = GetType(TipTracker.Core.[Event])
        '
        'NameDataGridViewTextBoxColumn
        '
        Me.NameDataGridViewTextBoxColumn.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.Fill
        Me.NameDataGridViewTextBoxColumn.DataPropertyName = "Name"
        Me.NameDataGridViewTextBoxColumn.FillWeight = 70!
        Me.NameDataGridViewTextBoxColumn.HeaderText = "Special Function"
        Me.NameDataGridViewTextBoxColumn.Name = "NameDataGridViewTextBoxColumn"
        Me.NameDataGridViewTextBoxColumn.ReadOnly = true
        '
        'DateDataGridViewTextBoxColumn
        '
        Me.DateDataGridViewTextBoxColumn.AutoSizeMode = System.Windows.Forms.DataGridViewAutoSizeColumnMode.Fill
        Me.DateDataGridViewTextBoxColumn.DataPropertyName = "Date"
        DataGridViewCellStyle1.Format = "MM/dd/yyyy"
        Me.DateDataGridViewTextBoxColumn.DefaultCellStyle = DataGridViewCellStyle1
        Me.DateDataGridViewTextBoxColumn.FillWeight = 30!
        Me.DateDataGridViewTextBoxColumn.HeaderText = "Date"
        Me.DateDataGridViewTextBoxColumn.Name = "DateDataGridViewTextBoxColumn"
        Me.DateDataGridViewTextBoxColumn.ReadOnly = true
        '
        'frmManageSpecialFunctions
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6!, 13!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.ClientSize = New System.Drawing.Size(410, 273)
        Me.Controls.Add(Me.btnEdit)
        Me.Controls.Add(Me.btnDelete)
        Me.Controls.Add(Me.btnAdd)
        Me.Controls.Add(Me.btnClose)
        Me.Controls.Add(Me.SpecialFunctionsDataGridView)
        Me.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog
        Me.MaximizeBox = false
        Me.MinimizeBox = false
        Me.Name = "frmManageSpecialFunctions"
        Me.ShowInTaskbar = false
        Me.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent
        Me.Text = "Manage Special Functions"
        CType(Me.SpecialFunctionsDataGridView,System.ComponentModel.ISupportInitialize).EndInit
        CType(Me.EventBindingSource,System.ComponentModel.ISupportInitialize).EndInit
        Me.ResumeLayout(false)

End Sub
    Friend WithEvents SpecialFunctionsDataGridView As System.Windows.Forms.DataGridView
    Friend WithEvents btnClose As System.Windows.Forms.Button
    Friend WithEvents btnAdd As System.Windows.Forms.Button
    Friend WithEvents btnDelete As System.Windows.Forms.Button
    Friend WithEvents btnEdit As System.Windows.Forms.Button
    Friend WithEvents EventBindingSource As BindingSource
    Friend WithEvents NameDataGridViewTextBoxColumn As DataGridViewTextBoxColumn
    Friend WithEvents DateDataGridViewTextBoxColumn As DataGridViewTextBoxColumn
End Class
