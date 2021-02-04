using System;
using System.Collections.Generic;
using System.Linq;
using BackupLaboratory.Core.Models.Providers;
using BackupLaboratory.Core.Models.RestorePoints;

namespace BackupLaboratory.Core.Models.CleanAlgorithms
{
    public class DateCleanAlgorithm : ICleanAlgorithm
    {
        private readonly TimeSpan _timeOffset;

        private readonly DateTimeProvider _dateTimeProvider;

        public DateCleanAlgorithm(TimeSpan timeOffset, DateTimeProvider dateTimeProvider)
        {
            _timeOffset = timeOffset;
            _dateTimeProvider = dateTimeProvider;
        }

        public Int32 CalculatePointsForClean(List<RestorePoint> restorePoints)
        {
            DateTime edgeTime = _dateTimeProvider.GetUtcNow() - _timeOffset;
            return restorePoints.Count(point => point.CreationTime < edgeTime);
        }
            
    }
}