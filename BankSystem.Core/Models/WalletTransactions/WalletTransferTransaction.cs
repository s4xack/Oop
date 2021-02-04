using System;
using BankSystem.Core.Commands;
using BankSystem.Core.Commands.BalanceCommands;
using BankSystem.Core.Models.Wallets;

namespace BankSystem.Core.Models.WalletTransactions
{
    public class WalletTransferTransaction : WalletTransaction
    {
        internal WalletTransferTransaction(Decimal amount, AbstractWallet sourceWallet, AbstractWallet destinationWallet) 
            : base
            (
                amount,
                CommandChain
                    .StartWith(new ReduceBalanceCommand(sourceWallet, amount))
                    .Then(new IncreaseBalanceCommand(destinationWallet, amount))
            )        
        
        {
        }
    }
}