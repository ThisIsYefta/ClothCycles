using System;
using System.Collections.Generic;
using System.Windows.Forms;
using Npgsql;

namespace ClothCycles
{
    public partial class UsersForm : Form
    {
        private User currentUser;
        private NpgsqlConnection conn; 

        // List of material types
        private readonly List<string> materialTypes = new List<string>
        {
            "Cotton",
            "Silk",
            "Wool",
            "Polyester",
            "Linen",
            "Rayon"
            // Bisa Tambah Material Lain
        };

        public UsersForm(User user, string connString)
        {
            InitializeComponent();
            currentUser = user;
            conn = new NpgsqlConnection(connString); 
            conn.Open(); 

            LoadMaterialTypes(); 
            LoadUploadedItems(); 
        }

        private void UsersForm_Load(object sender, EventArgs e)
        {
            LoadUploadedItems();
        }

        private void LoadMaterialTypes()
        {
            cmbMaterialType.Items.Clear(); 

            foreach (var type in materialTypes)
            {
                cmbMaterialType.Items.Add(type); 
            }

            if (cmbMaterialType.Items.Count > 0)
                cmbMaterialType.SelectedIndex = 0;
        }

        private void LoadUploadedItems()
        {
            try
            {
                if (conn.State != System.Data.ConnectionState.Open)
                {
                    conn.Open(); 
                }

                currentUser.LoadUploadedItems(conn); 

                dataGridViewItems.Rows.Clear(); 

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

        private void btnPostItem_Click(object sender, EventArgs e)
        {
            string materialType = cmbMaterialType.SelectedItem.ToString(); 
            string modelName = txtItemName.Text.Trim(); 
            string description = txtDescription.Text.Trim(); 
            int quantity;

            if (!int.TryParse(txtQuantity.Text.Trim(), out quantity) || quantity <= 0)
            {
                MessageBox.Show("Please enter a valid quantity."); 
                return;
            }

            Item newItem = new Item(0, materialType, modelName, description, quantity, currentUser); 

            try
            {
                currentUser.UploadItem(newItem, conn); 
                MessageBox.Show("Item posted successfully!"); 
                LoadUploadedItems(); 
            }
            catch (Exception ex)
            {
                MessageBox.Show($"An error occurred while posting the item: {ex.Message}", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }


        private void btnGoToTransactions_Click_1(object sender, EventArgs e)
        {
            this.Hide(); 

            UsersTransactionForm transactionForm = new UsersTransactionForm(currentUser, conn);
            transactionForm.FormClosed += (s, args) => this.Show(); 
            transactionForm.Show(); 
        }

        private void btnDelete_Click(object sender, EventArgs e)
        {
            if (dataGridViewItems.SelectedRows.Count == 0)
            {
                MessageBox.Show("Please select an item to delete.", "Info", MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
            }

            var selectedRow = dataGridViewItems.SelectedRows[0];
            var itemModel = selectedRow.Cells[0].Value.ToString();

            try
            {
                var itemToDelete = currentUser.UploadedItems.Find(item => item.Model == itemModel);

                if (itemToDelete == null)
                {
                    MessageBox.Show("The selected item could not be found.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return;
                }

                currentUser.DeleteItem(itemToDelete, conn);

                MessageBox.Show("Item deleted successfully!", "Success", MessageBoxButtons.OK, MessageBoxIcon.Information);

                LoadUploadedItems();
            }
            catch (Exception ex)
            {
                MessageBox.Show($"An error occurred while deleting the item: {ex.Message}", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
    }
}