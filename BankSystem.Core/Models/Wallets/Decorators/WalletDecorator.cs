using System;

namespace BankSystem.Core.Models.Wallets.Decorators
{
    public abstract class WalletDecorator : AbstractWallet
    {
        public override Guid Id => _wallet.Id;

        protected readonly AbstractWallet _wallet;

        protected WalletDecorator(AbstractWallet wallet)
        {
            _wallet = wallet;
        }
    }
}