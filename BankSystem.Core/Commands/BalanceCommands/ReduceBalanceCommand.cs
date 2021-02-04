using System;
using BankSystem.Core.Models.Wallets;

namespace BankSystem.Core.Commands.BalanceCommands
{
    public class ReduceBalanceCommand : Command
    {
        public ReduceBalanceCommand(AbstractWallet wallet, Decimal amount) 
            : base
            (
                () => wallet.ReduceBalance(amount),
                () => wallet.IncreaseBalance(amount)
            )
        {
        }
    }
}