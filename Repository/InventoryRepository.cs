using Assignment_techshop.Exceptions;
using Assignment_techshop.Model;
using Assignment_techshop.Utility;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Reflection;

namespace Assignment_techshop.Repository
{
    internal class InventoryRepository
    {
        SqlConnection sqlConnection;
        SqlCommand cmd;

        public InventoryRepository()
        {
            sqlConnection = new SqlConnection(DatabaseUtility.GetConnectionString());
            cmd = new SqlCommand();
        }

        public int GetQuantityInStock(int inventoryID)
        {
            int quantityInStock = 0;

            if (sqlConnection.State != ConnectionState.Open)
                sqlConnection.Open();

            cmd.Connection = sqlConnection;
            cmd.CommandText = "SELECT QuantityInStock FROM Inventory WHERE InventoryID = @InventoryID";
            cmd.Parameters.AddWithValue("@InventoryID", inventoryID);

            object result = cmd.ExecuteScalar();
            if (result != null && result != DBNull.Value)
            {
                quantityInStock = Convert.ToInt32(result);
            }

            cmd.Parameters.Clear();
            sqlConnection.Close();

            return quantityInStock;
        }

        public void GetProducts(Products product, int inventoryID)
        {
            if (sqlConnection.State != ConnectionState.Open)
                sqlConnection.Open();

            cmd.Connection = sqlConnection;
            cmd.CommandText = "SELECT p.* FROM Inventory i INNER JOIN Products p ON i.ProductID = p.ProductID WHERE i.InventoryID = @InventoryID";
            cmd.Parameters.AddWithValue("@InventoryID", inventoryID);

            using (SqlDataReader reader = cmd.ExecuteReader())
            {
                if (reader.Read())
                {
                    product.ProductID = (int)reader["ProductID"];
                    product.ProductName = reader["ProductName"].ToString();
                    product.Price = (decimal)reader["Price"];
                    product.Description = reader["Description"].ToString();
                }
            }

            cmd.Parameters.Clear();
            sqlConnection.Close();
        }

        public void AddToInventory(int inventoryID, int productID, int quantityToAdd, DateTime laststockupdate)
        {

            sqlConnection.Open();
            cmd.Connection = sqlConnection;
            cmd.CommandText = "INSERT INTO Inventory (InventoryID,ProductID, QuantityInStock, LastStockUpdate) VALUES (@InventoryID,@ProductID, @QuantityToAdd, @LastStockUpdate);SELECT SCOPE_IDENTITY();";
            cmd.Parameters.AddWithValue("@InventoryID", inventoryID);
            cmd.Parameters.AddWithValue("@ProductID", productID);
            cmd.Parameters.AddWithValue("@QuantityToAdd", quantityToAdd);
            cmd.Parameters.AddWithValue("@LastStockUpdate", DateTime.Now);


            sqlConnection.Close();
        }
        public void RemoveFromInventory(int productID, int quantityToRemove)
        {
            if (sqlConnection.State != ConnectionState.Open)
                sqlConnection.Open();

            cmd.Connection = sqlConnection;
            cmd.CommandText = "UPDATE Inventory SET QuantityInStock = QuantityInStock - @QuantityToRemove WHERE ProductID = @ProductID";
            cmd.Parameters.Clear();
            // Set the parameters
            cmd.Parameters.AddWithValue("@ProductID", productID);
            cmd.Parameters.AddWithValue("@QuantityToRemove", quantityToRemove);

            cmd.ExecuteNonQuery();

            cmd.Parameters.Clear();
            sqlConnection.Close();
        }

        public void UpdateStockQuantity(int productID, int newQuantity)
        {
            if (sqlConnection.State != ConnectionState.Open)
                sqlConnection.Open();

            cmd.Connection = sqlConnection;
            cmd.CommandText = "UPDATE Inventory SET QuantityInStock = @NewQuantity WHERE ProductID = @ProductID";
            cmd.Parameters.Clear();
            cmd.Parameters.AddWithValue("@productID", productID);
            cmd.Parameters.AddWithValue("@NewQuantity", newQuantity);

            cmd.ExecuteNonQuery();

            cmd.Parameters.Clear();
            sqlConnection.Close();
        }

