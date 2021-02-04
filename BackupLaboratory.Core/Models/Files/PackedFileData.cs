using System;
using System.Collections.Generic;

namespace BackupLaboratory.Core.Models.Files
{
    public class PackedFileData : FileData
    {
        public List<FileData> Files { get; }
        
        public PackedFileData(String path, Int64 size, List<FileData> files) : base(path, size)
        {
            Files = files;
        }
    }
}