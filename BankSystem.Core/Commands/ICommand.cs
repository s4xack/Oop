namespace BankSystem.Core.Commands
{
    public interface ICommand
    {
        public void Execute();
        public void Revert();
    }
}