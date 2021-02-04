using System;

namespace BackupLaboratory.Core.Models.Providers
{
    public class DateTimeProvider
    {
        public virtual DateTime GetUtcNow()
        {
            return DateTime.UtcNow;
        }
    }
}