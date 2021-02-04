using System;
using BankSystem.Core.Models.WalletTransactions;

namespace BankSystem.Core.Models.Wallets.Decorators
{
    public class CommissionHoldDecorator : WalletDecorator
    {
        private Decimal _commission;

        public CommissionHoldDecorator(AbstractWallet wallet, Decimal commission) : base(wallet)
        {
            _commission = commission;
        }

        public override Decimal Balance => _wallet.Balance;
        internal override void ReduceBalance(Decimal amount)
        {
            _wallet.ReduceBalance(amount);
        }

        internal override void IncreaseBalance(Decimal amount)
        {
            _wallet.IncreaseBalance(amount);
        }

        public override WalletTransferTransaction TransferTo(Decimal amount, AbstractWallet wallet)
        {
            return _wallet.TransferTo(amount, wallet);
        }

        public override DepositTransaction Deposit(Decimal amount)
        {
            return _wallet.Deposit(amount);
        }

        public override WithdrawalTransaction Withdraw(Decimal amount)
        {
            return _wallet.Withdraw(amount);
        }

        public CommissionHoldTransaction HoldCommission()
        {
            if (Balance >= 0)
                throw new InvalidOperationException("Unable to hold commission! Balance is positive!");
            var transaction = new CommissionHoldTransaction(_commission, this);
            transaction.Initiate();
            return transaction;
        }
    }
}