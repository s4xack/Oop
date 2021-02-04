using System;
using BankSystem.Core.Commands;
using BankSystem.Core.Exceptions.CommandExceptions;
using NUnit.Framework;

namespace BankSystem.Core.Tests.Commands
{
    [TestFixture]
    public class CommandChainTest
    {
        [Test]
        public void Execute_ShouldCompleteAction()
        {
            var result = 0;
            var expected = 10;
            var command = new Command(() => result += expected / 2, () => result -= expected / 2);
            
            CommandChain.StartWith(command).Then(command).Execute();
            
            Assert.That(result, Is.EqualTo(expected));
        }

        [Test]
        public void Revert_ShouldCompleteRevertAction()
        {
            var result = 0;
            var expected = result;
            var command = new Command(() => result +=  2, () => result -=  2);
            
            var chain = CommandChain.StartWith(command).Then(command);
            chain.Execute();
            chain.Revert();
            
            Assert.That(result, Is.EqualTo(expected));
        }
        
        [Test]
        public void Execute_ShouldThrowExceptionAndRevertAction()
        {
            var result = 0;
            var expected = result;
            var command = new Command(() => result +=  2, () => result -=  2);
            var badCommand =  new Command(() => throw new InvalidOperationException(), () => throw new InvalidOperationException());
            
            var chain = CommandChain.StartWith(command).Then(badCommand);

            Assert.Throws<CommandExecutionException>(chain.Execute);
            Assert.That(result, Is.EqualTo(expected));
        }

        [Test]
        public void ExecuteWithRevertException_ShouldThrowExceptionAndRevertAction()
        {
            var result = 0;
            var expected = 2;
            var command = new Command(() => result +=  2, () => result -=  2);
            var badRevertCommand = new Command(() => result +=  2, () => throw new InvalidOperationException());
            var badCommand =  new Command(() => throw new InvalidOperationException(), () => throw new InvalidOperationException());
            
            var chain = CommandChain.StartWith(command).Then(badRevertCommand).Then(badCommand);

            Assert.Throws<CommandChainExecutionException>(chain.Execute);
            Assert.That(result, Is.EqualTo(expected));
        }
        
        [Test]
        public void ExecuteWithRevertExceptions_ShouldThrowExceptionWithCorrectRevertExceptionsAndRevertAction()
        {
            var result = 0;
            var expected = 4;
            var command = new Command(() => result +=  2, () => result -=  2);
            var badRevertCommand = new Command(() => result +=  2, () => throw new InvalidOperationException());
            var badCommand =  new Command(() => throw new InvalidOperationException(), () => throw new InvalidOperationException());
            
            var chain = CommandChain.StartWith(command).Then(badRevertCommand).Then(badRevertCommand).Then(badCommand);

            try
            {
                chain.Execute();
            }
            catch (CommandChainExecutionException exception)
            {
                Assert.That(exception.RevertException.InnerExceptions.Count, Is.EqualTo(2));
            }
            
            Assert.That(result, Is.EqualTo(expected));
        }
    }
}