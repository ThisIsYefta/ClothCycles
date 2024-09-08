using System;

namespace ClothCycles
{
    internal class Item
    {
        public string ItemID { get; set; }
        public string MaterialType { get; set; }
        public string Model { get; set; }
        public string Condition { get; set; }
        public User Owner { get; set; }

        public string GetDetails()
        {
            return $"ID: {ItemID}, Material: {MaterialType}, Model: {Model}, Condition: {Condition}";
        }
    }
}
