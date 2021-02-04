using System;
using BankSystem.Core.Models.Wallets;
using BankSystem.Core.Models.Wallets.Decorators;
using BankSystem.Core.Models.Wallets.Proxies.AmountLimitProxies;
using BankSystem.Core.Models.Wallets.Proxies.TimeBlockProxies;
using BankSystem.Core.Providers;

namespace BankSystem.Core.Builders
{
    public class WalletBuilder
    {
        private AbstractWallet _wallet;
        private readonly IDateTimeProvider _dateTimeProvider;

        public WalletBuilder(IDateTimeProvider dateTimeProvider)
        {
            _wallet = new Wallet(0);
            _dateTimeProvider = dateTimeProvider;
        }

        public WalletBuilder SetStartBalance(Decimal startBalance)
        {
            _wallet = new Wallet(startBalance);
            return this;
        }

        public WalletBuilder Refresh()
        {
            _wallet = new Wallet(0);
            return this;
        }

        public AbstractWallet Build()
        {
            var wallet = _wallet;
            Refresh();
            return wallet;
        }

        public WalletBuilder SetTransferAmountLimit(Decimal amountLimit)
        {
            _wallet = new TransferAmountLimitProxy(_wallet, amountLimit);
            return this;
        }
        
        public WalletBuilder SetWithdrawalAmountLimit(Decimal amountLimit)
        {
            _wallet = new WithdrawalAmountLimitProxy(_wallet, amountLimit);
            return this;
        }
        
        public WalletBuilder SetZeroBalanceLimit()
        {
            _wallet = new ZeroBalanceLimitProxy(_wallet);
            return this;
        }
        
        public WalletBuilder SetTransferTimeBlock(TimeSpan blockTime)
        {
            _wallet = new TransferTimeBlockProxy(_wallet, _dateTimeProvider, blockTime);
            return this;
        }
        
        public WalletBuilder SetWithdrawalTimeBlock(TimeSpan blockTime)
        {
            _wallet = new WithdrawalTimeBlockProxy(_wallet, _dateTimeProvider, blockTime);
            return this;
        }

        public WalletBuilder SetPercentageAccrual(Decimal annualPercentage)
        {
            _wallet = new PercentageAccrualDecorator(_wallet, _dateTimeProvider, annualPercentage);
            return this;
        }

        public WalletBuilder SetCommissionHold(Decimal commissionAmount)
        {
            _wallet = new CommissionHoldDecorator(_wallet, commissionAmount);
            return this;
        }
    }
}