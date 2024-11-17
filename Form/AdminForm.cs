using System;
using System.Data;
using System.Drawing;
using System.Windows.Forms;
using System.Xml.Linq;
using Npgsql;

namespace ClothCycles
{
    public partial class AdminForm : Form
    {
        public AdminForm(Admin account, string connString)
        {
            InitializeComponent();
        }

        private NpgsqlConnection conn;
        string connstring = "Host=localhost;Port=5432;Username=postgres;Password=Yefta21n0404;Database=ClothCycles;Include Error Detail=true"; // Ganti dengan nama database Anda
        public DataTable dt;
        public static NpgsqlCommand cmd;
        private string sql = null;
        private ListViewItem selectedItem;

        private void AdminForm_Load(object sender, EventArgs e)
        {
            conn = new NpgsqlConnection(connstring);

            // Ensure columns are added only once to prevent duplicates
            if (dataGridViewAccounts.Columns.Count == 0)
            {
                dataGridViewAccounts.Columns.Add("username", "Username");
                dataGridViewAccounts.Columns.Add("email", "Email");
                dataGridViewAccounts.Columns.Add("role", "Role");
                dataGridViewAccounts.Columns.Add("name", "Name");

                // Adding ID column (hidden)
                dataGridViewAccounts.Columns.Add("id", "ID");
                dataGridViewAccounts.Columns["id"].Visible = false; // Hide the ID column if not needed
            }

            // Now load the data
            LoadData();
        }


        private void LoadData()
        {
            dt = ReadAccounts();
            dataGridViewAccounts.Rows.Clear();  // Clear rows sebelumnya

            foreach (DataRow row in dt.Rows)
            {
                // Tambahkan baris baru pada DataGridView
                int rowIndex = dataGridViewAccounts.Rows.Add();
                DataGridViewRow rowData = dataGridViewAccounts.Rows[rowIndex];

                rowData.Cells["username"].Value = row["username"];
                rowData.Cells["email"].Value = row["email"];
                rowData.Cells["role"].Value = row["role"];
                rowData.Cells["name"].Value = row["name"];
                rowData.Tag = row["id"];  // Simpan ID untuk referensi
            }
        }

