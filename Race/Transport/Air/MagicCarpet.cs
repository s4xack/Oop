using System;

namespace Race.Transport.Air
{
    public class MagicCarpet : AirVehicle
    {
        public MagicCarpet() : base(10)
        {
        }

        protected override Double ReduceDistance(Double distance) =>
            true switch
            {
                { } when distance < 1000 => distance,
                { } when distance < 5000 => distance * 0.97,
                { } when distance < 10000 => distance * 0.90,
                { } => distance * 0.95
            };
    }
}