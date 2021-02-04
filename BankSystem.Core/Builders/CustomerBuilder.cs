using System;
using BankSystem.Core.Models.Customers;

namespace BankSystem.Core.Builders
{
    public class CustomerBuilder : ICustomerBuildStep
    {
        private Customer _customer;

        public CustomerBuildAdditionalStep SetName(String firstName, String lastName)
        {
            _customer = new Customer(firstName, lastName);
            return new CustomerBuildAdditionalStep(_customer);
        }
        
        public Customer Build()
        {
            if (_customer is null)
                throw  new InvalidOperationException("Set name is required!");
            var customer = _customer;
            Refresh();
            return customer;
        }

        private void Refresh()
        {
            _customer = null;
        }
    }

    public interface ICustomerBuildStep
    {
        public Customer Build();
    }

    public class CustomerBuildAdditionalStep : ICustomerBuildStep
    {
        private Customer _customer;

        internal CustomerBuildAdditionalStep(Customer customer)
        {
            _customer = customer;
        }

        public CustomerBuildAdditionalStep SetPassport(String passport)
        {
            _customer.Passport = passport;
            return this;
        }
        
        public CustomerBuildAdditionalStep SetAddress(String address)
        {
            _customer.Address = address;
            return this;
        }


        public Customer Build()
        {
            var customer = _customer;
            Refresh();
            return customer;
        }

        private void Refresh()
        {
            _customer = new Customer(_customer.FirstName, _customer.LastName);
        }
    }
    
}