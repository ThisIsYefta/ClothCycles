using Npgsql;
using System;
using System.Windows.Forms;

namespace ClothCycles
{
    public partial class UsersTransactionForm : Form
    {
        private User currentUser; // Store the current user
        private NpgsqlConnection conn; // Database connection

        public UsersTransactionForm(User user, NpgsqlConnection connection)
        {
            InitializeComponent(); // Initialize form components
            currentUser = user; // Store the current user
            conn = connection; // Store the database connection

            // For testing purposes, display a welcome message
            MessageBox.Show($"Welcome to Transactions, {currentUser.Username}!", "Welcome", MessageBoxButtons.OK, MessageBoxIcon.Information);
        }

        private void UsersTransactionForm_Load(object sender, EventArgs e)
        {
            // You can add any initialization code here if needed.
        }
    }
}