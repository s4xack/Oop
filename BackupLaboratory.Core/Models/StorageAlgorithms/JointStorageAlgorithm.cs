using System;
using System.Collections.Generic;
using BackupLaboratory.Core.Enums;
using BackupLaboratory.Core.Models.Files;
using BackupLaboratory.Core.Models.RestorePoints;

namespace BackupLaboratory.Core.Models.StorageAlgorithms
{
    public class JointStorageAlgorithm : IStorageAlgorithm
    {
        public RestorePoint PackRestorePoint(String backupPath, List<FileData> files, RestoreType restoreType, DateTime creationTime)
        {
            return new JointRestorePoint($"backupPath/RestorePoint_{creationTime:yy-MM-dd_hh-mm-ss}.zip", files, StorageAlgorithmType.Joint, restoreType, creationTime);
        }
    }
}