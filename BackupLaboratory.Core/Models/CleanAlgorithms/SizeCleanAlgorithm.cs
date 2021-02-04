using System;
using System.Collections.Generic;
using System.Linq;
using BackupLaboratory.Core.Models.RestorePoints;

namespace BackupLaboratory.Core.Models.CleanAlgorithms
{
    public class SizeCleanAlgorithm : ICleanAlgorithm
    {
        private readonly Int64 _maxSize;

        public SizeCleanAlgorithm(Int64 maxSize)
        {
            _maxSize = maxSize;
        }

        public Int32 CalculatePointsForClean(List<RestorePoint> restorePoints)
        {
            Int64 totalSize = restorePoints.Sum(point => point.Size);
            Int32 index = 0;
            while (totalSize > _maxSize)
            {
                index++;
                totalSize -= restorePoints[index].Size;
            }
            return index;
        }
    }
}