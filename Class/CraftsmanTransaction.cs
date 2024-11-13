using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ClothCycles
{
    internal class CraftsmanTransaction
    {
        public Craftsman Craftsman { get; set; }

        public CraftsmanTransaction(Craftsman craftsman)
        {
            Craftsman = craftsman;
        }

        // Method to take an item from a user
        public void TakeItem(Item item)
        {
            item.ChangeStatus("taken");
            Craftsman.EarnedPoints += 3; // Add points to craftsman for taking the item
            Console.WriteLine($"{Craftsman.Username} took item: {item.Model}");
        }
    }
}
