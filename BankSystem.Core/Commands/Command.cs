using System;
using BankSystem.Core.Exceptions.CommandExceptions;

namespace BankSystem.Core.Commands
{
    public class Command : ICommand
    {
        private readonly Action _executeAction;
        private readonly Action _revertAction;

        public Command(Action executeAction, Action revertAction)
        {
            _executeAction = executeAction;
            _revertAction = revertAction;
        }

        public void Execute()
        {
            try
            {
                _executeAction();
            }
            catch (Exception exception)
            {
                throw new CommandExecutionException(exception);
            }
        }

        public void Revert()
        {
            try
            {
                _revertAction();
            }
            catch (Exception exception)
            {
                throw new CommandRevertException(exception);
            }
        }
    }
}