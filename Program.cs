using Assignment_techshop.Model;
using System.Collections.Generic;
using System.Net;
using System.Numerics;
using Assignment_techshop.Repository;
using System.Transactions;
using System.Diagnostics;
using Assignment_techshop.Exceptions;
using System.Security.Cryptography;
using Assignment_techshop.TechShopManagementApp;
using Assignment_techshop.services;

namespace Assignment_techshop
{
    internal class Program
    {
        static void Main(string[] args)
        {
            TechShopManagement app = new TechShopManagement();
            app.MainMenu();
        }
    }
}

