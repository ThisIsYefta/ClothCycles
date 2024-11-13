using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ClothCycles
{
    internal class UserTransaction
    {
        public User Buyer { get; set; }
        public Product ProductPurchased { get; set; }
        public double TotalPrice { get; set; }

        public UserTransaction(User buyer, Product productPurchased)
        {
            Buyer = buyer;
            ProductPurchased = productPurchased;
            TotalPrice = productPurchased.Price;
        }

        // Method to apply discount using points
        public void ApplyDiscount(int points)
        {
            double discount = points * 5000;
            TotalPrice -= discount;
            Console.WriteLine($"Discount applied: {discount}. Total price now: {TotalPrice}");
        }
    }
}
