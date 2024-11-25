using System;

public class Transaction
{
    public int TransactionID { get; set; }
    public Product Product { get; set; } 
    public Craftsman Craftsman { get; set; }
    public int PointsUsed { get; set; } 
    public int Quantity { get; set; } 
    public decimal TotalPrice { get; set; } 
    public string Address { get; set; } 
    public DateTime Date { get; set; } 

    public Transaction(int transactionID, Product product, Craftsman craftsman, int pointsUsed, int quantity, decimal totalPrice, string address)
    {
        TransactionID = transactionID;
        Product = product;
        Craftsman = craftsman;
        PointsUsed = pointsUsed;
        Quantity = quantity;
        TotalPrice = totalPrice;
        Address = address;
        Date = DateTime.Now; 
    }
}
