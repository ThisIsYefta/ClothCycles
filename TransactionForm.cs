using System;
using System.Collections.Generic;
using System.Windows.Forms;

namespace ClothCycles
{
    public partial class TransactionForm : Form
    {
        private List<User> users; // Assume you have a user list
        private List<Craftsman> craftsmen; // Assume you have a craftsman list

        public TransactionForm(List<User> users, List<Craftsman> craftsmen)
        {
            InitializeComponent();
            this.users = users;
            this.craftsmen = craftsmen;
            LoadUsersAndCraftsmen();
        }

        private void LoadUsersAndCraftsmen()
        {
            // Load users into the ComboBox
            foreach (var user in users)
            {
                comboBoxUsers.Items.Add(user.Username); // Assuming User has a Username property
            }

            // Load craftsmen into the ComboBox (if needed)
            foreach (var craftsman in craftsmen)
            {
                comboBoxCraftsmen.Items.Add(craftsman.Username); // Assuming Craftsman has a Username property
            }
        }

        private void btnSubmit_Click(object sender, EventArgs e)
        {
            // Handle the transaction logic
            if (ValidateInput())
            {
                string selectedUser = comboBoxUsers.SelectedItem?.ToString();
                string selectedCraftsman = comboBoxCraftsmen.SelectedItem?.ToString();
                decimal transactionAmount;

                if (decimal.TryParse(txtAmount.Text.Trim(), out transactionAmount))
                {
                    string transactionType = radioButtonDeposit.Checked ? "Deposit" : "Withdrawal";
                    // Process the transaction (implement your logic here)
                    ProcessTransaction(selectedUser, selectedCraftsman, transactionAmount, transactionType);
                    MessageBox.Show("Transaction processed successfully!", "Success");
                }
                else
                {
                    lblErrorMessage.Text = "Invalid amount. Please enter a valid number.";
                    lblErrorMessage.Visible = true;
                }
            }
        }

        private bool ValidateInput()
        {
            // Validate that a user or craftsman is selected and amount is entered
            if (comboBoxUsers.SelectedItem == null && comboBoxCraftsmen.SelectedItem == null)
            {
                lblErrorMessage.Text = "Please select a user or craftsman.";
                lblErrorMessage.Visible = true;
                return false;
            }

            if (string.IsNullOrWhiteSpace(txtAmount.Text))
            {
                lblErrorMessage.Text = "Please enter an amount.";
                lblErrorMessage.Visible = true;
                return false;
            }

            lblErrorMessage.Visible = false; // Hide error message if validation passes
            return true;
        }

        private void ProcessTransaction(string user, string craftsman, decimal amount, string type)
        {
            // Implement your transaction logic here
            // This could involve updating user/craftsman balances, logging the transaction, etc.
            if (type == "Deposit")
            {
                // Example logic for deposit
                MessageBox.Show($"Deposited {amount} for {user ?? craftsman}.", "Deposit Transaction");
            }
            else
            {
                // Example logic for withdrawal
                MessageBox.Show($"Withdrew {amount} for {user ?? craftsman}.", "Withdrawal Transaction");
            }
        }
    }
}
