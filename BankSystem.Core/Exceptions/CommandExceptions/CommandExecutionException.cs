using System;

namespace BankSystem.Core.Exceptions.CommandExceptions
{
    public class CommandExecutionException : Exception
    {
        private const String MessageTemplate = "Command execution failed!";

        public CommandExecutionException(Exception innerException)
            : base(MessageTemplate, innerException)
        {
        }

        public virtual CommandChainExecutionException WithRevertException(CommandRevertException revertException)
        {
            return new CommandChainExecutionException(InnerException, revertException);
        }
    }
}