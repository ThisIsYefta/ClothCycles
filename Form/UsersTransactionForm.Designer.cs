﻿namespace ClothCycles
{
    partial class UsersTransactionForm
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(UsersTransactionForm));
            this.label1 = new System.Windows.Forms.Label();
            this.lblPoint = new System.Windows.Forms.Label();
            this.lblNormalPrice = new System.Windows.Forms.Label();
            this.txtPoint = new System.Windows.Forms.TextBox();
            this.lblEstimatedPrice = new System.Windows.Forms.Label();
            this.btnEstimate = new System.Windows.Forms.Button();
            this.btnBuy = new System.Windows.Forms.Button();
            this.txtAddress = new System.Windows.Forms.TextBox();
            this.dataGridViewProduct = new System.Windows.Forms.DataGridView();
            this.txtNominal = new System.Windows.Forms.TextBox();
            ((System.ComponentModel.ISupportInitialize)(this.dataGridViewProduct)).BeginInit();
            this.SuspendLayout();
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("Microsoft Sans Serif", 7.8F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label1.Location = new System.Drawing.Point(223, 22);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(50, 16);
            this.label1.TabIndex = 1;
            this.label1.Text = "Points";
            // 
            // lblPoint
            // 
            this.lblPoint.AutoSize = true;
            this.lblPoint.Font = new System.Drawing.Font("Microsoft Sans Serif", 7.8F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblPoint.Location = new System.Drawing.Point(223, 60);
            this.lblPoint.Name = "lblPoint";
            this.lblPoint.Size = new System.Drawing.Size(50, 16);
            this.lblPoint.TabIndex = 2;
            this.lblPoint.Text = "Points";
            this.lblPoint.Click += new System.EventHandler(this.lblPoint_Click);
            // 
            // lblNormalPrice
            // 
            this.lblNormalPrice.AutoSize = true;
            this.lblNormalPrice.Font = new System.Drawing.Font("Microsoft Sans Serif", 7.8F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblNormalPrice.Location = new System.Drawing.Point(39, 251);
            this.lblNormalPrice.Name = "lblNormalPrice";
            this.lblNormalPrice.Size = new System.Drawing.Size(97, 16);
            this.lblNormalPrice.TabIndex = 3;
            this.lblNormalPrice.Text = "Normal Price";
            this.lblNormalPrice.Click += new System.EventHandler(this.lblNormalPrice_Click);
            // 
            // txtPoint
            // 
            this.txtPoint.Location = new System.Drawing.Point(168, 301);
            this.txtPoint.Name = "txtPoint";
            this.txtPoint.Size = new System.Drawing.Size(143, 22);
            this.txtPoint.TabIndex = 4;
            this.txtPoint.TextChanged += new System.EventHandler(this.txtPoint_TextChanged);
            // 
            // lblEstimatedPrice
            // 
            this.lblEstimatedPrice.AutoSize = true;
            this.lblEstimatedPrice.Font = new System.Drawing.Font("Microsoft Sans Serif", 7.8F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblEstimatedPrice.Location = new System.Drawing.Point(28, 418);
            this.lblEstimatedPrice.Name = "lblEstimatedPrice";
            this.lblEstimatedPrice.Size = new System.Drawing.Size(128, 16);
            this.lblEstimatedPrice.TabIndex = 5;
            this.lblEstimatedPrice.Text = "Estimated Price >";
            this.lblEstimatedPrice.Click += new System.EventHandler(this.lblEstimatedPrice_Click);
            // 
            // btnEstimate
            // 
            this.btnEstimate.Font = new System.Drawing.Font("Microsoft Sans Serif", 7.8F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnEstimate.Location = new System.Drawing.Point(221, 370);
            this.btnEstimate.Name = "btnEstimate";
            this.btnEstimate.Size = new System.Drawing.Size(90, 23);
            this.btnEstimate.TabIndex = 6;
            this.btnEstimate.Text = "Estimate";
            this.btnEstimate.UseVisualStyleBackColor = true;
            this.btnEstimate.Click += new System.EventHandler(this.btnEstimate_Click);
            // 
            // btnBuy
            // 
            this.btnBuy.Font = new System.Drawing.Font("Microsoft Sans Serif", 7.8F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnBuy.Location = new System.Drawing.Point(145, 461);
            this.btnBuy.Name = "btnBuy";
            this.btnBuy.Size = new System.Drawing.Size(128, 58);
            this.btnBuy.TabIndex = 7;
            this.btnBuy.Text = "Buy";
            this.btnBuy.UseVisualStyleBackColor = true;
            this.btnBuy.Click += new System.EventHandler(this.btnBuy_Click);
            // 
            // txtAddress
            // 
            this.txtAddress.Location = new System.Drawing.Point(145, 189);
            this.txtAddress.Name = "txtAddress";
            this.txtAddress.Size = new System.Drawing.Size(171, 22);
            this.txtAddress.TabIndex = 9;
            this.txtAddress.TextChanged += new System.EventHandler(this.txtAddress_TextChanged);
            // 
            // dataGridViewProduct
            // 
            this.dataGridViewProduct.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dataGridViewProduct.Location = new System.Drawing.Point(367, 12);
            this.dataGridViewProduct.Name = "dataGridViewProduct";
            this.dataGridViewProduct.RowHeadersWidth = 51;
            this.dataGridViewProduct.RowTemplate.Height = 24;
            this.dataGridViewProduct.Size = new System.Drawing.Size(728, 518);
            this.dataGridViewProduct.TabIndex = 10;
            this.dataGridViewProduct.CellContentClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.dataGridViewProduct_CellContentClick);
            // 
            // txtNominal
            // 
            this.txtNominal.Location = new System.Drawing.Point(168, 342);
            this.txtNominal.Name = "txtNominal";
            this.txtNominal.Size = new System.Drawing.Size(143, 22);
            this.txtNominal.TabIndex = 11;
            this.txtNominal.TextChanged += new System.EventHandler(this.txtNominal_TextChanged);
            // 
            // UsersTransactionForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("$this.BackgroundImage")));
            this.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.ClientSize = new System.Drawing.Size(1107, 542);
            this.Controls.Add(this.txtNominal);
            this.Controls.Add(this.dataGridViewProduct);
            this.Controls.Add(this.txtAddress);
            this.Controls.Add(this.btnBuy);
            this.Controls.Add(this.btnEstimate);
            this.Controls.Add(this.lblEstimatedPrice);
            this.Controls.Add(this.txtPoint);
            this.Controls.Add(this.lblNormalPrice);
            this.Controls.Add(this.lblPoint);
            this.Controls.Add(this.label1);
            this.DoubleBuffered = true;
            this.Name = "UsersTransactionForm";
            this.Text = "UsersTransactionForm";
            this.Load += new System.EventHandler(this.UsersTransactionForm_Load);
            ((System.ComponentModel.ISupportInitialize)(this.dataGridViewProduct)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label lblPoint;
        private System.Windows.Forms.Label lblNormalPrice;
        private System.Windows.Forms.TextBox txtPoint;
        private System.Windows.Forms.Label lblEstimatedPrice;
        private System.Windows.Forms.Button btnEstimate;
        private System.Windows.Forms.Button btnBuy;
        private System.Windows.Forms.TextBox txtAddress;
        private System.Windows.Forms.DataGridView dataGridViewProduct;
        private System.Windows.Forms.TextBox txtNominal;
    }
}