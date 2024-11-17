using Npgsql;
using System;
using System.Data;
using System.Globalization;
using System.Windows.Forms;

namespace ClothCycles
{
    public partial class UsersTransactionForm : Form
    {
        private User currentUser; // Untuk menyimpan data user yang sedang login
        private NpgsqlConnection conn; // Koneksi ke database
        private Product selectedProduct; // Produk yang dipilih pengguna

        public UsersTransactionForm(User user, NpgsqlConnection connection)
        {
            InitializeComponent();
            currentUser = user;
            conn = connection;
        }

        private void UsersTransactionForm_Load(object sender, EventArgs e)
        {
            // Cek koneksi database
            if (conn.State != ConnectionState.Open)
            {
                MessageBox.Show("Database connection is not available.");
                Close();
                return; // Hentikan eksekusi jika koneksi tidak tersedia
            }

            LoadUserPoints();
            LoadProducts();
        }

        private void LoadUserPoints()
        {
            try
            {
                string query = "SELECT points FROM users WHERE accountid = @accountId";
                using (var cmd = new NpgsqlCommand(query, conn))
                {
                    cmd.Parameters.AddWithValue("accountId", currentUser.userid);
                    var result = cmd.ExecuteScalar();

                    currentUser.Points = result != null && result != DBNull.Value ? Convert.ToInt32(result) : 0;
                    lblPoint.Text = $"Your Points: {currentUser.Points}";
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error loading user points: " + ex.Message);
            }
        }

        private void LoadProducts()
        {
            try
            {
                string query = "SELECT product_id AS \"Product ID\", name AS \"Name\", description AS \"Description\", " +
                               "price AS \"Price\", stock AS \"Stock\" FROM product WHERE stock > 0";
                using (NpgsqlDataAdapter adapter = new NpgsqlDataAdapter(query, conn))
                {
                    DataTable productTable = new DataTable();
                    adapter.Fill(productTable);
                    dataGridViewProduct.DataSource = productTable; // Binding data ke DataGridView
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error loading products: " + ex.Message);
            }
        }

        private void dataGridViewProduct_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex >= 0)
            {
                DataGridViewRow row = dataGridViewProduct.Rows[e.RowIndex];
                int productId = Convert.ToInt32(row.Cells["Product ID"].Value);
                string name = row.Cells["Name"].Value.ToString();
                string description = row.Cells["Description"].Value.ToString();
                decimal price = Convert.ToDecimal(row.Cells["Price"].Value);
                int stock = Convert.ToInt32(row.Cells["Stock"].Value);

                selectedProduct = new Product(productId, name, description, price, stock, null);
                lblNormalPrice.Text = $"Normal Price: {price.ToString("C", new CultureInfo("id-ID"))}";
            }
        }

        private void btnEstimate_Click(object sender, EventArgs e)
        {
            if (selectedProduct == null)
            {
                MessageBox.Show("Please select a product first.");
                return;
            }

            if (int.TryParse(txtPoint.Text, out int pointsToUse) && int.TryParse(txtNominal.Text, out int quantity))
            {
                if (pointsToUse > currentUser.Points)
                {
                    MessageBox.Show("You do not have enough points.");
                    return;
                }

                if (quantity > selectedProduct.Stock)
                {
                    MessageBox.Show("The quantity exceeds available stock.");
                    return;
                }

                decimal discount = pointsToUse * 3000;
                decimal estimatedPrice = (selectedProduct.Price * quantity) - discount;

                lblEstimatedPrice.Text = $"Estimated Price: {Math.Max(estimatedPrice, 0).ToString("C", new CultureInfo("id-ID"))}";
            }
            else
            {
                MessageBox.Show("Invalid input for points or quantity.");
            }
        }

        private void btnBuy_Click(object sender, EventArgs e)
        {
            if (selectedProduct == null || string.IsNullOrWhiteSpace(txtAddress.Text))
            {
                MessageBox.Show("Please select a product and fill in your address.");
                return;
            }

            if (int.TryParse(txtPoint.Text, out int pointsToUse) && int.TryParse(txtNominal.Text, out int quantity))
            {
                decimal discount = pointsToUse * 3000;
                decimal totalPrice = (selectedProduct.Price * quantity) - discount;

                try
                {
                    using (var transaction = conn.BeginTransaction())
                    {
                        UpdateUserPoints(pointsToUse);
                        UpdateProductStock(quantity);
                        CreateTransaction(pointsToUse, quantity, totalPrice);

                        transaction.Commit();
                    }

                    MessageBox.Show($"Your order will be delivered to {txtAddress.Text}.");
                    lblPoint.Text = $"Your Points: {currentUser.Points - pointsToUse}";
                    LoadProducts(); // Refresh daftar produk
                    ResetInputs();
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Transaction failed: " + ex.Message);
                }
            }
            else
            {
                MessageBox.Show("Invalid input for points or quantity.");
            }
        }

