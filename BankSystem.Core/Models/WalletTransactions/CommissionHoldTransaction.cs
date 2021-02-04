using System;
using BankSystem.Core.Commands;
using BankSystem.Core.Commands.BalanceCommands;
using BankSystem.Core.Models.Wallets;

namespace BankSystem.Core.Models.WalletTransactions
{
    public class CommissionHoldTransaction : WalletTransaction
    {
        public CommissionHoldTransaction(Decimal amount, AbstractWallet sourceWallet) 
            : base
            (
                amount, 
                new ReduceBalanceCommand(sourceWallet, amount)
            )
        {
        }
    }
}