using System;

namespace Race.Transport.Air
{
    public class Broom : AirVehicle

    {
        public Broom() : base(20)
        {
        }

        protected override Double ReduceDistance(Double distance) =>
            (Int32)(distance / 1000) switch
            {
                { } percent when percent < 100 => distance * (100 - percent) / 100,
                { } => 0
            };
    }
}