        private void UpdateUserPoints(int pointsToUse)
        {
            string updatePointsQuery = "UPDATE users SET points = points - @points WHERE accountid = @accountId";
            using (var cmd = new NpgsqlCommand(updatePointsQuery, conn))
            {
                cmd.Parameters.AddWithValue("points", pointsToUse);
                cmd.Parameters.AddWithValue("accountId", currentUser.userid);
                cmd.ExecuteNonQuery();
            }
        }

        private void UpdateProductStock(int quantity)
        {
            string updateStockQuery = "UPDATE product SET stock = stock - @quantity WHERE product_id = @productId";
            using (var cmd = new NpgsqlCommand(updateStockQuery, conn))
            {
                cmd.Parameters.AddWithValue("quantity", quantity);
                cmd.Parameters.AddWithValue("productId", selectedProduct.product_id);
                cmd.ExecuteNonQuery();
            }
        }

        private void CreateTransaction(int pointsUsed, int quantity, decimal totalPrice)
        {
            string insertTransactionQuery = @"
            INSERT INTO transactions (item_id, craftsman_id, points_used, quantity, total_price, address, transaction_date) 
            VALUES (@itemId, @craftsmanId, @pointsUsed, @quantity, @totalPrice, @address, @transactionDate)";

            using (var cmd = new NpgsqlCommand(insertTransactionQuery, conn))
            {
                cmd.Parameters.AddWithValue("itemId", selectedProduct.product_id);

                // Pastikan untuk menggunakan ID craftsman yang baru ditambahkan
                if (selectedProduct.Craftsman != null)
                {
                    cmd.Parameters.AddWithValue("craftsmanId", selectedProduct.Craftsman.id); // Menggunakan id dari Craftsman
                }
                else
                {
                    MessageBox.Show("Selected product does not have an associated craftsman.");
                    return;
                }

                cmd.Parameters.AddWithValue("pointsUsed", pointsUsed);
                cmd.Parameters.AddWithValue("quantity", quantity);
                cmd.Parameters.AddWithValue("totalPrice", totalPrice);
                cmd.Parameters.AddWithValue("address", txtAddress.Text);

                // Menggunakan DateTime.UtcNow untuk konsistensi waktu.
                cmd.Parameters.AddWithValue("transactionDate", DateTime.UtcNow);

                cmd.ExecuteNonQuery();
            }
        }

        private void ResetInputs()
        {
            txtPoint.Text = "0";
            txtNominal.Text = "0";
            txtAddress.Clear();

            lblNormalPrice.Text = "Normal Price: -";
            lblEstimatedPrice.Text = "Estimated Price: -";

            selectedProduct = null;
        }

        private void txtPoint_TextChanged(object sender, EventArgs e) => ValidateNumericInput(txtPoint);

        private void txtNominal_TextChanged(object sender, EventArgs e) => ValidateNumericInput(txtNominal);

        private void ValidateNumericInput(TextBox textBox)
        {
            if (!int.TryParse(textBox.Text, out _))
            {
                MessageBox.Show($"Please enter a valid number for {textBox.Name}.");
                textBox.Text = "0"; // Reset to default value
            }
        }

        private void txtPoint_Enter(object sender, EventArgs e) => txtPoint.Clear();

        private void txtNominal_Enter(object sender, EventArgs e) => txtNominal.Clear();

        private void lblPoint_Click(object sender, EventArgs e) => ShowInfoMessage(lblPoint.Text);

        private void lblNormalPrice_Click(object sender, EventArgs e) => ShowInfoMessage(lblNormalPrice.Text);

        private void lblEstimatedPrice_Click(object sender, EventArgs e) => ShowInfoMessage(lblEstimatedPrice.Text);

        private void ShowInfoMessage(string message) => MessageBox.Show(message); // Menampilkan pesan info
        private void txtAddress_TextChanged(object sender, EventArgs e) { }

    }
}