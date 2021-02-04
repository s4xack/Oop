using System;

namespace BankSystem.Core.Exceptions.CommandExceptions
{
    public class CommandChainExecutionException : CommandExecutionException
    {
        public CommandRevertException RevertException { get; }
        public CommandChainExecutionException(Exception innerException, CommandRevertException revertException) 
            : base(innerException)
        {
            RevertException = revertException;
        }

        public override CommandChainExecutionException WithRevertException(CommandRevertException revertException)
        {
            return new CommandChainExecutionException(InnerException, RevertException.Collapse(revertException));
        }
    }
}