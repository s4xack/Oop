using System;
using BankSystem.Core.Models.WalletTransactions;

namespace BankSystem.Core.Models.Wallets.Proxies
{
    public abstract class WalletProxy : AbstractWallet
    {
        public override Guid Id => _wallet.Id;
        public override Decimal Balance => _wallet.Balance;

        private readonly AbstractWallet _wallet;

        protected WalletProxy(AbstractWallet wallet)
        {
            _wallet = wallet;
        }
        internal override void ReduceBalance(Decimal amount)
        {
            _wallet.ReduceBalance(amount);
        }

        internal override void IncreaseBalance(Decimal amount)
        {
            _wallet.IncreaseBalance(amount);
        }

        public override WalletTransferTransaction TransferTo(Decimal amount, AbstractWallet wallet)
        {
            return _wallet.TransferTo(amount, wallet);
        }

        public override DepositTransaction Deposit(Decimal amount)
        {
            return _wallet.Deposit(amount);
        }

        public override WithdrawalTransaction Withdraw(Decimal amount)
        {
            return _wallet.Withdraw(amount);
        }
    }
}