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
                string checkQuery = "SELECT COUNT(*) FROM Account WHERE username = @username";
                using (NpgsqlCommand checkCmd = new NpgsqlCommand(checkQuery, conn))
                {
                    checkCmd.Parameters.AddWithValue("username", username);
                    int userCount = Convert.ToInt32(checkCmd.ExecuteScalar());

                    if (userCount > 0)
                    {
                        MessageBox.Show("Username sudah terdaftar.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                        return;
                    }
                }

                // Simpan data baru ke tabel Account
                string insertAccountQuery = "INSERT INTO Account (username, email, password, role, name) VALUES (@username, @email, @password, @role, @name) RETURNING id";
                int accountId;

                using (NpgsqlCommand insertAccountCmd = new NpgsqlCommand(insertAccountQuery, conn))
                {
                    insertAccountCmd.Parameters.AddWithValue("username", username);
                    insertAccountCmd.Parameters.AddWithValue("email", email);
                    insertAccountCmd.Parameters.AddWithValue("password", password); // Pertimbangkan hashing password sebelum disimpan
                    insertAccountCmd.Parameters.AddWithValue("role", role); // Menggunakan role dari ComboBox
                    insertAccountCmd.Parameters.AddWithValue("name", name); // Menyimpan nama ke kolom 'name'

                    accountId = Convert.ToInt32(insertAccountCmd.ExecuteScalar());
                }

                // Simpan data tambahan berdasarkan role
                if (role == "user" || role == "craftsman")
                {
                    string insertUserQuery = $"INSERT INTO {role}s (accountid, points) VALUES (@accountid, @points)";
                    using (NpgsqlCommand insertUserCmd = new NpgsqlCommand(insertUserQuery, conn))
                    {
                        insertUserCmd.Parameters.AddWithValue("accountid", accountId);
                        insertUserCmd.Parameters.AddWithValue("points", 0); // Default points adalah 0

                        insertUserCmd.ExecuteNonQuery();
                    }
                }
                else if (role == "admin")
                {
                    // Simpan data untuk admin
                    string insertAdminQuery = "INSERT INTO Admins (accountid) VALUES (@accountid)";
                    using (NpgsqlCommand insertAdminCmd = new NpgsqlCommand(insertAdminQuery, conn))
                    {
                        insertAdminCmd.Parameters.AddWithValue("accountid", accountId);

                        insertAdminCmd.ExecuteNonQuery();
                    }
                }
            }

            MessageBox.Show("Registrasi berhasil! Anda dapat login sekarang.", "Sign Up Berhasil");
            this.Close(); // Menutup form setelah registrasi berhasil
        }
    }
}