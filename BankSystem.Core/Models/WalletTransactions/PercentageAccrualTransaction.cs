using System;
using BankSystem.Core.Commands;
using BankSystem.Core.Commands.BalanceCommands;
using BankSystem.Core.Models.Wallets;

namespace BankSystem.Core.Models.WalletTransactions
{
    public class PercentageAccrualTransaction : WalletTransaction
    {

        public PercentageAccrualTransaction(Decimal amount, AbstractWallet destinationWallet) 
            : base
                (
                amount,
                new IncreaseBalanceCommand(destinationWallet, amount)
                )
        {
        }
    }
}