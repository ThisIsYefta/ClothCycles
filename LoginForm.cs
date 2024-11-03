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
                                switch (role)
                                {
                                    case "user":
                                        account = new User(id, username, email, password, 0); // Assume points = 0 initially
                                        break;
                                    case "craftsman":
                                        account = new Craftsman(id, username, email, password, 0); // Assume earnedPoints = 0
                                        break;
                                    case "admin":
                                        account = new Admin(id, username, email, password);
                                        break;
                                    default:
                                        lblErrorMessage.Text = "Invalid role.";
                                        lblErrorMessage.Visible = true;
                                        return;
                                }

                                account.DisplayRoleMessage();
                                MessageBox.Show($"Login berhasil sebagai {role}", "Sukses");
                            }
                            else
                            {
                                lblErrorMessage.Text = "Password salah.";
                                lblErrorMessage.Visible = true;
                            }
                        }
                        else
                        {
                            lblErrorMessage.Text = "Username tidak ditemukan.";
                            lblErrorMessage.Visible = true;
                        }
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
