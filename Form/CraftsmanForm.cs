using System;
using System.Collections.Generic;
using System.Windows.Forms;
using Npgsql;

namespace ClothCycles
{
    public partial class CraftsmanForm : Form
    {
        private Craftsman currentCraftsman;
        private NpgsqlConnection conn;
        private string connectionString; // Tambahkan connectionString sebagai properti kelas


        public CraftsmanForm(Craftsman craftsman, string connString)
        {
            InitializeComponent();
            currentCraftsman = craftsman;
            conn = new NpgsqlConnection(connString);
            conn.Open();
            InitializeDataGridView();
            LoadUploadedProducts();
        }

        private void InitializeDataGridView()
        {
            dataGridViewProducts.Columns.Clear();
            dataGridViewProducts.Columns.Add("ProductName", "Product Name");
            dataGridViewProducts.Columns.Add("Price", "Price");
            dataGridViewProducts.Columns.Add("Description", "Description");
            dataGridViewProducts.Columns.Add("Stock", "Stock");
        }

        private void CraftsmanForm_Load(object sender, EventArgs e)
        {
            LoadUploadedProducts();
        }

        private void btnPostProduct_Click(object sender, EventArgs e)
        {
            string productName = txtProductName.Text.Trim();
            decimal priceText;
            string description = txtDescription.Text.Trim();
            int stockText;

            if (string.IsNullOrEmpty(productName) || string.IsNullOrEmpty(description))
            {
                MessageBox.Show("All fields must be filled.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            if (!decimal.TryParse(txtPrice.Text.Trim(), out priceText) || priceText <= 0)
            {
                MessageBox.Show("Please enter a valid price.");
                return;
            }

            if (!int.TryParse(txtStock.Text.Trim(), out stockText) || stockText <= 0)
            {
                MessageBox.Show("Please enter a valid stock quantity.");
                return;
            }

            Product newProduct = new Product(0, productName, description, priceText, stockText, currentCraftsman);

            try
            {
                currentCraftsman.UploadProduct(newProduct, conn);
                MessageBox.Show("Product posted successfully!");
                LoadUploadedProducts();
                ClearFields();
            }
            catch (Exception ex)
            {
                MessageBox.Show($"An error occurred while posting the product: {ex.Message}", "Error", MessageBoxButtons.OK);
            }
        }

        private void ClearFields()
        {
            txtProductName.Clear();
            txtPrice.Clear();
            txtDescription.Clear();
            txtStock.Clear();
        }

        private void LoadUploadedProducts()
        {
            try
            {
                if (conn.State != System.Data.ConnectionState.Open)
                {
                    conn.Open(); // Ensure connection is open before loading items
                }

                currentCraftsman.LoadUploadedProducts(conn); // Load items from database

                dataGridViewProducts.Rows.Clear(); // Clear existing rows from DataGridView

                foreach (var product in currentCraftsman.UploadedProducts)
                {
                    dataGridViewProducts.Rows.Add(product.Name, product.Price, product.Description, product.Stock);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"An error occurred while loading items: {ex.Message}", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void btnGoToTransactions_Click(object sender, EventArgs e)
        {
            this.Hide(); // Hide UsersForm

            CraftsmanTransactionForm craftsmantransactionForm = new CraftsmanTransactionForm(currentCraftsman, conn);
            craftsmantransactionForm.FormClosed += (s, args) => this.Show(); // Tampilkan LoginForm kembali saat UsersForm ditutup
            craftsmantransactionForm.Show(); // Show UsersTransactionForm
        }
        
    }
}
