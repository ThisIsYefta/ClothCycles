using System;
using System.Collections.Generic;

namespace ClothCycles
{
    internal class User : Account
    {
        public string UserID { get; set; }
        public string Name { get; set; }
        public int Points { get; set; }
        public List<Item> ScannedItems { get; set; }

        public User()
        {
            ScannedItems = new List<Item>();
            Points = 0;
        }

        public void ScanItem(Item item)
        {
            // Logic to scan item
            Console.WriteLine($"{Name} scanned item: {item.GetDetails()}");
            ScannedItems.Add(item);
        }

        public void UploadItem(Item item)
        {
            ScannedItems.Add(item);
            Console.WriteLine($"{Name} uploaded an item: {item.GetDetails()}");
        }

        public List<Product> ViewProducts(List<Product> availableProducts)
        {
            return availableProducts;
        }

        public void EarnPoints(int points)
        {
            Points += points;
            Console.WriteLine($"{Name} earned {points} points. Total points: {Points}");
        }

        public bool RedeemVoucher(Voucher voucher)
        {
            if (Points >= voucher.Value && voucher.ExpiryDate > DateTime.Now)
            {
                Points -= (int)voucher.Value;
                Console.WriteLine($"{Name} redeemed voucher {voucher.VoucherID} for {voucher.Value} points.");
                return true;
            }
            Console.WriteLine("Voucher redemption failed.");
            return false;
        }
    }
}
