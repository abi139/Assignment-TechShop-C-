using Assignment_techshop.Exceptions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Numerics;
using System.Text;
using System.Threading.Tasks;


namespace Assignment_techshop.Model
{
    internal class Customer
    {

        int customerID;
        string firstName;
        string lastName;
        string email;
        string phone;
        string address;

        public int CustomerID
        {
            get { return customerID; }
            set { customerID = value; }
        }

        public string FirstName
        {
            get { return firstName; }
            set { firstName = value; }
        }

        public string LastName
        {
            get { return lastName; }
            set { lastName = value; }
        }

        public string Email
        {
            get { return email; }
            set {
                if (!DataValidationException.IsValidEmail(value))
                {
                    throw new DataValidationException("Invalid email format.");
                }
                
                email = value; }
        }

        public string Phone
        {
            get { return phone; }
            set
            {
                //if (!DataValidationException.IsValidPhoneNumber(value))
                //{
                //    throw new DataValidationException("Invalid phone number format.");
                //}

                 phone = value; }
        }

        public string Address
        {
            get { return address; }
            set { address = value; }
        }
        public Customer()
        {

        }

        public Customer(int customerID, string firstName, string lastName, string email, string phone, string address)
        {
            this.customerID = customerID;
            this.firstName = firstName;
            this.lastName = lastName;
            this.email = email;
            this.phone = phone;
            this.address = address;

        }
        public override string ToString()
        {
            return $"{customerID} {firstName} {lastName} {email}{phone} {address}";
        }
    }
}

       