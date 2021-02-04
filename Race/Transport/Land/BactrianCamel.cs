using System;

namespace Race.Transport.Land
{
    public class BactrianCamel : LandVehicle
    {
        public BactrianCamel() : base(10, 30)
        {
        }

        protected override Double CalculateRestTime(Int32 restCount) =>
            restCount switch
            {
                0 => 0,
                _ => 5 + (restCount - 1) * 8
            };
    }
}