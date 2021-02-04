using System;

namespace BankSystem.Core.Providers
{
    public interface IDateTimeProvider
    {
        public DateTime UtcNow { get; }
    }
}