using System;
using System.Collections.Generic;
using System.Windows.Forms;
using Npgsql;

namespace ClothCycles
{
    public partial class LoginForm : Form
    {
        private string connString = "Host=localhost;Port=5432;Username=postgres;Password=Yefta21n0404;Database=ClothCycles";

        public LoginForm()
        {
            InitializeComponent();
        }

        private void btnLogin_Click(object sender, EventArgs e)
        {
            string enteredUsername = txtUsername.Text.Trim();
            string enteredPassword = txtPassword.Text.Trim();

            using (NpgsqlConnection conn = new NpgsqlConnection(connString))
            {
                conn.Open();
                string query = "SELECT role FROM Account WHERE username = @username AND password = @password";
                using (NpgsqlCommand cmd = new NpgsqlCommand(query, conn))
                {
                    cmd.Parameters.AddWithValue("username", enteredUsername);
                    cmd.Parameters.AddWithValue("password", enteredPassword);

                    var role = cmd.ExecuteScalar() as string;

                    if (role != null)
                    {
                        MessageBox.Show($"Login berhasil sebagai {role}", "Sukses");

                        // Tampilkan pop-up sesuai peran
                        if (role == "user")
                        {
                            // Arahkan ke form pengguna
                            MessageBox.Show("Selamat datang, User!");
                        }
                        else if (role == "craftsman")
                        {
                            // Arahkan ke form craftsman
                            MessageBox.Show("Selamat datang, Craftsman!");
                        }
                        else if (role == "admin")
                        {
                            // Arahkan ke dashboard admin
                            MessageBox.Show("Selamat datang, Admin!");
                        }
                    }
                    else
                    {
                        lblErrorMessage.Text = "Username atau password salah.";
                        lblErrorMessage.Visible = true;
                    }
                }
            }
        }

        private void label1_Click(object sender, EventArgs e)
        {
            // You can leave this empty or add functionality if needed
        }

        private void button1_Click(object sender, EventArgs e)
        {
            // Call your existing login method here
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
