using System;

namespace BankSystem.Core.Exceptions.TransactionExceptions
{
    public class TransactionExecutionException : Exception
    {
        private const String MessageTemplate = "Transaction execution failed!";

        public TransactionExecutionException(Exception innerException)
            : base(MessageTemplate, innerException)
        {
        }
    }
}