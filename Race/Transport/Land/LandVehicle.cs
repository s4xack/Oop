using System;

namespace Race.Transport.Land
{
    public abstract class LandVehicle : Vehicle
    {
        private readonly Double _restInterval;

        protected LandVehicle(Int32 speed, Int32 restInterval) : base(speed) => 
            _restInterval = restInterval;

        private Int32 CalculateRestNumber(Double rideTime) =>
            (Int32) (rideTime / _restInterval) switch
            {
                { } restTime when restTime == 0 => 0,
                { } restCount when Math.Abs(restCount * _restInterval - rideTime) < 0.00001 => restCount - 1,
                { } restCount => restCount
            };

        public override Double Ride(Double distance) => 
            distance / Speed + CalculateRestTime(CalculateRestNumber(distance / Speed));

        protected abstract Double CalculateRestTime(Int32 time);
    }
}