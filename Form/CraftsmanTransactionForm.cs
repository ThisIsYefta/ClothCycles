using System;
using System.Collections.Generic;
using System.Data;
using System.Windows.Forms;
using Npgsql;

namespace ClothCycles
{
    public partial class CraftsmanTransactionForm : Form
    {
        private NpgsqlConnection conn;
        private List<Item> selectedItems;
        private Craftsman currentCraftsman;
        private Item selectedItem; 

        public CraftsmanTransactionForm(Craftsman craftsman, NpgsqlConnection connection)
        {
            InitializeComponent();
            currentCraftsman = craftsman;
            conn = connection;
            selectedItems = new List<Item>(); 
        }

        private void CraftsmanTransactionForm_Load(object sender, EventArgs e)
        {
            if (conn.State != ConnectionState.Open)
            {
                try
                {
                    conn.Open();
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Connection failed: " + ex.Message);
                    return;
                }
            }

            InitializeDataGrids();
            LoadItems();
        }

        private void InitializeDataGrids()
        {
            // Initialize dataGridViewItem
            dataGridViewItem.Columns.Clear();
            dataGridViewItem.Columns.Add("itemid", "Item ID");
            dataGridViewItem.Columns.Add("materialtype", "Material Type");
            dataGridViewItem.Columns.Add("model", "Model");
            dataGridViewItem.Columns.Add("description", "Description");
            dataGridViewItem.Columns.Add("quantity", "Available Quantity");
            dataGridViewItem.Columns.Add("userid", "User ID");
            dataGridViewItem.Columns.Add("username", "User Name");


            // Initialize dataGridViewSelectedItem
            dataGridViewSelectedItem.Columns.Clear();
            dataGridViewSelectedItem.Columns.Add("itemid", "Item ID");
            dataGridViewSelectedItem.Columns.Add("materialtype", "Material Type");
            dataGridViewSelectedItem.Columns.Add("model", "Model");
            dataGridViewSelectedItem.Columns.Add("description", "Description");
            dataGridViewSelectedItem.Columns.Add("quantity", "Selected Quantity");
        }

