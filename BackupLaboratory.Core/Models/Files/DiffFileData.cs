using System;
using BackupLaboratory.Core.Enums;

namespace BackupLaboratory.Core.Models.Files
{
    public class DiffFileData : FileData
    {
        private readonly Int64 _oldSize;
        
        public DiffFileData(String path, Int64 size, Int64 oldSize) : base(path, size)
        {
            _oldSize = oldSize;
        }

        public override RestoredFileData CreateRestoredFile(String restorePath)
        {
            return Size == _oldSize
                ? new NullRestoredFileData()
                : new RestoredFileData(Path, $"{restorePath}/{Name}", Size - _oldSize, FileUpdateType.Diff);
        }
    }
}