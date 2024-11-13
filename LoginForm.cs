using System;
using System.Windows.Forms;
using Npgsql;

namespace ClothCycles
{
    public partial class LoginForm : Form
    {
        private string connString = "Host=localhost;Port=5432;Username=postgres;Password=Yefta21n0404;Database=ClothCycles2";
        private Timer errorTimer;

        public LoginForm()
        {
            InitializeComponent();
            errorTimer = new Timer();
            errorTimer.Interval = 3000; // 3000 ms = 3 detik
            errorTimer.Tick += ErrorTimer_Tick;
        }

        private void ErrorTimer_Tick(object sender, EventArgs e)
        {
            lblErrorMessage.Visible = false; // Sembunyikan pesan error
            errorTimer.Stop(); // Hentikan timer
        }

        private void btnLogin_Click(object sender, EventArgs e)
        {
            string enteredUsername = txtUsername.Text.Trim();
            string enteredPassword = txtPassword.Text.Trim();

            using (NpgsqlConnection conn = new NpgsqlConnection(connString))
            {
                conn.Open();
                string query = "SELECT id, username, email, password, role FROM Account WHERE username = @username";
                using (NpgsqlCommand cmd = new NpgsqlCommand(query, conn))
                {
                    cmd.Parameters.AddWithValue("username", enteredUsername);

                    using (NpgsqlDataReader reader = cmd.ExecuteReader())
                    {
                        if (reader.Read())
                        {
                            int id = reader.GetInt32(0);
                            string username = reader.GetString(1);
                            string email = reader.GetString(2);
                            string password = reader.GetString(3);
                            string role = reader.GetString(4);

                            Account account;

                            if (password == enteredPassword) // Validate password
                            {
                                // Buat objek sesuai role
                                switch (role)
                                {
                                    case "user":
                                        account = new User(id, username, email, password, 0); // Menambahkan 0 untuk points
                                        break;
                                    case "craftsman":
                                        account = new Craftsman(id, username, email, password, 0); // Menambahkan 0 untuk earnedPoints
                                        break;
                                    case "admin":
                                        account = new Admin(id, username, email, password);
                                        break;
                                    default:
                                        lblErrorMessage.Text = "Invalid role.";
                                        lblErrorMessage.Visible = true;
                                        return;
                                }

                                // Tampilkan pesan selamat datang di pop-up window
                                MessageBox.Show(account.DisplayRoleMessage(), "Login Berhasil");
                            }
                            else
                            {
                                lblErrorMessage.Text = "Password salah.";
                                lblErrorMessage.Visible = true;
                                errorTimer.Start(); // Mulai timer untuk menyembunyikan pesan
                            }
                        }
                        else
                        {
                            lblErrorMessage.Text = "Username tidak ditemukan.";
                            lblErrorMessage.Visible = true;
                            errorTimer.Start(); // Mulai timer untuk menyembunyikan pesan
                        }
                    }
                }
            }
        }

        private void btnSignUp_Click(object sender, EventArgs e)
        {
            this.Hide(); // Sembunyikan LoginForm

            using (SignUpForm signUpForm = new SignUpForm())
            {
                signUpForm.ShowDialog(); // Buka SignUpForm sebagai modal dialog
            }

            this.Show(); // Tampilkan kembali LoginForm setelah SignUpForm ditutup
        }


        private void label1_Click(object sender, EventArgs e)
        {
            // You can leave this empty or add functionality if needed
        }

        private void button1_Click(object sender, EventArgs e)
        {
            btnLogin_Click(sender, e);
        }

        private void LoginForm_Load(object sender, EventArgs e)
        {

        }

        private void pictureBox1_Click(object sender, EventArgs e)
        {

        }
    }
}