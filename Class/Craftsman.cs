using System.Collections.Generic;
using System;

public class Craftsman : Account
{
    public int EarnedPoints { get; private set; }

    public Craftsman(int id, string username, string email, string password, int earnedPoints)
        : base(id, username, email, password, "craftsman")
    {
        EarnedPoints = earnedPoints;
    }

    public override void DisplayRoleMessage()
    {
        Console.WriteLine("Welcome, Craftsman! You can view and manage earned points.");
    }

}
