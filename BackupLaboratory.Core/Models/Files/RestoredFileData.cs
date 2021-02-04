using System;
using BackupLaboratory.Core.Enums;

namespace BackupLaboratory.Core.Models.Files
{
    public class RestoredFileData : FileData
    {
        public String SourcePath { get; }
        public FileUpdateType UpdateType { get; }

        public RestoredFileData(String sourcePath, String path, Int64 size, FileUpdateType updateType) : base(path, size)
        {
            SourcePath = sourcePath;
            UpdateType = updateType;
        }
        
        public virtual Boolean IsNull => false;
    }
}