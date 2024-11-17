using System;

public class Transaction
{
    public int TransactionID { get; set; }
    public Product Product { get; set; } // Mengganti Item dengan Product
    public Craftsman Craftsman { get; set; }
    public int PointsUsed { get; set; } // Jumlah poin yang digunakan
    public int Quantity { get; set; } // Jumlah produk yang dibeli
    public decimal TotalPrice { get; set; } // Total harga setelah diskon
    public string Address { get; set; } // Alamat pengiriman
    public DateTime Date { get; set; } // Tanggal transaksi

    // Constructor
    public Transaction(int transactionID, Product product, Craftsman craftsman, int pointsUsed, int quantity, decimal totalPrice, string address)
    {
        TransactionID = transactionID;
        Product = product;
        Craftsman = craftsman;
        PointsUsed = pointsUsed;
        Quantity = quantity;
        TotalPrice = totalPrice;
        Address = address;
        Date = DateTime.Now; // Set tanggal transaksi ke waktu sekarang
    }

    // Overriding ToString untuk debugging
    public override string ToString()
    {
        return $"TransactionID: {TransactionID}, Product: {Product?.Name}, Craftsman: {Craftsman?.Name}, PointsUsed: {PointsUsed}, Quantity: {Quantity}, TotalPrice: {TotalPrice:C}, Address: {Address}, Date: {Date}";
    }
}
