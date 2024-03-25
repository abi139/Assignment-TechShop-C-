using Assignment_techshop.Model;
using Assignment_techshop.services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


namespace Assignment_techshop.TechShopManagementApp
{
    internal class TechShopManagement
    {
        CustomerService customerService;
        InventoryService inventoryService;
        OrderdetailsService orderdetailsService;
        OrderService orderService;
        ProductsService productsService;
        public TechShopManagement()
        {
            customerService = new CustomerService();
            inventoryService = new InventoryService();
            orderdetailsService = new OrderdetailsService();
            productsService = new ProductsService();
            orderService = new OrderService();

        }
       

        public void MainMenu()
        {
            int choice = 0;
            do
            {
                Console.Clear();
                Console.WriteLine("Main Menu");
                Console.WriteLine("****************************************");
                Console.WriteLine("1: Customer \n2: Products \n3: Orders \n4: OrderDetails \n5.Inventory");
                Console.WriteLine("Enter your choice");
                choice = Convert.ToInt32(Console.ReadLine());
                switch (choice)
                {
                    case 1:
                        List<Customer> customersList = new List<Customer>();
                        customerService.CustomerMenu(customersList);
                        break;

                    case 2:
                        List<Products> productlist = new List<Products>();
                        productsService.ProductsMenu(productlist);
                        break;

                    case 3:
                        orderService.OrdersMenu();
                        break;

                    case 4:
                        orderdetailsService.OrderDetailsMenu();
                        break;

                    case 5:
                        inventoryService.InventoryMenu();
                        break;
                    case 6:
                        Console.WriteLine("Exiting...........");
                        break;
                    case 7:
                        Console.WriteLine("Enter valid choice from 1 to 6");
                        break;

                }

            } while (choice != 6);
        }
    }
}
