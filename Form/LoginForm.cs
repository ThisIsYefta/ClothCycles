using System;
using System.Windows.Forms;
using Npgsql;

namespace ClothCycles
{
    public partial class LoginForm : Form
    {
        private string connString = "Host=localhost;Port=5432;Username=postgres;Password=Yefta21n0404;Database=ClothCycles";
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
        try
        {
            conn.Open(); // Open connection

            string query = "SELECT id, username, email, password, role, name FROM Account WHERE username = @username"; // Menambahkan 'name'
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
                        string name = reader.GetString(5); // Ambil nama pengguna

                        Account account;

                        if (password == enteredPassword) // Validate password
                        {
                            // Create object based on role
                            switch (role)
                            {
                                case "user":
                                    account = new User(id, username, email, password, name, 0); // Menambahkan name ke konstruktor
                                    OpenUserForm(account); // Open UsersForm
                                    break;
                                case "craftsman":
                                    account = new Craftsman(id, username, email, password, 0); // Adding 0 for earnedPoints
                                    // OpenCraftsmanForm(account); // Placeholder for Craftsman form
                                    break;
                                case "admin":
                                    account = new Admin(id, username, email, password);
                                    // OpenAdminForm(account); // Placeholder for Admin form
                                    break;
                                default:
                                    lblErrorMessage.Text = "Invalid role.";
                                    lblErrorMessage.Visible = true;
                                    return;
                            }

                            // Show welcome message in a pop-up window
                            MessageBox.Show(account.DisplayRoleMessage(), "Login Berhasil");
                        }
                        else
                        {
                            lblErrorMessage.Text = "Password salah.";
                            lblErrorMessage.Visible = true;
                            errorTimer.Start(); // Start timer to hide message
                        }
                    }
                    else
                    {
                        lblErrorMessage.Text = "Username tidak ditemukan.";
                        lblErrorMessage.Visible = true;
                        errorTimer.Start(); // Start timer to hide message
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
            this.Hide(); // Sembunyikan LoginForm tanpa menutupnya

            UsersForm usersForm = new UsersForm(account as User, connString); // Membuat UsersForm dengan connString
            usersForm.FormClosed += (s, args) => this.Show(); // Tampilkan LoginForm kembali saat UsersForm ditutup
            usersForm.Show(); // Tampilkan UsersForm
        }


        private void btnSignUp_Click(object sender, EventArgs e)
        {
            this.Hide(); // Hide LoginForm

            using (SignUpForm signUpForm = new SignUpForm())
            {
                signUpForm.ShowDialog(); // Open SignUpForm as a modal dialog
            }

            this.Show(); // Show LoginForm again after SignUpForm is closed
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
            // Optional: Any initialization code can go here.
        }

        private void pictureBox1_Click(object sender, EventArgs e)
        {
            // Optional: Any functionality for the picture box can go here.
        }
    }
}