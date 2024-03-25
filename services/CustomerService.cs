using Assignment_techshop.Exceptions;
using Assignment_techshop.Repository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Assignment_techshop.Model;

namespace Assignment_techshop.services
{
    internal class CustomerService
    {
        public CustomerRepository customerRepository;

        public CustomerService()
        {
            customerRepository = new CustomerRepository();
        }
        Customer customer = new Customer();

        public void CalculateTotalOrders(int customerId)
        {
            try
            {
               
                int totalOrders = customerRepository.CalculateTotalOrders(customerId);
                Console.WriteLine($"The Total Orders placed by customer ID {customerId} is {totalOrders}");
            }
            catch (CustomerNotFoundException ex)
            {
                Console.WriteLine(ex.Message);
            }
            catch (Exception ex)
            {
                Console.WriteLine("An error occurred: " + ex.Message);
            }
        }


        public void GetCustomerDetails(int customerId)
        {
            try
            {
                Customer customer = customerRepository.GetCustomerDetails(customerId);
                if (customer != null)
                {
                    Console.WriteLine($"CustomerID: {customer.CustomerID}");
                    Console.WriteLine($"FirstName: {customer.FirstName}");
                    Console.WriteLine($"Address: {customer.Address}");
                }
                else
                {
                    Console.WriteLine("Customer not found.");
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine("An error occurred: " + ex.Message);
            }
        }

        public void UpdateCustomerInfo(Customer customer)
        {
            try
            {
                

                customerRepository.UpdateCustomer(customer);
            }
            
            catch (Exception ex)
            {
                Console.WriteLine("An error occurred: " + ex.Message);
            }
        }

        public void CustomerMenu(List<Customer> customersList)
        {
            int customerChoice = 0;

            do
            {
                Console.WriteLine("Customer Menu:");
                Console.WriteLine("---------------------------------------------");
                Console.WriteLine("1. Calculate Total Orders");
                Console.WriteLine("2. Get Customer Details");
                Console.WriteLine("3. Update Customer Info");
                Console.WriteLine("4. Exit");
                Console.WriteLine("Enter your choice");

                try
                {
                    customerChoice = Convert.ToInt32(Console.ReadLine());

                    switch (customerChoice)
                    {
                        case 1:
                            Console.WriteLine("Calculate Total Orders");
                            Console.WriteLine("-----------------------------");
                            Console.WriteLine("Enter customer ID");
                            int customerId = Convert.ToInt32(Console.ReadLine());
                            CalculateTotalOrders(customerId);
                            break;

                        case 2:
                            Console.WriteLine("Get Customer Details");
                            Console.WriteLine("-----------------------------");
                            Console.WriteLine("Enter customer ID");
                            int customerIdForDetails = Convert.ToInt32(Console.ReadLine());
                            GetCustomerDetails(customerIdForDetails);
                            break;

                        case 3:
                            Console.WriteLine("Update Customer Info");
                            Console.WriteLine("------------------------------------");
                            Console.WriteLine("Enter Customer ID");
                            int id = Convert.ToInt32(Console.ReadLine());
                            //Console.WriteLine("Enter updating email");
                            //string email = Console.ReadLine();
                            //Console.WriteLine("Enter updating phone number");
                            //string phone = Console.ReadLine();
                            Console.WriteLine("Enter updating address");
                            string address = Console.ReadLine();
                            Customer customerToUpdate = new Customer() { CustomerID = id,  Address = address };
                            UpdateCustomerInfo(customerToUpdate);
                            break;

                        case 4:
                            Console.WriteLine("Exiting...........");
                            break;

                        default:
                            Console.WriteLine("Invalid choice. Please enter a number from 1 to 4");
                            break;
                    }
                }
                catch (Exception ex)
                {
                    Console.WriteLine("An error occurred: " + ex.Message);
                }
            } while (customerChoice != 4);
        }
    }
}

