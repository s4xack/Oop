using System;
using System.Collections.Generic;
using System.Linq;
using BackupLaboratory.Core.Enums;
using BackupLaboratory.Core.Models.RestorePoints;

namespace BackupLaboratory.Core.Models.CleanAlgorithms
{
    public class HybridCleanAlgorithm : ICleanAlgorithm
    {
        private readonly List<ICleanAlgorithm> _cleanAlgorithms;
        private readonly HybridType _hybridType;

        public HybridCleanAlgorithm(List<ICleanAlgorithm> cleanAlgorithms, HybridType hybridType)
        {
            _cleanAlgorithms = cleanAlgorithms ?? throw new ArgumentNullException(nameof(cleanAlgorithms));
            _hybridType = hybridType;
        }

        public Int32 CalculatePointsForClean(List<RestorePoint> restorePoints)
        {
            var algoResults = _cleanAlgorithms
                .Select(algo => algo.CalculatePointsForClean(restorePoints));
            return _hybridType switch
            {
                HybridType.All => algoResults
                    .Min(),
                HybridType.Any => algoResults
                    .Max(),
                _ => throw new NotSupportedException()
            };
        }
    }
}