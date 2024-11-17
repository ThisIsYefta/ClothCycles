using System;
using System.Windows.Forms;
using Npgsql;

namespace ClothCycles
{
    public partial class SignUpForm : Form
    {
        private string connString = "Host=localhost;Port=5432;Username=postgres;Password=Yefta21n0404;Database=ClothCycles";

        public SignUpForm()
        {
            InitializeComponent();
            // Inisialisasi ComboBox dengan role
            cmbRole.Items.Add("user");
            cmbRole.Items.Add("craftsman");
            cmbRole.Items.Add("admin");
            cmbRole.SelectedIndex = 0; // Default ke 'user'
        }

        private void SignUpForm_Load(object sender, EventArgs e)
        {
            // Initialize any components or data here if needed
        }

        private void btnSignUp_Click(object sender, EventArgs e)
        {
            string username = txtUsername.Text.Trim();
            string email = txtEmail.Text.Trim();
            string password = txtPassword.Text.Trim();
            string name = txtName.Text.Trim(); // Ambil nama dari TextBox
            string role = cmbRole.SelectedItem.ToString(); // Ambil role dari ComboBox

            if (string.IsNullOrEmpty(username) || string.IsNullOrEmpty(email) || string.IsNullOrEmpty(password) || string.IsNullOrEmpty(name))
            {
                MessageBox.Show("Semua field harus diisi.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            using (NpgsqlConnection conn = new NpgsqlConnection(connString))
            {
                conn.Open();

                // Cek apakah username sudah ada
                string checkQuery = "SELECT COUNT(*) FROM account WHERE username = @username";
                using (NpgsqlCommand checkCmd = new NpgsqlCommand(checkQuery, conn))
                {
                    checkCmd.Parameters.AddWithValue("username", username);
                    int userCount = Convert.ToInt32(checkCmd.ExecuteScalar());

                    if (userCount > 0)
                    {
                        MessageBox.Show("Username sudah terdaftar. Silakan pilih username lain.");
                        return;
                    }
                }

                // Insert data ke dalam tabel 'account' terlebih dahulu
                string insertAccountQuery = "INSERT INTO account (username, email, password, role, name) VALUES (@username, @email, @password, @role, @name) RETURNING id";
                int accountId;
                using (NpgsqlCommand cmd = new NpgsqlCommand(insertAccountQuery, conn))
                {
                    cmd.Parameters.AddWithValue("username", username);
                    cmd.Parameters.AddWithValue("email", email);
                    cmd.Parameters.AddWithValue("password", password);
                    cmd.Parameters.AddWithValue("role", role);
                    cmd.Parameters.AddWithValue("name", name);
                    accountId = Convert.ToInt32(cmd.ExecuteScalar());
                }

                // Insert data ke dalam tabel 'craftsmen' jika role = craftsman
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

                else if(role == "user")
{
                    string insertUserQuery = $"INSERT INTO {role}s (accountid, points, name) VALUES (@accountid, @points, @name)";
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

                MessageBox.Show("Akun berhasil dibuat!");
                this.Close(); // Close sign-up form
            }
        }
    }
}
