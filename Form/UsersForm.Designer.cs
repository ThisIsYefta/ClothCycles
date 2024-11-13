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
            this.cmbMaterialType = new System.Windows.Forms.ComboBox();
            this.txtItemName = new System.Windows.Forms.TextBox();
            this.txtDescription = new System.Windows.Forms.TextBox();
            this.txtQuantity = new System.Windows.Forms.TextBox();
            this.btnPostItem = new System.Windows.Forms.Button();
            this.btnGoToTransactions = new System.Windows.Forms.Button();
            this.listViewItems = new System.Windows.Forms.ListView();
            this.lblNamaItem = new System.Windows.Forms.Label();
            this.label1 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.SuspendLayout();
            // 
            // cmbMaterialType
            // 
            this.cmbMaterialType.FormattingEnabled = true;
            this.cmbMaterialType.Location = new System.Drawing.Point(122, 192);
            this.cmbMaterialType.Name = "cmbMaterialType";
            this.cmbMaterialType.Size = new System.Drawing.Size(121, 24);
            this.cmbMaterialType.TabIndex = 0;
            // 
            // txtItemName
            // 
            this.txtItemName.Location = new System.Drawing.Point(122, 148);
            this.txtItemName.Name = "txtItemName";
            this.txtItemName.Size = new System.Drawing.Size(100, 22);
            this.txtItemName.TabIndex = 1;
            // 
            // txtDescription
            // 
            this.txtDescription.Location = new System.Drawing.Point(122, 239);
            this.txtDescription.Name = "txtDescription";
            this.txtDescription.Size = new System.Drawing.Size(100, 22);
            this.txtDescription.TabIndex = 2;
            // 
            // txtQuantity
            // 
            this.txtQuantity.Location = new System.Drawing.Point(122, 290);
            this.txtQuantity.Name = "txtQuantity";
            this.txtQuantity.Size = new System.Drawing.Size(100, 22);
            this.txtQuantity.TabIndex = 3;
            // 
            // btnPostItem
            // 
            this.btnPostItem.Location = new System.Drawing.Point(39, 356);
            this.btnPostItem.Name = "btnPostItem";
            this.btnPostItem.Size = new System.Drawing.Size(91, 37);
            this.btnPostItem.TabIndex = 4;
            this.btnPostItem.Text = "Post Item";
            this.btnPostItem.UseVisualStyleBackColor = true;
            this.btnPostItem.Click += new System.EventHandler(this.btnPostItem_Click);
            // 
            // btnGoToTransactions
            // 
            this.btnGoToTransactions.Location = new System.Drawing.Point(684, 26);
            this.btnGoToTransactions.Name = "btnGoToTransactions";
            this.btnGoToTransactions.Size = new System.Drawing.Size(75, 23);
            this.btnGoToTransactions.TabIndex = 5;
            this.btnGoToTransactions.Text = "Go To Transactions";
            this.btnGoToTransactions.UseVisualStyleBackColor = true;
            // 
            // listViewItems
            // 
            this.listViewItems.HideSelection = false;
            this.listViewItems.Location = new System.Drawing.Point(282, 69);
            this.listViewItems.Name = "listViewItems";
            this.listViewItems.Size = new System.Drawing.Size(477, 337);
            this.listViewItems.TabIndex = 6;
            this.listViewItems.UseCompatibleStateImageBehavior = false;
            // 
            // lblNamaItem
            // 
            this.lblNamaItem.AutoSize = true;
            this.lblNamaItem.Location = new System.Drawing.Point(12, 154);
            this.lblNamaItem.Name = "lblNamaItem";
            this.lblNamaItem.Size = new System.Drawing.Size(78, 16);
            this.lblNamaItem.TabIndex = 7;
            this.lblNamaItem.Text = "Nama Item :";
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(12, 200);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(92, 16);
            this.label1.TabIndex = 8;
            this.label1.Text = "Tipe Material :";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(12, 245);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(70, 16);
            this.label2.TabIndex = 9;
            this.label2.Text = "Deskripsi: ";
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(12, 290);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(61, 16);
            this.label3.TabIndex = 10;
            this.label3.Text = "Quantity :";
            // 
            // UsersForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(800, 450);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.lblNamaItem);
            this.Controls.Add(this.listViewItems);
            this.Controls.Add(this.btnGoToTransactions);
            this.Controls.Add(this.btnPostItem);
            this.Controls.Add(this.txtQuantity);
            this.Controls.Add(this.txtDescription);
            this.Controls.Add(this.txtItemName);
            this.Controls.Add(this.cmbMaterialType);
            this.Name = "UsersForm";
            this.Text = "UsersForm";
            this.Load += new System.EventHandler(this.UsersForm_Load);
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
        private System.Windows.Forms.ListView listViewItems;
        private System.Windows.Forms.Label lblNamaItem;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label3;
    }
}