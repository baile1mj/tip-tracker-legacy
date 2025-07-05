namespace TipTracker.Ui.Controls
{
    partial class QuickTipEditor
    {
        /// <summary> 
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary> 
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Component Designer generated code

        /// <summary> 
        /// Required method for Designer support - do not modify 
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            components = new System.ComponentModel.Container();
            DataGridViewCellStyle dataGridViewCellStyle1 = new DataGridViewCellStyle();
            splTipEntrySplitter = new SplitContainer();
            lblTotal = new Label();
            tipEntryForm = new QuickEntryForm();
            dgvTips = new DataGridView();
            Number = new DataGridViewTextBoxColumn();
            serverNumberDataGridViewTextBoxColumn = new DataGridViewTextBoxColumn();
            lastNameDataGridViewTextBoxColumn = new DataGridViewTextBoxColumn();
            firstNameDataGridViewTextBoxColumn = new DataGridViewTextBoxColumn();
            amountDataGridViewTextBoxColumn = new DataGridViewTextBoxColumn();
            tipBindingSource = new BindingSource(components);
            ((System.ComponentModel.ISupportInitialize)splTipEntrySplitter).BeginInit();
            splTipEntrySplitter.Panel1.SuspendLayout();
            splTipEntrySplitter.Panel2.SuspendLayout();
            splTipEntrySplitter.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)dgvTips).BeginInit();
            ((System.ComponentModel.ISupportInitialize)tipBindingSource).BeginInit();
            SuspendLayout();
            // 
            // splTipEntrySplitter
            // 
            splTipEntrySplitter.Dock = DockStyle.Fill;
            splTipEntrySplitter.Location = new Point(0, 0);
            splTipEntrySplitter.Name = "splTipEntrySplitter";
            // 
            // splTipEntrySplitter.Panel1
            // 
            splTipEntrySplitter.Panel1.Controls.Add(lblTotal);
            splTipEntrySplitter.Panel1.Controls.Add(tipEntryForm);
            splTipEntrySplitter.Panel1MinSize = 175;
            // 
            // splTipEntrySplitter.Panel2
            // 
            splTipEntrySplitter.Panel2.Controls.Add(dgvTips);
            splTipEntrySplitter.Panel2MinSize = 200;
            splTipEntrySplitter.Size = new Size(640, 480);
            splTipEntrySplitter.SplitterDistance = 215;
            splTipEntrySplitter.TabIndex = 0;
            // 
            // lblTotal
            // 
            lblTotal.Anchor = AnchorStyles.Bottom | AnchorStyles.Left;
            lblTotal.AutoSize = true;
            lblTotal.Font = new Font("Segoe UI", 10F, FontStyle.Bold);
            lblTotal.Location = new Point(3, 458);
            lblTotal.Name = "lblTotal";
            lblTotal.Size = new Size(86, 19);
            lblTotal.TabIndex = 1;
            lblTotal.Text = "Total: $0.00";
            // 
            // tipEntryForm
            // 
            tipEntryForm.Anchor = AnchorStyles.Top | AnchorStyles.Left | AnchorStyles.Right;
            tipEntryForm.Location = new Point(3, 3);
            tipEntryForm.Name = "tipEntryForm";
            tipEntryForm.Size = new Size(209, 120);
            tipEntryForm.TabIndex = 0;
            // 
            // dgvTips
            // 
            dgvTips.AllowUserToAddRows = false;
            dgvTips.AllowUserToDeleteRows = false;
            dgvTips.AllowUserToResizeColumns = false;
            dgvTips.AllowUserToResizeRows = false;
            dgvTips.Anchor = AnchorStyles.Top | AnchorStyles.Bottom | AnchorStyles.Left | AnchorStyles.Right;
            dgvTips.AutoGenerateColumns = false;
            dgvTips.ColumnHeadersHeightSizeMode = DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            dgvTips.Columns.AddRange(new DataGridViewColumn[] { Number, serverNumberDataGridViewTextBoxColumn, lastNameDataGridViewTextBoxColumn, firstNameDataGridViewTextBoxColumn, amountDataGridViewTextBoxColumn });
            dgvTips.DataSource = tipBindingSource;
            dgvTips.Location = new Point(3, 3);
            dgvTips.Name = "dgvTips";
            dgvTips.ReadOnly = true;
            dgvTips.RowHeadersVisible = false;
            dgvTips.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
            dgvTips.ShowCellErrors = false;
            dgvTips.ShowCellToolTips = false;
            dgvTips.ShowEditingIcon = false;
            dgvTips.ShowRowErrors = false;
            dgvTips.Size = new Size(415, 474);
            dgvTips.TabIndex = 0;
            // 
            // Number
            // 
            Number.AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill;
            Number.DataPropertyName = "Number";
            Number.FillWeight = 10F;
            Number.HeaderText = "No.";
            Number.Name = "Number";
            Number.ReadOnly = true;
            // 
            // serverNumberDataGridViewTextBoxColumn
            // 
            serverNumberDataGridViewTextBoxColumn.AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill;
            serverNumberDataGridViewTextBoxColumn.DataPropertyName = "ServerNumber";
            serverNumberDataGridViewTextBoxColumn.FillWeight = 15F;
            serverNumberDataGridViewTextBoxColumn.HeaderText = "Server";
            serverNumberDataGridViewTextBoxColumn.Name = "serverNumberDataGridViewTextBoxColumn";
            serverNumberDataGridViewTextBoxColumn.ReadOnly = true;
            // 
            // lastNameDataGridViewTextBoxColumn
            // 
            lastNameDataGridViewTextBoxColumn.AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill;
            lastNameDataGridViewTextBoxColumn.DataPropertyName = "LastName";
            lastNameDataGridViewTextBoxColumn.FillWeight = 35F;
            lastNameDataGridViewTextBoxColumn.HeaderText = "Last Name";
            lastNameDataGridViewTextBoxColumn.Name = "lastNameDataGridViewTextBoxColumn";
            lastNameDataGridViewTextBoxColumn.ReadOnly = true;
            // 
            // firstNameDataGridViewTextBoxColumn
            // 
            firstNameDataGridViewTextBoxColumn.AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill;
            firstNameDataGridViewTextBoxColumn.DataPropertyName = "FirstName";
            firstNameDataGridViewTextBoxColumn.FillWeight = 25F;
            firstNameDataGridViewTextBoxColumn.HeaderText = "First Name";
            firstNameDataGridViewTextBoxColumn.Name = "firstNameDataGridViewTextBoxColumn";
            firstNameDataGridViewTextBoxColumn.ReadOnly = true;
            // 
            // amountDataGridViewTextBoxColumn
            // 
            amountDataGridViewTextBoxColumn.AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill;
            amountDataGridViewTextBoxColumn.DataPropertyName = "Amount";
            dataGridViewCellStyle1.Alignment = DataGridViewContentAlignment.MiddleRight;
            dataGridViewCellStyle1.Format = "N2";
            dataGridViewCellStyle1.NullValue = null;
            amountDataGridViewTextBoxColumn.DefaultCellStyle = dataGridViewCellStyle1;
            amountDataGridViewTextBoxColumn.FillWeight = 15F;
            amountDataGridViewTextBoxColumn.HeaderText = "Amount";
            amountDataGridViewTextBoxColumn.Name = "amountDataGridViewTextBoxColumn";
            amountDataGridViewTextBoxColumn.ReadOnly = true;
            // 
            // tipBindingSource
            // 
            tipBindingSource.DataSource = typeof(ViewData.TipView);
            // 
            // QuickTipEditor
            // 
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            Controls.Add(splTipEntrySplitter);
            Name = "QuickTipEditor";
            Size = new Size(640, 480);
            splTipEntrySplitter.Panel1.ResumeLayout(false);
            splTipEntrySplitter.Panel1.PerformLayout();
            splTipEntrySplitter.Panel2.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)splTipEntrySplitter).EndInit();
            splTipEntrySplitter.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)dgvTips).EndInit();
            ((System.ComponentModel.ISupportInitialize)tipBindingSource).EndInit();
            ResumeLayout(false);
        }

        #endregion

        private SplitContainer splTipEntrySplitter;
        private DataGridView dgvTips;
        private QuickEntryForm tipEntryForm;
        private Label lblTotal;
        private BindingSource tipBindingSource;
        private DataGridViewTextBoxColumn Number;
        private DataGridViewTextBoxColumn serverNumberDataGridViewTextBoxColumn;
        private DataGridViewTextBoxColumn lastNameDataGridViewTextBoxColumn;
        private DataGridViewTextBoxColumn firstNameDataGridViewTextBoxColumn;
        private DataGridViewTextBoxColumn amountDataGridViewTextBoxColumn;
    }
}
