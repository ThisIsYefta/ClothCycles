using System;

public class Product
{
    public int product_id { get; set; }
    public string Name { get; set; }
    public string Description { get; set; }
    public decimal Price { get; set; }
    public int Stock { get; set; }
    public Craftsman Craftsman { get; set; }

    public Product(int productID, string name, string description, decimal price, int stock, Craftsman craftsman)
    {
        product_id = productID;
        Name = name;
        Description = description;
        Price = price;
        Stock = stock;
        Craftsman = craftsman;
    }

    public void MarkAsSold()
    {
        Stock = 0;
        Console.WriteLine($"{Name} marked as sold.");
    }
}
