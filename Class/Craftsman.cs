using System;
using System.Collections.Generic;
using Npgsql;

public class Craftsman : Account
{
    public int id { get; private set; } // Menambahkan properti id
    public int EarnedPoints { get; private set; }
    public List<Product> UploadedProducts { get; private set; } // List untuk menyimpan produk yang diunggah
    public string Name { get; private set; } // Nama pengguna

    public Craftsman(int id, string username, string email, string password, string name, int earnedPoints)
        : base(id, username, email, password, "craftsman")
    {
        this.id = id; // Menginisialisasi id
        EarnedPoints = earnedPoints;
        Name = name; // Inisialisasi Name
        UploadedProducts = new List<Product>(); // Inisialisasi daftar produk yang diunggah
    }

    public override string DisplayRoleMessage()
    {
        return "Welcome, Craftsman! You can view and manage your points.";
    }

    public void LoadUploadedProducts(NpgsqlConnection conn)
    {
        UploadedProducts.Clear(); // Clear existing items

        string query = "SELECT * FROM product WHERE accountid = @craftsmanId"; // Adjust table name to "product"

        using (var cmd = new NpgsqlCommand(query, conn))
        {
            cmd.Parameters.AddWithValue("craftsmanId", Accountid);

            using (var reader = cmd.ExecuteReader())
            {
                while (reader.Read())
                {
                    Product product = new Product(
                        reader.GetInt32(0),
                        reader.GetString(1),
                        reader.GetString(2),
                        reader.GetDecimal(3),
                        reader.GetInt32(4),
                        this
                    );
                    UploadedProducts.Add(product);
                }
            }
        }
    }

    public void UploadProduct(Product product, NpgsqlConnection conn)
    {
        string query = "INSERT INTO product (name, description, price, stock, accountid) VALUES (@name, @description, @price, @stock, @craftsmanid)"; // Adjust table name to "product"
        using (var cmd = new NpgsqlCommand(query, conn))
        {
            cmd.Parameters.AddWithValue("name", product.Name);
            cmd.Parameters.AddWithValue("description", product.Description);
            cmd.Parameters.AddWithValue("price", product.Price);
            cmd.Parameters.AddWithValue("stock", product.Stock);
            cmd.Parameters.AddWithValue("craftsmanid", Accountid); // Correct foreign key column name
            cmd.ExecuteNonQuery();
        }
        UploadedProducts.Add(product);
    }
}
