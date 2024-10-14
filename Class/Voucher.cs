using System;

public class Voucher
{
    public int VoucherID { get; set; }
    public double Value { get; set; }
    public DateTime ExpiryDate { get; set; }
    public bool IsActive { get; set; }
    public User OwnedBy { get; set; }

    public Voucher(int voucherID, double value, DateTime expiryDate, User ownedBy)
    {
        VoucherID = voucherID;
        Value = value;
        ExpiryDate = expiryDate;
        IsActive = true;
        OwnedBy = ownedBy;
    }

    // Redeem the voucher
    public void Redeem()
    {
        if (IsActive && DateTime.Now < ExpiryDate)
        {
            IsActive = false;
            Console.WriteLine($"Voucher {VoucherID} redeemed by {OwnedBy.Username}");
        }
        else
        {
            Console.WriteLine("Voucher is expired or inactive.");
        }
    }
}
