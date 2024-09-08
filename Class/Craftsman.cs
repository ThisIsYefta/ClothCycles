using System;
using System.Collections.Generic;

namespace ClothCycles
{
    internal class Craftsman : Account
    {
        public string CraftsmanID { get; set; }
        public List<Product> PostedProducts { get; set; }

        public Craftsman()
        {
            PostedProducts = new List<Product>();
        }

        public void ExpressInterest(Item item)
        {
            Console.WriteLine($"Craftsman {Username} expressed interest in item {item.ItemID}");
        }

        public void PostProduct(Product product)
        {
            PostedProducts.Add(product);
            Console.WriteLine($"Craftsman {Username} posted a product: {product.GetDetails()}");
        }

        public List<Item> ViewAvailableItems(List<Item> allItems)
        {
            return allItems; // Filter items as needed
        }
    }
}
