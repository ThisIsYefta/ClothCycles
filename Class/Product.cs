using System;

namespace ClothCycles
{
    internal class Product
    {
        public string ProductID { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public double Price { get; set; }
        public Craftsman PostedBy { get; set; }
        public bool IsAvailable { get; set; }

        public string GetDetails()
        {
            return $"Product: {Name}, Price: {Price}, Posted by: {PostedBy.Username}";
        }

        public void UpdateAvailability(bool status)
        {
            IsAvailable = status;
            Console.WriteLine($"Product {ProductID} availability updated to {IsAvailable}");
        }
    }
}
