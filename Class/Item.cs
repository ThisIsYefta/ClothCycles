using System;

public class Item
{
    public int ItemID { get; set; }
    public string MaterialType { get; set; }
    public string Model { get; set; }
    public string Description { get; set; }
    public int Quantity { get; set; }
    public User User { get; set; }

    public Item(int itemId, string materialType, string model, string description, int quantity, User user)
    {
        ItemID = itemId;
        MaterialType = materialType;
        Model = model;
        Description = description;
        Quantity = quantity;
        User = user;
    }
}