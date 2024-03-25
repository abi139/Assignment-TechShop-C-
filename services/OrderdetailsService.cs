using Assignment_techshop.Exceptions;
using Assignment_techshop.Model;
using Assignment_techshop.Repository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Assignment_techshop.services
{
    internal class OrderdetailsService
    {

        public OrderDetailsRepository orderDetailsRepository;

        public OrderdetailsService()
        {
            orderDetailsRepository = new OrderDetailsRepository();
        }

        public void GetOrderDetailInfo(int orderDetailID)
        {
            try
            {
               List<OrderDetails> orderdetails= orderDetailsRepository.GetOrderDetailInfo(orderDetailID);
                if(orderDetailID==0)
                {
                    Console.WriteLine("Orderdetail id not found");
                }
                foreach(var orderdetail in  orderdetails)
                {
                    Console.WriteLine($"OrderDetailid:{orderdetail.OrderDetailID}\n OrderID: {orderdetail.Order.OrderID}\n ProductID: {orderdetail.Product.ProductID}\n Quantity: {orderdetail.Quantity} ");
                }
                

            }
            catch (OrderDetailsNotFound ex)
            {
                Console.WriteLine($"Error getting order detail: {ex.Message}");
               
            }
            catch (Exception ex)
            {
                Console.WriteLine($"An error occurred: {ex.Message}");
                
            }
        }

        public void UpdateQuantity(int orderDetailID, int newQuantity)
        {
            try
            {
                orderDetailsRepository.UpdateQuantity(orderDetailID, newQuantity);
                Console.WriteLine("Order detail quantity updated successfully.");
            }
            catch (Exception ex)
            {
                Console.WriteLine($"An error occurred while updating order detail quantity: {ex.Message}");
            }
        }

        public void AddDiscount(int orderDetailID, decimal discountAmount)
        {
            try
            {
                orderDetailsRepository.AddDiscount( orderDetailID, discountAmount);
                Console.WriteLine("Discount applied successfully.");
            }
            catch (Exception ex)
            {
                Console.WriteLine($"An error occurred while applying discount: {ex.Message}");
            }
        }
        public void CalculateSubtotal(int orderDetailID)
        {
            try
            {
                Console.WriteLine( $"CalculateSubtotal: {orderDetailsRepository.CalculateSubtotal(orderDetailID)}");
            }
            catch (Exception ex)
            {
                Console.WriteLine($"An error occurred while calculating subtotal: {ex.Message}");
                
            }
        }
        public void AddOrderDetail(int orderID, int productID, int quantity)
        {
            Products product = new Products { ProductID = productID };
            OrderDetails orderDetail = new OrderDetails();
            orderDetail.Order = new Orders { OrderID = orderID };
            orderDetail.Product = product;
            orderDetail.Quantity = quantity;
            orderDetailsRepository.AddOrderDetail(orderDetail);
            Console.WriteLine("Order detail added successfully.");
        }

        public void OrderDetailsMenu()
        {
            int choice=0 ;

            do
            {
                Console.WriteLine("Order Details Service Menu:");
                Console.WriteLine("1. Get Order Detail Information");
                Console.WriteLine("2. Update Quantity");
                Console.WriteLine("3. Add Discount");
                Console.WriteLine("4. Calculate Subtotal");
                Console.WriteLine("5. Add Order Detail");
                Console.WriteLine("6. Exit");
                Console.Write("Enter your choice: ");
                choice =int.Parse( Console.ReadLine());

                switch (choice)
                {
                    case 1:
                        Console.Write("Enter Order Detail ID: ");
                        int orderDetailID = int.Parse(Console.ReadLine());
                        GetOrderDetailInfo(orderDetailID);
                        break;
                    case 2:
                        Console.Write("Enter Order Detail ID: ");
                        int detailID = int.Parse(Console.ReadLine());
                        Console.Write("Enter New Quantity: ");
                        int newQuantity = int.Parse(Console.ReadLine());
                        UpdateQuantity(detailID, newQuantity);
                        break;
                    case 3:
                        Console.Write("Enter Order Detail ID: ");
                        int detailIDForDiscount = int.Parse(Console.ReadLine());
                        Console.Write("Enter Discount Amount: ");
                        decimal discountAmount = decimal.Parse(Console.ReadLine());
                        AddDiscount(detailIDForDiscount, discountAmount);
                        break;
                    case 4:
                        Console.Write("Enter Order Detail ID: ");
                        int detailIDForSubtotal = int.Parse(Console.ReadLine());
                        CalculateSubtotal(detailIDForSubtotal);
                        break;
                    case 5:
                        Console.Write("Enter Order ID: ");
                        int orderID = int.Parse(Console.ReadLine());
                        Console.Write("Enter Product ID: ");
                        int productID = int.Parse(Console.ReadLine());
                        Console.Write("Enter Quantity: ");
                        int quantity = int.Parse(Console.ReadLine());
                        AddOrderDetail(orderID, productID, quantity);

                        break;
                    case 6:
                        Console.WriteLine("Exiting Order Details Service.");
                        break;
                    default:
                        Console.WriteLine("Invalid choice. Please enter a number between 1 and 6.");
                        break;
                }



            } while (choice != 6);
        }
    }
}





