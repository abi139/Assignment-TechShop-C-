using Assignment_techshop.Exceptions;
using Assignment_techshop.Model;
using Assignment_techshop.Utility;
using Microsoft.VisualBasic;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Assignment_techshop.Repository
{
    internal class OrderDetailsRepository
    {
        SqlConnection sql = null;
        SqlCommand cmd = null;

        public OrderDetailsRepository()
        {
            sql = new SqlConnection(DatabaseUtility.GetConnectionString());
            cmd = new SqlCommand();
        }

        public void AddOrderDetail(OrderDetails orderDetail)
        {
            sql.Open();
            cmd.Connection = sql;
            cmd.CommandText = "INSERT INTO OrderDetails (OrderID, ProductID, Quantity) VALUES (@OrderID, @ProductID, @Quantity)";
            cmd.Parameters.AddWithValue("@OrderID", orderDetail.Order.OrderID);
            cmd.Parameters.AddWithValue("@ProductID", orderDetail.Product.ProductID);
            cmd.Parameters.AddWithValue("@Quantity", orderDetail.Quantity);
            int rowsAffected = cmd.ExecuteNonQuery();

            if (rowsAffected > 0)
            {
                Console.WriteLine("Order detail added successfully.");
            }
            else
            {
                Console.WriteLine("No rows were affected. Failed to add order detail.");
            }
            sql.Close();
        }

        public List<OrderDetails> GetOrderDetailInfo(int orderDetailID)
        {
            List<OrderDetails> orderdetaillist = new List<OrderDetails>();
            

            sql.Open();
            cmd.Connection = sql;
            cmd.CommandText = "SELECT * FROM OrderDetails WHERE OrderDetailID = @OrderDetailID";
            cmd.Parameters.Clear();
            cmd.Parameters.AddWithValue("@OrderDetailID", orderDetailID);
            SqlDataReader reader = cmd.ExecuteReader();

            while (reader.Read())
            {
                OrderDetails orderDetail = new OrderDetails
                {
                    OrderDetailID = (int)reader["OrderDetailID"],
                    Order = new Orders { OrderID = (int)reader["OrderID"] },
                    Product = new Products { ProductID = (int)reader["ProductID"] },
                    Quantity = (int)reader["Quantity"]
                };
                orderdetaillist.Add(orderDetail);
            }

            reader.Close();
            sql.Close();
            return orderdetaillist;

            
        }


        public void UpdateQuantity(int orderDetailID, int newQuantity)
        {
            sql.Open();
            cmd.Connection = sql;
            cmd.CommandText = "UPDATE OrderDetails SET Quantity = @Quantity WHERE OrderDetailID = @OrderDetailID";
            cmd.Parameters.Clear(); 
            cmd.Parameters.AddWithValue("@Quantity", newQuantity);
            cmd.Parameters.AddWithValue("@OrderDetailID", orderDetailID);
            cmd.ExecuteNonQuery();
            sql.Close();
        }

        public decimal CalculateSubtotal(int orderDetailID)
        {
            decimal subtotal = 0;
            sql.Open();
            cmd.Connection = sql;
            cmd.CommandText = "SELECT (od.Quantity * p.Price) FROM OrderDetails od INNER JOIN Products p ON od.ProductID = p.ProductID WHERE od.OrderDetailID = @OrderDetailID";
            cmd.Parameters.Clear();
            cmd.Parameters.AddWithValue("@OrderDetailID", orderDetailID);
            object result = cmd.ExecuteScalar();
            if (result != null && result != DBNull.Value)
            {
                subtotal = Convert.ToDecimal(result);
            }
            sql.Close();
            return subtotal;
        }

        public void AddDiscount(int orderDetailID, decimal discountAmount)
        {
            sql.Open();
            cmd.Connection = sql;
            cmd.CommandText = @"UPDATE Products 
                                SET Price = (
                                    SELECT (od.Quantity * p.Price) - @DiscountAmount
                                    FROM OrderDetails od
                                    INNER JOIN Products p ON od.ProductID = p.ProductID
                                    WHERE od.OrderDetailID = @OrderDetailID
                                )";
            cmd.Parameters.Clear(); 
            cmd.Parameters.AddWithValue("@OrderDetailID", orderDetailID);
            cmd.Parameters.AddWithValue("@DiscountAmount", discountAmount);
            cmd.ExecuteNonQuery();
            sql.Close();
        }
    }
}