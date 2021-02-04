using System;

namespace BankSystem.Core.Exceptions.TransactionExceptions
{
    public class TransactionRevertException : Exception
    {
        private const String MessageTemplate = "Transaction revert failed!";

        public TransactionRevertException(Exception innerException)
            : base(MessageTemplate, innerException)
        {
        }
    }
}