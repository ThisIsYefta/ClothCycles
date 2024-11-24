using System;
using System.Collections.Generic;
using Npgsql;

public class User : Account
{
    public int Points { get; set; }
    public List<Item> UploadedItems { get; private set; } // List to store uploaded items
    public string Name { get; private set; } // Nama pengguna
    public int userid { get; set; }

    public User(int id, string username, string email, string password, string name, int points)
        : base(id, username, email, password, "user")
    {
        Points = points;
        Name = name; // Inisialisasi Name
        UploadedItems = new List<Item>(); // Initialize the list of uploaded items
    }

    public void LoadUploadedItems(NpgsqlConnection conn)
    {
        UploadedItems.Clear(); // Clear existing items

        string query = "SELECT * FROM Items WHERE UserId = @userId";

        using (var cmd = new NpgsqlCommand(query, conn))
        {
            cmd.Parameters.AddWithValue("userId", Accountid);

            using (var reader = cmd.ExecuteReader())
            {
                while (reader.Read())
                {
                    Item item = new Item(
                        reader.GetInt32(0),
                        reader.GetString(1),
                        reader.GetString(2),
                        reader.GetString(3),
                        reader.GetInt32(4),
                        this
                    );
                    UploadedItems.Add(item);
                }
            }
        }
    }

    public void UploadItem(Item item, NpgsqlConnection conn)
    {
        string query = "INSERT INTO Items (MaterialType, Model, Description, Quantity, UserId) VALUES (@materialType, @model, @description, @quantity, @userId)";

        using (var cmd = new NpgsqlCommand(query, conn))
        {
            cmd.Parameters.AddWithValue("materialType", item.MaterialType);
            cmd.Parameters.AddWithValue("model", item.Model);
            cmd.Parameters.AddWithValue("description", item.Description);
            cmd.Parameters.AddWithValue("quantity", item.Quantity);
            cmd.Parameters.AddWithValue("userId", Accountid);

            cmd.ExecuteNonQuery();
        }

        UploadedItems.Add(item);
    }

    public void DeleteItem(Item item, NpgsqlConnection conn)
    {
        string query = "DELETE FROM items WHERE itemid = @itemid"; // Ganti 'items' dengan nama tabel yang sesuai

        using (var cmd = new NpgsqlCommand(query, conn))
        {
            cmd.Parameters.AddWithValue("itemid", item.ItemID); // Gunakan ID item untuk menghapus

            cmd.ExecuteNonQuery(); // Eksekusi query
        }

        // Hapus item dari daftar UploadedItems
        UploadedItems.Remove(item);
    }

}