using System;
using BankSystem.Core.Models.WalletTransactions;

namespace BankSystem.Core.Models.Wallets
{
    public abstract class AbstractWallet
    {
        public abstract Guid Id { get; }
        public abstract Decimal Balance { get; }

        internal abstract void ReduceBalance(Decimal amount);
        internal abstract void IncreaseBalance(Decimal amount);

        public abstract WalletTransferTransaction TransferTo(Decimal amount, AbstractWallet wallet);
        public abstract DepositTransaction Deposit(Decimal amount);
        public abstract WithdrawalTransaction Withdraw(Decimal amount);
    }
}