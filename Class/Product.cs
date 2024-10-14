using System;

public class Product
{
    public int ProductID { get; set; }
    public string Name { get; set; }
    public string Description { get; set; }
    public double Price { get; set; }
    public bool Availability { get; set; }
    public Craftsman PostedBy { get; set; }

    public Product(int productID, string name, string description, double price, Craftsman postedBy)
    {
        ProductID = productID;
        Name = name;
        Description = description;
        Price = price;
        Availability = true;
        PostedBy = postedBy;
    }

    // Mark the product as unavailable
    public void MarkAsSold()
    {
        Availability = false;
        Console.WriteLine($"{Name} marked as sold by {PostedBy.Username}");
    }
}
