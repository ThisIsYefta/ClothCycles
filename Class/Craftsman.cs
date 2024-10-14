using System.Collections.Generic;
using System;

public class Craftsman : Account
{
    public int CraftsmanID { get; set; }
    public List<string> PostedProducts { get; set; }
    public int EarnedPoints { get; set; }

    public Craftsman(int craftsmanID, string username, string email, string password)
        : base(username, email, password)
    {
        CraftsmanID = craftsmanID;
        PostedProducts = new List<string>();
        EarnedPoints = 0;
    }

    // Method to post a product
    public void PostProduct(string product)
    {
        PostedProducts.Add(product);
        Console.WriteLine($"{product} posted by Craftsman {Username}");
    }

    // Method to add earned points
    public void AddEarnedPoints(int points)
    {
        EarnedPoints += points;
        Console.WriteLine($"{points} points earned by {Username}. Total: {EarnedPoints}");
    }
}
