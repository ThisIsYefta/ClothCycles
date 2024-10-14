namespace ClothCycles
{
    partial class TransactionForm
    {
        private System.ComponentModel.IContainer components = null;

        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        private void InitializeComponent()
        {
            this.comboBoxUsers = new System.Windows.Forms.ComboBox();
            this.comboBoxCraftsmen = new System.Windows.Forms.ComboBox();
            this.txtAmount = new System.Windows.Forms.TextBox();
            this.radioButtonDeposit = new System.Windows.Forms.RadioButton();
            this.radioButtonWithdrawal = new System.Windows.Forms.RadioButton();
            this.btnSubmit = new System.Windows.Forms.Button();
            this.lblErrorMessage = new System.Windows.Forms.Label();
            this.SuspendLayout();

            // 
            // comboBoxUsers
            // 
            this.comboBoxUsers.FormattingEnabled = true;
            this.comboBoxUsers.Location = new System.Drawing.Point(50, 30);
            this.comboBoxUsers.Name = "comboBoxUsers";
            this.comboBoxUsers.Size = new System.Drawing.Size(200, 24);
            this.comboBoxUsers.TabIndex = 0;

            // 
            // comboBoxCraftsmen
            // 
            this.comboBoxCraftsmen.FormattingEnabled = true;
            this.comboBoxCraftsmen.Location = new System.Drawing.Point(50, 70);
            this.comboBoxCraftsmen.Name = "comboBoxCraftsmen";
            this.comboBoxCraftsmen.Size = new System.Drawing.Size(200, 24);
            this.comboBoxCraftsmen.TabIndex = 1;

            // 
            // txtAmount
            // 
            this.txtAmount.Location = new System.Drawing.Point(50, 110);
            this.txtAmount.Name = "txtAmount";
            this.txtAmount.Size = new System.Drawing.Size(200, 22);
            this.txtAmount.TabIndex = 2;

            // 
            // radioButtonDeposit
            // 
            this.radioButtonDeposit.AutoSize = true;
            this.radioButtonDeposit.Location = new System.Drawing.Point(50, 150);
            this.radioButtonDeposit.Name = "radioButtonDeposit";
            this.radioButtonDeposit.Size = new System.Drawing.Size(78, 20);
            this.radioButtonDeposit.TabIndex = 3;
            this.radioButtonDeposit.TabStop = true;
            this.radioButtonDeposit.Text = "Deposit";

            // 
            // radioButtonWithdrawal
            // 
            this.radioButtonWithdrawal.AutoSize = true;
            this.radioButtonWithdrawal.Location = new System.Drawing.Point(150, 150);
            this.radioButtonWithdrawal.Name = "radioButtonWithdrawal";
            this.radioButtonWithdrawal.Size = new System.Drawing.Size(102, 20);
            this.radioButtonWithdrawal.TabIndex = 4;
            this.radioButtonWithdrawal.TabStop = true;
            this.radioButtonWithdrawal.Text = "Withdrawal";

            // 
            // btnSubmit
            // 
            this.btnSubmit.Location = new System.Drawing.Point(50, 200);
            this.btnSubmit.Name = "btnSubmit";
            this.btnSubmit.Size = new System.Drawing.Size(75, 30);
            this.btnSubmit.TabIndex = 5;
            this.btnSubmit.Text = "Submit";
            this.btnSubmit.UseVisualStyleBackColor = true;
            this.btnSubmit.Click += new System.EventHandler(this.btnSubmit_Click);

            // 
            // lblErrorMessage
            // 
            this.lblErrorMessage.AutoSize = true;
            this.lblErrorMessage.ForeColor = System.Drawing.Color.Red;
            this.lblErrorMessage.Location = new System.Drawing.Point(50, 240);
            this.lblErrorMessage.Name = "lblErrorMessage";
            this.lblErrorMessage.Size = new System.Drawing.Size(0, 16);
            this.lblErrorMessage.TabIndex = 6;
            this.lblErrorMessage.Visible = false;

            // 
            // TransactionForm
            // 
            this.ClientSize = new System.Drawing.Size(300, 300);
            this.Controls.Add(this.lblErrorMessage);
            this.Controls.Add(this.btnSubmit);
            this.Controls.Add(this.radioButtonWithdrawal);
            this.Controls.Add(this.radioButtonDeposit);
            this.Controls.Add(this.txtAmount);
            this.Controls.Add(this.comboBoxCraftsmen);
            this.Controls.Add(this.comboBoxUsers);
            this.Name = "TransactionForm";
            this.Text = "Transaction Form";
            this.ResumeLayout(false);
            this.PerformLayout();
        }

        private System.Windows.Forms.ComboBox comboBoxUsers;
        private System.Windows.Forms.ComboBox comboBoxCraftsmen;
        private System.Windows.Forms.TextBox txtAmount;
        private System.Windows.Forms.RadioButton radioButtonDeposit;
        private System.Windows.Forms.RadioButton radioButtonWithdrawal;
        private System.Windows.Forms.Button btnSubmit;
        private System.Windows.Forms.Label lblErrorMessage;
    }
}
