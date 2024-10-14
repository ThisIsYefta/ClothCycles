using System;

public class Account
{
    public string Username { get; set; }
    public string Email { get; set; }
    public string Password { get; set; }

    public Account(string username, string email, string password)
    {
        this.Username = username;
        this.Email = email;
        this.Password = password;
    }

    // Simulate changing the password
    public void ChangePassword(string newPassword)
    {
        Password = newPassword;
        Console.WriteLine($"Password for {Username} has been changed.");
    }
}
