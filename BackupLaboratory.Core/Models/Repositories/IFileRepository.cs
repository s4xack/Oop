using System;
using BackupLaboratory.Core.Models.Files;

namespace BackupLaboratory.Core.Models.Repositories
{
    public interface IFileRepository
    {
        public FileData Read(String path);
        public void Remove(String path);
        public void Write(FileData file);
    }
}