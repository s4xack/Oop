using System;

namespace Race.Transport.Air
{
    public class Mortar : AirVehicle
    {
        public Mortar() : base(8)
        {
        }

        protected override Double ReduceDistance(Double distance) => distance * 0.94;
    }
}