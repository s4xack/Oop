using System;
using BankSystem.Core.Models.Wallets.Proxies;
using BankSystem.Core.Models.WalletTransactions;
using BankSystem.Core.Providers;

namespace BankSystem.Core.Models.Wallets.Decorators
{
    public class PercentageAccrualDecorator : WalletDecorator
    {
        private readonly Decimal _dayPercentage;
        
        private DateTime _lastUpdateTime;
        private Decimal _accumulation;

        private readonly IDateTimeProvider _dateTimeProvider;
        
        public PercentageAccrualDecorator(AbstractWallet wallet, IDateTimeProvider dateTimeProvider, Decimal annualPercentage) : base(wallet)
        {
            _dayPercentage = annualPercentage / 365 / 100;
            
            _dateTimeProvider = dateTimeProvider;
            _lastUpdateTime = _dateTimeProvider.UtcNow;
            _accumulation = 0;
        }


        public override Decimal Balance
        {
            get
            {
                UpdatePercentage();
                return _wallet.Balance;
            }
        }

        internal override void ReduceBalance(Decimal amount)
        {
            UpdatePercentage();
            _wallet.ReduceBalance(amount);
        }

        internal override void IncreaseBalance(Decimal amount)
        {
            UpdatePercentage();
            _wallet.IncreaseBalance(amount);
        }

        public override WalletTransferTransaction TransferTo(Decimal amount, AbstractWallet wallet)
        {
            UpdatePercentage();
            return _wallet.TransferTo(amount, wallet);
        }

        public override DepositTransaction Deposit(Decimal amount)
        {
            UpdatePercentage();
            return _wallet.Deposit(amount);
        }

        public override WithdrawalTransaction Withdraw(Decimal amount)
        {
            UpdatePercentage();
            return _wallet.Withdraw(amount);
        }

        private void UpdatePercentage()
        {
            var dayDiff = (_dateTimeProvider.UtcNow - _lastUpdateTime).Days;
            if (dayDiff < 1)
                return;
            
            _lastUpdateTime = _dateTimeProvider.UtcNow;
            _accumulation += _wallet.Balance * _dayPercentage * dayDiff;
        }

        public PercentageAccrualTransaction AccruePercentage()
        {
            UpdatePercentage();
            var transaction = new PercentageAccrualTransaction(_accumulation, this);
            transaction.Initiate();
            _accumulation = 0;
            return transaction;
        }
    }
}