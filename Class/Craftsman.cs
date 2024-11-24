using System;
using System.Collections.Generic;
using System.Windows.Forms;
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
                cmd.Parameters.AddWithValue("craftsmanid", craftsmanId); // ID dari tabel craftsmen
                cmd.Parameters.AddWithValue("accountid", Accountid); // ID dari tabel account
                cmd.ExecuteNonQuery();
            }

            // Tambahkan ke daftar produk yang telah diunggah
            UploadedProducts.Add(product);
        }
        catch (Exception ex)
        {
            MessageBox.Show($"Error saat mengunggah produk: {ex.Message}", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
        }
    }


}
