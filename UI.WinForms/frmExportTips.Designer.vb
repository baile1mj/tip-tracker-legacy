Imports TipTracker.Common.Data.PayPeriod

<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()>
Partial Class frmExportTips
    Inherits System.Windows.Forms.Form

    'Form overrides dispose to clean up the component list.
    <System.Diagnostics.DebuggerNonUserCode()> _
    Protected Overrides Sub Dispose(ByVal disposing As Boolean)
        Try
            If disposing AndAlso components IsNot Nothing Then
                components.Dispose()
            End If
        Finally
            MyBase.Dispose(disposing)
        End Try
    End Sub

    'Required by the Windows Form Designer
    Private components As System.ComponentModel.IContainer

    'NOTE: The following procedure is required by the Windows Form Designer
    'It can be modified using the Windows Form Designer.  
    'Do not modify it using the code editor.
    <System.Diagnostics.DebuggerStepThrough()> _
    Private Sub InitializeComponent()
        Me.components = New System.ComponentModel.Container
        Me.btnOK = New System.Windows.Forms.Button
        Me.btnCancel = New System.Windows.Forms.Button
        Me.grpSortOrder = New System.Windows.Forms.GroupBox
        Me.FileDataSet = New FileDataSet
        Me.gprServersToInclude = New System.Windows.Forms.GroupBox
        Me.cboServers = New System.Windows.Forms.ComboBox
        Me.optSelectedServer = New System.Windows.Forms.RadioButton
        Me.optAllServers = New System.Windows.Forms.RadioButton
        Me.grpTipsToInclude = New System.Windows.Forms.GroupBox
        Me.optCash = New System.Windows.Forms.CheckBox
        Me.optSpecialFunction = New System.Windows.Forms.CheckBox
        Me.optRoomCharge = New System.Windows.Forms.CheckBox
        Me.optCreditCard = New System.Windows.Forms.CheckBox
        Me.grpDatesToInclude = New System.Windows.Forms.GroupBox
        Me.txtSelectedDate = New System.Windows.Forms.MaskedTextBox
        Me.optSelectedDate = New System.Windows.Forms.RadioButton
        Me.optAllDates = New System.Windows.Forms.RadioButton
        Me.ServersBindingSource = New System.Windows.Forms.BindingSource(Me.components)
        Me.ServersLookupDataset = New TipTracker.ServersLookupDataset
        Me.cboSortLevel1 = New System.Windows.Forms.ComboBox
        Me.lblSortLevel1 = New System.Windows.Forms.Label
        Me.lblSortLevel2 = New System.Windows.Forms.Label
        Me.lblSortLevel3 = New System.Windows.Forms.Label
        Me.cboSortLevel2 = New System.Windows.Forms.ComboBox
        Me.cboSortLevel3 = New System.Windows.Forms.ComboBox
        Me.btnClear = New System.Windows.Forms.Button
        Me.grpSortOrder.SuspendLayout()
        CType(Me.FileDataSet, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.gprServersToInclude.SuspendLayout()
        Me.grpTipsToInclude.SuspendLayout()
        Me.grpDatesToInclude.SuspendLayout()
        CType(Me.ServersBindingSource, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.ServersLookupDataset, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.SuspendLayout()
        '
        'btnOK
        '
        Me.btnOK.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.btnOK.Location = New System.Drawing.Point(294, 260)
        Me.btnOK.Name = "btnOK"
        Me.btnOK.Size = New System.Drawing.Size(75, 23)
        Me.btnOK.TabIndex = 0
        Me.btnOK.Text = "OK"
        Me.btnOK.UseVisualStyleBackColor = True
        '
        'btnCancel
        '
        Me.btnCancel.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.btnCancel.Location = New System.Drawing.Point(375, 260)
        Me.btnCancel.Name = "btnCancel"
        Me.btnCancel.Size = New System.Drawing.Size(75, 23)
        Me.btnCancel.TabIndex = 1
        Me.btnCancel.Text = "Cancel"
        Me.btnCancel.UseVisualStyleBackColor = True
        '
        'grpSortOrder
        '
        Me.grpSortOrder.Controls.Add(Me.btnClear)
        Me.grpSortOrder.Controls.Add(Me.cboSortLevel3)
        Me.grpSortOrder.Controls.Add(Me.cboSortLevel2)
        Me.grpSortOrder.Controls.Add(Me.lblSortLevel3)
        Me.grpSortOrder.Controls.Add(Me.lblSortLevel2)
        Me.grpSortOrder.Controls.Add(Me.lblSortLevel1)
        Me.grpSortOrder.Controls.Add(Me.cboSortLevel1)
        Me.grpSortOrder.Location = New System.Drawing.Point(12, 12)
        Me.grpSortOrder.Name = "grpSortOrder"
        Me.grpSortOrder.Size = New System.Drawing.Size(230, 137)
        Me.grpSortOrder.TabIndex = 2
        Me.grpSortOrder.TabStop = False
        Me.grpSortOrder.Text = "Sort Order"
        '
        'FileDataSet
        '
        Me.FileDataSet.CaseSensitive = True
        Me.FileDataSet.DataSetName = "FileDataSet"
        Me.FileDataSet.SchemaSerializationMode = System.Data.SchemaSerializationMode.IncludeSchema
        '
        'gprServersToInclude
        '
        Me.gprServersToInclude.Anchor = CType(((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Bottom) _
                    Or System.Windows.Forms.AnchorStyles.Left), System.Windows.Forms.AnchorStyles)
        Me.gprServersToInclude.Controls.Add(Me.cboServers)
        Me.gprServersToInclude.Controls.Add(Me.optSelectedServer)
        Me.gprServersToInclude.Controls.Add(Me.optAllServers)
        Me.gprServersToInclude.Location = New System.Drawing.Point(12, 155)
        Me.gprServersToInclude.Name = "gprServersToInclude"
        Me.gprServersToInclude.Size = New System.Drawing.Size(230, 99)
        Me.gprServersToInclude.TabIndex = 3
        Me.gprServersToInclude.TabStop = False
        Me.gprServersToInclude.Text = "Servers to Include"
        '
        'cboServers
        '
        Me.cboServers.DataSource = Me.ServersBindingSource
        Me.cboServers.DisplayMember = "NameString"
        Me.cboServers.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList
        Me.cboServers.Enabled = False
        Me.cboServers.FormattingEnabled = True
        Me.cboServers.Location = New System.Drawing.Point(44, 63)
        Me.cboServers.Name = "cboServers"
        Me.cboServers.Size = New System.Drawing.Size(172, 21)
        Me.cboServers.TabIndex = 2
        Me.cboServers.ValueMember = "ServerNumber"
        '
        'optSelectedServer
        '
        Me.optSelectedServer.AutoSize = True
        Me.optSelectedServer.Location = New System.Drawing.Point(22, 40)
        Me.optSelectedServer.Name = "optSelectedServer"
        Me.optSelectedServer.Size = New System.Drawing.Size(104, 17)
        Me.optSelectedServer.TabIndex = 1
        Me.optSelectedServer.Text = "Selected Server:"
        Me.optSelectedServer.UseVisualStyleBackColor = True
        '
        'optAllServers
        '
        Me.optAllServers.AutoSize = True
        Me.optAllServers.Checked = True
        Me.optAllServers.Location = New System.Drawing.Point(22, 17)
        Me.optAllServers.Name = "optAllServers"
        Me.optAllServers.Size = New System.Drawing.Size(75, 17)
        Me.optAllServers.TabIndex = 0
        Me.optAllServers.TabStop = True
        Me.optAllServers.Text = "All Servers"
        Me.optAllServers.UseVisualStyleBackColor = True
        '
        'grpTipsToInclude
        '
        Me.grpTipsToInclude.Anchor = CType(((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Left) _
                    Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.grpTipsToInclude.Controls.Add(Me.optCash)
        Me.grpTipsToInclude.Controls.Add(Me.optSpecialFunction)
        Me.grpTipsToInclude.Controls.Add(Me.optRoomCharge)
        Me.grpTipsToInclude.Controls.Add(Me.optCreditCard)
        Me.grpTipsToInclude.Location = New System.Drawing.Point(248, 12)
        Me.grpTipsToInclude.Name = "grpTipsToInclude"
        Me.grpTipsToInclude.Size = New System.Drawing.Size(202, 137)
        Me.grpTipsToInclude.TabIndex = 4
        Me.grpTipsToInclude.TabStop = False
        Me.grpTipsToInclude.Text = "Tips to Include"
        '
        'optCash
        '
        Me.optCash.AutoSize = True
        Me.optCash.Checked = True
        Me.optCash.CheckState = System.Windows.Forms.CheckState.Checked
        Me.optCash.Location = New System.Drawing.Point(49, 94)
        Me.optCash.Name = "optCash"
        Me.optCash.Size = New System.Drawing.Size(50, 17)
        Me.optCash.TabIndex = 3
        Me.optCash.Text = "Cash"
        Me.optCash.UseVisualStyleBackColor = True
        '
        'optSpecialFunction
        '
        Me.optSpecialFunction.AutoSize = True
        Me.optSpecialFunction.Checked = True
        Me.optSpecialFunction.CheckState = System.Windows.Forms.CheckState.Checked
        Me.optSpecialFunction.Location = New System.Drawing.Point(49, 71)
        Me.optSpecialFunction.Name = "optSpecialFunction"
        Me.optSpecialFunction.Size = New System.Drawing.Size(105, 17)
        Me.optSpecialFunction.TabIndex = 2
        Me.optSpecialFunction.Text = "Special Function"
        Me.optSpecialFunction.UseVisualStyleBackColor = True
        '
        'optRoomCharge
        '
        Me.optRoomCharge.AutoSize = True
        Me.optRoomCharge.Checked = True
        Me.optRoomCharge.CheckState = System.Windows.Forms.CheckState.Checked
        Me.optRoomCharge.Location = New System.Drawing.Point(49, 48)
        Me.optRoomCharge.Name = "optRoomCharge"
        Me.optRoomCharge.Size = New System.Drawing.Size(91, 17)
        Me.optRoomCharge.TabIndex = 1
        Me.optRoomCharge.Text = "Room Charge"
        Me.optRoomCharge.UseVisualStyleBackColor = True
        '
        'optCreditCard
        '
        Me.optCreditCard.AutoSize = True
        Me.optCreditCard.Checked = True
        Me.optCreditCard.CheckState = System.Windows.Forms.CheckState.Checked
        Me.optCreditCard.Location = New System.Drawing.Point(49, 25)
        Me.optCreditCard.Name = "optCreditCard"
        Me.optCreditCard.Size = New System.Drawing.Size(78, 17)
        Me.optCreditCard.TabIndex = 0
        Me.optCreditCard.Text = "Credit Card"
        Me.optCreditCard.UseVisualStyleBackColor = True
        '
        'grpDatesToInclude
        '
        Me.grpDatesToInclude.Anchor = CType((((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Bottom) _
                    Or System.Windows.Forms.AnchorStyles.Left) _
                    Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.grpDatesToInclude.Controls.Add(Me.txtSelectedDate)
        Me.grpDatesToInclude.Controls.Add(Me.optSelectedDate)
        Me.grpDatesToInclude.Controls.Add(Me.optAllDates)
        Me.grpDatesToInclude.Location = New System.Drawing.Point(248, 155)
        Me.grpDatesToInclude.Name = "grpDatesToInclude"
        Me.grpDatesToInclude.Size = New System.Drawing.Size(202, 99)
        Me.grpDatesToInclude.TabIndex = 5
        Me.grpDatesToInclude.TabStop = False
        Me.grpDatesToInclude.Text = "Dates to Include"
        '
        'txtSelectedDate
        '
        Me.txtSelectedDate.Enabled = False
        Me.txtSelectedDate.Location = New System.Drawing.Point(119, 53)
        Me.txtSelectedDate.Mask = "00/00/0000"
        Me.txtSelectedDate.Name = "txtSelectedDate"
        Me.txtSelectedDate.Size = New System.Drawing.Size(77, 20)
        Me.txtSelectedDate.TabIndex = 2
        Me.txtSelectedDate.ValidatingType = GetType(Date)
        '
        'optSelectedDate
        '
        Me.optSelectedDate.AutoSize = True
        Me.optSelectedDate.Location = New System.Drawing.Point(17, 53)
        Me.optSelectedDate.Name = "optSelectedDate"
        Me.optSelectedDate.Size = New System.Drawing.Size(96, 17)
        Me.optSelectedDate.TabIndex = 1
        Me.optSelectedDate.Text = "Selected Date:"
        Me.optSelectedDate.UseVisualStyleBackColor = True
        '
        'optAllDates
        '
        Me.optAllDates.AutoSize = True
        Me.optAllDates.Checked = True
        Me.optAllDates.Location = New System.Drawing.Point(17, 30)
        Me.optAllDates.Name = "optAllDates"
        Me.optAllDates.Size = New System.Drawing.Size(67, 17)
        Me.optAllDates.TabIndex = 0
        Me.optAllDates.TabStop = True
        Me.optAllDates.Text = "All Dates"
        Me.optAllDates.UseVisualStyleBackColor = True
        '
        'ServersBindingSource
        '
        Me.ServersBindingSource.AllowNew = False
        Me.ServersBindingSource.DataMember = "Servers"
        Me.ServersBindingSource.DataSource = Me.ServersLookupDataset
        '
        'ServersLookupDataset
        '
        Me.ServersLookupDataset.DataSetName = "ServersLookupDataset"
        Me.ServersLookupDataset.SchemaSerializationMode = System.Data.SchemaSerializationMode.IncludeSchema
        '
        'cboSortLevel1
        '
        Me.cboSortLevel1.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList
        Me.cboSortLevel1.FormattingEnabled = True
        Me.cboSortLevel1.Items.AddRange(New Object() {"Order Entered", "Name", "Date", "Tip Type", "Amount"})
        Me.cboSortLevel1.Location = New System.Drawing.Point(69, 23)
        Me.cboSortLevel1.Name = "cboSortLevel1"
        Me.cboSortLevel1.Size = New System.Drawing.Size(145, 21)
        Me.cboSortLevel1.TabIndex = 0
        '
        'lblSortLevel1
        '
        Me.lblSortLevel1.AutoSize = True
        Me.lblSortLevel1.Location = New System.Drawing.Point(17, 26)
        Me.lblSortLevel1.Name = "lblSortLevel1"
        Me.lblSortLevel1.Size = New System.Drawing.Size(40, 13)
        Me.lblSortLevel1.TabIndex = 1
        Me.lblSortLevel1.Text = "Sort by"
        '
        'lblSortLevel2
        '
        Me.lblSortLevel2.AutoSize = True
        Me.lblSortLevel2.Location = New System.Drawing.Point(17, 53)
        Me.lblSortLevel2.Name = "lblSortLevel2"
        Me.lblSortLevel2.Size = New System.Drawing.Size(46, 13)
        Me.lblSortLevel2.TabIndex = 2
        Me.lblSortLevel2.Text = "Then by"
        '
        'lblSortLevel3
        '
        Me.lblSortLevel3.AutoSize = True
        Me.lblSortLevel3.Location = New System.Drawing.Point(17, 80)
        Me.lblSortLevel3.Name = "lblSortLevel3"
        Me.lblSortLevel3.Size = New System.Drawing.Size(46, 13)
        Me.lblSortLevel3.TabIndex = 3
        Me.lblSortLevel3.Text = "Then by"
        '
        'cboSortLevel2
        '
        Me.cboSortLevel2.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList
        Me.cboSortLevel2.Enabled = False
        Me.cboSortLevel2.FormattingEnabled = True
        Me.cboSortLevel2.Location = New System.Drawing.Point(69, 50)
        Me.cboSortLevel2.Name = "cboSortLevel2"
        Me.cboSortLevel2.Size = New System.Drawing.Size(145, 21)
        Me.cboSortLevel2.TabIndex = 4
        '
        'cboSortLevel3
        '
        Me.cboSortLevel3.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList
        Me.cboSortLevel3.Enabled = False
        Me.cboSortLevel3.FormattingEnabled = True
        Me.cboSortLevel3.Location = New System.Drawing.Point(69, 77)
        Me.cboSortLevel3.Name = "cboSortLevel3"
        Me.cboSortLevel3.Size = New System.Drawing.Size(145, 21)
        Me.cboSortLevel3.TabIndex = 5
        '
        'btnClear
        '
        Me.btnClear.Location = New System.Drawing.Point(164, 108)
        Me.btnClear.Name = "btnClear"
        Me.btnClear.Size = New System.Drawing.Size(60, 23)
        Me.btnClear.TabIndex = 6
        Me.btnClear.Text = "Clear"
        Me.btnClear.UseVisualStyleBackColor = True
        '
        'frmExportTips
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.ClientSize = New System.Drawing.Size(462, 295)
        Me.Controls.Add(Me.grpDatesToInclude)
        Me.Controls.Add(Me.grpTipsToInclude)
        Me.Controls.Add(Me.gprServersToInclude)
        Me.Controls.Add(Me.grpSortOrder)
        Me.Controls.Add(Me.btnCancel)
        Me.Controls.Add(Me.btnOK)
        Me.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog
        Me.MaximizeBox = False
        Me.MinimizeBox = False
        Me.Name = "frmExportTips"
        Me.ShowIcon = False
        Me.ShowInTaskbar = False
        Me.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent
        Me.Text = "Set Export Filters"
        Me.grpSortOrder.ResumeLayout(False)
        Me.grpSortOrder.PerformLayout()
        CType(Me.FileDataSet, System.ComponentModel.ISupportInitialize).EndInit()
        Me.gprServersToInclude.ResumeLayout(False)
        Me.gprServersToInclude.PerformLayout()
        Me.grpTipsToInclude.ResumeLayout(False)
        Me.grpTipsToInclude.PerformLayout()
        Me.grpDatesToInclude.ResumeLayout(False)
        Me.grpDatesToInclude.PerformLayout()
        CType(Me.ServersBindingSource, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.ServersLookupDataset, System.ComponentModel.ISupportInitialize).EndInit()
        Me.ResumeLayout(False)

    End Sub
    Friend WithEvents btnOK As System.Windows.Forms.Button
    Friend WithEvents btnCancel As System.Windows.Forms.Button
    Friend WithEvents grpSortOrder As System.Windows.Forms.GroupBox
    Friend WithEvents FileDataSet As FileDataSet
    Friend WithEvents gprServersToInclude As System.Windows.Forms.GroupBox
    Friend WithEvents cboServers As System.Windows.Forms.ComboBox
    Friend WithEvents optSelectedServer As System.Windows.Forms.RadioButton
    Friend WithEvents optAllServers As System.Windows.Forms.RadioButton
    Friend WithEvents grpTipsToInclude As System.Windows.Forms.GroupBox
    Friend WithEvents optCash As System.Windows.Forms.CheckBox
    Friend WithEvents optSpecialFunction As System.Windows.Forms.CheckBox
    Friend WithEvents optRoomCharge As System.Windows.Forms.CheckBox
    Friend WithEvents optCreditCard As System.Windows.Forms.CheckBox
    Friend WithEvents grpDatesToInclude As System.Windows.Forms.GroupBox
    Friend WithEvents txtSelectedDate As System.Windows.Forms.MaskedTextBox
    Friend WithEvents optSelectedDate As System.Windows.Forms.RadioButton
    Friend WithEvents optAllDates As System.Windows.Forms.RadioButton
    Friend WithEvents ServersBindingSource As System.Windows.Forms.BindingSource
    Friend WithEvents ServersLookupDataset As TipTracker.ServersLookupDataset
    Friend WithEvents lblSortLevel1 As System.Windows.Forms.Label
    Friend WithEvents cboSortLevel1 As System.Windows.Forms.ComboBox
    Friend WithEvents cboSortLevel3 As System.Windows.Forms.ComboBox
    Friend WithEvents cboSortLevel2 As System.Windows.Forms.ComboBox
    Friend WithEvents lblSortLevel3 As System.Windows.Forms.Label
    Friend WithEvents lblSortLevel2 As System.Windows.Forms.Label
    Friend WithEvents btnClear As System.Windows.Forms.Button
End Class
