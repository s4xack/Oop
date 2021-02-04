using System.Collections.Generic;
using System.Linq;
using BankSystem.Core.Exceptions.CommandExceptions;

namespace BankSystem.Core.Commands
{
    public class CommandChain : ICommand
    {
        private readonly List<ICommand> _commands;

        private CommandChain(ICommand startCommand)
        {
            _commands = new List<ICommand> {startCommand};
        }
        
        public static CommandChain StartWith(ICommand startCommand)
        {
            return new CommandChain(startCommand);
        }

        private CommandChain(IEnumerable<ICommand> commands)
        {
            _commands = commands.ToList();
        }


        public CommandChain Then(ICommand command)
        {
            _commands.Add(command);
            return this;
        }
        
        
        public void Execute()
        {
            for (var i = 0; i < _commands.Count; i++)
            {
                try
                {
                    _commands[i].Execute();
                }
                catch (CommandExecutionException exception)
                {
                    try
                    {
                        var revertChain = new CommandChain(_commands.Take(i));
                        revertChain.Revert();
                    }
                    catch (CommandRevertException revertException)
                    {
                        throw exception.WithRevertException(revertException);
                    }
                    throw;
                }
            }
        }

        public void Revert()
        {
            var revertExceptions = new List<CommandRevertException>();
            foreach (var command in _commands.AsEnumerable().Reverse())
            {
                try
                {
                    command.Revert();
                }
                catch (CommandRevertException exception)
                {
                    revertExceptions.Add(exception);
                }
            }
            
            if (revertExceptions.Count > 0)
                throw new CommandRevertException(revertExceptions);
        }
    }
}