        private void LoadItems()
        {
            try
            {
                string query = @"SELECT i.itemid, i.materialtype, i.model, i.description, i.quantity, 
                                COALESCE(u.userid, -1) AS userid, COALESCE(u.name, 'Unknown') AS name
                                 FROM items i
                                 LEFT JOIN users u ON i.userid = u.accountid
                                 WHERE i.quantity > 0";

                using (var cmd = new NpgsqlCommand(query, conn))
                using (var reader = cmd.ExecuteReader())
                {
                    dataGridViewItem.Rows.Clear();

                    while (reader.Read())
                    {
                        int itemId = reader.GetInt32(0);
                        string materialType = reader.GetString(1);
                        string model = reader.GetString(2);
                        string description = reader.GetString(3);
                        int quantity = reader.GetInt32(4);
                        int userId = reader.GetInt32(5); 
                        string userName = reader.GetString(6); 

                        // Validasi untuk memastikan userid dan userName tidak bernilai default
                        if (userId != -1 && userName != "Unknown")
                        {
                            dataGridViewItem.Rows.Add(itemId, materialType, model, description, quantity, userId, userName);
                        }
                        else
                        {
                            dataGridViewItem.Rows.Add(itemId, materialType, model, description, quantity, "No user", "No user");
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error loading items: " + ex.Message);
            }
        }


        private void dataGridViewItem_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex >= 0)
            {
                DataGridViewRow row = dataGridViewItem.Rows[e.RowIndex];
                int itemId = Convert.ToInt32(row.Cells["itemid"].Value);
                string materialType = row.Cells["materialtype"].Value.ToString();
                string model = row.Cells["model"].Value.ToString();
                string description = row.Cells["description"].Value.ToString();
                int availableQuantity = Convert.ToInt32(row.Cells["quantity"].Value);

                // Create an Item object for the selected row
                selectedItem = new Item(itemId, materialType, model, description, availableQuantity, null);

                // Display the selected item details in the UI
                MessageBox.Show($"Selected Item: {selectedItem.Model} with available quantity: {selectedItem.Quantity}");

                txtNominal.Text = selectedItem.Quantity.ToString();
            }
        }


        private void btnSaveItem_Click(object sender, EventArgs e)
        {
            if (selectedItem == null)
            {
                MessageBox.Show("Please select an item first.");
                return;
            }

            if (!int.TryParse(txtNominal.Text, out int quantity) || quantity <= 0)
            {
                MessageBox.Show("Please enter a valid quantity.");
                return;
            }

            if (quantity > selectedItem.Quantity)
            {
                MessageBox.Show("Quantity exceeds available stock.");
                return;
            }

            // Clone the selected item with the entered quantity
            Item itemToSave = new Item(
                selectedItem.ItemID,
                selectedItem.MaterialType,
                selectedItem.Model,
                selectedItem.Description,
                quantity,
                selectedItem.User
            );

            // Add the item to the selected items list
            selectedItems.Add(itemToSave);

            // Add the item to the selected items grid
            dataGridViewSelectedItem.Rows.Add(
                itemToSave.ItemID,
                itemToSave.MaterialType,
                itemToSave.Model,
                itemToSave.Description,
                itemToSave.Quantity
            );

            MessageBox.Show($"Item '{itemToSave.Model}' with quantity {quantity} saved temporarily.");
        }



        private void btnSelect_Click(object sender, EventArgs e)
        {
            if (selectedItems == null || selectedItems.Count == 0)
            {
                MessageBox.Show("No items selected.");
                return;
            }

            if (currentCraftsman == null)
            {
                MessageBox.Show("Craftsman data is missing. Please check the logged-in craftsman.");
                return;
            }

            try
            {
                // Ensure connection is open
                if (conn.State != ConnectionState.Open)
                {
                    conn.Open();
                }

                foreach (var item in selectedItems)
                {
                    if (item == null || item.ItemID <= 0 || item.Quantity <= 0)
                    {
                        MessageBox.Show("Invalid item data. Please check your selection.");
                        return;
                    }

                    // Update item quantity in database
                    string updateItemQuery = "UPDATE items SET quantity = quantity - @quantity WHERE itemid = @itemId";
                    using (var cmd = new NpgsqlCommand(updateItemQuery, conn))
                    {
                        cmd.Parameters.AddWithValue("quantity", item.Quantity);
                        cmd.Parameters.AddWithValue("itemId", item.ItemID);
                        cmd.ExecuteNonQuery();
                    }

                    // Get craftsman ID for the current craftsman
                    string queryCraftsmanId = "SELECT id FROM craftsmen WHERE accountid = @accountId";
                    int craftsmanId;
                    using (var cmd = new NpgsqlCommand(queryCraftsmanId, conn))
                    {
                        cmd.Parameters.AddWithValue("accountId", currentCraftsman.id);
                        var result = cmd.ExecuteScalar();
                        if (result != null)
                        {
                            craftsmanId = Convert.ToInt32(result);
                        }
                        else
                        {
                            throw new Exception("Craftsman not found for the specified account.");
                        }
                    }

                    // Get account ID of the user who owns the item
                    string queryAccountId = "SELECT userid FROM items WHERE itemid = @itemId";
                    int accountId;
                    using (var cmd = new NpgsqlCommand(queryAccountId, conn))
                    {
                        cmd.Parameters.AddWithValue("itemId", item.ItemID);
                        var result = cmd.ExecuteScalar();
                        if (result != null)
                        {
                            accountId = Convert.ToInt32(result);
                        }
                        else
                        {
                            throw new Exception("Account not found for the specified item.");
                        }
                    }

                    // Add points to the user based on account ID (update points in users table)
                    string updateUserPointsQuery = "UPDATE users SET points = points + @points WHERE accountid = @accountId";
                    using (var cmd = new NpgsqlCommand(updateUserPointsQuery, conn))
                    {
                        cmd.Parameters.AddWithValue("points", item.Quantity); // Points based on quantity
                        cmd.Parameters.AddWithValue("accountId", accountId);
                        cmd.ExecuteNonQuery();
                    }

                    // Record the transaction
                    string insertTransactionQuery = @"
                        INSERT INTO craftsman_item_transactions 
                        (craftsman_id, item_id, quantity_used, points_awarded)
                        VALUES (@craftsmanId, @itemId, @quantityUsed, @pointsAwarded)";
                    using (var cmd = new NpgsqlCommand(insertTransactionQuery, conn))
                    {
                        cmd.Parameters.AddWithValue("craftsmanId", craftsmanId);
                        cmd.Parameters.AddWithValue("itemId", item.ItemID);
                        cmd.Parameters.AddWithValue("quantityUsed", item.Quantity);
                        cmd.Parameters.AddWithValue("pointsAwarded", item.Quantity); // Points awarded to user
                        cmd.ExecuteNonQuery();
                    }
                }

                // Transaction successful
                MessageBox.Show("Transaction completed successfully!");
                selectedItems.Clear();
                dataGridViewSelectedItem.Rows.Clear();
                LoadItems(); // Refresh items
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error completing transaction: " + ex.Message);
            }
        }



        private void dataGridViewSelectedItem_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
        }

        private void lblAddress_Click(object sender, EventArgs e)
        {
        }

        private void txtAddress_TextChanged(object sender, EventArgs e)
        {
        }

        private void txtNominal_TextChanged(object sender, EventArgs e)
        {
        }

        private void btnDelete_Click(object sender, EventArgs e)
        {
            if (dataGridViewSelectedItem.SelectedRows.Count == 0)
            {
                MessageBox.Show("Please select an item to delete.");
                return;
            }

            try
            {
                var selectedRow = dataGridViewSelectedItem.SelectedRows[0];
                int selectedItemId = Convert.ToInt32(selectedRow.Cells["itemid"].Value);

                var itemToRemove = selectedItems.Find(item => item.ItemID == selectedItemId);
                if (itemToRemove != null)
                {
                    selectedItems.Remove(itemToRemove);

                    dataGridViewSelectedItem.Rows.Remove(selectedRow);

                    MessageBox.Show($"Item '{itemToRemove.Model}' removed successfully.");
                }
                else
                {
                    MessageBox.Show("Item not found in the selection list.");
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error removing item: " + ex.Message);
            }
        }
    }
}
