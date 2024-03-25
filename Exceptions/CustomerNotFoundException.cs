using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Assignment_techshop.Model;

namespace Assignment_techshop.Exceptions
{
    internal class CustomerNotFoundException:Exception
    {
       
        
            
            public CustomerNotFoundException(string message) : base(message) { }

            public CustomerNotFoundException(string message, Exception innerException) : base(message, innerException) { }
        
        }
}
