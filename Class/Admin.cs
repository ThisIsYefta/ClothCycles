using System;

public class Admin : Account
{
    public int AdminID { get; set; }

    public Admin(int adminID, string username, string email, string password)
        : base(username, email, password)
    {
        AdminID = adminID;
    }

    // Admin-specific logic: approving users
    public void ApproveUser(User user)
    {
        Console.WriteLine($"User {user.Username} has been approved by Admin {Username}");
    }
}
