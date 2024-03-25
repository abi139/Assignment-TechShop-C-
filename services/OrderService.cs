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
    internal class OrderService
    {

        public OrdersRepository ordersRepository;
       

        public OrderService()
        {
            ordersRepository = new OrdersRepository();
        }

        public void CalculateTotalAmount()
        {
            try
            {
                ordersRepository.CalculateTotalAmount();
                Console.WriteLine("Total amount calculated and updated for all orders successfully.");
            }
            catch (Exception ex)
            {
                Console.WriteLine("An error occurred while calculating total amount: " + ex.Message);
            }
        }

        public void GetOrderDetails()
        {
            try
            {
                List<Orders> orderlist = ordersRepository.GetOrderDetails();

                Console.WriteLine("Retrieved Order Details:");
                foreach (var orderDetail in orderlist)
                {
                    Console.WriteLine($"OrderID: {orderDetail.OrderID}, CustomerID: {orderDetail.Customer.CustomerID}, OrderDate: {orderDetail.OrderDate}, TotalAmount: {orderDetail.TotalAmount}");
                    
                }
            }
            
            catch (Exception ex)
            {
                Console.WriteLine("An error occurred while retrieving order details: " + ex.Message);
            }
        }

        public void UpdateOrderStatus(int orderId, string status)
        {
            try
            {
                    ordersRepository.UpdateOrderStatus(orderId, status);
                    Console.WriteLine($"Order status updated successfully for Order ID {orderId}.");
               
            }
           
            catch (Exception ex)
            {
                Console.WriteLine("An error occurred while updating order status: " + ex.Message);
            }
        }

        public void AddOrder(Orders order)
        {
            try
            {
                ordersRepository.AddOrder(order);
                Console.WriteLine("Order added successfully.");
            }
            catch (Exception ex)
            {
                Console.WriteLine("An error occurred while adding order: " + ex.Message);
            }
        }

        public void CancelOrder(int orderId)
        {
            try
            {
                if (!OrderNotFoundException.OrderIDNotFound(orderId))
                {
                    ordersRepository.CancelOrder(orderId);
                    Console.WriteLine($"Order with ID {orderId} canceled successfully.");
                }
                else
                {
                    throw new OrderNotFoundException($"Order with ID {orderId} not found.");
                }
            }
            catch (OrderNotFoundException ex)
            {
                Console.WriteLine(ex.Message);
            }

            catch (Exception ex)
            {
                Console.WriteLine("An error occurred while canceling order: " + ex.Message);
            }
        }

        public void OrdersMenu()
        {
            int choice;
            do
            {
                Console.WriteLine("Order Service Menu:");
                Console.WriteLine("1. Calculate Total Amount for Orders");
                Console.WriteLine("2. Get Order Details");
                Console.WriteLine("3. Update Order Status");
                Console.WriteLine("4. Add Order");
                Console.WriteLine("5. Cancel Order");
                Console.WriteLine("6. Exit");
                Console.Write("Enter your choice: ");
                choice = int.Parse(Console.ReadLine());

                    switch (choice)
                    {
                        case 1:
                            CalculateTotalAmount();
                            break;
                        case 2:
                            GetOrderDetails();
                            break;
                        case 3:
                            Console.Write("Enter Order ID: ");
                            int orderId = Convert.ToInt32(Console.ReadLine());
                            Console.Write("Enter Status: ");
                            string status = Console.ReadLine();
                            UpdateOrderStatus(orderId, status);
                            break;
                        case 4:
                            Console.WriteLine("Adding New Order:");
                            Console.Write("Enter Order ID: ");
                            int newOrderId = Convert.ToInt32(Console.ReadLine());
                            Console.Write("Enter Customer ID: ");
                            int newCustomerId = Convert.ToInt32(Console.ReadLine());
                            Console.Write("Enter Order Date (YYYY-MM-DD): ");
                            DateTime newdate = DateTime.Parse(Console.ReadLine());
                            Console.Write("Enter Total amount ");
                            decimal newtotalamount = Decimal.Parse(Console.ReadLine());
                            
                            Customer customer = new Customer
                            {
                                CustomerID = newCustomerId
                            };
                            Orders order = new Orders
                            {
                                OrderID = newOrderId,
                                Customer = customer,                               
                                OrderDate = newdate,
                                TotalAmount = newtotalamount
                            };
                            AddOrder(order);
                    
                                
                            break;
                        case 5:
                            Console.Write("Enter Order ID to Cancel: ");
                            int orderIdToCancel = Convert.ToInt32(Console.ReadLine());
                            CancelOrder(orderIdToCancel);
                            break;
                        case 6:
                            Console.WriteLine("Exiting Order Service.");
                            break;
                        default:
                            Console.WriteLine("Invalid choice. Please enter a number between 1 and 6.");
                            break;
                    }
                
               
            } while (choice != 6);
        }

    }
}
