using System;

public class Account
{
    protected int Id { get; set; }
    public string Username { get; private set; }
    public string Email { get; private set; }
    private string Password { get; set; }
    public string Role { get; private set; }

    public Account(int id, string username, string email, string password, string role)
    {
        Id = id;
        Username = username;
        Email = email;
        Password = password;
        Role = role;
    }

    public virtual void DisplayRoleMessage()
    {
        Console.WriteLine($"Welcome, {Role}!");
    }

    // Method to check if the entered password matches
    public bool ValidatePassword(string inputPassword)
    {
        return Password == inputPassword;
    }
}
