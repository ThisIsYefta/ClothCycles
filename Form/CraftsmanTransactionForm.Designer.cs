namespace ClothCycles
{
    partial class CraftsmanTransactionForm
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

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.dataGridViewItem = new System.Windows.Forms.DataGridView();
            this.txtAddress = new System.Windows.Forms.TextBox();
            this.lblAddress = new System.Windows.Forms.Label();
            this.txtNominal = new System.Windows.Forms.TextBox();
            this.dataGridViewSelectedItem = new System.Windows.Forms.DataGridView();
            this.btnSelect = new System.Windows.Forms.Button();
            this.btnSaveItem = new System.Windows.Forms.Button();
            ((System.ComponentModel.ISupportInitialize)(this.dataGridViewItem)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.dataGridViewSelectedItem)).BeginInit();
            this.SuspendLayout();
            // 
            // dataGridViewItem
            // 
            this.dataGridViewItem.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dataGridViewItem.Location = new System.Drawing.Point(460, 12);
            this.dataGridViewItem.Name = "dataGridViewItem";
            this.dataGridViewItem.RowHeadersWidth = 51;
            this.dataGridViewItem.RowTemplate.Height = 24;
            this.dataGridViewItem.Size = new System.Drawing.Size(728, 288);
            this.dataGridViewItem.TabIndex = 11;
            this.dataGridViewItem.CellContentClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.dataGridViewItem_CellContentClick);
            // 
            // txtAddress
            // 
            this.txtAddress.Location = new System.Drawing.Point(162, 119);
            this.txtAddress.Name = "txtAddress";
            this.txtAddress.Size = new System.Drawing.Size(171, 22);
            this.txtAddress.TabIndex = 13;
            this.txtAddress.TextChanged += new System.EventHandler(this.txtAddress_TextChanged);
            // 
            // lblAddress
            // 
            this.lblAddress.AutoSize = true;
            this.lblAddress.Location = new System.Drawing.Point(76, 125);
            this.lblAddress.Name = "lblAddress";
            this.lblAddress.Size = new System.Drawing.Size(64, 16);
            this.lblAddress.TabIndex = 12;
            this.lblAddress.Text = "Address :";
            this.lblAddress.Click += new System.EventHandler(this.lblAddress_Click);
            // 
            // txtNominal
            // 
            this.txtNominal.Location = new System.Drawing.Point(139, 190);
            this.txtNominal.Name = "txtNominal";
            this.txtNominal.Size = new System.Drawing.Size(100, 22);
            this.txtNominal.TabIndex = 14;
            this.txtNominal.TextChanged += new System.EventHandler(this.txtNominal_TextChanged);
            // 
            // dataGridViewSelectedItem
            // 
            this.dataGridViewSelectedItem.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dataGridViewSelectedItem.Location = new System.Drawing.Point(460, 317);
            this.dataGridViewSelectedItem.Name = "dataGridViewSelectedItem";
            this.dataGridViewSelectedItem.RowHeadersWidth = 51;
            this.dataGridViewSelectedItem.RowTemplate.Height = 24;
            this.dataGridViewSelectedItem.Size = new System.Drawing.Size(728, 211);
            this.dataGridViewSelectedItem.TabIndex = 16;
            this.dataGridViewSelectedItem.CellContentClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.dataGridViewSelectedItem_CellContentClick);
            // 
            // btnSelect
            // 
            this.btnSelect.Location = new System.Drawing.Point(123, 376);
            this.btnSelect.Name = "btnSelect";
            this.btnSelect.Size = new System.Drawing.Size(128, 58);
            this.btnSelect.TabIndex = 17;
            this.btnSelect.Text = "Select";
            this.btnSelect.UseVisualStyleBackColor = true;
            this.btnSelect.Click += new System.EventHandler(this.btnSelect_Click);
            // 
            // btnSaveItem
            // 
            this.btnSaveItem.Location = new System.Drawing.Point(279, 317);
            this.btnSaveItem.Name = "btnSaveItem";
            this.btnSaveItem.Size = new System.Drawing.Size(75, 23);
            this.btnSaveItem.TabIndex = 18;
            this.btnSaveItem.Text = "Save Item";
            this.btnSaveItem.UseVisualStyleBackColor = true;
            this.btnSaveItem.Click += new System.EventHandler(this.btnSaveItem_Click);
            // 
            // CraftsmanTransactionForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1200, 540);
            this.Controls.Add(this.btnSaveItem);
            this.Controls.Add(this.btnSelect);
            this.Controls.Add(this.dataGridViewSelectedItem);
            this.Controls.Add(this.txtNominal);
            this.Controls.Add(this.txtAddress);
            this.Controls.Add(this.lblAddress);
            this.Controls.Add(this.dataGridViewItem);
            this.Name = "CraftsmanTransactionForm";
            this.Text = "CraftsmanTransactionForm";
            this.Load += new System.EventHandler(this.CraftsmanTransactionForm_Load);
            ((System.ComponentModel.ISupportInitialize)(this.dataGridViewItem)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.dataGridViewSelectedItem)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.DataGridView dataGridViewItem;
        private System.Windows.Forms.TextBox txtAddress;
        private System.Windows.Forms.Label lblAddress;
        private System.Windows.Forms.TextBox txtNominal;
        private System.Windows.Forms.DataGridView dataGridViewSelectedItem;
        private System.Windows.Forms.Button btnSelect;
        private System.Windows.Forms.Button btnSaveItem;
    }
}