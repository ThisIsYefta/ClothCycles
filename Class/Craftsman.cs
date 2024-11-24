using System;
using System.Collections.Generic;
using System.Windows.Forms;
using Npgsql;

public class Craftsman : Account
{
    public int id { get; private set; } 
    public int EarnedPoints { get; private set; }
    public List<Product> UploadedProducts { get; private set; } 
    public string Name { get; private set; } 

    public Craftsman(int id, string username, string email, string password, string name, int earnedPoints)
        : base(id, username, email, password, "craftsman")
    {
        this.id = id; 
        EarnedPoints = earnedPoints;
        Name = name; 
        UploadedProducts = new List<Product>(); 
    }

    public void LoadUploadedProducts(NpgsqlConnection conn)
    {
        UploadedProducts.Clear(); 

        string query = "SELECT * FROM product WHERE accountid = @craftsmanId"; 

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
        try
        {
            // Cari craftsman_id berdasarkan accountid
            int craftsmanId;
            string getCraftsmanIdQuery = "SELECT id FROM craftsmen WHERE accountid = @accountid";
            using (var cmd = new NpgsqlCommand(getCraftsmanIdQuery, conn))
            {
                cmd.Parameters.AddWithValue("accountid", Accountid);
                var result = cmd.ExecuteScalar();
                if (result != null)
                {
                    craftsmanId = Convert.ToInt32(result);
                }
                else
                {
                    throw new Exception("Craftsman tidak ditemukan untuk accountid ini.");
                }
            }

            // Masukkan data ke tabel product
            string insertProductQuery = @"
            INSERT INTO product (name, description, price, stock, craftsmen_id, accountid) 
            VALUES (@name, @description, @price, @stock, @craftsmanid, @accountid)";
            using (var cmd = new NpgsqlCommand(insertProductQuery, conn))
            {
                cmd.Parameters.AddWithValue("name", product.Name);
                cmd.Parameters.AddWithValue("description", product.Description);
                cmd.Parameters.AddWithValue("price", product.Price);
                cmd.Parameters.AddWithValue("stock", product.Stock);
                cmd.Parameters.AddWithValue("craftsmanid", craftsmanId); 
                cmd.Parameters.AddWithValue("accountid", Accountid); 
                cmd.ExecuteNonQuery();
            }
            UploadedProducts.Add(product);
        }
        catch (Exception ex)
        {
            MessageBox.Show($"Error saat mengunggah produk: {ex.Message}", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
        }
    }

    public void DeleteProduct(Product product, NpgsqlConnection conn)
    {
        string query = "DELETE FROM product WHERE product_id = @product_id"; 

        using (var cmd = new NpgsqlCommand(query, conn))
        {
            cmd.Parameters.AddWithValue("product_id", product.product_id); 

            cmd.ExecuteNonQuery(); 
        }

        UploadedProducts.Remove(product);
    }

}
