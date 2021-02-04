using System;
using System.Collections.Generic;
using System.Linq;
using BackupLaboratory.Core.Enums;

namespace BackupLaboratory.Core.Models.Files
{
    public class FileData
    {
        public String Path { get; }
        protected String Name => Path.Split("/").Last(); 
        public Int64 Size { get; private set; }

        public FileData(String path, Int64 size)
        {
            Path = path;
            Size = size;
        }
        
        public virtual DiffFileData GetDiff(FileData targetFile, List<FileData> intermediateDiffs) =>
            new DiffFileData(Path, Size, targetFile.Size + intermediateDiffs.Sum(diff => diff.Size));
        
        public virtual RestoredFileData CreateRestoredFile(String restorePath)
        {
            return new RestoredFileData(Path, $"{restorePath}/{Name}", Size, FileUpdateType.Init);
        }
    }
}