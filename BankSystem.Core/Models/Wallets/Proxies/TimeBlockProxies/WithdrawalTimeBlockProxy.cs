using System;
using BankSystem.Core.Models.WalletTransactions;
using BankSystem.Core.Providers;

namespace BankSystem.Core.Models.Wallets.Proxies.TimeBlockProxies
{
    public class WithdrawalTimeBlockProxy : TimeBlockProxy
    {
        public WithdrawalTimeBlockProxy(AbstractWallet wallet, IDateTimeProvider dateTimeProvider, TimeSpan blockTime) : base(wallet, dateTimeProvider, blockTime)
        {
        }
        
        public override WithdrawalTransaction Withdraw(Decimal amount)
        {
            CheckTimeBlock("Unable to withdraw!");
            return base.Withdraw(amount);
        }
    }
}