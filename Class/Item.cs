using System;

public class Item
{
    public int ItemID { get; set; }
    public string MaterialType { get; set; }
    public string Model { get; set; }
    public string Condition { get; set; }
    public string Status { get; set; }
    public User Owner { get; set; }

    public Item(int itemID, string materialType, string model, string condition, User owner)
    {
        ItemID = itemID;
        MaterialType = materialType;
        Model = model;
        Condition = condition;
        Status = "available";
        Owner = owner;
    }

    // Method to change item status
    public void ChangeStatus(string newStatus)
    {
        Status = newStatus;
        Console.WriteLine($"Item {ItemID} status changed to {newStatus}");
    }
}
