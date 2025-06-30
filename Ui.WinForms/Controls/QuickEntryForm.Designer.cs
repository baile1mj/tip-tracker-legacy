namespace TipTracker.Ui.Controls
{
    partial class QuickEntryForm
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
            lblServer = new Label();
            lblAmount = new Label();
            txtServer = new TextBox();
            txtAmount = new TextBox();
            txtServerName = new TextBox();
            btnAdd = new Button();
            btnClear = new Button();
            tlpFormLayout = new TableLayoutPanel();
            tlpFormLayout.SuspendLayout();
            SuspendLayout();
            // 
            // lblServer
            // 
            lblServer.AutoSize = true;
            lblServer.Dock = DockStyle.Fill;
            lblServer.Location = new Point(3, 0);
            lblServer.Name = "lblServer";
            lblServer.Size = new Size(74, 29);
            lblServer.TabIndex = 0;
            lblServer.Text = "Server";
            lblServer.TextAlign = ContentAlignment.MiddleLeft;
            // 
            // lblAmount
            // 
            lblAmount.AutoSize = true;
            lblAmount.Dock = DockStyle.Fill;
            lblAmount.Location = new Point(3, 29);
            lblAmount.Name = "lblAmount";
            lblAmount.Size = new Size(74, 29);
            lblAmount.TabIndex = 1;
            lblAmount.Text = "Amount";
            lblAmount.TextAlign = ContentAlignment.MiddleLeft;
            // 
            // txtServer
            // 
            txtServer.Dock = DockStyle.Fill;
            txtServer.Location = new Point(83, 3);
            txtServer.Name = "txtServer";
            txtServer.Size = new Size(164, 23);
            txtServer.TabIndex = 2;
            // 
            // txtAmount
            // 
            txtAmount.Dock = DockStyle.Fill;
            txtAmount.Location = new Point(83, 32);
            txtAmount.Name = "txtAmount";
            txtAmount.Size = new Size(164, 23);
            txtAmount.TabIndex = 3;
            // 
            // txtServerName
            // 
            txtServerName.BorderStyle = BorderStyle.FixedSingle;
            tlpFormLayout.SetColumnSpan(txtServerName, 2);
            txtServerName.Dock = DockStyle.Fill;
            txtServerName.Location = new Point(3, 61);
            txtServerName.Name = "txtServerName";
            txtServerName.ReadOnly = true;
            txtServerName.Size = new Size(244, 23);
            txtServerName.TabIndex = 4;
            txtServerName.TabStop = false;
            // 
            // btnAdd
            // 
            btnAdd.Anchor = AnchorStyles.Bottom | AnchorStyles.Right;
            btnAdd.Location = new Point(94, 97);
            btnAdd.Name = "btnAdd";
            btnAdd.Size = new Size(75, 23);
            btnAdd.TabIndex = 1;
            btnAdd.Text = "Add";
            btnAdd.UseVisualStyleBackColor = true;
            btnAdd.Click += btnAdd_Click;
            // 
            // btnClear
            // 
            btnClear.Anchor = AnchorStyles.Bottom | AnchorStyles.Right;
            btnClear.Location = new Point(175, 97);
            btnClear.Name = "btnClear";
            btnClear.Size = new Size(75, 23);
            btnClear.TabIndex = 2;
            btnClear.Text = "Clear";
            btnClear.UseVisualStyleBackColor = true;
            btnClear.Click += btnClear_Click;
            // 
            // tlpFormLayout
            // 
            tlpFormLayout.Anchor = AnchorStyles.Top | AnchorStyles.Left | AnchorStyles.Right;
            tlpFormLayout.ColumnCount = 2;
            tlpFormLayout.ColumnStyles.Add(new ColumnStyle(SizeType.Absolute, 80F));
            tlpFormLayout.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 100F));
            tlpFormLayout.Controls.Add(lblServer, 0, 0);
            tlpFormLayout.Controls.Add(txtServer, 1, 0);
            tlpFormLayout.Controls.Add(lblAmount, 0, 1);
            tlpFormLayout.Controls.Add(txtServerName, 0, 2);
            tlpFormLayout.Controls.Add(txtAmount, 1, 1);
            tlpFormLayout.Location = new Point(0, 0);
            tlpFormLayout.Name = "tlpFormLayout";
            tlpFormLayout.RowCount = 4;
            tlpFormLayout.RowStyles.Add(new RowStyle());
            tlpFormLayout.RowStyles.Add(new RowStyle());
            tlpFormLayout.RowStyles.Add(new RowStyle());
            tlpFormLayout.RowStyles.Add(new RowStyle(SizeType.Percent, 100F));
            tlpFormLayout.Size = new Size(250, 90);
            tlpFormLayout.TabIndex = 0;
            // 
            // QuickEntryForm
            // 
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            Controls.Add(tlpFormLayout);
            Controls.Add(btnClear);
            Controls.Add(btnAdd);
            Name = "QuickEntryForm";
            Size = new Size(250, 120);
            tlpFormLayout.ResumeLayout(false);
            tlpFormLayout.PerformLayout();
            ResumeLayout(false);
        }

        #endregion

        private Label lblServer;
        private Label lblAmount;
        private TextBox txtServer;
        private TextBox txtAmount;
        private TextBox txtServerName;
        private Button btnAdd;
        private Button btnClear;
        private TableLayoutPanel tlpFormLayout;
    }
}
