using System;

namespace BankSystem.Core.Models.Customers
{
    public class Customer
    {
        public Guid Id { get; }
        public String FirstName { get; }
        public String LastName { get; }
        public String Address { get; internal set; }
        public String Passport { get; internal set; }

        public Boolean IsVerified => Address is not null && 
                                     Passport is not null;

        internal Customer(String firstName, String lastName)
        {
            Id = Guid.NewGuid();
            FirstName = firstName;
            LastName = lastName;
        }
    }
}