using System;
using System.Collections.Generic;
using System.Linq;
using BackupLaboratory.Core.Enums;
using BackupLaboratory.Core.Models.Files;

namespace BackupLaboratory.Core.Models.RestorePoints
{
    public class JointRestorePoint : RestorePoint
    {
        public JointRestorePoint
        (
            String path,
            IReadOnlyCollection<FileData> files,
            StorageAlgorithmType storageAlgorithmType,
            RestoreType restoreType,
            DateTime creationTime
        ) : base
        (
            path,
            files,
            storageAlgorithmType,
            restoreType,
            creationTime
        )
        {
        }

        public override PackedFileData ToPackedFileData()
        {
            return new ArchivedFileData(Path, Size, Files.Cast<FileData>().ToList());
        }
    }
}