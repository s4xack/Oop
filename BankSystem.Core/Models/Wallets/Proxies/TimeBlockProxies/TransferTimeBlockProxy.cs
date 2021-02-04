using System;
using BankSystem.Core.Models.WalletTransactions;
using BankSystem.Core.Providers;

namespace BankSystem.Core.Models.Wallets.Proxies.TimeBlockProxies
{
    public class TransferTimeBlockProxy : TimeBlockProxy
    {
        public TransferTimeBlockProxy(AbstractWallet wallet, IDateTimeProvider dateTimeProvider, TimeSpan blockTime) : base(wallet, dateTimeProvider, blockTime)
        {
        }

        public override WalletTransferTransaction TransferTo(Decimal amount, AbstractWallet wallet)
        {
            CheckTimeBlock("Unable to transfer!");
            return base.TransferTo(amount, wallet);
        }       
    }
}