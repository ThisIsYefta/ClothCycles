using System;
using System.Collections.Generic;
using System.Windows.Forms;
using Npgsql;

namespace ClothCycles
{
    public partial class UsersForm : Form
    {
        private User currentUser; // Store the current user
        private NpgsqlConnection conn; // Database connection

        // List of material types
        private readonly List<string> materialTypes = new List<string>
        {
            "Cotton",
            "Silk",
            "Wool",
            "Polyester",
            "Linen",
            "Rayon"
            // Tambahkan tipe material lain sesuai kebutuhan
        };

        public UsersForm(User user, string connString)
        {
            InitializeComponent();
            currentUser = user;
            conn = new NpgsqlConnection(connString); // Buat koneksi baru dengan connString
            conn.Open(); // Buka koneksi di sini atau di setiap metode saat diperlukan

            LoadMaterialTypes(); // Load material types to ComboBox
            LoadUploadedItems(); // Load items menggunakan koneksi baru
        }

        private void UsersForm_Load(object sender, EventArgs e)
        {
            LoadUploadedItems();
        }

        // Method to load material types into ComboBox
        private void LoadMaterialTypes()
        {
            cmbMaterialType.Items.Clear(); // Clear existing items

            foreach (var type in materialTypes)
            {
                cmbMaterialType.Items.Add(type); // Add each material type to ComboBox
            }

            if (cmbMaterialType.Items.Count > 0)
                cmbMaterialType.SelectedIndex = 0; // Set default selected item (optional)
        }

        // Method to load uploaded items from the database
        private void LoadUploadedItems()
        {
            try
            {
                if (conn.State != System.Data.ConnectionState.Open)
                {
                    conn.Open(); // Ensure connection is open before loading items
                }

                currentUser.LoadUploadedItems(conn); // Load items from database

                dataGridViewItems.Rows.Clear(); // Clear existing rows from DataGridView

                foreach (var item in currentUser.UploadedItems)
                {
                    dataGridViewItems.Rows.Add(item.Model, item.MaterialType, item.Description, item.Quantity, item.User.Username);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"An error occurred while loading items: {ex.Message}", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        // Event handler for button to upload a new item
        private void btnPostItem_Click(object sender, EventArgs e)
        {
            string materialType = cmbMaterialType.SelectedItem.ToString(); // Get selected material type
            string modelName = txtItemName.Text.Trim(); // Get item name from TextBox
            string description = txtDescription.Text.Trim(); // Get description from TextBox
            int quantity;

            if (!int.TryParse(txtQuantity.Text.Trim(), out quantity) || quantity <= 0)
            {
                MessageBox.Show("Please enter a valid quantity."); // Show error if quantity is invalid
                return;
            }

            Item newItem = new Item(0, materialType, modelName, description, quantity, currentUser); // Create new item

            try
            {
                currentUser.UploadItem(newItem, conn); // Upload item to database
                MessageBox.Show("Item posted successfully!"); // Show success message
                LoadUploadedItems(); // Refresh the list of uploaded items
            }
            catch (Exception ex)
            {
                MessageBox.Show($"An error occurred while posting the item: {ex.Message}", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }


        private void btnGoToTransactions_Click_1(object sender, EventArgs e)
        {
            this.Hide(); // Hide UsersForm

            UsersTransactionForm transactionForm = new UsersTransactionForm(currentUser, conn);
            transactionForm.Show(); // Show UsersTransactionForm

            this.Close(); // Close UsersForm after opening Transactions form

        }
    }
}