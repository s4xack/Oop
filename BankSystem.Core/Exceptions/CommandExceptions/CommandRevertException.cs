using System;
using System.Collections.Generic;
using System.Linq;

namespace BankSystem.Core.Exceptions.CommandExceptions
{
    public class CommandRevertException : AggregateException
    {
        private const String MessageTemplate = "Command revert failed!";
        public CommandRevertException(Exception innerException)
            : base(MessageTemplate, innerException)
        {
        }

        public CommandRevertException(IEnumerable<CommandRevertException> revertExceptions)
            : base(MessageTemplate, revertExceptions.SelectMany(revertException => revertException.InnerExceptions))
        {
        }

        private CommandRevertException(IEnumerable<Exception> innerExceptions)
            : base(MessageTemplate, innerExceptions)
        {
        }

        public CommandRevertException Collapse(CommandRevertException revertException)
        {
            return new CommandRevertException(InnerExceptions.Concat(revertException.InnerExceptions));
        }
    }
}