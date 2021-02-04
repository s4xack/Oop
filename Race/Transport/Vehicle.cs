using System;

namespace Race.Transport
{
    public abstract class Vehicle
    {
        protected readonly Int32 Speed;

        protected Vehicle(Int32 speed) => Speed = speed;

        public abstract Double Ride(Double distance);
    }
}