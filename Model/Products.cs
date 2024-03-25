using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Assignment_techshop.Model
{
    internal class Products
    {

        int productID;
        string productName;
        string description;
        decimal price;

        public int ProductID
        {
            get { return productID; }
            set { productID = value; }
        }
        public string ProductName
        {
            get { return productName; }
            set { productName = value; }
        }
        public string Description
        {
            get { return description; }
            set { description = value; }
        }
        public decimal Price
        {
            get { return price; }
            set
            {
                if (value >= 0)
                {
                    price = value;
                }
                else
                {
                    throw new ArgumentException("Quantity must be a positive integer.");
                }

            }
        }
        public Products()
        {

        }

        public Products(int productID, string productName, string description, decimal price)
        {
            this.productID = productID;
            this.productName = productName;
            this.description = description;
            this.price = price;
        }

        public override string ToString()
        {
            return $"{productID} {productName} {description} {price}";
        }
    }
}