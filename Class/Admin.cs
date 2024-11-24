using System;

public class Admin : Account
{
    public Admin(int id, string username, string email, string password)
        : base(id, username, email, password, "admin")
    {
    }
}
