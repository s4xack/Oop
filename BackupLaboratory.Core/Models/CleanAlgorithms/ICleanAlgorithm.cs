using System;
using System.Collections.Generic;
using BackupLaboratory.Core.Models.RestorePoints;

namespace BackupLaboratory.Core.Models.CleanAlgorithms
{
    public interface ICleanAlgorithm
    {
        public Int32 CalculatePointsForClean(List<RestorePoint> restorePoints);
    }
}