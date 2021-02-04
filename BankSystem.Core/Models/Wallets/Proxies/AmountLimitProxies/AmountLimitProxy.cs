using System;

namespace BankSystem.Core.Models.Wallets.Proxies.AmountLimitProxies
{
    public abstract class AmountLimitProxy : WalletProxy
    {
        private Decimal _limit;

        protected AmountLimitProxy(AbstractWallet wallet, Decimal limit) : base(wallet)
        {
            _limit = limit;
        }

        protected void CheckLimit(Decimal amount, String exceptionMessage)
        {
            if (amount > _limit)
                throw new InvalidOperationException($"{exceptionMessage} Limit is exceeded!");
        }
    }
}