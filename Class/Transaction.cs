using System;

namespace ClothCycles
{
    internal class Transaction
    {
        public string TransactionID { get; set; }
        public Item Item { get; set; }
        public Craftsman Craftsman { get; set; }
        public int PointsAwarded { get; set; }
        public DateTime TransactionDate { get; set; }

        public void ProcessTransaction(User user, int points)
        {
            user.EarnPoints(points);
            TransactionDate = DateTime.Now;
            Console.WriteLine($"Transaction {TransactionID} completed. {points} points awarded.");
        }
    }
}
