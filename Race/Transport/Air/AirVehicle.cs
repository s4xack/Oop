using System;

namespace Race.Transport.Air
{
    public abstract class AirVehicle : Vehicle
    {
        protected AirVehicle(Int32 speed) : base(speed)
        {
        }

        public override Double Ride(Double distance) => ReduceDistance(distance) / Speed;

        protected abstract Double ReduceDistance(Double distance);
    }
}