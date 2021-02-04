using System;
using BankSystem.Core.Builders;
using NUnit.Framework;

namespace BankSystem.Core.Tests
{
    [TestFixture]
    public class CustomerTests
    {
        [Test]
        public void BuildWithoutName_ShouldThrowException()
        {
            var builder = new CustomerBuilder();

            Assert.Throws<InvalidOperationException>(() => builder.Build());
        }

        [Test]
        public void BuildOnlyWithName_ShouldReturnNotVerifiedCutomer()
        {
            var firstName = "First";
            var lastName = "Last";            
            var builder = new CustomerBuilder();
            
            var customer = builder
                .SetName(firstName, lastName)
                .Build();
            
            Assert.That(customer.FirstName, Is.EqualTo(firstName));
            Assert.That(customer.LastName, Is.EqualTo(lastName));
            Assert.That(customer.IsVerified, Is.False);
        }

        [Test]
        public void BuildWithNameAndAddress_ShouldReturnNotVerifiedUser()
        {
            var firstName = "First";
            var lastName = "Last";
            var address = "Address";
            var builder = new CustomerBuilder();
            
            var customer = builder
                .SetName(firstName, lastName)
                .SetAddress(address)
                .Build();
            
            Assert.That(customer.Address, Is.EqualTo(address));
            Assert.That(customer.IsVerified, Is.False);
        }
        
        [Test]
        public void BuildWithNameAndPassport_ShouldReturnNotVerifiedUser()
        {
            var firstName = "First";
            var lastName = "Last";
            var passport = "Passport";
            var builder = new CustomerBuilder();
            
            var customer = builder
                .SetName(firstName, lastName)
                .SetPassport(passport)
                .Build();
            
            Assert.That(customer.Passport, Is.EqualTo(passport));
            Assert.That(customer.IsVerified, Is.False);
        }

        [Test]
        public void BuildWithAllFields_ShouldReturnVerifiedUser()
        {            
            var firstName = "First";
            var lastName = "Last";
            var address = "Address";
            var passport = "Passport";
            var builder = new CustomerBuilder();
            
            var customer = builder
                .SetName(firstName, lastName)
                .SetAddress(address)
                .SetPassport(passport)
                .Build();
            
            Assert.That(customer.FirstName, Is.EqualTo(firstName));
            Assert.That(customer.LastName, Is.EqualTo(lastName));
            Assert.That(customer.Address, Is.EqualTo(address));
            Assert.That(customer.Passport, Is.EqualTo(passport));
            Assert.That(customer.IsVerified, Is.True);
        }
    }
}