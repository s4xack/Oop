using System;
using BankSystem.Core.Commands;
using BankSystem.Core.Exceptions.CommandExceptions;
using NUnit.Framework;

namespace BankSystem.Core.Tests.Commands
{
    [TestFixture]
    public class CommandTests
    {
        [Test]
        public void Execute_ShouldCompleteAction()
        {
            var result = 0;
            var expected = 10;
            var command = new Command(() => result = expected, () => result = expected);
            
            command.Execute();
            
            Assert.That(result, Is.EqualTo(expected));
        }
        
        [Test]
        public void Revert_ShouldCompleteRevertAction()
        {
            var result = 0;
            var expected = result;
            var command = new Command(() => result += 2, () => result -= 2);
            
            command.Execute();
            command.Revert();
            
            Assert.That(result, Is.EqualTo(expected));
        }

        [Test]
        public void Execute_ShouldThrowException()
        {
            var command = new Command(() => throw new InvalidOperationException(), () => throw new InvalidOperationException());
            Assert.Throws<CommandExecutionException>(command.Execute);
        }
        
        [Test]
        public void Revert_ShouldThrowException()
        {
            var command = new Command(() => throw new InvalidOperationException(), () => throw new InvalidOperationException());
            Assert.Throws<CommandRevertException>(command.Revert);
        }
    }
}