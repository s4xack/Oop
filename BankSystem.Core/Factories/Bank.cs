using System;
using BankSystem.Core.Builders;
using BankSystem.Core.Models.Customers;
using BankSystem.Core.Models.Wallets;
using BankSystem.Core.Models.Wallets.Proxies.AmountLimitProxies;
using BankSystem.Core.Providers;

namespace BankSystem.Core.Factories
{
    public class Bank
    {
        private readonly IDateTimeProvider _dateTimeProvider;
        
        private readonly Decimal _outLimitForRestricted;
        private readonly Decimal _annualPercentage;
        private readonly Decimal _creditAmount;
        private readonly Decimal _creditCommission;
        private readonly TimeSpan _debitBlockTime;
        private readonly Func<Decimal, Decimal> _debitPercentageByAmountCounter;

        public Bank
        (
            IDateTimeProvider dateTimeProvider,
            Decimal outLimitForRestricted,
            Decimal annualPercentage, 
            Decimal creditAmount,
            Decimal creditCommission,
            TimeSpan debitBlockTime,
            Func<Decimal, Decimal> debitPercentageByAmountCounter)
        {
            _dateTimeProvider = dateTimeProvider;

            _outLimitForRestricted = outLimitForRestricted;
            _annualPercentage = annualPercentage;
            _creditAmount = creditAmount;
            _creditCommission = creditCommission;
            _debitBlockTime = debitBlockTime;
            _debitPercentageByAmountCounter = debitPercentageByAmountCounter;
        }

        public AbstractWallet CreateDebitWallet(Customer customer) =>
            CreateWalletBuilder(customer, 0)
                .SetZeroBalanceLimit()
                .SetPercentageAccrual(_annualPercentage)
                .Build();

        public AbstractWallet CreateDepositWallet(Customer customer, Decimal amount) =>
            CreateWalletBuilder(customer, amount)
                .SetZeroBalanceLimit()
                .SetTransferTimeBlock(_debitBlockTime)
                .SetWithdrawalTimeBlock(_debitBlockTime)
                .SetPercentageAccrual(_debitPercentageByAmountCounter(amount))
                .Build();

        public AbstractWallet CreateCreditWallet(Customer customer) =>
            CreateWalletBuilder(customer, _creditAmount)
                .SetCommissionHold(_creditCommission)
                .Build();

        private WalletBuilder CreateWalletBuilder(Customer customer, Decimal startBalance = 0)
        {
            var builder = new WalletBuilder(_dateTimeProvider);
            builder.SetStartBalance(startBalance);

            if (customer.IsVerified)
                return builder;

            return builder
                .SetTransferAmountLimit(_outLimitForRestricted)
                .SetWithdrawalAmountLimit(_outLimitForRestricted);
        }
    }
}