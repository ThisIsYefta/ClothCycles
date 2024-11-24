namespace ClothCycles
{
    partial class UsersForm
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(UsersForm));
            this.cmbMaterialType = new System.Windows.Forms.ComboBox();
            this.txtItemName = new System.Windows.Forms.TextBox();
            this.txtDescription = new System.Windows.Forms.TextBox();
            this.txtQuantity = new System.Windows.Forms.TextBox();
            this.btnPostItem = new System.Windows.Forms.Button();
            this.btnGoToTransactions = new System.Windows.Forms.Button();
            this.dataGridViewItems = new System.Windows.Forms.DataGridView();
            this.Model = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.MaterialType = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Deskripsi = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Kuantitas = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Username = new System.Windows.Forms.DataGridViewTextBoxColumn();
            ((System.ComponentModel.ISupportInitialize)(this.dataGridViewItems)).BeginInit();
            this.SuspendLayout();
            // 
            // cmbMaterialType
            // 
            this.cmbMaterialType.FormattingEnabled = true;
            this.cmbMaterialType.Location = new System.Drawing.Point(131, 209);
            this.cmbMaterialType.Name = "cmbMaterialType";
            this.cmbMaterialType.Size = new System.Drawing.Size(100, 24);
            this.cmbMaterialType.TabIndex = 0;
            // 
            // txtItemName
            // 
            this.txtItemName.Location = new System.Drawing.Point(131, 181);
            this.txtItemName.Name = "txtItemName";
            this.txtItemName.Size = new System.Drawing.Size(100, 22);
            this.txtItemName.TabIndex = 1;
            // 
            // txtDescription
            // 
            this.txtDescription.Location = new System.Drawing.Point(131, 239);
            this.txtDescription.Name = "txtDescription";
            this.txtDescription.Size = new System.Drawing.Size(100, 22);
            this.txtDescription.TabIndex = 2;
            // 
            // txtQuantity
            // 
            this.txtQuantity.Location = new System.Drawing.Point(131, 278);
            this.txtQuantity.Name = "txtQuantity";
            this.txtQuantity.Size = new System.Drawing.Size(100, 22);
            this.txtQuantity.TabIndex = 3;
            // 
            // btnPostItem
            // 
            this.btnPostItem.Font = new System.Drawing.Font("Microsoft Sans Serif", 7.8F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnPostItem.Location = new System.Drawing.Point(51, 356);
            this.btnPostItem.Name = "btnPostItem";
            this.btnPostItem.Size = new System.Drawing.Size(91, 37);
            this.btnPostItem.TabIndex = 4;
            this.btnPostItem.Text = "Post Item";
            this.btnPostItem.UseVisualStyleBackColor = true;
            this.btnPostItem.Click += new System.EventHandler(this.btnPostItem_Click);
            // 
            // btnGoToTransactions
            // 
            this.btnGoToTransactions.Font = new System.Drawing.Font("Microsoft Sans Serif", 7.8F, ((System.Drawing.FontStyle)((System.Drawing.FontStyle.Bold | System.Drawing.FontStyle.Italic))), System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnGoToTransactions.Location = new System.Drawing.Point(637, 45);
            this.btnGoToTransactions.Name = "btnGoToTransactions";
            this.btnGoToTransactions.Size = new System.Drawing.Size(149, 61);
            this.btnGoToTransactions.TabIndex = 5;
            this.btnGoToTransactions.Text = "Go To Transactions";
            this.btnGoToTransactions.UseVisualStyleBackColor = true;
            this.btnGoToTransactions.Click += new System.EventHandler(this.btnGoToTransactions_Click_1);
            // 
            // dataGridViewItems
            // 
            this.dataGridViewItems.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dataGridViewItems.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.Model,
            this.MaterialType,
            this.Deskripsi,
            this.Kuantitas,
            this.Username});
            this.dataGridViewItems.Location = new System.Drawing.Point(263, 148);
            this.dataGridViewItems.Name = "dataGridViewItems";
            this.dataGridViewItems.RowHeadersWidth = 51;
            this.dataGridViewItems.RowTemplate.Height = 24;
            this.dataGridViewItems.Size = new System.Drawing.Size(533, 310);
            this.dataGridViewItems.TabIndex = 11;
            // 
            // Model
            // 
            this.Model.HeaderText = "Model";
            this.Model.MinimumWidth = 6;
            this.Model.Name = "Model";
            this.Model.Width = 125;
            // 
            // MaterialType
            // 
            this.MaterialType.HeaderText = "MaterialType";
            this.MaterialType.MinimumWidth = 6;
            this.MaterialType.Name = "MaterialType";
            this.MaterialType.Width = 125;
            // 
            // Deskripsi
            // 
            this.Deskripsi.HeaderText = "Deskripsi";
            this.Deskripsi.MinimumWidth = 6;
            this.Deskripsi.Name = "Deskripsi";
            this.Deskripsi.Width = 125;
            // 
            // Kuantitas
            // 
            this.Kuantitas.HeaderText = "Kuantitas";
            this.Kuantitas.MinimumWidth = 6;
            this.Kuantitas.Name = "Kuantitas";
            this.Kuantitas.Width = 125;
            // 
            // Username
            // 
            this.Username.HeaderText = "Username";
            this.Username.MinimumWidth = 6;
            this.Username.Name = "Username";
            this.Username.Width = 125;
            // 
            // UsersForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("$this.BackgroundImage")));
            this.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.ClientSize = new System.Drawing.Size(823, 503);
            this.Controls.Add(this.dataGridViewItems);
            this.Controls.Add(this.btnGoToTransactions);
            this.Controls.Add(this.btnPostItem);
            this.Controls.Add(this.txtQuantity);
            this.Controls.Add(this.txtDescription);
            this.Controls.Add(this.txtItemName);
            this.Controls.Add(this.cmbMaterialType);
            this.DoubleBuffered = true;
            this.Name = "UsersForm";
            this.Text = "UsersForm";
            this.Load += new System.EventHandler(this.UsersForm_Load);
            ((System.ComponentModel.ISupportInitialize)(this.dataGridViewItems)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.ComboBox cmbMaterialType;
        private System.Windows.Forms.TextBox txtItemName;
        private System.Windows.Forms.TextBox txtDescription;
        private System.Windows.Forms.TextBox txtQuantity;
        private System.Windows.Forms.Button btnPostItem;
        private System.Windows.Forms.Button btnGoToTransactions;
        private System.Windows.Forms.DataGridView dataGridViewItems;
        private System.Windows.Forms.DataGridViewTextBoxColumn Model;
        private System.Windows.Forms.DataGridViewTextBoxColumn MaterialType;
        private System.Windows.Forms.DataGridViewTextBoxColumn Deskripsi;
        private System.Windows.Forms.DataGridViewTextBoxColumn Kuantitas;
        private System.Windows.Forms.DataGridViewTextBoxColumn Username;
    }
}