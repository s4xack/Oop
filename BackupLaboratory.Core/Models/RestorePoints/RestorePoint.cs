using System;
using System.Collections.Generic;
using System.Linq;
using BackupLaboratory.Core.Enums;
using BackupLaboratory.Core.Models.Files;

namespace BackupLaboratory.Core.Models.RestorePoints
{
    public class RestorePoint
    {
        public String Path { get; set; }
        public Int64 Size => Files.Sum(file => file.Size);
        public DateTime CreationTime { get; }
        public List<RestoredFileData> Files { get; }
        public StorageAlgorithmType StorageAlgorithmType { get; }
        public RestoreType RestoreType { get; }
        

        public RestorePoint
        (
            String path,
            IReadOnlyCollection<FileData> files,
            StorageAlgorithmType storageAlgorithmType,
            RestoreType restoreType,
            DateTime creationTime
        )
        {
            Path = path;
            Files = files.Select(file => file.CreateRestoredFile(path)).Where(file => !file.IsNull).ToList();
            CreationTime = creationTime;
            StorageAlgorithmType = storageAlgorithmType;
            RestoreType = restoreType;
        }

        public RestoredFileData GetRestoredFormPath(String sourcePath) =>
            Files.FirstOrDefault(file => file.SourcePath == sourcePath)
            ?? new NullRestoredFileData();

        public virtual PackedFileData ToPackedFileData()
        {
            return new PackedFileData(Path, Size, Files.Cast<FileData>().ToList());
        }
    }
}