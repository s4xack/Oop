using System;

namespace Race.Transport.Land
{
    public class Centaur : LandVehicle
    {
        public Centaur() : base(15, 8)
        {
        }

        protected override Double CalculateRestTime(Int32 restCount) => restCount * 2;
    }
}