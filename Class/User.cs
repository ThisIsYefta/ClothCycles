using System.Collections.Generic;
using System;

public class User : Account
{
    public int Points { get; private set; }

    public User(int id, string username, string email, string password, int points)
        : base(id, username, email, password, "user")
    {
        Points = points;
    }

    public override string DisplayRoleMessage()
    {
        return "Welcome, User! You can view and manage your points.";
    }
}
