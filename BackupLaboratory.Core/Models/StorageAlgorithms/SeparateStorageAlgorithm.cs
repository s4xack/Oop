using System;
using System.Collections.Generic;
using BackupLaboratory.Core.Enums;
using BackupLaboratory.Core.Models.Files;
using BackupLaboratory.Core.Models.RestorePoints;

namespace BackupLaboratory.Core.Models.StorageAlgorithms
{
    public class SeparateStorageAlgorithm : IStorageAlgorithm
    {
        public RestorePoint PackRestorePoint(String backupPath, List<FileData> files, RestoreType restoreType, DateTime creationTime)
        {
            return new RestorePoint($"backupPath/RestorePoint_{creationTime:yy-MM-dd_hh-mm-ss}", files, StorageAlgorithmType.Separate, restoreType, creationTime);
        }
    }
}