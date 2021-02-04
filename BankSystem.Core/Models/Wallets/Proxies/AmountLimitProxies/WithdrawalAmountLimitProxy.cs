using System;
using BankSystem.Core.Models.WalletTransactions;

namespace BankSystem.Core.Models.Wallets.Proxies.AmountLimitProxies
{
    public class WithdrawalAmountLimitProxy : AmountLimitProxy
    {
        public WithdrawalAmountLimitProxy(AbstractWallet wallet, Decimal limit) : base(wallet, limit)
        {
        }

        public override WithdrawalTransaction Withdraw(Decimal amount)
        {
            CheckLimit(amount, "Unable to withdraw!");
            return base.Withdraw(amount);
        }

        
    }
}