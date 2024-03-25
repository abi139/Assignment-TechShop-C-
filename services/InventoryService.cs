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
    internal class InventoryService
    {
        public InventoryRepository inventoryRepository;

        public InventoryService()
        {
            inventoryRepository = new InventoryRepository();
        }

        public int GetQuantityInStock(int inventoryID)
        {
            try
            {
                return inventoryRepository.GetQuantityInStock(inventoryID);
            }
            catch (Exception ex)
            {
                Console.WriteLine($"An error occurred while getting quantity in stock: {ex.Message}");
                return 0;
            }
        }

        public void GetProducts(Products product, int inventoryID)
        {
            try
            {
                inventoryRepository.GetProducts(product, inventoryID);
            }
            catch (Exception ex)
            {
                Console.WriteLine($"An error occurred while getting products: {ex.Message}");
            }
        }

        public void AddToInventory(int inventoryID, int productID, int quantityToAdd, DateTime laststockupdate)
        {
            try
            {
                inventoryRepository.AddToInventory( inventoryID, productID,      quantityToAdd,      laststockupdate);
                Console.WriteLine(  "Added successfully");
            }
            catch (InventoryNotFound ex)
            {
                Console.WriteLine($"Inventory not found: {ex.Message}");
            }
            catch (Exception ex)
            {
                Console.WriteLine($"An error occurred while adding to inventory: {ex.Message}");
            }
        }

        public void RemoveFromInventory(int productID, int quantityToRemove)
        {
            try
            {
                inventoryRepository.RemoveFromInventory(productID, quantityToRemove);
                Console.WriteLine("Details removed successfully");
            }
            catch (Exception ex)
            {
                Console.WriteLine($"An error occurred while removing from inventory: {ex.Message}");
            }
        }

        public void UpdateStockQuantity(int productID, int newQuantity)
        {
            try
            {
                inventoryRepository.UpdateStockQuantity(productID, newQuantity);
                Console.WriteLine(  "stock quantity updated successfully");
            }
            catch (Exception ex)
            {
                Console.WriteLine($"An error occurred while updating stock quantity: {ex.Message}");
            }
        }

        public bool IsProductAvailable(int productID, int quantityToCheck)
        {
            try
            {
                return inventoryRepository.IsProductAvailable(productID, quantityToCheck);
            }
            catch (Exception ex)
            {
                Console.WriteLine($"An error occurred while checking product availability: {ex.Message}");
                return false;
            }
        }

        public decimal GetInventoryValue()
        {
            try
            {
                return inventoryRepository.GetInventoryValue();
            }
            catch (Exception ex)
            {
                Console.WriteLine($"An error occurred while getting inventory value: {ex.Message}");
                return 0;
            }
        }

        public List<Inventory> ListLowStockProducts(int threshold)
        {
            try
            {
                return inventoryRepository.ListLowStockProducts(threshold);
            }
            catch (Exception ex)
            {
                Console.WriteLine($"An error occurred while listing low stock products: {ex.Message}");
                return new List<Inventory>();
            }
        }

        public List<Inventory> ListOutOfStockProducts()
        {
            try
            {
                return inventoryRepository.ListOutOfStockProducts();
            }
            catch (Exception ex)
            {
                Console.WriteLine($"An error occurred while listing out-of-stock products: {ex.Message}");
                return new List<Inventory>();
            }
        }
        public void InventoryMenu()
        {
            int choice = 0;
            do
            {
                Console.WriteLine("Inventory Service Menu:");
                Console.WriteLine("1. Add to Inventory");
                Console.WriteLine("2. Remove from Inventory");
                Console.WriteLine("3. Update Stock Quantity");
                Console.WriteLine("4. Check Product Availability");
                Console.WriteLine("5. Get Inventory Value");
                Console.WriteLine("6. List Low Stock Products");
                Console.WriteLine("7. List Out of Stock Products");
                Console.WriteLine("8. Exit");
                Console.Write("Enter your choice: ");
                choice = Convert.ToInt32(Console.ReadLine());


                switch (choice)
                {
                    case 1:
                        Console.WriteLine( "Enter inventory id");
                        int inventoryID = int.Parse(Console.ReadLine());
                        Console.Write("Enter Product ID: ");
                        int productIdToAdd = int.Parse(Console.ReadLine());
                        Console.Write("Enter Quantity to Add: ");
                        int quantityToAdd = int.Parse(Console.ReadLine());
                        Console.WriteLine("Enter laststockupdate");
                        DateTime laststockupdate= DateTime.Now;
                        AddToInventory(inventoryID, productIdToAdd, quantityToAdd, laststockupdate);
                        break;
                    case 2:
                        Console.Write("Enter Product ID: ");
                        int productIdToRemove = int.Parse(Console.ReadLine());
                        Console.Write("Enter Quantity to Remove: ");
                        int quantityToRemove = int.Parse(Console.ReadLine());
                        RemoveFromInventory(productIdToRemove, quantityToRemove);
                        break;
                    case 3:
                        Console.Write("Enter Product ID: ");
                        int productIdToUpdate = int.Parse(Console.ReadLine());
                        Console.Write("Enter New Quantity: ");
                        int newQuantity = int.Parse(Console.ReadLine());
                        UpdateStockQuantity(productIdToUpdate, newQuantity);
                        break;
                    case 4:
                        
                        Console.Write("Enter Product ID: ");
                        int productIdToCheck = int.Parse(Console.ReadLine());
                        Console.Write("Enter Quantity to Check: ");
                        int quantityToCheck = int.Parse(Console.ReadLine());
                        bool isAvailable = IsProductAvailable(productIdToCheck, quantityToCheck);
                        Console.WriteLine($"Product is {(isAvailable ? "available" : "not available")}");
                        break;
                    case 5:
                       
                        decimal inventoryValue = GetInventoryValue();
                        Console.WriteLine($"Inventory Value: {inventoryValue}");
                        break;

                    case 6:

                        Console.Write("Enter Threshold: ");
                        int threshold = int.Parse(Console.ReadLine());
                        var lowStockProducts = ListLowStockProducts(threshold);
                        foreach (var product in lowStockProducts)
                        {
                            Console.WriteLine($"Product Name: {product.Product.ProductName}, Quantity In Stock: {product.QuantityInStock}, Last Stock Update: {product.LastStockUpdate}");
                        }
                        break;
                    case 7:
                        
                        var outOfStockProducts = ListOutOfStockProducts();
                        foreach (var product in outOfStockProducts)
                        {
                            Console.WriteLine($"Product Name: {product.Product.ProductName}, Last Stock Update: {product.LastStockUpdate}");
                        }
                        break;
                    case 8:
                        Console.WriteLine("Exiting Inventory Service.");
                        break;
                    default:
                        Console.WriteLine("Enter number from 1 to 8");
                        break;


                }
            } while (choice != 8);
        }
    }
}
  
