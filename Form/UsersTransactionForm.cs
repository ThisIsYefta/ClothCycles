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
                    cmd.Parameters.AddWithValue("accountId", currentUser.Accountid);
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
                // Pastikan kolom sudah ditambahkan
                dataGridViewProduct.Columns.Clear();
                dataGridViewProduct.Columns.Add("Product ID", "Product ID");
                dataGridViewProduct.Columns.Add("Name", "Name");
                dataGridViewProduct.Columns.Add("Description", "Description");
                dataGridViewProduct.Columns.Add("Price", "Price");
                dataGridViewProduct.Columns.Add("Stock", "Stock");

                string query = @"SELECT p.product_id AS ""Product ID"", p.name AS ""Name"", 
                         p.description AS ""Description"", p.price AS ""Price"", 
                         p.stock AS ""Stock"", c.id AS ""Craftsman ID"", 
                         c.name AS ""Craftsman Name"" 
                         FROM product p
                         LEFT JOIN craftsmen c ON p.craftsmen_id = c.id 
                         WHERE p.stock > 0";

                using (NpgsqlCommand cmd = new NpgsqlCommand(query, conn))
                using (NpgsqlDataReader reader = cmd.ExecuteReader())
                {
                    dataGridViewProduct.Rows.Clear(); // Kosongkan DataGridView

                    while (reader.Read())
                    {
                        int productId = reader.GetInt32(0);
                        string name = reader.GetString(1);
                        string description = reader.GetString(2);
                        decimal price = reader.GetDecimal(3);
                        int stock = reader.GetInt32(4);

                        // Ambil informasi craftsman jika tersedia
                        int craftsmanId = reader["Craftsman ID"] != DBNull.Value ? Convert.ToInt32(reader["Craftsman ID"]) : 0;
                        string craftsmanName = reader["Craftsman Name"] != DBNull.Value ? reader["Craftsman Name"].ToString() : null;

                        Craftsman craftsman = craftsmanId > 0 ? new Craftsman(craftsmanId, "", "", "", craftsmanName, 0) : null;

                        // Buat objek produk
                        Product product = new Product(productId, name, description, price, stock, craftsman);

                        // Tambahkan ke DataGridView
                        dataGridViewProduct.Rows.Add(product.product_id, product.Name, product.Description, product.Price, product.Stock);
                    }
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

                        // Update earned_points for the craftsman
                        UpdateCraftsmanEarnedPoints(selectedProduct.Craftsman?.id ?? 0, pointsToUse);

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
                cmd.Parameters.AddWithValue("accountId", currentUser.Accountid);
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
            try
            {
                // Validasi poin tidak melebihi total harga
                if (pointsUsed > totalPrice)
                {
                    MessageBox.Show("Jumlah poin yang digunakan tidak dapat melebihi total harga.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return;
                }

                // Menghitung harga setelah diskon poin
                decimal discountedPrice = totalPrice - pointsUsed;

                // Cari user_id berdasarkan accountid
                int userId = GetUserIdByAccountId(currentUser.Accountid);  // Ambil userid berdasarkan accountid

                if (userId == -1)
                {
                    MessageBox.Show("User tidak ditemukan.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return;
                }

                // Query untuk menyimpan transaksi
                string insertTransactionQuery = @"
                      INSERT INTO transactions (user_id, product_id, craftsman_id, points_used, quantity, total_price, address, transaction_date) 
                      VALUES (@userId, @productId, @craftsmanId, @pointsUsed, @quantity, @totalPrice, @address, @transactionDate)";

                using (var cmd = new NpgsqlCommand(insertTransactionQuery, conn))
                {
                    cmd.Parameters.AddWithValue("userId", userId);  // Gunakan userid yang valid
                    cmd.Parameters.AddWithValue("productId", selectedProduct.product_id);

                    // Menggunakan craftsman_id, jika ada
                    if (selectedProduct.Craftsman != null)
                    {
                        cmd.Parameters.AddWithValue("craftsmanId", selectedProduct.Craftsman.id);  // craftsman_id jika ada
                    }
                    else
                    {
                        cmd.Parameters.AddWithValue("craftsmanId", DBNull.Value);  // NULL jika tidak ada craftsman
                    }

                    cmd.Parameters.AddWithValue("pointsUsed", pointsUsed);
                    cmd.Parameters.AddWithValue("quantity", quantity);
                    cmd.Parameters.AddWithValue("totalPrice", discountedPrice);  // Gunakan harga setelah diskon
                    cmd.Parameters.AddWithValue("address", txtAddress.Text);
                    cmd.Parameters.AddWithValue("transactionDate", DateTime.UtcNow);  // Waktu transaksi

                    // Eksekusi query untuk menyimpan transaksi
                    cmd.ExecuteNonQuery();
                }

                // Mengurangi stok produk setelah transaksi
                string updateStockQuery = "UPDATE product SET stock = stock - @quantity WHERE product_id = @productId";
                using (var updateCmd = new NpgsqlCommand(updateStockQuery, conn))
                {
                    updateCmd.Parameters.AddWithValue("quantity", quantity);
                    updateCmd.Parameters.AddWithValue("productId", selectedProduct.product_id);
                    updateCmd.ExecuteNonQuery();
                }

                // Mengurangi poin pengguna
                string updateUserPointsQuery = "UPDATE users SET points = points - @pointsUsed WHERE userid = @userId";
                using (var updatePointsCmd = new NpgsqlCommand(updateUserPointsQuery, conn))
                {
                    updatePointsCmd.Parameters.AddWithValue("pointsUsed", pointsUsed);
                    updatePointsCmd.Parameters.AddWithValue("userId", userId);  // Gunakan userid yang valid
                    updatePointsCmd.ExecuteNonQuery();
                }

                MessageBox.Show("Transaksi berhasil dilakukan.", "Sukses", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            catch (Exception ex)
            {
                // Menangani jika terjadi kesalahan pada proses transaksi
                MessageBox.Show($"Terjadi kesalahan: {ex.Message}", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        // Fungsi untuk mendapatkan userid berdasarkan accountid
        private int GetUserIdByAccountId(int accountId)
        {
            string query = "SELECT userid FROM users WHERE accountid = @accountid";
            using (var cmd = new NpgsqlCommand(query, conn))
            {
                cmd.Parameters.AddWithValue("accountid", accountId);

                var result = cmd.ExecuteScalar();
                if (result != null)
                {
                    return Convert.ToInt32(result);
                }
                else
                {
                    return -1;  // Jika tidak ditemukan, kembalikan -1
                }
            }
        }






        private void UpdateCraftsmanEarnedPoints(int craftsmanId, int pointsUsed)
        {
            if (craftsmanId > 0) // Pastikan craftsmanId valid
            {
                string updateCraftsmanPointsQuery = "UPDATE craftsmen SET earned_points = earned_points + @points WHERE id = @craftsmanId";

                using (var cmd = new NpgsqlCommand(updateCraftsmanPointsQuery, conn))
                {
                    cmd.Parameters.AddWithValue("points", pointsUsed);
                    cmd.Parameters.AddWithValue("craftsmanId", craftsmanId);
                    cmd.ExecuteNonQuery();
                }
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

        private void txtAddress_TextChanged(object sender, EventArgs e)
        {
        }
    }
}