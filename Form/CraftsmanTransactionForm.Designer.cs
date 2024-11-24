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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(CraftsmanTransactionForm));
            this.dataGridViewItem = new System.Windows.Forms.DataGridView();
            this.txtAddress = new System.Windows.Forms.TextBox();
            this.txtNominal = new System.Windows.Forms.TextBox();
            this.dataGridViewSelectedItem = new System.Windows.Forms.DataGridView();
            this.btnSelect = new System.Windows.Forms.Button();
            this.btnSaveItem = new System.Windows.Forms.Button();
            this.btnDelete = new System.Windows.Forms.Button();
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
            this.txtAddress.Location = new System.Drawing.Point(123, 187);
            this.txtAddress.Name = "txtAddress";
            this.txtAddress.Size = new System.Drawing.Size(171, 22);
            this.txtAddress.TabIndex = 13;
            this.txtAddress.TextChanged += new System.EventHandler(this.txtAddress_TextChanged);
            // 
            // txtNominal
            // 
            this.txtNominal.Location = new System.Drawing.Point(123, 265);
            this.txtNominal.Name = "txtNominal";
            this.txtNominal.Size = new System.Drawing.Size(171, 22);
            this.txtNominal.TabIndex = 14;
            this.txtNominal.TextChanged += new System.EventHandler(this.txtNominal_TextChanged);
            // 
            // dataGridViewSelectedItem
            // 
            this.dataGridViewSelectedItem.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dataGridViewSelectedItem.Location = new System.Drawing.Point(460, 343);
            this.dataGridViewSelectedItem.Name = "dataGridViewSelectedItem";
            this.dataGridViewSelectedItem.RowHeadersWidth = 51;
            this.dataGridViewSelectedItem.RowTemplate.Height = 24;
            this.dataGridViewSelectedItem.Size = new System.Drawing.Size(728, 185);
            this.dataGridViewSelectedItem.TabIndex = 16;
            this.dataGridViewSelectedItem.CellContentClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.dataGridViewSelectedItem_CellContentClick);
            // 
            // btnSelect
            // 
            this.btnSelect.Font = new System.Drawing.Font("Microsoft Sans Serif", 7.8F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnSelect.Location = new System.Drawing.Point(113, 427);
            this.btnSelect.Name = "btnSelect";
            this.btnSelect.Size = new System.Drawing.Size(128, 58);
            this.btnSelect.TabIndex = 17;
            this.btnSelect.Text = "Select";
            this.btnSelect.UseVisualStyleBackColor = true;
            this.btnSelect.Click += new System.EventHandler(this.btnSelect_Click);
            // 
            // btnSaveItem
            // 
            this.btnSaveItem.Font = new System.Drawing.Font("Microsoft Sans Serif", 7.8F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnSaveItem.Location = new System.Drawing.Point(273, 360);
            this.btnSaveItem.Name = "btnSaveItem";
            this.btnSaveItem.Size = new System.Drawing.Size(96, 35);
            this.btnSaveItem.TabIndex = 18;
            this.btnSaveItem.Text = "Save Item";
            this.btnSaveItem.UseVisualStyleBackColor = true;
            this.btnSaveItem.Click += new System.EventHandler(this.btnSaveItem_Click);
            // 
            // btnDelete
            // 
            this.btnDelete.Font = new System.Drawing.Font("Microsoft Sans Serif", 7.8F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnDelete.Location = new System.Drawing.Point(273, 401);
            this.btnDelete.Name = "btnDelete";
            this.btnDelete.Size = new System.Drawing.Size(96, 35);
            this.btnDelete.TabIndex = 19;
            this.btnDelete.Text = "Delete";
            this.btnDelete.UseVisualStyleBackColor = true;
            this.btnDelete.Click += new System.EventHandler(this.btnDelete_Click);
            // 
            // CraftsmanTransactionForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("$this.BackgroundImage")));
            this.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.ClientSize = new System.Drawing.Size(1200, 540);
            this.Controls.Add(this.btnDelete);
            this.Controls.Add(this.btnSaveItem);
            this.Controls.Add(this.btnSelect);
            this.Controls.Add(this.dataGridViewSelectedItem);
            this.Controls.Add(this.txtNominal);
            this.Controls.Add(this.txtAddress);
            this.Controls.Add(this.dataGridViewItem);
            this.DoubleBuffered = true;
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
        private System.Windows.Forms.TextBox txtNominal;
        private System.Windows.Forms.DataGridView dataGridViewSelectedItem;
        private System.Windows.Forms.Button btnSelect;
        private System.Windows.Forms.Button btnSaveItem;
        private System.Windows.Forms.Button btnDelete;
    }
}