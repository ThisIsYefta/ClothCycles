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

            try
            {
                MessageBox.Show($"Current User ID: {currentUser.userid}");
                // Ambil nilai poin dari database
                string query = "SELECT points FROM users WHERE accountid = @accountId";

                using (var cmd = new NpgsqlCommand(query, conn))
                {
                    cmd.Parameters.AddWithValue("accountId", currentUser.userid);

                    var result = cmd.ExecuteScalar();
                    if (result != null && result != DBNull.Value)
                    {
                        currentUser.Points = Convert.ToInt32(result); // Set nilai poin
                        lblPoint.Text = $"Your Points: {currentUser.Points}"; // Tampilkan poin di label
                    }
                    else
                    {
                        currentUser.Points = 0; // Default jika poin tidak ditemukan
                        lblPoint.Text = "Your Points: 0";
                        MessageBox.Show("Points not found in database.");
                    }
                }

                // Muat daftar produk
                LoadProducts();
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
                    "price AS \"Price\", stock AS \"Stock\" " +
                    "FROM product WHERE stock > 0";
                NpgsqlDataAdapter adapter = new NpgsqlDataAdapter(query, conn);
                DataTable productTable = new DataTable();
                adapter.Fill(productTable);

                // Binding data ke DataGridView
                dataGridViewProduct.DataSource = productTable;
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
                // Dapatkan produk yang dipilih
                DataGridViewRow row = dataGridViewProduct.Rows[e.RowIndex];
                //int productId = Convert.ToInt32(row.Cells["product_id"].Value);
                int productId = Convert.ToInt32(row.Cells["Product ID"].Value); // Pastikan nama header cocok
                string name = row.Cells["Name"].Value.ToString();
                string description = row.Cells["Description"].Value.ToString();
                decimal price = Convert.ToDecimal(row.Cells["Price"].Value);
                int stock = Convert.ToInt32(row.Cells["Stock"].Value);

                // Buat objek produk terpilih
                selectedProduct = new Product(productId, name, description, price, stock, null);

                // Tampilkan harga normal di lblNormalPrice
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

                // Hitung harga estimasi
                decimal discount = pointsToUse * 3000;
                decimal estimatedPrice = (selectedProduct.Price * quantity) - discount;

                if (estimatedPrice < 0) estimatedPrice = 0;

                // Tampilkan harga setelah diskon
                lblEstimatedPrice.Text = $"Estimated Price: {estimatedPrice.ToString("C", new CultureInfo("id-ID"))}";
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

                if (totalPrice < 0) totalPrice = 0;

                // Update database untuk mengurangi poin pengguna, stok produk, dan membuat transaksi
                try
                {
                    using (var transaction = conn.BeginTransaction())
                    {
                        // Kurangi poin pengguna
                        string updatePointsQuery = "UPDATE users SET points = points - @points WHERE accountid = @accountId";
                        using (var cmd = new NpgsqlCommand(updatePointsQuery, conn))
                        {
                            cmd.Parameters.AddWithValue("points", pointsToUse);
                            cmd.Parameters.AddWithValue("accountId", currentUser.userid);
                            cmd.ExecuteNonQuery();
                        }

                        // Kurangi stok produk
                        string updateStockQuery = "UPDATE product SET stock = stock - @quantity WHERE product_id = @productId";
                        using (var cmd = new NpgsqlCommand(updateStockQuery, conn))
                        {
                            cmd.Parameters.AddWithValue("quantity", quantity);
                            cmd.Parameters.AddWithValue("productId", selectedProduct.product_id);
                            cmd.ExecuteNonQuery();
                        }

                        // Tambahkan transaksi
                        string insertTransactionQuery = @"
                            INSERT INTO transactions (item_id, craftsman_id, points_used, quantity, total_price, address, transaction_date) 
                            VALUES (@itemId, @craftsmanId, @pointsUsed, @quantity, @totalPrice, @address, @transactionDate)";
                        using (var cmd = new NpgsqlCommand(insertTransactionQuery, conn))
                        {
                            cmd.Parameters.AddWithValue("itemId", selectedProduct.product_id);
                            cmd.Parameters.AddWithValue("craftsmanId", selectedProduct.Craftsman.userid);
                            cmd.Parameters.AddWithValue("pointsUsed", pointsToUse);
                            cmd.Parameters.AddWithValue("quantity", quantity);
                            cmd.Parameters.AddWithValue("totalPrice", totalPrice);
                            cmd.Parameters.AddWithValue("address", txtAddress.Text);
                            cmd.Parameters.AddWithValue("transactionDate", DateTime.Now);
                            cmd.ExecuteNonQuery();
                        }

                        transaction.Commit();
                    }

                    // Tampilkan pesan sukses
                    MessageBox.Show($"Your order will be delivered to {txtAddress.Text}.");
                    lblPoint.Text = $"Your Points: {currentUser.Points - pointsToUse}";
                    LoadProducts(); // Refresh daftar produk
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
            // Reset semua input
            txtPoint.Text = "0";
            txtNominal.Text = "0";
            txtAddress.Text = "";
            lblNormalPrice.Text = "Normal Price: -";
            lblEstimatedPrice.Text = "Estimated Price: -";
            selectedProduct = null;

        }
        private void txtPoint_TextChanged(object sender, EventArgs e)
        {
            if (!int.TryParse(txtPoint.Text, out _))
            {
                MessageBox.Show("Please enter a valid number for points.");
                txtPoint.Text = "0";
            }
        }
        private void txtNominal_TextChanged(object sender, EventArgs e)
        {
            if (!int.TryParse(txtNominal.Text, out _))
            {
                MessageBox.Show("Please enter a valid number for quantity.");
                txtNominal.Text = "0";
            }
        }

        private void txtPoint_Enter(object sender, EventArgs e)
        {
            txtPoint.Text = "";
        }

        private void txtNominal_Enter(object sender, EventArgs e)
        {
            txtNominal.Text = "";
        }

        private void txtAddress_Enter(object sender, EventArgs e)
        {

        }

        private void txtAddress_TextChanged(object sender, EventArgs e)
        {

        }

        private void lblPoint_Click(object sender, EventArgs e)
        {
            MessageBox.Show("This shows your current points available for transactions.");
        }

        private void lblNormalPrice_Click(object sender, EventArgs e)
        {
            MessageBox.Show("This is the standard price of the selected product without discounts.");
        }

        private void lblEstimatedPrice_Click(object sender, EventArgs e)
        {
            MessageBox.Show("This is the total price after applying discounts and points.");
        }


    }
}
