using System;
using System.Collections;
using System.Transactions;
using BankSystem.Core.Builders;
using BankSystem.Core.Exceptions.TransactionExceptions;
using BankSystem.Core.Factories;
using BankSystem.Core.Models.Customers;
using BankSystem.Core.Models.Wallets.Decorators;
using BankSystem.Core.Providers;
using NUnit.Framework;

namespace BankSystem.Core.Tests
{

    class MockDateTimeProvider : IDateTimeProvider
    {
        public DateTime UtcNow { get; set; }

        public MockDateTimeProvider()
        {
            UtcNow = DateTime.UtcNow;
        }
    }
    [TestFixture]
    public class BankTests
    {
        private Bank _bank;
        private MockDateTimeProvider _dateTimeProvider;
        private Customer _customer;
        
        [SetUp]
        public void SetUp()
        {
            _dateTimeProvider = new MockDateTimeProvider();
            _bank = new Bank
            (
                _dateTimeProvider,
                5000,
                3.65m,
                10000,
                300,
                TimeSpan.FromDays(10),
                amount => amount switch
                {
                    {} when amount < 5000 => 3.65m,
                    {} => 7.3m,
                }
            );
            
            var builder = new CustomerBuilder();
            _customer = builder
                .SetName("First", "Last")
                .SetAddress("Address")
                .SetPassport("Passport")
                .Build();
        }

        [Test]
        public void DebitWalletTests()
        {
            var wallet = _bank.CreateDebitWallet(_customer);

            Assert.That(wallet.Balance, Is.EqualTo(0));
            
            wallet.Deposit(1000);
            Assert.That(wallet.Balance, Is.EqualTo(1000));

            wallet.Withdraw(600);
            Assert.That(wallet.Balance, Is.EqualTo(400));

            Assert.Throws<TransactionExecutionException>(() => wallet.Withdraw(500));

            _dateTimeProvider.UtcNow = _dateTimeProvider.UtcNow.AddDays(5).ToUniversalTime();
            (wallet as PercentageAccrualDecorator).AccruePercentage();
            Assert.That(wallet.Balance, Is.EqualTo(400.2m));
        }

        [Test]
        public void CreditWalletTests()
        {
            var wallet = _bank.CreateCreditWallet(_customer);
            
            Assert.That(wallet.Balance, Is.EqualTo(10000));

            wallet.Withdraw(11000);
            Assert.That(wallet.Balance, Is.EqualTo(-1000));

            (wallet as CommissionHoldDecorator).HoldCommission();
            Assert.That(wallet.Balance, Is.EqualTo(-1300));
        }

        [Test]
        public void DepositWalletTests()
        {
            var wallet = _bank.CreateDepositWallet(_customer, 10000);

            Assert.Throws<InvalidOperationException>(() => wallet.Withdraw(1000));
            
            _dateTimeProvider.UtcNow = _dateTimeProvider.UtcNow.AddDays(10).ToUniversalTime();
            (wallet as PercentageAccrualDecorator).AccruePercentage();
            Assert.That(wallet.Balance, Is.EqualTo(10020));
            
            wallet.Withdraw(20);
            Assert.That(wallet.Balance, Is.EqualTo(10000));
        }

        [Test]
        public void TransferWithRevertTests()
        {
            var sourceWallet = _bank.CreateDebitWallet(_customer);
            var destinationWallet = _bank.CreateDebitWallet(_customer);

            sourceWallet.Deposit(1000);
            Assert.That(sourceWallet.Balance, Is.EqualTo(1000));
            Assert.That(destinationWallet.Balance, Is.EqualTo(0));

            var tx = sourceWallet.TransferTo(1000, destinationWallet);
            Assert.That(sourceWallet.Balance, Is.EqualTo(0));
            Assert.That(destinationWallet.Balance, Is.EqualTo(1000));
            
            tx.Revert();
            Assert.That(sourceWallet.Balance, Is.EqualTo(1000));
            Assert.That(destinationWallet.Balance, Is.EqualTo(0));
        }

        [Test]
        public void RestrictedWalletsTests()
        {
            var builder = new CustomerBuilder();
            _customer = builder.SetName("Lupa", "Pupa").Build();
            var wallet = _bank.CreateCreditWallet(_customer);
            
            Assert.Throws<InvalidOperationException>(() => wallet.Withdraw(6000));
        }
    }
}