        public bool IsProductAvailable(int productID, int quantityToCheck)
        {
            bool isAvailable = false;

            if (sqlConnection.State != ConnectionState.Open)
            sqlConnection.Open();

            cmd.Connection = sqlConnection;
            cmd.CommandText = "SELECT 1 FROM Inventory WHERE ProductID = @ProductID AND QuantityInStock >= @QuantityToCheck";
            cmd.Parameters.Clear();
            // Set the parameters
            cmd.Parameters.AddWithValue("@ProductiD", productID);
            cmd.Parameters.AddWithValue("@QuantityToCheck", quantityToCheck);

            object result = cmd.ExecuteScalar();
            if (result != null && result != DBNull.Value)
            {
                isAvailable = true;
            }

            cmd.Parameters.Clear();
            sqlConnection.Close();

            return isAvailable;
        }

        public decimal GetInventoryValue()
        {
            decimal totalValue = 0;

            if (sqlConnection.State != ConnectionState.Open)
                sqlConnection.Open();

            cmd.Connection = sqlConnection;
            cmd.CommandText = "SELECT SUM(p.Price * i.QuantityInStock) AS TotalValue " +
                         "FROM Inventory i " +
                         "INNER JOIN Products p ON i.ProductID = p.ProductID";
            cmd.Parameters.Clear();

            object result = cmd.ExecuteScalar();
            if (result != null && result != DBNull.Value)
            {
                totalValue = Convert.ToDecimal(result);
            }

            sqlConnection.Close();

            return totalValue;
        }

        public List<Inventory> ListLowStockProducts(int threshold)
        {
            List<Inventory> lowStockProducts = new List<Inventory>();

            using (SqlConnection sqlConnection = new SqlConnection(DatabaseUtility.GetConnectionString()))
            {
                sqlConnection.Open();
                using (SqlCommand cmd = new SqlCommand())
                {
                    cmd.Connection = sqlConnection;
                    cmd.CommandText = "SELECT p.ProductName, i.QuantityInStock, i.LastStockUpdate " +
                                     "FROM Inventory i " +
                                     "INNER JOIN Products p ON i.ProductID = p.ProductID " +
                                     "WHERE i.QuantityInStock < @Threshold";

                    cmd.Parameters.AddWithValue("@Threshold", threshold);

                    using (SqlDataReader reader = cmd.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            Inventory inventory = new Inventory();
                            inventory.Product = new Products();
                            inventory.Product.ProductName = reader["ProductName"].ToString();
                            inventory.QuantityInStock = Convert.ToInt32(reader["QuantityInStock"]);
                            inventory.LastStockUpdate = Convert.ToDateTime(reader["LastStockUpdate"]);
                            lowStockProducts.Add(inventory);
                        }
                    }
                }
            }

            return lowStockProducts;
        }

        public List<Inventory> ListOutOfStockProducts()
        {
            List<Inventory> outOfStockProducts = new List<Inventory>();

            using (SqlConnection sqlConnection = new SqlConnection(DatabaseUtility.GetConnectionString()))
            {
                sqlConnection.Open();
                using (SqlCommand cmd = new SqlCommand())
                {
                    cmd.Connection = sqlConnection;
                    cmd.CommandText = "SELECT p.ProductName, i.LastStockUpdate " +
                                     "FROM Inventory i " +
                                     "INNER JOIN Products p ON i.ProductID = p.ProductID " +
                                     "WHERE i.QuantityInStock = 0";

                    using (SqlDataReader reader = cmd.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            Inventory inventory = new Inventory();
                            inventory.Product = new Products();
                            inventory.Product.ProductName = reader["ProductName"].ToString();
                            inventory.LastStockUpdate = Convert.ToDateTime(reader["LastStockUpdate"]);
                            outOfStockProducts.Add(inventory);
                        }
                    }
                }
            }

            return outOfStockProducts;
        }




    }
}
