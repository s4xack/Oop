using System;
using BankSystem.Core.Models.WalletTransactions;

namespace BankSystem.Core.Models.Wallets
{
    public class Wallet : AbstractWallet
    {
        public override Guid Id { get; }
        public override Decimal Balance => _balance;
        private Decimal _balance;


        internal Wallet(Decimal balance)
        {
            Id = Guid.NewGuid();
            _balance = balance;
        }
        
        internal override void ReduceBalance(Decimal amount)
        {
            _balance -= amount;
        }

        internal override void IncreaseBalance(Decimal amount)
        {
            _balance += amount;
        }

        public override WalletTransferTransaction TransferTo(Decimal amount, AbstractWallet wallet)
        {
            var transaction = new WalletTransferTransaction(amount, this, wallet);
            transaction.Initiate();
            return transaction;
        }

        public override DepositTransaction Deposit(Decimal amount)
        {
            var transaction = new DepositTransaction(amount, this);
            transaction.Initiate();
            return transaction;
        }
        
        public override WithdrawalTransaction Withdraw(Decimal amount)
        {
            var transaction = new WithdrawalTransaction(amount, this);
            transaction.Initiate();
            return transaction;
        }
    }
}