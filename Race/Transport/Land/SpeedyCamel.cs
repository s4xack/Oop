using System;

namespace Race.Transport.Land
{
    public class SpeedyCamel : LandVehicle
    {
        public SpeedyCamel() : base(40, 10)
        {
        }

        protected override Double CalculateRestTime(Int32 restCount) =>
            restCount switch
            {
                0 => 0,
                1 => 5,
                _ => (5 + 6.5 + (restCount - 2) * 8)
            };
    }
}