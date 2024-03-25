using Assignment_techshop.Model;
using Assignment_techshop.Repository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Assignment_techshop.services
{
    internal class ProductsService
    {
        public ProductsRepository productsRepository;

        public ProductsService()
        {
            productsRepository = new ProductsRepository();
        }

        public void DisplayAllProducts()
        {
            try
            {
                List<Products> products = productsRepository.DisplayAllProducts();
                Console.WriteLine("All Products:");
                foreach (var product in products)
                {
                    Console.WriteLine($"ID: {product.ProductID}, Name: {product.ProductName}, Price: {product.Price}, Description: {product.Description}");
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine("An error occurred: " + ex.Message);
            }
        }

        public void GetProductDetails(int productId)
        {
            try
            {
                Products product = productsRepository.GetProductDetails(productId);
                if (product != null)
                {
                    Console.WriteLine($"ProductID: {product.ProductID}");
                    Console.WriteLine($"ProductName: {product.ProductName}");
                    Console.WriteLine($"Price: {product.Price}");
                    Console.WriteLine($"Description: {product.Description}");
                }
                else
                {
                    Console.WriteLine("Product not found.");
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine("An error occurred: " + ex.Message);
            }
        }

        public void UpdateProductInfo(Products product)
        {
            try
            {
                productsRepository.UpdateProductInfo(product);
            }
            catch (Exception ex)
            {
                Console.WriteLine("An error occurred: " + ex.Message);
            }
        }

        public void RemoveProduct(int productId)
        {
            try
            {
                productsRepository.RemoveProduct(productId);
                Console.WriteLine($"Product with ID {productId} has been removed successfully.");
            }
            catch (Exception ex)
            {
                Console.WriteLine("An error occurred: " + ex.Message);
            }
        }
        public void ProductsMenu(List<Products> products)
        {

            int choice = 0;

            do
            {
                Console.WriteLine("Product Menu:");
                Console.WriteLine("---------------------------------------------");
                Console.WriteLine("1. Display All Products");
                Console.WriteLine("2. Get Product Details");
                Console.WriteLine("3. Update Product Info");
                Console.WriteLine("4. Remove Product");
                Console.WriteLine("5. Exit");
                Console.WriteLine("Enter your choice:");

                try
                {
                    choice = Convert.ToInt32(Console.ReadLine());

                    switch (choice)
                    {
                        case 1:
                            Console.WriteLine("Display All Products");
                            Console.WriteLine("-----------------------------");
                            DisplayAllProducts();
                            break;

                        case 2:
                            Console.WriteLine("Get Product Details");
                            Console.WriteLine("-----------------------------");
                            Console.WriteLine("Enter Product ID:");
                            int productId = Convert.ToInt32(Console.ReadLine());
                            GetProductDetails(productId);
                            break;

                        case 3:
                            Console.WriteLine("Update Product Info");
                            Console.WriteLine("-----------------------------");
                            Console.WriteLine("Enter Product ID:");
                            int idToUpdate = Convert.ToInt32(Console.ReadLine());
                            Console.WriteLine("Enter Product Name:");
                            string productName = Console.ReadLine();
                            Console.WriteLine("Enter Product Description:");
                            string productDescription = Console.ReadLine();
                            Console.WriteLine("Enter Product Price:");
                            decimal productPrice = Convert.ToDecimal(Console.ReadLine());
                            Products productToUpdate = new Products() { ProductID = idToUpdate, ProductName = productName, Description = productDescription, Price = productPrice };
                            UpdateProductInfo(productToUpdate);
                            break;

                        case 4:
                            Console.WriteLine("Remove Product");
                            Console.WriteLine("-----------------------------");
                            Console.WriteLine("Enter Product ID:");
                            int idToRemove = Convert.ToInt32(Console.ReadLine());
                            RemoveProduct(idToRemove);
                            break;

                        case 5:
                            Console.WriteLine("Exiting...");
                            break;

                        default:
                            Console.WriteLine("Invalid choice. Please enter a number from 1 to 5");
                            break;
                    }
                }
                catch (Exception ex)
                {
                    Console.WriteLine("An error occurred: " + ex.Message);
                }

            } while (choice != 5);
        }
    }
}
   
