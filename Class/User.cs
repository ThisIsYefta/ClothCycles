using System.Collections.Generic;
using System;

public class User : Account
{
    public int UserID { get; set; }
    public string Name { get; set; }
    public int Points { get; set; }
    public List<string> ScannedItems { get; set; }

    public User(int userID, string username, string email, string password, string name)
        : base(username, email, password)
    {
        UserID = userID;
        Name = name;
        Points = 0;
        ScannedItems = new List<string>();
    }

    // Method to add points
    public void AddPoints(int points)
    {
        Points += points;
        Console.WriteLine($"{points} points added to {Name}. Total: {Points}");
    }

    // Method to scan items
    public void ScanItem(string item)
    {
        ScannedItems.Add(item);
        Console.WriteLine($"{item} scanned by {Name}");
    }
}
