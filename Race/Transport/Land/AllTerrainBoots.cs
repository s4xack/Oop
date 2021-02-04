using System;

namespace Race.Transport.Land
{
    public class AllTerrainBoots : LandVehicle
    {
        public AllTerrainBoots() : base(6, 60)
        {
        }

        protected override Double CalculateRestTime(Int32 restCount) =>
            restCount switch
            {
                0 => 0,
                _ => 10 + (restCount - 1) * 5
            };
    }
}