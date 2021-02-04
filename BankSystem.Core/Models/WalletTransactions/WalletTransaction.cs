using System;
using BankSystem.Core.Commands;
using BankSystem.Core.Enums;
using BankSystem.Core.Exceptions.TransactionExceptions;

namespace BankSystem.Core.Models.WalletTransactions
{
    public abstract class WalletTransaction
    {
        public Guid Id { get; }
        
        public Decimal Amount { get; }
        
        public WalletTransactionStatus Status { get; private set; }
        
        private readonly ICommand _command;

        internal WalletTransaction(Decimal amount, ICommand command)
        {
            Id = Guid.NewGuid();
            Amount = amount;
            _command = command;
            Status = WalletTransactionStatus.Pending;
        }

        public void Initiate() 
        {
            if (Status != WalletTransactionStatus.Pending)
                throw new InvalidOperationException($"Unable to initiate transaction {Id}.");
            try
            {
                _command.Execute();
            }
            catch (Exception exception)
            {
                Status = WalletTransactionStatus.Failed;
                throw new TransactionExecutionException(exception);
            }
            Status = WalletTransactionStatus.Completed;
        }

        public void Revert()
        {
            if (Status != WalletTransactionStatus.Completed)
                throw new InvalidOperationException($"Unable to invert transaction {Id}.");

            try
            {
                _command.Revert();
            }
            catch (Exception exception)
            {
                Status = WalletTransactionStatus.RevertFailed;
                throw new TransactionRevertException(exception);
            }
            
            Status = WalletTransactionStatus.Reverted;
        }
    }
}