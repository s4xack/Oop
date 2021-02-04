using System;
using BankSystem.Core.Models.WalletTransactions;

namespace BankSystem.Core.Models.Wallets.Proxies.AmountLimitProxies
{
    public class ZeroBalanceLimitProxy : AmountLimitProxy
    {
        public ZeroBalanceLimitProxy(AbstractWallet wallet) : base(wallet, 0)
        {
        }

        internal override void ReduceBalance(Decimal amount)
        {
            CheckLimit(amount - Balance, "Not enough balance!");
            base.ReduceBalance(amount);
        }
        
        public override WalletTransferTransaction TransferTo(Decimal amount, AbstractWallet wallet)
        {
            var transaction = new WalletTransferTransaction(amount, this, wallet);
            transaction.Initiate();
            return transaction;
        }

        public override DepositTransaction Deposit(Decimal amount)
        {
            var transaction = new DepositTransaction(amount, this);
            transaction.Initiate();
            return transaction;
        }
        
        public override WithdrawalTransaction Withdraw(Decimal amount)
        {
            var transaction = new WithdrawalTransaction(amount, this);
            transaction.Initiate();
            return transaction;
        }
    }
}