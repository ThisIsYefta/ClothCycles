using System;
using System.Collections.Generic;
using System.Windows.Forms;

namespace ClothCycles
{
    public partial class LoginForm : Form
    {
        // Dummy data for testing login
        List<Account> accounts = new List<Account>();
        List<User> users = new List<User>();
        List<Craftsman> craftsmen = new List<Craftsman>();
        List<Admin> admins = new List<Admin>();

        public LoginForm()
        {
            InitializeComponent();
            InitDummyData();
        }

        private void InitDummyData()
        {
            // Adjusting according to required constructor arguments
            accounts.Add(new Account("john_doe", "john@example.com", "password123"));
            accounts.Add(new Account("craftman123", "craftman@example.com", "craftmanpwd"));
            accounts.Add(new Account("admin1", "admin@example.com", "adminpass"));

            // Assuming User, Craftsman, and Admin constructors require specific parameters
            users.Add(new User(1, "john_doe", "John Doe", "password123", "John"));
            craftsmen.Add(new Craftsman(1, "craftman123", "craftmanpwd", "Craftsman1"));
            admins.Add(new Admin(1, "admin1", "admin@example.com", "adminpass")); 
        }1

        private void btnLogin_Click(object sender, EventArgs e)
        {
            string enteredUsername = txtUsername.Text.Trim();
            string enteredPassword = txtPassword.Text.Trim();

            // Validate login credentials
            Account matchingAccount = accounts.Find(acc => acc.Username == enteredUsername && acc.Password == enteredPassword);

            if (matchingAccount != null)
            {
                // Check if the user is a regular user, craftsman, or admin and redirect accordingly
                if (users.Exists(u => u.Username == enteredUsername))
                {
                    User loggedInUser = users.Find(u => u.Username == enteredUsername);
                    MessageBox.Show($"Welcome, {loggedInUser.Name}!\nYou have {loggedInUser.Points} points.", "User Login Success");
                    // Optionally, open the main user form here
                }
                else if (craftsmen.Exists(c => c.Username == enteredUsername))
                {
                    Craftsman loggedInCraftsman = craftsmen.Find(c => c.Username == enteredUsername);
                    MessageBox.Show($"Welcome, {loggedInCraftsman.Username}!\nYou have earned {loggedInCraftsman.EarnedPoints} points.", "Craftsman Login Success");
                    // Optionally, open the craftsman form here
                }
                else if (admins.Exists(a => a.Username == enteredUsername))
                {
                    Admin loggedInAdmin = admins.Find(a => a.Username == enteredUsername);
                    MessageBox.Show($"Welcome, Admin {loggedInAdmin.Username}!", "Admin Login Success");
                    // Optionally, open the admin dashboard form here
                }
            }
            else
            {
                // Invalid login credentials
                lblErrorMessage.Text = "Invalid username or password. Please try again.";
                lblErrorMessage.Visible = true;
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
    }
}
