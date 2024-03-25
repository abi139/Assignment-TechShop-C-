using Assignment_techshop.Model;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Assignment_techshop.Exceptions;
using System.Data.SqlClient;
using Assignment_techshop.Utility;

namespace Assignment_techshop.Repository
{
    internal class ProductsRepository
    {
        SqlConnection sql = null;
        SqlCommand cmd = null;
        public ProductsRepository()
        {
            sql = new SqlConnection(DatabaseUtility.GetConnectionString());
            cmd = new SqlCommand();
        }
        List<Products> productlist = new List<Products>();


        public List<Products> DisplayAllProducts()
        {
            cmd.CommandText = "SELECT * FROM PRODUCTS ";
            cmd.Connection = sql;
            sql.Open();
            SqlDataReader reader = cmd.ExecuteReader();
            while (reader.Read())
            {
                Products product = new Products();
                product.ProductID = (int)reader["ProductID"];
                product.ProductName = (string)reader["ProductName"];
                product.Price = (decimal)reader["Price"];
                product.Description = (string)reader["Descriptionn"];
                productlist.Add(product);
            }
            sql.Close();
            return productlist;

        }
        public void UpdateProductInfo(Products product)

        {
            using (SqlConnection sql = new SqlConnection(DatabaseUtility.GetConnectionString()))
            {
                sql.Open();

                using (SqlCommand cmd = sql.CreateCommand())
                {
                    cmd.CommandText = "INSERT INTO PRODUCTS (ProductID, ProductName, Descriptionn, Price) VALUES  (@ID,@NAME,@DESCRIPTION,@PRICE)";
                    cmd.Parameters.AddWithValue("@ID", product.ProductID);
                    cmd.Parameters.AddWithValue("@NAME", product.ProductName);
                    cmd.Parameters.AddWithValue("@DESCRIPTION", product.Description);
                    cmd.Parameters.AddWithValue("@PRICE", product.Price);
                    cmd.Connection = sql;


                    cmd.ExecuteNonQuery();

                }
            }



        }
        public Products GetProductDetails(int productId)
        {
            Products product = new Products();

            using (sql)
            {
                cmd.CommandText = "SELECT * FROM PRODUCTS WHERE ProductID = @ID";
                cmd.Parameters.AddWithValue("@ID", productId);
                cmd.Connection = sql;
                sql.Open();
                SqlDataReader reader = cmd.ExecuteReader();
                if (reader.Read())
                {
                    product.ProductID = (int)reader["ProductID"];
                    product.ProductName = (string)reader["ProductName"];
                    product.Price = (decimal)reader["Price"];
                    product.Description = (string)reader["Descriptionn"];
                }
            }

            return product;
        }
        public bool IsProductInStock(int productId)
        {
            int stockCount = 0;

            using (sql)
            {
                cmd.CommandText = "SELECT COUNT(*) FROM INVENTORY WHERE ProductID = @ID";
                cmd.Parameters.AddWithValue("@ID", productId);
                cmd.Connection = sql;
                sql.Open();
                stockCount = (int)cmd.ExecuteScalar();
            }

            return stockCount > 0;
        }
        public void RemoveProduct(int productId)
        {
            sql.Open();
            cmd.Connection = sql;

            cmd.CommandText = "DELETE FROM PRODUCTS WHERE ProductID = @ID";
            cmd.Parameters.AddWithValue("@ID", productId);

            cmd.ExecuteNonQuery();

        }
    }
}