        private DataTable ReadAccounts()
        {
            DataTable dataTable = new DataTable();
            try
            {
                conn.Open();
                sql = "SELECT * FROM account"; // Mengambil semua data dari tabel account
                cmd = new NpgsqlCommand(sql, conn);
                NpgsqlDataReader rd = cmd.ExecuteReader();
                dataTable.Load(rd);
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error: " + ex.Message, "Load Data FAIL!!", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            finally
            {
                conn.Close();
            }
            return dataTable;
        }

        private void btnInsert_Click(object sender, EventArgs e)
        {
            if (InsertAccount(txtUsername.Text, txtEmail.Text, txtPassword.Text, txtRole.Text, txtName.Text))
            {
                MessageBox.Show("Akun berhasil ditambahkan", "Well Done!", MessageBoxButtons.OK, MessageBoxIcon.Information);
                LoadData();
                ClearFields();
            }
        }

        private bool InsertAccount(string username, string email, string password, string role, string name)
        {
            int accountId = 0; // Deklarasikan variabel untuk menyimpan accountId
            try
            {
                conn.Open();
                sql = @"INSERT INTO account (username, email, password, role, name) VALUES (:_username, :_email, :_password, :_role, :_name) RETURNING id";
                cmd = new NpgsqlCommand(sql, conn);
                cmd.Parameters.AddWithValue("_username", username);
                cmd.Parameters.AddWithValue("_email", email);
                cmd.Parameters.AddWithValue("_password", password); // Pastikan untuk hashing password sebelum menyimpan
                cmd.Parameters.AddWithValue("_role", role);
                cmd.Parameters.AddWithValue("_name", name);

                // Eksekusi query dan ambil accountId
                accountId = (int)cmd.ExecuteScalar();

                // Tambahkan data ke tabel spesifik berdasarkan role
                if (role == "craftsman")
                {
                    string insertCraftsmanQuery = @"
                        INSERT INTO craftsmen (username, email, password, name, earned_points) 
                        VALUES (@username, @email, @password, @name, @earnedPoints)";
                    using (NpgsqlCommand cmd = new NpgsqlCommand(insertCraftsmanQuery, conn))
                    {
                        cmd.Parameters.AddWithValue("username", username);
                        cmd.Parameters.AddWithValue("email", email);
                        cmd.Parameters.AddWithValue("password", password);
                        cmd.Parameters.AddWithValue("name", name);
                        cmd.Parameters.AddWithValue("earnedPoints", 0); // Default earned points
                        cmd.ExecuteNonQuery();
                    }
                }
                else if (role == "user")
                {
                    string insertUserQuery = $"INSERT INTO users (accountid, points, name) VALUES (@accountid, @points, @name)";
                    using (NpgsqlCommand insertUserCmd = new NpgsqlCommand(insertUserQuery, conn))
                    {
                        insertUserCmd.Parameters.AddWithValue("accountid", accountId);
                        insertUserCmd.Parameters.AddWithValue("points", 0); // Default points adalah 0
                        insertUserCmd.Parameters.AddWithValue("name", name); // Tambahkan nilai name

                        insertUserCmd.ExecuteNonQuery();
                    }
                }
                else if (role == "admin")
                {
                    // Simpan data untuk admin
                    string insertAdminQuery = "INSERT INTO Admins (accountid, name) VALUES (@accountid, @name)";
                    using (NpgsqlCommand insertAdminCmd = new NpgsqlCommand(insertAdminQuery, conn))
                    {
                        insertAdminCmd.Parameters.AddWithValue("accountid", accountId);
                        insertAdminCmd.Parameters.AddWithValue("name", name); // Tambahkan nama admin

                        insertAdminCmd.ExecuteNonQuery();
                    }
                }

                return true;
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error: " + ex.Message, "Insert FAIL!!", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return false;
            }
            finally
            {
                conn.Close();
            }
        }

        private void dataGridViewAccounts_SelectionChanged(object sender, EventArgs e)
        {
            if (dataGridViewAccounts.SelectedRows.Count > 0)
            {
                // Dapatkan baris yang dipilih
                DataGridViewRow selectedRow = dataGridViewAccounts.SelectedRows[0];

                // Isi TextBox dengan nilai yang ada di baris yang dipilih
                txtUsername.Text = selectedRow.Cells["username"].Value.ToString();
                txtEmail.Text = selectedRow.Cells["email"].Value.ToString();
                txtRole.Text = selectedRow.Cells["role"].Value.ToString();
                txtName.Text = selectedRow.Cells["name"].Value.ToString();

                // Simpan ID untuk referensi saat update atau delete
                selectedItem = (ListViewItem)selectedRow.Tag; // Simpan ID untuk update/delete
            }
        }

        private void btnUpdate_Click(object sender, EventArgs e)
        {
            // Pastikan ada baris yang dipilih
            if (dataGridViewAccounts.SelectedRows.Count == 0)
            {
                MessageBox.Show("Silakan pilih akun yang akan diupdate", "Warning!", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            // Ambil name dari row yang dipilih (bukan ID)
            string name = dataGridViewAccounts.SelectedRows[0].Cells["name"].Value.ToString();

            // Panggil metode UpdateAccount dengan data dari TextBox
            if (UpdateAccount(name, txtUsername.Text, txtEmail.Text, txtPassword.Text, txtRole.Text, txtName.Text))
            {
                MessageBox.Show("Akun berhasil diupdate", "Well Done!", MessageBoxButtons.OK, MessageBoxIcon.Information);
                LoadData(); // Reload data setelah update
                ClearFields(); // Kosongkan TextBox
            }
        }

        private bool UpdateAccount(string name, string username, string email, string password, string role, string newName)
        {
            try
            {
                conn.Open();

                // Ambil role dari tabel account berdasarkan name
                string roleFromDB = string.Empty;
                sql = @"SELECT role FROM account WHERE name = :_name";
                cmd = new NpgsqlCommand(sql, conn);
                cmd.Parameters.AddWithValue("_name", name);
                var result = cmd.ExecuteScalar();

                if (result != null)
                {
                    roleFromDB = result.ToString();
                }

                // Cek role dan lakukan pembaruan data berdasarkan role
                if (roleFromDB == "admin")
                {
                    // Jika role admin, update data dari tabel users yang terkait
                    sql = @"UPDATE admins SET name = :_newName WHERE name = :_name";
                    cmd = new NpgsqlCommand(sql, conn);
                    cmd.Parameters.AddWithValue("_name", name);
                    cmd.Parameters.AddWithValue("_newName", newName);
                    cmd.ExecuteNonQuery();  // Update data name di tabel users
                }
                else if (roleFromDB == "user")
                {
                    // Jika role user, update data dari tabel lain sesuai kebutuhan
                    sql = @"UPDATE users SET name = :_newName WHERE name = :_name";
                    cmd = new NpgsqlCommand(sql, conn);
                    cmd.Parameters.AddWithValue("_name", name);
                    cmd.Parameters.AddWithValue("_newName", newName);
                    cmd.ExecuteNonQuery();  // Update data name di tabel users
                }
                else
                {
                    // Jika role lainnya, Anda bisa menyesuaikan
                    MessageBox.Show("Role tidak dikenali", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return false;
                }

                // Lanjutkan pembaruan data di tabel account
                sql = @"UPDATE account SET username = :_username, email = :_email, password = :_password, role = :_role, name = :_newName WHERE name = :_name";
                cmd = new NpgsqlCommand(sql, conn);
                cmd.Parameters.AddWithValue("_name", name);  // Gunakan name untuk menemukan record yang akan diupdate
                cmd.Parameters.AddWithValue("_username", username);
                cmd.Parameters.AddWithValue("_email", email);
                cmd.Parameters.AddWithValue("_password", password); // Pastikan untuk hashing password sebelum menyimpan
                cmd.Parameters.AddWithValue("_role", role);
                cmd.Parameters.AddWithValue("_newName", newName);

                return cmd.ExecuteNonQuery() == 1; // Mengembalikan true jika satu baris terpengaruh
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error: " + ex.Message, "Update FAIL!!", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return false;
            }
            finally
            {
                conn.Close();
            }
        }

        private void btnDelete_Click(object sender, EventArgs e)
        {
            // Pastikan ada baris yang dipilih
            if (dataGridViewAccounts.SelectedRows.Count == 0)
            {
                MessageBox.Show("Silakan pilih akun yang akan dihapus", "Warning!", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            // Dapatkan username dari row yang dipilih untuk konfirmasi
            string username = dataGridViewAccounts.SelectedRows[0].Cells["username"].Value.ToString();
            string name = dataGridViewAccounts.SelectedRows[0].Cells["name"].Value.ToString();

            // Konfirmasi penghapusan
            if (MessageBox.Show($"Apakah Anda yakin ingin menghapus akun {username}?", "Konfirmasi Hapus",
                                MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
            {
                // Panggil metode DeleteAccount dengan username
                if (DeleteAccount(username, name))
                {
                    MessageBox.Show("Akun berhasil dihapus", "Well Done!", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    LoadData(); // Reload data setelah penghapusan
                    ClearFields(); // Kosongkan TextBox
                }
            }
        }

        private bool DeleteAccount(string username, string name)
        {
            try
            {
                conn.Open();

                // Ambil role dari tabel account berdasarkan username
                string role = string.Empty;
                sql = @"SELECT role FROM account WHERE username = :_username";
                cmd = new NpgsqlCommand(sql, conn);
                cmd.Parameters.AddWithValue("_username", username);
                var result = cmd.ExecuteScalar();

                if (result != null)
                {
                    role = result.ToString();
                }

                // Cek role dan lakukan penghapusan berdasarkan role
                if (role == "admin")
                {
                    // Jika role admin, hapus data dari tabel users yang terkait
                    sql = @"DELETE FROM admins WHERE name = :_name";
                    cmd = new NpgsqlCommand(sql, conn);
                    cmd.Parameters.AddWithValue("_name", name);
                    cmd.ExecuteNonQuery();  // Hapus referensi di users
                }
                else if (role == "user")
                {
                    // Jika role user, hapus data dari tabel lain sesuai kebutuhan
                    sql = @"DELETE FROM users WHERE name = :_name";
                    cmd = new NpgsqlCommand(sql, conn);
                    cmd.Parameters.AddWithValue("_name", name);
                    cmd.ExecuteNonQuery();  // Hapus data user
                }
                else if (role == "craftsman")
                {
                    // Jika role user, hapus data dari tabel lain sesuai kebutuhan
                    sql = @"DELETE FROM craftsmen WHERE name = :_name";
                    cmd = new NpgsqlCommand(sql, conn);
                    cmd.Parameters.AddWithValue("_name", name);
                    cmd.ExecuteNonQuery();  // Hapus data user
                }
                else
                {
                    // Jika role lainnya, Anda bisa menyesuaikan
                    MessageBox.Show("Role tidak dikenali", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return false;
                }

                // Hapus akun dari tabel account setelah memeriksa dan menghapus referensi
                sql = @"DELETE FROM account WHERE username = :_username";
                cmd = new NpgsqlCommand(sql, conn);
                cmd.Parameters.AddWithValue("_username", username);

                return cmd.ExecuteNonQuery() == 1; // Mengembalikan true jika satu baris terpengaruh
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error: " + ex.Message, "Delete FAIL!!", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return false;
            }
            finally
            {
                conn.Close();
            }
        }

        private void ClearFields()
        {
            txtName.Clear();
            txtEmail.Clear();
            txtPassword.Clear(); // Jika ada field password
            txtRole.Clear();     // Field untuk role
            txtUsername.Clear();     // Field untuk nama
        }
    }
}