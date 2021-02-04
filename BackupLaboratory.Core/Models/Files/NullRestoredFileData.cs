using System;

namespace BackupLaboratory.Core.Models.Files
{
    public class NullRestoredFileData : RestoredFileData
    {
        public NullRestoredFileData() : base(default, default, default, default)
        {
            
        }

        public override Boolean IsNull => true;
    }
}