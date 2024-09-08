using System;

namespace ClothCycles
{
    internal class Voucher
    {
        public string VoucherID { get; set; }
        public double Value { get; set; }
        public DateTime ExpiryDate { get; set; }

        public Voucher(string id, double value, DateTime expiry)
        {
            VoucherID = id;
            Value = value;
            ExpiryDate = expiry;
        }

        public bool IsValid()
        {
            return ExpiryDate > DateTime.Now;
        }
    }
}
