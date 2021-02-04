using System;
using BankSystem.Core.Models.Wallets;

namespace BankSystem.Core.Commands.BalanceCommands
{
    public class IncreaseBalanceCommand : Command
    {
        public IncreaseBalanceCommand(AbstractWallet wallet, Decimal amount) 
            : base
            (
                () => wallet.IncreaseBalance(amount), 
                () => wallet.ReduceBalance(amount)
            )
        {
        }
    }
}