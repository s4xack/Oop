using System;
using System.Collections.Generic;
using System.Linq;
using Race.Transport;

namespace Race
{
    public class Race<TVehicle> where TVehicle : Vehicle
    {
        private readonly List<TVehicle> _vehicles;

        public Race() => _vehicles = new List<TVehicle>();

        public void Register(TVehicle vehicle) => _vehicles.Add(vehicle);

        public TVehicle Start(Double distance) =>
            _vehicles
                .AsParallel()
                .OrderBy(v => v.Ride(distance))
                .FirstOrDefault() ?? throw new InvalidOperationException("Unable to start race without vehicles!");
    }
}