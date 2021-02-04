using System;
using BankSystem.Core.Models.WalletTransactions;

namespace BankSystem.Core.Models.Wallets.Proxies.AmountLimitProxies
{
    public class TransferAmountLimitProxy : AmountLimitProxy
    {
        public TransferAmountLimitProxy(AbstractWallet wallet, Decimal limit) : base(wallet, limit)
        {
        }
        
        public override WalletTransferTransaction TransferTo(Decimal amount, AbstractWallet wallet)
        {
            CheckLimit(amount, "Unable to transfer!");
            return base.TransferTo(amount, wallet);
        }

        
    }
}