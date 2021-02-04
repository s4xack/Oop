using System;
using BankSystem.Core.Providers;

namespace BankSystem.Core.Models.Wallets.Proxies.TimeBlockProxies
{
    public abstract class TimeBlockProxy : WalletProxy
    {
        private readonly DateTime _creationTime;
        private readonly TimeSpan _blockTime;

        private readonly IDateTimeProvider _dateTimeProvider;

        protected TimeBlockProxy(AbstractWallet wallet, IDateTimeProvider dateTimeProvider, TimeSpan blockTime) : base(wallet)
        {
            _dateTimeProvider = dateTimeProvider;
            _blockTime = blockTime;
            _creationTime = _dateTimeProvider.UtcNow;
        }

        protected void CheckTimeBlock(String exceptionMessage)
        {
            if (_creationTime + _blockTime > _dateTimeProvider.UtcNow)
                throw new InvalidOperationException($"{exceptionMessage} Blocking time has not expired!");
        }
    }
}