using Assignment_techshop.Exceptions;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Runtime.Intrinsics.X86;
using System.Text;
using System.Threading.Tasks;

namespace Assignment_techshop.Model
{
    internal class Orders
    {

        int orderID;
        Customer customer;
        DateTime orderDate;
        decimal totalAmount;
        string status;

        public int OrderID
        {
            get { return orderID; }
            set { orderID = value; }
        }
        public Customer Customer
        {
            get { return customer; }
            set { customer = value; }
        }
        public DateTime OrderDate
        {
            get { return orderDate; }
            set {
                if (!DataValidationException.IsValidDateTime(value))
                {
                    throw new DataValidationException("Please provide a valid date-time format for LastStockUpdate.");
                }
                orderDate = value;
            }
        }
        
        public decimal TotalAmount
        {
            get { return totalAmount; }
            set { totalAmount = value; }
        }
        public string Status
        {
            get { return status; }
            set { status = value; }
        }
        public Orders()//default constructor
        {

        }
        public Orders(int orderID, Customer customer, DateTime orderDate, string status)
        {
            this.orderID = orderID;
            this.customer = customer;
            this.orderDate = orderDate;
            this.status = status;
        }
        public override string ToString()
        {
            return $"{OrderID} {Customer} {OrderDate} {Status}";
        }
    }
}
