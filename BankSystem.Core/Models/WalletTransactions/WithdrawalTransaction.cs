using System;
using BankSystem.Core.Commands.BalanceCommands;
using BankSystem.Core.Models.Wallets;

namespace BankSystem.Core.Models.WalletTransactions
{
    public class WithdrawalTransaction : WalletTransaction
    {
        internal WithdrawalTransaction(Decimal amount, AbstractWallet sourceWallet) 
            : base
            (
                amount,
                new ReduceBalanceCommand(sourceWallet, amount)
            )
        {
        }
    }
}