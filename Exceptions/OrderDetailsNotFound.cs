using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Assignment_techshop.Model;

namespace Assignment_techshop.Exceptions
{
    internal class OrderDetailsNotFound : Exception
    {
        public OrderDetailsNotFound(string message) : base(message)
        {
        }
        public static bool OrderDetailsIDNotFound(int value)
        {
            List<OrderDetails> orderdetaillist = new List<OrderDetails>();
            foreach (OrderDetails item in orderdetaillist)
            {
                if (value != item.OrderDetailID)
                {
                    return false;
                }
            }
            return true;
        }
        Products Product = new Products();
        //public static void CheckIncompleteOrder(OrderDetails orderDetail)
        //{
        //    if (orderDetail.Product == null)
        //    {
        //        throw new OrderDetailsNotFound("Order detail lacks a product reference.");
        //    }
        }
    }


