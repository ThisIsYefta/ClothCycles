using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace ClothCycles
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        private void btnTest_Click(object sender, EventArgs e)
        {
            // Sample dummy data for testing
            User user1 = new User() { UserID = "U001", Name = "John Doe", Username = "johnd", Password = "pass123", Points = 100 };
            Craftsman craftsman1 = new Craftsman() { CraftsmanID = "C001", Username = "craft123", Password = "craftpass" };
            Admin admin1 = new Admin() { AdminID = "A001", Username = "admin123", Password = "adminpass" };

            Item item1 = new Item() { ItemID = "I001", MaterialType = "Cotton", Model = "T-Shirt", Condition = "Good", Owner = user1 };
            Product product1 = new Product() { ProductID = "P001", Name = "Recycled Bag", Description = "Made from old T-Shirts", Price = 20.0, PostedBy = craftsman1, IsAvailable = true };

            Voucher voucher1 = new Voucher("V001", 50, DateTime.Now.AddMonths(1));

            // Add items, process transactions, and test logic
            user1.ScanItem(item1);
            craftsman1.PostProduct(product1);

            Transaction transaction1 = new Transaction() { TransactionID = "T001", Item = item1, Craftsman = craftsman1 };
            transaction1.ProcessTransaction(user1, 50); // Award points to user

            // Create a string to show all dummy data
            StringBuilder sb = new StringBuilder();
            sb.AppendLine("User Info:");
            sb.AppendLine($"UserID: {user1.UserID}, Name: {user1.Name}, Username: {user1.Username}, Points: {user1.Points}");

            sb.AppendLine("\nCraftsman Info:");
            sb.AppendLine($"CraftsmanID: {craftsman1.CraftsmanID}, Username: {craftsman1.Username}");

            sb.AppendLine("\nAdmin Info:");
            sb.AppendLine($"AdminID: {admin1.AdminID}, Username: {admin1.Username}");

            sb.AppendLine("\nItem Info:");
            sb.AppendLine($"ItemID: {item1.ItemID}, Material: {item1.MaterialType}, Model: {item1.Model}, Condition: {item1.Condition}, Owner: {item1.Owner.Name}");

            sb.AppendLine("\nProduct Info:");
            sb.AppendLine($"ProductID: {product1.ProductID}, Name: {product1.Name}, Description: {product1.Description}, Price: {product1.Price}, Posted by: {product1.PostedBy.Username}");

            sb.AppendLine("\nVoucher Info:");
            sb.AppendLine($"VoucherID: {voucher1.VoucherID}, Value: {voucher1.Value}, Expiry Date: {voucher1.ExpiryDate}");

            sb.AppendLine("\nTransaction Info:");
            sb.AppendLine($"TransactionID: {transaction1.TransactionID}, ItemID: {transaction1.Item.ItemID}, CraftsmanID: {transaction1.Craftsman.CraftsmanID}, Points Awarded: {transaction1.PointsAwarded}");

            // Show the entire info in a message box
            MessageBox.Show(sb.ToString(), "Dummy Data Overview");
        }


    }
}
