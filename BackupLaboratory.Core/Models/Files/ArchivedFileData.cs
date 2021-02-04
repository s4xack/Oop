using System;
using System.Collections.Generic;

namespace BackupLaboratory.Core.Models.Files
{
    public class ArchivedFileData : PackedFileData
    {
        public ArchivedFileData(String path, Int64 size, List<FileData> files) : base(path, size, files)
        {
        }
    }
}