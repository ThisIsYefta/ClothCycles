using System;

public class Transaction
{
    public int TransactionID { get; set; }
    public Item Item { get; set; }
    public Craftsman Craftsman { get; set; }
    public int PointsAwarded { get; set; }
    public DateTime Date { get; set; }
    public Voucher VoucherUsed { get; set; }

    public Transaction(int transactionID, Item item, Craftsman craftsman, int pointsAwarded, Voucher voucherUsed)
    {
        TransactionID = transactionID;
        Item = item;
        Craftsman = craftsman;
        PointsAwarded = pointsAwarded;
        VoucherUsed = voucherUsed;
        Date = DateTime.Now;
    }

}
