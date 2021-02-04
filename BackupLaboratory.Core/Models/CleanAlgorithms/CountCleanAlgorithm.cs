using System;
using System.Collections.Generic;
using BackupLaboratory.Core.Models.RestorePoints;

namespace BackupLaboratory.Core.Models.CleanAlgorithms
{
    public class CountCleanAlgorithm : ICleanAlgorithm
    {
        private readonly Int32 _maxCount;

        public CountCleanAlgorithm(Int32 maxCount)
        {
            _maxCount = maxCount;
        }

        public Int32 CalculatePointsForClean(List<RestorePoint> restorePoints) =>
            Math.Max(0, restorePoints.Count - _maxCount);
    }
}