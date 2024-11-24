using System;
using System.Windows.Forms;
using ClothCycles;
using Npgsql;

namespace ClothCycles
{
    public partial class LoginForm : Form
    {
        private string connString = "Host=localhost;Port=5432;Username=postgres;Password=Yefta21n0404;Database=ClothCycles;Include Error Detail=true";
        private Timer errorTimer;

        public LoginForm()
        {
            InitializeComponent();
            errorTimer = new Timer();
            errorTimer.Interval = 3000; 
            errorTimer.Tick += ErrorTimer_Tick;
        }

        private void ErrorTimer_Tick(object sender, EventArgs e)
        {
            lblErrorMessage.Visible = false; 
            errorTimer.Stop(); 
        }

        private void btnLogin_Click(object sender, EventArgs e)
        {
            string enteredUsername = txtUsername.Text.Trim();
            string enteredPassword = txtPassword.Text.Trim();

            using (NpgsqlConnection conn = new NpgsqlConnection(connString))
            {
                try
                {
                    conn.Open(); // Open connection

                    string query = "SELECT id, username, email, password, role, name FROM Account WHERE username = @username"; 
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
                                string name = reader.GetString(5); 

                                Account account;

                                        if (password == enteredPassword)
                                        {
                                            // Create object based on role
                                            switch (role)
                                            {
                                                case "user":
                                                    account = new User(id, username, email, password, name, 0); 
                                                    OpenUserForm(account); 
                                                    break;
                                                case "craftsman":
                                                    account = new Craftsman(id, username, email, password, name, 0); 
                                                    OpenCraftsmanForm(account); 
                                                    break;
                                                case "admin":
                                                    account = new Admin(id, username, email, password);
                                                    OpenAdminForm(account);  
                                                    break;
                                                default:
                                                    lblErrorMessage.Text = "Invalid role.";
                                                    lblErrorMessage.Visible = true;
                                                    return;
                                            }
                                        }
                                        else
                                {
                                    lblErrorMessage.Text = "Password salah.";
                                    lblErrorMessage.Visible = true;
                                    errorTimer.Start(); 
                                }
                            }
                            else
                            {
                                lblErrorMessage.Text = "Username tidak ditemukan.";
                                lblErrorMessage.Visible = true;
                                errorTimer.Start(); 
                            }
                        }
                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show($"An error occurred: {ex.Message}", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
        }

        private void OpenUserForm(Account account)
        {
            this.Hide(); 

            UsersForm usersForm = new UsersForm(account as User, connString); 
            usersForm.FormClosed += (s, args) => this.Show(); 
            usersForm.Show(); 
        }

        private void OpenCraftsmanForm(Account account)
        {
            this.Hide();

            CraftsmanForm craftsmanForm = new CraftsmanForm(account as Craftsman, connString); 
            craftsmanForm.FormClosed += (s, args) => this.Show();
            craftsmanForm.Show();
        }

        private void OpenAdminForm(Account account)
        {
            this.Hide();

            AdminForm adminForm = new AdminForm(account as Admin, connString); 
            adminForm.FormClosed += (s, args) => this.Show();
            adminForm.Show();
        }

        private void btnSignUp_Click(object sender, EventArgs e)
        {
            this.Hide(); 

            using (SignUpForm signUpForm = new SignUpForm())
            {
                signUpForm.ShowDialog();
            }

            this.Show(); 
        }

        private void label1_Click(object sender, EventArgs e)
        {
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