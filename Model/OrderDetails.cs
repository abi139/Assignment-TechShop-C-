using Assignment_techshop.Exceptions;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Assignment_techshop.Model
{
    internal class OrderDetails
    {

        int orderDetailID;
        Orders order;
        Products product;
        int quantity;

        public int OrderDetailID
        {
            get { return orderDetailID; }
            set { orderDetailID = value; }
        }
        public Orders Order
        {
            get { return order; }
            set { order = value; }
        }
        public Products Product
        {
            get { return product; }
            set { product = value; }
        }
        public int Quantity
        {
            get { return quantity; }
            set
            {
               if(!DataValidationException.ValidateQuantity(value))
                {
                  throw new DataValidationException("give proper quantity in stock.");
                }
               quantity = value;
            }   
        }
        public OrderDetails()
        {

        }

        public OrderDetails(int orderDetailID, Orders order, Products product, int quantity)
        {
            this.orderDetailID = orderDetailID;
            this.order = order;
            this.product = product;
            this.quantity = quantity;

        }
        public override string ToString()
        {
            return $"{orderDetailID} {order} {product} {quantity}";
        }


    }
}